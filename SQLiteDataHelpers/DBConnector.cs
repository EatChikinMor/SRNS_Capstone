using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.IO;
using System.Security.Cryptography;
using Microsoft.SqlServer.Server;
using SQLiteDataHelpers.Objects;

namespace SQLiteDataHelpers
{
    public class DBConnector
    {
        readonly SQLiteHelper SQLiteDataHelper = new SQLiteHelper();

        #region SELECTS ///////////////////////////////////////////////////////////////////////////////////////////////////////

        public int GetMaxId(string tableName)
        {
            string SQL = "SELECT MAX(ID) FROM " + tableName;

            int ID = Convert.ToInt32(SQLiteDataHelper.ExecuteScalar(SQL));

            return ID;
        }

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

        #region Current Speedcharts

        public bool doesSpeedchartExist(string speedchart)
        {
            string SQL = "SELECT COUNT(Speedchart) FROM CurrentSpeedcharts WHERE Speedchart = '" + speedchart + "'";

            int ret = Convert.ToInt32(SQLiteDataHelper.ExecuteScalar(SQL));

            return ret > 0;
        }

        #endregion

        #region Users

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

            SQL = "SELECT ID, FirstName, LastName, LoginID, ManagerID FROM Users WHERE LoginID = '" + username + "'" + " AND PassHash = '" + passHash + "'";
            //SQL = "SELECT ID, FirstName, LastName, IsAdmin, LoginID, ManagerID FROM Users WHERE LoginID = '" + username + "'" + " AND PassHash = '" + passHash + "'";

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

        /// <summary>
        /// Jessica added to populate Software names from database
        /// </summary>
        /// <returns></returns>
        public List<ComboboxItem> populateSoftwareList()
        {
            /// TODO: Alter OrderBy to Sort alphabetically 
            string SQL = "Select ID, SoftwareName FROM Software ORDER BY SoftwareName ASC";

            DataTable dt = SQLiteDataHelper.GetDataTable(SQL);

            List<ComboboxItem> software = new List<ComboboxItem>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ComboboxItem item = new ComboboxItem()
                {
                    Value = dt.Rows[i]["ID"].ToString(),
                    Text = (string)dt.Rows[i]["SoftwareName"]
                };

                software.Add(item);
            }

            return software;
        }

        public DataTable getManagers()
        {
            String SQL = "SELECT ID, FirstName, LastName FROM Users WHERE IsManager = 1";

            DataTable dt = SQLiteDataHelper.GetDataTable(SQL);

            return dt;
        }
        //Jessica - to get Software Names
        public DataTable getSoftwareName()
        {
            String SQL = "SELECT ID, SoftwareName FROM Software Where SoftwareName = 1";

            DataTable dt = SQLiteDataHelper.GetDataTable(SQL);

            return dt;
        }
        #endregion

        #region Software

        public DataTable getSoftwareById(string ID)
        {
            string SQL =
                "SELECT S.[ID], [SoftwareName], [Provider], [Organization] " +
                "FROM Software S " +
                "INNER JOIN Providers P ON Provider =  P.ID " +
                "WHERE S.ID = " + ID;

            return SQLiteDataHelper.GetDataTable(SQL);
        }

        public int GetSoftwareIdByName(string Name, string provider)
        {
            string SQL = "SELECT [ID] FROM Software WHERE [SoftwareName] = '" + Name + "' AND Provider = '" + provider + "'" ;

            var result = SQLiteDataHelper.ExecuteScalar(SQL);
            return String.IsNullOrEmpty(result) ? -1 : Convert.ToInt32(result);
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

                SQL = "SELECT [Provider] || '_' || S.[ID] AS [SoftCode], COALESCE([Organization], '[No Provider]') ||' '|| [SoftwareName] AS [SoftName], " +
                     "COALESCE(COUNT(L.SoftwareID), 0) AS [LicCount] " +
                     "FROM Software S " +
                     "LEFT JOIN Providers P ON Provider =  P.ID " +
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
                     "LEFT JOIN Providers P ON Provider =  P.ID " +
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
                "SELECT COALESCE(LK.KeyHolder, 'Not Assigned') AS Name, DATE([ExpirationDate]) AS ExpirationDate, [LicenseKey] " +
                //"SELECT COALESCE(LK.KeyHolder || ' - Login ID:' ||LK.HolderLoginID, 'Not Assigned') AS Name, DATE([ExpirationDate]) AS ExpirationDate, [LicenseKey] " +
                "FROM LicenseKeys LK " +
                "WHERE SoftwareID = "+ SoftwareId +"  " +
                andExpired +
                "ORDER BY KeyHolder, ExpirationDate";

            return SQLiteDataHelper.GetDataTable(SQL);
        }

