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
using System.Collections;
using System.Text;
using System.Xml;
using Google.GData.Client;
using Google.GData.Extensions;

namespace Google.GData.Photos {

 
    //////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Picasa Web Albums provides a variety of representations of photo- 
    /// and album-related data. There are three independent axes for 
    /// specifying what you want when you request data: visibility, projection, and path/kind.
    /// Visibility values let you request data at various levels of sharing. 
    /// For example, a visibility value of public requests publicly visible data. 
    /// For a list of values, see Visibility values, below. The default visibility 
    /// depends on whether the request is authenticated or not.
    /// Projection values let you indicate what elements and extensions 
    /// should appear in the feed you're requesting. For example, a projection 
    /// value of base indicates that the representation is a basic Atom feed 
    /// without any extension elements, suitable for display in an Atom reader. 
    /// You must specify a projection value. Path and kind values let you indicate what 
    /// type of items you want information about. For example, a path of user/liz 
    /// and a kind value of tag requests a feed of tags associated with the 
    /// user whose username is liz. Path and kind values are separate parts of the 
    /// URI, but they're used together to indicate the item type(s) desired. 
    /// You must specify a path, but kind is optional; the default kind depends on the path.
    /// To request a particular representation, you specify a visibility value, 
    /// a projection value, and a path and kind in the URI that you send 
    /// to Picasa Web Albums.
    /// </summary>
    //////////////////////////////////////////////////////////////////////
    public class PicasaFeed : AbstractFeed
    {

        /// <summary>
        ///  default constructor
        /// </summary>
        /// <param name="uriBase">the base URI of the feedEntry</param>
        /// <param name="iService">the Service to use</param>
        public PicasaFeed(Uri uriBase, IService iService) : base(uriBase, iService)
        {
            GPhotoExtensions.AddExtension(this);
        }

        /// <summary>
        /// this needs to get implemented by subclasses
        /// </summary>
        /// <returns>AtomEntry</returns>
        public override AtomEntry CreateFeedEntry()
        {
            return new PicasaEntry();
        }

        /// <summary>
        /// get's called after we already handled the custom entry, to handle all 
        /// other potential parsing tasks
        /// </summary>
        /// <param name="e"></param>
        /// <param name="parser">the atom feed parser used</param>
        protected override void HandleExtensionElements(ExtensionElementEventArgs e, AtomFeedParser parser)
        {
            base.HandleExtensionElements(e, parser);
        }
    }
}
