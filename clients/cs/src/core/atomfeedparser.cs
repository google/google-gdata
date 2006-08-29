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
#region Using directives

// #define USE_TRACING

using System;
using System.Xml;
using System.IO; 
using System.Globalization;

#endregion

//////////////////////////////////////////////////////////////////////
// <summary>Contains AtomFeedParser.</summary> 
//////////////////////////////////////////////////////////////////////
namespace Google.GData.Client
{

    //////////////////////////////////////////////////////////////////////
    /// <summary>AtomFeedParser.
    /// </summary> 
    //////////////////////////////////////////////////////////////////////
    public class AtomFeedParser : BaseFeedParser
    {
        /// <summary>holds the nametable used for parsing, based on XMLNameTable</summary>
        private AtomParserNameTable nameTable; 

        /// <summary>let's remember the feed version for parsing</summary> 
        private string version;

        //////////////////////////////////////////////////////////////////////
        /// <summary>standard empty constructor</summary> 
        //////////////////////////////////////////////////////////////////////
        public AtomFeedParser() : base()
        {
            Tracing.TraceCall("constructing AtomFeedParser");
            this.nameTable = new AtomParserNameTable(); 
            this.nameTable.InitAtomParserNameTable();
        }
        /////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////
        /// <summary>accessor method public string Version</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public string Version
        {
            get {return this.version;}
            set {this.version = value;}
        }
        /////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////
        /// <summary>starts the parsing process</summary> 
        /// <param name="streamInput">input stream to parse </param>
        /// <param name="feed">the basefeed object that should be set</param> 
        //////////////////////////////////////////////////////////////////////
        public override void Parse(Stream streamInput, AtomFeed feed)
        {
            Tracing.TraceCall("feedparser starts parsing");
            try
            {
                XmlReader reader = new DecodingTextReader(streamInput, this.nameTable.Nametable);

                MoveToStartElement(reader);
                ParseFeed(reader, feed);
            }
            catch (Exception e)
            {
                throw new ClientFeedException("Parsing failed", e); 
            }

        }
        /////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////
        /// <summary>reads in the feed properties, updates the feed object, then starts
        /// working on the entries...</summary> 
        /// <param name="reader"> xmlReader positioned at the Feed element</param>
        /// <param name="feed">the basefeed object that should be set</param>
        /// <returns> notifies user using event mechanism</returns>
        //////////////////////////////////////////////////////////////////////
        protected void ParseFeed(XmlReader reader, AtomFeed feed)
        {

            Tracing.Assert(reader != null, "reader should not be null");
            if (reader == null)
            {
                throw new ArgumentNullException("reader"); 
            }
            Tracing.Assert(feed != null, "feed should not be null");
            if (feed == null)
            {
                throw new ArgumentNullException("feed"); 
            }

            Tracing.TraceCall("entering AtomFeed Parser");
            object localname = reader.LocalName;
            Tracing.TraceInfo("localname is: " + reader.LocalName); 
            if (localname.Equals(this.nameTable.Feed))
            {
                Tracing.TraceInfo("Found standard feed document");
                // found the feed......
                // now parse the source base of this element
                ParseSource(reader, feed); 

                // feed parsing complete, send notfication
                this.OnNewAtomEntry(feed);
            }
            else if (localname.Equals(this.nameTable.Entry))
            {
                Tracing.TraceInfo("Found entry document");
                ParseEntry(reader);
            }
            else
            {
                Tracing.TraceInfo("ParseFeed called and nothing was parsed" + localname.ToString()); 
                // throw new ClientFeedException("An invalid Atom Document was passed to the parser. Neither Feed nor Entry started the document"); 

            }
            OnParsingDone(); 
            feed.MarkElementDirty(false);
        }
        /////////////////////////////////////////////////////////////////////////////



