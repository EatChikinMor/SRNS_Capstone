using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SQLiteDataHelpers.Objects;
using SQLiteDataHelpers;
using System.Globalization;

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
                lblSoftwareCount.ForeColor = Color.Black;
                gridCounts.Visible = true;
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

        protected void gridCounts_OnItemDataBound(object sender, DataGridItemEventArgs e)
        {
            DateTime now = DateTime.Now;

            if (e.Item != null)
            {
                int a = e.Item.ItemIndex;

                if (a >= 0 && e.Item.Cells[2].Text != "&nbsp;")
                {
                    var item = e.Item;
                    var gridTime = DateTime.ParseExact(item.Cells[2].Text, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    //Convert.ToDateTime(item.Cells[2].Text);
                    int result = DateTime.Compare(gridTime, now);

                    if (result < 0)
                    {
                        item.Cells[2].ForeColor = Color.Red;
                    }
                }
            }
        }

        protected void gridCounts_OnItemCommand(object source, DataGridCommandEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}