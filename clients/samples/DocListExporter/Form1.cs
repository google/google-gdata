using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;

#region Google_Documents_List5 specific imports
using Google.GData.Client;
using Google.GData.Extensions;
using Google.GData.Documents;
using Google.GData.Tools;
using Google.Documents;
#endregion

namespace Google_DocumentsList
{
    public partial class Form1 : Form
    {
        private List<Document> all = new List<Document>();
        private DocumentsRequest request = null;

        public Form1()
        {
            InitializeComponent();

            GoogleClientLogin loginDialog = new GoogleClientLogin(new DocumentsService("GoogleDocumentsSample"), "youremailhere@gmail.com");
            if (loginDialog.ShowDialog() == DialogResult.OK)
            {
                RequestSettings settings = new RequestSettings("GoogleDocumentsSample", loginDialog.Credentials);
                settings.AutoPaging = true;
                settings.PageSize = 100;
                if (settings != null)
                {
                    this.request = new DocumentsRequest(settings);
                    this.Text = "Successfully logged in";

                    Feed<Document> feed = this.request.GetEverything();
                    // this takes care of paging the results in
                    foreach (Document entry in feed.Entries)
                    {
                        all.Add(entry);
                    }

                    TreeNode noFolder = null;
                    noFolder = new TreeNode("Items with no folder");
                    this.documentsView.Nodes.Add(noFolder);
                    noFolder.SelectedImageIndex = 0;
                    noFolder.ImageIndex = 0;

                    foreach (Document entry in all)
                    {
                        // let's add those with no parents for the toplevel
                        if (entry.ParentFolders.Count == 0)
                        {
                            if (entry.Type != Document.DocumentType.Folder)
                            {
                                AddToTreeView(noFolder.Nodes, entry);
                            }
                            else
                            {
                                TreeNode n = AddToTreeView(this.documentsView.Nodes, entry);
                                AddAllChildren(n.Nodes, entry);
                            }

                        }
                    }
                }
            }
        }

        private void AddAllChildren(TreeNodeCollection col, Document entry)
        {
            foreach (Document d in this.all)
            {
                if (d.ParentFolders.Contains(entry.Self))
                {
                    TreeNode n = AddToTreeView(col, d);
                    AddAllChildren(n.Nodes, d);
                }
            }
        }

        private TreeNode FindEntry(TreeNodeCollection coll, Document entry)
        {
            foreach (TreeNode n in coll)
            {
                // title is not specific enough
                Document d = n.Tag as Document;
                if (d.Id == entry.Id)
                    return n;
                TreeNode x = FindEntry(n.Nodes, entry);
                if (x != null)
                    return x;
            }
            return null;
        }

        private TreeNode AddToTreeView(TreeNodeCollection parent, Document doc)
        {
            TreeNode node = new TreeNode(doc.Title);
            node.Tag = doc;
            if (doc.Type != Document.DocumentType.Folder)
            {
                node.ImageIndex = 2;
                node.SelectedImageIndex = 2;
            }
            else
            {
                node.ImageIndex = 0;
                node.SelectedImageIndex = 0;
            }
            parent.Add(node);
            return node;
        }

        private TreeNodeCollection FindParentTreeNode(TreeNodeCollection coll, Document doc)
        {
            foreach (TreeNode n in coll)
            {
                Document d = n.Tag as Document;
                if (doc.ParentFolders.Contains(d.Self))
                {
                    // found it.
                    return n.Nodes;
                }
                TreeNodeCollection x = FindParentTreeNode(n.Nodes, doc);
                if (x != null)
                    return x;
            }

            return null;
        }

        private TreeNodeCollection CreateParentTreeNode(Document doc)
        {
            TreeNode ret = null;
            foreach (Document d in this.all)
            {
                if (doc.ParentFolders.Contains(d.Self))
                {
                    TreeNodeCollection parent = null;
                    if (d.ParentFolders.Count != 0)
                    {
                        parent = FindParentTreeNode(this.documentsView.Nodes, d);
                    }
                    ret = AddToTreeView(parent == null ? this.documentsView.Nodes : parent, d);
                    return ret.Nodes;
                }
            }
            return this.documentsView.Nodes;
        }

        private void documentsView_AfterExpand(object sender, TreeViewEventArgs e)
        {
            TreeNode node = e.Node;
            Document d = node.Tag as Document;
            Document.DocumentType type = d == null ? Document.DocumentType.Folder : d.Type;
     
            if (node.Nodes.Count > 0 && type == Document.DocumentType.Folder)
            {
                node.SelectedImageIndex = 1;
                node.ImageIndex = 1;
            }
        }

        private void documentsView_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            TreeNode node = e.Node;

            Document d = node.Tag as Document;
            Document.DocumentType type = d == null ? Document.DocumentType.Folder : d.Type;

