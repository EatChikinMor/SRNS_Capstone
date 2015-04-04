using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.IO;
using System.Security.Cryptography;
using SQLiteDataHelpers.Objects;

namespace SQLiteDataHelpers
{
    public class DBConnector
    {
        readonly SQLiteHelper SQLiteDataHelper = new SQLiteHelper();

        #region SELECTS

        public int getTableRowCount(string table)
        {
            string SQL = "SELECT COUNT(ID) FROM " + table;

            return Convert.ToInt32(SQLiteDataHelper.ExecuteScalar(SQL));
        }

        public DataTable selectAllTableColumns(string table, string whereClause)
        {
            string SQL = "SELECT * FROM " + table + " " + whereClause;

            DataTable result = SQLiteDataHelper.GetDataTable(SQL);

            return result;
        }

        #region Table Users

        public bool IsUsernameAvailable(string username)
        {
            String SQL = "SELECT COUNT(LoginID) FROM Users WHERE LoginID = '" + username + "'";

            string result = SQLiteDataHelper.ExecuteScalar(SQL);

            return Convert.ToInt32(result) == 0;
        }

        public bool IsThisTheCurrentUsername(string username, string userId)
        {
            String SQL = "SELECT LoginID FROM Users WHERE ID = '" + userId + "'";

            string result = SQLiteDataHelper.ExecuteScalar(SQL);

            return String.CompareOrdinal(username, result) == 0;
        }

        public DataTable getUserOnLogin(string username, string password)
        {
            string SQL = "SELECT Salt FROM USERS WHERE LoginID = '" + username + "'";

            string Salt = SQLiteDataHelper.ExecuteScalar(SQL);

            string passHash;

            passHash = GenerateMd5Hash(Salt + password);
            
            SQL = "SELECT ID, FirstName, LastName, IsAdmin, LoginID, ManagerID FROM Users WHERE LoginID = '" + username + "'" + " AND PassHash = '" + passHash + "'";

            DataTable dt = SQLiteDataHelper.GetDataTable(SQL);

            return dt;
        }

        public DataTable getUserByID(int userID)
        {
            String SQL = "SELECT * FROM Users WHERE ID = " + userID;

            DataTable dt =  SQLiteDataHelper.GetDataTable(SQL);

            return dt;
        }

        public List<ComboboxItem> populateUserList()
        {
            // TODO: Alter OrderBy to Sort alphabetically by First, Last name
            const string SQL = "Select ID, FirstName, LastName, LoginID FROM Users ORDER BY FirstName, LastName DESC";

            var dt = SQLiteDataHelper.GetDataTable(SQL);

            var users = new List<ComboboxItem>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var item = new ComboboxItem()
                {
                    Value = dt.Rows[i]["ID"].ToString(),
                    Text = (string)dt.Rows[i]["FirstName"] + " " + (string)dt.Rows[i]["LastName"] + " - " + (string)dt.Rows[i]["LoginID"]
                };

                users.Add(item);
            }

            return users;
        }

        public DataTable getManagers()
        {
            String SQL = "SELECT ID, FirstName, LastName FROM Users WHERE IsManager = 1";

            DataTable dt = SQLiteDataHelper.GetDataTable(SQL);

            return dt;
        }

        #endregion

        #region Table Software

        public DataTable getSoftwareById(string ID)
        {
            string SQL =
                "SELECT S.[ID], [SoftwareName], [Provider], [Organization] " +
                "FROM Software S " +
                "INNER JOIN Providers P ON Provider =  P.ID " +
                "WHERE S.ID = " + ID;

            return SQLiteDataHelper.GetDataTable(SQL);
        }

        public DataTable getAllSoftware()
        {
            const string SQL = "SELECT  soft.[ID], [SoftwareName], [Organization] " +
                               "FROM Software soft " +
                               "JOIN Providers pro on soft.Provider = pro.ID";

            DataTable dt = SQLiteDataHelper.GetDataTable(SQL);

            return dt;
        }

        public DataTable getLicenseCountReport(bool showProvider = true)
        {
            string SQL;
            if (showProvider)
            {

                SQL ="SELECT [Provider] || '_' || S.[ID] AS [SoftCode], [Organization] ||' '|| [SoftwareName] AS [SoftName], " +
                     "COALESCE(COUNT(L.SoftwareID), 0) AS [LicCount] " +
                     "FROM Software S " +
                     "INNER JOIN Providers P ON Provider =  P.ID " +
                     "LEFT JOIN LicenseKeys L ON L.SoftwareID = S.ID " +
                     "GROUP BY SoftCode, SoftName " +
                     "ORDER BY Organization, SoftwareName";
            }
            else
            {
                SQL =
                    "SELECT [Provider] || '_' || S.[ID] AS [SoftCode], [SoftwareName] AS [SoftName], " +
                     "COALESCE(COUNT(L.SoftwareID), 0) AS [LicCount] " +
                     "FROM Software S " +
                     "INNER JOIN Providers P ON Provider =  P.ID " +
                     "LEFT JOIN LicenseKeys L ON L.SoftwareID = S.ID " +
                     "GROUP BY SoftCode, SoftName " +
                     "ORDER BY SoftwareName";
            }

            return SQLiteDataHelper.GetDataTable(SQL);
        }

