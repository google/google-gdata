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
using System.Globalization;
using System.ComponentModel;


#endregion

//////////////////////////////////////////////////////////////////////
// contains AtomId
//////////////////////////////////////////////////////////////////////
namespace Google.GData.Client
{

      /// <summary>enum to define the GDataBatchOperationType...</summary> 
    public enum GDataBatchOperationType
    {
        /// <summary>this is an insert operatoin</summary> 
        insert,
        /// <summary>this is an update operation</summary> 
        update,
        /// <summary>this is a delete operation</summary> 
        delete,
        /// <summary>this is a query operation</summary>
        query,
        /// <summary>the default (a no-op)</summary>
        Default
    }

    /// <summary>
    /// holds the batch status information
    /// </summary>
    public class GDataBatchStatus : IExtensionElement
    {
        private int code;
        private string reason; 
        private string contentType; 
        private string value;
        
        /// <summary>default value for the status code</summary>
        public static int CodeDefault = -1; 

        /// <summary>
        /// set's the defaults for code
        /// </summary>
        public GDataBatchStatus()
        {
            this.Code = CodeDefault; 
        }
        //////////////////////////////////////////////////////////////////////
        /// <summary>returns the status code of the operation</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public int Code
        {
            get {return this.code;}
            set {this.code = value;}
        }
        // end of accessor public string Code

        //////////////////////////////////////////////////////////////////////
        /// <summary>accessor method public string Reason</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public string Reason
        {
            get {return this.reason;}
            set {this.reason = value;}
        }
        // end of accessor public string Reason


        //////////////////////////////////////////////////////////////////////
        /// <summary>accessor method public string ContentType</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public string ContentType
        {
            get {return this.contentType;}
            set {this.contentType = value;}
        }
        // end of accessor public string ContentType

        //////////////////////////////////////////////////////////////////////
        /// <summary>accessor method public string Value</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public string Value
        {
            get {return this.value;}
            set {this.value = value;}
        }
        // end of accessor public string Value

        /* disabled for now
        //////////////////////////////////////////////////////////////////////
        /// <summary>the error list</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public GDataBatchErrorCollection Errors
        {
            get {return this.errorList;}
            set {this.errorList = value;}
        }
        // end of accessor Errors

        */ 
    
        #region Persistence overloads
        /// <summary>
        /// Persistence method for the GDataBatchStatus object
        /// </summary>
        /// <param name="writer">the xmlwriter to write into</param>
        public void Save(XmlWriter writer)
        {
            writer.WriteStartElement(BaseNameTable.gBatchPrefix, BaseNameTable.XmlElementBatchStatus, BaseNameTable.gBatchPrefix); 

            if (this.Code != GDataBatchStatus.CodeDefault) 
            {
                writer.WriteAttributeString(BaseNameTable.XmlAttributeBatchStatusCode, this.Code.ToString()); 
            }
            if (Utilities.IsPersistable(this.ContentType)) 
            {
                writer.WriteAttributeString(BaseNameTable.XmlAttributeBatchContentType, this.ContentType);
            }
            if (Utilities.IsPersistable(this.Reason))
            {
                writer.WriteAttributeString(BaseNameTable.XmlAttributeBatchReason, this.Reason); 
            }
            if (Utilities.IsPersistable(this.Value))
            {
                string encoded = Utilities.EncodeString(this.Value);
                writer.WriteString(encoded);
            }
            writer.WriteEndElement();
        }
        #endregion
     }

    /// <summary>
    ///  represents the Error element in the GDataBatch response
    /// </summary>
    public class GDataBatchError : IExtensionElement
    {
        private string errorType;
        private string errorReason;
        private string field; 

        //////////////////////////////////////////////////////////////////////
        /// <summary>accessor method Type</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public string Type
        {
            get {return this.errorType;}
            set {this.errorType = value;}
        }
        // end of accessor Type


        //////////////////////////////////////////////////////////////////////
        /// <summary>accessor method public string Field</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public string Field
        {
            get {return this.field;}
            set {this.field = value;}
        }
        // end of accessor public string Field

        //////////////////////////////////////////////////////////////////////
        /// <summary>accessor method public string Reason</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public string Reason
        {
            get {return this.errorReason;}
            set {this.errorReason = value;}
        }
        // end of accessor public string Reason

        #region Persistence overloads
        /// <summary>
        /// Persistence method for the GDataBatchError object
        /// </summary>
        /// <param name="writer">the xmlwriter to write into</param>
        public void Save(XmlWriter writer)
        {
        }
        #endregion
    }

    /// <summary>
    /// holds the batch status information
    /// </summary>
    public class GDataBatchInterrupt : IExtensionElement
    {
        private string reason; 
        private int    success; 
        private int    failures;
        private int    parsed; 
        private int    unprocessed; 


        //////////////////////////////////////////////////////////////////////
        /// <summary>accessor method public string Reason</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public string Reason
        {
            get {return this.reason;}
            set {this.reason = value;}
        }
        // end of accessor public string Reason

        //////////////////////////////////////////////////////////////////////
        /// <summary>accessor method public int Successes</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public int Successes
        {
            get {return this.success;}
            set {this.success = value;}
        }
        // end of accessor public int Success


        //////////////////////////////////////////////////////////////////////
        /// <summary>accessor method public int Failures</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public int Failures
        {
            get {return this.failures;}
            set {this.failures = value;}
        }
        // end of accessor public int Failures

