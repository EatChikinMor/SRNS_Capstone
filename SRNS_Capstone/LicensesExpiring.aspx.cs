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
    public partial class LicensesExpiring : System.Web.UI.Page
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
            bool showProvider = chkShowProvider.Checked;
            if (isWholeNumeric(txtCount.Text))
            {
                pnlError.Visible = false;
                var dt = new DBConnector().getExpiringLicensesReport(showProvider, Convert.ToInt32(txtCount.Text));

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
                    gridCounts.Columns[0].Visible = showProvider;
                    gridCounts.DataBind();
                }
            }
            else
            {
                pnlError.Visible = true;
                lblError.Text = "Only whole numbers allowed";
            }

            pnlExpired.Visible = false;
            pnlExpiringLicenses.Visible = true;
        }

        private bool isWholeNumeric(string text)
        {
            double num;
            double.TryParse(text, out num);
            return num == (int)num;
        }

        protected void chkShowProvider_OnCheckedChanged(object sender, EventArgs e)
        {
            if (pnlExpired.Visible)
            {
                btnViewExpired_OnClick(null, null);
            }
            else
            {

                buildGridCount();
            }
        }

        protected void OnClick(object sender, EventArgs e)
        {
            buildGridCount();
        }

        protected void btnViewExpired_OnClick(object sender, EventArgs e)
        {
            pnlError.Visible = false;
            bool showProvider = chkShowProvider.Checked;
            var dt = new DBConnector().getExpiredLicenses(showProvider);

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
                gridCounts.Columns[0].Visible = showProvider;
                gridCounts.DataBind();
            }

            pnlExpired.Visible = true;
            pnlExpiringLicenses.Visible = false;
        }

        protected void btnExpiring_OnClick(object sender, EventArgs e)
        {
            buildGridCount();
        }
    }
}