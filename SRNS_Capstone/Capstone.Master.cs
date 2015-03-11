using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SQLiteDataHelpers.Objects;

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
            /* TODO: Logout Logic Here */
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
                
            }
            //lstAvailLic.Visible = false;
        }
    }
}