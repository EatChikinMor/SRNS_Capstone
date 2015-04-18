<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Capstone.Master" CodeBehind="Requests.aspx.cs" Inherits="SRNS_Capstone.Home" %>

<asp:Content ContentPlaceHolderID="head" runat="server">

</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <asp:Panel runat="server" ID="pnlSuccess" Visible="false">
            <div class="row">
                <div class="col-lg-6 col-lg-offset-3">
                    <div class="alert alert-success" role="alert" style="margin-top:10px;">
                        <h4>
                            <asp:Label runat="server" ID="lblSuccess" Text="Database Changes Successful"></asp:Label>
                        </h4>
                    </div>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlError" Visible="false">
                <div class="alert alert-danger" role="alert" style="margin-top:10px;">
                    <h4>
                        <asp:Label runat="server" ID="lblError" Text="Changes not made"></asp:Label>
                    </h4>
                </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlInputRequest" Visible="False" >
            <div class="row text-center">
                <h1><asp:Label runat="server" ID="lblInputRequest" Text="Request"></asp:Label></h1>
            </div>
            <div class="row text-center">
                <div class="col-lg-12">
                    <h3>
                        Name: <asp:Label runat="server" ID="lblName"></asp:Label>
                    </h3>
                </div>
                <div class="col-lg-12">
                    <h3>
                        Login: 
                        <asp:Label runat="server" ID="lblLogin"></asp:Label>
                    </h3>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-6 col-lg-offset-3 text-left">
                    <h4>
                        Requested Software
                        <asp:TextBox runat="server" ID="txtRequestTitle" CssClass="form-control"></asp:TextBox>
                    </h4>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-6 col-lg-offset-3 text-left">
                    <h4>
                        Request
                        <asp:TextBox runat="server" ID="txtRequest" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                    </h4>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-6 col-lg-offset-3 text-center">
                    <asp:Button runat="server" ID="btnSubmit" CssClass="btn btn-lg btn-primary" Text="Submit" OnClick="btnSubmit_OnClick"/>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlPendingRequests" Visible="False" >
            <div class="row text-center">
                <h1><asp:Label runat="server" ID="lblPendingRequests"></asp:Label></h1>
            </div>
            <div id="PendingRequests">
                <div class="row">
                    <div class="col-lg-8 col-lg-offset-2">
                        <asp:DataGrid
                            runat="server"
                            ID="gridCounts"
                            AutoGenerateColumns="False"
                            Width="100%"
                            GridLines="None"
                            CssClass="dGrid"
                            ShowHeader="True"
                            HeaderStyle-CssClass="hdr"
                            FooterStyle-CssClass="ftr"
                            OnItemCommand="gridCounts_OnItemCommand">
                            <Columns>
                                <asp:BoundColumn DataField="ID" Visible="False"/>
                                <asp:BoundColumn HeaderText="Requested Software" DataField="RequestTitle" />
                                <asp:BoundColumn HeaderText="Request" DataField="Request" ItemStyle-Width="40%" />
                                <asp:BoundColumn HeaderText="Request Date" DataField="RequestDate" />
                                <asp:BoundColumn HeaderText="User" DataField="RequestingUser" />
                                <asp:BoundColumn HeaderText="User Login" DataField="RequestingUserLogin" />
                                <asp:ButtonColumn CommandName="Delete" DataTextField="Delete"/>
                            </Columns>
                        </asp:DataGrid>
                    </div>
                </div>
            </div>
        </asp:Panel>
    </div>
    
    <script>
        $('#<%=gridCounts.ClientID%>').click(function (e) {
            var txt = $(e.target).text();
            if (txt === 'Delete') {
                return confirm("Are you sure you want to delete this request?");
            }
            return null;
        });
    </script>

</asp:Content>
