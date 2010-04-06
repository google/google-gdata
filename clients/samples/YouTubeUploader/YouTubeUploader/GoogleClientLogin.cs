using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Google.GData.Client;

namespace Google.GData.Tools
{
    /// <summary>
    ///  This is a sample implementation for a login dialog. It returns you the authToken gained
    /// from the Google Client Login Service
    /// </summary>
    public class GoogleClientLogin : System.Windows.Forms.Form
    {
  
        private System.Windows.Forms.TextBox Password;
        private System.Windows.Forms.Button Login;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Username;
        private GDataCredentials credentials = null;
        private Label label3;
        private TextBox Account;
        private bool accountChanged = false;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        /// <summary>
        /// allows you to construct the dialog with a given service
        /// </summary>
        /// <param name="serviceToUse">the service object to use</param>
        public GoogleClientLogin(string username)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            this.Username.Text = username;
        }

        

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
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
            this.Login = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.Username = new System.Windows.Forms.TextBox();
            this.Password = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Account = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Login
            // 
            this.Login.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Login.Location = new System.Drawing.Point(240, 104);
            this.Login.Name = "Login";
            this.Login.Size = new System.Drawing.Size(80, 32);
            this.Login.TabIndex = 5;
            this.Login.Text = "&Login";
            this.Login.Click += new System.EventHandler(this.Login_Click);
            // 
            // Cancel
            // 
            this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel.Location = new System.Drawing.Point(144, 104);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(80, 32);
            this.Cancel.TabIndex = 4;
            this.Cancel.Text = "&Cancel";
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // Username
            // 
            this.Username.Location = new System.Drawing.Point(110, 16);
            this.Username.Name = "Username";
            this.Username.Size = new System.Drawing.Size(210, 22);
            this.Username.TabIndex = 1;
            this.Username.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Username_KeyUp);
            // 
            // Password
            // 
            this.Password.Location = new System.Drawing.Point(110, 48);
            this.Password.Name = "Password";
            this.Password.PasswordChar = '*';
            this.Password.Size = new System.Drawing.Size(210, 22);
            this.Password.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(16, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 32);
            this.label1.TabIndex = 5;
            this.label1.Text = "Username:";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(16, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 32);
            this.label2.TabIndex = 6;
            this.label2.Text = "Password: ";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(16, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 44);
            this.label3.TabIndex = 8;
            this.label3.Text = "YouTube Account:";
            // 
            // Account
            // 
            this.Account.Location = new System.Drawing.Point(110, 80);
            this.Account.Name = "Account";
            this.Account.Size = new System.Drawing.Size(210, 22);
            this.Account.TabIndex = 3;
            this.Account.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Account_KeyPress);
            // 
            // GoogleClientLogin
            // 
            this.AcceptButton = this.Login;
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
            this.CancelButton = this.Cancel;
            this.ClientSize = new System.Drawing.Size(344, 152);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Account);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Password);
            this.Controls.Add(this.Username);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.Login);
            this.Name = "GoogleClientLogin";
            this.Text = "GoogleClientLogin";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion


        /// <summary>
        /// returns the authentication token
        /// </summary>
        public GDataCredentials Credentials
        {
            get
            {
                return this.credentials;
            }
        }

        public string YoutubeAccount
        {
            get
            {
                return this.Account.Text;
            }
        }

      
      
        /// <summary>
        /// returns the user name 
        /// </summary>
        public string User
        {
            get
            {
                return this.Username.Text;
            }
        }

        private void Cancel_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void Login_Click(object sender, System.EventArgs e)
        {
            this.credentials = new GDataCredentials(this.Username.Text, this.Password.Text);
            this.Close();
        }

        private void Account_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.accountChanged = true;
        }

   
        private void UpdateAccount()
        {
            // only autoupdate this if it has not changed yet
            if (this.accountChanged == false)
            {
                string value = this.Username.Text;
                string firstpart = value.Split('@')[0];
                this.Account.Text = firstpart;
            }
        }

    
        private void Username_KeyUp(object sender, KeyEventArgs e)
        {
            UpdateAccount();
        }

    }
}
