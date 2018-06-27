<%@ Page Title="" Language="C#" MasterPageFile="~/mstAdmin.Master" AutoEventWireup="true" CodeBehind="frmIssueMaterial.aspx.cs" Inherits="Hospital.Store.frmIssueMaterial" %>
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
        .style1
        {
            width: 9%;
        }
        .style2
        {
            width: 11%;
        }
    </style>
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
                                    <asp:Label ID="Label1" runat="server" Text="Issue Material" Font-Names="Verdana"
                                        Font-Size="16px" Font-Bold="true" ForeColor="#3b3535"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 15%;">
                                    <table border="1px" cellpadding="0" cellspacing="0" style="height: 40px;" width="820px"
                                        align="center">
                                        <tr>
                                            <td align="center" style="border-right: none; width: 450px;">
                                                <asp:TextBox ID="txtSearch" runat="server" Width="550px"></asp:TextBox>
                                                <cc:TextBoxWatermarkExtender ID="txtSearchWatermark" runat="server" TargetControlID="txtSearch"
                                                    WatermarkText="Patient Name, Emp Name" WatermarkCssClass="watermarked" />
                                            </td>
                                            <td align="center" style="border-left: none; border-right: none; width: 60px;">
                                                <asp:Button ID="btnSearch" runat="server" Text="Search" Font-Names="Verdana" Font-Size="13px"
                                                    BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black"
                                                    OnClick="btnSearch_Click" />
                                            </td>
                                            <td align="center" style="border-left: none; border-right: none; width: 60px;">
                                                <asp:Button ID="btnReset" runat="server" BackColor="#3b3535" Font-Names="Verdana"
                                                    Font-Size="13px" ForeColor="White" OnClick="btnReset_Click" Style="border: 1px solid black"
                                                    Text="Reset" Width="80px" />
                                            </td>
                                            <td align="center" style="border-left: none; width: 60px;">
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
                                        border-style: solid; border-width: 1px" Width="1040px">
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
                                                            DataKeyNames="IssueId" GridLines="Both" Font-Names="Verdana" Width="100%" Font-Size="Small"
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
                                                                <asp:BoundField DataField="IssueId" HeaderText="Issue Material No" ReadOnly="True"
                                                                    SortExpression="IssueId" />
                                                                <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" ReadOnly="True"
                                                                    SortExpression="EmployeeName" />
                                                                <asp:BoundField DataField="PatientName" HeaderText="Patient Name" ReadOnly="True"
                                                                    SortExpression="PatientName" />
                                                                <asp:BoundField DataField="IssueDate" HeaderText="Issue Date" ReadOnly="True" SortExpression="IssueDate"
                                                                    DataFormatString="{0:dd/MM/yyyy}" />
                                                                <asp:BoundField DataField="TotalAmount" HeaderText="Total Amount" ReadOnly="True"
                                                                    SortExpression="TotalAmount" />
                                                                <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="ImageEdit" runat="server" ImageUrl="~/images/edit.jpg" Height="24px"
                                                                            Width="24px" OnClick="btnUpdate_Click" /></ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Print" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="ImgPrint" runat="server" ImageUrl="~/images/Report.bmp" Height="24px"
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
                        <table width="100%" style="border-color: Green; border-style: solid; border-width: 2px"
                            cellpadding="0">
                            <tr style="width: 100%">
                                <td style="width: 100%">
                                    <asp:Panel ID="Panel2" runat="server" Width="100%" Style="text-align: center; background-color: #E0F0E8;
                                        height: 100%;" BorderColor="Green" BorderStyle="Solid" BorderWidth="1px">
                                        <table style="padding-left: 45%;">
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="lblHeading" runat="server" Text="Issue Material" Font-Names="Verdana"
                                                        Font-Size="15px" Font-Bold="true" ForeColor="#3b3535"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                        <div style="float: right; position: relative; width: 80%; right: 9%; top: 2px; left: -89px;"
                                            align="center">
                                            <table style="margin-right: 2px;">
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
                                                        <asp:Label ID="lblPatientName" Text="Patient Name :" runat="server" ForeColor="#3B3535"
                                                            Font-Names="verdana" Font-Size="11px" />
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlPatient" runat="server" Width="150px">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ForeColor="Red"
                                                            ControlToValidate="ddlPatient" Font-Size="13" ValidationGroup="Save" ErrorMessage="*"
                                                            InitialValue="0" Display="Dynamic"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <asp:Label ID="lblEmployeeName" Text="Employee Name :" runat="server" ForeColor="#3B3535"
                                                            Font-Names="verdana" Font-Size="11px" />
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlEmployee" runat="server" Width="150px">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ForeColor="Red"
                                                            ControlToValidate="ddlEmployee" Font-Size="13" ValidationGroup="Save" ErrorMessage="*"
                                                            InitialValue="0" Display="Dynamic"></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td align="right">
                                                        <asp:Label ID="lblDate" Text="Issue Date :" runat="server" ForeColor="#3B3535" Font-Names="verdana"
                                                            Font-Size="11px" />
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtPurchaseDate" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                            Width="150px" MaxLength="10" Format="dd/MM/yyyy"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ForeColor="Red"
                                                            ControlToValidate="txtPurchaseDate" Font-Size="13" ValidationGroup="Save" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td align="left" colspan="2">
                                                        
                                                        <cc:CalendarExtender ID="CalPurchaseDate" runat="server" TargetControlID="txtPurchaseDate"
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
                                                        <asp:Label ID="lblOther" Text="Item Name :" runat="server" ForeColor="#3B3535" Font-Names="verdana"
                                                            Font-Size="11px" />
                                                    </td>
                                                    <td style="padding-left: 5px; width: 8%; border-right: none; border-left: none; border-bottom: none;
                                                        border-top: none;" align="left">
                                                        <asp:DropDownList ID="ddlProduct" runat="server" Width="150px" AutoPostBack="True"
                                                            OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ForeColor="Red"
                                                            ControlToValidate="ddlProduct" Font-Size="13" ValidationGroup="Add" ErrorMessage="*"
                                                            InitialValue="0"></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td align="right" style="border-style: none; border-color: inherit; border-width: medium;" 
                                                        class="style2">
                                                        <asp:Label ID="lblQuantiy" Text="Quantity :" runat="server" ForeColor="#3B3535" Font-Names="verdana"
                                                            Font-Size="11px" />
                                                    </td>
                                                    <td align="left" style="padding-left: 5px; width: 20%; border-left: none; border-bottom: none;
                                                        border-top: none;">
                                                        <asp:TextBox ID="txtQuantity" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                            Width="150px" MaxLength="10"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ForeColor="Red"
                                                            ControlToValidate="txtQuantity" Font-Size="13" ValidationGroup="Add" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr style="height: 5px;">
                                                    <td colspan="2" style="border-top: none; border-bottom: none; border-right: none;">
                                                    </td>
                                                    <td colspan="2" style="border-top: none; border-left: none; border-bottom: none;">
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationExpression="^[0-9]{1,11}(?:\.[0-9]{1,3})?$"
                                                            ErrorMessage="Please Enter Only Number" Font-Bold="False" Font-Size="11px" ForeColor="Red"
                                                            ControlToValidate="txtItemCharge" runat="server" ValidationGroup="Save" Display="Dynamic"
                                                            Font-Names="verdana" />
                                                    </td>
                                                </tr>
                                                <tr style="height: 5px;">
                                                    <td colspan="2" style="border-top: none; border-right: none; border-bottom: none;">
                                                    </td>
                                                    <td colspan="2" style="border-bottom: none; border-top: none; border-left: none;">
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationExpression="^[0-9]{1,11}(?:\.[0-9]{1,3})?$"
                                                            ErrorMessage="Please Enter Only Number" Font-Bold="False" Font-Size="11px" ForeColor="Red"
                                                            ControlToValidate="txtQuantity" runat="server" ValidationGroup="AddProduct" Display="Dynamic"
                                                            Font-Names="verdana" />
                                                    </td>
                                                </tr>
                                                <tr style="height: 10px;">
                                                    <td align="right" style="border-bottom: none; border-top: none; border-right: none;">
                                                        <asp:Label ID="lblCharge0" runat="server" Font-Names="verdana" Font-Size="11px" ForeColor="#3B3535"
                                                            Text="Item Charges :" />
                                                    </td>
                                                    <td style="padding-left: 5px; border-bottom: none; border-top: none; border-left: none;
                                                        border-right: none;">
                                                        <asp:TextBox ID="txtItemCharge" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                            MaxLength="10" Width="150px"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtItemCharge"
                                                            ErrorMessage="*" Font-Size="13" ForeColor="Red" ValidationGroup="Add"></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td align="right" style="border-top: none; border-left: none; border-right: none;
                                                        border-bottom: none;" class="style2">
                                                        <asp:Label ID="Label2" runat="server" Font-Names="verdana" Font-Size="11px" 
                                                            ForeColor="#3B3535" Text="Balance Quantity :" />
                                                        &nbsp;
                                                    </td>
                                                    <td align="left" style="width: 20%; border-left: none; border-bottom: none; border-top: none;">
                                                        &nbsp;
                                                        <asp:Label ID="lblBalQty" runat="server" Font-Names="verdana" Font-Size="11px" 
                                                            ForeColor="#3B3535" />
                                                    </td>
                                                </tr>
                                                <tr style="height: 10px;">
                                                    <td colspan="2" style="border-top: none; border-bottom: none; border-right: none;">
                                                    </td>
                                                    <td colspan="2" style="border-bottom: none; border-top: none; border-left: none;">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" align="right" style="border-top: none; border-bottom: none; border-right: none;
                                                        padding-right: 2px;">
                                                        <asp:Button ID="btnAdd" runat="server" Text="Add" Font-Names="Verdana" Font-Size="12px"
                                                            ValidationGroup="Add" BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black"
                                                            OnClick="btnAddCharge_Click" />
                                                        <asp:Button ID="btnUpdateCharge" runat="server" Text="Update" Font-Names="Verdana"
                                                            Font-Size="12px" ValidationGroup="Add" BackColor="#3b3535" ForeColor="White"
                                                            Width="80px" Style="border: 1px solid black" OnClick="btnUpdateCharge_Click" />
                                                    </td>
                                                    <td align="left" style="border-top: none; border-bottom: none; border-right: none;
                                                        border-left: none; padding-left: 2px;" class="style2">
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
                                            <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" GridLines="Both"
                                                Font-Names="Verdana" Width="80%" Font-Size="Small" AllowPaging="true" PageSize="5"
                                                AutoGenerateColumns="false" DataKeyNames="TempId">
                                                <FooterStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" />
                                                <RowStyle BackColor="#E0F0E8" Font-Size="11px" Wrap="False" />
                                                <PagerStyle BackColor="#3b3535" ForeColor="White" HorizontalAlign="Left" Font-Size="11px" />
                                                <SelectedRowStyle BackColor="#006600" Font-Bold="True" ForeColor="#333333" />
                                                <HeaderStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" Font-Size="12px"
                                                    Wrap="False" />
                                                <EditRowStyle BackColor="#2461BF" />
                                                <AlternatingRowStyle BackColor="White" Wrap="False" />
                                                <Columns>
                                                    <asp:BoundField DataField="ProductId" HeaderText="Product Id" ReadOnly="True" SortExpression="ProductId" />
                                                    <asp:BoundField DataField="ProductName" HeaderText="Product Name" ReadOnly="True"
                                                        SortExpression="ProductName" />
                                                    
                                                    <asp:BoundField DataField="Quantity" HeaderText="Quantity" ReadOnly="True" SortExpression="Quantity" />
                                                    <asp:BoundField DataField="Rate" HeaderText="Charge" ReadOnly="True" SortExpression="Rate" />
                                                    <asp:BoundField DataField="Total" HeaderText="Total" ReadOnly="true" SortExpression="Total" />
                                                    <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImageEditItem" runat="server" ImageUrl="~/images/edit.jpg" Height="24px"
                                                                Width="24px" OnClick="ImageEditItem_Click" /></ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImageDelete" runat="server" ImageUrl="~/images/Erase.png" Height="24px"
                                                                Width="24px" OnClick="btnDelete_Click" /></ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
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
                                                <tr style="width: 100%;">
                                                    <td align="right" style="border-top: none; border-right: none; border-bottom: none;
                                                        width: 40%;">
                                                    </td>
                                                    <td align="right" style="border-top: none; border-right: none; border-left: none;
                                                        border-bottom: none; width: 30%;">
                                                        <asp:Label ID="lblTotal" Text="Total :" runat="server" ForeColor="#3B3535" Font-Names="verdana"
                                                            Font-Size="11px"></asp:Label>
                                                    </td>
                                                    <td align="left" style="padding-left: 5px; border-top: none; border-left: none; border-bottom: none;">
                                                        <asp:TextBox ID="txtTotal" runat="server" Font-Names="Verdana" Font-Size="11px" Width="150px"
                                                            MaxLength="10" ReadOnly="true"></asp:TextBox>
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
    <input id="hdnPanel" type="hidden" runat="server" value="none" />
</asp:Content>

