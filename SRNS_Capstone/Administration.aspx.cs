using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SQLiteDataHelpers.Objects;
using SQLiteDataHelpers;

namespace SRNS_Capstone
{
    public partial class Administration : System.Web.UI.Page
    {

        #region Private Members
        
        private int _userID
        {
            get
            {
                if (ViewState["userID"] == null)
                {
                    ViewState["userID"] = 0;
                }
                return (int)ViewState["userID"];
            }
            set
            {
                ViewState["IsAdmin"] = value;
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                User user = (User)Session["User"];

                //Remove before release
                if ("localhost" == Request.Url.DnsSafeHost)
                {
                    User a = new User() { ID = 0, FirstName = "Austin", LastName = "Rich", IsAdmin = true, LoginID = "arich", ManagerID = 0 };
                    user = a;
                }
                //Remove before release

                if (user != null)
                {
                    ((Capstone)Page.Master).showMenuOptions(user.IsAdmin, _userID);
                    _userID = user.ID;
                    ddlUsersPopulate();
                    ddlManagersPopulate();
                }
                else
                {
                    Response.Redirect("~/Default.aspx");
                }
            }
            
            hdnIsPostBack.Value = Page.IsPostBack.ToString();
            hdnIsManager.Value = managerTrue.Checked == true ? "true" : "false";
            hdnIsAdmin.Value = adminTrue.Checked == true ? "true" : "false";
        }

        protected void ddlUsersPopulate()
        {
            List<ComboboxItem> users = new DBConnector().populateUserList();

            for (int i = 0; i < users.Count; i++)
            {
                ListItem item = new ListItem(users[i].Text, users[i].Value);
                ddlUserSelect.Items.Add(item);
                //ddlUserSelect.Items[i].Text = users[i].Text;
                //ddlUserSelect.Items[i].Value = users[i].Value;
            }
        }

        protected void ddlManagersPopulate()
        {
            DataTable dt = new DBConnector().getManagers();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ListItem item = new ListItem((string)dt.Rows[i]["FirstName"] + " " + (string)dt.Rows[i]["LastName"], dt.Rows[i]["ID"].ToString());
                ddlManagers.Items.Add(item);
            }
        }

        protected void clearForm()
        {
            txtFirstName.Text = txtLastName.Text = txtLoginID.Text = txtPassword.Text = "";
            ddlManagers.SelectedIndex = 0;
            ddlUserSelect.SelectedIndex = 0;
        }

        protected void btnAddUser_Click(object sender, EventArgs e)
        {
            labelPasswordInfo.Visible = false;
            pnlSelection.Visible = false;
            pnlForm.Visible = true;
            pnlBtnAddUser.Visible = true;
            pnlBtnUpdateUser.Visible = false;

            clearForm();
        }

        protected void ddlUserSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataRow user = new DBConnector().getUserByID(Convert.ToInt32(ddlUserSelect.SelectedValue)).Rows[0];
            DataRow access = new DBConnector().getUserPrivileges(Convert.ToInt32(user["ID"].ToString())).Rows[0];

            txtFirstName.Text = user["FirstName"].ToString();
            txtLastName.Text = user["FirstName"].ToString();
            txtLoginID.Text = user["LoginID"].ToString();
            hdnIsAdmin.Value = Convert.ToInt32(user["IsAdmin"].ToString()) == 1 ? "true" : "false";
            hdnIsManager.Value = Convert.ToInt32(user["IsAdmin"].ToString()) == 1 ? "true" : "false";
            int manager = Convert.ToInt32(user["ManagerID"].ToString());
            if(manager > 0)
            {
                try
                {
                    ddlManagers.SelectedValue = manager.ToString();
                }
                catch(ArgumentOutOfRangeException)
                {
                    ddlManagers.SelectedIndex = 0;
                }
            }
            else
            {
                ddlManagers.SelectedIndex = 0;
            }

            chkRequests.Checked = (access["Requests"].ToString() == "1");
            chkAddLicense.Checked = (access["AddLicense"].ToString() == "1");
            chkLicenseCount.Checked = (access["LicenseCountReport"].ToString() == "1");
            chkAvailableLicense.Checked = (access["AvailLicenseReport"].ToString() == "1");
            chkManagerLicenseHolders.Checked = (access["ManagLicenseReport"].ToString() == "1");
            chkLicensesExpiring.Checked = (access["LicenseExpReport"].ToString() == "1");
            chkPendingChargebacks.Checked = (access["PendChargeReport"].ToString() == "1");

            labelPasswordInfo.Visible = true;
            pnlSelection.Visible = false;
            pnlForm.Visible = true;
            pnlBtnAddUser.Visible = false;
            pnlBtnUpdateUser.Visible = true;
        }

        protected void ddlManagers_SelectedIndexChanged(object sender, EventArgs e)
        {

            String something = ddlManagers.SelectedIndex.ToString();

            DBConnector a = new DBConnector();

            //txtFirstName.Text = a.getMaxID("Users").ToString() + " " + something;
        }

        protected void btnOne_Click(object sender, EventArgs e)
        {
            String something = ddlManagers.SelectedIndex.ToString();

            DBConnector a = new DBConnector();

            //txtFirstName.Text = a.getMaxID("Users").ToString() + " " + something;
        }

        protected void btnCancelNewUser_Click(object sender, EventArgs e)
        {
            txtFirstName.Text = txtLastName.Text = txtLoginID.Text = txtPassword.Text = "";
            hdnIsAdmin.Value = hdnIsManager.Value = "false";
            ddlManagers.SelectedIndex = 0;
            pnlForm.Visible = false;
            pnlSelection.Visible = true;
        }

        protected void btnNewUser_Click(object sender, EventArgs e)
        {
            //Validate
            User user = new User()
            {
                FirstName = txtFirstName.Text,
                LastName = txtLastName.Text,
                LoginID = txtLoginID.Text,
                IsAdmin = Convert.ToBoolean(hdnIsAdmin.Value),
                IsManager = Convert.ToBoolean(hdnIsManager.Value),
                ManagerID = Convert.ToInt32(ddlManagers.SelectedItem.Value),
                PassHash = txtPassword.Text, //Converted to hash on insert to DB
                Salt = "" //Added on insert
            };

            UserAccess access = new UserAccess()
            {
                Requests = chkRequests.Checked,
                AddLicense = chkAddLicense.Checked,
                AvailLicenseReport = chkAvailableLicense.Checked,
                LicenseCountReport = chkLicenseCount.Checked,
                LicenseExpReport = chkLicensesExpiring.Checked,
                ManagLicenseReport = chkManagerLicenseHolders.Checked,
                PendChargeReport = chkPendingChargebacks.Checked
            };

            string response = new DBConnector().InsertUser(user, access);

            if (response.Length > 25)
            {
                //Error
                lblError.Text = response;
                pnlError.Visible = true;
            }
            else
            {
                //Success
                clearForm();
                pnlForm.Visible = false;
                pnlSelection.Visible = true;
                pnlSuccess.Visible = true;
                lblSuccess.Text = response;
            }
        }

        protected void btnUpdateUser_Click(object sender, EventArgs e)
        {

        }
    }
}