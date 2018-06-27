﻿<%@ Page Title="" Language="C#" MasterPageFile="~/mstAdmin.Master" AutoEventWireup="true" CodeBehind="frmDailyNursingManagement.aspx.cs" Inherits="Hospital.frmDailyNursingManagement" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" language="javascript">
 
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="width: 99%">
                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="View1" runat="server">
                        <asp:HiddenField ID="Myflag" runat="server" />
                        <asp:HiddenField ID="Charges" runat="server" />
                        <asp:HiddenField ID="TempId" runat="server" />
                        <asp:HiddenField ID="Prescript" runat="server" />
                        <asp:HiddenField ID="PrescriptionId" runat="server" />
                        <asp:HiddenField ID="Sr_No" runat="server" />
                        
                        
                        <table width="100%">
                            <tr>
                                <td align="center">
                                    <asp:Label ID="Label1" runat="server" Text="Daily Nursing Management" Font-Names="Verdana"
                                        Font-Size="16px" Font-Bold="True" ForeColor="#3B3535"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="width: 15%;">
                                    <table border="1px" cellpadding="0" cellspacing="0" style="height: 40px;" width="930px">
                                        <tr>
                                            <td align="center" style="border-right: none; width: 575px;">
                                                <asp:TextBox ID="txtSearch" runat="server" Font-Bold="true" Font-Names="Verdana"
                                                    Font-Size="13px" Width="580px" />
                                            </td>
                                            <td align="center" style="border-left: none; border-right: none; width: 60px;">
                                                <asp:Button ID="btnSearch" runat="server" BackColor="#3b3535" Font-Names="Verdana"
                                                    Font-Size="14px" ForeColor="White" OnClick="btnSearch_Click" Style="border: 1px solid black"
                                                    Text="Search" Width="80px" />
                                            </td>
                                            <td align="center" style="border-left: none; border-right: none; width: 60px;">
                                                <asp:Button ID="btnReset" runat="server" BackColor="#3b3535" Font-Names="Verdana"
                                                    Font-Size="14px" ForeColor="White" OnClick="btnReset_Click" Style="border: 1px solid black"
                                                    Text="Reset" Width="80px" />
                                            </td>
                                            <td align="center" style="border-left: none; border-right: none; width: 60px;">
                                                <asp:Button ID="BtnAddNewPrescription" runat="server" Text="Add New" Font-Names="Verdana"
                                                    Font-Size="14px" BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black"
                                                    onmouseover="SetBtnMouseOver(this)" onmouseout="SetBtnMouseOut(this)" OnClick="BtnAddNewPrescription_Click" />
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
                            <tr>
                                <td>
                                    <asp:Panel ID="pnlShow" BorderColor="Maroon" BorderWidth="1px" runat="server" Style="border-color: Green;
                                        border-style: solid; border-width: 1px">
                                        <table width="100%">
                                            <tr>
                                                <td align="left" class="style1">
                                                    <asp:Label ID="lblPageCount" runat="server" Text="" Font-Names="Verdana" Font-Size="11px"
                                                        ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="right" class="style1" colspan="2">
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
                                                    <asp:Panel ID="pnl" runat="server" Width="1020px" Style="border-color: Green; border-style: solid;
                                                        border-width: 1px">
                                                        <asp:Label ID="lblMessage" runat="server" Text="" Font-Names="Verdana" Font-Size="11px"
                                                            ForeColor="Red"></asp:Label>
                                                        <asp:GridView ID="dgvClaim" runat="server" CellPadding="4" ForeColor="#333333" GridLines="Both"
                                                            Font-Names="Verdana" Width="100%" Font-Size="Small" AllowPaging="true" PageSize="20"
                                                            AutoGenerateColumns="false" DataKeyNames="SrNo" OnDataBound="dgvClaim_DataBound"
                                                            OnPageIndexChanged="dgvClaim_PageIndexChanged" OnPageIndexChanging="dgvClaim_PageIndexChanging">
                                                            <FooterStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" />
                                                            <RowStyle BackColor="#E0F0E8" Font-Size="11px" Wrap="False" />
                                                            <PagerStyle BackColor="#3b3535" ForeColor="White" HorizontalAlign="Left" Font-Size="11px" />
                                                            <SelectedRowStyle BackColor="#006600" Font-Bold="True" ForeColor="#333333" />
                                                            <HeaderStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" Font-Size="12px"
                                                                Wrap="False" />
                                                            <EditRowStyle BackColor="#2461BF" />
                                                            <AlternatingRowStyle BackColor="White" Wrap="False" />
                                                            <Columns>
                                                                <asp:BoundField DataField="SrNo" HeaderText="Sr No" ReadOnly="True" SortExpression="SrNo" />
                                                                <asp:BoundField DataField="TreatmentDate" HeaderText="Treatment Date" ReadOnly="True" DataFormatString="{0:dd/MM/yyyy}" />
                                                                <asp:BoundField DataField="NurseName" HeaderText="Nurse Name" ReadOnly="True" SortExpression="NurseName" />
                                                                <asp:BoundField DataField="Department" HeaderText="Department" ReadOnly="True" SortExpression="Department" />
                                                                <asp:BoundField DataField="CategoryDesc" HeaderText="Category Name" ReadOnly="True"
                                                                    SortExpression="CategoryDesc" />
                                                                <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="ImageEdit" runat="server" ImageUrl="~/images/edit.jpg" Height="24px"
                                                                            Width="24px" OnClick="BtnEdit_Click" /></ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Print" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="ImgPrint" runat="server" ImageUrl="~/images/Report.bmp" Height="24px"
                                                                            Width="24px" OnClick="btnPrint_Click" /></ItemTemplate>
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
                        <table width="100%">
                            <tr style="width: 100%;">
                                <td width="100%">
                                    <asp:Panel ID="pnlGrid" runat="server" Width="1040px" Style="text-align: center;
                                        background-color: #E0F0E8; height: auto;">
                                        <table width="100%" style="border-color: #3b3535; border-style: solid; border-width: 2px"
                                            cellpadding="0">
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="Panel5" runat="server" Width="99.7%" Style="text-align: center; background-color: #E0F0E8;
                                                        height: auto;" BorderColor="#3b3535" BorderStyle="Solid" BorderWidth="1px">
                                                        <table width="100%">
                                                            <tr>
                                                                <td align="center" colspan="4">
                                                                    <asp:Label ID="lblHeading" runat="server" Text="Daily Nursing Management" Font-Names="Verdana"
                                                                        Font-Size="15px" Font-Bold="true" ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4" align="center">
                                                                    <asp:Label ID="lblMsg" Text="" runat="server" ForeColor="Red" Font-Names="Verdana"
                                                                        Font-Size="11px"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 25%;" align="right">
                                                                    <asp:Label ID="lblRoomNo" runat="server" Text="Nurse Name :" Font-Names="Verdana"
                                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td style="width: 25%;" align="left">
                                                                    <asp:DropDownList ID="ddlNurseName" runat="server" Width="200px" >
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ForeColor="Red"
                                                                        ControlToValidate="ddlNurseName" Font-Size="13" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                                                                </td>
                                                                <td style="width: 25%;" align="right">
                                                                    <asp:Label ID="lblBedNo" runat="server" Text="Department :" Font-Names="Verdana"
                                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td style="width: 25%;" align="left">
                                                                    <asp:TextBox ID="txtDepartment" runat="server" Font-Names="Verdana" MaxLength="250"
                                                                        Font-Size="11px" Width="130px" ReadOnly="true"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="lblCategory" Text="Ward Category :" runat="server" Font-Names="Verdana"
                                                                        Font-Size="11px" ForeColor="#3b3535" />
                                                                </td>
                                                                <td align="left">
                                                                    <asp:DropDownList ID="ddlWardCategory" runat="server" Width="200px" Font-Size="11px"
                                                                        Font-Names="Verdana">
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvCategory" runat="server" ForeColor="Red" ControlToValidate="ddlWardCategory"
                                                                        InitialValue="0" Font-Size="13" ValidationGroup="Save" ErrorMessage="*" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                </td>
                                                                <td align="right">
                                                                    <asp:Label ID="Label4" Text="Treatment Date : " runat="server" Font-Names="Verdana"
                                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtTreatmentDate" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                        Width="130px" MaxLength="10"></asp:TextBox>
                                                                    <cc:CalendarExtender ID="CalDate" runat="server" TargetControlID="txtTreatmentDate"
                                                                        Format="dd/MM/yyyy">
                                                                    </cc:CalendarExtender>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtTreatmentDate"
                                                                        Display="Dynamic" ErrorMessage="*" Font-Size="13" ForeColor="Red" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                </td>
                                                                <td align="center" colspan="2">
                                                                    
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="Panel3" runat="server" Width="99.7%" Style="border-color: #3b3535;
                                                        border-style: solid; border-width: 1px">
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="width: 25%;" align="right">
                                                                    <asp:Label ID="Label2" Text="Patient Name : " runat="server" Font-Names="Verdana"
                                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td style="width: 25%;" align="left">
                                                                    <asp:DropDownList ID="ddlPatient" runat="server" Width="200px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td align="right" style="width: 25%;">
                                                                    <asp:Label ID="Label15" runat="server" Text="Treatment Time :" Font-Names="Verdana"
                                                                        Font-Size="11px" ForeColor="#3B3535"></asp:Label>
                                                                </td>
                                                                <td align="left" style="width: 25%;">
                                                                    <cc1:TimeSelector ID="TreatmentTimeSelector" runat="server" DisplaySeconds="false"
                                                                        SelectedTimeFormat="TwentyFour">
                                                                    </cc1:TimeSelector>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="Label3" runat="server" Text="INJECTABLE MEDICATIONS :" Font-Names="Verdana"
                                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtInjectableMedi" runat="server" Font-Names="Verdana" TextMode="MultiLine"
                                                                        Font-Size="11px" Width="280px" ReadOnly="false"></asp:TextBox>
                                                                </td>
                                                                <td align="right">
                                                                    <asp:Label ID="lblInfusions" runat="server" Text="INFUSIONS :" Font-Names="Verdana"
                                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtInfusions" runat="server" Font-Names="Verdana" TextMode="MultiLine"
                                                                        Font-Size="11px" Width="280px" ReadOnly="false"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="lblOral" runat="server" Text="Oral/Topical/Aerosolised /Others Routes :"
                                                                        Font-Names="Verdana" Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtOral" runat="server" Font-Names="Verdana" TextMode="MultiLine"
                                                                        Font-Size="11px" Width="280px" ReadOnly="false"></asp:TextBox>
                                                                </td>
                                                                <td align="right">
                                                                    <asp:Label ID="Label6" runat="server" Text="NON Pharmacological Therapy And Nursing Care :"
                                                                        Font-Names="Verdana" Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtNursingCare" runat="server" Font-Names="Verdana" TextMode="MultiLine"
                                                                        Font-Size="11px" Width="280px" ReadOnly="false"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr style="height: 30px;">
                                                                <td colspan='4'>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4" align="center" style="border-top: none; border-bottom: none;">
                                                                    <asp:Button ID="btnAdd" runat="server" Text="Add" Font-Names="Verdana" Font-Size="12px"
                                                                        ValidationGroup="AddOther" BackColor="#3b3535" ForeColor="White" Width="80px"
                                                                        Style="border: 1px solid black" OnClick="btnAddCharge_Click" />
                                                                    <asp:Button ID="btnUpdatecharge" runat="server" Text="Update" Font-Names="Verdana"
                                                                        Font-Size="12px" ValidationGroup="AddOther" BackColor="#3b3535" ForeColor="White"
                                                                        Width="80px" Style="border: 1px solid black" OnClick="btnUpdatecharge_Click" />
                                                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" Font-Names="Verdana" Font-Size="12px"
                                                                        BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black"
                                                                        OnClick="btnCancel_Click" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4">
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="Panel1" runat="server" ScrollBars="Both" Width="1040px" Style="border-color: #3b3535;
                                                        border-style: solid; border-width: 1px">
                                                        <asp:GridView ID="dgvChargeDetails" runat="server" CellPadding="4" ForeColor="#333333"
                                                            GridLines="Both" Font-Names="Verdana" Width="100%" Font-Size="Small" AllowPaging="false"
                                                            AutoGenerateColumns="false" DataKeyNames="TempId">
                                                            <FooterStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" />
                                                            <RowStyle BackColor="#E0F0E8" Font-Size="11px" Wrap="False" />
                                                            <PagerStyle BackColor="#3b3535" ForeColor="White" HorizontalAlign="Left" Font-Size="11px" />
                                                            <SelectedRowStyle BackColor="#006600" Font-Bold="True" ForeColor="#333333" />
                                                            <HeaderStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" Font-Size="12px"
                                                                Wrap="False" />
                                                            <EditRowStyle BackColor="#2461BF" />
                                                            <AlternatingRowStyle BackColor="White" Wrap="False" />
                                                            <Columns>
                                                                <asp:BoundField DataField="PatientName" HeaderText="Patient Name" ReadOnly="True"
                                                                    SortExpression="PatientName" />
                                                                <asp:BoundField DataField="TreatmentTime" HeaderText="TreatmentTime" ReadOnly="True"
                                                                    SortExpression="TreatmentTime" />
                                                                <asp:BoundField DataField="InjectableMedications" HeaderText="InjectableMedications"
                                                                    ReadOnly="True" SortExpression="InjectableMedications" />
                                                                <asp:BoundField DataField="Infusions" HeaderText="Infusions"
                                                                    ReadOnly="True" SortExpression="Infusions" />
                                                                <asp:BoundField DataField="Oral" HeaderText="Oral/Topical" ReadOnly="True"
                                                                    SortExpression="Oral" />
                                                                <asp:BoundField DataField="NursingCare" HeaderText="Nursing Care" ReadOnly="True"
                                                                    SortExpression="NursingCare" />
                                                                <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="ImageEdit" runat="server" ImageUrl="~/images/edit.jpg" Height="24px"
                                                                            Width="24px" OnClick="BtnEditCharge_Click" /></ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="ImgDelete" runat="server" ImageUrl="~/images/Erase.png" Height="24px"
                                                                            Width="24px" OnClick="btnDelete_Click" /></ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="BtnSave" runat="server" Text="Save" Font-Names="Verdana" Font-Size="12px"
                                                        ValidationGroup="Save" BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black"
                                                        OnClick="BtnSave_Click" />
                                                    <asp:Button ID="btnUpdate" runat="server" Text="Update" Font-Names="Verdana" Font-Size="12px"
                                                        BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black"
                                                        OnClick="BtnUpdate_Click" />
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
</asp:Content>
