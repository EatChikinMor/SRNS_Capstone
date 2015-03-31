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
    public partial class Capstone : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!Page.IsPostBack)
            //{
            //    User user = (User)Session["User"];
            //}
            //if (user != null)
            //{
            //    pnlNav.Visible = true;
            //}
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            /* TODO: Any Further Logout Logic Here */
            Session.Abandon();
            Response.Redirect("~/Default.aspx");
        }

        public void showMenuOptions(bool IsAdmin, int UserID)
        {
            if (IsAdmin)
            {
                admin.Visible =
                lstAddLicense.Visible =
                lstReports.Visible =
                lstAvailLic.Visible = 
                lstLicCount.Visible =
                lstLicExp.Visible = 
                lstManagLic.Visible =
                lstPendCharg.Visible = true;
            }
            else
            {
                var access = new DBConnector().getUserPrivileges(UserID).Rows[0];
                admin.Visible = false;

                lstRequests.Visible = (access["Requests"].ToString() == "1");
                lstAddLicense.Visible = (access["AddLicense"].ToString() == "1");
                lstLicCount.Visible = (access["LicenseCountReport"].ToString() == "1");
                lstAvailLic.Visible = (access["AvailLicenseReport"].ToString() == "1");
                lstManagLic.Visible = (access["ManagLicenseReport"].ToString() == "1");
                lstLicExp.Visible = (access["LicenseExpReport"].ToString() == "1");
                lstPendCharg.Visible = (access["PendChargeReport"].ToString() == "1");

                lstReports.Visible = lstAvailLic.Visible || lstLicCount.Visible || lstLicExp.Visible ||
                                     lstManagLic.Visible || lstPendCharg.Visible;
            }
            //lstAvailLic.Visible = false;
        }
    }
}