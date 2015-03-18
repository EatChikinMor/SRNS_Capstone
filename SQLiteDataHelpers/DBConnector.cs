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
        SQLiteHelper SQLiteDataHelper = new SQLiteHelper();

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

        public bool DoesUsernameExist(string username)
        {
            String SQL = "SELECT COUNT(LoginID) FROM Users WHERE LoginID = '" + username + "'";

            string result = SQLiteDataHelper.ExecuteScalar(SQL);

            return Convert.ToBoolean(Convert.ToInt32(result));
        }

        public DataTable getUserOnLogin(string username, string password)
        {
            string SQL = "SELECT Salt FROM USERS WHERE LoginID = '" + username + "'";

            string Salt = SQLiteDataHelper.ExecuteScalar(SQL);

            string passHash;

            using (MD5 md5Hash = MD5.Create())
            {
                passHash = GenerateMd5Hash(md5Hash, Salt + password);
            }

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

        public DataTable getUserPrivileges(int UserID)
        {
            string SQL = "SELECT Requests, AddLicense, LicenseCountReport, AvailLicenseReport, ManagLicenseReport, LicenseExpReport, PendChargeReport FROM UserAccess WHERE UserID =" + UserID;

            DataTable dt = SQLiteDataHelper.GetDataTable(SQL);

            return dt;
        }

        public List<ComboboxItem> populateUserList()
        {
            /// TODO: Alter OrderBy to Sort alphabetically by First, Last name
            string SQL = "Select ID, FirstName, LastName, LoginID FROM Users ORDER BY ID DESC";

            DataTable dt = SQLiteDataHelper.GetDataTable(SQL);

            List<ComboboxItem> users = new List<ComboboxItem>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ComboboxItem item = new ComboboxItem()
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

        #endregion

        #region INSERTS

        public string InsertUser(User user, UserAccess userA, Boolean updateUser) //Change to Upsert
        {
            if (updateUser)
            {
                //Update goes here
                return "failed update";
            }
            else
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
                //-----------------------------------------

                if (!DoesUsernameExist(user.LoginID))
                {
                    user.Salt = GenerateRandomString();

                    using (MD5 md5Hash = MD5.Create())
                    {
                        user.PassHash = GenerateMd5Hash(md5Hash, user.Salt + user.PassHash);
                    }

                    Dictionary<String, String> Users = SQLTables.TableColumns.Users;
                    Users["FirstName"] = user.FirstName;
                    Users["LastName"] = user.LastName;
                    Users["IsAdmin"] = user.IsAdmin.ToString();
                    Users["LoginID"] = user.LoginID;
                    Users["PassHash"] = user.PassHash;
                    Users["ManagerID"] = user.ManagerID.ToString();
                    Users["Salt"] = user.Salt;
                    Users["IsManager"] = user.IsManager.ToString();

                    string Error = "", retError = "";
                    if (!SQLiteDataHelper.Insert("USERS", Users, ref Error))
                    {
                        retError = Error;
                    }

                    if (Error.Length < 1)
                    {
                        userA.UserID = getMaxID("Users");

                        Dictionary<String, String> userAccess = SQLTables.TableColumns.UserAccess;

                        userAccess["UserID"] = userA.UserID.ToString();
                        userAccess["Requests"] = userA.Requests.ToString();
                        userAccess["AddLicense"] = userA.AddLicense.ToString();
                        userAccess["LicenseCountReport"] = userA.LicenseCountReport.ToString();
                        userAccess["AvailLicenseReport"] = userA.AvailLicenseReport.ToString();
                        userAccess["ManagLicenseReport"] = userA.ManagLicenseReport.ToString();
                        userAccess["LicenseExpReport"] = userA.LicenseCountReport.ToString();
                        userAccess["PendChargeReport"] = userA.PendChargeReport.ToString();

                        Error = "";
                        if (SQLiteDataHelper.Insert("UserAccess", Users, ref Error))
                        {
                            return "User successfully created";
                        }
                        else
                        {
                            return "User Access insert failed - " + Error + "- try updating user access or deleting and recreating the user";
                        }
                    }
                    else
                    {
                        return "Insert into Database failed - " + Error;
                    }
                }
                else
                {
                    return "The chosen username already exists.";
                }
            }
        }

        public int getMaxID(string tableName)
        {
            string SQL = "SELECT MAX(ID) FROM " + tableName;

            int ID = Convert.ToInt32(SQLiteDataHelper.ExecuteScalar(SQL));

            return ID;
        }

        #endregion

        #region UPDATES

        #endregion

        #region DELETES

        #endregion

        static string GenerateMd5Hash(MD5 md5Hash, string input)
        {
            // Convert the input string to a byte array and compute the hash. 
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes 
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data  
            // and format each one as a hexadecimal string. 
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string. 
            return sBuilder.ToString();
        }

        public string GenerateRandomString()
        {
            char[] charArr = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".ToCharArray();
            string randomString = string.Empty;
            Random objRandom = new Random();
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
