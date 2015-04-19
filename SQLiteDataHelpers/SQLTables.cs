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
            public static Dictionary<String, String> CurrentSpeedcharts
            {
                get
                {
                    var columns = new Dictionary<String, String>() { { "Speedchart", "" } };
                    return columns;
                }
            }

            public static Dictionary<String, String> LicenseKeys
            {
                get
                {
                    var columns = new Dictionary<String, String>() 
                    { 
                        {"SoftwareID", ""},
                        {"LicenseKey", ""},
                        {"DateModified", ""},
                        {"ExpirationDate", ""},
                        {"KeyHolder", ""},
                        {"KeyManager", ""},
                        {"HolderLoginID",""},
                        {"LicenseCost",""},
                        {"RequisitionNumber",""},
                        {"ChargebackComplete",""},
                        {"ProviderID",""},
                        {"AssignmentStatus",""},
                        {"SpeedChart",""},
                        {"DateAssigned",""},
                        {"DateRemoved",""},
                        {"LicenseHolderCompany",""},
                        {"Description",""},
                        {"Comments",""},
                        {"FileSubpath",""},
                        {"LastModifiedBy", ""}
                    };
                    return columns;
                }
            }

            public static Dictionary<String, String> Providers
            {
                get
                {
                    var columns = new Dictionary<String, String>()  
                    {{"Organization", ""}};
                    return columns;
                }
            }

            public static Dictionary<String, String> Requests
            {
                get
                {
                    var columns = new Dictionary<String, String>()
                    {
                        {"RequestTitle",""},
                        {"Request",""},
                        {"RequestDate",""},
                        {"RequestingUser",""},
                        {"RequestingUserLogin",""}
                    };
                    return columns;
                }
            }

            public static Dictionary<String, String> Settings
            {
                get
                {
                    var columns = new Dictionary<String, String>() 
                    {                
                        {"Setting", ""},
                        {"Description", ""},
                        {"Value", ""},
                    };
                    return columns;
                }
            }

            public static Dictionary<String, String> Software
            {
                get
                {
                    var columns = new Dictionary<String, String>()
                    {
                        {"SoftwareName", ""},
                        {"Provider", ""}
                    };
                    return columns;
                }
            }

            public static Dictionary<String, String> Users
            {
                get
                {
                    var columns = new Dictionary<String, String>() 
                    {                         
                        {"FirstName", ""},
                        {"LastName", ""},
                        //{"IsAdmin", ""},
                        {"LoginID", ""},
                        {"PassHash", ""},
                        {"ManagerID", ""},
                        {"Salt", ""},
                        //{"IsManager",""}
                    };
                    return columns;
                }
            }

            public static Dictionary<String, String> UserAccess
            {
                get
                {
                    var columns = new Dictionary<String, String>() 
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
