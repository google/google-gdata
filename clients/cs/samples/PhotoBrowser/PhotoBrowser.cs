using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Google.GData.Photos;
using Google.GData.Extensions.MediaRss;
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
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public PictureBrowser(PicasaService service, PicasaFeed feed, string albumTitle)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

            this.photoFeed = feed;
            this.picasaService = service;
            this.Text += albumTitle;

            if (this.photoFeed != null && this.photoFeed.Entries.Count > 0) 
            {
                foreach (PicasaEntry entry in this.photoFeed.Entries)
                {
                    ListViewItem item = new ListViewItem(entry.Title.Text);
                    item.Tag = entry;
                    this.PhotoList.Items.Add(item);
                }
            }
            
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
            this.PhotoList.Location = new System.Drawing.Point(24, 64);
            this.PhotoList.Name = "PhotoList";
            this.PhotoList.Size = new System.Drawing.Size(176, 216);
            this.PhotoList.TabIndex = 0;
            this.PhotoList.View = System.Windows.Forms.View.List;
            this.PhotoList.SelectedIndexChanged += new System.EventHandler(this.PhotoList_SelectedIndexChanged);
            // 
            // PhotoInspector
            // 
            this.PhotoInspector.CommandsVisibleIfAvailable = true;
            this.PhotoInspector.LargeButtons = false;
            this.PhotoInspector.LineColor = System.Drawing.SystemColors.ScrollBar;
            this.PhotoInspector.Location = new System.Drawing.Point(24, 296);
            this.PhotoInspector.Name = "PhotoInspector";
            this.PhotoInspector.Size = new System.Drawing.Size(496, 192);
            this.PhotoInspector.TabIndex = 2;
            this.PhotoInspector.Text = "propertyGrid1";
            this.PhotoInspector.ViewBackColor = System.Drawing.SystemColors.Window;
            this.PhotoInspector.ViewForeColor = System.Drawing.SystemColors.WindowText;
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
            this.DownloadPhoto.Location = new System.Drawing.Point(560, 64);
            this.DownloadPhoto.Name = "DownloadPhoto";
            this.DownloadPhoto.Size = new System.Drawing.Size(88, 40);
            this.DownloadPhoto.TabIndex = 5;
            this.DownloadPhoto.Text = "&Export Photos";
            this.DownloadPhoto.Click += new System.EventHandler(this.DownloadPhoto_Click);
            // 
            // UploadPhoto
            // 
            this.UploadPhoto.Location = new System.Drawing.Point(560, 128);
            this.UploadPhoto.Name = "UploadPhoto";
            this.UploadPhoto.Size = new System.Drawing.Size(88, 48);
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
            // PictureBrowser
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
            this.ClientSize = new System.Drawing.Size(672, 584);
            this.Controls.Add(this.UploadPhoto);
            this.Controls.Add(this.DownloadPhoto);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PhotoInspector);
            this.Controls.Add(this.PhotoList);
            this.Controls.Add(this.PhotoPreview);
            this.Name = "PictureBrowser";
            this.Text = "Browsing album: ";
            this.ResumeLayout(false);

        }
		#endregion

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
                Stream stream  = this.picasaService.Query(new Uri(findLargestThumbnail(entry.Media.Thumbnails)));
                this.PhotoPreview.Image = new Bitmap(stream);
                this.PhotoInspector.SelectedObject = new PhotoAccessor(entry);
                this.Cursor = Cursors.Default;
            }
            else 
            {
                this.PhotoPreview.Image = null;
                this.PhotoInspector.SelectedObject = null; 
            }
        }

        private string findLargestThumbnail(ThumbnailCollection collection)
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
            this.Cursor = Cursors.WaitCursor;
        
            if( result == DialogResult.OK )
            {
                string folderName = folderBrowserDialog.SelectedPath;
                int i=1;
                foreach (ListViewItem item in this.PhotoList.SelectedItems) 
                {
                    string filename = folderName + "\\image" + i.ToString() + ".jpg";
                    i++;
                    PicasaEntry entry = item.Tag as PicasaEntry;
                    PictureBrowser.saveImageFile(entry, filename, this.picasaService);
                }
            }
            this.Cursor = Cursors.Default;
   
        }

        static public void saveImageFile(PicasaEntry entry, string filename, PicasaService service)
        {
            if (entry.Media != null &&
                entry.Media.Content != null)
            {
                Stream stream  = service.Query(new Uri(entry.Media.Content.Attributes["url"] as string));
                FileStream fs = new FileStream(filename, FileMode.OpenOrCreate);
                BinaryWriter w = new BinaryWriter(fs);
                byte []buffer = new byte[1024];
                int iRead=0;
                int iOffset = 0; 
                while ((iRead = stream.Read(buffer, 0, 1024)) > 0) 
                {
                    w.Write(buffer, 0, iRead);
                    iOffset += iRead;
                }
                w.Close();
                fs.Close();
            }    
        }

        private void UploadPhoto_Click(object sender, System.EventArgs e)
        {
            DialogResult result = openFileDialog.ShowDialog();
            this.Cursor = Cursors.WaitCursor;
        
            if( result == DialogResult.OK )
            {
                string[] files = openFileDialog.FileNames;
                Uri postUri = new Uri(this.photoFeed.Post);

                // Open each file and display the image in PictureBox1.
                // Call Application.DoEvents to force a repaint after each
                // file is read.        
                foreach (string file in files )
                {
                    System.IO.FileInfo fileInfo = new System.IO.FileInfo(file);
                    System.IO.FileStream fileStream = fileInfo.OpenRead();

                    PicasaEntry entry = this.picasaService.Insert(postUri, fileStream, "image/jpeg", file) as PicasaEntry;

                    ListViewItem item = new ListViewItem(entry.Title.Text);
                    item.Tag = entry;
                    this.PhotoList.Items.Add(item);
            
                }
            }
        }
 
	}
}
