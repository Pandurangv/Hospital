<%@ Page Title="" Language="C#" MasterPageFile="~/mstAdmin.Master" AutoEventWireup="true" CodeBehind="frmOTScheduling.aspx.cs" Inherits="Hospital.Scheduling.frmOTScheduling" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="width: 99%; height: 1203px;">
                <asp:MultiView ID="MultiView1" runat="server">
                    <asp:View ID="View1" runat="server">
                        <table width="100%">
                            <tr>
                                <td align="center">
                                    <asp:Label ID="Label1" runat="server" Text="OT SCHEDULING" Font-Names="Verdana"
                                        Font-Size="16px" Font-Bold="true" ForeColor="#3b3535"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 15%;">
                                    &nbsp;
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
                                                        <asp:GridView ID="dgvDepartment" runat="server" CellPadding="4" ForeColor="#333333"
                                                            DataKeyNames="BedId" GridLines="Both" Font-Names="Verdana" Width="100%" Font-Size="Small"
                                                            AllowPaging="true" PageSize="20" AutoGenerateColumns="false" OnDataBound="dgvDepartment_DataBound"
                                                            OnPageIndexChanged="dgvDepartment_PageIndexChanged" OnPageIndexChanging="dgvDepartment_PageIndexChanging"
                                                            OnRowDataBound="dgvDepartment_RowDataBound" Style="font-family: System">
                                                            <FooterStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" />
                                                            <RowStyle BackColor="#E0F0E8" Font-Size="11px" Wrap="False" Font-Names="Verdana" />
                                                            <PagerStyle BackColor="#3b3535" ForeColor="White" HorizontalAlign="Left" Font-Size="11px" />
                                                            <SelectedRowStyle BackColor="#006600" Font-Bold="True" ForeColor="#333333" />
                                                            <HeaderStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" Font-Size="12px"
                                                                Wrap="False" Font-Names="verdana" />
                                                            <EditRowStyle BackColor="#2461BF" />
                                                            <AlternatingRowStyle BackColor="White" Wrap="False" />
                                                            <Columns>
                                                                <asp:BoundField DataField="BedId" HeaderText="Bed Id" ReadOnly="True" SortExpression="BedId" />
                                                                <asp:BoundField DataField="BedNo" HeaderText="Bed Number" ReadOnly="True" SortExpression="BedNo" />
                                                                <asp:BoundField DataField="RoomNo" HeaderText="Room Number" ReadOnly="True" SortExpression="RoomNo" />
                                                                <asp:BoundField DataField="FloorName" HeaderText="Floor Name" ReadOnly="True" SortExpression="FloorName" />
                                                               <%-- <asp:BoundField DataField="FullName" HeaderText="Patient Name" ReadOnly="true" SortExpression="FullName" />
                                                                <asp:BoundField DataField="EmpName" HeaderText="Doctor Name" ReadOnly="true" SortExpression="EmpName" />
                                                                <asp:BoundField DataField="CategoryName" HeaderText="Operation Type" ReadOnly="true"
                                                                    SortExpression="CategoryName" />
                                                                <asp:BoundField DataField="AllocationDate" HeaderText="Allocation Time" ReadOnly="true"
                                                                    SortExpression="AllocationDate" />
                                                                <asp:BoundField DataField="DischargeDate" HeaderText="Discharge Time" ReadOnly="true"
                                                                    SortExpression="DischargeDate" />--%>
                                                                <asp:TemplateField HeaderText="Allocate" ItemStyle-HorizontalAlign="Left">
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
                                        height: 310px;" BorderColor="Green" BorderStyle="Solid" BorderWidth="1px">
                                        <table width="100%">
                                            <tr style="width: 100%;">
                                                <td align="center" colspan="6">
                                                    <asp:Label ID="lblHeading" runat="server" Text="OT SCHEDULING" Font-Names="Verdana"
                                                        Font-Size="15px" Font-Bold="true" ForeColor="#3b3535"></asp:Label>
                                                </td>
                                            </tr>
                                            <%--<tr>
                                                <td colspan="6">
                                                    &nbsp;
                                                </td>
                                            </tr>--%>
                                            <tr>
                                                <td colspan="6" align="center">
                                                    <asp:Label Text="" ID="lblMsg" ForeColor="Red" Font-Names="Verdana" Font-Size="11px"
                                                        runat="server" />
                                                </td>
                                            </tr>
                                            <tr style="width: 100%;">
                                                <td align="right" style="width: 12%;">
                                                    <asp:Label ID="lblOperationName" runat="server" Text="Surgery/Operation Type :" Font-Names="Verdana"
                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td style="width: 7%;" colspan="2" align="left">
                                                    <asp:DropDownList ID="ddlOperationName" runat="server" Width="150px" AutoPostBack="True"
                                                        OnSelectedIndexChanged="ddlOperationName_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red"
                                                        ControlToValidate="ddlOperationName" Font-Size="13" ValidationGroup="Save" ErrorMessage="*"
                                                        InitialValue="0" Display="Dynamic"></asp:RequiredFieldValidator>
                                                </td>
                                                <td style="width: 7%;" align="right">
                                                    <asp:Label ID="lblDocName" runat="server" Text="Assistant Doctor :" Font-Names="Verdana"
                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td style="width: 5%;" colspan="2" align="left">
                                                    <asp:DropDownList ID="ddlDocName" runat="server" Width="150px" AutoPostBack="True"
                                                        OnSelectedIndexChanged="ddlDocName_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ForeColor="Red"
                                                        ControlToValidate="ddlDocName" Font-Size="13" ValidationGroup="Save" ErrorMessage="*"
                                                        InitialValue="0" Display="Dynamic"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr style="width: 100%;">
                                                <td align="right" style="width: 15%;">
                                                    <asp:Label ID="Label2" runat="server" Text="Surgery/Operation Name :" Font-Names="Verdana"
                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td style="width: 7%;" colspan="2" align="left">
                                                    <asp:DropDownList ID="ddlOper" runat="server" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="ddlOper_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvOper" runat="server" ForeColor="Red" ControlToValidate="ddlOper"
                                                        Font-Size="13" ValidationGroup="Save" ErrorMessage="*" InitialValue="0" Display="Dynamic"></asp:RequiredFieldValidator>
                                                </td>
                                                <td style="width: 5%;" align="right">
                                                    <asp:Label ID="Label3" runat="server" Text="Surgery/Operation Charges :" Font-Names="Verdana"
                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td style="width: 7%;" colspan="2" align="left">
                                                    <asp:TextBox ID="txtOperationCharges" runat="server" MaxLength="50" Font-Names="Verdana"
                                                        Font-Size="11px" Width="150px" ReadOnly="True"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr style="width: 100%;">
                                                <td align="right" style="width: 15%;">
                                                    <asp:Label ID="Label4" runat="server" Text="Assistant Nurse :" Font-Names="Verdana"
                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td style="width: 7%;" colspan="2" align="left">
                                                    <asp:DropDownList ID="ddlAssistant" runat="server" Width="150px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="width: 5%;" align="right">
                                                    <asp:Label ID="Label5" runat="server" Text="Anaesthetist :" Font-Names="Verdana"
                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td style="width: 7%;" colspan="2" align="left">
                                                    <asp:DropDownList ID="ddlAnesthatic" runat="server" Width="150px" AutoPostBack="True"
                                                        onselectedindexchanged="ddlAnesthatic_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr style="width: 100%;">
                                                <td align="right" style="width: 15%;">
                                                    <asp:Label ID="lblSurgeon" runat="server" Text="Surgeon :" Font-Names="Verdana"
                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left" colspan="2">
                                                    <asp:TextBox ID="txtSurgeon" runat="server" MaxLength="50" Font-Names="Verdana"
                                                        Font-Size="11px" Width="150px" ReadOnly="false"></asp:TextBox>
                                                </td>
                                                <td align="right" style="width: 5%;">
                                                    <asp:Label ID="Label7" runat="server" Text="TypeOfAnaesthesia :" Font-Names="Verdana"
                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left" colspan="2">
                                                    <asp:TextBox ID="txtTypeOfAnaesthetist" runat="server" MaxLength="50" Font-Names="Verdana" Font-Size="11px"
                                                        Width="150px" ReadOnly="True"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr style="width: 100%;">
                                                 <td align="right" style="width: 15%;">
                                                    <asp:Label ID="Label6" runat="server" Text="Implant :" Font-Names="Verdana"
                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left" colspan="2">
                                                    <asp:TextBox ID="txtImplant" runat="server" MaxLength="50" Font-Names="Verdana"
                                                        Font-Size="11px" Width="150px" ReadOnly="false"></asp:TextBox>
                                                </td>
                                                <td align="right" style="width: 5%;">
                                                    <asp:Label ID="lblPatientName" runat="server" Text="Patient Name :" Font-Names="Verdana"
                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left" colspan="2">
                                                    <asp:DropDownList ID="ddlPatientName" runat="server" Width="150px" 
                                                    OnSelectedIndexChanged="ddlPatientName_SelectedIndexChanged1" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvDeptDesc" runat="server" ForeColor="Red" ControlToValidate="ddlPatientName"
                                                        Font-Size="13" ValidationGroup="Save" ErrorMessage="*" InitialValue="0" Display="Dynamic"></asp:RequiredFieldValidator>
                                                </td>
                                                
                                            </tr>
                                            <tr style="width: 100%;">
                                                <td align="right" style="width: 15%;">
                                                    <asp:Label ID="lblFloorname" runat="server" Text="Floor Name :" Font-Names="Verdana"
                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left" colspan="2">
                                                    <asp:TextBox ID="txtFloorName" runat="server" MaxLength="50" Font-Names="Verdana"
                                                        Font-Size="11px" Width="150px" ReadOnly="True"></asp:TextBox>
                                                </td>
                                                <td align="right" style="width: 5%;">
                                                    <asp:Label ID="lblRoomNo" runat="server" Text="Room Number :" Font-Names="Verdana"
                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left" colspan="2">
                                                    <asp:TextBox ID="txtRoomNo" runat="server" MaxLength="50" Font-Names="Verdana" Font-Size="11px"
                                                        Width="150px" ReadOnly="True"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr style="width: 100%;">
                                                <td align="right" style="width: 15%;">
                                                    <asp:Label ID="lblAnaestheticNote" runat="server" Text="Anaesthetic Note :" Font-Names="Verdana"
                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left" colspan="2">
                                                    <asp:TextBox ID="txtAnaestheticNote" runat="server" Font-Names="Verdana" Font-Size="11px" TextMode="MultiLine"
                                                        Width="210" Font-Bold="False"></asp:TextBox>
                                                </td>
                                                <td align="right" style="width: 15%;">
                                                    <asp:Label ID="lblSurgeryNote" runat="server" Text="Surgery Note :" Font-Names="Verdana"
                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left" colspan="2">
                                                    <asp:TextBox ID="txtSurgeryNote" runat="server" Font-Names="Verdana" TextMode="MultiLine"
                                                        Font-Size="11px" Width="210px" ReadOnly="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr style="width: 100%;">
                                                <td align="right" style="width: 15%;">
                                                    <asp:Label ID="lblBedNo" runat="server" Text="Bed Number :" Font-Names="Verdana"
                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left" colspan="2">
                                                    <asp:TextBox ID="txtBedNo" runat="server" MaxLength="100" Font-Names="Verdana" Font-Size="11px"
                                                        Width="150px" Font-Bold="False" ReadOnly="True"></asp:TextBox>
                                                </td>
                                                <td align="right" style="width: 15%;">
                                                    <asp:Label ID="lblMaterialHPE" runat="server" Text="Material For H.P.E. :" Font-Names="Verdana"
                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left" colspan="2">
                                                    <asp:TextBox ID="txtMaterialHPE" runat="server" MaxLength="400" Font-Names="Verdana" TextMode="MultiLine"
                                                        Font-Size="11px" Width="210px" ReadOnly="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr style="height: 10px;">
                                                <td align="right" style="width: 15%;">
                                                    <asp:Label ID="lblAllocationTime" runat="server" Text="Allocation Time :" Font-Names="Verdana"
                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtAllocTime" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                        Width="150px" MaxLength="10" Format="dd/MM/yyyy"></asp:TextBox>
                                                    <cc:CalendarExtender ID="CalAllocTime" runat="server" TargetControlID="txtAllocTime"
                                                        Format="dd/MM/yyyy" DaysModeTitleFormat="MMMM" TodaysDateFormat="dd/MM/yyyy">
                                                    </cc:CalendarExtender>
                                                </td>
                                                <td align="left" style="width: 5%;">
                                                    <cc1:TimeSelector ID="StartTimeSelector" runat="server" DisplaySeconds="false" SelectedTimeFormat="TwentyFour">
                                                    </cc1:TimeSelector>
                                                </td>
                                                <td align="right" style="width: 5%;">
                                                    <asp:Label ID="lblDischargeTime" runat="server" Text="Discharge Time :" Font-Names="Verdana"
                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtDischargeTime" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                        Width="150px" MaxLength="10" Format="dd/MM/yyyy"></asp:TextBox>
                                                    <cc:CalendarExtender ID="CalDischargeTime" runat="server" TargetControlID="txtDischargeTime"
                                                        Format="dd/MM/yyyy" DaysModeTitleFormat="MMMM" TodaysDateFormat="dd/MM/yyyy">
                                                    </cc:CalendarExtender>
                                                </td>
                                                <td align="left" style="width: 5%;">
                                                    <cc1:TimeSelector ID="EndTimeSelector" runat="server" DisplaySeconds="false" SelectedTimeFormat="TwentyFour">
                                                    </cc1:TimeSelector>
                                                </td>
                                            </tr>
                                            <tr style="height: 10px;">
                                                <td colspan="6">
                                                </td>
                                            </tr>
                                            <tr align="center" style="width: 100%;">
                                                <td align="center" colspan="6">
                                                    <asp:Button ID="BtnSave" runat="server" Text="Save" Font-Names="Verdana" Font-Size="12px"
                                                        ValidationGroup="Save" BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black" />
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
</asp:Content>
