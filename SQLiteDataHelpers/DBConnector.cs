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
            const string SQL = "Select ID, FirstName, LastName, LoginID FROM Users ORDER BY ID DESC";

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

        #region Table UserAccess

        public DataTable getUserPrivileges(int UserID)
        {
            string SQL = "SELECT Requests, AddLicense, LicenseCountReport, AvailLicenseReport, ManagLicenseReport, LicenseExpReport, PendChargeReport FROM UserAccess WHERE UserID =" + UserID;

            DataTable dt = SQLiteDataHelper.GetDataTable(SQL);

            return dt;
        }

        #endregion

        #endregion

        #region INSERTS

        public string InsertUser(User user, UserAccess userA)
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
                retError = error + ",";
            }

            if (error.Length < 1)
            {
                userA.UserID = GetMaxId("Users");

                Dictionary<String, String> userAccess = SQLTables.TableColumns.UserAccess;

                userAccess["UserID"] = userA.UserID.ToString();
                userAccess["Requests"] = Convert.ToInt32(userA.Requests).ToString();
                userAccess["AddLicense"] = Convert.ToInt32(userA.AddLicense).ToString();
                userAccess["LicenseCountReport"] = Convert.ToInt32(userA.LicenseCountReport).ToString();
                userAccess["AvailLicenseReport"] = Convert.ToInt32(userA.AvailLicenseReport).ToString();
                userAccess["ManagLicenseReport"] = Convert.ToInt32(userA.ManagLicenseReport).ToString();
                userAccess["LicenseExpReport"] = Convert.ToInt32(userA.LicenseCountReport).ToString();
                userAccess["PendChargeReport"] = Convert.ToInt32(userA.PendChargeReport).ToString();

                error = "";

                return SQLiteDataHelper.Insert("UserAccess", userAccess, ref error)
                    ? "User successfully created"
                    : "User Access insert failed - " + retError + error +
                      "- try updating user access or deleting and recreating the user";
            }

            return "Insert into Database failed - " + error;
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

        public string UpdateUser(string UserID, User user, UserAccess userA)
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

            if (!SQLiteDataHelper.Update("USERS", Users, @where)) 
                return "User Update failed";

            userA.UserID = Convert.ToInt32(UserID);

            var userAccess = SQLTables.TableColumns.UserAccess;

            userAccess["UserID"] = userA.UserID.ToString();
            userAccess["Requests"] = Convert.ToInt32(userA.Requests).ToString();
            userAccess["AddLicense"] = Convert.ToInt32(userA.AddLicense).ToString();
            userAccess["LicenseCountReport"] = Convert.ToInt32(userA.LicenseCountReport).ToString();
            userAccess["AvailLicenseReport"] = Convert.ToInt32(userA.AvailLicenseReport).ToString();
            userAccess["ManagLicenseReport"] = Convert.ToInt32(userA.ManagLicenseReport).ToString();
            userAccess["LicenseExpReport"] = Convert.ToInt32(userA.LicenseCountReport).ToString();
            userAccess["PendChargeReport"] = Convert.ToInt32(userA.PendChargeReport).ToString();

            where = "UserID = " + UserID;

            return SQLiteDataHelper.Update("UserAccess", userAccess, @where) 
                ? "User successfully updated" 
                : "Access control update failed - User information successfully updated";
        }

        #endregion

        #region DELETES

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
