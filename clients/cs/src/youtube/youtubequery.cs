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
using System.Collections.Generic;
using Google.GData.Client;

namespace Google.GData.YouTube {



    //////////////////////////////////////////////////////////////////////
    /// <summary>
    /// A subclass of FeedQuery, to create an YouTube query URI.
    /// The YouTube Data API supports the following standard Google Data query parameters.
    /// Name	Definition
    /// alt	        The alt parameter specifies the format of the feed to be returned. 
    ///             Valid values for this parameter are atom, rss and json. The default 
    ///             value is atom and this document only explains the format of Atom responses.
    /// author	    The author parameter restricts the search to videos uploaded by a 
    ///             particular YouTube user. The Videos uploaded by a specific user 
    ///             section discusses this parameter in more detail.
    /// max-results	The max-results parameter specifies the maximum number of results 
    ///             that should be included in the result set. This parameter works 
    ///             in conjunction with the start-index parameter to determine which 
    ///             results to return. For example, to request the second set of 10 
    ///             results Ð i.e. results 11-20 Ð set the max-results parameter to 10 
    ///             and the start-index parameter to 11. The default value of this 
    ///             parameter for all Google Data APIs is 25, and the maximum value is 50. 
    ///             However, for displaying lists of videos, we recommend that you 
    ///             set the max-results parameter to 10.
    /// start-index	The start-index parameter specifies the index of the first matching result 
    ///             that should be included in the result set. This parameter uses a one-based 
    ///             index, meaning the first result is 1, the second result is 2 and so forth. 
    ///             This parameter works in conjunction with the max-results parameter to determine
    ///             which results to return. For example, to request the second set of 10 
    ///             results Ð i.e. results 11-20 Ð set the start-index parameter to 11 
    ///             and the max-results parameter to 10.
    /// Please see the Google Data APIs Protocol Reference for more information about standard Google 
    /// Data API functionality or about these specific parameters.
    /// Custom parameters for the YouTube Data API
    /// In addition to the standard Google Data query parameters, the YouTube Data API defines 
    /// the following API-specific query parameters. These parameters are only available on video
    /// and playlist feeds.
    /// Name	    Definition
    /// vq	        The vq parameter specifies a search query term. YouTube will search all video 
    ///             metadata for videos matching the term. Video metadata includes titles, keywords, 
    ///             descriptions, authors' usernames, and categories.
    ///             Note that any spaces, quotes or other punctuation in the parameter value must be 
    ///             URL-escaped. To search for an exact phrase, enclose the phrase in quotation marks. 
    ///             For example, to search for videos matching the phrase "spy plane", set the 
    ///             vq parameter to %22spy+plane%22.
    ///             Your request can also use the Boolean NOT (-) and OR (|) operators to exclude 
    ///             videos or to find videos that are associated with one of several search terms. 
    ///             For example, to search for videos matching either "boating" or "sailing", 
    ///             set the vq parameter to boating%7Csailing. (Note that the pipe character must 
    ///             be URL-escaped.) 
    /// orderby	    The orderby parameter specifies the value that will be used to sort videos in the
    ///             search result set. Valid values for this parameter are relevance, published, viewCount 
    ///             and rating. In addition, you can request results that are most relevant to a specific 
    ///             language by setting the parameter value to relevance_lang_languageCode, where 
    ///             languageCode is an ISO 639-1 two-letter language code. (Use the values zh-Hans for 
    ///             simplified Chinese and zh-Hant for traditional Chinese.) In addition, please note that 
    ///             results in other languages will still be returned if they are highly relevant to the 
    ///             search query term.
    ///             The default value for this parameter is relevance for a search results feed. For a
    ///             playlist feed, the default ordering is based on the position of each video in the playlist. 
    ///             For a user's playlists or subscriptions feed, the default ordering is arbitrary.
    /// client	    The client parameter is an alphanumeric string that identifies your application. The 
    ///             client parameter is an alternate way of specifying your client ID. You can also use the 
    ///             X-GData-Client request header to specify your client ID. Your application does not need to
    ///             specify your client ID twice by using both the client parameter and the X-GData-Client 
    ///             request header, but it should provide your client ID using at least one of those two methods.
    /// format	    The format parameter specifies that videos must be available in a particular video format. 
    ///             Your request can specify any of the following formats:
    ///     Value	    Video Format
    ///         1	    RTSP streaming URL for mobile video playback. H.263 video (up to 176x144) and AMR audio.
    ///         5	    HTTP URL to the embeddable player (SWF) for this video. This format is not available for a
    ///                 video that is not embeddable. Developers commonly add format=5 to their queries to restrict
    ///                 results to videos that can be embedded on their sites.
    ///         6	    RTSP streaming URL for mobile video playback. MPEG-4 SP video (up to 176x144) and AAC audio
    /// lr	    The lr parameter restricts the search to videos that have a title, description or keywords in a
    ///         specific language. Valid values for the lr parameter are ISO 639-1 two-letter language codes. 
    ///         You can also use the values zh-Hans for simplified Chinese and zh-Hant for traditional Chinese. This
    ///         parameter can be used when requesting any video feeds other than standard feeds.
    /// racy	The racy parameter allows a search result set to include restricted content as well as standard 
    ///         content. Valid values for this parameter are include and exclude. By default, restricted content 
    ///         is excluded. Feed entries for videos that contain restricted content will contain an additional 
    ///         yt:racy element.
    /// restriction	The restriction parameter identifies the IP address that should be used to filter videos 
    ///         that can only be played in specific countries. By default, the API filters out videos that cannot 
    ///         be played in the country from which you send API requests. This restriction is based on your 
    ///         client application's IP address.
    ///         To request videos playable from a specific computer, include the restriction parameter 
    ///         in your request and set the parameter value to the IP address of the computer where the videos
    ///         will be played Ð e.g. restriction=255.255.255.255.
    ///         To request videos that are playable in a specific country, include the restriction parameter in your 
    ///         request and set the parameter value to the ISO 3166 two-letter country code of the country where 
    ///         the videos will be played Ð e.g. restriction=DE.
    /// time	The time parameter, which is only available for the top_rated, top_favorites, most_viewed, 
    ///         most_discussed, most_linked and most_responded standard feeds, restricts the search to videos 
    ///         uploaded within the specified time. Valid values for this parameter are today (1 day), 
    ///         this_week (7 days), this_month (1 month) and all_time. The default value for this parameter is all_time.
    /// </summary>
    //////////////////////////////////////////////////////////////////////
    public class YouTubeQuery : FeedQuery
    {


