using System;
using System.IO;
using System.Xml; 
using System.Collections;
using System.Configuration;
using System.Net; 
using NUnit.Framework;
using Google.GData.Client;
using Google.GData.Client.UnitTests;
using Google.GData.Extensions;
using Google.GData.Calendar;
using Google.GData.AccessControl;

namespace Google.GData.Client.LiveTests
{
	/// <summary>
	/// Summary description for gziplivetest.
	/// </summary>
    [TestFixture] 
    [Category("LiveTest")]
    public class GZipTestSuite : BaseLiveTestClass
	{
        private CalendarService calendarService;
        private string  calendarUri = "";

		public GZipTestSuite()
		{
		}

        public override void InitTest()
        {
            base.InitTest();
            this.calendarService = new CalendarService(null);
            calendarService.RequestFactory.UseGZip = true;
        }

        protected override void ReadConfigFile()
        {
            base.ReadConfigFile();

            if (unitTestConfiguration.Contains("calendarURI"))
                this.calendarUri = (string)unitTestConfiguration["calendarURI"];
        }

        [Test]
        public void TestGZipQuery()
        {
            IGDataRequest request = this.calendarService.RequestFactory.CreateRequest(GDataRequestType.Query, new Uri(calendarUri));
            Assert.IsTrue(request.UseGZip, "IGDataRequest.UseGZip property should be true.");

            request.Credentials = new GDataCredentials(this.userName, this.passWord);
            request.Execute();
            Assert.IsTrue(request.UseGZip, "IGDataRequest.UseGZip is not true, the response was NOT in GZip format.");

            Stream responseStream = request.GetResponseStream();
            Assert.IsTrue(responseStream != null, "Response stream should not be null.");

            AtomFeed feed = new AtomFeed(new Uri(calendarUri), this.calendarService);
            feed.Parse(request.GetResponseStream(), AlternativeFormat.Atom);
            Assert.IsTrue(feed.Self != null, "AtomFeed object is not right.");
        }
	}
}
