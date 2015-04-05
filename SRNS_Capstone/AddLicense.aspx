<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Capstone.Master" CodeBehind="AddLicense.aspx.cs" Inherits="SRNS_Capstone.AddLicense" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
    <style>
        .day{
            cursor:pointer;
        }
    </style>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
        <div class="row">
		<div class="col-lg-12 col-md-12 text-center">
			<div class="row">
             <asp:Button CssClass="btn btn-lg btn-primary" runat="server" ID="btnNew" Text="New Software" OnClick="btnSubmit_Click" />
			</div>
			<div class="row">
				<h1>Add new license for: </h1>
					<div class="col-lg-4 col-lg-offset-4">
						<asp:DropDownList runat="server" ID="ddlSoftwareSelect" CssClass="form-control" Style="width: 100%; max-width: 100%;">
                            <asp:ListItem Enabled="true" Text="" Value="0"></asp:ListItem>
						</asp:DropDownList>
					</div>
					<hr />
				</div>
			</div>
         </div>

<div class="col-lg-8 col-md-8" style="margin-top: 10vh;">
	<div class="row hidden-sm hidden-xs">
		<div class="col-lg-6 col-md-6">
            <asp:Label runat="server" ID="lblSoftName">Software Name</asp:Label>
		</div>
		<div class="col-lg-6 col-md-6">
            <asp:Label runat="server" ID="lblSoftDescription">Software Description</asp:Label>
		</div>
		
	</div>
	<div class="row">
		<div class="col-lg-6 col-md-6">
			<asp:TextBox CssClass="form-control" placeholder="Software Name" runat="server" ID="txtSoftName"></asp:TextBox>
		</div>
		<div class="col-lg-6 col-md-6">
			<asp:TextBox CssClass="form-control" placeholder="Software Description" runat="server" ID="txtSoftDescription"></asp:TextBox>
		</div>
		
	</div>
    <div class="row">
		<div class="col-lg-8 col-md-8">
			<asp:RadioButton runat="server" type="radio" GroupName="Assignment" name="options" ID="radioBtnAssign" />
            Assign
		</div>
	</div>
	<div class="row">
		<div class="col-lg-8 col-md-8">
            <asp:RadioButton runat="server" type="radio" GroupName="Assignment" name="options" ID="radioBtnRemove" />
            Remove
        </div>
	</div>
    <div class="row">
		<div class="col-lg-8 col-md-8">
            <asp:RadioButton runat="server" type="radio" GroupName="Assignment" name="options" ID="radioBtnAvailable" />
            Available
        </div>
	</div>
	<div class="row hidden-sm hidden-xs">
		<div class="col-lg-6 col-md-6">
            <asp:Label runat="server" ID="lblLiNum">License Number</asp:Label>
        </div>
		<div class="col-lg-6 col-md-6">
			<asp:Label runat="server" ID="lblSpeedchart">Speedchart</asp:Label>
		</div>
        
        <%--<div class="col-lg-4 col-md-4">
			Date Modified:
		                <input type="date" name="Date">
		</div>--%>
	</div>

	<div class="row">
		<div class="col-lg-6 col-md-6">
             <asp:TextBox CssClass="form-control" placeholder="License Number" runat="server" ID="txtLiNum"></asp:TextBox>
        </div>
		<div class="col-lg-6 col-md-6">
			<asp:TextBox CssClass="form-control" placeholder="Speedchart" runat="server" ID="txtSpeedchart"></asp:TextBox>
		</div>
		<%--<div class="col-lg-4 col-md-4">
			Date Assigned:
		                <input type="date" name="Date">
		</div>--%>
	</div>
    <div class="row hidden-sm hidden-xs">
		<div class="col-lg-6 col-md-6">
			<asp:Label runat="server" ID="lblLiHold">License Holder</asp:Label>
		</div>
		<div class="col-lg-6 col-md-6">
			<asp:Label runat="server" ID="lblLicenseMan">License Manager</asp:Label>
		</div>
		<%--<div class="col-lg-4 col-md-4">
			Date Removed:
		            <input type="date" name="Date">
		</div>--%>
    </div>
    <div class="row">
		<div class="col-lg-6 col-md-6">
			<asp:TextBox CssClass="form-control" placeholder="License Holder" runat="server" ID="txtLiHold"></asp:TextBox>
		</div>
		<div class="col-lg-6 col-md-6">
			<asp:TextBox CssClass="form-control" placeholder="License Manager" runat="server" ID="txtLicenseMan"></asp:TextBox>
		</div>
		<%--<div class="col-lg-4 col-md-4">
			Date Removed:
		            <input type="date" name="Date">
		</div>--%>
	</div>
    <div class="row hidden-sm hidden-xs">
		<div class="col-lg-6 col-md-6">
			<asp:Label runat="server" ID="lblLiHoldUserId">License Holder User id</asp:Label>
		</div>
		<div class="col-lg-6 col-md-6">
			<asp:Label runat="server" ID="lblLiCost">License Cost</asp:Label>
		</div>
		<%--<div class="col-lg-4 col-md-4">
			Date Expiration:
		                <input type="date" name="Date">
		</div>--%>
	</div>
    <div class="row">
	    <div class="col-lg-6 col-md-6">
			<asp:TextBox CssClass="form-control" placeholder="License Holder User id" runat="server" ID="txtLiHoldUserId"></asp:TextBox>
		</div>
		<div class="col-lg-6 col-md-6">
			<asp:TextBox CssClass="form-control" placeholder="License Cost" runat="server" ID="txtLiCost"></asp:TextBox>
		</div>
		<%--<div class="col-lg-4 col-md-4">
			Date Expiration:
		                <input type="date" name="Date">
		</div>--%>
	</div>
    <div class="row hidden-sm hidden-xs">
		<div class="col-lg-6 col-md-6">
			<asp:Label runat="server" ID="lblReqNum">Associated Requisition Number</asp:Label>
		</div>
		<div class="col-lg-6 col-md-6">
            <asp:Label runat="server" ID="lblChargeback">Chargeback Complete</asp:Label>
		</div>
	</div>
     <div class="row">
		<div class="col-lg-6 col-md-6">
			<asp:TextBox CssClass="form-control" placeholder="Associated Requisition Number" runat="server" ID="txtReqNum"></asp:TextBox>
		</div>
		<div class="col-lg-6 col-md-6">
            <asp:DropDownList runat="server" ID="ddlChargeback" CssClass="form-control" Style="width: 100%; max-width: 100%;">
                        <asp:ListItem Text="Yes"></asp:ListItem>
                        <asp:ListItem Text="No"></asp:ListItem>
            </asp:DropDownList>
		</div>
	</div>
     <div class="row">
		<div class="col-lg-6 col-md-6">
			<asp:Label runat="server" ID="lblLiComp">License Company</asp:Label>
		</div>
	</div>
    <div class="row">
        <div class="col-lg-6 col-md-6">
			<asp:RadioButton runat="server" type="radio" GroupName="Company" name="options" ID="radiobtnSRNS" />
            SRNS
		</div>
	</div>
    <div class="row">
        <div class="col-lg-6 col-md-6">
			<asp:RadioButton runat="server" type="radio" GroupName="Company" name="options" ID="radiobtnSRR" />
            SRR
		</div>
	</div>
    <div class="row">
        <div class="col-lg-6 col-md-6">
			<asp:RadioButton runat="server" type="radio" GroupName="Company" name="options" ID="radiobtnDOE" />
            DOE
		</div>
	</div>
    <div class="row">
        <div class="col-lg-6 col-md-6">
			<asp:RadioButton runat="server" type="radio" GroupName="Company" name="options" ID="radiobtnCen" />
            Centerra
		</div>
	</div>
        <br />
    <div class="row">
	    <div class="col-lg-6 col-md-6 col-lg-offset-6">
			<asp:Label runat="server" ID="lblComments">Comments:</asp:Label>
		</div>
	</div>
    <div class="row">
	    <div class="col-lg-6 col-md-6 col-lg-offset-6">
			<asp:TextBox CssClass="form-control" placeholder="Comments" runat="server" ID="txtComments"></asp:TextBox>
		</div>
	</div>
    <div class="row">
        <div class="col-lg-6 col-md-6 col-lg-offset-6">
            <div class="fileinput fileinput-new input-group" data-provides="fileinput">
                <div class="form-control" data-trigger="fileinput"><i class="glyphicon glyphicon-file fileinput-exists"></i> <span class="fileinput-filename"></span></div>
                <span class="input-group-addon btn btn-default btn-file"><span class="fileinput-new">Select file</span><span class="fileinput-exists">Change</span><input type="file" name="..."></span>
                <a href="#" class="input-group-addon btn btn-default fileinput-exists" data-dismiss="fileinput">Remove</a>
            </div>
        </div>
    </div>
        <br />
    <div class="row">
	    <div class="col-lg-6 col-md-6 text-center col-lg-offset-6">
             <asp:Button CssClass="btn btn-lg btn-primary" runat="server" ID="btnSubmit" Text="Submit" OnClick="btnSubmit_Click" />
		</div>
	</div>
