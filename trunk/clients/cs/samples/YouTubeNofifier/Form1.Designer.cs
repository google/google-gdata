namespace YouTubeNotifier
{
    partial class YouTubeNotifier
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private const string AppTitle = "YouTube Notifier";


        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(YouTubeNotifier));
            this.defaultMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.activitiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearActivitiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshTimer = new System.Windows.Forms.Timer(this.components);
            this.tabPageSettings = new System.Windows.Forms.TabPage();
            this.UpdateFrequency = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.notifcationBalloons = new System.Windows.Forms.NumericUpDown();
            this.isAuthenticated = new System.Windows.Forms.CheckBox();
            this.ButtonIgnore = new System.Windows.Forms.Button();
            this.ButtonSaveSettings = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.InitialDataPullTime = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.passWord = new System.Windows.Forms.TextBox();
            this.userName = new System.Windows.Forms.TextBox();
            this.tabPageActivities = new System.Windows.Forms.TabPage();
            this.linkList = new System.Windows.Forms.FlowLayoutPanel();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.defaultMenu.SuspendLayout();
            this.tabPageSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UpdateFrequency)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.notifcationBalloons)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.InitialDataPullTime)).BeginInit();
            this.tabPageActivities.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // defaultMenu
            // 
            this.defaultMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.activitiesToolStripMenuItem,
            this.clearActivitiesToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.defaultMenu.Name = "defaultMenu";
            this.defaultMenu.Size = new System.Drawing.Size(183, 92);
            // 
            // activitiesToolStripMenuItem
            // 
            this.activitiesToolStripMenuItem.Name = "activitiesToolStripMenuItem";
            this.activitiesToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.activitiesToolStripMenuItem.Text = "&Activities";
            this.activitiesToolStripMenuItem.Click += new System.EventHandler(this.activitiesToolStripMenuItem_Click);
            // 
            // clearActivitiesToolStripMenuItem
            // 
            this.clearActivitiesToolStripMenuItem.Name = "clearActivitiesToolStripMenuItem";
            this.clearActivitiesToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.clearActivitiesToolStripMenuItem.Text = "&Clear activities";
            this.clearActivitiesToolStripMenuItem.Click += new System.EventHandler(this.clearActivitiesToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.settingsToolStripMenuItem.Text = "&Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.exitToolStripMenuItem.Text = "&Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // refreshTimer
            // 
            this.refreshTimer.Interval = 900000;
            this.refreshTimer.Tick += new System.EventHandler(this.refreshTimer_Tick);
            // 
            // tabPageSettings
            // 
            this.tabPageSettings.Controls.Add(this.UpdateFrequency);
            this.tabPageSettings.Controls.Add(this.label7);
            this.tabPageSettings.Controls.Add(this.label8);
            this.tabPageSettings.Controls.Add(this.notifcationBalloons);
            this.tabPageSettings.Controls.Add(this.isAuthenticated);
            this.tabPageSettings.Controls.Add(this.ButtonIgnore);
            this.tabPageSettings.Controls.Add(this.ButtonSaveSettings);
            this.tabPageSettings.Controls.Add(this.label6);
            this.tabPageSettings.Controls.Add(this.label5);
            this.tabPageSettings.Controls.Add(this.label4);
            this.tabPageSettings.Controls.Add(this.label3);
            this.tabPageSettings.Controls.Add(this.InitialDataPullTime);
            this.tabPageSettings.Controls.Add(this.label2);
            this.tabPageSettings.Controls.Add(this.label1);
            this.tabPageSettings.Controls.Add(this.passWord);
            this.tabPageSettings.Controls.Add(this.userName);
            this.tabPageSettings.Location = new System.Drawing.Point(4, 25);
            this.tabPageSettings.Name = "tabPageSettings";
            this.tabPageSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSettings.Size = new System.Drawing.Size(392, 339);
            this.tabPageSettings.TabIndex = 1;
            this.tabPageSettings.Text = "Settings";
            this.tabPageSettings.UseVisualStyleBackColor = true;
            // 
            // UpdateFrequency
            // 
            this.UpdateFrequency.Location = new System.Drawing.Point(11, 195);
            this.UpdateFrequency.Maximum = new decimal(new int[] {
            90,
            0,
            0,
            0});
            this.UpdateFrequency.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.UpdateFrequency.Name = "UpdateFrequency";
            this.UpdateFrequency.Size = new System.Drawing.Size(69, 22);
            this.UpdateFrequency.TabIndex = 16;
            this.UpdateFrequency.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 220);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(212, 17);
            this.label7.TabIndex = 15;
            this.label7.Text = "Display notification balloons for: ";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(99, 248);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(61, 17);
            this.label8.TabIndex = 14;
            this.label8.Text = "seconds";
            // 
            // notifcationBalloons
            // 
            this.notifcationBalloons.Location = new System.Drawing.Point(11, 243);
            this.notifcationBalloons.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.notifcationBalloons.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.notifcationBalloons.Name = "notifcationBalloons";
            this.notifcationBalloons.Size = new System.Drawing.Size(69, 22);
            this.notifcationBalloons.TabIndex = 13;
            this.notifcationBalloons.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // isAuthenticated
            // 
            this.isAuthenticated.AutoSize = true;
            this.isAuthenticated.Enabled = false;
            this.isAuthenticated.Location = new System.Drawing.Point(148, 97);
            this.isAuthenticated.Name = "isAuthenticated";
            this.isAuthenticated.Size = new System.Drawing.Size(229, 21);
            this.isAuthenticated.TabIndex = 12;
            this.isAuthenticated.Text = "You are currently authenticated";
            this.isAuthenticated.UseVisualStyleBackColor = true;
            // 
            // ButtonIgnore
            // 
            this.ButtonIgnore.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ButtonIgnore.Location = new System.Drawing.Point(148, 292);
            this.ButtonIgnore.Name = "ButtonIgnore";
            this.ButtonIgnore.Size = new System.Drawing.Size(110, 29);
            this.ButtonIgnore.TabIndex = 11;
            this.ButtonIgnore.Text = "&Cancel";
            this.ButtonIgnore.UseVisualStyleBackColor = true;
            this.ButtonIgnore.Click += new System.EventHandler(this.ButtonIgnore_Click);
            // 
            // ButtonSaveSettings
            // 
            this.ButtonSaveSettings.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ButtonSaveSettings.Enabled = false;
            this.ButtonSaveSettings.Location = new System.Drawing.Point(266, 292);
            this.ButtonSaveSettings.Name = "ButtonSaveSettings";
            this.ButtonSaveSettings.Size = new System.Drawing.Size(110, 29);
            this.ButtonSaveSettings.TabIndex = 10;
            this.ButtonSaveSettings.Text = "&Save";
            this.ButtonSaveSettings.UseVisualStyleBackColor = true;
            this.ButtonSaveSettings.Click += new System.EventHandler(this.ButtonSaveSettings_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 172);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(262, 17);
            this.label6.TabIndex = 9;
            this.label6.Text = "After that, check for new activities every:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 121);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(238, 17);
            this.label5.TabIndex = 8;
            this.label5.Text = "On Startup, get activities for the last:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(99, 200);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "minutes";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(99, 152);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "hours";
            // 
            // InitialDataPullTime
            // 
            this.InitialDataPullTime.Location = new System.Drawing.Point(11, 147);
            this.InitialDataPullTime.Name = "InitialDataPullTime";
            this.InitialDataPullTime.Size = new System.Drawing.Size(69, 22);
            this.InitialDataPullTime.TabIndex = 4;
            this.InitialDataPullTime.Value = new decimal(new int[] {
            24,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(135, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "YouTube Password:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "YouTube Username:";
            // 
            // passWord
            // 
            this.passWord.Location = new System.Drawing.Point(148, 68);
            this.passWord.Name = "passWord";
            this.passWord.Size = new System.Drawing.Size(220, 22);
            this.passWord.TabIndex = 1;
            this.passWord.UseSystemPasswordChar = true;
            this.passWord.TextChanged += new System.EventHandler(this.passWord_TextChanged);
            // 
            // userName
            // 
            this.userName.Location = new System.Drawing.Point(148, 31);
            this.userName.Name = "userName";
            this.userName.Size = new System.Drawing.Size(220, 22);
            this.userName.TabIndex = 0;
            this.userName.TextChanged += new System.EventHandler(this.userName_TextChanged);
            // 
            // tabPageActivities
            // 
            this.tabPageActivities.Controls.Add(this.linkList);
            this.tabPageActivities.Location = new System.Drawing.Point(4, 25);
            this.tabPageActivities.Name = "tabPageActivities";
            this.tabPageActivities.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageActivities.Size = new System.Drawing.Size(392, 339);
            this.tabPageActivities.TabIndex = 0;
            this.tabPageActivities.Text = "Activities";
            this.tabPageActivities.UseVisualStyleBackColor = true;
            // 
            // linkList
            // 
            this.linkList.AutoScroll = true;
            this.linkList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.linkList.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.linkList.Location = new System.Drawing.Point(3, 3);
            this.linkList.Name = "linkList";
            this.linkList.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.linkList.Size = new System.Drawing.Size(386, 333);
            this.linkList.TabIndex = 1;
            this.linkList.WrapContents = false;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageActivities);
            this.tabControl.Controls.Add(this.tabPageSettings);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(400, 368);
            this.tabControl.TabIndex = 0;
            // 
            // YouTubeNotifier
            // 
            this.AcceptButton = this.ButtonSaveSettings;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.ButtonIgnore;
            this.ClientSize = new System.Drawing.Size(400, 368);
            this.Controls.Add(this.tabControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "YouTubeNotifier";
            this.ShowInTaskbar = false;
            this.Text = "YouTube Notifier";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.YouTubeNotifier_FormClosing);
            this.Resize += new System.EventHandler(this.YouTubeNotifier_Resize);
            this.defaultMenu.ResumeLayout(false);
            this.tabPageSettings.ResumeLayout(false);
            this.tabPageSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UpdateFrequency)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.notifcationBalloons)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.InitialDataPullTime)).EndInit();
            this.tabPageActivities.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip defaultMenu;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem activitiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearActivitiesToolStripMenuItem;
        private System.Windows.Forms.Timer refreshTimer;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPageSettings;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown notifcationBalloons;
        private System.Windows.Forms.CheckBox isAuthenticated;
        private System.Windows.Forms.Button ButtonIgnore;
        private System.Windows.Forms.Button ButtonSaveSettings;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown InitialDataPullTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox passWord;
        private System.Windows.Forms.TextBox userName;
        private System.Windows.Forms.TabPage tabPageActivities;
        private System.Windows.Forms.FlowLayoutPanel linkList;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.NumericUpDown UpdateFrequency;


    }
}

