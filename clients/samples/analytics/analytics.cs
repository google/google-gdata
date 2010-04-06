using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Google.GData.Analytics;
using Google.GData.Extensions;

namespace Analytics
{
    public partial class Analytics : Form
    {
        public Analytics()
        {
            InitializeComponent();
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.Run(new Analytics());
        }

        private void Go_Click(object sender, System.EventArgs e)
        {
            RefreshFeed();
        }

        private void RefreshFeed()
        {
            string userName = this.Username.Text;
            string passWord = this.Password.Text;

            AccountQuery query = new AccountQuery();
            AnalyticsService service = new AnalyticsService("AnalyticsSampleApp");
            if (!string.IsNullOrEmpty(userName))
            {
                service.setUserCredentials(userName, passWord);
            }

            AccountFeed accountFeed = service.Query(query);
            foreach (AccountEntry entry in accountFeed.Entries)
            {
                ListViewItem item = new ListViewItem(entry.Title.Text);
                //item.SubItems.Add(entry.Title.Text);
                item.SubItems.Add(entry.ProfileId.Value);
                this.ProfileIds.Items.Add(item);
            }
        }

        private void GoGet_Click(object sender, EventArgs e)
        {
            PageviewFeed();
        }

        private void PageviewFeed()
        {
            string userName = this.Username.Text;
            string passWord = this.Password.Text;
            string profileId = this.ProfileIds.SelectedItems[0].SubItems[1].Text;
            const string dataFeedUrl = "https://www.google.com/analytics/feeds/data";

            AnalyticsService service = new AnalyticsService("AnalyticsSampleApp");
            if (!string.IsNullOrEmpty(userName))
            {
                service.setUserCredentials(userName, passWord);
            }

            DataQuery query = new DataQuery(dataFeedUrl);
            query.Ids = profileId;
            query.Metrics = "ga:pageviews";
            query.Dimensions = "ga:browser";
            query.Sort = "ga:browser,ga:pageviews";
            query.GAStartDate = DateTime.Now.AddDays(-14).ToString("yyyy-MM-dd");
            query.GAEndDate = DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd");

            DataFeed dataFeed = service.Query(query);
            foreach (DataEntry entry in dataFeed.Entries)
            {
                ListViewItem item = new ListViewItem(entry.Title.Text);
                item.SubItems.Add(entry.Metrics[0].Value);
                this.ListPageviews.Items.Add(item);
            }
        }
    }
}
