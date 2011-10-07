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
using System.Xml;
using System.IO;
using System.Collections;
using Google.GData.Client;
using Google.GData.Extensions;

namespace Google.GData.Calendar {
    //////////////////////////////////////////////////////////////////////
    /// <summary>subelements definition for calendar-specific event entries
    /// </summary>
    //////////////////////////////////////////////////////////////////////
    public class GCalNameTable {
        /// <summary>syncEvent</summary>
        public const string XmlSyncEventElement = "syncEvent";
        /// <summary>sequence element</summary>
        public const string XmlSequenceElement = "sequence";
        /// <summary>uid element</summary>
        public const string XmlUidElement = "uid";
        /// <summary>guestsCanModify element</summary>
        public const string XmlGuestsCanModifyElement = "guestsCanModify";
        /// <summary>guestsCanInviteOthers element</summary>
        public const string XmlGuestsCanInviteOthersElement = "guestsCanInviteOthers";
        /// <summary>guestsCanSeeGuests element</summary>
        public const string XmlGuestsCanSeeGuestsElement = "guestsCanSeeGuests";
    }

    /// <summary>
    /// Indicates whether this is a sync scenario where we allow setting the gCal:uid, the gCal:sequence,
    /// and the organizer of an event. This element makes sense only when inserting and updating
    ///  events. This element should primarily be used in a sync scenario.
    /// </summary>
    public class GCalSyncEvent : SimpleAttribute {
        /// <summary>
        /// default calendar access level constructor
        /// </summary>
        public GCalSyncEvent()
            : base(GCalNameTable.XmlSyncEventElement,
            GDataParserNameTable.gCalPrefix,
            GDataParserNameTable.NSGCal) {
        }

        /// <summary>
        /// default calendar acccess level
        /// constructor with an initial value
        /// </summary>
        /// <param name="initValue"></param>
        public GCalSyncEvent(string initValue)
            : base(GCalNameTable.XmlSyncEventElement,
            GDataParserNameTable.gCalPrefix,
            GDataParserNameTable.NSGCal,
            initValue) {
        }
    }

    /// <summary>
    /// Indicates the globally unique identifier (UID) of the event as defined in Section 4.8.4.7 of RFC 2445.
    /// </summary>
    public class GCalUid : SimpleAttribute {
        /// <summary>
        /// default calendar access level constructor
        /// </summary>
        public GCalUid()
            : base(GCalNameTable.XmlUidElement,
            GDataParserNameTable.gCalPrefix,
            GDataParserNameTable.NSGCal) {
        }

        /// <summary>
        /// default calendar acccess level
        /// constructor with an initial value
        /// </summary>
        /// <param name="initValue"></param>
        public GCalUid(string initValue)
            : base(GCalNameTable.XmlUidElement,
            GDataParserNameTable.gCalPrefix,
            GDataParserNameTable.NSGCal,
            initValue) {
        }
    }

    /// <summary>
    /// Indicates the revision sequence number of the event as defined in Section 4.8.7.4 of RFC 2445.
    /// Must be non-negative.
    /// </summary>
    public class GCalSequence : SimpleAttribute {
        /// <summary>
        /// default calendar access level constructor
        /// </summary>
        public GCalSequence()
            : base(GCalNameTable.XmlSequenceElement,
            GDataParserNameTable.gCalPrefix,
            GDataParserNameTable.NSGCal) {
        }

        /// <summary>
        /// default calendar acccess level
        /// constructor with an initial value
        /// </summary>
        /// <param name="initValue"></param>
        public GCalSequence(string initValue)
            : base(GCalNameTable.XmlSequenceElement,
            GDataParserNameTable.gCalPrefix,
            GDataParserNameTable.NSGCal,
            initValue) {
        }
    }

    /// <summary>
    /// Indicates whether or not guests can modify the event.
    /// </summary>
    public class GCalGuestsCanModify : SimpleAttribute {
        /// <summary>
        /// default constructor
        /// </summary>
        public GCalGuestsCanModify()
            : base(GCalNameTable.XmlGuestsCanModifyElement,
            GDataParserNameTable.gCalPrefix,
            GDataParserNameTable.NSGCal) {
        }

        /// <summary>
        /// constructor with an initial value
        /// </summary>
        /// <param name="initValue"></param>
        public GCalGuestsCanModify(string initValue)
            : base(GCalNameTable.XmlGuestsCanModifyElement,
            GDataParserNameTable.gCalPrefix,
            GDataParserNameTable.NSGCal,
            initValue) {
        }
    }

