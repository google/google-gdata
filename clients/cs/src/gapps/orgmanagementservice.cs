using System;
using System.Collections.Generic;
using System.Web;
using Google.GData.Extensions.Apps;

namespace Google.GData.Apps
{
    /// <summary>
    /// Service class for managing organization units and users within Google Apps. 
    /// 
    /// An OrgUnit path is the URL-encoding (e.g., using HttpUtility.UrlPathEncode) of an OrgUnit's lineage, 
    /// concatenated together with the slash ('/') character.
    /// </summary>
    public class OrganizationService : AppsPropertyService
    {
        public enum OrgUnitProperty
        {
            Name, Description, ParentOrgUnitPath, BlockInheritance, UsersToMove
        }

        public OrganizationService(String domain, String applicationName)
            : base(domain, applicationName)
        {
        }

        /// <summary>
        /// Retrieves the customer Id that will be used for all other operations.
        /// </summary>
        /// <param name="domain">the customer domain</param>
        /// <returns></returns>
        public AppsExtendedEntry RetrieveCustomerId(string domain)
        {
            String customerIdUri = String.Format("{0}/{1}",
                AppsOrganizationNameTable.AppsCustomerBaseFeedUri, AppsOrganizationNameTable.CustomerId);
            return Get(customerIdUri) as AppsExtendedEntry;
        }

        /// <summary>
        /// Create a new organization unit under the given parent.
        /// </summary>
        /// <param name="customerId">The unique Id of the customer retrieved through customer feed.</param>
        /// <param name="orgUnitName">The new organization name.</param>
        /// <param name="parentOrgUnitPath">The path of the parent organization unit where 
        /// '/' denotes the root of the organization hierarchy. 
        /// For any OrgUnits to be created directly under root, specify '/' as parent path.</param>
        /// <param name="description">A description for the organization unit created.</param>
        /// <param name="blockInheritance">If true, blocks inheritance of policies from 
        /// parent units.</param>
        /// <returns></returns>
        public AppsExtendedEntry CreateOrganizationUnit(String customerId, String orgUnitName,
            String parentOrgUnitPath, String description, bool blockInheritance)
        {
            Uri orgUnitUri = new Uri(String.Format("{0}/{1}",
                AppsOrganizationNameTable.AppsOrgUnitBaseFeedUri, customerId));

            AppsExtendedEntry entry = new AppsExtendedEntry();
            entry.Properties.Add(new PropertyElement(AppsOrganizationNameTable.ParentOrgUnitPath, parentOrgUnitPath));
            entry.Properties.Add(new PropertyElement(AppsOrganizationNameTable.Description, description));
            entry.Properties.Add(new PropertyElement(AppsOrganizationNameTable.OrgUnitName, orgUnitName));
            entry.Properties.Add(new PropertyElement(AppsOrganizationNameTable.BlockInheritance, blockInheritance.ToString()));
            return Insert(orgUnitUri, entry); ;
        }


        /// <summary>
        /// Retrieves an organization unit from the customer's domain.
        /// </summary>
        /// <param name="orgUnitPath">The path of the unit to be retrieved for e.g /corp</param>
        /// <returns></returns>
        public AppsExtendedEntry RetrieveOrganizationUnit(String customerId, String orgUnitPath)
        {
            String uri = String.Format("{0}/{1}/{2}",
                                    AppsOrganizationNameTable.AppsOrgUnitBaseFeedUri,
                                    customerId, orgUnitPath);

            return Get(uri) as AppsExtendedEntry;

        }

        /// <summary>
        /// Retrieves all organization units for the given customer account.
        /// </summary>
        /// <param name="customerId">The unique Id of the customer retrieved through customer feed.</param>
        /// <returns></returns>
        public AppsExtendedFeed RetrieveAllOrganizationUnits(String customerId)
        {
            String uri = String.Format("{0}/{1}?get=all", AppsOrganizationNameTable.AppsOrgUnitBaseFeedUri, customerId);
            return QueryExtendedFeed(new Uri(uri), true);
        }

        /// <summary>
        /// Retrieves all the child units of the given organization unit.
        /// </summary>
        /// <param name="customerId">The unique Id of the customer retrieved through customer feed.</param>
        /// <param name="orgUnitPath">The path of the unit to be retrieved for e.g /corp</param>
        /// <returns></returns>
        public AppsExtendedFeed RetrieveChildOrganizationUnits(String customerId, String orgUnitPath)
        {
            String uri = String.Format("{0}/{1}?get=children&orgUnitPath={2}",
                                       AppsOrganizationNameTable.AppsOrgUnitBaseFeedUri, customerId,
                                       HttpUtility.UrlEncode(orgUnitPath));
            return QueryExtendedFeed(new Uri(uri), true);
        }

