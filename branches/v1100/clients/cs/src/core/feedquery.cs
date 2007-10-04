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
#region Using directives

#define USE_TRACING

using System;
using System.Xml;
using System.Text; 
using System.Globalization;
using System.Diagnostics;

#endregion

namespace Google.GData.Client
{


    //////////////////////////////////////////////////////////////////////
    /// <summary>Enum to describe the different category boolean operations.
    /// </summary> 
    //////////////////////////////////////////////////////////////////////
    public enum QueryCategoryOperator
    {
        /// <summary>A logical AND operation.</summary>
        AND,                       
        /// <summary>A logical OR operation.</summary>
        OR
    }
    /////////////////////////////////////////////////////////////////////////////




    //////////////////////////////////////////////////////////////////////
    /// <summary>Base class to hold an Atom category plus the boolean
    /// to create the query category.
    /// </summary> 
    //////////////////////////////////////////////////////////////////////
    public class QueryCategory 
    {
        /// <summary>AtomCategory holder.</summary> 
        private AtomCategory category;
        /// <summary>Boolean operator (can be OR or AND).</summary> 
        private QueryCategoryOperator categoryOperator; 
        /// <summary>Boolean negator (can be true or false).</summary> 
        private bool isExcluded; 

        
        //////////////////////////////////////////////////////////////////////
        /// <summary>Constructor, given a category.</summary>
        //////////////////////////////////////////////////////////////////////
        public QueryCategory(AtomCategory category)
        {
            this.category = category;
            this.categoryOperator = QueryCategoryOperator.AND; 
        }



        //////////////////////////////////////////////////////////////////////
        /// <summary>Constructor, given a category as a string from the URI.</summary>
        //////////////////////////////////////////////////////////////////////
        public QueryCategory(string strCategory, QueryCategoryOperator op)
        {
            Tracing.TraceMsg("Depersisting category from: " + strCategory); 
            this.categoryOperator = op; 
            strCategory = FeedQuery.CleanPart(strCategory); 

            // let's parse the string
            if (strCategory[0] == '-')
            {
                // negator
                this.isExcluded = true; 
                // remove him
                strCategory = strCategory.Substring(1, strCategory.Length-1); 
            }

            // let's extract the scheme if there is one...
            int iStart = strCategory.IndexOf('{') ; 
            int iEnd = strCategory.IndexOf('}') ; 
            AtomUri scheme = null; 
            if (iStart != -1 && iEnd != -1)
            {
                // 
                iEnd++;
                iStart++;
                scheme = new AtomUri(strCategory.Substring(iStart, iEnd- iStart-1)); 
                // the rest is then
                strCategory = strCategory.Substring(iEnd, strCategory.Length - iEnd); 

            }

            Tracing.TraceMsg("Category found: " + strCategory + " - scheme: " + scheme); 

            this.category = new AtomCategory(strCategory, scheme);
        }


        //////////////////////////////////////////////////////////////////////
        /// <summary>Accessor method public AtomCategory Category</summary> 
        /// <returns></returns>
        //////////////////////////////////////////////////////////////////////
        public AtomCategory Category
        {
            get {return this.category;}
            set {this.category = value;}
        }
        /////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////
        /// <summary>Accessor method public QueryCategoryOperator Operator</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public QueryCategoryOperator Operator
        {
            get {return this.categoryOperator;}
            set {this.categoryOperator = value;}
        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>Accessor method public bool Excluded</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public bool Excluded
        {
            get {return this.isExcluded;}
            set {this.isExcluded = value;}
        }
        /////////////////////////////////////////////////////////////////////////////


    }

