﻿<%@ Page Title="" Language="C#" MasterPageFile="~/mstAdmin.Master" AutoEventWireup="true" CodeBehind="frmDebitNote.aspx.cs" Inherits="Hospital.Store.frmDebitNote" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .unwatermarked
        {
            height: 18px;
            width: 148px;
        }
        .watermarked
        {
            height: 20px;
            width: 150px;
            padding: 2px 0 0 2px;
            border: 1px solid #BEBEBE;
            background-color: #F0F8FF;
            color: gray;
        }
    </style>
    <script type="text/javascript">
        function CheckList() {
            var dis = document.getElementById("<%=txtDiscount.ClientID %>").value;
            var vat = document.getElementById("<%=txtVAT.ClientID %>").value;
            var Ser = document.getElementById("<%=txtService.ClientID %>").value;
            var total = document.getElementById("<%=txtTotal.ClientID %>").value;

            if (dis == null && vat == null && Ser == null && total == null) {
                document.getElementById("<%=txtNetAmount.ClientID %>").value = null;
            }
            else if (dis == null && vat == null && Ser == null && total != null) {
                document.getElementById("<%=txtNetAmount.ClientID %>").value = total;
            }
            else if (dis != null && vat == null && Ser == null && total != null) {
                var disamt = total * dis / 100;
                document.getElementById("<%=txtNetAmount.ClientID %>").value = total - disamt;
            }
            else if (dis == null && vat != null && Ser == null && total != null) {
                var VatAmt = total * vat / 100;
                document.getElementById("<%=txtNetAmount.ClientID %>").value = total + VatAmt;
            }
            else if (dis == null && vat == null && Ser != null && total != null) {
                var SerAmt = total * Ser / 100;
                document.getElementById("<%=txtNetAmount.ClientID %>").value = total + SerAmt;
            }
            else if (dis != null && vat != null && Ser == null && total != null) {
                var disamt = total * dis / 100;
                var TotalDis = total - disamt;
                var VatAmt = TotalDis * vat / 100;
                document.getElementById("<%=txtNetAmount.ClientID %>").value = TotalDis + VatAmt;
            }
            else if (dis != null && vat == null && Ser != null && total != null) {
                var disamt = total * dis / 100;
                var TotalDis = total - disamt;
                var SerAmt = TotalDis * Ser / 100;
                document.getElementById("<%=txtNetAmount.ClientID %>").value = TotalDis + SerAmt;
            }
            else if (dis == null && vat != null && Ser != null && total != null) {
                var VatAmt = total * vat / 100;
                var TotalVat = total + VatAmt;
                var SerAmt = TotalVat * Ser / 100;
                document.getElementById("<%=txtNetAmount.ClientID %>").value = TotalVat + SerAmt;
            }
            else {
                var disamt = total * dis / 100;
                var TotalDis = total - disamt;
                var VatAmt = TotalDis * vat / 100;
                var TotalVat = TotalDis + VatAmt;
                var SerAmt = TotalVat * Ser / 100;
                document.getElementById("<%=txtNetAmount.ClientID %>").value = TotalVat + SerAmt;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="width: 99%">
                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="View1" runat="server">
                        <table width="1040px">
                            <tr>
                                <td align="center">
                                    <asp:Label ID="Label1" runat="server" Text="Debit Note" Font-Names="Verdana" Font-Size="16px"
                                        Font-Bold="True" ForeColor="#3B3535"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="width: 15%;">
                                    <table border="1px" cellpadding="0" cellspacing="0" width="820px" style="height: 40px;">
                                        <tr>
                                            <td align="center" style="border-right: none; width: 575px;">
                                                <asp:TextBox ID="txtSearch" runat="server" Width="550px"></asp:TextBox>
                                                <cc:TextBoxWatermarkExtender ID="txtSearchWatermark" runat="server" TargetControlID="txtSearch"
                                                    WatermarkText="Supplier Name, Address" WatermarkCssClass="watermarked" />
                                            </td>
                                            <td align="center" style="border-left: none; border-right: none; width: 60px;">
                                                <asp:Button ID="btnSearch" runat="server" Text="Search" Font-Names="Verdana" Font-Size="13px"
                                                    BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black"
                                                    OnClick="btnSearch_Click" />
                                            </td>
                                            <td align="center" style="border-left: none; border-right: none; width: 90px;">
                                                <asp:Button ID="btnReset" runat="server" BackColor="#3b3535" Font-Names="Verdana"
                                                    Font-Size="13px" ForeColor="White" Style="border: 1px solid black" Text="Reset"
                                                    Width="80px" OnClick="btnReset_Click" />
                                            </td>
                                            <td style="border-left: none; width: 60px; padding-right: 4px;">
                                                <asp:Button ID="BtnAddNewDept" runat="server" Text="Add New" Font-Names="Verdana"
                                                    Font-Size="13px" OnClick="BtnAddNewDept_Click" BackColor="#3b3535" ForeColor="White"
                                                    Width="80px" Style="border: 1px solid black" />
                                            </td>
                                        </tr>
                                    </table>
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
                                                <td colspan="2">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100%" colspan="3">
                                                    <asp:Label ID="lblMessage" runat="server" Text="" Font-Names="Verdana" Font-Size="11px"
                                                        ForeColor="Red"></asp:Label>
                                                    <asp:Panel ID="pnl" ScrollBars="Horizontal" runat="server" Width="1010px" BorderColor="Green"
                                                        BorderStyle="Solid" BorderWidth="1px">
                                                        <asp:GridView ID="dgvTestParameter" runat="server" CellPadding="4" ForeColor="#333333"
                                                            DataKeyNames="DNNo" GridLines="Both" Font-Names="Verdana" Width="100%" Font-Size="Small"
                                                            AllowPaging="true" PageSize="20" AutoGenerateColumns="false" OnPageIndexChanged="dgvTestParameter_PageIndexChanged"
                                                            OnPageIndexChanging="dgvTestParameter_PageIndexChanging">
                                                            <FooterStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" />
                                                            <RowStyle BackColor="#E0F0E8" Font-Size="11px" Wrap="False" />
                                                            <PagerStyle BackColor="#3b3535" ForeColor="White" HorizontalAlign="Left" Font-Size="11px" />
                                                            <SelectedRowStyle BackColor="#006600" Font-Bold="True" ForeColor="#333333" />
                                                            <HeaderStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" Font-Size="12px"
                                                                Wrap="False" />
                                                            <EditRowStyle BackColor="#2461BF" />
                                                            <AlternatingRowStyle BackColor="White" Wrap="False" />
                                                            <Columns>
                                                                <asp:BoundField DataField="SupplierName" HeaderText="Supplier Name" ReadOnly="True"
                                                                    SortExpression="SupplierName" />
                                                                <asp:BoundField DataField="Address" HeaderText="Address" ReadOnly="True" SortExpression="Address" />
                                                                <asp:BoundField DataField="DNDate" HeaderText="DN Date" ReadOnly="True" SortExpression="DNDate"
                                                                    DataFormatString="{0:dd/MM/yyyy}" />
                                                                <asp:BoundField DataField="NetAmount" HeaderText="NetAmount" ReadOnly="True" SortExpression="NetAmount" />
                                                                <asp:BoundField DataField="Amount" HeaderText="Amount" ReadOnly="True" SortExpression="Amount" />
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
                        <table width="1045px" style="border-color: Green; border-style: solid; border-width: 2px"
                            cellpadding="0">
                            <tr style="width: 100%">
                                <td style="width: 100%">
                                    <asp:Panel ID="Panel2" runat="server" Width="100%" Style="text-align: center; background-color: #E0F0E8;
                                        height: 100%;" BorderColor="Green" BorderStyle="Solid" BorderWidth="1px">
                                        <table style="padding-left: 45%;">
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="lblHeading" runat="server" Text="Debit Note" Font-Names="Verdana"
                                                        Font-Size="15px" Font-Bold="true" ForeColor="#3b3535"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                        <div style="float: right; position: relative; width: 80%; right: 9%; top: 2px; left: -89px;"
                                            align="center">
                                            <table>
                                                <tr>
                                                    <td colspan="4">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4">
                                                        <asp:Label ID="lblMsg" ForeColor="Red" Font-Names="Verdana" Font-Size="11px" Text=""
                                                            runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <asp:Label ID="lblSupplierName" Text="Supplier Name :" runat="server" ForeColor="#3B3535"
                                                            Font-Names="verdana" Font-Size="11px" />
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlSupplier" runat="server" Width="150px">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ForeColor="Red"
                                                            ControlToValidate="ddlSupplier" Font-Size="13" ValidationGroup="Save" ErrorMessage="*"
                                                            InitialValue="0" Display="Dynamic"></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Label ID="lblDate" Text="Debit Note Date :" runat="server" ForeColor="#3B3535"
                                                            Font-Names="verdana" Font-Size="11px" />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtDebitDate" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                            Width="150px" MaxLength="10" Format="dd/MM/yyyy"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ForeColor="Red"
                                                            ControlToValidate="txtDebitDate" Font-Size="13" ValidationGroup="Save" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td align="left" colspan="2">
                                                        
                                                        <cc:CalendarExtender ID="CalPurchaseDate" runat="server" TargetControlID="txtDebitDate"
                                                            Format="dd/MM/yyyy" DaysModeTitleFormat="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy">
                                                        </cc:CalendarExtender>
                                                    </td>
                                                </tr>
                                                <tr style="height: 5px">
                                                    <td colspan="4">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <asp:Label ID="lblOther" Text="Item Name :" runat="server" ForeColor="#3B3535" Font-Names="verdana"
                                                            Font-Size="11px" />
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlProduct" runat="server" Width="150px" AutoPostBack="True"
                                                            OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ForeColor="Red"
                                                            ControlToValidate="ddlProduct" Font-Size="13" ValidationGroup="Add" ErrorMessage="*"
                                                            Display="Dynamic" InitialValue="0">
                                                        </asp:RequiredFieldValidator>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Label ID="Label6" runat="server" Font-Names="verdana" Font-Size="11px" ForeColor="#3B3535"
                                                            Text="Batch No :" />
                                                    </td>
                                                    <td align="left">
                                                        <asp:DropDownList ID="ddlBatchNo" runat="server" Width="150px" AutoPostBack="True"
                                                            OnSelectedIndexChanged="ddlBatchNo_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ForeColor="Red"
                                                            ControlToValidate="ddlBatchNo" Font-Size="13" ValidationGroup="Add" ErrorMessage="*"
                                                            Display="Dynamic" InitialValue="0">
                                                        </asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <asp:Label ID="Label7" Text="Expiry Date :" runat="server" ForeColor="#3B3535" Font-Names="verdana"
                                                            Font-Size="11px" />
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlExpiryDate" runat="server" Width="150px" DataTextFormatString="{0:dd/MM/yyyy}">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ForeColor="Red"
                                                            ControlToValidate="ddlExpiryDate" Font-Size="13" ValidationGroup="Add" ErrorMessage="*"
                                                            Display="Dynamic" InitialValue="0">
                                                        </asp:RequiredFieldValidator>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Label ID="lblCharge" Text="Item Charges :" runat="server" ForeColor="#3B3535"
                                                            Font-Names="verdana" Font-Size="11px" />
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtItemCharge" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                            Width="150px" MaxLength="10"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red"
                                                            ControlToValidate="txtItemCharge" Font-Size="13" ValidationGroup="Add" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">                                                        
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td align="right" colspan="2">
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtItemCharge"
                                                            Display="Dynamic" ErrorMessage="Please Enter Only Number" Font-Bold="False" Font-Names="verdana"
                                                            Font-Size="11px" ForeColor="Red" ValidationExpression="^[0-9]{1,11}(?:\.[0-9]{1,3})?$"
                                                            ValidationGroup="Add" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <asp:Label ID="lblQuantiy" Text="Debit Note Quantity :" runat="server" ForeColor="#3B3535"
                                                            Font-Names="verdana" Font-Size="11px" />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtQuantity" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                            Width="150px" MaxLength="10"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ForeColor="Red"
                                                            ControlToValidate="txtQuantity" Font-Size="13" ValidationGroup="Add" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label2" runat="server" Font-Names="verdana" Font-Size="11px" 
                                                            ForeColor="#3B3535" Text="Balance Quantity :" />
                                                    </td>
                                                    <td align="left" style="padding-left:25px;">
                                                        <asp:Label ID="lblBalQty" runat="server" Font-Names="verdana" Font-Size="11px" 
                                                            ForeColor="#3B3535" />
                                                        
                                                    </td>
                                                </tr>
                                                <tr style="height: 10px;">
                                                    <td>
                                                    </td>
                                                    <td style="border-top: none; border-bottom: none;">
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationExpression="^[0-9]{1,11}(?:\.[0-9]{1,3})?$"
                                                            ErrorMessage="Please Enter Only Number" Font-Bold="False" Font-Size="11px" ForeColor="Red"
                                                            ControlToValidate="txtQuantity" runat="server" ValidationGroup="Add" Display="Dynamic"
                                                            Font-Names="verdana" />
                                                    </td>
                                                </tr>
                                                <tr style="height: 10px;">
                                                    <td colspan="2" align="right">
                                                        <asp:Button ID="btnAdd" runat="server" Text="Add" Font-Names="Verdana" Font-Size="12px"
                                                            BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black"
                                                            OnClick="btnAddCharge_Click" ValidationGroup="Add" />
                                                        <asp:Button ID="btnUpdateItem" runat="server" Text="Update" Font-Names="Verdana"
                                                            Font-Size="12px" BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black"
                                                            OnClick="btnUpdateItem_Click" ValidationGroup="Add" />
                                                    </td>
                                                    <td align="left" colspan="2">
                                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" Font-Names="Verdana" Font-Size="12px"
                                                            BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black"
                                                            OnClick="btnCancel_Click" />
                                                    </td>
                                                </tr>
                                                <tr style="height: 10px;">
                                                    <td colspan="4" style="border-top: none;">
                                                    </td>
                                                </tr>
                                            </table>
                                            <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" DataKeyNames="TempId"
                                                GridLines="Both" Font-Names="Verdana" Width="80%" Font-Size="Small" AllowPaging="true"
                                                PageSize="5" AutoGenerateColumns="false">
                                                <FooterStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" />
                                                <RowStyle BackColor="#E0F0E8" Font-Size="11px" Wrap="False" />
                                                <PagerStyle BackColor="#3b3535" ForeColor="White" HorizontalAlign="Center" Font-Size="11px" />
                                                <SelectedRowStyle BackColor="#006600" Font-Bold="True" ForeColor="#333333" />
                                                <HeaderStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" Font-Size="12px"
                                                    Wrap="False" />
                                                <EditRowStyle BackColor="#2461BF" />
                                                <AlternatingRowStyle BackColor="White" Wrap="False" />
                                                <Columns>
                                                    <asp:BoundField DataField="ProductCode" HeaderText="Product Id" ReadOnly="True" SortExpression="ProductCode" />
                                                    <asp:BoundField DataField="ProductName" HeaderText="Product Name" ReadOnly="True"
                                                        SortExpression="ProductName" />
                                                    <asp:BoundField DataField="Quantity" HeaderText="Quantity" ReadOnly="True" SortExpression="Quantity" />
                                                    <asp:BoundField DataField="BatchNo" HeaderText="Batch No" ReadOnly="True" SortExpression="BatchNo" />
                                                    <asp:BoundField DataField="ExpiryDate" HeaderText="Expiry Date" ReadOnly="True" SortExpression="ExpiryDate"
                                                        DataFormatString="{0:dd/MM/yyyy}" />
                                                    <asp:BoundField DataField="Price" HeaderText="Charge" ReadOnly="True" SortExpression="Price" />
                                                    <asp:BoundField DataField="Amount" HeaderText="Amount" ReadOnly="True" SortExpression="Amount" />
                                                    <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImageEdit" runat="server" ImageUrl="~/images/edit.jpg" Height="24px"
                                                                Width="24px" OnClick="btnEditItem_Click" /></ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImageDelete" runat="server" ImageUrl="~/images/Erase.png" Height="24px"
                                                                Width="24px" OnClick="btnDelete_Click" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                        <table align="center" border="1px" cellpadding="0" cellspacing="0" width="90%">
                            <tr style="height: 8px;">
                                <td colspan="4" style="border-bottom: none;">
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="border-top: none; border-right: none; border-bottom: none;">
                                    <asp:Label ID="lblDiscount" Text="Discount (%) :" runat="server" ForeColor="#3b3535"></asp:Label>
                                </td>
                                <td align="left" style="border-top: none; border-left: none; border-bottom: none;
                                    border-right: none;">
                                    <asp:TextBox ID="txtDiscount" runat="server" Font-Names="Verdana" Font-Size="11px"
                                        Width="150px" MaxLength="10" onblur="javascript:return CheckList();"></asp:TextBox>
                                </td>
                                <td align="right" style="border-top: none; border-right: none; border-left: none;
                                    border-bottom: none;">
                                    <asp:Label ID="lblTotal" Text="Total :" runat="server" ForeColor="#3b3535"></asp:Label>
                                </td>
                                <td align="left" style="border-top: none; border-left: none; border-bottom: none;">
                                    <asp:TextBox ID="txtTotal" runat="server" Font-Names="Verdana" Font-Size="11px" Width="150px"
                                        MaxLength="10" ReadOnly="true"></asp:TextBox>
                            </tr>
                            <tr>
                                <td colspan="2" style="border-top: none; border-bottom: none; border-right: none;"
                                    align="center">
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationExpression="^[0-9]{1,11}(?:\.[0-9]{1,3})?$"
                                        ErrorMessage="Please Enter Only Number" Font-Size="11px" ForeColor="Red" ControlToValidate="txtDiscount"
                                        runat="server" ValidationGroup="Save" Display="Dynamic" Font-Names="verdana" />
                                </td>
                                <td style="border-top: none; border-bottom: none; border-right: none; border-left: none;">
                                </td>
                                <td style="border-top: none; border-bottom: none; border-left: none;">
                                </td>
                            </tr>
                            <tr style="height: 5px;">
                                <td colspan="4" style="border-top: none; border-bottom: none;">
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="border-right: none; border-top: none; border-bottom: none;">
                                    <asp:Label ID="lblVat" Text="VAT (%) :" runat="server" ForeColor="#3b3535"></asp:Label>
                                </td>
                                <td align="left" style="border-right: none; border-top: none; border-bottom: none;
                                    border-left: none;">
                                    <asp:TextBox ID="txtVAT" runat="server" Font-Names="Verdana" Font-Size="11px" Width="150px"
                                        MaxLength="10" onblur="javascript:return CheckList();"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvVat" runat="server" ForeColor="Red" ControlToValidate="txtVAT"
                                        Font-Size="13" ValidationGroup="Save" ErrorMessage="*"></asp:RequiredFieldValidator>
                                </td>
                                <td align="right" style="border-top: none; border-bottom: none; border-right: none;
                                    border-left: none;">
                                    <asp:Label ID="lblService" Text="Service Tax (%) :" runat="server" ForeColor="#3b3535"></asp:Label>
                                </td>
                                <td align="left" style="border-top: none; border-bottom: none; border-left: none;">
                                    <asp:TextBox ID="txtService" runat="server" Font-Names="Verdana" Font-Size="11px"
                                        Width="150px" MaxLength="10" onblur="javascript:return CheckList();"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="border-top: none; border-bottom: none; border-right: none;"
                                    align="center">
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ValidationExpression="^[0-9]{1,11}(?:\.[0-9]{1,3})?$"
                                        ErrorMessage="Please Enter Only Number" Font-Size="11px" ForeColor="Red" ControlToValidate="txtVAT"
                                        runat="server" ValidationGroup="Save" Display="Dynamic" Font-Names="verdana" />
                                </td>
                                <td colspan="2" style="border-top: none; border-bottom: none; border-left: none;"
                                    align="center">
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" ValidationExpression="^[0-9]{1,11}(?:\.[0-9]{1,3})?$"
                                        ErrorMessage="Please Enter Only Number" Font-Size="11px" ForeColor="Red" ControlToValidate="txtService"
                                        runat="server" ValidationGroup="Save" Display="Dynamic" Font-Names="verdana" />
                                </td>
                            </tr>
                            <tr style="height: 8px;">
                                <td colspan="4" style="border-top: none; border-bottom: none;">
                                </td>
                            </tr>
                            <tr>
                                <td style="border-top: none; border-bottom: none; border-right: none;">
                                </td>
                                <td align="right" style="border-top: none; border-bottom: none; border-right: none;
                                    border-left: none;">
                                    <asp:Label ID="lblNetAmount" Text="Net Amount :" runat="server" ForeColor="#3b3535"></asp:Label>
                                </td>
                                <td align="left" colspan="2" style="border-top: none; border-bottom: none; border-left: none;">
                                    <asp:TextBox ID="txtNetAmount" runat="server" Font-Names="Verdana" Font-Size="11px"
                                        Width="150px" MaxLength="10"></asp:TextBox>
                                </td>
                            </tr>
                            <tr style="height: 8px;">
                                <td colspan="4" style="border-top: none;">
                                </td>
                            </tr>
                        </table>
                        </td> </tr> </table>
                        <table width="100%">
                            <tr>
                                <td colspan="2">
                                </td>
                            </tr>
                            <tr align="center">
                                <td align="center" colspan="2">
                                    <asp:Button ID="BtnSave" runat="server" Text="Save" Font-Names="Verdana" Font-Size="12px"
                                        ValidationGroup="Save" BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black"
                                        OnClick="BtnSave_Click" />
                                    <asp:Button ID="btnUpdate" runat="server" Text="Update" Font-Names="Verdana" Font-Size="12px"
                                        BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black"
                                        OnClick="BtnEdit_Click" />
                                    <asp:Button ID="BtnClose" runat="server" Text="Close" Font-Names="Verdana" Font-Size="12px"
                                        BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black"
                                        OnClick="BtnClose_Click" />
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                </asp:MultiView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