        //////////////////////////////////////////////////////////////////////
        /// <summary>accessor method public int Unprocessed</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public int Unprocessed
        {
            get {return this.unprocessed;}
            set {this.unprocessed= value;}
        }
        // end of accessor public int Unprocessed

        //////////////////////////////////////////////////////////////////////
        /// <summary>accessor method public int Parsed</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public int Parsed
        {
            get {return this.parsed;}
            set {this.parsed = value;}
        }
        // end of accessor public int Parsed

        #region Persistence overloads
        /// <summary>
        /// Persistence method for the GDataBatchInterrupt object
        /// </summary>
        /// <param name="writer">the xmlwriter to write into</param>
        public void Save(XmlWriter writer)
        {
        }
        #endregion
    }


    //////////////////////////////////////////////////////////////////////
    /// <summary>The GDataFeedBatch object holds batch related information
    /// for the AtomFeed
    /// </summary> 
    //////////////////////////////////////////////////////////////////////
    public class GDataBatchFeedData : IExtensionElement
    {
        private GDataBatchOperationType operationType; 
        /// <summary>
        /// constructor, set's the default for the operation type
        /// </summary>
        public GDataBatchFeedData()
        {
            this.operationType = GDataBatchOperationType.Default;
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>accessor method public GDataBatchOperationType Type</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public GDataBatchOperationType Type
        {
            get {return this.operationType;}
            set {this.operationType = value;}
        }
        // end of accessor public GDataBatchOperationType Type


        #region Persistence overloads
        /// <summary>
        /// Persistence method for the GDataBatch object
        /// </summary>
        /// <param name="writer">the xmlwriter to write into</param>
        public void Save(XmlWriter writer)
        {
            if (this.Type != GDataBatchOperationType.Default) 
            {
                writer.WriteStartElement(BaseNameTable.gBatchPrefix, BaseNameTable.XmlElementBatchOperation, BaseNameTable.gBatchNamespace); 
                writer.WriteAttributeString(BaseNameTable.XmlAttributeType, this.operationType.ToString());           
                writer.WriteEndElement(); 
            }
        }
        #endregion


    }
    /////////////////////////////////////////////////////////////////////////////

    //////////////////////////////////////////////////////////////////////
    /// <summary>The GDataEntryBatch object holds batch related information\
    /// for an AtomEntry
    /// </summary> 
    //////////////////////////////////////////////////////////////////////
    public class GDataBatchEntryData : IExtensionElement
    {
        private GDataBatchOperationType operationType; 
        private string id; 
        private GDataBatchStatus status; 
        private GDataBatchInterrupt interrupt; 

        /// <summary>
        /// constructor, sets the default for the operation type
        /// </summary>
        public GDataBatchEntryData()
        {
            this.operationType = GDataBatchOperationType.Default;
        }

        /// <summary>
        /// Constructor for the batch data
        /// </summary>
        /// <param name="type">The batch operation to be performed</param>
        public GDataBatchEntryData(GDataBatchOperationType type)
        {
            this.Type = type;
        }


        /// <summary>
        /// Constructor for batch data
        /// </summary>
        /// <param name="ID">The batch ID of this entry</param>
        /// <param name="type">The batch operation to be performed</param>
        public GDataBatchEntryData(string ID, GDataBatchOperationType type) : this(type)
        {
            this.Id = ID;
        }

    
        //////////////////////////////////////////////////////////////////////
        /// <summary>accessor method public GDataBatchOperationType Type</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public GDataBatchOperationType Type
        {
            get {return this.operationType;}
            set {this.operationType = value;}
        }
        // end of accessor public GDataBatchOperationType Type

        //////////////////////////////////////////////////////////////////////
        /// <summary>accessor method public string Id</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public string Id
        {
            get {return this.id;}
            set {this.id = value;}
        }
        // end of accessor public string Id

        
        //////////////////////////////////////////////////////////////////////
        /// <summary>accessor for the GDataBatchInterrrupt element</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public GDataBatchInterrupt Interrupt
        {
            get {return this.interrupt;}
            set {this.interrupt= value;}
        }
        // end of accessor public Interrupt


        //////////////////////////////////////////////////////////////////////
        /// <summary>accessor method public GDataBatchStatus Status</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public GDataBatchStatus Status
        {
            get {
                if (this.status == null)
                {
                    this.status = new GDataBatchStatus();
                }
                return this.status; 
            }
            set {this.status = value;}
        }
        // end of accessor public GDataBatchStatus Status


    
        #region Persistence overloads
        /// <summary>
        /// Persistence method for the GDataEntryBatch object
        /// </summary>
        /// <param name="writer">the xmlwriter to write into</param>
        public void Save(XmlWriter writer)
        {
            if (this.Id != null) 
            {
                writer.WriteElementString(BaseNameTable.XmlElementBatchId, BaseNameTable.gBatchNamespace, this.id);
            }
            if (this.Type != GDataBatchOperationType.Default) 
            {
                writer.WriteStartElement(BaseNameTable.gBatchPrefix, BaseNameTable.XmlElementBatchOperation, BaseNameTable.gBatchNamespace); 
                writer.WriteAttributeString(BaseNameTable.XmlAttributeType, this.operationType.ToString());           
                writer.WriteEndElement(); 
            }            
            if (this.status != null)
            {
                this.status.Save(writer);
            }
        }
        #endregion


    }
    /////////////////////////////////////////////////////////////////////////////

} 
/////////////////////////////////////////////////////////////////////////////
