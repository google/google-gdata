using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;

using Google.GData.Client;
using Google.GData.YouTube;
using Google.GData.Tools;
using Google.YouTube;
using Google.GData.Extensions.MediaRss;
using Google.GData.Client.ResumableUpload;


/// this file contains the CSV related support code
namespace YouTubeUploader {
    public partial class YouTubeUploader : Form {
        private ResumableUploader ru = null;

        /// <summary>
        /// userstate is the object that is passed to the async code to identify a particular upload
        /// this object remembers the http operation, the row in the spreadsheet, progress, etc.
        /// This object get's added to the the current queue, or the retryqueue
        /// </summary>
        internal class UserState {
            private DataGridViewRow row;
            private long currentPosition;
            private string httpVerb;
            private Uri resumeUri;
            private int retryCounter;
            private string errorText;

            internal UserState(DataGridViewRow r) {
                this.row = r;
                this.currentPosition = 0;
                this.retryCounter = 0;
            }

            internal DataGridViewRow Row {
                get {
                    return this.row;
                }
            }

            internal long CurrentPosition {
                get {
                    return this.currentPosition;
                }
                set {
                    this.currentPosition = value;
                }
            }

            internal string Error {
                get {
                    return this.errorText;
                }
                set {
                    this.errorText = value;
                }
            }

            internal int RetryCounter {
                get {
                    return this.retryCounter;
                }
                set {
                    this.retryCounter = value;
                }
            }

            internal string HttpVerb {
                get {
                    return this.httpVerb;
                }
                set {
                    this.httpVerb = value;
                }
            }

            internal Uri ResumeUri {
                get {
                    return this.resumeUri;
                }
                set {
                    this.resumeUri = value;
                }
            }
        }

        // helper to create a ResumableUploader object and setup the event handlers
        private void EnsureRU() {
            this.ru = new ResumableUploader((int)this.ChunkSize.Value);
            this.ru.AsyncOperationCompleted += new AsyncOperationCompletedEventHandler(this.OnDone);
            this.ru.AsyncOperationProgress += new AsyncOperationProgressEventHandler(this.OnProgress);
        }

        // retry one row. The userstate was removed from the retry queue before that call
        private bool RetryRow(UserState us) {
            if (us != null) {
                Trace.Indent();
                Trace.TraceInformation("Retrying a row, current position is: " + us.CurrentPosition + "for uri: " + us.ResumeUri);
                Trace.Unindent();
                if (us.CurrentPosition > 0 && us.ResumeUri != null) {
                    string contentType = MediaFileSource.GetContentTypeForFileName(us.Row.Cells[COLUMNINDEX_FILENAME].Value.ToString());
                    MediaFileSource mfs = new MediaFileSource(us.Row.Cells[COLUMNINDEX_FILENAME].Value.ToString(), contentType);

                    Stream s = mfs.GetDataStream();
                    s.Seek(us.CurrentPosition, SeekOrigin.Begin);

                    lock (this.flag) {
                        this.queue.Add(us);
                    }
                    us.Row.Cells[COLUMNINDEX_STATUS].Value = "Retrying (" + us.RetryCounter + ") - Last error was: " + us.Error;
                    ru.ResumeAsync(this.youTubeAuthenticator, us.ResumeUri, us.HttpVerb, s, contentType, us);

                    return true;
                }

                // else treat this as a new one, a resume from null
                return InsertVideo(us.Row, us.RetryCounter + 1);
            }
            return false;
        }

