using System;
using System.Collections.Generic;
using Google.GData.ContentForShopping;
using Google.GData.ContentForShopping.Elements;
using Google.GData.Client;

namespace InventorySample {
    class InventoryDemo {
        private static string username;
        private static string password;
        private static string accountId;
        private static string language;
        private static string country;
        private static string productId;
        private static string storeCode;

        /// <summary>
        /// This console application demonstrates all the Google
        /// Content for Shopping inventory API calls.
        /// </summary>
        /// <param name="args">Command-line arguments: args[0] is
        /// the username, args[1] is the password, args[2] is the account ID,
        /// args[3] is the language of an existing product,
        /// args[4] is the country of an existing product,
        /// args[5] is the product ID of an existing product,
        /// args[6] is the store code where the local product will be updated.
        ///
        /// Example: InventoryDemo admin@example.com my_password 123456 en US SKU123 store1</param>
        public static void Main(string[] args) {
            if (args.Length != 7) {
                Console.WriteLine("Syntax: InventoryDemo <username> <password> <account_id> <language> <country> <product_id> <store_code>");
            } else {
                username = args[0];
                password = args[1];
                accountId = args[2];
                language = args[3];
                country = args[4];
                productId = args[5];
                storeCode = args[6];

                RunSample(username, password, accountId, language, country, productId, storeCode);
            }
        }

        private static void RunSample(string username, string password, string accountId,
                                      string language, string country, string productId,
                                      string storeCode)
            {
            // Connect to the service
            ContentForShoppingService service = new ContentForShoppingService("Inventory-Sample", accountId);
            service.setUserCredentials(username, password);

            // Create a new inventory entry
            InventoryEntry entry = new InventoryEntry();
            entry.Availability = "in stock";
            entry.Price = new Price("usd", "250.00");
            entry.Quantity = 1000;
            entry.SalePrice = new SalePrice("usd", "199.90");
            entry.SalePriceEffectiveDate = "2012-01-09 2012-01-13";

            // Add necessary EditUri
            bool setEditUri = true;
            entry = service.AddLocalId(entry, language, country, productId, storeCode, setEditUri);

            // Update the product we just constructed
            Console.WriteLine("Updating local product");
            InventoryEntry updated = service.UpdateInventoryEntry(entry);

            // Attempt a similar update via batch
            updated.Quantity = 900;
            updated.Price = new Price("usd", "240.00");

            updated = service.AddLocalId(updated, language, country, productId, storeCode);
            InventoryFeed feed = service.UpdateInventoryFeed(new List<InventoryEntry> {updated});

            // Display title and id for each local product
            Console.WriteLine("Listing all inventory returned");
            foreach (InventoryEntry m in feed.Entries) {
                Console.WriteLine("Locsl Product: " + m.Title.Text + " (" + m.EditUri + ")");
            }
        }
    }
}