</div>
        <div class="col-lg-4 col-md-4" style="margin-top: 10vh;">
            <asp:Label runat="server" ID="lblDocCreate">Document Created By/Last Updated by</asp:Label>
            <div class="row">
                  <asp:TextBox CssClass="form-control" placeholder="Document Created By/Last Updated by" runat="server" ID="txtDocCreate"></asp:TextBox>
            </div>
            <div class="row"  style="margin-top: 10vh;">
            <asp:Label runat="server" ID="Label1">Date Updated</asp:Label>
            </div>
            <div class="row">
                <div class="input-group date">
                    <input runat="server" ID="txtDate" type="text" class="form-control" />
                    <span class="input-group-addon">
                        <i class="glyphicon glyphicon-th"></i>
                    </span>
                </div>
            </div>
            <div class="row">
            <asp:Label runat="server" ID="Label2">Date Assigned</asp:Label>
            </div>
            <div class="row">
                <div class="input-group date">
                    <input type="text" class="form-control" />
                    <span class="input-group-addon">
                        <i class="glyphicon glyphicon-th"></i>
                    </span>
                </div>
            </div>
            <div class="row">
            <asp:Label runat="server" ID="Label3">Date Removed</asp:Label>
            </div>
            <div class="row">
                <div class="input-group date">
                    <input type="text" class="form-control" />
                    <span class="input-group-addon">
                        <i class="glyphicon glyphicon-th"></i>
                    </span>
                </div>
            </div>
            <div class="row">
            <asp:Label runat="server" ID="Label4">Date of Expiration</asp:Label>
            </div>
            <div class="row">
                <div class="input-group date">
                    <input type="text" class="form-control" />
                    <span class="input-group-addon">
                        <i class="glyphicon glyphicon-th"></i>
                    </span>
                </div>
            </div>
        </div>

    </div>

<script src="http://code.jquery.com/jquery-1.11.0.min.js"></script>
<script src="//netdna.bootstrapcdn.com/bootstrap/3.1.1/js/bootstrap.min.js"></script>
<script src="http://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.3.0/js/bootstrap-datepicker.js"></script>
<script>
    $('.input-group.date').datepicker({
        format: "mm/dd/yyyy",
        startDate: "01-01-2012",
        endDate: "01-01-2020",
        todayBtn: "linked",
        autoclose: true,
        todayHighlight: true
    });
</script>
<script>
        $('.fileupload').fileupload()
</script>

</asp:Content>
