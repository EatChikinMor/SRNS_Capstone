<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Capstone.Master" CodeBehind="Administration.aspx.cs" Inherits="SRNS_Capstone.Administration" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <asp:Panel runat="server" ID="pnlSelection" Visible="false">
            <div class="col-lg-12 col-md-12 text-center">
                <h1 style="margin-top: 10vh;">User Management</h1>
                <div class="row">
                    <h3>Select User to Edit</h3>
                    <div class="col-lg-4 col-lg-offset-4">
                        <asp:DropDownList runat="server" ID="ddlUserSelect" CssClass="form-control" Style="width: 100%; max-width: 100%;" OnSelectedIndexChanged="ddlUserSelect_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row center">
                    <h3>or</h3>
                </div>
                <div class="col-lg-4 col-lg-offset-4">
                    <asp:Button runat="server" ID="btnAddUser" class="btn btn-lg btn-block btn-default" Text="Add User" OnClick="btnAddUser_Click"></asp:Button>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlForm">
            <div class="container">
                <div class="row">
                    <div class="col-lg-4">
                        <asp:label runat="server" ID="lblFirstName">First Name</asp:label>
                    </div>
                    <div class="col-lg-4">
                        <asp:label runat="server" ID="lblLastName">Last Name</asp:label>
                    </div>
                    <div class="col-lg-4">
                        <asp:label runat="server" ID="lblUsername">Username</asp:label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-4">
                        <asp:TextBox CssClass="form-control" placeholder="First Name" runat="server" ID="txtFirstName"></asp:TextBox>
                    </div>
                    <div class="col-lg-4">
                        <asp:TextBox CssClass="form-control" placeholder="Last Name" runat="server" ID="txtLastName"></asp:TextBox>
                    </div>
                    <div class="col-lg-4">
                        <asp:TextBox CssClass="form-control" placeholder="Username" runat="server" ID="txtLoginID"></asp:TextBox>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-lg-4">
                        <label></label>
                    </div>
                    <div class="col-lg-4">
                        <asp:Label runat="server" ID="lblManager">Manager</asp:Label>
                    </div>
                    <div class="col-lg-4">
                        <asp:Label runat="server" ID="lblPassword">Password</asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-4">
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
                    <div class="col-lg-4">
                        <asp:DropDownList runat="server" ID="ddlManagers" CssClass="form-control" style="width:100%; max-width:100%;" OnSelectedIndexChanged="ddlManagers_SelectedIndexChanged">
                            <asp:ListItem Enabled="true" Text="Manager Select"></asp:ListItem>
                            <asp:ListItem Enabled="true" Text="Manager Select 1"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-lg-4">
                        <asp:TextBox CssClass="form-control" placeholder="Password" runat="server" ID="txtPassword"></asp:TextBox>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-lg-12">
                        <asp:Button CssClass="btn btn-lg btn-primary" runat="server" ID="btnSubmitUser" />
                    </div>
                </div>
            </div>
        </asp:Panel>
    </div>

    <asp:HiddenField runat="server" id="hdnIsPostBack" />
    <asp:HiddenField runat="server" id="hdnIsAdmin" />
    <asp:HiddenField runat="server" id="hdnIsManager" />
    
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

        //Admin.Click();
        //Manager.Click();
    }
</script>

</asp:Content>