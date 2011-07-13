using System;
using System.Collections.Generic;
using System.Text;
using Google.GData.Apps;
using Google.GData.Apps.GoogleMailSettings;
using Google.GData.Client;
using Google.GData.Extensions;

namespace GoogleAppsMailSettingsDemo
{
    class GoogleMailSettingsDemo
    {
        private static string domain;
        private static string adminEmail;
        private static string adminPassword;
        private static string testUserName;

        private static void RunSample(GoogleMailSettingsService service)
        {
            try
            {
                // Create a new Label for the user testUserName
                service.CreateLabel(testUserName, "Test-Label");

				// Retrieve all labels for the user testUserName
				AppsExtendedFeed labels = service.RetrieveLabels(testUserName);
				Console.WriteLine(String.Format("First label: {0}",
					((AppsExtendedEntry)labels.Entries[0]).getPropertyValueByName("label")));

                // Create a filter for emails from test@domain.com
                // for the user testUserName and applies the new label "Test-Label"
                service.CreateFilter(testUserName, "test@"+domain, "", "", "", "", "", "Test-Label", "true", "");

                // Create a filter for emails having "important" in the subject
                // for the user testUserName to never send them to Spam and star them
                service.CreateFilter(testUserName, "", "", "important", "", "", "", "", "", "", "true", "true", "", "");

                // Create a new Send As for the user testUserName
                service.CreateSendAs(testUserName, "Test email", testUserName+"@"+domain, "", "");

				// Retrieve all send-as for user testUserName
				AppsExtendedFeed sendas = service.RetrieveSendAs(testUserName);
				Console.WriteLine(String.Format("First send-as: {0}",
					((AppsExtendedEntry)sendas.Entries[0]).getPropertyValueByName("name")));

                // Updates the forwarding rule to forward emails to 
                // test@domain for the user testUserName and keeps the email.
                service.UpdateForwarding(testUserName, "true", "test@"+domain, "KEEP");

                // Disable web clip for the user testUserName.
                service.UpdateWebclip(testUserName, "false");

				// Retrieve forwarding settings for user testUserName
				AppsExtendedEntry forwarding = service.RetrieveForwarding(testUserName);
				Console.WriteLine(String.Format("Forwarding to: {0}",
					forwarding.getPropertyValueByName("forwardTo")));

                // Deactivate POP for the user testUserName
                service.UpdatePop(testUserName, "false", null, null);

				// Retrieve POP settings for user testUserName
				AppsExtendedEntry pop = service.RetrievePop(testUserName);
				Console.WriteLine(String.Format("POP enabled: {0}",
					pop.getPropertyValueByName("enable")));

                // Activate IMAP for the user testUserName
                service.UpdateImap(testUserName, "true");

				// Retrieve IMAP settings for user testUserName
				AppsExtendedEntry imap = service.RetrieveImap(testUserName);
				Console.WriteLine(String.Format("IMAP enabled: {0}",
					imap.getPropertyValueByName("enable")));

                // Activate vacation autoresponse for the user testUserName
                service.UpdateVacation(testUserName, "true", "vacation", "vacation text...", "false", "true", "2012-01-15", "2012-01-22");

				// Retrieve vacation responder settings for user testUserName
				AppsExtendedEntry vacation = service.RetrieveVacation(testUserName);
				Console.WriteLine(String.Format("Vacation responder message: {0}",
					vacation.getPropertyValueByName("message")));

                // Update the signature for the user testUserName
                service.UpdateSignature(testUserName, "Signature text...");

				// Retrieve signature for user testUserName
				AppsExtendedEntry signature = service.RetrieveSignature(testUserName);
				Console.WriteLine(String.Format("Signature: {0}",
					signature.getPropertyValueByName("signature")));

                // Update the language settings to French (fr) for the user testUserName
                service.UpdateLanguage(testUserName, "fr");

                // Update general settings for the user testUserName
                service.UpdateGeneralSettings(testUserName, "50", "false", "false", "false", "false");

                // Create a new Delegate for the user testUserName
                service.CreateDelegate(testUserName, adminEmail);

                // Retrieve all delegates for the user testUserName
                AppsExtendedFeed delegates = service.RetrieveDelegates(testUserName);
                Console.WriteLine(String.Format("First delegate: {0}",
                    ((AppsExtendedEntry)delegates.Entries[0]).getPropertyValueByName("delegationId")));

                // Delete the Delegate for the user testUserName
                service.DeleteDelegate(testUserName, adminEmail);
            }
            catch (AppsException a)
            {
                Console.WriteLine("A Google Apps error occurred.");
                Console.WriteLine();
                Console.WriteLine("Error code: {0}", a.ErrorCode);
                Console.WriteLine("Invalid input: {0}", a.InvalidInput);
                Console.WriteLine("Reason: {0}", a.Reason);
            }
        }

        /// <summary>
        /// This console application demonstrates all the Google Apps
        /// Mail Settings API calls. 
        /// </summary>
        /// <param name="args">Command-line arguments: args[0] is
        /// the domain, args[1] is the admin email address, args[2]
        /// is the admin password, and args[3] is the username to be modified 
        /// by all actions. 
        /// 
        /// Example: GoogleMailSettingsDemo example.com admin@example.com my_password user_name</param>
        public static void Main(string[] args)
        {
            if (args.Length != 4)
            {
                Console.WriteLine("Syntax: AppsDemo <domain> <admin_email> <admin_password> <test_username>");
            }
            else
            {
                domain = args[0];
                adminEmail = args[1];
                adminPassword = args[2];
                testUserName = args[3];

                GoogleMailSettingsService service = new GoogleMailSettingsService(domain, "apps-demo");
                service.setUserCredentials(adminEmail, adminPassword);

                RunSample(service);
            }
        }
    }
}
