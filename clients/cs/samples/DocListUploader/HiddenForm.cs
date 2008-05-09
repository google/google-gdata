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
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;
using Microsoft.Win32;
using Google.GData.Documents;
using Google.GData.Client;

namespace DocListUploader
{
    /// <summary>
    /// The main form of the uploader, controls most of the service interaction.
    /// It is not actually displayed to the user.
    /// </summary>
    public partial class HiddenForm : Form
    {
        //A child form for the user to interact with.
        private OptionsForm optionsForm;
        //Keeps track of our logged in state.
        public bool loggedIn = false;
        //A connection with the DocList API.
        private DocumentsService service;
        //The name of the shell context menu option.
        private const string KEY_NAME = "Send to Google Docs";
        //Keeps track of if we've minimized the OptionsForm before.
        private bool firstMinimize = true;
        //The timer in milliseconds to display balloon tips.
        private const int BALLOON_TIMER = 10000;
        //The most recently uploaded document.
        private DocumentEntry lastUploadEntry = null;
        //Keeps track of if the last balloon tip was an upload complete message.
        private bool lastToolTipWasUpload = false;

        /// <summary>
        /// Constructor for the form.
        /// </summary>
        public HiddenForm()
        {
            InitializeComponent();
            optionsForm = new OptionsForm(this);
            ListenForSuccessor();
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            bool firstInstance;
            Mutex mutex = new Mutex(true, "Local\\DocListUploader", out firstInstance);
            if (!firstInstance)
            {
                //If we have an argument, we were most likely called by
                //the shell context menu
                if (args.Length == 1)
                {
                    NotifyPredecessor(args[0]);
                }
                return;
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new HiddenForm());
        }

        /// <summary>
        /// This is run by the first instance of the program in order
        /// to accept information from later instances using the
        /// .NET Framework's IPC mechanism.
        /// </summary>
        public void ListenForSuccessor()
        {
            IpcServerChannel serverChannel = new IpcServerChannel("DocListUploader");
            ChannelServices.RegisterChannel(serverChannel, false);

            RemoteMessage remoteMessage = new RemoteMessage(this);
            RemotingServices.Marshal(remoteMessage,"FirstInstance");
            
        }

        /// <summary>
        /// This is used by later instances of the program when
        /// executed from the shell's context menu to pass along
        /// the path of a file that will be uploaded. This message
        /// is passed using the .NET Framework's IPC mechanism.
        /// </summary>
        /// <param name="file">The file to be uploaded</param>
        public static void NotifyPredecessor(string file)
        {
            IpcClientChannel clientChannel = new IpcClientChannel();
            ChannelServices.RegisterChannel(clientChannel, false);

            RemoteMessage message = (RemoteMessage) Activator.GetObject(typeof(RemoteMessage), "ipc://DocListUploader/FirstInstance");

            if (!message.Equals(null))
            {
                message.SendMessage(file);
            }
        }

        /// <summary>
        /// Authenticates to Google servers
        /// </summary>
        /// <param name="username">The user's username (e-mail)</param>
        /// <param name="password">The user's password</param>
        /// <exception cref="AuthenticationException">Thrown on invalid credentials.</exception>
        public void Login(string username, string password)
        {
            if(loggedIn) {
                throw new ApplicationException("Already logged in.");
            }
            try
            {
                service = new DocumentsService("DocListUploader");
                ((GDataRequestFactory) service.RequestFactory).KeepAlive = false;
                service.setUserCredentials(username, password);
                //force the service to authenticate
                DocumentsListQuery query = new DocumentsListQuery();
                query.NumberToRetrieve = 1;
                service.Query(query);
                loggedIn = true;
            }
            catch(AuthenticationException e)
            {
                loggedIn = false;
                service = null;
                throw e;
            }
        }

        /// <summary>
        /// Logs the user out of Google Docs.
        /// </summary>
        public void Logout()
        {
            loggedIn = false;
            service = null;
        }

        /// <summary>
        /// Retrieves a list of documents from the server.
        /// </summary>
        /// <returns>The list of documents as a DocumentsFeed.</returns>
        public DocumentsFeed GetDocs()
        {
            DocumentsListQuery query = new DocumentsListQuery();
            DocumentsFeed feed = service.Query(query);
            return feed;
        }

