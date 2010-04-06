namespace Analytics
{
    partial class Analytics
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
            this.Username = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Password = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ProfileIds = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.label3 = new System.Windows.Forms.Label();
            this.Go = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.GetPageviews = new System.Windows.Forms.Button();
            this.ListPageviews = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // Username
            // 
            this.Username.Location = new System.Drawing.Point(16, 38);
            this.Username.Name = "Username";
            this.Username.Size = new System.Drawing.Size(119, 20);
            this.Username.TabIndex = 0;
            this.Username.Text = "sample@gmail.com";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Username";
            // 
            // Password
            // 
            this.Password.Location = new System.Drawing.Point(158, 38);
            this.Password.Name = "Password";
            this.Password.PasswordChar = '*';
            this.Password.Size = new System.Drawing.Size(119, 20);
            this.Password.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(155, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.Text = "Password";
            // 
            // ProfileIds
            // 
            this.ProfileIds.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.ProfileIds.FullRowSelect = true;
            this.ProfileIds.GridLines = true;
            this.ProfileIds.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.ProfileIds.Location = new System.Drawing.Point(16, 135);
            this.ProfileIds.Name = "ProfileIds";
            this.ProfileIds.Size = new System.Drawing.Size(300, 135);
            this.ProfileIds.TabIndex = 4;
            this.ProfileIds.UseCompatibleStateImageBehavior = false;
            this.ProfileIds.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Title";
            this.columnHeader1.Width = 150;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Profile ID";
            this.columnHeader2.Width = 125;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.Text = "Profile Ids";
            // 
            // Go
            // 
            this.Go.Location = new System.Drawing.Point(16, 65);
            this.Go.Name = "Go";
            this.Go.Size = new System.Drawing.Size(75, 23);
            this.Go.TabIndex = 3;
            this.Go.Text = "Get Profiles";
            this.Go.UseVisualStyleBackColor = true;
            this.Go.Click += new System.EventHandler(this.Go_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(389, 113);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.Text = "Pageviews";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Title";
            this.columnHeader3.Width = 150;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Pageviews";
            this.columnHeader4.Width = 125;
            // 
            // GetPageviews
            // 
            this.GetPageviews.Location = new System.Drawing.Point(16, 278);
            this.GetPageviews.Name = "GetPageviews";
            this.GetPageviews.Size = new System.Drawing.Size(96, 23);
            this.GetPageviews.TabIndex = 5;
            this.GetPageviews.Text = "Get Pageviews";
            this.GetPageviews.UseVisualStyleBackColor = true;
            this.GetPageviews.Click += new System.EventHandler(this.GoGet_Click);
            // 
            // ListPageviews
            // 
            this.ListPageviews.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4});
            this.ListPageviews.FullRowSelect = true;
            this.ListPageviews.GridLines = true;
            this.ListPageviews.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.ListPageviews.Location = new System.Drawing.Point(392, 135);
            this.ListPageviews.Name = "ListPageviews";
            this.ListPageviews.Size = new System.Drawing.Size(300, 135);
            this.ListPageviews.UseCompatibleStateImageBehavior = false;
            this.ListPageviews.View = System.Windows.Forms.View.Details;
            // 
            // Analytics
            // 
            this.ClientSize = new System.Drawing.Size(794, 313);
            this.Controls.Add(this.GetPageviews);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ListPageviews);
            this.Controls.Add(this.Go);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ProfileIds);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Password);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Username);
            this.Name = "Analytics";
            this.Text = "Google Analytics Demo Application";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Username;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Password;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView ProfileIds;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button Go;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListView ListPageviews;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Button GetPageviews;
    }
}
