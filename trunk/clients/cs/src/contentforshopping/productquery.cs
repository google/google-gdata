using System;
using Google.GData.Client;

namespace Google.GData.ContentForShopping
{
    /// <summary>
    /// A subclass of ItemQuery, to create a product query URI.
    /// Provides public properties that describe the different
    /// aspects of the URI, as well as a composite URI.
    /// </summary>
    public class ProductQuery : ItemQuery
    {
        private const string productDataType = "products";

         /// <summary>
        /// Constructor
        /// </summary>
        public ProductQuery()
            : base(productDataType)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public ProductQuery(string projection, string accountId)
            : base(productDataType, projection, accountId)
        {
        }
    }
}
