using System;
using System.Collections.Generic;
using System.Text;
using Google.GData.Extensions.Apps;
using Google.GData.Apps;


namespace GoogleAppsConsoleApplication
{
    class AppsMultiDomainDemo
    {
        private static string primaryDomain;
        private static string secondaryDomain;
        private static string adminEmail;
        private static string adminPassword;
        private static string testUserEmail;

        /// <summary>
        /// This console application demonstrates all the Google Apps
        /// MultiDomain Management APIs. 
        /// </summary>
        /// <param name="args">Command-line arguments: args[0] is
        /// the primary domain, args[1] is the secondary domain,
        /// args[2] is the admin email address, args[3] is the admin 
        /// password and args[4] is test user(email address)
        /// 
        /// Example: AppsMultiDomainDemo example.com alias.com admin@example.com my_password test_user_email</param>
        public static void Main(string[] args)
        {
            if (args.Length != 5)
            {
                Console.WriteLine("Syntax: AppsMultiDomainDemo <primary_domain> <secondary_domain> <admin_email> <admin_password> <testUserEmail>");
            }
            else
            {
                primaryDomain = args[0];
                secondaryDomain = args[1];
                adminEmail = args[2];
                adminPassword = args[3];
                testUserEmail = args[4];

                MultiDomainManagementService service = new MultiDomainManagementService(primaryDomain, "multidomainapis-apps-demo");
                service.setUserCredentials(adminEmail, adminPassword);

                RunSample(service);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        private static void RunSample(MultiDomainManagementService service)
        {
            const String testUserHashFunction = "SHA-1";
            const String testUserPassword = "51eea05d46317fadd5cad6787a8f562be90b4446";
            const String testUserFirstName = "Liz";
            const String testUserLastName = "Smith";
            const bool testUserIsAdmin = true;
            const String testUserNewFirstName = "Elizabeth";
            String testUserNewEmail = "liz@" + secondaryDomain;
            String testUserAliasEmail = "helpdesk@" + secondaryDomain;

            try
            {
                // Create a new Domain User
                Console.WriteLine("\n-----------Creating domain user-----------");
                AppsExtendedEntry entry = service.CreateDomainUser(primaryDomain, testUserEmail, testUserPassword,
                    testUserHashFunction, testUserFirstName, testUserLastName, testUserIsAdmin);
                Console.WriteLine("Created: " +
                                  entry.getPropertyValueByName(AppsMultiDomainNameTable.UserEmail));

                // Update Domain User and list all properties
                Console.WriteLine("\n-----------Updating domain user----");
                IDictionary<MultiDomainManagementService.MultiDomainUserProperty, string> updates =
                    new Dictionary<MultiDomainManagementService.MultiDomainUserProperty, string>();
                updates[MultiDomainManagementService.MultiDomainUserProperty.FirstName] = testUserNewFirstName;
                entry = service.UpdateDomainUser(primaryDomain, testUserEmail, updates);
                foreach (PropertyElement element in entry.Properties)
                {
                    Console.WriteLine(String.Format("{0} - {1}", element.Name, element.Value));
                }

                // Create a new Domain User to be renamed
                Console.WriteLine("\n-----------Creating domain user to be renamed-----------");
                String tempEmail = "TOBERENAMED@" + primaryDomain;
                entry = service.CreateDomainUser(primaryDomain, tempEmail, testUserPassword,
                                                 testUserFirstName, testUserLastName, testUserIsAdmin);
                Console.WriteLine("Created: " +
                                  entry.getPropertyValueByName(AppsMultiDomainNameTable.UserEmail));

                // Rename Domain User
                Console.WriteLine("\n-----------Renaming domain user---------------------");
                entry = service.RenameDomainUser(primaryDomain, tempEmail, testUserNewEmail);
                Console.WriteLine("Renamed domain user: " +
                                  entry.getPropertyValueByName(AppsMultiDomainNameTable.NewEmail));

                // Retrieve Domain User
                Console.WriteLine("\n-----------Retrieving domain user----");
                entry = service.RetrieveDomainUser(primaryDomain, testUserEmail);
                String firstName =
                    entry.getPropertyValueByName(AppsMultiDomainNameTable.FirstName);
                Console.WriteLine("FirstName: " + firstName);

                // Retrieve all domain users unit and list the emails
                Console.WriteLine("\n-----------Retrieving all domain users----");
                AppsExtendedFeed feed = service.RetrieveAllDomainUsers(primaryDomain);
                foreach (AppsExtendedEntry unit in feed.Entries)
                {
                    Console.WriteLine(
                        unit.getPropertyValueByName(AppsMultiDomainNameTable.UserEmail));
                }

                // Create a new User Alias
                Console.WriteLine("\n-----------Creating user alias-----------");
                entry = service.CreateDomainUserAlias(primaryDomain, testUserEmail, testUserAliasEmail);
                Console.WriteLine("Created Alias: " +
                                  entry.getPropertyValueByName(AppsMultiDomainNameTable.AliasEmail));

                // Retrieve User Alias
                entry = service.RetrieveDomainUserAlias(primaryDomain, testUserAliasEmail);
                String userEmail =
                    entry.getPropertyValueByName(AppsMultiDomainNameTable.UserEmail);
                Console.WriteLine("UserEmail: " + userEmail);

                // Retrieve all user aliases for the domain
                Console.WriteLine("\n-----------Retrieving all user aliases----");
                feed = service.RetrieveAllDomainUserAlias(primaryDomain);
                foreach (AppsExtendedEntry unit in feed.Entries)
                {
                    Console.WriteLine(
                        unit.getPropertyValueByName(AppsMultiDomainNameTable.UserEmail));
                }

                // Retrieve all aliases for an user
                Console.WriteLine("\n-----------Retrieving all aliases for user----");
                feed = service.RetrieveAllDomainUserAliasForUser(primaryDomain, testUserEmail);
                foreach (AppsExtendedEntry unit in feed.Entries)
                {
                    Console.WriteLine(
                        unit.getPropertyValueByName(AppsMultiDomainNameTable.AliasEmail));
                }

                // Delete User Alias
                Console.WriteLine("\n-----------Deleting alias----");
                service.DeleteDomainUserAlias(primaryDomain, testUserAliasEmail);

                // Delete User
                Console.WriteLine("\n-----------Deleting user----");
                service.DeleteDomainUser(primaryDomain, testUserEmail);
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
    }
}
