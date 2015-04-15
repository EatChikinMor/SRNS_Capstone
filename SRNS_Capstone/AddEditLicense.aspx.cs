using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SQLiteDataHelpers.Objects;
using SQLiteDataHelpers;
using System.Text;
using System.Data;

namespace SRNS_Capstone
{
    
    public partial class AddLicense : System.Web.UI.Page
    {
        #region Private Members

        private string directoryPath = AppDomain.CurrentDomain.BaseDirectory + "attachmentsDirectory\\";

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

        private DataTable VSconflictingTable
        {
            get
            {
                if (ViewState["conflictingTable"] == null)
                {
                    ViewState["conflictingTable"] = new DataTable();
                }
                return (DataTable)ViewState["conflictingTable"];
            }
            set
            {
                ViewState["conflictingTable"] = value;
            }
        }

        #endregion

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
                    //_userID = user.ID;

                    if (_user.IsAdmin)
                    {
                        ((Capstone)Page.Master).showMenuOptions(true);
                    }
                    populatDropDowns();
                    txtDateUpdated.Text = DateTime.Now.Date.ToString("d");
                    
                    //txtDateAssigned.Text = "4/02/2014";
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
            ddlLicHolder.Items[0].Value = "";
            ddlHolderManager.Items.Add("");
            ddlHolderManager.Items[0].Value = "";

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

        private List<String> ValidateEmptyInput(ref bool[] valid)
        {
            //TODO: Get validation Requirements From Karla
            clearErrors();

            var errors = new List<string>();

            if (String.IsNullOrEmpty(txtSoftName.Text))
            {
                ShowTextError(txtSoftName);
                ShowLabelError(lblSoftName);
                errors.Add(lblSoftName.Text);
                valid[0] = false;
            }
            if (String.IsNullOrEmpty(txtProvider.Text))
            {
                ShowTextError(txtProvider);
                ShowLabelError(lblProvider);
                errors.Add(errors.Count > 0 ? ", " + lblProvider.Text : lblProvider.Text);
                valid[1] = false;
            }
            if (String.IsNullOrEmpty(txtLiKey.Text))
            {
                ShowTextError(txtLiKey);
                ShowLabelError(lblLiNum);
                errors.Add(errors.Count > 0 ? ", " + lblLiNum.Text : lblLiNum.Text);
                valid[2] = false;
            }
            if (String.IsNullOrEmpty(txtLiCost.Text))
            {
                ShowTextError(txtLiCost);
                ShowLabelError(lblLiCost);
                errors.Add(errors.Count > 0 ? ", " + lblLiCost.Text : lblLiCost.Text);
            }
            if (String.IsNullOrEmpty(txtDateExpiring.Text))
            {
                ShowTextError(txtDateExpiring);
                ShowLabelError(lblDateExpiring);
                errors.Add(errors.Count > 0 ? ", " + lblDateExpiring.Text : lblDateExpiring.Text);
            }

            if (!radiobtnSRNS.Checked && !radiobtnCen.Checked && !radiobtnDOE.Checked && !radiobtnSRR.Checked)
            {
                ShowLabelError(lblLiComp);
                errors.Add(errors.Count > 0 ? ", " + lblLiComp.Text : lblLiComp.Text);
            }

            if (!radioBtnAssign.Checked && !radioBtnAvailable.Checked && !radioBtnRemove.Checked)
            {
                ShowLabelError(lblAssignStatus);
                errors.Add(errors.Count > 0 ? ", " + lblAssignStatus.Text : lblAssignStatus.Text);
            }

            return errors;
        }

        private List<String> ValidateInput()
        {
            var valid = new[] { true, true, true };
            var errors = ValidateEmptyInput(ref valid);


            if (ddlLicHolder.SelectedIndex == 0 && radioBtnAssign.Checked)
            {
                ShowDropdownError(ddlLicHolder);
                errors.Add(errors.Count > 0 ? ", License marked as assigned with no holder chosen" : "License marked as assigned with no holder chosen");
            }

            if (!(radioBtnAssign.Checked || radioBtnAvailable.Checked || radioBtnRemove.Checked))
            {
                ShowLabelError(lblAssignStatus);
                errors.Add(errors.Count > 0 ? ", License Availablility not marked" : " License Availablility not marked");
            }

            return errors;
        }

