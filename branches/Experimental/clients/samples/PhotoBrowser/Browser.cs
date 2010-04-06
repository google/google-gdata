using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using Google.GData.Photos;
using Google.GData.Extensions.MediaRss;
using Google.GData.Tools;
using Google.Picasa;
using System.IO;

namespace PhotoBrowser
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class PhotoBrowser : System.Windows.Forms.Form
	{
        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.ListView AlbumList;
        private System.Windows.Forms.PictureBox AlbumPicture;
        private String googleAuthToken = null;
        private String user = null;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PropertyGrid AlbumInspector;
        private System.Windows.Forms.Button AddAlbum;
        private System.Windows.Forms.Button SaveAlbum;
        private System.Windows.Forms.Button DeleteAlbum;
        private PicasaService picasaService = new PicasaService("PhotoBrowser");
        private System.Windows.Forms.Button SaveAlbumData;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private PicasaFeed picasaFeed = null;


		public PhotoBrowser()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
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
            this.components = new System.ComponentModel.Container();
            this.AlbumList = new System.Windows.Forms.ListView();
            this.AlbumPicture = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.AlbumInspector = new System.Windows.Forms.PropertyGrid();
            this.SaveAlbumData = new System.Windows.Forms.Button();
            this.AddAlbum = new System.Windows.Forms.Button();
            this.SaveAlbum = new System.Windows.Forms.Button();
            this.DeleteAlbum = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // AlbumList
            // 
            this.AlbumList.FullRowSelect = true;
            this.AlbumList.GridLines = true;
            this.AlbumList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.AlbumList.LabelEdit = true;
            this.AlbumList.Location = new System.Drawing.Point(8, 56);
            this.AlbumList.MultiSelect = false;
            this.AlbumList.Name = "AlbumList";
            this.AlbumList.Size = new System.Drawing.Size(296, 152);
            this.AlbumList.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.AlbumList.TabIndex = 0;
            this.AlbumList.View = System.Windows.Forms.View.List;
            this.AlbumList.DoubleClick += new System.EventHandler(this.OnBrowseAlbum);
            this.AlbumList.SelectedIndexChanged += new System.EventHandler(this.AlbumList_SelectedIndexChanged);
            // 
            // AlbumPicture
            // 
            this.AlbumPicture.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.AlbumPicture.Location = new System.Drawing.Point(320, 56);
            this.AlbumPicture.Name = "AlbumPicture";
            this.AlbumPicture.Size = new System.Drawing.Size(176, 152);
            this.AlbumPicture.TabIndex = 1;
            this.AlbumPicture.TabStop = false;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(16, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(288, 24);
            this.label1.TabIndex = 2;
            this.label1.Text = "List of Albums:";
            // 
            // imageList1
            // 
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(328, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Cover:";
            // 
            // AlbumInspector
            // 
            this.AlbumInspector.CommandsVisibleIfAvailable = true;
            this.AlbumInspector.LargeButtons = false;
            this.AlbumInspector.LineColor = System.Drawing.SystemColors.ScrollBar;
            this.AlbumInspector.Location = new System.Drawing.Point(8, 224);
            this.AlbumInspector.Name = "AlbumInspector";
            this.AlbumInspector.Size = new System.Drawing.Size(488, 232);
            this.AlbumInspector.TabIndex = 4;
            this.AlbumInspector.Text = "AlbumInspector";
            this.AlbumInspector.ViewBackColor = System.Drawing.SystemColors.Window;
            this.AlbumInspector.ViewForeColor = System.Drawing.SystemColors.WindowText;
            // 
            // SaveAlbumData
            // 
            this.SaveAlbumData.Location = new System.Drawing.Point(16, 480);
            this.SaveAlbumData.Name = "SaveAlbumData";
            this.SaveAlbumData.Size = new System.Drawing.Size(88, 40);
            this.SaveAlbumData.TabIndex = 5;
            this.SaveAlbumData.Text = "&Save Album Data";
            this.SaveAlbumData.Click += new System.EventHandler(this.SaveAlbumData_Click);
            // 
            // AddAlbum
            // 
            this.AddAlbum.Location = new System.Drawing.Point(130, 480);
            this.AddAlbum.Name = "AddAlbum";
            this.AddAlbum.Size = new System.Drawing.Size(88, 40);
            this.AddAlbum.TabIndex = 6;
            this.AddAlbum.Text = "&Add a new Album";
            this.AddAlbum.Click += new System.EventHandler(this.AddAlbum_Click);
            // 
            // SaveAlbum
            // 
            this.SaveAlbum.Location = new System.Drawing.Point(240, 480);
            this.SaveAlbum.Name = "SaveAlbum";
            this.SaveAlbum.Size = new System.Drawing.Size(88, 40);
            this.SaveAlbum.TabIndex = 7;
            this.SaveAlbum.Text = "&Backup Album";
            this.SaveAlbum.Click += new System.EventHandler(this.SaveAlbum_Click);
            // 
            // DeleteAlbum
            // 
            this.DeleteAlbum.Location = new System.Drawing.Point(400, 480);
            this.DeleteAlbum.Name = "DeleteAlbum";
            this.DeleteAlbum.Size = new System.Drawing.Size(88, 40);
            this.DeleteAlbum.TabIndex = 8;
            this.DeleteAlbum.Text = "&Delete Album";
            this.DeleteAlbum.Click += new System.EventHandler(this.DeleteAlbum_Click);
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.RootFolder = System.Environment.SpecialFolder.MyPictures;
            // 
            // PhotoBrowser
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
            this.ClientSize = new System.Drawing.Size(504, 544);
            this.Controls.Add(this.DeleteAlbum);
            this.Controls.Add(this.SaveAlbum);
            this.Controls.Add(this.AddAlbum);
            this.Controls.Add(this.SaveAlbumData);
            this.Controls.Add(this.AlbumInspector);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.AlbumPicture);
            this.Controls.Add(this.AlbumList);
            this.Name = "PhotoBrowser";
            this.Text = "Google Picasa Browser";
            this.Load += new System.EventHandler(this.OnLoad);
            this.ResumeLayout(false);

        }
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new PhotoBrowser());
		}

        private void OnLoad(object sender, System.EventArgs e)
        {
            if (this.googleAuthToken == null) 
            {
                GoogleClientLogin loginDialog = new GoogleClientLogin(new PicasaService("PhotoBrowser"), "youremailhere@gmail.com"); 
                loginDialog.ShowDialog();
              
                this.googleAuthToken = loginDialog.AuthenticationToken;
                this.user = loginDialog.User;

                if (this.googleAuthToken != null)
                {
                    picasaService.SetAuthenticationToken(loginDialog.AuthenticationToken);
                    UpdateAlbumFeed();
                }
                else
                {
                    this.Close();
                }
            }
        
        }


        private void UpdateAlbumFeed()
        {
            AlbumQuery query = new AlbumQuery();

            this.AlbumList.Clear();

            
            query.Uri = new Uri(PicasaQuery.CreatePicasaUri(this.user));

            this.picasaFeed = this.picasaService.Query(query);

            if (this.picasaFeed != null && this.picasaFeed.Entries.Count > 0) 
            {
                foreach (PicasaEntry entry in this.picasaFeed.Entries)
                {
                    ListViewItem item = new ListViewItem(entry.Title.Text + 
                                    " (" + entry.GetPhotoExtensionValue(GPhotoNameTable.NumPhotos) + " )");
                    item.Tag = entry;
                    this.AlbumList.Items.Add(item);
                }
            }
            this.AlbumList.Update();
        }

        private void AlbumList_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            foreach (ListViewItem item in this.AlbumList.SelectedItems) 
            {
                PicasaEntry entry = item.Tag as PicasaEntry;
                setSelection(entry);
            }
        }

        private void OnBrowseAlbum(object sender, System.EventArgs e)
        {
            foreach (ListViewItem item in this.AlbumList.SelectedItems) 
            {
                PicasaEntry entry = item.Tag as PicasaEntry;
                string photoUri = entry.FeedUri; 
                if (photoUri != null) 
                {
                    PictureBrowser b = new PictureBrowser(this.picasaService, false);
                    b.Show();
                    b.StartQuery(photoUri, entry.Title.Text);
                }
            }
        }

        private void AddAlbum_Click(object sender, System.EventArgs e)
        {
            NewAlbumDialog dialog = new NewAlbumDialog(this.picasaService, this.picasaFeed);
            dialog.ShowDialog();
            PicasaEntry entry = dialog.CreatedEntry;
            if (entry != null) 
            {
                ListViewItem item = new ListViewItem(entry.Title.Text + 
                    " (" + entry.GetPhotoExtensionValue(GPhotoNameTable.NumPhotos) + " )");
                item.Tag = entry;
                this.AlbumList.Items.Add(item);
                this.AlbumList.Refresh();
            }
        }

        private void DeleteAlbum_Click(object sender, System.EventArgs e)
        {

            if (MessageBox.Show("Are you really sure? This is not undoable.", 
                "Delete this Album", MessageBoxButtons.YesNo)  == DialogResult.Yes)
            {
                foreach (ListViewItem item in this.AlbumList.SelectedItems) 
                {
                    PicasaEntry entry = item.Tag as PicasaEntry;
                    entry.Delete();
                    this.AlbumList.Items.Remove(item);
                    setSelection(null);
                }
            }
        }

        private void setSelection(PicasaEntry entry)
        {
            if (entry != null) 
            {
                this.Cursor = Cursors.WaitCursor;
                MediaThumbnail thumb = entry.Media.Thumbnails[0];
                try
                {
                    Stream stream = this.picasaService.Query(new Uri(thumb.Attributes["url"] as string));
                    this.AlbumPicture.Image = new Bitmap(stream);
                }
                catch
                {
                    Icon error = new Icon(SystemIcons.Exclamation, 40, 40);
                    this.AlbumPicture.Image = error.ToBitmap();
                }
                Album a = new Album();
                a.AtomEntry = entry;
                this.AlbumInspector.SelectedObject = a;
                this.Cursor = Cursors.Default;
            }
            else 
            {
                this.AlbumPicture.Image = null;
                this.AlbumInspector.SelectedObject = null; 
            }
        }

        private void SaveAlbumData_Click(object sender, System.EventArgs e)
        {
            foreach (ListViewItem item in this.AlbumList.SelectedItems) 
            {
                PicasaEntry entry = item.Tag as PicasaEntry;
                entry.Update();
            }
        }

        private void SaveAlbum_Click(object sender, System.EventArgs e)
        {
            foreach (ListViewItem item in this.AlbumList.SelectedItems) 
            {
                PicasaEntry entry = item.Tag as PicasaEntry;
                string photoUri = entry.FeedUri; 

                // Show the FolderBrowserDialog.
                DialogResult result = folderBrowserDialog.ShowDialog();

                PictureBrowser backgroundjob = new PictureBrowser(this.picasaService, true);
                backgroundjob.Show();
              
                if( result == DialogResult.OK )
                {
                    string folderName = folderBrowserDialog.SelectedPath;
                    if (photoUri != null) 
                    {
                        backgroundjob.BackupAlbum(photoUri, folderName);
                    }
                }
            }
        }
	}
}
