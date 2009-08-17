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
using Google.GData.Client;

namespace Google.GData.Health
{
	/// <summary>
	/// Represents an simplified atom entry author.
	/// </summary>
	public class Author
	{
		/// <summary>
		/// Gets or sets the email of the author.
		/// </summary>
		public string Email { get; set; }

		/// <summary>
		/// Gets or sets the name of the author.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the language for this author.
		/// </summary>
		public string Language { get; set; }

		/// <summary>
		/// Gets or sets the author's uri.
		/// </summary>
		public string Uri { get; set; }

		public static Author FromAtomPerson(AtomPerson person)
		{
			Author author = new Author();
			author.Uri = person.Uri.Content;
			author.Language = person.Language;	
			author.Email = person.Email;
			author.Name = person.Name;

			return author;
		}
	}
}
