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
#define USE_TRACING

using System;
using Google.GData.Client;
using Google.GData.Extensions;


namespace Google.GData.Contacts {


    //////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Entry API customization class for defining entries in an Event feed.
    /// </summary>
    //////////////////////////////////////////////////////////////////////
    public class ContactEntry : AbstractEntry, IContainsDeleted
    {

        /// <summary>
        /// default contact term string for the contact relationship link
        /// </summary>
        public static string ContactTerm = "http://schemas.google.com/contact/2008#contact";
        /// <summary>
        /// Category used to label entries that contain contact extension data.
        /// </summary>
        public static AtomCategory CONTACT_CATEGORY =
        new AtomCategory(ContactEntry.ContactTerm, new AtomUri(BaseNameTable.gKind));


        private EMailCollection emails;
        private IMCollection ims;
        private PhonenumberCollection phonenumbers;
        private PostalAddressCollection postals;
        private OrganizationCollection organizations;


        /// <summary>
        /// Constructs a new ContactEntry instance with the appropriate category
        /// to indicate that it is an event.
        /// </summary>
        public ContactEntry()
        : base()
        {
            Tracing.TraceMsg("Created Contact Entry");
            Categories.Add(CONTACT_CATEGORY);
            ContactsExtensions.AddExtension(this);
        }

        /// <summary>
        /// convienience accessor to find the primary Email
        /// there is no setter, to change this use the Primary Flag on 
        /// an individual object
        /// </summary>
        public EMail PrimaryEmail
        {
            get
            {
                foreach (EMail e in this.Emails)
                {
                    if (e.Primary == true)
                    {
                        return e;
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// convienience accessor to find the primary Phonenumber
        /// there is no setter, to change this use the Primary Flag on 
        /// an individual object
        /// </summary>
        public PhoneNumber PrimaryPhonenumber
        {
            get
            {
                foreach (PhoneNumber p in this.Phonenumbers)
                {
                    if (p.Primary == true)
                    {
                        return p;
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// convienience accessor to find the primary PostalAddress
        /// there is no setter, to change this use the Primary Flag on 
        /// an individual object
        /// </summary>
        public PostalAddress PrimaryPostalAddress
        {
            get
            {
                foreach (PostalAddress p in this.PostalAddresses)
                {
                    if (p.Primary == true)
                    {
                        return p;
                    }
                }
                return null;
            }
        }
        
        /// <summary>
        /// convienience accessor to find the primary IMAddress
        /// there is no setter, to change this use the Primary Flag on 
        /// an individual object
        /// </summary>
        public IMAddress PrimaryIMAddress
        {
            get
            {
                foreach (IMAddress im in this.IMs)
                {
                    if (im.Primary == true)
                    {
                        return im;
                    }
                }
                return null;
            }
        }


        /// <summary>
        /// getter/setter for the email extension element
        /// </summary>
        public EMailCollection Emails
        {
            get 
            {
                if (this.emails == null)
                {
                    this.emails =  new EMailCollection(this); 
                }
                return this.emails;
            }
        }

        /// <summary>
        /// getter/setter for the IM extension element
        /// </summary>
        public IMCollection IMs
        {
            get 
            {
                if (this.ims == null)
                {
                    this.ims =  new IMCollection(this); 
                }
                return this.ims;
            }
        }

        /// <summary>
        /// returns the phonenumber collection
        /// </summary>
        public PhonenumberCollection Phonenumbers
        {
            get 
            {
                if (this.phonenumbers == null)
                {
                    this.phonenumbers =  new PhonenumberCollection(this); 
                }
                return this.phonenumbers;
            }
        }

        /// <summary>
        /// returns the phonenumber collection
        /// </summary>
        public PostalAddressCollection PostalAddresses
        {
            get 
            {
                if (this.postals == null)
                {
                    this.postals =  new PostalAddressCollection(this); 
                }
                return this.postals;
            }
        }

        /// <summary>
        /// returns the phonenumber collection
        /// </summary>
        public OrganizationCollection Organizations
        {
            get 
            {
                if (this.organizations == null)
                {
                    this.organizations =  new OrganizationCollection(this); 
                }
                return this.organizations;
            }
        }
        /// <summary>
        /// if this is a previously deleted contact, returns true
        /// to delete a contact, use the delete method
        /// </summary>
        public bool Deleted
        {
            get
            {
                if (FindExtension(GDataParserNameTable.XmlDeletedElement,
                                     BaseNameTable.gNamespace) != null) 
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// retrieves the Uri of the Photo Link. To set this, you need to create an AtomLink object
        /// and add/replace it in the atomlinks colleciton. 
        /// </summary>
        /// <returns></returns>
        public Uri PhotoUri
        {
            get 
            {
                AtomLink link = this.Links.FindService(GDataParserNameTable.ServicePhoto, null);
                return link == null ? null : new Uri(link.HRef.ToString());
            }
        }

        /// <summary>
        /// retrieves the Uri of the Photo Edit Link. To set this, you need to create an AtomLink object
        /// and add/replace it in the atomlinks colleciton. 
        /// </summary>
        /// <returns></returns>
        public Uri PhotoEditUri
        {
            get 
            {
                AtomLink link = this.Links.FindService(GDataParserNameTable.ServicePhotoEdit, null);
                return link == null ? null : new Uri(link.HRef.ToString());
            }
        }
    }
}