        //////////////////////////////////////////////////////////////////////
        /// <summary>parses xml to fill a precreated AtomSource object (might be a feed)</summary> 
        /// <param name="reader">correctly positioned reader</param>
        /// <param name="source">created source object to be filled</param>
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        protected void ParseSource(XmlReader reader, AtomSource source)
        {
            Tracing.Assert(reader != null, "reader should not be null");
            if (reader == null)
            {
                throw new ArgumentNullException("reader"); 
            }
            Tracing.Assert(source != null, "source should not be null");
            if (source == null)
            {
                throw new ArgumentNullException("source"); 
            }
            
            Tracing.TraceCall();
            //
            // atomSource =
            //    element atom:source {
            //       atomCommonAttributes,
            //       (atomAuthor?
            //        & atomCategory*
            //        & atomContributor*
            //        & atomGenerator?
            //        & atomIcon?
            //        & atomId?
            //        & atomLink*
            //        & atomLogo?
            //        & atomRights?
            //        & atomSubtitle?
            //        & atomTitle?
            //        & atomUpdated?
            //        & extensionElement*)
            //  this will also parse the gData extension elements.
            //    }

            int depth = -1; 
            ParseBasicAttributes(reader, source);

            while(NextChildElement(reader, ref depth))
            {
                object localname = reader.LocalName; 
                AtomFeed feed = source as AtomFeed; 
                if (IsCurrentNameSpace(reader, BaseNameTable.NSAtom))
                {
                    if (localname.Equals(this.nameTable.Title))
                    {
                        source.Title = ParseTextConstruct(reader, AtomTextConstructElementType.Title);
                    }
                    else if (localname.Equals(this.nameTable.Updated))
                    {
                        source.Updated = DateTime.Parse(reader.ReadString(), CultureInfo.InvariantCulture);
                    }
                    else if (localname.Equals(this.nameTable.Link))
                    {
                        // create the link
                        source.Links.Add(ParseLink(reader)); 
                    }
                    else if (localname.Equals(this.nameTable.Id))
                    {
                        source.Id = new AtomId();
                        ParseBaseLink(reader, source.Id);
                    }
                    else if (localname.Equals(this.nameTable.Icon))
                    {
                        source.Icon = new AtomIcon();
                        ParseBaseLink(reader, source.Icon);
                    }
                    else if (localname.Equals(this.nameTable.Logo))
                    {
                        source.Logo = new AtomLogo();
                        ParseBaseLink(reader, source.Logo);
                    }
                    
                    else if (localname.Equals(this.nameTable.Author))
                    {
                        source.Authors.Add(ParsePerson(reader, this.nameTable.Author));
                    }
                    else if (localname.Equals(this.nameTable.Contributor))
                    {
                        source.Contributors.Add(ParsePerson(reader, this.nameTable.Contributor));
                    }
                    else if (localname.Equals(this.nameTable.Subtitle))
                    {
                        source.Subtitle = ParseTextConstruct(reader, AtomTextConstructElementType.Subtitle);
                    }
                    else if (localname.Equals(this.nameTable.Rights))
                    {
                        source.Rights = ParseTextConstruct(reader, AtomTextConstructElementType.Rights);
                    }
                    else if (localname.Equals(this.nameTable.Generator))
                    {
                        source.Generator = ParseGenerator(reader); 
                    }
                    else if (localname.Equals(this.nameTable.Category))
                    {
                        // need to make this another colleciton
                        source.Categories.Add(ParseCategory(reader)); 
                    }
                    else if (feed != null && localname.Equals(this.nameTable.Entry))
                    {
                        ParseEntry(reader);
                    }
                    // this will either move the reader to the end of an element, or, 
                    // if at the end, to the start of a new one. 
                    reader.Read();
                }
                else if (feed != null && IsCurrentNameSpace(reader, BaseNameTable.gBatchNamespace))
                {
                    // parse the google batch extensions if they are there
                    ParseBatch(reader, feed); 
                }
                else if (feed != null && IsCurrentNameSpace(reader, BaseNameTable.NSOpenSearchRss))
                {
                    if (localname.Equals(this.nameTable.TotalResults))
                    {
                        feed.TotalResults = int.Parse(reader.ReadString(), CultureInfo.InvariantCulture);
                    }
                    else if (localname.Equals(this.nameTable.StartIndex))
                    {
                        feed.StartIndex = int.Parse(reader.ReadString(), CultureInfo.InvariantCulture);
                    }
                    else if (localname.Equals(this.nameTable.ItemsPerPage))
                    {
                        feed.ItemsPerPage = int.Parse(reader.ReadString(), CultureInfo.InvariantCulture);
                    }
                }
                else 
                {
                    // default extension parsing.
                    ParseExtensionElements(reader, source);
                }
            }
            return;
        }
        /////////////////////////////////////////////////////////////////////////////



