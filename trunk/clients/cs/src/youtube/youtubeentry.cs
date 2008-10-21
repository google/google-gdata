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
#define USE_TRACING

using System;
using System.Xml;
using System.IO; 
using System.Collections;
using Google.GData.Client;
using Google.GData.Extensions;
using Google.GData.Extensions.MediaRss;
using Google.GData.Extensions.Exif;
using Google.GData.Extensions.Location;
using Google.GData.Extensions.AppControl;

namespace Google.GData.YouTube {

    //////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Entry API customization class for defining entries in a YouTubeFeed feed.
    /// </summary>
    //////////////////////////////////////////////////////////////////////
    public class YouTubeEntry : YouTubeBaseEntry
    {
        /// <summary>
        /// Constructs a new YouTubeEntry instance
        /// </summary>
        public YouTubeEntry()
        : base()
        {
            Tracing.TraceMsg("Created YouTubeEntry");
            addYouTubeEntryExtensions();
        
        }


         /// <summary>
        ///  helper method to add extensions to the evententry
        /// </summary>
        private void addYouTubeEntryExtensions()
        {
            MediaGroup mg = new MediaGroup();
            // extend the MediaGroup to accept a Duration 
            // and Private element()
            mg.ExtensionFactories.Add(new Duration());
            mg.ExtensionFactories.Add(new Private());

            // replace the media group default media credit with a new one. 
            MediaCredit c = new MediaCredit();
            c.Attributes.Add("type", null);
            c.AttributeNamespaces.Add("type", YouTubeNameTable.NSYouTube);
            mg.ReplaceExtension(c.XmlName, c.XmlNameSpace, c);
            // now add it to us
            this.AddExtension(mg);

            GeoRssExtensions.AddExtension(this);

            AppControl app = new AppControl();
            AppControl acf = FindExtensionFactory(app.XmlName, app.XmlNameSpace) as AppControl;
            if (acf == null)
            {
                // create a default appControl element
                acf = new AppControl();
                this.AddExtension(acf);
            }
            // add the youtube state element
            acf.ExtensionFactories.Add(new State());

            // things from the gd namespce
            this.AddExtension(new Comments());
            this.AddExtension(new Rating());

            // add youtube namespace elements
            this.AddExtension(new Statistics());
            this.AddExtension(new Location());
            this.AddExtension(new Recorded());
            this.AddExtension(new Uploaded());
            this.AddExtension(new VideoId());

        }

        /// <summary>
        /// getter/setter for the GeoRssWhere extension element
        /// </summary>
        public GeoRssWhere Location 
        {
            get
            {
                return FindExtension(GeoNametable.GeoRssWhereElement,
                                     GeoNametable.NSGeoRss) as GeoRssWhere;
            }
            set
            {
                ReplaceExtension(GeoNametable.GeoRssWhereElement,
                                GeoNametable.NSGeoRss,
                                value);
            }
        }

        /// <summary>
        /// returns the media:rss group container element
        /// </summary>
        public MediaGroup Media
        {
            get
            {
                return FindExtension(MediaRssNameTable.MediaRssGroup,
                                     MediaRssNameTable.NSMediaRss) as MediaGroup;
            }
            set
            {
                ReplaceExtension(MediaRssNameTable.MediaRssGroup,
                                MediaRssNameTable.NSMediaRss,
                                value);
            }
        }

        /// <summary>
        /// returns the yt:statistics element
        /// </summary>
        /// <returns></returns>
        public Statistics Statistics
        {
            get
            {
                return FindExtension(YouTubeNameTable.Statistics,
                                     YouTubeNameTable.NSYouTube) as Statistics;
            }
            set
            {
                ReplaceExtension(YouTubeNameTable.Statistics,
                                YouTubeNameTable.NSYouTube,
                                value);
            }
        }


        /// <summary>
        /// Updates this YouTubeEntry.
        /// </summary>
        /// <returns>the updated YouTubeEntry or null</returns>
        public new YouTubeEntry Update()
        {
            return base.Update() as YouTubeEntry;
        }



        /// <summary>
        ///  property accessor for the Comments
        /// </summary>
        public Comments Comments
        {
            get
            {
                return FindExtension(GDataParserNameTable.XmlCommentsElement,
                                 GDataParserNameTable.gNamespace) as Comments;
            }
            set
            {
                ReplaceExtension(GDataParserNameTable.XmlCommentsElement,
                                     GDataParserNameTable.gNamespace, value);
            }
        }


       /// <summary>
       /// returns the gd:rating element
       /// </summary>
       /// <returns></returns>
       public Rating Rating
       {
           get
           {
               return FindExtension(GDataParserNameTable.XmlRatingElement,
                                    GDataParserNameTable.gNamespace) as Rating;
           }
           set
           {

                ReplaceExtension(GDataParserNameTable.XmlRatingElement,
                               GDataParserNameTable.gNamespace,
                               value);
           }
       }
        
