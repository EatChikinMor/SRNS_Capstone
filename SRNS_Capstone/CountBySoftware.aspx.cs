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
                    User a = new User() { ID = 0, FirstName = "Austin", LastName = "Rich", IsAdmin = true, LoginID = "arich", ManagerID = 0 };
                    user = a;
                }
                //Remove before release

                if (user != null)
                {

                    ((Capstone)Page.Master).showMenuOptions(user.IsAdmin, _userID);
                    _userID = user.ID;
                    string softCode = Request.QueryString["SoftCode"];

                    if (softCode != null)
                    {
                        string[] codes = softCode.Split('_');
                        buildData(codes[1]);
                    }
                    else
                    {
                        RepeatAllSoftware();
                        populateSoftwareSelection();
                    }
                }
                else
                {
                    Response.Redirect("~/Default.aspx");
                }
            }
        }

        protected void ddlSoftwareSelect_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            buildData(ddlSoftwareSelect.SelectedItem.Value);
            lblSoftwareHeader.Text = ddlSoftwareSelect.SelectedItem.Text;
        }

        private void RepeatAllSoftware()
        {
            var items = new DBConnector().getLicenseCountReport().AsEnumerable().ToList();
            rptrSoftList.DataSource = items;
            rptrSoftList.DataBind();
        }

        private void populateSoftwareSelection()
        {
            ddlSoftwareSelect.Items.Clear();
            ddlSoftwareSelect.Items.Add(new ListItem("", ""));

            DataTable dt = new DBConnector().getAllSoftware();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ListItem item = new ListItem(dt.Rows[i]["SoftwareName"] + " - " + dt.Rows[i]["Organization"], dt.Rows[i]["ID"].ToString());
                ddlSoftwareSelect.Items.Add(item);
            }
        }

        private void buildData(string SelectedSoftware)
        {
            var connector = new DBConnector();
            var row = connector.getSoftwareById(SelectedSoftware).Rows[0];
            lblSoftwareHeader.Text = row["Organization"] + " " + row["SoftwareName"];

            var dt = connector.getLicenseCountReportDetail(SelectedSoftware);
            if (dt.Rows.Count == 0)
            {
                gridCounts.Visible = false;
                lblSoftwareCount.Text = "No Keys Exist for this Software";
                lblSoftwareCount.ForeColor = Color.Red;
            }
            else
            {
                lblSoftwareCount.Text = dt.Rows.Count.ToString();
                gridCounts.DataSource = dt;
                gridCounts.DataBind();
            }
            pnlSelect.Visible = false;
            pnlData.Visible = true;
        }

        protected void rptrSoftList_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }
    }
}