        public DataTable getLicenseCountReportDetail(string SoftwareId, bool expired = false)
        {
            var andExpired = expired 
                ? "AND [ExpirationDate] < CURRENT_TIMESTAMP " 
                : "AND [ExpirationDate] > CURRENT_TIMESTAMP ";

            var SQL =
                "SELECT COALESCE(U.[FirstName] || ' ' || U.[LastName], 'Not Assigned') AS Name, [ExpirationDate], [LicenseKey] " +
                "FROM LicenseKeys " +
                "LEFT OUTER JOIN Users U ON KeyOwnerID = U.ID " +
                "WHERE SoftwareID = "+ SoftwareId +"  " +
                andExpired +
                "ORDER BY KeyOwnerID, ExpirationDate";

            return SQLiteDataHelper.GetDataTable(SQL);
        }

        #endregion

        #endregion

        #region INSERTS

        public string InsertUser(User user, bool IsAdmin)
        {
            //////////// Manually Add user \\\\\\\\\\\\
            //User a = new User();
            //a.FirstName = "Austin";
            //a.LastName = "Rich";
            //a.IsAdmin = true;
            //a.LoginID = "user";
            //a.PassHash = "123456a";
            //a.ManagerID = 1;
            //a.Salt = GenerateRandomString();
            //a.IsManager = true;

            //user = a;

            //UserAccess b = new UserAccess()
            //{
            //    UserID = 0,
            //    Requests = true,
            //    AddLicense = true,
            //    AvailLicenseReport = true,
            //    LicenseCountReport = true,
            //    LicenseExpReport = true,
            //    ManagLicenseReport = true,
            //    PendChargeReport = false
            //};

            //userA = b;
            //-----------------------------------------\\

            if (!IsUsernameAvailable(user.LoginID)) return "The chosen username already exists.";

            user.Salt = GenerateRandomString();

            user.PassHash = GenerateMd5Hash(user.Salt + user.PassHash);

            Dictionary<String, String> Users = SQLTables.TableColumns.Users;
            Users["FirstName"] = user.FirstName;
            Users["LastName"] = user.LastName;
            Users["IsAdmin"] = Convert.ToInt32(user.IsAdmin).ToString();
            Users["LoginID"] = user.LoginID;
            Users["PassHash"] = user.PassHash;
            Users["ManagerID"] = Convert.ToInt32(user.ManagerID).ToString();
            Users["Salt"] = user.Salt;
            Users["IsManager"] = Convert.ToInt32(user.IsManager).ToString();

            string error = "", retError = "";
            if (!SQLiteDataHelper.Insert("USERS", Users, ref error))
            {
                return error;
            }

            return "User successfully created";
        }

        public int GetMaxId(string tableName)
        {
            string SQL = "SELECT MAX(ID) FROM " + tableName;

            int ID = Convert.ToInt32(SQLiteDataHelper.ExecuteScalar(SQL));

            return ID;
        }

        #endregion

        #region UPSERTS
        //
        #endregion

        #region UPDATES

        public string UpdateUser(string UserID, User user)
        {
            if (!IsUsernameAvailable(user.LoginID) && !IsThisTheCurrentUsername(user.LoginID, UserID))
                return "The chosen username already exists.";

            user.Salt = GenerateRandomString();

            user.PassHash = GenerateMd5Hash(user.Salt + user.PassHash);

            var Users = SQLTables.TableColumns.Users;
            Users["FirstName"] = user.FirstName;
            Users["LastName"] = user.LastName;
            Users["IsAdmin"] = user.IsAdmin.ToString();
            Users["LoginID"] = user.LoginID;
            Users["PassHash"] = user.PassHash;
            Users["ManagerID"] = user.ManagerID.ToString();
            Users["Salt"] = user.Salt;
            Users["IsManager"] = user.IsManager.ToString();

            string where = "ID = " + UserID;

            return !SQLiteDataHelper.Update("USERS", Users, @where) 
                ? "User Update failed" 
                : "User successfully updated";
        }

        #endregion

        #region DELETES

        public bool DeleteUser(string UserId)
        {
            string whereUsers = "ID = " + UserId;
            string whereUserA = "UserID = " + UserId;
            int ret = 0;

            return SQLiteDataHelper.Delete("Users", whereUsers);
        }

        #endregion

        static string GenerateMd5Hash(string input)
        {
            //// Convert the input string to a byte array and compute the hash. 
            //byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            //// Create a new Stringbuilder to collect the bytes 
            //// and create a string.
            //StringBuilder sBuilder = new StringBuilder();

            //// Loop through each byte of the hashed data  
            //// and format each one as a hexadecimal string. 
            //for (int i = 0; i < data.Length; i++)
            //{
            //    sBuilder.Append(i.ToString("x2"));
            //}

            //// Return the hexadecimal string. 
            //return sBuilder.ToString();

            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            foreach (byte t in hash)
            {
                sb.Append(t.ToString("x2"));
            }
            return sb.ToString();
        }

        public string GenerateRandomString()
        {
            var charArr = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".ToCharArray();
            var randomString = string.Empty;
            var objRandom = new Random();
            for (int i = 0; i < 10; i++)
            {
                //Don't Allow Repetation of Characters
                int x = objRandom.Next(1, charArr.Length);
                if (!randomString.Contains(charArr.GetValue(x).ToString()))
                    randomString += charArr.GetValue(x);
                else
                    i--;
            }
            return randomString;
        }
    }
}