        /// <summary>
        /// describing the requested video format
        /// </summary>
        public enum VideoFormat
        {
            /// <summary>
            /// no parameter. Setting the accessLevel to undefined
            /// implies the server default
            /// </summary>
            FormatUndefined,
            /// <summary>
            /// RTSP streaming URL for mobile video playback. H.263 video (up to 176x144) and AMR audio.
            /// </summary>
            RTSP,
            /// <summary>
            /// HTTP URL to the embeddable player
            /// </summary>
            Embeddable,
            /// <summary>
            /// SRTSP streaming URL for mobile video playback.
            /// </summary>
            Mobile,
        }

        /// <summary>
        /// describing the requested video format
        /// </summary>
        public enum UploadTime
        {
            /// <summary>
            /// time undefined, default value for the server
            /// </summary>
            UploadTimeUndefined,
            /// <summary>
            /// today (1day)
            /// </summary>
            Today,
            /// <summary>
            /// This week (7days)
            /// </summary>
            ThisWeek,
            /// <summary>
            /// 1 month
            /// </summary>
            ThisMonth,
            /// <summary>all time</summary>
            AllTime
        }


        private List<VideoFormat> formats;
        private string videoQuery;
        private string orderBy;
        private string client;
        private string lr;
        private string racy;
        private string restriction;
        private UploadTime uploadTime = UploadTime.UploadTimeUndefined;
        