            if (node.Nodes.Count > 0 && type == Document.DocumentType.Folder)
            {
                node.SelectedImageIndex = 0;
                node.ImageIndex = 0;
            }
        }

        private void documentsView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode node = e.Node;
            Document d = node.Tag as Document;
            if (d != null && d.Type != Document.DocumentType.Folder)
            {
                this.propertyGrid1.SelectedObject = d;
                if (d.Type == Document.DocumentType.PDF)
                {
                    this.Export.Enabled = this.ShowRevisions.Enabled = false;
                }
                else
                {
                    this.Export.Enabled = this.ShowRevisions.Enabled =  true;
                }
            }
            else
            {
                this.propertyGrid1.SelectedObject = null;
                this.Export.Enabled = false;
            }
        }

        private void Export_Click(object sender, EventArgs e)
        {
            TreeNode node = this.documentsView.SelectedNode;
            if (node == null)
                return;

            Document d = node.Tag as Document;
            // fill the filter based on the document type
            switch (d.Type)
            {
                case Document.DocumentType.Presentation:
                    this.exportDialog.Filter = "PDF|*.pdf|Flash|*.swf|Powerpoint|*.ppt";
                    break;
                case Document.DocumentType.Spreadsheet:
                    this.exportDialog.Filter = "PDF|*.pdf|HTML|*.html|Excel|*.xls|Comma seperated|*.csv|Open Document Spreadsheet|*.ods|Tab seperated|*.tsv";
                    break;
                case Document.DocumentType.PDF:
                    return;
                default:
                    this.exportDialog.Filter = "PDF|*.pdf|HTML|*.html|Text|*.txt|Open Document|*.ods|Rich Text|*.rtf|Microsoft Word|*.doc|Portable Networks Graphics|*.png";
                    break;
            }

            

            if (this.exportDialog.ShowDialog() == DialogResult.OK)
            {

                Document.DownloadType type = Document.DownloadType.pdf;

                switch (d.Type)
                {
                    case Document.DocumentType.Presentation:
                        switch (this.exportDialog.FilterIndex)
                        {
                            case 2:
                                type = Document.DownloadType.swf;
                                break;
                            case 3:
                                type = Document.DownloadType.ppt;
                                break;
                        }
                        break;
                    case Document.DocumentType.Spreadsheet:
                        switch (this.exportDialog.FilterIndex)
                        {
                            case 2:
                                type = Document.DownloadType.html;
                                break;
                            case 3:
                                type = Document.DownloadType.xls;
                                break;
                            case 4:
                                type = Document.DownloadType.csv;
                                break;
                            case 5:
                                type = Document.DownloadType.ods;
                                break;
                            case 6:
                                type = Document.DownloadType.tsv;
                                break;
                        }
                        break;
                    default:
                        switch (this.exportDialog.FilterIndex)
                        {
                            case 2:
                                type = Document.DownloadType.html;
                                break;
                            case 3:
                                type = Document.DownloadType.txt;
                                break;
                            case 4:
                                type = Document.DownloadType.ods;
                                break;
                            case 5:
                                type = Document.DownloadType.rtf;
                                break;
                            case 6:
                                type = Document.DownloadType.doc;
                                break;
                            case 7:
                                type = Document.DownloadType.png;
                                break;
                        }
                        break;
                }

                Stream stream = this.request.Download(d, type);

                Stream file = this.exportDialog.OpenFile();

                if (file != null)
                {
                    int nBytes = 2048;
                    int count = 0;
                    Byte[] arr = new Byte[nBytes];

                    do
                    {
                        count = stream.Read(arr, 0, nBytes);
                        file.Write(arr, 0, count);

                    } while (count > 0);
                    file.Flush();
                    file.Close();
                }
                stream.Close();
            }
        }


        private void ShowRevisions_Click(object sender, EventArgs e)
        {
            TreeNode documentNode = this.documentsView.SelectedNode;
            if (documentNode == null)
                return;

            
            Document d = documentNode.Tag as Document;

            if (d.Type == Document.DocumentType.Folder)
                return;

            // fill the filter based on the document type

            // so we have a document and a revision link, get that feed
            Feed<Document> revFeed = this.Request.Get<Document>(d.RevisionDocument);

            foreach (Document doc in revFeed.Entries)
            {
                TreeNode node = new TreeNode(doc.Title);
                node.Tag = doc;
                node.ImageIndex = 2;
                node.SelectedImageIndex = 2;
            
                TreeNode author = new TreeNode(doc.Author + ": " + doc.Updated);
                author.ImageIndex = 2;
                author.SelectedImageIndex = 2;
            
                node.Nodes.Add(author);
                author.Tag = doc;
                documentNode.Nodes.Add(node);
                documentNode.ExpandAll();
            }
        }

    
        public DocumentsRequest Request
        {
            get
            {
                return this.request;
            }
        }

        private void showPDFs_Click(object sender, EventArgs e)
        {

        }
    }
}