    /// <summary>
    /// Indicates whether or not guests can invite other guests.
    /// </summary>
    public class GCalGuestsCanInviteOthers : SimpleAttribute {
        /// <summary>
        /// default constructor
        /// </summary>
        public GCalGuestsCanInviteOthers()
            : base(GCalNameTable.XmlGuestsCanInviteOthersElement,
            GDataParserNameTable.gCalPrefix,
            GDataParserNameTable.NSGCal) {
        }

        /// <summary>
        /// constructor with an initial value
        /// </summary>
        /// <param name="initValue"></param>
        public GCalGuestsCanInviteOthers(string initValue)
            : base(GCalNameTable.XmlGuestsCanInviteOthersElement,
            GDataParserNameTable.gCalPrefix,
            GDataParserNameTable.NSGCal,
            initValue) {
        }
    }

    /// <summary>
    /// Indicates whether or not guests can see other guests.
    /// </summary>
    public class GCalGuestsCanSeeGuests : SimpleAttribute {
        /// <summary>
        /// default constructor
        /// </summary>
        public GCalGuestsCanSeeGuests()
            : base(GCalNameTable.XmlGuestsCanSeeGuestsElement,
            GDataParserNameTable.gCalPrefix,
            GDataParserNameTable.NSGCal) {
        }

        /// <summary>
        /// constructor with an initial value
        /// </summary>
        /// <param name="initValue"></param>
        public GCalGuestsCanSeeGuests(string initValue)
            : base(GCalNameTable.XmlGuestsCanSeeGuestsElement,
            GDataParserNameTable.gCalPrefix,
            GDataParserNameTable.NSGCal,
            initValue) {
        }
    }

    /// <summary>
    /// Entry API customization class for defining entries in an Event feed.
    /// </summary>
    public class EventEntry : AbstractEntry {
        /// <summary>
        /// Category used to label entries that contain Event extension data.
        /// </summary>
        public static AtomCategory EVENT_CATEGORY =
            new AtomCategory(GDataParserNameTable.Event, new AtomUri(BaseNameTable.gKind));

        /// <summary>
        /// Constructs a new EventEntry instance with the appropriate category
        /// to indicate that it is an event.
        /// </summary>
        public EventEntry()
            : base() {
            Categories.Add(EVENT_CATEGORY);
            addEventEntryExtensions();
        }

        /// <summary>
        /// Constructs a new EventEntry instance with provided data.
        /// </summary>
        /// <param name="title">The descriptive title of the event ("What" in the UI)</param>
        public EventEntry(string title)
            : this() {
            this.Title.Text = title;
        }

        /// <summary>
        /// Constructs a new EventEntry instance with provided data.
        /// </summary>
        /// <param name="title">The descriptive title of the event ("What" in the UI)</param>
        /// <param name="description">A longer description of the event
        /// ("Description" in the UI)</param>
        public EventEntry(string title, string description)
            : this(title) {
            this.Content.Content = description;
        }

        /// <summary>
        /// Constructs a new EventEntry instance with provided data.
        /// </summary>
        /// <param name="title">The descriptive title of the event ("What" in the UI)</param>
        /// <param name="description">A longer description of the event
        /// ("Description" in the UI)</param>
        /// <param name="location">The location of the event ("Where" in the UI)</param>
        public EventEntry(string title, string description, string location)
            : this(title, description) {
            Where eventLocation = new Where();
            eventLocation.ValueString = location;
            this.Locations.Add(eventLocation);
        }

        /// <summary>
        ///  helper method to add extensions to the evententry
        /// </summary>
        private void addEventEntryExtensions() {
            this.AddExtension(new Reminder());
            this.AddExtension(new Where());
            this.AddExtension(new Who());
            this.AddExtension(new When());
            this.AddExtension(new OriginalEvent());
            this.AddExtension(new SendNotifications());
            this.AddExtension(new Transparency());
            this.AddExtension(new Visibility());
            this.AddExtension(new EventStatus());
            this.AddExtension(new RecurrenceException());
            this.AddExtension(new Comments());
            this.AddExtension(new ExtendedProperty());
            this.AddExtension(new Recurrence());
            this.AddExtension(new GCalSequence());
            this.AddExtension(new GCalUid());
            this.AddExtension(new GCalSyncEvent());
            this.AddExtension(new GCalGuestsCanSeeGuests());
            this.AddExtension(new GCalGuestsCanInviteOthers());
            this.AddExtension(new GCalGuestsCanModify());
        }

