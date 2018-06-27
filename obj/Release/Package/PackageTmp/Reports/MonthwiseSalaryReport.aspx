<%@ Page Title="" Language="C#" MasterPageFile="~/mstAdmin.Master" AutoEventWireup="true" CodeBehind="MonthwiseSalaryReport.aspx.cs" Inherits="Hospital.Reports.MonthwiseSalaryReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1
        {
            width: 59px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="width: 99%">
                <table width="100%">
                    <tr>
                        <td align="center">
                            <asp:Label ID="Label1" runat="server" Text="Monthwise Salary Report" Font-Names="Verdana"
                                Font-Size="16px" Font-Bold="true" ForeColor="#3b3535"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="width: 15%;">
                            <table border="1px" cellpadding="0" cellspacing="0" style="height: 40px;" width="530px"
                                align="center">
                                <tr>
                                    <td style="border-right: none; width: 90px;" align="center">
                                        <asp:Label ID="Label2" runat="server" Text="Salary Month : " Font-Names="Verdana"
                                            Font-Size="11px" Font-Bold="true" ForeColor="#3b3535"></asp:Label>
                                    </td>
                                    <td style="border-left: none; border-right: none; width: 150px;">
                                        <asp:TextBox ID="txtMonth" runat="server" Font-Names="Verdana" Font-Size="11px" Width="150px"
                                            MaxLength="10"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ForeColor="Red"
                                            ControlToValidate="txtMonth" Font-Size="13" ValidationGroup="Save" ErrorMessage="*"></asp:RequiredFieldValidator>
                                        <cc:CalendarExtender ID="Calmonth" runat="server" TargetControlID="txtMonth" Format="MMM/yyyy">
                                        </cc:CalendarExtender>
                                    </td>
                                    <td style="border-left: none; border-right: none; width: 60px;">
                                        <asp:Button ID="btnSearch" runat="server" BackColor="#3b3535" Font-Names="Verdana"
                                            Font-Size="14px" ForeColor="White" OnClick="btnSearch_Click" Style="border: 1px solid black"
                                            Text="Search" Width="80px" />
                                    </td>
                                    <td align="center" style="border-left: none; border-right: none; width: 60px;">
                                        <asp:Button ID="btnReset" runat="server" BackColor="#3b3535" Font-Names="Verdana"
                                            Font-Size="14px" ForeColor="White" OnClick="btnReset_Click" Style="border: 1px solid black"
                                            Text="Reset" Width="80px" />
                                    </td>
                                    <td align="center" style="border-left: none; width: 60px;">
                                        <asp:Button ID="btnExcel" runat="server" Text="Excel" Font-Names="Verdana" Font-Size="14px"
                                            BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black;"
                                            onmouseover="SetBtnMouseOver(this)" onmouseout="SetBtnMouseOut(this)" OnClick="btnExcel_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr style="width: 100%;">
                        <td style="width: 100%;">
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
                                    <tr style="width: 100%">
                                        <td align="center" style="width: 100%" colspan="3">
                                            <asp:Label ID="lblMessage" runat="server" Text="" Font-Names="Verdana" Font-Size="11px"
                                                ForeColor="Red"></asp:Label>
                                            <table border="0" cellpadding="0" cellspacing="0" width="80%">
                                                <tr>
                                                    <td style="width: 50%; padding-left:20%;" align="left">
                                                        <asp:Label ID="lblMonth" runat="server" Text="" Font-Names="Verdana" Font-Size="11px"
                                                            ForeColor="#3b3535"></asp:Label>
                                                    </td>
                                                    <td style="width: 50%;padding-right:20%;" align="right">
                                                        <asp:Label ID="lblDate" runat="server" Text="" Font-Names="Verdana" Font-Size="11px"
                                                            ForeColor="#3b3535"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                            <asp:Panel ID="Panel1" Width="80%" runat="server" ScrollBars="Both">
                                                <asp:GridView ID="dgvMonthSalary" runat="server" ForeColor="#333333" GridLines="Both"
                                                    Font-Names="Verdana" Font-Size="Small" DataKeyNames="SalId">
                                                    <FooterStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" />
                                                    <RowStyle BackColor="#E0F0E8" Font-Size="11px" Wrap="False" />
                                                    <PagerStyle BackColor="#3b3535" ForeColor="White" HorizontalAlign="Left" Font-Size="11px" />
                                                    <SelectedRowStyle BackColor="#006600" Font-Bold="True" ForeColor="#333333" />
                                                    <HeaderStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" Font-Size="12px"
                                                        Wrap="False" />
                                                    <EditRowStyle BackColor="#2461BF" />
                                                    <AlternatingRowStyle BackColor="White" Wrap="False" />
                                                    <Columns>
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
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
