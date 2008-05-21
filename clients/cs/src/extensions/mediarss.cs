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
using Google.GData.Client;

namespace Google.GData.Extensions.MediaRss {

    /// <summary>
    /// helper to instantiate all factories defined in here and attach 
    /// them to a base object
    /// </summary>
    public class MediaRssExtensions
    {
        /// <summary>
        /// helper to add all MediaRss extensions to a base object
        /// </summary>
        /// <param name="baseObject"></param>
        public static void AddExtension(AtomBase baseObject) 
        {
            baseObject.AddExtension(new MediaGroup());
        }
    }


    /// <summary>
    /// short table for constants related to mediaRss declarations
    /// </summary>
    public class MediaRssNameTable 
    {
          /// <summary>static string to specify the mediarss namespace
          /// TODO: picasa has the namespace slighly wrong.
          /// </summary>
        public const string NSMediaRss  = "http://search.yahoo.com/mrss/";
        /// <summary>static string to specify the used mediarss prefix</summary>
        public const string mediaRssPrefix  = "media";
        /// <summary>static string to specify the media:group element</summary>
        public const string MediaRssGroup    = "group";
        /// <summary>static string to specify the media:credit element</summary>
        public const string MediaRssCredit    = "credit";
        /// <summary>static string to specify the media:content element</summary>
        public const string MediaRssContent    = "content";
        /// <summary>static string to specify the media:category element</summary>
        public const string MediaRssCategory    = "category";
        /// <summary>static string to specify the media:description element</summary>
        public const string MediaRssDescription    = "description";
         /// <summary>static string to specify the media:keywords element</summary>
        public const string MediaRssKeywords    = "keywords";
        /// <summary>static string to specify the media:thumbnail element</summary>
        public const string MediaRssThumbnail    = "thumbnail";
        /// <summary>static string to specify the media:title element</summary>
        public const string MediaRssTitle    = "title";

    }

    /// <summary>
    /// MediaGroup container element for the MediaRss namespace
    /// </summary>
    public class MediaGroup : SimpleContainer
    {
        private ThumbnailCollection thumbnails;
        private MediaContentCollection contents;
        private MediaCategoryCollection categories;
        /// <summary>
        /// default constructor for media:group
        /// </summary>
        public MediaGroup() :
            base(MediaRssNameTable.MediaRssGroup,
                 MediaRssNameTable.mediaRssPrefix,
                 MediaRssNameTable.NSMediaRss)
        {
            this.ExtensionFactories.Add(new MediaCredit());
            this.ExtensionFactories.Add(new MediaDescription());
            this.ExtensionFactories.Add(new MediaContent());
            this.ExtensionFactories.Add(new MediaKeywords());
            this.ExtensionFactories.Add(new MediaThumbnail());
            this.ExtensionFactories.Add(new MediaTitle());
            this.ExtensionFactories.Add(new MediaCategory());
        }

        /// <summary>
        /// returns the media:credit element
        /// </summary>
        public MediaCredit Credit
        {
            get
            {
                return FindExtension(MediaRssNameTable.MediaRssCredit,
                                     MediaRssNameTable.NSMediaRss) as MediaCredit;
            }
            set
            {
                ReplaceExtension(MediaRssNameTable.MediaRssCredit,
                                MediaRssNameTable.NSMediaRss,
                                value);
            }
        }

             /// <summary>
        /// returns the media:credit element
        /// </summary>
        public MediaDescription Description
        {
            get
            {
                return FindExtension(MediaRssNameTable.MediaRssDescription,
                                     MediaRssNameTable.NSMediaRss) as MediaDescription;
            }
            set
            {
                ReplaceExtension(MediaRssNameTable.MediaRssDescription,
                                MediaRssNameTable.NSMediaRss,
                                value);
            }
        }
        /// <summary>
        /// returns the media:content element
        /// </summary>
        public MediaKeywords Keywords
        {
            get
            {
                return FindExtension(MediaRssNameTable.MediaRssKeywords,
                                     MediaRssNameTable.NSMediaRss) as MediaKeywords;
            }
            set
            {
                ReplaceExtension(MediaRssNameTable.MediaRssKeywords,
                                MediaRssNameTable.NSMediaRss,
                                value);
            }
        }
        /// <summary>
        /// returns the media:credit element
        /// </summary>
        public MediaTitle Title
        {
            get
            {
                return FindExtension(MediaRssNameTable.MediaRssTitle,
                                     MediaRssNameTable.NSMediaRss) as MediaTitle;
            }
            set
            {
                ReplaceExtension(MediaRssNameTable.MediaRssTitle,
                                MediaRssNameTable.NSMediaRss,
                                value);
            }
        }


