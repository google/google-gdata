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

#define USE_TRACING

using System;
using System.Xml;
using System.IO; 
using System.Globalization;
using System.Collections;


#endregion

//////////////////////////////////////////////////////////////////////
// Contains AtomBase, the base class for all Atom-related objects.
// AtomBase holds the two common properties (xml:lang and xml:base).
//////////////////////////////////////////////////////////////////////
namespace Google.GData.Client
{
    /// <summary>Helper object to walk the tree for the dirty flag.</summary> 
    public class BaseIsDirty : IBaseWalkerAction
    {
        //////////////////////////////////////////////////////////////////////
        /// <summary>Walker action. Just gets a property.</summary> 
        /// <param name="atom">object to set the property on</param>
        /// <returns>the value of the dirty flag</returns>
        //////////////////////////////////////////////////////////////////////
        public bool Go(AtomBase atom)
        {
            Tracing.Assert(atom != null, "atom should not be null");
            if (atom == null)
            {
                throw new ArgumentNullException("atom"); 
            }
            return atom.Dirty;
        }
    }


    /// <summary>Helper object to walk the tree for the dirty flag.</summary> 
    public class BaseMarkDirty : IBaseWalkerAction
    {
        /// <summary>Holds if set/unset to dirty.</summary> 
        private bool flag; 

        //////////////////////////////////////////////////////////////////////
        /// <summary>Constructor.</summary> 
        /// <param name="flag">indicates the value to pass </param>
        //////////////////////////////////////////////////////////////////////
        internal BaseMarkDirty(bool flag)
        {
            this.flag = flag;
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>Walker action. Just sets a property.</summary> 
        /// <param name="atom">object to set the property on </param>
        /// <returns> always false, indicating to walk the whole tree</returns>
        //////////////////////////////////////////////////////////////////////
        public bool Go(AtomBase atom)
        {
            Tracing.Assert(atom != null, "atom should not be null");
            if (atom == null)
            {
                throw new ArgumentNullException("atom"); 
            }
            atom.Dirty = this.flag; 
            return false; 
        }
    }

    /// <summary>Helper class, mainly used to walk the tree for the dirty flag.</summary> 
    public class BaseIsPersistable : IBaseWalkerAction
    {
        //////////////////////////////////////////////////////////////////////
        /// <summary>Walker action. Just gets a property.</summary> 
        /// <param name="atom">object to set the property on </param>
        /// <returns>returns the value of the ShouldBePersisted() of the object</returns>
        //////////////////////////////////////////////////////////////////////
        public bool Go(AtomBase atom)
        {
            Tracing.Assert(atom != null, "atom should not be null");
            if (atom == null)
            {
                throw new ArgumentNullException("atom"); 
            }
            
            bool f = atom.ShouldBePersisted(); 
            Tracing.TraceInfo(atom.ToString() + " ... is persistable: " + f.ToString()); 
            return f; 
        }
    }

    //////////////////////////////////////////////////////////////////////
    /// <summary>AtomBase object representation.
    /// </summary> 
    //////////////////////////////////////////////////////////////////////
    public abstract class AtomBase : IExtensionContainer
    {
        /// <summary>holds the base Uri</summary> 
        private AtomUri uriBase;
        /// <summary>implied base, get's pushed down</summary> 
        private AtomUri uriImpliedBase;
        /// <summary>holds the xml:lang element</summary> 
        private string atomLanguageTag;
        /// <summary>extension element collection</summary>
        private ArrayList extensionsList; 
        /// <summary> extension element factories </summary>
        private ArrayList extensionFactories; 
       /// <summary>a boolean indicating that recalc is allowed to happen implicitly now</summary> 
        private bool fAllowRecalc;
        /// <summary>holds a flag indicating if the thing should be send to the server</summary> 
        private bool fIsDirty; 

