/* Copyright (c) 2010 Google Inc.
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
using System.Collections.Generic;
using System.Text;
using Google.GData.Client;
using Google.GData.Extensions.Apps;

namespace Google.GData.Apps
{
    /// <summary>
    /// CalendarResourceService is a specialization of <code>AppsPropertyService</code> to help
    /// manage calendar resources in the domain.
    /// </summary>
    public class CalendarResourceService : AppsPropertyService
    {
        /// <summary>
        /// Constructs a CalendarResourceService instance for its clients.
        /// </summary>
        /// <param name="domain">the hosted domain</param>
        /// <param name="applicationName">the user application identifier</param>
        public CalendarResourceService(string domain, string applicationName)
            : base(domain, applicationName)
        {
        }

        /// <summary>
        /// Creates a new calendar resource. A 
        /// <a href="http://code.google.com/apis/apps/calendar_resource/docs/1.0/calendar_resource_developers_guide_protocol.html#naming_strategy">
        /// good naming strategy</a> is strongly suggested for resources.
        /// 
        /// </summary>
        /// <param name="resourceId">a unique name you give this resource. This is a required 
        /// property. The maximum length is 64 characters.</param>
        /// <param name="commonName">is the resource name seen by users in a calendar's resource
        /// list. This is an optional property when creating a resource, but is strongly 
        /// recommended. This name has a maximum of 100 characters.</param>
        /// <param name="description">a brief summary of this resource shown in the control panel.
        /// This is an optional property, but is strongly recommended. The description is limited 
        /// to a maximum of 1,000 characters.</param>
        /// <param name="type">is a general category common to several resources. This is an 
        /// optional property, but is strongly recommended. The type name has a maximum of 
        /// 100 characters.</param>
        /// <returns>newly created <code>AppsExtendedEntry</code> instance.</returns>
        public AppsExtendedEntry CreateCalendarResource(string resourceId, string commonName,
            string description, string type)
        {
            Uri calendarResourceUri = new Uri(String.Format("{0}/{1}",
                AppsCalendarResourceNameTable.AppsCalendarResourceBaseFeedUri, DomainName));
            AppsExtendedEntry entry = new AppsExtendedEntry();
            entry.Properties.Add(
                new PropertyElement(
                AppsCalendarResourceNameTable.resourceId, resourceId));
            if (!String.IsNullOrEmpty(commonName))
                entry.Properties.Add(
                    new PropertyElement(
                    AppsCalendarResourceNameTable.resourceCommonName, commonName));
            if (!String.IsNullOrEmpty(description))
                entry.Properties.Add(
                    new PropertyElement(
                    AppsCalendarResourceNameTable.resourceDescription, description));
            if (!String.IsNullOrEmpty(type))
                entry.Properties.Add(
                    new PropertyElement(
                    AppsCalendarResourceNameTable.resourceType, type));
            return base.Insert(calendarResourceUri, entry) as AppsExtendedEntry;
        }

        /// <summary>
        /// Deletes a calendar resource.
        /// </summary>
        /// <param name="resourceId">the unique ID of the resource to be deleted.</param>
        public void DeleteCalendarResource(string resourceId)
        {
            Uri calendarResourceUri = new Uri(String.Format("{0}/{1}/{2}",
                AppsCalendarResourceNameTable.AppsCalendarResourceBaseFeedUri, DomainName,
                resourceId));
            base.Delete(calendarResourceUri);
        }

        /// <summary>
        /// Retrieves a single calendar resource.
        /// </summary>
        /// <param name="resourceId">the unique id of the resource.</param>
        /// <returns></returns>
        public AppsExtendedEntry RetrieveCalendarResource(string resourceId)
        {
            return base.Get(String.Format("{0}/{1}/{2}",
                AppsCalendarResourceNameTable.AppsCalendarResourceBaseFeedUri, DomainName,
                resourceId)) as AppsExtendedEntry;
        }

        /// <summary>
        /// Retrieves a single page i.e 100 calendar resources.
        /// </summary>
        /// <param name="startKey">The resource ID of the starting entry. Use String.Empty for the
        /// firse page</param>
        /// <returns></returns>
        public AppsExtendedFeed RetrievePageOfCalendarResources(String startKey)
        {
            return QueryExtendedFeed(new Uri(String.Format("{0}/{1}/?start={2}",
                AppsCalendarResourceNameTable.AppsCalendarResourceBaseFeedUri, DomainName,
                startKey)));
        }

        /// <summary>
        /// Retrieves a feed containing all the calendar resource entries in the domain.
        /// <para>Warning: The feed cycles through all the pages and may take a long time. To
        /// retrieve a single page use <code>RetrievePageOfCalendarResources</code></para>
        /// </summary>
        /// <returns></returns>
        public AppsExtendedFeed RetrieveAllCalendarResources()
        {
            return QueryExtendedFeed(new Uri(String.Format("{0}/{1}/",
                AppsCalendarResourceNameTable.AppsCalendarResourceBaseFeedUri, DomainName)), true);
        }


        /// <summary>
        /// Updates a calendar resource. A 
        /// <a href="http://code.google.com/apis/apps/calendar_resource/docs/1.0/calendar_resource_developers_guide_protocol.html#naming_strategy">
        /// good naming strategy</a> is strongly suggested for resources.
        /// 
        /// </summary>
        /// <param name="resourceId">the resource Id of the resource to be updated</param>
        /// <param name="commonName">is the resource name seen by users in a calendar's resource
        /// list. This is an optional property when creating a resource, but is strongly 
        /// recommended. This name has a maximum of 100 characters.</param>
        /// <param name="description">a brief summary of this resource shown in the control panel.
        /// This is an optional property, but is strongly recommended. The description is limited 
        /// to a maximum of 1,000 characters.</param>
        /// <param name="type">is a general category common to several resources. This is an 
        /// optional property, but is strongly recommended. The type name has a maximum of 
        /// 100 characters.</param>
        /// <returns>newly created <code>AppsExtendedEntry</code> instance.</returns>
        public AppsExtendedEntry UpdateCalendarResource(string resourceId, string commonName,
            string description, string type)
        {
            Uri calendarResourceUri = new Uri(String.Format("{0}/{1}/{2}",
                AppsCalendarResourceNameTable.AppsCalendarResourceBaseFeedUri, DomainName, resourceId));
            AppsExtendedEntry entry = new AppsExtendedEntry();
            entry.EditUri = calendarResourceUri;
            entry.Properties.Add(
                new PropertyElement(
                AppsCalendarResourceNameTable.resourceId, resourceId));
            if (!String.IsNullOrEmpty(commonName))
                entry.Properties.Add(
                    new PropertyElement(
                    AppsCalendarResourceNameTable.resourceCommonName, commonName));
            if (!String.IsNullOrEmpty(description))
                entry.Properties.Add(
                    new PropertyElement(
                    AppsCalendarResourceNameTable.resourceDescription, description));
            if (!String.IsNullOrEmpty(type))
                entry.Properties.Add(
                    new PropertyElement(
                    AppsCalendarResourceNameTable.resourceType, type));
            return base.Update<AppsExtendedEntry>(entry);
        }
    }
}
