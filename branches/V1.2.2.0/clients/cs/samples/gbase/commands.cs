using System;
using System.IO;
using System.Text;
using System.Xml;
using Google.GData.GoogleBase;
using Google.GData.Client;

namespace Google.GData.GoogleBase.Examples {

    //////////////////////////////////////////////////////////////////////
    /// <summary>Simple command interface</summary>
    //////////////////////////////////////////////////////////////////////
    interface ICommand
    {
        //////////////////////////////////////////////////////////////////////
        /// <summary>Executes the command.</summary>
        //////////////////////////////////////////////////////////////////////
        void Execute();
    }

    //////////////////////////////////////////////////////////////////////
    /// <summary>Base class for all commands that contains
    /// fields and methods.</summary>
    //////////////////////////////////////////////////////////////////////
    abstract class CommandBase
    {
        protected readonly GBaseService service;
        protected readonly GBaseUriFactory uriFactory;
        private static readonly Encoding StreamEncoding = new UTF8Encoding();

        protected CommandBase(GBaseService service, GBaseUriFactory uriFactory)
        {
            this.service = service;
            this.uriFactory = uriFactory;
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>Copies data from a stream (UTF-8) into a text writer
        /// (Usually stdout)</summary>
        //////////////////////////////////////////////////////////////////////
        protected static void Copy(Stream input, TextWriter output)
        {
            StreamReader reader = new StreamReader(input, StreamEncoding);
            Copy(reader, output);
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>Copies characters from a TextReader to a TextWriter.
        /// </summary>
        //////////////////////////////////////////////////////////////////////
        protected static void Copy(TextReader input, TextWriter output)
        {
            char[] buffer = new char[1024];
            int l;

            while ( (l = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, l);
            }
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>Writes a feed or entry to stdout.</summary>
        //////////////////////////////////////////////////////////////////////
        protected void WriteToStandardOutput(AtomBase atomBase)
        {
            XmlTextWriter xmlWriter = new XmlTextWriter(Console.Out);
            xmlWriter.Formatting = Formatting.Indented;
            xmlWriter.Indentation = 2;
            atomBase.SaveToXml(xmlWriter);
            xmlWriter.Flush();
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>Reads a feed or entry from stdin.</summary>
        //////////////////////////////////////////////////////////////////////
        protected GBaseFeed ReadFromStandardInput()
        {
            GBaseFeed feed = new GBaseFeed(uriFactory.ItemsFeedUri, service);
            Stream stdin = Console.OpenStandardInput();
            try
            {
                feed.Parse(stdin, AlternativeFormat.Atom);
                return feed;
            }
            finally
            {
                stdin.Close();
            }
        }

    }

    //////////////////////////////////////////////////////////////////////
    /// <summary>Executes a query on the items feed and writes the
    /// result to standard output.</summary>
    //////////////////////////////////////////////////////////////////////
    class QueryCommand : CommandBase, ICommand
    {
        private readonly string queryString;

        public QueryCommand(GBaseService service,
                            GBaseUriFactory uriFactory,
                            string queryString)
                : base(service, uriFactory)
        {
            this.queryString = queryString;
        }

        public void Execute()
        {
            GBaseQuery query = new GBaseQuery(uriFactory.ItemsFeedUri);
            query.GoogleBaseQuery = queryString;

            WriteToStandardOutput(service.Query(query));
        }
    }

    //////////////////////////////////////////////////////////////////////
    /// <summary>Deletes an item given its id/url.</summary>
    //////////////////////////////////////////////////////////////////////
    class DeleteCommand : CommandBase, ICommand
    {
        private readonly Uri uri;

        public DeleteCommand(GBaseService service,
                             GBaseUriFactory uriFactory,
                             string uri)
                : base(service, uriFactory)
        {
            this.uri = new Uri(uri);
        }

        public void Execute()
        {
            service.Delete(uri);
            Console.WriteLine("Deleted: " + uri);
        }
    }

    //////////////////////////////////////////////////////////////////////
    /// <summary>Execute an insert (POST) command on the server,
    /// reading the item to insert from standard input.</summary>
    //////////////////////////////////////////////////////////////////////
    class InsertCommand : CommandBase, ICommand
    {
        public InsertCommand(GBaseService service, GBaseUriFactory uriFactory)
                : base(service, uriFactory)
        {

        }

        public void Execute()
        {
            GBaseFeed feed = ReadFromStandardInput();

            GBaseEntry result = service.Insert(uriFactory.ItemsFeedUri,
                                               feed.Entries[0] as GBaseEntry);

            WriteToStandardOutput(result);
        }
    }

    //////////////////////////////////////////////////////////////////////
    /// <summary>Execute an update (PUT) command on the server,
    /// reading the item to update from standard input.</summary>
    //////////////////////////////////////////////////////////////////////
    class UpdateCommand : CommandBase, ICommand
    {
        public UpdateCommand(GBaseService service,
                             GBaseUriFactory uriFactory)
                : base(service, uriFactory)
        {
        }

        public void Execute()
        {
            GBaseFeed feed = ReadFromStandardInput();
            GBaseEntry entry = feed.Entries[0] as GBaseEntry;
            entry.EditUri = entry.Id.Uri;
            service.Update(entry);

            Console.WriteLine("Item updated: " + entry.Id.Uri);
        }
    }

    //////////////////////////////////////////////////////////////////////
    /// <summary>Do a GET on the given url and writes the
    /// result to standard output, as a feed or as an entry.
    ///
    /// The big difference between this command and simply typing
    /// the url into a browser is that this query will be
    /// authenticated.
    /// </summary>
    //////////////////////////////////////////////////////////////////////
    class GetCommand : CommandBase, ICommand
    {
        private readonly string url;

        public GetCommand(GBaseService service,
                          GBaseUriFactory uriFactory,
                          string url)
                : base(service, uriFactory)
        {
            this.url = url;
        }

        public void Execute()
        {
            GBaseFeed feed = service.Query(new GBaseQuery(new Uri(url)));
            if (feed.Entries.Count == 1)
            {
                // It was most probably a single entry Uri
                WriteToStandardOutput(feed.Entries[0]);
            }
            else
            {
                WriteToStandardOutput(feed);
            }
        }
    }

    //////////////////////////////////////////////////////////////////////
    /// <summary>Do a POST on the feed's batch URL and writes the
    /// result to standard output, as a feed.</summary>
    //////////////////////////////////////////////////////////////////////
    class BatchCommand : CommandBase, ICommand
    {
        public BatchCommand(GBaseService service,
                            GBaseUriFactory uriFactory)
                : base(service, uriFactory)
        {

        }

        public void Execute()
        {
            GBaseFeed feed = ReadFromStandardInput();
            GBaseFeed result = service.Batch(feed, uriFactory.ItemsFeedBatchUri);
            WriteToStandardOutput(result);
        }
    }

}
