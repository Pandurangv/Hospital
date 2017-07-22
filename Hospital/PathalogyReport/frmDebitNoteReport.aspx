<%@ Page Title="" Language="C#" MasterPageFile="~/mstAdmin.Master" AutoEventWireup="true" CodeBehind="frmDebitNoteReport.aspx.cs" Inherits="Hospital.PathalogyReport.frmDebitNoteReport" %>
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
            var printWindow = window.open('', '', 'height=auto;width=auto');
            printWindow.document.write('<html><head><title>Ganga Nursing Home, Mundhwa</title>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');
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
                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View runat="server" ID="View1">
                        <table width="100%" align="center">
                            <tr>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table align="center" border="2px" cellpadding="0" cellspacing="0" width="845px">
                                        <tr style="height: 10px;">
                                            <td colspan="4" style="border-bottom: none;">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="width: 575px; border-top: none; border-bottom: none; border-right: none;">
                                                <asp:TextBox runat="server" ID="txtSearch" Width="550px" Font-Bold="true" Font-Names="Verdana"
                                                    Font-Size="13px" />
                                                <cc:TextBoxWatermarkExtender ID="txtSearchWatermark" runat="server" TargetControlID="txtSearch"
                                                    WatermarkText="Supplier Name,Supplier Address" WatermarkCssClass="watermarked" />
                                            </td>
                                            <td style="width: 82px; border-top: none; border-bottom: none; border-right: none;
                                                border-left: none;">
                                                <asp:Button ID="btnSearch" Text="Search" runat="server" Font-Names="Verdana" Font-Size="14px"
                                                    ForeColor="White" BackColor="#3b3535" Width="80px" Style="border: 1px solid black"
                                                    OnClick="btnSearch_Click" />
                                            </td>
                                            <td align="center" style="border-left: none; border-top: none; border-right: none;
                                                border-bottom: none; width: 90px;">
                                                <asp:Button ID="btnReset" runat="server" Text="Reset" Font-Names="Verdana" Font-Size="14px"
                                                    BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black"
                                                    OnClick="btnReset_Click" />
                                            </td>
                                            <td style="width: 82px; border-top: none; border-bottom: none; border-left: none;">
                                                <asp:Button ID="Button1" Text="Print" runat="server" Font-Names="Verdana" Font-Size="14px" Width="80px"
                                                    Style="border: 1px solid black" BackColor="#3b3535" ForeColor="White" OnClientClick="javascript:return PrintPanel()" />
                                            </td>
                                        </tr>
                                        <tr style="height: 10px;">
                                            <td colspan="4" style="border-top: none; border-bottom: none;">
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="border-top: none;">
                                    <asp:Label ID="lblMessage" runat="server" Text="" Font-Names="Verdana" Font-Size="11px"
                                        ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Panel ID="pnlContents" runat="server" Height="100%" Width="100%" align="center">
                                        <center>
                                            <asp:Label Text="Debit Note Report" ID="lblDebitNote" ForeColor="#3b3535" Font-Names="Verdana"
                                                Font-Size="16px" Font-Bold="true" runat="server" />
                                        </center>
                                        <asp:GridView ID="dgvTestParameter" runat="server" CellPadding="4" ForeColor="#333333"
                                            GridLines="Both" Font-Names="Verdana" Width="845px" Font-Size="Small" AllowPaging="false"
                                            AutoGenerateColumns="false" align="Center" DataKeyNames="DNNo">
                                            <FooterStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" />
                                            <RowStyle BackColor="#E0F0E8" Font-Size="11px" Wrap="False" />
                                            <PagerStyle BackColor="#3b3535" ForeColor="White" HorizontalAlign="Left" Font-Size="11px" />
                                            <SelectedRowStyle BackColor="#006600" Font-Bold="True" ForeColor="#333333" />
                                            <HeaderStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" Font-Size="12px"
                                                Wrap="False" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <AlternatingRowStyle BackColor="White" Wrap="False" />
                                            <Columns>
                                                <asp:BoundField DataField="DNNo" HeaderText="DebitNoteNo" ReadOnly="true" SortExpression="DNNo" />
                                                <asp:BoundField DataField="SupplierName" HeaderText="Supplier Name" ReadOnly="True"
                                                    SortExpression="SupplierName" />
                                                <asp:BoundField DataField="Address" HeaderText="Address" ReadOnly="True"
                                                    SortExpression="Address" />
                                                <asp:BoundField DataField="DNDate" HeaderText="DebitNoteDate" ReadOnly="True" SortExpression="DNDate"
                                                    DataFormatString="{0:dd/MM/yyyy}" />
                                                <asp:BoundField DataField="NetAmount" HeaderText="NetAmount" ReadOnly="True" SortExpression="NetAmount" />
                                                <asp:BoundField DataField="Amount" HeaderText="Amount" ReadOnly="True" SortExpression="Amount" />
                                                <asp:TemplateField HeaderText="Print" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ImgPrint" runat="server" ImageUrl="~/images/Report.bmp" Height="24px"
                                                            Width="24px" OnClick="btnDebitNotePrint_Click" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                </asp:MultiView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
