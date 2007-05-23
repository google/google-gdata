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
        public BaseLiveTestClass()
        {
            ServicePointManager.CertificatePolicy = this;
        }

        public bool     CheckValidationResult(ServicePoint srvPoint, X509Certificate certificate, WebRequest request, int certificateProblem) 
        {
            return (true);
        }
	}
}
