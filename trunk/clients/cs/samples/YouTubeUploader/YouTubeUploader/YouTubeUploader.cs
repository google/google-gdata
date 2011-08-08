using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Configuration;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using System.Collections;
using YouTubeUploader.Properties;
using System.Diagnostics;
using Google.GData.Client;
using Google.GData.Tools;


/* 2. Accepts a csv file with the following columns:

Title - string
Description - multiline string
Tags - comma separated list
Category - string
Private - True/False
Path - path to video file to upload

Any additional columns should be ignored.

When the user loads the csv file into the client, the client should display the contents of the file in a 
 * scrollable table so that the user can verify the contents of the file.

3. The client marches down the csv file and uploads each file sequentially using the resumable API.
 * It should attempt each upload a few times before giving up altogether.

4. All the videos are uploaded to the same account. There needs to be a place in the UI for 
 * the user to enter their username/password for ClientLogin.

5. As videos are uploaded, the status of the video is updated in the table. 

6. As videos are uploaded, write out a log csv file containing the following columns:

Title - string
Description - multiline string
Tags - comma separated list
Category - string
Private - True/False
Path - path to video file to upload
Timestamp
Video ID (assigned by YT)
Status (Success/Failed)

Error Reason (HTTP response code or "File not found" or ...)

Note that the first couple of columns are identical to the input file's. The idea is that, in the event of failure, 
 * it is easy to take this output csv file, massage it in Excel (e.g. to remove all videos with the status Success) 
 * and use the file to kick off a second round of uploads.
 * 
*/
namespace YouTubeUploader {
    public partial class YouTubeUploader : Form {

        private Authenticator youTubeAuthenticator;
        private const int COLUMN_MAX = 6;
        private const int COLUMNINDEX_FILENAME = 5;
        private const int COLUMNINDEX_STATUS = 6;
        private const int COLUMNINDEX_VIDEOID = 7;

        private const string CONFIG_DEVKEY = "DevKey";
        private const string CONFIG_CSVFILE = "CsvFile";
        private const string CONFIG_OUTPUTFILE = "OutputFile";
        private const string CONFIG_MAXTHREADS = "MaxThreads";
        private const string CONFIG_CHUNKSIZE = "ChunkSize";
        private const string CONFIG_RETRYCOUNT = "RetryCount";
        private const string CONFIG_USERNAME = "Username";
        private const string CONFIG_PASSWORD = "Password";
        private const string CONFIG_YTACCOUNT = "YoutubeAccount";

        private const string AutoStartString = "/autostart";
        private bool autoStart = false;
        private List<UserState> queue;
        private List<UserState> retryQueue;

        private const string ApplicationName = "YouTubeUploader";

        // this is the default devkey that has very limited allowance against the service
        // private const string DeveloperKeyDefault = "AI39si5Zg5AcJCNVptBgkZPLRY5DwgAgRZN3tYH9h3phjLq442KMal341c7HaxgBhOOmH0qDso6BgK65ji7VhsZ888evpZij_w";
        // this is a different devkey, uncomment this line and comment the above 
        private const string DeveloperKeyDefault = "AI39si5HV9zaLn4okq_gTqY4vARdZBf2_8D3bTkiK2FWgqaVVH9tITjBa6nyaAdg19Y4in-hnqcFTXu7i3d-2RIKSCvIMBARAg";

        private string developerKey;
        private string csvFileName;
        private string outputFileName;
        private string youtubeAccount;

        private GDataCredentials credentials;
        private Object flag = new Object();

        /// <summary>
        /// Default constructor
        /// </summary>
        public YouTubeUploader() {
            InitializeComponent();
            Bitmap b = Resources.gdata_youtube;
            IntPtr Hicon = b.GetHicon();
            this.Icon = Icon.FromHandle(Hicon);
        }

        // the applications is done when both queues are empty
        private bool Finished() {
            if (this.queue.Count == 0 &&
                this.retryQueue.Count == 0) {
                return true;
            }
            return false;
        }

        // we set the application title different if we still have pending uploads
        private void SetTitle() {
            if (NumberOfUploadsPending() > 0) {
                this.Text = YouTubeUploader.ApplicationName + " - " + NumberOfUploadsPending() + " uploads pending";
            } else {
                this.Text = YouTubeUploader.ApplicationName;
            }
        }

        // when the form loads, we want to load the application settings,
        // get the cmd line args, and process that 
        private void YouTubeUploader_Load(object sender, EventArgs e) {
            LoadAppSettings();
            GetCmdLineArgs();

            if (this.credentials != null) {
                try {
                    this.youTubeAuthenticator = new ClientLoginAuthenticator(YouTubeUploader.ApplicationName,
                        ServiceNames.YouTube, this.credentials);
                    this.youTubeAuthenticator.DeveloperKey = this.developerKey;
                } catch {
                    this.youTubeAuthenticator = null;
                }
            }

            if (this.youTubeAuthenticator == null) {
                GoogleClientLogin loginDialog = new GoogleClientLogin("youremailhere@gmail.com");
                loginDialog.ShowDialog();

                if (loginDialog.Credentials != null) {
                    this.youTubeAuthenticator = new ClientLoginAuthenticator(YouTubeUploader.ApplicationName,
                        ServiceNames.YouTube, loginDialog.Credentials);
                    this.youTubeAuthenticator.DeveloperKey = this.developerKey;

                    this.youtubeAccount = loginDialog.YoutubeAccount;
                } else {
                    this.Close();
                }
            }

            if (this.csvFileName != null) {
                LoadCSVFile(this.csvFileName);
                if (this.autoStart) {
                    Upload_Click(null, null);
                }
            }
        }