        //////////////////////////////////////////////////////////////////////
        /// <summary>sets the element and all subelemts dirty flag</summary> 
        /// <param name="fFlag">indicates the property value to set</param>
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        internal void MarkElementDirty(bool fFlag)
        {
            this.WalkTree(new BaseMarkDirty(fFlag));
        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>checks if the element or one subelement are persistable</summary> 
        //////////////////////////////////////////////////////////////////////
        public bool IsPersistable()
        {
            return this.WalkTree(new BaseIsPersistable());
        }
        /////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////
        /// <summary>returns if the element or one subelement is dirty</summary> 
        //////////////////////////////////////////////////////////////////////
        public bool IsDirty()
        {
            return WalkTree(new BaseIsDirty()); 
        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>The dirty property - indicates if exactly this element is dirty</summary> 
        /// <returns>returns true or false</returns>
        //////////////////////////////////////////////////////////////////////
        public bool Dirty
        {
            get {return this.fIsDirty;}
            set {this.fIsDirty = value;}
        }
        /////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////
        /// <summary>property that holds the implied base URI</summary> 
        /// <returns> the implied base URI as an AtomUri</returns>
        //////////////////////////////////////////////////////////////////////
        protected AtomUri ImpliedBase
        {
            get {return this.uriImpliedBase;}
            set {this.uriImpliedBase = value;}
        }
        /////////////////////////////////////////////////////////////////////////////



        //////////////////////////////////////////////////////////////////////
        /// <summary>Returns the XML name as string used for the element when persisting.</summary> 
        //////////////////////////////////////////////////////////////////////
        public abstract string XmlName
        {
            get;
        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>Read only accessor for AbsoluteUri. This is pushed down
        /// whenever a base changes.</summary> 
        //////////////////////////////////////////////////////////////////////
        public string GetAbsoluteUri(string part)
        {
            return Utilities.CalculateUri(this.Base, this.ImpliedBase, part);
        }
        /////////////////////////////////////////////////////////////////////////////




        //////////////////////////////////////////////////////////////////////
        /// <summary>This starts the calculation, to push down the base
        /// URI changes.</summary> 
        /// <param name="uriValue">the baseuri calculated so far</param>
        //////////////////////////////////////////////////////////////////////
        internal virtual void BaseUriChanged(AtomUri uriValue)
        {
            // if this is ever getting called explicitly (parsing), we turn on recalc
            this.fAllowRecalc = true;
            this.uriImpliedBase = uriValue;
        }
        /////////////////////////////////////////////////////////////////////////////



        #region accessors

        //////////////////////////////////////////////////////////////////////
        /// <summary>calculates or set's the base uri of an element</summary> 
        /// <returns>an AtomUri for the Base URI when get is called</returns>
        //////////////////////////////////////////////////////////////////////
        public AtomUri Base
        {
            get {return this.uriBase;}
            set 
            {
                this.Dirty = true; 
                this.uriBase = value; 
                if (this.fAllowRecalc == true)
                    BaseUriChanged(value);
            }
        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>accessor method public string Language</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public string Language
        {
            get {return this.atomLanguageTag;}
            set {this.Dirty = true; this.atomLanguageTag = value;}
        }
        /////////////////////////////////////////////////////////////////////////////


        /// <summary>
        /// adding an extension factory for extension elements
        /// </summary>
        /// <param name="factory">The factory</param>
        public void AddExtension(Object factory) 
        {
            Tracing.Assert(factory != null, "factory should not be null");
            if (factory == null)
            {
                throw new ArgumentNullException("factory"); 
            }
         
            if (this.extensionFactories == null)
            {
                this.extensionFactories = new ArrayList();
            }
            this.extensionFactories.Add(factory);
        }
        //////////////////////////////////////////////////////////////////////


        /// <summary>
        /// read only accessor for the Extension Factories
        /// </summary>
        public ArrayList ExtensionFactories 
        {
            get 
            { 
                return this.extensionFactories;
            }
        }
        


        //////////////////////////////////////////////////////////////////////
        /// <summary>Calls the action on this object and all children.</summary> 
        /// <param name="action">an IBaseWalkerAction interface to call </param>
        /// <returns>true or false, pending outcome of the passed in action</returns>
        //////////////////////////////////////////////////////////////////////
        public virtual bool WalkTree(IBaseWalkerAction action)
        {
            Tracing.Assert(action != null, "action should not be null");
            if (action == null)
            {
                throw new ArgumentNullException("action"); 
            }
            return action.Go(this); 
        }
        /////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////
        /// <summary>read only accessor for the ExtensionsElements Collections</summary> 
        /// <returns> an ArrayList of ExtensionElements</returns>
        //////////////////////////////////////////////////////////////////////
        public ArrayList ExtensionElements
        {
            get 
            {
                if (this.extensionsList == null)
                {
                    this.extensionsList = new ArrayList();
                }
                return this.extensionsList;
            }
        }
        /////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Finds a specific ExtensionElement based on it's local name
        /// and it's namespace. If namespace is NULL, the first one where
        /// the localname matches is found. If there are extensionelements that do 
        /// not implment ExtensionElementFactory, they will not be taken into account
        /// Primary use of this is to find XML nodes
        /// </summary>
        /// <param name="localName">the xml local name of the element to find</param>
        /// <param name="ns">the namespace of the elementToPersist</param>
        /// <returns>Object</returns>
        public Object FindExtension(string localName, string ns) 
        {
            return Utilities.FindExtension(this.ExtensionElements, localName, ns);
        }

        /// <summary>
        /// Creates an extension for a given name and namespace by walking the
        /// extension factories list and calling CreateInstance for the right one
        /// </summary>
        /// <param name="localName">the xml local name of the element to find</param>
        /// <param name="ns">the namespace of the elementToPersist</param>
        /// <returns>Object</returns>
        public IExtensionElement CreateExtension(string localName, string ns) 
        {
            IExtensionElement ele = null;
            IExtensionElementFactory f = FindExtensionFactory(localName, ns);
            if (f != null)
            {
                ele = f.CreateInstance(null, null);
            }
            return ele;
        }



        /// <summary>
        /// Finds the extension factory for a given name/namespace
        /// </summary>
        /// <param name="localName">the xml local name of the element to find</param>
        /// <param name="ns">the namespace of the elementToPersist</param>
        /// <returns>Object</returns>
        public IExtensionElementFactory FindExtensionFactory(string localName, string ns) 
        {
            foreach (IExtensionElementFactory f in this.ExtensionFactories)
            {
                if (String.Compare(ns, f.XmlNameSpace, true) == 0)
                {
                    if (String.Compare(localName, f.XmlName) == 0)
                    {
                        return f;
                    }
                }
            }
            return null;
        }




        /// <summary>
        /// Finds all ExtensionElement based on it's local name
        /// and it's namespace. If namespace is NULL, allwhere
        /// the localname matches is found. If there are extensionelements that do 
        /// not implment ExtensionElementFactory, they will not be taken into account
        /// Primary use of this is to find XML nodes
        /// </summary>
        /// <param name="localName">the xml local name of the element to find</param>
        /// <param name="ns">the namespace of the elementToPersist</param>
        /// <returns>Object</returns>
        public ArrayList FindExtensions(string localName, string ns) 
        {
            return FindExtensions(localName, ns, new ArrayList());
        }

        /// <summary>
        /// Finds all ExtensionElement based on it's local name
        /// and it's namespace. If namespace is NULL, allwhere
        /// the localname matches is found. If there are extensionelements that do 
        /// not implment ExtensionElementFactory, they will not be taken into account
        /// Primary use of this is to find XML nodes
        /// </summary>
        /// <param name="localName">the xml local name of the element to find</param>
        /// <param name="ns">the namespace of the elementToPersist</param>
        /// <param name="arr">the array to fill</param>
        /// <returns>none</returns>
        public ArrayList FindExtensions(string localName, string ns, ArrayList arr) 
        {
            return Utilities.FindExtensions(this.ExtensionElements, 
                                            localName, ns, arr);

        }

        /// <summary>
        /// Delete's all Extensions from the Extension list that match
        /// a localName and a Namespace. 
        /// </summary>
        /// <param name="localName">the local name to find</param>
        /// <param name="ns">the namespace to match, if null, ns is ignored</param>
        /// <returns>int - the number of deleted extensions</returns>
        public int DeleteExtensions(string localName, string ns) 
        {
            // Find them first
            ArrayList arr = FindExtensions(localName, ns);
            foreach (object ob in arr)
            {
                this.ExtensionElements.Remove(ob);
            }
            return arr.Count;
        }

    
        /// <summary>
        /// all extension elements that match a namespace/localname
        /// given will be removed and replaced with the new ones.
        /// the input array can contain several different
        /// namespace/localname combinations
        /// if the passed list is NULL or empty, this will just result
        /// in additions
        /// </summary>
        /// <param name="newList">a list of xmlnodes or IExtensionElementFactory objects</param>
        /// <returns>int - the number of deleted extensions</returns>
        public int ReplaceExtensions(ArrayList newList) 
        {
             int count = 0;
            // get rid of all of the old ones matching the specs
            if (newList != null)
            {
                foreach (Object ob in newList)
                {
                    string localName = null;
                    string ns = null;
                    XmlNode node = ob as XmlNode;
                    if (node != null)
                    {
                        localName = node.LocalName;
                        ns = node.NamespaceURI;
                    } 
                    else 
                    {
                        IExtensionElementFactory ele = ob as IExtensionElementFactory;
                        if (ele != null)
                        {
                            localName = ele.XmlName;
                            ns = ele.XmlNameSpace;
                        }
                    }
                
                    if (localName != null)
                    {
                        count += DeleteExtensions(localName, ns);
                    }
                }
            }
            // now add the new ones
            foreach (Object ob in newList)
            {
                this.ExtensionElements.Add(ob);
            }
            return count;
        }

        /// <summary>
        /// all extension elements that match a namespace/localname
        /// given will be removed and the new one will be inserted
        /// </summary> 
        /// <param name="localName">the local name to find</param>
        /// <param name="ns">the namespace to match, if null, ns is ignored</param>
        /// <param name="obj">the new element to put in</param>
        public void ReplaceExtension
            (string localName, string ns, Object obj)
        {
            DeleteExtensions(localName, ns);
            this.ExtensionElements.Add(obj);
        }


        /// <summary>
        /// this is the subclassing method for AtomBase derived 
        /// classes to overload what childelements should be created
        /// needed to create CustomLink type objects, like WebContentLink etc
        /// </summary>
        /// <param name="reader">The XmlReader that tells us what we are working with</param>
        /// <param name="parser">the parser is primarily used for nametable comparisons</param>
        /// <returns>AtomBase</returns>
        public virtual AtomBase CreateAtomSubElement(XmlReader reader, AtomFeedParser parser)
        {
            Object localname = reader.LocalName;
            if (localname.Equals(parser.Nametable.Id))
            {
                return new AtomId();
            }
            else if (localname.Equals(parser.Nametable.Link))
            {
                return new AtomLink();
            } 
            else if (localname.Equals(parser.Nametable.Icon))
            {
                return new AtomIcon();
            } 
            else if (localname.Equals(parser.Nametable.Logo))
            {
                return new AtomLogo();
            } else if (localname.Equals(parser.Nametable.Author)) 
            {
                return new AtomPerson(AtomPersonType.Author);
            }
            else if (localname.Equals(parser.Nametable.Contributor)) 
            {
                return new AtomPerson(AtomPersonType.Contributor);
            } else if (localname.Equals(parser.Nametable.Title))
            {
                return new AtomTextConstruct(AtomTextConstructElementType.Title);
            } else if (localname.Equals(parser.Nametable.Subtitle))
            {
                return new AtomTextConstruct(AtomTextConstructElementType.Subtitle);
            } else if (localname.Equals(parser.Nametable.Rights))
            {
                return new AtomTextConstruct(AtomTextConstructElementType.Rights);
            } else if (localname.Equals(parser.Nametable.Summary))
            {
                return new AtomTextConstruct(AtomTextConstructElementType.Summary);
             
            } else if (localname.Equals(parser.Nametable.Generator))
            {
                return new AtomGenerator();
            } else if (localname.Equals(parser.Nametable.Category))
            {
                return new AtomCategory();
            }

            throw new NotImplementedException("AtomBase CreateChild should NEVER be called for: " + reader.LocalName);
        }

    
        //////////////////////////////////////////////////////////////////////
        /// <summary>Saves the object as XML.</summary> 
        /// <param name="stream">stream to save to</param>
        /// <returns>how many bytes written</returns>
        //////////////////////////////////////////////////////////////////////
        public void SaveToXml(Stream stream)
        {
            Tracing.Assert(stream != null, "stream should not be null");
            if (stream == null)
            {
                throw new ArgumentNullException("stream"); 
            }
            XmlTextWriter writer = new XmlTextWriter(stream,System.Text.Encoding.UTF8);
            writer.Formatting = Formatting.Indented;
            writer.WriteStartDocument();
            
            SaveToXml(writer);
            writer.Flush();
        }
        /////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////
        /// <summary>saves the object as XML</summary> 
        /// <param name="writer">the xmlwriter to save to</param>
        //////////////////////////////////////////////////////////////////////
        public void SaveToXml(XmlWriter writer)
        {
            Tracing.Assert(writer != null, "writer should not be null");
            if (writer == null)
            {
                throw new ArgumentNullException("writer"); 
            }
            if (IsPersistable())
            {
                WriteElementStart(writer, this.XmlName);
                AddOtherNamespaces(writer); 
                SaveXmlAttributes(writer);
                SaveInnerXml(writer);
                writer.WriteEndElement();
            }
            this.MarkElementDirty(false);
        }

        /////////////////////////////////////////////////////////////////////////////



        //////////////////////////////////////////////////////////////////////
        /// <summary>protected virtual int SaveXmlAttributes(XmlWriter writer)</summary> 
        /// <param name="writer">the XmlWriter to save to</param>
        //////////////////////////////////////////////////////////////////////
        protected virtual void SaveXmlAttributes(XmlWriter writer)
        {

            Tracing.Assert(writer != null, "writer should not be null");
            if (writer == null)
            {
                throw new ArgumentNullException("writer"); 
            }

            // this will save the base and language attributes, if we have them
            if (Utilities.IsPersistable(this.uriBase))
            {
                //Lookup the prefix and then write the ISBN attribute.
                writer.WriteAttributeString("xml", "base", BaseNameTable.NSXml, this.Base.ToString());
            }
            if (Utilities.IsPersistable(this.atomLanguageTag))
            {
                writer.WriteAttributeString("xml", "lang", BaseNameTable.NSXml, this.Language);
            }

            foreach (object ob in this.ExtensionElements)
            {
                XmlNode node = ob as XmlNode;
                if (node != null)
                {
                    if (SkipNode(node))
                    {
                        continue;
                    }
                    Tracing.TraceInfo("Saving out additonal attributes..." + node.Name); 
                    node.WriteTo(writer);
                }
                else
                {
                    IExtensionElement ele = ob as IExtensionElement;
                    if (ele != null)
                    {
                        ele.Save(writer);
                    }
                }
            }

        }
        /////////////////////////////////////////////////////////////////////////////
         
        //////////////////////////////////////////////////////////////////////
        /// <summary>checks if this is a namespace 
        /// decl that we already added</summary> 
        /// <param name="node">XmlNode to check</param>
        /// <returns>true if this node should be skipped </returns>
        //////////////////////////////////////////////////////////////////////
        protected virtual bool SkipNode(XmlNode node)
        {
            if (node.NodeType == XmlNodeType.Attribute && 
                (String.Compare(node.Name, "xmlns")==0) && 
                (String.Compare(node.Value,BaseNameTable.NSAtom)==0))
                return true;
            return false; 
        }


        //////////////////////////////////////////////////////////////////////
        /// <summary>protected WriteElementStart(XmlWriter writer)</summary> 
        /// <param name="writer"> the xmlwriter to use</param>
        /// <param name="elementName"> the elementToPersist to use</param>
        //////////////////////////////////////////////////////////////////////
        static protected void WriteElementStart(XmlWriter writer, string elementName)
        {
            Tracing.Assert(writer != null, "writer should not be null");
            if (writer == null)
            {
                throw new ArgumentNullException("writer"); 
            }
            writer.WriteStartElement(elementName);
            Utilities.EnsureAtomNamespace(writer);
        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>empty base implementation</summary> 
        /// <param name="writer">the xmlwriter, where we want to add default namespaces to</param>
        //////////////////////////////////////////////////////////////////////
        protected virtual void AddOtherNamespaces(XmlWriter writer) 
        {
            Utilities.EnsureAtomNamespace(writer);
        }
        /////////////////////////////////////////////////////////////////////////////


        

        //////////////////////////////////////////////////////////////////////
        /// <summary>Writes out a LOCAL datetime in ISO 8601 format.
        /// </summary> 
        /// <param name="writer"> the xmlwriter to use</param>
        /// <param name="elementName"> the elementToPersist to use</param>
        /// <param name="dateTime"> the localDateTime to convert and persist</param>
        //////////////////////////////////////////////////////////////////////
        static protected void WriteLocalDateTimeElement(XmlWriter writer, string elementName, DateTime dateTime)
        {
            Tracing.Assert(writer != null, "writer should not be null");
            if (writer == null)
            {
                throw new ArgumentNullException("writer"); 
            }
            Tracing.Assert(elementName != null, "elementName should not be null");
            if (elementName == null)
            {
                throw new ArgumentNullException("elementName"); 
            }
            Tracing.Assert(elementName.Length > 0 , "elementName should be longer than null"); 

            // only save out if valid
            if (elementName.Length > 0 && Utilities.IsPersistable(dateTime))
            {

                string strOutput = Utilities.LocalDateTimeInUTC(dateTime);

                WriteElementStart(writer, elementName);
                writer.WriteString(strOutput);
                writer.WriteEndElement();
            }
        }
        /////////////////////////////////////////////////////////////////////////////



        //////////////////////////////////////////////////////////////////////
        /// <summary>empty base implementation</summary> 
        /// <param name="writer">the xmlwriter to save to</param>
        //////////////////////////////////////////////////////////////////////
        protected virtual void SaveInnerXml(XmlWriter writer) 
        {
        }
        /////////////////////////////////////////////////////////////////////////////



        //////////////////////////////////////////////////////////////////////
        /// <summary>helper method to encapsulate a string encoding, uses HTML encoding now</summary> 
        /// <param name="writer">the xml writer to write to</param> 
        /// <param name="content">the string to encode</param>
        //////////////////////////////////////////////////////////////////////
        static protected void WriteEncodedString(XmlWriter writer, string content)
        {
            if (writer == null)
            {
                throw new System.ArgumentNullException("writer", "No valid xmlWriter");
            }
            if (Utilities.IsPersistable(content))
            {
                string encoded = Utilities.EncodeString(content);
                writer.WriteString(encoded);
            }
        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>helper method to encapsulate a string encoding, uses HTML encoding now</summary>
        /// <param name="writer">the xml writer to write to</param> 
        /// <param name="content">the string to encode</param>
        //////////////////////////////////////////////////////////////////////
        static protected void WriteEncodedString(XmlWriter writer, AtomUri content)
        {
            if (writer == null)
            {
                throw new System.ArgumentNullException("writer", "No valid xmlWriter");
            }

            if (Utilities.IsPersistable(content))
            {
                string encoded = Utilities.EncodeString(content.ToString());
                writer.WriteString(encoded);
            }
        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>helper method to encapsulate encoding, uses HTML encoding now</summary> 
        /// <param name="writer">the xml writer to write to</param>
        /// <param name="attributeName">the attribute the write</param>
        /// <param name="content">the atomUri to encode</param>
        //////////////////////////////////////////////////////////////////////
        static protected void WriteEncodedAttributeString(XmlWriter writer, string attributeName, AtomUri content)
        {
            if (writer == null)
            {
                throw new System.ArgumentNullException("writer", "No valid xmlWriter");
            }
            if (attributeName == null)
            {
                throw new System.ArgumentNullException( "attributeName", "No valid attributename");
            }


            if (Utilities.IsPersistable(content))
            {
                string encoded = Utilities.EncodeString(content.ToString());
                writer.WriteAttributeString(attributeName, encoded);
            }
        }
        /////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////
        /// <summary>helper method to encapsulate encoding, uses HTML encoding now</summary> 
        /// <param name="writer">the xml writer to write to</param>
        /// <param name="attributeName">the attribute the write</param>
        /// <param name="content">the string to encode</param>
        //////////////////////////////////////////////////////////////////////
        static protected void WriteEncodedAttributeString(XmlWriter writer, string attributeName, string content)
        {
            if (Utilities.IsPersistable(content))
            {
                string encoded = Utilities.EncodeString(content);
                writer.WriteAttributeString(attributeName, encoded);
            }
        }
        /////////////////////////////////////////////////////////////////////////////


        //////////////////////////////////////////////////////////////////////
        /// <summary>helper method to encapsulate encoding, uses HTML encoding now</summary> 
        /// <param name="writer">the xml writer to write to</param>
        /// <param name="elementName">the attribute the write</param>
        /// <param name="content">the string to encode</param>
        //////////////////////////////////////////////////////////////////////
        static protected void WriteEncodedElementString(XmlWriter writer, string elementName, string content)
        {
            if (Utilities.IsPersistable(content))
            {
                string encoded = Utilities.EncodeString(content);
                writer.WriteElementString(elementName, BaseNameTable.NSAtom, encoded); 
            }
        }
        /////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////
        /// <summary>helper method to encapsulate encoding, uses HTML encoding now</summary> 
        /// <param name="writer">the xml writer to write to</param>
        /// <param name="elementName">the attribute the write</param>
        /// <param name="content">the string to encode</param>
        //////////////////////////////////////////////////////////////////////
        static protected void WriteEncodedElementString(XmlWriter writer, string elementName, AtomUri content)
        {
            if (Utilities.IsPersistable(content))
            {
                string encoded = Utilities.EncodeString(content.ToString());
                writer.WriteElementString(elementName, BaseNameTable.NSAtom, encoded); 
            }
        }
        /////////////////////////////////////////////////////////////////////////////

         


        /// <summary>Method to check whether object should be saved.
        /// This doesn't check whether the object is dirty; it only
        /// checks whether the XML content is worth saving.
        /// </summary> 
        public virtual bool ShouldBePersisted()
        {
            if (Utilities.IsPersistable(this.uriBase) || Utilities.IsPersistable(this.atomLanguageTag))
            {
                return true;
            }
            if (this.extensionsList != null && this.extensionsList.Count > 0 && this.extensionsList[0] != null)
            {
                return true;
            }
            return false;
        }

        #endregion 

    }
    /////////////////////////////////////////////////////////////////////////////
} 
/////////////////////////////////////////////////////////////////////////////
