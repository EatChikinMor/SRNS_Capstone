<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Capstone.Master" CodeBehind="AvailableCount.aspx.cs" Inherits="SRNS_Capstone.AvailableCount" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="row text-center">
        <h3>
            Count:
            <asp:Label runat="server" ID="lblSoftwareCount" Visible="true"></asp:Label>
        </h3>
    </div>
    <div class="container">
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
                    CommandRowStyle-CssClass="ftr" >
                    <Columns>
                        <asp:BoundColumn HeaderText="Provider" DataField="Organization"/>
                        <asp:BoundColumn HeaderText="Owner" DataField="SoftwareName"/>
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
