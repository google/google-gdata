using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Google.GData.Client;
using Google.GData.CodeSearch;

namespace CodeSearch
{
    /// <summary>
    /// Summary description for CodeSearch.
    /// </summary>
    public class CodeSearch : System.Windows.Forms.Form
    {
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;
        private System.Windows.Forms.TreeView FeedView;


        private ArrayList entryList;
        private System.Windows.Forms.TextBox CodeSearchURI;
        private System.Windows.Forms.TextBox Query;
        private System.Windows.Forms.Button Fetch_Code; 

        public CodeSearch()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
            this.entryList = new ArrayList();
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() 
        {
            Application.Run(new CodeSearch());
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
            this.CodeSearchURI = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Query = new System.Windows.Forms.TextBox();
            this.Fetch_Code = new System.Windows.Forms.Button();
            this.FeedView = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // CodeSearchURI
            // 
            this.CodeSearchURI.Location = new System.Drawing.Point(88, 16);
            this.CodeSearchURI.Name = "CodeSearchURI";
            this.CodeSearchURI.Size = new System.Drawing.Size(296, 20);
            this.CodeSearchURI.TabIndex = 1;
            this.CodeSearchURI.Text = "https://www.google.com/codesearch/feeds/search";
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
            this.label2.Text = "Query";
            // 
            // Query
            // 
            this.Query.Location = new System.Drawing.Point(88, 48);
            this.Query.Name = "Query";
            this.Query.Size = new System.Drawing.Size(296, 20);
            this.Query.TabIndex = 5;
            this.Query.Text = "<termToLookFor>";
            // 
            // Fetch_Code
            // 
            this.Fetch_Code.Location = new System.Drawing.Point(440, 16);
            this.Fetch_Code.Name = "Fetch_Code";
            this.Fetch_Code.Size = new System.Drawing.Size(104, 24);
            this.Fetch_Code.TabIndex = 7;
            this.Fetch_Code.Text = "&Fetch Code";
            this.Fetch_Code.Click += new System.EventHandler(this.Go_Click);
            // 
            // FeedView
            // 
            this.FeedView.ImageIndex = -1;
            this.FeedView.Location = new System.Drawing.Point(16, 112);
            this.FeedView.Name = "FeedView";
            this.FeedView.SelectedImageIndex = -1;
            this.FeedView.Size = new System.Drawing.Size(544, 280);
            this.FeedView.TabIndex = 8;
            // 
            // CodeSearch
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(592, 414);
            this.Controls.Add(this.FeedView);
            this.Controls.Add(this.Fetch_Code);
            this.Controls.Add(this.Query);
            this.Controls.Add(this.CodeSearchURI);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.Name = "CodeSearch";
            this.Text = "Google Code Search Demo";
            this.ResumeLayout(false);

        }
        #endregion


        private void Go_Click(object sender, System.EventArgs e)
        {
            RefreshFeedList(); 
        }

        private void RefreshFeedList()
        {
            string codeSearchURI  = this.CodeSearchURI.Text;

            FeedQuery query = new FeedQuery();
            CodeSearchService service = new CodeSearchService("CodeSearchSampleApp");

            query.Uri = new Uri(codeSearchURI);
            query.Query = this.Query.Text;
            query.StartIndex = 1;
            query.NumberToRetrieve = 2;
            // start repainting
            this.FeedView.BeginUpdate();  
            Cursor.Current = Cursors.WaitCursor;
            // send the request.
            CodeSearchFeed feed = service.Query(query);
   
            // Clear the TreeView each time the method is called.
            this.FeedView.Nodes.Clear(); 
            int iIndex = this.FeedView.Nodes.Add(new TreeNode(feed.Title.Text));
            this.FeedView.Nodes.Add(new TreeNode("Number of entries "));
            this.FeedView.Nodes.Add(new TreeNode(feed.Entries.Count.ToString())); 
            
            if (iIndex >= 0) 
            {
                foreach (CodeSearchEntry entry in feed.Entries) 
                {
                    this.FeedView.Nodes[iIndex].Nodes.Add(new TreeNode(
                        "Entry title: " + entry.Title.Text));
                    this.FeedView.Nodes[iIndex].Nodes.Add(new TreeNode(
                        "File Name"));
                    this.FeedView.Nodes[iIndex].Nodes.Add(new TreeNode(
                        entry.FileElement.Name));

                    this.FeedView.Nodes[iIndex].Nodes.Add(new TreeNode(
                        "Package Name"));
                    this.FeedView.Nodes[iIndex].Nodes.Add(new TreeNode(
                        entry.PackageElement.Name));
                    this.FeedView.Nodes[iIndex].Nodes.Add(new TreeNode(
                        "Package Uri"));
                    this.FeedView.Nodes[iIndex].Nodes.Add(new TreeNode(
                        entry.PackageElement.Uri));

                    int jIndex = this.FeedView.Nodes[iIndex].Nodes.Add(new TreeNode(
                        "Matches:"));
                    foreach (Match m in entry.Matches)
                    {
                        this.FeedView.Nodes[iIndex].Nodes[jIndex].Nodes.Add(new TreeNode(
                            m.LineNumber + ": " +
                            m.LineTextElement));
                    }
                }
            }
            // Reset the cursor to the default for all controls.
            Cursor.Current = Cursors.Default;
            // End repainting the combobox
            this.FeedView.EndUpdate();
        
            
        }
    }
}
