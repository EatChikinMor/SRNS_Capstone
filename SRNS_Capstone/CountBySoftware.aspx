<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Capstone.Master" CodeBehind="CountBySoftware.aspx.cs" Inherits="SRNS_Capstone.Reports.CountBySoftware" %>

<asp:Content ContentPlaceHolderID="head" runat="server">

</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    
    <style>
        .dGrid {
            background-color: #fff;
            margin: 5px 0 10px 0;
            border: solid 1px #525252;
            border-collapse: collapse;
        }

            .dGrid td {
                padding: 2px;
                border: solid 1px #c1c1c1;
                color: #717171;
                background: #fcfcfc url(assets/img/grd_alt.png) repeat-x top;
            }

            .dGrid .hdr {
                padding: 4px 2px;
                color: #fff;
                background: #424242 url(assets/img/grd_head.png) repeat-x top;
                border-left: solid 1px #525252;
                font-size: 0.9em;
            }

            .dGrid .ftr td {
                padding: 4px 2px;
                color: #fff;
                background: #424242 url(assets/img/grd_head.png) repeat-x top;
                border-left: solid 1px #525252;
                font-size: 0.9em;
                font-weight: bold;
            }

            .dGrid .ftr a {
                color: #fff;
                text-decoration: none;
            }

                .dGrid .ftr a:hover {
                    color: #fff;
                    text-decoration: none;
                }
    </style>

    <div class="container">
        <div class="row"> <%--Permitting Time - Alphabet Subsections - http://stackoverflow.com/questions/2923137/repeater-in-repeater 
                            IE -  Heading Label for "A" then all Software provided by "A" Companies then Adobe, AMD etc. THen another Letter header "B"  --%>
            <asp:Panel runat="server" ID="pnlSelect">
                <div class="row text-center">
                    <h2>
                        License Count By Software
                    </h2>
                </div>
                <div class="col-lg-2 col-md-2">
                    Sort by Software Provider Name
                    <asp:CheckBox runat="server" ID="chkShowProvider" Checked="false" AutoPostBack="True" OnCheckedChanged="chkShowProvider_OnCheckedChanged"/>
                </div>
                <asp:Repeater runat="server" ID="rptrSoftList">
                    <ItemTemplate>
                        <div class="row">
                            <div class="col-lg-8 col-lg-offset-2 col-md-8 col-md-offset-2 col-sm-12 col-xs-12">
                                <h4>
                                    <asp:HyperLink runat="server" ID="lnkSoftwareGrid" NavigateUrl='<%# Eval("[SoftCode]", "~/CountBySoftware.aspx?SoftCode={0}") %>' Text='<%# Eval("[SoftName]") %>'></asp:HyperLink>
                                    <div class="pull-right">
                                        <b>Count:<asp:Label runat="server" ID="lblSoftCount" Text='<%# Eval("[LicCount]") %>'></asp:Label></b>
                                    <div/>
                                    <br/>
                                </h4>
                            </div>
                        </div>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <div class="row">
                            <div class="col-lg-8 col-lg-offset-2 col-md-8 col-md-offset-2 col-sm-12  col-xs-12" style="background-color: #FFF">
                                <h4>
                                    <asp:HyperLink runat="server" ID="lnkSoftwareGrid" NavigateUrl='<%# Eval("[SoftCode]", "~/CountBySoftware.aspx?SoftCode={0}") %>' Text='<%# Eval("[SoftName]") %>'></asp:HyperLink>
                                    <div class="pull-right">
                                        <b>Count:<asp:Label runat="server" ID="lblSoftCount" Text='<%# Eval("[LicCount]") %>'></asp:Label></b>
                                    <div/>
                                    <br/>
                                </h4>
                            </div>
                        </div>
                    </AlternatingItemTemplate>
                </asp:Repeater>
            </asp:Panel>
            <asp:Panel runat="server" ID="pnlData" Visible="false">
                
                <div class="row text-center">
                    <h1>
                        Showing:
                        <asp:Label runat="server" ID="lblSoftwareHeader" Visible="true"></asp:Label>
                    </h1>
                </div>
                <div class="row text-center">
                    <h3>
                        Count:
                        <asp:Label runat="server" ID="lblSoftwareCount" Visible="true"></asp:Label>
                    </h3>
                </div>
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
                                <asp:BoundColumn HeaderText="Owner" DataField="Name"/>
                                <asp:BoundColumn HeaderText="Expires" DataField="ExpirationDate" />
                                <asp:BoundColumn HeaderText="Key" DataField="LicenseKey" />
                            </Columns>
                        </asp:DataGrid>
                    </div>
                </div>
                <br />
                <br />
                <div class="row text-center">
                    <h4>
                        <asp:Label runat="server" ID="lblExpired" ForeColor="red" Visible="false">Expired</asp:Label>
                    </h4>
                </div>
                
                <div class="row">
                    <div class="col-lg-8 col-lg-offset-2">
                        <asp:DataGrid 
                            runat="server" 
                            ID="gridExpired" 
                            AutoGenerateColumns="False" 
                            Width="100%" 
                            GridLines="None" 
                            CssClass="dGrid" 
                            FieldHeaderStyle-CssClass="hdr" 
                            CommandRowStyle-CssClass="ftr" >
                            <Columns>
                                <asp:BoundColumn HeaderText="Owner" DataField="Name"/>
                                <asp:BoundColumn HeaderText="Expired" DataField="ExpirationDate" />
                                <asp:BoundColumn HeaderText="Key" DataField="LicenseKey" />
                            </Columns>
                        </asp:DataGrid>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-8 col-lg-offset-2">
                        <a class="btn btn-primary btn-lg pull-right" href="/CountBySoftware.aspx">Back</a>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>
</asp:Content>
