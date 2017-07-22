<%@ Page Title="" Language="C#" MasterPageFile="~/mstAdmin.Master" AutoEventWireup="true" CodeBehind="frmMaterialInword.aspx.cs" Inherits="Hospital.StoreForms.frmMaterialInword" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../css/content.css" rel="stylesheet" type="text/css" />
    <script src="../Notimoo/mootools-1.2-core.js" type="text/javascript"></script>
    <link href="../Notimoo/notimoo-documented-1.1.css" rel="stylesheet" type="text/css" />
    <script src="../Notimoo/notimoo-documented-1.1.js" type="text/javascript"></script>
    <link href="../Notimoo/notimoo-v1.1.css" rel="stylesheet" type="text/css" />
    <script src="../Notimoo/notimoo-v1.1.js" type="text/javascript"></script>
    <link href="../Style/AdminStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function resetControls() {

            var ddlSupplierName = document.getElementById("ContentPlaceHolder1_ddlSupplier");
            var dtStartDate = document.getElementById("ContentPlaceHolder1_txtStartDate");
            var dtEndDate = document.getElementById("ContentPlaceHolder1_txtEndDate");
            var lInwordCode = document.getElementById("ContentPlaceHolder1_txtInwardNumber");
            var pnl = document.getElementById("ContentPlaceHolder1_PanelView");

            pnl.style.display = "none";

            var currDate = new Date();
            var month = currDate.getMonth() + 1;
            var day = currDate.getDate();
            var year = currDate.getFullYear();
            if (month.toString().length == 1) {
                month = "0" + month;
            }
            if (day.toString().length == 1) {
                day = "0" + day;
            }

            dtStartDate.value = day + '/' + month + '/' + year;
            dtEndDate.value = day + '/' + month + '/' + year;
            ddlSupplierName.selectedIndex = 0;
            lInwordCode.value = "";


            return false;
        }

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
        <cc:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </cc:ToolkitScriptManager>
    </div>
    <table width="100%">
        <tr>
            <td align="center">
                <asp:Panel ID="pnlShow" runat="server" Width="600px" Style="text-align: center;">
                    <table width="100%" style="border-color: Green; border-style: solid; border-width: 1px">
                        <tr>
                            <td colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="4">
                                <asp:Label ID="lblHeading" runat="server" Text="Meterial Inward Master" Font-Names="Verdana"
                                    Font-Size="16px" Font-Bold="true" ForeColor="#006600"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 15%; padding-right: 10px;" colspan="4">
                                <asp:Button ID="BtnAddNew" runat="server" Text="Add New" Font-Names="Verdana" Font-Size="13px"
                                    BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black"
                                    onmouseover="SetBtnMouseOver(this)" onmouseout="SetBtnMouseOut(this)" OnClick="BtnAddNew_Click" />
                                <asp:Button ID="btnShowAll" runat="server" Text="Show All" Font-Names="Verdana" Font-Size="13px"
                                    BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black"
                                    onmouseover="SetBtnMouseOver(this)" onmouseout="SetBtnMouseOut(this)" OnClick="btnShowAll_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 25%">
                                <asp:Label ID="lblStartDate" runat="server" Text="Start Date :" Font-Names="Verdana"
                                    Font-Size="11px" ForeColor="#006600"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtStartDate" runat="server" MaxLength="10" Font-Names="Verdana"
                                    OnKeyDown="return isNumber(event);" Font-Size="11px" Width="120px"></asp:TextBox><cc:CalendarExtender
                                        ID="CalStartDate" runat="server" TargetControlID="txtStartDate" Format="dd/MM/yyyy">
                                    </cc:CalendarExtender>
                            </td>
                            <td align="right">
                                <asp:Label ID="lblEndDate" runat="server" Text="End Date :" Font-Names="Verdana"
                                    Font-Size="11px" ForeColor="#006600"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtEndDate" runat="server" MaxLength="10" Font-Names="Verdana" Font-Size="11px"
                                    Width="120px" OnKeyDown="return isNumber(event);"></asp:TextBox>
                                <cc:CalendarExtender ID="CalEndDate" runat="server" TargetControlID="txtEndDate"
                                    Format="dd/MM/yyyy">
                                </cc:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="lblInwardNumber" runat="server" Text="Inward Number :" ForeColor="#006600"
                                    Font-Size="11px" Font-Names="Verdana"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtInwardNumber" runat="server" Width="120px" Font-Size="11px" Font-Names="Verdana"></asp:TextBox>                      
                            </td>
                            <td colspan="2">
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="Lable1" runat="server" Text="Supplier Name :" Font-Names="Verdana"
                                    Font-Size="11px" ForeColor="#006600"></asp:Label>
                            </td>
                            <td align="left" colspan="3">
                                <asp:DropDownList ID="ddlSupplier" runat="server" Font-Names="Verdana" Font-Size="11px"
                                    Width="250px">
                                </asp:DropDownList>                         
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                &nbsp;
                            </td>
                        </tr>
                        <tr align="center">
                            <td align="center" colspan="4">
                                <asp:Button ID="btnView" runat="server" Text="View" Font-Names="Verdana" Font-Size="13px" ValidationGroup="ViewInward"
                                    BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black"
                                    onmouseover="SetBtnMouseOver(this)" onmouseout="SetBtnMouseOut(this)" OnClick="btnView_Click" />
                                <asp:Button ID="BtnReset" runat="server" Text="Reset" Font-Names="Verdana" Font-Size="13px"
                                    OnClientClick="return resetControls();" BackColor="#3b3535" ForeColor="White"
                                    Width="80px" Style="border: 1px solid black" onmouseover="SetBtnMouseOver(this)"
                                    onmouseout="SetBtnMouseOut(this)" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td>
                <asp:Panel ID="PanelView" runat="server" BorderColor="Green" BorderStyle="Solid"
                    BorderWidth="1px" Style="display: none; width: 100%;">
                    <table width="100%">
                        <tr>
                            <td align="left">
                                <asp:Label ID="lblPageCount" runat="server" Text="" Font-Names="Verdana" Font-Size="11px"
                                    ForeColor="#006600"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lblRowCount" runat="server" Text="" Font-Names="Verdana" Font-Size="11px"
                                    ForeColor="#006600"></asp:Label>
                            </td>
                            <td align="right">
                                <asp:Button ID="BtnDelete" runat="server" Text="Delete" Font-Names="Verdana" Font-Size="13px"
                                    BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black"
                                    onmouseover="SetBtnMouseOver(this)" onmouseout="SetBtnMouseOut(this)" OnClick="BtnDelete_Click" />
                                <asp:Button ID="BtnDownload" runat="server" Text="Download" Font-Names="Verdana"
                                    Font-Size="13px" BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black"
                                    onmouseover="SetBtnMouseOver(this)" onmouseout="SetBtnMouseOut(this)" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:Panel ID="Pnl" ScrollBars="Horizontal" runat="server" Width="100%" BorderColor="Green"
                                    BorderStyle="Solid" BorderWidth="1px">
                                    <asp:GridView ID="dgvMaterialInwardShow" runat="server" CellPadding="4" ForeColor="#333333"
                                        OnRowCommand="dgvMaterialInwardShow_RowCommand" DataKeyNames="InwardNo" GridLines="Both"
                                        OnRowDataBound="dgvMaterialInwardShow_RowDataBound" OnDataBound="dgvMaterialInwardShow_DataBound"
                                        OnPageIndexChanged="dgvMaterialInwardShow_PageIndexChanged" OnPageIndexChanging="dgvMaterialInwardShow_PageIndexChanging"
                                        Font-Names="Verdana" Width="100%" Font-Size="Small" AllowPaging="true" BorderColor="Green"
                                        BorderStyle="Solid" BorderWidth="1px" PageSize="20" AutoGenerateColumns="false">
                                        <FooterStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" />
                                        <RowStyle BackColor="#E0F0E8" Font-Size="11px" Wrap="False" />
                                        <PagerStyle BackColor="#3b3535" ForeColor="White" HorizontalAlign="Left" Font-Size="11px" />
                                        <SelectedRowStyle BackColor="#006600" Font-Bold="True" ForeColor="#333333" />
                                        <HeaderStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" Font-Size="12px"
                                            Wrap="False" />
                                        <EditRowStyle BackColor="#2461BF" />
                                        <AlternatingRowStyle BackColor="White" Wrap="False" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkDelete" runat="server" CausesValidation="false" AutoPostBack="true"
                                                        OnCheckedChanged="chkDelete_CheckedChanged" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Inward No" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkInwardNo" runat="server" CausesValidation="false" AutoPostBack="true"
                                                        Text='<%# Eval("InwardNo") %>' CommandArgument="<%# ((GridViewRow)Container).RowIndex %>"
                                                        CommandName="ShowInward"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="InwardDate" HeaderText="Inward Date" ReadOnly="true" SortExpression="InwardDate"
                                                DataFormatString="{0:d}" />
                                            <asp:TemplateField HeaderText="Supplier Code" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldgvSupplierCode" runat="server" Text='<%# Eval("SupplierCode")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--  <asp:BoundField DataField="SupplierCode" HeaderText="Supplier Code" ReadOnly="true"
                                                SortExpression="SupplierCode" />--%>
                                            <asp:BoundField DataField="SupplierName" HeaderText="Supplier Name" ReadOnly="true"
                                                SortExpression="SupplierName" />
                                            <asp:TemplateField HeaderText="Total Inward Amount" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldgvTotalInwardAmt" runat="server" Text='<%# Eval("TotalInwardAmount") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:BoundField DataField="TotalInwardAmount" HeaderText="Total Inward Amount " ReadOnly="true"
                                                SortExpression="TotalInwardAmount" />--%>
                                            <asp:BoundField DataField="EntryBy" HeaderText="EntryBy" ReadOnly="true" SortExpression="EntryBy" />
                                            <asp:BoundField DataField="EntryDate" HeaderText="EntryDate" ReadOnly="true" SortExpression="EntryDate"
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
                <asp:Panel ID="pnlGrid" runat="server" Width="1100px" Style="text-align: center;
                    background-color: #E0F0E8; height: 600px; display: none; border-color: Green;
                    border-style: solid; border-width: 2px">
                    <table width="100%" cellpadding="0">
                        <tr>
                            <td>
                                <%-- <asp:UpdatePanel runat="server" ID="UdpnlInward">
                            <ContentTemplate>--%>
                                <asp:Panel ID="Panel1" runat="server" Height="595px" Style="border-color: Green;
                                    border-style: solid; border-width: 1px">
                                    <table width="100%">
                                        <tr>
                                            <td colspan="4" align="center">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" align="center">
                                                <asp:Label ID="Label8" runat="server" Text="Material Inward Master" Font-Names="Verdana"
                                                    Font-Size="16px" Font-Bold="true" ForeColor="#006600"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" style="border-top: 1px Solid Green; border-bottom: 1px Solid Green"
                                                align="left">
                                                <asp:Label ID="Label9" runat="server" Text="Material Inward Details :" Font-Names="Verdana"
                                                    Font-Size="13px" Font-Bold="true" ForeColor="#006600"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="width: 25%;">
                                                <asp:Label ID="lblInwordNo" runat="server" Text="Inward Number :" Font-Names="Verdana"
                                                    Font-Size="11px" ForeColor="#006600"></asp:Label>
                                            </td>
                                            <td align="left" style="width: 25%;">
                                                <asp:TextBox ID="txtInwardNo" runat="server" ReadOnly="true" Width="140px" Font-Names="Verdana"
                                                    Font-Size="11px" Font-Bold="true"></asp:TextBox>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="Label6" runat="server" Text="Inword Date :" Font-Names="Verdana" Font-Size="11px"
                                                    ForeColor="#006600"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtInwordDate" runat="server" Width="80px" Font-Names="Verdana"
                                                    OnKeyDown="return isNumber(event);" Font-Size="11px" MaxLength="10"></asp:TextBox>
                                                <cc:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtInwordDate"
                                                    Format="dd/MM/yyyy">
                                                </cc:CalendarExtender>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Font-Size="11"
                                                    ControlToValidate="txtInwordDate" ForeColor="Red" ValidationGroup="SaveInward">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="width: 25%;">
                                                <asp:Label ID="lblSupplierName" runat="server" Text="Supplier Name :" Font-Names="Verdana"
                                                    Font-Size="11px" ForeColor="#006600"></asp:Label>
                                            </td>
                                            <td align="left" colspan="3">
                                                <asp:DropDownList ID="ddlSupplierName" runat="server" Width="400px" Font-Names="Verdana"
                                                    Font-Size="11px" CausesValidation="true">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvsupplier" runat="server" ForeColor="Red" InitialValue="0"
                                                    ControlToValidate="ddlSupplierName" Font-Size="11" ValidationGroup="SaveInward">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="width: 25%;">
                                                <asp:Label ID="lblGroup" runat="server" Font-Names="Verdana" Font-Size="11px" ForeColor="#006600"
                                                    Text="Group Type :"></asp:Label>
                                            </td>
                                            <td align="left" colspan="3">
                                                <asp:DropDownList ID="ddlGroup" runat="server" AutoPostBack="true" Width="170px"
                                                    OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged" Font-Names="Verdana" Font-Size="11px">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red"
                                                    InitialValue="0" ControlToValidate="ddlGroup" Font-Size="11" ValidationGroup="SaveInward">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" style="border-top: 1px Solid Green; border-bottom: 1px Solid Green"
                                                align="left">
                                                <asp:Label ID="Label12" runat="server" Text="Item Details :" Font-Names="Verdana"
                                                    Font-Size="13px" Font-Bold="true" ForeColor="#006600"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" style="width: 100%; height: 270px; vertical-align: top;">
                                                <asp:Panel ID="pnlGridItem" ScrollBars="Both" runat="server" Width="99.3%" BorderColor="Green"
                                                    BorderStyle="Solid" BorderWidth="1px" Style="display: none">
                                                    <asp:GridView ID="dgvMaterialInword" runat="server" CellPadding="4" ForeColor="#333333"
                                                        DataKeyNames="ItemCode" GridLines="Both" Font-Names="Verdana" Width="100%" Font-Size="Small"
                                                        AllowPaging="true" BorderColor="Green" BorderStyle="Solid" BorderWidth="1px"
                                                        PageSize="20" AutoGenerateColumns="false">
                                                        <FooterStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" />
                                                        <RowStyle BackColor="#E0F0E8" Font-Size="11px" Wrap="False" />
                                                        <PagerStyle BackColor="#3b3535" ForeColor="White" HorizontalAlign="Left" Font-Size="11px" />
                                                        <SelectedRowStyle BackColor="#006600" Font-Bold="True" ForeColor="#333333" />
                                                        <HeaderStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" Font-Size="12px"
                                                            Wrap="False" />
                                                        <EditRowStyle BackColor="#2461BF" />
                                                        <AlternatingRowStyle BackColor="White" Wrap="False" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="ItemCode" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblItemCode" runat="server" CausesValidation="false" Text='<%# Eval("ItemCode") %>'
                                                                        CommandArgument="<%# ((GridViewRow)Container).RowIndex %>"> </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ItemDesc" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblItemDesc" runat="server" CausesValidation="false" Text='<%# Eval("ItemDesc") %>'
                                                                        CommandArgument="<%# ((GridViewRow)Container).RowIndex %>"> </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Quantity" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtQty" runat="server" CausesValidation="false" Text="" Font-Names="Verdana" Font-Size="11px"> </asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Unit" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUnitCode" runat="server" CausesValidation="false" Text='<%# Eval("UnitCode") %>'
                                                                        CommandArgument="<%# ((GridViewRow)Container).RowIndex %>"> </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Rate" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRate" runat="server" Text='<%# Eval("Rate") %>' CommandArgument="<%# ((GridViewRow)Container).RowIndex %>"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="TotalAmount" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTotalAmount" runat="server" CausesValidation="false" Text=""> </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Other Charges" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtOtherCharges" Text="" runat="server" CausesValidation="false" Font-Names="Verdana" Font-Size="11px"> </asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Total Product Amount" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTotalProductAmount" runat="server" Text=""></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Select" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkSelect" runat="server" CausesValidation="false" AutoPostBack="true"
                                                                        OnCheckedChanged="chkSelect_CheckedChanged" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" style="border-top: 1px Solid Green; border-bottom: 1px Solid Green"
                                                align="left">
                                                <asp:Label ID="Label13" runat="server" Text="Inward Detail :" Font-Names="Verdana"
                                                    Font-Size="13px" Font-Bold="true" ForeColor="#006600"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="4" style="padding-left: 10px;">
                                                <asp:Label ID="Label7" runat="server" Text="Total Inward Amount :" Font-Names="Verdana"
                                                    Font-Size="14px" ForeColor="#006600" Font-Bold="true"></asp:Label>
                                                <asp:Label ID="lblTotalInwardAmount" runat="server" ForeColor="#006600" Font-Names="Verdana"
                                                    Font-Size="14px" Font-Bold="true"></asp:Label>
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
                                                    BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black"
                                                    onmouseover="SetBtnMouseOver(this)" onmouseout="SetBtnMouseOut(this)" OnClick="BtnSave_Click"
                                                    ValidationGroup="SaveInward" />
                                                <asp:Button ID="BtnClose" runat="server" BackColor="#3b3535" Font-Names="Verdana"
                                                    Font-Size="13px" ForeColor="White" OnClientClick="return hideModalPopup();" onmouseout="SetBtnMouseOut(this)"
                                                    onmouseover="SetBtnMouseOver(this)" Style="border: 1px solid black" Text="Close"
                                                    Width="80px" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <%--   </ContentTemplate>
                        </asp:UpdatePanel>--%>
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
                <asp:Panel ID="pnlEditGrid" runat="server" Width="1100px" Style="text-align: center;
                    background-color: #E0F0E8; display: none">
                    <table width="100%" style="border-color: Green; border-style: solid; border-width: 2px"
                        cellpadding="0">
                        <tr>
                            <td>
                                <asp:Panel ID="Panel3" runat="server" Width="99.7%" Style="text-align: center; background-color: #E0F0E8;
                                    height: 470px;" BorderColor="Green" BorderStyle="Solid" BorderWidth="1px">
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                                    <ContentTemplate>
                                                        <asp:Panel ID="Panel2" runat="server">
                                                            <table width="100%">
                                                                <tr>
                                                                    <td colspan="4" align="center">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="4" align="center">
                                                                        <asp:Label ID="Label1" runat="server" Text="Material Inward Details" Font-Names="Verdana"
                                                                            Font-Size="16px" Font-Bold="true" ForeColor="#006600"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="4">
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="4">
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" style="width: 40%">
                                                                        <asp:Label ID="Label3" runat="server" Font-Names="Verdana" Font-Size="11px" ForeColor="#006600"
                                                                            Text="Group Type :"></asp:Label>
                                                                    </td>
                                                                    <td align="left" style="width: 10%">
                                                                        <asp:Label ID="lblGroupType" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                            Font-Bold="true" ForeColor="#006600"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" style="width: 40%">
                                                                        <asp:Label ID="Label2" runat="server" Text="Supplier Code :" Font-Names="Verdana"
                                                                            Font-Size="11px" ForeColor="#006600"></asp:Label>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:Label ID="lblSupplierCode" runat="server" Font-Names="Verdana" Font-Bold="true"
                                                                            Font-Size="11px" ForeColor="#006600"></asp:Label>
                                                                    </td>
                                                                    <td align="right">
                                                                        <asp:Label ID="Label4" runat="server" Text="Supplier Name :" Font-Names="Verdana"
                                                                            Font-Size="11px" ForeColor="#006600"></asp:Label>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:Label ID="lblSupplierNM" runat="server" Font-Names="Verdana" Font-Bold="true"
                                                                            Font-Size="11px" ForeColor="#006600"></asp:Label>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" style="width: 40%">
                                                                        <asp:Label ID="Label5" runat="server" Text="Inward Number :" Font-Names="Verdana"
                                                                            Font-Size="11px" ForeColor="#006600"></asp:Label>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:Label ID="lblInwardNum" runat="server" ReadOnly="true" Font-Names="Verdana"
                                                                            Font-Bold="true" Font-Size="11px" ForeColor="#006600"></asp:Label>
                                                                    </td>
                                                                    <td align="right">
                                                                        <asp:Label ID="Label11" runat="server" Text="Inword Date :" Font-Names="Verdana"
                                                                            Font-Size="11px" ForeColor="#006600"></asp:Label>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:Label ID="lblInwardDate" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                            Font-Bold="true" ForeColor="#006600"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="4">
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="4" style="width: 100%; height: 100px;">
                                                                        <asp:Panel ID="Panel4" ScrollBars="Both" runat="server" Width="99.3%" BorderColor="Green"
                                                                            Height="250px" BorderStyle="Solid" BorderWidth="1px">
                                                                            <asp:GridView ID="dgvInwardStatus" runat="server" CellPadding="4" ForeColor="#333333"
                                                                                DataKeyNames="ItemCode" GridLines="Both" Font-Names="Verdana" Width="100%" Font-Size="Small"
                                                                                AllowPaging="true" BorderColor="Green" BorderStyle="Solid" BorderWidth="1px"
                                                                                PageSize="20" AutoGenerateColumns="false">
                                                                                <FooterStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" />
                                                                                <RowStyle BackColor="#E0F0E8" Font-Size="11px" Wrap="False" />
                                                                                <PagerStyle BackColor="#3b3535" ForeColor="White" HorizontalAlign="Left" Font-Size="11px" />
                                                                                <SelectedRowStyle BackColor="#006600" Font-Bold="True" ForeColor="#333333" />
                                                                                <HeaderStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" Font-Size="12px"
                                                                                    Wrap="False" />
                                                                                <EditRowStyle BackColor="#2461BF" />
                                                                                <AlternatingRowStyle BackColor="White" Wrap="False" />
                                                                                <Columns>
                                                                                    <asp:BoundField DataField="ItemCode" HeaderText="Item Code" SortExpression="ItemCode"
                                                                                        ReadOnly="true" />
                                                                                    <asp:BoundField DataField="ItemDesc" HeaderText="Item Desc" SortExpression="Item Desc"
                                                                                        ReadOnly="false" />
                                                                                    <asp:BoundField DataField="Quantity" HeaderText="Quantity" SortExpression="Quantity"
                                                                                        ReadOnly="true" />
                                                                                    <asp:BoundField DataField="UnitDesc" HeaderText="Unit Desc" SortExpression="UnitDesc"
                                                                                        ReadOnly="true" />
                                                                                    <asp:BoundField DataField="TotalAmount" HeaderText="Total Amount" SortExpression="TotalAmount"
                                                                                        ReadOnly="true" />
                                                                                    <asp:BoundField DataField="OtherCharges" HeaderText="OtherCharges" SortExpression="OtherCharges"
                                                                                        ReadOnly="true" />
                                                                                    <asp:BoundField DataField="TotalProductAmount" HeaderText="Total Product Amount"
                                                                                        SortExpression="TotalProductAmount" ReadOnly="true" />
                                                                                    <asp:BoundField DataField="TotalInwardAmount" HeaderText="Total Inward Amount" SortExpression="TotalInwardAmount"
                                                                                        ReadOnly="true" />
                                                                                    <asp:BoundField DataField="EntryBy" HeaderText="EntryBy" SortExpression="EntryBy"
                                                                                        ReadOnly="true" />
                                                                                    <asp:BoundField DataField="EntryDate" HeaderText="EntryDate" SortExpression="EntryDate"
                                                                                        ReadOnly="true" />
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                        </asp:Panel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="4">
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" colspan="4">
                                                                        <asp:Button ID="Button4" runat="server" BackColor="#3b3535" Font-Names="Verdana"
                                                                            Font-Size="13px" ForeColor="White" OnClientClick="return hideEditModalPopup()();"
                                                                            onmouseout="SetBtnMouseOut(this)" onmouseover="SetBtnMouseOver(this)" Style="border: 1px solid black"
                                                                            Text="Close" Width="80px" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Button runat="server" ID="hiddenTargetControlForModalPopupDelete" Style="display: none" />
                <asp:Button runat="server" ID="Button1" Style="display: none" />
                <cc:ModalPopupExtender runat="server" ID="modalpopupDelete" BehaviorID="DeleteprogrammaticModalPopupBehavior"
                    TargetControlID="hiddenTargetControlForModalPopupDelete" PopupControlID="pnlDelete"
                    BackgroundCssClass="modalBackground" DropShadow="true" PopupDragHandleControlID="DeleteprogrammaticPopupDragHandle"
                    RepositionMode="RepositionOnWindowScroll">
                </cc:ModalPopupExtender>
                <asp:Panel ID="pnlDelete" runat="server" Width="500px" Style="text-align: center;
                    background-color: #E0F0E8; display: none;" BorderColor="Green" BorderStyle="Solid"
                    BorderWidth="2px">
                    <%-- <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>--%>
                    <table width="100%">
                        <tr align="center">
                            <td align="center" colspan="6" style="font-family: Verdana; font-size: 15px; color: Green;
                                font-weight: bold;">
                                <asp:Label ID="Label10" runat="server" Text="Do You Want To Delete Record ?" Font-Names="Verdana"
                                    Font-Size="15px" ForeColor="#006600"></asp:Label>
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
                    <%--     </ContentTemplate>
                    </asp:UpdatePanel>--%>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <input id="hdnPanel" type="hidden" runat="server" value="none" />
</asp:Content>
