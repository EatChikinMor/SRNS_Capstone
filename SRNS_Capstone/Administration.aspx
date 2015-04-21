<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Capstone.Master" CodeBehind="Administration.aspx.cs" Inherits="SRNS_Capstone.Administration" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="row">
            <div class="col-lg-4 col-lg-offset-4">
                <asp:Panel runat="server" ID="pnlError" Visible="false">
                    <div class="alert alert-danger" role="alert" style="margin-top: 10px;">
                        <h4>
                            <asp:Label runat="server" ID="lblError" Text="Changes not made"></asp:Label>
                        </h4>
                    </div>
                </asp:Panel>
                <asp:Panel runat="server" ID="pnlSuccess" Visible="false">
                    <div class="alert alert-success" role="alert" style="margin-top: 10px;">
                        <h4>
                            <asp:Label runat="server" ID="lblSuccess" Text="Database Changes Successful"></asp:Label>
                        </h4>
                    </div>
                </asp:Panel>
            </div>
        </div>
        <br />
        <asp:Panel runat="server" ID="pnlAdminOptions" Visible="True">
            <div class="container">
                <asp:Button runat="server" ID="btnAdmins" CssClass="btn btn-default btn-block btn-lg" Text="Add/Edit Administrators" OnClick="btnAdmins_OnClick" />
                <br />
                <asp:Button runat="server" ID="btnSpeedChartAdmin" CssClass="btn btn-block btn-default btn-lg" Text="Update Speedchart List" OnClick="btnSpeedChartAdmin_OnClick"></asp:Button>
            </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlSelection" Visible="false">
            <div class="col-lg-12 col-md-12 text-center">
                <h1 style="margin-top: 5vh;">Administrator Management</h1>
                <div class="row">
                    <h3>Select Administrator to Edit</h3>
                    <div class="col-lg-4 col-lg-offset-4 col-md-4 col-md-offset-4">
                        <asp:DropDownList runat="server" ID="ddlUserSelect" CssClass="form-control" Style="width: 100%; max-width: 100%;" AutoPostBack="True" OnSelectedIndexChanged="ddlUserSelect_SelectedIndexChanged">
                            <asp:ListItem Enabled="true" Text="" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row text-center">
                    <h3>or</h3>
                </div>
                <div class="col-lg-4 col-lg-offset-4 col-md-4 col-md-offset-4">
                    <asp:Button runat="server" ID="btnAddUser" class="btn btn-lg btn-block btn-default" Text="Add Administrator" OnClick="btnAddUser_Click"></asp:Button>
                </div>
                <div class="row text-center">
                    <div class="col-lg-4 col-lg-offset-4 col-md-4 col-md-offset-4">
                        <br/>
                        <br/>
                        <asp:Button runat="server" ID="btnBack2" OnClick="btnBack_OnClick" CssClass="btn btn-default" Text="Back"/>
                    <div>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlForm" Visible="false">
            <div class="container">
                <div class="row hidden-sm hidden-xs">
                    <div class="col-lg-4 col-md-4">
                        <asp:Label runat="server" ID="lblFirstName">First Name</asp:Label>
                    </div>
                    <div class="col-lg-4 col-md-4">
                        <asp:Label runat="server" ID="lblLastName">Last Name</asp:Label>
                    </div>
                    <div class="col-lg-4 col-md-4">
                        <asp:Label runat="server" ID="lblUsername">Username</asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-4 col-md-4">
                        <asp:TextBox CssClass="form-control" placeholder="First Name" runat="server" ID="txtFirstName" autocomplete="off" TabIndex="1"></asp:TextBox>
                    </div>
                    <div class="col-lg-4 col-md-4">
                        <asp:TextBox CssClass="form-control" placeholder="Last Name" runat="server" ID="txtLastName" autocomplete="off" TabIndex="2"></asp:TextBox>
                    </div>
                    <div class="col-lg-4 col-md-4">
                        <asp:TextBox CssClass="form-control" placeholder="Username" runat="server" ID="txtLoginID" autocomplete="off" TabIndex="3"></asp:TextBox>
                    </div>
                </div>
                <br />
                <div class="row hidden-sm hidden-xs">
                    <div class="col-lg-4 col-md-4">
                        <asp:Label runat="server" ID="lblPassword">Password</asp:Label>
                    </div>
                </div>
                <div class="row">
                    <%--<div class="col-lg-4 col-md-4 col-sm-12">
                        <div class="row">
                            <label style="margin-left:52px;">This user is a Manager &nbsp;</label>
                            <asp:RadioButton runat="server" type="radio" name="options" id="rdManagerTrue" Text="Yes" OnCheckedChanged="rdManagerTrue_OnCheckedChanged" AutoPostBack="true" GroupName="manager"  TabIndex="4" />
                            <asp:RadioButton runat="server" type="radio" name="options" id="rdManagerFalse" Text="No" OnCheckedChanged="rdManagerTrue_OnCheckedChanged" AutoPostBack="true" GroupName="manager"  TabIndex="5" />
                        </div>
                        <br />
                        <div class="row">
                            <label style="margin-left:15px;">This user is an Administrator &nbsp;</label>
                            <asp:RadioButton runat="server" type="radio" name="options" id="rdAdminTrue" Text="Yes" GroupName="admin" TabIndex="6" />
                            <asp:RadioButton runat="server" type="radio" name="options" id="rdAdminFalse" Text="No" GroupName="admin" TabIndex="7" />
                        </div>
                    </div>--%>
                    <div class="col-lg-4 col-md-4">
                        <asp:TextBox CssClass="form-control" placeholder="Password" runat="server" ID="txtPassword" autocomplete="off" TabIndex="9"></asp:TextBox>
                        <span runat="server" id="labelPasswordInfo" visible="false" style="font-size: x-small; color: #051874;">Leaving the password field blank will keep the administrator's current password</span>
                    </div>
                </div>
                <br />
                <%--<asp:Panel runat="server" ID="pnlAccessControl" Visible="true">
                    <div class="row text-center">
                        <h3>Access Control</h3>
                        <a class="btn btn-sm btn-default" onclick="checkAllReports();" tabindex="10">Check All Reports</a>
                    </div>
                    <br />
                    <div class="row text-center">
                        <div class="col-lg-12 col-md-12">
                            <label class="checkbox-inline">
                              <asp:CheckBox runat="server" type="checkbox" id="chkRequests" value="option1"  TabIndex="11" /> Requests
                            </label>
                            <label class="checkbox-inline">
                              <asp:CheckBox runat="server" type="checkbox" id="chkAddLicense" value="option2" TabIndex="12" />Add License
                            </label>
                            <label class="checkbox-inline">
                              <asp:CheckBox runat="server" type="checkbox" id="chkLicenseCount" value="option3" TabIndex="13" />License Count Report
                            </label>
                            <label class="checkbox-inline">
                              <asp:CheckBox runat="server" type="checkbox" id="chkAvailableLicense" value="option3" TabIndex="14" />Available Licenses Report
                            </label>
                        </div>
                    </div>
                    <br />
                    <div class="row text-center">
                        <label class="checkbox-inline">
                            <asp:CheckBox runat="server" type="checkbox" id="chkManagerLicenseHolders" value="option3" TabIndex="15" />Managers License Holders Report
                        </label>
                        <label class="checkbox-inline">
                            <asp:CheckBox runat="server" type="checkbox" id="chkLicensesExpiring" value="option3" TabIndex="16" />Licenses Expiring Report
                        </label>
                        <label class="checkbox-inline">
                            <asp:CheckBox runat="server" type="checkbox" id="chkPendingChargebacks" value="option3" TabIndex="17" />Pending Chargebacks Report
                        </label>
                    </div>
                </asp:Panel>--%>
                <br />
                <div class="row">
                    <div class="col-lg-2 col-md-2 col-lg-offset-5 text-center">
                        <asp:Panel runat="server" ID="pnlBtnAddUser">
                            <asp:Button CssClass="btn btn-lg btn-primary" runat="server" ID="btnNewUser" Text="Add Administrator" OnClick="btnNewUser_Click" TabIndex="18" />
                        </asp:Panel>
                        <asp:Panel runat="server" ID="pnlBtnUpdateUser">
                            <asp:Button CssClass="btn btn-lg btn-primary" runat="server" ID="btnUpdateUser" Text="Update Administrator" OnClick="btnUpdateUser_Click" TabIndex="19" />
                            <br />
                            <br />
                            <a class="btn btn-sm btn-danger" id="btnDeleteUser" onclick="if(confirm('Are you sure you wish to delete this administrator?')){ deleteThisUser(); }" tabindex="19">Delete Administrator</a>
                        </asp:Panel>
                        <br />
                        <a onclick="cancelNewUser();" tabindex="20">Cancel</a>
                    </div>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlSpeedchart" runat="server" Visible="false">
            <div class="container">
                <h2 class="text-center">Upload Speedchart List</h2>
                <asp:Label runat="server" ID="lblSpeedchartHeader" Text="Upload .CSV containing latest speedcharts"></asp:Label>
                <div class="fileinput fileinput-new input-group" data-provides="fileinput" style="margin-top: 5px;">
                    <div class="form-control" data-trigger="fileinput">
                        <i class="glyphicon glyphicon-file fileinput-exists"></i>
                        <span class="fileinput-filename"></span>
                    </div>
                    <span class="input-group-addon btn btn-default btn-file">
                        <span class="fileinput-new">Select file</span>
                        <span class="fileinput-exists">Change</span>
                        <asp:FileUpload runat="server" ID="fileUpload" />
                    </span>
                    <a href="#" class="input-group-addon btn btn-default fileinput-exists" data-dismiss="fileinput">Remove</a>
                </div>
                <br />
                Note: All current speedcharts are replaced by a .CSV upload<br/>
                <asp:Button runat="server" ID="btnUploadSpeedcharts" OnClick="btnUploadSpeedcharts_OnClick" Text="Upload Speedcharts" CssClass="btn btn-default"/>
                <br/>
                <div class="row text-center">
                <h2>Add/Remove single speedchart</h2>
                <asp:TextBox runat="server" ID="txtSpeedChart" CssClass="form-control"></asp:TextBox>
                <br />
                <asp:Button runat="server" ID="btnAddSpeedchart" OnClick="btnSingleSpeedchart_OnClick" CssClass="btn btn-default" Text="Add Speedchart"/> &nbsp; &nbsp; &nbsp; 
                    <asp:Button runat="server" ID="btnRemoveSpeedchart" OnClick="btnRemoveSpeedchart_OnClick" CssClass="btn btn-default" Text="Remove Speedchart"/>
                </div>
                <br/>
                <asp:Button runat="server" ID="btnBack" OnClick="btnBack_OnClick" Text="Back" />
            </div>
        </asp:Panel>
    </div>

    <asp:HiddenField runat="server" ID="hdnUserToUpdate" />
    <asp:HiddenField runat="server" ID="hdnIsPostBack" />
    <asp:HiddenField runat="server" ID="hdnIsAdmin" />
    <asp:HiddenField runat="server" ID="hdnIsManager" />
    <asp:Button href="#" runat="server" ID="btnCancelNewUser" Style="display: none;" OnClick="btnCancelNewUser_Click" />
    <asp:Button href="#" runat="server" ID="btnHdnDelete" Style="display: none;" OnClick="btnDeleteUser_OnClick" />

    <script type="text/javascript">
    var isPostback = $("#<%=hdnIsPostBack.ClientID%>").val();

    if (isPostback === "False") {
        $('#btnManagerFalse').click();
        $('#btnAdminFalse').click();
        $("#<%=hdnIsPostBack.ClientID%>").val('true');
        $("#<%=hdnIsAdmin.ClientID%>").val('false');
        $("#<%=hdnIsManager.ClientID%>").val('false');
    } else {
        $("#<%=hdnIsAdmin.ClientID%>").val() === "true" ? $('#btnAdminTrue').click() : $('#btnAdminFalse').click();
        $("#<%=hdnIsManager.ClientID%>").val() === "true" ? $('#btnManagerTrue').click() : $('#btnManagerFalse').click();
    }

    var cancelNewUser = (function () {
        return function () {
            $("#<%=btnCancelNewUser.ClientID%>").click();
        }
    })();

    var deleteThisUser = (function () {
        return function () {
            $("#<%=btnHdnDelete.ClientID%>").click();
        }
    })();

    $("#btnManagerTrue").keypress(function () {
        $('#rdManagerTrue').click();
    });

    $("#btnAdminTrue").keypress(function () {
        $('#rdAdminTrue').click();
    });

   <%-- var checkAllReports = (function () {

        return function() {
            $('#<%=chkPendingChargebacks.ClientID%>').attr('checked', true);
            $('#<%=chkAvailableLicense.ClientID%>').attr('checked', true);
            $('#<%=chkLicenseCount.ClientID%>').attr('checked', true);
            $('#<%=chkLicensesExpiring.ClientID%>').attr('checked', true);
            $('#<%=chkManagerLicenseHolders.ClientID%>').attr('checked', true);
        }

    })();

    var countChecked = (function() {
        return function() {
            var count = 0;
            count = $('#<%=chkPendingChargebacks.ClientID%>').is(':checked') ? count + 1 : count;
            count = $('#<%=chkAvailableLicense.ClientID%>').is(':checked') ? count + 1 : count;
            count = $('#<%=chkLicenseCount.ClientID%>').is(':checked') ? count + 1 : count;
            count = $('#<%=chkLicensesExpiring.ClientID%>').is(':checked') ? count + 1 : count;
            count = $('#<%=chkManagerLicenseHolders.ClientID%>').is(':checked') ? count + 1 : count;
            return count;
        }
    })();--%>
    </script>
</asp:Content>
