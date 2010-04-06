namespace NotifierForYT
{
    partial class YouTubeNotifier
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private const string AppTitle = "Notifier for YouTube";


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
            this.defaultMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.activitiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearActivitiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.tabPageUsers = new System.Windows.Forms.TabPage();
            this.label9 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.usernameGrid = new System.Windows.Forms.DataGridView();
            this.YTUser = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.defaultMenu.SuspendLayout();
            this.tabPageSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UpdateFrequency)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.notifcationBalloons)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.InitialDataPullTime)).BeginInit();
            this.tabPageActivities.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPageUsers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.usernameGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // defaultMenu
            // 
            this.defaultMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.activitiesToolStripMenuItem,
            this.clearActivitiesToolStripMenuItem,
            this.usersToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.defaultMenu.Name = "defaultMenu";
            this.defaultMenu.Size = new System.Drawing.Size(183, 114);
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
            // usersToolStripMenuItem
            // 
            this.usersToolStripMenuItem.Name = "usersToolStripMenuItem";
            this.usersToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.usersToolStripMenuItem.Text = "&Users";
            this.usersToolStripMenuItem.Click += new System.EventHandler(this.usersToolStripMenuItem_Click);
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
            this.tabControl.Controls.Add(this.tabPageUsers);
            this.tabControl.Controls.Add(this.tabPageSettings);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(400, 368);
            this.tabControl.TabIndex = 0;
            // 
            // tabPageUsers
            // 
            this.tabPageUsers.Controls.Add(this.label9);
            this.tabPageUsers.Controls.Add(this.button1);
            this.tabPageUsers.Controls.Add(this.usernameGrid);
            this.tabPageUsers.Location = new System.Drawing.Point(4, 25);
            this.tabPageUsers.Name = "tabPageUsers";
            this.tabPageUsers.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageUsers.Size = new System.Drawing.Size(392, 339);
            this.tabPageUsers.TabIndex = 2;
            this.tabPageUsers.Text = "Users";
            this.tabPageUsers.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(26, 303);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(234, 17);
            this.label9.TabIndex = 12;
            this.label9.Text = "Enter a list of max 20 youtube users";
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(265, 297);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(110, 29);
            this.button1.TabIndex = 11;
            this.button1.Text = "&Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // usernameGrid
            // 
            this.usernameGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.usernameGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.YTUser});
            this.usernameGrid.Location = new System.Drawing.Point(8, 15);
            this.usernameGrid.Name = "usernameGrid";
            this.usernameGrid.RowTemplate.Height = 24;
            this.usernameGrid.Size = new System.Drawing.Size(376, 276);
            this.usernameGrid.TabIndex = 0;
            this.usernameGrid.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.usernameGrid_UserAddedRow);
            // 
            // YTUser
            // 
            this.YTUser.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.YTUser.HeaderText = "YouTube Username";
            this.YTUser.MaxInputLength = 255;
            this.YTUser.Name = "YTUser";
            // 
            // YouTubeNotifier
            // 
            this.AcceptButton = this.ButtonSaveSettings;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.ButtonIgnore;
            this.ClientSize = new System.Drawing.Size(400, 368);
            this.Controls.Add(this.tabControl);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "YouTubeNotifier";
            this.ShowInTaskbar = false;
            this.Text = "Notifier for YouTube";
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
            this.tabPageUsers.ResumeLayout(false);
            this.tabPageUsers.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.usernameGrid)).EndInit();
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
        private System.Windows.Forms.TabPage tabPageUsers;
        private System.Windows.Forms.DataGridView usernameGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn YTUser;
        private System.Windows.Forms.DataGridViewTextBoxColumn acceptButtonDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn autoScrollDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn autoSizeDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn autoSizeModeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn autoValidateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn backColorDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn formBorderStyleDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cancelButtonDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn controlBoxDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn helpButtonDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewImageColumn iconDataGridViewImageColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isMdiContainerDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn keyPreviewDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn locationDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn maximumSizeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn mainMenuStripDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn minimumSizeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn maximizeBoxDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn minimizeBoxDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn opacityDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn rightToLeftLayoutDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn showInTaskbarDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn showIconDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sizeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sizeGripStyleDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn startPositionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn textDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn topMostDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn transparencyKeyDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn windowStateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn autoScrollMarginDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn autoScrollMinSizeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn accessibleDescriptionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn accessibleNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn accessibleRoleDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn allowDropDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn anchorDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewImageColumn backgroundImageDataGridViewImageColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn backgroundImageLayoutDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn causesValidationDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn contextMenuStripDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cursorDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataBindingsDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dockDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn enabledDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fontDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn foreColorDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn rightToLeftDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tagDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn useWaitCursorDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn visibleDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn paddingDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn imeModeDataGridViewTextBoxColumn;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ToolStripMenuItem usersToolStripMenuItem;


    }
}