        // helper to toggle the buttons (pending if we are processing uploads
        // when only cancel is active)
        private void ToggleButtons(bool enabled) {
            this.OpenCSVFile.Enabled = enabled;
            this.Upload.Enabled = enabled;
            this.MaxQueue.Enabled = enabled;
            this.ChunkSize.Enabled = enabled;
            this.ChooseOutputFile.Enabled = enabled;

            EnableCancelButton(!enabled);

            this.Upload.Enabled = enabled && NumberOfUploadsPending() > 0;
        }

        // helper to make the cancel button visible and enabled (or not)
        private void EnableCancelButton(bool value) {
            this.CancelAsync.Enabled = value;
            this.CancelAsync.Visible = value;
        }

        /// <summary>
        /// the number of pending uplods is the sum of the retry queue
        /// and the rows that have not been processed at all.
        /// </summary>
        /// <returns></returns>
        private int NumberOfUploadsPending() {
            int count = this.retryQueue.Count;

            // this will go over all rows and tries to upload them
            foreach (DataGridViewRow row in this.csvDisplayGrid.Rows) {
                if (row.Cells[COLUMNINDEX_STATUS].Tag == null)
                    count++;
            }
            return count;
        }

        // Upload button was clicked. Modify UI state and fill the processing queue
        private void Upload_Click(object sender, EventArgs e) {
            ToggleButtons(false);
            EnsureRU();

            Trace.TraceInformation("Uploading started");
            Trace.Indent();
            Trace.TraceInformation("csv file contains " + this.csvDisplayGrid.Rows.Count + " rows");
            Trace.TraceInformation("Retry counter = " + this.automaticRetries.Value);
            Trace.TraceInformation("Threads = " + this.MaxQueue.Value);
            Trace.TraceInformation("Chunksize = " + this.ChunkSize.Value);
            Trace.Unindent();

            // this will go over all rows and set the status
            foreach (DataGridViewRow row in this.csvDisplayGrid.Rows) {
                if (row.Cells[COLUMNINDEX_STATUS].Value == null) {
                    row.Cells[COLUMNINDEX_STATUS].Value = "Waiting to be uploaded....";
                }
            }

            // this will go over all rows and try to upload them
            foreach (DataGridViewRow row in this.csvDisplayGrid.Rows) {
                UploadRow(row);
            }

            RetryRows();

            if (Finished()) {
                this.OnDone(null, null);
            }
        }

        // returns true if a row was added to the queue
        private bool UploadRow(DataGridViewRow row) {
            if (this.queue.Count >= (int)this.MaxQueue.Value) {
                return false;
            }

            UserState u = row.Cells[COLUMNINDEX_STATUS].Tag as UserState;
            // if this was already processed, don't. It will be either
            // in the queue already or moved to the retry queue
            if (u != null) {
                return false;
            }

            return InsertVideo(row, 1);
        }

        // when we get a progress notification we remember a bunch of state info send
        // that info is needed to retry later
        private void OnProgress(object sender, AsyncOperationProgressEventArgs e) {
            UserState u = e.UserState as UserState;

            if (u != null && u.Row != null) {
                string status = "";
                if (u.RetryCounter > 1) {
                    status = "Retrying (" + (u.RetryCounter - 1).ToString() + "), uploading: " + e.ProgressPercentage + "% done";
                } else {
                    status = "Uploading: " + e.ProgressPercentage + "% done";
                }

                Trace.TraceInformation("OnProgress: " + status);
                Trace.Indent();
                Trace.TraceInformation("Verb: " + e.HttpVerb);
                Trace.TraceInformation("Uri: " + e.Uri);
                Trace.TraceInformation("Current position: " + e.Position);
                Trace.Unindent();

                u.CurrentPosition = e.Position;
                u.ResumeUri = e.Uri;
                u.HttpVerb = e.HttpVerb;
                u.Row.Cells[COLUMNINDEX_STATUS].Value = status;
            }
        }