       /// <summary>
       /// returns the ratings link relationship as an atomUri
       /// </summary>
       public AtomUri RatingsLink
       {
           get
           {
                AtomLink link = this.Links.FindService(YouTubeNameTable.RatingsRelationship, AtomLink.ATOM_TYPE);
                // scan the link collection
                return link == null ? null : link.HRef;
            }
            set
            {
                AtomLink link = this.Links.FindService(YouTubeNameTable.RatingsRelationship, AtomLink.ATOM_TYPE);
                if (link == null)
                {
                    link = new AtomLink(AtomLink.ATOM_TYPE, YouTubeNameTable.RatingsRelationship);
                    this.Links.Add(link);
                }
                link.HRef = value;
            }
        }
        /////////////////////////////////////////////////////////////////////////////

       /// <summary>
       /// returns the yt:duration element
       /// </summary>
       /// <returns></returns>
       public Duration Duration
       {
           get
           {
               if (this.Media != null)
               {
                   return Media.FindExtension(YouTubeNameTable.Duration,
                                        YouTubeNameTable.NSYouTube) as Duration;
               }
               return null;
           }
           set
           {
               if (this.Media == null)
               {
                   this.Media = new MediaGroup();
               }
               this.Media.ReplaceExtension(YouTubeNameTable.Duration,
                               YouTubeNameTable.NSYouTube,
                               value);
           }
       }



        /// <summary>
        /// Returns the yt:state tag inside of app:control
        /// </summary>
        public State State
        {
            get
            {
                if (this.AppControl != null)
                {
                    return this.AppControl.FindExtension(YouTubeNameTable.State, 
                        YouTubeNameTable.NSYouTube) as State;
                }

                return null;
            }
            set
            {
                this.Dirty = true;
                if (this.AppControl == null)
                {
                    this.AppControl = new AppControl();
                }
                this.AppControl.ReplaceExtension(YouTubeNameTable.State,
                    YouTubeNameTable.NSYouTube, value);
            }
        }

        /// <summary>
        ///  property accessor for the VideoID, if applicable
        /// </summary>
        public string VideoId
        {
            get
            {
                return GetExtensionValue(YouTubeNameTable.VideoID, YouTubeNameTable.NSYouTube);
            }
            set
            {
                SetExtensionValue(YouTubeNameTable.VideoID, YouTubeNameTable.NSYouTube, value);
            }
        }

        /// <summary>
        ///  property accessor for the Uploaded element, if applicable
        /// returns the date the video was uplaoded
        /// </summary>
        public string Uploaded
        {
            get
            {
                return GetExtensionValue(YouTubeNameTable.Uploaded, YouTubeNameTable.NSYouTube);
            }
            set
            {
                SetExtensionValue(YouTubeNameTable.Uploaded, YouTubeNameTable.NSYouTube, value);
            }
        }

      

        //////////////////////////////////////////////////////////////////////
        /// <summary>accessor for the related videos feed URI</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public AtomUri RelatedVideosUri
        {
            get
            {
                AtomLink link = this.Links.FindService(YouTubeNameTable.RelatedVideo, AtomLink.ATOM_TYPE);
                // scan the link collection
                return link == null ? null : link.HRef;
            }
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>accessor for the video responses feed URI</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public AtomUri VideoResponsesUri
        {
            get
            {
                AtomLink link = this.Links.FindService(YouTubeNameTable.ResponseVideo, AtomLink.ATOM_TYPE);
                // scan the link collection
                return link == null ? null : link.HRef;
            }
        }


        /// <summary>
        /// boolean property shortcut to set the mediagroup/yt:private element. Setting this to true 
        /// adds the element, if not already there (otherwise nothing happens)
        /// setting this to false, removes it
        /// it returns if the mediagroup:yt:private element exists, or not
        /// </summary>
        /// <returns></returns>
        public bool Private
        {
            get
            {
                Private p = null;
                if (this.Media != null)
                {
                    p =  Media.FindExtension(YouTubeNameTable.Private,
                                    YouTubeNameTable.NSYouTube) as Private;
                }
               return p != null;
            }

            set
            {
                Private p = null;
                if (this.Media != null)
                {
                    p =  Media.FindExtension(YouTubeNameTable.Private,
                                    YouTubeNameTable.NSYouTube) as Private;
                }
                if (value == true)
                {
                    if (p == null)
                    {
                        if (this.Media == null)
                        {
                            this.Media = new MediaGroup();
                        }
                        this.Media.ExtensionElements.Add(new Private());
                    }
                }
                else 
                {
                    if (p != null)
                    {
                        this.Media.ReplaceExtension(p.XmlName, p.XmlNameSpace, null);
                    }
                }
            }
        }
    }
}