        /// <summary>
        ///  Deletes the given organization unit. The unit must not have any OrgUsers or any
        /// child OrgUnits for it to be deleted
        /// </summary>
        /// <param name="customerId">The unique Id of the customer retrieved through customer feed.</param>
        /// <param name="orgUnitPath">The path of the unit to be retrieved for e.g /corp</param>
        public void DeleteOrganizationUnit(String customerId, String orgUnitPath)
        {
            String uri = String.Format("{0}/{1}/{2}", AppsOrganizationNameTable.AppsOrgUnitBaseFeedUri, customerId,
                                       orgUnitPath);
            Delete(new Uri(uri));
        }

        /// <summary>
        /// Updates the given organization attributes. 
        /// attributes.USERS_TO_MOVE is a comma separated list of email addresses that are to be moved across orgUnits.
        /// </summary>
        /// <param name="customerId">The unique Id of the customer retrieved through customer feed.</param>
        /// <param name="orgUnitPath">The path of the unit to be retrieved for e.g /corp</param>
        /// <param name="attributes">A dictionary of <code>OrgUnitProperty</code> and values to be updated.</param>
        /// <returns></returns>
        public AppsExtendedEntry UpdateOrganizationUnit(String customerId, String orgUnitPath, IDictionary<OrgUnitProperty, String> attributes)
        {
            AppsExtendedEntry entry = new AppsExtendedEntry();
            String uri = String.Format("{0}/{1}/{2}",
                                       AppsOrganizationNameTable.AppsOrgUnitBaseFeedUri, customerId,
                                       orgUnitPath);
            entry.EditUri = new Uri(uri);
            foreach (KeyValuePair<OrgUnitProperty, String> mapEntry in attributes)
            {
                String value = mapEntry.Value;
                if (string.IsNullOrEmpty(value))
                {
                    continue;
                }
                switch (mapEntry.Key)
                {
                    case OrgUnitProperty.Name:
                        entry.Properties.Add(new PropertyElement(AppsOrganizationNameTable.OrgUnitName, value));
                        break;
                    case OrgUnitProperty.ParentOrgUnitPath:
                        entry.Properties.Add(new PropertyElement(AppsOrganizationNameTable.ParentOrgUnitPath, value));

                        break;
                    case OrgUnitProperty.Description:
                        entry.Properties.Add(new PropertyElement(AppsOrganizationNameTable.Description, value));
                        break;
                    case OrgUnitProperty.BlockInheritance:
                        entry.Properties.Add(new PropertyElement(AppsOrganizationNameTable.BlockInheritance, value));
                        break;
                    case OrgUnitProperty.UsersToMove:
                        entry.Properties.Add(new PropertyElement(AppsOrganizationNameTable.UsersToMove, value));

                        break;
                    default:
                        break;
                }
            }
            return Update(entry);
        }

        /// <summary>
        /// Updates the orgunit of an organization user.
        /// </summary>
        /// 
        /// <param name="customerId"></param>
        /// <param name="orgUserEmail">The email address of the user</param>
        /// <param name="oldOrgUnitPath">The old organization unit path.
        /// If specified, validates the OrgUser's current path.</param>
        /// <param name="newOrgUnitPath"></param>
        /// <returns></returns>
        public AppsExtendedEntry UpdateOrganizationUser(String customerId, String orgUserEmail,
          String newOrgUnitPath, String oldOrgUnitPath)
        {
            String uri = String.Format("{0}/{1}/{2}",
                                       AppsOrganizationNameTable.AppsOrgUserBaseFeedUri, customerId,
                                       orgUserEmail);
            AppsExtendedEntry entry = new AppsExtendedEntry();
            entry.EditUri = new Uri(uri);

            if (!string.IsNullOrEmpty(oldOrgUnitPath))
            {
                entry.Properties.Add(new PropertyElement(AppsOrganizationNameTable.OldOrgUnitPath, oldOrgUnitPath));
            }
            entry.Properties.Add(new PropertyElement(AppsOrganizationNameTable.NewOrgUnitPath, newOrgUnitPath));
            return Update(entry);
        }

