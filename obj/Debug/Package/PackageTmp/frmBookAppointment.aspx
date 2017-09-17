<%@ Page Title="" Language="C#" MasterPageFile="~/mstAdmin.Master" AutoEventWireup="true" CodeBehind="frmBookAppointment.aspx.cs" Inherits="Hospital.frmBookAppointment" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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

        function Refresh() {
            window.location.reload();

        }

        function ResetControls() {

            var PatientCode = document.getElementById("ContentPlaceHolder1_txtPatientCode");
            var PatientName = document.getElementById("ContentPlaceHolder1_txtPatientName");
            var ddlOPD = document.getElementById("ContentPlaceHolder1_ddlOPD");
            var ddlConsultant = document.getElementById("ContentPlaceHolder1_ddlConsultingDoctor");
            var ddlVisitType = document.getElementById("ContentPlaceHolder1_ddlVisitype");
            var ddlShift = document.getElementById("ContentPlaceHolder1_ddlShift");
            var ddlDepartment = document.getElementById("ContentPlaceHolder1_ddlMedDepartment");

            PatientCode.value = " ";
            PatientName.value = " ";
            ddlOPD.selectedIndex = 0;
            ddlConsultant.selectedIndex = 0;
            ddlShift.selectedIndex = 0;
            ddlVisitType.selectedIndex = 0;
            ddlDepartment.selectedIndex = 0;
            return false;
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
    <div class="Message">
        <table width="100%">
            <tr>
                <td align="center">
                    <asp:Panel ID="pnlLogin" runat="server" Width="60%" BackColor=" #f0f7fe" Style="border-color: Green;
                        border-style: solid; border-width: 1px">
                        <table width="100%">
                                                     <tr>
                                <td colspan="2" align="center" style="border-bottom:1px Solid Green; background-color:#006600;">
                                    <asp:Label ID="Label1" runat="server" Text="OPD Appointment" Font-Names="Tahoma"
                                        Font-Bold="true" Font-Size="14px" ForeColor="White"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 40%;">
                                    <asp:Label ID="Label5" runat="server" Text="Patient Code :" Font-Names="Tahoma" Font-Size="12px"
                                        ForeColor="#3b3535"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtPatientCode" runat="server" Font-Names="Tahoma" Font-Size="11px"
                                        Width="145px"></asp:TextBox><asp:Button ID="btnGetInfo" runat="server" Text="Get Name"
                                            Font-Names="Tahoma" Font-Size="12px" BackColor="#3b3535" ForeColor="White" Font-Bold="true"
                                            BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" OnClick="btnGetInfo_Click" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtPatientCode"
                                        runat="server" ForeColor="Red" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label3" runat="server" Text="Patient Name :" Font-Names="Tahoma" Font-Size="12px"
                                        ForeColor="#3b3535"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtPatientName" runat="server" Font-Names="Tahoma" Font-Size="11px"
                                        Width="300px" ReadOnly="true" Font-Bold="true"></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtPatientName"
                                        runat="server" ForeColor="Red" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                              <tr>
                                <td align="right">
                                    <asp:Label ID="Label8" runat="server" Text="Department :" Font-Names="Tahoma" Font-Size="12px"
                                        ForeColor="#3b3535"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlMedDepartment" runat="server" Font-Names="Tahoma" Font-Size="11px">
                                    </asp:DropDownList>
                                   <%--   <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="ddlMedDepartment"
                                        runat="server" ForeColor="Red" ValidationGroup="Save">*</asp:RequiredFieldValidator>--%>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label9" runat="server" Text="OPD Room :" Font-Names="Tahoma" Font-Size="12px"
                                        ForeColor="#3b3535"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlOPD" runat="server" Font-Names="Tahoma" Font-Size="11px">
                                    </asp:DropDownList>
                                   <%--   <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="ddlOPD"
                                        runat="server" ForeColor="Red" ValidationGroup="Save">*</asp:RequiredFieldValidator>--%>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label6" runat="server" Text="Consulting Doctor :" Font-Names="Tahoma"
                                        Font-Size="12px" ForeColor="#3b3535"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlConsultingDoctor" runat="server" Font-Names="Tahoma" Font-Size="11px">
                                    </asp:DropDownList>
                                   <%--   <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="ddlConsultingDoctor"
                                        runat="server" ForeColor="Red" ValidationGroup="Save">*</asp:RequiredFieldValidator>--%>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label2" runat="server" Text="Visit Type :" Font-Names="Tahoma" Font-Size="12px"
                                        ForeColor="#3b3535"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlVisitype" runat="server" Font-Names="Tahoma" Font-Size="11px"
                                        Width="120px">
                                        <asp:ListItem Text="--Select Visit Type--" Value="0" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="New" Value="N"></asp:ListItem>
                                        <asp:ListItem Text="ReVisit" Value="R"></asp:ListItem>
                                    </asp:DropDownList>
                                    <%--   <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="ddlVisitype"
                                        InitialValue="0" runat="server" ForeColor="Red" ValidationGroup="Save">*</asp:RequiredFieldValidator>--%>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label4" runat="server" Text="Shift :" Font-Names="Tahoma" Font-Size="12px"
                                        ForeColor="#3b3535"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlShift" runat="server" Font-Names="Tahoma" Font-Size="11px"
                                        Width="120px">
                                        <asp:ListItem Text="--Select Shift--" Value="" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="Morning" Value="M"></asp:ListItem>
                                        <asp:ListItem Text="Night" Value="N"></asp:ListItem>
                                    </asp:DropDownList>
                               <%--        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="ddlVisitype"
                                        InitialValue="0" runat="server" ForeColor="Red" ValidationGroup="Save">*</asp:RequiredFieldValidator>--%>
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
                                        BorderWidth="1px" Width="60px" OnClick="BtnSave_Click" ValidationGroup="Save"/>
                                    <asp:Button ID="BtnReset" runat="server" Text="Reset" Font-Names="Tahoma" Font-Size="12px"
                                        BackColor="#3b3535" Font-Bold="true" ForeColor="White" BorderColor="Black" BorderStyle="Solid"
                                        BorderWidth="1px" Width="60px" OnClientClick="return ResetControls();" />
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
                <td align="center">
                    <asp:Panel ID="pnlShow" runat="server" Width="99.7%" Style="text-align: center; background-color: #E0F0E8;
                        display: none;" BorderColor="Green" BorderStyle="Solid" BorderWidth="1px">
                        <table width="100%">
                            <tr>
                                <td align="right">
                                    <asp:Button ID="BtnDelete" runat="server" Text="Delete" Font-Names="Tahoma" Font-Size="12px"
                                        BackColor="#3b3535" ForeColor="White" Font-Bold="true" BorderColor="Black" BorderStyle="Solid"
                                        BorderWidth="1px" Width="60px" OnClick="BtnDelete_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:DataList ID="datalistPatient" runat="server" BackColor="Gray" BorderColor="#006600"
                                        BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" Font-Names="Verdana"
                                        Font-Size="Small" GridLines="Both" RepeatColumns="3" RepeatDirection="Horizontal" RepeatLayout="Table"
                                        Width="100%">
                                        <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                                        <HeaderStyle BackColor="#3b3535" Font-Bold="True" Font-Size="Large" ForeColor="White"
                                            HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <HeaderTemplate>
                                            Patient Waiting List</HeaderTemplate>
                                        <ItemStyle BackColor="#f0f7fe" ForeColor="Black" BorderWidth="2px" />
                                        <ItemTemplate>
                                            No :
                                            <asp:Label ID="lblPKId" runat="server" Text='<%# Eval("PKId") %>' ForeColor="#006600"
                                                Font-Bold="true"></asp:Label>
                                            <asp:CheckBox ID="chkDelete" runat="server" Text="Delete" Font-Bold="true"></asp:CheckBox>
                                            <br />
                                            Patient Code:
                                            <asp:Label ID="lblCName" runat="server" Text='<%# Eval("PateintCode") %>' ForeColor="#006600"></asp:Label>
                                            <br />
                                            Patient Name:
                                            <asp:Label ID="lblName" runat="server" Text='<%# Eval("FullName") %>' ForeColor="OrangeRed"></asp:Label>
                                            <br />
                                            OPD :
                                            <asp:Label ID="lblCity" runat="server" Text=' <%# Eval("OPDDesc") %>' ForeColor="#006600"></asp:Label>
                                            <br />
                                            Consulting Doctor :
                                            <asp:Label ID="lblCountry" runat="server" Text='<%# Eval("Consultantname") %>' ForeColor="#006600"></asp:Label>
                                            <br />
                                            Visit Type :
                                            <asp:Label ID="Label7" runat="server" Text='<%# Eval("VisitType") %>' ForeColor="#006600"></asp:Label>
                                            <br />
                                            Department :
                                            <asp:Label ID="Label10" runat="server" Text='<%# Eval("MedDeptDesc") %>' ForeColor="#006600"></asp:Label>
                                            <br />
                                        </ItemTemplate>
                                    </asp:DataList>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
        <input id="hdnPanel" type="hidden" runat="server" value="none" />
    </div>
</asp:Content>
