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

                // Create a filter for emails from test@domain.com 
                // for the user testUserName and applies the new label "Test-Label"
                service.CreateFilter(testUserName, "test@"+domain, "", "", "", "", "", "Test-Label", "true", "");

                // Create a new Send As for the user testUserName
                service.CreateSendAs(testUserName, "Test email", testUserName+"@"+domain, "", "");

                // Updates the forwarding rule to forward emails to 
                // test@domain.com for the user testUserName amd keeps the email.
                service.UpdateForwarding(testUserName, "true", "test@"+domain, "KEEP");

                // Deactivate POP for the user testUserName
                service.UpdatePop(testUserName,"false", null, null);

                // Activate IMAP for the user testUserName
                service.UpdateImap(testUserName, "true");

                // Activate vacation autoresponse for the user testUserName
                service.UpdateVacation(testUserName, "true", "vacation", "vacation text...", "true");

                // Update the signature for the user testUserName
                service.UpdateSignature(testUserName, "Signature text...");

                // Update the language settings to French (fr) for the user testUserName
                service.UpdateLanguage(testUserName, "fr");

                // Update general settings for the user testUserName
                service.UpdateGeneralSettings(testUserName, "50", "false", "false", "false", "false");
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
        /// Google Mail Settings API calls. 
        /// </summary>
        /// <param name="args">Command-line arguments: args[0] is
        /// the domain, args[1] is the admin email address, args[2]
        /// is the admin psasword, and args[3] is the username to be modified 
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
                service.setUserCredentials( adminEmail, adminPassword);

                RunSample(service);
            }
        }
    }
}
