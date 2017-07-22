<%@ Page Title="" Language="C#" MasterPageFile="~/mstAdmin.Master" AutoEventWireup="true" CodeBehind="frmRoomMaster.aspx.cs" Inherits="Hospital.frmRoomMaster" %>
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

        function Refresh() {
            window.opener.location.reload();

        }
    </script>
    <script type="text/javascript">


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


        var oldgridcolor;
        function SetBtnMouseOver(element) {
            oldgridcolor = element.style.backgroundColor;
            element.style.backgroundColor = 'Black';

        }
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="width: 99%">
                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="View1" runat="server">
                        <table width="100%">
                            <tr>
                                <td align="center">
                                    <asp:Label ID="Label1" runat="server" Text="Ward Master" Font-Names="Verdana" Font-Size="16px"
                                        Font-Bold="true" ForeColor="#3b3535"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 15%;">
                                    <asp:Button ID="BtnAddNewRoom" runat="server" BackColor="#3b3535" Font-Names="Verdana"
                                        Font-Size="13px" ForeColor="White" OnClick="BtnAddNewRoom_Click" Style="border: 1px solid black"
                                        Text="Add New" Width="80px" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel ID="pnlShow" BorderColor="Maroon" BorderWidth="1px" runat="server" Style="border-color: Green;
                                        border-style: solid; border-width: 1px">
                                        <table width="100%">
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lblPageCount" runat="server" Text="" Font-Names="Verdana" Font-Size="11px"
                                                        ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="lblRowCount" runat="server" Text="" Font-Names="Verdana" Font-Size="11px"
                                                        ForeColor="#3b3535"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100%" colspan="3">
                                                    <asp:Label ID="lblMessage" runat="server" Text="" Font-Names="Verdana" Font-Size="11px"
                                                        ForeColor="Red"></asp:Label>
                                                    <asp:Panel ID="pnl"  runat="server" Width="1010px" BorderColor="Green"
                                                        BorderStyle="Solid" BorderWidth="1px">
                                                        <asp:GridView ID="dgvRoomMaster" runat="server" CellPadding="4" ForeColor="#333333"
                                                            GridLines="Both" Font-Names="Verdana" Width="100%" Font-Size="Small" AllowPaging="true"
                                                            PageSize="10" AutoGenerateColumns="false" OnRowCommand="dgvRoomMaster_RowCommand"
                                                            OnDataBound="dgvRoomMaster_DataBound" OnPageIndexChanged="dgvRoomMaster_PageIndexChanged"
                                                            OnPageIndexChanging="dgvRoomMaster_PageIndexChanging" OnRowDataBound="dgvRoomMaster_RowDataBound"
                                                            DataKeyNames="RoomId">
                                                            <FooterStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" />
                                                            <RowStyle BackColor="#E0F0E8" Font-Size="11px" Wrap="False" />
                                                            <PagerStyle BackColor="#3b3535" ForeColor="White" HorizontalAlign="Left" Font-Size="11px" />
                                                            <SelectedRowStyle BackColor="#006600" Font-Bold="True" ForeColor="#333333" />
                                                            <HeaderStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" Font-Size="12px"
                                                                Wrap="False" />
                                                            <EditRowStyle BackColor="#2461BF" />
                                                            <AlternatingRowStyle BackColor="White" Wrap="False" />
                                                            <Columns>
                                                                <asp:BoundField DataField="RoomId" HeaderText="Room Code" ReadOnly="True" SortExpression="RoomId" />
                                                                <asp:BoundField DataField="RoomNo" HeaderText="Room No" ReadOnly="True" SortExpression="RoomNo" />
                                                                <asp:BoundField DataField="FloorName" HeaderText="Floor Name" ReadOnly="True" SortExpression="FloorName" />
                                                                <asp:BoundField DataField="CategoryDesc" HeaderText="Room Category" ReadOnly="True"
                                                                    SortExpression="CategoryDesc" />
                                                                <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="ImageEdit" runat="server" ImageUrl="~/images/edit.jpg" Height="24px"
                                                                            Width="24px" OnClick="btnUpdate_Click" /></ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                    <asp:View ID="View2" runat="server">
                        <asp:Panel Width="1010px" runat="server" ID="pnlGrid">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td align="center" colspan="4">
                                        <asp:Label ID="lblHeader" Text="Ward Master" runat="server" Font-Names="Verdana"
                                            Font-Size="16px" ForeColor="#3b3535" Font-Bold="true" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" align="center">
                                        <asp:Label ID="lblMsg" runat="server" Text="" Font-Names="Verdana" Font-Size="11px"
                                            ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="width: 100%;">
                                    <td align="right" style="width: 40%;">
                                        <asp:Label ID="lblRoomNo" Text="Room No :" runat="server" Font-Names="Verdana" Font-Size="11px"
                                            ForeColor="#3b3535" />
                                    </td>
                                    <td align="left" style="width: 20%;">
                                        <asp:TextBox ID="txtRoomNo" Width="150px" runat="server" Font-Size="11px" Font-Names="Verdana" />
                                        <asp:RequiredFieldValidator ID="rfvRoomNo" runat="server" ForeColor="Red" ControlToValidate="txtRoomNo"
                                            Font-Size="13" ValidationGroup="Save" ErrorMessage="*" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                    <td align="right" style="width: 20%;">
                                        <asp:Label ID="lblCategory" Text="Category :" runat="server" Font-Names="Verdana"
                                            Font-Size="11px" ForeColor="#3b3535" />
                                    </td>
                                    <td align="left" style="width: 30%;">
                                        <asp:DropDownList ID="ddlCategory" runat="server" Width="150px" Font-Size="11px"
                                            Font-Names="Verdana">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvCategory" runat="server" ForeColor="Red" ControlToValidate="ddlCategory"
                                            InitialValue="0" Font-Size="13" ValidationGroup="Save" ErrorMessage="*" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr style="height: 10px;">
                                    <td colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="lblFloorNo" Text="Floor No :" runat="server" Font-Names="Verdana"
                                            Font-Size="11px" ForeColor="#3b3535" />
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlFloorNo" runat="server" Width="150px" Font-Size="11px" Font-Names="Verdana">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvFloorNo" runat="server" ForeColor="Red" ControlToValidate="ddlFloorNo"
                                            Font-Size="13" ValidationGroup="Save" ErrorMessage="*" InitialValue="0" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr style="height: 10px;">
                                    <td colspan="4">
                                    </td>
                                </tr>
                            </table>
                            <asp:Button ID="BtnSave" runat="server" Text="Save" Font-Names="Verdana" Font-Size="12px"
                                ValidationGroup="Save" BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black"
                                OnClick="BtnSave_Click" />
                            <asp:Button ID="btnUpdate" runat="server" Text="Update" Font-Names="Verdana" Font-Size="12px"
                                BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black"
                                OnClick="BtnEdit_Click" />
                            <asp:Button ID="BtnClose" runat="server" Text="Close" Font-Names="Verdana" Font-Size="12px"
                                BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black"
                                OnClick="BtnClose_Click" />
                        </asp:Panel>
                    </asp:View>
                </asp:MultiView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <input id="hdnPanel" type="hidden" runat="server" value="none" />
</asp:Content>
