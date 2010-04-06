using System;
using System.Collections.Generic;
using System.Text;

namespace Google.GData.Health
{
	//////////////////////////////////////////////////////////////////////
	/// <summary>
	/// The Google Health Data API allows applications to view and send health data in the form of Google Data feeds. 
	/// The HealthService class encapsulates authentication to the Google HealthService.
	/// </summary>
	//////////////////////////////////////////////////////////////////////
	public class H9Service : BaseHealthService
	{
		/// <summary>The Google Health service's name</summary> 
		public const string ServiceName = "weaver";
		/// <summary>The Google Health service's environment.</summary>
		public const string ServiceEnvironment = "h9";
		/// <summary>The base Google Health Feeds address.</summary>
		public const string BaseAddress = "https://www.google.com/h9/feeds/";
		/// <summary>The Google Health Profile list feed.</summary>
		public const string ProfileListFeed = BaseAddress + "profile/list/";
		/// <summary>The Google Health Profile UI feed</summary>
		public const string ProfileFeed = BaseAddress + "profile/ui/";
		/// <summary>The Google Health Register UI feed</summary>
		public const string RegisterFeed = BaseAddress + "register/ui/";
		/// <summary>The Google Health Default Profile UI feed</summary>
		public const string DefaultProfileFeed = BaseAddress + "profile/default";
		/// <summary>The Google Health Default Register UI feed</summary>
		public const string DefaultRegisterFeed = BaseAddress + "register/default";

		/// <summary>
		/// Constructs the Google Health Service using the 
		/// primary H9 Feeds address.
		/// </summary>
		/// <param name="applicationName">The name of the application leveraging the service.</param>
		public H9Service(string applicationName)
			: base(applicationName,
				BaseAddress, ProfileListFeed, ProfileFeed, RegisterFeed,
				DefaultProfileFeed, DefaultRegisterFeed) { }
	}
}
