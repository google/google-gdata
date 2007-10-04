using System;
using System.Net;
using System.Security.Cryptography.X509Certificates;

using Google.GData.Client;
using Google.GData.Client.UnitTests;

using NUnit.Framework;


namespace Google.GData.Client.LiveTests
{
	/// <summary>
	/// This is the base class that should be used by every
	/// tests against live servers. It handles the case
	///  where the SSL certificate isn't valid anymore.
	/// </summary>
	public class BaseLiveTestClass : BaseTestClass, ICertificatePolicy
	{
        /// <summary>holds the username to use</summary>
        protected string userName;
        /// <summary>holds the password to use</summary>
        protected string passWord;
        /// <summary>holds the default authhandler</summary> 
        protected string strAuthHandler;
        /// <summary>decides if we should empty out the feed when done.</summary>
        protected bool wipeFeeds = true;

        public BaseLiveTestClass()
        {
            ServicePointManager.CertificatePolicy = this;
        }

        public bool     CheckValidationResult(ServicePoint srvPoint, X509Certificate certificate, WebRequest request, int certificateProblem) 
        {
            return (true);
        }

        protected override void ReadConfigFile()
        {
            base.ReadConfigFile();

            if (unitTestConfiguration.Contains("authHandler") == true)
            {
                this.strAuthHandler = (string) unitTestConfiguration["authHandler"];
                Tracing.TraceInfo("Read authHandler value: " + this.strAuthHandler);
            }
            if (unitTestConfiguration.Contains("userName") == true)
            {
                this.userName = (string) unitTestConfiguration["userName"];
                Tracing.TraceInfo("Read userName value: " + this.userName);
            }
            if (unitTestConfiguration.Contains("passWord") == true)
            {
                this.passWord = (string) unitTestConfiguration["passWord"];
                Tracing.TraceInfo("Read passWord value: " + this.passWord);
            }
            if (unitTestConfiguration.Contains("wipeFeeds") == true)
            {
                this.wipeFeeds = bool.Parse((string)unitTestConfiguration["wipeFeeds"]);
                Tracing.TraceInfo("Wiping feeds option: " + this.wipeFeeds.ToString());
            }
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>empty a feed</summary> 
        //////////////////////////////////////////////////////////////////////
        protected void FeedCleanup(String uriToClean, String userName, String pwd)
        {
            Tracing.TraceCall();
            Tracing.TraceCall("Cleaning URI: " + uriToClean);

            if (!this.wipeFeeds)
            {
                Tracing.TraceInfo("Skipped cleaning URI due to configuration.");
                return;
            }

            FeedQuery query = new FeedQuery();
            Service service = new Service();

            if (uriToClean != null)
            {
                if (userName != null)
                {
                    service.Credentials = new GDataCredentials(userName, pwd);
                }

                GDataLoggingRequestFactory factory = (GDataLoggingRequestFactory)this.factory;
                factory.MethodOverride = true;
                service.RequestFactory = this.factory; 

                query.Uri = new Uri(uriToClean);

                Tracing.TraceCall("Querying " + uriToClean);
                AtomFeed feed = service.Query(query);
                Tracing.TraceCall("Queryed " + uriToClean);
                if (feed != null)
                    Tracing.TraceCall("Entries: " + feed.Entries.Count.ToString());

                int iCount = 0; 

                if (feed.Entries.Count > 0)
                {
                    while (feed.Entries.Count > 0)
                    {
                        Tracing.TraceCall("Feed has still " + feed.Entries.Count.ToString() + " entries left.");
                        foreach (AtomEntry entry in feed.Entries)
                        {
                            Tracing.TraceCall("Deleting entry " + iCount);
                            entry.Delete();
                            iCount++; 
                            Tracing.TraceCall("Deleted entry " + iCount);
                        }
                        feed = service.Query(query);
                    }

                    Assert.AreEqual(0, feed.Entries.Count, "Feed should have no more entries, it has: " + feed.Entries.Count); 
                    service.Credentials = null; 
                    factory.MethodOverride = false;
                }
            }
        }
        /////////////////////////////////////////////////////////////////////////////
	}
}
