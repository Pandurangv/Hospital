<%@ Page Title="" Language="C#" MasterPageFile="~/mstAdmin.Master" AutoEventWireup="true" CodeBehind="frmMaterialRequisiton.aspx.cs" Inherits="Hospital.StoreForms.frmMaterialRequisiton" %>

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

            var dtStartDate = document.getElementById("ContentPlaceHolder1_txtStartDate");
            var dtEndDate = document.getElementById("ContentPlaceHolder1_txtEndDate");
            var lRequisitionCode = document.getElementById("ContentPlaceHolder1_txtRequisitionNo");
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
            dtEndDate.value = day + '/' + month + '/' + year;
            lRequisitionCode.value = "";

            return false;
        }

        function show_alert(msg) {
            var show1 = new Notimoo({ locationVType: 'bottom', locationHType: 'right' });
            show1.show({ message: msg, sticky: false, visibleTime: 5000, width: 200 });
        }

        function Refresh() {
            window.location.reload();

        }
        function Startvalidkey(e) {
            var dtStartDate = document.getElementById("ContentPlaceHolder1_txtStartDate");
            dtStartDate.onkeydown = function (e) {
                var e = window.event || e
                var keyunicode = e.charCode || e.keyCode
                return (keyunicode <= '<%=Commons.CapA%>' && keyunicode >= '<%=Commons.F1Key%>' || keyunicode == '<%=Commons.Backspace%>' || keyunicode == '<%=Commons.Space%>'
             || keyunicode >= '<%=Commons.Zero%>' && keyunicode <= '<%=Commons.Nine%>' || keyunicode >= '<%=Commons.Grave%>' && keyunicode <= '<%=Commons.Smalli%>'
             || keyunicode == '<%=Commons.InvQueMark%>' || keyunicode == '<%=Commons.HoTab%>' || keyunicode == '<%=Commons.Smallo%>' || keyunicode >= '<%=Commons.Percent%>' && keyunicode <= '<%=Commons.OpenParenth%>' ||
             keyunicode == '<%=Commons.Percent%>') ? true : false
            }
        }

        function Endvalidkey(e) {
            var dtEndDate = document.getElementById("ContentPlaceHolder1_txtEndDate");
            dtEndDate.onkeydown = function (e) {
                var e = window.event || e
                var keyunicode = e.charCode || e.keyCode
                return (keyunicode <= '<%=Commons.CapA%>' && keyunicode >= '<%=Commons.F1Key%>' || keyunicode == '<%=Commons.Backspace%>' || keyunicode == '<%=Commons.Space%>'
             || keyunicode >= '<%=Commons.Zero%>' && keyunicode <= '<%=Commons.Nine%>' || keyunicode >= '<%=Commons.Grave%>' && keyunicode <= '<%=Commons.Smalli%>'
             || keyunicode == '<%=Commons.InvQueMark%>' || keyunicode == '<%=Commons.HoTab%>' || keyunicode == '<%=Commons.Smallo%>' || keyunicode >= '<%=Commons.Percent%>' && keyunicode <= '<%=Commons.OpenParenth%>' ||
             keyunicode == '<%=Commons.Percent%>') ? true : false
            }
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
        <cc:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </cc:ToolkitScriptManager>
        <asp:Panel ID="pnlAddNew" runat="server">
            <table width="100%">
                <tr>
                    <td>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    <table width="100%">
        <tr>
            <td>
                <asp:Panel ID="pnlShow" runat="server">
                    <table width="100%">
                        <tr>
                            <td colspan="3" align="center">
                                <table width="550px" style="border-color: Green; border-style: solid; border-width: 1px">
                                    <tr>
                                        <td colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" align="center">
                                            <asp:Label ID="Label7" runat="server" Text="Material Requisition  Master" Font-Names="Verdana"
                                                Font-Size="16px" Font-Bold="true" ForeColor="#006600"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td align="right" style="width: 35%;">
                                            <asp:Button ID="BtnAddNew" runat="server" Text="Add New" Font-Names="Verdana" Font-Size="13px"
                                                BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black"
                                                onmouseover="SetBtnMouseOver(this)" onmouseout="SetBtnMouseOut(this)" OnClick="BtnAddNew_Click" />
                                            <asp:Button ID="btnShowAll" runat="server" Text="Show All" Font-Names="Verdana" Font-Size="13px"
                                                BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black"
                                                onmouseover="SetBtnMouseOver(this)" onmouseout="SetBtnMouseOut(this)" OnClick="btnShowAll_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="lblStartDate" runat="server" Text="Start Date :" Font-Names="Verdana"
                                                Font-Size="11px" ForeColor="#006600"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtStartDate" runat="server" OnKeyDown="return Startvalidkey(event);"
                                                onkeypress="return KeyValid(event);" MaxLength="10" Width="80px" Font-Size="11px"
                                                Font-Names="Verdana"></asp:TextBox>
                                            <cc:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtStartDate"
                                                Format="dd/MM/yyyy" EnableViewState="true">
                                            </cc:CalendarExtender>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lblEndDate" runat="server" Text="End Date :" Font-Names="Verdana"
                                                Font-Size="11px" ForeColor="#006600"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtEndDate" runat="server" OnKeyDown="return Startvalidkey(event);"
                                                onkeypress="return KeyValid(event);" MaxLength="10" Width="80px" Font-Size="11px"
                                                Font-Names="Verdana"></asp:TextBox>
                                            <cc:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtEndDate"
                                                Format="dd/MM/yyyy" EnableViewState="true">
                                            </cc:CalendarExtender>
                                        </td>
                                    </tr>
                                    <%--<tr>
                                        <td align="right">
                                            <asp:Label ID="lblInwardNo" runat="server" Text="InwardNo :" Font-Names="Verdana"
                                                Font-Size="11px" ForeColor="#006600"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtInwardNo" runat="server" Width="120px" Font-Size="11px"></asp:TextBox>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>--%>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="lbl" runat="server" Text="RequisitionNo :" Font-Names="Verdana" Font-Size="11px"
                                                ForeColor="#006600"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtRequisitionNo" runat="server" Width="120px" Font-Size="11px"></asp:TextBox>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" align="right">
                                            <asp:Button ID="btnView" runat="server" Text="View" Font-Names="Verdana" Font-Size="13px"
                                                BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black"
                                                onmouseover="SetBtnMouseOver(this)" onmouseout="SetBtnMouseOut(this)" OnClick="btnView_Click" />
                                            <asp:Button ID="btnReset" runat="server" Text="Reset" Font-Names="Verdana" Font-Size="13px"
                                                BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black"
                                                onmouseover="SetBtnMouseOver(this)" onmouseout="SetBtnMouseOut(this)" OnClientClick="return resetControls();" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="PanelView" runat="server" BorderColor="Green" BorderStyle="Solid"
                    BorderWidth="1px" Style="display: none">
                    <table width="100%">
                        <tr>
                            <td>
                            </td>
                            <td>
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
                            <td style="width: 100%" colspan="3">
                                <asp:Panel ID="pnl" ScrollBars="Vertical" runat="server" Width="100%" Height="300px">
                                    <asp:GridView ID="dgvRequisition" runat="server" CellPadding="4" ForeColor="#333333"
                                        DataKeyNames="RequisitionCode" GridLines="Both" Font-Names="Verdana" Width="100%"
                                        Font-Size="Small" AllowPaging="true" PageSize="20" AutoGenerateColumns="false"
                                        BorderColor="Green" BorderStyle="Solid" BorderWidth="1px" OnDataBound="dgvRequisition_DataBound"
                                        OnPageIndexChanged="dgvRequisition_PageIndexChanged" OnPageIndexChanging="dgvRequisition_PageIndexChanging"
                                        OnRowDataBound="dgvRequisition_RowDataBound">
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
                                                    <asp:CheckBox ID="chkDelete" runat="server" CausesValidation="false" OnCheckedChanged="chkDelete_CheckedChanged"
                                                        AutoPostBack="true" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Requisition No" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkRequisitionCode" runat="server" CausesValidation="false" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>"
                                                        CommandName="EditRequisition" ForeColor="Blue" Font-Underline="false" Text='<%#Eval("RequisitionCode") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ItemCode" HeaderText="Item Code" ReadOnly="True" SortExpression="ItemCode" />
                                            <asp:BoundField DataField="ItemDesc" HeaderText="Item Description" ReadOnly="True"
                                                SortExpression="ItemDesc" />
                                            <asp:BoundField DataField="Quantity" HeaderText="Quantity" SortExpression="Quantity"
                                                ReadOnly="true" />
                                            <asp:BoundField DataField="Unit" HeaderText="Unit" SortExpression="Unit" ReadOnly="true" />
                                            <asp:BoundField DataField="RequisitionBy" HeaderText="Requisition By" SortExpression="RequisitionBy"
                                                ReadOnly="true" />
                                            <asp:BoundField DataField="GroupDesc" HeaderText="Group Description" ReadOnly="true"
                                                SortExpression="GroupDesc" />
                                            <asp:BoundField DataField="EntryBy" HeaderText="Entry By" ReadOnly="True" SortExpression="EntryBy" />
                                            <asp:BoundField DataField="EntryDate" HeaderText="Entry Date" ReadOnly="True" SortExpression="EntryDate"
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
                <asp:Panel ID="pnlGrid" runat="server" Width="1000px" Style="text-align: center;
                    background-color: #E0F0E8; height: 580px; display: none">
                    <table width="100%" style="border-color: Green; height: 100%; border-style: solid;
                        border-width: 2px" cellpadding="0">
                        <tr>
                            <td>
                                <asp:UpdatePanel runat="server" ID="UdpnlRequisition">
                                    <ContentTemplate>
                                        <asp:Panel ID="Panel1" runat="server">
                                            <table width="100%">
                                                <tr>
                                                    <td colspan="4" align="center">
                                                        <table width="100%">
                                                            <tr>
                                                                <td colspan="3">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4" align="center">
                                                                    <asp:Label ID="Label5" runat="server" Text="Material Requisition Details" Font-Names="Verdana"
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
                                                                    <asp:Label ID="Label1" runat="server" Text="Material Requisition Details :" Font-Names="Verdana"
                                                                        Font-Size="13px" Font-Bold="true" ForeColor="#006600"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="Label9" runat="server" Text="RequisitionNo  :" Font-Names="Verdana"
                                                                        Font-Size="11px" ForeColor="#006600"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtRequisitionCode" runat="server" ReadOnly="true" Font-Names="Verdana"
                                                                        Font-Size="11px"></asp:TextBox>
                                                                </td>
                                                                <td align="right">
                                                                    <asp:Label ID="Label11" runat="server" Text="Group Type :" Font-Names="Verdana" Font-Size="11px"
                                                                        ForeColor="#006600"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:DropDownList ID="ddlGroup" runat="server" Width="160px" AutoPostBack="true"
                                                                        OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged" Font-Names="Verdana" Font-Size="11px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4" style="width: 100%; height: 270px;">
                                                            <asp:Panel ID="pnlGridForItem" ScrollBars="Vertical" runat="server" Width="99.6%"
                                                            Height="250px" BorderColor="Green" BorderStyle="Solid" BorderWidth="1px" Style="display: none;">
                                                            <asp:GridView ID="dgvSaveRequisition" runat="server" CellPadding="4" ForeColor="#333333"
                                                                DataKeyNames="ItemCode" GridLines="Both" Font-Names="Verdana" Width="100%" Font-Size="Small"
                                                                AllowPaging="true" PageSize="20" AutoGenerateColumns="false" BorderColor="Green"
                                                                BorderStyle="Solid" BorderWidth="1px" >
                                                                <FooterStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" />
                                                                <RowStyle BackColor="#E0F0E8" Font-Size="11px" Wrap="False" />
                                                                <PagerStyle BackColor="#3b3535" ForeColor="White" HorizontalAlign="Left" Font-Size="11px" />
                                                                <SelectedRowStyle BackColor="#006600" Font-Bold="True" ForeColor="#333333" />
                                                                <HeaderStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" Font-Size="12px"
                                                                    Wrap="False" />
                                                                <EditRowStyle BackColor="#2461BF" />
                                                                <AlternatingRowStyle BackColor="White" Wrap="False" />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="ItemCode" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblItemCode" runat="server" CausesValidation="false" Text='<%#Eval("ItemCode") %>'
                                                                                CommandArgument="<%# ((GridViewRow)Container).RowIndex %>"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="ItemDesc" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblItemDesc" runat="server" CausesValidation="false" Text='<%#Eval("ItemDesc") %>'
                                                                                CommandArgument="<%# ((GridViewRow)Container).RowIndex %>"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Quantity" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="130px">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtQty" runat="server" CausesValidation="false" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>"
                                                                                Text='<%#Eval("Quantity") %>' onkeypress="return isNumber();" Width="100px"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Unit" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="130px">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblUnit" runat="server" CausesValidation="false" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>"
                                                                                Text='<%#Eval("UnitCode") %>' onkeypress="return isNumber();" Width="100px"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Select" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chkSelect" runat="server" CausesValidation="false" OnCheckedChanged="chkSelect_CheckedChanged"
                                                                                AutoPostBack="true" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
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
                                                        <td align="center" colspan="4">
                                                            <asp:Button ID="btnSave" runat="server" BackColor="#3b3535" Font-Names="Verdana"
                                                                Font-Size="13px" ForeColor="White" OnClick="btnSave_Click" onmouseout="SetBtnMouseOut(this)"
                                                                onmouseover="SetBtnMouseOver(this)" Style="border: 1px solid black" Text="Save"
                                                                Width="80px" />
                                                            <asp:Button ID="btnClose" runat="server" BackColor="#3b3535" Font-Names="Verdana"
                                                                Font-Size="13px" ForeColor="White" OnClientClick="return hideModalPopup();" onmouseout="SetBtnMouseOut(this)"
                                                                onmouseover="SetBtnMouseOver(this)" Style="border: 1px solid black" Text="Close"
                                                                Width="80px" />
                                                        </td>
                                                    </tr>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
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
