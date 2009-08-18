using System;
using System.Collections.Generic;
using System.Text;
using Google.GData.Client;
using ASTM.Org.CCR;
using Google.GData.Extensions;
using System.Xml.Serialization;
using System.Xml;
using System.IO;

namespace Google.GData.Health
{
	//////////////////////////////////////////////////////////////////////
	/// <summary>
	/// The Google Health Data API allows applications to view and send health data in the form of Google Data feeds. 
	/// The HealthService class encapsulates authentication to the Google HealthService.
	/// </summary>
	//////////////////////////////////////////////////////////////////////
	public abstract class BaseHealthService : Service
	{
		/// <summary>The Calendar service's name</summary> 
		public const string ServiceName = "health";
		/// <summary>The base Google Health Feeds address.</summary>
		private string BaseAddress;
		/// <summary>The Google Health Profile list feed.</summary>
		private string ProfileListFeed;
		/// <summary>The Google Health Profile UI feed</summary>
		private string ProfileFeed;
		/// <summary>The Google Health Register UI feed</summary>
		private string RegisterFeed;
		/// <summary>The Google Health Default Profile UI feed</summary>
		private string DefaultProfileFeed;
		/// <summary>The Google Health Default Register UI feed</summary>
		private string DefaultRegisterFeed;

		/// <summary>
		/// default constructor
		/// </summary>
		/// <param name="applicationName"></param>
		/// <returns></returns>
		public BaseHealthService(string applicationName, string baseAddress, 
			string profileListFeed, string profileFeed, string registerFeed, 
			string defaultProfileFeed, string defaultRegisterFeed)
			: base(ServiceName, applicationName)
		{
			this.NewFeed += new ServiceEventHandler(this.OnNewFeed);
			this.BaseAddress = baseAddress;
			this.ProfileListFeed = profileListFeed;
			this.ProfileFeed = profileFeed;
			this.RegisterFeed = registerFeed;
			this.DefaultProfileFeed = defaultProfileFeed;
			this.DefaultRegisterFeed = defaultRegisterFeed;
		}

		/// <summary>
		/// overloaded to create typed version of Query
		/// </summary>
		/// <param name="feedQuery"></param>
		/// <returns>EventFeed</returns>
		public HealthFeed Query(HealthQuery feedQuery)
		{
			return base.Query(feedQuery) as HealthFeed;
		}

		/// <summary>
		/// Queries the Google Health Profile list feed. This is related to the 
		/// ClientLogin queries using ClientLogin.
		/// </summary>
		/// <returns>The list of profiles for the authenticated client.</returns>
		public List<Profile> GetProfiles()
		{
			HealthFeed feed = this.Query(new HealthQuery(ProfileListFeed));
			List<Profile> profiles = new List<Profile>();
			foreach (AtomEntry entry in feed.Entries)
			{
				profiles.Add(Profile.FromAtomEntry(entry));
			}
			return profiles;
		}

		/// <summary>
		/// Queries the Google Health Profile Register feed for the list of notices. This 
		/// is for ClientLogin related queries.
		/// </summary>
		/// <param name="profileId">The profile to query against.</param>
		/// <returns>The list of notices available on this profile.</returns>
		public List<Notice> GetNotices(string profileId)
		{
			HealthQuery query = new HealthQuery(RegisterFeed + profileId);
			query.Digest = true;
			HealthFeed feed = this.Query(query);
			List<Notice> notices = new List<Notice>();
			foreach (AtomEntry entry in feed.Entries)
			{
				notices.Add(Notice.FromAtomEntry(entry));
			}
			return notices;
		}

		/// <summary>
		/// Queries the profile feed for the list of available CCR records with digest enabled. Available 
		/// for ClientLogin related queries.
		/// </summary>
		/// <param name="profileId">The profile to query against.</param>
		/// <returns>The list of CCR records available.</returns>
		public ContinuityOfCareRecord GetCareRecordsDigest(string profileId)
		{
			List<ContinuityOfCareRecord> records = GetCareRecords(profileId, true);
			if (records.Count > 0)
				return records[0];
			return null;
		}

		/// <summary>
		/// Queries the profile feed for the list of available CCR records. Available 
		/// for ClientLogin related queries.
		/// </summary>
		/// <param name="profileId">The profile to query against.</param>
		/// <param name="digest">True to aggregate all CCR into a single CCR element, false otherwise.</param>
		/// <returns>The list of CCR records available.</returns>
		public List<ContinuityOfCareRecord> GetCareRecords(string profileId, bool digest)
		{
			HealthQuery query = new HealthQuery(ProfileFeed + profileId);
			query.Digest = true;
			HealthFeed feed = this.Query(query);
			List<ContinuityOfCareRecord> ccrs = new List<ContinuityOfCareRecord>();
			foreach (AtomEntry entry in feed.Entries)
			{
				IExtensionElementFactory factory = entry.FindExtension("ContinuityOfCareRecord", "urn:astm-org:CCR");
				if (factory != null)
				{
					XmlExtension extension = factory as XmlExtension;
					XmlSerializer serializer = new XmlSerializer(typeof(ContinuityOfCareRecord));
					XmlTextReader reader = new XmlTextReader(new StringReader(extension.Node.OuterXml));
					ccrs.Add(serializer.Deserialize(reader) as ContinuityOfCareRecord);
				}
			}
			return ccrs;
		}

		/// <summary>
		/// Posts a notice to the particular profile on the register feed. Available 
		/// under ClientLogin only.
		/// </summary>
		/// <param name="notice"></param>
		/// <param name="profileId"></param>
		public void PostNotice(Notice notice, string profileId)
		{
			PostNoticeUrl(notice, RegisterFeed + profileId);
		}

		/// <summary>
		/// Posts a notice to the default profile on the register feed. Usable 
		/// for the AuthSub profile feed.
		/// </summary>
		/// <param name="notice"></param>
		public void PostNotice(Notice notice)
		{
			PostNoticeUrl(notice, DefaultRegisterFeed);
		}

		private void PostNoticeUrl(Notice notice, string url)
		{
			AtomEntry entry = new AtomEntry();
			entry.Title.Text = notice.Subject;
			entry.Content.Content = notice.Subject;
			entry.Content.Type = notice.ContentType;

			if (notice.CareRecord != null)
			{
				XmlSerializer serializer = new XmlSerializer(typeof(ContinuityOfCareRecord));
				using (StringWriter strWriter = new StringWriter())
				{
					XmlTextWriter writer = new XmlTextWriter(new StringWriter());
					serializer.Serialize(writer, notice.CareRecord);

					XmlDocument doc = new XmlDocument();
					doc.LoadXml(strWriter.ToString());
					entry.ExtensionElements.Add(new XmlExtension(doc.DocumentElement));
				}
			}

			notice = Notice.FromAtomEntry(this.Insert(new Uri(url), entry));
		}

		//////////////////////////////////////////////////////////////////////
		/// <summary>eventchaining. We catch this by from the base service, which 
		/// would not by default create an atomFeed</summary> 
		/// <param name="sender"> the object which send the event</param>
		/// <param name="e">FeedParserEventArguments, holds the feedentry</param> 
		/// <returns> </returns>
		//////////////////////////////////////////////////////////////////////
		protected void OnNewFeed(object sender, ServiceEventArgs e)
		{
			Tracing.TraceMsg("Created new Google Health Feed");
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}

			e.Feed = new HealthFeed(e.Uri, e.Service);
		}
		/////////////////////////////////////////////////////////////////////////////
	}
}
