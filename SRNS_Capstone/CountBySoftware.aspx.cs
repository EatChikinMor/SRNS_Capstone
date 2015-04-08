using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using SQLiteDataHelpers.Objects;
using SQLiteDataHelpers;
using System.Text.RegularExpressions;

namespace SRNS_Capstone.Reports
{
    public partial class CountBySoftware : System.Web.UI.Page
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
                ViewState["userID"] = value;
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
                    User a = new User() { ID = "0", FirstName = "Austin", LastName = "Rich", IsAdmin = true, LoginID = "arich", ManagerID = 0 };
                    user = a;
                }
                //Remove before release

                if (user != null)
                {

                    ((Capstone)Page.Master).showMenuOptions(user.IsAdmin);
                    //_userID = user.ID;
                    string softCode = Request.QueryString["SoftCode"];

                    if (softCode != null)
                    {
                        string[] codes = softCode.Split('_');
                        buildData(codes[1]);
                    }
                    else
                    {
                        RepeatAllSoftware();
                    }
                }
                else
                {
                    Response.Redirect("~/Default.aspx");
                }
            }
        }

        protected void chkShowProvider_OnCheckedChanged(object sender, EventArgs e)
        {
            RepeatAllSoftware();
        }

        private void RepeatAllSoftware()
        {
            var items = new DBConnector().getLicenseCountReport(chkShowProvider.Checked).AsEnumerable().ToList();
            rptrSoftList.DataSource = items;
            rptrSoftList.DataBind();
        }

        private void buildData(string SelectedSoftware)
        {
            var connector = new DBConnector();
            var row = connector.getSoftwareById(SelectedSoftware).Rows[0];
            lblSoftwareHeader.Text = row["Organization"] + " " + row["SoftwareName"];

            var dt = connector.getLicenseCountReportDetail(SelectedSoftware);
            if (dt.Rows.Count == 0)
            {
                if (buildExpiredData(SelectedSoftware, connector))
                {
                    gridCounts.Visible = false;
                    lblSoftwareCount.Text = "All Keys Expired";
                    lblSoftwareCount.ForeColor = Color.Red;
                }
                else
                {
                    gridCounts.Visible = false;
                    lblSoftwareCount.Text = "No Keys Exist for this Software";
                    lblSoftwareCount.ForeColor = Color.Red;
                }
            }
            else
            {
                lblSoftwareCount.Text = dt.Rows.Count.ToString();
                gridCounts.DataSource = dt;
                gridCounts.DataBind();
                buildExpiredData(SelectedSoftware, connector);
            }

            pnlSelect.Visible = false;
            pnlData.Visible = true;
        }

        private bool buildExpiredData(string SelectedSoftware, DBConnector connector)
        {
            var dt = connector.getLicenseCountReportDetail(SelectedSoftware, true);
            if (dt.Rows.Count == 0)
            {
                return false;
            }

            lblExpired.Text = "Expired :" + dt.Rows.Count;
            lblExpired.Visible = true;
            gridExpired.DataSource = dt;
            gridExpired.DataBind();
            return true;
        }
    }
}