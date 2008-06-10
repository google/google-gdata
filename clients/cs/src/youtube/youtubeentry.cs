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
    /// Entry API customization class for defining entries in an Event feed.
    /// </summary>
    //////////////////////////////////////////////////////////////////////
    public class YouTubeEntry : AbstractEntry
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

            // now add it to us
            this.AddExtension(mg);

            GeoRssExtensions.AddExtension(this);


            // create a default appControl element
            AppControl ac = new AppControl();
            // add the youtube state element
            ac.ExtensionFactories.Add(new State());
            this.AddExtension(ac);

            // things from the gd namespce
            this.AddExtension(new Comments());
            this.AddExtension(new Rating());

            // add youtube namespace elements
            this.AddExtension(new Description());  // only playlist entry
            this.AddExtension(new Position());  // only playlist entry
            this.AddExtension(new Statistics());
            this.AddExtension(new Location());
            this.AddExtension(new Recorded());

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
       /// returns the app:control element
       /// </summary>
       /// <returns></returns>
       public AppControl AppControl
       {
           get
           {
               return FindExtension(BaseNameTable.XmlElementPubControl,
                                    BaseNameTable.NSAppPublishing) as AppControl;
           }
           set
           {
               ReplaceExtension(BaseNameTable.XmlElementPubControl,
                                    BaseNameTable.NSAppPublishing,
                               value);
           }
       }

       //////////////////////////////////////////////////////////////////////
        /// <summary>specifies if app:control/app:draft is yes or no. 
        /// this is determined by walking the extension elements collection</summary> 
        /// <returns>true if this is a draft element</returns>
        //////////////////////////////////////////////////////////////////////
        public override bool IsDraft
        {
            get {
                if (this.AppControl != null && this.AppControl.Draft != null)
                {
                    return this.AppControl.Draft.BooleanValue;
                }
                return false; 
            }

            set {
                this.Dirty = true; 
                if (this.AppControl == null)
                {
                    this.AppControl = new AppControl();
                }
                if (this.AppControl.Draft == null)
                {
                    this.AppControl.Draft = new AppDraft();
                }
                this.AppControl.Draft.BooleanValue = value;
            }
        }
        // end of accessor public bool IsDraft

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
        /// instead of having 20 extension elements
        /// we have one string based getter
        /// usage is: entry.getPhotoExtension("albumid") to get the element
        /// </summary>
        /// <param name="extension">the name of the extension to look for</param>
        /// <returns>SimpleElement, or NULL if the extension was not found</returns>
        public SimpleElement getYouTubeExtension(string extension) 
        {
            return FindExtension(extension, YouTubeNameTable.NSYouTube) as SimpleElement;
        }

        /// <summary>
        /// instead of having 20 extension elements
        /// we have one string based getter
        /// usage is: entry.getPhotoExtensionValue("albumid") to get the elements value
        /// </summary>
        /// <param name="extension">the name of the extension to look for</param>
        /// <returns>value as string, or NULL if the extension was not found</returns>
        public string getYouTubeExtensionValue(string extension) 
        {
            SimpleElement e = getYouTubeExtension(extension);
            if (e != null)
            {
                return e.Value;
            }
            return null;
        }




        /// <summary>
        /// instead of having 20 extension elements
        /// we have one string based setter
        /// usage is: entry.setYouTubeExtension("albumid") to set the element
        /// this will create the extension if it's not there
        /// note, you can ofcourse, just get an existing one and work with that 
        /// object: 
        ///     SimpleElement e = entry.getPhotoExtension("albumid");
        ///     e.Value = "new value";  
        /// 
        /// or 
        ///    entry.setPhotoExtension("albumid", "new Value");
        /// </summary>
        /// <param name="extension">the name of the extension to look for</param>
        /// <param name="newValue">the new value for this extension element</param>
        /// <returns>SimpleElement, either a brand new one, or the one
        /// returned by the service</returns>
        public SimpleElement setYouTubeExtension(string extension, string newValue) 
        {
            if (extension == null)
            {
                throw new System.ArgumentNullException("extension");
            }
            
            SimpleElement ele = getYouTubeExtension(extension);
            if (ele == null)
            {
                ele = CreateExtension(extension, YouTubeNameTable.NSYouTube) as SimpleElement;
                if (ele != null)
                {
                    this.ExtensionElements.Add(ele);
                }
            }
            if (ele != null)
                ele.Value = newValue;

            return ele;
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
    }
}


