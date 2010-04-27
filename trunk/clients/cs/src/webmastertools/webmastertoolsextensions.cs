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

/* 
 * Created by Morten Christensen, http://blog.sitereactor.dk | http://twitter.com/sitereactor
 */

using System;
using Google.GData.Client;
using Google.GData.Extensions;

namespace Google.GData.WebmasterTools
{
    #region Ananlytics specific constants

    public class WebmasterToolsNameTable
    {
        /// <summary>
        /// base uri for user based feeds
        /// </summary>
        public const string BaseUserUri = "https://www.google.com/webmasters/tools/feeds/";

        // <summary>GData webmaster tools extension namespace</summary>
        public const string gWebmasterToolsNamspace = "http://schemas.google.com/webmasters/tools/2007";
        /// <summary>prefix for gWebmasterToolsNamspace if writing</summary>
        public const string gWebmasterToolsPrefix = "wt";
        public const string mWebmasterToolsPrefix = "mobile";
      
        /// Sites feed
        /// <summary>xmlelement for wt:crawl-rate</summary> 
        public const string XmlCrawlRateElement = "crawl-rate";
        /// <summary>xmlelement for wt:geolocation</summary>
        public const string XmlGeoLocationElement = "geolocation";
        /// <summary>xmlelement for wt:preferred-domain</summary>
        public const string XmlPreferredDomainElement = "preferred-domain";
        /// <summary>xmlelement for wt:verification-method</summary>
        public const string XmlVerificationMethodElement = "verification-method";
        /// <summary>xml attribute type for wt:verification-method</summary> 
        public const string XmlAttributeType = "type";
        /// <summary>xml attribute in-use for wt:verification-method</summary> 
        public const string XmlAttributeInUse = "in-use";
        /// <summary>xmlelement for wt:verified</summary>
        public const string XmlVerifiedElement = "verified";
        /// <summary>xmlelement for wt:date</summary>
        public const string XmlDateElement = "date";

        /// Keywords feed
        /// <summary>xmlelement for wt:keyword</summary> 
        public const string XmlKeywordElement = "keyword";
        /// <summary>xml attribute source for wt:keyword</summary> 
        public const string XmlAttributeSource = "source";

        /// Sitemaps feed
        /// <summary>xmlelement for mobile:mobile</summary>
        public const string XmlMobileElement = "mobile";
        /// <summary>xmlelement for wt:sitemap-last-downloaded</summary>
        public const string XmlSitemapLastDownloadedElement = "sitemap-last-downloaded";
        /// <summary>xmlelement for wt:sitemap-mobile</summary>
        public const string XmlSitemapMobileElement = "sitemap-mobile";
        /// <summary>xmlelement for wt:sitemap-mobile-markup-language</summary>
        public const string XmlSitemapMobileMarkupLanguageElement = "sitemap-mobile-markup-language";
        /// <summary>xmlelement for wt:sitemap-news</summary>
        public const string XmlSitemapNewsElement = "sitemap-news";
        /// <summary>xmlelement for wt:sitemap-news-publication-label</summary>
        public const string XmlSitemapNewsPublicationLabelElement = "sitemap-news-publication-label";
        /// <summary>xmlelement for wt:sitemap-type</summary>
        public const string XmlSitemapTypeElement = "sitemap-type";
        /// <summary>xmlelement for wt:sitemap-url-count</summary>
        public const string XmlSitemapUrlCountElement = "sitemap-url-count";

        /// Messages feed
        /// <summary>xmlelement for wt:body</summary>
        public const string XmlBodyElement = "body";
        /// wt:date already exists
        /// <summary>xmlelement for wt:language</summary>
        public const string XmlLanguageElement = "language";
        /// <summary>xml attribute language for wt:language</summary> 
        public const string XmlAttributeLanguage = "language";
        /// <summary>xmlelement for wt:read</summary>
        public const string XmlReadElement = "read";
        /// <summary>xmlelement for wt:subject</summary>
        public const string XmlSubjectElement = "subject";
        /// <summary>xml attribute subject for wt:subject</summary>
        public const string XmlAttributeSubject = "subject";

