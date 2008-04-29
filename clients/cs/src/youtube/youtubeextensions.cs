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
//using System.Collections;
//using System.Text;
using Google.GData.Client;
using Google.GData.Extensions;

namespace Google.GData.YouTube {

    /// <summary>
    /// helper to instantiate all factories defined in here and attach 
    /// them to a base object
    /// </summary> 
    public class YouTubeExtensions 
    {
        /// <summary>
        /// helper to add all picasa photo extensions to the base object
        /// </summary>
        /// <param name="baseObject"></param>
        public static void AddExtension(AtomBase baseObject) 
        {
            baseObject.AddExtension(new Age());
        }
    }

    /// <summary>
    /// short table to hold the namespace and the prefix
    /// </summary>
    public class YouTubeNameTable 
    {
        /// <summary>static string to specify the GeoRSS namespace supported</summary>
        public const string NSYouTube = "http://gdata.youtube.com/schemas/2007"; 
        /// <summary>static string to specify the Google Picasa prefix used</summary>
        public const string ytPrefix = "yt"; 

        /// <summary>
        /// Comment Kind definition
        /// this is the term value for a category
        /// </summary>
//        public const string CommentKind = NSGPhotos + "#comment";
        /// <summary>
        /// age element string
        /// </summary>
        public const string Age = "age";
        /// <summary>
        /// books element string
        /// </summary>
        public const string Books = "books";
        /// <summary>
        /// Company element string
        /// </summary>
        public const string Company = "company";
        /// <summary>
        /// Description element string
        /// </summary>
        public const string Description = "description";
        /// <summary>
        /// Duration element string
        /// </summary>
        public const string Duration = "duration";
        /// <summary>
        /// FirstName element string
        /// </summary>
        public const string FirstName = "firstname";
        /// <summary>
        /// Gender element string
        /// </summary>
        public const string Gender = "gender";
        /// <summary>
        /// Hobbies element string
        /// </summary>
        public const string Hobbies = "hobbies";
        /// <summary>
        /// HomeTown element string
        /// </summary>
        public const string HomeTown = "tometown";
        /// <summary>
        /// LastName element string
        /// </summary>
        public const string LastName = "lastName";
        /// <summary>
        /// Location element string
        /// </summary>
        public const string Location = "location";
        /// <summary>
        /// Movies element string
        /// </summary>
        public const string Movies = "movies";
        /// <summary>
        /// Music element string
        /// </summary>
        public const string Music = "music";
        /// <summary>
        /// NoEmbed element string
        /// </summary>
        public const string NoEmbed = "noembed";
        /// <summary>
        /// Occupation element string
        /// </summary>
        public const string Occupation = "ccupation";
        /// <summary>
        /// Position element string
        /// </summary>
        public const string Position = "position";
        /// <summary>
        /// Private element string
        /// </summary>
        public const string Private = "private";
        /// <summary>
        /// QueryString element string
        /// </summary>
        public const string QueryString = "queryString";
        /// <summary>
        /// Racy element string
        /// </summary>
        public const string Racy = "racy";
        /// <summary>
        /// Recorded element string
        /// </summary>
        public const string Recorded = "recorded";
        /// <summary>
        /// Relationship element string
        /// </summary>
        public const string Relationship = "relationship";
        /// <summary>
        /// School element string
        /// </summary>
        public const string School = "school";
        /// <summary>
        /// State element string
        /// </summary>
        public const string State = "state";
        /// <summary>
        /// Statistics element string
        /// </summary>
        public const string Statistics = "statistics";
        /// <summary>
        /// Status element string
        /// </summary>
        public const string Status = "status";
        /// <summary>
        /// UserName element string
        /// </summary>
        public const string UserName = "username";
    }



    /// <summary>
    /// id schema extension describing an ID.
    /// </summary>
    public class Age : SimpleElement
    {
        /// <summary>
        /// default constructor 
        /// </summary>
        public Age()
        : base(YouTubeNameTable.Age, YouTubeNameTable.ytPrefix, YouTubeNameTable.NSYouTube)
        {}

        /// <summary>
        /// default constructor with an initial value as a string 
        /// </summary>
        public Age(string initValue)
        : base(YouTubeNameTable.Age, YouTubeNameTable.ytPrefix, YouTubeNameTable.NSYouTube, initValue)
        {}
    }

