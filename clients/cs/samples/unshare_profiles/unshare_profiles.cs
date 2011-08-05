using System;
using System.Collections.Generic;

using Google.Contacts;
using Google.GData.Contacts;
using Google.GData.Client;
using Google.GData.Extensions;

namespace ProfilesDemoConsoleApplication {
    //////////////////////////////////////////////////////////////////////
    /// <summary>hold batch processing results
    /// </summary>
    //////////////////////////////////////////////////////////////////////
    public class BatchResult {
        public int Success { get; set; }
        public int Error { get; set; }
        public List<Contact> ErrorEntries { get; set; }
    }

    //////////////////////////////////////////////////////////////////////
    /// <summary>used to unshare domain users contact information
    /// </summary>
    //////////////////////////////////////////////////////////////////////
    public class ProfilesManager {
        private String domain;
        private ContactsRequest cr;
        private List<Contact> profiles;

        /// <summary>
        /// constructs a new ProfilesManager and authenticate using 2-Legged OAuth
        /// </summary>
        /// <param name="consumerKey">Domain's consumer key</param>
        /// <param name="consumerSecret">Domain's consumer secret</param>
        /// <param name="adminEmail">Domain administrator's email</param>
        public ProfilesManager(String consumerKey, String consumerSecret, String adminEmail) {
            String admin = adminEmail.Substring(0, adminEmail.IndexOf('@'));
            this.domain = adminEmail.Substring(adminEmail.IndexOf('@') + 1);

            RequestSettings settings =
                new RequestSettings("GoogleInc-UnshareProfilesSample-1", consumerKey,
                                    consumerSecret, admin, this.domain);
            settings.AutoPaging = true;
            this.cr = new ContactsRequest(settings);

            this.BatchSize = 100;
        }

        /// <summary>
        /// get or set the batch processing size
        /// </summary>
        /// <returns></returns>
        public int BatchSize { get; set; }

        /// <summary>
        /// returns the list of profiles for the domain
        /// </summary>
        /// <returns></returns>
        public List<Contact> Profiles {
            get {
                if (this.profiles == null) {
                    this.GetAllProfiles();
                }
                return this.profiles;
            }
        }

        /// <summary>
        /// retrieve all profiles for the domain
        /// </summary>
        public void GetAllProfiles() {
            ContactsQuery query =
                new ContactsQuery("https://www.google.com/m8/feeds/profiles/domain/" + this.domain + "/full");

            Feed<Contact> f = cr.Get<Contact>(query);
            this.profiles = new List<Contact>(f.Entries);
        }

        /// <summary>
        /// Unshare all profiles for the domain
        /// </summary>
        public BatchResult UnshareProfiles() {
            BatchResult result = new BatchResult() {
                ErrorEntries = new List<Contact>(),
            };
            int index = 0;

            if (this.profiles == null) {
                this.GetAllProfiles();
            }
            while (index < this.Profiles.Count) {
                List<Contact> requestFeed = new List<Contact>();

                for (int i = 0; i < this.BatchSize && index < this.Profiles.Count; ++i, ++index) {
                    Contact entry = this.Profiles[index];

                    entry.ContactEntry.Status = new Status(false);
                    entry.BatchData = new GDataBatchEntryData(GDataBatchOperationType.update);
                    requestFeed.Add(entry);
                }

                Feed<Contact> responseFeed =
                    cr.Batch(requestFeed,
                             new Uri("https://www.google.com/m8/feeds/profiles/domain/" + this.domain + "/full/batch"),
                             GDataBatchOperationType.Default);

                // Check the status of each operation.
                foreach (Contact entry in responseFeed.Entries) {
                    if (entry.BatchData.Status.Code == 200) {
                        ++result.Success;
                    } else {
                        ++result.Error;
                        result.ErrorEntries.Add(entry);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Runs the methods above to demonstrate usage of the .NET
        /// client library.
        /// </summary>
        static void Main(string[] args) {
            if (args.Length != 3) {
                Console.WriteLine("Usage: unshare_profiles <consumerKey> <consumerSecret> <adminEmail>");
            } else {
                String consumerKey = args[0];
                String consumerSecret = args[1];
                String adminEmail = args[2];

                ProfilesManager manager = new ProfilesManager(consumerKey, consumerSecret, adminEmail);

                BatchResult result = manager.UnshareProfiles();

                Console.WriteLine("Success: " + result.Success + " - Error: " + result.Error);
                foreach (Contact entry in result.ErrorEntries) {
                    Console.WriteLine(" > Failed to update " + entry.Id +
                                      ": (" + entry.BatchData.Status.Code + ") "
                                      + entry.BatchData.Status.Reason);
                }
            }
        }
    }
}
