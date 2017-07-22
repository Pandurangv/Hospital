<%@ Page Title="" Language="C#" MasterPageFile="~/mstAdmin.Master" AutoEventWireup="true" CodeBehind="frmICUInvoiceDischargeBilling.aspx.cs" Inherits="Hospital.Billing.frmICUInvoiceDischargeBilling" %>
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

        function CalculateCharges() {
            var txtChargePerDay = document.getElementById("<%=txtCharges.ClientID %>").value;
            var txtNoofDays = document.getElementById("<%=txtNoofDays.ClientID %>").value;
            if (txtChargePerDay != null && txtChargePerDay != '' && txtNoofDays != null && txtNoofDays != '') {
                document.getElementById("<%=txtAmount.ClientID %>").value = txtChargePerDay * txtNoofDays;
            }
        }
        function CalculateChargesByQty() {
            var txtChargePerDay = document.getElementById("<%=txtCharges.ClientID %>").value;
            var txtQuantity = document.getElementById("<%=txtQuantity.ClientID %>").value;
            if (txtChargePerDay != null && txtChargePerDay != '' && txtQuantity != null && txtQuantity != '') {
                document.getElementById("<%=txtNoofDays.ClientID %>").value = '';
                document.getElementById("<%=txtAmount.ClientID %>").value = txtChargePerDay * txtQuantity;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="width: 100%">
                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="View1" runat="server">
                        <table width="1050px" style="border-color: #595353">
                            <tr>
                                <td align="center">
                                    <asp:Label ID="Label1" runat="server" Text="ICU Invoice" Font-Names="Verdana" Font-Size="16px"
                                        Font-Bold="true" ForeColor="#3b3535"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="width: 15%;">
                                    <table border="1px" cellpadding="0" cellspacing="0" width="860px" style="height: 40px;">
                                        <tr>
                                            <td align="center" style="border-right: none; width: 575px;">
                                                <asp:TextBox ID="txtSearch" runat="server" Width="550px"></asp:TextBox>
                                                <cc:TextBoxWatermarkExtender ID="txtSearchWatermark" runat="server" TargetControlID="txtSearch"
                                                    WatermarkText="Patient Name, Address" WatermarkCssClass="watermarked" />
                                            </td>
                                            <td align="center" style="border-left: none; border-right: none; width: 60px;">
                                                <asp:Button ID="btnSearch" runat="server" Text="Search" Font-Names="Verdana" Font-Size="13px"
                                                    BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black"
                                                    OnClick="btnSearch_Click" />
                                            </td>
                                            <td align="center" style="border-left: none; border-right: none; width: 90px;">
                                                <asp:Button ID="btnReset" runat="server" BackColor="#3b3535" Font-Names="Verdana"
                                                    Font-Size="13px" ForeColor="White" OnClick="btnReset_Click" Style="border: 1px solid black"
                                                    Text="Reset" Width="80px" />
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
                                                            DataKeyNames="ICUInvoiceNo" GridLines="Both" Font-Names="Verdana" Width="100%"
                                                            Font-Size="Small" AllowPaging="true" PageSize="20" AutoGenerateColumns="false"
                                                            OnDataBound="dgvTestParameter_DataBound" OnPageIndexChanged="dgvTestParameter_PageIndexChanged"
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
                                                                <asp:BoundField DataField="PatientAdmitId" HeaderText="Patient Id" ReadOnly="True"
                                                                    SortExpression="PatientAdmitId" />
                                                                <asp:BoundField DataField="FullName" HeaderText="Bill Date" ReadOnly="True" SortExpression="FullName" />
                                                                <asp:BoundField DataField="ICUInvoiceDate" HeaderText="Bill Date" ReadOnly="True" DataFormatString="{0:dd/MM/yyyy}"/>
                                                                <asp:BoundField DataField="NetAmount" HeaderText="Net Amount" ReadOnly="True" SortExpression="NetAmount" />
                                                                <asp:BoundField DataField="Discount" HeaderText="Discount" ReadOnly="True" SortExpression="Discount" />
                                                                <asp:BoundField DataField="Vat" HeaderText="Vat" ReadOnly="True" SortExpression="Vat" />
                                                                <asp:BoundField DataField="ServiceTax" HeaderText="ServiceTax" ReadOnly="True" SortExpression="ServiceTax" />
                                                                <asp:BoundField DataField="TotalAmount" HeaderText="Total Amount" ReadOnly="True"
                                                                    SortExpression="TotalAmount" />
                                                                <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="ImageEdit" runat="server" ImageUrl="~/images/edit.jpg" Height="24px"
                                                                            Width="24px" OnClick="btnUpdate_Click" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Print" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="ImagePrint" runat="server" ImageUrl="~/images/Report.bmp" Height="24px"
                                                                            Width="24px" OnClick="btnPrint_Click" /></ItemTemplate>
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
                        <table width="1050px" style="border-color: #595353; border-style: solid; border-width: 2px"
                            cellpadding="0">
                            <tr style="width: 100%">
                                <td style="width: 100%">
                                    <asp:Panel ID="Panel2" runat="server" Width="98%" align="center" Style="text-align: center;
                                        background-color: #E0F0E8; height: 100%;" BorderColor="#3b3535" BorderStyle="Solid"
                                        BorderWidth="1px">
                                        <center>
                                            <table>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label ID="lblHeading" runat="server" Text="ICU Invoice" Font-Names="Verdana"
                                                            Font-Size="15px" Font-Bold="true" ForeColor="#3b3535"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label ID="lblMsg" ForeColor="Red" runat="server" Font-Names="verdana" Font-Size="11px" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </center>
                                        <div style="float: right; position: relative; width: 80%; right: 9%; top: 2px; left: -89px;"
                                            align="center">
                                            <table style="margin-right: 2px;">
                                                <tr>
                                                    <td align="right">
                                                        <asp:Label ID="lblPatientName" Text="Patient Name :" runat="server" ForeColor="#3b3535" />
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlPatient" runat="server" Width="150px" AutoPostBack="True">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ForeColor="Red"
                                                            ControlToValidate="ddlPatient" Font-Size="13" ValidationGroup="Save" ErrorMessage="*"
                                                            InitialValue="0" Display="Dynamic"></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Label ID="lblDate" Text="Billing Date :" runat="server" ForeColor="#3b3535" />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtBillDate" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                            Width="150px" MaxLength="10"  OnTextChanged="txtBillDate_TextChanged"
                                                            AutoPostBack="true"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ForeColor="Red"
                                                            ControlToValidate="txtBillDate" Font-Size="13" ValidationGroup="Save" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <asp:Label ID="Label4" Text="Cash :" runat="server" ForeColor="#3b3535" />
                                                    </td>
                                                    <td align="left">
                                                        <asp:CheckBox ID="chkIsCash" runat="server" Checked="false" />
                                                    </td>
                                                    <td align="right">
                                                        <asp:Label ID="lblDate0" runat="server" ForeColor="#3B3535" Text="Bed No. :" />
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlBedMaster" runat="server" Width="150px">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlBedMaster"
                                                            ErrorMessage="*" Font-Size="13" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <asp:Label Text="Shift to Ward" ID="lblDischarge" ForeColor="#3B3535" runat="server" />
                                                    </td>
                                                    <td align="left">
                                                        <asp:CheckBox ID="chkDischarge" Checked="false" runat="server" />
                                                    </td>
                                                    <td align="left" colspan="2">
                                                        
                                                        <cc:CalendarExtender ID="CalBillDate" runat="server" TargetControlID="txtBillDate"
                                                            Format="dd/MM/yyyy" DaysModeTitleFormat="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy">
                                                        </cc:CalendarExtender>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div style="float: right; position: relative; width: 80%; margin-right: 9%; top: 0px;
                                            left: 0px;" align="center">
                                            <table border="1px" cellpadding="0" cellspacing="0" width="80%">
                                                <tr style="height: 10px;">
                                                    <td colspan="4" style="border-bottom: none;">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" style="width: 15%; border-right: none; border-bottom: none; border-top: none;">
                                                        <asp:Label ID="lblOther" Text="Other Charges :" runat="server" ForeColor="#3b3535" />
                                                    </td>
                                                    <td style="padding-left: 5px; width: 8%; border-right: none; border-left: none; border-bottom: none;
                                                        border-top: none;" align="left">
                                                        <asp:DropDownList ID="ddlOther" Width="150px" runat="server" AutoPostBack="True"
                                                            OnSelectedIndexChanged="ddlOther_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td align="right" style="width: 15%; border-right: none; border-bottom: none; border-left: none;
                                                        border-top: none;">
                                                        <asp:Label ID="Label3" Text="Charges :" runat="server" ForeColor="#3b3535" />
                                                    </td>
                                                    <td style="padding-left: 5px; width: 8%; border-left: none; border-bottom: none;
                                                        border-top: none;" align="left">
                                                        <asp:TextBox ID="txtCharges" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                            Width="150px" MaxLength="10" onblur="javascript:return CalculateCharges();"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr height="10px">
                                                    <td colspan="4" style="border-bottom: none; border-top: none;">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" style="width: 8%; border-right: none; border-bottom: none; border-top: none;">
                                                        <asp:Label ID="Label5" Text="No of Days :" runat="server" ForeColor="#3b3535" />
                                                    </td>
                                                    <td align="left" style="padding-left: 5px; width: 8%; border-right: none; border-left: none;
                                                        border-bottom: none; border-top: none;">
                                                        <asp:TextBox ID="txtNoofDays" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                            Width="150px" MaxLength="10" onblur="javascript:return CalculateCharges();"></asp:TextBox>
                                                    </td>
                                                    <td align="right" style="width: 15%; border-right: none; border-left: none; border-bottom: none;
                                                        border-top: none;">
                                                        <asp:Label ID="Label7" Text="Quantity :" runat="server" ForeColor="#3b3535" />
                                                    </td>
                                                    <td style="padding-left: 5px; width: 8%; border-left: none; border-bottom: none;
                                                        border-top: none;" align="left">
                                                        <asp:TextBox ID="txtQuantity" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                            Width="150px" MaxLength="10" onblur="javascript:return CalculateChargesByQty();"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr height="10px">
                                                    <td colspan="4" style="border-bottom: none; border-top: none;">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" style="width: 8%; border-right: none; border-bottom: none; border-top: none;">
                                                        <asp:Label ID="Label2" Text="Amount :" runat="server" ForeColor="#3b3535" />
                                                    </td>
                                                    <td align="left" style="padding-left: 5px; width: 8%; border-right: none; border-left: none;
                                                        border-bottom: none; border-top: none;" colspan="2">
                                                        <asp:TextBox ID="txtAmount" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                            Width="150px" MaxLength="10"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red"
                                                            ControlToValidate="txtAmount" Font-Size="13" ValidationGroup="Save" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td style="border-left: none; border-top: none; border-bottom: none;">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" style="width: 8%; border-right: none; border-bottom: none; border-top: none;"
                                                        colspan="2">
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationExpression="^[0-9]{1,11}(?:\.[0-9]{1,3})?$"
                                                            ErrorMessage="Please Enter Only Number" Font-Bold="False" Font-Size="11px" ForeColor="Red"
                                                            ControlToValidate="txtAmount" runat="server" ValidationGroup="Save" Display="Dynamic"
                                                            Font-Names="verdana" />
                                                    </td>
                                                    <td colspan="2" style="border-left: none; border-top: none; border-bottom: none;">
                                                    </td>
                                                </tr>
                                                <tr style="height: 10px;">
                                                    <td colspan="4" style="border-top: none; border-bottom: none;">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" align="right" style="border-top: none; border-bottom: none; padding-right: 2px;">
                                                        <asp:Button ID="btnAdd" runat="server" Text="Add" Font-Names="Verdana" Font-Size="12px"
                                                            ValidationGroup="Save" BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black"
                                                            OnClick="btnAddCharge_Click" />
                                                        <asp:Button ID="btnUpdateCharge" runat="server" Text="Update" Font-Names="Verdana"
                                                            Font-Size="12px" ValidationGroup="Add" BackColor="#3b3535" ForeColor="White"
                                                            Width="80px" Style="border: 1px solid black" OnClick="btnUpdateCharge_Click" />
                                                    </td>
                                                    <td align="left" style="border-top: none; border-bottom: none; border-right: none;
                                                        padding-left: 2px;">
                                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" Font-Names="Verdana" Font-Size="12px"
                                                            BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black"
                                                            OnClick="btnCancel_Click" />
                                                    </td>
                                                    <td style="border-top: none; border-bottom: none; border-left: none;">
                                                    </td>
                                                </tr>
                                                <tr style="height: 10px;">
                                                    <td colspan="4" style="border-top: none;">
                                                    </td>
                                                </tr>
                                            </table>
                                            <asp:Panel ID="Panel1" ScrollBars="Both" BorderColor="Maroon" Width="80%" BorderWidth="1px"
                                                Height="200px" runat="server" Style="border-color: Green; border-style: solid;
                                                border-width: 1px">
                                                <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" DataKeyNames="TempId"
                                                    GridLines="Both" Font-Names="Verdana" Width="100%" Font-Size="Small" AutoGenerateColumns="false"
                                                    OnRowDataBound="GridView1_RowDataBound">
                                                    <FooterStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" />
                                                    <RowStyle BackColor="#E0F0E8" Font-Size="11px" Wrap="False" />
                                                    <PagerStyle BackColor="#3b3535" ForeColor="White" HorizontalAlign="Left" Font-Size="11px" />
                                                    <SelectedRowStyle BackColor="#006600" Font-Bold="True" ForeColor="#333333" />
                                                    <HeaderStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" Font-Size="12px"
                                                        Wrap="False" />
                                                    <EditRowStyle BackColor="#2461BF" />
                                                    <AlternatingRowStyle BackColor="White" Wrap="False" />
                                                    <Columns>
                                                        <asp:BoundField DataField="ChargesId" HeaderText="Charge Id" ReadOnly="True" SortExpression="ChargesId" />
                                                        <asp:BoundField DataField="ChargesName" HeaderText="Charge Name" ReadOnly="True"
                                                            SortExpression="ChargesName" />
                                                        <asp:BoundField DataField="Quantity" HeaderText="Quantity" ReadOnly="True" SortExpression="Quantity" />
                                                        <asp:BoundField DataField="NoofDays" HeaderText="No of Days" ReadOnly="True" SortExpression="NoofDays" />
                                                        <asp:BoundField DataField="Charges" HeaderText="Charge" ReadOnly="True" SortExpression="Charges" />
                                                        <asp:BoundField DataField="Amount" HeaderText="Amount" ReadOnly="True" SortExpression="Amount" />
                                                        <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ImageEditCharge" runat="server" ImageUrl="~/images/edit.jpg"
                                                                    Height="24px" Width="24px" OnClick="btnEditCharges_Click" /></ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ImageDelete" runat="server" ImageUrl="~/images/Erase.png" Height="24px"
                                                                    Width="24px" OnClick="btnDelete_Click" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </asp:Panel>
                                            <table border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                            <table border="1px" cellpadding="0" cellspacing="0" width="80%">
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
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationExpression="^[0-9]{1,11}(?:\.[0-9]{1,3})?$"
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
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationExpression="^[0-9]{1,11}(?:\.[0-9]{1,3})?$"
                                                            ErrorMessage="Please Enter Only Number" Font-Size="11px" ForeColor="Red" ControlToValidate="txtVAT"
                                                            runat="server" ValidationGroup="Save" Display="Dynamic" Font-Names="verdana" />
                                                    </td>
                                                    <td colspan="2" style="border-top: none; border-bottom: none; border-left: none;"
                                                        align="center">
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ValidationExpression="^[0-9]{1,11}(?:\.[0-9]{1,3})?$"
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
                                        </div>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                        <table width="100%">
                            <tr>
                                <td colspan="2">
                                </td>
                            </tr>
                            <tr align="center">
                                <td align="center" colspan="2">
                                    <asp:Button ID="BtnSave" runat="server" Text="Save" Font-Names="Verdana" Font-Size="12px"
                                        ValidationGroup="Save" BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black;
                                        height: 20px;" OnClick="BtnSave_Click" />
                                    <asp:Button ID="btnUpdate" runat="server" Text="Update" Font-Names="Verdana" Font-Size="12px"
                                        BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black"
                                        ValidationGroup="Save" OnClick="BtnEdit_Click" />
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
    <input id="hdnPanel" type="hidden" runat="server" value="none" />
</asp:Content>
