using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SQLiteDataHelpers.Objects;
using SQLiteDataHelpers;

namespace SRNS_Capstone
{
    public partial class ManagersLicenseHolders : System.Web.UI.Page
    {
        private User _user
        {
            get
            {
                if (ViewState["userID"] == null)
                {
                    ViewState["userID"] = 0;
                }
                return (User)ViewState["userID"];
            }
            set
            {
                ViewState["userID"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                User user = (User)Session["User"];

                //Remove before release
                //if ("localhost" == Request.Url.DnsSafeHost)
                //{
                //    User a = new User() { ID = "0", FirstName = "Austin", LastName = "Rich", IsAdmin = true, LoginID = "arich", ManagerID = 0 };
                //    user = a;
                //}
                //Remove before release

                if (user != null)
                {
                    _user = user;
                    ((Capstone)Page.Master).showMenuOptions(user.IsAdmin);
                    lblMangerNameHead.Text = _user.FirstName + " " + _user.LastName;
                    buildGridCount();
                }
                else
                {
                    Response.Redirect("~/Default.aspx");
                }
            }
        }

        private void buildGridCount()
        {
            //bool showProvider = chkShowProvider.Checked;
            var dt = new DBConnector().getManagersLicenseHolders( _user.FirstName + " " + _user.LastName);

            if (dt.Rows.Count == 0)
            {
                lblSoftwareCount.Text = "No Keys Exist";
                lblSoftwareCount.ForeColor = Color.Red;
                gridCounts.Visible = false;
            }
            else
            {
                lblSoftwareCount.Text = dt.Rows.Count.ToString();
                gridCounts.DataSource = dt;
                //gridCounts.Columns[0].Visible = showProvider;
                gridCounts.DataBind();
            }
        }

        protected void chkShowProvider_OnCheckedChanged(object sender, EventArgs e)
        {
            buildGridCount();
        }
    }
}