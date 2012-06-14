using System;
using System.Collections.Generic;
using Google.GData.ContentForShopping;
using Google.GData.ContentForShopping.Elements;
using Google.GData.Client;

namespace ManagedAccountsSample {
    class ManagedAccountsDemo {
        private static string username;
        private static string password;
        private static string accountId;

        /// <summary>
        /// This console application demonstrates all the Google
        /// Content for Shopping managed accounts API calls.
        /// </summary>
        /// <param name="args">Command-line arguments: args[0] is
        /// the username, args[1] is the password, args[2] is the account ID.
        ///
        /// Example: ManagedAccountsDemo admin@example.com my_password 123456</param>
        public static void Main(string[] args) {
            if (args.Length != 3) {
                Console.WriteLine("Syntax: ManagedAccountsDemo <username> <password> <account_id>");
            } else {
                username = args[0];
                password = args[1];
                accountId = args[2];

                RunSample(username, password, accountId);
            }
        }

        private static void RunSample(string username, string password, string accountId) {
            // Connect to the service
            ContentForShoppingService service = new ContentForShoppingService("ManagedAccounts-Sample", accountId);
            service.setUserCredentials(username, password);

            // Retrieve the list of all existing accounts
            ManagedAccountsFeed feed = service.QueryManagedAccounts();

            // Display title and id for each managed account
            Console.WriteLine("Listing all managed accounts returned");
            foreach (ManagedAccountsEntry m in feed.Entries) {
                Console.WriteLine("Managed Account: " + m.Title.Text + " (" + m.InternalId + ")");
            }

            // Create a new subaccount entry
            ManagedAccountsEntry entry = new ManagedAccountsEntry();
            entry.Title.Text = "Bob\'s Shirts";
            AtomContent c = new AtomContent();
            c.Content = "Founded in 1980, Bob has been selling shirts that you don\'t want for over 30 years.";
            entry.Content = c;
            entry.AdultContent = "no";
            entry.InternalId = "Subaccount100";
            entry.ReviewsUrl = "http://my.site.com/reviews?mo=user-rating&user=Subaccount100";

            // Add the subaccount entry
            Console.WriteLine("Inserting managed account");
            ManagedAccountsEntry inserted = service.InsertManagedAccount(entry);

            // Update the managed account we just inserted
            c = new AtomContent();
            c.Content = "Founded in 1980, Bob has been selling shirts that you love for over 30 years.";
            inserted.Content = c;
            Console.WriteLine("Updating managed account");
            ManagedAccountsEntry updated = service.UpdateManagedAccount(inserted);

            // Retrieve the new list of managed accounts
            feed = service.QueryManagedAccounts();

            // Display title and id for each managed account
            Console.WriteLine("Listing all managed accounts returned");
            foreach (ManagedAccountsEntry m in feed.Entries) {
                Console.WriteLine("Managed Account: " + m.Title.Text + " (" + m.InternalId + ")");
            }

            // Delete the managed account we inserted and updated
            Console.WriteLine("Deleting product");
            service.DeleteManagedAccount(updated);
        }
    }
}
