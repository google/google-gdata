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

namespace Google.GData.Photos {

    //////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Entry API customization class for defining entries in an Event feed.
    /// </summary>
    //////////////////////////////////////////////////////////////////////
    public class CommentEntry : AbstractEntry
    {
        /// <summary>
        /// Category used to label entries that contain Event extension data.
        /// </summary>
        public static AtomCategory COMMENT_CATEGORY =
        new AtomCategory(GDataParserNameTable.Event, new AtomUri(BaseNameTable.gKind));

        /// <summary>
        /// Constructs a new EventEntry instance with the appropriate category
        /// to indicate that it is an event.
        /// </summary>
        public CommentEntry()
        : base()
        {
            Categories.Add(COMMENT_CATEGORY);
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
            /*
            if (String.Compare(eventNode.NamespaceURI, BaseNameTable.gNamespace, true) == 0)
            {
                // Parse a When Element
                if (eventNode.LocalName == GDataParserNameTable.XmlWhenElement)
                {
                    this.Times.Add(When.ParseWhen(eventNode));
                    e.DiscardEntry = true;
                }
                // Parse a Where Element
                else if (eventNode.LocalName == GDataParserNameTable.XmlWhereElement)
                {
                    this.Locations.Add((Where.ParseWhere(eventNode, parser)));
                    e.DiscardEntry = true;
                }
                // Parse a Who Element
                else if (eventNode.LocalName == GDataParserNameTable.XmlWhoElement)
                {
                    this.Participants.Add((Who.ParseWho(eventNode, parser)));
                    e.DiscardEntry = true;
                }
                // Parse a Status Element
                else if (eventNode.LocalName == GDataParserNameTable.XmlEventStatusElement)
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
                // Parse an Original Event Element
                else if (eventNode.LocalName == GDataParserNameTable.XmlOriginalEventElement)
                {
                    this.OriginalEvent = OriginalEvent.ParseOriginal(eventNode);
                    e.DiscardEntry = true;
                }
                // Parse a Reminder Element - recurrence event, g:reminder is in top level
                else if (eventNode.LocalName == GDataParserNameTable.XmlReminderElement)
                {
                    this.Reminder = Reminder.ParseReminder(eventNode);
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
            */
        }

#endregion

    }
}

