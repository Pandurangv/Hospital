<%@ Page Title="" Language="C#" MasterPageFile="~/mstAdmin.Master" AutoEventWireup="true" CodeBehind="frmPrescription.aspx.cs" Inherits="Hospital.frmPrescription" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
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
                        <asp:HiddenField ID="TempId" runat="server" />
                        <asp:HiddenField ID="Myflag" runat="server" />
                        <asp:HiddenField ID="PrescriptionId" runat="server" />
                        <asp:HiddenField ID="Prescript" runat="server" />
                        <asp:HiddenField ID="invDtl" runat="server" />
                        <table width="100%">
                            <tr>
                                <td align="center">
                                    <asp:Label ID="Label1" runat="server" Text="Prescription Master" Font-Names="Verdana"
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
                                                    OnClick="BtnAddNewPrescription_Click" />
                                            </td>
                                            <td align="center" style="border-left: none; width: 60px;">
                                                <asp:Button ID="btnExcel" runat="server" Text="Excel" Font-Names="Verdana" Font-Size="14px"
                                                    BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black;" OnClick="btnExcel_Click" />
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
                                                <td align="right" class="style1">
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
                                                    <asp:Panel ID="pnl" runat="server" Width="1020" Style="border-color: Green; border-style: solid;
                                                        border-width: 1px">
                                                        <div runat="server" style="float:left;width:220px" id="divPatientList">
                                                            <asp:GridView ID="dgvPatientlist" runat="server" CellPadding="4" ForeColor="#333333"
                                                                GridLines="Both" Font-Names="Verdana" Font-Size="Small" AllowPaging="true" PageSize="20"
                                                                Style="float: left;" AutoGenerateColumns="false" DataKeyNames="AdmitId">
                                                                <FooterStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" />
                                                                <RowStyle BackColor="#E0F0E8" Font-Size="11px" Wrap="False" />
                                                                <PagerStyle BackColor="#3b3535" ForeColor="White" HorizontalAlign="Left" Font-Size="11px" />
                                                                <SelectedRowStyle BackColor="#006600" Font-Bold="True" ForeColor="#333333" />
                                                                <HeaderStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" Font-Size="12px"
                                                                    Wrap="False" />
                                                                <EditRowStyle BackColor="#2461BF" />
                                                                <AlternatingRowStyle BackColor="White" Wrap="False" />
                                                                <Columns>
                                                                    <asp:BoundField DataField="AdmitId" HeaderText="Admit Id." ReadOnly="True" />
                                                                    <asp:BoundField DataField="FullName" HeaderText="Patient Name" ReadOnly="True" />
                                                                    <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton ID="ImageEdit" runat="server" ImageUrl="~/images/edit.jpg" Height="24px"
                                                                                Width="24px" OnClick="BtnAdd_Prescription" /></ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                        <div runat="server" style="float:left;position:relative;width:720px;" id="divPrescription">
                                                            <asp:GridView ID="dgvClaim" runat="server" ForeColor="#333333" GridLines="Both" CellPadding="4"
                                                                Font-Names="Verdana" Font-Size="Small" AllowPaging="true" PageSize="20" Width="720px"
                                                                AutoGenerateColumns="false" DataKeyNames="Prescription_Id"
                                                                OnDataBound="dgvClaim_DataBound" OnPageIndexChanged="dgvClaim_PageIndexChanged"
                                                                OnPageIndexChanging="dgvClaim_PageIndexChanging">
                                                                <FooterStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" />
                                                                <RowStyle BackColor="#E0F0E8" Font-Size="11px" Wrap="False" />
                                                                <PagerStyle BackColor="#3b3535" ForeColor="White" HorizontalAlign="Left" Font-Size="11px" />
                                                                <SelectedRowStyle BackColor="#006600" Font-Bold="True" ForeColor="#333333" />
                                                                <HeaderStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" Font-Size="12px"
                                                                    Wrap="False" />
                                                                <EditRowStyle BackColor="#2461BF" />
                                                                <AlternatingRowStyle BackColor="White" Wrap="False" />
                                                                <Columns>
                                                                    <asp:BoundField DataField="PatientName" HeaderText="Patient Name" ReadOnly="True" HeaderStyle-Width="25%"/>
                                                                    <asp:BoundField DataField="Prescription_Date" HeaderText="Prescription Date" HeaderStyle-Width="25%" ReadOnly="True" DataFormatString="{0:dd/MM/yyyy}" />
                                                                    <asp:BoundField DataField="DeptDoctor" HeaderText="Dept.Doctor" ReadOnly="True" HeaderStyle-Width="20%" SortExpression="DeptDoctor" />
                                                                    <asp:TemplateField HeaderText="Print" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="5%">
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton ID="ImgPrint" runat="server" ImageUrl="~/images/Report.bmp" Height="24px"
                                                                                Width="24px" OnClick="btnPrint_Click" /></ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
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
                                                    <asp:Panel ID="Panel2" runat="server" Width="99.7%" Style="text-align: center; background-color: #E0F0E8;
                                                        height: auto;" BorderColor="#3b3535" BorderStyle="Solid" BorderWidth="1px">
                                                        <table width="100%">
                                                            <tr>
                                                                <td align="center" colspan="4">
                                                                    <asp:Label ID="lblHeading" runat="server" Text="Prescription Master" Font-Names="Verdana"
                                                                        Font-Size="15px" Font-Bold="true" ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4" align="center" style="height: 20px;">
                                                                    <asp:Label ID="lblMsg" Text="" runat="server" ForeColor="Red" Font-Names="Verdana"
                                                                        Font-Size="11px"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 30%;" align="right">
                                                                    <asp:Label ID="Label7" runat="server" Text="Doctors :" Font-Names="Verdana" Font-Size="11px"
                                                                        ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td style="width: 20%;" align="left">
                                                                    <asp:DropDownList ID="ddlDoctors" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                        Width="150px" AutoPostBack="true" 
                                                                        onselectedindexchanged="ddlDoctors_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ForeColor="Red"
                                                                        ControlToValidate="ddlDoctors" Font-Size="11" ValidationGroup="Save" ErrorMessage="*"
                                                                        Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                </td>
                                                                <td align="right">
                                                                    <asp:Label ID="Label9" Text="Followup Date : " runat="server" Font-Names="Verdana"
                                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtFollowupDate" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                        Width="150px" MaxLength="10"></asp:TextBox>
                                                                    <cc:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFollowupDate"
                                                                        Format="dd/MM/yyyy">
                                                                    </cc:CalendarExtender>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="Label2" Text="Patient Name : " runat="server" Font-Names="Verdana"
                                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:DropDownList ID="ddlPatient" runat="server" Width="150px" AutoPostBack="true"
                                                                        OnSelectedIndexChanged="ddlPatient_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red"
                                                                        ControlToValidate="ddlPatient" Font-Size="13" ValidationGroup="Save" ErrorMessage="*"
                                                                        Display="Dynamic" InitialValue="0"></asp:RequiredFieldValidator>
                                                                </td>
                                                                <td align="right">
                                                                    <asp:Label ID="Label4" Text="Prescription Date : " runat="server" Font-Names="Verdana"
                                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtPrescriptionDate" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                        Width="150px" MaxLength="10"></asp:TextBox>
                                                                    <cc:CalendarExtender ID="CalDate" runat="server" TargetControlID="txtPrescriptionDate"
                                                                        Format="dd/MM/yyyy">
                                                                    </cc:CalendarExtender>
                                                                    
                                                                </td>
                                                            </tr>
                                                            
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:CheckBox ID="chkDress" runat="server" AutoPostBack="true" Font-Names="Verdana"
                                                                        Font-Size="11px" ForeColor="#3b3535" Text="Dressing" />
                                                                </td>
                                                                <td align="left">
                                                                    <asp:CheckBox ID="chkInject" runat="server" AutoPostBack="true" Font-Names="Verdana"
                                                                        Font-Size="11px" ForeColor="#3b3535" OnCheckedChanged="chkInject_CheckedChanged"
                                                                        Text="Injection" />
                                                                </td>
                                                                <td align="right">
                                                                    <asp:Label ID="lblInjection" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                        ForeColor="#3b3535" Text="Injection : "></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtInjection" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                        MaxLength="30" Width="150px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="Label17" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                        ForeColor="#3b3535" Text="Language : "></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:DropDownList ID="ddlLanType" runat="server" Width="150px">
                                                                        <asp:ListItem Value="Eng" Text="English"></asp:ListItem>
                                                                        <asp:ListItem Value="Mar" Text="Marathi"></asp:ListItem>
                                                                        <asp:ListItem Value="Guj" Text="Gujrathi"></asp:ListItem>
                                                                        <asp:ListItem Value="Tam" Text="Tamil"></asp:ListItem>
                                                                        <asp:ListItem Value="Tel" Text="Telgu"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td align="right">
                                                                    
                                                                </td>
                                                                <td align="left">
                                                                    
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
                                                                <td style="width: 15%;" align="right">
                                                                    <asp:Label ID="Label8" Text="Medicine Name : " runat="server" Font-Names="Verdana"
                                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td style="width: 15%;" align="left">
                                                                    <%--<asp:DropDownList ID="ddlTablet" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                        Width="154px">
                                                                    </asp:DropDownList>--%>
                                                                    <asp:TextBox ID="txtMedicine" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                        Width="150px"  MaxLength="30"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvDept" runat="server" ForeColor="Red" SetFocusOnError="true"
                                                                        Display="Dynamic" ControlToValidate="txtMedicine" Font-Size="13"
                                                                        ValidationGroup="Add" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                </td>
                                                                <td align="right">
                                                                    <asp:Label ID="Label3" Text="जेवणाआधी : " runat="server" Font-Names="Verdana"
                                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td style="width: 20%;" align="left">
                                                                    <asp:CheckBox runat="server" ID="chkbeforebf"  />
                                                                </td>
                                                            </tr>

                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="Label11" Text="सकाळी : " runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                        ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtMorning" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                        Width="150px"  MaxLength="30"></asp:TextBox>
                                                                </td>
                                                                 <td style="width: 10%;" align="right">
                                                                    <asp:Label ID="lblNoDays" Text="No of Days : " runat="server" Font-Names="Verdana"
                                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td style="width: 30%;" align="left">
                                                                    <asp:TextBox ID="txtNoOfDays" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                        Width="150px"  MaxLength="30"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="lblafter" Text="दुपारी : " runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                        ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtafternoon" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                        Width="150px"  MaxLength="30"></asp:TextBox>
                                                                </td>
                                                                <td align="right">
                                                                    <asp:Label ID="Label12" Text="Quantity : " runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                        ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                     <asp:TextBox ID="txtQuantity" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                        Width="150px"  MaxLength="30"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="lblNight" Text="संध्याकाळी : " runat="server" Font-Names="Verdana"
                                                                        Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                                </td>
                                                                <td align="left">
                                                                   <asp:TextBox ID="txtNight" runat="server" Font-Names="Verdana" Font-Size="11px" Width="150px"
                                                                        MaxLength="30"></asp:TextBox>
                                                                </td>
                                                                <td align="right">
                                                                    
                                                                </td>
                                                                <td align="left">
                                                                    
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="lblOther" Text="Other Charges :" runat="server" Font-Names="Verdana" Font-Size="11px" ForeColor="#3b3535" />
                                                                </td>
                                                                <td align="left">
                                                                   <asp:DropDownList ID="ddlOther" Width="153px" runat="server" AutoPostBack="True"
                                                                        OnSelectedIndexChanged="ddlOther_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td align="right">
                                                                    <asp:Label ID="Label6" Text="Charges Per Day :" runat="server" Font-Names="Verdana" Font-Size="11px" ForeColor="#3b3535" />
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtChargePerDay" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                        onblur="javascript:return CalculateCharges();" Width="150px" MaxLength="10"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" colspan="2">
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ForeColor="Red" SetFocusOnError="true"
                                                                        Display="Dynamic" InitialValue="0" ControlToValidate="ddlOther" Font-Size="13"
                                                                        ValidationGroup="Add" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                </td>
                                                                <td align="center" colspan="2">
                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" ValidationExpression="^[0-9]{1,11}(?:\.[0-9]{1,3})?$"
                                                                    ErrorMessage="Please Enter Only Number" Font-Bold="False" Font-Size="11px" ForeColor="Red"
                                                                    ControlToValidate="txtChargePerDay" runat="server" ValidationGroup="Add" Display="Dynamic"
                                                                    Font-Names="verdana" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="Label13" Text="Quantity :" runat="server" Font-Names="Verdana" Font-Size="11px" ForeColor="#3b3535" />
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtQty" runat="server" Font-Names="Verdana" Font-Size="11px" onblur="javascript:return CalculateCharges();"
                                                                        Width="150px" MaxLength="10"></asp:TextBox>
                                                                </td>
                                                                <td align="right">
                                                                    <asp:Label ID="Label14" Text="No Of Days :" runat="server" Font-Names="Verdana" Font-Size="11px" ForeColor="#3b3535" />
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtNoDays" runat="server" Font-Names="Verdana" Font-Size="11px" onblur="javascript:return CalculateCharges();"
                                                                        Width="150px" MaxLength="10"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center" colspan="2">
                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationExpression="^[0-9]{1,11}(?:\.[0-9]{1,3})?$"
                                                                        ErrorMessage="Please Enter Only Number" Font-Bold="False" Font-Size="11px" ForeColor="Red"
                                                                        ControlToValidate="txtQty" runat="server" ValidationGroup="Add" Display="Dynamic"
                                                                        Font-Names="verdana" />
                                                                </td>
                                                                <td align="center" colspan="2">
                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" ValidationExpression="^[0-9]{1,11}(?:\.[0-9]{1,3})?$"
                                                                        ErrorMessage="Please Enter Only Number" Font-Bold="False" Font-Size="11px" ForeColor="Red"
                                                                        ControlToValidate="txtNoDays" runat="server" ValidationGroup="Add" Display="Dynamic"
                                                                        Font-Names="verdana" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:Label ID="Label15" Text="Total Charges :" runat="server" Font-Names="Verdana" Font-Size="11px" ForeColor="#3b3535" />
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtAmount" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                        Width="150px" MaxLength="10"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ForeColor="Red"
                                                                        ControlToValidate="txtAmount" Font-Size="13" ValidationGroup="Add" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                </td>
                                                                <td align="right">
                                                                    
                                                                </td>
                                                                <td  align="left">
                                                                    
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
                                                    <asp:Panel ID="Panel1" runat="server" ScrollBars="Both" Width="99.7%" Style="border-color: #3b3535;
                                                        border-style: solid; border-width: 1px">
                                                        <asp:GridView ID="dgvChargeDetails" runat="server" CellPadding="4" ForeColor="#333333"
                                                            GridLines="Both" Font-Names="Verdana" Width="100%" Font-Size="Small" AllowPaging="false"
                                                            AutoGenerateColumns="false" DataKeyNames="TempId" OnSelectedIndexChanged="dgvChargeDetails_SelectedIndexChanged">
                                                            <FooterStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" />
                                                            <RowStyle BackColor="#E0F0E8" Font-Size="11px" Wrap="False" />
                                                            <PagerStyle BackColor="#3b3535" ForeColor="White" HorizontalAlign="Left" Font-Size="11px" />
                                                            <SelectedRowStyle BackColor="#006600" Font-Bold="True" ForeColor="#333333" />
                                                            <HeaderStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" Font-Size="12px"
                                                                Wrap="False" />
                                                            <EditRowStyle BackColor="#2461BF" />
                                                            <AlternatingRowStyle BackColor="White" Wrap="False" />
                                                            <Columns>
                                                                <asp:BoundField DataField="ProductName" HeaderText="Medicine Name" ReadOnly="True"
                                                                    SortExpression="ProductName" />
                                                                <asp:BoundField DataField="Morning" HeaderText="Morning" ReadOnly="True"  />
                                                                <asp:BoundField DataField="Afternoon" HeaderText="Afternoon" ReadOnly="True"  />
                                                                <asp:BoundField DataField="Night" HeaderText="Night" ReadOnly="True"  />
                                                                <asp:BoundField DataField="NoOfDays" HeaderText="No Of Days" ReadOnly="True"  />
                                                                <asp:BoundField DataField="Quantity" HeaderText="Quantity" ReadOnly="True"  />
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
                                                        <asp:GridView ID="dgvChargesOPD" runat="server" CellPadding="4" 
                                                            ForeColor="#333333" GridLines="Both"
                                                            Font-Names="Verdana" Width="100%" Font-Size="Small" AutoGenerateColumns="false"
                                                            DataKeyNames="TempId">
                                                            <FooterStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" />
                                                            <RowStyle BackColor="#E0F0E8" Font-Size="11px" Wrap="False" />
                                                            <PagerStyle BackColor="#3b3535" ForeColor="White" HorizontalAlign="Left" Font-Size="11px" />
                                                            <SelectedRowStyle BackColor="#006600" Font-Bold="True" ForeColor="#333333" />
                                                            <HeaderStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" Font-Size="12px"
                                                                Wrap="False" />
                                                            <EditRowStyle BackColor="#2461BF" />
                                                            <AlternatingRowStyle BackColor="White" Wrap="False" />
                                                            <Columns>
                                                                <asp:BoundField DataField="OtherChargesId" HeaderText="Charge Id" ReadOnly="True" />
                                                                <asp:BoundField DataField="ChargesName" HeaderText="Particulars" ReadOnly="True" />
                                                                <asp:BoundField DataField="Quantity" HeaderText="Quantity" ReadOnly="True" />
                                                                <asp:BoundField DataField="NoOfDays" HeaderText="No Of Days" ReadOnly="True" />
                                                                <asp:BoundField DataField="PerDayCharge" HeaderText="Per Day Charge" ReadOnly="True" />
                                                                <asp:BoundField DataField="Amount" HeaderText="Charge" ReadOnly="True" />
                                                                <%--<asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="ImageEdit" runat="server" ImageUrl="~/images/edit.jpg" Height="24px"
                                                                            Width="24px"  /></ItemTemplate>
                                                                </asp:TemplateField>--%>
                                                                <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="ImageDelete" runat="server" ImageUrl="~/images/Erase.png" Height="24px"
                                                                            Width="24px" OnClick="btnDeleteCharge_Click" /></ItemTemplate>
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
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="Label10" Text="Discount : " runat="server" Font-Names="Verdana"
                                                                    Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtDiscount" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                    Width="300px" ></asp:TextBox>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Label ID="Label16" Text="Total Amount : " runat="server" Font-Names="Verdana"
                                                                    Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtTotalAmount" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                    Width="300px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="lblInvestigation" Text="Investigations Done : " runat="server" Font-Names="Verdana"
                                                                    Font-Size="11px" ForeColor="#3b3535" Visible="false"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtInvestigation" runat="server" Font-Names="Verdana" Font-Size="11px" TextMode="MultiLine"
                                                                    Width="300px" Height="35px" Visible="false"></asp:TextBox>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Label ID="lblImpression" Text="Impression : " runat="server" Font-Names="Verdana"
                                                                    Font-Size="11px" ForeColor="#3b3535" Visible="false"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtImpression" runat="server" Font-Names="Verdana" Font-Size="11px" TextMode="MultiLine"
                                                                    Width="300px" Height="35px" Visible="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="lblAdvice" Text="Advice Note : " runat="server" Font-Names="Verdana"
                                                                    Font-Size="11px" ForeColor="#3b3535" Visible="false"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtAdviceNote" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                    TextMode="MultiLine" Width="300px" Height="35px" Visible="false"></asp:TextBox>
                                                            </td>
                                                            <td align="right">
                                                                <asp:Label ID="Label5" Text="Instructions: " runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                    ForeColor="#3b3535"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtRemarks" runat="server" Font-Names="Verdana" Font-Size="11px" Width="300px"
                                                                    TextMode="MultiLine" Height="35px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
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