        #endregion

        #region License Keys

        public DataTable getLicenseByKey(string Key)
        {
            string SQL =
                "SELECT [SoftwareName],[LicenseKey], [DateModified], [ExpirationDate], [KeyHolder],[KeyManager]," +
                "[HolderLoginID], [LicenseCost], [RequisitionNumber], [ChargebackComplete], [Organization], " +
                "[AssignmentStatus],[SpeedChart], [DateAssigned], [DateRemoved], [LicenseHolderCompany], " +
                "[Description], [Comments], [FileSubPath], [LastModifiedBy] " +
                "FROM LicenseKeys LK " +
                "INNER JOIN Software S ON [SoftwareID] = S.ID " +
                "INNER JOIN Providers P ON ProviderID = P.ID " +
                "WHERE [LicenseKey] = '" + Key + "'";

            return SQLiteDataHelper.GetDataTable(SQL);
        }

        public bool doesKeyExist(string Key)
        {
            string SQL = "SELECT COUNT(LicenseKey) FROM LicenseKeys WHERE LicenseKey = '" + Key + "'";

            return SQLiteDataHelper.ExecuteScalar(SQL) == "1";
        }

        public bool DoesLicenseExist(string LicenseKey, int SoftwareID)
        {
            string SQL = "SELECT Count(SoftwareID) FROM LicenseKeys WHERE SoftwareID = " + SoftwareID +
                         " AND LicenseKey = '" + LicenseKey + "'";
            var result = SQLiteDataHelper.ExecuteScalar(SQL);

            return result == "1";
        }

        #endregion

        #region Providers

        public int GetProviderIdByName(string name)
        {
            string SQL = "SELECT [ID] FROM Providers WHERE [Organization] = '"+ name +"'";

            var result = SQLiteDataHelper.ExecuteScalar(SQL);
            return String.IsNullOrEmpty(result) ? -1 : Convert.ToInt32(result);
        }

        public int GetProviderNameById(int id)
        {
            string SQL = "SELECT [Organization] FROM Providers WHERE [ID] = '"+ id +"'";

            var result = SQLiteDataHelper.ExecuteScalar(SQL);
            return result == "" ? -1 : Convert.ToInt32(result);
        }

        #endregion

        #region Reports

        public DataTable getAvailableLicensesReport(bool showProvider)
        {
            string orderBy = showProvider 
                ? "ORDER BY Provider, SoftwareName, ExpirationDate DESC" 
                : "ORDER BY SoftwareName, ExpirationDate DESC";

            string provider = showProvider
                ? "[Organization],"
                : "NULL AS [Organization],";

            string SQL =
                "SELECT " + provider + " S.[SoftwareName], LK.[LicenseKey], DATE(ExpirationDate) AS ExpirationDate, [SpeedChart] " +
                "FROM LicenseKeys LK " +
                "INNER JOIN Providers P ON S.Provider = P.ID " +
                "INNER JOIN Software S ON S.ID = LK.SoftwareID " +
                //"LEFT OUTER JOIN Speedcharts SC ON SC.KeyChargedAgainst = LK.[LicenseKey] " +
                "WHERE ExpirationDate > CURRENT_TIMESTAMP AND (LK.KeyHolder = '' OR LK.KeyHolder IS NULL) " +
                orderBy;

            DataTable result = SQLiteDataHelper.GetDataTable(SQL);

            return result;
        }

        public DataTable getExpiringLicensesReport(bool showProvider, int months = 3)
        {
            string orderBy = showProvider
                ? "ORDER BY Provider, SoftwareName, ExpirationDate DESC"
                : "ORDER BY SoftwareName, ExpirationDate DESC";

            string provider = showProvider
                ? "[Organization],"
                : "NULL AS [Organization],";

            string SQL =
                "SELECT " + provider + " S.[SoftwareName], LK.[LicenseKey], DATE(ExpirationDate) AS ExpirationDate , [SpeedChart] " +
                "FROM LicenseKeys LK " +
                "INNER JOIN Providers P ON S.Provider = P.ID " +
                "INNER JOIN Software S ON S.ID = LK.SoftwareID " +
                //"LEFT OUTER JOIN Speedcharts SC ON SC.KeyChargedAgainst = LK.[LicenseKey] " +
                "WHERE ExpirationDate > CURRENT_TIMESTAMP " +
                "AND ExpirationDate < DATE(CURRENT_TIMESTAMP, '+"+ months +" month')" +
                orderBy;

            DataTable result = SQLiteDataHelper.GetDataTable(SQL);

            return result;
        }

