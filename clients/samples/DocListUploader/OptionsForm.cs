/* Copyright (c) 2007 Google Inc.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using Google.GData.Client;
using Google.GData.Documents;

namespace DocListUploader
{
    public partial class OptionsForm : Form
    {
        //The parent form to this one.
        private HiddenForm mainForm;

        /// <summary>
        /// Constructor for the OptionsForm.
        /// </summary>
        /// <param name="mainForm">The parent form that created this one.</param>
        public OptionsForm(HiddenForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void OptionsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Don't destroy this form, just hide it.
            if(e.CloseReason == CloseReason.UserClosing) {
                this.Hide();
                mainForm.HandleMinimize();
                e.Cancel = true;
            }
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            if (Username.Text == "")
            {
                MessageBox.Show("Please specify a username", "No user name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (Password.Text == "")
            {
                MessageBox.Show("Please specify a password", "No password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                LoginButton.Text = "Logging In";
                UploaderStatus.Text = "Connecting to server...";
                LoginButton.Enabled = false;
                LogoutButton.Enabled = true;
                RefreshButton.Enabled = true;
                Username.Enabled = false;
                Password.Enabled = false;
                mainForm.Login(Username.Text, Password.Text);
                LoginButton.Text = "Logged In";
                UploaderStatus.Text = "Login complete";
                UpdateDocList();

            }
            catch (Exception ex)
            {
                LoginButton.Enabled = true;
                LogoutButton.Enabled = false;
                Username.Enabled = true;
                Password.Enabled = true;
                RefreshButton.Enabled = false;
                LoginButton.Text = "Login";
                UploaderStatus.Text = "Error authenticating";
                MessageBox.Show("Error logging into Google Docs: " + ex.Message, "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Username_KeyPress(object sender, KeyPressEventArgs e)
        {
            //If the user hits enter, skip to the next field.
            if (e.KeyChar == (char) 13)
            {
                Password.Focus();
            }
        }

        private void Password_KeyPress(object sender, KeyPressEventArgs e)
        {
            //If the user hits enter, try to log in.
            if (e.KeyChar == (char) 13)
            {
                LoginButton_Click(null, null);
            }
        }

        private void OptionsForm_DragDrop(object sender, DragEventArgs e)
        {
            if (!mainForm.loggedIn)
            {
                MessageBox.Show("Please log in before uploading documents", "Upload Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string[] fileList = (string[]) e.Data.GetData(DataFormats.FileDrop);

            foreach (string file in fileList)
            {
                try
                {
                    UploaderStatus.Text = "Uploading " + file;
                    this.Refresh();
                    mainForm.UploadFile(file);
                    UploaderStatus.Text = "Successfully uploaded " + file;
                    UpdateDocList();
                }
                catch (ArgumentException)
                {
                    DialogResult result = MessageBox.Show("Error, unable to upload the file: '" + file + "'. It is not one of the valid types.", "Upload Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    UploaderStatus.Text = "Problems uploading";
                    if (result == DialogResult.Cancel)
                    {
                        return;
                    }
                }
                catch (Exception ex)
                {
                    DialogResult result = MessageBox.Show("Error, unable to upload the file: '" + file + "'. " + ex.Message, "Upload Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    UploaderStatus.Text = "Problems uploading";
                    if (result == DialogResult.Cancel)
                    {
                        return;
                    }
                }
            }

        }

        private void OptionsForm_DragEnter(object sender, DragEventArgs e)
        {
            //If they are dragging a file, let the cursor reflect
            //the operation is permitted.
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void LogoutButton_Click(object sender, EventArgs e)
        {
            mainForm.Logout();
            LoginButton.Enabled = true;
            LogoutButton.Enabled = false;
            Username.Enabled = true;
            Password.Enabled = true;
            RefreshButton.Enabled = false;
            LoginButton.Text = "Login";
            UploaderStatus.Text = "Logged out.";
        }

        /// <summary>
        /// Gets a new list of documents from the server and renders
        /// them in the ListView called DocList on the form.
        /// </summary>
        public void UpdateDocList()
        {
            if (!mainForm.loggedIn)
            {
                MessageBox.Show("Log in before retrieving documents.", "Log in", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DocList.Items.Clear();

            try
            {
                DocumentsFeed feed = mainForm.GetDocs();


                foreach (DocumentEntry entry in feed.Entries)
                {
                    string imageKey = "";
                    if (entry.IsDocument)
                    {
                        imageKey = "Document.gif";
                        
                    }
                    else if (entry.IsSpreadsheet)
                    {
                        imageKey = "Spreadsheet.gif";
                    }
                    else
                    {
                        imageKey = "Presentation.gif";
                    }

                    ListViewItem item = new ListViewItem(entry.Title.Text, imageKey);
                    item.SubItems.Add(entry.Updated.ToString());
                    item.Tag = entry;
                    DocList.Items.Add(item); 
                }

                foreach (ColumnHeader column in DocList.Columns)
                {
                    column.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error retrieving documents: " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Opens the currently selected document in the form's ListView 
        /// in the default browser.
        /// </summary>
        private void OpenSelectedDocument()
        {
            if (DocList.SelectedItems.Count > 0)
            {
                DocumentEntry entry = (DocumentEntry) DocList.SelectedItems[0].Tag;
                try
                {
                    Process.Start(entry.AlternateUri.ToString());
                }
                catch (Win32Exception)
                {
                    //nothing is registered to handle URLs, so let's use IE!
                    Process.Start("IExplore.exe", entry.AlternateUri.ToString());
                }
            }
        }

        private void contextCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (contextCheckBox.Checked)
            {
                mainForm.Register();
            }
            else
            {
                mainForm.UnRegister();
            }
        }

        private void OptionsForm_Resize(object sender, EventArgs e)
        {
            //When minimizing, hide this form from the taskbar.
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                mainForm.HandleMinimize();
            }
        }

        private void DocList_DoubleClick(object sender, EventArgs e)
        {
            OpenSelectedDocument();
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            UpdateDocList();
        }

        private void OpenMenuItem_Click(object sender, EventArgs e)
        {
            OpenSelectedDocument();
        }

        private void DeleteMenuItem_Click(object sender, EventArgs e)
        {
            if (DocList.SelectedItems.Count > 0)
            {
                DocumentEntry entry = (DocumentEntry) DocList.SelectedItems[0].Tag;
                DialogResult result = MessageBox.Show("Are you sure you want to delete " + entry.Title.Text + "?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        entry.Delete();
                        UpdateDocList();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error when deleting: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }
        }

    }
}