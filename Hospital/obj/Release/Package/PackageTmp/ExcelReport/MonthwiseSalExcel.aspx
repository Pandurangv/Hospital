<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/mstAdmin.Master" CodeBehind="MonthwiseSalExcel.aspx.cs" Inherits="Hospital.ExcelReport.MonthwiseSalExcel" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:GridView ID="dgvMonthSalary" runat="server" ForeColor="#333333" GridLines="Both"
            Font-Names="Verdana" Font-Size="Small">
            <FooterStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#E0F0E8" Font-Size="11px" Wrap="False" />
            <PagerStyle BackColor="#3b3535" ForeColor="White" HorizontalAlign="Left" Font-Size="11px" />
            <SelectedRowStyle BackColor="#006600" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" Font-Size="12px"
                Wrap="False" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" Wrap="False" />
        </asp:GridView>
    </div>
</asp:Content>
