using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Drawing;
using System.Security.AccessControl;
using SQLiteDataHelpers;

namespace SRNS_Capstone
{
    public class ActiveDirectoryHelper
    {
        private static readonly string _domain = getCurrentDomain();

        public static string getCurrentDomain()
        {
            DBConnector conn = new DBConnector();

            return conn.getSettingValueByName("DOMAIN");
        }

        public static bool AuthenticeUser(string username, string password)
        {
            bool isValid;
            using (PrincipalContext auth = new PrincipalContext(ContextType.Domain, _domain))
            {
                isValid = auth.ValidateCredentials(username, password);
            }
            return isValid;
        }

        /// <summary>
        /// Item 1 = User.Name,
        /// Item 2 = User.DistinguishedName,
        /// Item 3 = User.sAMAccountName
        /// </summary>
        /// <returns></returns>
        public static List<Tuple<String, String, String>> GetAdUsers()
        {
            PrincipalContext AD = new PrincipalContext(ContextType.Domain, _domain);
            UserPrincipal u = new UserPrincipal(AD);
            PrincipalSearcher search = new PrincipalSearcher(u);

            return (from user in search.FindAll() where user != null && user.DisplayName != null select new Tuple<string, string, string>(user.Name, user.DistinguishedName, user.SamAccountName)).ToList();

            /* ////  Below statement equivalent to above  \\\\
            
            List<Tuple<String, String, String>> lstADUsers = new List<Tuple<String, String, String>>();
            
            foreach (var user in search.FindAll())
            {
                if (user != null && user.DisplayName != null)
                {
                    lstADUsers.Add(new Tuple<string, string, string>(user.Name, user.DistinguishedName, user.SamAccountName));
                }
            }

            return lstADUsers;             
            */
        }

        public static DirectoryEntry getUserByLogin(string LoginID, string domain = null)
        {
            using (DirectorySearcher adSearch = new DirectorySearcher("(sAMAccountName=" + LoginID + ")"))
            {
                SearchResult adSearchResult = adSearch.FindOne();

                return adSearchResult.GetDirectoryEntry();
            }
        }

        public static bool doesUsernameExist(string Username)
        {
            var checkAgainst = GetAdUsers();

            return checkAgainst.Any(item => item.Item3 == Username);
        }

        #region Deprecated / Unused 

        private string LDAP = GenerateLDAP();

        private static string GenerateLDAP()
        {
            DirectoryEntry root = new DirectoryEntry("LDAP://RootDSE");

            using (root)
            {
                string dnc = root.Properties["defaultNamingContext"][0].ToString();
                string server = root.Properties["dnsHostName"][0].ToString();

                string adsPath = String.Format(
                    "LDAP://{0}/{1}",
                    server,
                    dnc
                    );

                return adsPath;
            }
        }

        /// <summary>
        /// Deprecated - use GetAdUsers()
        /// Item1:Login, Item2: "FirstName LastName"
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public static List<Tuple<String, String>> getAllUsersOnDomain(string domain = null)
        {
            /* Very very slow method, it brings back a large collection of large objects
             * (DirectoryEntries) Which then have to be searched over to return any properties.
             * Iterating over 74 records takes about 15 seconds. I Assume SRNS has a much
             * larger Active Directory that would bring it to its knees.
             * 
             * Deferring to GetAdUsers()
             */

            var ADUsers = new List<Tuple<String, String>>();

            DirectoryEntry directoryEntry = new DirectoryEntry("WinNT://" + (domain ?? _domain));


            //string root = GenerateLDAP();
            //DirectoryEntry directoryEntry = new DirectoryEntry(root);
            foreach (DirectoryEntry child in directoryEntry.Children)
            {
                if (child.SchemaClassName == "User")
                {


                    string user = child.Properties["Name"].Value.ToString();
                    DirectorySearcher searcher = new DirectorySearcher("(samaccountname=" + user + ")");
                    SearchResult result = searcher.FindOne();
                    DirectoryEntry dentry = result.GetDirectoryEntry();
                    try
                    {
                        ADUsers.Add(new Tuple<string, string>(user, dentry.Properties["displayName"].Value.ToString()));//
                    }
                    catch (NullReferenceException)
                    {
                        //Catch users with no displayName field
                        continue;
                    }
                }
            }

            return ADUsers;
        }

        #endregion
    }
}