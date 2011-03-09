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

using Google.GData.Client;
using Google.GData.Spreadsheets;
using System.Collections.Generic;
using System;

namespace Google.Spreadsheets
{
    //////////////////////////////////////////////////////////////////////
    /// <summary>the main object to access everything else with
    /// 
    /// </summary> 
    //////////////////////////////////////////////////////////////////////
    public class Application
    {
        /// <summary>
        /// the name of this application
        /// </summary>
        public static string Name = ".NETSDK Spreadsheets";

        private static SpreadsheetsService service;
        private static SpreadsheetEntry se;
        private static WorksheetEntry we;
        private static SpreadsheetFeed sf; 
        //private static string currentSpreadsheet; //not used //TODO determine if this is a bug

        /// <summary>
        /// default constructor for client login. 
        /// </summary>
        /// <param name="user">the username to user</param>
        /// <param name="password">the users password</param>
        /// <returns></returns>
        public Application(string user, string password)
        {
            Application.service = new SpreadsheetsService(Application.Name);
            Application.service.setUserCredentials(user, password);
        }

        /// <summary>
        /// constructor for webapplications. Obtain the token using the authsub
        /// helper methods
        /// </summary>
        /// <param name="token">Your authentication token</param>
        /// <returns></returns>
        public Application(string token)
        {
            GAuthSubRequestFactory authFactory =
                new GAuthSubRequestFactory("wise", Application.Name);

            authFactory.Token = token;
            Application.service = new SpreadsheetsService(authFactory.ApplicationName);
            Application.service.RequestFactory = authFactory;
        }

        /// <summary>
        /// this will reload the list of spreadsheets from the server and reset the 
        /// currently active spreadsheet/worksheet
        /// </summary>
        /// <returns></returns>
        public void Refresh()
        {
            Application.sf = null;
            Application.se = null;
            Application.we = null; 
        }


        /// <summary>
        ///  this returns a list of strings, with the names of each spreadsheet
        /// this will do a roundtrip to the google servers to retrieve the list 
        /// of spreadsheets if not done yet
        /// </summary>
        /// <returns>List of strings</returns>
        public List<string> Spreadsheets
        {
            get 
            {
                List<string> results = new List<string>();
                EnsureSpreadsheetFeed();

                foreach (SpreadsheetEntry entry in Application.sf.Entries)
                {
                    results.Add(entry.Title.Text);
                }
                return results; 
            }
        }


        /// <summary>
        /// this will return the current spreadsheetname that is being worked on, or, if set
        /// see if there is a spreadsheet of that name, and if so, make this the current spreadsheet
        /// </summary>
        /// <returns></returns>
        public string CurrentSpreadsheet
        {
            get 
            {
                if (Application.se != null)
                {
                    return Application.se.Title.Text;
                }
                return null;
            }

            set 
            {

                Application.se = null; 
                if (value == null)
                {
                    return;
                }
                EnsureSpreadsheetFeed();
                foreach (SpreadsheetEntry entry in Application.sf.Entries)
                {
                    if (value.Equals(entry.Title.Text))
                    {
                        Application.se = entry;
                        break;
                    }
                }
                if (Application.se == null)
                {
                    throw new ArgumentException(value + " was not a valid spreadsheet name");
                }
            }
        }

        /// <summary>
        /// returns a list of Worksheet names for the currently set spreadsheet,
        /// or NULL if no spreadsheet was set
        /// </summary>
        /// <returns>NULL for no spreadsheet, an empty list for no worksheets in the 
        /// spreadsheet or a list of names of the worksheets in the current spreadsheet</returns>
        public List<string> WorkSheets
        {
            get
            {
                if (Application.se != null)
                {
                    List<string> result = new List<string>();

                    WorksheetFeed feed = Application.se.Worksheets;
                    foreach (WorksheetEntry worksheet in feed.Entries)
                    {
                        result.Add(worksheet.Title.Text);
                    }
                    return result;
                }
                return null; 
            }
        }

        /// <summary>
        /// this will return the current worksheetname that is being worked on, or, if set
        /// see if there is a worksheet of that name, and if so, make this the current worksheet
        /// Note that this requires that a current spreadsheet is set
        /// </summary>
        /// <returns></returns>
        public string CurrentWorksheet
        {
            get 
            {
                if (Application.we != null)
                {
                    return Application.we.Title.Text;
                }
                return null;
            }

            set 
            {

                Application.we = null; 

                if (value == null)
                {
                    return;
                }

                if (Application.se != null)
                {
                    WorksheetFeed feed = Application.se.Worksheets;
                    foreach (WorksheetEntry worksheet in feed.Entries)
                    {
                        if (value.Equals(worksheet.Title.Text))
                        {
                            Application.we = worksheet;
                            break;
                        }
                    }
                }
                else 
                {
                    throw new ArgumentException(value + " is invalid if no spreadsheet is set");
                }
            }
        }

        /// <summary>
        /// get the whole spreadsheet as a range, one line is one row, one cell is  
        /// </summary>
        /// <returns></returns>
        public List<string> CompleteRange
        {
            get
            {
                if (this.CurrentWorksheet != null)
                {
                    CellFeed cells = Application.we.QueryCellFeed(ReturnEmptyCells.yes);
                    List<string> result = new List<string>();

                    foreach (CellEntry curCell in cells.Entries)
                    {
                        uint r = curCell.Cell.Row, c = curCell.Cell.Column;
                        result.Add("R" + r + "C" + c + " " + curCell.Cell.Value);
                    }
                    return result;
                }
                return null;
            }
        }





        private void EnsureSpreadsheetFeed()
        {
            if (Application.sf == null)
            {
                SpreadsheetQuery query = new SpreadsheetQuery();
                Application.sf = service.Query(query);
            }
        }

        ///
        ///  Parse a range given as, e.g., A2:D4 into numerical
        /// coordinates. The letter indicates the column in use,
        /// the number the row
        /// 
        public int[] ParseRangeString(String range) 
        {
            int [] retArray =  { -1, -1, -1, -1 }; 


            range = range.ToUpper();
            string []address = range.Split(':');
            int count = 0; 

            foreach (string s in address )
            {

                string rowString=null;
                string colString=null;

                int rowStart = -1; 
                for (int i=0; i< s.Length; i++)
                {
                    if (Char.IsDigit(s, i))
                    {
                        rowStart = i; 
                        break;
                    }
                }

                if (rowStart > 0)
                {
                    rowString = s.Substring(rowStart);
                    colString = s.Substring(0, rowStart);
                }

                // rowstring should be a numer, so that is easy
                retArray[count+1] = Int32.Parse(rowString);

                // colstring is a tad more complicated. a column addressed
                // with DE for example is 26 * 4 + 5 ... and ZFE would be
                // 26 * 26 * 26 + 6 * 26 + 5 
                // so we go from right to left
                int colValue = 0;

                for (int y=0, x = colString.Length-1; x >= 0; x--, y++)
                {
                    char c = colString[x]; 
                    colValue += (c - 'A' + 1) * (int) Math.Pow(26, y); 
                }
                retArray[count] = colValue;
                count+=2;
            }
            return retArray;
        }




    }
    //end of public class Application
    
}