    //////////////////////////////////////////////////////////////////////
    /// <summary>Base class to create a GData query URI. Provides public 
    /// properties that describe the different aspects of the URI
    /// as well as a composite URI.
    /// </summary> 
    //////////////////////////////////////////////////////////////////////
    public class FeedQuery
    {
        #region member variables
        /// <summary>baseUri property holder</summary> 
        private string query;
        /// <summary>category part as string, comma seperated</summary> 
        private QueryCategoryCollection categories;
        /// <summary>author part as string</summary> 
        private string author;
        /// <summary>extra parameters as string</summary> 
        private string extraParameters;
        /// <summary>mininum date/time as DateTime</summary> 
        private DateTime datetimeMin;
        /// <summary>maximum date/time as DateTime</summary> 
        private DateTime datetimeMax;
        /// <summary>mininum date/time for the publicationdate as DateTime</summary> 
        private DateTime publishedMin;
        /// <summary>maximum date/time for the publicationdate as DateTime</summary> 
        private DateTime publishedMax;
        /// <summary>start-index as integer</summary> 
        private int startIndex;
        /// <summary>number of entries to retrieve as integer</summary> 
        private int numToRetrieve;
        /// <summary>alternative format as AlternativeFormat</summary> 
        private AlternativeFormat altFormat;
        /// <summary>the base URI</summary> 
        protected string baseUri;
        #endregion
        //////////////////////////////////////////////////////////////////////
        /// <summary>Default constructor.</summary> 
        //////////////////////////////////////////////////////////////////////
        public FeedQuery()
        {
            // set some defaults...
            this.FeedFormat = AlternativeFormat.Atom;
        }
        /////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////
        /// <summary>constructor taking a base URI constructor.</summary> 
        //////////////////////////////////////////////////////////////////////
        public FeedQuery(string baseUri)
        {
            // set some defaults...
            this.FeedFormat = AlternativeFormat.Atom;
            this.baseUri = baseUri; 
        }
        /////////////////////////////////////////////////////////////////////////////


     
        //////////////////////////////////////////////////////////////////////
        /// <summary>We do not hold on to the precalculated Uri.
        /// It's safer and cheaper to calculate this on the fly.
        /// Setting this loses the base Uri.</summary> 
        /// <returns>returns the complete UriPart that is used to execute the query</returns>
        //////////////////////////////////////////////////////////////////////
        public Uri Uri
        {
            get {
                return new Uri(this.baseUri + CalculateQuery());
                }
            
#if WindowsCE || PocketPC
#else
            set 
                {
                ParseUri(value);
                }
#endif
        }
        /////////////////////////////////////////////////////////////////////////////


#if WindowsCE || PocketPC
#else
        //////////////////////////////////////////////////////////////////////
        /// <summary>Passing in a complete URI, we strip all the
        /// GData query-related things and then treat the rest
        /// as the base URI. For this we create a service.</summary> 
        /// <param name="uri">a complete URI</param>
        /// <param name="service">the new GData service for this URI</param>
        /// <param name="query">the parsed query object for this URI</param>
        //////////////////////////////////////////////////////////////////////
        public static void Parse(Uri uri, out Service service, out FeedQuery query)
        {
            query = new FeedQuery();

            query.ParseUri(uri);

            service = new Service();
        }
        /////////////////////////////////////////////////////////////////////////////
#endif