        /// <summary>
        ///  property accessor for the Thumbnails 
        /// </summary>
        public ThumbnailCollection Thumbnails
        {
            get 
            {
                if (this.thumbnails == null)
                {
                    this.thumbnails =  new ThumbnailCollection(this); 
                }
                return this.thumbnails;
            }
        }

        /// <summary>
        /// returns the media:content element
        /// </summary>
        public MediaContent Content
        {
            get
            {
                return FindExtension(MediaRssNameTable.MediaRssContent,
                                     MediaRssNameTable.NSMediaRss) as MediaContent;
            }
            set
            {
                ReplaceExtension(MediaRssNameTable.MediaRssContent,
                                MediaRssNameTable.NSMediaRss,
                                value);
            }
        }


        /// <summary>
        ///  property accessor for the Contents Collection 
        /// </summary>
        public MediaContentCollection Contents
        {
            get 
            {
                if (this.contents == null)
                {
                    this.contents =  new MediaContentCollection(this); 
                }
                return this.contents;
            }
        }

        /// <summary>
        ///  property accessor for the Category Collection 
        /// </summary>
        public MediaCategoryCollection Categories
        {
            get 
            {
                if (this.categories == null)
                {
                    this.categories =  new MediaCategoryCollection(this); 
                }
                return this.categories;
            }
        }
   }

    /// <summary>
    /// media:credit schema extension describing an credit given to media
    /// it's a child of media:group
    /// </summary>
    public class MediaCredit : SimpleElement
    {
        /// <summary>
        /// default constructor for media:credit
        /// </summary>
        public MediaCredit()
        : base(MediaRssNameTable.MediaRssCredit, 
               MediaRssNameTable.mediaRssPrefix,
               MediaRssNameTable.NSMediaRss)
        {}

        /// <summary>
        /// default constructor for media:credit with an initial value
        /// </summary>
        /// <param name="initValue"/>
        public MediaCredit(string initValue)
        : base(MediaRssNameTable.MediaRssCredit, 
               MediaRssNameTable.mediaRssPrefix,
               MediaRssNameTable.NSMediaRss, initValue)
        {}
    }

    /// <summary>
    /// media:description schema extension describing an description given to media
    /// it's a child of media:group
    /// </summary>
    public class MediaDescription : SimpleElement
    {
        /// <summary>
        /// default constructor for media:description 
        /// </summary>
        public MediaDescription()
        : base(MediaRssNameTable.MediaRssDescription, 
               MediaRssNameTable.mediaRssPrefix,
               MediaRssNameTable.NSMediaRss)
        {
            this.Attributes.Add("type", null);
        }

        /// <summary>
        /// default constructor for media:description with an initial value
        /// </summary>
        /// <param name="initValue"/>
        public MediaDescription(string initValue)
        : base(MediaRssNameTable.MediaRssDescription, 
               MediaRssNameTable.mediaRssPrefix,
               MediaRssNameTable.NSMediaRss, initValue)
        {
            this.Attributes.Add("type", null);
        }
    }


    /// <summary>
    /// media:content schema extension describing the content URL
    /// it's a child of media:group
    /// </summary>
    public class MediaContent : SimpleElement
    {
        /// <summary>
        /// default constructor for media:content
        /// </summary>
        public MediaContent()
        : base(MediaRssNameTable.MediaRssContent, 
               MediaRssNameTable.mediaRssPrefix,
               MediaRssNameTable.NSMediaRss)
        {
            this.Attributes.Add("url", null);
            this.Attributes.Add("type", null);
            this.Attributes.Add("medium", null);
            this.Attributes.Add("height", null);
            this.Attributes.Add("width", null);
            this.Attributes.Add("fileSize", null);
        }
    }

     /// <summary>
    /// media:content schema extension describing the content URL
    /// it's a child of media:group
    /// </summary>
    public class MediaCategory : SimpleElement
    {
        /// <summary>
        /// default constructor for media:content
        /// </summary>
        public MediaCategory()
        : base(MediaRssNameTable.MediaRssCategory, 
               MediaRssNameTable.mediaRssPrefix,
               MediaRssNameTable.NSMediaRss)
        {
            this.Attributes.Add("scheme", null);
            this.Attributes.Add("label", null);
        }

