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
    /// <summary>
    /// short table to hold the namespace and the prefix
    /// </summary>
    public class ContactsNameTable {
        /// <summary>static string to specify the Contacts namespace supported</summary>
        public const string NSContacts = "http://schemas.google.com/contact/2008";

        /// <summary>static string to specify the Google Contacts prefix used</summary>
        public const string contactsPrefix = "gContact";

        /// <summary>
        /// Group Member ship info element string
        /// </summary>
        public const string GroupMembershipInfo = "groupMembershipInfo";

        /// <summary>
        /// SystemGroup element, indicating that this entry is a system group
        /// </summary>
        public const string SystemGroupElement = "systemGroup";

        /// <summary>
        /// Specifies billing information of the entity represented by the contact. The element cannot be repeated
        /// </summary>
        public const string BillingInformationElement = "billingInformation";

        /// <summary>
        /// Stores birthday date of the person represented by the contact. The element cannot be repeated
        /// </summary>
        public const string BirthdayElement = "birthday";

        /// <summary>
        /// Storage for URL of the contact's calendar. The element can be repeated
        /// </summary>
        public const string CalendarLinkElement = "calendarLink";

        /// <summary>
        /// A directory server associated with this contact. May not be repeated
        /// </summary>
        public const string DirectoryServerElement = "directoryServer";

        /// <summary>
        /// An event associated with a contact. May be repeated.
        /// </summary>
        public const string EventElement = "event";

        /// <summary>
        /// Describes an ID of the contact in an external system of some kind. This element may be repeated.
        /// </summary>
        public const string ExternalIdElement = "externalId";

        /// <summary>
        /// Specifies the gender of the person represented by the contact. The element cannot be repeated.
        /// </summary>
        public const string GenderElement = "gender";

        /// <summary>
        /// Specifies hobbies or interests of the person specified by the contact. The element can be repeated
        /// </summary>
        public const string HobbyElement = "hobby";

        /// <summary>
        /// Specifies the initials of the person represented by the contact. The element cannot be repeated.
        /// </summary>
        public const string InitialsElement = "initials";

        /// <summary>
        /// Storage for arbitrary pieces of information about the contact. Each jot has a type specified by the rel attribute and a text value. The element can be repeated.
        /// </summary>
        public const string JotElement = "jot";

        /// <summary>
        /// Specifies the preferred languages of the contact. The element can be repeated
        /// </summary>
        public const string LanguageElement = "language";

        /// <summary>
        /// Specifies maiden name of the person represented by the contact. The element cannot be repeated.
        /// </summary>
        public const string MaidenNameElement = "maidenName";

        /// <summary>
        /// Specifies the mileage for the entity represented by the contact. Can be used for example to document distance needed for reimbursement purposes.
        /// The value is not interpreted. The element cannot be repeated
        /// </summary>
        public const string MileageElement = "mileage";

        /// <summary>
        /// Specifies the nickname of the person represented by the contact. The element cannot be repeated
        /// </summary>
        public const string NicknameElement = "nickname";

        /// <summary>
        /// Specifies the occupation/profession of the person specified by the contact. The element cannot be repeated.
        /// </summary>
        public const string OccupationElement = "occupation";

        /// <summary>
        /// Classifies importance into 3 categories. can not be repeated
        /// </summary>
        public const string PriorityElement = "priority";

        /// <summary>
        /// Describes the relation to another entity. may be repeated
        /// </summary>
        public const string RelationElement = "relation";

        /// <summary>
        /// Classifies sensitifity of the contact
        /// </summary>
        public const string SensitivityElement = "sensitivity";

        /// <summary>
        /// Specifies short name of the person represented by the contact. The element cannot be repeated.
        /// </summary>
        public const string ShortNameElement = "shortName";

        /// <summary>
        /// Specifies status of the person.
        /// </summary>
        public const string StatusElement = "status";

        /// <summary>
        /// Specifies the subject of the contact. The element cannot be repeated.
        /// </summary>
        public const string SubjectElement = "subject";

        /// <summary>
        /// Represents an arbitrary key-value pair attached to the contact.
        /// </summary>
        public const string UserDefinedFieldElement = "userDefinedField";

        /// <summary>
        /// Websites associated with the contact. May be repeated
        /// </summary>
        public const string WebsiteElement = "website";

        /// <summary>
        /// rel Attribute
        /// </summary>
        /// <returns></returns>
        public static string AttributeRel = "rel";

        /// <summary>
        /// label Attribute
        /// </summary>
        /// <returns></returns>
        public static string AttributeLabel = "label";
    }

    /// <summary>
    /// an element is defined that represents a group to which the contact belongs
    /// </summary>
    public class GroupMembership : SimpleElement {
        /// <summary>the  href attribute </summary>
        public const string XmlAttributeHRef = "href";
        /// <summary>the deleted attribute </summary>
        public const string XmlAttributeDeleted = "deleted";

        /// <summary>
        /// default constructor
        /// </summary>
        public GroupMembership()
            : base(ContactsNameTable.GroupMembershipInfo, ContactsNameTable.contactsPrefix, ContactsNameTable.NSContacts) {
            this.Attributes.Add(XmlAttributeHRef, null);
            this.Attributes.Add(XmlAttributeDeleted, null);
        }

        /// <summary>Identifies the group to which the contact belongs or belonged.
        /// The group is referenced by its id.</summary>
        public string HRef {
            get {
                return this.Attributes[XmlAttributeHRef] as string;
            }
            set {
                this.Attributes[XmlAttributeHRef] = value;
            }
        }

        /// <summary>Means that the group membership was removed for the contact.
        /// This attribute will only be included if showdeleted is specified
        /// as query parameter, otherwise groupMembershipInfo for groups a contact
        /// does not belong to anymore is simply not returned.</summary>
        public string Deleted {
            get {
                return this.Attributes[XmlAttributeDeleted] as string;
            }
        }
    }

    /// <summary>
    /// extension element to represent a system group
    /// </summary>
    public class SystemGroup : SimpleElement {
        /// <summary>
        /// id attribute for the system group element
        /// </summary>
        /// <returns></returns>
        public const string XmlAttributeId = "id";

        /// <summary>
        /// default constructor
        /// </summary>
        public SystemGroup()
            : base(ContactsNameTable.SystemGroupElement, ContactsNameTable.contactsPrefix, ContactsNameTable.NSContacts) {
            this.Attributes.Add(XmlAttributeId, null);
        }

        /// <summary>Identifies the system group. Note that you still need
        /// to use the group entries href membership to retrieve the group
        /// </summary>
        public string Id {
            get {
                return this.Attributes[XmlAttributeId] as string;
            }
        }
    }

    /// <summary>
    /// abstract class for a basecontactentry, used for contacts and groups
    /// </summary>
    public abstract class BaseContactEntry : AbstractEntry, IContainsDeleted {
        private ExtensionCollection<ExtendedProperty> xproperties;

        /// <summary>
        /// Constructs a new BaseContactEntry instance
        /// to indicate that it is an event.
        /// </summary>
        public BaseContactEntry()
            : base() {
            Tracing.TraceMsg("Created BaseContactEntry Entry");
            this.AddExtension(new ExtendedProperty());
            this.AddExtension(new Deleted());
        }

        /// <summary>
        /// returns the extended properties on this object
        /// </summary>
        /// <returns></returns>
        public ExtensionCollection<ExtendedProperty> ExtendedProperties {
            get {
                if (this.xproperties == null) {
                    this.xproperties = new ExtensionCollection<ExtendedProperty>(this);
                }
                return this.xproperties;
            }
        }

        /// <summary>
        /// if this is a previously deleted contact, returns true
        /// to delete a contact, use the delete method
        /// </summary>
        public bool Deleted {
            get {
                if (FindExtension(GDataParserNameTable.XmlDeletedElement,
                    BaseNameTable.gNamespace) != null) {
                    return true;
                }
                return false;
            }
        }
    }

    /// <summary>
    /// Specifies billing information of the entity represented by the contact. The element cannot be repeated
    /// </summary>
    public class BillingInformation : SimpleElement {
        /// <summary>
        /// default constructor for BillingInformation
        /// </summary>
        public BillingInformation()
            : base(ContactsNameTable.BillingInformationElement,
            ContactsNameTable.contactsPrefix,
            ContactsNameTable.NSContacts) {
        }

        /// <summary>
        /// default constructor for BillingInformation with an initial value
        /// </summary>
        /// <param name="initValue"/>
        public BillingInformation(string initValue)
            : base(ContactsNameTable.BillingInformationElement,
            ContactsNameTable.contactsPrefix,
            ContactsNameTable.NSContacts, initValue) {
        }
    }

    /// <summary>
    /// Stores birthday date of the person represented by the contact. The element cannot be repeated.
    /// </summary>
    public class Birthday : SimpleElement {
        /// <summary>
        /// When Attribute
        /// </summary>
        /// <returns></returns>
        public static string AttributeWhen = "when";

        /// <summary>
        /// default constructor for Birthday
        /// </summary>
        public Birthday()
            : base(ContactsNameTable.BirthdayElement,
            ContactsNameTable.contactsPrefix,
            ContactsNameTable.NSContacts) {
            this.Attributes.Add(AttributeWhen, null);
        }

        /// <summary>
        /// default constructor for Birthday with an initial value
        /// </summary>
        /// <param name="initValue"/>
        public Birthday(string initValue)
            : base(ContactsNameTable.BirthdayElement,
            ContactsNameTable.contactsPrefix,
            ContactsNameTable.NSContacts) {
            this.Attributes.Add(AttributeWhen, initValue);
        }

        /// <summary>Birthday date, given in format YYYY-MM-DD (with the year), or --MM-DD (without the year)</summary>
        /// <returns> </returns>
        public string When {
            get {
                return this.Attributes[AttributeWhen] as string;
            }
            set {
                this.Attributes[AttributeWhen] = value;
            }
        }
    }

    /// <summary>
    /// Storage for URL of the contact's information. The element can be repeated.
    /// </summary>
    public class ContactsLink : LinkAttributesElement {
        /// <summary>
        /// href Attribute
        /// </summary>
        /// <returns></returns>
        public static string AttributeHref = "href";

        /// <summary>
        /// default constructor for CalendarLink
        /// </summary>
        public ContactsLink(string elementName, string elementPrefix, string elementNamespace)
            : base(elementName, elementPrefix, elementNamespace) {
            this.Attributes.Add(AttributeHref, null);
        }

        /// <summary>The URL of the the related link.</summary>
        /// <returns> </returns>
        public string Href {
            get {
                return this.Attributes[AttributeHref] as string;
            }
            set {
                this.Attributes[AttributeHref] = value;
            }
        }
    }

    /// <summary>
    /// Storage for URL of the contact's calendar. The element can be repeated.
    /// </summary>
    public class CalendarLink : ContactsLink {
        public CalendarLink()
            : base(ContactsNameTable.CalendarLinkElement,
            ContactsNameTable.contactsPrefix,
            ContactsNameTable.NSContacts) {
        }
    }

    /// <summary>
    /// DirectoryServer schema extension
    /// </summary>
    public class DirectoryServer : SimpleElement {
        /// <summary>
        /// default constructor for DirectoryServer
        /// </summary>
        public DirectoryServer()
            : base(ContactsNameTable.DirectoryServerElement,
            ContactsNameTable.contactsPrefix,
            ContactsNameTable.NSContacts) {
        }

        /// <summary>
        /// default constructor for DirectoryServer with an initial value
        /// </summary>
        /// <param name="initValue"/>
        public DirectoryServer(string initValue)
            : base(ContactsNameTable.DirectoryServerElement,
            ContactsNameTable.contactsPrefix,
            ContactsNameTable.NSContacts, initValue) {
        }
    }

    /// <summary>
    /// Event schema extension
    /// </summary>
    public class Event : SimpleContainer {
        /// <summary>
        /// default constructor for Event
        /// </summary>
        public Event()
            : base(ContactsNameTable.EventElement,
            ContactsNameTable.contactsPrefix,
            ContactsNameTable.NSContacts) {
            this.Attributes.Add(ContactsNameTable.AttributeRel, null);
            this.Attributes.Add(ContactsNameTable.AttributeLabel, null);
            this.ExtensionFactories.Add(new When());
        }

        /// <summary>Predefined calendar link type. Can be one of work, home or free-busy</summary>
        /// <returns> </returns>
        public string Relation {
            get {
                return this.Attributes[ContactsNameTable.AttributeRel] as string;
            }
            set {
                this.Attributes[ContactsNameTable.AttributeRel] = value;
            }
        }

        /// <summary>User-defined calendar link type.</summary>
        /// <returns> </returns>
        public string Label {
            get {
                return this.Attributes[ContactsNameTable.AttributeLabel] as string;
            }
            set {
                this.Attributes[ContactsNameTable.AttributeLabel] = value;
            }
        }

        /// <summary>
        /// exposes the When element for this event
        /// </summary>
        /// <returns></returns>
        public When When {
            get {
                return FindExtension(GDataParserNameTable.XmlWhenElement,
                    BaseNameTable.gNamespace) as When;
            }
            set {
                ReplaceExtension(GDataParserNameTable.XmlWhenElement,
                    BaseNameTable.gNamespace,
                    value);
            }
        }
    }

    /// <summary>
    /// ExternalId schema extension
    /// </summary>
    public class ExternalId : SimpleAttribute {
        /// <summary>
        /// default constructor for ExternalId
        /// </summary>
        public ExternalId()
            : base(ContactsNameTable.ExternalIdElement,
            ContactsNameTable.contactsPrefix,
            ContactsNameTable.NSContacts) {
            this.Attributes.Add(ContactsNameTable.AttributeRel, null);
            this.Attributes.Add(ContactsNameTable.AttributeLabel, null);
        }

        /// <summary>
        /// default constructor for ExternalId with an initial value
        /// </summary>
        /// <param name="initValue"/>
        public ExternalId(string initValue)
            : base(ContactsNameTable.ExternalIdElement,
            ContactsNameTable.contactsPrefix,
            ContactsNameTable.NSContacts, initValue) {
            this.Attributes.Add(ContactsNameTable.AttributeRel, null);
            this.Attributes.Add(ContactsNameTable.AttributeLabel, null);
        }

        /// <summary>Predefined calendar link type. Can be one of work, home or free-busy</summary>
        /// <returns> </returns>
        public string Relation {
            get {
                return this.Attributes[ContactsNameTable.AttributeRel] as string;
            }
            set {
                this.Attributes[ContactsNameTable.AttributeRel] = value;
            }
        }

        /// <summary>User-defined calendar link type.</summary>
        /// <returns> </returns>
        public string Label {
            get {
                return this.Attributes[ContactsNameTable.AttributeLabel] as string;
            }
            set {
                this.Attributes[ContactsNameTable.AttributeLabel] = value;
            }
        }
    }

    /// <summary>
    /// Gender schema extension
    /// </summary>
    public class Gender : SimpleAttribute {
        /// <summary>
        /// default constructor for Gender
        /// </summary>
        public Gender()
            : base(ContactsNameTable.GenderElement,
            ContactsNameTable.contactsPrefix,
            ContactsNameTable.NSContacts) {
        }

        /// <summary>
        /// default constructor for Gender with an initial value
        /// </summary>
        /// <param name="initValue"/>
        public Gender(string initValue)
            : base(ContactsNameTable.GenderElement,
            ContactsNameTable.contactsPrefix,
            ContactsNameTable.NSContacts, initValue) {
        }
    }

    /// <summary>
    /// Hobby schema extension
    /// </summary>
    public class Hobby : SimpleElement {
        /// <summary>
        /// default constructor for Hobby
        /// </summary>
        public Hobby()
            : base(ContactsNameTable.HobbyElement,
            ContactsNameTable.contactsPrefix,
            ContactsNameTable.NSContacts) {
        }

        /// <summary>
        /// default constructor for Hobby with an initial value
        /// </summary>
        /// <param name="initValue"/>
        public Hobby(string initValue)
            : base(ContactsNameTable.HobbyElement,
            ContactsNameTable.contactsPrefix,
            ContactsNameTable.NSContacts, initValue) {
        }
    }

    /// <summary>
    /// Initials schema extension
    /// </summary>
    public class Initials : SimpleElement {
        /// <summary>
        /// default constructor for Initials
        /// </summary>
        public Initials()
            : base(ContactsNameTable.InitialsElement,
            ContactsNameTable.contactsPrefix,
            ContactsNameTable.NSContacts) {
        }

        /// <summary>
        /// default constructor for Initials with an initial value
        /// </summary>
        /// <param name="initValue"/>
        public Initials(string initValue)
            : base(ContactsNameTable.InitialsElement,
            ContactsNameTable.contactsPrefix,
            ContactsNameTable.NSContacts, initValue) {
        }
    }

    /// <summary>
    /// Jot schema extension
    /// </summary>
    public class Jot : SimpleElement {
        /// <summary>
        /// default constructor for Jot
        /// </summary>
        public Jot()
            : base(ContactsNameTable.JotElement,
            ContactsNameTable.contactsPrefix,
            ContactsNameTable.NSContacts) {
            this.Attributes.Add(ContactsNameTable.AttributeRel, null);
        }

        /// <summary>Predefined calendar link type. Can be one of work, home or free-busy</summary>
        /// <returns> </returns>
        public string Relation {
            get {
                return this.Attributes[ContactsNameTable.AttributeRel] as string;
            }
            set {
                this.Attributes[ContactsNameTable.AttributeRel] = value;
            }
        }
    }

    /// <summary>
    /// Language schema extension
    /// </summary>
    public class Language : SimpleElement {
        /// <summary>
        /// the code attribute
        /// </summary>
        /// <returns></returns>
        public static string AttributeCode = "code";

        /// <summary>
        /// default constructor for Language
        /// </summary>
        public Language()
            : base(ContactsNameTable.LanguageElement,
            ContactsNameTable.contactsPrefix,
            ContactsNameTable.NSContacts) {
            this.Attributes.Add(ContactsNameTable.AttributeLabel, null);
            this.Attributes.Add(AttributeCode, null);
        }

        /// <summary>
        /// default constructor for Language with an initial value
        /// </summary>
        /// <param name="initValue"/>
        public Language(string initValue)
            : base(ContactsNameTable.LanguageElement,
            ContactsNameTable.contactsPrefix,
            ContactsNameTable.NSContacts, initValue) {
            this.Attributes.Add(ContactsNameTable.AttributeLabel, null);
            this.Attributes.Add(AttributeCode, null);
        }

        /// <summary>A freeform name of a language. Must not be empty or all whitespace.</summary>
        /// <returns> </returns>
        public string Label {
            get {
                return this.Attributes[ContactsNameTable.AttributeLabel] as string;
            }
            set {
                this.Attributes[ContactsNameTable.AttributeLabel] = value;
            }
        }

        /// <summary>A language code conforming to the IETF BCP 47 specification.</summary>
        /// <returns> </returns>
        public string Code {
            get {
                return this.Attributes[AttributeCode] as string;
            }
            set {
                this.Attributes[AttributeCode] = value;
            }
        }
    }

    /// <summary>
    /// Specifies maiden name of the person represented by the contact. The element cannot be repeated.
    /// </summary>
    public class MaidenName : SimpleElement {
        /// <summary>
        /// default constructor for MaidenName
        /// </summary>
        public MaidenName()
            : base(ContactsNameTable.MaidenNameElement,
            ContactsNameTable.contactsPrefix,
            ContactsNameTable.NSContacts) {
        }

        /// <summary>
        /// default constructor for MaidenName with an initial value
        /// </summary>
        /// <param name="initValue"/>
        public MaidenName(string initValue)
            : base(ContactsNameTable.MaidenNameElement,
            ContactsNameTable.contactsPrefix,
            ContactsNameTable.NSContacts, initValue) {
        }
    }

    /// <summary>
    /// Specifies the mileage for the entity represented by the contact. Can be used for example to
    /// document distance needed for reimbursement purposes. The value is not interpreted. The element cannot be repeated.
    /// </summary>
    public class Mileage : SimpleElement {
        /// <summary>
        /// default constructor for Mileage
        /// </summary>
        public Mileage()
            : base(ContactsNameTable.MileageElement,
            ContactsNameTable.contactsPrefix,
            ContactsNameTable.NSContacts) {
        }

        /// <summary>
        /// default constructor for Mileage with an initial value
        /// </summary>
        /// <param name="initValue"/>
        public Mileage(string initValue)
            : base(ContactsNameTable.MileageElement,
            ContactsNameTable.contactsPrefix,
            ContactsNameTable.NSContacts, initValue) {
        }
    }

    /// <summary>
    /// Specifies the nickname of the person represented by the contact. The element cannot be repeated
    /// </summary>
    public class Nickname : SimpleElement {
        /// <summary>
        /// default constructor for Nickname
        /// </summary>
        public Nickname()
            : base(ContactsNameTable.NicknameElement,
            ContactsNameTable.contactsPrefix,
            ContactsNameTable.NSContacts) {
        }

        /// <summary>
        /// default constructor for Nickname with an initial value
        /// </summary>
        /// <param name="initValue"/>
        public Nickname(string initValue)
            : base(ContactsNameTable.NicknameElement,
            ContactsNameTable.contactsPrefix,
            ContactsNameTable.NSContacts, initValue) {
        }
    }

    /// <summary>
    /// Specifies the occupation/profession of the person specified by the contact. The element cannot be repeated.
    /// </summary>
    public class Occupation : SimpleElement {
        /// <summary>
        /// default constructor for Occupation
        /// </summary>
        public Occupation()
            : base(ContactsNameTable.OccupationElement,
            ContactsNameTable.contactsPrefix,
            ContactsNameTable.NSContacts) {
        }

        /// <summary>
        /// default constructor for Occupation with an initial value
        /// </summary>
        /// <param name="initValue"/>
        public Occupation(string initValue)
            : base(ContactsNameTable.OccupationElement,
            ContactsNameTable.contactsPrefix,
            ContactsNameTable.NSContacts, initValue) {
        }
    }

    /// <summary>
    /// Classifies importance of the contact into 3 categories, low, normal and high
    /// </summary>
    public class Priority : SimpleElement {
        /// <summary>
        /// default constructor for Priority
        /// </summary>
        public Priority()
            : base(ContactsNameTable.PriorityElement,
            ContactsNameTable.contactsPrefix,
            ContactsNameTable.NSContacts) {
            this.Attributes.Add(ContactsNameTable.AttributeRel, null);
        }

        /// <summary>
        /// default constructor for Priority with an initial value
        /// </summary>
        /// <param name="initValue"/>
        public Priority(string initValue)
            : base(ContactsNameTable.OccupationElement,
            ContactsNameTable.contactsPrefix,
            ContactsNameTable.NSContacts) {
            this.Attributes.Add(ContactsNameTable.AttributeRel, initValue);
        }

        /// <summary>Predefined calendar link type. Can be one of work, home or free-busy</summary>
        /// <returns> </returns>
        public string Relation {
            get {
                return this.Attributes[ContactsNameTable.AttributeRel] as string;
            }
            set {
                this.Attributes[ContactsNameTable.AttributeRel] = value;
            }
        }
    }

    /// <summary>
    /// Relation schema extension
    /// </summary>
    public class Relation : SimpleElement {
        /// <summary>
        /// default constructor for Relation
        /// </summary>
        public Relation()
            : base(ContactsNameTable.RelationElement,
            ContactsNameTable.contactsPrefix,
            ContactsNameTable.NSContacts) {
            this.Attributes.Add(ContactsNameTable.AttributeLabel, null);
            this.Attributes.Add(ContactsNameTable.AttributeRel, null);
        }

        /// <summary>A freeform name of a language. Must not be empty or all whitespace.</summary>
        /// <returns> </returns>
        public string Label {
            get {
                return this.Attributes[ContactsNameTable.AttributeLabel] as string;
            }
            set {
                this.Attributes[ContactsNameTable.AttributeLabel] = value;
            }
        }

        /// <summary>defines the link type.</summary>
        /// <returns> </returns>
        public string Rel {
            get {
                return this.Attributes[ContactsNameTable.AttributeRel] as string;
            }
            set {
                this.Attributes[ContactsNameTable.AttributeRel] = value;
            }
        }
    }

    /// <summary>
    /// Classifies sensitivity of the contact into the following categories:
    /// confidential, normal, personal or private
    /// </summary>
    public class Sensitivity : SimpleElement {
        /// <summary>
        /// default constructor for Sensitivity
        /// </summary>
        public Sensitivity()
            : base(ContactsNameTable.SensitivityElement,
            ContactsNameTable.contactsPrefix,
            ContactsNameTable.NSContacts) {
            this.Attributes.Add(ContactsNameTable.AttributeRel, null);
        }

        /// <summary>
        /// default constructor for Sensitivity with an initial value
        /// </summary>
        /// <param name="initValue"/>
        public Sensitivity(string initValue)
            : base(ContactsNameTable.SensitivityElement,
            ContactsNameTable.contactsPrefix,
            ContactsNameTable.NSContacts) {
            this.Attributes.Add(ContactsNameTable.AttributeRel, initValue);
        }

        /// <summary>returns the relationship value</summary>
        /// <returns> </returns>
        public string Relation {
            get {
                return this.Attributes[ContactsNameTable.AttributeRel] as string;
            }
            set {
                this.Attributes[ContactsNameTable.AttributeRel] = value;
            }
        }
    }

    /// <summary>
    /// Specifies short name of the person represented by the contact. The element cannot be repeated.
    /// </summary>
    public class ShortName : SimpleElement {
        /// <summary>
        /// default constructor for ShortName
        /// </summary>
        public ShortName()
            : base(ContactsNameTable.ShortNameElement,
            ContactsNameTable.contactsPrefix,
            ContactsNameTable.NSContacts) {
        }

        /// <summary>
        /// default constructor for ShortName with an initial value
        /// </summary>
        /// <param name="initValue"/>
        public ShortName(string initValue)
            : base(ContactsNameTable.ShortNameElement,
            ContactsNameTable.contactsPrefix,
            ContactsNameTable.NSContacts, initValue) {
        }
    }

    /// <summary>
    /// Specifies the status element of the person.
    /// </summary>
    public class Status : SimpleElement {
        /// <summary>
        /// indexed attribute for the status element
        /// </summary>
        /// <returns></returns>
        public const String XmlAttributeIndexed = "indexed";

        /// <summary>
        /// default constructor for Status
        /// </summary>
        public Status()
            : base(ContactsNameTable.StatusElement,
            ContactsNameTable.contactsPrefix,
            ContactsNameTable.NSContacts) {
            this.Attributes.Add(XmlAttributeIndexed, null);
        }

        /// <summary>
        /// default constructor for Status with an initial value
        /// </summary>
        /// <param name="initValue"/>
        public Status(bool initValue)
            : base(ContactsNameTable.StatusElement,
            ContactsNameTable.contactsPrefix,
            ContactsNameTable.NSContacts) {
            this.Attributes.Add(XmlAttributeIndexed, initValue ? Utilities.XSDTrue : Utilities.XSDFalse);
        }

        /// <summary>Indexed attribute.</summary>
        /// <returns> </returns>
        public bool Indexed {
            get {
                bool result;
                if (!Boolean.TryParse(this.Attributes[XmlAttributeIndexed] as string, out result)) {
                    result = false;
                }
                return result;
            }
            set {
                this.Attributes[XmlAttributeIndexed] = value ? Utilities.XSDTrue : Utilities.XSDFalse;
            }
        }
    }

    /// <summary>
    /// Specifies the subject of the contact. The element cannot be repeated.
    /// </summary>
    public class Subject : SimpleElement {
        /// <summary>
        /// default constructor for Subject
        /// </summary>
        public Subject()
            : base(ContactsNameTable.SubjectElement,
            ContactsNameTable.contactsPrefix,
            ContactsNameTable.NSContacts) {
        }

        /// <summary>
        /// default constructor for Subject with an initial value
        /// </summary>
        /// <param name="initValue"/>
        public Subject(string initValue)
            : base(ContactsNameTable.SubjectElement,
            ContactsNameTable.contactsPrefix,
            ContactsNameTable.NSContacts, initValue) {
        }
    }

    /// <summary>
    /// Represents an arbitrary key-value pair attached to the contact.
    /// </summary>
    public class UserDefinedField : SimpleAttribute {
        /// <summary>
        /// key attribute
        /// </summary>
        /// <returns></returns>
        public static string AttributeKey = "key";

        /// <summary>
        /// default constructor for UserDefinedField
        /// </summary>
        public UserDefinedField()
            : base(ContactsNameTable.UserDefinedFieldElement,
            ContactsNameTable.contactsPrefix,
            ContactsNameTable.NSContacts) {
            this.Attributes.Add(AttributeKey, null);
        }

        /// <summary>
        /// default constructor for UserDefinedField with an initial value
        /// </summary>
        /// <param name="initValue"/>
        /// <param name="initKey"/>
        public UserDefinedField(string initValue, string initKey)
            : base(ContactsNameTable.UserDefinedFieldElement,
            ContactsNameTable.contactsPrefix,
            ContactsNameTable.NSContacts, initValue) {
            this.Attributes.Add(AttributeKey, initKey);
        }

        /// <summary>A simple string value used to name this field. Case-sensitive</summary>
        /// <returns> </returns>
        public string Key {
            get {
                return this.Attributes[AttributeKey] as string;
            }
            set {
                this.Attributes[AttributeKey] = value;
            }
        }
    }

    /// <summary>
    /// WebSite schema extension
    /// </summary>
    public class Website : ContactsLink {
        /// <summary>
        /// default constructor for WebSite
        /// </summary>
        public Website()
            : base(ContactsNameTable.WebsiteElement,
            ContactsNameTable.contactsPrefix,
            ContactsNameTable.NSContacts) {
        }
    }
}
