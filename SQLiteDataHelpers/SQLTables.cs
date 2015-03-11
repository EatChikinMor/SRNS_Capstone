using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SQLiteDataHelpers
{
    class SQLTables
    {
        public static class TableColumns
        {
            public static Dictionary<String, String> LicenseKeys
            {
                get
                {
                    Dictionary<String, String> columns = new Dictionary<String, String>() 
                    { 
                        {"SoftwareID", ""},
                        {"Key", ""},
                        {"DateModified", ""},
                        {"ExpirationDate", ""},
                        {"KeyOwnerID", ""},
                        {"AttachmentFiles", ""}
                    };
                    return columns;
                }
            }

            public static Dictionary<String, String> Organizations
            {
                get
                {
                    Dictionary<String, String> columns = new Dictionary<String, String>()  
                    {{"Organization", ""}};
                    return columns;
                }
            }

            public static Dictionary<String, String> Requests
            {
                get
                {
                    Dictionary<String, String> columns = new Dictionary<String, String>()
                    {
                        {"RequestingUser",""},
                        {"RequestedSoftware",""},
                        {"RequestedDate",""}
                    };
                    return columns;
                }
            }

            public static Dictionary<String, String> Software
            {
                get
                {
                    Dictionary<String, String> columns = new Dictionary<String, String>()
                    {{"SoftwareName", ""}};
                    return columns;
                }
            }

            public static Dictionary<String, String> Speedcharts
            {
                get
                {
                    Dictionary<String, String> columns = new Dictionary<String, String>()
                    {
                        {"KeyChargedAgainst",""},
                        {"SpeedChart",""},
                        {"OrganizationID",""}
                    };
                    return columns;
                }
            }

            public static Dictionary<String, String> Users
            {
                get
                {
                    Dictionary<String, String> columns = new Dictionary<String, String>() 
                    {                         
                        {"FirstName", ""},
                        {"LastName", ""},
                        {"IsAdmin", ""},
                        {"LoginID", ""},
                        {"PassHash", ""},
                        {"ManagerID", ""},
                        {"Salt", ""},
                        {"IsManager",""}
                    };
                    return columns;
                }
            }

            public static Dictionary<String, String> UserAccess
            {
                get
                {
                    Dictionary<String, String> columns = new Dictionary<String, String>() 
                    {                         
                        {"UserID", ""},
                        {"Requests", ""},
                        {"AddLicense", ""},
                        {"LicenseCountReport", ""},
                        {"AvailLicenseReport", ""},
                        {"ManagLicenseReport", ""},
                        {"LicenseExpReport", ""},
                        {"PendChargeReport", ""}
                    };
                    return columns;
                }
            }
        }
    }

}