        // send when an upload is done. This can mean it completed, it failed
        // or it was cancelled
        private void OnDone(object sender, AsyncOperationCompletedEventArgs e) {
            if (e != null) {
                UserState u = e.UserState as UserState;

                Trace.TraceInformation("OnDone - Upload finished for :" + u.ResumeUri);
                Trace.Indent();

                if (u != null && u.Row != null) {
                    if (e.Cancelled) {
                        Trace.TraceInformation("Cancelled. Current Pos = " + u.CurrentPosition);
                        u.Row.Cells[COLUMNINDEX_STATUS].Value = "Operation was cancelled";
                        // if it was cancelled, reset the retry counter
                        u.RetryCounter = 0;
                        u.Row.Cells[COLUMNINDEX_STATUS].Tag = u;
                        AddToRetryQueue(u);
                    } else if (e.Error != null) {
                        Trace.TraceInformation("Error. Current Pos = " + u.CurrentPosition);
                        Trace.TraceInformation("Error was: " + e.Error.ToString() + " - " + e.Error.Message);
                        u.Error = e.Error.Message;
                        u.Row.Cells[COLUMNINDEX_STATUS].Value = e.Error.Message;
                        u.Row.Cells[COLUMNINDEX_STATUS].Value = "Tried (" + u.RetryCounter + ") - Last error was: " + u.Error;
                        u.Row.Cells[COLUMNINDEX_STATUS].Tag = u;
                        TryARetry(u);
                    } else {
                        ParseAndFinish(u, e.ResponseStream);
                    }
                }

                RemoveFromProcessingQueue(u);

                // only add new ones, if we did not have a cancellation
                if (!e.Cancelled) {
                    // now add a new row, if there is one
                    // this will go over all rows and tries to upload them
                    foreach (DataGridViewRow row in this.csvDisplayGrid.Rows) {
                        if (UploadRow(row)) {
                            break;
                        }
                    }
                }

                Trace.Unindent();
            }

            if (Finished()) {
                ToggleButtons(true);

                if (this.autoStart && this.outputFileName != null) {
                    SaveGridAsCSV(outputFileName);
                }
            }
            SetTitle();
        }

        private void csvDisplayGrid_CellMouseEnter(object sender, DataGridViewCellEventArgs e) {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0) {
                DataGridViewCell cell = this.csvDisplayGrid.Rows[e.RowIndex].Cells[e.ColumnIndex];

                if (e.ColumnIndex == COLUMNINDEX_FILENAME) {
                    string contentType = MediaFileSource.GetContentTypeForFileName(cell.Value.ToString());
                    cell.ToolTipText = "We assume this file is of type: " + contentType;
                }
            }
        }

        private void CancelAsync_Click(object sender, EventArgs e) {
            CancelProcessingQueue();

            ToggleButtons(true);
        }

        private void GetCmdLineArgs() {
            string[] args = Environment.GetCommandLineArgs();

            foreach (string arg in args) {
                this.autoStart = YouTubeUploader.AutoStartString.Equals(arg.ToLower());
            }
        }

        // the retry timer ticks once a minute.
        private void RetryTimer_Tick(object sender, EventArgs e) {
            // sometimes this message get's posted to us even 
            // though we disabled the retrytimer shortly before.
            // so better check if we are still supposed to process
            if (this.RetryTimer.Enabled) {
                Trace.TraceInformation("Entering the retryTimer");
                RetryRows();
            }
        }

        // opens up the wiki page with the documentation
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            try {
                this.linkLabel1.LinkVisited = true;
                System.Diagnostics.Process.Start("http://code.google.com/p/google-gdata/wiki/YouTubeUploader");
            } catch (Exception ex) {
                MessageBox.Show("Unable to open the documentation page: " + ex.Message);
            }
        }

        private void LoadAppSettings() {
            // Get the AppSettings collection.
            NameValueCollection appSettings = ConfigurationManager.AppSettings;

            if (appSettings[YouTubeUploader.CONFIG_DEVKEY] != null) {
                this.developerKey = appSettings[YouTubeUploader.CONFIG_DEVKEY];
            } else {
                this.developerKey = YouTubeUploader.DeveloperKeyDefault;
            }

            if (String.IsNullOrEmpty(this.developerKey)) {
                MessageBox.Show("You need to enter a developer key in the source code. Look for DeveloperKeyDefault and paste your key in");
                this.Close();
            }

            if (appSettings[YouTubeUploader.CONFIG_MAXTHREADS] != null) {
                this.MaxQueue.Value = Decimal.Parse(appSettings[YouTubeUploader.CONFIG_MAXTHREADS]);
            } else {
                this.MaxQueue.Value = 3;
            }

            if (appSettings[YouTubeUploader.CONFIG_RETRYCOUNT] != null) {
                this.automaticRetries.Value = Decimal.Parse(appSettings[YouTubeUploader.CONFIG_RETRYCOUNT]);
            } else {
                this.automaticRetries.Value = 10;
            }

            if (appSettings[YouTubeUploader.CONFIG_CHUNKSIZE] != null) {
                this.ChunkSize.Value = Decimal.Parse(appSettings[YouTubeUploader.CONFIG_CHUNKSIZE]);
            } else {
                this.ChunkSize.Value = 25;
            }

            this.csvFileName = appSettings[YouTubeUploader.CONFIG_CSVFILE];
            this.outputFileName = appSettings[YouTubeUploader.CONFIG_OUTPUTFILE];
            if (appSettings[YouTubeUploader.CONFIG_USERNAME] != null) {
                this.credentials = new GDataCredentials(appSettings[YouTubeUploader.CONFIG_USERNAME],
                    appSettings[YouTubeUploader.CONFIG_PASSWORD]);
            }
            this.youtubeAccount = appSettings[YouTubeUploader.CONFIG_YTACCOUNT];
        }
    }
}    
