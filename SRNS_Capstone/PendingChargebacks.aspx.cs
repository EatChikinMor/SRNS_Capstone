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
    public partial class PendingChargebacks : System.Web.UI.Page
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
            var dt = new DBConnector().getPendingChargebackReport();

            if (dt.Rows.Count == 0)
            {

                lblSoftwareCount.Text = "No licenses exist with a pending chargeback";
                lblSoftwareCount.ForeColor = Color.Red;
                gridCounts.Visible = false;
            }
            else
            {
                lblSoftwareCount.ForeColor = Color.Black;
                gridCounts.Visible = true;
                lblSoftwareCount.Text = dt.Rows.Count.ToString();
                gridCounts.DataSource = dt;
                gridCounts.DataBind();
            }
        }

        protected void chkShowProvider_OnCheckedChanged(object sender, EventArgs e)
        {
            buildGridCount();
        }
    }
}