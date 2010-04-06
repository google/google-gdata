using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Google.GData.Client;
using Google.GData.Blogger;
using System.Net;

namespace Blogger
{
	/// <summary>
	/// Summary description for Calendar.
	/// </summary>
    public class Blogger : System.Windows.Forms.Form
    {
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox UserName;
        private System.Windows.Forms.TextBox Password;
        private System.Windows.Forms.Button Go;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;
        private System.Windows.Forms.TextBox BloggerURI;
        private System.Windows.Forms.TreeView FeedView;
        private System.Windows.Forms.Button AddEntry;
        public System.Windows.Forms.ComboBox FeedChooser;


        private ArrayList entryList; 
        private string feedUri; 

        public Blogger()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            this.entryList = new ArrayList();
            this.feedUri = null; 
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() 
        {
            Application.Run(new Blogger());
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
            this.BloggerURI = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.UserName = new System.Windows.Forms.TextBox();
            this.Password = new System.Windows.Forms.TextBox();
            this.Go = new System.Windows.Forms.Button();
            this.FeedView = new System.Windows.Forms.TreeView();
            this.AddEntry = new System.Windows.Forms.Button();
            this.FeedChooser = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // BloggerURI
            // 
            this.BloggerURI.Location = new System.Drawing.Point(88, 16);
            this.BloggerURI.Name = "BloggerURI";
            this.BloggerURI.Size = new System.Drawing.Size(296, 20);
            this.BloggerURI.TabIndex = 1;
            this.BloggerURI.Text = "http://www.blogger.com/feeds/default/blogs";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "URL:";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 24);
            this.label2.TabIndex = 3;
            this.label2.Text = "User:";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(8, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Password";
            // 
            // UserName
            // 
            this.UserName.Location = new System.Drawing.Point(88, 48);
            this.UserName.Name = "UserName";
            this.UserName.Size = new System.Drawing.Size(296, 20);
            this.UserName.TabIndex = 5;
            this.UserName.Text = "<yourUserName>";
            // 
            // Password
            // 
            this.Password.Location = new System.Drawing.Point(88, 88);
            this.Password.Name = "Password";
            this.Password.PasswordChar = '*';
            this.Password.Size = new System.Drawing.Size(296, 20);
            this.Password.TabIndex = 6;
            this.Password.Text = "<yourPassword>";
            // 
            // Go
            // 
            this.Go.Location = new System.Drawing.Point(440, 16);
            this.Go.Name = "Go";
            this.Go.Size = new System.Drawing.Size(104, 24);
            this.Go.TabIndex = 7;
            this.Go.Text = "&Go";
            this.Go.Click += new System.EventHandler(this.Go_Click);
            // 
            // FeedView
            // 
            this.FeedView.ImageIndex = -1;
            this.FeedView.Location = new System.Drawing.Point(16, 160);
            this.FeedView.Name = "FeedView";
            this.FeedView.SelectedImageIndex = -1;
            this.FeedView.Size = new System.Drawing.Size(544, 280);
            this.FeedView.TabIndex = 8;
            // 
            // AddEntry
            // 
            this.AddEntry.Enabled = false;
            this.AddEntry.Location = new System.Drawing.Point(440, 56);
            this.AddEntry.Name = "AddEntry";
            this.AddEntry.Size = new System.Drawing.Size(104, 24);
            this.AddEntry.TabIndex = 9;
            this.AddEntry.Text = "&Add...";
            this.AddEntry.Click += new System.EventHandler(this.AddEntry_Click);
            // 
            // FeedChooser
            // 
            this.FeedChooser.Location = new System.Drawing.Point(16, 120);
            this.FeedChooser.Name = "FeedChooser";
            this.FeedChooser.Size = new System.Drawing.Size(368, 21);
            this.FeedChooser.TabIndex = 10;
            this.FeedChooser.Text = "choose the feed.... ";
            this.FeedChooser.SelectedIndexChanged += new System.EventHandler(this.FeedChooser_SelectedIndexChanged);
            // 
            // Blogger
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(592, 470);
            this.Controls.Add(this.FeedChooser);
            this.Controls.Add(this.AddEntry);
            this.Controls.Add(this.FeedView);
            this.Controls.Add(this.Go);
            this.Controls.Add(this.Password);
            this.Controls.Add(this.UserName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BloggerURI);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.Name = "Blogger";
            this.Text = "Blogger Demo Application";
            this.ResumeLayout(false);

        }
        #endregion


        private void Go_Click(object sender, System.EventArgs e)
        {
            RefreshFeedList(); 
        }

        private void RefreshFeedList()
        {
            string bloggerURI  = this.BloggerURI.Text;
            string userName =    this.UserName.Text;
            string passWord =    this.Password.Text;

            BloggerQuery query = new BloggerQuery();
            BloggerService service = new BloggerService("BloggerSampleApp.NET");

            if (userName != null && userName.Length > 0)
            {
                service.Credentials = new GDataCredentials(userName, passWord);
            }

            // only get event's for today - 1 month until today + 1 year

            query.Uri = new Uri(bloggerURI);

            Cursor.Current = Cursors.WaitCursor; 

            // start repainting
            this.FeedChooser.BeginUpdate(); 

            BloggerFeed bloggerFeed = service.Query(query);
            // Display a wait cursor while the TreeNodes are being created.

            this.FeedChooser.DisplayMember = "Title"; 

            while (bloggerFeed != null && bloggerFeed.Entries.Count > 0)
            {
                foreach (BloggerEntry entry in bloggerFeed.Entries) 
                {
                    int iIndex = this.FeedChooser.Items.Add(new ListEntry(entry)); 
                }
                // do the chunking...
                if (bloggerFeed.NextChunk != null) 
                {
                    query.Uri = new Uri(bloggerFeed.NextChunk); 
                    bloggerFeed = service.Query(query);
                }
                else 
                {
                    bloggerFeed = null; 
                }
            }

            if (this.FeedChooser.Items.Count > 0) 
            {
                this.FeedChooser.Items.Insert(0, "Choose the feed..."); 
            }
            else 
            {
                 this.FeedChooser.Items.Insert(0, "No feeds for this user..."); 
            }
            
            
            // Reset the cursor to the default for all controls.
            Cursor.Current = Cursors.Default;
            // End repainting the combobox
            this.FeedChooser.EndUpdate();
        
            
        }



