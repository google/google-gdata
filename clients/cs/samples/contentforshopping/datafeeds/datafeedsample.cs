using System;
using System.Collections.Generic;
using Google.GData.ContentForShopping;
using Google.GData.ContentForShopping.Elements;
using Google.GData.Client;

namespace DatafeedSample {
    class DatafeedDemo {
        private static string username;
        private static string password;
        private static string accountId;

        /// <summary>
        /// This console application demonstrates all the Google
        /// Content for Shopping datafeeds API calls.
        /// </summary>
        /// <param name="args">Command-line arguments: args[0] is
        /// the username, args[1] is the password, args[2] is the account ID.
        ///
        /// Example: DatafeedDemo admin@example.com my_password 123456</param>
        public static void Main(string[] args) {
            if (args.Length != 3) {
                Console.WriteLine("Syntax: DatafeedDemo <username> <password> <account_id>");
            } else {
                username = args[0];
                password = args[1];
                accountId = args[2];

                RunSample(username, password, accountId);
            }
        }

        private static void RunSample(string username, string password, string accountId) {
            // Connect to the service
            ContentForShoppingService service = new ContentForShoppingService("Datafeed-Sample", accountId);
            service.setUserCredentials(username, password);

            // Retrieve the list of all existing accounts
            DatafeedFeed feed = service.QueryDatafeeds();

            // Display title and filename for each datafeed
            Console.WriteLine("Listing all datafeeds returned");
            foreach (DatafeedEntry m in feed.Entries) {
                Console.WriteLine("Datafeed: " + m.Title.Text + " (" + m.FeedFileName + ")");
            }

            // Create a new datafeed entry
            DatafeedEntry entry = new DatafeedEntry();
            entry.Title.Text = "ABC Store Electronics products feed";
            entry.AttributeLanguage = "en";
            entry.ContentLanguage = "en";
            entry.FeedFileName = "electronics.txt";
            entry.TargetCountry = "US";

            FileFormat format = new FileFormat();
            format.Format = "dsv";
            format.Delimiter = "pipe";
            format.Encoding = "utf8";
            format.UseQuotedFields = "no";

            entry.FileFormat = format;

            // Add the datafeed
            Console.WriteLine("Inserting datafeed");
            DatafeedEntry inserted = service.InsertDatafeed(entry);

            // Update the datafeed we just inserted
            Console.WriteLine("Updating datafeed");
            inserted.Title.Text = "DEF Store Electronics products feed";
            DatafeedEntry updated = service.UpdateDatafeed(inserted);

            // Retrieve the new list of datafeeds
            feed = service.QueryDatafeeds();

            // Display title and filename for each datafeed
            Console.WriteLine("Listing all datafeed returned");
            foreach (DatafeedEntry m in feed.Entries) {
                Console.WriteLine("Datafeed: " + m.Title.Text + " (" + m.FeedFileName + ")");
            }

            // Delete the datafeed we inserted and updated
            Console.WriteLine("Deleting datafeed");
            service.DeleteDatafeed(updated);
        }
    }
}
