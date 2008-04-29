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
using System.Xml;
using Google.GData.Client;

namespace Google.GData.Extensions 
{

    /// <summary>
    /// The gd:rating tag specifies the rating that you are assigning to a resource (in a request to add a rating) 
    /// or the current average rating of the resource based on aggregated user ratings
    /// </summary>
    public class Rating : SimpleElement
    {
        /// <summary>
        /// default constructor for gd:rating
        /// </summary>
        public Rating()
        : base(GDataParserNameTable.XmlRatingElement, 
               GDataParserNameTable.gDataPrefix,
               GDataParserNameTable.gNamespace)
        {
            this.Attributes.Add(GDataParserNameTable.XmlAttributeMin, null);
            this.Attributes.Add(GDataParserNameTable.XmlAttributeMax, null);
            this.Attributes.Add(GDataParserNameTable.XmlAttributeNumRaters, null);
            this.Attributes.Add(GDataParserNameTable.XmlAttributeAverage, null);
        }

        /// <summary>
        /// The min attribute specifies the minimum rating that can be assigned to a resource. This value must be 1.
        /// </summary>
        /// <returns></returns>
        public string Min
        {
            get
            {
                return this.Attributes[GDataParserNameTable.XmlAttributeMin] as string;
            }
            set
            {
                this.Attributes[GDataParserNameTable.XmlAttributeMin] = value;
            }
        }

        /// <summary>
        /// The max attribute specifies the maximum rating that can be assigned to a resource. This value must be 5.
        /// </summary>
        /// <returns></returns>
        public string Max
        {
            get
            {
                return this.Attributes[GDataParserNameTable.XmlAttributeMax] as string;
            }
            set
            {
                this.Attributes[GDataParserNameTable.XmlAttributeMax] = value;
            }
        }

        /// <summary>
        /// The numRaters attribute indicates how many people have rated the resource. This attribute is not used 
        /// in a request to add a rating
        /// </summary>
        /// <returns></returns>
        public string NumRaters
        {
            get
            {
                return this.Attributes[GDataParserNameTable.XmlAttributeNumRaters] as string;
            }
            set
            {
                this.Attributes[GDataParserNameTable.XmlAttributeNumRaters] = value;
            }
        }

        /// <summary>
        /// The average attribute indicates the average rating given to the resource.
        /// This attribute is not used in a request to add a rating.
        /// </summary>
        /// <returns></returns>
        public string Average
        {
            get
            {
                return this.Attributes[GDataParserNameTable.XmlAttributeAverage] as string;
            }
            set
            {
                this.Attributes[GDataParserNameTable.XmlAttributeAverage] = value;
            }
        }


    }
}
