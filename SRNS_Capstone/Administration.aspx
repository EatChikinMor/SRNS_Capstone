<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Capstone.Master" CodeBehind="Administration.aspx.cs" Inherits="SRNS_Capstone.Administration" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <asp:Panel runat="server" ID="pnlSelection" Visible="true">
            <asp:Panel runat="server" ID="pnlSuccess" Visible="false">
                    <div class="alert alert-success" role="alert" style="margin-top:10px;">
                        <h4>
                            <asp:Label runat="server" ID="lblSuccess" Text="Database Changes Successful"></asp:Label>
                        </h4>
                    </div>
            </asp:Panel>
            <div class="col-lg-12 col-md-12 text-center">
                <h1 style="margin-top: 10vh;">User Management</h1>
                <div class="row">
                    <h3>Select User to Edit</h3>
                    <div class="col-lg-4 col-lg-offset-4 col-md-4 col-md-offset-4">
                        <asp:DropDownList runat="server" ID="ddlUserSelect" CssClass="form-control" Style="width: 100%; max-width: 100%;" AutoPostBack="true" OnSelectedIndexChanged="ddlUserSelect_SelectedIndexChanged">
                            <asp:ListItem Enabled="true" Text="" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row center">
                    <h3>or</h3>
                </div>
                <div class="col-lg-4 col-lg-offset-4 col-md-4 col-md-offset-4">
                    <asp:Button runat="server" ID="btnAddUser" class="btn btn-lg btn-block btn-default" Text="Add User" OnClick="btnAddUser_Click"></asp:Button>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlForm" Visible="false">
            <div class="container">
                <div class="col-lg-4 col-lg-offset-4">
                    <asp:Panel runat="server" ID="pnlError" Visible="false">
                            <div class="alert alert-danger" role="alert" style="margin-top:10px;">
                                <h4>
                                    <asp:Label runat="server" ID="lblError" Text="Changes not made"></asp:Label>
                                </h4>
                            </div>
                    </asp:Panel>
                </div>
                <br />
                <div class="row hidden-sm hidden-xs">
                    <div class="col-lg-4 col-md-4">
                        <asp:label runat="server" ID="lblFirstName">First Name</asp:label>
                    </div>
                    <div class="col-lg-4 col-md-4">
                        <asp:label runat="server" ID="lblLastName">Last Name</asp:label>
                    </div>
                    <div class="col-lg-4 col-md-4">
                        <asp:label runat="server" ID="lblUsername">Username</asp:label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-4 col-md-4">
                        <asp:TextBox CssClass="form-control" placeholder="First Name" runat="server" ID="txtFirstName"></asp:TextBox>
                    </div>
                    <div class="col-lg-4 col-md-4">
                        <asp:TextBox CssClass="form-control" placeholder="Last Name" runat="server" ID="txtLastName"></asp:TextBox>
                    </div>
                    <div class="col-lg-4 col-md-4">
                        <asp:TextBox CssClass="form-control" placeholder="Username" runat="server" ID="txtLoginID"></asp:TextBox>
                    </div>
                </div>
                <br />
                <div class="row hidden-sm hidden-xs">
                    <div class="col-lg-4 col-md-4">
                        <label></label>
                    </div>
                    <div class="col-lg-4 col-md-4">
                        <asp:Label runat="server" ID="lblManager">Manager</asp:Label>
                    </div>
                    <div class="col-lg-4 col-md-4">
                        <asp:Label runat="server" ID="lblPassword">Password</asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-4 col-md-4 col-sm-12">
                        <div class="row">
                            <label style="margin-left:52px;">This user is a Manager &nbsp;</label>
                            <div class="btn-group" data-toggle="buttons">
                                <label id="btnManagerTrue" class="btn btn-primary">
                                    <asp:RadioButton runat="server" type="radio" name="options" id="managerTrue" />
                                    Yes
                                </label>
                                <label id="btnManagerFalse" class="btn btn-primary">
                                    <asp:RadioButton runat="server" type="radio" name="options" id="managerFalse" />
                                    No
                                </label>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <label style="margin-left:15px;">This user is an Administrator &nbsp;</label>
                            <div class="btn-group" data-toggle="buttons">
                                <label id="btnAdminTrue" class="btn btn-primary">
                                    <asp:RadioButton runat="server" type="radio" name="options" id="adminTrue" />
                                    Yes
                                </label>
                                <label id="btnAdminFalse" class="btn btn-primary">
                                    <asp:RadioButton runat="server" type="radio" name="options" id="adminFalse" />
                                    No
                                </label>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-4 col-md-4">
                        <div class="hidden-lg hidden-md col-sm-2 col-xs-5 text-center">
                            <label>Manager</label>
                        </div>
                        <div class="col-lg-12 col-md-12 col-sm-10 col-xs-7">
                            <asp:DropDownList runat="server" ID="ddlManagers" CssClass="form-control" style="width:100%; max-width:100%;" OnSelectedIndexChanged="ddlManagers_SelectedIndexChanged">
                                <asp:ListItem Enabled="true" Text=""></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-lg-4 col-md-4">
                        <asp:TextBox CssClass="form-control" placeholder="Password" runat="server" ID="txtPassword"></asp:TextBox>
                        <span runat="server" id="labelPasswordInfo" visible="false" style="font-size:x-small; color:#051874;">Leaving the password field blank will keep the user's current password</span>
                    </div>
                </div>
                <br />
                <div class="row text-center">
                    <h3>Access Control</h3>
                </div>
                <br />
                <div class="row text-center">
                    <div class="col-lg-12 col-md-12 center">
                        <label class="checkbox-inline">
                          <asp:CheckBox runat="server" type="checkbox" id="chkRequests" value="option1" /> Requests
                        </label>
                        <label class="checkbox-inline">
                          <asp:CheckBox runat="server" type="checkbox" id="chkAddLicense" value="option2" />Add License
                        </label>
                        <label class="checkbox-inline">
                          <asp:CheckBox runat="server" type="checkbox" id="chkLicenseCount" value="option3" />License Count Report
                        </label>
                        <label class="checkbox-inline">
                          <asp:CheckBox runat="server" type="checkbox" id="chkAvailableLicense" value="option3" />Available Licenses Report
                        </label>
                    </div>
                </div>
                <br />
                <div class="row text-center">
                    <label class="checkbox-inline">
                        <asp:CheckBox runat="server" type="checkbox" id="chkManagerLicenseHolders" value="option3" />Managers License Holders Report
                    </label>
                    <label class="checkbox-inline">
                        <asp:CheckBox runat="server" type="checkbox" id="chkLicensesExpiring" value="option3" />Licenses Expiring Report
                    </label>
                    <label class="checkbox-inline">
                        <asp:CheckBox runat="server" type="checkbox" id="chkPendingChargebacks" value="option3" />Pending Chargebacks Report
                    </label>
                </div>
                <br />
                <div class="row">
                    <div class="col-lg-2 col-md-2 col-lg-offset-5 text-center">
                        <asp:Panel runat="server" ID="pnlBtnAddUser">
                            <asp:Button CssClass="btn btn-lg btn-primary" runat="server" ID="btnNewUser" Text="Add User" OnClick="btnNewUser_Click" />
                        </asp:Panel>
                        <asp:Panel runat="server" ID="pnlBtnUpdateUser">
                            <asp:Button CssClass="btn btn-lg btn-primary" runat="server" ID="btnUpdateUser" Text="Update User" OnClick="btnUpdateUser_Click"/>
                        </asp:Panel>
                        <br />
                        <a onclick="cancelNewUser();">Cancel</a>
                    </div>
                </div>
            </div>
        </asp:Panel>
    </div>

    <asp:HiddenField runat="server" id="hdnIsPostBack" />
    <asp:HiddenField runat="server" id="hdnIsAdmin" />
    <asp:HiddenField runat="server" id="hdnIsManager" />
    <asp:Button href="#" runat="server" ID="btnCancelNewUser" style="display:none;" OnClick="btnCancelNewUser_Click" />
    
<script type="text/javascript">
    <%--$('#btnManagerFalse').click(function () {
        enableDropDown(false);
    });

    $('#btnManagerTrue').click(function () {
        enableDropDown(true);
    });

    var enableDropDown = (function () {
        return function (status) {
            $('#<%=ddlManagers.ClientID%>').attr('disabled', status);
        }
    })();--%>

    var isPostback = $("#<%=hdnIsPostBack.ClientID%>").val();

    if (isPostback === "False") {
        $('#btnManagerFalse').click();
        $('#btnAdminFalse').click();
        $("#<%=hdnIsPostBack%>").val('true');
    } else {
        var Admin = $("#<%=hdnIsAdmin.ClientID%>").val() === "true" ? $('#btnAdminTrue').click() : $('#btnAdminFalse').click()
        var Manager = $("#<%=hdnIsManager.ClientID%>").val() === "true" ? $('#btnManagerTrue').click() : $('#btnManagerFalse').click()
    }

    var cancelNewUser = (function () {
        return function () {
            $("#<%=btnCancelNewUser.ClientID%>").click();
        }
    })();
</script>

</asp:Content>