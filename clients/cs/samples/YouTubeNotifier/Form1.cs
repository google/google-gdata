using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using Microsoft.Win32;
using Google.GData.YouTube;
using Google.GData.Client;
using Google.YouTube;
using NotifierForYT.Properties;


namespace NotifierForYT
{
    public partial class YouTubeNotifier : Form
    {
        private NotifyIcon nIcon;
        private string authToken;
        private YouTubeRequest ytRequest;
        private string user;
        private int initialPullinHours;
        private int updateFrequency;
        private int notificationDuration;
        private bool closeThis=false;
        private DateTime lastUpdate;
        private const string AppKey = "NotifierYT"; 

        private const string YTNOTIFIERKEY = "Software\\GoogleYouTubeNotifier";
        private const string YTDEVKEY = "AI39si4v3E6oIYiI60ndCNDqnPP5lCqO28DSvvDPnQt-Mqia5uPz2e4E-gMSBVwHXwyn_LF1tWox4LyM-0YQd2o4i_3GcXxa2Q";

        private List<Activity> allActivities = new List<Activity>();
        private List<Video> allVideos = new List<Video>(); 

        public YouTubeNotifier()
        {
            InitializeComponent();
            CreateNotifyIcon();
            this.Icon = this.nIcon.Icon;

            if (CheckForFirstStartup() == true)
            {
                ShowSettings();
            }
            else
            {
                if (this.authToken != null)
                {
                    this.isAuthenticated.Checked = true;
                    UpdateActivities();
                }
            }
            this.Text = AppTitle;
            this.refreshTimer.Interval = 10000; 
            this.refreshTimer.Enabled = true;
        }

        private void ShowSettings()
        {
            ShowMe(this.tabPageSettings);
        }

        private void CreateNotifyIcon()
        {
            this.ShowInTaskbar = false;
            this.nIcon = new NotifyIcon(this.components);
            // Displays a TV Icon
            // Bitmap b = Resources.gdata_youtube;
            // this.nIcon.Icon = Icon.FromHandle(b.GetHicon());
            Bitmap b = Resources.ytfavicon;
            IntPtr Hicon = b.GetHicon();

            this.nIcon.Icon = Icon.FromHandle(Hicon);

            this.nIcon.Text = AppTitle + " - double click to restore it";
            this.nIcon.Visible = true;
            this.nIcon.ContextMenuStrip = this.defaultMenu;
            this.nIcon.DoubleClick += new EventHandler(nIcon_DoubleClick);
            HideMe();
        }

        private void nIcon_DoubleClick(Object sender, EventArgs e)
        {
            ToggleVisibility();
        }

        private bool CheckForFirstStartup()
        {
            RegistryKey ytNotifier = Registry.CurrentUser.OpenSubKey(YTNOTIFIERKEY);

            if (ytNotifier == null)
                return true;

            this.user = this.userName.Text = ytNotifier.GetValue("userName") as string;
            this.authToken = ytNotifier.GetValue("token") as string;
            this.InitialDataPullTime.Value = this.initialPullinHours = (int)ytNotifier.GetValue("initialDataPullTime", 24);
            this.UpdateFrequency.Value = this.updateFrequency = (int)ytNotifier.GetValue("updateFrequency", 15);
            this.notifcationBalloons.Value = this.notificationDuration = (int)ytNotifier.GetValue("notificationDuration", 10);

            string s = ytNotifier.GetValue("userList") as string;
            if (s != null)
            {
                string[] users = s.Split(',');
                foreach (string u in users)
                {
                    this.usernameGrid.Rows.Add(u);
                }
            }


            YouTubeRequestSettings settings = new YouTubeRequestSettings(AppKey, 
                    YTDEVKEY,
                    this.userName.Text,
                    "");

            this.ytRequest = new YouTubeRequest(settings);
            OnAuthenticationModified(this.authToken);
            return false;
        }


        private void OnAuthenticationModified(string token)
        {
            this.authToken = token;
            this.isAuthenticated.Checked = token != null;
            if (this.ytRequest != null && this.ytRequest.Service != null)
                this.ytRequest.Service.SetAuthenticationToken(token);
        }