        //////////////////////////////////////////////////////////////////////
        /// <summary>checks to see if the passed in namespace is the current one</summary> 
        /// <param name="reader">correctly positioned xmlreader</param>
        /// <param name="namespaceToCompare">the namespace to test for</param> 
        /// <returns> true if this is the one</returns>
        //////////////////////////////////////////////////////////////////////
        static protected bool IsCurrentNameSpace(XmlReader reader, string namespaceToCompare)
        {
            Tracing.Assert(reader != null, "reader should not be null");
            if (reader == null)
            {
                throw new ArgumentNullException("reader"); 
            }
            string curNamespace = reader.NamespaceURI;
            if (curNamespace.Length == 0)
            {
                curNamespace = reader.LookupNamespace(String.Empty);
            }
            return curNamespace == namespaceToCompare; 
        }
        /////////////////////////////////////////////////////////////////////////////




        //////////////////////////////////////////////////////////////////////
        /// <summary>Parses the base attributes and puts the rest in extensions.
        /// This needs to happen AFTER known attributes are parsed.</summary> 
        /// <param name="reader">correctly positioned xmlreader</param>
        /// <param name="baseObject">the base object to set the property on</param>
        /// <returns> true if an unknown attribute was found</returns>
        //////////////////////////////////////////////////////////////////////
        protected bool ParseBaseAttributes(XmlReader reader, AtomBase baseObject)
        {
            Tracing.Assert(reader != null, "reader should not be null");
            if (reader == null)
            {
                throw new ArgumentNullException("reader"); 
            }
            Tracing.Assert(baseObject != null, "baseObject should not be null");
            if (baseObject == null)
            {
                throw new ArgumentNullException("baseObject"); 
            }
            bool fRet = false;

            fRet = true;
            object localName = reader.LocalName;
            Tracing.TraceCall(); 

            if (IsCurrentNameSpace(reader, BaseNameTable.NSXml))
            {
                if (localName.Equals(this.nameTable.Base))
                {
                    baseObject.Base = new AtomUri(reader.Value);
                    fRet = false;
                }
                else if (localName.Equals(this.nameTable.Language))
                {
                    baseObject.Language = reader.Value;
                    fRet = false;
                }
            }
            if (fRet==true)
            {
               Tracing.TraceInfo("Found an unknown attribute");
               this.OnNewExtensionElement(reader, baseObject);
            }
            return fRet;
        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>parses extension elements, needs to happen when known attributes are done</summary>
        /// <param name="reader">correctly positioned xmlreader</param>
        /// <param name="baseObject">the base object to set the property on</param>
        //////////////////////////////////////////////////////////////////////
        protected void ParseExtensionElements(XmlReader reader, AtomBase baseObject)
        {
            Tracing.TraceCall();
            if (reader == null)
            {
                throw new System.ArgumentNullException("reader", "No XmlReader supplied");
            }
            if (baseObject == null)
            {
                throw new System.ArgumentNullException("baseObject", "No baseObject supplied");
            }
            if (IsCurrentNameSpace(reader, BaseNameTable.NSAtom))
            {
                Tracing.TraceInfo("Found an unknown ATOM element = this might be a bug, either in the code or the document");
                // maybe we should throw here, but I rather not - makes parsing more flexible
            }
            else
            {
                // everything NOT in Atom, call it
                Tracing.TraceInfo("Found an unknown element");
                this.OnNewExtensionElement(reader, baseObject);
                // position back to the element
                reader.MoveToElement();
            }
        }
        /////////////////////////////////////////////////////////////////////////////



        //////////////////////////////////////////////////////////////////////
        /// <summary>nifty loop to check for base attributes for an object</summary> 
        /// <param name="reader">correctly positioned xmlreader</param>
        /// <param name="baseObject">the base object to set the property on</param>
        //////////////////////////////////////////////////////////////////////
        protected void ParseBasicAttributes(XmlReader reader, AtomBase baseObject)
        {
            Tracing.TraceCall();
            Tracing.Assert(reader != null, "reader should not be null");
            if (reader == null)
            {
                throw new ArgumentNullException("reader"); 
            }
            Tracing.Assert(baseObject != null, "baseObject should not be null");
            if (baseObject == null)
            {
                throw new ArgumentNullException("baseObject"); 
            }
            if (reader.NodeType == XmlNodeType.Element && reader.HasAttributes) 
            {
                while (reader.MoveToNextAttribute())
                {
                    ParseBaseAttributes(reader, baseObject); 
                }
                // position back to the element
                reader.MoveToElement();
            }
        }
        /////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////
        /// <summary>parses a baselink object, like AtomId, AtomLogo, or AtomIcon</summary> 
        /// <param name="reader"> correctly positioned xmlreader</param>
        /// <param name="baseLink">the base object to set the property on</param> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        protected void ParseBaseLink(XmlReader reader, AtomBaseLink baseLink)
        {
            Tracing.TraceCall();
            Tracing.Assert(reader != null, "reader should not be null");
            if (reader == null)
            {
                throw new ArgumentNullException("reader"); 
            }
            Tracing.Assert(baseLink != null, "baseLink should not be null");
            if (baseLink == null)
            {
                throw new ArgumentNullException("baseLink"); 
            }
            ParseBasicAttributes(reader, baseLink);
            if (reader.NodeType == XmlNodeType.Element) 
            {
                // read the element content
                baseLink.Uri = new AtomUri(reader.ReadString()); 
            }
        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>parses an author/person object</summary> 
        /// <param name="reader"> an XmlReader positioned at the start of the author</param>
        /// <param name="whoIsIt">indicates the localname to check for</param>
        /// <returns> the created author object</returns>
        //////////////////////////////////////////////////////////////////////
        protected AtomPerson ParsePerson(XmlReader reader, object whoIsIt)
        {
            Tracing.Assert(reader != null, "reader should not be null");
            if (reader == null)
            {
                throw new ArgumentNullException("reader"); 
            }
            Tracing.Assert(whoIsIt != null, "whoIsIt should not be null");
            if (whoIsIt == null)
            {
                throw new ArgumentNullException("whoIsIt"); 
            }
            Tracing.TraceCall();
            AtomPerson author = null;
            object localname = null; 

            if (whoIsIt.Equals(this.nameTable.Author))
            {
                author = new AtomPerson(AtomPersonType.Author);
            }
            else 
            {
                author = new AtomPerson(AtomPersonType.Contributor);
            }

            ParseBasicAttributes(reader, author);
            
            int lvl = -1;
            while(NextChildElement(reader, ref lvl))
            {
                localname = reader.LocalName;

                if (localname.Equals(this.nameTable.Name))
                {
                    // author.Name = Utilities.DecodeString(reader.ReadString());
                    author.Name = reader.ReadString();
                    reader.Read();
                }
                else if (localname.Equals(this.nameTable.Uri))
                {
                    author.Uri = new AtomUri(reader.ReadString());
                    reader.Read();
                }
                else if (localname.Equals(this.nameTable.Email))
                {
                    author.Email = reader.ReadString();
                    reader.Read();
                }
                else 
                {
                    // default extension parsing.
                    ParseExtensionElements(reader, author);

                }
            }
            return author;
        }
        /////////////////////////////////////////////////////////////////////////////





        //////////////////////////////////////////////////////////////////////
        /// <summary>parses an xml stream to create an AtomCategory object</summary> 
        /// <param name="reader">correctly positioned xmlreader</param>
        /// <returns> the created AtomCategory object</returns>
        //////////////////////////////////////////////////////////////////////
        protected AtomCategory ParseCategory(XmlReader reader)
        {
            Tracing.TraceCall();
            AtomCategory category = null;
            Tracing.Assert(reader != null, "reader should not be null");
            if (reader == null)
            {
                throw new ArgumentNullException("reader"); 
            }

            object localname = reader.LocalName;
            if (localname.Equals(this.nameTable.Category))
            {
                category = new AtomCategory();
                if (reader.HasAttributes)
                {
                    while (reader.MoveToNextAttribute())
                    {
                        localname = reader.LocalName;
                        if (localname.Equals(this.nameTable.Term))
                        {
                            category.Term = reader.Value;
                        }
                        else if (localname.Equals(this.nameTable.Scheme))
                        {
                            category.Scheme = new AtomUri(reader.Value);
                        }
                        else if (localname.Equals(this.nameTable.Label))
                        {
                            category.Label = reader.Value;
                        }
                        else 
                        {
                            ParseBaseAttributes(reader, category);
                        }
                    }
                }
            }
            return category;
        }
        /////////////////////////////////////////////////////////////////////////////



        //////////////////////////////////////////////////////////////////////
        /// <summary>creates an atomlink object</summary> 
        /// <param name="reader">correctly positioned xmlreader</param>
        /// <returns> the created AtomLink object</returns>
        //////////////////////////////////////////////////////////////////////
        protected AtomLink ParseLink(XmlReader reader)
        {
            Tracing.Assert(reader != null, "reader should not be null");
            if (reader == null)
            {
                throw new ArgumentNullException("reader"); 
            }
            
            
            Tracing.TraceCall();
            AtomLink link = null;
            object localname = null;

            link = new AtomLink();
            if (reader.HasAttributes)
            {
                while (reader.MoveToNextAttribute())
                {
                    localname = reader.LocalName;
                    if (localname.Equals(this.nameTable.HRef))
                    {
                        link.HRef = new AtomUri(reader.Value);
                    }
                    else if (localname.Equals(this.nameTable.Rel))
                    {
                        link.Rel = reader.Value;
                    }
                    else if (localname.Equals(this.nameTable.Type))
                    {
                        link.Type = reader.Value;
                    }
                    else if (localname.Equals(this.nameTable.HRefLang))
                    {
                        link.HRefLang = reader.Value;
                    }
                    else if (localname.Equals(this.nameTable.Title))
                    {
                        link.Title = reader.Value;
                    }
                    else if (localname.Equals(this.nameTable.Length))
                    {
                        link.Length = int.Parse(reader.Value, CultureInfo.InvariantCulture);
                    }
                    else 
                    {
                        ParseBaseAttributes(reader, link);
                    }
                }
            }
            return link;
        }
        /////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////
        /// <summary>reads one of the feed entries at a time</summary> 
        /// <param name="reader"> XmlReader positioned at the entry element</param>
        /// <returns> notifies user using event mechanism</returns>
        //////////////////////////////////////////////////////////////////////
        public void ParseEntry(XmlReader reader)
        {
            Tracing.Assert(reader != null, "reader should not be null");
            if (reader == null)
            {
                throw new ArgumentNullException("reader"); 
            }
            
            
            object localname = reader.LocalName; 
            Tracing.TraceCall("Parsing atom entry");
            if (localname.Equals(this.nameTable.Entry)==false)
            {
                throw new ClientFeedException("trying to parse an atom entry, but reader is not at the right spot");
            }

            AtomEntry entry = OnCreateNewEntry();
            ParseBasicAttributes(reader, entry);


            // remember the depth of entry
            int depth = -1;
            while(NextChildElement(reader, ref depth))
            {
                localname = reader.LocalName; 

                if (IsCurrentNameSpace(reader, BaseNameTable.NSAtom))
                {
                    if (localname.Equals(this.nameTable.Id))
                    {
                        entry.Id = new AtomId();
                        ParseBaseLink(reader, entry.Id);
                    }
                    else if (localname.Equals(this.nameTable.Link))
                    {
                        AtomLink link = ParseLink(reader);
                        entry.Links.Add(link);
                    }
                    else if (localname.Equals(this.nameTable.Updated))
                    {
                        entry.Updated = DateTime.Parse(reader.ReadString(), CultureInfo.InvariantCulture);
                    }
                    else if (localname.Equals(this.nameTable.Published))
                    {
                        entry.Published = DateTime.Parse(reader.ReadString(), CultureInfo.InvariantCulture);
                    }
                    else if (localname.Equals(this.nameTable.Author))
                    {
                        entry.Authors.Add(ParsePerson(reader, localname));
                    }
                    else if (localname.Equals(this.nameTable.Contributor))
                    {
                        entry.Contributors.Add(ParsePerson(reader, localname));
                    }
                    else if (localname.Equals(this.nameTable.Rights))
                    {
                        entry.Rights = ParseTextConstruct(reader, AtomTextConstructElementType.Rights);
                    }
                    else if (localname.Equals(this.nameTable.Category))
                    {
                        AtomCategory category = ParseCategory(reader);
                        entry.Categories.Add(category);
                    }
                    else if (localname.Equals(this.nameTable.Summary))
                    {
                        entry.Summary = ParseTextConstruct(reader, AtomTextConstructElementType.Summary);
                    }
                    else if (localname.Equals(this.nameTable.Content))
                    {
                        entry.Content = ParseContent(reader);
                    }
                    else if (localname.Equals(this.nameTable.Source))
                    {
                        entry.Source = new AtomSource();
                        ParseSource(reader, entry.Source);
                    }
                    else if (localname.Equals(this.nameTable.Title))
                    {
                        entry.Title = ParseTextConstruct(reader, AtomTextConstructElementType.Title);
                    }
                    // this will either move the reader to the end of an element, or, 
                    // if at the end, to the start of a new one. 
                    reader.Read();
                }
                else if (IsCurrentNameSpace(reader, BaseNameTable.gBatchNamespace))
                {
                    // parse the google batch extensions if they are there
                    ParseBatch(reader, entry); 
                }
                else
                {
                    // default extension parsing
                    ParseExtensionElements(reader, entry);
                }
            }
            OnNewAtomEntry(entry);

            return;
        }






        /// <summary>
        /// parses the current position in the xml reader and fills 
        /// the provided GDataEntryBatch property on the entry object 
        /// </summary>
        /// <param name="reader">the xmlreader positioned at a batch element</param>
        /// <param name="entry">the atomentry object to fill in</param>
        protected void ParseBatch(XmlReader reader, AtomEntry entry)
        {
            if (IsCurrentNameSpace(reader, BaseNameTable.gBatchNamespace))
            {
                object elementName = reader.LocalName;

                if (entry.BatchData == null)
                {
                    entry.BatchData = new GDataBatchEntryData(); 
                }

                GDataBatchEntryData batch = entry.BatchData; 

                if (elementName.Equals(this.nameTable.BatchId))
                {
                    batch.Id = reader.ReadString(); 
                }
                else if (elementName.Equals(this.nameTable.BatchOperation))
                {
                    batch.Type = ParseOperationType(reader);
                }
                else if (elementName.Equals(this.nameTable.BatchStatus))
                {
                    batch.Status = ParseBatchStatus(reader); 
                }
                else if (elementName.Equals(this.nameTable.BatchInterrupt))
                {
                    batch.Interrupt= ParseBatchInterrupt(reader); 
                }
                else 
                {
                    Tracing.TraceInfo("got an unknown batch element: "  + elementName.ToString()); 
                    // default extension parsing
                    ParseExtensionElements(reader, entry);
                }
            }
        }

        /// <summary>
        /// reads the current positioned reader and creates a operationtype enum
        /// </summary>
        /// <param name="reader">XmlReader positioned at the start of the status element</param>
        /// <returns>GDataBatchOperationType</returns>
        protected GDataBatchOperationType ParseOperationType(XmlReader reader) 
        {
            Tracing.Assert(reader != null, "reader should not be null");
            if (reader == null)
            {
                throw new ArgumentNullException("reader"); 
            }
            GDataBatchOperationType type = GDataBatchOperationType.Default; 

            object localname = reader.LocalName;
            if (localname.Equals(this.nameTable.BatchOperation))
            {
                if (reader.HasAttributes)
                {
                    while (reader.MoveToNextAttribute())
                    {
                        localname = reader.LocalName;
                        if (localname.Equals(this.nameTable.BatchOperationType))
                        {
                            type = (GDataBatchOperationType)Enum.Parse(typeof(GDataBatchOperationType), reader.Value); 
                        }
                    }
                }
            }
            return type;
        }



        /// <summary>
        /// parses the current position in the xml reader and fills 
        /// the provided GDataFeedBatch property on the feed object
        /// </summary>
        /// <param name="reader">the xmlreader positioned at a batch element</param>
        /// <param name="feed">the atomfeed object to fill in</param>
        protected void ParseBatch(XmlReader reader, AtomFeed feed) 
        {
            if (IsCurrentNameSpace(reader, BaseNameTable.gBatchNamespace))
            {
                object elementName = reader.LocalName;

                if (feed.BatchData == null)
                {
                    feed.BatchData = new GDataBatchFeedData(); 
                }

                GDataBatchFeedData batch = feed.BatchData; 

                if (elementName.Equals(this.nameTable.BatchOperation))
                {
                    batch.Type = (GDataBatchOperationType)Enum.Parse(typeof(GDataBatchOperationType), 
                                                reader.GetAttribute(BaseNameTable.XmlAttributeBatchOperationType));
                }
                else 
                {
                    Tracing.TraceInfo("got an unknown batch element: "  + elementName.ToString()); 
                    reader.Skip(); 
                }
            }
        }


        /// <summary>
        /// reads the current positioned reader and creates a batchstatus element
        /// </summary>
        /// <param name="reader">XmlReader positioned at the start of the status element</param>
        /// <returns>GDataBatchStatus</returns>
        protected GDataBatchStatus ParseBatchStatus(XmlReader reader) 
        {
            Tracing.Assert(reader != null, "reader should not be null");
            if (reader == null)
            {
                throw new ArgumentNullException("reader"); 
            }
            GDataBatchStatus status = null; 

            object localname = reader.LocalName;
            if (localname.Equals(this.nameTable.BatchStatus))
            {
                status = new GDataBatchStatus();
                if (reader.HasAttributes)
                {
                    while (reader.MoveToNextAttribute())
                    {
                        localname = reader.LocalName;
                        if (localname.Equals(this.nameTable.BatchReason))
                        {
                            status.Reason = reader.Value;
                        }
                        else if (localname.Equals(this.nameTable.BatchContentType))
                        {
                            status.ContentType = reader.Value; 
                        }
                        else if (localname.Equals(this.nameTable.BatchStatusCode))
                        {
                            status.Code = int.Parse(reader.Value, CultureInfo.InvariantCulture);
                        }
                    }
                }

                reader.MoveToElement();

                status.Value = reader.ReadString();

                // status can have one child element, errors
                // for now disabled, as this is currently not supported on the server
                // instead the errors come as encoded strings
                /*
                int lvl = -1;
                while(NextChildElement(reader, ref lvl))
                {
                    localname = reader.LocalName;

                    if (localname.Equals(this.nameTable.BatchErrors))
                    {
                        // author.Name = Utilities.DecodeString(reader.ReadString());
                        status.Errors = ParseBatchErrors(reader); 
                    }
                }
                */
            }
            return status;
        }

        /// <summary>
        ///  parses a list of errors
        /// </summary>
        /// <param name="reader">XmlReader positioned at the start of the status element</param>
        /// <returns>GDataBatchErrorCollection</returns>
        protected GDataBatchErrorCollection ParseBatchErrors(XmlReader reader)
        {
            object localname = reader.LocalName;
            GDataBatchErrorCollection collection = null;
            if (localname.Equals(this.nameTable.BatchErrors))
            {
                collection = new GDataBatchErrorCollection(); 

                int lvl = -1;
                while(NextChildElement(reader, ref lvl))
                {
                    if (localname.Equals(this.nameTable.BatchError))
                    {
                        collection.Add(ParseBatchError(reader));
                    }

                }

            }
            return collection;
        }

        /// <summary>
        /// parses a single error element
        /// </summary>
        /// <param name="reader">XmlReader positioned at the start of the status element</param>
        /// <returns>GDataBatchError</returns>
        protected GDataBatchError ParseBatchError(XmlReader reader)
        {
            object localname = reader.LocalName;
            GDataBatchError error = null;
            if (localname.Equals(this.nameTable.BatchError))
            {
                error = new GDataBatchError(); 
                if (reader.HasAttributes)
                {
                    while (reader.MoveToNextAttribute())
                    {
                        localname = reader.LocalName;
                        if (localname.Equals(this.nameTable.BatchReason))
                        {
                            error.Reason = reader.Value;
                        }
                        else if (localname.Equals(this.nameTable.BatchOperationType))
                        {
                            error.Type = reader.Value; 
                        }
                        else if (localname.Equals(this.nameTable.BatchField))
                        {
                            error.Field = reader.Value; 
                        }
                    }
                }
            }
            return error; 
        }


        /// <summary>
        /// parses a batchinterrupt element from a correctly positioned reader
        /// </summary>
        /// <param name="reader">XmlReader at the start of the element</param>
        /// <returns>GDataBatchInterrupt</returns>
        protected GDataBatchInterrupt ParseBatchInterrupt(XmlReader reader)
        {
            object localname = reader.LocalName;
            GDataBatchInterrupt interrupt = null;
            if (localname.Equals(this.nameTable.BatchInterrupt))
            {
                interrupt = new GDataBatchInterrupt(); 
                if (reader.HasAttributes)
                {
                    while (reader.MoveToNextAttribute())
                    {
                        localname = reader.LocalName;
                        if (localname.Equals(this.nameTable.BatchReason))
                        {
                            interrupt.Reason = reader.Value;
                        }
                        else if (localname.Equals(this.nameTable.BatchSuccessCount))
                        {
                            interrupt.Successes = int.Parse(reader.Value); 
                        }
                        else if (localname.Equals(this.nameTable.BatchFailureCount))
                        {
                            interrupt.Failures = int.Parse(reader.Value); 
                        }
                        else if (localname.Equals(this.nameTable.BatchParsedCount))
                        {
                            interrupt.Parsed = int.Parse(reader.Value); 
                        }
                        else if (localname.Equals(this.nameTable.BatchUnprocessed))
                        {
                            interrupt.Unprocessed= int.Parse(reader.Value); 
                        }

                    }
                }
            }
            return interrupt; 

        }


        //////////////////////////////////////////////////////////////////////
        /// <summary>parses an AtomTextConstruct</summary> 
        /// <param name="reader">the xmlreader correctly positioned at the construct </param>
        /// <param name="elementType">the type of element to create</param>
        /// <returns>the new text construct </returns>
        //////////////////////////////////////////////////////////////////////
        protected AtomTextConstruct ParseTextConstruct(XmlReader reader, AtomTextConstructElementType elementType)
        {
            Tracing.Assert(reader != null, "reader should not be null");
            if (reader == null)
            {
                throw new ArgumentNullException("reader"); 
            }
            
            
            AtomTextConstruct construct = null;

            Tracing.TraceCall("Parsing atomTextConstruct");

            construct = new AtomTextConstruct(elementType);

            if (reader.NodeType == XmlNodeType.Element) 
            {
                if (reader.HasAttributes)
                {
                    while (reader.MoveToNextAttribute())
                    {
                        object attributeName = reader.LocalName;
    
                        if (attributeName.Equals(this.nameTable.Type))
                        {
                            construct.Type = (AtomTextConstructType)Enum.Parse(typeof(AtomTextConstructType), reader.Value);
                        }
                        else 
                        {
                            ParseBaseAttributes(reader, construct); 
                        }
                    }
                }
                reader.MoveToElement();
                switch (construct.Type)
                {
                    case AtomTextConstructType.text:
                        construct.Text = reader.ReadString();
                        break;
                    case AtomTextConstructType.xhtml:
                        construct.Text = reader.ReadInnerXml();
                        break;
                    case AtomTextConstructType.html:
                        construct.Text = reader.ReadInnerXml();
                        break;
                }
            }
            return construct;
        }
        /////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////
        /// <summary>parses an AtomGenerator</summary> 
        /// <param name="reader">the xmlreader correctly positioned at the generator </param>
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        protected AtomGenerator ParseGenerator(XmlReader reader)
        {

            //    atomGenerator = element atom:generator {
            //    atomCommonAttributes,
            //    attribute url { atomUri }?,
            //    attribute version { text }?,
            //    text
            //     }

            Tracing.Assert(reader != null, "reader should not be null");
            if (reader == null)
            {
                throw new ArgumentNullException("reader"); 
            }

            Tracing.TraceCall();
            AtomGenerator generator = null; 
            object localname = reader.LocalName;

            if (localname.Equals(this.nameTable.Generator))
            {
                generator = new AtomGenerator();

                generator.Text = reader.ReadString();
                if (reader.HasAttributes)
                {
                    while (reader.MoveToNextAttribute())
                    {
                        object attributeName = reader.LocalName;

                        if (attributeName.Equals(this.nameTable.Uri))
                        {
                            generator.Uri = new AtomUri(reader.Value);
                        }
                        else if (attributeName.Equals(this.nameTable.Version))
                        {
                            generator.Version = reader.Value;
                        }
                        else
                        {
                            ParseBaseAttributes(reader, generator); 
                        }
                    }
                }
            }
            
            return generator;
        }
        /////////////////////////////////////////////////////////////////////////////



        //////////////////////////////////////////////////////////////////////
        /// <summary>creates an AtomContent object by parsing an xml stream</summary> 
        /// <param name="reader">a XMLReader positioned correctly </param>
        /// <returns> null or an AtomContent object</returns>
        //////////////////////////////////////////////////////////////////////
        protected AtomContent ParseContent(XmlReader reader)
        {
            Tracing.Assert(reader != null, "reader should not be null");
            if (reader == null)
            {
                throw new ArgumentNullException("reader"); 
            }
            
            AtomContent content = null;
            object localname = reader.LocalName;

            Tracing.TraceCall();
            if (localname.Equals(this.nameTable.Content))
            {
                content = new AtomContent();
                if (reader.HasAttributes)
                {
                    while (reader.MoveToNextAttribute())
                    {
                        localname = reader.LocalName;
                        if (localname.Equals(this.nameTable.Type))
                        {
                            content.Type = reader.Value;
                        }
                        else if (localname.Equals(this.nameTable.Src))
                        {
                            content.Src = new AtomUri(reader.Value);
                        }
                        else
                        {
                            ParseBaseAttributes(reader, content); 
                        }
                    }
                }
                
                if (MoveToStartElement(reader) == true)
                {
                    // using readInnerXml has disadvantages, even for HTML/XHTML. in .NET 1.1
                    // decoding will happen and text like "this & that" will come back
                    // as "this &amp; that" 
                    content.Content = reader.ReadString();
                }
            }
            return content;
        }
        /////////////////////////////////////////////////////////////////////////////
    }
    /////////////////////////////////////////////////////////////////////////////
}
/////////////////////////////////////////////////////////////////////////////

