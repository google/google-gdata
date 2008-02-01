using Google.GData.GoogleBase;

namespace Google.GData.GoogleBase.Examples {

    //////////////////////////////////////////////////////////////////////
    /// <summary>Executes a simple, generic Google Base query.</summary>
    //////////////////////////////////////////////////////////////////////
    public class QueryExample : Example
    {
        private const int MAX_RESULTS = 10;

        //////////////////////////////////////////////////////////////////////
        /// <summary>Main method
        ///
        /// The command takes as arguments the options <c>--key</c>
        /// (the developer key), optionally <c>--url</c> (the url to
        /// connect to). This is handled by <see cref="Example">the
        /// superclass</see>
        ///
        /// Following the option, the query examples accepts one argument
        /// containing a Google Base Query.
        ///
        /// QueryExample connects to the Google Base server, runs the
        /// query and displays up to 10 results.
        ///
        /// Usage: queryexample.exe --key <key> [--url <url>] [<query>]
        ///</summary>
        //////////////////////////////////////////////////////////////////////
        public static void Main(string[] args)
        {
            QueryExample example = new QueryExample();
            example.Execute(args);
        }

        private void Execute(string[] args)
        {
            string queryString = null;

            if (args.Length == 3)
            {
                args = Init(args, "Google-CsharpQueryExample-1.0");
                queryString = args[0];
            }
            else 
            {
                FatalError("Invalid argument count. Usage:\n" +
                           "  queryexample.exe --key <key> \"<query>\"");
            }

            // Creates a query on the snippets feed.
            GBaseQuery query = new GBaseQuery(uriFactory.SnippetsFeedUri);
            query.GoogleBaseQuery = queryString;
            query.NumberToRetrieve = MAX_RESULTS;

            System.Console.WriteLine("Sending request to: " + query.Uri);

            // Connects to the server and gets the result, which is
            // then parsed to create a GBaseFeed object.
            GBaseFeed result = service.Query(query);

            PrintResult(result);
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>Prints the results to standard output.</summary>
        //////////////////////////////////////////////////////////////////////
        private void PrintResult(GBaseFeed result)
        {
            if (result.TotalResults == 0)
            {
                System.Console.WriteLine("No matches.");
                return;
            }

            foreach (GBaseEntry entry in result.Entries)
            {
                System.Console.WriteLine(entry.GBaseAttributes.ItemType +
                                         ": " + entry.Title.Text +
                                         " - " + entry.Id.Uri);
            }
        }


    }

}