        private void ButtonSaveSettings_Click(object sender, EventArgs e)
        {
            bool fHide = true; 
            if (this.isAuthenticated.Checked == false && this.userName.Text.Length > 0)
            {
                // let's see if we get a valid authtoken back for the passed in credentials....
                YouTubeRequestSettings settings = new YouTubeRequestSettings(AppKey,
                                    YTDEVKEY,
                                    this.userName.Text,
                                    this.passWord.Text);
                // settings.PageSize = 15;
                this.ytRequest = new YouTubeRequest(settings);
                try
                {
                    this.authToken = this.ytRequest.Service.QueryClientLoginToken();
                    this.passWord.Text = ""; 
                }
                catch
                {
                    MessageBox.Show("There was a problem with your credentials");
                    this.authToken = null;
                    fHide = false; 
                }
                OnAuthenticationModified(this.authToken);
            }

            // let's save the username to the registry, but not the password
            RegistryKey ytNotifier = Registry.CurrentUser.OpenSubKey(YTNOTIFIERKEY, true);

            if (ytNotifier == null)
            {
                ytNotifier = Registry.CurrentUser.CreateSubKey(YTNOTIFIERKEY);
            }

           
            ytNotifier.SetValue("initialDataPullTime", this.InitialDataPullTime.Value, RegistryValueKind.DWord);
            ytNotifier.SetValue("updateFrequency", this.UpdateFrequency.Value, RegistryValueKind.DWord);
            ytNotifier.SetValue("notificationDuration", this.notifcationBalloons.Value, RegistryValueKind.DWord);
            ytNotifier.SetValue("userList", GetUserNamesToSave());

            this.initialPullinHours = (int)this.InitialDataPullTime.Value;
            this.updateFrequency = (int)this.UpdateFrequency.Value;
            this.notificationDuration = (int)this.notifcationBalloons.Value;

            if (this.authToken != null)
            {
                ytNotifier.SetValue("userName", this.userName.Text);
                ytNotifier.SetValue("token", this.authToken);

                this.user = this.userName.Text;
            }

            if (fHide == true)
            {
                HideMe();
                UpdateActivities();
            }

        }


        private void userName_TextChanged(object sender, EventArgs e)
        {
            this.isAuthenticated.Checked = false;
        }

        private void passWord_TextChanged(object sender, EventArgs e)
        {
            this.isAuthenticated.Checked = false; 
        }

        private void ButtonIgnore_Click(object sender, EventArgs e)
        {
            HideMe();
            this.userName.Text = this.user;
            this.passWord.Text = null;
            this.UpdateFrequency.Value = this.updateFrequency;
            this.InitialDataPullTime.Value = this.initialPullinHours;
            this.notifcationBalloons.Value = this.notificationDuration; 
        }

        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            CheckForUpdateActivities();
        }

        private void CheckForUpdateActivities()
        {
            DateTime d = this.lastUpdate.AddMinutes(this.updateFrequency);
            if (d < DateTime.Now)
            {
                UpdateActivities();
            }
        }


        private List<string> GetUserNames()
        {
            // first update the not authenticated case. 
            List<string> users = new List<string>();

            foreach (DataGridViewRow r in this.usernameGrid.Rows)
            {
                if (r.Cells[0].Value != null)
                    users.Add(r.Cells[0].Value as string);
            }
            return users;
        }

        private string GetUserNamesToSave()
        {
            string ret = ""; 
            List<string> users = GetUserNames();
            foreach (string s in users)
            {
                if (String.IsNullOrEmpty(s) != true)
                {
                    if (ret.Length > 0)
                    {
                        ret += ",";
                    }
                    ret += s;
                }
            }
            return ret;
        }


