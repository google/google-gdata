using System;
using System.Collections;
using System.Text;
using Google.GData.Client;
using Google.GData.Extensions;
using Google.GData.Spreadsheets;

namespace Spreadsheets
{
    /// <summary>
    /// SpreadsheetDemo is a simple console-based application
    /// to demonstrate the operations supported by the Google
    /// Spreadsheets Data API.  It requires authentication in
    /// the form of your Google Docs & Spreadsheets username
    /// and password, and performs a number of operations on
    /// a worksheet of your choice.
    /// </summary>
    class SpreadsheetDemo
    {
        private static string userName, password;
        private static ArrayList allWorksheets = new ArrayList();

        /// <summary>
        /// Prints a list of all worksheets in the specified spreadsheet.
        /// </summary>
        /// <param name="service">an authenticated SpreadsheetsService object</param>
        /// <param name="entry">the spreadsheet whose worksheets are to be retrieved</param>
        private static void PrintAllWorksheets(SpreadsheetsService service,
            SpreadsheetEntry entry)
        {
            AtomLink link = entry.Links.FindService(GDataSpreadsheetsNameTable.WorksheetRel, null);

            WorksheetQuery query = new WorksheetQuery(link.HRef.ToString());
            WorksheetFeed feed = service.Query(query);

            Console.WriteLine("Worksheets in " + entry.Title.Text + ":");
            foreach (WorksheetEntry worksheet in feed.Entries)
            {
                allWorksheets.Add(worksheet);
                Console.WriteLine("  " + worksheet.Title.Text);
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Prints a list of all the user's spreadsheets, and the
        /// list of worksheets that each spreadsheet contains.
        /// </summary>
        /// <param name="service">an authenticated SpreadsheetsService object</param>
        private static void PrintAllSpreadsheetsAndWorksheets(SpreadsheetsService service)
        {
            SpreadsheetQuery query = new SpreadsheetQuery();
            SpreadsheetFeed feed = service.Query(query);

            Console.WriteLine("Your spreadsheets:");
            foreach (SpreadsheetEntry entry in feed.Entries)
            {
                Console.WriteLine("Spreadsheet: {0}", entry.Title.Text);
                PrintAllWorksheets(service, entry);
            }
        }

        /// <summary>
        /// Retrieves and prints a list feed of the specified worksheet.
        /// </summary>
        /// <param name="service">an authenticated SpreadsheetsService object</param>
        /// <param name="entry">the worksheet to retrieve</param>
        /// <param name="reverseRows">true if the rows in the worksheet should
        /// be reversed when returned from the server</param>
        private static void RetrieveListFeed(SpreadsheetsService service, WorksheetEntry entry,
            bool reverseRows)
        {
            AtomLink listFeedLink = entry.Links.FindService(GDataSpreadsheetsNameTable.ListRel, null);

            Console.WriteLine();
            Console.WriteLine("This worksheet's list feed URL is:");
            Console.WriteLine(listFeedLink.HRef);

            ListQuery query = new ListQuery(listFeedLink.HRef.ToString());
            if (reverseRows)
            {
                query.OrderByPosition = true;
                query.Reverse = true;
            }
            ListFeed feed = service.Query(query);

            Console.WriteLine();
            Console.WriteLine("Worksheet has {0} rows:", feed.Entries.Count);
            foreach (ListEntry worksheetRow in feed.Entries)
            {
                ListEntry.CustomElementCollection elements = worksheetRow.Elements;
                foreach (ListEntry.Custom element in elements) {
                    Console.Write(element.Value + "\t");
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Executes a structured query against the list feed of
        /// the specified worksheet.
        /// </summary>
        /// <param name="service">an authenticated SpreadsheetsService object</param>
        /// <param name="entry">the worksheet to query</param>
        /// <param name="queryText">the structured query</param>
        private static void StructuredQuery(SpreadsheetsService service, WorksheetEntry entry,
            string queryText)
        {
            AtomLink listFeedLink = entry.Links.FindService(GDataSpreadsheetsNameTable.ListRel, null);

            ListQuery query = new ListQuery(listFeedLink.HRef.ToString());
            query.SpreadsheetQuery = queryText;
            ListFeed feed = service.Query(query);

            Console.WriteLine();
            Console.WriteLine("{0} rows matched your query:", feed.Entries.Count);
            foreach (ListEntry worksheetRow in feed.Entries)
            {
                ListEntry.CustomElementCollection elements = worksheetRow.Elements;
                foreach (ListEntry.Custom element in elements)
                {
                    Console.Write(element.Value + "\t");
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Inserts a new row in the specified worksheet.
        /// </summary>
        /// <param name="service">an authenticated SpreadsheetsService object</param>
        /// <param name="entry">the worksheet into which the row will be inserted</param>
        /// <returns>the inserted ListEntry object, representing the new row</returns>
        private static ListEntry InsertRow(SpreadsheetsService service, WorksheetEntry entry)
        {
            AtomLink listFeedLink = entry.Links.FindService(GDataSpreadsheetsNameTable.ListRel, null);

            ListQuery query = new ListQuery(listFeedLink.HRef.ToString());
            ListFeed feed = service.Query(query);

            ListEntry firstRow = feed.Entries[0] as ListEntry;
            ListEntry newRow = new ListEntry();

            Console.WriteLine();
            Console.WriteLine("Inserting a new row...");
            foreach (ListEntry.Custom element in firstRow.Elements)
                {
                Console.Write("Enter the value of column \"{0}\": ", element.LocalName);
                String elementValue = Console.ReadLine();

                ListEntry.Custom curElement = new ListEntry.Custom();
                curElement.LocalName = element.LocalName;
                curElement.Value = elementValue;

                newRow.Elements.Add(curElement);
            }

            ListEntry insertedRow = feed.Insert(newRow); 
            Console.WriteLine("Successfully inserted new row: \"{0}\"",
                insertedRow.Content.Content);

            return insertedRow;
        }

        /// <summary>
        /// Updates the value of a cell in a single worksheet row.
        /// </summary>
        /// <param name="service">an authenticated SpreadsheetsService object</param>
        /// <param name="entry">the ListEntry representing the row to update</param>
        /// <returns>the updated ListEntry object</returns>
        private static ListEntry UpdateRow(SpreadsheetsService service, ListEntry entry)
        {
            ListEntry.Custom firstColumn = entry.Elements[0];

            Console.WriteLine();
            Console.Write("Enter a new value for \"{0}\" (currently \"{1}\"): ",
                firstColumn.LocalName, firstColumn.Value);
            String newValue = Console.ReadLine();

            firstColumn.Value = newValue;

            ListEntry updatedRow = entry.Update() as ListEntry;

            Console.WriteLine("Successfully updated \"{0}\": \"{1}\"",
                updatedRow.Elements[0].LocalName, updatedRow.Elements[0].Value);

            return updatedRow;
        }

        /// <summary>
        /// Demonstrates retrieving and printing the cell feed for a
        /// worksheet.
        /// </summary>
        /// <param name="service">an authenticated SpreadsheetsService object</param>
        /// <param name="entry">the worksheet whose cell feed is to be retrieved</param>
        private static void RetrieveCellFeed(SpreadsheetsService service, WorksheetEntry entry)
        {
            AtomLink cellFeedLink = entry.Links.FindService(GDataSpreadsheetsNameTable.CellRel, null);

            Console.WriteLine();
            Console.WriteLine("This worksheet's cells feed URL is:");
            Console.WriteLine(cellFeedLink.HRef);

            CellQuery query = new CellQuery(cellFeedLink.HRef.ToString());
            CellFeed feed = service.Query(query);

            Console.WriteLine();
            Console.WriteLine("Cells in this worksheet:");
            foreach (CellEntry curCell in feed.Entries)
            {
                Console.WriteLine("Row {0}, column {1}: {2}", curCell.Cell.Row,
                    curCell.Cell.Column, curCell.Cell.Value);
            }
        }

        /// <summary>
        /// Performs a cell range query on the specified worksheet to
        /// retrieve only the cells in the first column.
        /// </summary>
        /// <param name="service">an authenticated SpreadsheetsService object</param>
        /// <param name="entry">the worksheet to retrieve</param>
        private static void CellRangeQuery(SpreadsheetsService service, WorksheetEntry entry)
        {
            AtomLink cellFeedLink = entry.Links.FindService(GDataSpreadsheetsNameTable.CellRel, null);

            CellQuery query = new CellQuery(cellFeedLink.HRef.ToString());
            query.MinimumColumn = 1;
            query.MaximumColumn = 1;
            query.MinimumRow = 2;

            CellFeed feed = service.Query(query);
            Console.WriteLine();
            Console.WriteLine("Cells in column 1:");
            foreach (CellEntry curCell in feed.Entries)
            {
                Console.WriteLine("Row {0}: {1}", curCell.Cell.Row, curCell.Cell.Value);
            }
        }

        /// <summary>
        /// Updates a single cell in the specified worksheet.
        /// </summary>
        /// <param name="service">an authenticated SpreadsheetsService object</param>
        /// <param name="entry">the worksheet to update</param>
        private static void UpdateCell(SpreadsheetsService service, WorksheetEntry entry)
        {
            AtomLink cellFeedLink = entry.Links.FindService(GDataSpreadsheetsNameTable.CellRel, null);
            CellQuery query = new CellQuery(cellFeedLink.HRef.ToString());
            Console.WriteLine();

            Console.Write("Row of cell to update? ");
            string row = Console.ReadLine();

            Console.Write("Column of cell to update? ");
            string column = Console.ReadLine();

            query.MinimumRow = query.MaximumRow = uint.Parse(row);
            query.MinimumColumn = query.MaximumColumn = uint.Parse(column);

            CellFeed feed = service.Query(query);
            CellEntry cell = feed.Entries[0] as CellEntry;

            Console.WriteLine();
            Console.WriteLine("Current cell value: {0}", cell.Cell.Value);
            Console.Write("Enter a new value: ");
            string newValue = Console.ReadLine();

            cell.Cell.InputValue = newValue;
            AtomEntry updatedCell = cell.Update();

            Console.WriteLine("Successfully updated cell: {0}", updatedCell.Content.Content);
        }

        /// <summary>
        /// Creates a new SpreadsheetsService with the user's specified
        /// authentication credentials and runs all of the Spreadsheets
        /// operations above.
        /// </summary>
        private static void RunSample()
        {
            SpreadsheetsService service = new SpreadsheetsService("exampleCo-exampleApp-1");
            service.setUserCredentials(userName, password);

            // Demonstrate printing all spreadsheets and worksheets.
            PrintAllSpreadsheetsAndWorksheets(service);

            // Demonstrate retrieving the list feed for a single worksheet,
            // with the rows (ordered by position) reversed.
            int userChoice = GetUserWorksheetChoice();
            WorksheetEntry entry = allWorksheets[userChoice] as WorksheetEntry;

            RetrieveListFeed(service, entry, true);

            // Demonstrate sending a structured query.
            Console.Write("Enter a structured query to execute: ");
            string queryText = Console.ReadLine();
            StructuredQuery(service, entry, queryText);

            // Demonstrate inserting a new row in the worksheet.
            ListEntry insertedEntry = InsertRow(service, entry);

            // Demonstrate updating the inserted row.
            UpdateRow(service, insertedEntry);

            // Demonstrate deleting the entry.
            insertedEntry.Delete();

            // Demonstrate retrieving a cell feed for a worksheet.
            RetrieveCellFeed(service, entry);

            // Demonstrate a cell range query.
            CellRangeQuery(service, entry);

            // Demonstrate updating a single cell.
            UpdateCell(service, entry);
        }

        /// <summary>
        /// Prompts the user for a number representing one of their
        /// worksheets; this worksheet will then be used to demonstrate
        /// the various Spreadsheets operations above.
        /// </summary>
        /// <returns>the number of the worksheet chosen by the user</returns>
        private static int GetUserWorksheetChoice()
        {
            Console.WriteLine("Select the worksheet on which to demonstrate");
            Console.WriteLine("add/edit/delete operations by entering its number:");
            Console.WriteLine();
            for (int i = 0; i < allWorksheets.Count; i++)
            {
                WorksheetEntry entry = allWorksheets[i] as WorksheetEntry;
                Console.WriteLine("{0}: {1}", i + 1, entry.Title.Text);
            }

            Console.WriteLine();
            Console.Write("Your choice: ");
            String userResponse = Console.ReadLine();

            return int.Parse(userResponse) - 1;
        }

        /// <summary>
        /// Program entry point.
        /// </summary>
        /// <param name="args">the username and password used to log
        /// in to Google Docs & Spreadsheets.  For example:
        /// 
        /// SpreadsheetDemo jdoe@gmail.com mypassword
        /// </param>
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.Error.WriteLine("Syntax: SpreadsheetDemo <username> <password>");
                return;
            }
            else
            {
                userName = args[0];
                password = args[1];

                RunSample();
            }
        }
    }
}
