using System;
using System.Net;
using System.Xml;
using Google.GData.Client;

namespace Google.GData.Apps
{
    /// <summary>
    /// The AppsException class defines an error resulting from
    /// a Google Apps provisioning request.
    /// </summary>
    public class AppsException : GDataRequestException
    {
        private string errorCode;
        private string invalidInput;
        private string reason;

        #region Defined Google Apps error codes

        /// <summary>
        /// Google Apps error indicating that the request failed
        /// for an unknown reason.
        /// </summary>
        public const string UnknownError = "1000";

        /// <summary>
        /// Google Apps error indicating that the request instructs
        /// Google to create a new user but uses the username of an
        /// account that was deleted in the previous five days.
        /// </summary>
        public const string UserDeletedRecently = "1100";

        /// <summary>
        /// Google Apps error indicating that the user identified in
        /// the request is suspended.
        /// </summary>
        public const string UserSuspended = "1101";

        /// <summary>
        /// Google Apps error indicating that the specified domain has
        /// already reached its quota of user accounts.
        /// </summary>
        public const string DomainUserLimitExceeded = "1200";

        /// <summary>
        /// Google Apps error indicating that the specified domain has
        /// already reached its quota of aliases. Aliases include
        /// nicknames and email lists. 
        /// </summary>
        public const string DomainAliasLimitExceeded = "1201";

        /// <summary>
        /// Google Apps error indicating that Google has suspended the
        /// specified domain's access to Google Apps.
        /// </summary>
        public const string DomainSuspended = "1202";

        /// <summary>
        /// Google Apps error indicating that the request instructs
        /// Google to create an entity that already exists.
        /// </summary>
        public const string EntityExists = "1300";

        /// <summary>
        /// Google Apps error indicating that the request asks Google
        /// to retrieve an entity that does not exist.
        /// </summary>
        public const string EntityDoesNotExist = "1301";

        /// <summary>
        /// Google Apps error indicating that the request instructs
        /// Google to create an entity with a reserved name, such as
        /// "abuse" or "postmaster".
        /// </summary>
        public const string EntityNameIsReserved = "1302";

        /// <summary>
        /// Google Apps error indicating that the request provides an
        /// invalid name for a requested resource.
        /// </summary>
        public const string EntityNameNotValid = "1303";

        /// <summary>
        /// Google Apps error indicating that the value in the API request 
        /// for the user's first name, or given name, contains unaccepted
        /// characters.
        /// </summary>
        public const string InvalidGivenName = "1400";

        /// <summary>
        /// Google Apps error indicating that the value in the API request
        /// for the user's surname, or family name, contains unaccepted
        /// characters.
        /// </summary>
        public const string InvalidFamilyName = "1401";

        /// <summary>
        /// Google Apps error indicating that the value in the API request
        /// for the user's password contains an invalid number of characters
        /// or unaccepted characters.
        /// </summary>
        public const string InvalidPassword = "1402";

        /// <summary>
        /// Google Apps error indicating that the value in the API request
        /// for the user's username contains unaccepted characters.
        /// </summary>
        public const string InvalidUsername = "1403";

        /// <summary>
        /// Google Apps error indicating that the request instructs Google
        /// to add users to an email list, but that list has already reached
        /// the maximum number of subscribers.
        /// </summary>
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
                                if (reader.NodeType == XmlNodeType.Element && reader.LocalName.Equals(AppsNameTable.XmlElementError))
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
