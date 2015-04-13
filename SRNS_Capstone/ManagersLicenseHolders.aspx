<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManagersLicenseHolders.aspx.cs" MasterPageFile="~/Capstone.Master" Inherits="SRNS_Capstone.ManagersLicenseHolders" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">


    <div class="container">
        <div class="row text-center">
            <h2>
                Licenses Held Under <asp:Label runat="server" ID="lblMangerNameHead"></asp:Label>
            </h2>
        </div>
        <div class="row text-center">
            <div class="col-lg-2 col-md-2">
                Sort by Software Provider Name
            <asp:CheckBox runat="server" ID="chkShowProvider" Checked="false" AutoPostBack="True" OnCheckedChanged="chkShowProvider_OnCheckedChanged" />
            </div>
            <div class="col-lg-8">
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
                        FieldHeaderStyle-CssClass="hdr"
                        CommandRowStyle-CssClass="ftr">
                        <Columns>
                            <asp:BoundColumn HeaderText="Provider" DataField="KeyHolder" />
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

