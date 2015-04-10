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

        private List<Tuple<String, String, String>> VSActiveUsers
        {
            get
            {
                if (ViewState["ActiveUsers"] == null)
                {
                    ViewState["ActiveUsers"] = new List<Tuple<String, String, String>>();
                }
                return (List<Tuple<String, String, String>>)ViewState["ActiveUsers"];
            }
            set { ViewState["ActiveUsers"] = value; }
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

                    if (_IsAdmin)
                    {
                        ((Capstone)Page.Master).showMenuOptions(_IsAdmin);
                    }
                    populatDropDowns();
                    txtDateUpdated.Text = DateTime.Now.Date.ToString("d");
                    lblEditor.Text = "Document Created By/Last Updated By: " + user.FirstName + " " + user.LastName;
                    txtDateAssigned.Text = "4/02/2014";
                }
                else
                {
                    Response.Redirect("~/Default.aspx");
                }
            }
        }

        protected void clearForm()
        {
            txtSoftName.Text = txtSoftDescription.Text = txtLiKey.Text = txtSpeedchart.Text = txtLiHoldUserId.Text = txtLiCost.Text = txtReqNum.Text = "";
            ddlLicHolder.SelectedIndex = ddlHolderManager.SelectedIndex = 0;
            radiobtnDOE.Checked =
                radioBtnAssign.Checked = 
                    radioBtnAvailable.Checked =
                        radioBtnRemove.Checked =
                            radiobtnCen.Checked =
                                radiobtnDOE.Checked = 
                                    radiobtnSRNS.Checked = 
                                        radiobtnSRR.Checked = false;
        }

        protected void populatDropDowns()
        {
            ddlLicHolder.Items.Clear();
            ddlHolderManager.Items.Clear();
            ddlLicHolder.SelectedIndex = ddlHolderManager.SelectedIndex = 0;
            ddlLicHolder.Items.Add("");
            ddlLicHolder.Items[0].Value = "0";
            ddlHolderManager.Items.Add("");
            ddlHolderManager.Items[0].Value = "0";

            if (VSActiveUsers.Count > 0)
            {
                foreach (var item in VSActiveUsers)
                {
                    ListItem input = new ListItem(item.Item1, item.Item2);
                    ddlLicHolder.Items.Add(input);
                    ddlHolderManager.Items.Add(input);
                }
            }
            else
            {
                var everyone = ActiveDirectoryHelper.GetAdUsers();
                //var anyone = ActiveDirectoryHelper.getAllUsersOnDomain();
                var list = new List<Tuple<String,String,String>>();

                foreach (var item in everyone)
                {
                    ListItem input = new ListItem(item.Item1, item.Item3);
                    ddlLicHolder.Items.Add(input);
                    ddlHolderManager.Items.Add(input);
                    list.Add(item);
                }

                VSActiveUsers = list;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            var checkedButton = pnlHolders.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
            int holderCompany = (int)Enum.Parse(typeof(CompanyHolders), checkedButton.ToolTip);

            checkedButton = pnlAssign.Controls.OfType<RadioButton>().FirstOrDefault(btn => btn.Checked);
            int assignmentStatus = (int) Enum.Parse(typeof (Assigned), checkedButton.ToolTip);

            DBConnector conn = new DBConnector();

            int provID = conn.GetProviderIdByName(txtProvider.Text);

            bool passed = false;

            if (provID < 0)
            {
                var result = conn.insertProvider(txtProvider.Text, ref passed);
                
                if (passed)
                {
                    provID = Convert.ToInt32(result);
                }
            }

            passed = false;

            int softID = conn.GetSoftwareIdByName(txtSoftName.Text);

            if (softID < 0)
            {
                var result = conn.insertSoftware(txtSoftName.Text, ref passed);

                if (passed)
                {
                    softID = Convert.ToInt32(result);
                }
            }

            LicenseKey LK = new LicenseKey()
            {
                SoftwareId = softID,
                Description = txtSoftDescription.Text,
                Key = txtLiKey.Text,
                Holder = ddlLicHolder.SelectedItem.Text,
                HolderID = ddlLicHolder.SelectedItem.Value,
                Manager = ddlHolderManager.SelectedItem.Value,
                LicenseCost = Convert.ToDecimal(2)
            };

            //string response = new DBConnector().InsertSoftware(software);
            clearForm();

        }

        private List<String> ValidateEmptyInput(ref bool[] valid, bool update = false)
        {
            //TODO: Get validation Requirements From Karla
            //pnlError.Visible = false;

            var errors = new List<string>();

            if (String.IsNullOrEmpty(txtSoftName.Text))
            {
                ShowTextError(txtSoftName);
                ShowLabelError(lblSoftName);
                errors.Add("Software Name Empty");
                valid[0] = false;
            }
            if (String.IsNullOrEmpty(txtProvider.Text))
            {
                ShowTextError(txtProvider);
                ShowLabelError(lblProvider);
                errors.Add(errors.Count > 0 ? ", Provider Empty" : "Provider Empty");
                valid[1] = false;
            }
            if (String.IsNullOrEmpty(txtLiKey.Text))
            {
                ShowTextError(txtLiKey);
                ShowLabelError(lblLiNum);
                errors.Add(errors.Count > 0 ? ", License Key Empty" : "License Key Empty");
                valid[2] = false;
            }
            if (String.IsNullOrEmpty(txtLiCost.Text) && !update)
            {
                ShowTextError(txtLiCost);
                ShowLabelError(lblLiCost);
                errors.Add(errors.Count > 0 ? ", License Cost Empty" : "License Cost Empty");
            }
            if (ddlLicHolder.SelectedIndex == 0 && radioBtnAssign.Checked)
            {
                ShowDropdownError(ddlLicHolder);
                errors.Add(errors.Count > 0 ? ", License marked as assigned with no holder chosen" : "License marked as assigned with no holder chosen");
            }

            //pnlError.Visible = true;

            return errors;
        }

        private List<String> ValidateInput(bool update = false)
        {
            var valid = new[] { true, true, true };
            var errors = ValidateEmptyInput(ref valid, update);
            //if (valid[0] && !IsAlpha(txtFirstName.Text))
            //{
            //    txtFirstName.BackColor = ColorTranslator.FromHtml("#FFD8D8");
            //    lblFirstName.ForeColor = Color.Red;
            //    errors.Add("First Name Invalid");
            //}
            //if (valid[1] && !IsAlpha((txtLastName.Text)))
            //{
            //    txtLastName.BackColor = ColorTranslator.FromHtml("#FFD8D8");
            //    lblLastName.ForeColor = Color.Red;
            //    errors.Add(errors.Count > 0 ? ", Last Name Invalid" : "Last Name Invalid");
            //}
            //if (valid[2] && !IsAlphaNumeric(txtLoginID.Text))
            //{
            //    txtLoginID.BackColor = ColorTranslator.FromHtml("#FFD8D8");
            //    lblUsername.ForeColor = Color.Red;
            //    errors.Add(errors.Count > 0 ? ", Username Can Only contain A-Z,a-z,0-9" : "Username Can Only contain A-Z,a-z,0-9");
            //}

            return errors;
        }

        protected void ShowLabelError(Label l)
        {
            l.ForeColor = Color.Red;
        }

        protected void ShowTextError(TextBox t)
        {
            t.BackColor = ColorTranslator.FromHtml("#FFD8D8");
        }

        protected void ShowDropdownError(DropDownList d)
        {
            d.BackColor = ColorTranslator.FromHtml("#FFD8D8");
        }

        private enum CompanyHolders
        {
            SRNS = 0,
            SRR = 1,
            DOE = 2,
            Centerra = 4
        }

        private enum Assigned
        {
            Assign = 0,
            Remove = 1,
            Available = 2
        }

    }
}