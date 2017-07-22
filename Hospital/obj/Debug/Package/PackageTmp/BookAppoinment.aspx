<%@ Page Title="" Language="C#" MasterPageFile="~/mstAdmin.Master" AutoEventWireup="true" CodeBehind="BookAppoinment.aspx.cs" Inherits="Hospital.BookAppoinment" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--<link href="../css/content.css" rel="stylesheet" type="text/css" />
<script src="../Notimoo/mootools-1.2-core.js" type="text/javascript"></script>
<link href="../Notimoo/notimoo-documented-1.1.css" rel="stylesheet" type="text/css" />
<script src="../Notimoo/notimoo-documented-1.1.js" type="text/javascript"></script>
<link href="../Notimoo/notimoo-v1.1.css" rel="stylesheet" type="text/css" />
<script src="../Notimoo/notimoo-v1.1.js" type="text/javascript"></script>
<link href="../Style/AdminStyle.css" rel="stylesheet" type="text/css" />--%>
    <link href="css/content.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function resetControls() {

            var FirstName = document.getElementById("ContentPlaceHolder1_txtFirstName");
            var MiddleName = document.getElementById("ContentPlaceHolder1_txtMiddleName");
            var LastName = document.getElementById("ContentPlaceHolder1_txtLastName");
            var Address = document.getElementById("ContentPlaceHolder1_txtAddress");
            var PhoneNo = document.getElementById("ContentPlaceHolder1_txtPhoneNo");
            var Date = document.getElementById("ContentPlaceHolder1_txtDate");
            var ddlTime = document.getElementById("ContentPlaceHolder1_ddlTime");
            var ddlVisitType = document.getElementById("ContentPlaceHolder1_ddlVisitype");

            //            var currDate = new Date();
            //            var month = currDate.getMonth() + 1;
            //            var day = currDate.getDate();
            //            var year = currDate.getFullYear();
            //            if (month.toString().length == 1) {
            //                month = "0" + month;
            //            }
            //            if (day.toString().length == 1) {
            //                day = "0" + day;
            //            }

            //            Date.value = day + '/' + month + '/' + year;
            Date.value = "";
            FirstName.value = "";
            MiddleName.value = "";
            LastName.value = "";
            Address.value = "";
            PhoneNo.value = "";
            ddlVisitType.selectedIndex = 0;
            ddlTime.selectedIndex = 0;
            return false;
        }

        function show_alert(msg) {
            var show1 = new Notimoo({ locationVType: 'bottom', locationHType: 'right' });
            show1.show({ message: msg, sticky: false, visibleTime: 5000, width: 200 });
        }

        function Refresh() {
            window.location.reload();

        }


        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 46 || charCode > 57 || charCode == 47)) {
                return false;
            }
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="width: 99%">
        <cc:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </cc:ToolkitScriptManager>
    </div>
    <div class="contact">
        <table width="100%">
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Label ID="lblHeading" runat="server" Text="युनिक हॉस्पिटल अॅक्सिडेंट अॅण्ड मल्टीस्पेशालिस्ट" Font-Names="Tahoma"
                        Font-Bold="true" Font-Size="20px" ForeColor="#3b3535"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Panel ID="pnlLogin" runat="server" Width="50%" BackColor=" #f0f7fe" Style="border-color: Green;
                        border-style: solid; border-width: 1px">
                        <table width="100%">
                            <tr>
                                <td colspan="2">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                    <asp:Label ID="Label1" runat="server" Text="Appoinment Detail Form" Font-Names="Tahoma"
                                        Font-Bold="true" Font-Size="14px" ForeColor="#3b3535"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label8" runat="server" Text="Appoinment No :" Font-Names="Tahoma"
                                        Font-Size="12px" ForeColor="#3b3535"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtAppoinmentNo" runat="server" Font-Names="Tahoma" Font-Size="12px"
                                        ReadOnly="true" Width="80px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label5" runat="server" Text="First Name :" Font-Names="Tahoma" Font-Size="12px"
                                        ForeColor="#3b3535"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtFirstName" runat="server" Font-Names="Tahoma" Font-Size="12px"
                                        Width="145px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtFirstName"
                                        runat="server" ForeColor="Red">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label9" runat="server" Text="Middle Name :" Font-Names="Tahoma" Font-Size="12px"
                                        ForeColor="#3b3535"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtMiddleName" runat="server" Font-Names="Tahoma" Font-Size="12px"
                                        Width="145px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="txtMiddleName"
                                        runat="server" ForeColor="Red">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label10" runat="server" Text="Last Name :" Font-Names="Tahoma" Font-Size="12px"
                                        ForeColor="#3b3535"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtLastName" runat="server" Font-Names="Tahoma" Font-Size="12px"
                                        Width="145px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="txtLastName"
                                        runat="server" ForeColor="Red">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label6" runat="server" Text="Address :" Font-Names="Tahoma" Font-Size="12px"
                                        ForeColor="#3b3535"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" Font-Names="Tahoma"
                                        Font-Size="12px" Width="165px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtAddress"
                                        runat="server" ForeColor="Red">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label3" runat="server" Text="Phone No :" Font-Names="Tahoma" Font-Size="12px"
                                        ForeColor="#3b3535"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtPhoneNo" runat="server" Font-Names="Tahoma" Font-Size="12px"
                                        Width="145px" MaxLength="10" OnKeyDown="return isNumber(event);"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtPhoneNo"
                                        runat="server" ForeColor="Red">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label4" runat="server" Text="Appoinment Date :" Font-Names="Tahoma"
                                        Font-Size="12px" ForeColor="#3b3535"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtDate" runat="server" Font-Names="Tahoma" Font-Size="12px" Width="145px"
                                        MaxLength="10" OnKeyDown="return isNumber(event);"></asp:TextBox>
                                    <cc:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDate"
                                        Format="dd/MM/yyyy">
                                    </cc:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label7" runat="server" Text="Time :" Font-Names="Tahoma" Font-Size="12px"
                                        ForeColor="#3b3535"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlTime" runat="server" Font-Names="Tahoma" Font-Size="12px"
                                        Width="150px">
                                        <asp:ListItem Text="--Select Time--" Value="0" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="Morning" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Evening" Value="2"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue="0"
                                        ControlToValidate="ddlTime" ForeColor="Red">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label2" runat="server" Text="Visit Type :" Font-Names="Tahoma" Font-Size="12px"
                                        ForeColor="#3b3535"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlVisitype" runat="server" Font-Names="Tahoma" Font-Size="12px"
                                        Width="150px">
                                        <asp:ListItem Text="--Select Visit Type--" Value="0" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="New" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="ReVisit" Value="2"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="ddlVisitype"
                                        InitialValue="0" runat="server" ForeColor="Red">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                    <asp:Button ID="BtnSave" runat="server" Text="Save" Font-Names="Tahoma" Font-Size="12px"
                                        BackColor="#3b3535" ForeColor="White" Font-Bold="true" BorderColor="Black" BorderStyle="Solid"
                                        BorderWidth="1px" OnClick="BtnSave_Click" />
                                    <asp:Button ID="BtnReset" runat="server" Text="Reset" Font-Names="Tahoma" Font-Size="12px"
                                        BackColor="#3b3535" Font-Bold="true" ForeColor="White" BorderColor="Black" BorderStyle="Solid"
                                        BorderWidth="1px" OnClick="BtnReset_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
