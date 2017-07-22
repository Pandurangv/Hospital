<%@ Page Title="" Language="C#" MasterPageFile="~/mstAdmin.Master" AutoEventWireup="true" CodeBehind="RegistrationPortal.aspx.cs" Inherits="Hospital.RegistrationPortal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/content.css" rel="stylesheet" type="text/css" />
    <script src="Notimoo/mootools-1.2-core.js" type="text/javascript"></script>
    <link href="Notimoo/notimoo-documented-1.1.css" rel="stylesheet" type="text/css" />
    <script src="Notimoo/notimoo-documented-1.1.js" type="text/javascript"></script>
    <link href="Notimoo/notimoo-v1.1.css" rel="stylesheet" type="text/css" />
    <script src="Notimoo/notimoo-v1.1.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function ResetControl() {
            var ddName = document.getElementById("ContentPlaceHolder1_ddlEmpName");
            var ddDepartment = document.getElementById("ContentPlaceHolder1_ddlDepartmentName");
            var Password = document.getElementById("ContentPlaceHolder1_txtPassword");
            var ConfirmPassword = document.getElementById("ContentPlaceHolder1_txtConfirmPassword");
            ddName.selectedIndex = 0;
            ddDepartment.selectedIndex = 0;
            Password.value = "";
            ConfirmPassword.value = "";
            return false;
        }
        function show_alert(msg) {
            var show1 = new Notimoo({ locationVType: 'bottom', locationHType: 'right' });
            show1.show({ message: msg, sticky: false, visibleTime: 5000, width: 200 });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <table width="100%">
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
                    <asp:Panel ID="Panel1" runat="server" Width="50%" BackColor=" #f0f7fe" BorderColor="#003300"
                        BorderStyle="Solid" BorderWidth="1px">
                        <table width="100%">
                            <tr>
                                <td colspan="2" align="center">
                                    <asp:Label ID="Label2" runat="server" Text="Registration For Portal" Font-Names="Tahoma"
                                        Font-Bold="true" Font-Size="20px" ForeColor="#3b3535"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" colspan="2">
                                    <asp:Label ID="Label3" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                    <asp:Label ID="Label1" runat="server" Text="All Field Are Manditory" ForeColor="#3b3535"
                                        Font-Names="Tahoma" Font-Size="12px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblName" runat="server" Text="Name :" Font-Names="Tahoma" Font-Size="12px"
                                        ForeColor="#3b3535"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlEmpName" runat="server" Width="50%" Font-Names="Tahoma"
                                        Font-Size="12px" OnSelectedIndexChanged="ddlEmpName_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlEmpName"
                                        ErrorMessage="RequiredFieldValidator" ForeColor="Red">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblDepartment" runat="server" Text="Department Name :" Font-Names="Tahoma"
                                        Font-Size="12px" ForeColor="#3b3535"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlDepartmentName" runat="server" Width="50%" Font-Names="Tahoma"
                                        Font-Size="12px">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlDepartmentName"
                                        ErrorMessage="RequiredFieldValidator" ForeColor="Red">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblPassword" runat="server" Text="Password :" Font-Names="Tahoma"
                                        Font-Size="12px" ForeColor="#3b3535"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtPassword" runat="server" Font-Names="Tahoma" Font-Size="12px"
                                        TextMode="Password" Width="120px" MaxLength="50"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPassword"
                                        ErrorMessage="RequiredFieldValidator" ForeColor="Red">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblConfirmPass" runat="server" Text="Confirm Password :" Font-Names="Tahoma"
                                        Font-Size="12px" ForeColor="#3b3535"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" Width="120px"
                                        Font-Size="12px" MaxLength="50"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtConfirmPassword"
                                        ErrorMessage="RequiredFieldValidator" ForeColor="Red">*</asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtPassword"
                                        ControlToValidate="txtConfirmPassword" ErrorMessage="Password Does Not Match"
                                        ForeColor="Red" SetFocusOnError="True">*</asp:CompareValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToCompare="txtPassword"
                                        ControlToValidate="txtConfirmPassword" ErrorMessage="Password Does Not Match"
                                        ForeColor="Red" Font-Names="Tahoma" Font-Size="12px"></asp:CompareValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                    <asp:Button ID="btnCreateUser" runat="server" Text="Create User" Font-Names="Tahoma"
                                        Font-Size="12px" BackColor="#3b3535" ForeColor="White" Font-Bold="true" BorderColor="Black"
                                        BorderStyle="Solid" BorderWidth="1px" OnClick="btnCreateUser_Click" />
                                    <asp:Button ID="btnReset" runat="server" Text="Reset" Font-Names="Tahoma" Font-Size="12px"
                                        BackColor="#3b3535" ForeColor="White" Font-Bold="true" BorderColor="Black" BorderStyle="Solid"
                                        BorderWidth="1px" OnClientClick="return  ResetControl();" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
