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
        private string startToken;

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
        /// Accessor method for StartToken.
        /// </summary>
        public string StartToken {
            get { return startToken; }
            set { startToken = value; }
        }

        /// <summary>
        /// Creates the URI query string based on all set properties.
        /// </summary>
        /// <returns>the URI query string</returns>
        protected override string CalculateQuery(string basePath)
        {
            string path = base.CalculateQuery(basePath);
            StringBuilder newPath = new StringBuilder(path, 2048);
            char paramInsertion = InsertionParameter(path);

            paramInsertion = AppendQueryPart(this.StartToken, "start-token", paramInsertion, newPath);
            return newPath.ToString();
        }

        /// <summary>
        /// Parses an incoming URI string and sets the instance variables of this object.
        /// </summary>
        /// <param name="targetUri">Takes an incoming Uri string and parses all the properties of it</param>
        /// <returns>Throws a query exception when it finds something wrong with the input, otherwise returns a baseuri.</returns>
        protected override Uri ParseUri(Uri targetUri) {
            base.ParseUri(targetUri);
            if (targetUri != null) {
                char[] delimiters = { '?', '&' };

                string source = HttpUtility.UrlDecode(targetUri.Query);
                TokenCollection tokens = new TokenCollection(source, delimiters);
                foreach (String token in tokens) {
                    if (token.Length > 0) {
                        char[] otherDelimiters = { '=' };
                        String[] parameters = token.Split(otherDelimiters, 2);
                        switch (parameters[0]) {
                            case "start-token":
                                StartToken = parameters[1];
                                break;
                        }
                    }
                }
            }
            return this.Uri;
        }

        /// <summary>
        /// Resets object state to default, as if newly created.
        /// </summary>
        protected override void Reset() {
            base.Reset();
            StartToken = null;
        }

        /// <summary>
        /// Returns the base Uri for the feed.
        /// </summary>
        protected override string GetBaseUri() {
            StringBuilder sb = new StringBuilder(itemFeedBaseUri, 2048);

            sb.Append(accountId);
            sb.Append("/items/");
            sb.Append(dataType);
            sb.Append("/");
            sb.Append(projection);

            return sb.ToString();
        }
    }
}