        /// <summary>
        /// Some error handling around the file upload method. Used for handling files from the context menu.
        /// </summary>
        /// <param name="file">The file to upload.</param>
        public void HandleUpload(string file)
        {
            if (!loggedIn)
            {
                MessageBox.Show("Please log in before uploading documents", "Upload Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                DocListNotifyIcon.ShowBalloonTip(BALLOON_TIMER, "Uploading", "Uploading " + file, ToolTipIcon.Info);
                lastToolTipWasUpload = false;
                UploadFile(file);
                DocListNotifyIcon.ShowBalloonTip(BALLOON_TIMER, "Upload Success", "Successfully uploaded " + file, ToolTipIcon.Info);
                lastToolTipWasUpload = true;
                optionsForm.UpdateDocList();
            }
            catch (ArgumentException)
            {
                DocListNotifyIcon.ShowBalloonTip(BALLOON_TIMER, "Upload Error", "Error: unable to upload the file: '" + file + "'. It is not one of the valid types.", ToolTipIcon.Error);
                lastToolTipWasUpload = false;
            }
            catch (Exception ex)
            {
                DocListNotifyIcon.ShowBalloonTip(BALLOON_TIMER, "Upload Error", "Error: unable to upload the file: '" + file + "'. " + ex.Message, ToolTipIcon.Error);
                lastToolTipWasUpload = false;
            }
        }

        /// <summary>
        /// Uploads the file to Google Docs
        /// </summary>
        /// <param name="fileName">The file with path to upload</param>
        /// <exception cref="ApplicationException">Thrown when user isn't logged in.</exception>
        public void UploadFile(string fileName)
        {
            if (!loggedIn)
            {
                throw new ApplicationException("Need to be logged in to upload documents.");
            }
            else
            {
                lastUploadEntry = service.UploadDocument(fileName, null);
            }
        }

        /// <summary>
        /// Called when the OptionsForm is minimized/hidden.
        /// </summary>
        public void HandleMinimize()
        {
            if (firstMinimize)
            {
                DocListNotifyIcon.ShowBalloonTip(BALLOON_TIMER, "DocList Uploader", "Uploader now running in the system tray.", ToolTipIcon.Info);
                lastToolTipWasUpload = false;
                firstMinimize = false;
            }

        }

        /// <summary>
        /// Adds the application to the shell's context menu by
        /// creating a registry key.
        /// </summary>
        public void Register()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\Classes\\*\\shell\\" + KEY_NAME + "\\command");

            if (key == null)
            {
                key = Registry.CurrentUser.CreateSubKey("Software\\Classes\\*\\shell\\" + KEY_NAME + "\\command");
            }

            key.SetValue("", Application.ExecutablePath + " \"%1\"");
        }

        /// <summary>
        /// Removes the application from the shell's context menu
        /// by deleting a registry key.
        /// </summary>
        public void UnRegister()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\Classes\\*\\shell\\" + KEY_NAME);

            if (key != null)
            {
                Registry.CurrentUser.DeleteSubKeyTree("Software\\Classes\\*\\shell\\" + KEY_NAME);
            }
        }

        /// <summary>
        /// Displays the OptionsForm.
        /// </summary>
        private void ShowOptionsWindow()
        {
            optionsForm.Show();
            if (optionsForm.WindowState == FormWindowState.Minimized)
            {
                optionsForm.WindowState = FormWindowState.Normal;
            }
        }

        private void DocListNotifyIcon_DoubleClick(object sender, EventArgs e)
        {
            optionsForm.Show();
        }

        private void CloseMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void HiddenForm_Load(object sender, EventArgs e)
        {
            Hide();
            ShowOptionsWindow();
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowOptionsWindow();
        }

        private void HiddenForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            UnRegister();
        }

        private void DocListNotifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            //A single left click should be the same as a double-click.
            if (e.Button == MouseButtons.Left)
            {
                ShowOptionsWindow();
            }
        }


        private void DocListNotifyIcon_BalloonTipClicked(object sender, EventArgs e)
        {
            if (lastUploadEntry != null && lastToolTipWasUpload)
            {
                //display the last uploaded document in the browser.
                try
                {
                    Process.Start(lastUploadEntry.AlternateUri.ToString());
                }
                catch (Win32Exception)
                {
                    //nothing is registered to handle URLs, so let's use IE!
                    Process.Start("IExplore.exe", lastUploadEntry.AlternateUri.ToString());
                }
            }
        }


    }
}