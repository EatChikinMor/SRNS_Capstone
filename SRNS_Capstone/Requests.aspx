<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Capstone.Master" CodeBehind="Requests.aspx.cs" Inherits="SRNS_Capstone.Home" %>

<asp:Content ContentPlaceHolderID="head" runat="server">

</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel runat="server" ID="pnlPendingRequests" Visible="false" >
        <div class="row text-center">
            <h1><asp:Label runat="server" ID="lblPendingRequests"></asp:Label></h1>
        </div>
    </asp:Panel>

</asp:Content>
