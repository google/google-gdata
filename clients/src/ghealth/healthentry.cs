/* Copyright (c) 2006-2008 Google Inc.
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

namespace Google.GData.Health {


    /// <summary>
    ///  contains the static strings for the health namespace
    /// </summary>
    /// <returns></returns>
    public class HealthNameTable 
    {
       /// <summary> Continuity of Care Record namespace (CCR) namespace</summary>
       public const string CCR = "urn:astm-org:CCR";
        
       /// <summary> Continuity of Care Record namespace (CCR) namespace prefix</summary>
       public const string CCR_PREFIX = CCR + "#";
        
       /// <summary> Continuity of Care Record namespace (CCR) namespace alias</summary>
       public const string CCR_ALIAS = "ccr";

       /// <summary>
       /// the CCR element name
       /// </summary>
       public const string ContinuityOfCareElement = "ContinuityOfCareRecord";
        
       /// <summary> The h9 namespace (H9) namespace</summary>
       public const string H9 = "http://schemas.google.com/health/data";
        
       /// <summary> The h9 namespace (H9) namespace prefix</summary>
       public const string H9_PREFIX = H9 + "#";
        
       /// <summary> The h9 namespace (H9) namespace alias</summary>
       public const string H9_ALIAS = "h9";
        
       /// <summary> The h9kinds namespace (H9KINDS) namespace</summary>
       public const string H9KINDS = "http://schemas.google.com/health/kinds";
        
       /// <summary> The h9kinds namespace (H9KINDS) namespace prefix</summary>
       public const string H9KINDS_PREFIX = H9KINDS + "#";
        
       /// <summary>the term for a profile entry</summary>
       public const string H9PROFILE = H9KINDS_PREFIX + "profile";

       /// <summary>the term for a registry entry</summary>
       public const string H9REGISTER = H9KINDS_PREFIX + "register";

       /// <summary> The h9kinds namespace (H9KINDS) namespace alias</summary>
       public const string H9KINDS_ALIAS = "h9kinds";
        
       /// <summary> The h9 metadata namespace (H9M) namespace</summary>
       public const string H9M = "http://schemas.google.com/health/metadata";
        
       /// <summary> The h9 metadata namespace (H9M) namespace prefix</summary>
       public const string H9M_PREFIX = H9M + "#";
        
       /// <summary> The h9 metadata namespace (H9M) namespace alias</summary>
       public const string H9M_ALIAS = "h9m";
        
       /// <summary> Link Rel for a complete url of an entry, indicating the smallest feed
       /// containing the entry.</summary>
       public const string REL_COMPLETE_URL = H9 + "#complete";
    }

    //////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Entry API customization class for defining entries in an Event feed.
    /// </summary>
    //////////////////////////////////////////////////////////////////////
    public class HealthEntry : AbstractEntry
    {

           /// <summary>
        /// Category used to label entries that contain photo extension data.
        /// </summary>
        public static AtomCategory PROFILE_CATEGORY =
        new AtomCategory(HealthNameTable.H9PROFILE, new AtomUri(BaseNameTable.gKind));

        /// <summary>
        /// Category used to label entries that contain photo extension data.
        /// </summary>
        public static AtomCategory REGISTER_CATEGORY =
        new AtomCategory(HealthNameTable.H9REGISTER, new AtomUri(BaseNameTable.gKind));

    
        /// <summary>
        /// Constructs a new HealthEntry instance with the appropriate category
        /// to indicate that it is an Health Entry.
        /// </summary>
        public HealthEntry()
        : base()
        {
            Tracing.TraceMsg("Created HealthEntry");
        }

        /// <summary>
        /// returns true if the entry is a profile entry
        /// </summary>
        public bool IsProfile
        {
            get 
            {
                return (this.Categories.Contains(PROFILE_CATEGORY));
            }
            set 
            {
                ToggleCategory(PROFILE_CATEGORY, value);
            }
        } 
        /// <summary>
        /// returns true if the entry is a register entry
        /// </summary>
        public bool IsRegister
        {
            get 
            {
                return (this.Categories.Contains(REGISTER_CATEGORY));
            }
            set 
            {
                ToggleCategory(REGISTER_CATEGORY, value);
            }
        } 

        /// <summary>
        /// get/sets the CCR node inside the extensions collection. If you want to find specific nodes
        /// inside that CCR node, you need to use xpath with namespaces. That looks like this:
        /// <code>
        /// xmlDocument = new XmlDocument();
        /// xmlDocument.ImportNode(healthEntry.CCR);
        /// ccrNameManager = new XmlNamespaceManager(xmlDocument.NameTable);
        /// ccrNameManager.AddNamespace("ccr", "urn:astm-org:CCR");
        /// xmlNode node = xmlDocument.CCR.SelectSingleNode("//ccr:ProductName/ccr:Text", ccrNameManager);
        /// </code>
        /// </summary>
        /// <returns></returns>
        public XmlNode CCR
        {
            get
            {
                IExtensionElementFactory factory = FindExtension(HealthNameTable.ContinuityOfCareElement, HealthNameTable.CCR);
                if (factory != null)
                {
                    XmlExtension extension = factory as XmlExtension;
                    if (extension != null) 
                    {
                        return extension.Node;
                    }
                    
                }
                return null;
            }

            set 
            {
                ReplaceExtension(HealthNameTable.ContinuityOfCareElement, HealthNameTable.CCR, new XmlExtension(value));
            }
        }
    }

    /// <summary>
    /// shallow subclass to add the appropriate category for construction
    /// </summary>
    /// <returns></returns>
    public class ProfileEntry : HealthEntry
    {
          /// <summary>
        /// Constructs a new ProfileEntry instance with the appropriate category
        /// to indicate that it is an event.
        /// </summary>
        public ProfileEntry()
            : base()
        {
            Tracing.TraceMsg("Created ProfileEntry");
            Categories.Add(PROFILE_CATEGORY);
        }
    }

    /// <summary>
    /// shallow subclass to add the appropriate category for construction
    /// </summary>
    /// <returns></returns>
    public class RegisterEntry : HealthEntry
    {
          /// <summary>
        /// Constructs a new RegisterEntry instance with the appropriate category
        /// to indicate that it is an event.
        /// </summary>
        public RegisterEntry()
            : base()
        {
            Tracing.TraceMsg("Created RegisterEntry");
            Categories.Add(REGISTER_CATEGORY);
        }
    }

}

