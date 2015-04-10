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
                        {"SpeedChartID",""},
                        {"DateAssigned",""},
                        {"DateRemoved",""},
                        {"DateExpiring",""},
                        {"LicenseHolderCompany",""},
                        {"Description",""},
                        {"Comments",""}
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
                        {"RequestingUser",""},
                        {"RequestedSoftware",""},
                        {"RequestedDate",""}
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
                    {{"SoftwareName", ""}};
                    return columns;
                }
            }

            public static Dictionary<String, String> Speedcharts
            {
                get
                {
                    var columns = new Dictionary<String, String>()
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

            //private int _SoftwareId;
            //private string _Description;
            //private string _Key;
            //private string _Holder;
            //private string _Manager;
            //private string _HolderID;
            //private decimal _LicenseCost;
            //private string _RequisitionNumber;
            //private bool _ChargebackComplete;
            //private int _Provider;
            //private int _Assignment;
            //private string _Speedchart;
            //private DateTime _DateUpdated;
            //private DateTime _DateAssigned;
            //private DateTime _DateRemoved;
            //private DateTime _DateExpiring;
            //private int _LicenseHolderCompany;
            //private string _Comments;
        }
    }

}
