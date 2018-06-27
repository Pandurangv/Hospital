﻿<%@ Page Title="" Language="C#" MasterPageFile="~/mstAdmin.Master" AutoEventWireup="true" CodeBehind="frmCustomerTransaction.aspx.cs" Inherits="Hospital.Billing.frmCustomerTransaction" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="cc1" %>
<script runat="server">   
</script>
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="width: 99%">
                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="View1" runat="server">
                    <asp:HiddenField ID="update" runat="server" />
                    <asp:HiddenField ID="Status" runat="server" />
                    <asp:HiddenField ID="ReceiptNo" runat="server" />
                        <table width="100%">
                            <tr>
                                <td align="center">
                                    <asp:Label ID="lblCUstomer" runat="server" Text="Patient Receipt" Font-Names="Verdana"
                                        Font-Size="16px" Font-Bold="True" ForeColor="#3b3535"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <asp:Label ID="lblMsg" ForeColor="Red" runat="server" Font-Names="verdana" Font-Size="11px" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel ID="pnlShow" BorderColor="Maroon" BorderWidth="1px" runat="server" Style="border-color: Green;
                                        border-style: solid; border-width: 1px">
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2" align="center">
                                                    <asp:Label ID="lblNote" Text="NOTE :- Please Pay Full Amount" runat="server" ForeColor="Red"
                                                        Font-Names="Verdana" Font-Size="11px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" style='width:45%'>
                                                    <asp:Label ID="Label1" runat="server" Text=" " Font-Names="Verdana"
                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left" style="width:55%">
                                                    <asp:TextBox ID="txtNew" runat="server" Width="248px" AutoPostBack="true" OnTextChanged="txtNew_TextChanged"></asp:TextBox>
                                                    <cc:TextBoxWatermarkExtender ID="txtNewWatermark" runat="server" TargetControlID="txtNew"
                                                        WatermarkText="Type First Name For Search" WatermarkCssClass="watermarked" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" style='width:45%'>
                                                    <asp:Label ID="lblShift" runat="server" Text="Patient Name :" Font-Names="Verdana"
                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left" style="width:55%">
                                                    <asp:DropDownList ID="ddlPatient" runat="server" Style="width: 180px;" AutoPostBack="True"
                                                        OnSelectedIndexChanged="ddlPatient_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red"
                                                        ControlToValidate="ddlPatient" Font-Size="13" ValidationGroup="Save" ErrorMessage="*"
                                                        Display="Dynamic" InitialValue="0"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label7" runat="server" Text="TransactionDate :" Font-Names="Verdana"
                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left" ">
                                                    <asp:TextBox ID="txtTransactDate" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                        Width="140px" MaxLength="10"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ForeColor="Red"
                                                        ControlToValidate="txtTransactDate" Font-Size="13" ValidationGroup="Save" ErrorMessage="*"
                                                        Display="Dynamic"></asp:RequiredFieldValidator>
                                                    <cc:CalendarExtender ID="CalDOBDate" runat="server" TargetControlID="txtTransactDate"
                                                        Format="dd/MM/yyyy" DaysModeTitleFormat="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy hh:mm:ss tt">
                                                    </cc:CalendarExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="2">
                                                    
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="lblBillRefNo" runat="server" Text="Bill Ref. No :" Font-Names="Verdana"
                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label> 
                                                </td>
                                                <td align="left" >
                                                    <asp:TextBox ID="txtBillRefNo" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label3" runat="server" Text="Balance Amount :" Font-Names="Verdana"
                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtTotal" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="lblPatientCat" runat="server" Text="Patient Category :" Font-Names="Verdana"
                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left" >
                                                    <asp:DropDownList ID="ddlPatientCategory" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                        Width="180px" >
                                                        <asp:ListItem Selected="True" Text="--Select --" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="Self" Value="Self"></asp:ListItem>
                                                        <asp:ListItem Text="Company" Value="Company"></asp:ListItem>
                                                        <asp:ListItem Text="Insurance" Value="Insurance"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="lbltransact" runat="server" Text="Transaction Type :" Font-Names="Verdana"
                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left" >
                                                    <asp:RadioButton ID="IsCash" runat="server" Text="Is Cash" GroupName="AmountType"
                                                        OnCheckedChanged="IsCash_CheckedChanged" AutoPostBack="True" Font-Names="verdana"
                                                        Font-Size="11px"></asp:RadioButton>
                                                    <asp:RadioButton ID="IsCheque" runat="server" Text="Is Cheque" GroupName="AmountType"
                                                        OnCheckedChanged="IsCash_CheckedChanged" AutoPostBack="True" Font-Names="verdana"
                                                        Font-Size="11px"></asp:RadioButton>
                                                    
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    
                                                </td>
                                                <td align="left" >
                                                    <asp:RadioButton ID="IsCard" runat="server" Text="Is Card" GroupName="AmountType"
                                                        OnCheckedChanged="IsCash_CheckedChanged" AutoPostBack="True"
                                                        Font-Names="verdana" Font-Size="11px"></asp:RadioButton>
                                                    <asp:RadioButton ID="IsRTGS" runat="server" Text="Is RTGS/NEFT" GroupName="AmountType"
                                                        OnCheckedChanged="IsCash_CheckedChanged" AutoPostBack="True"
                                                        Font-Names="verdana" Font-Size="11px"></asp:RadioButton>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="lblAdvanceAmnt" runat="server" Text="Advance Amount :" Font-Names="Verdana"
                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left" >
                                                    <asp:TextBox ID="txtAdvanceAmount" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="lblCompanyName" runat="server" Text="Company Name :" Font-Names="Verdana"
                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left" >
                                                    <asp:DropDownList ID="ddlCompany" runat="server" Style="width: 180px;">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="lblBankName" runat="server" Text="Bank Name :" Font-Names="Verdana"
                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtBankName" runat="server" Width="180px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="lblchequeNo" runat="server" Text="Cheque No :" Font-Names="Verdana"
                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left" >
                                                    <asp:TextBox ID="txtChequeNo" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ForeColor="Red"
                                                        ControlToValidate="txtChequeNo" Font-Size="13" ValidationGroup="Save" ErrorMessage="*"
                                                        Display="Dynamic"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="lblChequeDate" runat="server" Text="Cheque Date :" Font-Names="Verdana"
                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtChequeDate" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ForeColor="Red"
                                                        ControlToValidate="txtChequeDate" Font-Size="13" ValidationGroup="Save" ErrorMessage="*"
                                                        Display="Dynamic"></asp:RequiredFieldValidator>
                                                    <cc:CalendarExtender ID="CalChecqueDate" runat="server" TargetControlID="txtChequeDate"
                                                        Format="dd/MM/yyyy" DaysModeTitleFormat="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy hh:mm:ss tt">
                                                    </cc:CalendarExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="2">
                                                    
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="lblBankRefNo" runat="server" Text="Bank Reference No :" Font-Names="Verdana"
                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtBankRefNo" runat="server" Width="180px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" >
                                                    <asp:Label ID="lblCashAmount" runat="server" Text="Amount :" Font-Names="Verdana"
                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtPayAmount" runat="server" Width="180px"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ForeColor="Red"
                                                        ControlToValidate="txtPayAmount" Font-Size="13" ValidationGroup="Save" ErrorMessage="*"
                                                        Display="Dynamic"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr style="height: 7px;">
                                                <td colspan="2">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td align="center" colspan="2">
                                                    <asp:Button ID="BtnSave" runat="server" Text="Save" Font-Names="Verdana" Font-Size="12px"
                                                        ValidationGroup="Save" BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black;"
                                                        OnClick="BtnSave_Click" />
                                                    <asp:Button ID="BtnUpdate" runat="server" BackColor="#3b3535" Font-Names="Verdana"
                                                        Font-Size="12px" ForeColor="White" OnClick="BtnEdit_Click" Style="border: 1px solid black"
                                                        Text="Update" ValidationGroup="Save" Width="80px" />
                                                    <asp:Button ID="btnCancel" runat="server" BackColor="#3b3535" Font-Names="Verdana"
                                                        Font-Size="12px" ForeColor="White" OnClick="btnCancel_Click" Style="border: 1px solid black"
                                                        Text="Cancel" Width="80px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                    <asp:View runat="server" ID="View2">
                        <div style="width: 100%; height: auto">
                            <br />
                            <table width="100%">
                                <tr style="width: 100%;">
                                    <td colspan="3" align="center">
                                        <asp:Label ID="lblPatientReceipt" runat="server" Text="Patient Receipt" Font-Names="Verdana"
                                            Font-Size="16px" Font-Bold="True" ForeColor="#3b3535"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" style="width: 15%;" colspan="3">
                                        <table border="1px" cellpadding="0" cellspacing="0" width="860px" style="height: 40px;">
                                            <tr style="width: 100%">
                                                <td align="center" style="border-right: none; width: 575px;">
                                                    <asp:TextBox ID="txtSearch" runat="server" Width="580px"></asp:TextBox>
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
                                                    <asp:Button ID="btnAddNew" runat="server" BackColor="#3b3535" Font-Names="Verdana"
                                                        Font-Size="13px" ForeColor="White" OnClick="btnAddNew_Click" Style="border: 1px solid black"
                                                        Text="New" Width="80px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="2">
                                        <asp:Label ID="lblPageCount" runat="server" Text="" Font-Names="Verdana" Font-Size="11px"
                                            ForeColor="#3b3535"></asp:Label>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="lblRowCount1" runat="server" Font-Names="Verdana" Font-Size="11px"
                                            ForeColor="#3b3535" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="3">
                                        <asp:Label ID="lblMessage" runat="server" Text="" Font-Names="Verdana" Font-Size="11px"
                                            ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <asp:GridView ID="dgvCustTransaction" runat="server" CellPadding="4" ForeColor="#333333"
                                DataKeyNames="ReceiptNo" GridLines="Both" Font-Names="Verdana" Width="100%" Font-Size="Small"
                                AllowPaging="true" PageSize="15" AutoGenerateColumns="false" OnPageIndexChanged="dgvCustTransaction_PageIndexChanged"
                                OnPageIndexChanging="dgvCustTransaction_PageIndexChanging">
                                <FooterStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" />
                                <RowStyle BackColor="#E0F0E8" Font-Size="11px" Wrap="False" Font-Names="Verdana" />
                                <PagerStyle BackColor="#3b3535" ForeColor="White" HorizontalAlign="Left" Font-Size="11px" />
                                <SelectedRowStyle BackColor="#006600" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" Font-Size="12px"
                                    Wrap="False" Font-Names="verdana" />
                                <EditRowStyle BackColor="#2461BF" />
                                <AlternatingRowStyle BackColor="White" Wrap="False" />
                                <Columns>
                                    <asp:BoundField DataField="ReceiptNo" HeaderText="Receipt No." ReadOnly="True" SortExpression="ReceiptNo" />
                                    <asp:BoundField DataField="ReceiptDate" HeaderText="Date" ReadOnly="True" DataFormatString="{0:dd/MM/yyyy}" />
                                    <asp:BoundField DataField="PatientName" HeaderText="Patient Name" ReadOnly="True"
                                        SortExpression="PatientName" />
                                    <asp:BoundField DataField="Address" HeaderText="Address" ReadOnly="True" SortExpression="Address" />
                                    <asp:BoundField DataField="AdvanceAmount" HeaderText="AdvanceAmount" ReadOnly="True"
                                        SortExpression="AdvanceAmount" DataFormatString="{0:0.00}" />
                                    <asp:BoundField DataField="Amount" HeaderText="Amount" ReadOnly="True" SortExpression="Amount" />
                                    <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageEdit" runat="server" ImageUrl="~/images/edit.jpg" Height="24px"
                                                Width="24px" OnClick="btnUpdate_Click" /></ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Print" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImagePrint" runat="server" ImageUrl="~/images/Report.bmp" Height="24px"
                                                Width="24px" OnClick="btnPrint_Click" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </asp:View>
                </asp:MultiView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
