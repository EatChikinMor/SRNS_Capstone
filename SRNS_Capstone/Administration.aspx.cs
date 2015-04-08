using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using SQLiteDataHelpers.Objects;
using SQLiteDataHelpers;
using System.Text.RegularExpressions;

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
                ViewState["userID"] = value;
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                User user = (User)Session["User"];

                //Remove before release
                //if ("localhost" == Request.Url.DnsSafeHost)
                //{
                //    User a = new User() { ID = 0, FirstName = "Austin", LastName = "Rich", IsAdmin = true, LoginID = "arich", ManagerID = 0 };
                //    user = a;
                //}
                //Remove before release

                if (user != null)
                {
                    ((Capstone)Page.Master).showMenuOptions(user.IsAdmin);
                    //_userID = user.ID;
                    ddlUsersPopulate();
                    ddlManagersPopulate();
                }
                else
                {
                    Response.Redirect("~/Default.aspx");
                }
            }


            //hdnIsPostBack.Value = Page.IsPostBack.ToString();
            //hdnIsManager.Value = rdManagerTrue.Checked ? "true" : "false";
            //hdnIsAdmin.Value = rdAdminTrue.Checked  ? "true" : "false";
        }

        protected void ddlUsersPopulate()
        {
            ddlUserSelect.Items.Clear();

            ddlUserSelect.Items.Add(new ListItem("",""));
            List<ComboboxItem> users = new DBConnector().populateUserList();

            foreach (ListItem item in users.Select(t => new ListItem(t.Text, t.Value)))
            {
                ddlUserSelect.Items.Add(item);
            }
        }

        protected void ddlManagersPopulate()
        {
            ddlManagers.Items.Clear();
            DataTable dt = new DBConnector().getManagers();
            ddlManagers.Items.Add(new ListItem("", ""));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ListItem item = new ListItem((string)dt.Rows[i]["FirstName"] + " " + (string)dt.Rows[i]["LastName"], dt.Rows[i]["ID"].ToString());
                ddlManagers.Items.Add(item);
            }
        }


        protected void clearForm()
        {
            txtFirstName.Text = txtLastName.Text = txtLoginID.Text = txtPassword.Text = "";
            //rdManagerTrue.Checked = rdManagerFalse.Checked = false;
            //rdAdminFalse.Checked = rdAdminTrue.Checked = false;
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
            pnlError.Visible = false;

            clearForm();
        }

        protected void ddlUserSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataRow user = new DBConnector().getUserByID(Convert.ToInt32(ddlUserSelect.SelectedValue)).Rows[0];
            //DataRow access = new DBConnector().getUserPrivileges(Convert.ToInt32(user["ID"].ToString())).Rows[0];

            int IsAdmin = Convert.ToInt32(user["IsAdmin"].ToString());
            int IsManager = Convert.ToInt32(user["IsManager"].ToString());
            hdnUserToUpdate.Value = ddlUserSelect.SelectedValue;

            txtFirstName.Text = user["FirstName"].ToString();
            txtLastName.Text = user["LastName"].ToString();
            txtLoginID.Text = user["LoginID"].ToString();
            //rdAdminTrue.Checked = IsAdmin == 1;
            //rdAdminFalse.Checked = IsAdmin != 1;
            //rdManagerTrue.Checked = IsManager == 1;
            //rdManagerFalse.Checked = IsManager != 1;
            hdnIsAdmin.Value = IsAdmin == 1 ? "true" : "false";
            hdnIsManager.Value = IsManager == 1 ? "true" : "false";
            int manager = Convert.ToInt32(user["ManagerID"].ToString());
            if (manager > 0)
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

            //chkRequests.Checked = (access["Requests"].ToString() == "1");
            //chkAddLicense.Checked = (access["AddLicense"].ToString() == "1");
            //chkLicenseCount.Checked = (access["LicenseCountReport"].ToString() == "1");
            //chkAvailableLicense.Checked = (access["AvailLicenseReport"].ToString() == "1");
            //chkManagerLicenseHolders.Checked = (access["ManagLicenseReport"].ToString() == "1");
            //chkLicensesExpiring.Checked = (access["LicenseExpReport"].ToString() == "1");
            //chkPendingChargebacks.Checked = (access["PendChargeReport"].ToString() == "1");

            labelPasswordInfo.Visible = true;
            pnlSelection.Visible = false;
            pnlForm.Visible = true;
            pnlBtnAddUser.Visible = false;
            pnlBtnUpdateUser.Visible = true;
            pnlError.Visible = false;
        }

        protected void ddlManagers_SelectedIndexChanged(object sender, EventArgs e)
        {

            //String something = ddlManagers.SelectedIndex.ToString();

            //DBConnector a = new DBConnector();

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
            var errors = ValidateInput();

            if (errors.Count == 0)
            {
                User user = new User()
                {
                    FirstName = txtFirstName.Text,
                    LastName = txtLastName.Text,
                    LoginID = txtLoginID.Text,
                    //IsAdmin = rdAdminTrue.Checked,
                    //IsManager = rdManagerTrue.Checked,
                    ManagerID = rdManagerTrue.Checked ? 0 : Convert.ToInt32(ddlManagers.SelectedItem.Value),
                    PassHash = txtPassword.Text //Converted to hash on insert to DB
                };

                //UserAccess access = new UserAccess()
                //{
                //    Requests = chkRequests.Checked || rdAdminTrue.Checked,
                //    AddLicense = chkAddLicense.Checked || rdAdminTrue.Checked,
                //    AvailLicenseReport = chkAvailableLicense.Checked || rdAdminTrue.Checked,
                //    LicenseCountReport = chkLicenseCount.Checked || rdAdminTrue.Checked,
                //    LicenseExpReport = chkLicensesExpiring.Checked || rdAdminTrue.Checked,
                //    ManagLicenseReport = chkManagerLicenseHolders.Checked || rdAdminTrue.Checked,
                //    PendChargeReport = chkPendingChargebacks.Checked || rdAdminTrue.Checked
                //};

                string response = new DBConnector().InsertUser(user);

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
                    ddlUsersPopulate();
                }
            }
            else
            {
                lblError.Visible = true;
                lblError.ForeColor = Color.Red;

                var E  = new StringBuilder();

                foreach (var error in errors)
                {
                    E.Append(error);
                }

                lblError.Text = "Input Required for the following fields:" + E;
            }
        }

        protected void btnUpdateUser_Click(object sender, EventArgs e)
        {
            var errors = ValidateInput(true);

            if (errors.Count == 0)
            {
                User user = new User()
                {
                    FirstName = txtFirstName.Text,
                    LastName = txtLastName.Text,
                    LoginID = txtLoginID.Text,
                    IsAdmin = rdAdminTrue.Checked,
                    IsManager = rdManagerTrue.Checked,
                    ManagerID = Convert.ToInt32(ddlManagers.SelectedItem.Value),
                    PassHash = txtPassword.Text //Converted to hash on insert to DB
                };

                //UserAccess access = new UserAccess()
                //{
                //    Requests = chkRequests.Checked,
                //    AddLicense = chkAddLicense.Checked,
                //    AvailLicenseReport = chkAvailableLicense.Checked,
                //    LicenseCountReport = chkLicenseCount.Checked,
                //    LicenseExpReport = chkLicensesExpiring.Checked,
                //    ManagLicenseReport = chkManagerLicenseHolders.Checked,
                //    PendChargeReport = chkPendingChargebacks.Checked
                //};

                string response = new DBConnector().UpdateUser(ddlUserSelect.SelectedValue, user);

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
            else
            {
                lblError.Visible = true;
                lblError.ForeColor = Color.Red;

                var E  = new StringBuilder();

                foreach (var error in errors)
                {
                    E.Append(error);
                }

                lblError.Text = "Input Required for the following fields:" + E;
                pnlError.Visible = true;
            }
        }

        private List<String> ValidateEmptyInput(ref bool[] valid, bool update = false)
        {
            pnlError.Visible = false;

            var errors = new List<string>();

            if (String.IsNullOrEmpty(txtFirstName.Text))
            {
                txtFirstName.BackColor = ColorTranslator.FromHtml("#FFD8D8");
                lblFirstName.ForeColor = Color.Red;
                errors.Add("First Name Empty");
                valid[0] = false;
            }
            if (String.IsNullOrEmpty(txtLastName.Text))
            {
                txtLastName.BackColor = ColorTranslator.FromHtml("#FFD8D8");
                lblLastName.ForeColor = Color.Red;
                errors.Add(errors.Count > 0 ? ", Last Name Empty" : "Last Name Empty");
                valid[1] = false;
            }
            if (String.IsNullOrEmpty(txtLoginID.Text))
            {
                txtLoginID.BackColor = ColorTranslator.FromHtml("#FFD8D8");
                lblUsername.ForeColor = Color.Red;
                errors.Add(errors.Count > 0 ? ", Username Empty" : "Username Empty");
                valid[2] = false;
            }
            if (String.IsNullOrEmpty(txtPassword.Text) && !update)
            {
                txtPassword.BackColor = ColorTranslator.FromHtml("#FFD8D8");
                lblPassword.ForeColor = Color.Red;
                errors.Add(errors.Count > 0 ? ", Password Empty" : "Password Empty");
            }
            if (ddlManagers.SelectedIndex == 0 && !Convert.ToBoolean(hdnIsManager.Value) && !update)
            {
                ddlManagers.BackColor = ColorTranslator.FromHtml("#FFD8D8");
                lblManager.ForeColor = Color.Red;
                errors.Add(errors.Count > 0 ? ", Select a Manager" : "Select a Manager");
            }

            pnlError.Visible = true;

            return errors;
        }

        private List<String> ValidateInput(bool update = false)
        {
            var valid = new [] {true, true, true};
            var errors = ValidateEmptyInput(ref valid, update);
            if (valid[0] && !IsAlpha(txtFirstName.Text))
            {
                txtFirstName.BackColor = ColorTranslator.FromHtml("#FFD8D8");
                lblFirstName.ForeColor = Color.Red;
                errors.Add("First Name Invalid");
            }
            if (valid[1] && !IsAlpha((txtLastName.Text)))
            {
                txtLastName.BackColor = ColorTranslator.FromHtml("#FFD8D8");
                lblLastName.ForeColor = Color.Red;
                errors.Add(errors.Count > 0 ? ", Last Name Invalid" : "Last Name Invalid");
            }
            if (valid[2] && !IsAlphaNumeric(txtLoginID.Text))
            {
                txtLoginID.BackColor = ColorTranslator.FromHtml("#FFD8D8");
                lblUsername.ForeColor = Color.Red;
                errors.Add(errors.Count > 0 ? ", Username Can Only contain A-Z,a-z,0-9" : "Username Can Only contain A-Z,a-z,0-9");
            }

            return errors;
        }

        public static Boolean IsAlphaNumeric(string strToCheck)
        {
            Regex rg = new Regex(@"^[a-zA-Z0-9\s,]*$");
            return rg.IsMatch(strToCheck);
        }

        public static Boolean IsAlpha(string strToCheck)
        {
            Regex rg = new Regex(@"^[a-zA-Z\s,]*$");
            return rg.IsMatch(strToCheck);
        }

        protected void rdManagerTrue_OnCheckedChanged(object sender, EventArgs e)
        {
            ddlManagers.Enabled = !rdManagerTrue.Checked;
        }

        protected void btnDeleteUser_OnClick(object sender, EventArgs e)
        {
            string userId = ddlUserSelect.SelectedValue;

            if (new DBConnector().DeleteUser(userId))
            {
                lblSuccess.Text = "User Successfully Deleted";
                clearForm();
                pnlForm.Visible = false;
                pnlSelection.Visible = true;
                pnlSuccess.Visible = true;
                ddlUsersPopulate();
                ddlManagersPopulate();
            }
        }
    }
}