        /// <summary>
        /// Constructs a new EventStatus instance
        /// </summary>
        public class EventStatus : EnumConstruct {
            /// <summary>
            /// default constructor
            /// </summary>
            public EventStatus()
                : base(GDataParserNameTable.XmlEventStatusElement) {
            }

            /// <summary>
            /// EventStatus constructor
            /// </summary>
            /// <param name="value">indicates the default status</param>
            public EventStatus(string value)
                : base(GDataParserNameTable.XmlEventStatusElement, value) {
            }

            /// <summary>string constant for a confirmed event</summary>
            public const string CONFIRMED_VALUE = BaseNameTable.gNamespacePrefix + "event.confirmed";
            /// <summary>string constant for a tentative accepted event</summary>
            public const string TENTATIVE_VALUE = BaseNameTable.gNamespacePrefix + "event.tentative";
            /// <summary>string constant for a cancelled event</summary>
            public const string CANCELED_VALUE = BaseNameTable.gNamespacePrefix + "event.canceled";

            /// <summary>EventStatus constant for a confirmed event</summary>
            public static EventStatus CONFIRMED = new EventStatus(CONFIRMED_VALUE);
            /// <summary>EventStatus constant for a tentative event</summary>
            public static EventStatus TENTATIVE = new EventStatus(TENTATIVE_VALUE);
            /// <summary>EventStatus constant for a Cancelled  event</summary>
            public static EventStatus CANCELED = new EventStatus(CANCELED_VALUE);
        }

        /// <summary>
        /// Visibility class indicates the visibility of an eventNode
        /// </summary>
        public class Visibility : EnumConstruct {
            /// <summary>
            ///  default constructor
            /// </summary>
            public Visibility()
                : base(GDataParserNameTable.XmlVisibilityElement) {
            }

            /// <summary>
            /// Visibility constructor with a string to indicate default value
            /// </summary>
            /// <param name="value">the default visibility value</param>
            public Visibility(string value)
                : base(GDataParserNameTable.XmlVisibilityElement, value) {
            }

            /// <summary>string constant for the default visibility value</summary>
            public const string DEFAULT_VALUE = BaseNameTable.gNamespacePrefix + "event.default";
            /// <summary>string constant for the public visibility value</summary>
            public const string PUBLIC_VALUE = BaseNameTable.gNamespacePrefix + "event.public";
            /// <summary>string constant for the confidential visibility value</summary>
            public const string CONFIDENTIAL_VALUE = BaseNameTable.gNamespacePrefix + "event.confidential";
            /// <summary>string constant for the private visibility value</summary>
            public const string PRIVATE_VALUE = BaseNameTable.gNamespacePrefix + "event.private";

            /// <summary>object constant for the private visibility value</summary>
            public static Visibility DEFAULT = new Visibility(DEFAULT_VALUE);
            /// <summary>object constant for the private visibility value</summary>
            public static Visibility PUBLIC = new Visibility(PUBLIC_VALUE);
            /// <summary>object constant for the private visibility value</summary>
            public static Visibility CONFIDENTIAL = new Visibility(CONFIDENTIAL_VALUE);
            /// <summary>object constant for the private visibility value</summary>
            public static Visibility PRIVATE = new Visibility(PRIVATE_VALUE);
        }

        /// <summary>
        /// the Transparency of an event class
        /// </summary>
        public class Transparency : EnumConstruct {
            /// <summary>
            ///  default constructor
            /// </summary>
            public Transparency()
                : base(GDataParserNameTable.XmlTransparencyElement) {
            }

            /// <summary>
            /// constructor with a default string value
            /// </summary>
            /// <param name="value">transparency value</param>
            public Transparency(string value)
                : base(GDataParserNameTable.XmlTransparencyElement, value) {
            }

            /// <summary>string constant for the opaque transparency value</summary>
            public const string OPAQUE_VALUE = BaseNameTable.gNamespacePrefix + "event.opaque";
            /// <summary>string constant for the transparent trancparency value</summary>
            public const string TRANSPARENT_VALUE = BaseNameTable.gNamespacePrefix + "event.transparent";

            /// <summary>object constant for the opaque transparency value</summary>
            public static Transparency OPAQUE = new Transparency(OPAQUE_VALUE);
            /// <summary>object constant for the transparent transparency value</summary>
            public static Transparency TRANSPARENT = new Transparency(TRANSPARENT_VALUE);
        }