        /// <summary>
        /// the standard feeds URL
        /// </summary>
        public const string StandardFeeds = "http://gdata.youtube.com/feeds/api/standardfeeds/";
        /// <summary>
        /// youTube base video URI 
        /// </summary>
        public const string DefaultVideoUri = "http://gdata.youtube.com/feeds/api/videos";


        /// <summary>
        /// youTube base mobile video URI 
        /// </summary>
        public const string MobileVideoUri = "http://gdata.youtube.com/feeds/mobile/videos";
       
        /// <summary>
        /// youTube base standard top rated video URI 
        /// </summary>
        public const string TopRatedVideo = YouTubeQuery.StandardFeeds + "top_rated";

        /// <summary>
        /// youTube base standard favorites video URI 
        /// </summary>
        public const string FavoritesVideo = YouTubeQuery.StandardFeeds +"top_favorites";
        
        /// <summary>
        /// youTube base standard most viewed video URI 
        /// </summary>
        public const string MostViewedVideo = YouTubeQuery.StandardFeeds +"most_viewed";

        /// <summary>
        /// youTube base standard most recent video URI 
        /// </summary>
        public const string MostRecentVideo = YouTubeQuery.StandardFeeds +"most_recent";

        /// <summary>
        /// youTube base standard most discussed video URI 
        /// </summary>
        public const string MostDiscussedVideo = YouTubeQuery.StandardFeeds +"most_discussed";

        /// <summary>
        /// youTube base standard most linked video URI 
        /// </summary>
        public const string MostLinkedVideo = YouTubeQuery.StandardFeeds +"most_linked";

        /// <summary>
        /// youTube base standard most responded video URI 
        /// </summary>
        public const string MostRespondedVideo = YouTubeQuery.StandardFeeds +"most_responded";

        /// <summary>
        /// youTube base standard recently featured video URI 
        /// </summary>
        public const string RecentlyFeaturedVideo = YouTubeQuery.StandardFeeds +"recently_featured";

        /// <summary>
        /// youTube base standard mobile phones video URI 
        /// </summary>
        public const string MobilePhonesVideo = YouTubeQuery.StandardFeeds +"watch_on_mobile";
        /// <summary>
        /// base constructor
        /// </summary>
        public YouTubeQuery()
        : base()
        {
        }



