using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Google.GData.Photos;
using Google.GData.Client;
using Google.GData.Extensions.MediaRss;
using Google.GData.Extensions;
using Google.Picasa;
using System.IO;

namespace PhotoBrowser
{
    /// <summary>
	/// Summary description for PhotoBrowser.
	/// </summary>
	public class PictureBrowser : System.Windows.Forms.Form
	{
        private PicasaService picasaService;
        private PicasaFeed photoFeed;
        private System.Windows.Forms.PictureBox PhotoPreview;
        private System.Windows.Forms.ListView PhotoList;
        private System.Windows.Forms.PropertyGrid PhotoInspector;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button DownloadPhoto;
        private System.Windows.Forms.Button UploadPhoto;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private List<UserState> states = new List<UserState>();

        private delegate void SaveAnotherPictureDelegate(UserState us);

        private ProgressBar progressBar;
        private Label FileInfo;
        private Button CancelAsync;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;



        public PictureBrowser(PicasaService service, bool doBackup)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
            this.picasaService = service;

            this.picasaService.AsyncOperationCompleted += new AsyncOperationCompletedEventHandler(this.OnDone);
            this.picasaService.AsyncOperationProgress += new AsyncOperationProgressEventHandler(this.OnProgress);

            if (doBackup == true)
            {
                this.DownloadPhoto.Enabled = false;
                this.DownloadPhoto.Visible = false;
                this.UploadPhoto.Enabled = false;
                this.UploadPhoto.Visible = false;
            }
        }