        //////////////////////////////////////////////////////////////////////
        /// <summary>Accessor method public string Query.</summary> 
        /// <returns>returns the query string portion of the URI</returns>
        //////////////////////////////////////////////////////////////////////
        public string Query
        {
            get {return this.query;}
            set {this.query = value; }
        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>Accessor method public string Category.</summary> 
        /// <returns>the category filter</returns>
        //////////////////////////////////////////////////////////////////////
        public QueryCategoryCollection Categories
        {
            get 
            {
                if (this.categories == null)
                {
                    this.categories = new QueryCategoryCollection(); 
                }
                return this.categories;
            }
        }
        /////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////
        /// <summary>Accessor method public string ExtraParameters.</summary> 
        /// <returns></returns>
        //////////////////////////////////////////////////////////////////////
        public string ExtraParameters
        {
            get {return this.extraParameters;}
            set {this.extraParameters = value;}
        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>Accessor method public string Author.</summary> 
        /// <returns>the requested author</returns>
        //////////////////////////////////////////////////////////////////////
        public string Author
        {
            get { return this.author;}
            set { this.author = value; }
        }
        /////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////
        /// <summary>set's the mininum daterange value for the updated element</summary> 
        /// <returns>the min (inclusive) date/time</returns>
        //////////////////////////////////////////////////////////////////////
        public DateTime StartDate
        {
            get {return this.datetimeMin;}
            set {this.datetimeMin = value; }
        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>set's the maximum daterange value for the updated element</summary> 
        /// <returns>the max (exclusive) date/time</returns>
        //////////////////////////////////////////////////////////////////////
        public DateTime EndDate
        {
            get {return this.datetimeMax;}
            set {this.datetimeMax = value; }
        }
        /////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////
        /// <summary>set's the mininum daterange value for the publication element</summary> 
        /// <returns>the min (inclusive) date/time</returns>
        //////////////////////////////////////////////////////////////////////
        public DateTime MinPublication
        {
            get {return this.publishedMin;}
            set {this.publishedMin = value; }
        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>set's the maximum daterange value for the publication element</summary> 
        /// <returns>the max (exclusive) date/time</returns>
        //////////////////////////////////////////////////////////////////////
        public DateTime MaxPublication
        {
            get {return this.publishedMax;}
            set {this.publishedMax = value; }
        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>Accessor method public int StartIndex.</summary> 
        /// <returns>the start-index query parameter, a 1-based index
        /// indicating the first result to be retrieved.</returns>
        //////////////////////////////////////////////////////////////////////
        public int StartIndex
        {
            get {return this.startIndex;}
            set {this.startIndex = value; }
        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>Accessor method public int NumberToRetrieve.</summary> 
        /// <returns>the number of entries to retrieve</returns>
        //////////////////////////////////////////////////////////////////////
        public int NumberToRetrieve
        {
            get {return this.numToRetrieve;}
            set {this.numToRetrieve = value; }
        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>Accessor method public AlternativeFormat FeedFormat.
        /// </summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public AlternativeFormat FeedFormat
        {
            get {return this.altFormat;}
            set {this.altFormat = value; }
        }
        /////////////////////////////////////////////////////////////////////////////

#if WindowsCE || PocketPC
#else
        //////////////////////////////////////////////////////////////////////
        /// <summary>protected void ParseUri</summary> 
        /// <param name="targetUri">takes an incoming Uri string and parses all the properties out of it</param>
        /// <returns>throws a query exception when it finds something wrong with the input, otherwise returns a baseuri</returns>
        //////////////////////////////////////////////////////////////////////
        protected virtual Uri ParseUri(Uri targetUri)
        {
            Reset(); 
            StringBuilder newPath = null;
            UriBuilder newUri = null; 

            if (targetUri != null)
            {
                TokenCollection tokens; 
                // want to check some basic things on this guy first...
                ValidateUri(targetUri);
                newPath = new StringBuilder("", 2048);  
                newUri = new UriBuilder(targetUri);
                newUri.Path = null; 
                newUri.Query = null; 

                // now parse the query string and take the properties out
                string [] parts = targetUri.Segments;
                bool fCategory = false;

                foreach (string part in parts)
                {
                    string segment = CleanPart(part);
                    if (segment.Equals("-") == true)
                    {
                        // found the category simulator
                        fCategory = true; 
                    }
                    else if (fCategory == true)
                    {
                        // take the string, and create some category objects out of it...
                        
                        // replace the curly braces and the or operator | so that we can tokenize better
                        segment = segment.Replace("%7B", "{"); 
                        segment = segment.Replace("%7D", "}");
                        segment = segment.Replace("%7C", "|");

                        // let's see if it's the only one...
                        tokens = new TokenCollection(segment, new char[1] {'|'}); 
                        QueryCategoryOperator op = QueryCategoryOperator.AND; 
                        foreach (String token in tokens)
                        {
                            // each one is a category
                            QueryCategory category = new QueryCategory(token, op); 
                            this.Categories.Add(category);
                            op = QueryCategoryOperator.OR; 
                        }
                    }
                    else
                    {
                        newPath.Append(part);
                    }
                }

                char [] deli = {'?','&'}; 

                tokens = new TokenCollection(targetUri.Query, deli); 
                foreach (String token in tokens )
                {
                    if (token.Length > 0)
                    {
                        char [] otherDeli = {'='};
                        String [] parameters = token.Split(otherDeli,2); 
                        switch (parameters[0])
                        {
                            case "q":
                                this.Query = parameters[1];
                                break;
                            case "author":
                                this.Author = parameters[1];
                                break;
                            case "start-index":
                                this.StartIndex = int.Parse(parameters[1], CultureInfo.InvariantCulture);
                                break;
                            case "max-results":
                                this.NumberToRetrieve = int.Parse(parameters[1], CultureInfo.InvariantCulture);
                                break;
                            case "updated-min":
                                this.StartDate = DateTime.Parse(parameters[1], CultureInfo.InvariantCulture);
                                break;
                            case "updated-max":
                                this.EndDate = DateTime.Parse(parameters[1], CultureInfo.InvariantCulture);
                                break;
                            case "published-min":
                                this.MinPublication = DateTime.Parse(parameters[1], CultureInfo.InvariantCulture);
                                break;
                            case "published-max":
                                this.MaxPublication = DateTime.Parse(parameters[1], CultureInfo.InvariantCulture);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }

            if (newPath != null)
            {
                if (newPath[newPath.Length-1] == '/')
                    newPath.Length = newPath.Length -1 ;

                newUri.Path = newPath.ToString(); 
                this.baseUri = newUri.Uri.AbsoluteUri;

            }
            return null; 
        }
        /////////////////////////////////////////////////////////////////////////////
        

        //////////////////////////////////////////////////////////////////////
        /// <summary>protected void ParseUri</summary> 
        /// <param name="target">takes an incoming string and parses all the properties out of it</param>
        /// <returns>throws a query exception when it finds something wrong with the input, otherwise returns a baseuri</returns>
        //////////////////////////////////////////////////////////////////////
        protected Uri ParseUri(string target)
        {
            Reset(); 
            if (target != null)
            {
                return ParseUri(new Uri(target));
            }
            return null; 
            
        }
        /////////////////////////////////////////////////////////////////////////////
#endif 
        //////////////////////////////////////////////////////////////////////
        /// <summary>Takes an incoming URI segment and removes leading/trailing slashes.</summary> 
        /// <param name="part">the URI segment to clean</param>
        /// <returns>the cleaned string</returns>
        //////////////////////////////////////////////////////////////////////
        static internal string CleanPart(string part)
        {
            Tracing.Assert(part != null, "part should not be null");
            if (part == null)
            {
                throw new ArgumentNullException("part"); 
            }
            
            string cleaned = part.Trim();
            if (cleaned.EndsWith("/"))
            {
                cleaned = cleaned.Substring(0, cleaned.Length-1);
            }
            if (cleaned.StartsWith("/"))
            {
                cleaned = cleaned.Substring(1, cleaned.Length-1);
            }

            return cleaned;
        }
        /////////////////////////////////////////////////////////////////////////////



        //////////////////////////////////////////////////////////////////////
        /// <summary>Checks to see if the URI is valid to be used for an Atom query.</summary> 
        /// <returns>Throws a client exception if not</returns>
        //////////////////////////////////////////////////////////////////////
        static protected void ValidateUri(Uri uriToTest)
        {
            Tracing.Assert(uriToTest != null, "uriToTest should not be null");
            if (uriToTest == null)
            {
                throw new ArgumentNullException("uriToTest"); 
            }

            if (uriToTest.Scheme == Uri.UriSchemeFile || uriToTest.Scheme == Uri.UriSchemeHttp  || uriToTest.Scheme == Uri.UriSchemeHttps)
            {
                return;
            }
            throw new ClientQueryException("Only HTTP/HTTPS 1.1 protocol is currently supported");
        }
        /////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////
        /// <summary>Resets object state to default, as if newly created.
        /// </summary> 
        //////////////////////////////////////////////////////////////////////
        protected virtual void Reset()
        {
            this.query = this.author = String.Empty; 
            this.categories = null; 
            this.datetimeMax = this.datetimeMin = Utilities.EmptyDate; 
            this.MinPublication = this.MaxPublication = Utilities.EmptyDate; 
            this.startIndex = this.numToRetrieve = 0; 
            this.altFormat = AlternativeFormat.Atom;
        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>Creates the partial URI query string based on all set properties.</summary> 
        /// <returns> string => the query part of the URI</returns>
        //////////////////////////////////////////////////////////////////////
        protected virtual string CalculateQuery()
        {
            Tracing.TraceCall("creating target Uri");


            StringBuilder newPath = new StringBuilder("", 2048);  


            // now let's build up a big string and check if we have all the parts we need
            bool firstTime = true; 

            // categories come first 
            
            foreach (QueryCategory category in this.Categories )
            {
                string strCategory = Utilities.UriEncodeReserved(category.Category.UriString); 

                if (Utilities.IsPersistable(strCategory))
                {
                    if (firstTime == true)
                    {
                        newPath.Append("/-/"); 
                    }
                    else
                    {
                        switch (category.Operator)
                        {
                            case QueryCategoryOperator.AND:
                                // we get another AND, so it's a new path
                                newPath.Append("/");
                                break;
                            case QueryCategoryOperator.OR:
                                newPath.Append("|");
                                break;
                        }
                    }
    
                    firstTime = false; 
    
                    if (category.Excluded == true)
                    {
                        newPath.AppendFormat(CultureInfo.InvariantCulture, "-{0}", strCategory);
                    }
                    else
                    {
                        newPath.AppendFormat(CultureInfo.InvariantCulture, "{0}", strCategory);
                    }   
                }
                else
                {
                    throw new ClientQueryException("One of the categories could not be persisted to a string");
                }
            }

            char paramInsertion = '?';
            
            if (this.FeedFormat != AlternativeFormat.Atom)
            {
                newPath.Append(paramInsertion);
                newPath.AppendFormat(CultureInfo.InvariantCulture, "alt={0}", FormatToString(this.FeedFormat));
                paramInsertion = '&'; 
            }

            // just add all the other things, as they are available... 
            if (Utilities.IsPersistable(this.Query))
            {  
                newPath.Append(paramInsertion);
                newPath.AppendFormat(CultureInfo.InvariantCulture, "q={0}", Utilities.UriEncodeReserved(this.Query));
                paramInsertion = '&'; 
            }
            if (Utilities.IsPersistable(this.Author))
            {
                newPath.Append(paramInsertion);
                newPath.AppendFormat(CultureInfo.InvariantCulture, "author={0}", Utilities.UriEncodeReserved(this.Author)); 
                paramInsertion = '&'; 
            }
            if (Utilities.IsPersistable(this.StartDate))
            {
                newPath.Append(paramInsertion);
                newPath.AppendFormat(CultureInfo.InvariantCulture, "updated-min={0}", Utilities.UriEncodeReserved(Utilities.LocalDateTimeInUTC(this.StartDate)));
                paramInsertion = '&'; 
            }
            if (Utilities.IsPersistable(this.EndDate))
            {
                newPath.Append(paramInsertion);
                newPath.AppendFormat(CultureInfo.InvariantCulture, "updated-max={0:G}", Utilities.UriEncodeReserved(Utilities.LocalDateTimeInUTC(this.EndDate)));
                paramInsertion = '&'; 
            }

            if (Utilities.IsPersistable(this.MinPublication))
            {
                newPath.Append(paramInsertion);
                newPath.AppendFormat(CultureInfo.InvariantCulture, "published-min={0:G}", Utilities.UriEncodeReserved(Utilities.LocalDateTimeInUTC(this.MinPublication)));
                paramInsertion = '&'; 
            }
            if (Utilities.IsPersistable(this.MaxPublication))
            {
                newPath.Append(paramInsertion);
                newPath.AppendFormat(CultureInfo.InvariantCulture, "published-max={0:G}", Utilities.UriEncodeReserved(Utilities.LocalDateTimeInUTC(this.MaxPublication)));
                paramInsertion = '&'; 
            }

            if (this.StartIndex != 0)
            {
                newPath.Append(paramInsertion);
                newPath.AppendFormat(CultureInfo.InvariantCulture, "start-index={0:d}", this.StartIndex);
                paramInsertion = '&'; 
            }
            if (this.NumberToRetrieve != 0)
            {
                newPath.Append(paramInsertion);
                newPath.AppendFormat(CultureInfo.InvariantCulture, "max-results={0:d}", this.NumberToRetrieve);
                paramInsertion = '&'; 
            }

            if (Utilities.IsPersistable(this.ExtraParameters))
            {
                newPath.Append(paramInsertion);
                newPath.Append(ExtraParameters);
            }

            return newPath.ToString();

        }
        /////////////////////////////////////////////////////////////////////////////



        //////////////////////////////////////////////////////////////////////
        /// <summary>Converts an AlternativeFormat to a string for use in
        /// the query string.</summary> 
        /// <param name="format">the format that we want to be converted to string </param>
        /// <returns>string version of the format</returns>
        //////////////////////////////////////////////////////////////////////
        static protected string FormatToString(AlternativeFormat format)
        {
            switch (format)
            {
                case AlternativeFormat.Atom:
                    return "atom";
                case AlternativeFormat.Rss:
                    return "rss";
                case AlternativeFormat.OpenSearchRss:
                    return "osrss";
            }
            return null;
        }
        /////////////////////////////////////////////////////////////////////////////
    }
    /////////////////////////////////////////////////////////////////////////////
    
}
