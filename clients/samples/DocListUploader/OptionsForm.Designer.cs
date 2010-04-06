namespace DocListUploader
{
    partial class OptionsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionsForm));
            this.Username = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Password = new System.Windows.Forms.TextBox();
            this.LoginButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.LogoutButton = new System.Windows.Forms.Button();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.UploaderStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.contextCheckBox = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.DocList = new System.Windows.Forms.ListView();
            this.Title = new System.Windows.Forms.ColumnHeader();
            this.LastModified = new System.Windows.Forms.ColumnHeader();
            this.DocListItemMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.OpenMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.DeleteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DocIcons = new System.Windows.Forms.ImageList(this.components);
            this.RefreshButton = new System.Windows.Forms.Button();
            this.statusStrip.SuspendLayout();
            this.panel1.SuspendLayout();
            this.DocListItemMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // Username
            // 
            this.Username.Location = new System.Drawing.Point(162, 29);
            this.Username.Name = "Username";
            this.Username.Size = new System.Drawing.Size(146, 20);
            this.Username.TabIndex = 0;
            this.Username.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Username_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(55, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "E-mail Address";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(55, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Password";
            // 
            // Password
            // 
            this.Password.Location = new System.Drawing.Point(162, 55);
            this.Password.Name = "Password";
            this.Password.Size = new System.Drawing.Size(108, 20);
            this.Password.TabIndex = 3;
            this.Password.UseSystemPasswordChar = true;
            this.Password.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Password_KeyPress);
            // 
            // LoginButton
            // 
            this.LoginButton.Location = new System.Drawing.Point(77, 96);
            this.LoginButton.Name = "LoginButton";
            this.LoginButton.Size = new System.Drawing.Size(77, 34);
            this.LoginButton.TabIndex = 4;
            this.LoginButton.Text = "Login";
            this.LoginButton.UseVisualStyleBackColor = true;
            this.LoginButton.Click += new System.EventHandler(this.LoginButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
            this.label3.Location = new System.Drawing.Point(55, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(213, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "Drag and drop files to upload";
            // 
            // LogoutButton
            // 
            this.LogoutButton.Enabled = false;
            this.LogoutButton.Location = new System.Drawing.Point(209, 96);
            this.LogoutButton.Name = "LogoutButton";
            this.LogoutButton.Size = new System.Drawing.Size(77, 34);
            this.LogoutButton.TabIndex = 6;
            this.LogoutButton.Text = "Logout";
            this.LogoutButton.UseVisualStyleBackColor = true;
            this.LogoutButton.Click += new System.EventHandler(this.LogoutButton_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.UploaderStatus});
            this.statusStrip.Location = new System.Drawing.Point(0, 531);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(358, 22);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 7;
            this.statusStrip.Text = "statusStrip1";
            // 
            // UploaderStatus
            // 
            this.UploaderStatus.Name = "UploaderStatus";
            this.UploaderStatus.Size = new System.Drawing.Size(71, 17);
            this.UploaderStatus.Text = "Please sign in";
            // 
            // contextCheckBox
            // 
            this.contextCheckBox.AutoSize = true;
            this.contextCheckBox.Location = new System.Drawing.Point(74, 147);
            this.contextCheckBox.Name = "contextCheckBox";
            this.contextCheckBox.Size = new System.Drawing.Size(219, 17);
            this.contextCheckBox.TabIndex = 8;
            this.contextCheckBox.Text = "Add DocList Uploader to right click menu";
            this.contextCheckBox.UseVisualStyleBackColor = true;
            this.contextCheckBox.CheckedChanged += new System.EventHandler(this.contextCheckBox_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(17, 170);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(320, 41);
            this.panel1.TabIndex = 9;
            // 
            // DocList
            // 
            this.DocList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Title,
            this.LastModified});
            this.DocList.ContextMenuStrip = this.DocListItemMenu;
            this.DocList.FullRowSelect = true;
            this.DocList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.DocList.Location = new System.Drawing.Point(17, 217);
            this.DocList.MultiSelect = false;
            this.DocList.Name = "DocList";
            this.DocList.Size = new System.Drawing.Size(320, 271);
            this.DocList.SmallImageList = this.DocIcons;
            this.DocList.TabIndex = 10;
            this.DocList.UseCompatibleStateImageBehavior = false;
            this.DocList.View = System.Windows.Forms.View.Details;
            this.DocList.DoubleClick += new System.EventHandler(this.DocList_DoubleClick);
            // 
            // Title
            // 
            this.Title.Text = "Title";
            this.Title.Width = 167;
            // 
            // LastModified
            // 
            this.LastModified.Text = "Last Modified";
            this.LastModified.Width = 139;
            // 
            // DocListItemMenu
            // 
            this.DocListItemMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenMenuItem,
            this.toolStripSeparator1,
            this.DeleteMenuItem});
            this.DocListItemMenu.Name = "DocListItemMenu";
            this.DocListItemMenu.ShowImageMargin = false;
            this.DocListItemMenu.Size = new System.Drawing.Size(143, 76);
            // 
            // OpenMenuItem
            // 
            this.OpenMenuItem.Name = "OpenMenuItem";
            this.OpenMenuItem.Size = new System.Drawing.Size(142, 22);
            this.OpenMenuItem.Text = "Open in Browser";
            this.OpenMenuItem.Click += new System.EventHandler(this.OpenMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(139, 6);
            // 
            // DeleteMenuItem
            // 
            this.DeleteMenuItem.Name = "DeleteMenuItem";
            this.DeleteMenuItem.Size = new System.Drawing.Size(142, 22);
            this.DeleteMenuItem.Text = "Delete Document";
            this.DeleteMenuItem.Click += new System.EventHandler(this.DeleteMenuItem_Click);
            // 
            // DocIcons
            // 
            this.DocIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer) (resources.GetObject("DocIcons.ImageStream")));
            this.DocIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.DocIcons.Images.SetKeyName(0, "Document.gif");
            this.DocIcons.Images.SetKeyName(1, "Presentation.gif");
            this.DocIcons.Images.SetKeyName(2, "Spreadsheet.gif");
            // 
            // RefreshButton
            // 
            this.RefreshButton.Enabled = false;
            this.RefreshButton.Location = new System.Drawing.Point(110, 494);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(138, 24);
            this.RefreshButton.TabIndex = 11;
            this.RefreshButton.Text = "Refresh";
            this.RefreshButton.UseVisualStyleBackColor = true;
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // OptionsForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(358, 553);
            this.Controls.Add(this.RefreshButton);
            this.Controls.Add(this.DocList);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.contextCheckBox);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.LogoutButton);
            this.Controls.Add(this.LoginButton);
            this.Controls.Add(this.Password);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Username);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon) (resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "OptionsForm";
            this.Text = "DocList Uploader";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.OptionsForm_DragDrop);
            this.Resize += new System.EventHandler(this.OptionsForm_Resize);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OptionsForm_FormClosing);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.OptionsForm_DragEnter);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.DocListItemMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Username;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Password;
        private System.Windows.Forms.Button LoginButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button LogoutButton;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel UploaderStatus;
        private System.Windows.Forms.CheckBox contextCheckBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListView DocList;
        private System.Windows.Forms.ColumnHeader Title;
        private System.Windows.Forms.ColumnHeader LastModified;
        private System.Windows.Forms.ImageList DocIcons;
        private System.Windows.Forms.Button RefreshButton;
        private System.Windows.Forms.ContextMenuStrip DocListItemMenu;
        private System.Windows.Forms.ToolStripMenuItem OpenMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem DeleteMenuItem;
    }
}