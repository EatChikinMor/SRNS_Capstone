<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SRNS_Capstone._Default" %>

<!DOCTYPE html>

<link type="text/css" rel="stylesheet" href="assets/css/bootstrap/bootstrap.css" />
<link type="text/css" rel="stylesheet" href="assets/css/Main.css" />

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <asp:Panel runat="server" ID="pnlHomeCss" Visible="true">
    <style>
        .homeIMG {
            -webkit-animation: fadein 2s; /* Safari, Chrome and Opera > 12.1 */
               -moz-animation: fadein 2s; /* Firefox < 16 */
                -ms-animation: fadein 2s; /* Internet Explorer */
                 -o-animation: fadein 2s; /* Opera < 12.1 */
                    animation: fadein 2s;
        }
        @-webkit-keyframes fadein {
            0% {
                top: 20px;
                opacity: 0;
            }
            50% {
                top: 20px;
                opacity: 0;
            }
            100% {
                top: 0;
                opacity: 1;
            }
        }
        @-moz-keyframes fadein {
            0% {
                top: 20px;
                opacity: 0;
            }
            50% {
                top: 20px;
                opacity: 0;
            }
            100% {
                top: 0;
                opacity: 1;
            }
        }
        @-o-keyframes fadein {
            0% {
                top: 20px;
                opacity: 0;
            }
            50% {
                top: 20px;
                opacity: 0;
            }
            100% {
                top: 0;
                opacity: 1;
            }
        }
        @keyframes fadein {
            0% {
                top: 20px;
                opacity: 0;
            }
            50% {
                top: 20px;
                opacity: 0;
            }
            100% {
                top: 0;
                opacity: 1;
            }
        }
    </style>
    </asp:Panel>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server" ID="scriptManager"></asp:ScriptManager>
        <asp:Panel runat="server" ID="pnlMain" CssClass="homeIMG">
            <div class="container" style="align-items:center;">
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
                        <asp:Label runat="server" ID="lblDomain"></asp:Label>
                        <asp:HyperLink runat="server" ID="lnkDomain" style="cursor: pointer" onclick="$('#domainEntry').slideToggle('slow');" Text="Change?"/>
                        <div id="domainEntry" class="" style="display: none;">
                            <asp:TextBox CssClass="form-control text-center" runat="server" ID="txtDomain"></asp:TextBox>
                            <asp:Button runat="server" CssClass="btn btn-primary btn-block" ID="btnSubmitDomain" Style="margin-top:7px;" OnClick="btnSubmitDomain_OnClick" Text="Update Domain"/>
                        </div>
                        <asp:Panel runat="server" ID="pnlError" Visible="false">
                            <div class="alert alert-danger" role="alert" style="margin-top:10px;">
                                <h4>Invalid Username or password</h4>
                            </div>
                        </asp:Panel>
                        <%--<a href="#">Forgot your password?</a>--%>
                        <%--<a class="btn btn-lg btn-default" onclick="showInvalid(); return false;"></a>--%>
                    </div>
                </div>
            </div>
        </asp:Panel>
    </form>
</body>
    
<script src="assets/js/jquery-2.3.1.js"></script>

</html>