        /// <summary>
        /// insert a new video. 
        /// </summary>
        /// <param name="row"></param>
        /// <param name="retryCounter"></param>
        /// <returns></returns>
        private bool InsertVideo(DataGridViewRow row, int retryCounter) {
            Trace.TraceInformation("Entering InsertVideo: starting a new upload");
            Video v = new Video();
            v.Title = row.Cells[0].Value.ToString();
            v.Description = row.Cells[1].Value.ToString();
            v.Keywords = row.Cells[2].Value.ToString();
            v.Tags.Add(new MediaCategory(row.Cells[3].Value.ToString()));
            v.Private = row.Cells[4].Value.ToString().ToLower() == "true";

            string contentType = MediaFileSource.GetContentTypeForFileName(row.Cells[COLUMNINDEX_FILENAME].Value.ToString());
            v.MediaSource = new MediaFileSource(row.Cells[COLUMNINDEX_FILENAME].Value.ToString(), contentType);

            // add the upload uri to it
            AtomLink link = new AtomLink("http://uploads.gdata.youtube.com/resumable/feeds/api/users/" + this.youtubeAccount + "/uploads");
            link.Rel = ResumableUploader.CreateMediaRelation;
            v.YouTubeEntry.Links.Add(link);

            UserState u = new UserState(row);
            u.RetryCounter = retryCounter;

            lock (this.flag) {
                this.queue.Add(u);
            }
            ru.InsertAsync(this.youTubeAuthenticator, v.YouTubeEntry, u);

            row.Cells[COLUMNINDEX_STATUS].Value = "Queued up...";
            row.Cells[COLUMNINDEX_STATUS].Tag = u;

            return true;
        }

        // when an upload is completed successfully we parse the return stream
        // to setup the videoid.
        // also all retry counts will be reset to 0, as a successful upload incidates
        // a restored network condition (e.g.).
        private void ParseAndFinish(UserState u, Stream s) {
            YouTubeRequestSettings ys = new YouTubeRequestSettings(YouTubeUploader.ApplicationName,
                this.developerKey);
            YouTubeRequest ytr = new YouTubeRequest(ys);
            Video v = ytr.ParseVideo(s);

            Trace.TraceInformation("Upload successful");
            u.Row.Cells[COLUMNINDEX_STATUS].Value = "Upload successful";
            u.Row.Cells[COLUMNINDEX_STATUS].Tag = u;
            u.Row.Cells[COLUMNINDEX_VIDEOID].Value = v.VideoId;
            // we had one successful upload, reset the retry counts to 0
            lock (this.flag) {
                foreach (UserState us in retryQueue) {
                    us.RetryCounter = 0;
                }
            }
        }

        private void RemoveFromProcessingQueue(UserState u) {
            lock (this.flag) {
                this.queue.Remove(u);
            }
        }

        private bool AddToRetryQueue(UserState u) {
            u.RetryCounter++;
            if (u.RetryCounter > (int)this.automaticRetries.Value) {
                u.Row.Cells[COLUMNINDEX_STATUS].Value = "Number of retries exceeded. Last error was: " + u.Error;
                return false;
            }

            lock (this.flag) {
                this.retryQueue.Add(u);
            }
            return true;
        }

        // moves an entry into the retry queue
        // and enables the retry timer
        private void TryARetry(UserState u) {
            if (AddToRetryQueue(u)) {
                this.RetryTimer.Enabled = true;
            }
        }

        // cancels all currently active threads and empties the queue
        private void CancelProcessingQueue() {
            // this will go over all rows and tries to upload them
            lock (this.flag) {
                this.RetryTimer.Enabled = false;
                foreach (UserState u in this.queue) {
                    this.ru.CancelAsync(u);
                }
                this.queue = new List<UserState>();
            }
        }

        // walks over the retry queue and moves them into the processing queue
        // if the queue has free slots
        private void RetryRows() {
            if (this.retryQueue.Count > 0) {
                this.RetryTimer.Enabled = true;
                while (this.queue.Count < (int)this.MaxQueue.Value &&
                       this.retryQueue.Count > 0) {
                    lock (this.flag) {
                        UserState u = this.retryQueue[0];
                        this.retryQueue.Remove(u);
                        RetryRow(u);
                    }
                }
            } else {
                this.RetryTimer.Enabled = false;
                if (Finished())
                    ToggleButtons(true);
            }
        }
    }
}
