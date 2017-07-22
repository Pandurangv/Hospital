﻿<%@ Page Title="" Language="C#" MasterPageFile="~/mstAdmin.Master" AutoEventWireup="true" CodeBehind="frmOperationMaster.aspx.cs" Inherits="Hospital.frmOperationMaster" %>
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="width: 99%">
                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="View1" runat="server">
                        <asp:HiddenField ID="Cat_Id" runat="server" />
                        <table width="100%">
                            <tr>
                                <td align="center">
                                    <asp:Label ID="Label1" runat="server" Text="Operation Master" Font-Names="Verdana"
                                        Font-Size="16px" Font-Bold="true" ForeColor="#3b3535"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="width: 15%;">
                                    <table border="2px" cellpadding="0" cellspacing="0" width="860px" style="height: 40px;">
                                        <tr>
                                            <td align="center" style="border-right: none; width: 575px;">
                                                <asp:TextBox ID="txtSearch" runat="server" Font-Bold="true" Font-Names="Verdana"
                                                    Font-Size="13px" Width="580px" />
                                                <cc:TextBoxWatermarkExtender ID="txtSearchWatermark" runat="server" TargetControlID="txtSearch"
                                                    WatermarkCssClass="watermarked" WatermarkText="Operation Name,Category Name" />
                                            </td>
                                            <td align="center" style="border-left: none; border-right: none; width: 60px;">
                                                <asp:Button ID="btnSearch" runat="server" BackColor="#3b3535" Font-Names="Verdana"
                                                    Font-Size="13px" ForeColor="White" Style="border: 1px solid black" Text="Search"
                                                    Width="80px" OnClick="btnSearch_Click" />
                                            </td>
                                            <td align="center" style="border-left: none; border-right: none; width: 90px;">
                                                <asp:Button ID="btnReset" runat="server" BackColor="#3b3535" Font-Names="Verdana"
                                                    Font-Size="13px" ForeColor="White" Style="border: 1px solid black" Text="Reset"
                                                    Width="80px" OnClick="btnReset_Click" />
                                            </td>
                                            <td align="center" style="border-left: none; width: 60px; padding-right: 4px;">
                                                <asp:Button ID="BtnAddNewRoom" runat="server" BackColor="#3b3535" Font-Names="Verdana"
                                                    Font-Size="13px" ForeColor="White" OnClick="BtnAddNewRoom_Click" Style="border: 1px solid black"
                                                    Text="Add New" Width="80px" />
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
                                                <td colspan="3">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100%" colspan="3">
                                                    <asp:Label ID="lblMessage" runat="server" Text="" Font-Names="Verdana" Font-Size="11px"
                                                        ForeColor="Red"></asp:Label>
                                                    <asp:Panel ID="pnl" runat="server" Width="1010px" BorderColor="Green" BorderStyle="Solid"
                                                        BorderWidth="1px">
                                                        <asp:GridView ID="dgvRoomMaster" runat="server" CellPadding="4" ForeColor="#333333"
                                                            GridLines="Both" Font-Names="Verdana" Width="100%" Font-Size="Small" AllowPaging="true"
                                                            PageSize="15" AutoGenerateColumns="false" OnPageIndexChanged="dgvRoomMaster_PageIndexChanged"
                                                            OnPageIndexChanging="dgvRoomMaster_PageIndexChanging" DataKeyNames="OperationId">
                                                            <FooterStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" />
                                                            <RowStyle BackColor="#E0F0E8" Font-Size="11px" Wrap="False" />
                                                            <PagerStyle BackColor="#3b3535" ForeColor="White" HorizontalAlign="Left" Font-Size="11px" />
                                                            <SelectedRowStyle BackColor="#006600" Font-Bold="True" ForeColor="#333333" />
                                                            <HeaderStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" Font-Size="12px"
                                                                Wrap="False" />
                                                            <EditRowStyle BackColor="#2461BF" />
                                                            <AlternatingRowStyle BackColor="White" Wrap="False" />
                                                            <Columns>
                                                                <asp:BoundField DataField="OperationId" HeaderText="Operation Id" ReadOnly="True"
                                                                    SortExpression="OperationId" />
                                                                <asp:BoundField DataField="CatName" HeaderText="Category Name" ReadOnly="True" SortExpression="CatName" />
                                                                <asp:BoundField DataField="OperationName" HeaderText="Operation Name" ReadOnly="True"
                                                                    SortExpression="OperationName" />
                                                                <asp:BoundField DataField="Price" HeaderText="Charges" ReadOnly="True" SortExpression="Price" />
                                                                <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="ImageEdit" runat="server" ImageUrl="~/images/edit.jpg" Height="24px"
                                                                            Width="24px" OnClick="btnUpdate_Click" /></ItemTemplate>
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
                        <asp:Panel Width="1010px" runat="server" ID="pnlGrid">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td align="center" colspan="2">
                                        <asp:Label ID="lblHeader" Text="Operation Master" runat="server" Font-Names="Verdana"
                                            Font-Size="16px" ForeColor="#3b3535" Font-Bold="true" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center">
                                        <asp:Label ID="lblMsg" runat="server" Text="" Font-Names="Verdana" Font-Size="11px"
                                            ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="width: 100%;">
                                    <td align="right" style="width: 45%;">
                                        <asp:Label ID="lblCategoryName" Text="Category Name : " runat="server" Font-Names="Verdana"
                                            Font-Size="11px" ForeColor="#3b3535" />
                                    </td>
                                    <td align="left" style="width: 30%;">
                                        <asp:DropDownList ID="ddlCategoryName" runat="server" Width="150px" Font-Size="11px"
                                            Font-Names="Verdana" AutoPostBack="True" OnSelectedIndexChanged="ddlCategoryName_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvCategoryName" runat="server" ForeColor="Red" ControlToValidate="ddlCategoryName"
                                            Font-Size="13" ValidationGroup="Save" ErrorMessage="*" InitialValue="0" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr style="height: 5px;">
                                    <td colspan="2">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="lblCatOperation" Text="Operation/Surgery Name : " runat="server" Font-Names="Verdana"
                                            Font-Size="11px" ForeColor="#3b3535" />
                                    </td>
                                    <td align="left" style="width: 45%;">
                                        <asp:TextBox ID="txtCatOperation" runat="server" MaxLength="50" Font-Names="Verdana"
                                            Font-Size="11px" Width="300px"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="revCatOperation" ValidationExpression="[a-zA-Z ]*$"
                                            ErrorMessage="Please Enter Only Character" Font-Bold="False" Font-Size="11px"
                                            ForeColor="Red" ControlToValidate="txtCatOperation" runat="server" ValidationGroup="Save"
                                            Display="Dynamic" Font-Names="verdana" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ForeColor="Red"
                                            ControlToValidate="txtCatOperation" Font-Size="13" ValidationGroup="Save" ErrorMessage="*"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr style="height: 5px;">
                                    <td colspan="2">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="lblPrice" runat="server" Text="Charges :" Font-Names="Verdana" Font-Size="11px"
                                            ForeColor="#3b3535"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtPrice" runat="server" MaxLength="100" Font-Names="Verdana" Font-Size="11px"
                                            Width="150px" Font-Bold="False"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationExpression="^[0-9]{1,11}(?:\.[0-9]{1,3})?$"
                                            ErrorMessage="Please Enter Only Number" Font-Bold="False" Font-Size="11px" ForeColor="Red"
                                            ControlToValidate="txtPrice" runat="server" ValidationGroup="Save" Display="Dynamic"
                                            Font-Names="verdana" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ForeColor="Red"
                                            ControlToValidate="txtPrice" Font-Size="13" ValidationGroup="Save" ErrorMessage="*"
                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr style="height: 5px;">
                                    <td colspan="2">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
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
                        </asp:Panel>
                    </asp:View>
                </asp:MultiView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <input id="hdnPanel" type="hidden" runat="server" value="none" />
</asp:Content>
