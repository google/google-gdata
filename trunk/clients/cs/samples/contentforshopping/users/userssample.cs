using System;
using System.Collections.Generic;
using Google.GData.ContentForShopping;
using Google.GData.ContentForShopping.Elements;
using Google.GData.Client;
using Google.GData.Extensions;

namespace UsersSample {
    class UsersDemo {
        private static string username;
        private static string password;
        private static string accountId;

        /// <summary>
        /// This console application demonstrates all the Google
        /// Content for Shopping users API calls.
        /// </summary>
        /// <param name="args">Command-line arguments: args[0] is
        /// the username, args[1] is the password, args[2] is the account ID.
        ///
        /// Example: UsersDemo admin@example.com my_password 123456</param>
        public static void Main(string[] args) {
            if (args.Length != 3) {
                Console.WriteLine("Syntax: UsersDemo <username> <password> <account_id>");
            } else {
                username = args[0];
                password = args[1];
                accountId = args[2];

                RunSample(username, password, accountId);
            }
        }

        private static void RunSample(string username, string password, string accountId) {
            // Connect to the service
            ContentForShoppingService service = new ContentForShoppingService("Users-Sample", accountId);
            service.setUserCredentials(username, password);

            // Retrieve the list of all existing users
            UsersFeed feed = service.QueryUsers();

            // Display title and admin for each user
            Console.WriteLine("Listing all users returned");
            foreach (UsersEntry m in feed.Entries) {
                Console.WriteLine("User: " + m.Title.Text + " (" + m.Admin + ")");
            }

            // Create a new users entry
            UsersEntry entry = new UsersEntry();
            entry.Title.Text = "john.doe@shop.com";
            entry.Admin = false;
	        entry.Permissions.Add(new Permission("online", "readwrite"));
	        entry.Permissions.Add(new Permission("local", "noaccess"));

            // Add the user
            Console.WriteLine("Inserting user");
            UsersEntry inserted = service.InsertUser(entry);

            // Update the user we just inserted
            Console.WriteLine("Updating user");
            inserted.Permissions.Add(new Permission("online", "readonly"));
	        inserted.Permissions.Add(new Permission("local", "noaccess"));
            UsersEntry updated = service.UpdateUser(inserted);

            // Retrieve the new list of users
            feed = service.QueryUsers();

            // Display title and admin for each user
            Console.WriteLine("Listing all users returned");
            foreach (UsersEntry m in feed.Entries) {
                Console.WriteLine("User: " + m.Title.Text + " (" + m.Admin + ")");
            }

            // Delete the user we inserted and updated
            Console.WriteLine("Deleting user");
            service.DeleteUser(updated);
        }
    }
}
