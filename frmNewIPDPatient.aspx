<%@ Page Title="" Language="C#" MasterPageFile="~/mstAdmin.Master" AutoEventWireup="true" CodeBehind="frmNewIPDPatient.aspx.cs" Inherits="Hospital.frmNewIPDPatient" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/content.css" rel="stylesheet" type="text/css" />
    <script src="Notimoo/mootools-1.2-core.js" type="text/javascript"></script>
    <link href="Notimoo/notimoo-documented-1.1.css" rel="stylesheet" type="text/css" />
    <script src="Notimoo/notimoo-documented-1.1.js" type="text/javascript"></script>
    <link href="Notimoo/notimoo-v1.1.css" rel="stylesheet" type="text/css" />
    <script src="Notimoo/notimoo-v1.1.js" type="text/javascript"></script>
    <link href="Style/AdminStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function show_alert(msg) {
            var show1 = new Notimoo({ locationVType: 'bottom', locationHType: 'right' });
            show1.show({ message: msg, sticky: false, visibleTime: 5000, width: 200 });
        }
    </script>
    <script type="text/javascript">
        function ResetControls() {

            var ddlInitials = document.getElementById("ContentPlaceHolder1_ddlInitials");
            var lstrFirstName = document.getElementById("ContentPlaceHolder1_txtFirstName");
            var lstrMiddleName = document.getElementById("ContentPlaceHolder1_txtMidleName");
            var lstrLastName = document.getElementById("ContentPlaceHolder1_txtLastName");
            var ddlGender = document.getElementById("ContentPlaceHolder1_ddlGender");
            var lstrAge = document.getElementById("ContentPlaceHolder1_txtAge");
            var ddlOccupation = document.getElementById("ContentPlaceHolder1_ddlOccupation");
            var ddlReligion = document.getElementById("ContentPlaceHolder1_ddlReligion");
            var ddlCaste = document.getElementById("ContentPlaceHolder1_ddlCasts");
            var lstrAddress = document.getElementById("ContentPlaceHolder1_txtAddress");
            var lstrContactNo = document.getElementById("ContentPlaceHolder1_txtContactNo");
            var lstrCity = document.getElementById("ContentPlaceHolder1_txtCity");
            var lstrState = document.getElementById("ContentPlaceHolder1_txtState");
            var lstrCoutry = document.getElementById("ContentPlaceHolder1_txtCountry");
            var lstrReason = document.getElementById("ContentPlaceHolder1_txtReasonForAdmit");
            var lstrReferedBy = document.getElementById("ContentPlaceHolder1_txtReferedBy");
            var ddlConsultant = document.getElementById("ContentPlaceHolder1_ddlConsultingDoctor");
            var ddlCompanyName = document.getElementById("ContentPlaceHolder1_ddlCompName");
            var lstrCompAddress = document.getElementById("ContentPlaceHolder1_txtCompAddress");
            var lstrCompContact = document.getElementById("ContentPlaceHolder1_txtComContNo");
            var ddlFloorNo = document.getElementById("ContentPlaceHolder1_ddlFloor");
            var ddlWardNo = document.getElementById("ContentPlaceHolder1_ddlWard");
            var ddlBedNo = document.getElementById("ContentPlaceHolder1_ddlBedNo");
            var ddlInsurence = document.getElementById("ContentPlaceHolder1_ddlInsurance");
            var lstrInsurenceFile = document.getElementById("ContentPlaceHolder1_fileInsurance");
            var lstrId = document.getElementById("ContentPlaceHolder1_fileId");

            lstrFirstName.value = "";
            lstrMiddleName.value = "";
            lstrLastName.value = "";
            lstrAddress.value = "";
            lstrAge.value = "";
            lstrCity.value = "";
            lstrState.value = "";
            lstrCoutry.value = "";
            lstrReferedBy.value = "";
            lstrCompAddress.value = "";
            lstrCompContact.value = "";
            lstrReason.value = "";
            lstrContactNo.value = "";
            lstrInsurenceFile = "";
            lstrId = "";

            ddlBedNo.selectedIndex = 0;
            ddlCaste.selectedIndex = 0;
            ddlCompanyName.selectedIndex = 0;
            ddlConsultant.selectedIndex = 0;
            ddlInitials.selectedIndex = 0;
            ddlOccupation.selectedIndex = 0;
            ddlFloorNo.selectedIndex = 0;
            ddlReligion.selectedIndex = 0;
            ddlGender.selectedIndex = 0;
            ddlWardNo.selectedIndex = 0;
            ddlInsurence.selectedIndex = 0;

        }


        function KeyValid(e) {
            var keynum
            var keychar
            var numcheck
            // For Internet Explorer
            if (window.event) {
                keynum = e.keyCode
            }
            // For Netscape/Firefox/Opera
            else if (e.which) {
                keynum = e.which
            }
            keychar = String.fromCharCode(keynum)

            //List of special characters you want to restrict
            if (keychar == "!" || keychar == "@" || keychar == "#"
    || keychar == "$" || keychar == "%" || keychar == "^" || keychar == "&"
    || keychar == "*" || keychar == "(" || keychar == ")"
    || keychar == "<" || keychar == ">") {
                return false;
            }
            else {
                return true;
            }
        }

        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }

        var oldgridcolor;
        function SetMouseOver(element) {
            oldgridcolor = element.style.backgroundColor;
            element.style.backgroundColor = '#B2C2D1';

        }
        function SetMouseOut(element) {
            element.style.backgroundColor = oldgridcolor;
            element.style.textDecoration = 'none';
        }

        var oldgridcolor;
        function SetBtnMouseOver(element) {
            oldgridcolor = element.style.backgroundColor;
            element.style.backgroundColor = 'Black';

        }
        function SetBtnMouseOut(element) {
            element.style.backgroundColor = oldgridcolor;
            element.style.textDecoration = 'none';
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="width: 99%">
        <table>
            <tr>
                <td align="center">
                    <asp:Panel ID="pnlGrid" runat="server" Width="1030px" Style="text-align: center;
                        background-color: #E0F0E8; height: 780px;">
                        <table width="100%" style="border-color: Green; border-style: solid; border-width: 2px"
                            cellpadding="0">
                            <tr>
                                <td>
                                    <asp:Panel ID="Panel2" runat="server" Width="99.7%" Style="text-align: center; background-color: #E0F0E8;
                                        height: 770px;" BorderColor="Green" BorderStyle="Solid" BorderWidth="1px">
                                        <table width="100%">
                                            <tr>
                                                <td align="center" colspan="6">
                                                    <asp:Label ID="Label2" runat="server" Text="IPD Patient Registration" Font-Names="Verdana"
                                                        Font-Size="16px" Font-Bold="true" ForeColor="#3b3535"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6" align="left" style="border-top-style: solid; border-top-color: Green;
                                                    border-top-width: 1px; border-bottom-color: Green; border-bottom-style: solid;
                                                    border-bottom-width: 1px">
                                                    <asp:Label ID="Label13" runat="server" Text="Patient Detail" Font-Names="Verdana"
                                                        Font-Size="11px" Font-Bold="true" ForeColor="#3b3535"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="width: 20%;">
                                                    <asp:Label ID="lblEmpCode" runat="server" Text="Patient Code :" Font-Names="Verdana"
                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtPatientCode" runat="server" MaxLength="50" ReadOnly="true" Font-Names="Verdana"
                                                        Font-Size="11px" Width="80px" Font-Bold="true"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="Label14" runat="server" Text="Admission Date :" Font-Names="Verdana"
                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtAdmitDate" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                        Width="80px" OnKeyDown="return isNumber(event);" MaxLength="10" onkeypress="return KeyValid(event);"></asp:TextBox>
                                                        <cc:CalendarExtender
                                                            ID="CalendarExtender4" runat="server" TargetControlID="txtAdmitDate" Format="dd/MM/yyyy">
                                                        </cc:CalendarExtender>
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="Label15" runat="server" Text="Admission Time :" Font-Names="Verdana"
                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtAdmitTime" runat="server" MaxLength="50" Font-Names="Verdana"
                                                        Font-Size="11px" Width="80px" Font-Bold="true"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="lblFirstName" runat="server" Text="Patient First Name :" Font-Names="Verdana"
                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:DropDownList ID="ddlInitials" runat="server" Font-Names="Verdana" Font-Size="11px">
                                                    </asp:DropDownList>
                                                    <asp:TextBox ID="txtFirstName" runat="server" MaxLength="50" Font-Names="Verdana"
                                                        Font-Size="11px" Width="120px" ToolTip="Enter Patient First Name" Font-Bold="true"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ForeColor="Red" ControlToValidate="txtFirstName"
                                                        Font-Size="13" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="lblMiddleName" runat="server" Text="Middle Name :" Font-Names="Verdana"
                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtMidleName" runat="server" MaxLength="50" Font-Names="Verdana"
                                                        Font-Size="11px" Width="120px" ToolTip="Enter Patient Middle Name" Font-Bold="true"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ForeColor="Red"
                                                        ControlToValidate="txtMidleName" Font-Size="13" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="lblLastName" runat="server" Text="Last Name :" Font-Names="Verdana"
                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtLastName" runat="server" MaxLength="50" Font-Names="Verdana"
                                                        Font-Size="11px" Width="120px" ToolTip="Enter Patient Last Name" Font-Bold="true"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red"
                                                        ControlToValidate="txtLastName" Font-Size="13" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label1" runat="server" Text="Gender :" Font-Names="Verdana" Font-Size="11px"
                                                        ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:DropDownList ID="ddlGender" runat="server" Font-Names="Verdana" Font-Size="11px">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ForeColor="Red"
                                                        ControlToValidate="ddlGender" Font-Size="13" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="Label3" runat="server" Text="Age In Years :" Font-Names="Verdana"
                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtAge" runat="server" MaxLength="50" Font-Names="Verdana" Font-Size="11px"
                                                        Width="120px" onkeypress="return isNumber(event);"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ForeColor="Red"
                                                        ControlToValidate="txtAge" Font-Size="13" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="Label4" runat="server" Text="Occupation :" Font-Names="Verdana" Font-Size="11px"
                                                        ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:DropDownList ID="ddlOccupation" runat="server" Font-Names="Verdana" Font-Size="11px">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ForeColor="Red"
                                                        ControlToValidate="ddlOccupation" Font-Size="13" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label5" runat="server" Text="Religion :" Font-Names="Verdana" Font-Size="11px"
                                                        ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:DropDownList ID="ddlReligion" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                        OnSelectedIndexChanged="ddlReligion_SelectedIndexChanged" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ForeColor="Red"
                                                        ControlToValidate="ddlReligion" Font-Size="11" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="Label6" runat="server" Text="Caste :" Font-Names="Verdana" Font-Size="11px"
                                                        ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:DropDownList ID="ddlCasts" runat="server" Font-Names="Verdana" Font-Size="11px">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ForeColor="Red"
                                                        ControlToValidate="ddlCasts" Font-Size="11" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="Label10" runat="server" Text="Birth Date :" Font-Names="Verdana" Font-Size="11px"
                                                        ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtBirthDate" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                        Width="120px" OnKeyDown="return isNumber(event);" MaxLength="10" onkeypress="return KeyValid(event);"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ForeColor="Red"
                                                        ControlToValidate="txtBirthDate" Font-Size="11" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                                                    <cc:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtBirthDate"
                                                        Format="dd/MM/yyyy">
                                                    </cc:CalendarExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label11" runat="server" Text="Address :" Font-Names="Verdana" Font-Size="11px"
                                                        ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left" colspan="5">
                                                    <asp:TextBox ID="txtAddress" runat="server" MaxLength="100" TextMode="MultiLine"
                                                        Width="470px" Font-Names="Verdana" Font-Size="11px"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ForeColor="Red"
                                                        ControlToValidate="txtAddress" Font-Size="11" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label23" runat="server" Text="Contact No :" Font-Names="Verdana" Font-Size="11px"
                                                        ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left" colspan="5">
                                                    <asp:TextBox ID="txtContactNo" runat="server" MaxLength="50" Font-Names="Verdana"
                                                        Font-Size="11px" Width="120px" onkeypress="return isNumber(event);"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ForeColor="Red"
                                                        ControlToValidate="txtContactNo" Font-Size="11" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="lblCountry" runat="server" Text="City :" Font-Names="Verdana" Font-Size="11px"
                                                        ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtCity" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ForeColor="Red"
                                                        ControlToValidate="txtCity" Font-Size="13" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="Label16" runat="server" Text="State :" Font-Names="Verdana" Font-Size="11px"
                                                        ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtState" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ForeColor="Red"
                                                        ControlToValidate="txtState" Font-Size="13" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="Label17" runat="server" Text="Country :" Font-Names="Verdana" Font-Size="11px"
                                                        ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtCountry" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ForeColor="Red"
                                                        ControlToValidate="txtCountry" Font-Size="13" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="lblReason" runat="server" Text="Reason For Admition :" Font-Names="Verdana"
                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left" colspan="5">
                                                    <asp:TextBox ID="txtReasonForAdmit" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                        Width="470px" TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label19" runat="server" Text="Refered By :" Font-Names="Verdana" Font-Size="11px"
                                                        ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left" colspan="5">
                                                    <asp:TextBox ID="txtReferedBy" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                        Width="470px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label7" runat="server" Text="Consulting Doctor :" Font-Names="Verdana"
                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left" colspan="5">
                                                    <asp:DropDownList ID="ddlConsultingDoctor" runat="server" Font-Names="Verdana" Font-Size="11px">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ForeColor="Red"
                                                        ControlToValidate="ddlConsultingDoctor" Font-Size="11" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6" align="left" style="border-top-style: solid; border-top-color: Green;
                                                    border-top-width: 1px; border-bottom-color: Green; border-bottom-style: solid;
                                                    border-bottom-width: 1px">
                                                    <asp:Label ID="Label18" runat="server" Text="Company Detail" Font-Names="Verdana"
                                                        Font-Size="11px" Font-Bold="true" ForeColor="#3b3535"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="lblName" runat="server" Text="Company Name :" Font-Names="Verdana"
                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left" colspan="5">
                                                    <asp:DropDownList ID="ddlCompName" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                        OnSelectedIndexChanged="ddlCompName_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label8" runat="server" Text="Company Address :" Font-Names="Verdana"
                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left" colspan="5">
                                                    <asp:TextBox ID="txtCompAddress" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                        Width="470px" TextMode="MultiLine" ReadOnly="true"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label9" runat="server" Text="Contact No :" Font-Names="Verdana" Font-Size="11px"
                                                        ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left" colspan="5">
                                                    <asp:TextBox ID="txtComContNo" runat="server" MaxLength="50" Font-Names="Verdana"
                                                        Font-Size="11px" Width="120px" ReadOnly="true"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6" align="left" style="border-top-style: solid; border-top-color: Green;
                                                    border-top-width: 1px; border-bottom-color: Green; border-bottom-style: solid;
                                                    border-bottom-width: 1px">
                                                    <asp:Label ID="Label12" runat="server" Text="Room Detail" Font-Names="Verdana" Font-Size="11px"
                                                        Font-Bold="true" ForeColor="#3b3535"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label20" runat="server" Text="Floor No :" Font-Names="Verdana" Font-Size="11px"
                                                        ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:DropDownList ID="ddlFloor" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                        OnSelectedIndexChanged="ddlFloor_SelectedIndexChanged" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ForeColor="Red"
                                                        ControlToValidate="ddlFloor" Font-Size="11" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="Label21" runat="server" Text="Ward :" Font-Names="Verdana" Font-Size="11px"
                                                        ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:DropDownList ID="ddlWard" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                        OnSelectedIndexChanged="ddlWard_SelectedIndexChanged" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ForeColor="Red"
                                                        ControlToValidate="ddlWard" Font-Size="11" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="Label22" runat="server" Text="Bed No:" Font-Names="Verdana" Font-Size="11px"
                                                        ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:DropDownList ID="ddlBedNo" runat="server" Font-Names="Verdana" Font-Size="11px">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ForeColor="Red"
                                                        ControlToValidate="ddlBedNo" Font-Size="11" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6" align="left" style="border-top-style: solid; border-top-color: Green;
                                                    border-top-width: 1px; border-bottom-color: Green; border-bottom-style: solid;
                                                    border-bottom-width: 1px">
                                                    <asp:Label ID="Label24" runat="server" Text="Insurance Detail" Font-Names="Verdana"
                                                        Font-Size="11px" Font-Bold="true" ForeColor="#3b3535"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" colspan="6">
                                                    <asp:CheckBox ID="ChkInsurance" runat="server" Text="Insurance Patient" Font-Names="Verdana"
                                                        Font-Size="11px" ForeColor="#3b3535" OnCheckedChanged="ChkInsurance_CheckedChanged"
                                                        AutoPostBack="true" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label25" runat="server" Text="Insurance Company :" Font-Names="Verdana"
                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left" colspan="5">
                                                    <asp:DropDownList ID="ddlInsurance" runat="server" Font-Names="Verdana" Font-Size="11px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label26" runat="server" Text="Insurance Proof :" Font-Names="Verdana"
                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left" colspan="5">
                                                    <asp:FileUpload ID="fileInsurance" runat="server" Width="600px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label27" runat="server" Text="Id Proof :" Font-Names="Verdana" Font-Size="11px"
                                                        ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left" colspan="5">
                                                    <asp:FileUpload ID="fileId" runat="server" Width="600px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td align="center" colspan="6">
                                                    <asp:Button ID="BtnSave" runat="server" Text="Save" Font-Names="Verdana" Font-Size="12px"
                                                        ValidationGroup="Save" BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black"
                                                        onmouseover="SetBtnMouseOver(this)" onmouseout="SetBtnMouseOut(this)" OnClick="BtnSave_Click" />
                                                    <asp:Button ID="BtnClose" runat="server" Text="Reset" Font-Names="Verdana" Font-Size="12px"
                                                        BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black"
                                                        onmouseover="SetBtnMouseOver(this)" onmouseout="SetBtnMouseOut(this)" OnClientClick="ResetControls();" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </div>
    <input id="hdnPanel" type="hidden" runat="server" value="none" />
</asp:Content>
