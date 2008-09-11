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
    /// A subclass of PicasaQuery, to create an Album query URI.
    /// Provides public properties that describe the different
    /// aspects of the URI, as well as a composite URI.
    /// The AlbumQuery automatically set's the kind parameter to Album
    /// </summary> 
    //////////////////////////////////////////////////////////////////////
    public abstract class KindQuery : PicasaQuery
    {

        /// <summary>
        /// base constructor
        /// </summary>
        public KindQuery(Kinds kind)
        : base()
        {
            this.kindsAsText = kind.ToString();
        }

        /// <summary>
        /// base constructor, with initial queryUri
        /// </summary>
        /// <param name="queryUri">the query to use</param>
        /// <param name="kind">the kind of query</param>
        public KindQuery(string queryUri, Kinds kind)
        : base(queryUri)
        {
            this.kindsAsText = kind.ToString();
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>read only: the kinds to retrieve</summary>
        /// <returns>a comma separated string of Kinds </returns>
        //////////////////////////////////////////////////////////////////////
        public override string KindParameter
        {
            get {return this.kindsAsText;}
        }
        // end of accessor public WebAlbumKinds

    }

    //////////////////////////////////////////////////////////////////////
    /// <summary>
    /// A subclass of PicasaQuery, to create an Album query URI.
    /// Provides public properties that describe the different
    /// aspects of the URI, as well as a composite URI.
    /// The AlbumQuery automatically set's the kind parameter to Album
    /// </summary> 
    //////////////////////////////////////////////////////////////////////
    public class AlbumQuery : KindQuery
    {

        /// <summary>
        /// base constructor
        /// </summary>
        public AlbumQuery()
        : base(Kinds.album)
        {
        }

        /// <summary>
        /// base constructor, with initial queryUri
        /// </summary>
        /// <param name="queryUri">the query to use</param>
        public AlbumQuery(string queryUri)
        : base(queryUri, Kinds.album)
        {
        }

    }


    //////////////////////////////////////////////////////////////////////
    /// <summary>
    /// A subclass of PicasaQuery, to create an Album query URI.
    /// Provides public properties that describe the different
    /// aspects of the URI, as well as a composite URI.
    /// The PhotoQuery automatically set's the kind parameter to Photo
    /// </summary> 
    //////////////////////////////////////////////////////////////////////
    public class PhotoQuery : KindQuery
    {

        /// <summary>
        /// base constructor
        /// </summary>
        public PhotoQuery()
        : base(Kinds.photo)
        {
        }

        /// <summary>
        /// base constructor, with initial queryUri
        /// </summary>
        /// <param name="queryUri">the query to use</param>
        public PhotoQuery(string queryUri)
        : base(queryUri, Kinds.photo)
        {
        }
    }


    //////////////////////////////////////////////////////////////////////
    /// <summary>
    /// A subclass of PicasaQuery, to create an Comments query URI.
    /// Provides public properties that describe the different
    /// aspects of the URI, as well as a composite URI.
    /// The CommentsQuery automatically set's the kind parameter to Comment
    /// </summary> 
    //////////////////////////////////////////////////////////////////////
    public class CommentsQuery : KindQuery
    {

        /// <summary>
        /// base constructor
        /// </summary>
        public CommentsQuery()
        : base(Kinds.comment)
        {
        }

        /// <summary>
        /// base constructor, with initial queryUri
        /// </summary>
        /// <param name="queryUri">the query to use</param>
        public CommentsQuery(string queryUri)
        : base(queryUri, Kinds.comment)
        {
        }
    }

     //////////////////////////////////////////////////////////////////////
    /// <summary>
    /// A subclass of PicasaQuery, to create an Comments query URI.
    /// Provides public properties that describe the different
    /// aspects of the URI, as well as a composite URI.
    /// The TagQuery automatically set's the kind parameter to Tag
    /// </summary> 
    //////////////////////////////////////////////////////////////////////
    public class TagQuery : KindQuery
    {

        /// <summary>
        /// base constructor
        /// </summary>
        public TagQuery()
        : base(Kinds.tag)
        {
        }

        /// <summary>
        /// base constructor, with initial queryUri
        /// </summary>
        /// <param name="queryUri">the query to use</param>
        public TagQuery(string queryUri)
        : base(queryUri, Kinds.tag)
        {
        }
    }
}
