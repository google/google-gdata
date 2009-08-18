using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Google.GData.Client.LiveTests;
using Google.GData.Client;
using Google.GData.Client.UnitTests;

namespace Google.GData.Health.UnitTests
{
	[TestFixture]
	[Category("LiveTest")]
	public class H9ServiceTestSuite : BaseLiveTestClass
	{
		[Test]
		public void GoogleAuthenticationTest()
		{
			Tracing.TraceMsg("Entering Health AuthenticationTest");

			HealthQuery query = new HealthQuery();
			H9Service service = new H9Service(this.ApplicationName);
			if (this.userName != null)
			{
				service.Credentials = new GDataCredentials(this.userName, this.passWord);
			}
			service.RequestFactory = this.factory;

			HealthFeed feed = service.Query(query) as HealthFeed;

			ObjectModelHelper.DumpAtomObject(feed, CreateDumpFileName("AuthenticationTest"));
			service.Credentials = null; 
		}
	}
}
