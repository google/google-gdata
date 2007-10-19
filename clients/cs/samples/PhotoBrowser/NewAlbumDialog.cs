using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Google.GData.Photos;
using Google.GData.Extensions.MediaRss;

namespace PhotoBrowser
{
	/// <summary>
	/// Summary description for NewAlbumDialog.
	/// </summary>
	public class NewAlbumDialog : System.Windows.Forms.Form
	{
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox AlbumName;
        private System.Windows.Forms.TextBox AlbumDescription;
        private System.Windows.Forms.TextBox AlbumKeywords;
        private System.Windows.Forms.TextBox AlbumLocation;
        private System.Windows.Forms.Button Ok;
        private System.Windows.Forms.Button Cancel;
        private PicasaService service;
        private PicasaFeed feed;
        private PicasaEntry newEntry = null;
        private System.Windows.Forms.RadioButton AlbumPublic;
        private System.Windows.Forms.RadioButton AlbumPrivate;
        private System.Windows.Forms.CheckBox AllowComments;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public NewAlbumDialog(PicasaService service, PicasaFeed feed)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
            this.service = service;
            this.feed = feed;
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.AlbumPublic = new System.Windows.Forms.RadioButton();
            this.AlbumPrivate = new System.Windows.Forms.RadioButton();
            this.AllowComments = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.AlbumName = new System.Windows.Forms.TextBox();
            this.AlbumKeywords = new System.Windows.Forms.TextBox();
            this.AlbumLocation = new System.Windows.Forms.TextBox();
            this.AlbumDescription = new System.Windows.Forms.TextBox();
            this.Ok = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(16, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name of the Album:";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(16, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 24);
            this.label2.TabIndex = 1;
            this.label2.Text = "Description:";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(16, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 32);
            this.label3.TabIndex = 2;
            this.label3.Text = "Location:";
            // 
            // AlbumPublic
            // 
            this.AlbumPublic.Checked = true;
            this.AlbumPublic.Location = new System.Drawing.Point(16, 128);
            this.AlbumPublic.Name = "AlbumPublic";
            this.AlbumPublic.TabIndex = 5;
            this.AlbumPublic.TabStop = true;
            this.AlbumPublic.Text = "public";
            // 
            // AlbumPrivate
            // 
            this.AlbumPrivate.Location = new System.Drawing.Point(16, 160);
            this.AlbumPrivate.Name = "AlbumPrivate";
            this.AlbumPrivate.Size = new System.Drawing.Size(128, 24);
            this.AlbumPrivate.TabIndex = 6;
            this.AlbumPrivate.Text = "private";
            // 
            // AllowComments
            // 
            this.AllowComments.Location = new System.Drawing.Point(176, 128);
            this.AllowComments.Name = "AllowComments";
            this.AllowComments.Size = new System.Drawing.Size(88, 32);
            this.AllowComments.TabIndex = 7;
            this.AllowComments.Text = "Allow Comments?";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(16, 96);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(160, 24);
            this.label4.TabIndex = 6;
            this.label4.Text = "Keywords:";
            // 
            // AlbumName
            // 
            this.AlbumName.Location = new System.Drawing.Point(160, 24);
            this.AlbumName.Name = "AlbumName";
            this.AlbumName.Size = new System.Drawing.Size(192, 22);
            this.AlbumName.TabIndex = 1;
            this.AlbumName.Text = "";
            this.AlbumName.TextChanged += new System.EventHandler(this.AlbumName_TextChanged);
            // 
            // AlbumKeywords
            // 
            this.AlbumKeywords.Location = new System.Drawing.Point(160, 96);
            this.AlbumKeywords.Name = "AlbumKeywords";
            this.AlbumKeywords.Size = new System.Drawing.Size(192, 22);
            this.AlbumKeywords.TabIndex = 4;
            this.AlbumKeywords.Text = "";
            // 
            // AlbumLocation
            // 
            this.AlbumLocation.Location = new System.Drawing.Point(160, 72);
            this.AlbumLocation.Name = "AlbumLocation";
            this.AlbumLocation.Size = new System.Drawing.Size(192, 22);
            this.AlbumLocation.TabIndex = 3;
            this.AlbumLocation.Text = "";
            // 
            // AlbumDescription
            // 
            this.AlbumDescription.Location = new System.Drawing.Point(160, 48);
            this.AlbumDescription.Name = "AlbumDescription";
            this.AlbumDescription.Size = new System.Drawing.Size(192, 22);
            this.AlbumDescription.TabIndex = 2;
            this.AlbumDescription.Text = "";
            this.AlbumDescription.TextChanged += new System.EventHandler(this.AlbumDescription_TextChanged);
            // 
            // Ok
            // 
            this.Ok.Enabled = false;
            this.Ok.Location = new System.Drawing.Point(328, 208);
            this.Ok.Name = "Ok";
            this.Ok.Size = new System.Drawing.Size(88, 32);
            this.Ok.TabIndex = 9;
            this.Ok.Text = "&Ok";
            this.Ok.Click += new System.EventHandler(this.Ok_Click);
            // 
            // Cancel
            // 
            this.Cancel.Location = new System.Drawing.Point(232, 208);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(80, 32);
            this.Cancel.TabIndex = 8;
            this.Cancel.Text = "&Cancel";
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // NewAlbumDialog
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
            this.ClientSize = new System.Drawing.Size(440, 260);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.Ok);
            this.Controls.Add(this.AlbumDescription);
            this.Controls.Add(this.AlbumLocation);
            this.Controls.Add(this.AlbumKeywords);
            this.Controls.Add(this.AlbumName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.AllowComments);
            this.Controls.Add(this.AlbumPrivate);
            this.Controls.Add(this.AlbumPublic);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "NewAlbumDialog";
            this.Text = "Create a new Album:";
            this.ResumeLayout(false);

        }
		#endregion

        private void Cancel_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void Ok_Click(object sender, System.EventArgs e)
        {
            AlbumEntry entry = new AlbumEntry();
            AlbumAccessor acc = new AlbumAccessor(entry);
            entry.Title.Text = this.AlbumName.Text;
            entry.Summary.Text = this.AlbumDescription.Text;
            if (this.AlbumLocation.Text.Length > 0) 
            {
                acc.Location = this.AlbumLocation.Text;
            }
            if (this.AlbumKeywords.Text.Length > 0) 
            {
                entry.Media = new MediaGroup();
                MediaKeywords keywords = new MediaKeywords(this.AlbumKeywords.Text);
                entry.Media.Keywords = keywords;
            }
            acc.Access = this.AlbumPublic.Checked ? "public" : "private";
            acc.CommentingEnabled = this.AllowComments.Checked;

            this.newEntry = this.service.Insert(this.feed, entry) as PicasaEntry;
            this.Close();

        }

        private void AlbumName_TextChanged(object sender, System.EventArgs e)
        {
            verifyOk();
        }

        private void AlbumDescription_TextChanged(object sender, System.EventArgs e)
        {
            verifyOk();
        }

        private void verifyOk()
        {
            this.Ok.Enabled = (this.AlbumName.Text.Length > 0 && this.AlbumDescription.Text.Length > 0);
        }

        public PicasaEntry CreatedEntry
        {
            get
            {
                return this.newEntry;
            }
        }

       
       
	}
}
