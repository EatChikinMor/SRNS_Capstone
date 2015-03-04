using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SQLiteDataHelpers
{
    class SQL
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
                        {"Salt", ""}
                    };                    
                    //columns[0] = "ID";
                    //columns[1] = "FirstName";
                    //columns[2] = "LastName";
                    //columns[3] = "IsAdmin";
                    //columns[4] = "LoginID";
                    //columns[5] = "PassHash";
                    //columns[6] = "ManagerID";
                    //columns[7] = "Salt";
                    return columns;
                }
            }
        }

        public static string insertInto(string table, string[] columns, bool skipFirst)
        {
            ///Currently returns half the statement - "INSERT INTO [TABLE] ([COLUMN1], [COLUMN2], ...) VALUES ("
            int index = skipFirst ? 1 : 0;
            StringBuilder SQLStatement = new StringBuilder("INSERT INTO " + table + " (");

            for (int i = index; i < columns.Length; i++)
            {
                SQLStatement.Append(columns[i]);

                if (i != columns.Length - 1)
                {
                    SQLStatement.Append(", ");
                }
            }

            SQLStatement.Append(") VALUES(");

            return SQLStatement.ToString();
        }
    }

}