        /// <summary>
        /// indicates if an eventupdate should reissue notifications
        /// false by default
        /// </summary>
        public class SendNotifications : EnumConstruct {
            /// <summary>
            ///  default constructor
            /// </summary>
            public SendNotifications()
                : base(GDataParserNameTable.XmlSendNotificationsElement,
                GDataParserNameTable.gCalPrefix,
                GDataParserNameTable.NSGCal) {
            }

            /// <summary>
            /// constructor with a default string value
            /// </summary>
            /// <param name="value">transparency value</param>
            public SendNotifications(string value)
                : base(GDataParserNameTable.XmlSendNotificationsElement,
                GDataParserNameTable.gCalPrefix,
                GDataParserNameTable.NSGCal,
                value) {
            }
        }

        /// <summary>
        /// indicates if this new entry should be a quickadd
        /// false by default
        /// </summary>
        public class QuickAddElement : EnumConstruct {
            /// <summary>
            ///  default constructor
            /// </summary>
            public QuickAddElement()
                : base(GDataParserNameTable.XmlQuickAddElement,
                GDataParserNameTable.gCalPrefix,
                GDataParserNameTable.NSGCal) {
            }

            /// <summary>
            /// constructor with a default string value
            /// </summary>
            /// <param name="value">transparency value</param>
            public QuickAddElement(string value)
                : base(GDataParserNameTable.XmlQuickAddElement, value) {
            }
        }

        #region EventEntry Attributes

        private ExtensionCollection<When> times;
        private ExtensionCollection<Where> locations;
        private ExtensionCollection<Who> participants;
        #endregion

        #region Public Methods
        /// <summary>
        /// property accessor for the WhenCollection
        /// </summary>
        public ExtensionCollection<When> Times {
            get {
                if (this.times == null) {
                    this.times = new ExtensionCollection<When>(this);
                }
                return this.times;
            }
        }

        /// <summary>
        /// property accessor for the WhereCollection
        /// </summary>
        public ExtensionCollection<Where> Locations {
            get {
                if (this.locations == null) {
                    this.locations = new ExtensionCollection<Where>(this);
                }
                return this.locations;
            }
        }

        /// <summary>
        /// property accessor for the whos in the event
        /// </summary>
        public ExtensionCollection<Who> Participants {
            get {
                if (this.participants == null) {
                    this.participants = new ExtensionCollection<Who>(this);
                }
                return this.participants;
            }
        }


        /// <summary>
        /// property accessor for the Event notifications
        /// set this to True for notifications to be sent
        /// </summary>
        public bool Notifications {
            get {
                SendNotifications n = 
                    FindExtension(GDataParserNameTable.XmlSendNotificationsElement,
                    GDataParserNameTable.NSGCal) as SendNotifications;

                if (n == null) {
                    return false;
                }
                return n.Value == Utilities.XSDTrue;
            }
            set {
                SendNotifications n =
                    FindExtension(GDataParserNameTable.XmlSendNotificationsElement,
                    GDataParserNameTable.NSGCal) as SendNotifications;

                if (value) {
                    if (n == null) {
                        n = new SendNotifications();
                        ExtensionElements.Add(n);
                    }
                    n.Value = Utilities.XSDTrue;
                } else {
                    if (n != null) {
                        DeleteExtensions(GDataParserNameTable.XmlSendNotificationsElement,
                            GDataParserNameTable.NSGCal);
                    }
                }
            }
        }

        /// <summary>
        /// property accessor QuickAdd
        /// To create an event using Google Calendar's quick add feature, set the event
        /// entry's content to the quick add string you'd like to use. Then add a
        /// gCal:quickadd element with a value attribute set to true
        /// </summary>
        public bool QuickAdd {
            get {
                QuickAddElement q = 
                    FindExtension(GDataParserNameTable.XmlQuickAddElement,
                    GDataParserNameTable.NSGCal) as QuickAddElement;

                if (q == null) {
                    return false;
                }
                return q.Value == Utilities.XSDTrue;
            }
            set {
                QuickAddElement q =
                    FindExtension(GDataParserNameTable.XmlQuickAddElement,
                    GDataParserNameTable.NSGCal) as QuickAddElement;

                if (value) {
                    if (q == null) {
                        q = new QuickAddElement();
                        ExtensionElements.Add(q);
                    }
                    q.Value = Utilities.XSDTrue;
                } else {
                    if (q != null) {
                        DeleteExtensions(GDataParserNameTable.XmlQuickAddElement,
                                     GDataParserNameTable.NSGCal);
                    }
                }
            }
        }