        public void StartQuery(string uri, string albumTitle)
        {
            UserState us = new UserState();
            this.states.Add(us);

            us.opType = UserState.OperationType.query;
            us.filename = albumTitle;

            this.picasaService.QueryFeedAync(new Uri(uri), DateTime.MinValue, us);
        }

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.PhotoPreview = new System.Windows.Forms.PictureBox();
            this.PhotoList = new System.Windows.Forms.ListView();
            this.PhotoInspector = new System.Windows.Forms.PropertyGrid();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.DownloadPhoto = new System.Windows.Forms.Button();
            this.UploadPhoto = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.FileInfo = new System.Windows.Forms.Label();
            this.CancelAsync = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.PhotoPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // PhotoPreview
            // 
            this.PhotoPreview.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.PhotoPreview.Location = new System.Drawing.Point(240, 64);
            this.PhotoPreview.Name = "PhotoPreview";
            this.PhotoPreview.Size = new System.Drawing.Size(280, 216);
            this.PhotoPreview.TabIndex = 1;
            this.PhotoPreview.TabStop = false;
            // 
            // PhotoList
            // 
            this.PhotoList.Alignment = System.Windows.Forms.ListViewAlignment.Default;
            this.PhotoList.FullRowSelect = true;
            this.PhotoList.GridLines = true;
            this.PhotoList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.PhotoList.LabelWrap = false;
            this.PhotoList.Location = new System.Drawing.Point(24, 64);
            this.PhotoList.MultiSelect = false;
            this.PhotoList.Name = "PhotoList";
            this.PhotoList.ShowGroups = false;
            this.PhotoList.Size = new System.Drawing.Size(210, 216);
            this.PhotoList.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.PhotoList.TabIndex = 0;
            this.PhotoList.UseCompatibleStateImageBehavior = false;
            this.PhotoList.View = System.Windows.Forms.View.List;
            this.PhotoList.SelectedIndexChanged += new System.EventHandler(this.PhotoList_SelectedIndexChanged);
            // 
            // PhotoInspector
            // 
            this.PhotoInspector.LineColor = System.Drawing.SystemColors.ScrollBar;
            this.PhotoInspector.Location = new System.Drawing.Point(24, 296);
            this.PhotoInspector.Name = "PhotoInspector";
            this.PhotoInspector.Size = new System.Drawing.Size(496, 192);
            this.PhotoInspector.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(24, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(176, 24);
            this.label1.TabIndex = 3;
            this.label1.Text = "List of photos:";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(240, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 32);
            this.label2.TabIndex = 4;
            this.label2.Text = "Preview:";
            // 
            // DownloadPhoto
            // 
            this.DownloadPhoto.Enabled = false;
            this.DownloadPhoto.Location = new System.Drawing.Point(27, 575);
            this.DownloadPhoto.Name = "DownloadPhoto";
            this.DownloadPhoto.Size = new System.Drawing.Size(88, 40);
            this.DownloadPhoto.TabIndex = 5;
            this.DownloadPhoto.Text = "&Export Photo";
            this.DownloadPhoto.Click += new System.EventHandler(this.DownloadPhoto_Click);
            // 
            // UploadPhoto
            // 
            this.UploadPhoto.Enabled = false;
            this.UploadPhoto.Location = new System.Drawing.Point(131, 575);
            this.UploadPhoto.Name = "UploadPhoto";
            this.UploadPhoto.Size = new System.Drawing.Size(88, 40);
            this.UploadPhoto.TabIndex = 6;
            this.UploadPhoto.Text = "&Upload Photos";
            this.UploadPhoto.Click += new System.EventHandler(this.UploadPhoto_Click);
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.RootFolder = System.Environment.SpecialFolder.MyPictures;
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "JPeg Files|*.jpg";
            this.openFileDialog.Multiselect = true;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(24, 551);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(496, 18);
            this.progressBar.TabIndex = 7;
            // 
            // FileInfo
            // 
            this.FileInfo.Location = new System.Drawing.Point(24, 491);
            this.FileInfo.Name = "FileInfo";
            this.FileInfo.Size = new System.Drawing.Size(496, 57);
            this.FileInfo.TabIndex = 8;
            // 
            // CancelAsync
            // 
            this.CancelAsync.Enabled = false;
            this.CancelAsync.Location = new System.Drawing.Point(421, 575);
            this.CancelAsync.Name = "CancelAsync";
            this.CancelAsync.Size = new System.Drawing.Size(99, 40);
            this.CancelAsync.TabIndex = 9;
            this.CancelAsync.Text = "&Cancel";
            this.CancelAsync.UseVisualStyleBackColor = true;
            this.CancelAsync.Visible = false;
            this.CancelAsync.Click += new System.EventHandler(this.CancelAsync_Click);
            // 
            // PictureBrowser
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
            this.ClientSize = new System.Drawing.Size(534, 627);
            this.Controls.Add(this.CancelAsync);
            this.Controls.Add(this.FileInfo);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.UploadPhoto);
            this.Controls.Add(this.DownloadPhoto);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PhotoInspector);
            this.Controls.Add(this.PhotoList);
            this.Controls.Add(this.PhotoPreview);
            this.Name = "PictureBrowser";
            this.Text = "Waiting for data to load";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PictureBrowser_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.PhotoPreview)).EndInit();
            this.ResumeLayout(false);

        }
		#endregion

        private void InitializeList(string title)
        {
            this.Text = title;

            if (this.photoFeed != null && this.photoFeed.Entries.Count > 0) 
            {
                foreach (PicasaEntry entry in this.photoFeed.Entries)
                {
                    ListViewItem item = new ListViewItem(entry.Title.Text);
                    item.Tag = entry;
                    this.PhotoList.Items.Add(item);
                }
                this.UploadPhoto.Enabled = true;
            }
        }

        private void PhotoList_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (this.PhotoList.SelectedItems.Count == 1)
            {
                foreach (ListViewItem item in this.PhotoList.SelectedItems) 
                {
                    PicasaEntry entry = item.Tag as PicasaEntry;
                    setSelection(entry);
                }
            } 
            else 
            {
                setSelection(null);
            }
        }
       
        private void setSelection(PicasaEntry entry)
        {
            if (entry != null) 
            {
                this.Cursor = Cursors.WaitCursor;
                try
                {
                    Stream stream = this.picasaService.Query(new Uri(findLargestThumbnail(entry.Media.Thumbnails)));
                    this.PhotoPreview.Image = new Bitmap(stream);
                }
                catch
                {
                    Icon error = new Icon(SystemIcons.Exclamation, 40, 40);
                    this.PhotoPreview.Image = error.ToBitmap();
                }
                Photo photo = new Photo();
                photo.AtomEntry = entry;
                this.PhotoInspector.SelectedObject = photo;
                this.Cursor = Cursors.Default;
                this.DownloadPhoto.Enabled = true;
            }
            else 
            {
                this.PhotoPreview.Image = null;
                this.PhotoInspector.SelectedObject = null;
                this.DownloadPhoto.Enabled = false;
            }
        }

        private string findLargestThumbnail(ExtensionCollection<MediaThumbnail> collection)
        {
            MediaThumbnail largest = null;
            int width = 0; 
            foreach (MediaThumbnail thumb in collection) 
            {
                int iWidth = int.Parse(thumb.Attributes["width"] as string);
                if (iWidth > width) 
                {
                    largest = thumb;
                }
            }
            return largest.Attributes["url"] as string;
        }

        private void DownloadPhoto_Click(object sender, System.EventArgs e)
        {
            // Show the FolderBrowserDialog.
            DialogResult result = folderBrowserDialog.ShowDialog();
        
            if( result == DialogResult.OK )
            {
                string folderName = folderBrowserDialog.SelectedPath;
                int i=1;
                foreach (ListViewItem item in this.PhotoList.SelectedItems) 
                {
                    string filename = folderName + "\\image" + i.ToString() + ".jpg";
                    i++;
                    PicasaEntry entry = item.Tag as PicasaEntry;
                    this.DoSaveImageFile(entry, filename);
                }
            }
        }




        public void BackupAlbum(string albumUri, string foldername)
        {

            UserState us = new UserState();

            us.opType = UserState.OperationType.queryForBackup;
            us.filename = "Starting backup to : " + foldername;
            us.foldername = foldername;
            this.states.Add(us);

            this.picasaService.QueryFeedAync(new Uri(albumUri), DateTime.MinValue, us);


        }


        public void DoSaveImageFile(PicasaEntry entry, string filename)
        {
            if (entry.Media != null &&
                entry.Media.Content != null)
            {
                UserState ut = new UserState();
                ut.opType = UserState.OperationType.download;
                ut.filename = filename;
                this.states.Add(ut);

                this.picasaService.QueryStreamAync(new Uri(entry.Media.Content.Attributes["url"] as string), DateTime.MinValue, ut);
            }
        }

        private void UploadPhoto_Click(object sender, System.EventArgs e)
        {
            DialogResult result = openFileDialog.ShowDialog();
 
            if( result == DialogResult.OK )
            {
                string[] files = openFileDialog.FileNames;
                // Open each file and display the image in PictureBox1.
                // Call Application.DoEvents to force a repaint after each
                // file is read.        
                foreach (string file in files )
                {
                    System.IO.FileInfo fileInfo = new System.IO.FileInfo(file);
                    System.IO.FileStream fileStream = fileInfo.OpenRead();

                    this.FileInfo.Text = "Starting upload....";
                    PicasaEntry entry = new PhotoEntry();

                    UserState ut = new UserState();
                    ut.opType = UserState.OperationType.upload;
                    this.states.Add(ut);
                    entry.MediaSource = new Google.GData.Client.MediaFileSource(fileStream, file, "image/jpeg");
                    this.picasaService.InsertAsync(new Uri(this.photoFeed.Post), entry, ut);
                }
            }
        }

        private void OnProgress(object sender, AsyncOperationProgressEventArgs e)
        {
            this.CancelAsync.Enabled = true;
            this.CancelAsync.Visible = true;

            if (this.states.Contains(e.UserState as UserState) == true)
            {
                this.progressBar.Value = e.ProgressPercentage ;
            }
        }


        private void OnDone(object sender, AsyncOperationCompletedEventArgs e)
        {
            UserState ut = e.UserState as UserState;

            if (this.states.Contains(ut) == false)
                return;

            this.states.Remove(ut);
        
            if (e.Error == null && e.Cancelled == false)
            {

                if (ut.opType == UserState.OperationType.query ||
                    ut.opType == UserState.OperationType.queryForBackup)
                {
                    if (e.Feed != null)
                    {
                        this.photoFeed = e.Feed as PicasaFeed;
                        this.InitializeList(ut.filename);
                    }
                }

                if (ut.opType == UserState.OperationType.upload)
                {
                    if (e.Entry != null)
                    {
                        ListViewItem item = new ListViewItem(e.Entry.Title.Text);
                        item.Tag = e.Entry;
                        this.PhotoList.Items.Add(item);
                        this.FileInfo.Text = "Upload succeeded";
                    }
                }
                if (ut.opType == UserState.OperationType.download ||
                    ut.opType == UserState.OperationType.downloadList)
                {
                    if (e.ResponseStream != null)
                    {
                        WriteFile(ut.filename, e.ResponseStream);
                        this.FileInfo.Text = "Saved file: " + ut.filename;
                    }
                }
                if (ut.opType == UserState.OperationType.downloadList)
                {
                    // we need to create a new object for uniqueness

                    UserState u = new UserState();
                    u.counter = ut.counter + 1;
                    u.feed = ut.feed;
                    u.foldername = ut.foldername;
                    u.opType = UserState.OperationType.downloadList;
                    
                    if (u.feed.Entries.Count > 0)
                    {
                        u.feed.Entries.RemoveAt(0);
                        this.PhotoList.Items.RemoveAt(0);

                    }
                    this.states.Add(u);
                    SaveAnotherPictureDelegate d = new SaveAnotherPictureDelegate(this.CreateAnotherSaveFile);

                    this.BeginInvoke(d, u);

                }
                if (ut.opType == UserState.OperationType.queryForBackup)
                {
                    UserState u = new UserState();
                    u.opType = UserState.OperationType.downloadList;
                    u.feed = this.photoFeed;
                    u.counter = 1;
                    u.foldername = ut.foldername;
                    u.filename = ut.foldername + "\\image1.jpg";
                    this.states.Add(u);
                    SaveAnotherPictureDelegate d = new SaveAnotherPictureDelegate(this.CreateAnotherSaveFile);
                    this.BeginInvoke(d, u);
                }
            }
            this.progressBar.Value = 0;

            if (this.states.Count == 0)
            {
                this.CancelAsync.Enabled = false;
                this.CancelAsync.Visible = false;
            }

        }

        private void CreateAnotherSaveFile(UserState us)
        {
            if (us.feed.Entries.Count > 0)
            {
                PicasaEntry p = us.feed.Entries[0] as PicasaEntry;
                us.filename = us.foldername + "\\image" + us.counter.ToString() + ".jpg";
                this.picasaService.QueryStreamAync(new Uri(p.Media.Content.Attributes["url"] as string), DateTime.MinValue, us);
            }
            else if (us.feed.Entries.Count == 0 && us.feed.NextChunk != null)
            {
            }
            else
            {
                this.Close();
            }
        }

        private void WriteFile(string filename, Stream input)
        {
            FileStream fs = new FileStream(filename, FileMode.OpenOrCreate);
            BinaryWriter w = new BinaryWriter(fs);
            byte[] buffer = new byte[4096];
            int iRead = 0;
            int iOffset = 0;
            while ((iRead = input.Read(buffer, 0, 4069)) > 0)
            {
                w.Write(buffer, 0, iRead);
                iOffset += iRead;
            }
            w.Close();
            fs.Close();
        }

        private void PictureBrowser_FormClosing(object sender, FormClosingEventArgs e)
        {
            // here we should cancel our async events
            foreach (object o in this.states)
            {
                this.picasaService.CancelAsync(o);
            }

        }

        private void CancelAsync_Click(object sender, EventArgs e)
        {
            // here we should cancel our async events
            foreach (object o in this.states)
            {
                this.picasaService.CancelAsync(o);
            }

            this.FileInfo.Text = "Operation was cancelled";

        }
	}

    public class UserState
    {
        public enum OperationType
        {
            upload,
            download,
            downloadList,
            query,
            queryForBackup
        }

        public string filename;
        public OperationType opType;
        public PicasaFeed feed;
        public int counter = 0; 
        public string foldername;

    }

}
