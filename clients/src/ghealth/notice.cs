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
using System.Collections.Generic;
using System.Text;
using Google.GData.Health;
using Google.GData.Client;
using ASTM.Org.CCR;
using Google.GData.Extensions;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace Google.GData.Health
{
	/// <summary>
	/// Represents a simple user profile's notice.
	/// </summary>
	public class Notice
	{
		/// <summary>
		/// Creates a notice with the default content type of text.
		/// </summary>
		public Notice()
		{
			this.ContentType = "text";
		}

		/// <summary>
		/// Gets or sets the notice title.
		/// </summary>
		public string Subject { get; set; }

		/// <summary>
		/// Gets or sets the notice content.
		/// </summary>
		public string Content { get; set; }

		/// <summary>
		/// Gets or sets the notice content type.
		/// </summary>
		public string ContentType { get; set; }

		/// <summary>
		/// Gets the notice entry id.
		/// </summary>
		public string Id { get; set; }

		/// <summary>
		/// Gets the notice entry last updated datetime.
		/// </summary>
		public DateTime DateUpdated { get; set; }

		/// <summary>
		/// Gets the notice entry publishing datetime.
		/// </summary>
		public DateTime DatePublished { get; set; }

		/// <summary>
		/// Gets or sets the continuity of care record attached to this entry.
		/// </summary>
		public ContinuityOfCareRecord CareRecord { get; set; }

		/// <summary>
		/// Gets whether the notice content type to html.
		/// </summary>
		public bool IsHtml { get { return ContentType.Equals("html"); } }

		public static Notice FromAtomEntry(AtomEntry entry)
		{
			Notice notice = new Notice();
			notice.DatePublished = entry.Published;
			notice.DateUpdated = entry.Updated;
			notice.Id = entry.Id.Uri.Content;
			notice.ContentType = entry.Content.Type;
			notice.Content = entry.Content.Content;
			notice.Subject = entry.Title.Text;
			IExtensionElementFactory factory = entry.FindExtension("ContinuityOfCareRecord", "urn:astm-org:CCR");
			if (factory != null)
			{
				XmlExtension extension = factory as XmlExtension;
				XmlSerializer serializer = new XmlSerializer(typeof(ContinuityOfCareRecord));
				XmlTextReader reader = new XmlTextReader(new StringReader(extension.Node.OuterXml));
				notice.CareRecord = serializer.Deserialize(reader) as ContinuityOfCareRecord;
			}
			return notice;
		}
	}
}
