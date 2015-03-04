using System;
using System.Collections.Generic;
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

        public string InsertUser(User user)
        {
            //////////// Manually Add user \\\\\\\\\\\\
            User a = new User();
            a.FirstName = "Austin";
            a.LastName = "Rich";
            a.IsAdmin = true;
            a.LoginID = "arich";
            a.PassHash = "123456a";
            a.ManagerID = 1;
            a.Salt = GenerateRandomString();

            user = a;
            //-----------------------------------------

            if (!DoesUsernameExist(user.LoginID))
            {
                using (MD5 md5Hash = MD5.Create())
                {
                    user.PassHash = GenerateMd5Hash(md5Hash, user.Salt + user.PassHash);
                }
                //StringBuilder sql = new StringBuilder();

                //sql.Append(SQL.insertInto("USERS", SQL.TableColumns.Users, true));
                //sql.Append(
                //    " '"+ user.FirstName + "'," +
                //    " '"+ user.LastName + "'," +
                //     Convert.ToInt32(user.IsAdmin) + "," +
                //    " '"+ user.LoginID + "'," +
                //    " '" + user.PassHash + "'," +
                //     user.ManagerID + "," +
                //    " '"+ user.Salt + "'"
                //    );
                //sql.Append(")");

                Dictionary<String, String> Users = SQL.TableColumns.Users;

                Users["FirstName"] = user.FirstName;
                Users["LastName"] = user.LastName;
                Users["IsAdmin"] = user.IsAdmin.ToString();
                Users["LoginID"] = user.LoginID;
                Users["PassHash"] = user.PassHash;
                Users["ManagerID"] = user.ManagerID.ToString();
                Users["Salt"] = user.Salt;

                string Error = "";
                if (SQLiteDataHelper.Insert("USERS", Users, ref Error))
                {
                    return "User successfully created";
                }
                else
                {
                    return "Database insert failed - " + Error;
                }
            }
            else
            {
                return "Username already exists.";
            }
        }

        public bool DoesUsernameExist(string username)
        {
            String SQL = "SELECT COUNT(LoginID) FROM Users WHERE LoginID = '" + username + "'";

            string result = SQLiteDataHelper.ExecuteScalar(SQL);

            return Convert.ToBoolean(Convert.ToInt32(result));
        }

        public bool validateUser(string username, string password)
        {
            string SQL = "SELECT Salt FROM USERS WHERE LoginID = '" + username + "'";

            string Salt = SQLiteDataHelper.ExecuteScalar(SQL);



            return true;
        }

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
