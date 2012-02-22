using System;
using System.Collections.Generic;
using System.Text;
using Google.GData.Apps;
using Google.GData.Extensions.Apps;
using Google.GData.Client;
using Google.Contacts;
using Google.GData.Apps.Groups;

namespace GoogleAppsConsoleApplication {
    class OAuth2Demo {
        private static string clientId;
        private static string clientSecret;
        private static string domain;

        private static string applicationName = "Test-OAuth2";

        // Installed (non-web) application
        private static string redirectUri = "urn:ietf:wg:oauth:2.0:oob";

        // Requesting access to Contacts API and Groups Provisioning API
        private static string scopes = "https://www.google.com/m8/feeds/ https://apps-apis.google.com/a/feeds/groups/";

        /// <summary>
        /// This console application demonstrates the usage of OAuth 2.0 with the Google Apps APIs.
        /// </summary>
        /// <param name="args">Command-line arguments: args[0] is
        /// the client ID, args[1] is the client secret, args[2] is domain name.
        /// </param>
        public static void Main(string[] args) {
            if (args.Length != 3) {
                Console.WriteLine("Syntax: OAuth2Demo <client_id> <client_secret> <domain>");
            } else {
                clientId = args[0];
                clientSecret = args[1];
                domain = args[2];

                OAuth2Parameters parameters = new OAuth2Parameters() {
                    ClientId = clientId,
                    ClientSecret = clientSecret,
                    RedirectUri = redirectUri,
                    Scope = scopes
                };

                string url = OAuthUtil.CreateOAuth2AuthorizationUrl(parameters);
                Console.WriteLine("Authorize URI: " + url);
                parameters.AccessCode = Console.ReadLine();

                OAuthUtil.GetAccessToken(parameters);

                // Testing OAuth 2.0 with a Request-based library
                RunContactsSample(parameters);

                // Testing OAuth 2.0 with a Service-based library
                RunGroupsSample(parameters, domain);
            }
        }

        /// <summary>
        /// Send authorized queries to a Request-based library
        /// </summary>
        /// <param name="service"></param>
        private static void RunContactsSample(OAuth2Parameters parameters) {
            try {
                RequestSettings settings = new RequestSettings(applicationName, parameters);
                ContactsRequest cr = new ContactsRequest(settings);

                Feed<Contact> f = cr.GetContacts();
                foreach (Contact c in f.Entries) {
                    Console.WriteLine(c.Name.FullName);
                }
            } catch (AppsException a) {
                Console.WriteLine("A Google Apps error occurred.");
                Console.WriteLine();
                Console.WriteLine("Error code: {0}", a.ErrorCode);
                Console.WriteLine("Invalid input: {0}", a.InvalidInput);
                Console.WriteLine("Reason: {0}", a.Reason);
            }
        }

        /// <summary>
        /// Send authorized queries to a Service-based library
        /// </summary>
        /// <param name="service"></param>
        private static void RunGroupsSample(OAuth2Parameters parameters, string domain) {
            try {
                GOAuth2RequestFactory requestFactory = new GOAuth2RequestFactory("apps", applicationName, parameters);

                GroupsService service = new GroupsService(domain, applicationName);
                service.RequestFactory = requestFactory;

                GroupFeed feed = service.RetrieveAllGroups();
                foreach (GroupEntry group in feed.Entries) {
                    Console.WriteLine(group.GroupName);
                }
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
