using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Google.GData.Client;

namespace Google.GData.Tools
{
	/// <summary>
	/// Summary description for GoogleClientLoging.
	/// </summary>
	public class GoogleClientLogin : System.Windows.Forms.Form
	{
        private string authToken; 

        private System.Windows.Forms.TextBox Password;
        private System.Windows.Forms.CheckBox RememberToken;
        private System.Windows.Forms.Button Login;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Username;
        private Service service;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public GoogleClientLogin(Service serviceToUse)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

            this.service = serviceToUse;
		}

        public GoogleClientLogin(string username)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            this.Username.Text = username;
        }

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
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
            this.RememberToken = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Login
            // 
            this.Login.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Login.Location = new System.Drawing.Point(240, 104);
            this.Login.Name = "Login";
            this.Login.Size = new System.Drawing.Size(80, 32);
            this.Login.TabIndex = 4;
            this.Login.Text = "&Login";
            this.Login.Click += new System.EventHandler(this.Login_Click);
            // 
            // Cancel
            // 
            this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel.Location = new System.Drawing.Point(144, 104);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(80, 32);
            this.Cancel.TabIndex = 3;
            this.Cancel.Text = "&Cancel";
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // Username
            // 
            this.Username.Location = new System.Drawing.Point(112, 16);
            this.Username.Name = "Username";
            this.Username.Size = new System.Drawing.Size(208, 22);
            this.Username.TabIndex = 1;
            this.Username.Text = "joe@gmail.com";
            // 
            // Password
            // 
            this.Password.Location = new System.Drawing.Point(112, 48);
            this.Password.Name = "Password";
            this.Password.PasswordChar = '*';
            this.Password.Size = new System.Drawing.Size(208, 22);
            this.Password.TabIndex = 2;
            this.Password.Text = "";
            // 
            // RememberToken
            // 
            this.RememberToken.Location = new System.Drawing.Point(16, 80);
            this.RememberToken.Name = "RememberToken";
            this.RememberToken.Size = new System.Drawing.Size(160, 24);
            this.RememberToken.TabIndex = 4;
            this.RememberToken.Text = "Remember the Token";
            this.RememberToken.Visible = false;
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
            // GoogleClientLogin
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
            this.ClientSize = new System.Drawing.Size(344, 152);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.RememberToken);
            this.Controls.Add(this.Password);
            this.Controls.Add(this.Username);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.Login);
            this.Name = "GoogleClientLogin";
            this.Text = "GoogleClientLoging";
            this.ResumeLayout(false);

        }
		#endregion
 
      
        public string AuthenticationToken
        {
            get 
            {
                return this.authToken;
            }
        }

        public bool RememberAuthentication 
        {
            get 
            {
                return this.RememberToken.Checked;
            }
        }

        public string User
        {
            get 
            {
                return this.Username.Text;
            }
        }

        private void Cancel_Click(object sender, System.EventArgs e)
        {
            this.authToken = null;
            this.Close();
        }

        private void Login_Click(object sender, System.EventArgs e)
        {
            this.service.setUserCredentials(this.Username.Text, this.Password.Text);
            this.authToken = this.service.QueryAuthenticationToken();
            this.Close();
        }


	}
}
