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

namespace Google.GData.Health {



    //////////////////////////////////////////////////////////////////////
    /// <summary>
    /// <para>Google Health supports the following subset of Google Data API query parameters:</para>
    /// <list type="bullet">
    /// <item><description>q (only supported for the register feed)</description></item>
    /// <item><description>start-index</description></item>
    /// <item><description>max-results</description></item>
    /// <item><description>published-min</description></item>
    /// <item><description>published-max</description></item>
    /// <item><description>updated-min</description></item>
    /// <item><description>updated-max</description></item>
    /// </list>
    /// <para>
    /// Google Health also supports the following additional parameters:
    /// </para>
    ///  <list type="bullet">
    /// <item><description>/category - Category Filter</description>
    /// <para>
    /// Refer to the Google Data documentation for usage information and category queries 
    /// for specifics about the Google Health implementation of this parameter.
    /// <exampleFor example: -/medication/{http://schemas.google.com/health/item}Lipitor max-results=10 will return the first 10 entries for the medication Lipitor
    /// </para>
    /// </item>
    /// <item><description>digest - May only be used on the profile feed. Returns content as an aggregation of all entries into a single CCR entry, 
    /// which contains the collection of enclosed entries. </description></item>
    /// <item><description>grouped - Returns a count of results per group. </description></item>
    /// <item><description>max-results-group   Specifies the maximum number of groups to be retrieved.</description></item>
    /// <item><description>max-results-in-group    Specifies the maximum number of records to be retrieved from each group.</description></item>
    /// <item><description>start-index-group   Retrieves only items whose group ranking is at least start-index-group. </description></item>
    /// <item><description>tart-index-in-group     Is a 1-based index of the records to be retrieved from each group</description></item>
    /// </list>
    /// </summary>
    //////////////////////////////////////////////////////////////////////
    public class HealthQuery : FeedQuery
    {

        /// <summary>
        /// starting url template for health feeds
        /// </summary>
        public const string HealthFeeds = "https://www.google.com/health/feeds/";

        /// <summary>
        /// the register feed used with AuthSub authentication
        /// </summary>
        public const string AuthSubRegisterFeed = HealthFeeds + "register/default";
        /// <summary>
        /// the register feed used with AuthSub authentication
        /// </summary>
        public const string AuthSubProfileFeed = HealthFeeds + "profile/default";

        /// <summary>
        /// the register feed used with client login authentication, note that this
        /// requires the profile id to be appended
        /// </summary>
        public const string RegisterFeed = HealthFeeds + "register/ui/";
        /// <summary>
        /// the register feed used with client login authentication, note that this
        /// requires the profile id to be appended
        /// </summary>
        public const string ProfileFeed = HealthFeeds + "profile/ui/";

        /// <summary>
        /// feed url to retrieve a list of profiles, this is a basic atom feed
        /// </summary>
        /// <returns></returns>
        public const string ProfileListFeed = HealthFeeds + "profile/list";


        ///<summary>
        /// base constructor
        /// </summary>
        public HealthQuery()
        : base()
        {
        }

        /// <summary>
        /// base constructor, with initial queryUri
        /// </summary>
        /// <param name="queryUri">the query to use</param>
        public HealthQuery(string queryUri)
        : base(queryUri)
        {
        }

        /// <summary>
        /// returns a HealthQuery for the Register UI feed for a given profileID
        /// </summary>
        /// <param name="profileId"></param>
        /// <returns></returns>
        public static HealthQuery RegisterQueryForId(string profileId)
        {
            return new HealthQuery(HealthQuery.RegisterFeed + profileId);
        }

        /// <summary>
        /// returns a HealthQuery for the Profile UI feed for a given profileID
        /// </summary>
        /// <param name="profileId"></param>
        /// <returns></returns>
        public static HealthQuery ProfileQueryForId(string profileId)
        {
            return new HealthQuery(HealthQuery.ProfileFeed + profileId);
        }

        private bool digest;

        //////////////////////////////////////////////////////////////////////
        /// <summary>May only be used on the profile feed.
        /// Returns content as an aggregation of all entries into a single CCR entry, 
        /// which contains the collection of enclosed entries. Default is false. 
        /// For example: https://www.google.com/health/feeds/profile/default?digest=true will 
        /// return a digest version of the profile feed.
        /// </summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public bool Digest
        {
            get {return this.digest;}
            set {this.digest = value;}
        }


        private bool grouped;

        //////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Returns a count of results per group.
        /// The default value is grouped=false. You may set this to true or false.
        /// <para>
        /// For example: ?grouped=true&amp;max-results-in-group=50 will return the first 50 records for each group.
        /// </para>
        /// ?grouped=true&amp;max-results-in-group=1 will return the top record for each 
        /// group (which, incidentally, would be the profile summary query.)
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public bool Grouped
        {
            get {return this.grouped;}
            set {this.grouped = value;}
        }