        private void RefreshFeed(string bloggerURI) 
        {
            string userName =    this.UserName.Text;
            string passWord =    this.Password.Text;

            // Suppress repainting the TreeView until all the objects have been created.
            this.FeedView.BeginUpdate(); 
            // Clear the TreeView each time the method is called.
            this.FeedView.Nodes.Clear(); 

            if (this.feedUri != null)
            {
                BloggerQuery query = new BloggerQuery();
                BloggerService service = new BloggerService("BloggerSampleApp.NET");

                if (userName != null && userName.Length > 0)
                {
                    service.Credentials = new GDataCredentials(userName, passWord);
                }

                // only get event's for today - 1 month until today + 1 year

                query.Uri = new Uri(bloggerURI);

                // set wait cursor before...
                Cursor.Current = Cursors.WaitCursor; 
            

                BloggerFeed bloggerFeed = service.Query(query);

                // Reset the cursor to the default for all controls.
                Cursor.Current = Cursors.Default;



                // now populate the calendar
                while (bloggerFeed != null && bloggerFeed.Entries.Count > 0)
                {
                    foreach (BloggerEntry entry in bloggerFeed.Entries) 
                    {
                        int iIndex = this.FeedView.Nodes.Add(new TreeNode(entry.Title.Text)); 
                        if (iIndex >= 0) 
                        {
                            AtomPerson author = entry.Authors[0];
                            this.FeedView.Nodes[iIndex].Nodes.Add(new TreeNode("by: " + author.Name));
                            this.FeedView.Nodes[iIndex].Nodes.Add(new TreeNode("published: " + entry.Published.ToString())); 
                            if (entry.Content.Content.Length > 50) 
                            {
                                this.FeedView.Nodes[iIndex].Nodes.Add(new TreeNode(entry.Content.Content.Substring(0, 50)+"....")); 
                            } 
                            else 
                            {
                                this.FeedView.Nodes[iIndex].Nodes.Add(new TreeNode(entry.Content.Content)); 
                            }

                        }
                    }

                    // do the chunking...
                    if (bloggerFeed.NextChunk != null) 
                    {
                        query.Uri = new Uri(bloggerFeed.NextChunk); 
                        bloggerFeed = service.Query(query);
                    }
                    else
                    {
                        bloggerFeed = null;
                    }
                }
            }

            // Reset the cursor to the default for all controls.
            Cursor.Current = Cursors.Default;
            // stop repainting the TreeView.
            this.FeedView.EndUpdate();
        }

        private void AddEntry_Click(object sender, System.EventArgs e)
        {
            addentry dlg = new addentry();
            if (dlg.ShowDialog() == DialogResult.OK &&
                dlg.Entry.Length > 0) 
            {
                // now add this to the feed.

                BloggerEntry entry = new BloggerEntry();

                entry.Content.Content = dlg.Entry;
                entry.Content.Type = "html"; 
                entry.Title.Text = dlg.EntryTitle;


                string userName =    this.UserName.Text;
                string passWord =    this.Password.Text;

                BloggerService service = new BloggerService("BloggerSampleApp.NET");

                if (userName != null && userName.Length > 0)
                {
                    service.Credentials = new GDataCredentials(userName, passWord);
                }

                service.Insert(new Uri(this.feedUri), entry); 

                RefreshFeed(this.feedUri);
            }
        }

        /// <summary>
        /// get's called when a new feed was choosen from the combobox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FeedChooser_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            ListEntry listEntry = this.FeedChooser.SelectedItem as ListEntry; 

            this.AddEntry.Enabled = false; 
            this.feedUri = null; 

            if (listEntry != null) 
            {
                BloggerEntry entry = listEntry.Entry; 

                if (entry != null) 
                {
                    // find the link.rel==feed uri and refresh the treeview
                    foreach (AtomLink link in entry.Links) 
                    {
                        if (link.Rel == BaseNameTable.ServiceFeed) 
                        {
                            this.feedUri = link.HRef.ToString(); 
                            break;
                        }
                    }

                    if (this.feedUri != null) 
                    {
                        this.AddEntry.Enabled = true; 
                        
                    }
                }
            }
            RefreshFeed(this.feedUri); 
        }
    }


    /// <summary>
    /// little helper class to put an atomentry into a combobox
    /// </summary>
    public class ListEntry 
    {
        private BloggerEntry entry;

        public BloggerEntry Entry 
        {
            get 
            {
                return this.entry;
            }
            set 
            {
                this.entry = value;
            }
        }

        public ListEntry(BloggerEntry entry)
        {
            this.entry = entry; 
        }

        public override string ToString() 
        {
            return this.entry != null ? this.entry.Title.Text : "No Entry"; 
        }
    }


}
