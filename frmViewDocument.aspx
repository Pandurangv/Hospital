<%@ Page Title="" Language="C#" MasterPageFile="~/mstAdmin.Master" AutoEventWireup="true" CodeBehind="frmViewDocument.aspx.cs" Inherits="Hospital.frmViewDocument" %>
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
    <table width="100%">
        <tr>
            <td align="center">
                <asp:Label ID="Label1" runat="server" Text="List Of Patient Documents" Font-Names="Verdana" ForeColor="Black"
                    Font-Size="16px" Font-Bold="true"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="center" style="width: 15%;">
                <table border="2px" cellpadding="0" cellspacing="0" width="860px" style="height: 40px;">
                    <tr>
                        <td align="center" style="border-right: none; width: 575px;">
                            <asp:TextBox ID="txtSearch" Font-Bold="true" Font-Names="Verdana" Font-Size="13px"
                                Width="580px" runat="server" />
                            <cc:textboxwatermarkextender id="txtSearchWatermark" runat="server" targetcontrolid="txtSearch"
                                watermarktext="Patient Name,DocumentName" watermarkcssclass="watermarked" />
                        </td>
                        <td align="center" style="border-left: none; border-right: none; width: 60px;">
                            <asp:Button ID="btnSearch" Text="Search" Font-Names="Verdana" Font-Size="14px" runat="server"
                                Width="80px" Style="border: 1px solid black" OnClick="btnSearch_Click" BackColor="#3b3535"
                                ForeColor="White" />
                        </td>
                        <td align="center" style="border-left: none; border-right: none; width: 90px;">
                            <asp:Button ID="btnReset" runat="server" Text="Reset" Font-Names="Verdana" Font-Size="14px"
                                BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black"
                                OnClick="btnReset_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="pnlShow" BorderColor="Maroon" BorderWidth="1px" runat="server" Style="border-color: Maroon;
                    border-style: solid; border-width: 1px; display: none;">
                    <table width="100%" style="overflow: hidden; table-layout: fixed; position: relative;">
                        <tr>
                            <td align="left">
                                <asp:Label ID="lblPageCount" runat="server" Text="" Font-Names="Verdana" Font-Size="11px" ForeColor="Black"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblRowCount" runat="server" Text="" Font-Names="Verdana" Font-Size="11px" ForeColor="Black"></asp:Label>
                            </td>
                            <td align="right">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%" colspan="3">
                                <asp:Panel ID="pnl" ScrollBars="Both" runat="server" Width="100%">
                                    <asp:GridView ID="dgvCustomerDetail" runat="server" CellPadding="4" ForeColor="#333333"
                                        DataKeyNames="PKId" GridLines="Both" Font-Names="Verdana" Width="100%" Font-Size="Small"
                                        AllowPaging="true" PageSize="20" AutoGenerateColumns="false" BackColor="#FAF0FF"
                                        OnDataBound="dgvCustomerDetail_DataBound" OnPageIndexChanged="dgvCustomerDetail_PageIndexChanged"
                                        OnPageIndexChanging="dgvCustomerDetail_PageIndexChanging" OnRowCommand="dgvCustomerDetail_RowCommand"
                                        OnRowDataBound="dgvCustomerDetail_RowDataBound">
                                        <FooterStyle BackColor="#666699" Font-Bold="True" ForeColor="Black" />
                                        <RowStyle BackColor="#EFF3FB" Font-Size="11px" Wrap="False" />
                                        <PagerStyle BackColor="#666699" ForeColor="White" HorizontalAlign="Left" Font-Size="11px" />
                                        <SelectedRowStyle BackColor="#666699" Font-Bold="True" ForeColor="#333333" />
                                        <HeaderStyle BackColor="#666699" Font-Bold="True" ForeColor="White" Font-Size="12px"
                                            Wrap="False" />
                                        <EditRowStyle BackColor="#FAF0FF" />
                                        <AlternatingRowStyle BackColor="#F0E6F0" Wrap="False" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr. No." ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPKId" runat="server" Text='<%#Eval("PKId") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="UploadDate" HeaderText="Date" ReadOnly="True" DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField DataField="PatientName" HeaderText="Patient Name" ReadOnly="True"
                                                SortExpression="PatientName" />
                                            <asp:TemplateField HeaderText="Document Name" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDocumentName" runat="server" Text='<%#Eval("DocumentName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Document File" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="DownloadFile" runat="server" ImageUrl="~/Images/pdf-icon.png"
                                                        ImageAlign="Middle" ToolTip="Download File" CommandName="DownloadFile" />
                                                </ItemTemplate>
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
    <input id="hdnPanel" type="hidden" runat="server" value="none" />
</asp:Content>
