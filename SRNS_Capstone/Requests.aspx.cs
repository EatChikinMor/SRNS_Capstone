using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SQLiteDataHelpers;
using SQLiteDataHelpers.Objects;

namespace SRNS_Capstone
{
    public partial class Home : System.Web.UI.Page
    {
        #region Private Members

        private bool _IsAdmin
        {
            get
            {
                return Convert.ToBoolean(ViewState["IsAdmin"]);
            }
            set
            {
                ViewState["IsAdmin"] = value;
            }
        }

        private int _userID
        {
            get
            {
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
            if(!Page.IsPostBack)
            {
                User user = (User)Session["User"];

                //Remove before release
                //if ("localhost" == Request.Url.DnsSafeHost)
                //{
                //    User a = new User() { ID = 1, FirstName = "Austin", LastName = "Rich", IsAdmin = true, LoginID = "arich", ManagerID = 0 };
                //    user = a;
                //}
                //Remove before release

                if (user != null)
                {
                    _IsAdmin = user.IsAdmin;
                    //_userID = user.ID;

                    ((Capstone)Page.Master).showMenuOptions(_IsAdmin);

                    if (_IsAdmin)
                    {
                        pnlPendingRequests.Visible = true;
                        IsAdmin();
                    }
                }
                else
                {
                    Response.Redirect("~/Default.aspx");
                }
                //anything here
            }

            //This will run everytime the page loads
        }

        protected void IsAdmin()
        {
            int RequestCount = new DBConnector().getTableRowCount("Requests");

            switch (RequestCount)
            {
                case 0:
                    lblPendingRequests.Text = "There are no pending requests";
                    break;

                case 1:
                    lblPendingRequests.Text = "There is 1 pending software request";
                    break;
                default:
                    lblPendingRequests.Text = "There are " + RequestCount.ToString() + " pending software request";
                    break;

            }
        }
    }
}