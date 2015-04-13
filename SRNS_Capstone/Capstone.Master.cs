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

        public void showMenuOptions(bool IsAdmin)
        {
            if (IsAdmin)
            {
                admin.Visible =
                    lstAddLicense.Visible =
                        lstUserReports.Visible = 
                            lstReports.Visible = true;
            }
            else
            {
                lstAddLicense.Visible = false;
                lstReports.Visible = false;
                lstUserReports.Visible = true;
                admin.Visible = false;
            }
            //lstAvailLic.Visible = false;
        }
    }
}