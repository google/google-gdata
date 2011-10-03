using System;
using System.Text;
using System.IO;
using Google.GData.Apps;
using Google.GData.Apps.Groups;
using Google.GData.Extensions;

namespace GoogleAppsConsoleApplication
{
    class AppsDemo
    {
        private static string domain;
        private static string adminEmail;
        private static string adminPassword;

        // If desired, you may replace these values of username, nickname
        // and email list name with others that do not already exist on
        // your domain.
        private static string testUserName = "testUserName" + new Random().Next().ToString();
        private const string testNickname = "testNickname";
        private static string testGroup = "testGroup" + new Random().Next().ToString();

        private static void UserOperations(AppsService service)
        {
            // Create a new user.
            UserEntry insertedEntry = service.CreateUser(testUserName, "Jane",
                "Doe", "testuser-password");
            Console.WriteLine("Created new user '{0}'", insertedEntry.Login.UserName);

            // Suspend the user.
            UserEntry suspendedEntry = service.SuspendUser(testUserName);
            Console.WriteLine("Suspended account for {0}", suspendedEntry.Login.UserName);

            // Restore the user.
            UserEntry restoredEntry = service.RestoreUser(testUserName);
            Console.WriteLine("Restored user {0}", restoredEntry.Login.UserName);

            // Query for a single user.
            UserEntry entry = service.RetrieveUser(testUserName);
            Console.WriteLine("Retrieved user {0}", entry.Login.UserName);

            // Query for a page of users.
            UserFeed feed = service.RetrievePageOfUsers(testUserName);
            entry = feed.Entries[0] as UserEntry;
            Console.WriteLine("Retrieved page of {0} users, beginning with '{1}'", feed.Entries.Count,
                entry.Login.UserName);

            // Query for all users.
            feed = service.RetrieveAllUsers();
            entry = feed.Entries[0] as UserEntry;
            Console.WriteLine("Retrieved all {0} users in the domain, beginning with '{1}'",
                feed.Entries.Count, entry.Login.UserName);

            // Update the user's given name.
            restoredEntry.Name.GivenName = "John";
            UserEntry updatedEntry = service.UpdateUser(entry);
            Console.WriteLine("Updated user with new given name '{0}'", updatedEntry.Name.GivenName);
        }

        private static void NicknameOperations(AppsService service)
        {
            // Create a new nickname.
            NicknameEntry insertedEntry = service.CreateNickname(testUserName, testNickname);
            Console.WriteLine("Created nickname '{0}' for user {1}", insertedEntry.Nickname.Name,
                insertedEntry.Login.UserName);

            // Retrieve the newly-created nickname.
            NicknameEntry entry = service.RetrieveNickname(testNickname);
            Console.WriteLine("Retrieved nickname {0}", entry.Nickname.Name);

            // Retrieve all nicknames for testUserName (really, just this one).
            NicknameFeed feed = service.RetrieveNicknames(testUserName);
            entry = feed.Entries[0] as NicknameEntry;
            Console.WriteLine("Retrieved nickname '{0}' for user {1}", entry.Nickname.Name,
                entry.Login.UserName);

            // Retrieve a page of nicknames.
            feed = service.RetrievePageOfNicknames(testNickname);
            entry = feed.Entries[0] as NicknameEntry;
            Console.WriteLine("Retrieved page of {0} nicknames, beginning with '{1}'",
                feed.Entries.Count, entry.Nickname.Name);

            // Retrieve the feed of all nicknames.
            feed = service.RetrieveAllNicknames();
            entry = feed.Entries[0] as NicknameEntry;
            Console.WriteLine("Retrieved all {0} nicknames in the domain, beginning with '{1}'",
                feed.Entries.Count, entry.Nickname.Name);
        }