        public DataTable getExpiredLicenses(bool showProvider)
        {
            string orderBy = showProvider
                ? "ORDER BY Provider, SoftwareName, ExpirationDate DESC"
                : "ORDER BY SoftwareName, ExpirationDate DESC";

            string provider = showProvider
                ? "[Organization],"
                : "NULL AS [Organization],";

            string SQL =
                "SELECT " + provider + " S.[SoftwareName], LK.[LicenseKey], DATE(ExpirationDate) AS ExpirationDate, [SpeedChart] " +
                "FROM LicenseKeys LK " +
                "INNER JOIN Providers P ON S.Provider = P.ID " +
                "INNER JOIN Software S ON S.ID = LK.SoftwareID " +
                //"LEFT OUTER JOIN Speedcharts SC ON SC.KeyChargedAgainst = LK.[LicenseKey] " +
                "WHERE ExpirationDate < CURRENT_TIMESTAMP " +
                orderBy;

            DataTable result = SQLiteDataHelper.GetDataTable(SQL);

            return result;
        }

        public DataTable getManagersLicenseHolders(string Manager)
        {
            string SQL = "SELECT KeyHolder, SoftwareName, DATE(ExpirationDate) AS ExpirationDate, Speedchart " +
                         "FROM LicenseKeys " +
                         "INNER JOIN Software S ON S.ID = SoftwareID " +
                         //"LEFT OUTER JOIN Speedcharts ON SpeedChartID = SpeedChart " +
                         "WHERE KeyManager = '" + Manager + "' ORDER BY KeyHolder";

            return SQLiteDataHelper.GetDataTable(SQL);
        }

        public DataTable getMyLicensesReport(string keyHolder, string loginID, bool showProvider)
        {
            string orderBy = showProvider
                ? "ORDER BY Provider, SoftwareName, ExpirationDate DESC"
                : "ORDER BY SoftwareName, ExpirationDate DESC";

            string provider = showProvider
                ? "[Organization],"
                : "NULL AS [Organization],";

            string SQL =
                "SELECT " + provider + " [SoftwareName], [LicenseKey], DATE(ExpirationDate) AS ExpirationDate " +
                "FROM LicenseKeys " +
                "INNER JOIN Providers P ON S.Provider = P.ID " +
                "INNER JOIN Software S ON S.ID = SoftwareID " +
                "WHERE KeyHolder = '" + keyHolder + "' AND HolderLoginID = '"+ loginID +"' " +
                orderBy;

            return SQLiteDataHelper.GetDataTable(SQL);
        }

        public DataTable getPendingChargebackReport()
        {
            string SQL =
                "SELECT [SpeedChart], [SoftwareName], [LicenseKey], [KeyHolder], [KeyManager], [LicenseCost], [Organization] " +
                "FROM LicenseKeys LK " +
                "INNER JOIN Software S ON [SoftwareID] = S.ID " +
                "INNER JOIN Providers P ON ProviderID = P.ID " +
                "WHERE [ChargebackComplete] = 0 " +
                "ORDER BY SpeedChart";

            DataTable result = SQLiteDataHelper.GetDataTable(SQL);

            return result;
        }

        #endregion

        #region Requests

        public DataTable getPendingRequests(string User = "", string Name = "", bool all = true)
        {
            string WHERE = all ? "" : " WHERE RequestingUser = '" + Name + "' AND RequestingUserLogin = '" + User + "'";

            string SQL =
                "SELECT [ID], [RequestTitle], [Request], DATE([RequestDate]) AS RequestDate, [RequestingUser], [RequestingUserLogin], 'Delete' AS [Delete] " +
                "FROM Requests "
                + WHERE;

            return SQLiteDataHelper.GetDataTable(SQL);
        }

        #endregion

        #region Table Settings