        /// <summary>
        /// property accessor for the EventStatus
        /// </summary>
        public EventStatus Status {
            get {
                return FindExtension(GDataParserNameTable.XmlEventStatusElement,
                    GDataParserNameTable.gNamespace) as EventStatus;
            }
            set {
                ReplaceExtension(GDataParserNameTable.XmlEventStatusElement,
                    GDataParserNameTable.gNamespace, value);
            }
        }

        /// <summary>
        /// property accessor for the Event Visibility
        /// </summary>
        public Visibility EventVisibility {
            get {
                return FindExtension(GDataParserNameTable.XmlVisibilityElement,
                    GDataParserNameTable.gNamespace) as Visibility;
            }
            set {
                ReplaceExtension(GDataParserNameTable.XmlVisibilityElement,
                    GDataParserNameTable.gNamespace, value);
            }
        }

        /// <summary>
        /// property accessor for the EventTransparency
        /// </summary>
        public Transparency EventTransparency {
            get {
                return FindExtension(GDataParserNameTable.XmlTransparencyElement,
                    GDataParserNameTable.gNamespace) as Transparency;
            }
            set {
                ReplaceExtension(GDataParserNameTable.XmlTransparencyElement,
                    GDataParserNameTable.gNamespace, value);
            }
        }

        /// <summary>
        /// property accessor for the Recurrence
        /// </summary>
        public Recurrence Recurrence {
            get {
                return FindExtension(GDataParserNameTable.XmlRecurrenceElement,
                    GDataParserNameTable.gNamespace) as Recurrence;
            }
            set {
                ReplaceExtension(GDataParserNameTable.XmlRecurrenceElement,
                    GDataParserNameTable.gNamespace, value);
            }
        }

        /// <summary>
        /// property accessor for the RecurrenceException
        /// </summary>
        public RecurrenceException RecurrenceException {
            get {
                return FindExtension(GDataParserNameTable.XmlRecurrenceExceptionElement,
                    GDataParserNameTable.gNamespace) as RecurrenceException;
            }
            set {
                ReplaceExtension(GDataParserNameTable.XmlRecurrenceExceptionElement,
                    GDataParserNameTable.gNamespace, value);
            }
        }

        /// <summary>
        /// property accessor for the OriginalEvent
        /// </summary>
        public OriginalEvent OriginalEvent {
            get {
                return FindExtension(GDataParserNameTable.XmlOriginalEventElement,
                    GDataParserNameTable.gNamespace) as OriginalEvent;
            }
            set {
                ReplaceExtension(GDataParserNameTable.XmlOriginalEventElement,
                    GDataParserNameTable.gNamespace, value);
            }
        }

        /// <summary>
        /// property accessor for the Comments
        /// </summary>
        public Comments Comments {
            get {
                return FindExtension(GDataParserNameTable.XmlCommentsElement,
                    GDataParserNameTable.gNamespace) as Comments;
            }
            set {
                ReplaceExtension(GDataParserNameTable.XmlCommentsElement,
                    GDataParserNameTable.gNamespace, value);
            }
        }

        /// <summary>
        /// returns the first reminder of the Times collection
        /// </summary>
        /// <returns>When object for the reminder or NULL</returns>
        protected When GetFirstReminder() {
            return this.Times != null && this.Times.Count > 0 ? this.Times[0] : null;
        }

        /// <summary>
        /// returns the FIRST reminder for backwards compatibility
        /// if set, will REMOVE all reminders, but this one (array of one)
        /// </summary>
        public Reminder Reminder {
            get {
                if (this.Reminders != null && this.Reminders.Count > 0) {
                    return this.Reminders[0];
                }
                return null;
            }
            set {
                this.Reminders.Clear();
                if (value != null) {
                    this.Reminders.Add(value);
                }
            }
        }