        private static void GroupOperations(AppsService service)
        {
            // Create a new group.
            AppsExtendedEntry insertedEntry = service.Groups.CreateGroup(testGroup, testGroup, testGroup,null);
            Console.WriteLine("Created new group '{0}'", insertedEntry.getPropertyByName("groupId" ).Value);            

            // Retrieve the newly-created group.
            AppsExtendedEntry entry = service.Groups.RetrieveGroup(testGroup);
            Console.WriteLine("Retrieved group '{0}'", entry.getPropertyByName("groupId").Value);

            // Add Member To Group
            AppsExtendedEntry newMemberEntry = service.Groups.AddMemberToGroup(testUserName, testGroup);
            Console.WriteLine("User '{0}' was added as member to group '{1}'", 
                newMemberEntry.getPropertyByName("memberId").Value, testGroup);

            // Add Owner to Group
            AppsExtendedEntry newOwnerEntry = service.Groups.AddOwnerToGroup(testUserName, testGroup);
            Console.WriteLine("User '{0}' was added as ownter to group '{1}'",
                newOwnerEntry.getPropertyByName("email").Value, testGroup);

            // Check if a User is a Group Member
            Console.WriteLine("Is User '{0}' member of group '{1}'? '{2}'",
                 testUserName, testGroup, service.Groups.IsMember(testUserName, testGroup));

            // Check if a User is a Group Member
            Console.WriteLine("Is User '{0}' owner of group '{1}'? '{2}'",
                 testUserName, testGroup, service.Groups.IsOwner(testUserName, testGroup));

            // Remove Member from Group
            service.Groups.RemoveMemberFromGroup(testUserName, testGroup);
            Console.WriteLine("User '{0}' was removed as member to group '{1}'",
               testUserName, testGroup);

            // Remove Owner from Group
            service.Groups.RemoveOwnerFromGroup(testUserName, testGroup);
            Console.WriteLine("User '{0}' was removed as ownter to group '{1}'", 
                testUserName, testGroup);

            // Retreive all groups
            AppsExtendedFeed groupsFeed = service.Groups.RetrieveAllGroups();
            Console.WriteLine("First Group from All groups: '{0}'",
                (groupsFeed.Entries[0] as AppsExtendedEntry).getPropertyByName("groupId").Value);            
        }

        private static void CleanUp(AppsService service)
        {
            // Delete the group that was created
            service.Groups.DeleteGroup(testGroup);
            Console.WriteLine("Deleted group {0}", testGroup);

            // Delete the nickname that was created.
            service.DeleteNickname(testNickname);
            Console.WriteLine("Deleted nickname {0}", testNickname);

            // Delete the user that was created.
            service.DeleteUser(testUserName);
            Console.WriteLine("Deleted user {0}", testUserName);            
        }

        private static void RunSample(AppsService service)
        {
            try
            {
                // Demonstrate operations on user accounts.
                UserOperations(service);

                // Demonstrate operations on nicknames.
                NicknameOperations(service);

                // Demostrate operations on groups
                GroupOperations(service);
                
                // Clean up (delete the username, nickname and email list
                // that were created).
                CleanUp(service);
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
        /// Provisioning API calls.  It accepts authentication information
        /// from the command-line and performs a series of create/
        /// retrieve/update/delete operations on user accounts, nicknames,
        /// and email lists, as described in the comments of RunTests.
        /// </summary>
        /// <param name="args">Command-line arguments: args[0] is
        /// the domain, args[1] is the admin email address, and args[2]
        /// is the admin psasword.
        /// 
        /// Example: AppsDemo example.com admin@example.com my_password</param>
        public static void Main(string[] args)
        {
            if (args.Length != 3)
            {
                Console.WriteLine("Syntax: AppsDemo <domain> <admin_email> <admin_password>");
            }
            else
            {
                domain = args[0];
                adminEmail = args[1];
                adminPassword = args[2];

                AppsService service = new AppsService(domain, adminEmail, adminPassword);

                RunSample(service);
            }
        }
    }
}
