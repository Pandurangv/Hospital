<%@ Page Title="" Language="C#" MasterPageFile="~/mstAdmin.Master" AutoEventWireup="true" CodeBehind="frmConsultantMaster.aspx.cs" Inherits="Hospital.frmConsultantMaster" %>
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
            window.location.reload();

        }
    </script>
    <script type="text/javascript">
        function hideModalPopup() {
            var modalPopupBehavior = $find('programmaticModalPopupBehavior');
            modalPopupBehavior.hide();
            return false;
        }

        function hideDeleteModalPopup() {
            var modalPopupBehavior = $find('DeleteprogrammaticModalPopupBehavior');
            modalPopupBehavior.hide();
            return false;
        }

        function hideEditModalPopup() {
            var modalPopupBehavior = $find('programmaticModalPopupBehaviorEdit');
            modalPopupBehavior.hide();
            return false;
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
            if (charCode > 31 && (charCode < 46 || charCode > 57 || charCode == 47)) {
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
        <asp:Panel ID="pnlAddNew" runat="server">
            <table width="100%">
                <tr>
                    <td align="center">
                        <asp:Label ID="Label1" runat="server" Text="Consultant Master" Font-Names="Verdana"
                            Font-Size="16px" Font-Bold="true" ForeColor="#3b3535"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 15%;">
                        <asp:Button ID="BtnAddNewConsultant" runat="server" Text="Add New" Font-Names="Verdana"
                            Font-Size="13px" BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black"
                            onmouseover="SetBtnMouseOver(this)" onmouseout="SetBtnMouseOut(this)" OnClick="BtnAddNewConsultant_Click" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    <table width="100%">
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
                                <asp:Panel ID="pnl" ScrollBars="Both" runat="server" Width="1020px" BorderColor="Green"
                                    BorderStyle="Solid" BorderWidth="1px">
                                    <asp:GridView ID="dgvConsultant" runat="server" CellPadding="4" ForeColor="#333333"
                                        DataKeyNames="ConsCode" GridLines="Both" Font-Names="Verdana" Width="100%" Font-Size="Small"
                                        AllowPaging="true" PageSize="20" AutoGenerateColumns="false" OnRowCommand="dgvConsultant_RowCommand"
                                        OnDataBound="dgvConsultant_DataBound" OnPageIndexChanged="dgvConsultant_PageIndexChanged"
                                        OnPageIndexChanging="dgvConsultant_PageIndexChanging" OnRowDataBound="dgvConsultant_RowDataBound">
                                        <FooterStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" />
                                        <RowStyle BackColor="#E0F0E8" Font-Size="11px" Wrap="False" />
                                        <PagerStyle BackColor="#3b3535" ForeColor="White" HorizontalAlign="Left" Font-Size="11px" />
                                        <SelectedRowStyle BackColor="#006600" Font-Bold="True" ForeColor="#333333" />
                                        <HeaderStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" Font-Size="12px"
                                            Wrap="False" />
                                        <EditRowStyle BackColor="#2461BF" />
                                        <AlternatingRowStyle BackColor="White" Wrap="False" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Consultant Code" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkConsCode" runat="server" CausesValidation="false" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>"
                                                        CommandName="EditConsultant" ForeColor="Blue" Font-Underline="false" Text='<%#Eval("ConsCode") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ConsFirstName" HeaderText="First Name" ReadOnly="True"
                                                SortExpression="FirstName" />
                                            <asp:BoundField DataField="ConsMiddleName" HeaderText="Middle Name" ReadOnly="True"
                                                SortExpression="MiddleName" />
                                            <asp:BoundField DataField="ConsLastName" HeaderText="Last Name" ReadOnly="True" SortExpression="LastName" />
                                            <asp:BoundField DataField="ConsAddress" HeaderText="Address" ReadOnly="True" SortExpression="ConsAddress" />
                                            <asp:BoundField DataField="ConsDOB" HeaderText="Birth Date" ReadOnly="True" SortExpression="ConsDOB"
                                                DataFormatString="{0:d}" />
                                            <asp:BoundField DataField="ConsDOJ" HeaderText="Joining Date" ReadOnly="True" SortExpression="ConsDOJ"
                                                DataFormatString="{0:d}" />
                                             <asp:BoundField DataField="WardNo" HeaderText="Ward No" ReadOnly="True"
                                                SortExpression="WardNo" />
                                            <asp:BoundField DataField="Fees" HeaderText="Fees" ReadOnly="True"
                                                SortExpression="Fees" />                                         
                                            <asp:BoundField DataField="EntryBy" HeaderText="Entry By" ReadOnly="True" SortExpression="EntryBy" />
                                            <asp:BoundField DataField="EntryDate" HeaderText="Entry Date" ReadOnly="True" SortExpression="EntryDate"
                                                DataFormatString="{0:d}" />
                                            <asp:BoundField DataField="ChangeBy" HeaderText="Change By" ReadOnly="True" SortExpression="ChangeBy" />
                                            <asp:BoundField DataField="ChangeDate" HeaderText="Change Date" ReadOnly="True" SortExpression="ChangeDate"
                                                DataFormatString="{0:d}" />
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
    <table>
        <tr>
            <td>
                <asp:Button runat="server" ID="hiddenTargetControlForModalPopup" Style="display: none" />
                <cc:ModalPopupExtender runat="server" ID="programmaticModalPopup" BehaviorID="programmaticModalPopupBehavior"
                    TargetControlID="hiddenTargetControlForModalPopup" PopupControlID="pnlGrid" BackgroundCssClass="modalBackground"
                    DropShadow="true" PopupDragHandleControlID="programmaticPopupDragHandle" RepositionMode="RepositionOnWindowScroll">
                </cc:ModalPopupExtender>
                <asp:Panel ID="pnlGrid" runat="server" Width="500px" Style="text-align: center; background-color: #E0F0E8;
                    height: 350px; display: none;">
                    <table width="100%" style="border-color: Green; border-style: solid; border-width: 2px"
                        cellpadding="0">
                        <tr>
                            <td>
                                <asp:Panel ID="Panel2" runat="server" Width="99.7%" Style="text-align: center; background-color: #E0F0E8;
                                    height: 340px;" BorderColor="Green" BorderStyle="Solid" BorderWidth="1px">
                                    <table width="100%">
                                        <tr>
                                            <td align="center" colspan="2">
                                                <asp:Label ID="lblHeading" runat="server" Text="Consultant Master" Font-Names="Verdana"
                                                    Font-Size="15px" Font-Bold="true" ForeColor="#3b3535"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="lblConsultantCode" runat="server" Text="Consultant Code :" Font-Names="Verdana"
                                                    Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtConsultantCode" runat="server" MaxLength="10" Font-Names="Verdana"
                                                    ReadOnly="true" Font-Size="11px" Width="150px"></asp:TextBox><asp:RequiredFieldValidator
                                                        ID="rfvConsultantCode" runat="server" ForeColor="Red" ControlToValidate="txtConsultantCode"
                                                        Font-Size="11" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="lblFirstName" runat="server" Text="First Name :" Font-Names="Verdana"
                                                    Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtFirstName" runat="server" MaxLength="50" Font-Names="Verdana"
                                                    Font-Size="11px" Width="150px"></asp:TextBox><asp:RequiredFieldValidator ID="rfvFirstName"
                                                        runat="server" ForeColor="Red" ControlToValidate="txtFirstName" Font-Size="11"
                                                        ValidationGroup="Save">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="lblMiddleName" runat="server" Text="Middle Name :" Font-Names="Verdana"
                                                    Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtMiddleName" runat="server" MaxLength="50" Font-Names="Verdana"
                                                    Font-Size="11px" Width="150px"></asp:TextBox><asp:RequiredFieldValidator ID="rfvMiddleName"
                                                        runat="server" ForeColor="Red" ControlToValidate="txtMiddleName" Font-Size="11"
                                                        ValidationGroup="Save">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="lblLastName" runat="server" Text="Last Name :" Font-Names="Verdana"
                                                    Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtLastName" runat="server" MaxLength="50" Font-Names="Verdana"
                                                    Font-Size="11px" Width="150px"></asp:TextBox><asp:RequiredFieldValidator ID="rfvLastName"
                                                        runat="server" ForeColor="Red" ControlToValidate="txtLastName" Font-Size="11"
                                                        ValidationGroup="Save">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="lblAddress" runat="server" Text="Address :" Font-Names="Verdana" Font-Size="11px"
                                                    ForeColor="#3b3535"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtAddress" runat="server" MaxLength="50" Font-Names="Verdana" Font-Size="11px"
                                                    Width="150px"></asp:TextBox><asp:RequiredFieldValidator ID="rfvAddress" runat="server"
                                                        ForeColor="Red" ControlToValidate="txtAddress" Font-Size="11" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="lblBirthDate" runat="server" Text="Birth Date :" Font-Names="Verdana"
                                                    Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtBirthDate" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                    Width="80px" OnKeyDown="return isNumber(event);" MaxLength="10" onkeypress="return KeyValid(event);"></asp:TextBox>
                                                    <cc:CalendarExtender ID="CalDOBDate" runat="server" TargetControlID="txtBirthDate" Format="dd/MM/yyyy">
                                                    </cc:CalendarExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="lblJoiningDate" runat="server" Text="Joining Date :" Font-Names="Verdana"
                                                    Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtJoiningDate" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                    Width="80px" OnKeyDown="return isNumber(event);" MaxLength="10" onkeypress="return KeyValid(event);"></asp:TextBox>
                                                    <cc:CalendarExtender ID="CalDOJDate" runat="server" TargetControlID="txtJoiningDate" Format="dd/MM/yyyy">
                                                    </cc:CalendarExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                              <asp:Label ID="Label8" runat="server" Text="Ward :" Font-Names="Verdana"
                                                    Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddlWard" runat="server"  Font-Names="Verdana"
                                                    Font-Size="11px" CausesValidation="true">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvddlWard" runat="server"
                                                        ForeColor="Red" ControlToValidate="ddlWard" Font-Size="11" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                         <tr>
                                            <td align="right">
                                              <asp:Label ID="Label12" runat="server" Text="Fees :" Font-Names="Verdana"
                                                    Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                            </td>
                                            <td align="left">
                                               <asp:TextBox ID="txtFees" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                    Width="80px" OnKeyDown="return isNumber(event);" MaxLength="10"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                        ForeColor="Red" ControlToValidate="txtFees" Font-Size="11" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr align="center">
                                            <td align="center" colspan="2">
                                                <asp:Button ID="BtnSave" runat="server" Text="Save" Font-Names="Verdana" Font-Size="12px"
                                                    BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black"
                                                    ValidationGroup="Save" onmouseover="SetBtnMouseOver(this)" onmouseout="SetBtnMouseOut(this)"
                                                    OnClick="BtnSave_Click" />
                                                <asp:Button ID="BtnClose" runat="server" Text="Close" Font-Names="Verdana" Font-Size="12px"
                                                    OnClientClick="return hideModalPopup();" BackColor="#3b3535" ForeColor="White"
                                                    Width="80px" Style="border: 1px solid black" onmouseover="SetBtnMouseOver(this)"
                                                    onmouseout="SetBtnMouseOut(this)" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Button runat="server" ID="hiddenTargetControlForModalPopupEdit" Style="display: none" />
                <cc:ModalPopupExtender runat="server" ID="programmaticModalPopupEdit" BehaviorID="programmaticModalPopupBehaviorEdit"
                    TargetControlID="hiddenTargetControlForModalPopupEdit" PopupControlID="pnlEditGrid"
                    BackgroundCssClass="modalBackground" DropShadow="true" PopupDragHandleControlID="programmaticPopupDragHandleEdit"
                    RepositionMode="RepositionOnWindowScroll">
                </cc:ModalPopupExtender>
                <asp:Panel ID="pnlEditGrid" runat="server" Width="500px" Style="text-align: center;
                    background-color: #E0F0E8; height: 350px; display: none;">
                    <table width="100%" style="border-color: Green; border-style: solid; border-width: 2px"
                        cellpadding="0">
                        <tr>
                            <td>
                                <asp:Panel ID="Panel3" runat="server" Width="99.7%" Style="text-align: center; background-color: #E0F0E8;
                                    height: 340px;" BorderColor="Green" BorderStyle="Solid" BorderWidth="1px">
                                    <table width="100%">
                                        <tr>
                                            <td align="center" colspan="2">
                                                <asp:Label ID="Label2" runat="server" Text="Update Consultant Master" Font-Names="Verdana"
                                                    Font-Size="15px" Font-Bold="true" ForeColor="#3b3535"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label3" runat="server" Text="Consultant Code :" Font-Names="Verdana"
                                                    Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtEditConsultantCode" runat="server" MaxLength="10" Font-Names="Verdana"
                                                    ReadOnly="true" Font-Size="11px" Width="150px"></asp:TextBox><asp:RequiredFieldValidator
                                                        ID="rfvEditConsultantCode" runat="server" ForeColor="Red" ControlToValidate="txtEditConsultantCode"
                                                        Font-Size="13" ValidationGroup="Edit">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label4" runat="server" Text="First Name :" Font-Names="Verdana" Font-Size="11px"
                                                    ForeColor="#3b3535"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtEditFirstName" runat="server" MaxLength="50" Font-Names="Verdana"
                                                    Font-Size="11px" Width="150px"></asp:TextBox><asp:RequiredFieldValidator ID="rfvEditFirstName"
                                                        runat="server" ForeColor="Red" ControlToValidate="txtEditFirstName" Font-Size="13"
                                                        ValidationGroup="Edit">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label5" runat="server" Text="Middle Name :" Font-Names="Verdana" Font-Size="11px"
                                                    ForeColor="#3b3535"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtEditMiddleName" runat="server" MaxLength="50" Font-Names="Verdana"
                                                    Font-Size="11px" Width="150px"></asp:TextBox><asp:RequiredFieldValidator ID="rfvEditMiddleName"
                                                        runat="server" ForeColor="Red" ControlToValidate="txtEditMiddleName" Font-Size="13"
                                                        ValidationGroup="Edit">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label6" runat="server" Text="Last Name :" Font-Names="Verdana" Font-Size="11px"
                                                    ForeColor="#3b3535"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtEditLastName" runat="server" MaxLength="50" Font-Names="Verdana"
                                                    Font-Size="11px" Width="150px"></asp:TextBox><asp:RequiredFieldValidator ID="rfvEditLastName"
                                                        runat="server" ForeColor="Red" ControlToValidate="txtEditLastName" Font-Size="13"
                                                        ValidationGroup="Edit">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label7" runat="server" Text="Address :" Font-Names="Verdana" Font-Size="11px"
                                                    ForeColor="#3b3535"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtEditAddress" runat="server" MaxLength="50" Font-Names="Verdana"
                                                    Font-Size="11px" Width="150px"></asp:TextBox><asp:RequiredFieldValidator ID="rfvEditAddress"
                                                        runat="server" ForeColor="Red" ControlToValidate="txtEditAddress" Font-Size="13"
                                                        ValidationGroup="Edit">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label9" runat="server" Text="Birth Date :" Font-Names="Verdana" Font-Size="11px"
                                                    ForeColor="#3b3535"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtEditBirthDate" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                    Width="80px" OnKeyDown="return isNumber(event);" MaxLength="10" onkeypress="return KeyValid(event);"></asp:TextBox>
                                                    <cc:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtEditBirthDate" Format="dd/MM/yyyy">
                                                    </cc:CalendarExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Label11" runat="server" Text="Joining Date :" Font-Names="Verdana"
                                                    Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtEditJoiningDate" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                    Width="80px" OnKeyDown="return isNumber(event);" MaxLength="10" onkeypress="return KeyValid(event);"></asp:TextBox>
                                                    <cc:CalendarExtender  ID="CalendarExtender2" runat="server" TargetControlID="txtEditJoiningDate" Format="dd/MM/yyyy">
                                                    </cc:CalendarExtender>
                                            </td>
                                        </tr>
                                         <tr>
                                            <td align="right">
                                              <asp:Label ID="Label13" runat="server" Text="Ward :" Font-Names="Verdana"
                                                    Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddlEditWardNo" runat="server"  Font-Names="Verdana"
                                                    Font-Size="11px">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                                        ForeColor="Red" ControlToValidate="ddlEditWardNo" Font-Size="11" ValidationGroup="Edit">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                         <tr>
                                            <td align="right">
                                              <asp:Label ID="Label14" runat="server" Text="Fees :" Font-Names="Verdana"
                                                    Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                            </td>
                                            <td align="left">
                                               <asp:TextBox ID="txtEditFees" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                    Width="80px" OnKeyDown="return isNumber(event);" MaxLength="10"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                                        ForeColor="Red" ControlToValidate="txtEditFees" Font-Size="11" ValidationGroup="Edit">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr align="center">
                                            <td align="center" colspan="6">
                                                <asp:Button ID="BtnEdit" runat="server" Text="Update" Font-Names="Verdana" Font-Size="12px"
                                                    ValidationGroup="Edit" BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black"
                                                    onmouseover="SetBtnMouseOver(this)" onmouseout="SetBtnMouseOut(this)" OnClick="BtnEdit_Click" />
                                                <asp:Button ID="BtnEditClose" runat="server" Text="Close" Font-Names="Verdana" Font-Size="12px"
                                                    OnClientClick="return hideEditModalPopup();" BackColor="#3b3535" ForeColor="White"
                                                    Width="80px" Style="border: 1px solid black" onmouseover="SetBtnMouseOver(this)"
                                                    onmouseout="SetBtnMouseOut(this)" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Button runat="server" ID="hiddenTargetControlForModalPopupDelete" Style="display: none" />
                <cc:ModalPopupExtender runat="server" ID="modalpopupDelete" BehaviorID="DeleteprogrammaticModalPopupBehavior"
                    TargetControlID="hiddenTargetControlForModalPopupDelete" PopupControlID="pnlDelete"
                    BackgroundCssClass="modalBackground" DropShadow="true" PopupDragHandleControlID="DeleteprogrammaticPopupDragHandle"
                    RepositionMode="RepositionOnWindowScroll">
                </cc:ModalPopupExtender>
                <asp:Panel ID="pnlDelete" runat="server" Width="500px" Style="text-align: center;
                    background-color: #E0F0E8; display: none;" BorderColor="Green" BorderStyle="Solid"
                    BorderWidth="2px">
                    <table width="100%">
                        <tr align="center">
                            <td align="center" colspan="6" style="font-family: Verdana; font-size: 15px; color: Green;
                                font-weight: bold;">
                                <asp:Label ID="Label10" runat="server" Text="Do You Want To Delete Record ?" Font-Names="Verdana"
                                    Font-Size="15px" ForeColor="#3b3535"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="font-family: Verdana; width: 25%; font-size: 11px; color: #006600;">
                                Delete Remark :
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtDeleteRemark" runat="server" MaxLength="100" TextMode="MultiLine"
                                    Width="80%" Font-Names="Verdana" Font-Size="11px" Style="border: 1px solid Green;"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="center">
                            <td align="center" colspan="6">
                                <asp:Button ID="BtnDeleteOk" runat="server" Text="Delete" Font-Names="Verdana" Font-Size="12px"
                                    CausesValidation="false" BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black"
                                    onmouseover="SetBtnMouseOver(this)" onmouseout="SetBtnMouseOut(this)" OnClick="BtnDeleteOk_Click" />
                                <asp:Button ID="Button2" runat="server" Text="Close" Font-Names="Verdana" Font-Size="12px"
                                    OnClientClick="return hideDeleteModalPopup();" BackColor="#3b3535" ForeColor="White"
                                    Width="80px" Style="border: 1px solid black" onmouseover="SetBtnMouseOver(this)"
                                    onmouseout="SetBtnMouseOut(this)" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <asp:Label ID="lblMessage" runat="server" Text="" Font-Names="Verdana" Font-Size="11px"
                                    ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <input id="hdnPanel" type="hidden" runat="server" value="none" />
</asp:Content>