        private void clearErrors()
        {
            pnlError.Visible = false;
            ClearTextError(txtSoftName);
            ClearLabelError(lblSoftName);
            ClearTextError(txtProvider);
            ClearLabelError(lblProvider);
            ClearTextError(txtLiKey);
            ClearLabelError(lblLiNum);
            ClearTextError(txtLiCost);
            ClearLabelError(lblLiCost);
            ClearTextError(txtDateExpiring);
            ClearLabelError(lblDateExpiring);
            ClearDropdownError(ddlLicHolder);
            ClearLabelError(lblAssignStatus);
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

        protected void ClearLabelError(Label l)
        {
            l.ForeColor = Color.Black;
        }

        protected void ClearTextError(TextBox t)
        {
            t.BackColor = Color.White;
        }

        protected void ClearDropdownError(DropDownList d)
        {
            d.BackColor = Color.White;
        }

        protected void btnAddKey_OnClick(object sender, EventArgs e)
        {
            lblEditor.Text = "Document Created By/Last Updated By: " + _user.FirstName + " " + _user.LastName;
            txtLiKey.Enabled = true;
            pnlSuccess.Visible = false;
            clearForm();
            pnlSelection.Visible = false;
            pnlMain.Visible = true;
            btnDelete.Visible = false;
            btnSubmit.Visible = true;
            btnUpdate.Visible = false;
            pnlWarningAttach.Visible = false;
            lblLink.Visible = false;
            lnkFileLink.Visible = false;
        }

        protected void btnLookupKey_OnClick(object sender, EventArgs e)
        {
            pnlSuccess.Visible = false;
            btnDelete.Visible = true;
            btnSubmit.Visible = false;
            btnUpdate.Visible = true;

            if (String.IsNullOrEmpty(txtEnterKeyToEdit.Text))
            {
                pnlSuccess.Visible = true;
                lblSuccess.Text = "No keys Exist for the entered value";
            }
            else
            {
                DataTable dt = new DBConnector().getLicenseByKey(txtEnterKeyToEdit.Text);

                if (dt.Rows.Count == 0)
                {
                    pnlSuccess.Visible = true;
                    lblSuccess.Text = "No keys Exist for the entered value";   
                }
                else
                {
                    fillFields(dt.Rows[0]);
                    txtLiKey.Enabled = false;
                    pnlWarningAttach.Visible = true;
                }
            }
        }
        
        private enum CompanyHolders
        {
            SRNS = 0,
            SRR = 1,
            DOE = 2,
            Centerra = 3
        }

        private enum Assigned
        {
            Assign = 0,
            Remove = 1,
            Available = 2
        }

        protected void btnDelete_OnClick(object sender, EventArgs e)
        {
            //TODO: Test block wrapped in "//--Test--" tags

            //--Test--
            Guid g = Guid.Empty;

            if (!String.IsNullOrEmpty(hdnGuid.Value))
            {
                g = new Guid(hdnGuid.Value);
            }

            if (g != Guid.Empty)
            {
                string path = Directory.GetFiles(directoryPath + hdnGuid.Value).First();
               
                File.Delete(path);
            }
            //--Test--

            if (new DBConnector().DeleteKey(txtLiKey.Text))
            {
                pnlSuccess.Visible = true;
                pnlMain.Visible = false;
                pnlSelection.Visible = true;
                lblSuccess.Text = "Key '" + txtLiKey.Text + "' Sucessfully Deleted";
            }
            
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            var errors = ValidateInput();

            if (errors.Count == 0)
            {
                var checkedButton = pnlHolders.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
                int holderCompany = (int)Enum.Parse(typeof(CompanyHolders), checkedButton.ToolTip);

                checkedButton = pnlAssign.Controls.OfType<RadioButton>().FirstOrDefault(btn => btn.Checked);
                int assignmentStatus = (int)Enum.Parse(typeof(Assigned), checkedButton.ToolTip);

                DBConnector conn = new DBConnector();

                if (!conn.doesKeyExist(txtLiKey.Text))
                {

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
                        var result = conn.insertSoftware(txtSoftName.Text, provID, ref passed);

                        if (passed)
                        {
                            softID = Convert.ToInt32(result);
                        }
                    }

                    Guid g = Guid.NewGuid();
                    if (fileUpload.HasFile)
                    {
                        try
                        {
                            Directory.CreateDirectory(directoryPath + g);
                            string filename = Path.GetFileName(fileUpload.FileName);
                            fileUpload.SaveAs(directoryPath + g + @"\" + filename);
                        }
                        catch
                        {
                            g = Guid.Empty;
                        }
                    }

                    //String.Format("{0:0.00}", a); - reference
                    LicenseKey LK = new LicenseKey()
                    {
                        SoftwareId = softID,
                        Description = txtSoftDescription.Text,
                        Key = txtLiKey.Text,
                        Holder = ddlLicHolder.SelectedItem.Text,
                        HolderID = ddlLicHolder.SelectedItem.Value,
                        Manager = ddlHolderManager.SelectedItem.Text,
                        LicenseCost = Convert.ToDecimal(txtLiCost.Text),
                        RequisitionNumber = txtReqNum.Text,
                        ChargebackComplete = ddlChargeback.SelectedIndex == 0,
                        Provider = provID,
                        Assignment = assignmentStatus,
                        Speedchart = txtSpeedchart.Text,
                        DateUpdated = Convert.ToDateTime(txtDateUpdated.Text), //Validate empty
                        DateAssigned =
                            txtDateAssigned.Text.Length > 0
                                ? Convert.ToDateTime(txtDateUpdated.Text)
                                : DateTime.MinValue,
                        DateRemoved =
                            txtDateRemoved.Text.Length > 0 ? Convert.ToDateTime(txtDateRemoved.Text) : DateTime.MinValue,
                        DateExpiring = Convert.ToDateTime(txtDateExpiring.Text), //Validate empty
                        LicenseHolderCompany = holderCompany,
                        Comments = Server.HtmlEncode(editor.InnerText),
                        fileSubpath = g,
                        LastModifiedBy = _user.FirstName + " " + _user.LastName
                    };

                    string insert = new DBConnector().InsertLicense(LK);

                    pnlMain.Visible = false;
                    pnlSelection.Visible = true;
                    pnlSuccess.Visible = true;
                    lblSuccess.Text = insert;
                    //string response = new DBConnector().InsertSoftware(software);
                    //clearForm();
                }
                else
                {
                    pnlError.Visible = true;
                    lblError.Text = "License Key '" + txtLiKey.Text + "' already exists";
                }
            }
            else
            {
                lblError.Visible = true;
                lblError.ForeColor = Color.Red;

                var E = new StringBuilder();

                foreach (var error in errors)
                {
                    E.Append(error);
                }

                lblError.Text = E + " is required.";
                pnlError.Visible = true;
            }

        }

        protected void btnUpdate_OnClick(object sender, EventArgs e)
        {
            var errors = ValidateInput();

            if (errors.Count == 0)
            {
                var checkedButton = pnlHolders.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
                int holderCompany = (int)Enum.Parse(typeof(CompanyHolders), checkedButton.ToolTip);

                checkedButton = pnlAssign.Controls.OfType<RadioButton>().FirstOrDefault(btn => btn.Checked);
                int assignmentStatus = (int)Enum.Parse(typeof(Assigned), checkedButton.ToolTip);

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
                    var result = conn.insertSoftware(txtSoftName.Text, provID, ref passed);

                    if (passed)
                    {
                        softID = Convert.ToInt32(result);
                    }
                }

                Guid g = Guid.Empty;

                if (!String.IsNullOrEmpty(hdnGuid.Value))
                {
                    g = new Guid(hdnGuid.Value);
                }

                if (fileUpload.HasFile)
                {
                    try
                    {
                        if (g == Guid.Empty)
                        {
                            g = new Guid();

                            Directory.CreateDirectory(directoryPath + g);
                            string filename = Path.GetFileName(fileUpload.FileName);
                            fileUpload.SaveAs(directoryPath + g + @"\" + filename);
                        }
                        else
                        {
                            string path = Directory.GetFiles(directoryPath + hdnGuid.Value).First();
                            File.Delete(path);

                            string filename = Path.GetFileName(fileUpload.FileName);
                            fileUpload.SaveAs(directoryPath + g + @"\" + filename);
                        }
                    }
                    catch
                    {

                    }
                }

                //String.Format("{0:0.00}", a); - reference
                LicenseKey LK = new LicenseKey()
                {
                    SoftwareId = softID,
                    Description = txtSoftDescription.Text,
                    Key = txtLiKey.Text,
                    Holder = ddlLicHolder.SelectedItem.Text,
                    HolderID = ddlLicHolder.SelectedItem.Value,
                    Manager = ddlHolderManager.SelectedItem.Text,
                    LicenseCost = Convert.ToDecimal(txtLiCost.Text),
                    RequisitionNumber = txtReqNum.Text,
                    ChargebackComplete = ddlChargeback.SelectedIndex == 0,
                    Provider = provID,
                    Assignment = assignmentStatus,
                    Speedchart = txtSpeedchart.Text,
                    DateUpdated = Convert.ToDateTime(txtDateUpdated.Text), //Validate empty
                    DateAssigned =
                        txtDateAssigned.Text.Length > 0
                            ? Convert.ToDateTime(txtDateUpdated.Text)
                            : DateTime.MinValue,
                    DateRemoved =
                        txtDateRemoved.Text.Length > 0 ? Convert.ToDateTime(txtDateRemoved.Text) : DateTime.MinValue,
                    DateExpiring = Convert.ToDateTime(txtDateExpiring.Text), //Validate empty
                    LicenseHolderCompany = holderCompany,
                    Comments = Server.HtmlEncode(editor.InnerText),
                    fileSubpath = g,
                    LastModifiedBy = _user.FirstName + " " +_user.LastName
                };

                if( new DBConnector().UpdateLicenseKey(LK))
                {
                    pnlMain.Visible = false;
                    pnlSelection.Visible = true;
                    pnlSuccess.Visible = true;
                    lblSuccess.Text = "Update Successful";
                    pnlWarningAttach.Visible = false;
                }
                else
                {
                    lblError.Visible = true;
                    lblError.ForeColor = Color.Red;
                    lblError.Text = "An unknown error occurred, update failed";
                    pnlError.Visible = true;
                }
                //string response = new DBConnector().InsertSoftware(software);
                //clearForm();
            }
            else
            {
                lblError.Visible = true;
                lblError.ForeColor = Color.Red;

                var E = new StringBuilder();

                foreach (var error in errors)
                {
                    E.Append(error);
                }

                lblError.Text = E + " is required.";
                pnlError.Visible = true;
            }

        }

        private void fillFields(DataRow row)
        {
            txtSoftName.Text = row["SoftwareName"].ToString();
            txtSoftDescription.Text = row["Description"].ToString();
            txtProvider.Text = row["Organization"].ToString();
            txtLiKey.Text = row["LicenseKey"].ToString();
            txtSpeedchart.Text = row["SpeedChart"].ToString();
            var date = Convert.ToDateTime(row["DateModified"].ToString());
            txtDateUpdated.Text = date.ToShortDateString();
            ddlLicHolder.SelectedValue = row["HolderLoginID"].ToString();
            ddlHolderManager.SelectedValue = ddlLicHolder.Items.FindByText(row["KeyManager"].ToString()).Value;
            date = Convert.ToDateTime(row["DateAssigned"].ToString());
            txtDateAssigned.Text = date == DateTime.MinValue ? "" : date.ToShortDateString();
            txtLiHoldUserId.Text = row["HolderLoginID"].ToString();
            txtLiCost.Text = String.Format("{0:0.00}", Convert.ToDecimal(row["LicenseCost"].ToString()));
            date = Convert.ToDateTime(row["DateRemoved"].ToString());
            txtDateRemoved.Text = date == DateTime.MinValue ? "" : date.ToShortDateString();
            txtReqNum.Text = row["RequisitionNumber"].ToString();
            ddlChargeback.SelectedIndex = Convert.ToInt32(row["ChargebackComplete"]);
            date = Convert.ToDateTime(row["DateExpiring"].ToString());
            txtDateExpiring.Text = date == DateTime.MinValue ? "" : date.ToShortDateString();

            if (!String.IsNullOrEmpty(row["LicenseHolderCompany"].ToString()))
            {
                RadioButton[] rd = { radiobtnSRNS, radiobtnSRR, radiobtnDOE, radiobtnCen };
                rd[Convert.ToInt32(row["LicenseHolderCompany"])].Checked = true;
            }

            if (!String.IsNullOrEmpty(row["AssignmentStatus"].ToString()))
            {
                RadioButton[] rd = { radioBtnAssign, radioBtnRemove, radioBtnAvailable };
                rd[Convert.ToInt32(row["AssignmentStatus"])].Checked = true;
            }

            editor.InnerText = Server.HtmlDecode(row["Comments"].ToString());
            pnlSelection.Visible = false;
            pnlMain.Visible = true;
            hdnGuid.Value = row["FileSubpath"].ToString();

            lblEditor.Text = "Document Created By/Last Updated By: " + row["LastModifiedBy"];

            lnkFileLink.Visible = true;
            lblLink.Visible = true;
            if (!String.IsNullOrEmpty(hdnGuid.Value))
            {
                //StreamReader stream = new StreamReader(directoryPath + @"\" + hdnGuid.Value);
                string Path = Directory.GetFiles(directoryPath + @"\" + hdnGuid.Value).First();
                string File = System.IO.Path.GetFileName(Path);
                lnkFileLink.Text = File;
                lnkFileLink.NavigateUrl = @"\attachmentsDirectory\" + hdnGuid.Value + @"\" + File;
            }
        }
    }
}