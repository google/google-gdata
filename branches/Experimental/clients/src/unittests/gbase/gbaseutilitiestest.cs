/* Copyright (c) 2006-2008 Google Inc.
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
/* Change history
 * Oct 13 2008  Joe Feser       joseph.feser@gmail.com
 * Converted ArrayLists and other .NET 1.1 collections to use Generics
 * Combined IExtensionElement and IExtensionElementFactory interfaces
 * 
 */
using System;
using System.IO;
using System.Xml;
using System.Collections;
using System.Configuration;
using System.Net;
using NUnit.Framework;
using Google.GData.Client;
using Google.GData.GoogleBase;


namespace Google.GData.GoogleBase.UnitTests
{

    [TestFixture]
    [Category("GoogleBase")]
    public class GBaseUtilitiesTest
    {

        [Test]
        [Ignore("We no longer allow items to be added that are not an extension.")]
        public void GetSetExtensionTest()
        {
            //Dummy x = new Dummy("x");
            //Dummy y = new Dummy("y");
            //ExtensionList list = ExtensionList.NotVersionAware();
            ////list.Add("hello");
            ////list.Add("world");

            //Assert.IsNull(GBaseUtilities.GetExtension(list, typeof(Dummy)));

            //GBaseUtilities.SetExtension(list, typeof(Dummy), x);
            //Assert.AreEqual(x, GBaseUtilities.GetExtension(list, typeof(Dummy)));

            //GBaseUtilities.SetExtension(list, typeof(Dummy), y);
            //Assert.AreEqual(y, GBaseUtilities.GetExtension(list, typeof(Dummy)));

            //GBaseUtilities.SetExtension(list, typeof(Dummy), null);
            //Assert.IsNull(GBaseUtilities.GetExtension(list, typeof(Dummy)));
        }

        struct Dummy
        {
            public readonly string name;

            public Dummy(string name)
            {
                this.name = name;
            }

            public override string ToString()
            {
                return name;
            }
        }

    }

}
