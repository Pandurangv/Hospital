<%@ Page Title="" Language="C#" MasterPageFile="~/mstAdmin.Master" AutoEventWireup="true" CodeBehind="frmChargeCategory.aspx.cs" Inherits="Hospital.frmChargeCategory" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="cc1" %>
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
                        <asp:HiddenField ID="Charges_Id" runat="server" />

                        <table width="100%">
                            <tr>
                                <td align="center">
                                    <asp:Label ID="Label1" runat="server" Text="Charge Master" Font-Names="Verdana" Font-Size="16px"
                                        Font-Bold="true" ForeColor="#3b3535"></asp:Label>
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
                                                    WatermarkCssClass="watermarked" WatermarkText="Charges Name" />
                                            </td>
                                            <td align="center" style="border-left: none; border-right: none; width: 60px;">
                                                <asp:Button ID="btnSearch" runat="server" BackColor="#3b3535" Font-Names="Verdana"
                                                    Font-Size="13px" ForeColor="White" OnClick="btnSearch_Click" Style="border: 1px solid black"
                                                    Text="Search" Width="80px" />
                                            </td>
                                            <td align="center" style="border-left: none; border-right: none; width: 90px;">
                                                <asp:Button ID="btnReset" runat="server" BackColor="#3b3535" Font-Names="Verdana"
                                                    Font-Size="13px" ForeColor="White" OnClick="btnReset_Click" Style="border: 1px solid black"
                                                    Text="Reset" Width="80px" />
                                            </td>
                                            <td align="center" style="border-left: none; width: 60px; padding-right: 4px;">
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
                                                        <asp:GridView ID="dgvShift" runat="server" CellPadding="4" ForeColor="#333333" DataKeyNames="ChargesId"
                                                            GridLines="Both" Font-Names="Verdana" Width="100%" Font-Size="Small" AllowPaging="true"
                                                            PageSize="15" AutoGenerateColumns="false" OnDataBound="dgvDepartment_DataBound"
                                                            OnPageIndexChanged="dgvDepartment_PageIndexChanged" OnPageIndexChanging="dgvDepartment_PageIndexChanging"
                                                            Style="font-family: System">
                                                            <FooterStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" />
                                                            <RowStyle BackColor="#E0F0E8" Font-Size="11px" Wrap="False" Font-Names="Verdana" />
                                                            <PagerStyle BackColor="#3b3535" ForeColor="White" HorizontalAlign="Left" Font-Size="11px" />
                                                            <SelectedRowStyle BackColor="#006600" Font-Bold="True" ForeColor="#333333" />
                                                            <HeaderStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" Font-Size="12px"
                                                                Wrap="False" Font-Names="verdana" />
                                                            <EditRowStyle BackColor="#2461BF" />
                                                            <AlternatingRowStyle BackColor="White" Wrap="False" />
                                                            <Columns>
                                                                <asp:BoundField DataField="ChargesId" HeaderText="Charge Id" ReadOnly="True" SortExpression="ChargesId" />
                                                                <asp:BoundField DataField="ChargeCategoryName" HeaderText="Category Name" ReadOnly="True"
                                                                    SortExpression="ChargeCategoryName" />
                                                                <asp:BoundField DataField="Charges" HeaderText="Charges" ReadOnly="True"
                                                                    SortExpression="Charges" DataFormatString="{0:0.00}"/>
                                                                <asp:BoundField DataField="IsOperation" HeaderText="Operation" ReadOnly="True" SortExpression="IsOperation" />
                                                                <asp:BoundField DataField="IsBed" HeaderText="Bed" ReadOnly="True" SortExpression="IsBed" />
                                                                <asp:BoundField DataField="IsConsulting" HeaderText="Consulting" ReadOnly="True"
                                                                    SortExpression="IsConsulting" />
                                                                <asp:BoundField DataField="IsOther" HeaderText="Other" ReadOnly="True" SortExpression="IsOther" />
                                                                <asp:BoundField DataField="IsRMO" HeaderText="RMO" ReadOnly="True" SortExpression="IsRMO" />
                                                                <asp:BoundField DataField="IsNursing" HeaderText="Nursing" ReadOnly="True" SortExpression="IsNursing" />
                                                                <asp:BoundField DataField="IsICU" HeaderText="ICU" ReadOnly="True" SortExpression="IsICU" />
                                                                <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="ImageEdit" runat="server" ImageUrl="~/images/edit.jpg" Height="24px"
                                                                            Width="24px" OnClick="btnUpdate_Click" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" />
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
                                    <asp:Panel ID="Panel2" runat="server" Width="99.7%" Style="text-align: center; background-color: #E0F0E8;
                                        height: 220px;" BorderColor="Green" BorderStyle="Solid" BorderWidth="1px">
                                        <table width="100%">
                                            <tr style="width: 100%;">
                                                <td align="center" colspan="4">
                                                    <asp:Label ID="lblHeading" runat="server" Text="Charge Master" Font-Names="Verdana"
                                                        Font-Size="15px" Font-Bold="true" ForeColor="#3b3535"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4">
                                                    <asp:Label ID="lblMsg" runat="server" Text="" Font-Names="Verdana" Font-Size="11px"
                                                        ForeColor="Red"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr style="width: 100%;">
                                                <td align="right" style="width: 40%;">
                                                    <asp:Label ID="lblChargeName" runat="server" Text="Charge Name :" Font-Names="Verdana"
                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtChargeCategory" runat="server" MaxLength="50" Font-Names="Verdana"
                                                        Font-Size="11px" Width="150px"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red"
                                                        ControlToValidate="txtChargeCategory" Font-Size="13" ValidationGroup="Save" ErrorMessage="*"
                                                        Display="Dynamic"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr style="width: 100%;">
                                                <td align="right" style="width: 40%;">
                                                    <asp:Label ID="lblCharges" runat="server" Text="Charges :" Font-Names="Verdana" Font-Size="11px"
                                                        ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtCharges" runat="server" MaxLength="50" Font-Names="Verdana" Font-Size="11px"
                                                        Width="150px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="width: 40%;">
                                                    <asp:Label ID="lblCategory" runat="server" Text="Category Type :" Font-Names="Verdana"
                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:RadioButton ID="rdOpera" Text="Is Operation" GroupName="Category" Font-Names="Verdana"
                                                        Font-Size="11px" ForeColor="#3b3535" runat="server" Checked="false" />
                                                    <asp:RadioButton ID="rdBed" Text="Is Bed" GroupName="Category" Font-Names="Verdana"
                                                        Font-Size="11px" ForeColor="#3b3535" runat="server" Checked="false" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="width: 40%;">
                                                </td>
                                                <td align="left">
                                                    <asp:RadioButton ID="rdConsult" Text="Is Consulting" GroupName="Category" Font-Names="Verdana"
                                                        Font-Size="11px" ForeColor="#3b3535" runat="server" Checked="false" />
                                                    <asp:RadioButton ID="rdOther" Text="Is Other" GroupName="Category" Font-Names="Verdana"
                                                        Font-Size="11px" ForeColor="#3b3535" runat="server" Checked="false" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="width: 40%;">
                                                </td>
                                                <td align="left">
                                                    <asp:RadioButton ID="rdoRMO" Text="Is RMO" GroupName="Category" Font-Names="Verdana"
                                                        Font-Size="11px" ForeColor="#3b3535" runat="server" Checked="false" />
                                                    <asp:RadioButton ID="rdoNursing" Text="Is Nursing" GroupName="Category" Font-Names="Verdana"
                                                        Font-Size="11px" ForeColor="#3b3535" runat="server" Checked="false" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="width: 40%;">
                                                </td>
                                                <td align="left">
                                                    <asp:RadioButton ID="rdoICU" Text="ICU" GroupName="Category" Font-Names="Verdana"
                                                        Font-Size="11px" ForeColor="#3b3535" runat="server" Checked="false" />
                                                </td>
                                            </tr>
                                            <tr style="height: 10px;">
                                                <td colspan="4">
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td align="center" colspan="4">
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
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                </asp:MultiView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <input id="hdnPanel" type="hidden" runat="server" value="none" />
    <script type="text/javascript">
        function ValidateRadioButtons(sender, args) {
            args.IsValid = $("input[name=Category]:checked").length > 0;
        }
    </script>
    <script type="text/javascript">
        function ValidateRadioButtons(sender, args) {
            args.IsValid = $("input[name=Category]:checked").length > 0;
        }
    </script>
</asp:Content>
