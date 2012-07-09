using System;
using System.Collections.Generic;
using Google.GData.ContentForShopping;
using Google.GData.ContentForShopping.Elements;
using Google.GData.Client;

namespace DataQualitySample {
    class DataQualityDemo {
        private static string username;
        private static string password;
        private static string accountId;
        private static string subaccount;

        /// <summary>
        /// This console application demonstrates all the Google
        /// Content for Shopping data quality API calls.
        /// </summary>
        /// <param name="args">Command-line arguments: args[0] is
        /// the username, args[1] is the password, args[2] is the account ID
        /// and args[3] is the subaccount ID.
        ///
        /// Example: DataQualityDemo admin@example.com my_password 123456 789012</param>
        public static void Main(string[] args) {
            if (args.Length != 4) {
                Console.WriteLine("Syntax: DataQualityDemo <username> <password> <account_id> <subaccount>");
            } else {
                username = args[0];
                password = args[1];
                accountId = args[2];
                subaccount = args[3];

                RunSample(username, password, accountId, subaccount);
            }
        }

        private static void RunSample(string username, string password, string accountId, string subaccount) {
            // Connect to the service
            ContentForShoppingService service = new ContentForShoppingService("DataQuality-Sample", accountId);
            service.setUserCredentials(username, password);

            // Retrieve the list of DQ feeds for first 25 subaccounts
            DataQualityFeed feed = service.QueryDataQualityFeed();

            // Display title for each entry in the feed
            Console.WriteLine("Listing all data quality feeds returned");
            foreach (DataQualityEntry m in feed.Entries) {
                Console.WriteLine("Entry Name: " + m.Title.Text);
            }

            DataQualityEntry entry = service.GetDataQualityEntry(subaccount);
            Console.WriteLine("Entry Name: " + entry.Title.Text);
        }
    }
}
