<%@ Page Title="" Language="C#" MasterPageFile="~/Mstlogin.Master" AutoEventWireup="true" CodeBehind="frmFirstLogin.aspx.cs" Inherits="Hospital.frmFirstLogin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/content.css" rel="stylesheet" type="text/css" />
    <script src="Notimoo/mootools-1.2-core.js" type="text/javascript"></script>
    <link href="Notimoo/notimoo-documented-1.1.css" rel="stylesheet" type="text/css" />
    <script src="Notimoo/notimoo-documented-1.1.js" type="text/javascript"></script>
    <link href="Notimoo/notimoo-v1.1.css" rel="stylesheet" type="text/css" />
    <script src="Notimoo/notimoo-v1.1.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function ResetControls() {
            var ddlUser = document.getElementById("ContentPlaceHolder1_ddlUserType");
            var UserName = document.getElementById("ContentPlaceHolder1_txtUserName");
            var Password = document.getElementById("ContentPlaceHolder1_txtPassword");
            ddlUser.selectedIndex = 0;
            UserName.value = "";
            Password.value = "";
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
                <td align="center">
                    <asp:Label ID="lblHeading" runat="server" Text="Ganga Clinic And Nursing Home" Font-Names="Verdana"
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
                    <asp:Panel ID="pnlLogin" runat="server" Width="30%" BackColor=" #f0f7fe" BorderColor="#003300"
                        BorderStyle="Solid" BorderWidth="1px">
                        <table width="100%">
                            <tr>
                                <td colspan="2">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                    <asp:Label ID="Label1" runat="server" Text="Set New Password" Font-Names="Verdana"
                                        Font-Bold="true" Font-Size="14px" ForeColor="#3b3535"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                    <asp:Label Text="" ID="lblMessage" ForeColor="Red" Font-Names="Verdana" Font-Size="11px"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 50%;">
                                    <asp:Label ID="lblPass" runat="server" Text="New Password :" Font-Names="Verdana"
                                        Font-Size="12px" ForeColor="#3b3535"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtPassowrd" runat="server" Font-Names="Verdana" Font-Size="12px"
                                        Width="145px" TextMode="Password"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblConformPass" runat="server" Text="Conform Password :" Font-Names="Verdana"
                                        Font-Size="12px" ForeColor="#3b3535"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtConformPassword" runat="server" TextMode="Password" Font-Names="Verdana"
                                        Font-Size="11px" Width="145px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Button ID="BtnSubmit" runat="server" Text="Submit" Font-Names="Verdana" Font-Size="11px"
                                        Width="80px" BackColor="#3b3535" ForeColor="White" Font-Bold="true" OnClick="btnSubmit_Click"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                                </td>
                                <td align="left">
                                    <asp:Button ID="BtnReset" runat="server" Text="Reset" Font-Names="Verdana" Font-Size="11px"
                                        Width="80px" BackColor="#3b3535" Font-Bold="true" ForeColor="White" OnClientClick="return ResetControls();"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" OnClick="BtnReset_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
