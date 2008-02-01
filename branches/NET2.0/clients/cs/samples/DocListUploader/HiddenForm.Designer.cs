namespace DocListUploader
{
    partial class HiddenForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HiddenForm));
            this.DocListNotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.DocListMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CloseMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DocListMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // DocListNotifyIcon
            // 
            this.DocListNotifyIcon.ContextMenuStrip = this.DocListMenu;
            this.DocListNotifyIcon.Icon = ((System.Drawing.Icon) (resources.GetObject("DocListNotifyIcon.Icon")));
            this.DocListNotifyIcon.Text = "DocList Uploader";
            this.DocListNotifyIcon.Visible = true;
            this.DocListNotifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.DocListNotifyIcon_MouseClick);
            this.DocListNotifyIcon.DoubleClick += new System.EventHandler(this.DocListNotifyIcon_DoubleClick);
            this.DocListNotifyIcon.BalloonTipClicked += new System.EventHandler(this.DocListNotifyIcon_BalloonTipClicked);
            // 
            // DocListMenu
            // 
            this.DocListMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem,
            this.CloseMenuItem});
            this.DocListMenu.Name = "DocListMenu";
            this.DocListMenu.ShowImageMargin = false;
            this.DocListMenu.Size = new System.Drawing.Size(142, 48);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.optionsToolStripMenuItem.Text = "Options";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // CloseMenuItem
            // 
            this.CloseMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.CloseMenuItem.Name = "CloseMenuItem";
            this.CloseMenuItem.Size = new System.Drawing.Size(141, 22);
            this.CloseMenuItem.Text = "Close Application";
            this.CloseMenuItem.Click += new System.EventHandler(this.CloseMenuItem_Click);
            // 
            // HiddenForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon) (resources.GetObject("$this.Icon")));
            this.Name = "HiddenForm";
            this.Opacity = 0;
            this.ShowInTaskbar = false;
            this.Text = "DocList Uploader";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HiddenForm_FormClosing);
            this.Load += new System.EventHandler(this.HiddenForm_Load);
            this.DocListMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon DocListNotifyIcon;
        private System.Windows.Forms.ContextMenuStrip DocListMenu;
        private System.Windows.Forms.ToolStripMenuItem CloseMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
    }
}

