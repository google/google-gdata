using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Blogger
{
	/// <summary>
	/// Summary description for addentry.
	/// </summary>
	public class addentry : System.Windows.Forms.Form
	{
        private System.Windows.Forms.TextBox EntryText;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox EntryTitleBox;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public addentry()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}


        public string Entry 
        {
            get 
            {
                return this.EntryText.Text; 
            }
        }

        public string EntryTitle 
        {
            get 
            {
                return this.EntryTitleBox.Text;
            }
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
            this.EntryText = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.EntryTitleBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // EntryText
            // 
            this.EntryText.Location = new System.Drawing.Point(32, 64);
            this.EntryText.Multiline = true;
            this.EntryText.Name = "EntryText";
            this.EntryText.Size = new System.Drawing.Size(536, 136);
            this.EntryText.TabIndex = 3;
            this.EntryText.Text = "";
            this.EntryText.TextChanged += new System.EventHandler(this.EntryText_TextChanged);
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(472, 24);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(96, 24);
            this.button1.TabIndex = 4;
            this.button1.Text = "&Ok";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // EntryTitleBox
            // 
            this.EntryTitleBox.Location = new System.Drawing.Point(112, 16);
            this.EntryTitleBox.Name = "EntryTitleBox";
            this.EntryTitleBox.Size = new System.Drawing.Size(248, 20);
            this.EntryTitleBox.TabIndex = 2;
            this.EntryTitleBox.Text = "";
            this.EntryTitleBox.TextChanged += new System.EventHandler(this.EntryTitleBox_TextChanged);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(32, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Title";
            // 
            // addentry
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(592, 214);
            this.Controls.Add(this.EntryTitleBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.EntryText);
            this.Name = "addentry";
            this.Text = "addentry";
            this.ResumeLayout(false);

        }
		#endregion

        private void EntryText_TextChanged(object sender, System.EventArgs e)
        {
            Verify();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }


        private void Verify() 
        {
            if (this.Name.Length > 0 &&
                this.Entry.Length > 0 &&
                this.EntryTitle.Length > 0) 
            {
                this.button1.Enabled = true; 
            }
            else 
            {
                this.button1.Enabled = false; 
            }
        }

        private void EntryTitleBox_TextChanged(object sender, System.EventArgs e)
        {
            Verify();    
        }
	}
}
