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
#define USE_TRACING
#define DEBUG

using System;
using System.IO;
using System.Xml; 
using System.Net; 
using Google.GData.Client;
using Google.GData.Health;

namespace Google.GData.Client.Samples
{
    /// <summary>
    ///  simple utility app for the Health service
    /// </summary>
    class HealthTool
    {
        /// <summary>name of this application</summary>
        public const string ApplicationName = "HealthTool/1.0.0";  
            
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {

            if (args.Length == 0)
            {
                ShowUsage();
                return;
            }
            string pid       = findValue(args, "pid", null);
            string pname     = findValue(args, "pname", null);
            string authToken = findValue(args, "a", null);
            string exchangeToken = findValue(args, "e", null);
            string userName     = findValue(args, "user", null);
            string passWord     = findValue(args, "pwd", null);

         
            bool   isRegister   = isOption(args, "register", false);
            bool   isInsert     = isOption(args, "insert", false);
            bool   isClean      = isOption(args, "clean", false);
            bool   isShow       = isOption(args, "show", false);
            bool   isList       = isOption(args, "list", false);
            bool   doCCR        = isOption(args, "ccr", false);
            bool   doSummary    = isOption(args, "summary", false); 

            if ((pid == null && pname == null && isList == false) && (exchangeToken == null && authToken == null))
            {

                ShowUsage();
                return;

            }

            HealthService service = new HealthService(ApplicationName);

            if (authToken != null)
            {
                Console.WriteLine("Using AuthSubToken: " + authToken);
                GAuthSubRequestFactory factory = new GAuthSubRequestFactory(HealthService.ServiceName, ApplicationName);
                factory.Token = authToken;
                service.RequestFactory = factory;
            }
            else if (exchangeToken != null)
            {
                Console.WriteLine("Using Onetime token: " + exchangeToken);
                authToken = AuthSubUtil.exchangeForSessionToken(exchangeToken, null);
                Console.WriteLine("Exchanged for Session Token: " + exchangeToken);
                GAuthSubRequestFactory factory = new GAuthSubRequestFactory(HealthService.ServiceName, ApplicationName);
                factory.Token = authToken;
                service.RequestFactory = factory;
            }
            else 
            {
                Console.WriteLine("Setting user credentials for: " + userName);
                service.setUserCredentials(userName, passWord);
            }

            HealthQuery query;
    
            try
            {
                if (isList == true)
                {
                    HealthFeed feed = service.Query(new HealthQuery(HealthQuery.ProfileListFeed));
                    Console.WriteLine ("Feed =" + feed);
                    foreach (AtomEntry entry in feed.Entries)
                    {
                        Console.WriteLine("\tProfile " + entry.Title.Text + " has ID: " + entry.Content.Content);
                    }
                    return;
                }

                if (pid == null && pname != null)
                {
                    // need to find the ID first. 
                    // so we get the list feed, find the name in the TITLE, and then 
                    // get the ID out of the content field
                    HealthFeed feed = service.Query(new HealthQuery(HealthQuery.ProfileListFeed));
                    foreach (AtomEntry entry in feed.Entries)
                    {
                        if (entry.Title.Text == pname)
                        {
                            pid = entry.Content.Content;
                        }
                    }
               }

                if (authToken != null)
                {
                    if (isRegister == true)
                    {
                        query = new HealthQuery(HealthQuery.AuthSubRegisterFeed);
                    } 
                    else 
                    {
                        query = new HealthQuery(HealthQuery.AuthSubProfileFeed);
                        if (doSummary == true)
                        {
                            query.Digest = true;
                        } 
                    }
                } 
                else 
                {

                    if (isRegister == true)
                    {
                        query = HealthQuery.RegisterQueryForId(pid);
                    } 
                    else 
                    {
                        query = HealthQuery.ProfileQueryForId(pid);
                        if (doSummary == true)
                        {
                            query.Grouped = true;
                            query.GroupSize = 1; 
                        }
                    }
                }

                Console.WriteLine("Resolved targetUri to: " + query.Uri.ToString()); 

                if (doCCR == true)
                {
                    HealthFeed feed = service.Query(query);
                    foreach (HealthEntry e in feed.Entries )
                    {
                        XmlNode ccr = e.CCR;
                        if (ccr != null)
                        {
                            XmlTextWriter writer = new XmlTextWriter(Console.Out);
                            writer.Formatting = Formatting.Indented;
                            ccr.WriteTo(writer);
                        }
                    }
                } 
                else
                {
                    if (isShow == true || doSummary == true)
                    {
                        Stream result = service.Query(query.Uri);
                        DumpStream(result);
                    }
                }
                if (isInsert == true)
                {
                    String input = Console.In.ReadToEnd();
                    Console.Write(input);
                    Stream result = service.StringSend(query.Uri, input, GDataRequestType.Insert);
                    DumpStream(result);
                }
                if (isClean == true)
                {
                    int count = 0; 
                    AtomFeed feed = service.Query(query);
                    Console.WriteLine("Retrieved Feed, now deleting all entries in the feed");
                    foreach (AtomEntry entry in feed.Entries)
                    {
                        Console.WriteLine(".... deleting entry " + entry.Title.Text); 
                        entry.Delete();
                        count++;
                    }
                    Console.WriteLine("Deleted " + count + " entries"); 

                }
            } catch (GDataRequestException e)
            {
                HttpWebResponse response = e.Response as HttpWebResponse;
                Console.WriteLine("Error executing request for Verb, Errorcode: " + response.StatusCode);
                Console.WriteLine(response.StatusDescription);
                Console.WriteLine(e.ResponseString);
            }
        }

