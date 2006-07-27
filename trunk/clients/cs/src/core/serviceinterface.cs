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
using System.Net;
using System.IO;
using System.Xml;

#endregion

//////////////////////////////////////////////////////////////////////
// <summary>contains Service, the base interface that 
//   allows to query a service for different feeds
//  </summary>
//////////////////////////////////////////////////////////////////////
namespace Google.GData.Client
{
    //////////////////////////////////////////////////////////////////////
    /// <summary>base Service interface definition
    /// </summary> 
    //////////////////////////////////////////////////////////////////////
    public interface IService 
    {
        /// <summary>get/set for credentials to the service calls. Gets passed through to GDatarequest</summary> 
        ICredentials Credentials 
        {
            get;
            set;
        }
        /// <summary>get/set for the GDataRequestFactory object to use</summary> 
        IGDataRequestFactory RequestFactory
        {
            get;
            set;
        }
        /// <summary>the minimal Get OpenSearchRssDescription function</summary> 
        Stream QueryOpenSearchRssDescription(Uri serviceUri);

        /// <summary>the minimal query implementation</summary> 
        AtomFeed Query(FeedQuery feedQuery);
        /// <summary>the more sophisticated one</summary> 
        AtomEntry Update(AtomEntry entry);
        /// <summary>the more sophisticated one</summary> 
        AtomEntry Insert(AtomFeed feed, AtomEntry entry);
        /// <summary>the more sophisticated one</summary> 
        void Delete(AtomEntry entry);

    }


    //////////////////////////////////////////////////////////////////////
    /// <summary>the one that creates GDatarequests on the service
    /// </summary> 
    //////////////////////////////////////////////////////////////////////
    public interface IGDataRequestFactory
    {
        /// <summary>creation method for GDatarequests</summary> 
        IGDataRequest CreateRequest(GDataRequestType type, Uri uriTarget); 
    }
    //////////////////////////////////////////////////////////////////////

    //////////////////////////////////////////////////////////////////////
    /// <summary>enum to describe the different operations on the GDataRequest
    /// </summary> 
    //////////////////////////////////////////////////////////////////////
    public enum GDataRequestType
    {
        /// <summary>The request is used for query</summary>
        Query,                       
        /// <summary>The request is used for an insert</summary>
        Insert,                        
        /// <summary>The request is used for an update</summary>
        Update,                    
        /// <summary>The request is used for a delete</summary>
        Delete                        
    }
    /////////////////////////////////////////////////////////////////////////////

    

    //////////////////////////////////////////////////////////////////////
    /// <summary>Thin layer to abstract the request/response
    /// </summary> 
    //////////////////////////////////////////////////////////////////////
    public interface IGDataRequest 
    {
        /// <summary>get/set for credentials to the service calls. Get's passed through to GDatarequest</summary> 
        ICredentials Credentials 
        {
            get;
            set;
        }
        /// <summary>get's the request stream to write into</summary> 
        Stream GetRequestStream();
        /// <summary>Executes the request</summary> 
        void   Execute();
        /// <summary>get's the response stream to read from</summary> 
        Stream GetResponseStream();
    }

    //////////////////////////////////////////////////////////////////////
    /// <summary>Thin layer to create an action on an item/response
    /// </summary> 
    //////////////////////////////////////////////////////////////////////
    public interface IBaseWalkerAction
    {
        /// <summary>the only relevant method here</summary> 
        bool Go(AtomBase atom);
    }


    //////////////////////////////////////////////////////////////////////
    /// <summary>if an extension element is created and wants to persist itself,
    /// it needs to implement this interface
    /// </summary> 
    //////////////////////////////////////////////////////////////////////
    public interface IExtensionElement
    {
        /// <summary>the only relevant method here</summary> 
        void Save(XmlWriter writer);
    }





} 
/////////////////////////////////////////////////////////////////////////////
