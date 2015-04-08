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
    public partial class AddLicense : System.Web.UI.Page
    {
        #region Private Members

        private bool _IsAdmin
        {
            get
            {
                if (ViewState["IsAdmin"] == null)
                {
                    ViewState["IsAdmin"] = false;
                }
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
                if (ViewState["userID"] == null)
                {
                    ViewState["userID"] = 0;
                }
                return (int)ViewState["userID"];
            }
            set
            {
                ViewState["IsAdmin"] = value;
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
                    _IsAdmin = user.IsAdmin;
                    //_userID = user.ID;
                    ddlSoftwarePopulate();

                    if (_IsAdmin)
                    {
                        ((Capstone)Page.Master).showMenuOptions(_IsAdmin);
                    }
                }
                else
                {
                    Response.Redirect("~/Default.aspx");
                }
            }
        }
            //Check for session timeout
        protected void ddlSoftwarePopulate()
        {
            List<ComboboxItem> software = new DBConnector().populateSoftwareList();

            for (int i = 0; i < software.Count; i++)
            {
                ListItem item = new ListItem(software[i].Text, software[i].Value);
                ddlSoftwareSelect.Items.Add(item);
            }
        }

        protected void clearForm()
        {
            txtSoftName.Text = txtSoftDescription.Text = txtLiNum.Text = txtSpeedchart.Text = txtLiHold.Text = txtLiHoldUserId.Text = txtLiCost.Text = txtLicenseMan.Text = txtReqNum.Text = "";

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
        //    SoftwareInput software = new SoftwareInput()
        //        {
        //            SoftwareName = txtSoftName.Text,
        //            SoftwareDescription = txtSoftDescription.Text,
        //            LicenseNumber = txtLiNum.Text,
        //            LicenseHolder = txtLiHold.Text,
        //            LicenseHoldUid = txtLiHoldUserId.Text,
        //            AscReqNum = txtReqNum.Text,
        //            Speedchart = txtSpeedchart.Text,
        //            LicenseMan = txtLicenseMan.Text,
        //            LicenseCost = txtLiCost.Text,
        //            radioAssign = radioBtnAssign.Checked,
        //            radioRemove = radioBtnRemove.Checked,
        //            radioAvailable = radioBtnAvailable.Checked,
        //            radioSRNS = radiobtnSRNS.Checked,
        //            radioSRR = radiobtnSRR.Checked,
        //            radioDOE = radiobtnDOE.Checked,
        //            radioCen = radiobtnCen.Checked,
        //        };

          

        //        string response = new DBConnector().InsertSoftware(software);
        //        clearForm();

        }

    }
}