<%@ Page Title="" Language="C#" MasterPageFile="~/mstAdmin.Master" AutoEventWireup="true" CodeBehind="frmOPDPatient.aspx.cs" Inherits="Hospital.frmOPDPatient" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" language="javascript">
        function CalAge() {
            var txtBirthDate = document.getElementById("<%=txtBirthDate.ClientID %>").value;
            if (txtBirthDate != null && txtBirthDate != "") {
                var dt = new Date(txtBirthDate);
                var NowDate = new Date();
                document.getElementById("<%=txtAge.ClientID %>").value = NowDate.getFullYear() - dt.getFullYear();
            }
            else {
                document.getElementById("<%=txtAge.ClientID %>").value = 0;
            }
        }
    </script>
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
    <div style="width: 99%">
        <table>
            <tr>
                <td align="center">
                    <asp:Panel ID="pnlGrid" runat="server" Width="1030px" Style="text-align: center;
                        background-color: #E0F0E8; height: 930px;">
                        <asp:HiddenField ID="PatientId" runat="server" />
                        <table width="100%" style="border-color: Green; border-style: solid; border-width: 2px"
                            cellpadding="0">
                            <tr>
                                <td>
                                    <asp:Panel ID="Panel2" runat="server" Width="99.7%" Style="text-align: center; background-color: #E0F0E8;
                                        height: 920px;" BorderColor="Green" BorderStyle="Solid" BorderWidth="1px">
                                        <table width="100%">
                                            <tr>
                                                <td align="center" colspan="3">
                                                    <asp:Label ID="Label2" runat="server" Text="Patient Registration" Font-Names="Verdana"
                                                        Font-Size="16px" Font-Bold="true" ForeColor="#3b3535"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" align="center">
                                                    <asp:Label ID="lblMsg" runat="server" Text="" Font-Names="Verdana" Font-Size="11px"
                                                        ForeColor="Red"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" align="left" style="border-top-style: solid; border-top-color: Green;
                                                    border-top-width: 1px; border-bottom-color: Green; border-bottom-style: solid;
                                                    border-bottom-width: 1px">
                                                    <asp:Label ID="Label9" runat="server" Text="Photo" Font-Names="Verdana" Font-Size="11px"
                                                        Font-Bold="true" ForeColor="#3b3535"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" align="left" style="padding-left: 100px;">
                                                    <asp:Panel ID="Panel1" runat="server" Height="100px" Width="100px" BorderColor="#3b3535"
                                                        BorderStyle="Solid" BorderWidth="2px">
                                                        <asp:Image ID="imgPhoto" runat="server" Width="100px" Height="100px" />
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="Label7" runat="server" Text="Select Photo :" Font-Names="Verdana"
                                                        Font-Size="11px" Font-Bold="true" ForeColor="#3b3535"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:FileUpload ID="imgUpload" runat="server" Font-Bold="true" Font-Names="Verdana"
                                                        Font-Size="11px" />
                                                </td>
                                                <td align="left">
                                                    <asp:Button ID="BtnimgUpload" runat="server" Text="Upload Image" Font-Names="Verdana"
                                                        Font-Size="12px" BackColor="#3b3535" ForeColor="White" Width="100px" Style="border: 1px solid black"
                                                        OnClick="BtnimgUpload_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3">
                                                    <asp:UpdatePanel runat="server" ID="UpnlPatient">
                                                        <ContentTemplate>
                                                            <asp:Panel ID="Panel3" runat="server" Width="100%" Style="text-align: center; background-color: #E0F0E8;">
                                                                <table width="100%">
                                                                    <tr>
                                                                        <td colspan="6" align="left" style="border-top-style: solid; border-top-color: Green;
                                                                            border-top-width: 1px; border-bottom-color: Green; border-bottom-style: solid;
                                                                            border-bottom-width: 1px">
                                                                            <asp:Label ID="Label13" runat="server" Text="Patient Detail" Font-Names="Verdana"
                                                                                Font-Size="11px" Font-Bold="true" ForeColor="#3b3535"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                        </td>
                                                                        <td></td>
                                                                        <td align="right">
                                                                            <asp:RadioButton Text="OPD" runat="server" ID="rbtnOPD" Font-Names="Verdana" Font-Size="15px"
                                                                                ForeColor="#3b3535" GroupName="PatientType" AutoPostBack="true" OnCheckedChanged="OPD_CheckedChanged"/>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:RadioButton Text="IPD" runat="server" ID="rbtnIPD" Font-Names="Verdana" Font-Size="15px"
                                                                                ForeColor="#3b3535" GroupName="PatientType" AutoPostBack="true" OnCheckedChanged="IPD_CheckedChanged"/>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right">
                                                                            <asp:Label ID="lblPatientCode" runat="server" Text="MRN :" CssClass="lablestyle"></asp:Label>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="txtPatientCode" runat="server" MaxLength="50" ReadOnly="true" CssClass="textStyle" Font-Bold="true"></asp:TextBox>
                                                                        </td>
                                                                        <td align="right">
                                                                            <asp:Label ID="Label14" runat="server" Text="Admission Date :" CssClass="lablestyle"></asp:Label>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="txtAdmitDate" runat="server" CssClass="textStyle"></asp:TextBox>
                                                                            <cc:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtAdmitDate"
                                                                                Format="dd/MM/yyyy">
                                                                            </cc:CalendarExtender>
                                                                        </td>
                                                                        <td align="right">
                                                                            <asp:Label ID="Label15" runat="server" Text="Admission Time :" CssClass="lablestyle"></asp:Label>
                                                                        </td>
                                                                        <td align="left">
                                                                            <cc1:TimeSelector ID="AdmissionTimeSelector" runat="server" DisplaySeconds="false"
                                                                                SelectedTimeFormat="Twelve">
                                                                            </cc1:TimeSelector>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="5">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right">
                                                                            <%--<asp:Label ID="lblIPDNo" runat="server" Text="IPD No :" CssClass="lablestyle"></asp:Label>--%>
                                                                        </td>
                                                                        <td align="left">
                                                                            <%--<asp:TextBox ID="txtIPDNo" runat="server" MaxLength="50" CssClass="textStyle" Font-Bold="true"></asp:TextBox>--%>
                                                                        </td>
                                                                        <td align="right">
                                                                            <asp:Label ID="Label12" runat="server" Text="Dept. Category :" CssClass="lablestyle"></asp:Label>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:DropDownList ID="ddlDeptCategory" runat="server" CssClass="dropdownstyle"
                                                                            AutoPostBack="true" OnSelectedIndexChanged="ddlDeptCategory_SelectedIndexChanged" >
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ForeColor="Red"
                                                                                ControlToValidate="ddlDeptCategory" Font-Size="11" ValidationGroup="Save" InitialValue="0"
                                                                                ErrorMessage="*" Display="Dynamic" SetFocusOnError="true">
                                                                            </asp:RequiredFieldValidator>
                                                                        </td>
                                                                        <td align="right">
                                                                            <asp:Label ID="Label20" runat="server" Text="Incharge Doctor :" CssClass="lablestyle"></asp:Label>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:DropDownList ID="ddlDeptDoctor" runat="server" CssClass="dropdownstyle">
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ForeColor="Red"
                                                                                ControlToValidate="ddlDeptDoctor" Font-Size="11" ValidationGroup="Save" InitialValue="0"
                                                                                ErrorMessage="*" Display="Dynamic" SetFocusOnError="true">
                                                                            </asp:RequiredFieldValidator>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="5">
                                                                            
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right">
                                                                            <asp:Label ID="lblFirstName" runat="server" Text="Patient First Name :" CssClass="lablestyle"></asp:Label>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:DropDownList ID="ddlInitials" runat="server" CssClass="dropdownstyle">
                                                                            </asp:DropDownList>
                                                                            <asp:TextBox ID="txtFirstName" runat="server" MaxLength="50" CssClass="textStyle"  style="text-transform:capitalize;"
                                                                                ToolTip="Enter Patient First Name" Font-Bold="true"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ForeColor="Red" ControlToValidate="txtFirstName"
                                                                                Font-Size="11" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                                                                        </td>
                                                                        <td align="right">
                                                                            <asp:Label ID="lblMiddleName" runat="server" Text="Middle Name :" CssClass="lablestyle"></asp:Label>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="txtMidleName" runat="server" MaxLength="50" CssClass="textStyle" style="text-transform:capitalize;"
                                                                                ToolTip="Enter Patient Middle Name" Font-Bold="true"></asp:TextBox>
                                                                        </td>
                                                                        <td align="right">
                                                                            <asp:Label ID="lblLastName" runat="server" Text="Last Name :" CssClass="lablestyle"></asp:Label>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="txtLastName" runat="server" MaxLength="50" CssClass="textStyle" style="text-transform:capitalize;"
                                                                                ToolTip="Enter Patient Last Name" Font-Bold="true"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red"
                                                                                ControlToValidate="txtLastName" Font-Size="11" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                                                                        </td>
                                                                    </tr>
                                                                    <tr style="height: 9px;">
                                                                        <td>
                                                                        </td>
                                                                        <td>
                                                                            <asp:RegularExpressionValidator ID="revTxtFirstName" ValidationExpression="[a-zA-Z ]*$"
                                                                                ErrorMessage="Please Enter Only Character" Font-Bold="False" Font-Size="11px"
                                                                                ForeColor="Red" ControlToValidate="txtFirstName" runat="server" ValidationGroup="Save"
                                                                                Display="Dynamic" Font-Names="verdana" />
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationExpression="[a-zA-Z ]*$"
                                                                                ErrorMessage="Please Enter Only Character" Font-Bold="False" Font-Size="11px"
                                                                                ForeColor="Red" ControlToValidate="txtMidleName" runat="server" ValidationGroup="Save"
                                                                                Display="Dynamic" Font-Names="Verdana" />
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationExpression="[a-zA-Z ]*$"
                                                                                ErrorMessage="Please Enter Only Character" Font-Bold="False" Font-Size="11px"
                                                                                ForeColor="Red" ControlToValidate="txtLastName" runat="server" ValidationGroup="Save"
                                                                                Display="Dynamic" Font-Names="verdana" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right">
                                                                            <asp:Label ID="Label1" runat="server" Text="Gender :" CssClass="lablestyle"></asp:Label>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:DropDownList ID="ddlGender" runat="server" CssClass="dropdownstyle">
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ForeColor="Red"
                                                                                ControlToValidate="ddlGender" Font-Size="11" ValidationGroup="Save" InitialValue="0"
                                                                                ErrorMessage="*" Display="Dynamic" SetFocusOnError="true">
                                                                            </asp:RequiredFieldValidator>
                                                                        </td>
                                                                        <td align="right">
                                                                            <asp:Label ID="Label28" runat="server" CssClass="lablestyle"  Text="Birth Date :"></asp:Label>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="txtBirthDate" runat="server" MaxLength="10" CssClass="textStyle" onblur="javascript:return CalAge();"></asp:TextBox>
                                                                            <cc:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtBirthDate">
                                                                            </cc:CalendarExtender>
                                                                        </td>
                                                                        <td align="right">
                                                                            <asp:Label ID="Label29" runat="server" CssClass="lablestyle"  Text="Age In Years :"></asp:Label>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="txtAge" runat="server" Font-Names="Verdana" Font-Size="11px" MaxLength="2"
                                                                                onkeypress="return isNumber(event);" Width="60px"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="txtAge"
                                                                                Font-Size="11" ForeColor="Red" ValidationGroup="Save" ErrorMessage="*" Display="Dynamic"
                                                                                SetFocusOnError="true">
                                                                            </asp:RequiredFieldValidator>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td align="left">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td colspan="2" align="right">
                                                                            
                                                                        </td>
                                                                        <td align="right">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td align="left">
                                                                            &nbsp;
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="5">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right">
                                                                            <asp:Label ID="lblCotact" runat="server" Text="Contact No :" CssClass="lablestyle"></asp:Label>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="txtContactNo" runat="server" MaxLength="20" CssClass="textStyle"></asp:TextBox>
                                                                        </td>
                                                                        <td align="right">
                                                                            <asp:Label ID="lblbp" runat="server" Text="BP :" CssClass="lablestyle"></asp:Label>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="txtBP" runat="server" MaxLength="20" CssClass="textStyle"></asp:TextBox>
                                                                        </td>
                                                                        <td align="right">
                                                                            <asp:Label ID="lblweight" runat="server" Text="Weight :" CssClass="lablestyle"></asp:Label>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="txtWeight" runat="server" CssClass="textStyle"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr style="height:15px">
                                                                        <td colspan="5">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right">
                                                                            <asp:Label ID="lblPatientType" runat="server" Text="Patient Type :" CssClass="lablestyle"></asp:Label>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:DropDownList ID="ddlPatientType" runat="server" CssClass="dropdownstyle">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right">
                                                                            <asp:Label ID="lblref" runat="server" Text="Refered By :" CssClass="lablestyle"></asp:Label>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="txtRefDoctor" runat="server" CssClass="textStyle" style="text-transform:capitalize;"></asp:TextBox>
                                                                        </td>
                                                                        <td align="right">
                                                                            <asp:Label ID="lblblood" runat="server" Text="Blood Group :" Font-Names="Verdana" Font-Size="11px"
                                                                                ForeColor="#3b3535"></asp:Label>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:DropDownList ID="ddlBloodGroup" runat="server" CssClass="dropdownstyle">
                                                                                <asp:ListItem Text="--Select--" Value="0" Selected="True"></asp:ListItem>
                                                                                <asp:ListItem Text="O+" Value="O+"></asp:ListItem>
                                                                                <asp:ListItem Text="O-" Value="O-"></asp:ListItem>
                                                                                <asp:ListItem Text="A+" Value="A+"></asp:ListItem>
                                                                                <asp:ListItem Text="A-" Value="A-"></asp:ListItem>
                                                                                <asp:ListItem Text="B+" Value="B+"></asp:ListItem>
                                                                                <asp:ListItem Text="B-" Value="B-"></asp:ListItem>
                                                                                <asp:ListItem Text="AB+" Value="AB+"></asp:ListItem>
                                                                                <asp:ListItem Text="AB-" Value="AB-"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td align="right">
                                                                            <asp:Label ID="lblDiagnosys" runat="server" Text="Diagnosis :" Font-Names="Verdana" Font-Size="11px"
                                                                                ForeColor="#3b3535"></asp:Label>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="txtDignosys" runat="server" style="text-transform:capitalize;"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right">
                                                                            
                                                                        </td>
                                                                        <td align="left">
                                                                            
                                                                        </td>
                                                                        <td align="right">
                                                                            
                                                                        </td>
                                                                        <td align="left">
                                                                            
                                                                        </td>
                                                                        <td align="right">
                                                                            <asp:Label ID="lbloccupation" runat="server" Text="Occupation :" CssClass="lablestyle"></asp:Label>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:DropDownList ID="ddlOccupation" runat="server" CssClass="dropdownstyle">
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ForeColor="Red"
                                                                                ControlToValidate="ddlOccupation" Font-Size="11" ValidationGroup="Save" ErrorMessage="*"
                                                                                Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                        </td>
                                                                        
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right">
                                                                            <asp:Label ID="lbladdress" runat="server" Text="Address :" CssClass="lablestyle"></asp:Label>
                                                                        </td>
                                                                        <td align="left" colspan="3">
                                                                            <asp:TextBox ID="txtAddress" runat="server" MaxLength="100" TextMode="MultiLine" style="text-transform:capitalize;"
                                                                                Width="470px" Font-Names="Verdana" Font-Size="11px"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ForeColor="Red"
                                                                                ControlToValidate="txtAddress" Font-Size="11" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                                                                        </td>
                                                                        
                                                                    </tr>
                                                                    
                                                                    <tr>
                                                                        <td align="right">
                                                                            <asp:Label ID="lblcity" runat="server" Text="City :" CssClass="lablestyle"></asp:Label>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="txtCity" runat="server" Font-Names="Verdana" Font-Size="11px" style="text-transform:capitalize;"></asp:TextBox>
                                                                            
                                                                        </td>
                                                                        <td align="right">
                                                                            <asp:Label ID="lblstate" runat="server" Text="State :" CssClass="lablestyle"></asp:Label>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="txtState" runat="server" CssClass="textStyle" style="text-transform:capitalize;"></asp:TextBox>
                                                                            
                                                                        </td>
                                                                        <td align="right">
                                                                            <asp:Label ID="lblcountry" runat="server" Text="Country :" CssClass="lablestyle"></asp:Label>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="txtCountry" runat="server" CssClass="textStyle" style="text-transform:capitalize;"></asp:TextBox>
                                                                            
                                                                        </td>
                                                                    </tr>
                                                                    <tr style="height: 9px;">
                                                                        <td>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationExpression="[a-zA-Z ]*$"
                                                                                ErrorMessage="Please Enter Only Character" Font-Bold="False" Font-Size="11px"
                                                                                ForeColor="Red" ControlToValidate="txtCity" runat="server" ValidationGroup="Save"
                                                                                Display="Dynamic" Font-Names="verdana" />
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ValidationExpression="[a-zA-Z ]*$"
                                                                                ErrorMessage="Please Enter Only Character" Font-Bold="False" Font-Size="11px"
                                                                                ForeColor="Red" ControlToValidate="txtState" runat="server" ValidationGroup="Save"
                                                                                Display="Dynamic" Font-Names="verdana" />
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" ValidationExpression="[a-zA-Z ]*$"
                                                                                ErrorMessage="Please Enter Only Character" Font-Bold="False" Font-Size="11px"
                                                                                ForeColor="Red" ControlToValidate="txtCountry" runat="server" ValidationGroup="Save"
                                                                                Display="Dynamic" Font-Names="verdana" />
                                                                        </td>
                                                                    </tr>
                                                                    
                                                                    <tr>
                                                                        <td colspan="6" align="left" style="border-top-style: solid; border-top-color: Green;
                                                                            border-top-width: 1px; border-bottom-color: Green; border-bottom-style: solid;
                                                                            border-bottom-width: 1px">
                                                                            <asp:Label ID="Label18" runat="server" Text="Company Detail" CssClass="lablestyle"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="6" align="left">
                                                                            <asp:CheckBox ID="chkCom" runat="server" Text="Company Patient" Font-Names="Verdana"
                                                                                Font-Size="11px" ForeColor="#3b3535" AutoPostBack="true" OnCheckedChanged="chkCom_CheckedChanged" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right">
                                                                            <asp:Label ID="lblName" runat="server" Text="Company Name :" CssClass="lablestyle" style="text-transform:capitalize;"></asp:Label>
                                                                        </td>
                                                                        <td align="left" colspan="5">
                                                                            <asp:DropDownList ID="ddlCompName" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                                Width="250px">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="6" align="left" style="border-top-style: solid; border-top-color: Green;
                                                                            border-top-width: 1px; border-bottom-color: Green; border-bottom-style: solid;
                                                                            border-bottom-width: 1px">
                                                                            <asp:Label ID="Label24" runat="server" Text="Insurance Detail" CssClass="lablestyle" Font-Bold="true"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="left" colspan="6">
                                                                            <asp:CheckBox ID="ChkInsurance" runat="server" Text="Insurance Patient" Font-Names="Verdana"
                                                                                Font-Size="11px" ForeColor="#3b3535" OnCheckedChanged="ChkInsurance_CheckedChanged"
                                                                                AutoPostBack="true" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right">
                                                                            <asp:Label ID="Label25" runat="server" Text="Insurance Company :" CssClass="lablestyle"></asp:Label>
                                                                        </td>
                                                                        <td align="left" colspan="5">
                                                                            <asp:DropDownList ID="ddlInsurance" runat="server" Font-Names="Verdana" Font-Size="11px">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right">
                                                                            <asp:Label ID="Label26" runat="server" Text="Insurance Proof :" CssClass="lablestyle"></asp:Label>
                                                                        </td>
                                                                        <td align="left" colspan="5">
                                                                            <asp:FileUpload ID="fileInsurance" runat="server" Width="600px" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right">
                                                                            <asp:Label ID="Label27" runat="server" Text="Id Proof :" CssClass="lablestyle"></asp:Label>
                                                                        </td>
                                                                        <td align="left" colspan="5">
                                                                            <asp:FileUpload ID="fileId" runat="server" Width="600px" />
                                                                        </td>
                                                                    </tr>

                                                                    <tr>
                                                                        <td align="right">
                                                                            <asp:Label ID="Label3" runat="server" Text="Prov.Diag :" CssClass="lablestyle"
                                                                                            Style="text-transform: capitalize;"></asp:Label>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="txtProvDiag" runat="server" CssClass="textStyle" style="text-transform:capitalize;"></asp:TextBox>
                                                                        </td>
                                                                        <td align="right">
                                                                            <asp:Label ID="Label4" runat="server" Text="Final Diag :" CssClass="lablestyle"
                                                                                            Style="text-transform: capitalize;"></asp:Label>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="txtFinalDiag" runat="server" CssClass="textStyle" style="text-transform:capitalize;"></asp:TextBox>
                                                                        </td>
                                                                        <td align="right">
                                                                            <asp:Label ID="Label5" runat="server" Text="Symptomes of Present illness :" CssClass="lablestyle"
                                                                                            Style="text-transform: capitalize;"></asp:Label>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="txtSymptoms" runat="server" CssClass="textStyle" style="text-transform:capitalize;"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right">
                                                                            <asp:Label ID="Label6" runat="server" Text="Ailergies :" CssClass="lablestyle"
                                                                                            Style="text-transform: capitalize;"></asp:Label>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="txtAilergies" runat="server" CssClass="textStyle" style="text-transform:capitalize;"></asp:TextBox>
                                                                        </td>
                                                                        <td align="right">
                                                                             <asp:Label ID="Label8" runat="server" Text="Past Illness :" CssClass="lablestyle"
                                                                                            Style="text-transform: capitalize;"></asp:Label>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="txtPastIllness" runat="server" CssClass="textStyle" style="text-transform:capitalize;"></asp:TextBox>
                                                                        </td>
                                                                        <td align="right">
                                                                           <asp:Label ID="Label10" runat="server" Text="Temperature :" CssClass="lablestyle"
                                                                                            Style="text-transform: capitalize;"></asp:Label>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="txtTemperature" runat="server" CssClass="textStyle" style="text-transform:capitalize;"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right">
                                                                            <asp:Label ID="Label11" runat="server" Text="Pulse :" CssClass="lablestyle"
                                                                                            Style="text-transform: capitalize;"></asp:Label>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="txtPulse" runat="server" CssClass="textStyle" style="text-transform:capitalize;"></asp:TextBox>
                                                                        </td>
                                                                        <td align="right">
                                                                            <asp:Label ID="Label16" runat="server" Text="Respiration :" CssClass="lablestyle"
                                                                                            Style="text-transform: capitalize;"></asp:Label>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="txtRespiration" runat="server" CssClass="textStyle" style="text-transform:capitalize;"></asp:TextBox>
                                                                        </td>
                                                                        <td align="right">
                                                                           <asp:Label ID="Label17" runat="server" Text="Others :" CssClass="lablestyle"
                                                                                            Style="text-transform: capitalize;"></asp:Label>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="txtOthers" runat="server" CssClass="textStyle" style="text-transform:capitalize;"></asp:TextBox>
                                                                        </td>
                                                                    </tr>

                                                                    <tr>
                                                                        <td align="right">
                                                                            <asp:Label ID="Label19" runat="server" Text="R. S. :" CssClass="lablestyle"
																								Style="text-transform: capitalize;"></asp:Label>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="txtRS" runat="server" CssClass="textStyle" style="text-transform:capitalize;"></asp:TextBox>
                                                                        </td>
                                                                        <td align="right">
                                                                            <asp:Label ID="Label21" runat="server" Text="C.V.S. :" CssClass="lablestyle"
                                                                                            Style="text-transform: capitalize;"></asp:Label>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="txtCVS" runat="server" CssClass="textStyle" style="text-transform:capitalize;"></asp:TextBox>
                                                                        </td>
                                                                        <td align="right">
                                                                           <asp:Label ID="Label22" runat="server" Text="P.A. :" CssClass="lablestyle"
                                                                                            Style="text-transform: capitalize;"></asp:Label>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="txtPA" runat="server" CssClass="textStyle" style="text-transform:capitalize;"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right">
                                                                            <asp:Label ID="Label23" runat="server" Text="C.N.S. :" CssClass="lablestyle"
                                                                                            Style="text-transform: capitalize;"></asp:Label>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="txtCNS" runat="server" CssClass="textStyle" style="text-transform:capitalize;"></asp:TextBox>
                                                                        </td>
                                                                        <td align="right">
                                                                            <asp:Label ID="Label30" runat="server" Text="OB/GY :" CssClass="lablestyle"
                                                                                            Style="text-transform: capitalize;"></asp:Label>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="txtOBGY" runat="server" CssClass="textStyle" style="text-transform:capitalize;"></asp:TextBox>
                                                                        </td>
                                                                        <td align="right">
                                                                            <%--<asp:Label ID="Label31" runat="server" Text="Lab Investigations :" CssClass="lablestyle"
                                                                                            Style="text-transform: capitalize;"></asp:Label>--%>
                                                                        </td>
                                                                        <td align="left">
                                                                            <%--<asp:TextBox ID="txtLabI" runat="server" CssClass="textStyle" style="text-transform:capitalize;"></asp:TextBox>--%>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right">
                                                                            <asp:Label ID="Label32" runat="server" Text="X-Ray :" CssClass="lablestyle"
                                                                                            Style="text-transform: capitalize;"></asp:Label>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="txtXRay" runat="server" CssClass="textStyle" style="text-transform:capitalize;"></asp:TextBox>
                                                                        </td>
                                                                        <td align="right">
                                                                            <asp:Label ID="Label33" runat="server" Text="E.C.G :" CssClass="lablestyle"
                                                                                            Style="text-transform: capitalize;"></asp:Label>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="txtECG" runat="server" CssClass="textStyle" style="text-transform:capitalize;"></asp:TextBox>
                                                                        </td>
                                                                        <td align="right">
                                                                            <asp:Label ID="Label34" runat="server" Text="USG :" CssClass="lablestyle"
                                                                                            Style="text-transform: capitalize;"></asp:Label>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="txtUSG" runat="server" CssClass="textStyle" style="text-transform:capitalize;"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td align="center" colspan="6">
                                                    <asp:Button ID="BtnSave" runat="server" Text="Save" Font-Names="Verdana" Font-Size="12px"
                                                        ValidationGroup="Save" BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black;
                                                        height: 20px;" OnClick="BtnSave_Click" />
                                                    <asp:Button ID="btnUpdate" runat="server" Text="Update" Font-Names="Verdana" Font-Size="12px"
                                                        ValidationGroup="Save" BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black;
                                                        height: 20px;" OnClick="btnUpdate_Click" />
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
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </div>
    <input id="hdnPanel" type="hidden" runat="server" value="none" />
</asp:Content>