        /// <summary>
        /// Updates the orgunit of an organization user.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="orgUserEmail">The email address of the user.</param>
        /// <param name="newOrgUnitPath">The new organization unit path.</param>
        /// <returns></returns>
        public AppsExtendedEntry UpdateOrganizationUser(String customerId, String orgUserEmail,
          String newOrgUnitPath)
        {
            return UpdateOrganizationUser(customerId, orgUserEmail, newOrgUnitPath, null);
        }

        /// <summary>
        ///  Retrieves the details of a given organization user.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="orgUserEmail">The email address of the user</param>
        /// <returns>AppsExtendedEntry</returns>
        public AppsExtendedEntry RetrieveOrganizationUser(String customerId, String orgUserEmail)
        {
            String uri = String.Format("{0}/{1}/{2}",
                                       AppsOrganizationNameTable.AppsOrgUserBaseFeedUri, customerId,
                                       orgUserEmail);
            return Get(uri) as AppsExtendedEntry;

        }

        /// <summary>
        /// Retrieves the next page of a paginated feed using resumeKey from the previous feed.
        /// i.e. <code>atom:next</code>  link
        /// </summary>
        /// <param name="resumeKey"></param>
        /// <returns></returns>
        public AppsExtendedFeed RetrieveNextPageFromResumeKey(String resumeKey)
        {
            return QueryExtendedFeed(new Uri(resumeKey), false);
        }

        /// <summary>
        /// Retrieves all users belongging to all org units. This may take a long time to execute for
        /// domains with large number of users. Instead use pagination i.e. 
        /// <code>RetrieveFirstPageOrganizationUsers</code>
        /// followed by <code>RetrieveNextPageFromResumeKey</code>
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public AppsExtendedFeed RetrieveAllOrganizationUsers(String customerId)
        {
            string uri = String.Format("{0}/{1}?get=all",
                                       AppsOrganizationNameTable.AppsOrgUserBaseFeedUri, customerId);
            return QueryExtendedFeed(new Uri(uri), true);
        }

        /// <summary>
        /// Retrieves first page of results for all org users query.  For subsequent pages, 
        /// follow up with <code>RetrieveNextPageFromResumeKey</code>
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public AppsExtendedFeed RetrieveFirstPageOrganizationUsers(String customerId)
        {
            string uri = String.Format("{0}/{1}?get=all",
                                       AppsOrganizationNameTable.AppsOrgUserBaseFeedUri, customerId);
            return QueryExtendedFeed(new Uri(uri), false);
        }


        /// <summary>
        /// Retrieves all users by org unit. This may take a long time to execute for domains with 
        /// large number of users. Instead use pagination i.e. 
        /// <code>RetrieveFirstPageOfOrganizationUsersByOrgUnit</code>
        /// followed by <code>RetrieveNextPageFromResumeKey</code>
        /// </summary>
        /// <param name="customerId">The unique Id of the customer retrieved through customer feed.</param>
        /// <param name="orgUnitPath">The path of the unit to be retrieved for e.g /corp</param>
        /// <returns></returns>
        public AppsExtendedFeed RetrieveAllOrganizationUsersByOrgUnit(String customerId, String orgUnitPath)
        {
            String uri = String.Format("{0}/{1}?get=children&orgUnitPath={2}",
                                       AppsOrganizationNameTable.AppsOrgUserBaseFeedUri, customerId,
                                       HttpUtility.UrlEncode(orgUnitPath));
            return QueryExtendedFeed(new Uri(uri), true);
        }

        /// <summary>
        /// Retrieves first page of results for all org users by orgunit query.  For subsequent pages, 
        /// follow up with <code>RetrieveNextPageFromResumeKey</code>
        /// </summary>
        /// <param name="customerId">The unique Id of the customer retrieved through customer feed.</param>
        /// <param name="orgUnitPath">The path of the unit to be retrieved for e.g /corp</param>
        /// <returns></returns>
        public AppsExtendedFeed RetrieveFirstPageOfOrganizationUsersByOrgUnit(String customerId, String orgUnitPath)
        {
            String uri = String.Format("{0}/{1}?get=children&orgUnitPath={2}",
                                       AppsOrganizationNameTable.AppsOrgUserBaseFeedUri, customerId,
                                       HttpUtility.UrlEncode(orgUnitPath));
            return QueryExtendedFeed(new Uri(uri), false);
        }
    }
}
