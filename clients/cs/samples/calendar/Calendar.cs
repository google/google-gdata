using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Google.GData.Client;
using Google.GData.Extensions;
using Google.GData.Calendar;

namespace SampleApp
{
    /// <summary>
    /// Summary description for Calendar.
    /// </summary>
    public class Calendar : System.Windows.Forms.Form
    {
        private System.Windows.Forms.TextBox CalendarURI;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox UserName;
        private System.Windows.Forms.TextBox Password;
        private System.Windows.Forms.Button Go;
        private System.Windows.Forms.MonthCalendar calendarControl;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;
        private System.Windows.Forms.ListView DayEvents;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;


        private ArrayList entryList; 

        public Calendar()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            this.entryList = new ArrayList();
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() 
        {
            Application.Run(new Calendar());
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose( bool disposing )
        {
            if (disposing)
            {
                if (components != null)
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
            this.calendarControl = new System.Windows.Forms.MonthCalendar();
            this.CalendarURI = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.UserName = new System.Windows.Forms.TextBox();
            this.Password = new System.Windows.Forms.TextBox();
            this.Go = new System.Windows.Forms.Button();
            this.DayEvents = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // calendarControl
            // 
            this.calendarControl.Location = new System.Drawing.Point(0, 8);
            this.calendarControl.Name = "calendarControl";
            this.calendarControl.ShowTodayCircle = false;
            this.calendarControl.TabIndex = 0;
            this.calendarControl.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.calendarControl_DateSelected);
            // 
            // CalendarURI
            // 
            this.CalendarURI.Location = new System.Drawing.Point(280, 32);
            this.CalendarURI.Name = "CalendarURI";
            this.CalendarURI.Size = new System.Drawing.Size(296, 20);
            this.CalendarURI.TabIndex = 1;
            this.CalendarURI.Text = "https://www.google.com/calendar/feeds/default/private/full";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(200, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "URL:";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(200, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 24);
            this.label2.TabIndex = 3;
            this.label2.Text = "User:";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(200, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Password";
            // 
            // UserName
            // 
            this.UserName.Location = new System.Drawing.Point(280, 68);
            this.UserName.Name = "UserName";
            this.UserName.Size = new System.Drawing.Size(296, 20);
            this.UserName.TabIndex = 5;
            this.UserName.Text = "sample@gmail.com";
            // 
            // Password
            // 
            this.Password.Location = new System.Drawing.Point(280, 104);
            this.Password.Name = "Password";
            this.Password.PasswordChar = '*';
            this.Password.Size = new System.Drawing.Size(296, 20);
            this.Password.TabIndex = 6;
            this.Password.Text = "";
            // 
            // Go
            // 
            this.Go.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Go.Location = new System.Drawing.Point(480, 208);
            this.Go.Name = "Go";
            this.Go.Size = new System.Drawing.Size(96, 24);
            this.Go.TabIndex = 7;
            this.Go.Text = "&Go";
            this.Go.Click += new System.EventHandler(this.Go_Click);
            // 
            // DayEvents
            // 
            this.DayEvents.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                                                                                        this.columnHeader1,
                                                                                        this.columnHeader2,
                                                                                        this.columnHeader3,
                                                                                        this.columnHeader4});
            this.DayEvents.FullRowSelect = true;
            this.DayEvents.GridLines = true;
            this.DayEvents.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.DayEvents.LabelWrap = false;
            this.DayEvents.Location = new System.Drawing.Point(8, 184);
            this.DayEvents.Name = "DayEvents";
            this.DayEvents.Size = new System.Drawing.Size(424, 88);
            this.DayEvents.TabIndex = 8;
            this.DayEvents.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Event";
            this.columnHeader1.Width = 200;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Author";
            this.columnHeader2.Width = 100;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Start";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "End";
            // 
            // Calendar
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(592, 278);
            this.Controls.Add(this.DayEvents);
            this.Controls.Add(this.Go);
            this.Controls.Add(this.Password);
            this.Controls.Add(this.UserName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CalendarURI);
            this.Controls.Add(this.calendarControl);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.Name = "Calendar";
            this.Text = "Google Calendar Demo Application";
            this.ResumeLayout(false);

        }
#endregion

        private void textBox1_TextChanged(object sender, System.EventArgs e)
        {

        }

        private void Go_Click(object sender, System.EventArgs e)
        {
            RefreshFeed(); 
        }


        private void RefreshFeed() 
        {
            string calendarURI = this.CalendarURI.Text;
            string userName =    this.UserName.Text;
            string passWord =    this.Password.Text;

            this.entryList = new ArrayList(50); 
            ArrayList dates = new ArrayList(50); 
            EventQuery query = new EventQuery();
            CalendarService service = new CalendarService("CalendarSampleApp");

            if (userName != null && userName.Length > 0)
            {
                service.setUserCredentials(userName, passWord);
            }

            // only get event's for today - 1 month until today + 1 year

            query.Uri = new Uri(calendarURI);

            query.StartTime = DateTime.Now.AddDays(-28); 
            query.EndTime = DateTime.Now.AddMonths(6);


            EventFeed calFeed = service.Query(query) as EventFeed;

            // now populate the calendar
            while (calFeed != null && calFeed.Entries.Count > 0)
            {
                // look for the one with dinner time...
                foreach (EventEntry entry in calFeed.Entries)
                {
                    this.entryList.Add(entry); 
                    if (entry.Times.Count > 0)
                    {
                        foreach (When w in entry.Times) 
                        {
                            dates.Add(w.StartTime); 
                        }
                    }
                }
                // just query the same query again.
                if (calFeed.NextChunk != null)
                {
                    query.Uri = new Uri(calFeed.NextChunk); 
                    calFeed = service.Query(query) as EventFeed;
                }
                else
                    calFeed = null;
            }

            DateTime[] aDates = new DateTime[dates.Count]; 

            int i =0; 
            foreach (DateTime d in dates) 
            {
                aDates[i++] = d; 
            }


            this.calendarControl.BoldedDates = aDates;
        }


        private void calendarControl_DateSelected(object sender, System.Windows.Forms.DateRangeEventArgs e)
        {

            this.DayEvents.Items.Clear();

            ArrayList results = new ArrayList(5); 
            foreach (EventEntry entry in this.entryList) 
            {
                // let's find the entries for that date

                if (entry.Times.Count > 0)
                {
                    foreach (When w in entry.Times) 
                    {
                        if (e.Start.Date == w.StartTime.Date ||
                            e.Start.Date == w.EndTime.Date)
                        {
                            results.Add(entry); 
                            break;
                        }
                    }
                }
            }

            foreach (EventEntry entry in results) 
            {
                ListViewItem item = new ListViewItem(entry.Title.Text); 
                item.SubItems.Add(entry.Authors[0].Name); 
                if (entry.Times.Count > 0)
                {
                    item.SubItems.Add(entry.Times[0].StartTime.TimeOfDay.ToString()); 
                    item.SubItems.Add(entry.Times[0].EndTime.TimeOfDay.ToString()); 
                }

                this.DayEvents.Items.Add(item);
            }
        }


    }
}
