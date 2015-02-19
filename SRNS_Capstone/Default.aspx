<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SRNS_Capstone._Default" %>

<!DOCTYPE html>

<link type="text/css" rel="stylesheet" href="assets/css/bootstrap/bootstrap.css" />
<link type="text/css" rel="stylesheet" href="assets/css/Main.css" />

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="homeIMG" class="container" style="align-items:center;">
            <div class="row text-center">
                <div class="col-lg-6 col-lg-offset-3" style="margin-top:25vh">
                    <img src="assets/img/hd_srns-mast01-cut-2.svg" alt="SRNS License Manager" style="width:100%;"/>
                    <br />
                    <span style="font-family:Arial; font-size:25px;"><strong>Software License Manager</strong></span>
                </div>
            </div>
            <div class="row text-center">
                <div class="col-lg-4 col-lg-offset-4">
                    <asp:TextBox CssClass="form-control text-center" placeholder="Username" runat="server" ID="txtUsername"></asp:TextBox>
                    <asp:TextBox CssClass="form-control text-center" placeholder="Password" runat="server" ID="txtPassword" TextMode="Password" Style="margin-top:7px;"></asp:TextBox>
                    <asp:Button CssClass="btn btn-default btn-block" runat="server" ID="btnLogin" Text="Login" Style="margin-top:7px;" OnClick="btnLogin_Click" />
                    <a href="#">Forgot your password?</a>
                </div>
            </div>
        </div>
    </form>
</body>
</html>