    /// <summary>
    /// Books schema extension describing a YouTube Books
    /// </summary>
    public class Books : SimpleElement
    {
        public Books()
        : base(YouTubeNameTable.Books, YouTubeNameTable.ytPrefix, YouTubeNameTable.NSYouTube)
        {}
        public Books(string initValue)
        : base(YouTubeNameTable.Books, YouTubeNameTable.ytPrefix, YouTubeNameTable.NSYouTube, initValue)
        {}
    }

    /// <summary>
    /// Company schema extension describing a YouTube Company
    /// </summary>
    public class Company : SimpleElement
    {
        public Company()
        : base(YouTubeNameTable.Company, YouTubeNameTable.ytPrefix, YouTubeNameTable.NSYouTube)
        {}
        public Company(string initValue)
        : base(YouTubeNameTable.Company, YouTubeNameTable.ytPrefix, YouTubeNameTable.NSYouTube, initValue)
        {}
    }

    /// <summary>
    /// Description schema extension describing a YouTube Description
    /// </summary>
    public class Description : SimpleElement
    {
        public Description()
        : base(YouTubeNameTable.Description, YouTubeNameTable.ytPrefix, YouTubeNameTable.NSYouTube)
        {}
        public Description(string initValue)
        : base(YouTubeNameTable.Description, YouTubeNameTable.ytPrefix, YouTubeNameTable.NSYouTube, initValue)
        {}
    }

    /// <summary>
    /// Duration schema extension describing a YouTube Duration
    /// </summary>
    public class Duration : SimpleElement
    {
        public Duration()
        : base(YouTubeNameTable.Duration, YouTubeNameTable.ytPrefix, YouTubeNameTable.NSYouTube)
        {
            this.Attributes.Add("seconds", null);
        }
        public Duration(string initValue)
        : base(YouTubeNameTable.Duration, YouTubeNameTable.ytPrefix, YouTubeNameTable.NSYouTube, initValue)
        {
            this.Attributes.Add("seconds", null);
        }
    }

    /// <summary>
    /// FirstName schema extension describing a YouTube FirstName
    /// </summary>
    public class FirstName : SimpleElement
    {
        public FirstName()
        : base(YouTubeNameTable.FirstName, YouTubeNameTable.ytPrefix, YouTubeNameTable.NSYouTube)
        {}
        public FirstName(string initValue)
        : base(YouTubeNameTable.FirstName, YouTubeNameTable.ytPrefix, YouTubeNameTable.NSYouTube, initValue)
        {}
    }

    /// <summary>
    /// Gender schema extension describing a YouTube Gender
    /// </summary>
    public class Gender : SimpleElement
    {
        public Gender()
        : base(YouTubeNameTable.Gender, YouTubeNameTable.ytPrefix, YouTubeNameTable.NSYouTube)
        {}
        public Gender(string initValue)
        : base(YouTubeNameTable.Gender, YouTubeNameTable.ytPrefix, YouTubeNameTable.NSYouTube, initValue)
        {}
    }

    /// <summary>
    /// Hobbies schema extension describing a YouTube Hobbies
    /// </summary>
    public class Hobbies : SimpleElement
    {
        public Hobbies()
        : base(YouTubeNameTable.Hobbies, YouTubeNameTable.ytPrefix, YouTubeNameTable.NSYouTube)
        {}
        public Hobbies(string initValue)
        : base(YouTubeNameTable.Hobbies, YouTubeNameTable.ytPrefix, YouTubeNameTable.NSYouTube, initValue)
        {}
    }

    /// <summary>
    /// HomeTown schema extension describing a YouTube HomeTown
    /// </summary>
    public class HomeTown : SimpleElement
    {
        public HomeTown()
        : base(YouTubeNameTable.HomeTown, YouTubeNameTable.ytPrefix, YouTubeNameTable.NSYouTube)
        {}
        public HomeTown(string initValue)
        : base(YouTubeNameTable.HomeTown, YouTubeNameTable.ytPrefix, YouTubeNameTable.NSYouTube, initValue)
        {}
    }
    

    /// <summary>
    /// LastName schema extension describing a YouTube LastName
    /// </summary>
    public class LastName : SimpleElement
    {
        public LastName()
        : base(YouTubeNameTable.LastName, YouTubeNameTable.ytPrefix, YouTubeNameTable.NSYouTube)
        {}
        public LastName(string initValue)
        : base(YouTubeNameTable.LastName, YouTubeNameTable.ytPrefix, YouTubeNameTable.NSYouTube, initValue)
        {}
    }

