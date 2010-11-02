using System;
using System.Collections.Generic;
using System.Text;
using Google.GData.Client;
using Google.GData.Extensions.Apps;
using Google.GData.Apps;


namespace GoogleAppsConsoleApplication
{
    class AppsOrgManagementDemo
    {
        private static string domain;
        private static string adminEmail;
        private static string adminPassword;
        private static string testUser;

        /// <summary>
        /// This console application demonstrates all the Google Apps
        /// Organization Management APIs. 
        /// </summary>
        /// <param name="args">Command-line arguments: args[0] is
        /// the domain, args[1] is the admin email address, args[2]
        /// is the admin password and arg[3] is test user(email address)
        /// 
        /// Example: AppsOrgManagementDemo example.com admin@example.com my_password test_user_email</param>
        public static void Main(string[] args)
        {
            if (args.Length != 4)
            {
                Console.WriteLine(
                    "Syntax: AppsOrgManagementDemo <domain> <admin_email> <admin_password> <test_user_email>");
            }
            else
            {
                domain = args[0];
                adminEmail = args[1];
                adminPassword = args[2];
                testUser = args[3];

                OrganizationService service = new OrganizationService(domain, "organizationapis-apps-demo");
                service.setUserCredentials(adminEmail, adminPassword);

                RunSample(service);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        private static void RunSample(OrganizationService service)
        {
            try
            {
                const String testOrgunit = "TestOrgUnitForSample";
                const String testOrgunitDescription = "Test Organization";

                // Retrieve customer Id
                AppsExtendedEntry entry = service.RetrieveCustomerId(service.DomainName);
                String customerId =
                    entry.getPropertyValueByName(AppsOrganizationNameTable.CustomerId);
                Console.WriteLine("CustomerId: " + customerId);

                // Delete, if all already exists
                try
                {
                    service.DeleteOrganizationUnit(customerId, testOrgunit);
                }
                catch
                {
                }

                // Create a new Organization Unit
                Console.WriteLine("\n-----------Creating organization unit-----------");
                entry = service.CreateOrganizationUnit(
                    customerId, testOrgunit, "/", testOrgunitDescription, false);
               
                Console.WriteLine("Created: " + 
                    entry.getPropertyValueByName(AppsOrganizationNameTable.OrgUnitName));

                // Retrieve Organization Unit and list all properties
                Console.WriteLine("\n-----------Retrieving organization unit---------");
                entry = service.RetrieveOrganizationUnit(customerId, testOrgunit);

                foreach (PropertyElement element in entry.Properties)
                {
                    Console.WriteLine(String.Format("{0} - {1}", element.Name, element.Value));
                }

                // Update organization unit and list all properties
                Console.WriteLine("\n-----------Updating organization unit----");
                IDictionary<OrganizationService.OrgUnitProperty, string> updates =
                    new Dictionary<OrganizationService.OrgUnitProperty, string>();
                updates[OrganizationService.OrgUnitProperty.Description] = "Updated description";
                entry = service.UpdateOrganizationUnit(customerId, testOrgunit, updates);
                foreach (PropertyElement element in entry.Properties)
                {
                    Console.WriteLine(String.Format("{0} - {1}", element.Name, element.Value));
                }

                // Retrieve all organization units and list the names
                Console.WriteLine("\n-----------Retrieving all organization units----");
                AppsExtendedFeed feed = service.RetrieveAllOrganizationUnits(customerId);
                foreach (AppsExtendedEntry unit in feed.Entries)
                {
                    Console.WriteLine(
                        unit.getPropertyValueByName(AppsOrganizationNameTable.OrgUnitName));
                }

                // Retrieve child organization unit of a given unit
                Console.WriteLine("\n-----------Retrieving child organization units----");
                feed = service.RetrieveChildOrganizationUnits(customerId, testOrgunit);
                foreach (AppsExtendedEntry unit in feed.Entries)
                {
                    Console.WriteLine(
                        unit.getPropertyValueByName(AppsOrganizationNameTable.OrgUnitName));
                }

                // Retrieve org user
                Console.WriteLine("\n-----------Retrieving Org User-------------------");
                entry = service.RetrieveOrganizationUser(customerId, testUser);
                Console.WriteLine("Retrieved OrgUser");
                foreach (PropertyElement element in entry.Properties)
                {
                    Console.WriteLine(String.Format("{0} - {1}", element.Name, element.Value));
                }


                // update org user i.e. move from one org unit to another
                Console.WriteLine("\n-----------Updating Org User---------------------");
                entry = service.UpdateOrganizationUser(customerId, testUser, testOrgunit, "/");
                Console.WriteLine("Updated OrgUser");
                foreach (PropertyElement element in entry.Properties)
                {
                    Console.WriteLine(String.Format("{0} - {1}", element.Name, element.Value));
                }

                // Retrieve all org users
                Console.WriteLine("\n-----------Retrieving all Org Users--------------");
                feed = service.RetrieveAllOrganizationUsers(customerId);
                Console.WriteLine("Retrieved User count:  " + feed.Entries.Count);

                //using pagination
                Console.WriteLine("\n--------Retrieving all Org Users(paginated)------");
                feed = service.RetrieveFirstPageOrganizationUsers(customerId);
                Console.WriteLine("Retrieved User count:  " + feed.Entries.Count);
                AtomLink next, prev = null;
                while ((next = feed.Links.FindService("next", null)) != null && prev != next)
                {
                    feed = service.RetrieveNextPageFromResumeKey(next.HRef.ToString());
                    prev = next;
                    Console.WriteLine("Retrieved User count:  " + feed.Entries.Count);
                }


                // Retrieve org users by org unit
                Console.WriteLine("\n-----------Retrieving Org Users by orgunit--------------");
                feed = service.RetrieveAllOrganizationUsersByOrgUnit(customerId, testOrgunit);
                Console.WriteLine("Retrieved User count:  " + feed.Entries.Count);

                //cleanup
                try
                {
                    Console.WriteLine("\nCleaning up...");
                    entry = service.UpdateOrganizationUser(customerId, testUser, "/", testOrgunit);
                    service.DeleteOrganizationUnit(customerId, testOrgunit);
                }
                catch
                {
                }
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
