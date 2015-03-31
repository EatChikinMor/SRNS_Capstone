<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Capstone.Master" CodeBehind="CountBySoftware.aspx.cs" Inherits="SRNS_Capstone.Reports.CountBySoftware" %>

<asp:Content ContentPlaceHolderID="head" runat="server">

</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    
    <style>
        .dGrid { background-color: #fff; margin: 5px 0 10px 0; border: solid 1px #525252; border-collapse:collapse; } 
        .dGrid td { padding: 2px; border: solid 1px #c1c1c1; color: #717171; background: #fcfcfc url(assets/img/grd_alt.png) repeat-x top;} 
        .dGrid .hdr { padding: 4px 2px; color: #fff; background: #424242 url(assets/img/grd_head.png) repeat-x top; border-left: solid 1px #525252; font-size: 0.9em; } 
        .dGrid .ftr td { padding: 4px 2px; color: #fff; background: #424242 url(assets/img/grd_head.png) repeat-x top; border-left: solid 1px #525252; font-size: 0.9em; font-weight: bold;} 
        .dGrid .ftr a { color: #fff; text-decoration: none; } 
        .dGrid .ftr a:hover { color: #fff; text-decoration: none; }
    </style>

    <div class="container">
        <div class="row">
            <asp:Panel runat="server" ID="pnlSelect">
                <div class="col-lg-12 col-md-12 text-center">
                    <h1 style="margin-top:10vh;"> Software Count For: </h1>
                        <div class="col-lg-4 col-lg-offset-4">
                            <asp:DropDownList runat="server" ID="ddlSoftwareSelect" CssClass="form-control" style="width:100%; max-width:100%;" OnSelectedIndexChanged="ddlSoftwareSelect_OnSelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                        </div>
                    <hr />
                </div>
            </asp:Panel>
            <asp:Panel runat="server" ID="pnlData">
                <div class="row">
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
                                <asp:BoundColumn HeaderText="Owner" DataField="KeyOwnerID"/>
                                <asp:BoundColumn HeaderText="Expires" DataField="ExpirationDate" />
                                <asp:BoundColumn HeaderText="Key" DataField="LicenseKey" />
                            </Columns>
                        </asp:DataGrid>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>
</asp:Content>
