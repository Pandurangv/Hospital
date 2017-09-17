<%@ Page Title="" Language="C#" MasterPageFile="~/mstAdmin.Master" AutoEventWireup="true" CodeBehind="frmOPDCheckedPatient.aspx.cs" Inherits="Hospital.frmOPDCheckedPatient" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="Notimoo/mootools-1.2-core.js" type="text/javascript"></script>
    <link href="Notimoo/notimoo-documented-1.1.css" rel="stylesheet" type="text/css" />
    <script src="Notimoo/notimoo-documented-1.1.js" type="text/javascript"></script>
    <link href="Notimoo/notimoo-v1.1.css" rel="stylesheet" type="text/css" />
    <script src="Notimoo/notimoo-v1.1.js" type="text/javascript"></script>
    <link href="Style/AdminStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">

        function show_alert(msg) {
            var show1 = new Notimoo({ locationVType: 'bottom', locationHType: 'right' });
            show1.show({ message: msg, sticky: false, visibleTime: 5000, width: 200 });
        }
    </script>
    <script type="text/javascript">
        function hideModalPopup() {
            var modalPopupBehavior = $find('programmaticModalPopupBehavior');
            modalPopupBehavior.hide();
            return false;
        }


        function KeyValid(e) {
            var keynum
            var keychar
            var numcheck
            // For Internet Explorer
            if (window.event) {
                keynum = e.keyCode
            }
            // For Netscape/Firefox/Opera
            else if (e.which) {
                keynum = e.which
            }
            keychar = String.fromCharCode(keynum)

            //List of special characters you want to restrict
            if (keychar == "!" || keychar == "@" || keychar == "#"
    || keychar == "$" || keychar == "%" || keychar == "^" || keychar == "&"
    || keychar == "*" || keychar == "(" || keychar == ")"
    || keychar == "<" || keychar == ">") {
                return false;
            }
            else {
                return true;
            }
        }

        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 46 || charCode > 57 || charCode == 47)) {
                return false;
            }
            return true;
        }

        var oldgridcolor;
        function SetMouseOver(element) {
            oldgridcolor = element.style.backgroundColor;
            element.style.backgroundColor = '#B2C2D1';

        }
        function SetMouseOut(element) {
            element.style.backgroundColor = oldgridcolor;
            element.style.textDecoration = 'none';
        }

        var oldgridcolor;
        function SetBtnMouseOver(element) {
            oldgridcolor = element.style.backgroundColor;
            element.style.backgroundColor = 'Black';

        }
        function SetBtnMouseOut(element) {
            element.style.backgroundColor = oldgridcolor;
            element.style.textDecoration = 'none';
        }



        function checkDates() {
            var startCtrl = document.getElementById("ContentPlaceHolder1_txtStartDate");
            var endCtrl = document.getElementById("ContentPlaceHolder1_txtEndDate");
            if (!IsDate(startCtrl)) {
                show_alert('<span style=\"color:Green;font-size:13px;font-family:Verdana,Arial;font-weight:bold;\">Invalid Start Date.</span>');
                startCtrl.focus();
                return false;
            }
            if (!IsDate(endCtrl)) {
                show_alert('<span style=\"color:Green;font-size:13px;font-family:Verdana,Arial;font-weight:bold;\">Invalid End Date.</span>');
                endCtrl.focus();
                return false;
            }
            var startDate = startCtrl.value;
            var endDate = endCtrl.value;
            if (startDate != '' && endDate != '') {
                var sParts = startDate.split("/");
                var eParts = endDate.split("/");
                var sDate = new Date(sParts[1] + "/" + sParts[0] + "/" + sParts[2]);
                var eDate = new Date(eParts[1] + "/" + eParts[0] + "/" + eParts[2]);

                if (sDate > eDate) {
                    show_alert('<span style=\"color:Green;font-size:13px;font-family:Verdana,Arial;font-weight:bold;\">Start date cannot be greater than end date..</span>');
                    return false;
                    document.getElementById("ctl00$ContentPlaceHolder1$txtStartDate").focus();
                }
                else {
                    return true;
                }
            }
        }

        function Startvalidkey(e) {
            var dtStartDate = document.getElementById("ContentPlaceHolder1_txtStartDate");
            dtStartDate.onkeydown = function (e) {
                var e = window.event || e
                var keyunicode = e.charCode || e.keyCode
                return (keyunicode <= '<%=Commons.CapA%>' && keyunicode >= '<%=Commons.F1Key%>' || keyunicode == '<%=Commons.Backspace%>' || keyunicode == '<%=Commons.Space%>'
             || keyunicode >= '<%=Commons.Zero%>' && keyunicode <= '<%=Commons.Nine%>' || keyunicode >= '<%=Commons.Grave%>' && keyunicode <= '<%=Commons.Smalli%>'
             || keyunicode == '<%=Commons.InvQueMark%>' || keyunicode == '<%=Commons.HoTab%>' || keyunicode == '<%=Commons.Smallo%>' || keyunicode >= '<%=Commons.Percent%>' && keyunicode <= '<%=Commons.OpenParenth%>' ||
             keyunicode == '<%=Commons.Percent%>') ? true : false
            }
        }

        function Endvalidkey(e) {
            var dtEndDate = document.getElementById("ContentPlaceHolder1_txtEndDate");
            dtEndDate.onkeydown = function (e) {
                var e = window.event || e
                var keyunicode = e.charCode || e.keyCode
                return (keyunicode <= '<%=Commons.CapA%>' && keyunicode >= '<%=Commons.F1Key%>' || keyunicode == '<%=Commons.Backspace%>' || keyunicode == '<%=Commons.Space%>'
             || keyunicode >= '<%=Commons.Zero%>' && keyunicode <= '<%=Commons.Nine%>' || keyunicode >= '<%=Commons.Grave%>' && keyunicode <= '<%=Commons.Smalli%>'
             || keyunicode == '<%=Commons.InvQueMark%>' || keyunicode == '<%=Commons.HoTab%>' || keyunicode == '<%=Commons.Smallo%>' || keyunicode >= '<%=Commons.Percent%>' && keyunicode <= '<%=Commons.OpenParenth%>' ||
             keyunicode == '<%=Commons.Percent%>') ? true : false
            }
        }

   
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%">
        <tr>
            <td align="center">
                <table width="100%">
                    <tr>
                        <td align="center">
                            <asp:Panel ID="pnlShow" Width="1010px" runat="server">
                                <table width="100%" style="overflow: hidden; table-layout: fixed; position: relative;">
                                    <tr>
                                        <td colspan="3" align="center">
                                            <asp:Label ID="Label9" runat="server" Text="OPD Treated Patient List" Font-Names="Verdana"
                                                Font-Size="16px" Font-Bold="true" ForeColor="#3b3535"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="lblPageCount" runat="server" Text="" Font-Names="Verdana" Font-Size="11px"
                                                ForeColor="#3b3535"></asp:Label>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="lblRowCount" runat="server" Text="" Font-Names="Verdana" Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 90%" colspan="3">
                                            <asp:Panel ID="pnl" ScrollBars="Both" runat="server" Width="100%" BorderStyle="Solid"
                                                BorderWidth="1px" BorderColor="Green">
                                                <asp:GridView ID="dgvChekedPatient" runat="server" CellPadding="4" ForeColor="#333333"
                                                    GridLines="Both" Font-Names="Verdana" Width="100%" Font-Size="Small" AllowPaging="true"
                                                    DataKeyNames="PKId" PageSize="20" AutoGenerateColumns="false" BackColor="#FAF0FF"
                                                    OnRowCommand="dgvViewIPDPatient_RowCommand" OnPageIndexChanged="dgvChekedPatient_PageIndexChanged"
                                                    OnPageIndexChanging="dgvChekedPatient_PageIndexChanging" OnRowDataBound="dgvChekedPatient_RowDataBound">
                                                    <FooterStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" />
                                                    <RowStyle BackColor="#EFF3FB" Font-Size="11px" Wrap="False" />
                                                    <PagerStyle BackColor="#3b3535" ForeColor="White" HorizontalAlign="Left" Font-Size="11px" />
                                                    <SelectedRowStyle BackColor="#006600" Font-Bold="True" ForeColor="#333333" />
                                                    <HeaderStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" Font-Size="12px"
                                                        Wrap="False" />
                                                    <EditRowStyle BackColor="#FAF0FF" />
                                                    <AlternatingRowStyle BackColor="#FFFFD9" Wrap="False" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sr. No." ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPKId" runat="server" Text='<%#Eval("PKId") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Patient Id" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkPatientId" runat="server" CausesValidation="false" ForeColor="Maroon"
                                                                    Font-Underline="false" CommandName="OPDPatient" Text='<%#Eval("PatientCode") %>'></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="First Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFullName" runat="server" Text='<%#Eval("PatientName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%-- <asp:BoundField DataField="OPDRoom" HeaderText="OPD Room" ReadOnly="True" SortExpression="OPDRoom"
                                                            ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField DataField="ConsultingDoctor" HeaderText="ConsultingDoctor" ReadOnly="True"
                                                            SortExpression="ConsultingDoctor" />--%>
                                                        <asp:BoundField DataField="VisitType" HeaderText="Visit Type" ReadOnly="True" SortExpression="VisitType" />
                                                        <%--<asp:BoundField DataField="Shift" HeaderText="Shift" ReadOnly="True" SortExpression="Shift" />
                                                        <asp:BoundField DataField="Date" HeaderText="Date" ReadOnly="True" SortExpression="Date"
                                                            DataFormatString="{0:d}" />--%>
                                                    </Columns>
                                                </asp:GridView>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Button runat="server" ID="hiddenTargetControlForModalPopup" Style="display: none" />
                            <cc:ModalPopupExtender runat="server" ID="programmaticModalPopup" BehaviorID="programmaticModalPopupBehavior"
                                TargetControlID="hiddenTargetControlForModalPopup" PopupControlID="Panel1" BackgroundCssClass="modalBackground"
                                DropShadow="true" PopupDragHandleControlID="programmaticPopupDragHandle" RepositionMode="RepositionOnWindowScroll">
                            </cc:ModalPopupExtender>
                            <asp:Panel ID="Panel1" runat="server" Width="1055px" Style="text-align: center; background-color: #E0F0E8;
                                height: 600px; border: 2px Solid Green; display: none;">
                                <table width="100%">
                                    <tr>
                                        <td align="center">
                                            <asp:Panel ID="pnlGrid" runat="server" Width="1045px" Style="text-align: center;
                                                background-color: #E0F0E8; height: 560px;">
                                                <table width="100%" border="1px soild Green" style="border-collapse: collapse;">
                                                    <tr>
                                                        <td colspan="4">
                                                            <asp:Label ID="Label1" runat="server" Text="OPD Treated Patient Details" Font-Bold="true"
                                                                Font-Names="Verdana" Font-Size="16px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="Label19" runat="server" Text="Bill No :" Font-Names="Verdana" Font-Size="11px"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="txtBillNo" runat="server" Text="" Font-Names="Verdana" Font-Size="11px"
                                                                Font-Bold="true"></asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="Label20" runat="server" Text="OPD No :" Font-Names="Verdana" Font-Size="11px"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="txtOPDNo" runat="server" Text="" Font-Names="Verdana" Font-Size="11px"
                                                                Font-Bold="true"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="Label2" runat="server" Text="Patient Code :" Font-Names="Verdana"
                                                                Font-Size="11px"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="txtPatientCode" runat="server" Text="" Font-Names="Verdana" Font-Size="11px"
                                                                Font-Bold="true"></asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="Label4" runat="server" Text="Patient Name :" Font-Names="Verdana"
                                                                Font-Size="11px"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="txtPatientName" runat="server" Text="" Font-Names="Verdana" Font-Size="11px"
                                                                Font-Bold="true"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="Label17" runat="server" Text="Patient Type :" Font-Names="Verdana"
                                                                Font-Size="11px"></asp:Label>
                                                        </td>
                                                        <td colspan="3" align="left">
                                                            <asp:Label ID="lblPatientType" runat="server" Text="" Font-Names="Verdana" Font-Size="11px"
                                                                Font-Bold="true"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="Label3" runat="server" Text="Symptoms :" Font-Names="Verdana" Font-Size="11px"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="txtSymptoms" runat="server" Text="" Font-Names="Verdana" Font-Size="11px"
                                                                Font-Bold="true"></asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="Label6" runat="server" Text="Diagnosis :" Font-Names="Verdana" Font-Size="11px"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="txtDiagnosis" runat="server" Text="" Font-Names="Verdana" Font-Size="11px"
                                                                Font-Bold="true"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="Label5" runat="server" Text="X-Ray Test :" Font-Names="Verdana" Font-Size="11px"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="txtXRayTest" runat="server" Text="" Font-Names="Verdana" Font-Size="11px"
                                                                Font-Bold="true"></asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="Label8" runat="server" Text="Blood Test :" Font-Names="Verdana" Font-Size="11px"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="txtBloodTest" runat="server" Text="" Font-Names="Verdana" Font-Size="11px"
                                                                Font-Bold="true"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="Label7" runat="server" Text="OPD Room :" Font-Names="Verdana" Font-Size="11px"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="txtOPDRoom" runat="server" Text="" Font-Names="Verdana" Font-Size="11px"
                                                                Font-Bold="true"></asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="Label10" runat="server" Text="Consulted By :" Font-Names="Verdana"
                                                                Font-Size="11px"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="txtConsultant" runat="server" Text="" Font-Names="Verdana" Font-Size="11px"
                                                                Font-Bold="true"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            <asp:Panel ID="Panel2" ScrollBars="Both" runat="server" Width="100%" Height="200px">
                                                                <asp:GridView ID="dgvMedicineDetail" runat="server" CellPadding="4" ForeColor="#333333"
                                                                    BorderColor="Green" BorderStyle="Solid" BorderWidth="1px" GridLines="Both" Font-Names="Verdana"
                                                                    Width="100%" Font-Size="Small" AllowPaging="true" PageSize="20" AutoGenerateColumns="false">
                                                                    <FooterStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" />
                                                                    <RowStyle BackColor="#E0F0E8" Font-Size="11px" Wrap="False" />
                                                                    <PagerStyle BackColor="#3b3535" ForeColor="White" HorizontalAlign="Left" Font-Size="11px" />
                                                                    <SelectedRowStyle BackColor="#006600" Font-Bold="True" ForeColor="#333333" />
                                                                    <HeaderStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" Font-Size="12px"
                                                                        Wrap="False" />
                                                                    <EditRowStyle BackColor="#2461BF" />
                                                                    <AlternatingRowStyle BackColor="White" Wrap="False" />
                                                                    <Columns>
                                                                        <asp:BoundField DataField="Medicine" HeaderText="Medicine" ReadOnly="True" SortExpression="Medicine" />
                                                                        <asp:BoundField DataField="Morning" HeaderText="Morning" ReadOnly="True" SortExpression="Morning" />
                                                                        <asp:BoundField DataField="AfterNoon" HeaderText="AfterNoon" ReadOnly="True" SortExpression="AfterNoon" />
                                                                        <asp:BoundField DataField="Night" HeaderText="Night" ReadOnly="True" SortExpression="Night" />
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="Label14" runat="server" Text="Injection Charge :" Font-Names="Verdana"
                                                                Font-Size="11px"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="lblInjectionCharge" runat="server" Text="" Font-Names="Verdana" Font-Size="11px"
                                                                Font-Bold="true"></asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="Label16" runat="server" Text="Consultant Charge :" Font-Names="Verdana"
                                                                Font-Size="11px"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="lblConsultantCharge" runat="server" Text="" Font-Names="Verdana" Font-Size="11px"
                                                                Font-Bold="true"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="Label15" runat="server" Text="Dressing Charge :" Font-Names="Verdana"
                                                                Font-Size="11px"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="lblDressingCharge" runat="server" Text="" Font-Names="Verdana" Font-Size="11px"
                                                                Font-Bold="true"></asp:Label>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="Label18" runat="server" Text="Revisit Charge :" Font-Names="Verdana"
                                                                Font-Size="11px"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="lblRevisitCharge" runat="server" Text="" Font-Names="Verdana" Font-Size="11px"
                                                                Font-Bold="true"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="Label11" runat="server" Text="Total Fees :" Font-Names="Verdana" Font-Size="11px"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtTotalFees" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                ReadOnly="true" Font-Bold="true"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Label ID="Label12" runat="server" Text="Received Fees :" Font-Names="Verdana"
                                                                Font-Size="11px"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtReceived" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                Font-Bold="true" OnTextChanged="txtReceived_TextChanged" AutoPostBack="true"
                                                                onkeypress="return  isNumber(event);"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ForeColor="Red"
                                                                ControlToValidate="txtReceived" Font-Size="11" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="Label13" runat="server" Text="Balance Fees :" Font-Names="Verdana"
                                                                Font-Size="11px"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtBalanceFees" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                Font-Bold="true" ReadOnly="true"></asp:TextBox>
                                                        </td>
                                                        <td align="right">
                                                        </td>
                                                        <td align="left">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:UpdatePanel runat="server" ID="Upnl1">
                                                                <ContentTemplate>
                                                                    <asp:CheckBox ID="ChkComp" runat="server" OnCheckedChanged="ChkComp_CheckedChanged"
                                                                        AutoPostBack="true" Text="Company Patient :" Font-Names="Verdana" Font-Size="11px" />
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </td>
                                                        <td align="left" colspan="3">
                                                            <asp:UpdatePanel runat="server" ID="Upnl2">
                                                                <ContentTemplate>
                                                                    <asp:DropDownList ID="ddlCompany" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                        OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" AutoPostBack="true">
                                                                    </asp:DropDownList>
                                                                    <asp:Label ID="lblMessage" runat="server" Text="" Font-Names="Verdana" Font-Size="11px"
                                                                        ForeColor="Red"></asp:Label>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <asp:Button ID="BtnSave" runat="server" Text="Print" Font-Names="Verdana" Font-Size="12px"
                                                ValidationGroup="Save" BackColor="#3b3535" ForeColor="White" Width="125px" Style="border: 1px solid black"
                                                onmouseover="SetBtnMouseOver(this)" onmouseout="SetBtnMouseOut(this)" OnClick="BtnSave_Click" />
                                            <asp:Button ID="BtnClose" runat="server" Text="Close" Font-Names="Verdana" Font-Size="12px"
                                                OnClientClick="return hideModalPopup();" BackColor="#3b3535" ForeColor="White"
                                                Width="80px" Style="border: 1px solid black" onmouseover="SetBtnMouseOver(this)"
                                                onmouseout="SetBtnMouseOut(this)" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <input id="hdnPanel" type="hidden" runat="server" value="none" />
</asp:Content>
