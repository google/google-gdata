/* Copyright (c) 2006 Google Inc.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.IO;
using System.Collections;
using System.Text;
using System.Net;
using Google.GData.Client;
using Google.GData.Extensions;
using System.Collections.Generic;

namespace Google.GData.ContentForShopping {
    /// <summary>
    /// The ContentForShoppingService class extends the basic Service abstraction
    /// to define a service that is preconfigured for access to the
    /// Google Content API for Shopping.
    /// </summary>
    public class ContentForShoppingService : Service {
        /// <summary>The ID of the merchant account</summary>
        private string accountId;
        /// <summary>a projection determining response values
        /// can be one of "schema" and "generic"</summary>
        private string projection = "schema";
        /// <summary>Bool determining if warnings should be returned</summary>
        private bool showWarnings = false;
        /// <summary>Bool determining if dry run mode should be used</summary>
        private bool dryRun = false;

        /// <summary>The Content for Shopping service's name</summary>
        public const String GContentForShoppingService = "structuredcontent";

        private string homepage = "";
        public string Homepage
        {
            get { return homepage; }
            set { homepage = value; }
        }

        private string rootUrl = "";
        public string RootUrl
        {
            get { return rootUrl; }
            set { rootUrl = value; }
        }

        /// <summary>
        /// AccountId of the service object.
        /// </summary>
        public string AccountId
        {
            get { return this.accountId; }
            set { this.accountId = value; }
        }

        /// <summary>
        /// Accessor method for Projection.
        /// </summary>
        public string Projection {
            get { return projection; }
            set { projection = value; }
        }

        /// <summary>
        /// Accessor method for ShowWarnings.
        /// </summary>
        public bool ShowWarnings {
            get { return showWarnings; }
            set { showWarnings = value; }
        }

        /// <summary>
        /// Accessor method for DryRun.
        /// </summary>
        public bool DryRun {
            get { return dryRun; }
            set { dryRun = value; }
        }

        /// <summary>
        /// Constructor which takes account id and application name.
        /// </summary>
        /// <param name="applicationName">The name of the client application</param>
        /// <param name="accountId">The account ID of the user.</param>
        /// using this service.</param>
        public ContentForShoppingService(String applicationName, String accountId)
            : base(GContentForShoppingService, applicationName)
        {
            this.accountId = accountId;

            this.NewFeed += new ServiceEventHandler(this.OnNewFeed);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="applicationName">The name of the client application</param>
        /// using this service.</param>
        public ContentForShoppingService(String applicationName)
            : base(GContentForShoppingService, applicationName)
        {
            this.NewFeed += new ServiceEventHandler(this.OnNewFeed);
        }

        /// <summary>
        /// overwritten Query method
        /// </summary>
        /// <returns>the retrieved ProductFeed</returns>
        public ProductFeed Query()
        {
            return Query(new ProductQuery());
        }

        /// <summary>
        /// overwritten Query method
        /// </summary>
        /// <param name="accountId">the account Id intended on the request</param>
        /// <returns>the retrieved ProductFeed</returns>
        public ProductFeed Query(string accountId)
        {
            ProductQuery query = new ProductQuery();
            query.AccountId = accountId;
            return Query(query);
        }

        /// <summary>
        /// overwritten Query method
        /// </summary>
        /// <param name="feedQuery">The FeedQuery to use</param>
        /// <returns>the retrieved ProductFeed</returns>
        public ProductFeed Query(ProductQuery feedQuery)
        {
            if (feedQuery.AccountId == null) {
                feedQuery.AccountId = this.AccountId;
            }
            if (feedQuery.Projection == null) {
                feedQuery.Projection = this.Projection;
            }
            return base.Query(feedQuery) as ProductFeed;
        }

        /// <summary>
        /// Overwrites Get
        /// </summary>
        /// <param name="entryUri">The URI of the Product entry.</param>
        /// <returns>the retrieved ProductEntry</returns>
        public new ProductEntry Get(string entryUri)
        {
            return base.Get(entryUri) as ProductEntry;
        }

        /// <summary>
        /// Overwrites Get
        /// </summary>
        /// <param name="channel">The channel of the product. Usually online or local.</param>
        /// <param name="language">The language for the product.</param>
        /// <param name="country">The target country of the product.</param>
        /// <param name="productId">The unique identifier for the product in the given locale.</param>
        /// <returns>the retrieved ProductEntry</returns>
        public ProductEntry Get(string channel, string language, string country, string productId)
        {
            string path = CreateProductIdentifier(channel, language, country, productId);
            Uri entryUri = CreateUri("items/products", path);
            return Get(entryUri.ToString());
        }

        /// <summary>
        /// Overwrites Get
        /// </summary>
        /// <param name="language">The language for the product.</param>
        /// <param name="country">The target country of the product.</param>
        /// <param name="productId">The unique identifier for the product in the given locale.</param>
        /// <returns>the retrieved ProductEntry</returns>
        public ProductEntry Get(string language, string country, string productId)
        {
            return Get("online", language, country, productId);
        }

        /// <summary>
        /// Inserts a new product entry.
        /// </summary>
        /// <param name="entry">the entry to insert</param>
        /// <returns>the inserted entry</returns>
        public ProductEntry Insert(ProductEntry entry)
        {
            return base.Insert(CreateUri("items/products", null), entry) as ProductEntry;
        }

        /// <summary>
        /// Inserts a new product entry.
        /// </summary>
        /// <param name="entry">the entry to insert</param>
        /// <param name="accountId">The account ID of the user.</param>
        /// <param name="projection">a projection determining response values
        /// <returns>the inserted entry</returns>
        public ProductEntry Insert(ProductEntry entry, string accountId, string projection)
        {
            return base.Insert(CreateUri(accountId, "items/products", projection, null), entry);
        }

        /// <summary>
        /// Inserts a new product entry into the specified feed.
        /// </summary>
        /// <param name="feed">the feed into which this entry should be inserted</param>
        /// <param name="entry">the entry to insert</param>
        /// <returns>the inserted entry</returns>
        public ProductEntry Insert(ProductFeed feed, ProductEntry entry)
        {
            return base.Insert(feed, entry);
        }

        /// <summary>
        /// Updates an existing product entry with the new values
        /// </summary>
        /// <param name="entry">the entry to insert</param>
        /// <returns>the updated entry returned by the server</returns>
        public ProductEntry Update(ProductEntry entry)
        {
            return base.Update(entry);
        }

        /// <summary>
        /// Deletes an existing product.
        /// </summary>
        /// <param name="entry">the entry to delete</param>
        public void Delete(ProductEntry entry)
        {
            base.Delete(entry);
        }

        /// <summary>
        /// takes a given feed, and does a batch post of that feed
        /// </summary>
        /// <param name="feed">the feed to post</param>
        /// <param name="accountId">The account ID of the user.</param>
        /// <param name="projection">a projection determining response values
        /// <returns>the returned ProductFeed</returns>
        public ProductFeed Batch(ProductFeed feed, string accountId, string projection) {
            Uri batchUri = CreateUri(accountId, "items/products", projection, "batch");
            return base.Batch(feed, batchUri) as ProductFeed;
        }

        /// <summary>
        /// takes a given feed, and does a batch post of that feed
        /// </summary>
        /// <param name="feed">the feed to post</param>
        /// <param name="accountId">The account ID of the user.</param>
        /// <returns>the returned ProductFeed</returns>
        public ProductFeed Batch(ProductFeed feed, string accountId) {
            return Batch(feed, accountId, this.Projection);
        }

        /// <summary>
        /// takes a given feed, and does a batch post of that feed
        /// </summary>
        /// <param name="feed">the feed to post</param>
        /// <returns>the returned ProductFeed</returns>
        public ProductFeed Batch(ProductFeed feed) {
            return Batch(feed, this.AccountId, this.Projection);
        }

        /// <summary>
        /// takes a list of products, adds batch operation and adds each item to a feed
        /// </summary>
        /// <param name="products">the list of products to add to a feed</param>
        /// <returns>the created ProductFeed</returns>
        protected ProductFeed CreateBatchFeed(List<ProductEntry> products, GDataBatchOperationType operation) {
            ProductFeed feed = new ProductFeed(null, this);

            foreach(ProductEntry product in products)
            {
                if (product.BatchData != null) {
                    product.BatchData.Type = operation;
                }
                else {
                    product.BatchData = new GDataBatchEntryData(operation);
                }

                feed.Entries.Add(product);
            }

            return feed;
        }

        /// <summary>
        /// takes a list of products, adds insert as the batch operation and inserts them
        /// </summary>
        /// <param name="products">the list of products to insert</param>
        /// <param name="accountId">The account ID of the user.</param>
        /// <param name="projection">a projection determining response values</param>
        /// <returns>the returned ProductFeed</returns>
        public ProductFeed InsertProducts(List<ProductEntry> products, string accountId, string projection) {
            ProductFeed feed = CreateBatchFeed(products, GDataBatchOperationType.insert);
            return Batch(feed, accountId, projection);
        }

        /// <summary>
        /// takes a list of products, adds insert as the batch operation and inserts them
        /// </summary>
        /// <param name="products">the list of products to insert</param>
        /// <param name="accountId">The account ID of the user.</param>
        /// <returns>the returned ProductFeed</returns>
        public ProductFeed InsertProducts(List<ProductEntry> products, string accountId) {
            ProductFeed feed = CreateBatchFeed(products, GDataBatchOperationType.insert);
            return Batch(feed, accountId);
        }

        /// <summary>
        /// takes a list of products, adds insert as the batch operation and inserts them
        /// </summary>
        /// <param name="products">the list of products to insert</param>
        /// <returns>the returned ProductFeed</returns>
        public ProductFeed InsertProducts(List<ProductEntry> products) {
            ProductFeed feed = CreateBatchFeed(products, GDataBatchOperationType.insert);
            return Batch(feed);
        }

        /// <summary>
        /// takes a list of products, adds update as the batch operation and updates them
        /// </summary>
        /// <param name="products">the list of products to update</param>
        /// <param name="accountId">The account ID of the user.</param>
        /// <param name="projection">a projection determining response values</param>
        /// <returns>the returned ProductFeed</returns>
        public ProductFeed UpdateProducts(List<ProductEntry> products, string accountId, string projection) {
            ProductFeed feed = CreateBatchFeed(products, GDataBatchOperationType.update);
            return Batch(feed, accountId, projection);
        }

        /// <summary>
        /// takes a list of products, adds update as the batch operation and updates them
        /// </summary>
        /// <param name="products">the list of products to update</param>
        /// <param name="accountId">The account ID of the user.</param>
        /// <returns>the returned ProductFeed</returns>
        public ProductFeed UpdateProducts(List<ProductEntry> products, string accountId) {
            ProductFeed feed = CreateBatchFeed(products, GDataBatchOperationType.update);
            return Batch(feed, accountId);
        }

        /// <summary>
        /// takes a list of products, adds update as the batch operation and updates them
        /// </summary>
        /// <param name="products">the list of products to update</param>
        /// <returns>the returned ProductFeed</returns>
        public ProductFeed UpdateProducts(List<ProductEntry> products) {
            ProductFeed feed = CreateBatchFeed(products, GDataBatchOperationType.update);
            return Batch(feed);
        }

        /// <summary>
        /// takes a list of products, adds delete as the batch operation and delete them
        /// </summary>
        /// <param name="products">the list of products to delete</param>
        /// <param name="accountId">The account ID of the user.</param>
        /// <param name="projection">a projection determining response values</param>
        /// <returns>the returned ProductFeed</returns>
        public ProductFeed DeleteProducts(List<ProductEntry> products, string accountId, string projection) {
            ProductFeed feed = CreateBatchFeed(products, GDataBatchOperationType.delete);
            return Batch(feed, accountId, projection);
        }

        /// <summary>
        /// takes a list of products, adds delete as the batch operation and delete them
        /// </summary>
        /// <param name="products">the list of products to delete</param>
        /// <param name="accountId">The account ID of the user.</param>
        /// <returns>the returned ProductFeed</returns>
        public ProductFeed DeleteProducts(List<ProductEntry> products, string accountId) {
            ProductFeed feed = CreateBatchFeed(products, GDataBatchOperationType.delete);
            return Batch(feed, accountId);
        }

        /// <summary>
        /// takes a list of products, adds delete as the batch operation and delete them
        /// </summary>
        /// <param name="products">the list of products to delete</param>
        /// <returns>the returned ProductFeed</returns>
        public ProductFeed DeleteProducts(List<ProductEntry> products) {
            ProductFeed feed = CreateBatchFeed(products, GDataBatchOperationType.delete);
            return Batch(feed);
        }

        /// <summary>
        /// Query for Managed Accounts
        /// </summary>
        /// <returns>the retrieved ManagedAccountsFeed</returns>
        public ManagedAccountsFeed QueryManagedAccounts()
        {
            return QueryManagedAccounts(new ManagedAccountsQuery());
        }

        /// <summary>
        /// Query for Managed Accounts
        /// </summary>
        /// <param name="accountId">the account Id intended on the request</param>
        /// <returns>the retrieved ManagedAccountsFeed</returns>
        public ManagedAccountsFeed QueryManagedAccounts(string accountId)
        {
            ManagedAccountsQuery query = new ManagedAccountsQuery();
            query.AccountId = accountId;
            return QueryManagedAccounts(query);
        }

        /// <summary>
        /// Query for Managed Accounts
        /// </summary>
        /// <param name="feedQuery">The FeedQuery to use</param>
        /// <returns>the retrieved ManagedAccountsFeed</returns>
        public ManagedAccountsFeed QueryManagedAccounts(ManagedAccountsQuery feedQuery)
        {
            if (feedQuery.AccountId == null) {
                feedQuery.AccountId = this.AccountId;
            }
            return base.Query(feedQuery) as ManagedAccountsFeed;
        }

        /// <summary>
        /// Gets a subaccount
        /// </summary>
        /// <param name="subaccount">The subaccount ID.</param>
        /// <returns>the retrieved ManagedAccountsEntry</returns>
        public ManagedAccountsEntry GetManagedAccount(string subaccount)
        {
            string entryUri = CreateUri("managedaccounts", subaccount).ToString();
            return base.Get(entryUri) as ManagedAccountsEntry;
        }

        /// <summary>
        /// Gets a subaccount
        /// </summary>
        /// <param name="accountId">The account ID of the user.</param>
        /// <param name="subaccount">The subaccount ID.</param>
        /// <returns>the retrieved ManagedAccountsEntry</returns>
        public ManagedAccountsEntry GetManagedAccount(string accountId, string subaccount)
        {
            string entryUri = CreateUri(accountId, "managedaccounts", null, subaccount).ToString();
            return base.Get(entryUri) as ManagedAccountsEntry;
        }

        /// <summary>
        /// Inserts a new managed account entry.
        /// </summary>
        /// <param name="entry">the entry to insert</param>
        /// <returns>the inserted entry</returns>
        public ManagedAccountsEntry InsertManagedAccount(ManagedAccountsEntry entry)
        {
            return base.Insert(CreateUri("managedaccounts", null), entry) as ManagedAccountsEntry;
        }

        /// <summary>
        /// Inserts a new managed account entry.
        /// </summary>
        /// <param name="entry">the entry to insert</param>
        /// <param name="accountId">The account ID of the parent account.</param>
        /// <returns>the inserted entry</returns>
        public ManagedAccountsEntry InsertManagedAccount(ManagedAccountsEntry entry, string accountId)
        {
            return base.Insert(CreateUri(accountId, "managedaccounts", null, null), entry);
        }

        /// <summary>
        /// Inserts a new managed accounts entry into the specified feed.
        /// </summary>
        /// <param name="feed">the feed into which this entry should be inserted</param>
        /// <param name="entry">the entry to insert</param>
        /// <returns>the inserted entry</returns>
        public ManagedAccountsEntry InsertManagedAccount(ManagedAccountsFeed feed, ManagedAccountsEntry entry)
        {
            return base.Insert(feed, entry);
        }

        /// <summary>
        /// Updates an existing managed accounts entry with the new values
        /// </summary>
        /// <param name="entry">the entry to insert</param>
        /// <returns>the updated entry returned by the server</returns>
        public ManagedAccountsEntry UpdateManagedAccount(ManagedAccountsEntry entry)
        {
            return base.Update(entry);
        }

        /// <summary>
        /// Deletes an existing managed account.
        /// </summary>
        /// <param name="entry">the entry to delete</param>
        public void DeleteManagedAccount(ManagedAccountsEntry entry)
        {
            base.Delete(entry);
        }

        /// <summary>
        /// Query for Datafeeds
        /// </summary>
        /// <returns>the retrieved DatafeedFeed</returns>
        public DatafeedFeed QueryDatafeeds()
        {
            return QueryDatafeeds(new DatafeedQuery());
        }

        /// <summary>
        /// Query for Datafeeds
        /// </summary>
        /// <param name="accountId">the account Id intended on the request</param>
        /// <returns>the retrieved DatafeedFeed</returns>
        public DatafeedFeed QueryDatafeeds(string accountId)
        {
            DatafeedQuery query = new DatafeedQuery();
            query.AccountId = accountId;
            return QueryDatafeeds(query);
        }

        /// <summary>
        /// Query for Datafeeds
        /// </summary>
        /// <param name="feedQuery">The DatafeedQuery to use</param>
        /// <returns>the retrieved DatafeedFeed</returns>
        public DatafeedFeed QueryDatafeeds(DatafeedQuery feedQuery)
        {
            if (feedQuery.AccountId == null) {
                feedQuery.AccountId = this.AccountId;
            }
            return base.Query(feedQuery) as DatafeedFeed;
        }

        /// <summary>
        /// Gets a datafeed entry
        /// </summary>
        /// <param name="datafeedId">The datafeed entry ID.</param>
        /// <returns>the retrieved DatafeedEntry</returns>
        public DatafeedEntry GetDatafeed(string datafeedId)
        {
            string entryUri = CreateUri("datafeeds/products", datafeedId).ToString();
            return base.Get(entryUri) as DatafeedEntry;
        }

        /// <summary>
        /// Gets a datafeed entry
        /// </summary>
        /// <param name="accountId">The account ID of the user.</param>
        /// <param name="datafeedId">The datafeed entry ID.</param>
        /// <returns>the retrieved DatafeedEntry</returns>
        public DatafeedEntry GetDatafeed(string accountId, string datafeedId)
        {
            string entryUri = CreateUri(accountId, "datafeeds/products", null, datafeedId).ToString();
            return base.Get(entryUri) as DatafeedEntry;
        }

        /// <summary>
        /// Inserts a new datafeed entry.
        /// </summary>
        /// <param name="entry">the entry to insert</param>
        /// <returns>the inserted entry</returns>
        public DatafeedEntry InsertDatafeed(DatafeedEntry entry)
        {
            return base.Insert(CreateUri("datafeeds/products", null), entry) as DatafeedEntry;
        }

        /// <summary>
        /// Inserts a new datafeed entry.
        /// </summary>
        /// <param name="entry">the entry to insert</param>
        /// <param name="accountId">The ID of the account that owns the datafeed.</param>
        /// <returns>the inserted entry</returns>
        public DatafeedEntry InsertDatafeed(DatafeedEntry entry, string accountId)
        {
            return base.Insert(CreateUri(accountId, "datafeeds/products", null, null), entry);
        }

        /// <summary>
        /// Inserts a new datafeeds entry into the specified feed.
        /// </summary>
        /// <param name="feed">the feed into which this entry should be inserted</param>
        /// <param name="entry">the entry to insert</param>
        /// <returns>the inserted entry</returns>
        public DatafeedEntry InsertDatafeed(DatafeedFeed feed, DatafeedEntry entry)
        {
            return base.Insert(feed, entry);
        }

        /// <summary>
        /// Updates an existing datafeed entry with the new values
        /// </summary>
        /// <param name="entry">the entry to update</param>
        /// <returns>the updated entry returned by the server</returns>
        public DatafeedEntry UpdateDatafeed(DatafeedEntry entry)
        {
            return base.Update(entry);
        }

        /// <summary>
        /// Deletes an existing datafeed.
        /// </summary>
        /// <param name="entry">the entry to delete</param>
        public void DeleteDatafeed(DatafeedEntry entry)
        {
            base.Delete(entry);
        }

        /// <summary>
        /// Query for data quality feed of subaccounts
        /// </summary>
        /// <returns>the retrieved DataQualityFeed</returns>
        public DataQualityFeed QueryDataQualityFeed()
        {
            return QueryDataQualityFeed(new DataQualityQuery());
        }

        /// <summary>
        /// Query for data quality feed of subaccounts
        /// </summary>
        /// <param name="accountId">the account Id intended on the request</param>
        /// <returns>the retrieved DataQualityFeed</returns>
        public DataQualityFeed QueryDataQualityFeed(string accountId)
        {
            DataQualityQuery query = new DataQualityQuery();
            query.AccountId = accountId;
            return QueryDataQualityFeed(query);
        }

        /// <summary>
        /// Query for data quality feed of subaccounts
        /// </summary>
        /// <param name="feedQuery">The DataQualityQuery to use</param>
        /// <returns>the retrieved DataQualityFeed</returns>
        public DataQualityFeed QueryDataQualityFeed(DataQualityQuery feedQuery)
        {
            if (feedQuery.AccountId == null) {
                feedQuery.AccountId = this.AccountId;
            }
            return base.Query(feedQuery) as DataQualityFeed;
        }

        /// <summary>
        /// Gets a data quality entry for a subaccount
        /// </summary>
        /// <param name="subaccount">The subaccount ID.</param>
        /// <returns>the retrieved DataQualityEntry</returns>
        public DataQualityEntry GetDataQualityEntry(string subaccount)
        {
            string entryUri = CreateUri("dataquality", subaccount).ToString();
            return base.Get(entryUri) as DataQualityEntry;
        }

        /// <summary>
        /// Gets a data quality entry for a subaccount
        /// </summary>
        /// <param name="accountId">The ID of the parent account.</param>
        /// <param name="subaccount">The subaccount ID.</param>
        /// <returns>the retrieved DataQualityEntry</returns>
        public DataQualityEntry GetDataQualityEntry(string accountId, string subaccount)
        {
            string entryUri = CreateUri(accountId, "dataquality", null, subaccount).ToString();
            return base.Get(entryUri) as DataQualityEntry;
        }

        /// <summary>
        /// Query for Users
        /// </summary>
        /// <returns>the retrieved UsersFeed</returns>
        public UsersFeed QueryUsers()
        {
            return QueryUsers(new UsersQuery());
        }

        /// <summary>
        /// Query for Users
        /// </summary>
        /// <param name="accountId">the account Id intended on the request</param>
        /// <returns>the retrieved UsersFeed</returns>
        public UsersFeed QueryUsers(string accountId)
        {
            UsersQuery query = new UsersQuery();
            query.AccountId = accountId;
            return QueryUsers(query);
        }

        /// <summary>
        /// Query for Users
        /// </summary>
        /// <param name="feedQuery">The UsersQuery to use</param>
        /// <returns>the retrieved UsersFeed</returns>
        public UsersFeed QueryUsers(UsersQuery feedQuery)
        {
            if (feedQuery.AccountId == null) {
                feedQuery.AccountId = this.AccountId;
            }
            return base.Query(feedQuery) as UsersFeed;
        }

        /// <summary>
        /// Gets a users entry
        /// </summary>
        /// <param name="userEmail">The email of the user being retrieved.</param>
        /// <returns>the retrieved UsersEntry</returns>
        public UsersEntry GetUser(string userEmail)
        {
            string entryUri = CreateUri("users", userEmail).ToString();
            return base.Get(entryUri) as UsersEntry;
        }

        /// <summary>
        /// Gets a users entry
        /// </summary>
        /// <param name="accountId">The ID of the account that the user is associated with.</param>
        /// <param name="userEmail">The email of the user being retrieved.</param>
        /// <returns>the retrieved UsersEntry</returns>
        public UsersEntry GetUser(string accountId, string userEmail)
        {
            string entryUri = CreateUri(accountId, "users", null, userEmail).ToString();
            return base.Get(entryUri) as UsersEntry;
        }

        /// <summary>
        /// Inserts a new users entry.
        /// </summary>
        /// <param name="entry">the entry to insert</param>
        /// <returns>the inserted entry</returns>
        public UsersEntry InsertUser(UsersEntry entry)
        {
            return base.Insert(CreateUri("users", null), entry) as UsersEntry;
        }

        /// <summary>
        /// Inserts a new user entry.
        /// </summary>
        /// <param name="entry">the entry to insert</param>
        /// <param name="accountId">The ID of the account where the user will have privileges.</param>
        /// <returns>the inserted entry</returns>
        public UsersEntry InsertUser(UsersEntry entry, string accountId)
        {
            return base.Insert(CreateUri(accountId, "users", null, null), entry);
        }

        /// <summary>
        /// Inserts a new users entry into the specified feed.
        /// </summary>
        /// <param name="feed">the feed into which this entry should be inserted</param>
        /// <param name="entry">the entry to insert</param>
        /// <returns>the inserted entry</returns>
        public UsersEntry InsertUser(UsersFeed feed, UsersEntry entry)
        {
            return base.Insert(feed, entry);
        }

        /// <summary>
        /// Updates an existing users entry with the new values
        /// </summary>
        /// <param name="entry">the entry to update</param>
        /// <returns>the updated entry returned by the server</returns>
        public UsersEntry UpdateUser(UsersEntry entry)
        {
            return base.Update(entry);
        }

        /// <summary>
        /// Deletes an existing user
        /// </summary>
        /// <param name="entry">the entry to delete</param>
        public void DeleteUser(UsersEntry entry)
        {
            base.Delete(entry);
        }

        /// <summary>
        /// Creates a product identifier based on four attributes.
        /// </summary>
        /// <param name="channel">The channel of the product. Usually online or local.</param>
        /// <param name="language">The language for the product.</param>
        /// <param name="country">The target country of the product.</param>
        /// <param name="productId">The unique identifier for the product in the given locale.</param>
        /// <returns>the product identifier</returns>
        protected string CreateProductIdentifier(string channel, string language, string country, string productId)
        {
            StringBuilder result = new StringBuilder(channel, 256);
            result.Append(':');
            result.Append(language);
            result.Append(':');
            result.Append(country);
            result.Append(':');
            result.Append(productId);

            return result.ToString();
        }

        /// <summary>
        /// Creates the URI query string based on all set properties.
        /// </summary>
        /// <param name="resource">the resource for the service</param>
        /// <param name="path">the path after the projection (optional)</param>
        /// <returns>the URI query string</returns>
        protected Uri CreateUri(string resource, string path)
        {
            return CreateUri(this.AccountId, resource, this.Projection, path);
        }

        /// <summary>
        /// Creates the URI query string based on all set properties.
        /// </summary>
        /// <param name="accountId">the account Id intended on the request</param>
        /// <param name="resource">the resource for the service</param>
        /// <param name="projection">a projection determining response values
        /// can be one of "schema" and "generic" (optional) </param>
        /// <param name="path">the path after the projection (optional)</param>
        /// <returns>the URI query string</returns>
        protected Uri CreateUri(string accountId, string resource, string projection, string path)
        {
            StringBuilder result = new StringBuilder(ContentForShoppingNameTable.AllFeedsBaseUri, 2048);
            result.Append("/");
            result.Append(accountId);
            result.Append("/");
            result.Append(resource);

            if (projection != null && resource == "items/products") {
                result.Append("/");
                result.Append(projection);
            }

            if (path != null) {
                result.Append("/");
                result.Append(path);
            }

            char paramInsertion = '?';
            if (this.ShowWarnings) {
                result.Append(paramInsertion);
                result.Append(ContentForShoppingNameTable.ShowWarningsParameter);
                paramInsertion = '&';
            }
            if (this.DryRun) {
                result.Append(paramInsertion);
                result.Append(ContentForShoppingNameTable.DryRunParameter);
            }

            return new Uri(result.ToString());
        }

        /// <summary>
        /// Parses the service name from a Content API URI endpoint.
        /// </summary>
        /// <param name="endpoint">the endpoint of the request</param>
        protected string ParseService(string endpoint)
        {
            string baseUri = ContentForShoppingNameTable.AllFeedsBaseUri + "/";
            if (endpoint.StartsWith(baseUri)) {
                string remaining = endpoint.Substring(baseUri.Length,
                    endpoint.Length - baseUri.Length);

                // index 0 will be account id, index 1 will be service
                return remaining.Split('/')[1];
            }
            return "";
        }

        /// <summary>eventchaining. We catch this by from the base service, which
        /// would not by default create an atomFeed</summary>
        /// <param name="sender"> the object which send the event</param>
        /// <param name="e">FeedParserEventArguments, holds the feedentry</param>
        /// <returns> </returns>
        protected void OnNewFeed(object sender, ServiceEventArgs e) {
            Tracing.TraceMsg("Created new Content for Shopping Feed");
            if (e == null) {
                throw new ArgumentNullException("e");
            }

            string service = ParseService(e.Uri.AbsoluteUri);
            if (service == "items") {
                e.Feed = new ProductFeed(e.Uri, e.Service);
            } else if (service == "managedaccounts") {
                e.Feed = new ManagedAccountsFeed(e.Uri, e.Service);
            } else if (service == "datafeeds") {
                e.Feed = new DatafeedFeed(e.Uri, e.Service);
            }
        }
    }
}
