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
using System.Xml;
using System.Text;
using System.Globalization;
using System.Diagnostics;
using Google.GData.Client;

namespace Google.GData.Photos {

    //////////////////////////////////////////////////////////////////////
    /// <summary>
    /// A subclass of FeedQuery, to create an PicasaQuery query URI.
    /// Provides public properties that describe the different
    /// aspects of the URI, as well as a composite URI.
    /// The PicasaQuery supports the following GData parameters:
    ///     start-index and max-results parameters. It does not currently support the other standard parameters.
    ///  in addition, the following parameters:
    ///     Parameter   Meaning     
    ///      kind           what feed to retrieve
    ///      access     Visibility parameter    
    ///      thumbsize  Thumbnail size parameter 
    /// </summary> 
    //////////////////////////////////////////////////////////////////////
    public class PicasaQuery : FeedQuery
    {

        public enum Kinds 
        {
            album,
            photo, 
            comment,
            tag,
            none
        }

        public enum AccessLevel
        {
            AccessAll,
            AccessPrivate,
            AccessPublic
        }

        protected Kinds[] kinds = new Kinds[2];
        private AccessLevel access;
        private string thumbsize;

        public static string picasaBaseUri = "http://picasaweb.google.com/data/feed/";

        /// <summary>
        /// base constructor
        /// </summary>
        public PicasaQuery()
        : base()
        {
            this.kinds[0] = Kinds.tag;
            this.kinds[1] = Kinds.none;
            this.access = AccessLevel.AccessPublic;
        }



        /// <summary>
        /// base constructor, with initial queryUri
        /// </summary>
        /// <param name="queryUri">the query to use</param>
        public PicasaQuery(string queryUri)
        : base(queryUri)
        {
            this.kinds[0] = Kinds.tag;
            this.kinds[1] = Kinds.none;
        }

 

        //////////////////////////////////////////////////////////////////////
        /// <summary>indicates the kinds to retrieve
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public virtual Kinds[] KindParameter
        {
            get {return this.kinds;}
            set {this.kinds = value;}
        }
        // end of accessor public WebAlbumKinds

            //////////////////////////////////////////////////////////////////////
        /// <summary>indicates the access 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public AccessLevel Access
        {
            get {return this.access;}
            set {this.access = value;}
        }
        // end of accessor public Access

     
        //////////////////////////////////////////////////////////////////////
        /// <summary>indicates the thumbsize required
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public string Thumbsize
        {
            get {return this.thumbsize;}
            set {this.thumbsize = value;}
        }
        // end of accessor public Thumbsize

   
#if WindowsCE || PocketPC
#else
        //////////////////////////////////////////////////////////////////////
        /// <summary>protected void ParseUri</summary> 
        /// <param name="targetUri">takes an incoming Uri string and parses all the properties out of it</param>
        /// <returns>throws a query exception when it finds something wrong with the input, otherwise returns a baseuri</returns>
        //////////////////////////////////////////////////////////////////////
        protected override Uri ParseUri(Uri targetUri)
        {
            base.ParseUri(targetUri);
            if (targetUri != null)
            {
                char[] deli = { '?', '&'};

                TokenCollection tokens = new TokenCollection(targetUri.Query, deli);
                foreach (String token in tokens)
                {
                    if (token.Length > 0)
                    {
                        char[] otherDeli = { '='};
                        String[] parameters = token.Split(otherDeli, 2);
                        switch (parameters[0])
                        {
                            case "kind":
                                String[] kinds = parameters[1].Split(new char[] {','});

                                if (kinds != null && kinds.Length > 0)
                                {
                                    this.kinds[0] = (Kinds) Enum.Parse(typeof(Kinds), kinds[0]);
                                    if (kinds.Length == 2)
                                    {
                                        this.kinds[1] = (Kinds) Enum.Parse(typeof(Kinds), kinds[1]);
                                    }
                                }
                                break;
                            case "thumbsize":
                                this.thumbsize = parameters[1];
                                break;
                            case "access":
                                if (String.Compare("all", parameters[1], false, CultureInfo.InvariantCulture) == 0)
                                { 
                                    this.Access = AccessLevel.AccessAll;
                                } 
                                else if (String.Compare("private", parameters[1], false, CultureInfo.InvariantCulture) == 0)
                                {
                                    this.Access = AccessLevel.AccessPrivate;
                                } 
                                else 
                                {
                                    this.Access = AccessLevel.AccessPublic;
                                }
                                break;
                        }
                    }
                }
            }
            return this.Uri;
        }
#endif

        //////////////////////////////////////////////////////////////////////
        /// <summary>Resets object state to default, as if newly created.
        /// </summary> 
        //////////////////////////////////////////////////////////////////////
        protected override void Reset()
        {
            base.Reset();
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>Creates the partial URI query string based on all
        ///  set properties.</summary> 
        /// <returns> string => the query part of the URI </returns>
        //////////////////////////////////////////////////////////////////////
        protected override string CalculateQuery()
        {
            string path = base.CalculateQuery();
            StringBuilder newPath = new StringBuilder(path, 2048);

            char paramInsertion;

            if (path.IndexOf('?') == -1)
            {
                paramInsertion = '?';
            }
            else
            {
                paramInsertion = '&';
            }

            newPath.Append(paramInsertion);
            String kinds = "";

            if (this.kinds[0] != Kinds.none)
            {
                kinds = this.kinds[0].ToString();
            }

            if (this.kinds[1] != Kinds.none && this.kinds[1] != this.kinds[0])
            {
                if (kinds != null)
                {
                    kinds += ",";
                }
                kinds += this.kinds[1].ToString();
            }

            if (kinds.Length > 0)
            {
                newPath.AppendFormat(CultureInfo.InvariantCulture, "kind={0}", kinds);
                paramInsertion = '&';
            }
           
            if (Utilities.IsPersistable(this.Thumbsize))
            {
                newPath.Append(paramInsertion);
                newPath.AppendFormat(CultureInfo.InvariantCulture, "thumbsize={0}", Utilities.UriEncodeReserved(this.Thumbsize)); 
                paramInsertion = '&';
            }

            newPath.Append(paramInsertion);

            String acc;

            if (this.Access == AccessLevel.AccessAll)
            {
                acc = "all";
            } 
            else if (this.Access == AccessLevel.AccessPrivate)
            {
                acc = "private";
            }
            else 
            {
                acc = "public";
            }
            newPath.AppendFormat(CultureInfo.InvariantCulture, "access={0}", acc);
            return newPath.ToString();
        }
    }
}
