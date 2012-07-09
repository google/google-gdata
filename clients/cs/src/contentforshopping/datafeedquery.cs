using System;
using System.Text;
using Google.GData.Client;

namespace Google.GData.ContentForShopping
{
    /// <summary>
    /// A subclass of FeedQuery, to create a ContentForShopping managedaccounts
    /// query URI. Provides public properties that describe the different
    /// aspects of the URI, as well as a composite URI.
    /// </summary>
    public class DatafeedQuery : FeedQuery
    {
        private string accountId;

        /// <summary>
        /// Constructor
        /// </summary>
        public DatafeedQuery()
            : base(ContentForShoppingNameTable.AllFeedsBaseUri)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public DatafeedQuery(string accountId)
            : base(ContentForShoppingNameTable.AllFeedsBaseUri)
        {
            this.accountId = accountId;
        }

        /// <summary>
        /// Accessor method for AccountId.
        /// </summary>
        public string AccountId
        {
            get { return accountId; }
            set { accountId = value; }
        }

        /// <summary>
        /// Returns the base Uri for the feed.
        /// </summary>
        protected override string GetBaseUri() {
            StringBuilder sb = new StringBuilder(this.baseUri, 2048);

            sb.Append("/");
            sb.Append(accountId);
            sb.Append("/datafeeds/products/");

            return sb.ToString();
        }
    }
}