        static void ShowUsage()
        {
            Console.WriteLine("Usage is HealthTool [options] -user:<username> -pwd:<password>");
            Console.WriteLine("or");
            Console.WriteLine("HealthTool [options] -a:<authsubtoken>");
            Console.WriteLine("or");
            Console.WriteLine("HealthTool [options] -e:<authsubtoken> - to exchance a one time token for a session token");
            Console.WriteLine("options can be the following:");
            Console.WriteLine("\t-pid:profileID is the ID of the profile, like -pid:Xta1342123");
            Console.WriteLine("\t-pname:profileName is the name of the profile, like -pname:MyProfile. The tool will look up the ID then");
            Console.WriteLine("\t-register : the operation will work against the register, not the profile");
            Console.WriteLine("\t-summary : execute the summary query for the profile, if authsub, adds the &digest=true");
            Console.WriteLine("\t-insert : the tool will take the passed in file and post it against the profile/register.");
            Console.WriteLine(" \t\te.g: Healthtool -insert -user:MyUser -pwd:MyPwd < mytest.xml"); 
            Console.WriteLine("\t-clean : this option will delete all data from the specified profile");
            Console.WriteLine("\t-show : this option will list the specified feed to the console");
            Console.WriteLine("\t-ccr : this option will just dump the CCR records, and ignore the containing xml");
            Console.WriteLine("\t-list : this option will show the list of profiles available for the given authentication");
            
        }


    	static void DumpStream(Stream s) 
        {
            const int size = 4096;
            byte[] bytes = new byte[4096];
            int numBytes;
    
            while((numBytes = s.Read(bytes, 0, size)) > 0)
            {
                String responseData = System.Text.Encoding.ASCII.GetString(bytes, 0, numBytes);
                Console.Write(responseData);
            }
        }

        private static bool isOption(string[] args, string key, bool defValue)
        {
            key = "-" + key; 
            for (int i=0; i< args.Length; i++)
            {
                string test = args[i];
                if (test != null)
                {
                    if (key == test)
                    {
                        return true;
                    }
                }
            }
            return defValue;
        }

        private static string findValue(string[] args, string key, string defValue)
        {
            key = "-" + key; 
            for (int i=0; i< args.Length; i++)
            {
                string test = args[i];
                if (test != null)
                {
                    string []res = test.Split(new char[] {':'});
                    if (res != null && res.Length == 2)
                    {
                        if (key== res[0])
                        {
                            return res[1];
                        }
                    }
                }
            }
            return defValue;
        }
    }
}
