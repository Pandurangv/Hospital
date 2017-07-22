<%@ Page Title="" Language="C#" MasterPageFile="~/mstAdmin.Master" AutoEventWireup="true" CodeBehind="frmUploadDocuments.aspx.cs" Inherits="Hospital.frmUploadDocuments" %>
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



        function resetControls() {
            var lFileName = document.getElementById("ContentPlaceHolder1_txtFileName");
            var lfile = document.getElementById("ContentPlaceHolder1_fluDocument");
            lFileName.value = "";
            lfile.value = "";
            return false;
        }

        var oldgridcolor;
        function SetBtnMouseOver(element) {
            oldgridcolor = element.style.backgroundColor;
            element.style.backgroundColor = '#E373FF';

        }
        function SetBtnMouseOut(element) {
            element.style.backgroundColor = oldgridcolor;
            element.style.textDecoration = 'none';
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="width: 100%; margin-left: 0px;" align="center">
        <asp:Panel ID="pnlAddNew" runat="server" Width="50%" BorderColor="Maroon" BorderStyle="Solid"
            BorderWidth="1px" BackColor="#F0E6F0">
            <table width="100%">
                <tr>
                    <td align="center" colspan="4">
                        <asp:Label ID="Label1" runat="server" Text="Document Management" Font-Names="Verdana"
                            Font-Size="16px" Font-Bold="true" ForeColor="Black"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right" style="width: 15%;" colspan="4">
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblProductId" runat="server" Text="Patient Name :" Font-Names="Verdana"
                            Font-Size="11px" ForeColor="#3B3535"></asp:Label>
                    </td>
                    <td align="left" colspan="3">
                        <asp:DropDownList ID="ddlPatient" runat="server" Width="260px">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ForeColor="Red"
                            ControlToValidate="ddlPatient" Font-Size="13" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblProductName" runat="server" Text=" Date :" Font-Names="Verdana"
                            Font-Size="11px" ForeColor="#3B3535"></asp:Label>
                    </td>
                    <td align="left" colspan="3">
                        <asp:TextBox ID="txtUploadDate" runat="server" MaxLength="100" Font-Names="Verdana"
                            Font-Size="11px" Width="129px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvProductName" runat="server" ForeColor="Red" ControlToValidate="txtUploadDate"
                            Font-Size="13" ValidationGroup="Save" ErrorMessage="*" Display="Dynamic"></asp:RequiredFieldValidator>
                        <cc:CalendarExtender ID="calAdmitDate" runat="server" TargetControlID="txtUploadDate"
                            Format="dd/MM/yyyy">
                        </cc:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                    </td>
                    <td align="left" colspan="3">
                        
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="Label20" runat="server" Text="Document Name :" Font-Names="Verdana"
                            Font-Size="11px" ForeColor="#3B3535"></asp:Label>
                    </td>
                    <td align="left" colspan="3">
                        <asp:TextBox ID="txtFileName" runat="server" Font-Names="Verdana" Font-Size="11px"
                            Width="250px" MaxLength="500"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                runat="server" ErrorMessage="*" ControlToValidate="txtFileName" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="Label24" runat="server" Text="Select File:" Font-Names="Verdana" ForeColor="#3B3535"
                            Font-Size="11px"></asp:Label>
                    </td>
                    <td align="left" colspan="3">
                        <asp:FileUpload ID="fluDocument" runat="server" Font-Names="Verdana" Font-Size="11px"
                            ForeColor="#3B3535" /><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                ErrorMessage="*" ControlToValidate="fluDocument" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="4">
                        <asp:Button ID="BtnSave" runat="server" Text="Save" Font-Names="Verdana" Font-Size="13px"
                            BackColor="#666699" ForeColor="White" Width="80px" Style="border: 1px solid black"
                            OnClick="BtnSave_Click" />
                        <asp:Button ID="BtnReset" runat="server" Text="Reset" Font-Names="Verdana" Font-Size="13px"
                            BackColor="#666699" ForeColor="White" Width="80px" Style="border: 1px solid black"
                            OnClick="BtnRes_Click"  />
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        &nbsp;
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    <input id="hdnPanel" type="hidden" runat="server" value="none" />
</asp:Content>