    /// <summary>
    /// Location schema extension describing a YouTube Location
    /// </summary>
    public class Location : SimpleElement
    {
        public Location()
        : base(YouTubeNameTable.Location, YouTubeNameTable.ytPrefix, YouTubeNameTable.NSYouTube)
        {}
        public Location(string initValue)
        : base(YouTubeNameTable.Location, YouTubeNameTable.ytPrefix, YouTubeNameTable.NSYouTube, initValue)
        {}
    }

    /// <summary>
    /// Movies schema extension describing a YouTube Movies
    /// </summary>
    public class Movies : SimpleElement
    {
        public Movies()
        : base(YouTubeNameTable.Movies, YouTubeNameTable.ytPrefix, YouTubeNameTable.NSYouTube)
        {}
        public Movies(string initValue)
        : base(YouTubeNameTable.Movies, YouTubeNameTable.ytPrefix, YouTubeNameTable.NSYouTube, initValue)
        {}
    }

    /// <summary>
    /// Music schema extension describing a YouTube Music
    /// </summary>
    public class Music : SimpleElement
    {
        public Music()
        : base(YouTubeNameTable.Music, YouTubeNameTable.ytPrefix, YouTubeNameTable.NSYouTube)
        {}
        public Music(string initValue)
        : base(YouTubeNameTable.Music, YouTubeNameTable.ytPrefix, YouTubeNameTable.NSYouTube, initValue)
        {}
    }

    /// <summary>
    /// NoEmbed schema extension describing a YouTube NoEmbed
    /// </summary>
    public class NoEmbed : SimpleElement
    {
        public NoEmbed()
        : base(YouTubeNameTable.NoEmbed, YouTubeNameTable.ytPrefix, YouTubeNameTable.NSYouTube)
        {}
        public NoEmbed(string initValue)
        : base(YouTubeNameTable.NoEmbed, YouTubeNameTable.ytPrefix, YouTubeNameTable.NSYouTube, initValue)
        {}
    }

    /// <summary>
    /// Occupation schema extension describing a YouTube Occupation
    /// </summary>
    public class Occupation : SimpleElement
    {
        public Occupation()
        : base(YouTubeNameTable.Occupation, YouTubeNameTable.ytPrefix, YouTubeNameTable.NSYouTube)
        {}
        public Occupation(string initValue)
        : base(YouTubeNameTable.Occupation, YouTubeNameTable.ytPrefix, YouTubeNameTable.NSYouTube, initValue)
        {}
    }

    /// <summary>
    /// Position schema extension describing a YouTube Position
    /// </summary>
    public class Position : SimpleElement
    {
        public Position()
        : base(YouTubeNameTable.Position, YouTubeNameTable.ytPrefix, YouTubeNameTable.NSYouTube)
        {}
        public Position(string initValue)
        : base(YouTubeNameTable.Position, YouTubeNameTable.ytPrefix, YouTubeNameTable.NSYouTube, initValue)
        {}
    }

    /// <summary>
    /// Private schema extension describing a YouTube Private
    /// </summary>
    public class Private : SimpleElement
    {
        public Private()
        : base(YouTubeNameTable.Private, YouTubeNameTable.ytPrefix, YouTubeNameTable.NSYouTube)
        {}
        public Private(string initValue)
        : base(YouTubeNameTable.Private, YouTubeNameTable.ytPrefix, YouTubeNameTable.NSYouTube, initValue)
        {}
    }

    /// <summary>
    /// QueryString schema extension describing a YouTube QueryString
    /// </summary>
    public class QueryString : SimpleElement
    {
        public QueryString()
        : base(YouTubeNameTable.QueryString, YouTubeNameTable.ytPrefix, YouTubeNameTable.NSYouTube)
        {}
        public QueryString(string initValue)
        : base(YouTubeNameTable.QueryString, YouTubeNameTable.ytPrefix, YouTubeNameTable.NSYouTube, initValue)
        {}
    }

    /// <summary>
    /// Racy schema extension describing a YouTube Racy
    /// </summary>
    public class Racy : SimpleElement
    {
        public Racy()
        : base(YouTubeNameTable.Racy, YouTubeNameTable.ytPrefix, YouTubeNameTable.NSYouTube)
        {}
        public Racy(string initValue)
        : base(YouTubeNameTable.Racy, YouTubeNameTable.ytPrefix, YouTubeNameTable.NSYouTube, initValue)
        {}
    }

