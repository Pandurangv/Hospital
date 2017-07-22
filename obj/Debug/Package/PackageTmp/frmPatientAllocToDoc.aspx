﻿<%@ Page Title="" Language="C#" MasterPageFile="~/mstAdmin.Master" AutoEventWireup="true" CodeBehind="frmPatientAllocToDoc.aspx.cs" Inherits="Hospital.frmPatientAllocToDoc" %>
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
        .style1
        {
            width: 124px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="width: 99%">
                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="View1" runat="server">
                    <asp:HiddenField ID="Doc_Id" runat="server" />
                    <asp:HiddenField ID="update" runat="server" />
                    <asp:HiddenField ID="pat_id" runat="server" />
                        <table width="100%">
                            <tr>
                                <td align="center">
                                    <asp:Label ID="Label1" runat="server" Text="Patient Allocation" Font-Names="Verdana"
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
                                                    WatermarkCssClass="watermarked" WatermarkText="Patient Id,Patient Name,Employee Name" />
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
                                                <asp:Button ID="BtnAddNewRoom" runat="server" BackColor="#3b3535" Font-Names="Verdana"
                                                    Font-Size="13px" ForeColor="White" OnClick="BtnAddNewRoom_Click" Style="border: 1px solid black"
                                                    Text="Add New" Width="80px" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 15%;">
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
                                                <td colspan="2">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100%" colspan="2">
                                                    <asp:Label ID="lblMessage" runat="server" Text="" Font-Names="Verdana" Font-Size="11px"
                                                        ForeColor="Red"></asp:Label>
                                                    <asp:Panel ID="pnl"  runat="server" Width="1010px" BorderColor="Green"
                                                        BorderStyle="Solid" BorderWidth="1px">
                                                        <asp:GridView ID="dgvAllocPatient" runat="server" CellPadding="4" ForeColor="#333333"
                                                            GridLines="Both" Font-Names="Verdana" Width="100%" Font-Size="Small" AllowPaging="true"
                                                            PageSize="15" AutoGenerateColumns="false" 
                                                            OnPageIndexChanged="dgvRoomMaster_PageIndexChanged"
                                                            OnPageIndexChanging="dgvRoomMaster_PageIndexChanging" 
                                                            DataKeyNames="PKId">
                                                            <FooterStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" />
                                                            <RowStyle BackColor="#E0F0E8" Font-Size="11px" Wrap="False" />
                                                            <PagerStyle BackColor="#3b3535" ForeColor="White" HorizontalAlign="Left" Font-Size="11px" />
                                                            <SelectedRowStyle BackColor="#006600" Font-Bold="True" ForeColor="#333333" />
                                                            <HeaderStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" Font-Size="12px"
                                                                Wrap="False" />
                                                            <EditRowStyle BackColor="#2461BF" />
                                                            <AlternatingRowStyle BackColor="White" Wrap="False" />
                                                            <Columns>
                                                                <asp:BoundField DataField="PKId" HeaderText="Patient ID"  />
                                                                <asp:BoundField DataField="PatientName" HeaderText="Patient Name"  />
                                                                <asp:BoundField DataField="EmpName" HeaderText="Employee Name"  />
                                                                <asp:BoundField DataField="AppointDate" HeaderText="Appointment Date" DataFormatString="{0:dd/MM/yyyy}"/>
                                                                <asp:BoundField DataField="StartTime" HeaderText="Appointment Start Time" 
                                                                     DataFormatString="{0:hh:mm:ss tt}" />
                                                                <asp:BoundField DataField="EndTime" HeaderText="Appointment End Time" DataFormatString="{0:hh:mm:ss tt}" />
                                                                <asp:BoundField DataField="Charges" HeaderText="Charges"  />
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
                                    <td align="center" colspan="4">
                                        <asp:Label ID="lblHeader" Text="Patient Allocation" runat="server" Font-Names="Verdana"
                                            Font-Size="16px" ForeColor="#3b3535" Font-Bold="true" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" align="center">
                                        <asp:Label ID="lblMsg" runat="server" Text="" Font-Names="Verdana" Font-Size="11px"
                                            ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="width: 100%;">
                                    <td align="right" style="width: 25%;">
                                        <asp:Label ID="lblPatientName" Text="Patient Name : " runat="server" Font-Names="Verdana"
                                            Font-Size="11px" ForeColor="#3b3535" />
                                    </td>
                                    <td align="left" style="width:25%">
                                        <asp:DropDownList ID="ddlPatientName" runat="server" Width="150px" Font-Size="11px"
                                            Font-Names="Verdana" AutoPostBack="True" OnSelectedIndexChanged="ddlPatientName_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlPatientName" runat="server" ForeColor="Red"
                                            ControlToValidate="ddlPatientName" InitialValue="0" Font-Size="13" ValidationGroup="Save"
                                            ErrorMessage="*" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                    <td align="right" style="width: 25%;">
                                        <asp:Label ID="lblPatientType" Text="Patient Type : " runat="server" Font-Names="Verdana"
                                            Font-Size="11px" ForeColor="#3b3535" />
                                    </td>
                                    <td align="left" style="width: 25%;">
                                        <asp:TextBox runat="server" ID="txtPatientType" ReadOnly="True" Width="150px" />
                                    </td>
                                </tr>
                                <tr style="height: 5px;">
                                    <td colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="lblDoctorName" Text="Doctor Name  : " runat="server" Font-Names="Verdana"
                                            Font-Size="11px" ForeColor="#3b3535" />
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlDoctorName" runat="server" Width="150px" Font-Size="11px"
                                            Font-Names="Verdana" AutoPostBack="True" OnSelectedIndexChanged="ddlDoctorName_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvddlDoctorName" runat="server" ForeColor="Red"
                                            ControlToValidate="ddlDoctorName" Font-Size="13" ValidationGroup="Save" ErrorMessage="*"
                                            InitialValue="0" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="lblCharges" Text="Charges Per Day : " runat="server" Font-Names="Verdana"
                                            Font-Size="11px" ForeColor="#3b3535" />
                                    </td>
                                    <td align="left">
                                        <asp:TextBox runat="server" ID="txtCharge" Width="150px" />
                                    </td>
                                </tr>
                                <tr style="height: 5px;">
                                    <td colspan="3">
                                    </td>
                                    <td align="left">
                                        <asp:RegularExpressionValidator ID="revTxtFirstName" ValidationExpression="\d+(\.\d{1,2})?"
                                            ErrorMessage="Please Enter Only Amount" Font-Size="11px" ForeColor="Red" ControlToValidate="txtCharge"
                                            runat="server" ValidationGroup="Save" Display="Dynamic" Font-Names="verdana" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="lblAppDate" Text="Appointment Date : " runat="server" Font-Names="Verdana"
                                            Font-Size="11px" ForeColor="#3b3535" />
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtAppDate" runat="server" Font-Names="Verdana" Font-Size="11px"
                                            Width="120px" MaxLength="10"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ForeColor="Red"
                                            ControlToValidate="txtAppDate" Font-Size="11" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                                        <cc:CalendarExtender ID="CalAppDate" runat="server" TargetControlID="txtAppDate"
                                            Format="dd/MM/yyyy">
                                        </cc:CalendarExtender>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="lblAppointment" Text="Appointment Time  : " runat="server" Font-Names="Verdana"
                                            Font-Size="11px" ForeColor="#3b3535" />
                                    </td>
                                    <td align="left">
                                        <asp:TextBox runat="server" ID="txtStartTime" ReadOnly="True" Width="80px" />
                                        <asp:Label ID="Label2" Text="-" runat="server" Font-Bold="true" Font-Size="X-Large"
                                            ForeColor="#3b3535" />
                                        <asp:TextBox runat="server" ID="txtEndTime" ReadOnly="True" Width="80px" />
                                    </td>
                                </tr>
                                <tr style="height: 5px;">
                                    <td colspan="2">
                                        
                                    </td>
                                    <td align="left" class="style1">
                                        &nbsp;</td>
                                    <td colspan="2">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" align="center">
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
