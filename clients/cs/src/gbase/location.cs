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
using System;
using System.Collections;
using System.Text;
#endregion

namespace Google.GData.GoogleBase {

    ///////////////////////////////////////////////////////////////////////
    /// <summary>Object representation of a location; an address and
    /// optionally latitude and longitude.</summary>
    ///////////////////////////////////////////////////////////////////////
    public class Location
    {
        private bool hasCoordinates;
        private float latitude;
        private float longitude;
        private string address;

        //////////////////////////////////////////////////////////////////////
        /// <summary>Creates an address location.</summary>
        //////////////////////////////////////////////////////////////////////
        public Location(string address) {
            this.address = address;
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>Creates a location with latitude, longitude and address.
        /// </summary>
        //////////////////////////////////////////////////////////////////////
        public Location(string address, float latitude, float longitude) {
            this.address = address;
            this.latitude = latitude;
            this.longitude = longitude;
            this.hasCoordinates = true;
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>Extracts location information from an attribute.
        /// </summary>
        /// <exception cref="FormatException">If the attribute contains
        /// invalid sub-elements or lacks required sub-elements</exception>
        //////////////////////////////////////////////////////////////////////
        public Location(GBaseAttribute attribute)
        {
            this.address = attribute.Content;
            if (attribute["latitude"] != null && attribute["longitude"] != null)
            {
                this.latitude = NumberFormat.ToFloat(attribute["latitude"]);
                this.longitude = NumberFormat.ToFloat(attribute["longitude"]);
                this.hasCoordinates = true;
            }
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>Creates a GBaseAttribute of type location corresponding
        /// to the current object.</summary>
        /// <param name="name">attribute name</param>
        //////////////////////////////////////////////////////////////////////
        public GBaseAttribute CreateGBaseAttribute(string name)
        {
            GBaseAttribute retval = new GBaseAttribute(name, GBaseAttributeType.Location);
            retval.Content = address;
            if (hasCoordinates)
            {
                retval["latitude"] = NumberFormat.ToString(latitude);
                retval["longitude"] = NumberFormat.ToString(longitude);
            }
            return retval;
        }


        //////////////////////////////////////////////////////////////////////
        /// <summary>Checks whether coordinates have been defined.</summary>
        /// <returns>true if coordinates have been defined.</returns>
        //////////////////////////////////////////////////////////////////////
        public bool HasCoordinates
        {
            get
            {
                return hasCoordinates;
            }
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>Unset latitude and longitude.</summary>
        //////////////////////////////////////////////////////////////////////
        public void clearCoordinates() {
            hasCoordinates = false;
            this.latitude = 0.0f;
            this.longitude = 0.0f;
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>Address</summary>
        //////////////////////////////////////////////////////////////////////
        public string Address
        {
            get
            {
                return address;
            }

            set
            {
                this.address = value;
            }
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>Latitude</summary>
        //////////////////////////////////////////////////////////////////////
        public float Latitude
        {
            get
            {
                AssertHasCoordinates();
                return latitude;
            }

            set
            {
                this.latitude = value;
                this.hasCoordinates = true;
            }
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>Longitude</summary>
        //////////////////////////////////////////////////////////////////////
        public float Longitude
        {
            get
            {
                AssertHasCoordinates();
                return longitude;
            }

            set
            {
                this.longitude = value;
                this.hasCoordinates = true;
            }
        }

        private void AssertHasCoordinates()
        {
            if (!hasCoordinates)
            {
                throw new System.InvalidOperationException("No coordinates have been defined. " +
                                                           "(Check with hasCoordinates() first)");
            }
        }

    }

}