        /// <summary>
        /// base constructor, with initial queryUri
        /// </summary>
        /// <param name="queryUri">the query to use</param>
        public YouTubeQuery(string queryUri)
        : base(queryUri)
        {
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>
        /// format	    The format parameter specifies that videos must be available in a particular video format. 
        ///             Your request can specify any of the following formats:
        ///     Value	    Video Format
        ///         1	    RTSP streaming URL for mobile video playback. H.263 video (up to 176x144) and AMR audio.
        ///         5	    HTTP URL to the embeddable player (SWF) for this video. This format is not available for a
        ///                 video that is not embeddable. Developers commonly add format=5 to their queries to restrict
        ///                 results to videos that can be embedded on their sites.
        ///         6	    RTSP streaming URL for mobile video playback. MPEG-4 SP video (up to 176x144) and AAC audio
        /// </summary>
        /// <returns> the list of formats</returns>
        //////////////////////////////////////////////////////////////////////
        public List<VideoFormat> Formats
        {
            get {
                if (this.formats == null)
                {
                    this.formats = new List<VideoFormat>();
                }
                return this.formats;}
        }
        // end of accessor public VideoFormat Format

        //////////////////////////////////////////////////////////////////////
        /// <summary>accessor method public UploadTime Time</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public UploadTime Time
        {
            get {return this.uploadTime;}
            set {this.uploadTime = value;}
        }
        // end of accessor public UploadTime Time

        //////////////////////////////////////////////////////////////////////
        /// <summary>The vq parameter, which is only supported for video feeds, 
        /// specifies a search query term. YouTube will search all video 
        /// metadata for videos matching the term. Video metadata includes
        ///  titles, keywords, descriptions, authors' usernames, and 
        /// categories</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public string VQ
        {
            get {return this.videoQuery;}
            set {this.videoQuery = value;}
        }
        // end of accessor public string VideoQuery

        //////////////////////////////////////////////////////////////////////
        /// <summary>
        /// The orderby parameter, which is only supported for video feeds, 
        /// specifies the value that will be used to sort videos in the search
        ///  result set. Valid values for this parameter are relevance, 
        /// published, viewCount and rating. In addition, you can request
        ///  results that are most relevant to a specific language by
        ///  setting the parameter value to relevance_lang_languageCode, 
        /// where languageCode is an ISO 639-1 two-letter 
        /// language code. (Use the values zh-Hans for simplified Chinese
        ///  and zh-Hant for traditional Chinese.) In addition, 
        /// please note that results in other languages will still be 
        /// returned if they are highly relevant to the search query term.
        /// The default value for this parameter is relevance 
        /// for a search results feed.
        /// accessor method public string OrderBy</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public string OrderBy
        {
            get {return this.orderBy;}
            set {this.orderBy = value;}
        }
        // end of accessor public string OrderBy

        //////////////////////////////////////////////////////////////////////
        /// <summary>
        /// The client parameter is an alphanumeric string that identifies your
        ///  application. The client parameter is an alternate way of specifying 
        /// your client ID. You can also use the X-GData-Client request header to
        ///  specify your client ID. Your application does not need to 
        /// specify your client ID twice by using both the client parameter and 
        /// the X-GData-Client request header, but it should provide your 
        /// client ID using at least one of those two methods.
        /// Note that you should set this normally on the YouTubeService object,
        /// this property is only included for completeness
        /// </summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public string Client
        {
            get {return this.client;}
            set {this.client = value;}
        }
        // end of accessor public string Client

        //////////////////////////////////////////////////////////////////////
        /// <summary>
        /// The lr parameter restricts the search to videos that have a title, 
        /// description or keywords in a specific language. Valid values for 
        /// the lr parameter are ISO 639-1 two-letter language codes. You can
        /// also use the values zh-Hans for simplified Chinese and zh-Hant
        ///  for traditional Chinese. This parameter can be used when requesting 
        /// any video feeds other than standard feeds.
        /// </summary> 
        //////////////////////////////////////////////////////////////////////
        public string LR
        {
            get {return this.lr;}
            set {this.lr = value;}
        }
        // end of accessor public string LR


        //////////////////////////////////////////////////////////////////////
        /// <summary>
        /// The racy parameter allows a search result set to include restricted
        /// content as well as standard content. Valid values for this parameter
        ///  are include and exclude. By default, restricted content is excluded. 
        /// Feed entries for videos that contain restricted content will contain
        /// the media:rating element.
        /// </summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public string Racy
        {
            get {return this.racy;}
            set {this.racy = value;}
        }
        // end of accessor public string Racy

        //////////////////////////////////////////////////////////////////////
        /// <summary>
        /// The restriction parameter identifies the IP address that should be 
        /// used to filter videos that can only be played in specific countries. 
        /// We recommend that you always use this parameter to specify the end 
        /// user's IP address. (By default, the API filters out videos that
        ///  cannot be played in the country from which you send API requests. 
        /// This restriction is based on your client application's IP address.)
        /// To request videos playable from a specific computer, include the 
        /// restriction parameter in your request and set the parameter value 
        /// to the IP address of the computer where the videos will be 
        /// played Ð e.g. restriction=255.255.255.255.
        /// To request videos that are playable in a specific country, 
        /// include the restriction parameter in your request and set 
        /// the parameter value to the ISO 3166 two-letter country code 
        /// of the country where the videos will be played
        ///  Ð e.g. restriction=DE.
        /// </summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public string Restriction
        {
            get {return this.restriction;}
            set {this.restriction = value;}
        }
        // end of accessor public string Restriction
   
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
                char[] deli = { '?', '&' };

                TokenCollection tokens = new TokenCollection(targetUri.Query, deli);
                foreach (string token in tokens)
                {
                    if (token.Length > 0)
                    {
                        char[] otherDeli = { '=' };
                        string[] parameters = token.Split(otherDeli, 2);
                        switch (parameters[0])
                        {
                            case "format":
                                if (parameters[1] != null)
                                {
                                    string [] formats = parameters[1].Split(new char[] {','});
                                    foreach (string f in formats)
                                    {
                                        if (String.Compare(f, "1",  false, CultureInfo.InvariantCulture) == 0)
                                        {
                                            this.Formats.Add(VideoFormat.RTSP);
                                        } 
                                        else if (String.Compare(f, "5",  false, CultureInfo.InvariantCulture) == 0)
                                        {
                                            this.Formats.Add(VideoFormat.Embeddable);
                                        }
                                        else if (String.Compare(f, "6",  false, CultureInfo.InvariantCulture) == 0)
                                        {
                                            this.Formats.Add(VideoFormat.Mobile);
                                        }
                                    }
                                }
                                break;
                            case "vq":
                                this.VQ = parameters[1];
                                break;
                            case "orderby":
                                this.OrderBy = parameters[1];
                                break;
                            case "client":
                                this.Client = parameters[1];
                                break;
                            case "lr":
                                this.LR = parameters[1];
                                break;
                            case "racy":
                                this.Racy = parameters[1];
                                break;
                            case "restriction":
                                this.Restriction = parameters[1];
                                break;
                            case "time":
                                if ("all_time" == parameters[1])
                                {
                                    this.Time = UploadTime.AllTime;
                                } 
                                else if ("this_month" == parameters[1])
                                {
                                    this.Time = UploadTime.ThisMonth;
                                }
                                else if ("today" == parameters[1])
                                {
                                    this.Time = UploadTime.Today;
                                }
                                else if ("this_week" == parameters[1])
                                {
                                    this.Time = UploadTime.ThisWeek;
                                }
                                else 
                                {
                                    this.Time = UploadTime.UploadTimeUndefined;
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

            if (this.formats != null)
            {
                string res = ""; 
                foreach (VideoFormat v in this.formats )
                {
                    switch (v)
                    {
                        case VideoFormat.RTSP:
                            res += res.Length>0 ? ",1"  : "1";
                            break;
                        case VideoFormat.Embeddable:
                            res += res.Length>0 ? ",5"  : "5";
                            break;
                        case VideoFormat.Mobile:
                            res += res.Length>0 ? ",6"  : "6";
                            break;
                    }
                }

                if (res.Length > 0)
                {
                    newPath.Append(paramInsertion);
                    newPath.AppendFormat(CultureInfo.InvariantCulture, "format={0}", Utilities.UriEncodeReserved(res));
                    paramInsertion = '&';
                }
            }

            if (this.Time != UploadTime.UploadTimeUndefined)
            {
                string res = ""; 
                switch (this.Time)
                {
                    case UploadTime.AllTime:
                        res = "all_time";
                        break;
                    case UploadTime.ThisMonth:
                        res = "this_month";
                        break;
                    case UploadTime.ThisWeek:
                        res = "this_week";
                        break;
                    case UploadTime.Today:
                        res = "today";
                        break;
                }
                paramInsertion = AppendQueryPart(res, "time", paramInsertion, newPath);
            }

            paramInsertion = AppendQueryPart(this.VQ, "vq", paramInsertion, newPath);
            paramInsertion = AppendQueryPart(this.OrderBy, "orderby", paramInsertion, newPath);
            paramInsertion = AppendQueryPart(this.Client, "client", paramInsertion, newPath);
            paramInsertion = AppendQueryPart(this.LR, "lr", paramInsertion, newPath);
            paramInsertion = AppendQueryPart(this.Racy, "racy", paramInsertion, newPath);
            paramInsertion = AppendQueryPart(this.Restriction, "restriction", paramInsertion, newPath);

            return newPath.ToString();
        }
    }
}
