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
/* Change history
* Oct 13 2008  Joe Feser       joseph.feser@gmail.com
* Converted ArrayLists and other .NET 1.1 collections to use Generics
* Combined IExtensionElement and IExtensionElementFactory interfaces
* 
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
    public class ContactEntry : BaseContactEntry
    {

        /// <summary>
        /// default contact term string for the contact relationship link
        /// </summary>
        public static string ContactTerm = "http://schemas.google.com/contact/2008#contact";
        /// <summary>`
        /// Category used to label entries that contain contact extension data.
        /// </summary>
        public static AtomCategory CONTACT_CATEGORY =
        new AtomCategory(ContactEntry.ContactTerm, new AtomUri(BaseNameTable.gKind));


        private ExtensionCollection<EMail> emails;
        private ExtensionCollection<IMAddress> ims;
        private ExtensionCollection<PhoneNumber> phonenumbers;
        private ExtensionCollection<StructuredPostalAddress> structuredAddress;
        private ExtensionCollection<Organization> organizations;
        private ExtensionCollection<GroupMembership> groups;
        private ExtensionCollection<CalendarLink> calendars;
        private ExtensionCollection<Event> events;
        private ExtensionCollection<ExternalId> externalIds;
        private ExtensionCollection<Hobby> hobbies;
        private ExtensionCollection<Jot> jots;
        private ExtensionCollection<Language> languages;
        private ExtensionCollection<Relation> relations;
        private ExtensionCollection<UserDefinedField> userDefinedFiels;
        private ExtensionCollection<Website> websites;





        /// <summary>
        /// Constructs a new ContactEntry instance with the appropriate category
        /// to indicate that it is an event.
        /// </summary>
        public ContactEntry()
        : base()
        {
            Tracing.TraceMsg("Created Contact Entry");
            Categories.Add(CONTACT_CATEGORY);
            this.AddExtension(new GroupMembership());
            this.AddExtension(new Where());
            ContactsKindExtensions.AddExtension(this);

            // colletions
            this.AddExtension(new CalendarLink());
            this.AddExtension(new Event());
            this.AddExtension(new ExternalId());
            this.AddExtension(new Hobby());
            this.AddExtension(new Jot());
            this.AddExtension(new Language());
            this.AddExtension(new Relation());
            this.AddExtension(new UserDefinedField());
            this.AddExtension(new Website());

            // singletons
            this.AddExtension(new BillingInformation());
            this.AddExtension(new Birthday());
            this.AddExtension(new DirectoryServer());
            this.AddExtension(new Initials());
            this.AddExtension(new MaidenName());
            this.AddExtension(new Mileage());
            this.AddExtension(new Nickname());
            this.AddExtension(new Occupation());
            this.AddExtension(new Priority());
            this.AddExtension(new Sensitivity());
            this.AddExtension(new ShortName());
            this.AddExtension(new Subject());
           
        }


        /// <summary>
        /// typed override of the Update method
        /// </summary>
        /// <returns></returns>
        public new ContactEntry Update()
        {
            return base.Update() as ContactEntry;
        }


        /// <summary>
        /// Location associated with the contact
        /// </summary>
        /// <returns></returns>
        public String Location
        {
            get 
            {
                Where w = FindExtension(GDataParserNameTable.XmlWhereElement, BaseNameTable.gNamespace) as Where;
                return w != null ? w.ValueString : null;
            }
            set
            { 
                Where w = null;
                if (value != null)
                {
                    w = new Where(null, null, value);
                }
                ReplaceExtension(GDataParserNameTable.XmlWhereElement, BaseNameTable.gNamespace, w);
            }
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
                    if (e.Primary)
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
                    if (p.Primary)
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
        public StructuredPostalAddress PrimaryPostalAddress
        {
            get
            {
                foreach (StructuredPostalAddress p in this.PostalAddresses)
                {
                    if (p.Primary)
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
                    if (im.Primary)
                    {
                        return im;
                    }
                }
                return null;
            }
        }




        /// <summary>
        /// returns the groupmembership info on this object
        /// </summary>
        /// <returns></returns>
        public ExtensionCollection<GroupMembership> GroupMembership
        {
            get 
            {
                if (this.groups == null)
                {
                    this.groups = new ExtensionCollection<GroupMembership>(this); 
                }
                return this.groups;
            }
        }



        /// <summary>
        /// getter/setter for the email extension element
        /// </summary>
        public ExtensionCollection<EMail> Emails
        {
            get 
            {
                if (this.emails == null)
                {
                    this.emails = new ExtensionCollection<EMail>(this); 
                }
                return this.emails;
            }
        }

        /// <summary>
        /// getter/setter for the IM extension element
        /// </summary>
        public ExtensionCollection<IMAddress> IMs
        {
            get 
            {
                if (this.ims == null)
                {
                    this.ims = new ExtensionCollection<IMAddress>(this); 
                }
                return this.ims;
            }
        }

        /// <summary>
        /// returns the phonenumber collection
        /// </summary>
        public ExtensionCollection<PhoneNumber> Phonenumbers
        {
            get 
            {
                if (this.phonenumbers == null)
                {
                    this.phonenumbers = new ExtensionCollection<PhoneNumber>(this); 
                }
                return this.phonenumbers;
            }
        }

        /// <summary>
        /// Postal address split into components. It allows to store the address in locale independent format. 
        /// The fields can be interpreted and used to generate formatted, locale dependent address
        /// </summary>
        public ExtensionCollection<StructuredPostalAddress> PostalAddresses
        {
            get 
            {
                if (this.structuredAddress == null)
                {
                    this.structuredAddress = new ExtensionCollection<StructuredPostalAddress>(this); 
                }
                return this.structuredAddress;
            }
        }

        /// <summary>
        /// returns the phonenumber collection
        /// </summary>
        public ExtensionCollection<Organization> Organizations
        {
            get 
            {
                if (this.organizations == null)
                {
                    this.organizations = new ExtensionCollection<Organization>(this); 
                }
                return this.organizations;
            }
        }

        /// <summary>
        /// getter for the CalendarLink collections
        /// </summary>
        public ExtensionCollection<CalendarLink> Calendars
        {
            get 
            {
                if (this.calendars == null)
                {
                    this.calendars = new ExtensionCollection<CalendarLink>(this); 
                }
                return this.calendars;
            }
        } 

         /// <summary>
        /// getter for the Events collections
        /// </summary>
        public ExtensionCollection<Event> Events
        {
            get 
            {
                if (this.events == null)
                {
                    this.events = new ExtensionCollection<Event>(this); 
                }
                return this.events;
            }
        }

         /// <summary>
        /// getter for the externalids collections
        /// </summary>
        public ExtensionCollection<ExternalId> ExternalIds
        {
            get 
            {
                if (this.externalIds == null)
                {
                    this.externalIds = new ExtensionCollection<ExternalId>(this); 
                }
                return this.externalIds;
            }
        }

         /// <summary>
        /// getter for the Hobby collections
        /// </summary>
        public ExtensionCollection<Hobby> Hobbies
        {
            get 
            {
                if (this.hobbies == null)
                {
                    this.hobbies = new ExtensionCollection<Hobby>(this); 
                }
                return this.hobbies;
            }
        }

         /// <summary>
        /// getter for the Jot collections
        /// </summary>
        public ExtensionCollection<Jot> Jots
        {
            get 
            {
                if (this.jots == null)
                {
                    this.jots = new ExtensionCollection<Jot>(this); 
                }
                return this.jots;
            }
        }

         /// <summary>
        /// getter for the languages collections
        /// </summary>
        public ExtensionCollection<Language> Languages
        {
            get 
            {
                if (this.languages == null)
                {
                    this.languages = new ExtensionCollection<Language>(this); 
                }
                return this.languages;
            }
        }

         /// <summary>
        /// getter for the Relation collections
        /// </summary>
        public ExtensionCollection<Relation> Relations
        {
            get 
            {
                if (this.relations == null)
                {
                    this.relations = new ExtensionCollection<Relation>(this); 
                }
                return this.relations;
            }
        }

        /// <summary>
        /// getter for the UserDefinedField collections
        /// </summary>
        public ExtensionCollection<UserDefinedField> UserDefinedFields
        {
            get 
            {
                if (this.userDefinedFiels == null)
                {
                    this.userDefinedFiels = new ExtensionCollection<UserDefinedField>(this); 
                }
                return this.userDefinedFiels;
            }
        }

        /// <summary>
        /// getter for the website collections
        /// </summary>
        public ExtensionCollection<Website> Websites
        {
            get 
            {
                if (this.websites == null)
                {
                    this.websites = new ExtensionCollection<Website>(this); 
                }
                return this.websites;
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
        /// if a photo is present on this contact, it will have an etag associated with it,
        /// that needs to be used when you want to delete or update that picture.
        /// </summary>
        /// <returns>the etag value as a string</returns>
        public string PhotoEtag
        {
            get
            {
                AtomLink link = this.PhotoLink;
                if (link != null)
                {
                    foreach (XmlExtension x in link.ExtensionElements)
                    {
                        if (x.XmlNameSpace == GDataParserNameTable.gNamespace && x.XmlName == GDataParserNameTable.XmlEtagAttribute)
                        {
                            return x.Node.Value;
                        }
                    }
                }
                return null;
            }
            set 
            {
                AtomLink link = this.PhotoLink;
                if (link != null)
                {
                    foreach (XmlExtension x in link.ExtensionElements)
                    {
                        if (x.XmlNameSpace == GDataParserNameTable.gNamespace && x.XmlName == GDataParserNameTable.XmlEtagAttribute)
                        {
                            x.Node.Value = value;
                        }
                    }
                }
            }
        }

        private AtomLink PhotoLink
        {
            get 
            {
                AtomLink link = this.Links.FindService(GDataParserNameTable.ServicePhoto, null);
                return link;
            }
        }

        /// <summary>
        ///  returns the Name object
        /// </summary>
        public Name Name
        {
            get
            {
                return FindExtension(GDataParserNameTable.NameElement, BaseNameTable.gNamespace) as Name;
            }
            set
            {
                ReplaceExtension(GDataParserNameTable.NameElement, BaseNameTable.gNamespace, value);
            }
        }

        /// <summary>
        /// Contacts billing information.
        /// </summary>
        /// <returns></returns>
        public string BillingInformation 
        {
            get 
            {
                return GetStringValue<BillingInformation>(ContactsNameTable.BillingInformationElement,
                        ContactsNameTable.NSContacts);
            }
            set
            {
                SetStringValue<BillingInformation>(value, ContactsNameTable.BillingInformationElement,
                                        ContactsNameTable.NSContacts);
            }
        }


        /// <summary>
        /// Contact's birthday.
        /// </summary>
        /// <returns></returns>
        public string Birthday 
        {
            get 
            {
                Birthday b =  FindExtension(ContactsNameTable.BirthdayElement, ContactsNameTable.NSContacts) as Birthday;
                if (b != null)
                {
                    return b.When;
                }
                return null;
            }
            set
            {
                Birthday b = null;
                if (value != null)
                {
                    b = new Birthday(value);
                }
                ReplaceExtension(ContactsNameTable.BirthdayElement, ContactsNameTable.NSContacts, b);
            }
        }


        /// <summary>
        /// Directory server associated with the contact.
        /// </summary>
        /// <returns></returns>
        public string DirectoryServer 
        {
            get 
            {
                return GetStringValue<DirectoryServer>(ContactsNameTable.DirectoryServerElement,
                        ContactsNameTable.NSContacts);
            }
            set
            {
                SetStringValue<DirectoryServer>(value, ContactsNameTable.DirectoryServerElement,
                                        ContactsNameTable.NSContacts);
            }
        }
        

        /// <summary>
        /// Contacts initals
        /// </summary>
        /// <returns></returns>
        public string Initials 
        {
            get 
            {
                return GetStringValue<Initials>(ContactsNameTable.InitialsElement,
                        ContactsNameTable.NSContacts);
            }
            set
            {
                SetStringValue<Initials>(value, ContactsNameTable.InitialsElement,
                                        ContactsNameTable.NSContacts);
            }
        }
        
        /// <summary>
        /// Maiden name associated with the contact.
        /// </summary>
        /// <returns></returns>
        public string MaidenName 
        {
            get 
            {
                return GetStringValue<MaidenName>(ContactsNameTable.MaidenNameElement,
                        ContactsNameTable.NSContacts);
            }
            set
            {
                SetStringValue<MaidenName>(value, ContactsNameTable.MaidenNameElement,
                                        ContactsNameTable.NSContacts);
            }
        }
        
        /// <summary>
        /// Mileage associated with the contact.
        /// </summary>
        /// <returns></returns>
        public string Mileage 
        {
            get 
            {
                return GetStringValue<Mileage>(ContactsNameTable.MileageElement,
                        ContactsNameTable.NSContacts);
            }
            set
            {
                SetStringValue<Mileage>(value, ContactsNameTable.MileageElement,
                                        ContactsNameTable.NSContacts);
            }
        }
        

        /// <summary>
        /// Nickname associated with the contact.
        /// </summary>
        /// <returns></returns>
        public string Nickname 
        {
            get 
            {
                return GetStringValue<Nickname>(ContactsNameTable.NicknameElement,
                        ContactsNameTable.NSContacts);
            }
            set
            {
                SetStringValue<Nickname>(value, ContactsNameTable.NicknameElement,
                                        ContactsNameTable.NSContacts);
            }
        }
        
        /// <summary>
        /// Occupation associated with the contact.
        /// </summary>
        /// <returns></returns>
        public string Occupation 
        {
            get 
            {
                return GetStringValue<Occupation>(ContactsNameTable.OccupationElement,
                        ContactsNameTable.NSContacts);
            }
            set
            {
                SetStringValue<Occupation>(value, ContactsNameTable.OccupationElement,
                                        ContactsNameTable.NSContacts);
            }
        }

        /// <summary>
        /// Priority ascribed to the contact.
        /// </summary>
        /// <returns></returns>
        public string Priority 
        {
            get 
            {
                Priority b =  FindExtension(ContactsNameTable.PriorityElement, ContactsNameTable.NSContacts) as Priority;
                if (b != null)
                {
                    return b.Relation;
                }
                return null;
            }
            set
            {
                Priority b = null;
                if (value != null)
                {
                    b = new Priority(value);
                }
                ReplaceExtension(ContactsNameTable.PriorityElement, ContactsNameTable.NSContacts, b);
            }
        }
        
        /// <summary>
        /// Sensitivity ascribed to the contact.
        /// </summary>
        /// <returns></returns>
        public string Sensitivity 
        {
            get 
            {
                Sensitivity b =  FindExtension(ContactsNameTable.SensitivityElement, ContactsNameTable.NSContacts) as Sensitivity;
                if (b != null)
                {
                    return b.Relation;
                }
                return null;
            }
            set
            {
                Sensitivity b = null;
                if (value != null)
                {
                    b = new Sensitivity(value);
                }
                ReplaceExtension(ContactsNameTable.SensitivityElement, ContactsNameTable.NSContacts, b);
            }
        }
        
        /// <summary>
        /// Contact's short name.
        /// </summary>
        /// <returns></returns>
        public string ShortName 
        {
            get 
            {
                return GetStringValue<ShortName>(ContactsNameTable.ShortNameElement,
                        ContactsNameTable.NSContacts);
            }
            set
            {
                SetStringValue<ShortName>(value, ContactsNameTable.ShortNameElement,
                                        ContactsNameTable.NSContacts);
            }
        }
        
        /// <summary>
        /// Subject associated with the contact.
        /// </summary>
        /// <returns></returns>
        public string Subject 
        {
            get 
            {
                return GetStringValue<Subject>(ContactsNameTable.SubjectElement,
                        ContactsNameTable.NSContacts);
            }
            set
            {
                SetStringValue<Subject>(value, ContactsNameTable.SubjectElement,
                                        ContactsNameTable.NSContacts);
            }
        }
    }
}