        public string getSettingValueByName(string settingName)
        {
            var SQL = "SELECT [VALUE] FROM [Settings] WHERE [Setting] = '" + settingName + "'";

            return SQLiteDataHelper.ExecuteScalar(SQL);
        }

        public string getSettingValueByID(string settingId)
        {
            var SQL = "SELECT [VALUE] FROM [Settings] WHERE [ID] = '" + settingId + "'";

            return SQLiteDataHelper.ExecuteScalar(SQL);
        }

        #endregion

        #endregion ///////////////////////////////////////////////////////////////////////////////////////////////////////

        #region INSERTS ///////////////////////////////////////////////////////////////////////////////////////////////////////

        #region Current Speedcharts

        public bool insertSpeedchart(string speedchart, ref string error)
        {
            if (!doesSpeedchartExist(speedchart))
            {
                var DictSpeedchart = SQLTables.TableColumns.CurrentSpeedcharts;

                DictSpeedchart["Speedchart"] = speedchart;
                return SQLiteDataHelper.Insert("CurrentSpeedcharts", DictSpeedchart, ref error);
            }

            error = "Duplicate Speedchart exists";
            return false;
        }

        #endregion

        #region License Keys

        public string InsertLicense(LicenseKey LK)
        {
            if (DoesLicenseExist(LK.Key, LK.SoftwareId)) 
                return "License Key for specified software already exists";

            var License = SQLTables.TableColumns.LicenseKeys;

            License["SoftwareID"] = LK.SoftwareId.ToString();
            License["LicenseKey"] = LK.Key;
            License["DateModified"] = LK.DateUpdated.ToString("s");
            License["ExpirationDate"] = LK.DateExpiring.ToString("s");
            License["KeyHolder"] = LK.Holder;
            License["KeyManager"] = LK.Manager;
            License["HolderLoginID"] = LK.HolderID;
            License["LicenseCost"] = String.Format("{0:0.00}",LK.LicenseCost);
            License["RequisitionNumber"] = LK.RequisitionNumber;
            License["ChargebackComplete"] = Convert.ToInt32(LK.ChargebackComplete).ToString();
            License["ProviderID"] = LK.Provider.ToString();
            License["AssignmentStatus"] = LK.Assignment.ToString();
            License["SpeedChart"] = LK.Speedchart;
            License["DateAssigned"] = LK.DateAssigned.ToString("s");
            License["DateRemoved"] = LK.DateRemoved.ToString("s");
            License["LicenseHolderCompany"] = LK.LicenseHolderCompany.ToString();
            License["Description"] = LK.Description;
            License["Comments"] = LK.Comments;
            License["Comments"] = LK.Comments;
            License["FileSubpath"] = LK.fileSubpath == Guid.Empty ? "" : LK.fileSubpath.ToString();
            License["LastModifiedBy"] = LK.LastModifiedBy;

            string error = "";

            return SQLiteDataHelper.Insert("LicenseKeys", License, ref error)
                ? "License \"" + LK.Key + "\" Inserted Successfully"
                : error;

        }

        #endregion

        #region Providers

        public string insertProvider(string name, ref bool success)
        {
            Dictionary<String, String> provider = SQLTables.TableColumns.Providers;
            provider["Organization"] = name;

            string error = "";
            if (!SQLiteDataHelper.Insert("Providers", provider, ref error))
            {
                success = false;
                return error;
            }

            success = true;
            return GetMaxId("Providers").ToString();
        }

        #endregion

        #region Requests

        public string insertRequest(Request req, ref bool success)
        {
            var Request = SQLTables.TableColumns.Requests;

            Request["RequestTitle"] = req.RequestTitle;
            Request["Request"] = req.RequestContent;
            Request["RequestDate"] = req.RequestDate.ToString("s");
            Request["RequestingUser"] = req.Name;
            Request["RequestingUserLogin"] = req.LoginID;

            string error = "";

            success = SQLiteDataHelper.Insert("Requests", Request, ref error);

            return success ? "Request submitted successfully!" : error;
        }

        #endregion

        #region Software

        public string insertSoftware(string name, int Provider, ref bool success)
        {
            Dictionary<String, String> software = SQLTables.TableColumns.Software;
            software["SoftwareName"] = name;
            software["Provider"] = Provider.ToString();

            string error = "";
            if (!SQLiteDataHelper.Insert("Software", software, ref error))
            {
                success = false;
                return error;
            }

            success = true;
            return GetMaxId("Software").ToString();
        }