        private void UpdateActivities()
        {

            this.refreshTimer.Enabled = false;

            DateTime since = DateTime.MinValue;

            if (this.allActivities.Count == 0)
            {
                // first call, do a modified-since query
                since = DateTime.Now.AddHours(-1 * this.initialPullinHours);
            
            }


            int iCounter = 0;
            this.nIcon.BalloonTipText = "";
          
            List<string> users = GetUserNames();

            if (users.Count > 0 && String.IsNullOrEmpty(users[0]) != true)
            {

                // let's see if we get a valid authtoken back for the passed in credentials....
                YouTubeRequestSettings settings = new YouTubeRequestSettings("YouTubeNotifier",
                                    YTDEVKEY);
                // settings.PageSize = 15;
                YouTubeRequest r = new YouTubeRequest(settings);
                Feed<Activity> pf = r.GetActivities(users, since);
                iCounter += ProcessFeed(pf, since); 
            }



            if (this.authToken != null)
            {
                Feed<Activity> f = this.ytRequest.GetActivities(since);

                iCounter += ProcessFeed(f, since);
            }


            // now redo the layout in the right order of controls:
            this.linkList.SuspendLayout();
            this.linkList.Controls.Clear();
            foreach (Activity act in this.allActivities)
            {
                LinkLabel l = new LinkLabel();
                string when = act.Updated.ToShortDateString();

                if (act.Updated.Date == DateTime.Now.Date)
                {
                    // it happened today
                    when = act.Updated.ToShortTimeString();
                }

                if (act.Updated.Date == DateTime.Now.AddDays(-1).Date)
                {
                    when = "yesterday, at " + act.Updated.ToShortTimeString();
                }

                l.Text = act.Author + " has ";

                bool noLink = false;
                int len = 5; 
                switch (act.Type)
                {
                    case ActivityType.Commented:
                        l.Text += "commented on ";
                        len = AddVideoText(l, act.VideoId);
                        break;
                    case ActivityType.Favorited:
                        l.Text += "favorited ";
                        len = AddVideoText(l, act.VideoId);
                        break;

                    case ActivityType.FriendAdded:
                        l.Text += "added a friend - " + act.Username ;
                        noLink = true;
                        break;
                    case ActivityType.Rated:
                        l.Text += "rated ";
                        len = AddVideoText(l, act.VideoId);
                        break;
                    case ActivityType.Shared:
                        l.Text += "shared ";
                        len = AddVideoText(l, act.VideoId);
                        break;
                    case ActivityType.SubscriptionAdded:
                        l.Text += "subscriped to " + act.Username;
                        noLink = true;
                        break;
                    case ActivityType.Uploaded:
                        l.Text += "uploaded ";
                        len = AddVideoText(l, act.VideoId);
                        break;
                }

                l.AutoSize = true;
                if (noLink == false)
                {
                    l.Links[0].Start = l.Text.Length - len;
                    l.Links[0].Length = len;
                    l.Links[0].LinkData = YouTubeQuery.CreateVideoWatchUri(act.VideoId);
                    l.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Label_LinkClicked);
                }
                else
                {
                    // create a link for the user
                    l.Links[0].Start = l.Text.Length - act.Username.Length;
                    l.Links[0].Length = act.Username.Length;
                    l.Links[0].LinkData = "https://www.youtube.com/user/" + act.Username;
                    l.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Label_LinkClicked);

                }
                l.Text += " (" + when + ")";

