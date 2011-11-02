/* Copyright (c) 2007-2008 Google Inc.
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
/* Change history
* Oct 13 2008  Joe Feser       joseph.feser@gmail.com
* Converted ArrayLists and other .NET 1.1 collections to use Generics
* Combined IExtensionElement and IExtensionElementFactory interfaces
* 
*/
using System;
using System.Text;
using System.Xml;
using System.IO;
using System.Collections;
using Google.GData.Client;
using Google.GData.Extensions.Apps;

namespace Google.GData.Apps {
    /// <summary>
    /// A subclass of FeedQuery to query a Google Apps nickname
    /// feed URI.
    /// 
    /// Provides public properties that describe the different
    /// aspects of the URI, as well as a composite URI.
    /// </summary>
    public class NicknameQuery : FeedQuery {
        private const string feedUriExtension = "/nickname/2.0";

        private string domain;
        private string nickname;
        private string startNickname;
        private string userName;
        private bool retrieveAllNicknames;

        /// <summary>
        /// Constructs a new NicknameQuery to retrieve all nicknames
        /// in the specified domain.
        /// 
        /// In addition to calling the constructor, you may set at most
        /// one of Nickname, StartNickname or UserName to restrict your
        /// query.
        /// </summary>
        /// <param name="domain">the domain to query</param>
        public NicknameQuery(string domain) {
            this.domain = domain;
            this.nickname = null;
            this.startNickname = null;
            this.userName = null;
            this.retrieveAllNicknames = true;
        }

        /// <summary>
        /// Accessor method for Domain.
        /// </summary>
        public string Domain {
            get { return domain; }
            set { domain = value; }
        }

        /// <summary>
        /// Accessor method for StartNickname.  If this property
        /// is non-null, the query will return a feed of up to
        /// 100 nicknames beginning with StartNickname.
        /// </summary>
        public string StartNickname {
            get { return startNickname; }
            set {
                if (!string.IsNullOrEmpty(nickname) || !string.IsNullOrEmpty(userName)) {
                    throw new GDataRequestException("At most one of Nickname, StartNickname and UserName may be set.");
                } else {
                    startNickname = value;
                }
            }
        }

        /// <summary>
        /// Accessor method for Nickname.  If this property is
        /// non-null, the query will retrieve the specified
        /// nickname.
        /// </summary>
        public string Nickname {
            get { return nickname; }
            set {
                if (!string.IsNullOrEmpty(startNickname) || !string.IsNullOrEmpty(userName)) {
                    throw new GDataRequestException("At most one of Nickname, StartNickname and UserName may be set.");
                } else {
                    nickname = value;
                }
            }
        }

        /// <summary>
        /// Accessor method for UserName.  If this property is
        /// non-null, the query will retrieve all nicknames for
        /// the specified user.
        /// </summary>
        public string UserName {
            get { return userName; }
            set {
                if (!string.IsNullOrEmpty(startNickname) || !string.IsNullOrEmpty(nickname)) {
                    throw new GDataRequestException("At most one of Nickname, StartNickname and UserName may be set.");
                } else {
                    userName = value;
                }
            }
        }

        /// <summary>
        /// Accessor method for RetrieveAllNicknames.  If false,
        /// the query returns at most 100 matches; if it is
        /// true (default), all matches are returned.
        /// </summary>
        public bool RetrieveAllNicknames {
            get { return retrieveAllNicknames; }
            set { retrieveAllNicknames = value; }
        }

        /// <summary>
        /// Creates the URI query string based on all set properties.
        /// </summary>
        /// <returns>the URI query string</returns>
        protected override string CalculateQuery(string basePath) {
            StringBuilder path = new StringBuilder(AppsNameTable.appsBaseFeedUri, 2048);

            path.Append(domain);
            path.Append(feedUriExtension);

            if (UserName != null) {
                path.Append("?username=");
                path.Append(UserName);
            } else if (Nickname != null) {
                path.Append("/");
                path.Append(Nickname);
            } else if (StartNickname != null) {
                path.Append("?startNickname=");
                path.Append(StartNickname);
            }

            return path.ToString();
        }
    }
}
