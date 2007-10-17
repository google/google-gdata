using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using Google.GData.Photos;
using Google.GData.Extensions.MediaRss;
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
        private System.Windows.Forms.Button button1;
        private PicasaService picasaService = new PicasaService("PhotoBrowser");


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
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // AlbumList
            // 
            this.AlbumList.FullRowSelect = true;
            this.AlbumList.GridLines = true;
            this.AlbumList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.AlbumList.LabelEdit = true;
            this.AlbumList.Location = new System.Drawing.Point(8, 56);
            this.AlbumList.Name = "AlbumList";
            this.AlbumList.Size = new System.Drawing.Size(296, 152);
            this.AlbumList.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.AlbumList.TabIndex = 0;
            this.AlbumList.View = System.Windows.Forms.View.List;
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
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(16, 480);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(120, 40);
            this.button1.TabIndex = 5;
            this.button1.Text = "&Save Album Data";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // PhotoBrowser
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
            this.ClientSize = new System.Drawing.Size(512, 552);
            this.Controls.Add(this.button1);
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
                GoogleClientLogin loginDialog = new GoogleClientLogin(); 
                loginDialog.ShowDialog(this);
                this.googleAuthToken = loginDialog.AuthenticationToken;
                this.user = loginDialog.User;

                picasaService.SetAuthenticationToken(loginDialog.AuthenticationToken);
                UpdateAlbumFeed();
            }
        
        }


        private void UpdateAlbumFeed()
        {
            AlbumQuery query = new AlbumQuery();

            this.AlbumList.Clear();

            
            query.Uri = new Uri(PicasaQuery.CreatePicasaUri(this.user));

            PicasaFeed feed = this.picasaService.Query(query);

            if (feed != null && feed.Entries.Count > 0) 
            {
                foreach (PicasaEntry entry in feed.Entries)
                {
                    ListViewItem item = new ListViewItem(entry.Title.Text + 
                                    " (" + entry.getPhotoExtensionValue(GPhotoNameTable.NumPhotos) + " )");
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
                MediaThumbnail thumb = entry.Media.Thumbnails[0];
                Stream stream  = this.picasaService.Query(new Uri(thumb.Attributes["url"] as string));
                this.AlbumPicture.Image = new Bitmap(stream);
                this.AlbumInspector.SelectedObject = new AlbumMeta(entry);
            }
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            foreach (ListViewItem item in this.AlbumList.SelectedItems) 
            {
                PicasaEntry entry = item.Tag as PicasaEntry;
                entry.Update();
            }
        }
	}
}
