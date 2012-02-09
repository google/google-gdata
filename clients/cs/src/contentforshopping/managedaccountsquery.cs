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
    public class ManagedAccountsQuery : FeedQuery
    {
        private const string feedBaseUri = "https://content.googleapis.com/content/v1/";
        private const string startIndexParameter = "start-index";
        private const string maxResultsParameter = "max-results";

        private string accountId;
        private string startIndex;
        private string maxResults;

         /// <summary>
        /// Constructor
        /// </summary>
        public ManagedAccountsQuery()
            : base(feedBaseUri)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public ManagedAccountsQuery(string accountId)
            : base(feedBaseUri)
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
        /// Accessor method for StartIndex.
        /// </summary>
        public string StartIndex {
            get { return startIndex; }
            set { startIndex = value; }
        }

        /// <summary>
        /// Accessor method for MaxResults.
        /// </summary>
        public string MaxResults {
            get { return maxResults; }
            set { maxResults = value; }
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

            paramInsertion = AppendQueryPart(this.StartIndex, startIndexParameter, paramInsertion, newPath);
            if (MaxResults != null) {
                AppendQueryPart(this.MaxResults, maxResultsParameter, paramInsertion, newPath);
            }
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
                            case startIndexParameter:
                                StartIndex = parameters[1];
                                break;
                            case maxResultsParameter:
                                MaxResults = parameters[1];
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
            StartIndex = null;
        }

        /// <summary>
        /// Returns the base Uri for the feed.
        /// </summary>
        protected override string GetBaseUri() {
            StringBuilder sb = new StringBuilder(feedBaseUri, 2048);

            sb.Append(accountId);
            sb.Append("/managedaccounts/");

            return sb.ToString();
        }
    }
}