        #endregion

        #region Users

        public string InsertUser(User user)
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
            //-----------------------------------------\\

            if (!IsUsernameAvailable(user.LoginID)) return "The chosen username already exists.";

            user.Salt = GenerateRandomString();

            user.PassHash = GenerateMd5Hash(user.Salt + user.PassHash);

            Dictionary<String, String> Users = SQLTables.TableColumns.Users;
            Users["FirstName"] = user.FirstName;
            Users["LastName"] = user.LastName;
            //Users["IsAdmin"] = Convert.ToInt32(user.IsAdmin).ToString();
            Users["LoginID"] = user.LoginID;
            Users["PassHash"] = user.PassHash;
            //Users["ManagerID"] = Convert.ToInt32(user.ManagerID).ToString();
            Users["Salt"] = user.Salt;
            //Users["IsManager"] = Convert.ToInt32(user.IsManager).ToString();

            string error = "", retError = "";
            return !SQLiteDataHelper.Insert("USERS", Users, ref error) ? error : "User successfully created";
        }

        #endregion

        #endregion ///////////////////////////////////////////////////////////////////////////////////////////////////////

        #region UPDATES ///////////////////////////////////////////////////////////////////////////////////////////////////////

        public string UpdateUser(string UserID, User user)
        {
            if (!IsUsernameAvailable(user.LoginID) && !IsThisTheCurrentUsername(user.LoginID, UserID))
                return "The chosen username already exists.";

            string SQL = "SELECT Salt FROM USERS WHERE LoginID = '" + user.LoginID + "'";

            user.Salt = SQLiteDataHelper.ExecuteScalar(SQL);

            user.PassHash = String.IsNullOrEmpty(user.PassHash) ? "" : GenerateMd5Hash(user.Salt + user.PassHash);

            var Users = SQLTables.TableColumns.Users;
            Users.Remove("Salt");
            Users["FirstName"] = user.FirstName;
            Users["LastName"] = user.LastName;
            //Users["IsAdmin"] = user.IsAdmin.ToString();
            Users["LoginID"] = user.LoginID;
            if (String.IsNullOrEmpty(user.PassHash))
            {
                Users.Remove("PassHash");
            }
            else
            {
                Users["PassHash"] = user.PassHash;
            }
            Users["ManagerID"] = user.ManagerID.ToString();
            //Users["IsManager"] = user.IsManager.ToString();

            string where = "ID = " + UserID;

            return !SQLiteDataHelper.Update("USERS", Users, @where) 
                ? "User Update failed" 
                : "User successfully updated";
        }

        #region Settings

        public void updateSettingValueByName(string settingName,string settingValue)
        {
            var SQL = "UPDATE Settings SET VALUE = '" + settingValue + "' WHERE Setting = '" + settingName + "'";

            SQLiteDataHelper.ExecuteNonQuery(SQL);
        }

        #endregion

        #region License Keys

        public bool UpdateLicenseKey(LicenseKey LK)
        {
            var License = SQLTables.TableColumns.LicenseKeys;

            License["SoftwareID"] = LK.SoftwareId.ToString();
            License["LicenseKey"] = LK.Key;
            License["DateModified"] = LK.DateUpdated.ToString("s");
            License["ExpirationDate"] = LK.DateExpiring.ToString("s");
            License["KeyHolder"] = LK.Holder;
            License["KeyManager"] = LK.Manager;
            License["HolderLoginID"] = LK.HolderID;
            License["LicenseCost"] = String.Format("{0:0.00}", LK.LicenseCost);
            License["RequisitionNumber"] = LK.RequisitionNumber;
            License["ChargebackComplete"] = Convert.ToInt32(LK.ChargebackComplete).ToString();
            License["ProviderID"] = LK.Provider.ToString();
            License["AssignmentStatus"] = LK.Assignment.ToString();
            License["SpeedChart"] = LK.Speedchart;
            License["DateAssigned"] = LK.DateAssigned.ToString("s");
            License["DateRemoved"] = LK.DateRemoved.ToString("s");
            License["LicenseHolderCompany"] = LK.LicenseHolderCompany.ToString();
            License["Description"] = LK.Description;
            License["Comments"] = LK.Comments;
            License["Comments"] = LK.Comments;
            License["FileSubpath"] = LK.fileSubpath == Guid.Empty ? "" : LK.fileSubpath.ToString();
            License["LastModifiedBy"] = LK.LastModifiedBy;

            string WHERE = "LicenseKey = '" + LK.Key + "'";

            return SQLiteDataHelper.Update("LicenseKeys", License, WHERE);
        }

        public string ClearFileAssoc(string Key)
        {
            string SQL = 
                "UPDATE LicenseKeys SET FileSubpath = NULL WHERE LicenseKey = '" + Key + "'";

            return SQLiteDataHelper.ExecuteScalar(SQL);
        }

        #endregion

        #region Providers

        public bool updateSoftwareProvider(int softwareID, int newProvider)
        {
            string SQL = "UPDATE Software SET Provider = " + newProvider + " WHERE ID = " + softwareID;

            return SQLiteDataHelper.ExecuteNonQuery(SQL) > 0;
        }

        #endregion

        #endregion ///////////////////////////////////////////////////////////////////////////////////////////////////////

        #region DELETES ///////////////////////////////////////////////////////////////////////////////////////////////////////

        #region CurrentSpeedcharts

        public bool DeleteSpeedcharts(string speedchart = "", bool all = true)
        {
            string WHERE = all ? "Speedchart IS NOT NULL" : "Speedchart = '" + speedchart + "'";
            return SQLiteDataHelper.Delete("CurrentSpeedcharts", WHERE);
        }

        #endregion

        #region Users

        public bool DeleteUser(string UserId)
        {
            string whereUsers = "ID = " + UserId;
            string whereUserA = "UserID = " + UserId;
            int ret = 0;

            return SQLiteDataHelper.Delete("Users", whereUsers);
        }

        #endregion

        #region License Keys

        public bool DeleteKey(string Key)
        {
            string WHERE = "LicenseKey = '" + Key + "'";

            return SQLiteDataHelper.Delete("LicenseKeys", WHERE);
        }

        public bool DeleteAllKeysBySoftID(string SoftID)
        {
            string SQL = "SELECT [FileSubpath] FROM LicenseKeys WHERE SoftwareID = '" + SoftID + "'";

            DataTable dt = SQLiteDataHelper.GetDataTable(SQL);

            foreach (DataRow row in dt.Rows)
            {
                string subpath = row["FileSubpath"].ToString();

                if (!String.IsNullOrEmpty(subpath))
                {
                    try
                    {
                        DeleteAttachment(subpath);
                    }
                    catch
                    {
                        // ignored
                    }
                }

            }

            string WHERE = "SoftwareID = '" + SoftID +"'";

            return SQLiteDataHelper.Delete("LicenseKeys", WHERE);
        }
        #endregion

        #region Providers

        public bool DeleteProviderByID(string ID)
        {
            string WHERE = "ID = '" + ID + "'";

            return SQLiteDataHelper.Delete("Providers", WHERE);
        }

        #endregion

        #region Requests

        public bool DeleteRequest(string ID)
        {
            string WHERE = "ID = '" + ID + "'";

            return SQLiteDataHelper.Delete("Requests", WHERE);
        }

        #endregion

        #region Software

        public bool DeleteSoftwareByID(string ID, ref string error)
        {
            if (DeleteAllKeysBySoftID(ID))
            {
                string SQL = "SELECT [Provider] FROM Software WHERE ID = '" + ID + "'";
                string provider = SQLiteDataHelper.ExecuteScalar(SQL);
                string WHERE = "ID = '" + ID + "'";

                bool success = (SQLiteDataHelper.Delete("Software", WHERE));

                if (success)
                {
                    SQL = "SELECT COUNT([ID]) FROM Software WHERE Provider = '" + provider + "'";
                    if (SQLiteDataHelper.ExecuteScalar(SQL) == "0")
                    {
                        DeleteProviderByID(provider);
                    }
                }

                return success;
            }
            else
            {
                error = "Associated keys could not be deleted";
                return false;
            }
        }

        #endregion

        #endregion ///////////////////////////////////////////////////////////////////////////////////////////////////////

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

        public void DeleteAttachment(string guidPath)
        {
            string directoryPath = AppDomain.CurrentDomain.BaseDirectory + "attachmentsDirectory\\";

            string path = Directory.GetFiles(directoryPath + guidPath).First();

            File.Delete(path);

            Directory.Delete(directoryPath + guidPath);
        }
    }
}
