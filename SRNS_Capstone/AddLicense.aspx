<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Capstone.Master" CodeBehind="AddLicense.aspx.cs" Inherits="SRNS_Capstone.AddLicense" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
    <style>
        .day, .next, .prev, .today, .month, .datepicker-switch {
            cursor: pointer;
        }

            .month:nth-child(2n), .year:nth-child(2n) {
                background-color: #CCC;
            }
    </style>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="container">
        <div class="row">
            <div class="col-lg-4 col-md-4">
                <asp:Label runat="server" ID="lblSoftName">Software Name</asp:Label>
                <asp:TextBox CssClass="form-control" runat="server" ID="txtSoftName"></asp:TextBox>
            </div>
            <div class="col-lg-4 col-md-4">
                <asp:Label runat="server" ID="lblSoftDescription">Software Description</asp:Label>
                <asp:TextBox CssClass="form-control" runat="server" ID="txtSoftDescription"></asp:TextBox>
            </div>
            <div class="col-lg-4 col-md-4">
                <asp:Label runat="server" ID="lblProvider">Software Provider/Manufacturer</asp:Label>
                <asp:TextBox CssClass="form-control" runat="server" ID="txtProvider"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-4 col-md-4">
                <asp:Label runat="server" ID="lblLiNum">License Key/Number</asp:Label>
                <asp:TextBox CssClass="form-control" runat="server" ID="txtLiKey"></asp:TextBox>
            </div>
            <div class="col-lg-4 col-md-4">
                <asp:Label runat="server" ID="lblSpeedchart">Speedchart</asp:Label>
                <asp:TextBox CssClass="form-control" runat="server" ID="txtSpeedchart"></asp:TextBox>
            </div>
            <div class="col-lg-4 col-md-4">
                <asp:Label runat="server" ID="Label1">Date Updated</asp:Label>
                <div class="input-group date">
                    <asp:TextBox runat="server" ID="txtDateUpdated" type="text" class="form-control" />
                    <span class="input-group-addon">
                        <i class="glyphicon glyphicon-th"></i>
                    </span>
                </div>
            </div>
        </div>
        
        <div class="row">
            <div class="col-lg-4 col-md-4">
                <asp:Label runat="server" ID="lblLiHold">License Holder</asp:Label>
                <asp:DropDownList CssClass="form-control" runat="server" Style="min-width: 100%" ID="ddlLicHolder"></asp:DropDownList>
            </div>
            <div class="col-lg-4 col-md-4">
                <asp:Label runat="server" ID="lblLicenseMan">License Manager</asp:Label>
                <asp:DropDownList CssClass="form-control" runat="server" Style="min-width: 100%" ID="ddlHolderManager"></asp:DropDownList>
            </div>
            <div class="col-lg-4 col-md-4">
                <asp:Label runat="server" ID="Label2">Date Assigned</asp:Label>
                <div class="input-group date">
                    <asp:TextBox type="text" ID="txtDateAssigned" class="form-control" runat="server" />
                    <span class="input-group-addon">
                        <i class="glyphicon glyphicon-th"></i>
                    </span>
                </div>
            </div>
        </div>
        
        <div class="row">
            <div class="col-lg-4 col-md-4">
                <asp:Label runat="server" ID="lblLiHoldUserId">License Holder User ID</asp:Label>
                <asp:TextBox CssClass="form-control" runat="server" ID="txtLiHoldUserId"></asp:TextBox>
            </div>
            <div class="col-lg-4 col-md-4">
                <asp:Label runat="server" ID="lblLiCost">License Cost</asp:Label>
                <asp:TextBox CssClass="form-control" runat="server" ID="txtLiCost"></asp:TextBox>
            </div>
            <div class="col-lg-4 col-md-4">
                <asp:Label runat="server" ID="Label5">Date Removed</asp:Label>
                <div class="input-group date">
                    <asp:TextBox type="text" ID="txtDateRemoved" class="form-control" runat="server" />
                    <span class="input-group-addon">
                        <i class="glyphicon glyphicon-th"></i>
                    </span>
                </div>
            </div>
        </div>
        
        <div class="row">
            <div class="col-lg-4 col-md-4">
                <asp:Label runat="server" ID="lblReqNum">Associated Requisition Number</asp:Label>
                <asp:TextBox CssClass="form-control" runat="server" ID="txtReqNum"></asp:TextBox>
            </div>
            <div class="col-lg-4 col-md-4">
                <asp:Label runat="server" ID="lblChargeback">Chargeback Complete</asp:Label>
                <asp:DropDownList runat="server" ID="ddlChargeback" CssClass="form-control" Style="width: 100%; max-width: 100%;">
                    <asp:ListItem Text="Yes"></asp:ListItem>
                    <asp:ListItem Text="No"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-lg-4 col-md-4">
                <asp:Label runat="server" ID="Label4">Date of Expiration</asp:Label>
                <div class="input-group date">
                    <asp:TextBox type="text" ID="txtDateExpiring" class="form-control" runat="server" />
                    <span class="input-group-addon">
                        <i class="glyphicon glyphicon-th"></i>
                    </span>
                </div>
            </div>
        </div>
        
        <div class="row">
            <div class="col-lg-4 col-md-4">
                <asp:Panel runat="server" ID="pnlHolders">
                    <asp:Label runat="server" ID="lblLiComp">License Company</asp:Label>
                    <br/>
                    <asp:RadioButton runat="server" ToolTip="SRNS" type="radio" GroupName="Company" name="options" ID="radiobtnSRNS" />
                    SRNS
                    <br/>
                    <asp:RadioButton runat="server" ToolTip="SRR" type="radio" GroupName="Company" name="options" ID="radiobtnSRR" />
                    SRR
                    <br/>
                    <asp:RadioButton runat="server" type="radio" ToolTip="DOE" GroupName="Company" name="options" ID="radiobtnDOE" />
                    DOE
                    <br/>
                    <asp:RadioButton runat="server" type="radio" ToolTip="Centerra" GroupName="Company" name="options" ID="radiobtnCen" />
                    Centerra
                    <br/>
                </asp:Panel>
            </div>
            <div class="col-lg-4 col-md-4">
                <asp:Panel runat="server" ID="pnlAssign">
                    <asp:Label runat="server" ID="lblAssignStatus" Text="Assignment Status"></asp:Label>
                    <br/>
                    <asp:RadioButton runat="server" type="radio" ToolTip="Assign" GroupName="Assignment" name="options" ID="radioBtnAssign" />
                    Assigned
                    <br/>
                    <asp:RadioButton runat="server" type="radio" ToolTip="Remove" GroupName="Assignment" name="options" ID="radioBtnRemove" />
                    Removed
                    <br/>
                    <asp:RadioButton runat="server" type="radio" ToolTip="Available" GroupName="Assignment" name="options" ID="radioBtnAvailable" />
                    Available
                    <br/>
                </asp:Panel>
            </div>
            <div class="col-lg-4 col-md-4">
                <asp:Label runat="server" ID="lblComments">Comments:</asp:Label>

                <asp:TextBox CssClass="form-control" runat="server" ID="txtComments" TextMode="MultiLine"></asp:TextBox>

                <div class="fileinput fileinput-new input-group" data-provides="fileinput" style="margin-top: 5px;">
                    <div class="form-control" data-trigger="fileinput"><i class="glyphicon glyphicon-file fileinput-exists"></i><span class="fileinput-filename"></span></div>
                    <span class="input-group-addon btn btn-default btn-file"><span class="fileinput-new">Select file</span><span class="fileinput-exists">Change</span><input type="file" name="..."></span>
                    <a href="#" class="input-group-addon btn btn-default fileinput-exists" data-dismiss="fileinput">Remove</a>
                </div>
                <br />
                <div class="row">
                    <asp:Button CssClass="btn btn-lg btn-primary pull-right" runat="server" ID="btnSubmit" Text="Submit" OnClick="btnSubmit_Click" />
                </div>
            </div>
        </div>
    </div>

    <script src="http://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.3.0/js/bootstrap-datepicker.js"></script>
    <script src="assets/js/jquery.formatCurrency-1.4.0.min.js"></script>
    <script>
        $('.input-group.date').datepicker({
            format: "mm/dd/yyyy",
            startDate: "01-01-2012",
            endDate: "01-01-2020",
            todayBtn: "linked",
            autoclose: true,
            todayHighlight: true
        });

        $('.fileupload').fileupload();

        var inputMoney = (function() {
            return function(input) {
                var DecimalSeparator = Number(input).toLocaleString().substr(1, 1);

                var AmountWithCommas = Amount.toLocaleString();
                var arParts = String(AmountWithCommas).split(DecimalSeparator);
                var intPart = arParts[0];
                var decPart = (arParts.length > 1 ? arParts[1] : '');
                decPart = (decPart + '00').substr(0, 2);

                return '$ ' + intPart + DecimalSeparator + decPart;
            }
        })();

        function Comma(Num) { //function to add commas to textboxes
            Num += '';
            Num = Num.replace(',', ''); Num = Num.replace(',', ''); Num = Num.replace(',', '');
            Num = Num.replace(',', ''); Num = Num.replace(',', ''); Num = Num.replace(',', '');
            x = Num.split('.');
            x1 = x[0];
            x2 = x.length > 1 ? '.' + x[1] : '';
            var rgx = /(\d+)(\d{3})/;
            while (rgx.test(x1))
                x1 = x1.replace(rgx, '$1' + ',' + '$2');
            return x1 + x2;
        }


        

    </script>

</asp:Content>
