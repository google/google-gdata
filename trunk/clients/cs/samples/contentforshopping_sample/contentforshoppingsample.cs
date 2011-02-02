using System;
using System.Collections.Generic;
using Google.GData.ContentForShopping;
using Google.GData.ContentForShopping.Elements;
using Google.GData.Client;

namespace ContentForShoppingSample {
	class ContentForShoppingDemo {
		private static string username;
		private static string password;
		private static string accountId;

		/// <summary>
		/// This console application demonstrates all the Google
		/// Content for Shopping API calls. 
		/// </summary>
		/// <param name="args">Command-line arguments: args[0] is
		/// the username, args[1] is the password, args[2] is the account ID.
		/// 
		/// Example: ContentForShoppingDemo admin@example.com my_password 123456</param>
		public static void Main(string[] args) {
			if (args.Length != 3) {
				Console.WriteLine("Syntax: ContentForShoppingDemo <username> <password> <account_id>");
			} else {
				username = args[0];
				password = args[1];
				accountId = args[2];

				RunSample(username, password, accountId);
			}
		}

		private static void RunSample(string username, string password, string accountId) {
			// Connect to the service
			ContentForShoppingService service = new ContentForShoppingService("ContentForShopping-Sample");
			service.setUserCredentials(username, password);

			// Retrieve the list of all existing products
			string projection = "schema";
			ProductQuery query = new ProductQuery(projection, accountId);
			ProductFeed feed = service.Query(query);

			// Display title and id for each product
			Console.WriteLine("Listing all products");
			foreach (ProductEntry p in feed.Entries) {
				Console.WriteLine("Product: " + p.Title.Text + " (" + p.ProductId + ")");
			}

			// Create a new product			
			ProductEntry entry = new ProductEntry();
			entry.Title.Text = "Wool sweater";
			AtomContent c = new AtomContent();
			c.Content = "Comfortable and soft, this sweater will keep you warm on those cold winter nights. Red and blue stripes.";
			entry.Content = c;
			entry.ProductId = "123457";
			entry.Language = "it";
			entry.TargetCountry = "US";
			entry.ContentLanguage = "en";
			entry.Brand = "ACME";
			entry.Condition = "new";
			entry.Price = new Price("usd", "25");
			entry.ProductType = "Clothing & Accessories > Clothing > Outerwear > Sweaters";
			entry.Quantity = 3;
			entry.ShippingWeight = new ShippingWeight("lb", "0.1");

			AtomLink link = new AtomLink();
			link.HRef = "http://www.example.com";
			link.Rel = "alternate";
			link.Type = "text/html";
			entry.Links.Add(link);

			// Colors
			entry.Colors.Add(new Color("red"));
			entry.Colors.Add(new Color("blue"));

			// Shipping rules
			Shipping s1 = new Shipping();
			s1.Country = "US";
			s1.Region = "MA";
			s1.Service = "Speedy Shipping - Ground";
			s1.Price = new ShippingPrice("usd", "5.95");

			Shipping s2 = new Shipping();
			s2.Country = "US";
			s2.Region = "024*";
			s2.Service = "Speedy Shipping - Air";
			s2.Price = new ShippingPrice("usd", "7.95");

			entry.ShippingRules.Add(s1);
			entry.ShippingRules.Add(s2);

			// Tax rules
			Tax t1 = new Tax();
			t1.Country = "US";
			t1.Region = "CA";
			t1.Rate = "8.25";
			t1.Ship = true;

			Tax t2 = new Tax();
			t2.Country = "US";
			t2.Region = "926*";
			t2.Rate = "8.75";
			t2.Ship = false;

			entry.TaxRules.Add(t1);
			entry.TaxRules.Add(t2);

			// Image Links
			ImageLink il = new ImageLink("http://www.example.com/1.jpg");
			entry.ImageLinks.Add(il);

			// Add the product to the server feed
			Console.WriteLine("Inserting product");
			ProductEntry inserted = service.Insert(feed, entry);

			// Update the product we just inserted
			inserted.Title.Text = "2011 Wool sweater";
			Console.WriteLine("Updating product");
			ProductEntry updated = service.Update(inserted);

			// Retrieve the new list of products
			feed = service.Query(query);

			// Display title and id for each product
			Console.WriteLine("Listing all products again");
			foreach (ProductEntry p in feed.Entries) {
				Console.WriteLine("Product: " + p.Title.Text + " (" + p.ProductId + ")");
			}

			// Delete the item we inserted and updated
			Console.WriteLine("Deleting product");
			service.Delete(updated);
		}
	}
}
