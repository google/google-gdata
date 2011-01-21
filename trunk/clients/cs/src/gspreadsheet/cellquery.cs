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
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Xml;
using Google.GData.Client;

namespace Google.GData.Spreadsheets
{

    //////////////////////////////////////////////////////////////////////
    /// <summary>Enum to describe the different return empty parameters
    /// </summary> 
    //////////////////////////////////////////////////////////////////////
    public enum ReturnEmtpyCells
    {
        /// <summary> do not create the parameter, do whatever the server does</summary>
        serverDefault,
        /// <summary>return empty cells</summary>
        yes,                       
        /// <summary>do not return empty cells</summary>
        no
    }
    /////////////////////////////////////////////////////////////////////////////

    /// <summary>
    /// A subclass of FeedQuery, to create a Spreadsheets cell query URI.
    /// Provides public properties that describe the different
    /// aspects of the URI, as well as a composite URI.
    /// </summary>
    public class CellQuery : FeedQuery
    {
        private uint minimumRow;
        private uint maximumRow;

        private uint minimumColumn;
        private uint maximumColumn;

        private string range;

        private ReturnEmtpyCells returnEmpty;

        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="key">The spreadsheet key</param>
        /// <param name="worksheetId">The unique identifier or position of the worksheet</param>
        /// <param name="visibility">public or private</param>
        /// <param name="projection">full, values, or basic</param>
        public CellQuery(string key, string worksheetId, string visibility, string projection) 
        : base("https://spreadsheets.google.com/feeds/cells/" + key + "/" + worksheetId + "/" 
               + visibility + "/" + projection)
        {
            Reset();
        }

        /// <summary>
        /// Constructor - Sets the base URI
        /// </summary>
        /// <param name="baseUri">The feed base</param>
        /// <param name="key">The spreadsheet key</param>
        /// <param name="worksheetId">The unique identifier or position of the worksheet</param>
        /// <param name="visibility">public or private</param>
        /// <param name="projection">full, values, or basic</param>
        public CellQuery(string baseUri, string key, string worksheetId, string visibility, string projection)
        : base(baseUri + "/" + key + "/" + worksheetId + "/" + visibility + "/" + projection)
        {
            Reset();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="baseUri">The feed base with the key, worksheetId, visibility and 
        /// projections are appended and delimited by "/"</param>
        public CellQuery(string baseUri) : base(baseUri) 
        {
            Reset();
        }

        /// <summary>
        /// The minimum index for a row allowed in the response
        /// </summary>
        [CLSCompliant(false)]
        public uint MinimumRow
        {
            get
            {
                return minimumRow;
            }

            set
            {
                minimumRow = value;
            }
        }

        /// <summary>
        /// The maximum index for a row allowed in the response
        /// </summary>
        [CLSCompliant(false)]
        public uint MaximumRow
        {
            get
            {
                return maximumRow;
            }

            set
            {
                maximumRow = value;
            }
        }

        /// <summary>
        /// The minimum index for a column allowed in the response
        /// </summary>
        [CLSCompliant(false)]
        public uint MinimumColumn
        {
            get
            {
                return minimumColumn;
            }

            set
            {
                minimumColumn = value;
            }
        }

        /// <summary>
        /// The maximum index for a column allowed in a response
        /// </summary>
        [CLSCompliant(false)]
        public uint MaximumColumn
        {
            get
            {
                return maximumColumn;
            }

            set
            {
                maximumColumn = value;
            }
        }

        /// <summary>
        /// A range string in R1C1 or A1 notation.
        /// </summary>
        public string Range
        {
            get
            {
                return range;
            }

            set
            {
                range = value;
            }
        }

        /// <summary>
        /// If true, then empty cells will be included in the feed.
        /// </summary>
        public ReturnEmtpyCells ReturnEmpty
        {
            get
            {
                return returnEmpty;
            }

            set
            {
                returnEmpty = value;
            }
        }

  #if WindowsCE || PocketPC
  #else
 
        /// <summary>
        /// Parses an incoming URI string and sets the instance variables
        /// of this object.
        /// </summary>
        /// <param name="targetUri">Takes an incoming Uri string and parses all the properties of it</param>
        /// <returns>Throws a query exception when it finds something wrong with the input, otherwise returns a baseuri.</returns>
        protected override Uri ParseUri(Uri targetUri)
        {
            base.ParseUri(targetUri);
            if (targetUri != null)
            {
                char[] delimiters = { '?', '&'};

                TokenCollection tokens = new TokenCollection(targetUri.Query, delimiters);
                foreach (String token in tokens)
                {
                    if (token.Length > 0)
                    {
                        char[] otherDelimiters = { '='};
                        String[] parameters = token.Split(otherDelimiters, 2);
                        switch (parameters[0])
                        {
                            case "min-row":
                                this.MinimumRow = uint.Parse(parameters[1]);
                                break;
                            case "max-row":
                                this.MaximumRow = uint.Parse(parameters[1]);
                                break;
                            case "min-col":
                                this.MinimumColumn = uint.Parse(parameters[1]);
                                break;
                            case "max-col":
                                this.MaximumColumn = uint.Parse(parameters[1]);
                                break;
                            case "range":
                                this.Range = parameters[1];
                                break;
                            case "return-empty":
                                this.ReturnEmpty = (ReturnEmtpyCells) Enum.Parse(typeof(ReturnEmtpyCells), parameters[1]);
                                break;
                        }
                    }
                }
            }
            return this.Uri;
        }
#endif 
        /// <summary>
        /// Resets object state to default, as if newly created.
        /// </summary>
        protected override void Reset()
        {
            base.Reset();
            MinimumRow = MinimumColumn = 0;
            MaximumRow = MaximumColumn = uint.MaxValue;
            Range = "";
        }

        /// <summary>
        /// Creates the partial URI query string based on all set properties.
        /// </summary>
        /// <returns> string => the query part of the URI </returns>
        protected override string CalculateQuery(string basePath)
        {
            string path = base.CalculateQuery(basePath);
            StringBuilder newPath = new StringBuilder(path, 2048);
            char paramInsertion = InsertionParameter(path); 

            paramInsertion = AppendQueryPart(this.MinimumRow, 0, "min-row", paramInsertion, newPath);
            paramInsertion = AppendQueryPart(this.MaximumRow, uint.MaxValue, "max-row", paramInsertion, newPath);
            paramInsertion = AppendQueryPart(this.MinimumColumn, 0, "min-col", paramInsertion, newPath);
            paramInsertion = AppendQueryPart(this.MaximumColumn, uint.MaxValue, "max-col", paramInsertion, newPath);
            paramInsertion = AppendQueryPart(this.Range, "range", paramInsertion, newPath);

            if (ReturnEmpty == ReturnEmtpyCells.yes)
            {
                paramInsertion = AppendQueryPart("true", "return-empty", paramInsertion, newPath);
            } 
            else if (ReturnEmpty == ReturnEmtpyCells.no)
            { 
                paramInsertion = AppendQueryPart("false", "return-empty", paramInsertion, newPath);
            }

            return newPath.ToString();
        }
    }
}
