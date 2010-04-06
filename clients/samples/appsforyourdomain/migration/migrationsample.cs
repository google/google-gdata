/* Copyright (c) 2007 Google Inc.
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
using System.IO;
using System.Text;
using System.Xml;
using Google.GData.Apps.Migration;
using Google.GData.Client;
using Google.GData.Extensions;
using Google.GData.Extensions.Apps;

namespace GoogleAppsMigrationDemo
{
    /// <summary>
    /// Sample class to demonstrate the use of the Google Apps
    /// Domain Migration API client library.
    /// </summary>
    public class MigrationSample
    {
        private static MailItemService mailItemService;

        // Authentication credentials.
        private static string domain;
        private static string adminUsername;
        private static string adminPassword;

        // Destination email account into which mail should be transferred.
        private static string destinationUser;

        // RFC 822 email message to migrate.
        private const string rfcTxt =
            "Received: by 10.143.160.15 with HTTP; Mon, 16 Jul 2007 10:12:26 -0700 (PDT)\r\n" +
            "Message-ID: <_message_id_@mail.gmail.com>\r\n" +
            "Date: Mon, 16 Jul 2007 10:12:26 -0700\r\n" +
            "From: \"Mr. Serious\" <serious@adomain.com>\r\n" +
            "To: \"Mr. Admin\" <testadmin@apps-provisioning-test.com>\r\n" +
            "Subject: Random Subject \r\n" +
            "MIME-Version: 1.0\r\n" +
            "Content-Type: text/plain; charset=ISO-8859-1; format=flowed\r\n" + 
            "Content-Transfer-Encoding: 7bit\r\n" +
            "Content-Disposition: inline\r\n" +
            "Delivered-To: testadmin@apps-provisioning-test.com\r\n" +
            "\r\n" +
            "This is a message delivered via DMAPI\r\n" +
            "\r\n";
        private static readonly Random random = new Random();

        /// <summary>
        /// Sets up the <code>MailItemService</code> based on the provided arguments.
        /// </summary>
        /// <param name="args">arguments passed to the program</param>
        private static void Initialize(string[] args)
        {
            domain = args[0];
            adminUsername = args[1];
            adminPassword = args[2];
            if (args.Length > 3)
            {
                destinationUser = args[3];
            }
            else
            {
                destinationUser = adminUsername;
            }

            mailItemService = new MailItemService(domain, "Sample Migration Application");
            mailItemService.setUserCredentials(adminUsername + "@" + domain, adminPassword);
        }

        /// <summary>
        /// Generates a random RFC822 message based on the template in <code>rfcTxt</code>.
        /// We have to randomly modify the subject and message ID to prevent duplicate
        /// supression by the Gmail server.
        /// </summary>
        /// <returns>the randomly-modified RFC822 message</returns>
        private static string GenerateRandomRfcText()
        {
            string result = rfcTxt;

            result = result.Replace("Random Subject", "Random Subject " + random.Next().ToString());
            result = result.Replace("_message_id_", random.Next().ToString());

            return result;
        }

        /// <summary>
        /// Helper method to set up a new <code>MailItemEntry</code>.
        /// </summary>
        /// <param name="batchId">the batch ID for this entry</param>
        /// <returns>the newly created <code>MailItemEntry</code></returns>
        private static MailItemEntry SetupMailItemEntry(string batchId)
        {
            MailItemEntry entry = new MailItemEntry();

            entry.Labels.Add(new LabelElement("Friends"));
            entry.Labels.Add(new LabelElement("Event Invitations"));

            entry.MailItemProperties.Add(MailItemPropertyElement.INBOX);
            entry.MailItemProperties.Add(MailItemPropertyElement.STARRED);
            entry.MailItemProperties.Add(MailItemPropertyElement.UNREAD);

            entry.Rfc822Msg = new Rfc822MsgElement(GenerateRandomRfcText());

            entry.BatchData = new GDataBatchEntryData();
            entry.BatchData.Id = batchId;

            return entry;
        }

        /// <summary>
        /// Demonstrates inserting several mail items in a batch.
        /// </summary>
        /// <param name="numToInsert">the number of entries to insert</param>
        /// <returns>a <code>MailItemFeed</code> with the results of the insertions</returns>
        private static MailItemFeed BatchInsertMailItems(int numToInsert)
        {
            MailItemEntry[] entries = new MailItemEntry[numToInsert];

            // Set up the mail item entries to insert.
            for (int i = 0; i < numToInsert; i++)
            {
                entries[i] = SetupMailItemEntry(i.ToString());
            }

            // Execute the batch request and print the results.
            MailItemFeed batchResult = mailItemService.Batch(domain, destinationUser, entries);
            foreach (AtomEntry entry in batchResult.Entries)
            {
                GDataBatchEntryData batchData = entry.BatchData;

                Console.WriteLine("Mail message {0}: {1} {2}",
                    batchData.Id, batchData.Status.Code, batchData.Status.Reason);
            }

            return batchResult;
        }

        /// <summary>
        /// Program entry point.  Demonstrates the process of migrating a batch
        /// of email messages using the Google Apps Email Migration API.
        /// </summary>
        /// <param name="args">Authentication parameters, as explained in
        /// the <code>ShowUsage</code> method.</param>
        public static void Main(string[] args)
        {
            if ((args.Length < 3) || (args.Length > 0 && args[0].Equals("--help")))
            {
                ShowUsage();
                return;
            }
            Initialize(args);

            try
            {
                // Insert several emails in a batch.
                MailItemFeed batchResults = BatchInsertMailItems(10);
            }
            catch (GDataRequestException e)
            {
                Console.WriteLine("Operation failed ({0}): {1}", e.Message, e.ResponseString);
            }

        }

        /// <summary>
        /// Shows a message explaining the usage of the sample application.
        /// </summary>
        private static void ShowUsage()
        {
            Console.Error.WriteLine("Usage:" + "\n" + "  migration_sample <domain> <login_email> <login_password> " +
                                               "[destination_email]\n");
            Console.Error.WriteLine("domain:" + "\n" + "  The hosted domain (e.g. example.com) in which the migration " +
                                               "will occur.\n");
            Console.Error.WriteLine("login_user:" + "\n" + "  The username of the administrator or user migrating " +
                                                "mail.\n");
            Console.Error.WriteLine("login_password:" + "\n" + "  The password of the administrator or user migrating " +
                                                "mail.\n");
            Console.Error.WriteLine("destination_user:" + "\n" + "  The username to which emails should be migrated. " +
                                                "End users can only transfer mail to their own mailboxes.  If " +
                                                "unspecified, will default to login_email.");
        }
    }
}
