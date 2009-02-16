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
using System.Collections.Generic;
using Google.GData.Client;
using Google.GData.Extensions;
using Google.GData.AccessControl;


namespace Google.GData.Documents {


    //////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Entry API customization class for defining entries in an Event feed.
    /// </summary>
    //////////////////////////////////////////////////////////////////////
    public class DocumentEntry : AbstractEntry
    {

        static string PRESENTATION_KIND = "http://schemas.google.com/docs/2007#presentation";
        static string DOCUMENT_KIND = "http://schemas.google.com/docs/2007#document";
        static string SPREADSHEET_KIND = "http://schemas.google.com/docs/2007#spreadsheet";
        static string PDF_KIND = "http://schemas.google.com/docs/2007#pdf";
        static string STARRED_KIND = "http://schemas.google.com/g/2005/labels#starred";
        static string FOLDER_KIND = "http://schemas.google.com/docs/2007#folder";
        static string PARENT_FOLDER_REL = "http://schemas.google.com/docs/2007#parent";

        /// <summary>
        /// a predefined atom category for Documents
        /// </summary>
        public static AtomCategory DOCUMENT_CATEGORY =
            new AtomCategory(DOCUMENT_KIND, new AtomUri(BaseNameTable.gKind));
        /// <summary>
        /// a predefined atom category for Spreadsheets
        /// </summary>
        public static AtomCategory SPREADSHEET_CATEGORY =
            new AtomCategory(SPREADSHEET_KIND, new AtomUri(BaseNameTable.gKind));
        /// <summary>
        /// a predefined atom category for PDF
        /// </summary>
        public static AtomCategory PDF_CATEGORY =
            new AtomCategory(PDF_KIND, new AtomUri(BaseNameTable.gKind));
        /// <summary>
        /// a predefined atom category for starred documentss
        /// </summary>
        /// <summary>
        /// a predefined atom category for Presentations
        /// </summary>
        public static AtomCategory PRESENTATION_CATEGORY =
            new AtomCategory(PRESENTATION_KIND, new AtomUri(BaseNameTable.gKind));
        /// <summary>
        /// a predefined atom category for starred documentss
        /// </summary>        
        public static AtomCategory STARRED_CATEGORY =
            new AtomCategory(STARRED_KIND, new AtomUri(BaseNameTable.gLabels));
        /// <summary>
        /// a predefined atom category for folders
        /// </summary>        
        public static AtomCategory  FOLDER_CATEGORY =
            new AtomCategory(FOLDER_KIND, new AtomUri(BaseNameTable.gKind));



        /// <summary>
        /// Constructs a new EventEntry instance with the appropriate category
        /// to indicate that it is an event.
        /// </summary>
        public DocumentEntry()
        : base()
        {
            Tracing.TraceMsg("Created DocumentEntry");
            this.AddExtension(new FeedLink());
        }

        /// <summary>
        /// Reflects if this entry is a word processor document
        /// </summary>
        public bool IsDocument
        {
            get 
            {
                return this.Categories.Contains(DocumentEntry.DOCUMENT_CATEGORY);
            }
            set 
            {
                this.ToggleCategory(DocumentEntry.DOCUMENT_CATEGORY, value);
            }
        }

         /// <summary>
        /// Reflects if this entry is a spreadsheet document
        /// </summary>
        public bool IsSpreadsheet
        {
            get 
            {
                return this.Categories.Contains(DocumentEntry.SPREADSHEET_CATEGORY);
            }
            set 
            {
                this.ToggleCategory(DocumentEntry.SPREADSHEET_CATEGORY, value);
            }
        }

        /// <summary>
        /// Reflects if this entry is a presentation document
        /// </summary>
        public bool IsPresentation
        {
            get 
            {
                return this.Categories.Contains(DocumentEntry.PRESENTATION_CATEGORY);
            }
            set 
            {
                this.ToggleCategory(DocumentEntry.PRESENTATION_CATEGORY, value);
            }
        }

        /// <summary>
        /// Reflects if this entry is a PDF document
        /// </summary>
        public bool IsPDF
        {
            get 
            {
                return this.Categories.Contains(DocumentEntry.PDF_CATEGORY);
            }
            set 
            {
                this.ToggleCategory(DocumentEntry.PDF_CATEGORY, value);
            }
        }        
        
        /// <summary>
        /// Reflects if this entry is starred
        /// </summary>
        public bool IsStarred
        {
            get 
            {
                return this.Categories.Contains(DocumentEntry.STARRED_CATEGORY);
            }
            set 
            {
                this.ToggleCategory(DocumentEntry.STARRED_CATEGORY, value);
            }
        }

        /// <summary>
        /// returns true if this is a folder
        /// </summary>
        public bool IsFolder
        {
            get
            {
                return this.Categories.Contains(DocumentEntry.FOLDER_CATEGORY);
            }
            set 
            {
                this.ToggleCategory(DocumentEntry.FOLDER_CATEGORY, value);
            }
        }

        /// <summary>
        /// returns the string that should represent the Uri to the access control list
        /// </summary>
        /// <returns>the value of the hret attribute for the acl feedlink, or null if not found</returns>
        public string AccessControlList
        {
            get
            {
                List<FeedLink> list = FindExtensions<FeedLink>(GDataParserNameTable.XmlFeedLinkElement, 
                             BaseNameTable.gNamespace);

                foreach (FeedLink fl in list) 
                {
                    if (fl.Rel == AclNameTable.LINK_REL_ACCESS_CONTROL_LIST)
                    {
                        return fl.Href;
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// returns the href value of the parent link releationship
        /// </summary>
        /// <returns></returns>
        public List<AtomLink> ParentFolders
        {
            get
            {
                 List<AtomLink> links = this.Links.FindServiceList(PARENT_FOLDER_REL, AtomLink.ATOM_TYPE);
                 return links; 
            }
        }

    }
}

