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

        public static AtomCategory DOCUMENT_CATEGORY =
            new AtomCategory(DOCUMENT_KIND, new AtomUri(BaseNameTable.gKind));
        public static AtomCategory SPREADSHEET_CATEGORY =
            new AtomCategory(SPREADSHEET_KIND, new AtomUri(BaseNameTable.gKind));
        public static AtomCategory PRESENTATION_CATEGORY =
            new AtomCategory(PRESENTATION_KIND, new AtomUri(BaseNameTable.gKind));


        /// <summary>
        /// Constructs a new EventEntry instance with the appropriate category
        /// to indicate that it is an event.
        /// </summary>
        public DocumentEntry()
        : base()
        {
            Tracing.TraceMsg("Created DocumentEntry");
        }

        /// <summary>
        /// returns TRUE if this entry is a Document entry
        /// </summary>
        public bool IsDocument
        {
            get 
            {
                return this.Categories.Contains(DocumentEntry.DOCUMENT_CATEGORY);
            }
            set 
            {
                if (value == true)
                {
                    if (this.IsDocument == false)
                    {
                        this.Categories.Add(DocumentEntry.DOCUMENT_CATEGORY);
                    }
                }
            }
        }

         /// <summary>
        /// returns TRUE if this entry is a Document entry
        /// </summary>
        public bool IsSpreadsheet
        {
            get 
            {
                return this.Categories.Contains(DocumentEntry.SPREADSHEET_CATEGORY);
            }
            set 
            {
                if (value == true)
                {
                    if (this.IsSpreadsheet == false)
                    {
                        this.Categories.Add(DocumentEntry.SPREADSHEET_CATEGORY);
                    }
                }
            }
        }

         /// <summary>
        /// returns TRUE if this entry is a Document entry
        /// </summary>
        public bool IsPresentation
        {
            get 
            {
                return this.Categories.Contains(DocumentEntry.PRESENTATION_CATEGORY);
            }
            set 
            {
                if (value == true)
                {
                    if (this.IsPresentation == false)
                    {
                        this.Categories.Add(DocumentEntry.PRESENTATION_CATEGORY);
                    }
                }
            }
        }
    }
}

