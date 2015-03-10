using System;
using System.Collections.Generic;
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

            for (int i = 0; i < users.Count; i++ )
            {
                ddlUserSelect.Items.Add(users[i].Text);
                ddlUserSelect.Items[i].Value = users[i].Value;
            }
        }

        protected void btnAddUser_Click(object sender, EventArgs e)
        {
            pnlSelection.Visible = false;

            pnlForm.Visible = true;

            txtFirstName.Text = txtLastName.Text = txtLoginID.Text = txtPassword.Text = "";
            ddlManagers.SelectedIndex = 0;
        }

        protected void ddlUserSelect_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlManagers_SelectedIndexChanged(object sender, EventArgs e)
        {

            String something = ddlManagers.SelectedIndex.ToString();

            DBConnector a = new DBConnector();

            txtFirstName.Text = a.getMaxID("Users").ToString() + " " + something;
        }

        protected void btnOne_Click(object sender, EventArgs e)
        {
            String something = ddlManagers.SelectedIndex.ToString();

            DBConnector a = new DBConnector();

            txtFirstName.Text = a.getMaxID("Users").ToString() + " " + something;
        }
    }
}