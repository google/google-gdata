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
	/// Represents a simple user profile in Google Health.
	/// </summary>
	public class Profile
	{
		/// <summary>
		/// Gets or sets the profile name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the profile id.
		/// </summary>
		public string ID { get; set; }

		/// <summary>
		/// Gets or sets the authors of this profile.
		/// </summary>
		public List<Author> Authors { get; set; }

		public static Profile FromAtomEntry(AtomEntry entry)
		{
			Profile profile = new Profile();
			profile.ID = entry.Content.Content;
			profile.Name = entry.Title.Text;
			profile.Authors = new List<Author>();
			foreach (AtomPerson person in entry.Authors)
			{
				profile.Authors.Add(Author.FromAtomPerson(person));
			}

			return profile;
		}
	}
}
