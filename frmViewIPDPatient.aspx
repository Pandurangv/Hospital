<%@ Page Title="" Language="C#" MasterPageFile="~/mstAdmin.Master" AutoEventWireup="true" CodeBehind="frmViewIPDPatient.aspx.cs" Inherits="Hospital.frmViewIPDPatient" %>
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

        function ShowModalPopup() {
            var modalPopupBehavior = $find('ModalPopupProcess');
            modalPopupBehavior.show();
            return false;
        }

        function hideViewModalPopup() {
            var modalPopupBehavior = $find('modalpopupview');
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

        function resetControls() {

            var dtStartDate = document.getElementById("ContentPlaceHolder1_txtStartDate");
            var dtEndDate = document.getElementById("ContentPlaceHolder1_txtEndDate");
            var lPatientCode = document.getElementById("ContentPlaceHolder1_txtPatientId");
            var lPatientFName = document.getElementById("ContentPlaceHolder1_txtPatientFirstName");
            var lPatientMName = document.getElementById("ContentPlaceHolder1_txtPatientMiddleName");
            var lPatientLName = document.getElementById("ContentPlaceHolder1_txtPatientLastName");
            var lpnl = document.getElementById("ContentPlaceHolder1_pnlShow");

            var currDate = new Date();
            var month = currDate.getMonth() + 1;
            var day = currDate.getDate();
            var year = currDate.getFullYear();
            if (month.toString().length == 1) {
                month = "0" + month;
            }
            if (day.toString().length == 1) {
                day = "0" + day;
            }

            dtStartDate.value = day + '/' + month + '/' + year;
            dtEndDate.value = day + '/' + month + '/' + year;
            dtEndDate.value = day + '/' + month + '/' + year;

            lPatientCode.value = "";
            lPatientFName.value = "";
            lPatientMName.value = "";
            lPatientLName.value = "";
            lpnl.style.display = "none"
            return false;
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
                            <asp:Panel ID="pnlView" runat="server" BorderStyle="Solid" BorderColor="#006600"
                                BorderWidth="1px" Width="700px">
                                <table width="100%">
                                    <tr>
                                        <td colspan="4" align="center"  style="border-bottom:1px Solid Green; background-color:#3b3535;">
                                            <asp:Label ID="Label1" runat="server" Text="View/Edit Registered Patient Detail"
                                                Font-Names="Verdana" Font-Size="16px" Font-Bold="true" ForeColor="White"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" align="right">
                                            <asp:Button ID="BtnShowAll" runat="server" Text="Show All" Font-Names="Verdana" Font-Size="13px"
                                                BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black"
                                                onmouseover="SetBtnMouseOver(this)" onmouseout="SetBtnMouseOut(this)" OnClick="BtnShowAll_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="Label2" runat="server" Text="Start Date :" Font-Names="Verdana" Font-Size="11px"
                                                ForeColor="#3b3535"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtStartDate" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                OnKeyDown="return Startvalidkey(event);" onkeypress="return KeyValid(event);"
                                                MaxLength="10" Width="90px"></asp:TextBox>
                                            <cc:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtStartDate"
                                                Format="dd/MM/yyyy" EnableViewState="true">
                                            </cc:CalendarExtender>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="Label3" runat="server" Text="End Date :" Font-Names="Verdana" Font-Size="11px"
                                                ForeColor="#3b3535"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtEndDate" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                OnKeyDown="return Startvalidkey(event);" onkeypress="return KeyValid(event);"
                                                MaxLength="10" Width="90px"></asp:TextBox>
                                            <cc:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtEndDate"
                                                Format="dd/MM/yyyy" EnableViewState="true">
                                            </cc:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="Label4" runat="server" Text="Patient Id :" Font-Names="Verdana" Font-Size="11px"
                                                ForeColor="#3b3535"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtPatientId" runat="server" Font-Names="Verdana" Font-Size="11px"></asp:TextBox>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="Label20" runat="server" Text="Patient First Name :" Font-Names="Verdana"
                                                Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtPatientFirstName" runat="server" Font-Names="Verdana" Font-Size="11px"></asp:TextBox>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="Label21" runat="server" Text="Patient Middle Name :" Font-Names="Verdana"
                                                Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtPatientMiddleName" runat="server" Font-Names="Verdana" Font-Size="11px"></asp:TextBox>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="Label22" runat="server" Text="Patient Last Name :" Font-Names="Verdana"
                                                Font-Size="11px" ForeColor="#3b3535"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtPatientLastName" runat="server" Font-Names="Verdana" Font-Size="11px"></asp:TextBox>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" align="center">
                                            <asp:Button ID="BtnView" runat="server" Text="View" Font-Names="Verdana" Font-Size="13px"
                                                BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black"
                                                onmouseover="SetBtnMouseOver(this)" onmouseout="SetBtnMouseOut(this)" OnClick="BtnView_Click" />
                                            <asp:Button ID="BtnReset" runat="server" Text="Reset" Font-Names="Verdana" Font-Size="13px"
                                                BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black"
                                                onmouseover="SetBtnMouseOver(this)" onmouseout="SetBtnMouseOut(this)" OnClientClick="return resetControls();" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td align="center">
                            <asp:Panel ID="pnlShow" Width="1010px" BorderColor="Green" BorderWidth="1px" BorderStyle="Solid"
                                runat="server" Style="display: none;">
                                <table width="100%" style="overflow: hidden; table-layout: fixed; position: relative;">
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="lblPageCount" runat="server" Text="" Font-Names="Verdana" Font-Size="11px"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblRowCount" runat="server" Text="" Font-Names="Verdana" Font-Size="11px"></asp:Label>
                                        </td>
                                        <td align="right">
                                            <asp:Button ID="BtnDownload" runat="server" Text="Download" Font-Names="Verdana"
                                                Font-Size="13px" BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black"
                                                onmouseover="SetBtnMouseOver(this)" onmouseout="SetBtnMouseOut(this)" OnClick="BtnDownload_Click" />
                                            <asp:Button ID="BtnDelete" runat="server" Text="Delete" Font-Names="Verdana" Font-Size="13px"
                                                BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black"
                                                onmouseover="SetBtnMouseOver(this)" onmouseout="SetBtnMouseOut(this)" OnClick="BtnDelete_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 90%" colspan="3">
                                            <asp:Panel ID="pnl" ScrollBars="Horizontal" runat="server" Width="100%" BorderStyle="Solid"
                                                BorderWidth="1px" BorderColor="Green">
                                                <asp:GridView ID="dgvViewIPDPatient" runat="server" CellPadding="4" ForeColor="#333333"
                                                    GridLines="Both" Font-Names="Verdana" Width="100%" Font-Size="Small" AllowPaging="true"
                                                    PageSize="20" AutoGenerateColumns="false" BackColor="#FAF0FF" OnRowCommand="dgvViewIPDPatient_RowCommand"
                                                    OnDataBound="dgvViewIPDPatient_DataBound" OnRowDataBound="dgvViewIPDPatient_RowDataBound"
                                                    OnPageIndexChanged="dgvViewIPDPatient_PageIndexChanged" OnPageIndexChanging="dgvViewIPDPatient_PageIndexChanging">
                                                    <FooterStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" />
                                                    <RowStyle BackColor="#EFF3FB" Font-Size="11px" Wrap="False" />
                                                    <PagerStyle BackColor="#3b3535" ForeColor="White" HorizontalAlign="Left" Font-Size="11px" />
                                                    <SelectedRowStyle BackColor="#006600" Font-Bold="True" ForeColor="#333333" />
                                                    <HeaderStyle BackColor="#3b3535" Font-Bold="True" ForeColor="White" Font-Size="12px"
                                                        Wrap="False" />
                                                    <EditRowStyle BackColor="#FAF0FF" />
                                                    <AlternatingRowStyle BackColor="#FFFFD9" Wrap="False" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkDelete" runat="server" AutoPostBack="true" OnCheckedChanged="chkDelete_CheckedChanged"/>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Patient Id" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkPatientId" runat="server" CausesValidation="false" ForeColor="Maroon"
                                                                    Font-Underline="false" CommandName="EditIPDPatient" Text='<%#Eval("PatientCode") %>'></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="InitialDesc" HeaderText="Initial" ReadOnly="True" SortExpression="InitialDesc"
                                                            ItemStyle-HorizontalAlign="Center" />
                                                        <asp:TemplateField HeaderText="First Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPFirstName" runat="server" Text='<%#Eval("PatientFirstName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Middle Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPMiddleName" runat="server" Text='<%#Eval("PatientMiddleName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Last Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPLastName" runat="server" Text='<%#Eval("PatientLastName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Address" HeaderText="Address" ReadOnly="True" SortExpression="Address" />
                                                        <asp:BoundField DataField="ContactNo" HeaderText="Contact No" ReadOnly="True" SortExpression="ContactNo" />
                                                        <asp:BoundField DataField="AdminDate" HeaderText="Reg. Date" ReadOnly="True" SortExpression="AdminDate"
                                                            DataFormatString="{0:d}" />
                                                        <asp:BoundField DataField="AdmitTime" HeaderText="Reg. Time" ReadOnly="True" SortExpression="AdmitTime" />
                                                        <asp:BoundField DataField="BirthDate" HeaderText="Birth Date" ReadOnly="True" SortExpression="BirthDate"
                                                            DataFormatString="{0:d}" />
                                                        <asp:BoundField DataField="GenderDesc" HeaderText="Gender" ReadOnly="True" SortExpression="GenderDesc" />
                                                        <asp:BoundField DataField="ReferedBy" HeaderText="Refered By" ReadOnly="True" SortExpression="ReferedBy" />
                                                        <asp:BoundField DataField="Age" HeaderText="Age" ReadOnly="True" SortExpression="Age" />
                                                        <asp:BoundField DataField="City" HeaderText="City" ReadOnly="True" SortExpression="City" />
                                                        <asp:BoundField DataField="State" HeaderText="State" ReadOnly="True" SortExpression="State" />
                                                        <asp:BoundField DataField="Country" HeaderText="Country" ReadOnly="True" SortExpression="Country" />
                                                        <asp:BoundField DataField="EntryBy" HeaderText="Entry By" ReadOnly="True" SortExpression="EntryBy" />
                                                        <asp:BoundField DataField="EntryDate" HeaderText="Entry Date" ReadOnly="True" SortExpression="EntryDate" />
                                                        <asp:BoundField DataField="ChangeBy" HeaderText="Change By" ReadOnly="True" SortExpression="ChangeBy" />
                                                        <asp:BoundField DataField="ChangeDate" HeaderText="Change Date" ReadOnly="True" SortExpression="ChangeDate" />
                                                        <asp:TemplateField HeaderText="Insurance Company Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblInsurCompName" runat="server" Text='<%#Eval("InsuranceDesc") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Photo Id File" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="IDFile" runat="server" CommandName="IdProofDownload" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Insurance File" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="InsuranceFile" runat="server" CommandName="InsureanceProofDownload" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="HMS Card" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="HMSCard" runat="server" CommandName="HMSDownload" ImageUrl="~/images/pdfIcon.jpg" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
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
                            <asp:UpdatePanel runat="server" ID="UpnlPatient">
                                <ContentTemplate>
                                    <asp:Panel ID="Panel1" runat="server" Width="1055px" ScrollBars="Vertical" Style="text-align: center;
                                        background-color: #E0F0E8; height: 650px; border: 2px Solid Black; display: none;">
                                        <table>
                                            <tr>
                                                <td align="center">
                                                    <asp:Panel ID="pnlGrid" runat="server" Width="1030px" Style="text-align: center;
                                                        background-color: #E0F0E8; height: 700px;">
                                                        <table width="100%" style="border-color: Green; border-style: solid; border-width: 2px"
                                                            cellpadding="0">
                                                            <tr>
                                                                <td>
                                                                    <asp:Panel ID="Panel2" runat="server" Width="99.7%" Style="text-align: center; background-color: #E0F0E8;
                                                                        height: 690px;" BorderColor="Green" BorderStyle="Solid" BorderWidth="1px">
                                                                        <table width="100%">
                                                                            <tr>
                                                                                <td align="center" colspan="6">
                                                                                    <asp:Label ID="Label5" runat="server" Text="Update Patient Registration" Font-Names="Verdana"
                                                                                        Font-Size="16px" Font-Bold="true" ForeColor="#006600"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="6">
                                                                                    &nbsp;
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="6" align="left" style="border-top-style: solid; border-top-color: Green;
                                                                                    border-top-width: 1px; border-bottom-color: Green; border-bottom-style: solid;
                                                                                    border-bottom-width: 1px">
                                                                                    <asp:Label ID="Label31" runat="server" Text="Photo" Font-Names="Verdana" Font-Size="11px"
                                                                                        Font-Bold="true" ForeColor="#006600"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="6" align="left" style="padding-left: 100px;">
                                                                                    <asp:Panel ID="Panel3" runat="server" Height="100px" Width="100px" BorderColor="Green"
                                                                                        BorderStyle="Solid" BorderWidth="2px">
                                                                                        <asp:Image ID="imgPhoto" runat="server" Width="100px" Height="100px" />
                                                                                    </asp:Panel>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="6" align="left" style="border-top-style: solid; border-top-color: Green;
                                                                                    border-top-width: 1px; border-bottom-color: Green; border-bottom-style: solid;
                                                                                    border-bottom-width: 1px">
                                                                                    <asp:Label ID="lblUpdateHeading" runat="server" Text="Patient Detail" Font-Names="Verdana"
                                                                                        Font-Size="11px" Font-Bold="true" ForeColor="#006600"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right">
                                                                                    <asp:Label ID="lblEmpCode" runat="server" Text="Patient Code :" Font-Names="Verdana"
                                                                                        Font-Size="11px" ForeColor="#006600"></asp:Label>
                                                                                </td>
                                                                                <td align="left">
                                                                                    <asp:TextBox ID="txtPatientCode" runat="server" MaxLength="50" ReadOnly="true" Font-Names="Verdana"
                                                                                        Font-Size="11px" Width="80px" Font-Bold="true"></asp:TextBox>
                                                                                </td>
                                                                                <td align="right">
                                                                                    <asp:Label ID="Label14" runat="server" Text="Admission Date :" Font-Names="Verdana"
                                                                                        Font-Size="11px" ForeColor="#006600"></asp:Label>
                                                                                </td>
                                                                                <td align="left">
                                                                                    <asp:TextBox ID="txtAdmitDate" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                                        Width="80px" OnKeyDown="return isNumber(event);" MaxLength="10" onkeypress="return KeyValid(event);"></asp:TextBox>
                                                                                        <cc:CalendarExtender  ID="CalendarExtender4" runat="server" TargetControlID="txtAdmitDate" Format="dd/MM/yyyy">
                                                                                        </cc:CalendarExtender>
                                                                                </td>
                                                                                <td align="right">
                                                                                    <asp:Label ID="Label15" runat="server" Text="Admission Time :" Font-Names="Verdana"
                                                                                        Font-Size="11px" ForeColor="#006600"></asp:Label>
                                                                                </td>
                                                                                <td align="left">
                                                                                    <asp:TextBox ID="txtAdmitTime" runat="server" MaxLength="50" Font-Names="Verdana"
                                                                                        Font-Size="11px" Width="80px" Font-Bold="true"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right">
                                                                                    <asp:Label ID="lblFirstName" runat="server" Text="Patient First Name :" Font-Names="Verdana"
                                                                                        Font-Size="11px" ForeColor="#006600"></asp:Label>
                                                                                </td>
                                                                                <td align="left">
                                                                                    <asp:DropDownList ID="ddlInitials" runat="server" Font-Names="Verdana" Font-Size="11px">
                                                                                    </asp:DropDownList>
                                                                                    <asp:TextBox ID="txtFirstName" runat="server" MaxLength="50" Font-Names="Verdana"
                                                                                        Font-Size="11px" Width="120px" ToolTip="Enter Patient First Name" Font-Bold="true"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ForeColor="Red" ControlToValidate="txtFirstName"
                                                                                        Font-Size="13" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                                                                                </td>
                                                                                <td align="right">
                                                                                    <asp:Label ID="lblMiddleName" runat="server" Text="Middle Name :" Font-Names="Verdana"
                                                                                        Font-Size="11px" ForeColor="#006600"></asp:Label>
                                                                                </td>
                                                                                <td align="left">
                                                                                    <asp:TextBox ID="txtMidleName" runat="server" MaxLength="50" Font-Names="Verdana"
                                                                                        Font-Size="11px" Width="120px" ToolTip="Enter Patient Middle Name" Font-Bold="true"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ForeColor="Red"
                                                                                        ControlToValidate="txtMidleName" Font-Size="13" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                                                                                </td>
                                                                                <td align="right">
                                                                                    <asp:Label ID="lblLastName" runat="server" Text="Last Name :" Font-Names="Verdana"
                                                                                        Font-Size="11px" ForeColor="#006600"></asp:Label>
                                                                                </td>
                                                                                <td align="left">
                                                                                    <asp:TextBox ID="txtLastName" runat="server" MaxLength="50" Font-Names="Verdana"
                                                                                        Font-Size="11px" Width="120px" ToolTip="Enter Patient Last Name" Font-Bold="true"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red"
                                                                                        ControlToValidate="txtLastName" Font-Size="13" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right">
                                                                                    <asp:Label ID="Label6" runat="server" Text="Gender :" Font-Names="Verdana" Font-Size="11px"
                                                                                        ForeColor="#006600"></asp:Label>
                                                                                </td>
                                                                                <td align="left">
                                                                                    <asp:DropDownList ID="ddlGender" runat="server" Font-Names="Verdana" Font-Size="11px">
                                                                                    </asp:DropDownList>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ForeColor="Red"
                                                                                        ControlToValidate="ddlGender" Font-Size="13" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                                                                                </td>
                                                                                <td align="right">
                                                                                    <asp:Label ID="Label7" runat="server" Text="Age In Years :" Font-Names="Verdana"
                                                                                        Font-Size="11px" ForeColor="#006600"></asp:Label>
                                                                                </td>
                                                                                <td align="left">
                                                                                    <asp:TextBox ID="txtAge" runat="server" MaxLength="50" Font-Names="Verdana" Font-Size="11px"
                                                                                        Width="120px" onkeypress="return isNumber(event);"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ForeColor="Red"
                                                                                        ControlToValidate="txtAge" Font-Size="13" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                                                                                </td>
                                                                                <td align="right">
                                                                                    <asp:Label ID="Label8" runat="server" Text="Occupation :" Font-Names="Verdana" Font-Size="11px"
                                                                                        ForeColor="#006600"></asp:Label>
                                                                                </td>
                                                                                <td align="left">
                                                                                    <asp:DropDownList ID="ddlOccupation" runat="server" Font-Names="Verdana" Font-Size="11px">
                                                                                    </asp:DropDownList>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ForeColor="Red"
                                                                                        ControlToValidate="ddlOccupation" Font-Size="13" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right">
                                                                                    <asp:Label ID="Label9" runat="server" Text="Religion :" Font-Names="Verdana" Font-Size="11px"
                                                                                        ForeColor="#006600"></asp:Label>
                                                                                </td>
                                                                                <td align="left">
                                                                                    <asp:UpdatePanel runat="server" ID="UpnlPatient1">
                                                                                        <ContentTemplate>
                                                                                            <asp:DropDownList ID="ddlReligion" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                                                OnSelectedIndexChanged="ddlReligion_SelectedIndexChanged" AutoPostBack="true">
                                                                                            </asp:DropDownList>
                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ForeColor="Red"
                                                                                                ControlToValidate="ddlReligion" Font-Size="11" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                                                                                        </ContentTemplate>
                                                                                        <Triggers>
                                                                                            <asp:AsyncPostBackTrigger ControlID="ddlReligion" EventName="SelectedIndexChanged" />
                                                                                        </Triggers>
                                                                                    </asp:UpdatePanel>
                                                                                </td>
                                                                                <td align="right">
                                                                                    <asp:Label ID="Label10" runat="server" Text="Caste :" Font-Names="Verdana" Font-Size="11px"
                                                                                        ForeColor="#006600"></asp:Label>
                                                                                </td>
                                                                                <td align="left">
                                                                                    <asp:DropDownList ID="ddlCasts" runat="server" Font-Names="Verdana" Font-Size="11px">
                                                                                    </asp:DropDownList>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ForeColor="Red"
                                                                                        ControlToValidate="ddlCasts" Font-Size="11" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                                                                                </td>
                                                                                <td align="right">
                                                                                    <asp:Label ID="Label11" runat="server" Text="Birth Date :" Font-Names="Verdana" Font-Size="11px"
                                                                                        ForeColor="#006600"></asp:Label>
                                                                                </td>
                                                                                <td align="left">
                                                                                    <asp:TextBox ID="txtBirthDate" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                                        Width="120px" OnKeyDown="return isNumber(event);" MaxLength="10" onkeypress="return KeyValid(event);"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ForeColor="Red"
                                                                                        ControlToValidate="txtBirthDate" Font-Size="11" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                                                                                    <cc:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtBirthDate"
                                                                                        Format="dd/MM/yyyy">
                                                                                    </cc:CalendarExtender>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right">
                                                                                    <asp:Label ID="Label12" runat="server" Text="Address :" Font-Names="Verdana" Font-Size="11px"
                                                                                        ForeColor="#006600"></asp:Label>
                                                                                </td>
                                                                                <td align="left" colspan="5">
                                                                                    <asp:TextBox ID="txtAddress" runat="server" MaxLength="100" TextMode="MultiLine"
                                                                                        Width="470px" Font-Names="Verdana" Font-Size="11px"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ForeColor="Red"
                                                                                        ControlToValidate="txtAddress" Font-Size="11" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right">
                                                                                    <asp:Label ID="Label23" runat="server" Text="Contact No :" Font-Names="Verdana" Font-Size="11px"
                                                                                        ForeColor="#006600"></asp:Label>
                                                                                </td>
                                                                                <td align="left" colspan="5">
                                                                                    <asp:TextBox ID="txtContactNo" runat="server" MaxLength="50" Font-Names="Verdana"
                                                                                        Font-Size="11px" Width="120px" onkeypress="return isNumber(event);"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ForeColor="Red"
                                                                                        ControlToValidate="txtContactNo" Font-Size="11" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right">
                                                                                    <asp:Label ID="lblCountry" runat="server" Text="City :" Font-Names="Verdana" Font-Size="11px"
                                                                                        ForeColor="#006600"></asp:Label>
                                                                                </td>
                                                                                <td align="left">
                                                                                    <asp:TextBox ID="txtCity" runat="server" Font-Names="Verdana" Font-Size="11px"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ForeColor="Red"
                                                                                        ControlToValidate="txtCity" Font-Size="11" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                                                                                </td>
                                                                                <td align="right">
                                                                                    <asp:Label ID="Label16" runat="server" Text="State :" Font-Names="Verdana" Font-Size="11px"
                                                                                        ForeColor="#006600"></asp:Label>
                                                                                </td>
                                                                                <td align="left">
                                                                                    <asp:TextBox ID="txtState" runat="server" Font-Names="Verdana" Font-Size="11px"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ForeColor="Red"
                                                                                        ControlToValidate="txtState" Font-Size="11" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                                                                                </td>
                                                                                <td align="right">
                                                                                    <asp:Label ID="Label17" runat="server" Text="Country :" Font-Names="Verdana" Font-Size="11px"
                                                                                        ForeColor="#006600"></asp:Label>
                                                                                </td>
                                                                                <td align="left">
                                                                                    <asp:TextBox ID="txtCountry" runat="server" Font-Names="Verdana" Font-Size="11px"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ForeColor="Red"
                                                                                        ControlToValidate="txtCountry" Font-Size="11" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right">
                                                                                    <asp:Label ID="Label19" runat="server" Text="Refered By :" Font-Names="Verdana" Font-Size="11px"
                                                                                        ForeColor="#006600"></asp:Label>
                                                                                </td>
                                                                                <td align="left">
                                                                                    <asp:DropDownList ID="ddlRefDoctor" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                                        Width="200px">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                                <td align="right">
                                                                                    <asp:Label ID="Label34" runat="server" Text="Blood Group :" Font-Names="Verdana"
                                                                                        Font-Size="11px" ForeColor="#006600"></asp:Label>
                                                                                </td>
                                                                                <td colspan="4" align="left">
                                                                                    <asp:DropDownList ID="ddlBloodGroup" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                                        Width="80px">
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
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="6" align="left" style="border-top-style: solid; border-top-color: Green;
                                                                                    border-top-width: 1px; border-bottom-color: Green; border-bottom-style: solid;
                                                                                    border-bottom-width: 1px">
                                                                                    <asp:Label ID="Label18" runat="server" Text="Company Detail" Font-Names="Verdana"
                                                                                        Font-Size="11px" Font-Bold="true" ForeColor="#006600"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right">
                                                                                    <asp:Label ID="lblName" runat="server" Text="Company Name :" Font-Names="Verdana"
                                                                                        Font-Size="11px" ForeColor="#006600"></asp:Label>
                                                                                </td>
                                                                                <td align="left" colspan="5">
                                                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel3">
                                                                                        <ContentTemplate>
                                                                                            <asp:DropDownList ID="ddlCompName" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                                                AutoPostBack="true">
                                                                                            </asp:DropDownList>
                                                                                        </ContentTemplate>
                                                                                        <Triggers>
                                                                                            <asp:AsyncPostBackTrigger ControlID="ddlCompName" EventName="SelectedIndexChanged" />
                                                                                        </Triggers>
                                                                                    </asp:UpdatePanel>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="6" align="left" style="border-top-style: solid; border-top-color: Green;
                                                                                    border-top-width: 1px; border-bottom-color: Green; border-bottom-style: solid;
                                                                                    border-bottom-width: 1px">
                                                                                    <asp:Label ID="Label13" runat="server" Text="Patient History Detail :" Font-Names="Verdana"
                                                                                        Font-Size="11px" Font-Bold="true" ForeColor="#006600"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="center" colspan="2">
                                                                                    <asp:Label ID="Label28" runat="server" Text="Personal History" Font-Names="Verdana"
                                                                                        Font-Size="11px" ForeColor="#006600" Font-Bold="true"></asp:Label>
                                                                                </td>
                                                                                <td align="center" colspan="2">
                                                                                    <asp:Label ID="Label29" runat="server" Text="Past Medical History" Font-Names="Verdana"
                                                                                        Font-Size="11px" ForeColor="#006600" Font-Bold="true"></asp:Label>
                                                                                </td>
                                                                                <td colspan="2">
                                                                                    <asp:Label ID="Label30" runat="server" Text="Family History" Font-Names="Verdana"
                                                                                        Font-Size="11px" ForeColor="#006600" Font-Bold="true"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2">
                                                                                    <asp:TextBox ID="txtPatientHistory" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                                        Width="300px" TextMode="MultiLine" Height="100px"></asp:TextBox>
                                                                                </td>
                                                                                <td colspan="2">
                                                                                    <asp:TextBox ID="txtPastMedHistory" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                                        Width="300px" TextMode="MultiLine" Height="100px"></asp:TextBox>
                                                                                </td>
                                                                                <td colspan="2">
                                                                                    <asp:TextBox ID="txtFamilyHistory" runat="server" Font-Names="Verdana" Font-Size="11px"
                                                                                        Width="300px" TextMode="MultiLine" Height="100px"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="6">
                                                                                    &nbsp;
                                                                                </td>
                                                                            </tr>
                                                                            <tr align="center">
                                                                                <td align="center" colspan="6">
                                                                                    <asp:Button ID="BtnUpdate" runat="server" Text="Update" Font-Names="Verdana" Font-Size="12px"
                                                                                        ValidationGroup="Save" BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black"
                                                                                        onmouseover="SetBtnMouseOver(this)" onmouseout="SetBtnMouseOut(this)" OnClick="BtnUpdate_Click" />
                                                                                    <asp:Button ID="BtnClose" runat="server" Text="Close" Font-Names="Verdana" Font-Size="12px"
                                                                                        BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black"
                                                                                        onmouseover="SetBtnMouseOver(this)" onmouseout="SetBtnMouseOut(this)" OnClientClick="hideModalPopup()" />
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
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:Button runat="server" ID="hiddenTargetControlForModalPopupDelete" Style="display: none" />
                            <cc:ModalPopupExtender runat="server" ID="modalpopupDelete" BehaviorID="DeleteprogrammaticModalPopupBehavior"
                                TargetControlID="hiddenTargetControlForModalPopupDelete" PopupControlID="pnlDelete"
                                BackgroundCssClass="modalBackground" DropShadow="true" PopupDragHandleControlID="DeleteprogrammaticPopupDragHandle"
                                RepositionMode="RepositionOnWindowScroll">
                            </cc:ModalPopupExtender>
                            <asp:Panel ID="pnlDelete" runat="server" Width="500px" Style="text-align: center;
                                background-color: #E0F0E8; display: none;" BorderColor="Green" BorderStyle="Solid"
                                BorderWidth="2px">
                                <table width="100%">
                                    <tr align="center">
                                        <td align="center" colspan="6" style="font-family: Verdana; font-size: 15px; color: Green;
                                            font-weight: bold;">
                                            <asp:Label ID="Label24" runat="server" Text="Do You Want To Delete Record ?" Font-Names="Verdana"
                                                Font-Size="15px" ForeColor="#006600"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="font-family: Verdana; width: 25%; font-size: 11px; color: #006600;">
                                            Delete Remark :
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtDeleteRemark" runat="server" MaxLength="100" TextMode="MultiLine"
                                                Width="80%" Font-Names="Verdana" Font-Size="11px" Style="border: 1px solid Green;"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr align="center">
                                        <td align="center" colspan="6">
                                            <asp:Button ID="BtnDeleteOk" runat="server" Text="Delete" Font-Names="Verdana" Font-Size="12px"
                                                CausesValidation="false" BackColor="#3b3535" ForeColor="White" Width="80px" Style="border: 1px solid black"
                                                onmouseover="SetBtnMouseOver(this)" onmouseout="SetBtnMouseOut(this)" OnClick="BtnDeleteOk_Click" />
                                            <asp:Button ID="BtnDeleteClose" runat="server" Text="Close" Font-Names="Verdana"
                                                Font-Size="12px" OnClientClick="return hideDeleteModalPopup();" BackColor="#3b3535"
                                                ForeColor="White" Width="80px" Style="border: 1px solid black" onmouseover="SetBtnMouseOver(this)"
                                                onmouseout="SetBtnMouseOut(this)" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6">
                                            <asp:Label ID="lblMessage" runat="server" Text="" Font-Names="Verdana" Font-Size="11px"
                                                ForeColor="Red"></asp:Label>
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