        private int numberOfGroups;

        //////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Specifies the maximum number of groups to be retrieved.
        /// Must be an integer value greater than zero.
        /// This parameter is only valid if Grouped is true
        /// <para>
        /// For example: 
        ///     /feeds/profile/default/-medications?grouped=true&amp;max-results-in-group=2&amp;max-results-group=10 will return the top 10 medications with up to 2 items each.
        ///     /feeds/profile/default/-medications?grouped=true&amp;max-results-in-group=2&amp;max-results-group=10&amp;max-results=3 will 
        ///         return the top 10 medications with up to 2 items each, but no more than 3 total results.
        /// </para>
        /// </summary> 
        /// 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public int NumberOfGroups
        {
            get {return this.numberOfGroups;}
            set {this.numberOfGroups = value;}
        }

        private int groupSize;

        //////////////////////////////////////////////////////////////////////
        /// <summary>Specifies the maximum number of records to be retrieved from each group.
        /// The limits that you specify with this parameter apply to all groups.
        /// Must be an integer value greater than zero.
        /// This parameter is only valid if grouped=true
        /// 
        /// <para>
        /// For example:
        /// </para>
        /// <para>
        ///     /feeds/profile/default/-medications?grouped=true&amp;max-results-in-group=2&amp;max-results-group=10 will return the top 10 medications with up to 2 items each.
        /// </para>
        /// <para>
        ///      /feeds/profile/default/-medications?grouped=true&amp;max-results-in-group=2&amp;max-results-group=10&amp;max-results=3 will return the top 10 medications with up to 2 items each, but no more than 3 total results.
        /// </para>
        /// </summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public int GroupSize
        {
            get {return this.groupSize;}
            set {this.groupSize = value;}
        }

        private int groupStart;
        //////////////////////////////////////////////////////////////////////
        /// <summary>Retrieves only items whose group ranking is at least start-index-group. 
        /// This should be set to a 1-based index of the first group to be retrieved. 
        /// The range is applied per category. This parameter is only valid if 
        /// grouped=true</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public int GroupStart
        {
            get {return this.groupStart;}
            set {this.groupStart = value;}
        }
        // end of accessor public int GroupStart

        private int startInGroup;

        //////////////////////////////////////////////////////////////////////
        /// <summary>Is a 1-based index of the records to be retrieved from each group
        /// This parameter is only valid if grouped=true
        /// </summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public int StartInGroup
        {
            get {return this.startInGroup;}
            set {this.startInGroup = value;}
        }
        // end of accessor public int StartInGroup

        //////////////////////////////////////////////////////////////////////
        /// <summary>Creates the partial URI query string based on all
        ///  set properties.</summary> 
        /// <returns> string => the query part of the URI </returns>
        //////////////////////////////////////////////////////////////////////
        protected override string CalculateQuery(string basePath)
        {
            string path = base.CalculateQuery(basePath);
            StringBuilder newPath = new StringBuilder(path, 2048);
            char paramInsertion = InsertionParameter(path); 

            if (this.Digest)
            {
                newPath.Append(paramInsertion + "digest=true");
                paramInsertion = '&';
            }

            if (this.Grouped)
            {
                newPath.Append(paramInsertion + "grouped=true");
                paramInsertion = '&';

                if (this.NumberOfGroups > 0)
                {
                    newPath.Append(paramInsertion);
                    newPath.AppendFormat(CultureInfo.InvariantCulture, "max-results-group={0}", this.NumberOfGroups.ToString(CultureInfo.InvariantCulture));
                    paramInsertion = '&';
                }

                if (this.GroupSize > 0)
                {
                    newPath.Append(paramInsertion);
                    newPath.AppendFormat(CultureInfo.InvariantCulture, "max-results-in-group={0}", this.GroupSize.ToString(CultureInfo.InvariantCulture));
                    paramInsertion = '&';
                }
                if (this.GroupStart > 0)
                {
                    newPath.Append(paramInsertion);
                    newPath.AppendFormat(CultureInfo.InvariantCulture, "start-index-group={0}", this.GroupStart.ToString(CultureInfo.InvariantCulture));
                    paramInsertion = '&';
                }
                if (this.StartInGroup > 0)
                {
                    newPath.Append(paramInsertion);
                    newPath.AppendFormat(CultureInfo.InvariantCulture, "start-index-in-group={0}", this.StartInGroup.ToString(CultureInfo.InvariantCulture));
                    paramInsertion = '&';
                }
            }
            return newPath.ToString();
        }
    }
}
