<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Capstone.Master" CodeBehind="AddEditLicense.aspx.cs" Inherits="SRNS_Capstone.AddLicense" validateRequest="false" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="assets/js/cleditor/jquery.cleditor.css"/>
    <style>
         .day, .next, .prev, .today, .month, .datepicker-switch, .year {
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
            <asp:Panel runat="server" ID="pnlSelection" Visible="true">
            <asp:Panel runat="server" ID="pnlSuccess" Visible="false">
                    <div class="col-lg-6 col-lg-offset-3">
                        <div class="alert alert-success" role="alert" style="margin-top:10px;">
                            <h4>
                                <asp:Label runat="server" ID="lblSuccess" Text="Database Changes Successful"></asp:Label>
                            </h4>
                        </div>
                    </div>
            </asp:Panel>
            <div class="col-lg-12 col-md-12 text-center">
                <h1 style="margin-top: 10vh;">Add License</h1>
                <div class="row">
                <div class="col-lg-4 col-lg-offset-4 col-md-4 col-md-offset-4">
                    <asp:Button runat="server" ID="btnAddKey" class="btn btn-block btn-default" Text="Add License" OnClick="btnAddKey_OnClick"></asp:Button>
                </div>
                </div>
                <div class="row text-center">
                    <h3>or</h3>
                </div>
                <div class="row">
                    <h3>Enter License Key to modify</h3>
                    <div class="col-lg-4 col-lg-offset-4 col-md-4 col-md-offset-4">
                        
                        <asp:Panel runat="server" DefaultButton="btnLookupKey">
                            <div class="input-group">
                                    <asp:TextBox runat="server" ID="txtEnterKeyToEdit" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                    <span class="input-group-btn">
                                        <asp:Button runat="server" class="btn btn-default" type="button" ID="btnLookupKey" OnClick="btnLookupKey_OnClick" Text="Submit"></asp:Button>
                                    </span>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlMain" Visible="false">
        <div class="row">
            <div class="col-lg-6 col-lg-offset-3">
                <asp:Panel runat="server" ID="pnlError" Visible="false">
                        <div class="alert alert-danger" role="alert" style="margin-top:10px;">
                            <h4>
                                <asp:Label runat="server" ID="lblError" Text="Changes not made"></asp:Label>
                            </h4>
                        </div>
                </asp:Panel>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-4 col-md-4">
                  * Required Fields  
            </div>
        </div>
        <div class="row">
            <div class="col-lg-4 col-md-4">
                <asp:Label runat="server" ID="lblSoftName">*Software Name</asp:Label>
                <asp:TextBox CssClass="form-control" runat="server" ID="txtSoftName"></asp:TextBox>
            </div>
            <div class="col-lg-4 col-md-4">
                <asp:Label runat="server" ID="lblSoftDescription">Software Description</asp:Label>
                <asp:TextBox CssClass="form-control" runat="server" ID="txtSoftDescription"></asp:TextBox>
            </div>
            <div class="col-lg-4 col-md-4">
                <asp:Label runat="server" ID="lblProvider">*Software Provider/Manufacturer</asp:Label>
                <asp:TextBox CssClass="form-control" runat="server" ID="txtProvider"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-4 col-md-4">
                <asp:Label runat="server" ID="lblLiNum">*License Key/Number</asp:Label>
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
                <asp:Label runat="server" ID="lblLiHold">License Holder</asp:Label><%--&nbsp;&nbsp;&nbsp;<a onclick="manualEntry();" >Enter Manually</a>--%>
                <asp:DropDownList CssClass="form-control" runat="server" Style="min-width: 100%; display: block" ID="ddlLicHolder" onchange="populateLoginID()" ></asp:DropDownList>
                <asp:TextBox type="text" ID="txtManualLicHolder" class="form-control" runat="server" style="display: none;" />
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
                <asp:TextBox CssClass="form-control" runat="server" ID="txtLiHoldUserId" ></asp:TextBox>
            </div>
            <div class="col-lg-4 col-md-4">
                <asp:Label runat="server" ID="lblLiCost">*License Cost (Decimal point required)</asp:Label>
                <asp:TextBox CssClass="form-control" runat="server" ID="txtLiCost" onkeyup="javascript:this.value=validateCurrency(this.value);"></asp:TextBox>
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
                <asp:Label runat="server" ID="lblDateExpiring">*Date of Expiration</asp:Label>
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
            <div class="col-lg-3 col-md-3">
                <asp:Panel runat="server" ID="pnlAssign">
                    <asp:Label runat="server" ID="lblAssignStatus" Text="Assignment Status"></asp:Label>
                    <br/>
                    <asp:RadioButton runat="server" type="radio" ToolTip="Assign" GroupName="Assignment" name="options" onclick="radioBtnFillDate();" ID="radioBtnAssign" />
                    Assigned
                    <br/>
                    <asp:RadioButton runat="server" type="radio" ToolTip="Remove" GroupName="Assignment" name="options" onclick="radioBtnFillDate();" ID="radioBtnRemove" />
                    Removed
                    <br/>
                    <asp:RadioButton runat="server" type="radio" ToolTip="Available" GroupName="Assignment" name="options"  onclick="radioBtnFillDate();" ID="radioBtnAvailable" />
                    Available
                    <br/>
                </asp:Panel>
            </div>
            <div class="col-lg-5 col-md-5">
                <br/>
                <asp:Label runat="server" ID="lblComments">Comments:</asp:Label>

                <div style="width: 450px;">
                    <div>
                        <textarea runat="server" id="editor" rows="0" cols="0" ValidateRequestMode="Disabled"></textarea>
                    </div>
                    <div class="normaldiv" style="float: right">
                        <a href="#" class="siteButton btn btn-xs btn-default" id="btnClear">Clear</a>
                        <%--<a href="#" class="siteButton" id="btnAddImage">Add an image</a>--%>&nbsp;&nbsp;
                        <%--<a href="#" class="siteButton btn btn-xs btn-default" id="btnGetHtml">Get html</a>--%>
                    </div>
                </div>
                <br/>
                <h3>
                    <asp:Label runat="server" ID="lblLink" Text="Attachment:"></asp:Label>
                    <asp:HyperLink runat="server" ID="lnkFileLink" Visible="False"></asp:HyperLink>
                </h3>
                <br/>
                <asp:Panel runat="server" ID="pnlWarningAttach">
                    <span style="color:red">WARNING:</span>Attaching a file will replace current attachment. Attach .zip for multiple attachments
                </asp:Panel>
                <div class="fileinput fileinput-new input-group" data-provides="fileinput" style="margin-top: 5px;">
                    <div class="form-control" data-trigger="fileinput">
                        <i class="glyphicon glyphicon-file fileinput-exists"></i>
                        <span class="fileinput-filename"></span>
                    </div>
                    <span class="input-group-addon btn btn-default btn-file">
                        <span class="fileinput-new">Select file</span>
                        <span class="fileinput-exists">Change</span>
                        <asp:FileUpload runat="server" ID="fileUpload" />
                    </span>
                    <a href="#" class="input-group-addon btn btn-default fileinput-exists" data-dismiss="fileinput">Remove</a>
                </div>
                <br/>
                <strong><asp:Label runat="server" ID="lblEditor"></asp:Label></strong>
                <br/>
                <asp:Button runat="server" CssClass="btn btn-danger btn-md pull-left" ID="btnDelete" Text="Delete License" OnClick="btnDelete_OnClick" Visible="False"/>
                <asp:Button CssClass="btn btn-lg btn-primary pull-right" runat="server" ID="btnSubmit" Text="Submit" OnClick="btnSubmit_Click" />
                <asp:Button CssClass="btn btn-lg btn-primary pull-right" runat="server" ID="btnUpdate" Text="Update" OnClick="btnUpdate_OnClick" />
            </div>
        </div>
        </asp:Panel>
    </div>
    
    <asp:HiddenField runat="server" ID="hdnGuid"/>
    

    <script src="assets/js/datepicker.js"></script>
    <script src="assets/js/jquery.formatCurrency-1.4.0.min.js"></script>
    <script src="assets/js/cleditor/jquery.cleditor.min.js"></script>
    <script>
        $('.input-group.date').datepicker({
            format: "mm/dd/yyyy",
            startDate: "01-01-2012",
            endDate: "01-01-2020",
            todayBtn: "linked",
            autoclose: true,
            todayHighlight: true
        });

        function validateCurrency(input) {
            var regex = /^\d+(?:\.\d{0,2})$/;
            if (!regex.test(input)) {
                var txt = document.getElementById("<%=txtLiCost.ClientID%>");
                var lbl = document.getElementById("<%=lblLiCost.ClientID%>");
                txt.style.backgroundColor = "#FFD8D8";
                lbl.style.color = "red";
                document.getElementById('<%=btnSubmit.ClientID%>').disabled = true;
            } else {
                var txt = document.getElementById("<%=txtLiCost.ClientID%>");
                var lbl = document.getElementById("<%=lblLiCost.ClientID%>");
                txt.style.backgroundColor = "white";
                lbl.style.color = "black";
                document.getElementById('<%=btnSubmit.ClientID%>').disabled = false;
            }

            return input;
        };

        $(document).ready(function () {
            var options = {
                width: 450,
                height: 200,
                controls: "bold italic underline strikethrough subscript superscript |  font size " +
                    "style | color highlight removeformat | bullets numbering | outdent " +
                    "indent | alignleft center alignright justify | undo redo | " +
                    "rule link image unlink | cut copy paste pastetext | print source"
            };
 
            var editor = $("#<%=editor.ClientID%>").cleditor(options)[0];
 
            $("#btnClear").click(function (e) {
                e.preventDefault();
                editor.focus();
                editor.clear();
            });

            $("#btnGetHtml").click(function () {
                alert($("#editor").val());
            });

            _varOldAssign = document.getElementById('<%=txtDateAssigned.ClientID%>').value;
            _varOldRemove = document.getElementById('<%=txtDateRemoved.ClientID%>').value;
 
        });

        var populateLoginID = (function() {
            return function () {
                var holder = document.getElementById('<%=ddlLicHolder.ClientID%>');
                var holderLogin = holder.options[holder.selectedIndex].value;
                var txtLogin = document.getElementById('<%=txtLiHoldUserId.ClientID%>');
                txtLogin.value = holderLogin;
            }
        })();

        function radioBtnFillDate(myRadio) {
            if (document.getElementById("<%=radioBtnAssign.ClientID%>").checked) {
                var txtAssign = document.getElementById('<%=txtDateAssigned.ClientID%>');
                var date = new Date();
                txtAssign.value = (date.getMonth() + 1) + '/' + date.getDate() + '/' + date.getFullYear();
                var txtRemove = document.getElementById('<%=txtDateRemoved.ClientID%>');
                txtRemove.value = _varOldRemove;
            }
            if (document.getElementById("<%=radioBtnRemove.ClientID%>").checked) {
                var txtRemove = document.getElementById('<%=txtDateRemoved.ClientID%>');
                var date = new Date();
                txtRemove.value = (date.getMonth() + 1) + '/' + date.getDate() + '/' + date.getFullYear();
                var txtAssign = document.getElementById('<%=txtDateAssigned.ClientID%>');
                txtAssign.value = _varOldAssign;
            }
            if (document.getElementById("<%=radioBtnAvailable.ClientID%>").checked) {
                var txtRemove = document.getElementById('<%=txtDateRemoved.ClientID%>');
                var txtAssign = document.getElementById('<%=txtDateAssigned.ClientID%>');
                txtAssign.value = _varOldAssign;
                txtRemove.value = _varOldRemove;
            }
        }


        var manualEntry = (function() {
            return function() {
                $('#<%=ddlLicHolder.ClientID%>').slideToggle('slow');
                $('#<%=txtManualLicHolder.ClientID%>').slideToggle('slow');
            }
        })();

    </script>

</asp:Content>
