using System;
using System.Collections.Generic;
using System.Text;
using Google.GData.Apps;
using Google.GData.Extensions.Apps;

namespace GoogleAppsConsoleApplication {
    class AuditDemo {
        private static string domain;
        private static string adminEmail;
        private static string adminPassword;
        private static string srcUsername;
        private static string destUsername;

        /// <summary>
        /// This console application demonstrates the usage of the Google Apps Audit API.
        /// </summary>
        /// <param name="args">Command-line arguments: args[0] is
        /// the domain, args[1] is the admin email address,
        /// args[2] is the admin password, args[3] is the source 
        /// username and args[4] is destination username
        /// 
        /// Example: AuditDemo example.com admin@example.com my_password src_username dest_username</param>
        public static void Main(string[] args) {
            if (args.Length != 5) {
                Console.WriteLine("Syntax: AuditDemo <domain> <admin_email> <admin_password> <src_username> <dest_username>");
            } else {
                domain = args[0];
                adminEmail = args[1];
                adminPassword = args[2];
                srcUsername = args[3];
                destUsername = args[4];

                AuditService service = new AuditService(domain, "audit-demo");
                service.setUserCredentials(adminEmail, adminPassword);

                RunSample(service);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        private static void RunSample(AuditService service) {
            //A sample base64 encoded PGP format key
            string sampleKey = "LS0tLS1CRUdJTiBQR1AgUFVCTElDIEtFWSBCTE9DSy0tLS0tClZlcnNpb246IEdudVBHIHYxLjQu\n" +
                "MTAgKEdOVS9MaW51eCkKCm1RRU5CRXhGbWM0QkNBRG9RUU5OVzdRQTB5anNIWkhNVEY5RU9pMjJa\n" +
                "TTR2QkQrK3E5TkNvME5OcTA1b0R1WDQKR3JUWFVGdk9KV1ZoVDNtdCtaaCtSQlBhS3NYVUd3ZEpw\n" +
                "R1ZzMk4rbzZyMXF6TXorZ0paNEN3NDJZMzNRbjZJRgpPOTh4Mkh0TVNTUGk1QlVTYlExSnA3czYx\n" +
                "MVlPMWtzTmtUOFpiSDAvSHErcUhSQzF6L1g4cVZNK3hlSGh4Ull6Cml6MEx2bytYZGMvNDUwRUEx\n" +
                "dkRhNTRyM3Z5MUFIT0xhWDRpcmFzQ0I4N3NLV0YxcUp6UTQ5Zkw5SnRxZWVDOFAKYUtVbmlmQi9h\n" +
                "UXNHdER2cGVVVWt6NDRPRjB5R2pjRTg3b1NHTTdWSEJxZmVKb0xubStzYmgyWkZaSnZEZWVJWAph\n" +
                "ZzJSS3hpakJhcGlmR0xCcGkvc0p6c3pDekUrOFhFRDBCVEJBQkVCQUFHMEprTnNZWFZrYVc4Z1Ey\n" +
                "aGxjblZpCmFXNXZJRHhqYkdGMVpHbHZRR2R2YjJkc1pTNWpiMjAraVFFNEJCTUJBZ0FpQlFKTVJa\n" +
                "bk9BaHNEQmdzSkNBY0QKQWdZVkNBSUpDZ3NFRmdJREFRSWVBUUlYZ0FBS0NSRGJmeldDc0dtQWUw\n" +
                "SzRCLzlZaEpGdXN3NGE5Q1JEYXU2dgpDVnJNRTlrVko4UmxocEpCZEY2V1pndGRPZlhsbzJycEVt\n" +
                "Q1FRZ0lCRGJEeS9aQ3JHRWZDWlRIa0VLeXQrVVNpCndnUWY3NFRZZ0NzMTR0WmF5WHZKT3dENkIz\n" +
                "QW1NYkRtQVFVM0ppdVRaMXIzZEZYV3BnUTE4RDI0YUEwcGVCOWMKbmcwbXc0enhGUW15b29paG14\n" +
                "NWwvZmZ4UDFQUHNrL1kvV3F5ck5HQVhDUGxmRUFRdFN3bWRGYXNoVjYyZElEdwpOZXBMeTZSeE15\n" +
                "TFRTL2ZvMExmSnEreWVjWjZkck91bTlkUnA3T0ZUOERSWmNqSTQ4eVM5VFNMd1Rtc0hHUm1uCkNO\n" +
                "Nk1BWnBaK2lkVks5VEkveXVhOG4xQndNU3kwU3NGSjVpSG8wUFFHbSt5MHZxcDdGN2RCUlVCYWFw\n" +
                "MGc0K3QKOTEyRQo9aFpYYwotLS0tLUVORCBQR1AgUFVCTElDIEtFWSBCTE9DSy0tLS0tCg==";

            try {
                // Upload public key
                Console.WriteLine("\n-----------Uploading public key-----------");
                AppsExtendedEntry keyEntry = service.UploadPublicKey(sampleKey);

                // Setting the parameters for a MailMonitor request
                MailMonitor monitor = new MailMonitor();
                monitor.BeginDate = new DateTime(2011, 6, 15);
                monitor.EndDate = new DateTime(2011, 6, 30, 23, 20, 0);
                monitor.IncomingEmailMonitorLevel = MonitorLevel.FULL_MESSAGE;
                monitor.OutgoingEmailMonitorLevel = MonitorLevel.HEADER_ONLY;
                monitor.DraftMonitorLevel = MonitorLevel.FULL_MESSAGE;
                monitor.ChatMonitorLevel = MonitorLevel.FULL_MESSAGE;
                monitor.DestinationUserName = destUsername;

                // Send the MailMonitor creation request
                Console.WriteLine("\n-----------Creating mail monitor-----------");
                MailMonitor monitorEntry = service.CreateMailMonitor(srcUsername, monitor);

                // Retrieve all MailMonitors for the source user
                Console.WriteLine("\n-----------Retrieving all mail monitors-----------");
                GenericFeed<MailMonitor> monitors = service.RetrieveMailMonitors(srcUsername);
                foreach (MailMonitor m in monitors.Entries) {
                    Console.WriteLine(m.DestinationUserName);
                }

                // Delete MailMonitor
                Console.WriteLine("\n-----------Deleting mail monitor-----------");
                service.DeleteMailMonitor(srcUsername, destUsername);

                // Create Account Info request
                Console.WriteLine("\n-----------Creating account info request-----------");
                AccountInfo accountInfoRequest = service.CreateAccountInfoRequest(srcUsername);
                Console.WriteLine("Request ID: " + accountInfoRequest.RequestId);

                // Retrieve Account Info request
                Console.WriteLine("\n-----------Retrieving account info request-----------");
                AccountInfo accountInfo = service.RetrieveAccountInfoRequest(srcUsername, accountInfoRequest.RequestId);
                Console.WriteLine("Status: " + accountInfo.Status);

                // Retrieve all Account Info requests from April 1st 2011
                Console.WriteLine("\n-----------Retrieving all account info requests-----------");
                GenericFeed<AccountInfo> accountInfoRequests = service.RetrieveAllAccountInfoRequests(new DateTime(2011, 4, 1));
                foreach (AccountInfo info in accountInfoRequests.Entries) {
                    Console.WriteLine(info.RequestId + " - " + info.Status);
                }

                // Delete Account Info request
                // This can only be done when the Status is COMPLETED or MARKED_DELETE
                //Console.WriteLine("\n-----------Deleting the account info request-----------");
                //service.DeleteAccountInfoRequest(srcUsername, accountInfoRequest.RequestId);

                // Setting the parameters for a MailboxDump request
                MailboxDumpRequest mailboxDumpRequest = new MailboxDumpRequest();
                mailboxDumpRequest.BeginDate = new DateTime(2009, 6, 15);
                mailboxDumpRequest.EndDate = new DateTime(2009, 6, 30, 23, 20, 0);
                mailboxDumpRequest.SearchQuery = "in:chat";
                mailboxDumpRequest.PackageContent = MonitorLevel.FULL_MESSAGE;

                // Create Mailbox Dump request
                Console.WriteLine("\n-----------Creating mailbox dump request-----------");
                MailboxDumpRequest dumpRequest = service.CreateMailboxDumpRequest(srcUsername, mailboxDumpRequest);
                Console.WriteLine("Request ID: " + dumpRequest.RequestId);

                // Retrieve Mailbox Dump request
                Console.WriteLine("\n-----------Retrieving mailbox dump request-----------");
                MailboxDumpRequest checkRequest = service.RetrieveMailboxDumpRequest(srcUsername, dumpRequest.RequestId);
                Console.WriteLine("Status: " + dumpRequest.Status);

                // Retrieve all Mailbox Dump requests
                Console.WriteLine("\n-----------Retrieving all mailbox dump requests-----------");
                GenericFeed<MailboxDumpRequest> dumpRequests = service.RetrieveAllMailboxDumpRequests();
                foreach (MailboxDumpRequest dump in dumpRequests.Entries) {
                    Console.WriteLine(dump.RequestId + " - " + dump.Status);
                }

                // Delete Mailbox Dump request
                // This can only be done when the Status is COMPLETED or MARKED_DELETE
                //Console.WriteLine("\n-----------Deleting the mailbox dump request-----------");
                //service.DeleteMailboxDumpRequest(srcUsername, dumpRequest.RequestId);

            } catch (AppsException a) {
                Console.WriteLine("A Google Apps error occurred.");
                Console.WriteLine();
                Console.WriteLine("Error code: {0}", a.ErrorCode);
                Console.WriteLine("Invalid input: {0}", a.InvalidInput);
                Console.WriteLine("Reason: {0}", a.Reason);
            }
        }
    }
}
