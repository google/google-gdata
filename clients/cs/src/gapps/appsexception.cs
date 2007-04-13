using System;
// using System.Text;
 using System.Net;
// using System.IO;
using System.Xml;
using Google.GData.Client;

namespace Google.GData.Apps
{
    public class AppsException : GDataRequestException
    {
        private string errorCode;
        private string invalidInput;
        private string reason;

        #region Defined Google Apps error codes

        public const string UnknownError = "1000";
        public const string UserDeletedRecently = "1100";
        public const string UserSuspended = "1101";
        public const string DomainUserLimitExceeded = "1200";
        public const string DomainAliasLimitExceeded = "1201";
        public const string DomainSuspended = "1202";
        public const string EntityExists = "1300";
        public const string EntityDoesNotExist = "1301";
        public const string EntityNameIsReserved = "1302";
        public const string EntityNameNotValid = "1303";
        public const string InvalidGivenName = "1400";
        public const string InvalidFamilyName = "1401";
        public const string InvalidPassword = "1402";
        public const string InvalidUsername = "1403";
        public const string TooManyRecipientsOnEmailList = "1500";

        #endregion

        /// <summary>
        /// Constructs a new AppsException with no properties set.
        /// </summary>
        public AppsException()
            : base()
        {
            errorCode = null;
            invalidInput = null;
            reason = null;
        }

        /// <summary>
        /// Constructs a new AppsException to be parsed from the specified
        /// GDataRequestException.
        /// </summary>
        /// <param name="e"></param>
        /// <seealso cref="ParseAppsException(GDataRequestException)"/>
        public AppsException(GDataRequestException e)
            : base("A Google Apps error occurred.", e)
        {
            this.errorCode = null;
            this.invalidInput = null;
            this.reason = null;
        }

        /// <summary>
        /// Constructs a new AppsException with the specified properties.
        /// </summary>
        /// <param name="errorCode">the value of the ErrorCode property</param>
        /// <param name="invalidInput">the value of the InvalidInput property</param>
        /// <param name="reason">the value of the Reason property</param>
        public AppsException(string errorCode, string invalidInput, string reason)
            : base("Google Apps error: " + errorCode + ". Invalid input: " +
                   invalidInput + ". Reason: " + reason)
        {
            this.errorCode = errorCode;
            this.invalidInput = invalidInput;
            this.reason = reason;
        }

        /// <summary>
        /// Accessor for ErrorCode.  This property specifies the
        /// type of error that caused an API request to fail.
        /// </summary>
        public string ErrorCode
        {
            get { return errorCode; }
            set { errorCode = value; }
        }

        /// <summary>
        /// Accessor for InvalidInput.  This property contains the
        /// data that caused an API response to fail; it may not be
        /// provided for all error types.
        /// </summary>
        public string InvalidInput
        {
            get { return invalidInput; }
            set { invalidInput = value; }
        }

        /// <summary>
        /// Accessor for Reason.  This property contains a short
        /// explanation of the error that occurred.
        /// </summary>
        public string Reason
        {
            get { return reason; }
            set { reason = value; }
        }

        /// <summary>
        /// Parses a GDataRequestException, which wraps the HTTP
        /// error responses, into an AppsException.
        /// </summary>
        /// <param name="e">the GDataRequestException to parse</param>
        /// <returns>a new AppsException object. The object's ErrorCode,
        /// InvalidInput and Reason properties will be set if the XML
        /// in the HTTP response could be parsed, or null otherwise.</returns>
        public static AppsException ParseAppsException(GDataRequestException e)
        {
            AppsException result = null;

            if (e != null)
            {
                WebException w = e.InnerException as WebException;
                if (w != null)
                {
                    HttpWebResponse response = w.Response as HttpWebResponse;
                    if (response != null && response.GetResponseStream() != null)
                    {
                     
                        try
                        {
                            XmlReader reader = new XmlTextReader(response.GetResponseStream());
                            // now find the ErrorElement
                            while (reader.Read())
                            {
                                if (reader.NodeType == XmlNodeType.EndElement && reader.LocalName.Equals(AppsNameTable.XmlElementError))
                                {
                                    break;
                                }
                            }

                            if (reader.EOF == false)
                            {
                                result = new AppsException(e);
                                result.ErrorCode =
                                    reader.GetAttribute(AppsNameTable.XmlAttributeErrorErrorCode);
                                result.InvalidInput =
                                    reader.GetAttribute(AppsNameTable.XmlAttributeErrorInvalidInput);
                                result.Reason =
                                    reader.GetAttribute(AppsNameTable.XmlAttributeErrorReason);
                            }
                        }
                        catch (XmlException)
                        {
                            // Silently fail if we couldn't parse the XML
                        }
                    }
                }
            }

            return result;
        }
    }
}
