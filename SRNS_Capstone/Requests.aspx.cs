using System;
using System.Collections.Generic;
using System.Data;
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

        private User _user
        {
            get
            {
                return (User)ViewState["User"];
            }
            set
            {
                ViewState["User"] = value;
            }
        }

        private static readonly DBConnector connector = new DBConnector();

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
                    _user = user;
                    _IsAdmin = user.IsAdmin;
                    //_userID = user.ID;

                    ((Capstone)Page.Master).showMenuOptions(_IsAdmin);

                    if (_IsAdmin)
                    {
                        IsAdmin();
                    }
                    else
                    {
                        IsNotAdmin();
                    }
                }
                else
                {
                    Response.Redirect("~/Default.aspx");
                }

            }

            //This will run everytime the page loads
        }

        protected void IsAdmin()
        {
            pnlInputRequest.Visible = false;
            getAllPendingRequests();
        }

        protected void IsNotAdmin()
        {
            pnlInputRequest.Visible = true;
            lblLogin.Text = _user.LoginID;
            lblName.Text = _user.FirstName + " " + _user.LastName;
            getUserPendingRequests();
        }

        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {
            Request req = new Request()
            {
                Name = lblName.Text,
                LoginID = lblLogin.Text,
                RequestTitle = txtRequestTitle.Text,
                RequestContent = (txtRequest.Text.Replace("'", "")).Replace("\"", ""),
                RequestDate = DateTime.Now
            };

            bool success = false;
            string result = connector.insertRequest(req, ref success);

            if (success)
            {
                pnlSuccess.Visible = true;
                pnlError.Visible = false;
                lblSuccess.Text = result;
                txtRequestTitle.Text =
                    txtRequest.Text= "";
            }
            else
            {
                pnlError.Visible = true;
                pnlSuccess.Visible = false;
                lblError.Text = result;
            }
        }

        protected void getUserPendingRequests()
        {
            DataTable dt = connector.getPendingRequests(_user.LoginID, _user.FirstName + " " + _user.LastName,
                false);
            if (dt.Rows.Count > 0)
            {
                pnlPendingRequests.Visible = true;
                gridCounts.DataSource = dt;
                gridCounts.DataBind();
                int RequestCount = dt.Rows.Count;

                lblPendingRequests.Text = RequestCount == 1
                    ? "You have 1 pending software request"
                    : "You have " + RequestCount + " pending software requests";
            }
        }

        protected void getAllPendingRequests()
        {
            DataTable dt = connector.getPendingRequests();
            if (dt.Rows.Count > 0)
            {
                pnlPendingRequests.Visible = true;
                gridCounts.DataSource = dt;
                gridCounts.DataBind();
                int RequestCount = dt.Rows.Count;

                lblPendingRequests.Text = RequestCount == 1
                    ? "There is 1 pending software request"
                    : "There are " + RequestCount + " pending software requests";
            }
        }

        protected void gridCounts_OnItemCommand(object source, DataGridCommandEventArgs e)
        {
            string ID = e.Item.Cells[0].Text;
            
            connector.DeleteRequest(ID);

            if (_user.IsAdmin)
            {
                getAllPendingRequests();
            }
            else
            {
                getUserPendingRequests();
            }
        }
    }
}