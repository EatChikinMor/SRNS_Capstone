<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PendingChargebacks.aspx.cs" MasterPageFile="~/Capstone.Master" Inherits="SRNS_Capstone.PendingChargebacks" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
    <style>
        
    </style>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">


    <div class="container">
        <div class="row text-center">
            <h2>
                Pending Chargebacks
            </h2>
        </div>
        <div class="row text-center">
<%--            <div class="col-lg-2 col-md-2">
                Sort by Software Provider Name
                <asp:CheckBox runat="server" ID="chkShowProvider" Checked="false" AutoPostBack="True" OnCheckedChanged="chkShowProvider_OnCheckedChanged" />
            </div>--%>
            <div class="col-lg-8 col-lg-offset-2">
                <h3>
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
                            <asp:BoundColumn HeaderText="Speedchart" DataField="Speedchart" />
                            <asp:BoundColumn HeaderText="Software" DataField="SoftwareName" />
                            <asp:BoundColumn HeaderText="License Key" DataField="LicenseKey" />
                            <asp:BoundColumn HeaderText="Key Holder" DataField="KeyHolder" />
                            <asp:BoundColumn HeaderText="Key Manager" DataField="KeyManager" />
                            <asp:BoundColumn HeaderText="License Cost" DataField="LicenseCost" />
                            <asp:BoundColumn HeaderText="Provider" DataField="Organization" />
                        </Columns>
                    </asp:DataGrid>
                </div>
            </asp:Panel>
        </div>
    </div>
</asp:Content>