﻿<%@ Page Title="" Language="C#" MasterPageFile="~/mstAdmin.Master" AutoEventWireup="true" CodeBehind="frmDatewiseInsuranceClaim.aspx.cs" Inherits="Hospital.PathalogyReport.frmDatewiseInsuranceClaim" %>
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
        function PrintPanel() {
            var panel = document.getElementById("<%=pnlContents.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head><title>Ganga Nursing Home, Mundhwa</title>');
            printWindow.document.write('</head><center><body >');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></center></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div>
                <table align="center">
                    <tr>
                        <td style="width: 15%;">
                            <table border="1px" cellpadding="0" cellspacing="0" style="height: 40px;" width="690px"
                                align="center">
                                <tr>
                                    <td style="border-right: none; width: 150px; border-bottom: none;" align="center">
                                        <asp:TextBox ID="txtBillDate" runat="server" Font-Names="Verdana" Font-Size="11px"
                                            Width="150px" MaxLength="10" ></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ForeColor="Red"
                                            ControlToValidate="txtBillDate" Font-Size="13" ValidationGroup="Save" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    </td>
                                    <td style="border-left: none; border-right: none; width: 150px; border-bottom: none;">
                                        <asp:TextBox ID="txtToDate" runat="server" Font-Names="Verdana" Font-Size="11px"
                                            Width="150px" MaxLength="10" ></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red"
                                            ControlToValidate="txtToDate" Font-Size="13" ValidationGroup="Save" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    </td>
                                    <td align="center" style="border-left: none; border-right: none; width: 60px; border-bottom: none;">
                                        <asp:Button ID="btnSearch" Text="Search" Font-Names="Verdana" Font-Size="14px" runat="server"
                                            Width="80px" Style="border: 1px solid black" OnClick="btnSearch_Click" BackColor="#3b3535"
                                            ForeColor="White" ValidationGroup="Save" />
                                    </td>
                                    <td align="center" style="border-left: none; border-right: none; width: 60px; border-bottom: none;">
                                        <asp:Button ID="btnReset" Text="Reset" Font-Names="Verdana" Font-Size="14px" runat="server"
                                            Width="80px" Style="border: 1px solid black" OnClick="btnReset_Click" BackColor="#3b3535"
                                            ForeColor="White" />
                                    </td>
                                    <td align="center" style="border-left: none; border-right: none; width: 60px; border-bottom: none;">
                                        <asp:Button ID="btnPrint" Text="Print" Font-Names="Verdana" Font-Size="14px" runat="server"
                                            Width="80px" Style="border: 1px solid black" BackColor="#3b3535" ForeColor="White"
                                            OnClientClick="javascript:return PrintPanel()" />
                                    </td>
                                    <td align="center" style="border-left: none; width: 60px; border-bottom: none;">
                                        <asp:Button ID="btnExcel" runat="server" Text="Excel" Font-Names="Verdana" Font-Size="14px"
                                            BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black;"
                                             OnClick="btnExcel_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="border-top: none; border-right: none;">
                                        
                                        <cc:CalendarExtender ID="CalBillDate" runat="server" TargetControlID="txtBillDate"
                                            Format="dd/MM/yyyy" DaysModeTitleFormat="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy">
                                        </cc:CalendarExtender>
                                    </td>
                                    <td style="border-top: none; border-left: none;" colspan="5">
                                        
                                        <cc:CalendarExtender ID="CalToDate" runat="server" TargetControlID="txtToDate" Format="dd/MM/yyyy"
                                            DaysModeTitleFormat="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy">
                                        </cc:CalendarExtender>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <asp:Label ID="lblMessage" runat="server" Text="" Font-Names="Verdana" Font-Size="11px"
                    ForeColor="Red"></asp:Label>
                <asp:Panel ID="pnlContents" runat="server" Width="100%" align="center">
                    <table style="width: 60%;" align="center">
                        <tr>
                            <td colspan="2" align="center">
                                <asp:Label ID="lbl" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:Label ID="lblFrom" runat="server" Text=""></asp:Label>
                            </td>
                            <td align="right">
                                <asp:Label ID="lblTo" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="Panel2" ScrollBars="Both" runat="server" Width="100%" Style="text-align: center;
                        background-color: #E0F0E8; height: auto;" BorderColor="Green" BorderStyle="Solid"
                        BorderWidth="1px">
                        <asp:GridView ID="dgvTestParameter" runat="server" CellPadding="4" ForeColor="#333333"
                            GridLines="Both" Font-Names="Verdana" Width="100%" Font-Size="Small" AllowPaging="false"
                            AutoGenerateColumns="false" Height="100">
                            <FooterStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" />
                            <RowStyle BackColor="#E0F0E8" Font-Size="11px" Wrap="False" />
                            <PagerStyle BackColor="#3b3535" ForeColor="White" HorizontalAlign="Left" Font-Size="11px" />
                            <SelectedRowStyle BackColor="#006600" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" Font-Size="12px"
                                Wrap="False" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" Wrap="False" />
                            <Columns>
                                <%--<asp:BoundField DataField="ClaimId" HeaderText="Claim No." ReadOnly="True" SortExpression="ClaimId" />--%>
                                <asp:BoundField DataField="PatientName" HeaderText="Patient Name" ReadOnly="True"
                                    SortExpression="PatientName" />
                                <asp:BoundField DataField="CompanyName" HeaderText="Insurance Company" ReadOnly="True"
                                    SortExpression="CompanyName" />
                                <asp:BoundField DataField="ClaimDate" HeaderText="Claim Date" ReadOnly="True" SortExpression="ClaimDate"
                                    DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:BoundField DataField="Total" HeaderText="Claim Amount" ReadOnly="True" SortExpression="Total"
                                    DataFormatString="{0:0.00}" />
                                <asp:BoundField DataField="ApprovedAmount" HeaderText=" ApprovedAmount" ReadOnly="True"
                                    SortExpression="ApprovedAmount" DataFormatString="{0:0.00}" />
                                <asp:BoundField DataField="TDS" HeaderText=" TDS" ReadOnly="True"
                                    SortExpression="TDS"/>
                                <asp:BoundField DataField="CoPayment" HeaderText="CoPayment" ReadOnly="True"
                                    SortExpression="CoPayment" DataFormatString="{0:0.00}" />
                                <asp:BoundField DataField="BadDebts" HeaderText="BadDebts" ReadOnly="True"
                                    SortExpression="BadDebts" DataFormatString="{0:0.00}" />
                                <asp:BoundField DataField="Category" HeaderText="Status" ReadOnly="True" SortExpression="Category" />
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
