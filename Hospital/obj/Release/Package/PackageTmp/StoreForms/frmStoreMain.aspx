<%@ Page Title="" Language="C#" MasterPageFile="~/mstAdmin.Master" AutoEventWireup="true" CodeBehind="frmStoreMain.aspx.cs" Inherits="Hospital.StoreForms.frmStoreMain" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="98%">
        <tr>
            <td align="center">
                <asp:Label ID="Label1" runat="server" Text="Item Code" Font-Bold="true" Font-Names="Verdana"
                    Font-Size="11px"></asp:Label>
            </td>
            <td align="center">
                <asp:Label ID="Label2" runat="server" Text="Item Description" Font-Bold="true" Font-Names="Verdana"
                    Font-Size="11px"></asp:Label>
            </td>
            <td align="center">
                <asp:Label ID="Label3" runat="server" Text="Available Quantity" Font-Bold="true"
                    Font-Names="Verdana" Font-Size="11px"></asp:Label>
            </td>
            <td align="center">
                <asp:Label ID="Label4" runat="server" Text="Supplier Name" Font-Bold="true" Font-Names="Verdana"
                    Font-Size="11px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Panel ID="Panel2" runat="server" BackColor="White" Style="border-color: Green;
                    border-style: solid; border-width: 1px; width: 100%; margin-left: 10px; margin-top: 10px">
                    <marquee behaviour="Scroll" direction="Up" height="500px" width="100%" scrolldelay="200"
                        onmouseover="this.stop();" onmouseout="this.start();">       
        <table width="100%" style=" padding-left:50px;">        
        <tr>
        <td style="width:25%"> <asp:Label ID="lblItemCode" runat="server" Text=""  ForeColor="red" Font-Bold="true" Font-Names="Verdana" Font-Size="11px"></asp:Label></td>
        <td style="width:25%"><asp:Label ID="lblitemDesc" runat="server" Text="" ForeColor="red" Font-Bold="true" Font-Names="Verdana" Font-Size="11px"></asp:Label></td>
        <td style="width:25%"><asp:Label ID="lblQuantity" runat="server" Text="" ForeColor="red" Font-Bold="true" Font-Names="Verdana" Font-Size="11px"></asp:Label></td>
        <td style="width:25%"> <asp:Label ID="lblSupplierName" runat="server" Text="" ForeColor="red" Font-Bold="true" Font-Names="Verdana" Font-Size="11px"></asp:Label></td>
        </tr> </table>       
         </marquee>
                </asp:Panel>
            </td>
        </tr>
    </table>
    
</asp:Content>