        /// <summary>
        /// property accessor for the Reminder
        /// </summary>
        public ExtensionCollection<Reminder> Reminders {
            get {
                // if we are a recurrent event, reminder is on the entry/toplevel
                if (this.Recurrence != null) {
                    //TODO could not get the generic overload to work. we for now just copy the list a few times.
                    ExtensionList list = new ExtensionList(this);

                    FindExtensions(GDataParserNameTable.XmlReminderElement,
                        BaseNameTable.gNamespace, list);

                    ExtensionCollection<Reminder> collection = new ExtensionCollection<Reminder>(this);
                    foreach (Reminder var in list) {
                        collection.Add(var);
                    }

                    return collection;
                } else {
                    // in the non recurrent case, it's on the first when element
                    When w = GetFirstReminder();
                    if (w != null) {
                        return w.Reminders;
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// as eventId is a commonly used part in the calendar world, we expose it
        /// here. In general the EventId is the last part of the AtomId
        /// </summary>
        public string EventId {
            get {
                string[] elements = this.Id.AbsoluteUri.Split(new char[] { '/' });
                if (elements != null && elements.Length > 0) {
                    return elements[elements.Length - 1];
                }
                return null;
            }
        }

        /// <summary>
        /// Property to retrieve/set an associated WebContentLink
        /// </summary>
        public WebContentLink WebContentLink {
            get {
                return this.Links.FindService(WebContentLink.WEB_CONTENT_REL,
                    null) as WebContentLink;
            }
            set {
                this.Links.Add(value);
            }
        }

        /// <summary>
        /// property accessor for the SyncEvent element
        /// </summary>
        public GCalSyncEvent SyncEvent {
            get {
                return FindExtension(GCalNameTable.XmlSyncEventElement,
                    GDataParserNameTable.NSGCal) as GCalSyncEvent;
            }
            set {
                ReplaceExtension(GCalNameTable.XmlSyncEventElement,
                    GDataParserNameTable.NSGCal, value);
            }
        }

        /// <summary>
        /// property accessor for the uid element
        /// </summary>
        public GCalUid Uid {
            get {
                return FindExtension(GCalNameTable.XmlUidElement,
                    GDataParserNameTable.NSGCal) as GCalUid;
            }
            set {
                ReplaceExtension(GCalNameTable.XmlUidElement,
                    GDataParserNameTable.NSGCal, value);
            }
        }
        /// <summary>
        /// property accessor for the SyncEvent element
        /// </summary>
        public GCalSequence Sequence {
            get {
                return FindExtension(GCalNameTable.XmlSequenceElement,
                    GDataParserNameTable.NSGCal) as GCalSequence;
            }
            set {
                ReplaceExtension(GCalNameTable.XmlSequenceElement,
                    GDataParserNameTable.NSGCal, value);
            }
        }

        /// <summary>
        /// property accessor for the GuestsCanSeeGuests element
        /// </summary>
        public GCalGuestsCanSeeGuests GuestsCanSeeGuests {
            get {
                return FindExtension(GCalNameTable.XmlGuestsCanSeeGuestsElement,
                    GDataParserNameTable.NSGCal) as GCalGuestsCanSeeGuests;
            }
            set {
                ReplaceExtension(GCalNameTable.XmlGuestsCanSeeGuestsElement,
                    GDataParserNameTable.NSGCal, value);
            }
        }

        /// <summary>
        /// property accessor for the GuestsCanInviteOthers element
        /// </summary>
        public GCalGuestsCanInviteOthers GuestsCanInviteOthers {
            get {
                return FindExtension(GCalNameTable.XmlGuestsCanInviteOthersElement,
                    GDataParserNameTable.NSGCal) as GCalGuestsCanInviteOthers;
            }
            set {
                ReplaceExtension(GCalNameTable.XmlGuestsCanInviteOthersElement,
                    GDataParserNameTable.NSGCal, value);
            }
        }

        /// <summary>
        /// property accessor for the GuestsCanModify element
        /// </summary>
        public GCalGuestsCanModify GuestsCanModify {
            get {
                return FindExtension(GCalNameTable.XmlGuestsCanModifyElement,
                    GDataParserNameTable.NSGCal) as GCalGuestsCanModify;
            }
            set {
                ReplaceExtension(GCalNameTable.XmlGuestsCanModifyElement,
                    GDataParserNameTable.NSGCal, value);
            }
        }

        #endregion

        /// <summary>
        /// this is the subclassing method for AtomBase derived
        /// classes to overload what childelements should be created
        /// needed to create CustomLink type objects, like WebContentLink etc
        /// </summary>
        /// <param name="reader">The XmlReader that tells us what we are working with</param>
        /// <param name="parser">the parser is primarily used for nametable comparisons</param>
        /// <returns>AtomBase</returns>
        public override AtomBase CreateAtomSubElement(XmlReader reader, AtomFeedParser parser) {
            Object localname = reader.LocalName;

            if (localname.Equals(parser.Nametable.Link)) {
                if (reader.GetAttribute(GDataParserNameTable.XmlAttributeRel) ==
                    WebContentLink.WEB_CONTENT_REL) {
                    return new WebContentLink(false);
                }
            }
            return base.CreateAtomSubElement(reader, parser);
        }
    }
}
