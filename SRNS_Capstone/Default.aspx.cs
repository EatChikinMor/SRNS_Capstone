using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SQLiteDataHelpers;
using SQLiteDataHelpers.Objects;
using System.DirectoryServices;
using System.Web.Security;

namespace SRNS_Capstone
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                txtDomain.Text = ActiveDirectoryHelper.getCurrentDomain();
                lblDomain.Text = "Current Domain: <b>" + txtDomain.Text +"</b>";
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            //bool ua = ActiveDirectoryHelper.AuthenticeUser("","");

            DataTable user = getUserInformation(txtUsername.Text, txtPassword.Text);

            if (user.Rows.Count > 0)
            {
                User userSession = buildUser(user.Rows[0]);

                Session["User"] = userSession;
                
                Response.Redirect("~/Requests.aspx");
            }
            else
            {
                pnlError.Visible = true;
                //pnlFailedLogin.Update();
            }
            
            //Response.Redirect("~/Home.aspx");
        }

        private DataTable getUserInformation(string username, string password)
        {
            DataTable user = new DBConnector().getUserOnLogin(username, password);

            return user;
        }

        private User buildUser(DataRow row)
        {
            User user = new User() 
            { 
                ID = Convert.ToInt32(row["ID"].ToString()),
                LoginID = row["LoginID"].ToString(),
                FirstName = row["FirstName"].ToString(),
                LastName = row["LastName"].ToString(),
                IsAdmin = Convert.ToBoolean(Convert.ToInt32(row["IsAdmin"].ToString())),
                ManagerID = Convert.ToInt32(row["ManagerID"].ToString())
            };
            
            return user;
        }

        protected void pnlFailedLogin_Unload(object sender, EventArgs e)
        {
            MethodInfo methodInfo = typeof(ScriptManager).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
                .First(i => i.Name.Equals("System.Web.UI.IScriptManagerInternal.RegisterUpdatePanel"));
            methodInfo.Invoke(ScriptManager.GetCurrent(Page),
                new object[] { sender as UpdatePanel });
        }

        public bool AuthenticateUser(string domain, string username, string password, string LdapPath, out string Errmsg)
        {
            Errmsg = "";
            string domainAndUsername = domain + @"\" + username;
            DirectoryEntry entry = new DirectoryEntry(LdapPath, domainAndUsername, password);
            try
            {
                // Bind to the native AdsObject to force authentication.
                Object obj = entry.NativeObject;
                DirectorySearcher search = new DirectorySearcher(entry);
                search.Filter = "(SAMAccountName=" + username + ")";
                search.PropertiesToLoad.Add("cn");
                SearchResult result = search.FindOne();
                if (null == result)
                {
                    return false;
                }
                // Update the new path to the user in the directory
                LdapPath = result.Path;
                string _filterAttribute = (String)result.Properties["cn"][0];
            }
            catch (Exception ex)
            {
                Errmsg = ex.Message;
                return false;
                throw new Exception("Error authenticating user." + ex.Message);
            }
            return true;
        }

        protected void btnSubmitDomain_OnClick(object sender, EventArgs e)
        {
            new DBConnector().updateSettingValueByName("DOMAIN", txtDomain.Text);
            txtDomain.Text = ActiveDirectoryHelper.getCurrentDomain();
            lblDomain.Text = "Current Domain: <b>" + txtDomain.Text + "</b>";
            pnlError.Visible = false;
        }
    }
}