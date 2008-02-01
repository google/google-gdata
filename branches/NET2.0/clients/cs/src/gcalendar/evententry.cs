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
using System.IO; 
using System.Collections;
using Google.GData.Client;
using Google.GData.Extensions;

namespace Google.GData.Calendar {

    //////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Entry API customization class for defining entries in an Event feed.
    /// </summary>
    //////////////////////////////////////////////////////////////////////
    public class EventEntry : AbstractEntry
    {


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
        : base()
        {
            Categories.Add(EVENT_CATEGORY);
            addEventEntryExtensions();
        }

        /// <summary>
        /// Constructs a new EventEntry instance with provided data.
        /// </summary>
        /// <param name="title">The descriptive title of the event ("What" in the UI)</param>
        public EventEntry(string title) : this()
        {
            this.Title.Text = title;
        }

        /// <summary>
        /// Constructs a new EventEntry instance with provided data.
        /// </summary>
        /// <param name="title">The descriptive title of the event ("What" in the UI)</param>
        /// <param name="description">A longer description of the event 
        /// ("Description" in the UI)</param>
        public EventEntry(string title, string description) : this(title)
        {
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
            : this(title, description)
        {
            Where eventLocation = new Where();
            eventLocation.ValueString = location;
            this.Locations.Add(eventLocation);
            
        }

        /// <summary>
        ///  helper method to add extensions to the evententry
        /// </summary>
        private void addEventEntryExtensions()
        {
            this.AddExtension(new Reminder());
            this.AddExtension(new Where());
            this.AddExtension(new Who());
            this.AddExtension(new When());
            this.AddExtension(new OriginalEvent());
        }

        /// <summary>
        /// Constructs a new EventStatus  instance
        /// </summary>
        public class EventStatus : EnumConstruct
        {
            /// <summary>
            ///  default constructor
            /// </summary>
            public EventStatus()
            : base(GDataParserNameTable.XmlEventStatusElement)
            {
            }

            /// <summary>
            ///  EventStatus constructor 
            /// </summary>
            /// <param name="value">indicates the default status</param>
            public EventStatus(string value)
            : base(GDataParserNameTable.XmlEventStatusElement, value)
            {
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


            /// <summary>
            ///  parse method is called from the atom parser to populate an EventStatus node
            /// </summary>
            /// <param name="node">the xmlnode to parser</param>
            /// <returns>EventStatus object</returns>
            public static EventStatus parse(XmlNode node)
            {
                EventStatus eventStatus = null;
                if (String.Compare(node.NamespaceURI, BaseNameTable.gNamespace, true) == 0)
                {
                    eventStatus = new EventStatus();
                    if (node.Attributes != null)
                    {
                        eventStatus.Value = node.Attributes["value"].Value;
                    }
                }
                return eventStatus;
            }
        }

        /// <summary>
        /// Visibility class indicates the visibility of an eventNode
        /// </summary>
        public class Visibility : EnumConstruct
        {
            /// <summary>
            ///  default constructor
            /// </summary>
            public Visibility()
            : base(GDataParserNameTable.XmlVisibilityElement)
            {
            }

            /// <summary>
            ///  Visibility constructor with a string to indicate default value
            /// </summary>
            /// <param name="value">the default visibility value</param>
            public Visibility(string value)
            : base(GDataParserNameTable.XmlVisibilityElement, value)
            {
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

            /// <summary>
            ///  parse method is called from the atom parser to populate an Visibility node
            /// </summary>
            /// <param name="node">the xmlnode to parser</param>
            /// <returns>Visibility object</returns>
            public static Visibility parse(XmlNode node)
            {
                Visibility vis = null;
                if (String.Compare(node.NamespaceURI, BaseNameTable.gNamespace, true) == 0)
                {
                    vis = new Visibility();
                    if (node.Attributes != null)
                    {
                        vis.Value = node.Attributes["value"].Value;
                    }
                }
                return vis;
            }
        }

        /// <summary>
        ///  the Transparency of an event class
        /// </summary>
        public class Transparency : EnumConstruct
        {
            /// <summary>
            ///  default constructor
            /// </summary>
            public Transparency()
            : base(GDataParserNameTable.XmlTransparencyElement)
            {
            }

            /// <summary>
            ///  constructor with a default string value
            /// </summary>
            /// <param name="value">transparency value</param>
            public Transparency(string value)
            : base(GDataParserNameTable.XmlTransparencyElement, value)
            {
            }

            /// <summary>string constant for the opaque transparency value</summary>
            public const string OPAQUE_VALUE = BaseNameTable.gNamespacePrefix + "event.opaque";
            /// <summary>string constant for the transparent trancparency value</summary>
            public const string TRANSPARENT_VALUE = BaseNameTable.gNamespacePrefix + "event.transparent";

            /// <summary>object constant for the opaque transparency value</summary>
            public static Transparency OPAQUE = new Transparency(OPAQUE_VALUE);
            /// <summary>object constant for the transparent transparency value</summary>
            public static Transparency TRANSPARENT = new Transparency(TRANSPARENT_VALUE);

            /// <summary>
            ///  parse method is called from the atom parser to populate an Transparency node
            /// </summary>
            /// <param name="node">the xmlnode to parser</param>
            /// <returns>Transparency object</returns>
            public static Transparency parse(XmlNode node)
            {
                Transparency trans = null;
                if (String.Compare(node.NamespaceURI, BaseNameTable.gNamespace, true) == 0)
                {
                    trans = new Transparency();
                    if (node.Attributes != null)
                    {
                        trans.Value = node.Attributes["value"].Value;
                    }
                }
                return trans;
            }
        }

        /// <summary>
        ///  indicates if an eventupdate should reissue notifications
        /// false by default
        /// </summary>
        public class SendNotifications : EnumConstruct
        {
            /// <summary>
            ///  default constructor
            /// </summary>
            public SendNotifications()
            : base(GDataParserNameTable.XmlSendNotificationsElement)
            {
            }

            /// <summary>
            ///  constructor with a default string value
            /// </summary>
            /// <param name="value">transparency value</param>
            public SendNotifications(string value)
            : base(GDataParserNameTable.XmlSendNotificationsElement, value)
            {
            }

            //////////////////////////////////////////////////////////////////////
            /// <summary>Returns the constant representing this XML element.</summary> 
            //////////////////////////////////////////////////////////////////////
            public override string XmlNamespace
            {
                get { return GDataParserNameTable.NSGCal; }
            }
            //////////////////////////////////////////////////////////////////////
            /// <summary>Returns the constant representing this XML element.</summary> 
            //////////////////////////////////////////////////////////////////////
            public override string XmlNamespacePrefix
            {
                get { return GDataParserNameTable.gCalPrefix; }
            }



            /// <summary>
            ///  parse method is called from the atom parser to populate an Transparency node
            /// </summary>
            /// <param name="node">the xmlnode to parser</param>
            /// <returns>Notifications object</returns>
            public static SendNotifications parse(XmlNode node)
            {
                SendNotifications notify = null;
                Tracing.TraceMsg("Parsing a gCal:SendNotifications");
                if (String.Compare(node.NamespaceURI, GDataParserNameTable.NSGCal, true) == 0)
                {
                    notify = new SendNotifications();
                    if (node.Attributes != null)
                    {
                        notify.Value = node.Attributes["value"].Value;
                        Tracing.TraceMsg("Notification parsed, value = " + notify.Value);
                    }
                }
                return notify;
            }
        }

         /// <summary>
        ///  indicates if this new entry should be a quickadd
        /// false by default
        /// </summary>
        public class QuickAddElement : EnumConstruct
        {
            /// <summary>
            ///  default constructor
            /// </summary>
            public QuickAddElement()
            : base(GDataParserNameTable.XmlQuickAddElement)
            {
            }

            /// <summary>
            ///  constructor with a default string value
            /// </summary>
            /// <param name="value">transparency value</param>
            public QuickAddElement(string value)
            : base(GDataParserNameTable.XmlQuickAddElement, value)
            {
            }

            //////////////////////////////////////////////////////////////////////
            /// <summary>Returns the constant representing this XML element.</summary> 
            //////////////////////////////////////////////////////////////////////
            public override string XmlNamespace
            {
                get { return GDataParserNameTable.NSGCal; }
            }
            //////////////////////////////////////////////////////////////////////
            /// <summary>Returns the constant representing this XML element.</summary> 
            //////////////////////////////////////////////////////////////////////
            public override string XmlNamespacePrefix
            {
                get { return GDataParserNameTable.gCalPrefix; }
            }
        }



#region EventEntry Attributes

        private WhenCollection times;
        private WhereCollection locations;
        private WhoCollection participants;
        private EventStatus status;
        private Visibility visibility;
        private Transparency transparency;
        private Recurrence recurrence;
        private Comments comments;
        private RecurrenceException exception; 
        private SendNotifications sendNotifications;
        private QuickAddElement quickAdd;

#endregion

#region Public Methods
        /// <summary>
        ///  property accessor for the WhenCollection
        /// </summary>
        public WhenCollection Times
        {
            get 
            {
                if (this.times == null)
                {
                    this.times =  new WhenCollection(this);
                }
                return this.times;
            }
        }

        /// <summary>
        ///  property accessor for the WhereCollection
        /// </summary>
        public WhereCollection Locations
        {
            get 
            {
                if (this.locations == null)
                {
                    this.locations =  new WhereCollection(this);
                }
                return this.locations;
            }
        }

        /// <summary>
        ///  property accessor for the whos in the event
        /// </summary>
        public WhoCollection Participants
        {
            get 
            {
                if (this.participants == null)
                {
                    this.participants =  new WhoCollection(this); 
                }
                return this.participants;
            }
        }

        /// <summary>
        ///  property accessor for the EventStatus
        /// </summary>
        public EventStatus Status
        {
            get { return status;}
            set
            {
                if (status != null)
                {
                    ExtensionElements.Remove(status);
                }
                status = value; 
                ExtensionElements.Add(status);
            }
        }

        /// <summary>
        ///  property accessor for the Eventnotifications
        ///  set this to True for notfications to be send 
        /// </summary>
        public bool Notifications
        {
            get { 
                    if (this.sendNotifications == null)
                    {
                        return false;
                    }
                    return this.sendNotifications.Value == Utilities.XSDTrue;
                }

            set 
            {
                if (value == true)
                {

                    if (this.sendNotifications == null)
                    {
                        this.sendNotifications = new SendNotifications(); 
                        ExtensionElements.Add(this.sendNotifications);
                    }
                    this.sendNotifications.Value = Utilities.XSDTrue;
                }
                else 
                {
                    if (this.sendNotifications != null)
                    {
                        ExtensionElements.Remove(this.sendNotifications);
                        this.sendNotifications = null; 
                    }

                }
            }
        }


        /// <summary>
        ///  property accessor QuickAdd
        /// To create an event using Google Calendar's quick add feature, set the event 
        /// entry's content to the quick add string you'd like to use. Then add a 
        /// gCal:quickadd element with a value attribute set to true
        /// </summary>
        public bool QuickAdd
        {
            get { 
                    if (this.quickAdd == null)
                    {
                        return false;
                    }
                    return this.quickAdd.Value == "true"; 
                }

            set 
            {
                if (value == true)
                {

                    if (this.quickAdd == null)
                    {
                        this.quickAdd = new QuickAddElement(); 
                        ExtensionElements.Add(this.quickAdd);
                    }
                    this.quickAdd.Value = Utilities.XSDTrue;
                }
                else 
                {
                    if (this.quickAdd != null)
                    {
                        ExtensionElements.Remove(this.quickAdd);
                        this.quickAdd = null; 
                    }
                }
            }
        }




        /// <summary>
        ///  property accessor for the Event Visibility 
        /// </summary>
        public Visibility EventVisibility
        {
            get { return visibility; }
            set 
            {
                if (visibility != null)
                {
                    ExtensionElements.Remove(visibility);
                }
                visibility = value; 
                ExtensionElements.Add(visibility); 
            }
        }

        /// <summary>
        ///  property accessor for the EventTransparency
        /// </summary>
        public Transparency EventTransparency
        {
            get { return transparency;}
            set
            {
                if (transparency != null)
                {
                    ExtensionElements.Remove(transparency);
                }
                transparency = value; 
                ExtensionElements.Add(transparency);
            }
        }

        /// <summary>
        ///  property accessor for the Recurrence
        /// </summary>
        public Recurrence Recurrence
        {
            get { return recurrence;}
            set
            {
                if (recurrence != null)
                {
                    ExtensionElements.Remove(recurrence);
                }
                recurrence = value; 
                ExtensionElements.Add(recurrence);
            }
        }

      /// <summary>
      ///  property accessor for the RecurrenceException
      /// </summary>
      public RecurrenceException RecurrenceException
      {
          get { return exception;}
          set
          {
              if (exception != null)
              {
                  ExtensionElements.Remove(exception);
              }
              exception = value; 
              ExtensionElements.Add(exception);
          }
      }

        /// <summary>
        /// property accessor for the OriginalEvent
        /// </summary>
        public OriginalEvent OriginalEvent
        {
            get 
            { 
                return FindExtension(GDataParserNameTable.XmlOriginalEventElement,
                                     BaseNameTable.gNamespace) as OriginalEvent;
            }
            set
            {
                ReplaceExtension(GDataParserNameTable.XmlOriginalEventElement,
                                     BaseNameTable.gNamespace, value);
            }
        }


        /// <summary>
        /// returns the first reminder of the Times collection
        /// </summary>
        /// <returns>When object for the reminder or NULL</returns>
        protected When GetFirstReminder()
        {
            return this.Times != null && this.Times.Count > 0 ? this.Times[0] : null; 
        }


        /// <summary>
        /// returns the FIRST reminder for backwards compatibility
        /// if set, will REMOVE all reminders, but this one (array of one)
        /// </summary>
        public Reminder Reminder 
        {
            get 
            {
                ArrayList arr = this.Reminders;
                if (arr != null && arr.Count > 0)
                {
                    return arr[0] as Reminder;
                }
                return null;
            }
            set 
            {
                ArrayList arr = null;
                if (value != null)
                {
                    arr = new ArrayList();
                    arr.Add(value);
                }
                this.Reminders = arr;
            }
        }

        /// <summary>
        /// property accessor for the Reminder
        /// </summary>
        public ArrayList Reminders
        {
            get 
            { 
                // if we are a recurrent event, reminder is on the entry/toplevel
                if (this.Recurrence != null)
                {
                    return FindExtensions(GDataParserNameTable.XmlReminderElement,
                                      BaseNameTable.gNamespace);
                 
                } 
                else
                {
                    // in the non recurrent case, it's on the first when element
                    When w = GetFirstReminder(); 
                    if (w != null)
                    {
                        return w.Reminders; 
                    }
                }
                return null; 
            }

            set
            {
                if (this.Recurrence != null)
                {
                    DeleteExtensions(GDataParserNameTable.XmlReminderElement,
                                      BaseNameTable.gNamespace); 
                    if (value != null)
                    {
                        // now add the new ones
                        foreach (Object ob in value)
                        {
                            this.ExtensionElements.Add(ob);
                        }
                    }
                }
                else
                {
                    // non recurring case, set it on the first when
                    // in the non recurrent case, it's on the first when element
                    When w = GetFirstReminder(); 
                    if (w != null)
                    {
                        w.Reminders = value; 
                    }
                    else
                    {
                        throw new ArgumentException("Neither recurrence, nor a when object found. Please construct a when object, or the recurrence object first before setting a reminder time"); 
                    }
                }
            }
        }

        /// <summary>
        /// as eventId is a commonly used part in the calendar world, we expose it
        /// here. In general the EventId is the last part of the AtomId
        /// </summary>
        public string EventId 
        {
            get 
            {
                string[] elements = this.Id.AbsoluteUri.Split(new char[] {'/'});
                if (elements != null && elements.Length > 0)
                {
                    return elements[elements.Length - 1];
                }
                return null;
            }
        }

        /// <summary>
        ///  property accessor for the Comments
        /// </summary>
        public Comments Comments
        {
            get { return comments;}
            set
            {
                if (comments != null)
                {
                    ExtensionElements.Remove(comments);
                }
                comments = value;
                ExtensionElements.Add(comments);
            }
        }

        /// <summary>
        /// Property to retrieve/set an associated WebContentLink
        /// </summary>
        public WebContentLink WebContentLink
        {
            get
            {
                return this.Links.FindService(WebContentLink.WEB_CONTENT_REL, 
                    null) as WebContentLink;
            }
            set
            {
                this.Links.Add(value);
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
        public override AtomBase CreateAtomSubElement(XmlReader reader, AtomFeedParser parser)
        {
            Object localname = reader.LocalName;

            if ((localname.Equals(parser.Nametable.Link)))
            {
                if (reader.GetAttribute(GDataParserNameTable.XmlAttributeRel) == 
                    WebContentLink.WEB_CONTENT_REL)
                {
                    return new WebContentLink(false);
                }
            }
            return base.CreateAtomSubElement(reader, parser);
            
        }
     

#region Event Parser

        //////////////////////////////////////////////////////////////////////
        /// <summary>parses the inner state of the element</summary>
        /// <param name="e">evennt arguments</param>
        /// <param name="parser">the atomFeedParser that called this</param>
        //////////////////////////////////////////////////////////////////////
        public override void Parse(ExtensionElementEventArgs e, AtomFeedParser parser)
        {
            XmlNode eventNode = e.ExtensionElement;

            // Parse a Reminder Element - recurrence event, g:reminder is in top level
            // reminders are already changed to IExtensionElementFactory, so call base
            // see addEventEntryExtensions()
            base.Parse(e, parser);

            if (String.Compare(eventNode.NamespaceURI, BaseNameTable.gNamespace, true) == 0)
            {
                // Parse a Status Element
                if (eventNode.LocalName == GDataParserNameTable.XmlEventStatusElement)
                {
                    this.Status = EventStatus.parse(eventNode);
                    e.DiscardEntry = true;
                }
                // Parse a Visibility Element
                else if (eventNode.LocalName == GDataParserNameTable.XmlVisibilityElement)
                {
                    this.EventVisibility = Visibility.parse(eventNode);
                    e.DiscardEntry = true;
                }
                // Parse a Transparency Element
                else if (eventNode.LocalName == GDataParserNameTable.XmlTransparencyElement)
                {
                    this.EventTransparency = Transparency.parse(eventNode);
                    e.DiscardEntry = true;
                }
                // Parse a Recurrence Element
                else if (eventNode.LocalName == GDataParserNameTable.XmlRecurrenceElement)
                {
                    this.Recurrence = Recurrence.ParseRecurrence(eventNode);
                    e.DiscardEntry = true;
                }
                else if (eventNode.LocalName == GDataParserNameTable.XmlRecurrenceExceptionElement)
                {
                    this.RecurrenceException = RecurrenceException.ParseRecurrenceException(eventNode, parser);
                    e.DiscardEntry = true;
                }
                // Parse a Comments Element
                else if (eventNode.LocalName == GDataParserNameTable.XmlCommentsElement)
                {
                    this.Comments = Comments.ParseComments(eventNode);
                    e.DiscardEntry = true;
                } else if (eventNode.LocalName == GDataParserNameTable.XmlExtendedPropertyElement)
                {
                    ExtendedProperty p = ExtendedProperty.Parse(eventNode); 
                    if (p != null)
                    {
                        e.DiscardEntry = true;
                        this.ExtensionElements.Add(p);
                    }
                }
            }
            else if (String.Compare(eventNode.NamespaceURI, GDataParserNameTable.NSGCal, true) == 0)
            {
                // parse the eventnotification element
                Tracing.TraceMsg("Parsing in the gCal Namespace");
                if (eventNode.LocalName == GDataParserNameTable.XmlSendNotificationsElement)
                {
                    this.sendNotifications = SendNotifications.parse(eventNode);
                    e.DiscardEntry = true;
                }
            }
        }

#endregion

    }
}



