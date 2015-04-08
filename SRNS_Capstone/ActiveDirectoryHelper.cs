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
        #region unused LDAP
        //private static readonly string LDAP = GenerateLDAP();

        //private static string GenerateLDAP()
        //{
        //    DirectoryEntry root = new DirectoryEntry("LDAP://RootDSE");

        //    using (root)
        //    {
        //        string dnc = root.Properties["defaultNamingContext"][0].ToString();
        //        string server = root.Properties["dnsHostName"][0].ToString();

        //        string adsPath = String.Format(
        //            "LDAP://{0}/{1}",
        //            server,
        //            dnc
        //            );

        //        return adsPath;
        //    }
        //}
        #endregion

        private static string _domain = getCurrentDomain();

        public static string getCurrentDomain()
        {
            DBConnector conn = new DBConnector();

            return conn.getSettingValueByName("DOMAIN");
        }

        public static bool AuthenticeUser(string username, string password)
        {
            bool isValid;
            using (PrincipalContext auth = new PrincipalContext(ContextType.Domain, "CALLINGPOST"))
            {
                isValid = auth.ValidateCredentials(username, password);
            }
            return isValid;
        }

        /// <summary>
        /// Item1:Login, Item2: "FirstName LastName"
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public static List<Tuple<String, String>> getAllUsersOnDomain(string domain = null)
        {
            var ADUsers = new List<Tuple<String, String>>();

            DirectoryEntry directoryEntry = new DirectoryEntry("WinNT://" + (domain ?? _domain));
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

        public static DirectoryEntry getUserByLogin(string LoginID, string domain = null)
        {
            using (DirectorySearcher adSearch = new DirectorySearcher("(sAMAccountName=" + LoginID + ")"))
            {
                SearchResult adSearchResult = adSearch.FindOne();

                return adSearchResult.GetDirectoryEntry();
            }
        }

        public static bool doesUsernameExist( string Username )
        {
            var checkAgainst = getAllUsersOnDomain();

            return checkAgainst.Any(item => item.Item1 == Username);
        }

    }
}