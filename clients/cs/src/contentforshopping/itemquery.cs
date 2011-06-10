using System;
using System.Text;
using Google.GData.Client;

namespace Google.GData.ContentForShopping
{
    /// <summary>
	/// A subclass of FeedQuery, to create a ContentForShopping item query URI.
    /// Provides public properties that describe the different
    /// aspects of the URI, as well as a composite URI.
    /// </summary>
    public abstract class ItemQuery : FeedQuery
    {
        private const string itemFeedBaseUri = "https://content.googleapis.com/content/v1/";

        private readonly string dataType;
        private string accountId;
        private string projection;

         /// <summary>
        /// Constructor
        /// </summary>
        public ItemQuery(string dataType)
            : base(itemFeedBaseUri)
        {
            this.dataType = dataType;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public ItemQuery(string dataType, string projection, string accountId)
            : base(itemFeedBaseUri)
        {
            this.accountId = accountId;
            this.dataType = dataType;
            this.projection = projection;
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
        /// Accessor method for Projection.
        /// </summary>
        public string Projection
        {
            get { return projection; }
            set { projection = value; }
        }

        /// <summary>
        /// Creates the URI query string based on all set properties.
        /// </summary>
        /// <returns>the URI query string</returns>
        protected override string CalculateQuery(string basePath)
        {
            StringBuilder path = new StringBuilder(basePath, 2048);

            path.Append(accountId);
            path.Append("/items/");
            path.Append(dataType);
            path.Append("/");
            path.Append(projection);

            return base.CalculateQuery(path.ToString());
        }
    }
}