    /// <summary>
    /// Recorded schema extension describing a YouTube Recorded
    /// </summary>
    public class Recorded : SimpleElement
    {
        public Recorded()
        : base(YouTubeNameTable.Recorded, YouTubeNameTable.ytPrefix, YouTubeNameTable.NSYouTube)
        {}
        public Recorded(string initValue)
        : base(YouTubeNameTable.Recorded, YouTubeNameTable.ytPrefix, YouTubeNameTable.NSYouTube, initValue)
        {}
    }

    /// <summary>
    /// Relationship schema extension describing a YouTube Relationship
    /// </summary>
    public class Relationship : SimpleElement
    {
        public Relationship()
        : base(YouTubeNameTable.Relationship, YouTubeNameTable.ytPrefix, YouTubeNameTable.NSYouTube)
        {}
        public Relationship(string initValue)
        : base(YouTubeNameTable.Relationship, YouTubeNameTable.ytPrefix, YouTubeNameTable.NSYouTube, initValue)
        {}
    }

    /// <summary>
    /// Identifies the school that the user attended according to the information 
    /// in the user's public YouTube profile.
    /// </summary>
    public class School : SimpleElement
    {
        public School()
        : base(YouTubeNameTable.School, YouTubeNameTable.ytPrefix, YouTubeNameTable.NSYouTube)
        {}
        public School(string initValue)
        : base(YouTubeNameTable.School, YouTubeNameTable.ytPrefix, YouTubeNameTable.NSYouTube, initValue)
        {}
    }

    /// <summary>
    /// State schema extension describing a YouTube State, child of app:control
    /// The state tag contains information that describes the status of a video. 
    /// For videos that failed to upload or were rejected after the upload 
    /// process, the reasonCode attribute and the tag value provide 
    /// insight into the reason for the upload problem.
    /// </summary>
    public class State : SimpleElement
    {
        public State()
        : base(YouTubeNameTable.State, YouTubeNameTable.ytPrefix, YouTubeNameTable.NSYouTube)
        {

            this.Attributes.Add("name", null);
            this.Attributes.Add("reasonCode", null);
            this.Attributes.Add("helpUrl", null);
        }
        public State(string initValue)
        : base(YouTubeNameTable.State, YouTubeNameTable.ytPrefix, YouTubeNameTable.NSYouTube, initValue)
        {
            this.Attributes.Add("name", null);
            this.Attributes.Add("reasonCode", null);
            this.Attributes.Add("helpUrl", null);
        }
    }

    /// <summary>
    /// The statistics tag provides statistics about a video or a user. 
    /// The statistics tag is not provided in a video entry if the value 
    /// of the viewCount attribute is 0.
    /// </summary>
    public class Statistics : SimpleElement
    {
        public Statistics()
        : base(YouTubeNameTable.Statistics, YouTubeNameTable.ytPrefix, YouTubeNameTable.NSYouTube)
        {
            this.Attributes.Add("viewCount", null);
            this.Attributes.Add("videoWatchCount", null);
            this.Attributes.Add("subscriberCount", null);
            this.Attributes.Add("lastWebAccess", null);
            this.Attributes.Add("favoriteCount", null);
        }
        public Statistics(string initValue)
        : base(YouTubeNameTable.Statistics, YouTubeNameTable.ytPrefix, YouTubeNameTable.NSYouTube, initValue)
        {
            this.Attributes.Add("viewCount", null);
            this.Attributes.Add("videoWatchCount", null);
            this.Attributes.Add("subscriberCount", null);
            this.Attributes.Add("lastWebAccess", null);
            this.Attributes.Add("favoriteCount", null);
        }
    }

    /// <summary>
    /// Status schema extension describing a YouTube Status
    /// </summary>
    public class Status : SimpleElement
    {
        public Status()
        : base(YouTubeNameTable.Status, YouTubeNameTable.ytPrefix, YouTubeNameTable.NSYouTube)
        {}
        public Status(string initValue)
        : base(YouTubeNameTable.Status, YouTubeNameTable.ytPrefix, YouTubeNameTable.NSYouTube, initValue)
        {}
    }

    /// <summary>
    /// UserName schema extension describing a YouTube UserName
    /// </summary>
    public class UserName : SimpleElement
    {
        public UserName()
        : base(YouTubeNameTable.UserName, YouTubeNameTable.ytPrefix, YouTubeNameTable.NSYouTube)
        {}
        public UserName(string initValue)
        : base(YouTubeNameTable.UserName, YouTubeNameTable.ytPrefix, YouTubeNameTable.NSYouTube, initValue)
        {}
    }
    


}