                this.linkList.Controls.Add(l);

            }
            this.linkList.ResumeLayout();

            if (iCounter > 0)
            {
                this.nIcon.BalloonTipTitle = "YouTube activities for " + this.userName.Text + " friends";
                this.nIcon.ShowBalloonTip(this.notificationDuration * 1000);
            }
            UpdateTitle();
            this.refreshTimer.Enabled = true;
            this.lastUpdate = DateTime.Now;
        }


        private int AddVideoText(LinkLabel l, string videoId)
        {
            int len = 5; 
            Video v = FindVideo(videoId);
            if (v != null)
            {
                l.Text += v.Title;
                len = v.Title.Length;
            }
            else
            {
                l.Text += " a video";
            }
            return len; 
        }


        private void UpdateTitle()
        {
            this.nIcon.Text = AppTitle + " " + this.UnreadCount + " unread activities";
        }


        private int ProcessFeed(Feed<Activity> feed, DateTime since)
        {
            List<Activity> newGuys = new List<Activity>(); 

            try
            {
                foreach (Activity a in feed.Entries)
                {
                    // first check if the activity is inside our maximum timeframe
                    // substract the max number of hours to pull
                    DateTime oldest = DateTime.Now.AddHours(-1 * this.initialPullinHours);

                    if (a.Updated > oldest)
                    {
                        // so it's part of the timeline. Let's see if we already have it

                        if (IsContained(a) == false)
                        {
                            AddBallonText(newGuys.Count, a.ToString());
                            if (since == DateTime.MinValue)
                            {
                                this.allActivities.Insert(0, a);
                            }
                            else
                            {
                                this.allActivities.Add(a);
                            }
                            newGuys.Add(a);
                        }
                    }
                }
            }
            catch (GDataForbiddenException e)
            {
                MessageBox.Show("Your accesstoken has expired.", "Please reauthenticate by providing your password again");
                OnAuthenticationModified(null);
                ShowSettings();
            }
            catch (InvalidCredentialsException e)
            {
                MessageBox.Show("Your accesstoken has expired.", "Please reauthenticate by providing your password again");
                OnAuthenticationModified(null);
                ShowSettings();
            }
            catch (GDataRequestException e)
            {
                HttpWebResponse r = e.Response as HttpWebResponse;
                if (r != null && r.StatusCode == HttpStatusCode.Unauthorized)
                {
                    MessageBox.Show("Your accesstoken has expired.", "Please reauthenticate by providing your password again");
                    OnAuthenticationModified(null);
                    ShowSettings();
                }
                else if (r != null && r.StatusCode == HttpStatusCode.NotModified)
                {
                    // nothing new there, so ignore the exception.
                }
                else
                    MessageBox.Show(e.ResponseString, "There was a problem getting the data from YouTube");
            }
            // now get the metadata for the new guys

            GetMetaData(newGuys);


            return newGuys.Count; 

        }


        private void GetMetaData(List<Activity> list)
        {
            Feed<Video> meta = this.ytRequest.GetVideoMetaData(list);

            foreach (Video v in meta.Entries)
            {
                if (IsContained(v) == false)
                {
                    this.allVideos.Add(v);
                }
            }
        }



        private int UnreadCount
        {
            get
            {
                int iCounter = 0;
                foreach (LinkLabel l in linkList.Controls)
                {
                    if (l.Links[0].Visited == false)
                        iCounter++;
                }
                return iCounter;
            }
        }

        private void AddBallonText(int iCounter, string text)
        {
            if (iCounter == 0)
                this.nIcon.BalloonTipText = text;
            else if (iCounter < 4)
                this.nIcon.BalloonTipText += "\r" + text;
            else if (iCounter == 4)
                this.nIcon.BalloonTipText += "\r" + "and more...";
        }

        private bool IsContained(Activity a)
        {
            foreach (Activity act in this.allActivities)
            {
                if (act.Id == a.Id)
                    return true;
            }
            return false;
        }

        private bool IsContained(Video v)
        {
            foreach (Video video in this.allVideos)
            {
                if (video.Id == v.Id)
                    return true;
            }
            return false;
        }

        private Video FindVideo(string videoId)
        {
            foreach (Video video in this.allVideos)
            {
                if (video.VideoId == videoId)
                    return video;
            }
            return null;
        }


        private void HideMe()
        {
            this.Hide();
            this.WindowState = FormWindowState.Minimized;

        }

        private void ToggleVisibility()
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                ShowMe(null);
            }
            else
            {
                HideMe();
            }
        }

        private void ShowMe(TabPage page)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.Activate();
            if (page == null)
                this.tabControl.SelectedTab = this.tabPageActivities;
            else
                this.tabControl.SelectedTab = page;
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowSettings();
        }

        private void activitiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowMe(this.tabPageActivities);
        }

  
        private void YouTubeNotifier_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (e.CloseReason == CloseReason.UserClosing  && this.closeThis == false)
            {
                e.Cancel = true;
                HideMe();
                this.nIcon.BalloonTipText = "Use the context menu to exit or restore particular parts of the application";
                this.nIcon.BalloonTipTitle = AppTitle + " is still running !";
                this.nIcon.ShowBalloonTip(this.notificationDuration * 1000); 
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.closeThis = true;
            this.Close();
        }

        private void clearActivitiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.linkList.Controls.Clear();
            UpdateTitle();
        }


      

        private void YouTubeNotifier_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
                HideMe();
        }


        private void Label_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            string u = e.Link.LinkData as string;

            if (u != null)
            {
                try
                {
                    System.Diagnostics.Process.Start(u);
                }
                catch
                {
                    MessageBox.Show("There was a problem navigating to: " + u);
                }
                e.Link.Visited = true;
            }
            UpdateTitle();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // let's save the username to the registry, but not the password
            RegistryKey ytNotifier = Registry.CurrentUser.OpenSubKey(YTNOTIFIERKEY, true);
            if (ytNotifier == null)
            {
                ytNotifier = Registry.CurrentUser.CreateSubKey(YTNOTIFIERKEY);
            }

            ytNotifier.SetValue("userList", GetUserNamesToSave());
            HideMe();
            UpdateActivities();
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowMe(this.tabPageUsers);
        }

        private void usernameGrid_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            this.usernameGrid.AllowUserToAddRows = true; 
            if (this.usernameGrid.Rows.Count >= 20)
            {
                this.usernameGrid.AllowUserToAddRows = false; 
            }
        }
    }
}