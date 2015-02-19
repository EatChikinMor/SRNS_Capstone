<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Capstone.Master" CodeBehind="AddLicense.aspx.cs" Inherits="SRNS_Capstone.AddLicense" %>

<asp:Content ContentPlaceHolderID="head" runat="server">

</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-lg-12 col-md-12 text-center">
                <h1 style="margin-top:10vh;">Add new license for: </h1>
                    <div class="col-lg-4 col-lg-offset-4">
                        <asp:DropDownList runat="server" ID="ddlSoftwareSelect" CssClass="form-control" style="width:100%; max-width:100%;">
                            <asp:ListItem Text="Placeholder1"></asp:ListItem>
                            <asp:ListItem Text="Placeholder2"></asp:ListItem>
                            <asp:ListItem Text="Placeholder3"></asp:ListItem>
                            <asp:ListItem Text="Placeholder4"></asp:ListItem>
                            <asp:ListItem Text="Placeholder5"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                <hr />
            </div>
        </div>
    </div>
</asp:Content>