        /// Crawl Issues feed
        /// <summary>xmlelement for wt:crawl-type</summary>
        public const string XmlCrawlTypeElement = "crawl-type";
        /// <summary>xmlelement for wt:issue-type</summary>
        public const string XmlIssueTypeElement = "issue-type";
        /// <summary>xmlelement for wt:issue-detail</summary>
        public const string XmlIssueDetailElement = "issue-detail";
        /// <summary>xmlelement for wt:linked-from</summary>
        public const string XmlLinkedFromElement = "linked-from";
        /// <summary>xmlelement for wt:date-detected</summary>
        public const string XmlDateDetectedElement = "date-detected";
    }
    #endregion

    /// <summary>
    /// wt:crawl-rate schema extension describing a Webmaster Tools Crawl Rate
    /// </summary>
    public class CrawlRate : SimpleElement
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public CrawlRate()
            : base(WebmasterToolsNameTable.XmlCrawlRateElement, WebmasterToolsNameTable.gWebmasterToolsPrefix, WebmasterToolsNameTable.gWebmasterToolsNamspace)
        { }
        /// <summary>
        /// constructor taking the initial value
        /// </summary>
        /// <param name="initValue"></param>
        public CrawlRate(string initValue)
            : base(WebmasterToolsNameTable.XmlCrawlRateElement, WebmasterToolsNameTable.gWebmasterToolsPrefix, WebmasterToolsNameTable.gWebmasterToolsNamspace, initValue)
        { }
    }

    /// <summary>
    /// wt:geolocation schema extension describing a Webmaster Tools Geolocation
    /// </summary>
    public class GeoLocation : SimpleElement
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public GeoLocation()
            : base(WebmasterToolsNameTable.XmlGeoLocationElement, WebmasterToolsNameTable.gWebmasterToolsPrefix, WebmasterToolsNameTable.gWebmasterToolsNamspace)
        { }
        /// <summary>
        /// constructor taking the initial value
        /// </summary>
        /// <param name="initValue"></param>
        public GeoLocation(string initValue)
            : base(WebmasterToolsNameTable.XmlGeoLocationElement, WebmasterToolsNameTable.gWebmasterToolsPrefix, WebmasterToolsNameTable.gWebmasterToolsNamspace, initValue)
        { }
    }

    /// <summary>
    /// wt:perferred-domain schema extension describing a Webmaster Tools Perferred Domain
    /// </summary>
    public class PreferredDomain : SimpleElement
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public PreferredDomain()
            : base(WebmasterToolsNameTable.XmlPreferredDomainElement, WebmasterToolsNameTable.gWebmasterToolsPrefix, WebmasterToolsNameTable.gWebmasterToolsNamspace)
        { }
        /// <summary>
        /// constructor taking the initial value
        /// </summary>
        /// <param name="initValue"></param>
        public PreferredDomain(string initValue)
            : base(WebmasterToolsNameTable.XmlPreferredDomainElement, WebmasterToolsNameTable.gWebmasterToolsPrefix, WebmasterToolsNameTable.gWebmasterToolsNamspace, initValue)
        { }
    }

    /// <summary>
    /// wt:verification-method schema extension describing a Webmaster Tools Verification Method
    /// </summary>
    public class VerificationMethod : SimpleNameValueAttribute
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public VerificationMethod()
            : base(WebmasterToolsNameTable.XmlVerificationMethodElement, WebmasterToolsNameTable.gWebmasterToolsPrefix, WebmasterToolsNameTable.gWebmasterToolsNamspace)
        {
            this.Attributes.Add(WebmasterToolsNameTable.XmlAttributeType, null);
            this.Attributes.Add(WebmasterToolsNameTable.XmlAttributeInUse, null);
        }
        /// <summary>
        /// constructor taking the initial value
        /// </summary>
        /// <param name="type"></param>
        /// <param name="inUse"></param>
        public VerificationMethod(String type, String inUse)
            : base(WebmasterToolsNameTable.XmlVerificationMethodElement, WebmasterToolsNameTable.gWebmasterToolsPrefix, WebmasterToolsNameTable.gWebmasterToolsNamspace)
        {
            this.Attributes.Add(WebmasterToolsNameTable.XmlAttributeType, type);
            this.Attributes.Add(WebmasterToolsNameTable.XmlAttributeInUse, inUse);
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>convienience accessor for the Type attribute</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public string Type
        {
            get
            {
                return this.Attributes[WebmasterToolsNameTable.XmlAttributeType] as string;
            }
            set
            {
                this.Attributes[WebmasterToolsNameTable.XmlAttributeType] = value;
            }
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>convienience accessor for the In Use attribute</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public string InUse
        {
            get
            {
                return this.Attributes[WebmasterToolsNameTable.XmlAttributeInUse] as string;
            }
            set
            {
                this.Attributes[WebmasterToolsNameTable.XmlAttributeInUse] = value;
            }
        }
    }

    /// <summary>
    /// wt:verified schema extension describing a Webmaster Tools Verified
    /// </summary>
    public class Verified : SimpleElement
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public Verified()
            : base(WebmasterToolsNameTable.XmlVerifiedElement, WebmasterToolsNameTable.gWebmasterToolsPrefix, WebmasterToolsNameTable.gWebmasterToolsNamspace)
        { }
        /// <summary>
        /// constructor taking the initial value
        /// </summary>
        /// <param name="initValue"></param>
        public Verified(string initValue)
            : base(WebmasterToolsNameTable.XmlVerifiedElement, WebmasterToolsNameTable.gWebmasterToolsPrefix, WebmasterToolsNameTable.gWebmasterToolsNamspace, initValue)
        { }
    }

    /// <summary>
    /// wt:date schema extension describing a Webmaster Tools Date
    /// </summary>
    public class Date : SimpleElement
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public Date()
            : base(WebmasterToolsNameTable.XmlDateElement, WebmasterToolsNameTable.gWebmasterToolsPrefix, WebmasterToolsNameTable.gWebmasterToolsNamspace)
        { }
        /// <summary>
        /// constructor taking the initial value
        /// </summary>
        /// <param name="initValue"></param>
        public Date(string initValue)
            : base(WebmasterToolsNameTable.XmlDateElement, WebmasterToolsNameTable.gWebmasterToolsPrefix, WebmasterToolsNameTable.gWebmasterToolsNamspace, initValue)
        { }
    }

    public class Keyword : SimpleNameValueAttribute
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public Keyword()
            : base(WebmasterToolsNameTable.XmlKeywordElement, WebmasterToolsNameTable.gWebmasterToolsPrefix, WebmasterToolsNameTable.gWebmasterToolsNamspace)
        {
            this.Attributes.Add(WebmasterToolsNameTable.XmlAttributeSource, null);
        }
        /// <summary>
        /// constructor taking the initial value
        /// </summary>
        /// <param name="value"></param>
        public Keyword(String value)
            : base(WebmasterToolsNameTable.XmlKeywordElement, WebmasterToolsNameTable.gWebmasterToolsPrefix, WebmasterToolsNameTable.gWebmasterToolsNamspace)
        {
            this.Attributes.Add(WebmasterToolsNameTable.XmlAttributeSource, value);
        }
    }

    public class Mobile : SimpleElement
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public Mobile()
            : base(WebmasterToolsNameTable.XmlMobileElement, WebmasterToolsNameTable.mWebmasterToolsPrefix, WebmasterToolsNameTable.gWebmasterToolsNamspace)
        { }
        /// <summary>
        /// constructor taking the initial value
        /// </summary>
        /// <param name="initValue"></param>
        public Mobile(string initValue)
            : base(WebmasterToolsNameTable.XmlMobileElement, WebmasterToolsNameTable.mWebmasterToolsPrefix, WebmasterToolsNameTable.gWebmasterToolsNamspace, initValue)
        { }
    }

    public class SitemapLastDownloaded : SimpleElement
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public SitemapLastDownloaded()
            : base(WebmasterToolsNameTable.XmlSitemapLastDownloadedElement, WebmasterToolsNameTable.gWebmasterToolsPrefix, WebmasterToolsNameTable.gWebmasterToolsNamspace)
        {}
        /// <summary>
        /// constructor taking the initial value
        /// </summary>
        /// <param name="initValue"></param>
        public SitemapLastDownloaded(string initValue)
            : base(WebmasterToolsNameTable.XmlSitemapLastDownloadedElement, WebmasterToolsNameTable.gWebmasterToolsPrefix, WebmasterToolsNameTable.gWebmasterToolsNamspace, initValue)
        { }
    }

    public class SitemapMobile : SimpleElement
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public SitemapMobile()
            : base(WebmasterToolsNameTable.XmlSitemapMobileElement, WebmasterToolsNameTable.gWebmasterToolsPrefix, WebmasterToolsNameTable.gWebmasterToolsNamspace)
        { }
        /// <summary>
        /// constructor taking the initial value
        /// </summary>
        /// <param name="initValue"></param>
        public SitemapMobile(string initValue)
            : base(WebmasterToolsNameTable.XmlSitemapMobileElement, WebmasterToolsNameTable.gWebmasterToolsPrefix, WebmasterToolsNameTable.gWebmasterToolsNamspace, initValue)
        { }
    }

    public class SitemapMobileMarkupLanguage : SimpleElement
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public SitemapMobileMarkupLanguage()
            : base(WebmasterToolsNameTable.XmlSitemapMobileMarkupLanguageElement, WebmasterToolsNameTable.gWebmasterToolsPrefix, WebmasterToolsNameTable.gWebmasterToolsNamspace)
        { }
        /// <summary>
        /// constructor taking the initial value
        /// </summary>
        /// <param name="initValue"></param>
        public SitemapMobileMarkupLanguage(string initValue)
            : base(WebmasterToolsNameTable.XmlSitemapMobileMarkupLanguageElement, WebmasterToolsNameTable.gWebmasterToolsPrefix, WebmasterToolsNameTable.gWebmasterToolsNamspace, initValue)
        { }
    }

    public class SitemapNews : SimpleElement
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public SitemapNews()
            : base(WebmasterToolsNameTable.XmlSitemapNewsElement, WebmasterToolsNameTable.gWebmasterToolsPrefix, WebmasterToolsNameTable.gWebmasterToolsNamspace)
        { }
        /// <summary>
        /// constructor taking the initial value
        /// </summary>
        /// <param name="initValue"></param>
        public SitemapNews(string initValue)
            : base(WebmasterToolsNameTable.XmlSitemapNewsElement, WebmasterToolsNameTable.gWebmasterToolsPrefix, WebmasterToolsNameTable.gWebmasterToolsNamspace, initValue)
        { }
    }

    public class SitemapNewsPublicationLabel : SimpleElement
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public SitemapNewsPublicationLabel()
            : base(WebmasterToolsNameTable.XmlSitemapNewsPublicationLabelElement, WebmasterToolsNameTable.gWebmasterToolsPrefix, WebmasterToolsNameTable.gWebmasterToolsNamspace)
        { }
        /// <summary>
        /// constructor taking the initial value
        /// </summary>
        /// <param name="initValue"></param>
        public SitemapNewsPublicationLabel(string initValue)
            : base(WebmasterToolsNameTable.XmlSitemapNewsPublicationLabelElement, WebmasterToolsNameTable.gWebmasterToolsPrefix, WebmasterToolsNameTable.gWebmasterToolsNamspace, initValue)
        { }
    }

    public class SitemapType : SimpleElement
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public SitemapType()
            : base(WebmasterToolsNameTable.XmlSitemapTypeElement, WebmasterToolsNameTable.gWebmasterToolsPrefix, WebmasterToolsNameTable.gWebmasterToolsNamspace)
        { }
        /// <summary>
        /// constructor taking the initial value
        /// </summary>
        /// <param name="initValue"></param>
        public SitemapType(string initValue)
            : base(WebmasterToolsNameTable.XmlSitemapTypeElement, WebmasterToolsNameTable.gWebmasterToolsPrefix, WebmasterToolsNameTable.gWebmasterToolsNamspace, initValue)
        { }
    }

    public class SitemapUrlCount : SimpleElement
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public SitemapUrlCount()
            : base(WebmasterToolsNameTable.XmlSitemapUrlCountElement, WebmasterToolsNameTable.gWebmasterToolsPrefix, WebmasterToolsNameTable.gWebmasterToolsNamspace)
        { }
        /// <summary>
        /// constructor taking the initial value
        /// </summary>
        /// <param name="initValue"></param>
        public SitemapUrlCount(string initValue)
            : base(WebmasterToolsNameTable.XmlSitemapUrlCountElement, WebmasterToolsNameTable.gWebmasterToolsPrefix, WebmasterToolsNameTable.gWebmasterToolsNamspace, initValue)
        { }
    }

    public class Body : SimpleElement
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public Body()
            : base(WebmasterToolsNameTable.XmlBodyElement, WebmasterToolsNameTable.gWebmasterToolsPrefix, WebmasterToolsNameTable.gWebmasterToolsNamspace)
        { }
        /// <summary>
        /// constructor taking the initial value
        /// </summary>
        /// <param name="initValue"></param>
        public Body(string initValue)
            : base(WebmasterToolsNameTable.XmlBodyElement, WebmasterToolsNameTable.gWebmasterToolsPrefix, WebmasterToolsNameTable.gWebmasterToolsNamspace, initValue)
        { }
    }

    public class Language : SimpleNameValueAttribute
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public Language()
            : base(WebmasterToolsNameTable.XmlVerificationMethodElement, WebmasterToolsNameTable.gWebmasterToolsPrefix, WebmasterToolsNameTable.gWebmasterToolsNamspace)
        {
            this.Attributes.Add(WebmasterToolsNameTable.XmlAttributeLanguage, null);
        }
        /// <summary>
        /// constructor taking the initial value
        /// </summary>
        /// <param name="value"></param>
        public Language(String value)
            : base(WebmasterToolsNameTable.XmlAttributeLanguage, WebmasterToolsNameTable.gWebmasterToolsPrefix, WebmasterToolsNameTable.gWebmasterToolsNamspace)
        {
            this.Attributes.Add(WebmasterToolsNameTable.XmlAttributeLanguage, value);
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>convienience accessor for the Language attribute</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public string LanguageAttribute
        {
            get
            {
                return this.Attributes[WebmasterToolsNameTable.XmlAttributeLanguage] as string;
            }
            set
            {
                this.Attributes[WebmasterToolsNameTable.XmlAttributeLanguage] = value;
            }
        }
    }

    public class Read : SimpleElement
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public Read()
            : base(WebmasterToolsNameTable.XmlReadElement, WebmasterToolsNameTable.gWebmasterToolsPrefix, WebmasterToolsNameTable.gWebmasterToolsNamspace)
        { }
        /// <summary>
        /// constructor taking the initial value
        /// </summary>
        /// <param name="initValue"></param>
        public Read(string initValue)
            : base(WebmasterToolsNameTable.XmlReadElement, WebmasterToolsNameTable.gWebmasterToolsPrefix, WebmasterToolsNameTable.gWebmasterToolsNamspace, initValue)
        { }
    }

    public class Subject : SimpleNameValueAttribute
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public Subject()
            : base(WebmasterToolsNameTable.XmlSubjectElement, WebmasterToolsNameTable.gWebmasterToolsPrefix, WebmasterToolsNameTable.gWebmasterToolsNamspace)
        {
            this.Attributes.Add(WebmasterToolsNameTable.XmlSubjectElement, null);
        }

        /// <summary>
        /// constructor taking the initial value
        /// </summary>
        /// <param name="value"></param>
        public Subject(String value)
            : base(WebmasterToolsNameTable.XmlSubjectElement, WebmasterToolsNameTable.gWebmasterToolsPrefix, WebmasterToolsNameTable.gWebmasterToolsNamspace)
        {
            this.Attributes.Add(WebmasterToolsNameTable.XmlSubjectElement, value);
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>convienience accessor for the Subject attribute</summary> 
        /// <returns> </returns>
        //////////////////////////////////////////////////////////////////////
        public string SubjectAttribute
        {
            get
            {
                return this.Attributes[WebmasterToolsNameTable.XmlSubjectElement] as string;
            }
            set
            {
                this.Attributes[WebmasterToolsNameTable.XmlSubjectElement] = value;
            }
        }
    }

    /// <summary>
    /// wt:crawl-type schema extension describing a Webmaster Tools Crawl Type
    /// </summary>
    public class CrawlType : SimpleElement
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public CrawlType()
            : base(WebmasterToolsNameTable.XmlCrawlTypeElement, WebmasterToolsNameTable.gWebmasterToolsPrefix, WebmasterToolsNameTable.gWebmasterToolsNamspace)
        { }
        /// <summary>
        /// constructor taking the initial value
        /// </summary>
        /// <param name="initValue"></param>
        public CrawlType(string initValue)
            : base(WebmasterToolsNameTable.XmlCrawlTypeElement, WebmasterToolsNameTable.gWebmasterToolsPrefix, WebmasterToolsNameTable.gWebmasterToolsNamspace, initValue)
        { }
    }

    /// <summary>
    /// wt:issue-type schema extension describing a Webmaster Tools Issue Type
    /// </summary>
    public class IssueType : SimpleElement
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public IssueType()
            : base(WebmasterToolsNameTable.XmlIssueTypeElement, WebmasterToolsNameTable.gWebmasterToolsPrefix, WebmasterToolsNameTable.gWebmasterToolsNamspace)
        { }
        /// <summary>
        /// constructor taking the initial value
        /// </summary>
        /// <param name="initValue"></param>
        public IssueType(string initValue)
            : base(WebmasterToolsNameTable.XmlIssueTypeElement, WebmasterToolsNameTable.gWebmasterToolsPrefix, WebmasterToolsNameTable.gWebmasterToolsNamspace, initValue)
        { }
    }

    /// <summary>
    /// wt:issue-detail schema extension describing a Webmaster Tools Issue Detail
    /// </summary>
    public class IssueDetail : SimpleElement
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public IssueDetail()
            : base(WebmasterToolsNameTable.XmlIssueDetailElement, WebmasterToolsNameTable.gWebmasterToolsPrefix, WebmasterToolsNameTable.gWebmasterToolsNamspace)
        { }
        /// <summary>
        /// constructor taking the initial value
        /// </summary>
        /// <param name="initValue"></param>
        public IssueDetail(string initValue)
            : base(WebmasterToolsNameTable.XmlIssueDetailElement, WebmasterToolsNameTable.gWebmasterToolsPrefix, WebmasterToolsNameTable.gWebmasterToolsNamspace, initValue)
        { }
    }

    /// <summary>
    /// wt:linked-from schema extension describing a Webmaster Tools Linked From
    /// </summary>
    public class LinkedFrom : SimpleElement
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public LinkedFrom()
            : base(WebmasterToolsNameTable.XmlLinkedFromElement, WebmasterToolsNameTable.gWebmasterToolsPrefix, WebmasterToolsNameTable.gWebmasterToolsNamspace)
        { }
        /// <summary>
        /// constructor taking the initial value
        /// </summary>
        /// <param name="initValue"></param>
        public LinkedFrom(string initValue)
            : base(WebmasterToolsNameTable.XmlLinkedFromElement, WebmasterToolsNameTable.gWebmasterToolsPrefix, WebmasterToolsNameTable.gWebmasterToolsNamspace, initValue)
        { }
    }

    /// <summary>
    /// wt:date-detected schema extension describing a Webmaster Tools Date Detected
    /// </summary>
    public class DateDetected : SimpleElement
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public DateDetected()
            : base(WebmasterToolsNameTable.XmlDateDetectedElement, WebmasterToolsNameTable.gWebmasterToolsPrefix, WebmasterToolsNameTable.gWebmasterToolsNamspace)
        { }
        /// <summary>
        /// constructor taking the initial value
        /// </summary>
        /// <param name="initValue"></param>
        public DateDetected(string initValue)
            : base(WebmasterToolsNameTable.XmlDateDetectedElement, WebmasterToolsNameTable.gWebmasterToolsPrefix, WebmasterToolsNameTable.gWebmasterToolsNamspace, initValue)
        { }
    }
}