         /// <summary>
        /// default constructor for media:credit with an initial value
        /// </summary>
        /// <param name="initValue"/>
        public MediaCategory(string initValue)
        : base(MediaRssNameTable.MediaRssCategory, 
               MediaRssNameTable.mediaRssPrefix,
               MediaRssNameTable.NSMediaRss, initValue)
        {}
    }

    /// <summary>
    /// media:keywords schema extension describing a 
    /// comma seperated list of keywords
    /// it's a child of media:group
    /// </summary>
    public class MediaKeywords : SimpleElement
    {
        /// <summary>
        /// default constructor for media:keywords
        /// </summary>
        public MediaKeywords()
        : base(MediaRssNameTable.MediaRssKeywords, 
               MediaRssNameTable.mediaRssPrefix,
               MediaRssNameTable.NSMediaRss)
        {}

        /// <summary>
        /// default constructor for media:keywords with an initial value
        /// </summary>
        /// <param name="initValue"/>
        public MediaKeywords(string initValue)
        : base(MediaRssNameTable.MediaRssKeywords,
               MediaRssNameTable.mediaRssPrefix,
               MediaRssNameTable.NSMediaRss, initValue)
        {}
    }

    /// <summary>
    /// media:thumbnail schema extension describing a 
    /// thumbnail URL with height/width
    /// it's a child of media:group
    /// </summary>
    public class MediaThumbnail : SimpleElement
    {
        /// <summary>
        /// default constructor for media:thumbnail
        /// </summary>
        public MediaThumbnail()
        : base(MediaRssNameTable.MediaRssThumbnail, 
               MediaRssNameTable.mediaRssPrefix,
               MediaRssNameTable.NSMediaRss)
        {
            this.Attributes.Add("url", null);
            this.Attributes.Add("height", null);
            this.Attributes.Add("width", null);
        }
    }

    /// <summary>
    /// media:title schema extension describing the title given to media
    /// it's a child of media:group
    /// </summary>
    public class MediaTitle : SimpleElement
    {
        /// <summary>
        /// default constructor for media:title
        /// </summary>
        public MediaTitle()
        : base(MediaRssNameTable.MediaRssTitle, 
               MediaRssNameTable.mediaRssPrefix,
               MediaRssNameTable.NSMediaRss)
        {
            this.Attributes.Add("type", null);
        }

        /// <summary>
        /// default constructor for media:title with an initial value
        /// </summary>
        /// <param name="initValue"/>
        public MediaTitle(string initValue)
        : base(MediaRssNameTable.MediaRssTitle, 
               MediaRssNameTable.mediaRssPrefix,
               MediaRssNameTable.NSMediaRss, initValue)
        {
            this.Attributes.Add("type", null);
        }
    }

    //////////////////////////////////////////////////////////////////////
    /// <summary>Typed collection for Thumbnails Extensions.</summary>
    //////////////////////////////////////////////////////////////////////
    public class ThumbnailCollection : ExtensionCollection<MediaThumbnail>
    {
        private ThumbnailCollection() : base()
        {
        }

        /// <summary>constructor</summary> 
        public ThumbnailCollection(IExtensionContainer atomElement) 
            : base(atomElement, MediaRssNameTable.MediaRssThumbnail, MediaRssNameTable.NSMediaRss)
        {
        }
    }
    /////////////////////////////////////////////////////////////////////////////


    //////////////////////////////////////////////////////////////////////
    /// <summary>Typed collection for Thumbnails Extensions.</summary>
    //////////////////////////////////////////////////////////////////////
    public class MediaContentCollection : ExtensionCollection<MediaContent>
    {
        private MediaContentCollection() : base()
        {
        }

        /// <summary>constructor</summary> 
        public MediaContentCollection(IExtensionContainer atomElement) 
            : base(atomElement, MediaRssNameTable.MediaRssContent, MediaRssNameTable.NSMediaRss)
        {
        }

    }
    /////////////////////////////////////////////////////////////////////////////

    //////////////////////////////////////////////////////////////////////
    /// <summary>Typed collection for Thumbnails Extensions.</summary>
    //////////////////////////////////////////////////////////////////////
    public class MediaCategoryCollection : ExtensionCollection<MediaCategory>
    {
        private MediaCategoryCollection() : base()
        {
        }

        /// <summary>constructor</summary> 
        public MediaCategoryCollection(IExtensionContainer atomElement) 
            : base(atomElement, MediaRssNameTable.MediaRssCategory, MediaRssNameTable.NSMediaRss)
        {
        }

    }
    /////////////////////////////////////////////////////////////////////////////

}
