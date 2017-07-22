<%@ Page Title="" Language="C#" MasterPageFile="~/mstAdmin.Master" AutoEventWireup="true" CodeBehind="Sample.aspx.cs" Inherits="Hospital.Sample" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="width: 99%">
                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View runat="server" ID="View2">
                        <div style="width: 100%; height: auto">
                            <br />
                            <table width="100%">
                                <tr style="width: 100%;">
                                    <td colspan="3" align="center">
                                        <asp:Label ID="lblPatientReceipt" runat="server" Text="Supplier Payment" Font-Names="Verdana"
                                            Font-Size="16px" Font-Bold="True" ForeColor="#3B3535"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" style="width: 15%;" colspan="3">
                                        <table border="1px" cellpadding="0" cellspacing="0" width="860px" style="height: 40px;">
                                            <tr style="width: 100%">
                                                <td align="center" style="border-right: none; width: 575px;">
                                                </td>
                                                <td align="center" style="border-left: none; border-right: none; width: 60px;">
                                                    <asp:Button ID="btnAddNew" runat="server" BackColor="#3b3535" Font-Names="Verdana"
                                                        Font-Size="12px" ForeColor="White" OnClick="btnAddNew_Click" Style="border: 1px solid black"
                                                        Text="New" Width="80px" />
                                                </td>
                                                <td align="center" style="border-left: none; border-right: none; width: 90px;">
                                                </td>
                                                <td style="border-left: none; width: 60px; padding-right: 4px;">
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
                                GridLines="Both" Font-Names="Verdana" 
                                Width="100%" Font-Size="Small"
                                AllowPaging="true" PageSize="15" AutoGenerateColumns="false" >
                                <FooterStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" />
                                <RowStyle BackColor="#E0F0E8" Font-Size="11px" Wrap="False" Font-Names="Verdana" />
                                <PagerStyle BackColor="#3b3535" ForeColor="White" HorizontalAlign="Left" Font-Size="11px" />
                                <SelectedRowStyle BackColor="#006600" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" Font-Size="12px"
                                    Wrap="False" Font-Names="verdana" />
                                <EditRowStyle BackColor="#2461BF" />
                                <AlternatingRowStyle BackColor="White" Wrap="False" />
                                <Columns>
                                    <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="True" />
                                    <asp:BoundField DataField="SupplierName" HeaderText="Supplier Name" ReadOnly="True" />
                                    <asp:BoundField DataField="Address" HeaderText="Address" ReadOnly="True" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </asp:View>
                </asp:MultiView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
