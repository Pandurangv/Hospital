<%@ Page Title="" Language="C#" MasterPageFile="~/mstAdmin.Master" AutoEventWireup="true" CodeBehind="frmChangePassword.aspx.cs" Inherits="Hospital.frmChangePassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/content.css" rel="stylesheet" type="text/css" />
    <script src="Notimoo/mootools-1.2-core.js" type="text/javascript"></script>
    <link href="Notimoo/notimoo-documented-1.1.css" rel="stylesheet" type="text/css" />
    <script src="Notimoo/notimoo-documented-1.1.js" type="text/javascript"></script>
    <link href="Notimoo/notimoo-v1.1.css" rel="stylesheet" type="text/css" />
    <script src="Notimoo/notimoo-v1.1.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function resetControl() {
            var oldPassword = document.getElementById("ContentPlaceHolder1_txtOldPass");
            var newPassword = document.getElementById("ContentPlaceHolder1_txtNewPass");
            var confirmPassword = document.getElementById("ContentPlaceHolder1_txtConfirmPass");
            oldPassword.value = "";
            newPassword.value = "";
            confirmPassword.value = "";
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
                                <td colspan="2" align="right">
                                    <asp:Label ID="Label6" runat="server" Text="*" ForeColor="Red" Font-Size="12px"> </asp:Label>
                                    <asp:Label ID="Label5" runat="server" Text="Mandatory Field " ForeColor="#3b3535"
                                        Font-Names="Tahoma" Font-Size="12px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <asp:Label ID="Label1" runat="server" Text="Change Password" Font-Names="Tahoma"
                                        Font-Bold="true" Font-Size="20px" ForeColor="#3b3535"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                    <asp:Label ID="lblMsg" Text="" ForeColor="Red" Font-Names="Verdana" Font-Size="11px"
                                        runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label2" runat="server" Text="Old Password :" Font-Names="Tahoma" Font-Size="12px"
                                        ForeColor="#3b3535"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtOldPass" runat="server" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtOldPass"
                                        ForeColor="Red">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label3" runat="server" Text="New Password :" Font-Names="Tahoma" Font-Size="12px"
                                        ForeColor="#3b3535"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtNewPass" runat="server" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNewPass"
                                        ErrorMessage="RequiredFieldValidator" ForeColor="Red">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label4" runat="server" Text="Confirm Password :" Font-Names="Tahoma"
                                        Font-Size="12px" ForeColor="#3b3535"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtConfirmPass" runat="server" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtConfirmPass"
                                        ErrorMessage="RequiredFieldValidator" ForeColor="Red">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                    <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Password does not maches"
                                        ControlToCompare="txtNewPass" ControlToValidate="txtConfirmPass" ForeColor="Red"
                                        SetFocusOnError="True" Font-Names="Tahoma" Font-Size="12px"></asp:CompareValidator>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                    <asp:Button ID="btnsave" runat="server" Text="Change Password" Font-Names="Tahoma"
                                        Font-Size="12px" BackColor="#3b3535" ForeColor="White" Font-Bold="true" OnClick="btnsave_Click"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                                    <asp:Button ID="btnReset" runat="server" Text="Reset" Font-Names="Tahoma" Font-Size="12px"
                                        BackColor="#3b3535" ForeColor="White" Font-Bold="true" BorderColor="Black" BorderStyle="Solid"
                                        BorderWidth="1px" OnClientClick="return resetControl() " />
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
