<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Capstone.Master" CodeBehind="LicensesExpiring.aspx.cs" Inherits="SRNS_Capstone.LicensesExpiring" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">


    <div class="container">
        <asp:Panel runat="server" ID="pnlError" Visible="false">
                <div class="alert alert-danger" role="alert" style="margin-top:10px;">
                    <h4>
                        <asp:Label runat="server" ID="lblError" Text="Changes not made"></asp:Label>
                    </h4>
                </div>
        </asp:Panel>
        <asp:panel runat="server" ID="pnlExpiringLicenses" Visible="True">
            <div class="row text-center">
                <h2>
                    Licenses Expiring in 
                    <br/>
                    <asp:TextBox runat="server" ID="txtCount" Text="3" Width="50px"></asp:TextBox> months
                </h2>
                <div class="row">
                    <asp:Button runat="server" ID="btnSubmit" CssClass="btn btn-primary btn-sm" Text="Submit" OnClick="OnClick"/>
                    &nbsp;
                    <asp:Button runat="server" ID="btnViewExpired" CssClass="btn btn-danger btn-sm" Text="View Expired" OnClick="btnViewExpired_OnClick"/>
                </div>
            </div>
        </asp:panel>
        <asp:panel runat="server" ID="pnlExpired" Visible="False">
            <div class="row text-center">
                <h2>
                    Expired Licenses
                </h2>
                <div class="row">
                    <asp:Button runat="server" ID="btnExpiring" CssClass="btn btn-danger btn-sm" Text="View Expiring" OnClick="btnExpiring_OnClick"/>
                </div>
            </div>
        </asp:panel>
        <div class="row text-center">
            <div class="col-lg-2 col-md-2">
                Sort by Software Provider Name
            <asp:CheckBox runat="server" ID="chkShowProvider" Checked="false" AutoPostBack="True" OnCheckedChanged="chkShowProvider_OnCheckedChanged" />
            </div>
            <div class="col-lg-8">
                <h3 class="pull-left">
                Count:
                <asp:Label runat="server" ID="lblSoftwareCount" Visible="true"></asp:Label>
                </h3>
            </div>
        </div>
        <div class="row">
            <asp:Panel runat="server" ID="pnlNotExpired">
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
                        FooterStyle-CssClass="ftr">
                        <Columns>
                            <asp:BoundColumn HeaderText="Provider" DataField="Organization" />
                            <asp:BoundColumn HeaderText="Software" DataField="SoftwareName" />
                            <asp:BoundColumn HeaderText="Expires" DataField="ExpirationDate" />
                            <asp:BoundColumn HeaderText="Key" DataField="LicenseKey" />
                            <asp:BoundColumn HeaderText="Speedchart" DataField="Speedchart" />
                        </Columns>
                    </asp:DataGrid>
                </div>
            </asp:Panel>
        </div>
    </div>
</asp:Content>

