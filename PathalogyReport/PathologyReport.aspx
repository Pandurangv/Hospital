<%@ Page Title="" Language="C#" MasterPageFile="~/mstAdmin.Master" AutoEventWireup="true" CodeBehind="PathologyReport.aspx.cs" Inherits="Hospital.PathalogyReport.PathalogyReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=pnlContents.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head><title></title>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;
        }
    </script>
    <style type="text/css">
        .panelHeight
        {
            height: auto;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td align="left">
                <asp:Button ID="btnPrint" runat="server" Text="Print" OnClientClick="return PrintPanel();"
                    BackColor="#3b3535" Font-Names="Verdana" Font-Size="12px" ForeColor="White" Width="80px"
                    Style="border: 1px solid black;" />
                <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnClose_Click" BackColor="#3b3535"
                    Font-Names="Verdana" Font-Size="12px" ForeColor="White" Width="80px" Style="border: 1px solid black;" />
            </td>
        </tr>
    </table>
    <asp:Panel ID="pnlContents" runat="server" BorderStyle="Groove" CssClass="panelHeight">
        <%
            Hospital.Models.DataLayer.CriticareHospitalDataContext objData = new Hospital.Models.DataLayer.CriticareHospitalDataContext();
            string ReportType=string.Empty;
            if (Session["ReportType"]!=null)
            {
                ReportType = Convert.ToString(Session["ReportType"]);
            }
            else
            {
                ReportType = Hospital.Models.DataLayer.QueryStringManager.Instance.ReportType;
            }
            #region Pathology
            if (ReportType.Equals("Pathology"))
            {
                StringBuilder sb = new StringBuilder();

                List<Hospital.Models.DataLayer.STP_PrintPathologyResult> lstBill = objData.STP_PrintPathology(Hospital.Models.DataLayer.QueryStringManager.Instance.LabId).ToList();
                if (lstBill.Count > 0)
                {
                    sb.Append("<table style='border:1px solid;' width='1000px' align='center'><tr height='100'><td height='100'><table width='100%' Height='100%'><tr>");
                    sb.Append("<td  width='100%' align='center'> <img src='../images/BannerImages/HospitalNameGNH.png'  alt='#' width='100%' align='Left'/>");
                    sb.Append("</td></tr><tr><td width='100%' align='center'><strong>");
                    sb.Append("</strong></td></tr></table></td></tr><tr style='width:100%'><td style='width:90%'><table width='100%'>");
                    sb.Append("<tr style='width:100%'><td colspan='2' align='center'><strong><u>PATHOLOGY REPORT</u></strong></td></tr>");
                    sb.Append("<tr style='width:100%'><hr><td style='width:50%;' align='left'><strong>Lab Id :- ");
                    sb.Append(lstBill[0].LabId).Append("</strong></td><td style='width:50%;' align='right'><strong>Date :- ");
                    sb.Append(string.Format("{0:dd/MM/yyyy}", lstBill[0].TestDate)).Append("</strong></td></tr><tr><td style='width:50%;colspan='2' align='left'><strong>Patient Name :- ");
                    sb.Append(lstBill[0].PatientName).Append("</strong></td><td style='width:50%;' align='right'><strong>Age :- ");
                    sb.Append(lstBill[0].Age).Append("</strong></td> </tr><tr><td style='width:50%;colspan='2' align='left'></td>");
                    sb.Append("<td style='width:50%;' align='right'><strong>Patient Type :- ").Append(lstBill[0].PatientType).Append("</strong></td></table></td></tr>");
                    sb.Append("<tr><td align='left'><table><hr><strong>"); sb.Append(lstBill[0].TestName).Append("</strong></table></td></tr>");
                    sb.Append("<tr  width='100%' height='100%'><td  width='100%' height='100%'><table width='100%' border='1' height='100%'>");
                    sb.Append("</tr><tr><td align='center'><strong>Investigation</strong></td><td align='center'><strong>Reference Value</strong></td><td align='center'>");
                    sb.Append("<strong>Observed Value</strong></td><td align='center'><strong>Unit</strong></td><td align='center'><strong>Test Method</strong></td><td align='center'><strong>Comment</strong></td></tr>");
                    foreach (Hospital.Models.DataLayer.STP_PrintPathologyResult item in lstBill)
                    {
                        sb.Append("<tr><td align='center'>");
                        sb.Append(item.ParaName).Append("</td><td align='center'>");
                        sb.Append(item.MinPara).Append("-");
                        sb.Append(item.MaxPara).Append("</td><td align='center'>");
                        sb.Append(item.Result).Append("</td><td align='center'>");
                        sb.Append(item.UnitDesc).Append("</td><td align='center' >");
                        sb.Append(item.TestMethodName).Append("</td><td align='center' >");
                        sb.Append(item.Comment).Append("</tr>");
                    }
                    sb.Append("</table></td></tr>");
                    sb.Append("<tr style='width:100%;'><td width='100%' align='left'><table width='100%'><tr ><td style='width:70%;' align='left'><strong>Remark :- ");
                    sb.Append(lstBill[0].FinalComment).Append("</strong></td><td style='width:30%;' align='right' ></td></tr>");
                    sb.Append("<tr><td style='width:70%;' align='left'></td><td style='width:30%;' align='right'></td></tr>");
                    sb.Append("<tr><td style='width:70%;' align='left'></td><td style='width:30%;' align='right'></td></tr>");
                    sb.Append("<tr><td style='width:70%;' align='left'></td><td style='width:30%;' align='right'></td></tr>");
                    sb.Append("<tr><td style='width:70%;' align='left'></td><td style='width:30%;' align='right'><hr></td></tr>");
                    sb.Append("<tr><td style='width:70%;' align='left'><strong>");
                    sb.Append("</strong></td><td style='width:30%;' align='right' ><strong>Lab Technician</strong></td></tr></table></td></tr></table>");

                    Response.Write(sb.ToString());
                }
            }
            #endregion
            #region Patient Invoice
            else if (ReportType.Equals("Invoice"))
            {
                StringBuilder sb = new StringBuilder();
                List<Hospital.Models.DataLayer.STP_PrintPatientInvoiceResult> lstBill = objData.STP_PrintPatientInvoice(Hospital.Models.DataLayer.QueryStringManager.Instance.BILLNo).ToList();
                DateTime? dtDischargeTime = new Hospital.Models.BusinessLayer.MakeDischargeBLL().GetDischargeDetailsByPatientId(lstBill[0].AdmitId);

                string amt = Hospital.Models.DataLayer.Commons.Rupees(Convert.ToDecimal(lstBill[0].NetAmount));
                DateTime Today = DateTime.Now;
                
                string RoomType = string.Empty;
                decimal Disc = 0;
                if (lstBill[0].FixedDiscount == null)
                {
                    Disc = 0;
                }
                else
                {
                    Disc = Convert.ToDecimal(lstBill[0].FixedDiscount);
                }
                foreach (Hospital.Models.DataLayer.STP_PrintPatientInvoiceResult item in lstBill)
                {
                    if (!string.IsNullOrEmpty(item.CategoryDesc))
                    {
                        RoomType = item.CategoryDesc;
                        break;
                    }
                }

                sb.Append("<table align='center' style='border:1px solid;' width='900px'><tr height='100'><td height='100'><table width='100%' Height='100%'><tr>");
                sb.Append("<td  width='100%' align='center'><img src='../images/BannerImages/HospitalNameGNH.png'  alt='#' width='100%' align='Left'/>");
                sb.Append("</td></tr><tr><td width='100%' align='center'><strong>");
                sb.Append("</strong></td></tr>");
                sb.Append("<tr><td width='100%' align='center'> <hr></td></tr>");
                if (((lstBill[0].BillType) == "Original") && lstBill[0].PatientType == "OPD")
                {
                    sb.Append("<tr><td width='100%' align='center'><strong><u>");
                    sb.Append("&nbsp;Bill Cum Receipt</u></strong></td></tr>");
                }
                else
                {
                    if ((lstBill[0].BillType) == "Intermediate")
                    {
                        sb.Append("<tr><td width='100%' align='center'><strong><u>");
                        sb.Append("&nbsp;Intermediate Bill Cum Receipt</u></strong></td></tr>");
                    }
                    else
                    {
                        sb.Append("<tr><td width='100%' align='center'><strong><u>");
                        sb.Append(lstBill[0].BillType).Append("&nbsp;Bill</u></strong></td></tr>");
                    }
                }
                sb.Append("</table></td></tr><tr style='width:100%'><td style='width:90%'>");
                sb.Append("<table width='100%'>");
                if ((lstBill[0].BillType) == "Estimated")
                {
                    sb.Append("<tr style='width:100%'>");
                    sb.Append("<td style='width:15%;' align='left'><strong>Bill No </strong></td><td style='width:25%;' align='left'><strong>:- ");
                    sb.Append("</strong></td><td style='width:15%;'></td>");
                    sb.Append("<td style='width:20%;' align='left'><strong>Date </strong></td><td style='width:25%;' align='left'><strong> :- ");
                    sb.Append(string.Format("{0:dd/MM/yyyy}", lstBill[0].BillDate)).Append("</strong></td></tr>");
                }
                else
                {
                    sb.Append("<tr style='width:100%'>");
                    sb.Append("<td style='width:15%;' align='left'><strong>Bill No </strong></td><td style='width:25%;' align='left'><strong>:- ");
                    sb.Append(Convert.ToInt32(Session["BILLNo"])).Append("</strong></td><td style='width:15%;'></td>");
                    sb.Append("<td style='width:20%;' align='left'><strong>Date </strong></td><td style='width:25%;' align='left'><strong> :- ");
                    sb.Append(string.Format("{0:dd/MM/yyyy}", lstBill[0].BillDate)).Append("</strong></td></tr>"); 
                }
                sb.Append("<tr style='width:100%'>");
                sb.Append("<td style='width:15%;' align='left'><strong>Patient Name </strong></td><td style='width:25%;' align='left'><strong>:- ");
                sb.Append(lstBill[0].InitialDesc).Append("&nbsp;");
                sb.Append(lstBill[0].FullName).Append("</strong></td><td style='width:15%;'></td>");
                sb.Append("<td style='width:20%;' align='left'><strong>Patient Type </strong></td><td style='width:25%;' align='left'><strong> :- ");
                sb.Append(lstBill[0].PatientType).Append("</strong></td></tr>");
                if (lstBill[0].PatientType == "IPD")
                {
                    sb.Append("<tr><td style='width:15%;' align='left'><strong>Date Of Admission</strong></td><td style='width:25%;' align='left'><strong>:- ");
                    sb.Append(string.Format("{0:dd/MM/yyyy HH:mm}", lstBill[0].AdmitDate)).Append("</strong></td><td style='width:15%;'></td>");
                    sb.Append("<td style='width:20%; colspan='2' align='left'><strong>IPD No </strong></td><td style='width:25%;' align='left'><strong>:- ");
                    sb.Append(lstBill[0].IPDNo).Append("</strong></td></tr>");
                }
                if (lstBill[0].PatientType == "OPD")
                {
                    sb.Append("<tr><td style='width:15%;colspan='2' align='left'><strong>Address </strong></td><td style='width:25%;' align='left'><strong>:- ");
                    sb.Append(lstBill[0].Address).Append("</strong></td><td style='width:15%;'></td>");
                    sb.Append("<td style='width:20%; colspan='2' align='left'><strong>OPD No </strong></td><td style='width:25%;' align='left'><strong>:- ");
                    sb.Append(lstBill[0].OPDNo).Append("</strong></td></tr>");
                }
                if (lstBill[0].PatientType == "IPD")
                {
                    sb.Append("<tr><td style='width:15%;colspan='2' align='left'><strong>Address </strong></td><td style='width:25%;' align='left'><strong>:- ");
                    sb.Append(lstBill[0].Address).Append("</strong></td><td style='width:15%;'></td><td style='width:20%;' align='left'><strong>Room Type</strong>");
                    sb.Append("</td><td style='width:25%;' align='left'><strong> :- ");
                    sb.Append(RoomType).Append("</strong></td> </tr>");
                }
                
                sb.Append("<tr><td style='width:15%;colspan='2' align='left'><strong>Age</strong></td><td style='width:25%;' align='left'><strong> :- ");
                sb.Append(lstBill[0].Age).Append("&nbsp;&nbsp;").Append(lstBill[0].AgeIn).Append("</strong></td><td style='width:15%;'></td>");
                sb.Append("<td style='width:20%;' align='left'><strong>MRN</strong></td><td style='width:25%;' align='left'><strong> :- ");
                sb.Append(lstBill[0].PatientCode).Append("</strong></td></tr>");
                sb.Append("<tr><td style='width:15%;colspan='2' align='left'><strong>Department</strong></td><td style='width:25%;' align='left'><strong> :- ");
                sb.Append(lstBill[0].CategoryName).Append("</strong></td><td style='width:15%;'></td>");
                sb.Append("<td style='width:20%;' align='left'><strong>Consult Doctor</strong></td><td style='width:25%;' align='left'><strong> :- ");
                sb.Append(lstBill[0].DeptDoctorName).Append("</strong></td></tr>");

                if (lstBill[0].PatientType == "OPD" && lstBill[0].BillType == "Estimated")
                {
                    sb.Append("<tr><td colspan='2'></td><td></td><td style='width:15%;' align='left'><strong> </strong></td><td style='width:25%;' align='left'><strong>&nbsp; ");
                    sb.Append("&nbsp;(").Append(lstBill[0].Education).Append(")</strong></td></tr>");
                }
                if (lstBill[0].PatientType == "OPD" && lstBill[0].BillType == "Original")
                {
                    sb.Append("<tr><td colspan='2'></td><td></td><td style='width:15%;' align='left'><strong> </strong></td><td style='width:25%;' align='left'><strong>&nbsp; ");
                    sb.Append("&nbsp;(").Append(lstBill[0].Education).Append(")</strong></td></tr>");
                }
                if (lstBill[0].PatientType == "IPD" && lstBill[0].BillType == "Estimated")
                {
                    sb.Append("<tr><td colspan='2'></td><td></td><td style='width:15%;' align='left'><strong> </strong></td><td style='width:25%;' align='left'><strong>&nbsp; ");
                    sb.Append("&nbsp;(").Append(lstBill[0].Education).Append(")</strong></td></tr>");
                }
                if (lstBill[0].PatientType == "IPD" && lstBill[0].BillType == "Original")
                {
                    sb.Append("<tr><td style='width:15%;' align='left'><strong>Date Of Discharge </strong></td><td style='width:25%;' align='left'><strong> :- ");
                    sb.Append(string.Format("{0:dd/MM/yyyy}", lstBill[0].BillDate));

                    if (dtDischargeTime != null)
                    {
                        sb.Append(string.Format("{0: HH:mm}", dtDischargeTime.Value));
                    }
                    else
                    {
                        sb.Append(string.Format("{0: HH:mm}", Today));
                    }

                    sb.Append("</strong></td>");
                    sb.Append("<td style='width:20%;' align='left'><strong></strong></td><td></td><td align='left'><strong>&nbsp; (");
                    sb.Append(lstBill[0].Education).Append(")</strong></td></tr>");
                }
                
                sb.Append("</table></td></tr>");
                
                sb.Append("<tr  width='100%' height='100%'><td  width='100%' height='100%'>");
                sb.Append("<table width='100%' height='100%' border='1'><tr>");
                sb.Append("<td align='center'><strong>Sr No.</strong></td>");
                sb.Append("<td align='center'><strong>Particulars</strong></td>");
                if (lstBill[0].PatientType == "OPD")
                {
                }
                else
                {
                    sb.Append("<td align='center'><strong>Remarks</strong></td>");
                }
                sb.Append("<td align='center'><strong>Quantity</strong></td>");
                sb.Append("<td align='center'><strong>No Of Days</strong></td>");
                sb.Append("<td align='center'><strong>Charge</strong></td>");
                sb.Append("<td align='center'><strong>Amount</strong></td></tr>");
                int count = 1;
                foreach (Hospital.Models.DataLayer.STP_PrintPatientInvoiceResult item in lstBill)
                {
                    sb.Append("<tr>");
                    sb.Append("<td align='center'style='border-bottom:hidden !important;'>");
                    sb.Append(count).Append("</td>");
                    sb.Append("<td align='center'style='border-bottom:hidden !important;'>");
                    if (item.BedNo == null)
                    {
                        sb.Append(item.ChargesName).Append("</td>");
                    }
                    else
                    {
                        sb.Append(item.ChargesName).Append("(").Append(item.BedNo).Append(")</td>");
                    }
                    if (lstBill[0].PatientType == "OPD")
                    {
                    }
                    else
                    {
                        sb.Append("<td align='center'style='border-bottom:hidden !important;'>");
                        sb.Append(item.Remarks).Append("</td>");
                    }
                    sb.Append("<td align='center'style='border-bottom:hidden !important;'>");
                    sb.Append(item.Quantity).Append("</td>");
                    sb.Append("<td align='center'style='border-bottom:hidden !important;'>");
                    sb.Append(item.NoOfDays).Append("</td>");
                    sb.Append("<td align='center'style='border-bottom:hidden !important;'>");
                    sb.Append(item.ChargePerDay).Append("</td>");
                    sb.Append("<td align='center'style='border-bottom:hidden !important;'>");
                    sb.Append(item.Amount).Append("</td></tr>");
                    count++;
                }
                sb.Append("</table></td></tr>");
                sb.Append("<tr style='width:100%'><td style='width:100%' align='Right'>");
                sb.Append("<table width='100%'>");

                if (lstBill[0].PatientType == "IPD" && lstBill[0].BillType == "Original")
                {
                    sb.Append("<tr style='width:100%'>");
                    sb.Append("<td colspan='3' align='right'><strong>Total Amount :- Rs.</strong></td><td align='right'><strong>");
                    sb.Append(decimal.Round(Convert.ToDecimal(lstBill[0].Total), 2)).Append("/-</strong></td></tr>");
                    sb.Append("<tr>");
                    sb.Append("<td colspan='3' align='right'><strong>Discount");
                    sb.Append(" :- </td><td style='width:10%;' align='right'><strong>");
                    sb.Append(decimal.Round(Disc, 2));
                    sb.Append("/-</strong></td></tr>");
                    //sb.Append("<tr><td style='width:15%;' align='right'><strong>Refund Amount :-</strong></td><td colspan='3' align='left'><strong>");
                    //sb.Append(decimal.Round(Convert.ToDecimal(lstBill[0].RefundAmount), 2)).Append("/-</strong></td>");
                    //sb.Append("</td></tr>");
                    //<td style='width:20%;' align='right'><strong>Service Tax(");
                    //sb.Append(lstBill[0].Service).Append("%) :- </td><td style='width:20%;' align='right'><strong>");
                    //sb.Append(decimal.Round(Convert.ToDecimal(lstBill[0].Total * lstBill[0].Service / 100), 2));
                    //sb.Append("/-</strong></td></tr>");
                    sb.Append("<tr height='6px'><td colspan='4' style='width:100%;' align='left'><hr></td>");
                    sb.Append("</tr>");
                    sb.Append("<tr><td colspan='2' style='width:60%;' align='left'><strong>Amount In Words:- Rupees ");
                    sb.Append(amt).Append(" Only.</strong></td><td style='width:30%;' align='right'><strong>NetAmount :- Rs.</td><td style='width:10%;' align='right'><strong>");
                    sb.Append(decimal.Round(Convert.ToDecimal(lstBill[0].NetAmount), 2)).Append("/-</strong></td></tr>");
                }
                else
                {
                    sb.Append("<tr style='width:100%'><td style='width:'15%;' align='right'><strong>Total Advance :-</strong></td><td style='width:45%;' align='left'><strong>");
                    sb.Append(decimal.Round(Convert.ToDecimal(lstBill[0].TotalAdvance), 2)).Append("/-</strong></td>");
                    sb.Append("<td style='width:30%;' align='right'><strong>Total Amount :- Rs.</strong></td><td style='width:10%;' align='right'><strong>");
                    sb.Append(decimal.Round(Convert.ToDecimal(lstBill[0].Total), 2)).Append("/-</strong></td></tr>");
                    sb.Append("<tr><td style='width:'15%;' align='right'><strong>Total Received :-</strong></td><td style='width:45%;' align='left'><strong>");
                    sb.Append(decimal.Round(Convert.ToDecimal(lstBill[0].ReceivedAmount), 2)).Append("/-</strong></td>");
                    sb.Append("<td style='width:30%;' align='right'><strong>Discount");
                    sb.Append(" :- </td><td style='width:10%;' align='right'><strong>");
                    sb.Append(decimal.Round(Disc, 2));
                    sb.Append("/-</strong></td></tr>");
                    sb.Append("<tr><td style='width:'15%;' align='right'><strong>Balance Amount :-</strong></td><td colspan='3' align='left'><strong>");
                    sb.Append(decimal.Round(Convert.ToDecimal(lstBill[0].BalanceAmount), 2)).Append("/-</strong></td>");
                    sb.Append("</td>");
                    //    <td style='width:20%;' align='right'><strong>VAT(");
                    //sb.Append(lstBill[0].Vat).Append("%) :- </td><td style='width:20%;' align='right'><strong>");
                    //sb.Append(decimal.Round(Convert.ToDecimal(lstBill[0].Total * lstBill[0].Vat / 100), 2)/-</strong></td>);
                    sb.Append("</tr>");
                    sb.Append("<tr><td style='width:15%;' align='right'><strong>Refund Amount :-</strong></td><td colspan='3' align='left'><strong>");
                    sb.Append(decimal.Round(Convert.ToDecimal(lstBill[0].RefundAmount), 2)).Append("/-</strong></td>");
                    sb.Append("</td></tr>");
                    //<td style='width:20%;' align='right'><strong>Service Tax(");
                    //sb.Append(lstBill[0].Service).Append("%) :- </td><td style='width:20%;' align='right'><strong>");
                    //sb.Append(decimal.Round(Convert.ToDecimal(lstBill[0].Total * lstBill[0].Service / 100), 2));
                    //sb.Append("/-</strong></td></tr>");
                    sb.Append("<tr height='6px'><td colspan='4' style='width:100%;' align='left'><hr></td>");
                    sb.Append("</tr>");
                    sb.Append("<tr><td colspan='2' style='width:60%;' align='left'><strong>Amount In Words:- Rupees ");
                    sb.Append(amt).Append(" Only.</strong></td><td style='width:30%;' align='right'><strong>NetAmount :- Rs.</td><td style='width:10%;' align='right'><strong>");
                    sb.Append(decimal.Round(Convert.ToDecimal(lstBill[0].NetAmount), 2)).Append("/-</strong></td></tr>");
                }
                
                sb.Append("<tr height='15px'><td colspan='2' style='width:60%;' align='left'><strong>Prepared By :-");
                sb.Append(lstBill[0].PreparedByName).Append("</td> <td style='width:20%;' align='right'></td><td style='width:20%;' align='right'><strong></td></tr>");
                if(lstBill[0].PatientType == "IPD")
                {
                    sb.Append("<tr><td colspan='2' style='width:60%;' align='center'><hr style='width:400px;'><strong>Received Signature");
                    sb.Append("</td><td  style='height:130px;' colspan='2' align='center'><hr><strong>Billing Incharge </strong></td></tr>");
                }
                if (lstBill[0].PatientType == "OPD")
                {
                    sb.Append("<tr><td colspan='4'><hr></td></tr>");
                    sb.Append("<tr><td colspan='4' align='center'><strong> ( This is computer generated print signature not required. )</strong></td></tr>");
                }
                
                sb.Append("</table>");
                sb.Append("</td></tr></table>");
                Response.Write(sb.ToString());
            }
            #endregion
            #region Receipt
            else if (ReportType.Equals("Receipt"))
            {
                StringBuilder sb = new StringBuilder();

                List<Hospital.Models.DataLayer.STP_PrintReceiptResult> lstBill = objData.STP_PrintReceipt(Convert.ToInt32(Session["ReceiptNo"])).ToList();
                if (lstBill.Count > 0)
                {
                    if (lstBill[0].IsCheque == true && lstBill[0].PatientCategory=="Self")
                    {
                        string amt = Hospital.Models.DataLayer.Commons.Rupees(Convert.ToDecimal(lstBill[0].PrintAmount));
                        sb.Append("<table align='center' width='1000px' style='border:1px solid;'><tr><td><table width='100%' align='center'><tr width='100%'>");
                        sb.Append("<td style='width:100%;' align='center'> <img src='../images/BannerImages/HospitalNameGNH.png'  alt='#' width='100%' align='Left'/>");
                        sb.Append("<hr></td></tr>");
                        sb.Append("<tr><td colpspan='3' align='center'><strong>");
                        sb.Append("</strong></td></tr>");
                        sb.Append("</table></td></tr><tr width='100%'><td style='width:100%;'><table style='border:1px solid;border-color:Black;' width='100%' align='center'>");
                        sb.Append("<tr width='100%'><td colspan='2' style='width:100%;' align='center'><strong><u>");
                        sb.Append("RECEIPT</u></strong></td></tr><tr width='100%'><td style='width:100%;'><table style='border:1px solid;border-color:Black;' width='100%' align='center'>");
                        sb.Append("<tr><td style='width:60%;' align='left'><strong>Receipt No :- ").Append(lstBill[0].ReceiptNo).Append("</strong></td><td style='width:40%;' align='left'><strong>Receipt Date :- ");
                        sb.Append(string.Format("{0:dd/MM/yyyy}", lstBill[0].ReceiptDate)).Append("</strong></td> </tr> ");
                        if (lstBill[0].PatientType == "IPD")
                        {
                            sb.Append("<tr><td align='left'><strong>Bill Ref. No :-").Append(lstBill[0].BillRefNo).Append("</strong></td><td align='left'><strong>IPD No :- ");
                            sb.Append(lstBill[0].IPDNo).Append("</strong></td></tr>");
                        }
                        if (lstBill[0].PatientType == "OPD")
                        {
                            sb.Append("<tr><td align='left'><strong>Bill Ref. No :-").Append(lstBill[0].BillRefNo).Append("</strong></td><td align='left'><strong>OPD No :- ");
                            sb.Append(lstBill[0].OPDNo).Append("</strong></td></tr>");
                        }
                        sb.Append("<tr><td align='left'><strong>Received From :-").Append(lstBill[0].FullName).Append("</strong></td><td align='left'><strong>MRN :- ");
                        sb.Append(lstBill[0].PatientCode).Append("</strong></td></tr>");
                        sb.Append("<tr><td align='left'><strong>Age :- ").Append(lstBill[0].Age).Append("&nbsp;").Append(lstBill[0].AgeIn).Append("</strong></td><td align='left'><strong>Patient Type :- ").Append(lstBill[0].PatientType).Append("</strong></td> </tr>");
                        sb.Append("<tr><td align='left'><strong>Department :- ").Append(lstBill[0].CategoryName).Append("</strong></td><td align='left'><strong>Consult Doctor :- ").Append(lstBill[0].DeptDoctorName).Append("&nbsp;(").Append(lstBill[0].Education).Append(")").Append("</strong></td></tr></table></td></tr>");
                        sb.Append("<tr width='100%' height='25px'><td align='left'></td></tr>");
                        sb.Append("<tr width='100%'><td align='left'>Transaction Details(Cheque)</td></tr>");
                        sb.Append("<tr width='100%'><td style='width:100%;'><table style='border:1px solid;border-color:Black;' width='100%' align='center'>");

                        sb.Append("<tr><td style='width:60%' align='left'><strong>Cheque No :- ").Append(lstBill[0].ChequeNo).Append("</strong></td><td style='width='40%'' align='left'><strong>Cheque Date :- ").Append(string.Format("{0:dd/MM/yyyy}",lstBill[0].ChequeDate)).Append("</strong></td> </tr>");
                        sb.Append("<tr><td align='left'><strong>Bank Name :-").Append(lstBill[0].BankName).Append("</strong></td></tr>");
                        sb.Append("<tr><td align='left' colsapn='2'><strong>Advance Amount :- Rs.").Append(lstBill[0].AdvanceAmount).Append("/-</strong></td></tr>");
                        sb.Append("<tr><td align='left' colspan='2'><strong>Receipt Amount :- Rs.");
                        sb.Append(lstBill[0].PayAmount).Append("/-</strong></td></tr>");
                        sb.Append("<tr><td align='left' colspan='2'><strong>Rupees In Words :- ").Append(amt).Append(" Only.</strong></td></tr></table></td></tr>");
                        sb.Append("<tr width='100%'><td style='width:100%;'><table style='border:1px solid;border-color:Black;' width='100%' align='center'>");
                        sb.Append("<tr><td colspan='2'></td></tr><tr height='150px'><td align='left' style='width:60%;'><strong>Prepared By :- ").Append(lstBill[0].PreparedByName).Append("</strong></td><td align='center' width:40%;'><strong><hr>Signature</strong></td></tr></table></td></tr></table></td></tr></table>");
                        Response.Write(sb.ToString());
                    }
                    else 
                    {
                        if (lstBill[0].IsCheque == true && lstBill[0].PatientCategory == "Company")
                        {
                            string amt = Hospital.Models.DataLayer.Commons.Rupees(Convert.ToDecimal(lstBill[0].PrintAmount));
                            sb.Append("<table align='center' width='1000px' style='border:1px solid;'><tr><td><table width='100%' align='center'><tr width='100%'>");
                            sb.Append("<td style='width:100%;' align='center'> <img src='../images/BannerImages/HospitalNameGNH.png'  alt='#' width='100%' align='Left'/>");
                            sb.Append("<hr></td></tr>");
                            sb.Append("<tr><td colpspan='3' align='center'><strong>");
                            sb.Append("</strong></td></tr>");
                            sb.Append("</table></td></tr><tr width='100%'><td style='width:100%;'><table style='border:1px solid;border-color:Black;' width='100%' align='center'>");
                            sb.Append("<tr width='100%'><td colspan='2' style='width:100%;' align='center'><strong><u>");
                            sb.Append("RECEIPT</u></strong></td></tr><tr width='100%'><td style='width:100%;'><table style='border:1px solid;border-color:Black;' width='100%' align='center'>");
                            sb.Append("<tr><td style='width:60%;' align='left'><strong>Receipt No :- ").Append(lstBill[0].ReceiptNo).Append("</strong></td><td style='width:40%;' align='left'><strong>Receipt Date :- ");
                            sb.Append(string.Format("{0:dd/MM/yyyy}", lstBill[0].ReceiptDate)).Append("</strong></td> </tr>");
                            if (lstBill[0].PatientType == "IPD")
                            {
                                sb.Append("<tr><td align='left'><strong>Bill Ref. No :-").Append(lstBill[0].BillRefNo).Append("</strong></td><td align='left'><strong>IPD No :- ");
                                sb.Append(lstBill[0].IPDNo).Append("</strong></td></tr>");
                            }
                            if (lstBill[0].PatientType == "OPD")
                            {
                                sb.Append("<tr><td align='left'><strong>Bill Ref. No :-").Append(lstBill[0].BillRefNo).Append("</strong></td><td align='left'><strong>OPD No :- ");
                                sb.Append(lstBill[0].OPDNo).Append("</strong></td></tr>");
                            }
                            sb.Append("<tr><td align='left'><strong>Received From :- ").Append(lstBill[0].FullName).Append("</strong></td><td align='left'><strong>MRN :- ");
                            sb.Append(lstBill[0].PatientCode).Append("</strong></td></tr>");
                            sb.Append("<tr><td align='left'><strong>Age :- ").Append(lstBill[0].Age).Append("&nbsp;(").Append(lstBill[0].AgeIn).Append(")").Append("</strong></td><td align='left'><strong>Patient Type :- ").Append(lstBill[0].PatientType).Append("</strong></td> </tr>");
                            sb.Append("<tr><td align='left'><strong>Department :- ").Append(lstBill[0].CategoryName).Append("</strong></td><td align='left'><strong>Consult Doctor :- ").Append(lstBill[0].DeptDoctorName).Append("&nbsp;").Append(lstBill[0].Education).Append("</strong></td></tr></table></td></tr>");
                            sb.Append("<tr width='100%' height='25px'><td align='left'></td></tr>");
                            sb.Append("<tr width='100%'><td align='left'>Transaction Details(Company Cheque)</td></tr>");
                            sb.Append("<tr width='100%'><td style='width:100%;'><table style='border:1px solid;border-color:Black;' width='100%' align='center'>");

                            sb.Append("<tr><td style='width:60%' align='left'><strong>Patient Category :- ").Append(lstBill[0].PatientCategory).Append("</strong></td><td style='width='40%'' align='left'><strong>Company Name :- ").Append(lstBill[0].CompanyName).Append("</strong></td> </tr>");
                            sb.Append("<tr><td style='width:60%' align='left'><strong>Cheque No :- ").Append(lstBill[0].ChequeNo).Append("</strong></td><td style='width='40%'' align='left'><strong>Cheque Date :- ").Append(string.Format("{0:dd/MM/yyyy}", lstBill[0].ChequeDate)).Append("</strong></td> </tr>");
                            sb.Append("<tr><td align='left'><strong>Bank Name :-").Append(lstBill[0].BankName).Append("</strong></td></tr>");
                            sb.Append("<tr><td align='left' colsapn='2'><strong>Advance Amount :- Rs.").Append(lstBill[0].AdvanceAmount).Append("/-</strong></td></tr>");
                            sb.Append("<tr><td align='left' colspan='2'><strong>Receipt Amount :- Rs.");
                            sb.Append(lstBill[0].PayAmount).Append("/-</strong></td></tr>");
                            sb.Append("<tr><td align='left' colspan='2'><strong>Rupees In Words :- ").Append(amt).Append(" Only.</strong></td></tr></table></td></tr>");
                            sb.Append("<tr width='100%'><td style='width:100%;'><table style='border:1px solid;border-color:Black;' width='100%' align='center'>");
                            sb.Append("<tr><td colspan='2'></td></tr><tr height='150px'><td align='left' style='width:60%;'><strong>Prepared By :- ").Append(lstBill[0].PreparedByName).Append("</strong></td><td align='center' width:40%;'><strong><hr>Signature</strong></td></tr></table></td></tr></table></td></tr></table>");
                            Response.Write(sb.ToString());
                        }

                        if (lstBill[0].IsCheque == true && lstBill[0].PatientCategory == "Insurance")
                        {
                            string amt = Hospital.Models.DataLayer.Commons.Rupees(Convert.ToDecimal(lstBill[0].PrintAmount));
                            sb.Append("<table align='center' width='1000px' style='border:1px solid;'><tr><td><table width='100%' align='center'><tr width='100%'>");
                            sb.Append("<td style='width:100%;' align='center'> <img src='../images/BannerImages/HospitalNameGNH.png'  alt='#' width='100%' align='Left'/>");
                            sb.Append("<hr></td></tr>");
                            sb.Append("<tr><td colpspan='3' align='center'><strong>");
                            sb.Append("</strong></td></tr>");
                            sb.Append("</table></td></tr><tr width='100%'><td style='width:100%;'><table style='border:1px solid;border-color:Black;' width='100%' align='center'>");
                            sb.Append("<tr width='100%'><td colspan='2' style='width:100%;' align='center'><strong><u>");
                            sb.Append("RECEIPT</u></strong></td></tr><tr width='100%'><td style='width:100%;'><table style='border:1px solid;border-color:Black;' width='100%' align='center'>");
                            sb.Append("<tr><td style='width:60%;' align='left'><strong>Receipt No :- ").Append(lstBill[0].ReceiptNo).Append("</strong></td><td style='width:40%;' align='left'><strong>Receipt Date :- ");
                            sb.Append(string.Format("{0:dd/MM/yyyy}", lstBill[0].ReceiptDate)).Append("</strong></td> </tr>");
                            if (lstBill[0].PatientType == "IPD")
                            {
                                sb.Append("<tr><td align='left'><strong>Bill Ref. No :-").Append(lstBill[0].BillRefNo).Append("</strong></td><td align='left'><strong>IPD No :- ");
                                sb.Append(lstBill[0].IPDNo).Append("</strong></td></tr>");
                            }
                            if (lstBill[0].PatientType == "OPD")
                            {
                                sb.Append("<tr><td align='left'><strong>Bill Ref. No :-").Append(lstBill[0].BillRefNo).Append("</strong></td><td align='left'><strong>OPD No :- ");
                                sb.Append(lstBill[0].OPDNo).Append("</strong></td></tr>");
                            }
                            sb.Append("<tr><td align='left'><strong>Received From :- ").Append(lstBill[0].FullName).Append("</strong></td><td align='left'><strong>MRN :- ");
                            sb.Append(lstBill[0].PatientCode).Append("</strong></td></tr>");
                            sb.Append("<tr><td align='left'><strong>Age :- ").Append(lstBill[0].Age).Append("&nbsp;").Append(lstBill[0].AgeIn).Append("</strong></td><td align='left'><strong>Patient Type :- ").Append(lstBill[0].PatientType).Append("</strong></td> </tr>");
                            sb.Append("<tr><td align='left'><strong>Department :- ").Append(lstBill[0].CategoryName).Append("</strong></td><td align='left'><strong>Consult Doctor :- ").Append(lstBill[0].DeptDoctorName).Append("&nbsp;(").Append(lstBill[0].Education).Append(")").Append("</strong></td></tr></table></td></tr>");
                            sb.Append("<tr width='100%' height='25px'><td align='left'></td></tr>");
                            sb.Append("<tr width='100%'><td align='left'>Transaction Details(Insurance Cheque)</td></tr>");
                            sb.Append("<tr width='100%'><td style='width:100%;'><table style='border:1px solid;border-color:Black;' width='100%' align='center'>");

                            sb.Append("<tr><td style='width:60%' align='left'><strong>Patient Category :- ").Append(lstBill[0].PatientCategory).Append("</strong></td><td style='width='40%'' align='left'><strong> Insurance Company Name :- ").Append(lstBill[0].InsuranceName).Append("</strong></td> </tr>");
                            sb.Append("<tr><td style='width:60%' align='left'><strong>Cheque No :- ").Append(lstBill[0].ChequeNo).Append("</strong></td><td style='width='40%'' align='left'><strong>Cheque Date :- ").Append(string.Format("{0:dd/MM/yyyy}", lstBill[0].ChequeDate)).Append("</strong></td> </tr>");
                            sb.Append("<tr><td align='left'><strong>Bank Name :-").Append(lstBill[0].BankName).Append("</strong></td></tr>");
                            sb.Append("<tr><td align='left' colsapn='2'><strong>Advance Amount :- Rs.").Append(lstBill[0].AdvanceAmount).Append("/-</strong></td></tr>");
                            sb.Append("<tr><td align='left' colspan='2'><strong>Receipt Amount :- Rs.");
                            sb.Append(lstBill[0].PayAmount).Append("/-</strong></td></tr>");
                            sb.Append("<tr><td align='left' colspan='2'><strong>Rupees In Words :- ").Append(amt).Append(" Only.</strong></td></tr></table></td></tr>");
                            sb.Append("<tr width='100%'><td style='width:100%;'><table style='border:1px solid;border-color:Black;' width='100%' align='center'>");
                            sb.Append("<tr><td colspan='2'></td></tr><tr height='150px'><td align='left' style='width:60%;'><strong>Prepared By :- ").Append(lstBill[0].PreparedByName).Append("</strong></td><td align='center' width:40%;'><strong><hr>Signature</strong></td></tr></table></td></tr></table></td></tr></table>");
                            Response.Write(sb.ToString());
                        }
                        
                        
                        if (lstBill[0].IsCash == true)
                        {
                            string amt = Hospital.Models.DataLayer.Commons.Rupees(Convert.ToDecimal(lstBill[0].PrintAmount));
                            sb.Append("<table align='center' width='1000px' style='border:1px solid;'><tr><td><table width='100%' align='center'><tr width='100%'>");
                            sb.Append("<td style='width:100%;' align='center'> <img src='../images/BannerImages/HospitalNameGNH.png'  alt='#' width='100%' align='Left'/>");
                            sb.Append("<hr></td></tr>");
                            sb.Append("<tr><td colpspan='3' align='center'><strong>");
                            sb.Append("</strong></td></tr>");
                            sb.Append("</table></td></tr><tr width='100%'><td style='width:100%;'><table style='border:1px solid;border-color:Black;' width='100%' align='center'>");
                            sb.Append("<tr width='100%'><td colspan='2' style='width:100%;' align='center'><strong><u>");
                            sb.Append("RECEIPT</u></strong></td></tr><tr width='100%'><td style='width:100%;'><table style='border:1px solid;border-color:Black;' width='100%' align='center'>");
                            sb.Append("<tr><td style='width:60%;' align='left'><strong>Receipt No :- ").Append(lstBill[0].ReceiptNo).Append("</strong></td><td style='width:40%;' align='left'><strong>Receipt Date :- ");
                            sb.Append(string.Format("{0:dd/MM/yyyy}", lstBill[0].ReceiptDate)).Append("</strong></td> </tr>");
                            if (lstBill[0].PatientType == "IPD")
                            {
                                sb.Append("<tr><td align='left'><strong>Bill Ref. No :-").Append(lstBill[0].BillRefNo).Append("</strong></td><td align='left'><strong>IPD No :- ");
                                sb.Append(lstBill[0].IPDNo).Append("</strong></td></tr>");
                            }
                            if (lstBill[0].PatientType == "OPD")
                            {
                                sb.Append("<tr><td align='left'><strong>Bill Ref. No :-").Append(lstBill[0].BillRefNo).Append("</strong></td><td align='left'><strong>OPD No :- ");
                                sb.Append(lstBill[0].OPDNo).Append("</strong></td></tr>");
                            }
                            sb.Append("<tr><td align='left'><strong>Received From :- ").Append(lstBill[0].FullName).Append("</strong></td><td align='left'><strong>MRN :- ");
                            sb.Append(lstBill[0].PatientCode).Append("</strong></td></tr>");
                            sb.Append("<tr><td align='left'><strong>Age :- ").Append(lstBill[0].Age).Append("&nbsp;").Append(lstBill[0].AgeIn).Append("</strong></td><td align='left'><strong>Patient Type :- ").Append(lstBill[0].PatientType).Append("</strong></td> </tr>");
                            sb.Append("<tr><td align='left'><strong>Department :- ").Append(lstBill[0].CategoryName).Append("</strong></td><td align='left'><strong>Consult Doctor :- ").Append(lstBill[0].DeptDoctorName).Append("&nbsp;(").Append(lstBill[0].Education).Append(")").Append("</strong></td></tr></table></td></tr>");
                            sb.Append("<tr width='100%' height='25px'><td align='left'></td></tr>");
                            sb.Append("<tr width='100%'><td align='left'>Transaction Details(Cash)</td></tr>");
                            sb.Append("<tr width='100%'><td style='width:100%;'><table style='border:1px solid;border-color:Black;' width='100%' align='center'>");
                            sb.Append("<tr><td align='left' colsapn='2'><strong>Advance Amount :- Rs.").Append(lstBill[0].AdvanceAmount).Append("/-</strong></td></tr>");
                            sb.Append("<tr><td align='left' colspan='2'><strong>Receipt Amount :- Rs.");
                            sb.Append(lstBill[0].PayAmount).Append("/-</strong></td></tr>");
                            sb.Append("<tr><td align='left' colspan='2'><strong>Rupees In Words :- ").Append(amt).Append(" Only.</strong></td></tr></table></td></tr>");
                            //    <tr><td colspan='3' align='left'><strong>Date Of Admission :- ");
                            //sb.Append(string.Format("{0:dd/MM/yyyy}", lstBill[0].AdmitDate)).Append("</td></tr>");
                            sb.Append("<tr width='100%'><td style='width:100%;'><table style='border:1px solid;border-color:Black;' width='100%' align='center'>");
                            sb.Append("<tr><td colspan='2'></td></tr><tr height='150px'><td align='left' style='width:60%;'><strong>Prepared By :- ").Append(lstBill[0].PreparedByName).Append("</strong></td><td align='center' width:40%;'><strong><hr>Signature</strong></td></tr></table></td></tr></table></td></tr></table>");
                            Response.Write(sb.ToString());
                        }

                        if (lstBill[0].IsCard == true && lstBill[0].PatientCategory == "Self")
                        {
                            string amt = Hospital.Models.DataLayer.Commons.Rupees(Convert.ToDecimal(lstBill[0].PrintAmount));
                            sb.Append("<table align='center' width='1000px' style='border:1px solid;'><tr><td><table width='100%' align='center'><tr width='100%'>");
                            sb.Append("<td style='width:100%;' align='center'> <img src='../images/BannerImages/HospitalNameGNH.png'  alt='#' width='100%' align='Left'/>");
                            sb.Append("<hr></td></tr>");
                            sb.Append("<tr><td colpspan='3' align='center'><strong>");
                            sb.Append("</strong></td></tr>");
                            sb.Append("</table></td></tr><tr width='100%'><td style='width:100%;'><table style='border:1px solid;border-color:Black;' width='100%' align='center'>");
                            sb.Append("<tr width='100%'><td colspan='2' style='width:100%;' align='center'><strong><u>");
                            sb.Append("RECEIPT</u></strong></td></tr><tr width='100%'><td style='width:100%;'><table style='border:1px solid;border-color:Black;' width='100%' align='center'>");
                            sb.Append("<tr><td style='width:60%;' align='left'><strong>Receipt No :- ").Append(lstBill[0].ReceiptNo).Append("</strong></td><td style='width:40%;' align='left'><strong>Receipt Date :- ");
                            sb.Append(string.Format("{0:dd/MM/yyyy}", lstBill[0].ReceiptDate)).Append("</strong></td> </tr>");
                            if (lstBill[0].PatientType == "IPD")
                            {
                                sb.Append("<tr><td align='left'><strong>Bill Ref. No :-").Append(lstBill[0].BillRefNo).Append("</strong></td><td align='left'><strong>IPD No :- ");
                                sb.Append(lstBill[0].IPDNo).Append("</strong></td></tr>");
                            }
                            if (lstBill[0].PatientType == "OPD")
                            {
                                sb.Append("<tr><td align='left'><strong>Bill Ref. No :-").Append(lstBill[0].BillRefNo).Append("</strong></td><td align='left'><strong>OPD No :- ");
                                sb.Append(lstBill[0].OPDNo).Append("</strong></td></tr>");
                            }
                            sb.Append("<tr><td align='left'><strong>Received From :- ").Append(lstBill[0].FullName).Append("</strong></td><td align='left'><strong>MRN :- ");
                            sb.Append(lstBill[0].PatientCode).Append("</strong></td></tr>");
                            sb.Append("<tr><td align='left'><strong>Age :- ").Append(lstBill[0].Age).Append("&nbsp;").Append(lstBill[0].AgeIn).Append("</strong></td><td align='left'><strong>Patient Type :- ").Append(lstBill[0].PatientType).Append("</strong></td> </tr>");
                            sb.Append("<tr><td align='left'><strong>Department :- ").Append(lstBill[0].CategoryName).Append("</strong></td><td align='left'><strong>Consult Doctor :- ").Append(lstBill[0].DeptDoctorName).Append("&nbsp;(").Append(lstBill[0].Education).Append(")").Append("</strong></td></tr></table></td></tr>");
                            sb.Append("<tr width='100%' height='25px'><td align='left'></td></tr>");
                            sb.Append("<tr width='100%'><td align='left'>Transaction Details(Card)</td></tr>");
                            sb.Append("<tr width='100%'><td style='width:100%;'><table style='border:1px solid;border-color:Black;' width='100%' align='center'>");

                            sb.Append("<tr><td align='left'><strong>Bank Ref. No :-").Append(lstBill[0].BankRefNo).Append("</strong></td></tr>");
                            sb.Append("<tr><td align='left' colsapn='2'><strong>Advance Amount :- Rs.").Append(lstBill[0].AdvanceAmount).Append("/-</strong></td></tr>");
                            sb.Append("<tr><td align='left' colspan='2'><strong>Receipt Amount :- Rs.");
                            sb.Append(lstBill[0].PayAmount).Append("/-</strong></td></tr>");
                            sb.Append("<tr><td align='left' colspan='2'><strong>Rupees In Words :- ").Append(amt).Append(" Only.</strong></td></tr></table></td></tr>");
                            sb.Append("<tr width='100%'><td style='width:100%;'><table style='border:1px solid;border-color:Black;' width='100%' align='center'>");
                            sb.Append("<tr><td colspan='2'></td></tr><tr height='150px'><td align='left' style='width:60%;'><strong>Prepared By :- ").Append(lstBill[0].PreparedByName).Append("</strong></td><td align='center' width:40%;'><strong><hr>Signature</strong></td></tr></table></td></tr></table></td></tr></table>");
                            Response.Write(sb.ToString());
                        }

                        if (lstBill[0].IsCard == true && lstBill[0].PatientCategory == "Company")
                        {
                            string amt = Hospital.Models.DataLayer.Commons.Rupees(Convert.ToDecimal(lstBill[0].PrintAmount));
                            sb.Append("<table align='center' width='1000px' style='border:1px solid;'><tr><td><table width='100%' align='center'><tr width='100%'>");
                            sb.Append("<td style='width:100%;' align='center'> <img src='../images/BannerImages/HospitalNameGNH.png'  alt='#' width='100%' align='Left'/>");
                            sb.Append("<hr></td></tr>");
                            sb.Append("<tr><td colpspan='3' align='center'><strong>");
                            sb.Append("</strong></td></tr>");
                            sb.Append("</table></td></tr><tr width='100%'><td style='width:100%;'><table style='border:1px solid;border-color:Black;' width='100%' align='center'>");
                            sb.Append("<tr width='100%'><td colspan='2' style='width:100%;' align='center'><strong><u>");
                            sb.Append("RECEIPT</u></strong></td></tr><tr width='100%'><td style='width:100%;'><table style='border:1px solid;border-color:Black;' width='100%' align='center'>");
                            sb.Append("<tr><td style='width:60%;' align='left'><strong>Receipt No :- ").Append(lstBill[0].ReceiptNo).Append("</strong></td><td style='width:40%;' align='left'><strong>Receipt Date :- ");
                            sb.Append(string.Format("{0:dd/MM/yyyy}", lstBill[0].ReceiptDate)).Append("</strong></td> </tr>");
                            if (lstBill[0].PatientType == "IPD")
                            {
                                sb.Append("<tr><td align='left'><strong>Bill Ref. No :-").Append(lstBill[0].BillRefNo).Append("</strong></td><td align='left'><strong>IPD No :- ");
                                sb.Append(lstBill[0].IPDNo).Append("</strong></td></tr>");
                            }
                            if (lstBill[0].PatientType == "OPD")
                            {
                                sb.Append("<tr><td align='left'><strong>Bill Ref. No :-").Append(lstBill[0].BillRefNo).Append("</strong></td><td align='left'><strong>OPD No :- ");
                                sb.Append(lstBill[0].OPDNo).Append("</strong></td></tr>");
                            }
                            sb.Append("<tr><td align='left'><strong>Received From :- ").Append(lstBill[0].FullName).Append("</strong></td><td align='left'><strong>MRN :- ");
                            sb.Append(lstBill[0].PatientCode).Append("</strong></td></tr>");
                            sb.Append("<tr><td align='left'><strong>Age :- ").Append(lstBill[0].Age).Append("&nbsp;").Append(lstBill[0].AgeIn).Append("</strong></td><td align='left'><strong>Patient Type :- ").Append(lstBill[0].PatientType).Append("</strong></td> </tr>");
                            sb.Append("<tr><td align='left'><strong>Department :- ").Append(lstBill[0].CategoryName).Append("</strong></td><td align='left'><strong>Consult Doctor :- ").Append(lstBill[0].DeptDoctorName).Append("&nbsp;(").Append(lstBill[0].Education).Append(")").Append("</strong></td></tr></table></td></tr>");
                            sb.Append("<tr width='100%' height='25px'><td align='left'></td></tr>");
                            sb.Append("<tr width='100%'><td align='left'>Transaction Details(Company By Card)</td></tr>");
                            sb.Append("<tr width='100%'><td style='width:100%;'><table style='border:1px solid;border-color:Black;' width='100%' align='center'>");

                            sb.Append("<tr><td style='width:60%' align='left'><strong>Patient Category :- ").Append(lstBill[0].PatientCategory).Append("</strong></td><td style='width='40%'' align='left'><strong>Company Name :- ").Append(lstBill[0].CompanyName).Append("</strong></td> </tr>");
                            sb.Append("<tr><td align='left'><strong>Bank Ref. No :-").Append(lstBill[0].BankName).Append("</strong></td></tr>");
                            sb.Append("<tr><td align='left' colsapn='2'><strong>Advance Amount :- Rs.").Append(lstBill[0].AdvanceAmount).Append("/-</strong></td></tr>");
                            sb.Append("<tr><td align='left' colspan='2'><strong>Receipt Amount :- Rs.");
                            sb.Append(lstBill[0].PayAmount).Append("/-</strong></td></tr>");
                            sb.Append("<tr><td align='left' colspan='2'><strong>Rupees In Words :- ").Append(amt).Append(" Only.</strong></td></tr></table></td></tr>");
                            sb.Append("<tr width='100%'><td style='width:100%;'><table style='border:1px solid;border-color:Black;' width='100%' align='center'>");
                            sb.Append("<tr><td colspan='2'></td></tr><tr height='150px'><td align='left' style='width:60%;'><strong>Prepared By :- ").Append(lstBill[0].PreparedByName).Append("</strong></td><td align='center' width:40%;'><strong><hr>Signature</strong></td></tr></table></td></tr></table></td></tr></table>");
                            Response.Write(sb.ToString());
                        }

                        if (lstBill[0].IsCard == true && lstBill[0].PatientCategory == "Insurance")
                        {
                            string amt = Hospital.Models.DataLayer.Commons.Rupees(Convert.ToDecimal(lstBill[0].PrintAmount));
                            sb.Append("<table align='center' width='1000px' style='border:1px solid;'><tr><td><table width='100%' align='center'><tr width='100%'>");
                            sb.Append("<td style='width:100%;' align='center'> <img src='../images/BannerImages/HospitalNameGNH.png'  alt='#' width='100%' align='Left'/>");
                            sb.Append("<hr></td></tr>");
                            sb.Append("<tr><td colpspan='3' align='center'><strong>");
                            sb.Append("</strong></td></tr>");
                            sb.Append("</table></td></tr><tr width='100%'><td style='width:100%;'><table width='100%' align='center'>");
                            sb.Append("<tr width='100%'><td colspan='2' style='width:100%;' align='center'><strong><u>");
                            sb.Append("RECEIPT</u></strong></td></tr><tr width='100%'><td style='width:100%;'><table style='border:1px solid;' width='100%' align='center'>");
                            sb.Append("<tr><td style='width:60%;' align='left'><strong>Receipt No :- ").Append(lstBill[0].ReceiptNo).Append("</strong></td><td style='width:40%;' align='left'><strong>Receipt Date :- ");
                            sb.Append(string.Format("{0:dd/MM/yyyy}", lstBill[0].ReceiptDate)).Append("</strong></td> </tr>");
                            if (lstBill[0].PatientType == "IPD")
                            {
                                sb.Append("<tr><td align='left'><strong>Bill Ref. No :-").Append(lstBill[0].BillRefNo).Append("</strong></td><td align='left'><strong>IPD No :- ");
                                sb.Append(lstBill[0].IPDNo).Append("</strong></td></tr>");
                            }
                            if (lstBill[0].PatientType == "OPD")
                            {
                                sb.Append("<tr><td align='left'><strong>Bill Ref. No :-").Append(lstBill[0].BillRefNo).Append("</strong></td><td align='left'><strong>OPD No :- ");
                                sb.Append(lstBill[0].OPDNo).Append("</strong></td></tr>");
                            }
                            sb.Append("<tr><td align='left'><strong>Received From :- ").Append(lstBill[0].FullName).Append("</strong></td><td align='left'><strong>MRN :- ");
                            sb.Append(lstBill[0].PatientCode).Append("</strong></td></tr>");
                            sb.Append("<tr><td align='left'><strong>Age :- ").Append(lstBill[0].Age).Append("&nbsp;").Append(lstBill[0].AgeIn).Append("</strong></td><td align='left'><strong>Patient Type :- ").Append(lstBill[0].PatientType).Append("</strong></td> </tr>");
                            sb.Append("<tr><td align='left'><strong>Department :- ").Append(lstBill[0].CategoryName).Append("</strong></td><td align='left'><strong>Consult Doctor :- ").Append(lstBill[0].DeptDoctorName).Append("&nbsp;(").Append(lstBill[0].Education).Append(")").Append("</strong></td></tr></table></td></tr>");
                            sb.Append("<tr width='100%' height='25px'><td align='left'></td></tr>");
                            sb.Append("<tr width='100%'><td align='left'>Transaction Details(Insurance Card)</td></tr>");
                            sb.Append("<tr width='100%'><td style='width:100%;'><table style='border:1px solid;' width='100%' align='center'>");

                            sb.Append("<tr><td style='width:60%' align='left'><strong>Patient Category :- ").Append(lstBill[0].PatientCategory).Append("</strong></td><td style='width='40%'' align='left'><strong> Insurance Company Name :- ").Append(lstBill[0].InsuranceName).Append("</strong></td> </tr>");
                            sb.Append("<tr><td align='left'><strong>Bank Ref No :-").Append(lstBill[0].BankRefNo).Append("</strong></td></tr>");
                            sb.Append("<tr><td align='left' colsapn='2'><strong>Advance Amount :- Rs.").Append(lstBill[0].AdvanceAmount).Append("/-</strong></td></tr>");
                            sb.Append("<tr><td align='left' colspan='2'><strong>Receipt Amount :- Rs.");
                            sb.Append(lstBill[0].PayAmount).Append("/-</strong></td></tr>");
                            sb.Append("<tr><td align='left' colspan='2'><strong>Rupees In Words :- ").Append(amt).Append(" Only.</strong></td></tr></table></td></tr>");
                            sb.Append("<tr width='100%'><td style='width:100%;'><table style='border:1px solid;' width='100%' align='center'>");
                            sb.Append("<tr><td colspan='2'></td></tr><tr height='150px'><td align='left' style='width:60%;'><strong>Prepared By :- ").Append(lstBill[0].PreparedByName).Append("</strong></td><td align='center' width:40%;'><strong><hr>Signature</strong></td></tr></table></td></tr></table></td></tr></table>");
                            Response.Write(sb.ToString());
                        }

                        if (lstBill[0].IsRTGS == true && lstBill[0].PatientCategory == "Self")
                        {
                            string amt = Hospital.Models.DataLayer.Commons.Rupees(Convert.ToDecimal(lstBill[0].PrintAmount));
                            sb.Append("<table align='center' width='1000px' style='border:1px solid;'><tr><td><table width='100%' align='center'><tr width='100%'>");
                            sb.Append("<td style='width:100%;' align='center'> <img src='../images/BannerImages/HospitalNameGNH.png'  alt='#' width='100%' align='Left'/>");
                            sb.Append("<hr></td></tr>");
                            sb.Append("<tr><td colpspan='3' align='center'><strong>");
                            sb.Append("</strong></td></tr>");
                            sb.Append("</table></td></tr><tr width='100%'><td style='width:100%;'><table  width='100%' align='center'>");
                            sb.Append("<tr width='100%'><td colspan='2' style='width:100%;' align='center'><strong><u>");
                            sb.Append("RECEIPT</u></strong></td></tr><tr width='100%'><td style='width:100%;'><table style='border:1px solid;' width='100%' align='center'>");
                            sb.Append("<tr><td style='width:60%;' align='left'><strong>Receipt No :- ").Append(lstBill[0].ReceiptNo).Append("</strong></td><td style='width:40%;' align='left'><strong>Receipt Date :- ");
                            sb.Append(string.Format("{0:dd/MM/yyyy}", lstBill[0].ReceiptDate)).Append("</strong></td> </tr>");
                            if (lstBill[0].PatientType == "IPD")
                            {
                                sb.Append("<tr><td align='left'><strong>Bill Ref. No :-").Append(lstBill[0].BillRefNo).Append("</strong></td><td align='left'><strong>IPD No :- ");
                                sb.Append(lstBill[0].IPDNo).Append("</strong></td></tr>");
                            }
                            if (lstBill[0].PatientType == "OPD")
                            {
                                sb.Append("<tr><td align='left'><strong>Bill Ref. No :-").Append(lstBill[0].BillRefNo).Append("</strong></td><td align='left'><strong>OPD No :- ");
                                sb.Append(lstBill[0].OPDNo).Append("</strong></td></tr>");
                            }
                            sb.Append("<tr><td align='left'><strong>Received From :- ").Append(lstBill[0].FullName).Append("</strong></td><td align='left'><strong>MRN :- ");
                            sb.Append(lstBill[0].PatientCode).Append("</strong></td></tr>");
                            sb.Append("<tr><td align='left'><strong>Age :- ").Append(lstBill[0].Age).Append("&nbsp;").Append(lstBill[0].AgeIn).Append("</strong></td><td align='left'><strong>Patient Type :- ").Append(lstBill[0].PatientType).Append("</strong></td> </tr>");
                            sb.Append("<tr><td align='left'><strong>Department :- ").Append(lstBill[0].CategoryName).Append("</strong></td><td align='left'><strong>Consult Doctor :- ").Append(lstBill[0].DeptDoctorName).Append("&nbsp;(").Append(lstBill[0].Education).Append(")").Append("</strong></td></tr></table></td></tr>");
                            sb.Append("<tr width='100%' height='25px'><td align='left'></td></tr>");
                            sb.Append("<tr width='100%'><td align='left'>Transaction Details(RTGS/NEFT)</td></tr>");
                            sb.Append("<tr width='100%'><td style='width:100%;'><table style='border:1px solid;' width='100%' align='center'>");

                            sb.Append("<tr><td align='left'><strong>Bank Ref. No :-").Append(lstBill[0].BankRefNo).Append("</strong></td></tr>");
                            sb.Append("<tr><td align='left' colsapn='2'><strong>Advance Amount :- Rs.").Append(lstBill[0].AdvanceAmount).Append("/-</strong></td></tr>");
                            sb.Append("<tr><td align='left' colspan='2'><strong>Receipt Amount :- Rs.");
                            sb.Append(lstBill[0].PayAmount).Append("/-</strong></td></tr>");
                            sb.Append("<tr><td align='left' colspan='2'><strong>Rupees In Words :- ").Append(amt).Append(" Only.</strong></td></tr></table></td></tr>");
                            sb.Append("<tr width='100%'><td style='width:100%;'><table style='border:1px solid;' width='100%' align='center'>");
                            sb.Append("<tr><td colspan='2'></td></tr><tr height='150px'><td align='left' style='width:60%;'><strong>Prepared By :- ").Append(lstBill[0].PreparedByName).Append("</strong></td><td align='center' width:40%;'><strong><hr>Signature</strong></td></tr></table></td></tr></table></td></tr></table>");
                            Response.Write(sb.ToString());
                        }

                        if (lstBill[0].IsRTGS == true && lstBill[0].PatientCategory == "Company")
                        {
                            string amt = Hospital.Models.DataLayer.Commons.Rupees(Convert.ToDecimal(lstBill[0].PrintAmount));
                            sb.Append("<table align='center' width='1000px' style='border:1px solid;'><tr><td><table width='100%' align='center'><tr width='100%'>");
                            sb.Append("<td style='width:100%;' align='center'> <img src='../images/BannerImages/HospitalNameGNH.png'  alt='#' width='100%' align='Left'/>");
                            sb.Append("<hr></td></tr>");
                            sb.Append("<tr><td colpspan='3' align='center'><strong>");
                            sb.Append("</strong></td></tr>");
                            sb.Append("</table></td></tr><tr width='100%'><td style='width:100%;'><table width='100%' align='center'>");
                            sb.Append("<tr width='100%'><td colspan='2' style='width:100%;' align='center'><strong><u>");
                            sb.Append("RECEIPT</u></strong></td></tr><tr width='100%'><td style='width:100%;'><table style='border:1px solid;' width='100%' align='center'>");
                            sb.Append("<tr><td style='width:60%;' align='left'><strong>Receipt No :- ").Append(lstBill[0].ReceiptNo).Append("</strong></td><td style='width:40%;' align='left'><strong>Receipt Date :- ");
                            sb.Append(string.Format("{0:dd/MM/yyyy}", lstBill[0].ReceiptDate)).Append("</strong></td> </tr>");
                            if (lstBill[0].PatientType == "IPD")
                            {
                                sb.Append("<tr><td align='left'><strong>Bill Ref. No :-").Append(lstBill[0].BillRefNo).Append("</strong></td><td align='left'><strong>IPD No :- ");
                                sb.Append(lstBill[0].IPDNo).Append("</strong></td></tr>");
                            }
                            if (lstBill[0].PatientType == "OPD")
                            {
                                sb.Append("<tr><td align='left'><strong>Bill Ref. No :-").Append(lstBill[0].BillRefNo).Append("</strong></td><td align='left'><strong>OPD No :- ");
                                sb.Append(lstBill[0].OPDNo).Append("</strong></td></tr>");
                            }
                            sb.Append("<tr><td align='left'><strong>Received From :- ").Append(lstBill[0].FullName).Append("</strong></td><td align='left'><strong>MRN :- ");
                            sb.Append(lstBill[0].PatientCode).Append("</strong></td></tr>");
                            sb.Append("<tr><td align='left'><strong>Age :- ").Append(lstBill[0].Age).Append("&nbsp;").Append(lstBill[0].AgeIn).Append("</strong></td><td align='left'><strong>Patient Type :- ").Append(lstBill[0].PatientType).Append("</strong></td> </tr>");
                            sb.Append("<tr><td align='left'><strong>Department :- ").Append(lstBill[0].CategoryName).Append("</strong></td><td align='left'><strong>Consult Doctor :- ").Append(lstBill[0].DeptDoctorName).Append("&nbsp;(").Append(lstBill[0].Education).Append(")").Append("</strong></td></tr></table></td></tr>");
                            sb.Append("<tr width='100%' height='25px'><td align='left'></td></tr>");
                            sb.Append("<tr width='100%'><td align='left'>Transaction Details(Company By RTGS/NEFT)</td></tr>");
                            sb.Append("<tr width='100%'><td style='width:100%;'><table style='border:1px solid;' width='100%' align='center'>");

                            sb.Append("<tr><td style='width:60%' align='left'><strong>Patient Category :- ").Append(lstBill[0].PatientCategory).Append("</strong></td><td style='width='40%'' align='left'><strong>Company Name :- ").Append(lstBill[0].CompanyName).Append("</strong></td> </tr>");
                            sb.Append("<tr><td align='left'><strong>Bank Ref. No :-").Append(lstBill[0].BankRefNo).Append("</strong></td></tr>");
                            sb.Append("<tr><td align='left' colsapn='2'><strong>Advance Amount :- Rs.").Append(lstBill[0].AdvanceAmount).Append("/-</strong></td></tr>");
                            sb.Append("<tr><td align='left' colspan='2'><strong>Receipt Amount :- Rs.");
                            sb.Append(lstBill[0].PayAmount).Append("/-</strong></td></tr>");
                            sb.Append("<tr><td align='left' colspan='2'><strong>Rupees In Words :- ").Append(amt).Append(" Only.</strong></td></tr></table></td></tr>");
                            sb.Append("<tr width='100%'><td style='width:100%;'><table style='border:1px solid;' width='100%' align='center'>");
                            sb.Append("<tr><td colspan='2'></td></tr><tr height='150px'><td align='left' style='width:60%;'><strong>Prepared By :- ").Append(lstBill[0].PreparedByName).Append("</strong></td><td align='center' width:40%;'><strong><hr>Signature</strong></td></tr></table></td></tr></table></td></tr></table>");
                            Response.Write(sb.ToString());
                        }

                        if (lstBill[0].IsRTGS == true && lstBill[0].PatientCategory == "Insurance")
                        {
                            string amt = Hospital.Models.DataLayer.Commons.Rupees(Convert.ToDecimal(lstBill[0].PrintAmount));
                            sb.Append("<table align='center' width='1000px' style='border:1px solid;'><tr><td><table width='100%' align='center'><tr width='100%'>");
                            sb.Append("<td style='width:100%;' align='center'> <img src='../images/BannerImages/HospitalNameGNH.png'  alt='#' width='100%' align='Left'/>");
                            sb.Append("<hr></td></tr>");
                            sb.Append("<tr><td colpspan='3' align='center'><strong>");
                            sb.Append("</strong></td></tr>");
                            sb.Append("</table></td></tr><tr width='100%'><td style='width:100%;'><table ' width='100%' align='center'>");
                            sb.Append("<tr width='100%'><td colspan='2' style='width:100%;' align='center'><strong><u>");
                            sb.Append("RECEIPT</u></strong></td></tr><tr width='100%'><td style='width:100%;'><table style='border:1px solid;border-color:Black;' width='100%' align='center'>");
                            sb.Append("<tr><td style='width:60%;' align='left'><strong>Receipt No :- ").Append(lstBill[0].ReceiptNo).Append("</strong></td><td style='width:40%;' align='left'><strong>Receipt Date :- ");
                            sb.Append(string.Format("{0:dd/MM/yyyy}", lstBill[0].ReceiptDate)).Append("</strong></td> </tr>");
                            if (lstBill[0].PatientType == "IPD")
                            {
                                sb.Append("<tr><td align='left'><strong>Bill Ref. No :-").Append(lstBill[0].BillRefNo).Append("</strong></td><td align='left'><strong>IPD No :- ");
                                sb.Append(lstBill[0].IPDNo).Append("</strong></td></tr>");
                            }
                            if (lstBill[0].PatientType == "OPD")
                            {
                                sb.Append("<tr><td align='left'><strong>Bill Ref. No :-").Append(lstBill[0].BillRefNo).Append("</strong></td><td align='left'><strong>OPD No :- ");
                                sb.Append(lstBill[0].OPDNo).Append("</strong></td></tr>");
                            }
                            sb.Append("<tr><td align='left'><strong>Received From :- ").Append(lstBill[0].FullName).Append("</strong></td><td align='left'><strong>MRN :- ");
                            sb.Append(lstBill[0].PatientCode).Append("</strong></td></tr>");
                            sb.Append("<tr><td align='left'><strong>Age :- ").Append(lstBill[0].Age).Append("&nbsp;").Append(lstBill[0].AgeIn).Append("</strong></td><td align='left'><strong>Patient Type :- ").Append(lstBill[0].PatientType).Append("</strong></td> </tr>");
                            sb.Append("<tr><td align='left'><strong>Department :- ").Append(lstBill[0].CategoryName).Append("</strong></td><td align='left'><strong>Consult Doctor :- ").Append(lstBill[0].DeptDoctorName).Append("&nbsp;(").Append(lstBill[0].Education).Append(")").Append("</strong></td></tr></table></td></tr>");
                            sb.Append("<tr width='100%' height='25px'><td align='left'></td></tr>");
                            sb.Append("<tr width='100%'><td align='left'>Transaction Details(Insurance RTGS/NEFT)</td></tr>");
                            sb.Append("<tr width='100%'><td style='width:100%;'><table style='border:1px solid;' width='100%' align='center'>");

                            sb.Append("<tr><td style='width:60%' align='left'><strong>Patient Category :- ").Append(lstBill[0].PatientCategory).Append("</strong></td><td style='width='40%'' align='left'><strong> Insurance Company Name :- ").Append(lstBill[0].InsuranceName).Append("</strong></td> </tr>");
                            sb.Append("<tr><td align='left'><strong>Bank Ref No :-").Append(lstBill[0].BankRefNo).Append("</strong></td></tr>");
                            sb.Append("<tr><td align='left' colsapn='2'><strong>Advance Amount :- Rs.").Append(lstBill[0].AdvanceAmount).Append("/-</strong></td></tr>");
                            sb.Append("<tr><td align='left' colspan='2'><strong>Receipt Amount :- Rs.");
                            sb.Append(lstBill[0].PayAmount).Append("/-</strong></td></tr>");
                            sb.Append("<tr><td align='left' colspan='2'><strong>Rupees In Words :- ").Append(amt).Append(" Only.</strong></td></tr></table></td></tr>");
                            sb.Append("<tr width='100%'><td style='width:100%;'><table style='border:1px solid;' width='100%' align='center'>");
                            sb.Append("<tr><td colspan='2'></td></tr><tr height='150px'><td align='left' style='width:60%;'><strong>Prepared By :- ").Append(lstBill[0].PreparedByName).Append("</strong></td><td align='center' width:40%;'><strong><hr>Signature</strong></td></tr></table></td></tr></table></td></tr></table>");
                            Response.Write(sb.ToString());
                        }
                    }
                }
            }
            #endregion
            #region Refund
            else if (ReportType.Equals("Refund"))
            {
                StringBuilder sb = new StringBuilder();

                List<Hospital.Models.DataLayer.STP_PrintRefundReceiptResult> lstBill = objData.STP_PrintRefundReceipt(Convert.ToInt32(Session["ReceiptNo"])).ToList();
                if (lstBill.Count > 0)
                {
                    string amt = Hospital.Models.DataLayer.Commons.Rupees(Convert.ToDecimal(lstBill[0].BillAmount));
                    sb.Append("<table align='center' width='1000px' style='border:1px solid;'><tr><td><table width='100%' align='center'><tr width='100%'>");
                    sb.Append("<td style='width:100%;' align='center'> <img src='../images/BannerImages/HospitalNameGNH.png'  alt='#' width='100%' align='Left'/>");
                    sb.Append("<hr></td></tr><tr><td colpspan='3' align='center'><strong>");
                    sb.Append("</strong></td></tr></table></td></tr><tr width='100%'><td style='width:100%;'>");
                    sb.Append("<table style='border:1px solid;border-color:Black;' width='100%' align='center'><tr width='100%'><td colspan='3' style='width:100%;' align='center'>");
                    sb.Append("<strong><u>REFUND VOUCHER<hr></u></strong></td></tr><tr><td style='width:30%;' align='left'><strong>Voucher No :- ");
                    sb.Append(lstBill[0].ReceiptNo).Append("</strong></td><td align='center'><strong>MRN :- ");
                    sb.Append(lstBill[0].PatientCode).Append("</strong></td><td style='width:35%;' align='left'><strong>Receipt Date :- ");
                    sb.Append(string.Format("{0:dd/MM/yyyy}", lstBill[0].ReceiptDate)).Append("</strong></td> </tr><tr><td colspan='3' align='left'><strong>Received From :- ");
                    sb.Append(lstBill[0].FullName).Append("</strong></td></tr><tr><td align='left'><strong>Age :- ").Append(lstBill[0].Age).Append("</strong></td></tr>");
                    sb.Append("<tr><td colspan='2' align='left'><strong>Rupees In Words :- ").Append(amt).Append("</strong></td>");
                    sb.Append("<td align='left' style='border:1px solid;background-color:LightGray;'><strong>Amount :- Rs.");
                    sb.Append(lstBill[0].BillAmount).Append("/-</strong></td></tr><tr><td colspan='3' align='left'><strong>Date Of Admission :- ");
                    sb.Append(string.Format("{0:dd/MM/yyyy}", lstBill[0].AdmitDate)).Append("</td></tr>");
                    sb.Append("<tr><td colspan='3'></td></tr>");
                    sb.Append("<tr><td colspan='2' align='left'><strong>Prepared By :- ").Append(lstBill[0].PreparedByName).Append("</strong></td>");
                    sb.Append("<td align='center'><strong><hr>Signature</strong></td></tr></table></td></tr></table>");
                    Response.Write(sb.ToString());
                }
            }
            #endregion
            #region ICUInvoice
            else if (ReportType.Equals("ICUInvoice"))
            {
                StringBuilder sb = new StringBuilder();
                Hospital.Models.BusinessLayer.ICUInvoiceBLL objICUInvoice = new Hospital.Models.BusinessLayer.ICUInvoiceBLL();
                string BedNumb = Convert.ToString(Session["Bed_NO"]);
                List<Hospital.Models.DataLayer.sp_GetAllBedAllocICUResult> lstBill = objICUInvoice.GetICUInvoice(Convert.ToInt32(Session["BILLNo"])).ToList();
                List<Hospital.Models.Models.EntityICUInvoiceDetail> lstBedCharge = objICUInvoice.SelectICUInvoiceDetails(Convert.ToInt32(Session["BILLNo"]));
                string amount = Hospital.Models.DataLayer.Commons.Rupees(lstBill[0].NetAmount.Value);
                sb.Append("<table align='center' style='border:1px solid;' width='1000px'><tr height='100'><td height='100'><table width='100%' Height='100%'><tr>");
                sb.Append("<td  width='100%' align='center'><img src='../images/BannerImages/HospitalNameGNH.png'  alt='#' width='100%' align='Left'/>");
                sb.Append("</td></tr><tr><td width='100%' align='center'><strong></strong></td></tr><tr><td  align='center'><strong><u>");
                sb.Append("PATIENT ICU INVOICE<hr></u></strong></td></tr></table></td></tr><tr style='width:100%'><td style='width:90%'><table width='100%'>");
                sb.Append("<tr style='width:100%'><td style='width:50%;' align='left'><strong>Patient Name :- ");
                sb.Append(lstBill[0].FullName).Append("</strong></td><td style='width:50%;' align='right'><strong>Date :- ");
                sb.Append(string.Format("{0:dd/MM/yyyy}", lstBill[0].AllocationDate)).Append("</strong></td></tr><tr><td style='width:50%;colspan='2' align='left'>");
                sb.Append("</td><td style='width:50%;' align='right'><strong>Bed No :- ");
                sb.Append(BedNumb).Append("</strong></td> </tr>");
                sb.Append("<tr><td style='width:50%;' align='left'><strong>Date of Admission:- ");
                sb.Append(string.Format("{0:dd/MM/yyyy}", lstBill[0].AllocationDate)).Append("</strong></td>");
                sb.Append("<td style='width:50%;' align='right'><strong>Date of Discharge :- ");
                sb.Append(string.Format("{0:dd/MM/yyyy}", lstBill[0].AllocationDate)).Append("</strong></td></tr></table></td></tr>");
                sb.Append("<tr  width='100%' height='100%'><td  width='100%' height='100%'><table width='100%' height='100%' border='1'><tr>");
                sb.Append("<td style='width:50%;' align='center'><strong>Particulars</strong></td><td align='center'><strong>Amount</strong></td></tr>");
                foreach (Hospital.Models.Models.EntityICUInvoiceDetail item in lstBedCharge)
                {
                    sb.Append("<tr><td style='border-bottom:hidden !important;' align='center'>");
                    sb.Append(item.ChargesName).Append("</td><td style='border-bottom:hidden !important;' align='center'>");
                    sb.Append(decimal.Round(item.Amount.Value, 2)).Append("</td></tr>");
                }
                sb.Append("</table></td></tr>");
                sb.Append("<tr></td><td><table width='100%'><tr><td width='50%'></td><td width='35%' align='right'><strong>Total Amount :- </strong></td><td align='right'><strong>");
                sb.Append(lstBill[0].TotalAmount).Append("</strong></td></tr>");
                sb.Append("<tr><td width='50%'></td><td width='35%' align='right'><strong>Discount :- </strong></td><td align='right'><strong>");
                sb.Append(lstBill[0].Discount).Append("</strong></td></tr>");
                sb.Append("<tr><td width='50%'></td><td width='35%' align='right'><strong>VAT :- </strong></td><td align='right'><strong>");
                sb.Append(lstBill[0].Tax1).Append("</strong></td></tr>");
                sb.Append("<tr><td width='50%'></td><td width='35%' align='right'><strong>Service Tax :- </strong></td><td align='right'><strong>");
                sb.Append(lstBill[0].Tax2).Append("</strong></td></tr>");
                sb.Append("<tr><td width='50%'><strong>Amount In Words:-" + amount + "</strong></td><td width='35%' align='right'><strong>NetAmount :- </strong></td><td align='right'><strong>");
                sb.Append(lstBill[0].NetAmount).Append("</strong></td></tr><tr><td colspan='2' height='70px'></td></tr><tr><td></td><td colspan='2' align='right'>");
                sb.Append("<strong><hr>Accountant Sign<strong></td></tr></table></td></tr></table>");
                Response.Write(sb.ToString());
            }
            #endregion
            #region TestInvoice
            else if (ReportType.Equals("TestInvoice"))
            {
                StringBuilder sb = new StringBuilder();
                List<Hospital.Models.DataLayer.STP_PrintTestInvoiceResult> lstBill = objData.STP_PrintTestInvoice(Convert.ToInt32(Session["TestInvoiceID"])).ToList();
                string amt = Hospital.Models.DataLayer.Commons.Rupees(Convert.ToDecimal(lstBill[0].NetAmount));

                sb.Append("<table align='center' style='border:1px solid;' width='1000px'><tr height='100'><td height='100'><table width='100%' Height='100%'><tr>");
                sb.Append("<td  width='100%' align='center'><img src='../images/BannerImages/HospitalNameGNH.png'  alt='#' width='100%' align='Left'/>");
                sb.Append("</td></tr><tr><td width='100%' align='center'><strong></strong></td></tr></table></td></tr>");
                sb.Append("<tr style='width:100%'><td style='width:90%'><table width='100%'><tr style='width:100%'><td colspan='2' align='center'><strong></u>");
                sb.Append("PATHOLOGY INVOICE REPORT</u></strong></td></tr><tr style='width:100%'><td style='width:50%;' align='left'><strong>Patient Name :- ");
                sb.Append(lstBill[0].PatientNAme).Append("</strong></td><td style='width:50%;' align='right'><strong>Date :- ");
                sb.Append(string.Format("{0:dd/MM/yyyy}", lstBill[0].TestInvoiceDate)).Append("</strong></td></tr><tr><td style='width:50%;colspan='2' align='left'><strong>Address :- ");
                sb.Append(lstBill[0].Address).Append("</strong></td><td style='width:50%;' align='right'><strong>MRN :- ");
                sb.Append(lstBill[0].PatientCode).Append("</strong></td> </tr> </table></td></tr>");
                sb.Append("<tr  width='100%' height='100%'><td  width='100%' height='100%'><table width='100%' height='100%' border='1'><tr><td align='center'>");
                sb.Append("<strong>Particulars</strong></td><td align='center'><strong>Amount</strong></td></tr>");
                foreach (Hospital.Models.DataLayer.STP_PrintTestInvoiceResult item in lstBill)
                {
                    sb.Append("<tr><td align='center'style='border-bottom:hidden !important;'>");
                    sb.Append(item.TestName).Append("</td><td align='center'style='border-bottom:hidden !important;'>");
                    sb.Append(decimal.Round(item.Charges.Value, 2)).Append("</td></tr>");
                }
                sb.Append("</table></td></tr>");
                sb.Append("<tr style='width:100%'><td style='width:100%' align='Right'><table width='100%'><tr style='width:100%'><td style='width:'70%;' align='left'><strong>");
                sb.Append("</strong></td><td style='width:30%;' align='right'><strong>Total Amount :- Rs.");
                sb.Append(decimal.Round(lstBill[0].TotalAmount.Value, 2)).Append("/-</strong></td></tr>");
                sb.Append("<tr><td style='width:70%;' align='left'>");
                sb.Append("</td><td style='width:30%;' align='right'><strong>Discount :- Rs.");
                sb.Append(decimal.Round(lstBill[0].Discount.Value, 2)).Append("/-</strong></td></tr>");
                sb.Append("<tr><td style='width:70%;' align='left'><strong>Rupees In Words:-");
                sb.Append(amt).Append("</strong></td><td style='width:30%;' align='right'><strong>NetAmount :- Rs.");
                sb.Append(decimal.Round(lstBill[0].NetAmount.Value, 2)).Append("/-</strong></td></tr>");
                sb.Append("<tr><td style='width:70%;' align='left'></td> <td style='width:30%;' align='right'></td></tr>");
                sb.Append("<tr><td style='width:70%;' align='left'></td> <td style='width:30%;' align='right'></td></tr>");
                sb.Append("<tr><td style='width:70%;height:70px;' align='left'>");
                sb.Append("</td><td style='width:30%;' align='right'></td></tr>");
                sb.Append("<tr><td style='width:70%;' align='left'>");
                sb.Append("</td><td  style='width:30%;' align='right'><hr><strong>Accountant Signature");
                sb.Append("</strong></td></tr></table></td></tr></table>");
                Response.Write(sb.ToString());
            }
            #endregion

            #region OPDPaper
            else if (ReportType.Equals("OPDPaper"))
            {
                StringBuilder sb = new StringBuilder();
                List<Hospital.Models.DataLayer.STP_GetOPDPaperDetailResult> lstBill = objData.STP_GetOPDPaperDetail(Hospital.Models.DataLayer.QueryStringManager.Instance.AdmitId).ToList();
                if (lstBill[0].PatientType == "IPD")
                {
                    sb.Append("<div style='border:1px solid;height:1260px;width:900px;margin-top:200px'><table align='center' style='border:1px solid;' width='900px'><tr height='100'><td height='100'><table width='100%' Height='100%'><tr>");
                    sb.Append("<td  width='100%' align='center'><img src='../images/BannerImages/HospitalNameGNH.png'  alt='#' width='100%' align='Left'/>");
                    sb.Append("</td></tr><tr><td width='100%' align='center'><strong>");
                    sb.Append("</strong></td></tr>");
                    sb.Append("<tr><td width='100%' align='center'> <hr></td></tr>");
                    sb.Append("<tr><td width='100%' align='center'><strong><u>");
                    sb.Append("OPD PAPER</u></strong></td></tr>");
                    sb.Append("</table></td></tr><tr style='width:100%'><td style='width:90%'>");
                    sb.Append("<table width='100%'>");
                    sb.Append("<tr style='width:100%'>");
                    sb.Append("<td style='width:13%;' align='left'><strong>MRN NO </strong></td><td style='width:45%;' align='left'><strong>:- ");
                    sb.Append(lstBill[0].PatientCode).Append("</strong></td>");
                    sb.Append("<td style='width:15%;' align='left'><strong>Date </strong></td><td style='width:27%;' align='left'><strong> :- ");
                    sb.Append(string.Format("{0:dd/MM/yyyy HH:mm}", lstBill[0].AdmitDate)).Append("</strong></td></tr>");
                    sb.Append("<tr><td style='width:13%;' align='left'><strong>Patient Name </strong></td><td style='width:45%;' align='left'><strong>:- ");
                    sb.Append(lstBill[0].InitialDesc).Append("&nbsp;");
                    sb.Append(lstBill[0].PatName).Append("</strong></td>");
                    sb.Append("<td style='width:15%;' align='left'><strong>IPD No </strong></td><td style='width:27%;' align='left'><strong> :- ");
                    sb.Append("").Append("</strong></td></tr>");
                    sb.Append("<tr><td style='width:13%;' align='left'><strong>Address </strong></td><td style='width:45%;' align='left'><strong>:- ");
                    sb.Append(lstBill[0].Address).Append("</strong></td><td style='width:15%;' align='left'><strong>Mobile No</strong>");
                    sb.Append("</td><td style='width:27%;' align='left'><strong> :- ");
                    sb.Append(lstBill[0].ContactNo).Append("</strong></td> </tr><tr><td style='width:13%;' align='left'><strong>Age</strong></td><td style='width:50%;' align='left'><strong> :- ");
                    sb.Append(lstBill[0].Age).Append("&nbsp;&nbsp;").Append(lstBill[0].AgeIn).Append("</strong></td>");
                    sb.Append("<td style='width:15%;' align='left'><strong>Gender</strong></td><td style='width:27%;' align='left'><strong> :- ");
                    sb.Append(lstBill[0].GenderDesc).Append("</strong></td></tr>");
                    sb.Append("<tr><td style='width:13%;' align='left'><strong>Weight</strong></td><td style='width:45%;' align='left'><strong> :- ");
                    sb.Append(lstBill[0].Weight).Append("</strong></td>");
                    sb.Append("<td style='width:15%;' align='left'><strong>Department</strong></td><td style='width:27%;' align='left'><strong> :- ");
                    sb.Append(lstBill[0].CategoryName).Append("</strong></td></tr>");
                    sb.Append("<tr><td style='width:13%;' align='left'><strong>Referred By</strong></td><td style='width:45%;' align='left'><strong>:- ");
                    sb.Append(lstBill[0].ReferedBy).Append("</strong></td>");
                    sb.Append("<td style='width:15%;' align='left'><strong>Consult Doctor</strong></td><td style='width:27%;' align='left'><strong> :- ");
                    sb.Append(lstBill[0].DeptDoctorName).Append("</strong></td></tr>");
                    sb.Append("<tr><td colspan='3'><strong></strong></td><td align='left'><strong> &nbsp;( ");
                    sb.Append(lstBill[0].Education).Append("&nbsp;)</strong></td></tr>");
                    sb.Append("</table></td></tr>");
                    sb.Append("</td></tr></table></div>");
                    sb.Append("<div style='border:1px border-top:none; solid;height:40px;width:900px;'><table align='left' width:890px;><tr><td align='left'>Consultants Name : <strong>");
                    sb.Append(lstBill[0].DeptDoctorName).Append(" (").Append(lstBill[0].Education).Append(")</strong></td></tr>");
                    sb.Append("<tr><td align='left'>Registration No ").Append(" &nbsp;&nbsp;&nbsp; : <strong>");
                    sb.Append(lstBill[0].RegNo).Append("</strong></td></tr></table></div>");
                }
                else
                {
                    sb.Append("Please select OPD Type Patient For OPD Paper. This Patient is IPD Type....");
                }
                Response.Write(sb.ToString());
            }
            #endregion    
           

            #region AdmitCard
            else if (ReportType.Equals("AdmitCard"))
            {
                StringBuilder sb = new StringBuilder();
                List<Hospital.Models.DataLayer.STP_GetAdmitCardResult> lstDischarge = objData.STP_GetAdmitCard(Convert.ToInt32(Session["AdmitId"])).ToList();
                //DateTime Today = DateTime.Now.Date;
                //DateTime Birth = DateTime.Parse(Convert.ToString(lstDischarge[0].BirthDate));
                //int days = Today.Subtract(Birth).Days;
                //int Age = Convert.ToInt32(Math.Floor(days / 365.24219));
                sb.Append("<div border='1px' style='border-radius:25px; width:740px; height:350px;'>");
                sb.Append("<table background='../images/AdmitCard.png' width='100%' height='100%' style='padding-bottom:20px;border:1px solid; border-radius:25px;'>");
                sb.Append("<tr style='height:15px;'><td align='center' colspan='2'>");
                sb.Append("<img src='../images/BannerImages/HospitalNameGNH.png' alt='#' width='100%' height='100px'align='center' style='border-top-left-radius: 15px; border-top-right-radius: 15px;' />");
                sb.Append("</td></tr><tr style='height:15px;'><td align='center' colspan='2'><strong></strong></td></tr><tr style='height:15px;'><td align='center' colspan='2'>");
                sb.Append("<strong><u>ADMIT CARD</u></strong></td></tr><tr style='height:15px;'><td style='width: 50%;' align='left'><strong>Patient Name :- ");
                sb.Append(lstDischarge[0].PatName).Append("</strong></td><td></td></tr><tr style='height:15px;'><td style='width: 50%;' align='left'><strong>Address :- ");
                sb.Append(lstDischarge[0].Address).Append("</strong</td></tr><tr style='height:15px;'><td style='width: 50%;' colspan='2' align='left'><strong>Age :- ");
                sb.Append(lstDischarge[0].Age).Append(lstDischarge[0].AgeIn).Append("</strong></td></tr><tr style='height:15px;'>");
                sb.Append("<td style='width: 50%;' align='left'><strong>MRN :-  ");
                sb.Append(lstDischarge[0].PatientCode).Append("</strong></td></tr><tr><td style='width: 50%;' align='left'><strong>Sex :- ");
                sb.Append(lstDischarge[0].GenderDesc).Append("</strong></td></tr><tr style='height:15px;'><td style='width: 50%;' align='left'><strong>Date Of Admission :- ");
                sb.Append(lstDischarge[0].AdmitDate).Append("</strong></td></tr><tr style='height:15px;'><td style='width: 50%; border-bottom: none;' align='left'>");
                sb.Append("<strong>Diagnosis :- ");
                sb.Append(lstDischarge[0].Dignosys).Append("</strong></td><td style='width: 50%; border-bottom: none;' align='center'><strong>Signature</strong></td></tr>");
                sb.Append("</table></div>");
                Response.Write(sb.ToString());
            }
            #endregion

            #region AdmitCardNew
            else if (ReportType.Equals("AdmitCardNew"))
            {
                StringBuilder sb = new StringBuilder();
                List<Hospital.Models.DataLayer.STP_GetAdmitCardResult> lstDischarge = objData.STP_GetAdmitCard(Convert.ToInt32(Session["AdmitId"])).ToList();
                //DateTime Today = DateTime.Now.Date;
                //DateTime Birth = DateTime.Parse(Convert.ToString(lstDischarge[0].BirthDate));
                //int days = Today.Subtract(Birth).Days;
                //int Age = Convert.ToInt32(Math.Floor(days / 365.24219));
                sb.Append("<div border='1px' style='border-radius:25px; width:740px; height:350px;'>");
                sb.Append("<table background='../images/AdmitCard.png' width='100%' height='100%' style='padding-bottom:20px;border:1px solid; border-radius:25px;'>");
                sb.Append("<tr style='height:15px;'><td align='center' colspan='2'>");
                sb.Append("<img src='../images/BannerImages/HospitalNameGNH.png' alt='#' width='100%' height='100px'align='center' style='border-top-left-radius: 15px; border-top-right-radius: 15px;' />");
                sb.Append("</td></tr><tr style='height:15px;'><td align='center' colspan='2'><strong></strong></td></tr><tr style='height:15px;'><td align='center' colspan='2'>");
                sb.Append("<strong><u>ADMIT CARD</u></strong></td></tr><tr style='height:15px;'><td style='width: 50%;' align='left'><strong>Patient Name :- ");
                sb.Append(lstDischarge[0].PatName).Append("</strong></td><td></td></tr><tr style='height:15px;'><td style='width: 50%;' align='left'><strong>Address :- ");
                sb.Append(lstDischarge[0].Address).Append("</strong</td></tr><tr style='height:15px;'><td style='width: 50%;' colspan='2' align='left'><strong>Age :- ");
                sb.Append(lstDischarge[0].Age).Append(lstDischarge[0].AgeIn).Append("</strong></td></tr><tr style='height:15px;'>");
                sb.Append("<td style='width: 50%;' align='left'><strong>MRN :-  ");
                sb.Append(lstDischarge[0].PatientCode).Append("</strong></td></tr><tr><td style='width: 50%;' align='left'><strong>Sex :- ");
                sb.Append(lstDischarge[0].GenderDesc).Append("</strong></td></tr><tr style='height:15px;'><td style='width: 50%;' align='left'><strong>Date Of Admission :- ");
                sb.Append(lstDischarge[0].AdmitDate).Append("</strong></td></tr><tr style='height:15px;'><td style='width: 50%; border-bottom: none;' align='left'>");
                sb.Append("<strong>Diagnosis :- ");
                sb.Append(lstDischarge[0].Dignosys).Append("</strong></td><td style='width: 50%; border-bottom: none;' align='center'><strong>Signature</strong></td></tr>");
                sb.Append("</table></div>");

                sb.Append("<div style='width:740px; height:150px;'></div>");
                
                sb.Append("<div border='1px' style='border-radius:25px; width:740px; height:350px;'>");
                sb.Append("<table background='../images/AdmitCard.png' width='100%' height='100%' style='padding-bottom:20px;border:1px solid; border-radius:25px;'>");
                sb.Append("<tr style='height:15px;'><td align='center' colspan='2'>");
                sb.Append("<img src='../images/BannerImages/HospitalNameGNH.png' alt='#' width='100%' height='100px'align='center' style='border-top-left-radius: 15px; border-top-right-radius: 15px;' />");
                sb.Append("</td></tr><tr style='height:15px;'><td align='center' colspan='2'><strong></strong></td></tr><tr style='height:15px;'><td align='center' colspan='2'>");
                sb.Append("<strong><u>ADMIT CARD</u></strong></td></tr><tr style='height:15px;'><td style='width: 50%;' align='left'><strong>Patient Name :- ");
                sb.Append(lstDischarge[0].PatName).Append("</strong></td><td></td></tr><tr style='height:15px;'><td style='width: 50%;' align='left'><strong>Address :- ");
                sb.Append(lstDischarge[0].Address).Append("</strong</td></tr><tr style='height:15px;'><td style='width: 50%;' colspan='2' align='left'><strong>Age :- ");
                sb.Append(lstDischarge[0].Age).Append(lstDischarge[0].AgeIn).Append("</strong></td></tr><tr style='height:15px;'>");
                sb.Append("<td style='width: 50%;' align='left'><strong>MRN :-  ");
                sb.Append(lstDischarge[0].PatientCode).Append("</strong></td></tr><tr><td style='width: 50%;' align='left'><strong>Sex :- ");
                sb.Append(lstDischarge[0].GenderDesc).Append("</strong></td></tr><tr style='height:15px;'><td style='width: 50%;' align='left'><strong>Date Of Admission :- ");
                sb.Append(lstDischarge[0].AdmitDate).Append("</strong></td></tr><tr style='height:15px;'><td style='width: 50%; border-bottom: none;' align='left'>");
                sb.Append("<strong>Diagnosis :- ");
                sb.Append(lstDischarge[0].Dignosys).Append("</strong></td><td style='width: 50%; border-bottom: none;' align='center'><strong>Signature</strong></td></tr>");
                sb.Append("</table></div>");

                sb.Append("<div style='width:740px; height:150px;'></div>");

                sb.Append("<div border='1px' style='border-radius:25px; width:740px; height:350px;'>");
                sb.Append("<table background='../images/AdmitCard.png' width='100%' height='100%' style='padding-bottom:20px;border:1px solid; border-radius:25px;'>");
                sb.Append("<tr style='height:15px;'><td align='center' colspan='2'>");
                sb.Append("<img src='../images/BannerImages/HospitalNameGNH.png' alt='#' width='100%' height='100px'align='center' style='border-top-left-radius: 15px; border-top-right-radius: 15px;' />");
                sb.Append("</td></tr><tr style='height:15px;'><td align='center' colspan='2'><strong></strong></td></tr><tr style='height:15px;'><td align='center' colspan='2'>");
                sb.Append("<strong><u>ADMIT CARD</u></strong></td></tr><tr style='height:15px;'><td style='width: 50%;' align='left'><strong>Patient Name :- ");
                sb.Append(lstDischarge[0].PatName).Append("</strong></td><td></td></tr><tr style='height:15px;'><td style='width: 50%;' align='left'><strong>Address :- ");
                sb.Append(lstDischarge[0].Address).Append("</strong</td></tr><tr style='height:15px;'><td style='width: 50%;' colspan='2' align='left'><strong>Age :- ");
                sb.Append(lstDischarge[0].Age).Append(lstDischarge[0].AgeIn).Append("</strong></td></tr><tr style='height:15px;'>");
                sb.Append("<td style='width: 50%;' align='left'><strong>MRN :-  ");
                sb.Append(lstDischarge[0].PatientCode).Append("</strong></td></tr><tr><td style='width: 50%;' align='left'><strong>Sex :- ");
                sb.Append(lstDischarge[0].GenderDesc).Append("</strong></td></tr><tr style='height:15px;'><td style='width: 50%;' align='left'><strong>Date Of Admission :- ");
                sb.Append(lstDischarge[0].AdmitDate).Append("</strong></td></tr><tr style='height:15px;'><td style='width: 50%; border-bottom: none;' align='left'>");
                sb.Append("<strong>Diagnosis :- ");
                sb.Append(lstDischarge[0].Dignosys).Append("</strong></td><td style='width: 50%; border-bottom: none;' align='center'><strong>Signature</strong></td></tr>");
                sb.Append("</table></div>");
                
                Response.Write(sb.ToString());
            }
            #endregion
                    
            #region Discharge
            else if (ReportType.Equals("Discharge"))
            {
                StringBuilder sb = new StringBuilder();
                List<Hospital.Models.DataLayer.STP_GetOperaConsultResult> lstOperCon = objData.STP_GetOperaConsult(Hospital.Models.DataLayer.QueryStringManager.Instance.AdmitId).ToList();
                List<Hospital.Models.DataLayer.STP_GetDischargeTestResult> lstDisTest = objData.STP_GetDischargeTest(Hospital.Models.DataLayer.QueryStringManager.Instance.AdmitId).ToList();
                List<Hospital.Models.DataLayer.STP_GetDischargeDetailsResult> lstDischarge = objData.STP_GetDischargeDetails(Hospital.Models.DataLayer.QueryStringManager.Instance.DischargeId).ToList();
                DateTime Today = DateTime.Now.Date;
                if (lstDischarge.Count > 0)
                {
                    sb.Append("<div style='height: auto; width: 695px; margin-left:70px'><table style='width: 100%; height: 200px;'><tr><td align='center'>");
                    sb.Append("<img src='../images/BannerImages/HospitalNameGNH.png' alt='#' width='100%' height='100%' align='center' /></td></tr>");
                    sb.Append("<tr><td align='center'><strong></strong><hr>");
                    sb.Append("</td></tr><tr><td align='center'><strong><u>Discharge Report</u></strong></td></tr></table><hr style='color: #000000;height: 1px;background: #000000;'><div style='height: auto; width: 100%; margin-top: 0; margin-left:20px'>");
                    sb.Append("<table width='100%'>");
                    sb.Append("<tr style='height:30px'>");
                    sb.Append("<td width='25%' align='left'><strong>MRN :-</strong></td><td width='25%' align='left'>").Append(lstDischarge[0].PatientCode).Append("</td><td width='25%' align='left'><strong>IPD Reg. No. :-</strong></td><td width='25%' align='left'>").Append(lstDischarge[0].IPDNo).Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr style='height:30px'><td width='25%' align='left'><strong>Patient Name :-</strong></td><td width='25%' align='left'>").Append(lstDischarge[0].PatName).Append("</td><td width='25%' align='left'><strong>Age / Sex :-</strong></td><td width='25%' align='left'>").Append(lstDischarge[0].Age).Append("&nbsp").Append(lstDischarge[0].AgeIn).Append("<strong>/</strong>").Append(lstDischarge[0].GenderDesc).Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr style='height:30px'>");
                    sb.Append("<td width='25%' align='left'><strong>Address :-</strong></td><td width='25%' align='left'>").Append(lstDischarge[0].Address).Append("</td><td width='25%' align='left'><strong>Tel. No. :-</strong></td><td width='25%' align='left'>").Append(lstDischarge[0].ContactNo).Append("</td>");
                    sb.Append("</tr>");
                    //sb.Append("<tr style='height:30px'>");
                    //sb.Append("<td width='25%' align='left'><strong>Age :-</strong></td><td width='25%' align='left'>").Append(lstDischarge[0].Age).Append(lstDischarge[0].AgeIn).Append("</td><td width='25%' align='left'><strong>Sex :-</strong></td><td width='25%' align='left'>").Append(lstDischarge[0].GenderDesc).Append("</td>");
                    //sb.Append("</tr>");
                    sb.Append("<tr style='height:30px'>");
                    sb.Append("<td width='25%' align='left'><strong>Referred by Dr. :-</strong></td><td width='25%' align='left'> ").Append(lstDischarge[0].ReferedBy).Append("</td><td width='25%' align='left'><strong>Blood Group :</strong></td><td width='25%' align='left'>");
                    sb.Append(lstDischarge[0].BloodGroup == "0" ? "" : lstDischarge[0].BloodGroup);
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr style='height:30px'>");
                    sb.Append("<td width='25%' align='left'><strong>DOA :-</strong></td><td width='25%' align='left'>").Append(string.Format("{0:dd-MMM-yyyy HH:mm}", lstDischarge[0].AdmitDate)).Append("</td><td width='25%' align='left'><strong>DOD :-</strong></td><td width='25%' align='left'>").Append(string.Format("{0:dd-MMM-yyyy HH:mm}", lstDischarge[0].DischargeReceiptDate)).Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr style='height:30px'><td width='25%' align='left'><strong>Consultant :-</strong></td><td width='25%' align='left'>").Append(lstDischarge[0].EmpName).Append("</td><td width='25%' align='left'><strong>Department :</strong></td><td width='25%' align='left'>");
                    sb.Append(lstDischarge[0].CategoryName);
                    sb.Append("</td></tr>");
                    //sb.Append("<tr style='height:30px'>");
                    //sb.Append("<td width='25%' align='left'><strong> Final Diagnosis :-</strong></td><td width='25%' align='left'>").Append(lstDischarge[0].Diagnosis).Append("</td><td width='25%' align='left'></td><td width='25%' align='left'>");
                    //sb.Append("</td>");
                    //sb.Append("</tr>");
                    //sb.Append("<tr style='height:30px'>");
                    //sb.Append("<td width='25%' align='left'><strong>Name Of Surgery :-</strong></td><td width='25%' align='left'>").Append(lstDischarge[0].NameOfSurgery).Append("</td><td width='25%' align='left'></td><td width='25%' align='left'></td>");
                    //sb.Append("</tr>");
                    //sb.Append("<tr style='height:30px'>");
                    //sb.Append("<td width='25%' align='left'><strong>Operational Procedure</strong></td><td width='75%' align='left' colspan='3'>").Append(lstDischarge[0].OperationalProcedure).Append("</td>");
                    //sb.Append("</tr>");
                    sb.Append("</table><hr style='color: #000000;height: 1px;background: #000000;>");
                    sb.Append("<div>");
                    sb.Append("<p style='font-weight:normal;' align='justify'><strong>");
                    sb.Append("<u>Final Diagnosis : </u></strong>").Append(" </p>");
                    sb.Append("<p style='font-weight:normal;' align='justify'>").Append(lstDischarge[0].Diagnosis).Append("</p>");
                    sb.Append("<p style='font-weight:normal;' align='justify'><strong>");
                    sb.Append("<u>Name Of Surgery : </u></strong>").Append(" </p>");
                    sb.Append("<p style='font-weight:normal;' align='justify'>").Append(lstDischarge[0].NameOfSurgery).Append("</p>");
                    sb.Append("<p style='font-weight:normal;' align='justify'><strong>");
                    sb.Append("<u>Operational Procedure : </u></strong>").Append(" </p>");
                    sb.Append("<p style='font-weight:normal;' align='justify'>").Append(lstDischarge[0].OperationalProcedure).Append("</p>");
                    sb.Append("<p style='font-weight:bold;' align='justify'><strong>");
                    sb.Append("<u>Complaints & History :</u> </strong></p>");
                    sb.Append("<div style='height: auto;'>");
                    sb.Append("<p align='justify'>");
                    string[] strReplace = lstDischarge[0].HistoryClinical.Split('\n');
                    string traet = string.Empty;
                    for (int i = 0; i < strReplace.Length; i++)
                    {
                        if (i == 0)
                        {
                            traet = traet + strReplace[i];
                        }
                        else
                        {
                            traet = traet + "<br>" + strReplace[i];
                        }
                    }
                    sb.Append(traet).Append("</p></div>");
                    
                    sb.Append("<div style='height: auto;'></div><table width='100%'><tr><td colspan='4' align='left'><strong><u>Vital Parameters :</u></strong></td></tr>");
                    sb.Append("<tr><td colspan='4' align='left'>● <strong>Temp :</strong>");
                    sb.Append(lstDischarge[0].Temp);
                    sb.Append("°F. &nbsp;&nbsp;&nbsp;&nbsp;● <strong>Pulse : </strong>");
                    sb.Append(lstDischarge[0].Pulse);
                    sb.Append("/min.  &nbsp;&nbsp;&nbsp;&nbsp;● <strong>B.P. : </strong>");
                    sb.Append(lstDischarge[0].BP);
                    sb.Append("mm of Hg. &nbsp;&nbsp;&nbsp;&nbsp;● <strong>Resp. Rate :</strong>");
                    sb.Append(lstDischarge[0].RespRate);
                    sb.Append("/min.");
                    sb.Append("</td></tr>");
                    
                    sb.Append("<tr><td colspan='4' align='left'><br><strong><u>General Examination :</u></strong></td></tr>");
                    sb.Append("<tr><td colspan='4' align='left'>● <strong>Pallor :</strong>&nbsp;&nbsp;");
                    sb.Append(lstDischarge[0].Pallor);
                    sb.Append("&nbsp;&nbsp;&nbsp;&nbsp; ● <strong>Oedema :</strong> &nbsp;&nbsp;");
                    sb.Append(lstDischarge[0].Oedema);
                    sb.Append(" &nbsp;&nbsp;&nbsp;&nbsp; ● <strong>Cyanosis : </strong>&nbsp;&nbsp;");
                    sb.Append(lstDischarge[0].Cyanosis);
                    sb.Append("</td></tr>");
                    sb.Append("<tr><td colspan='4' align='left'>● <strong>Clubbing :</strong>&nbsp;&nbsp;");
                    sb.Append(lstDischarge[0].Clubbing);
                    sb.Append("&nbsp;&nbsp;&nbsp;&nbsp; ● <strong>Icterus : </strong>&nbsp;&nbsp;");
                    sb.Append(lstDischarge[0].Icterus);
                    sb.Append(" &nbsp;&nbsp;&nbsp;&nbsp; ● <strong>Skin : </strong>&nbsp;&nbsp;");
                    sb.Append(lstDischarge[0].Skin);
                    sb.Append("</td></tr>");

                    sb.Append("<tr><td colspan='4' align='left'><br><strong><u>Systemic Examination :</u></strong></td></tr>");
                    sb.Append("<tr><td align='left' colspan='2'><strong>Resp. System :</strong>");
                    sb.Append(lstDischarge[0].RespSystem);
                    sb.Append("</td><td align='left' colspan='2'><strong>CNS :</strong>");
                    sb.Append(lstDischarge[0].CNS);
                    sb.Append("</td></tr>");
                    sb.Append("<tr><td align='left' colspan='2'><strong>Per Abd :</strong>");
                    sb.Append(lstDischarge[0].PerAbd);
                    sb.Append("</td><td align='left' colspan='2'><strong>CVS :</strong>");
                    sb.Append(lstDischarge[0].CVS);
                    sb.Append("</td></tr>");
                    
                    sb.Append("<tr><td colspan='4' align='left'><br><strong><u>Investigations :</u></strong></td></tr>");
                    int cnt = 0;
                    sb.Append("<tr>");
                    if (!string.IsNullOrEmpty(lstDischarge[0].Haemogram))
                    {
                        cnt++;
                        sb.Append("<td align='left'>Haemogram</td><td align='left'>").Append(lstDischarge[0].Haemogram).Append("</td>");
                    }
                    if (!string.IsNullOrEmpty(lstDischarge[0].UrineR))
                    {
                        cnt++;
                        sb.Append("<td align='left'>Urine R</td><td align='left'>").Append(lstDischarge[0].UrineR).Append("</td>");
                    }
                    if (cnt == 2)
                    {
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        cnt = 0;
                    }
                    if (!string.IsNullOrEmpty(lstDischarge[0].BSL))
                    {
                        sb.Append("<td align='left'>B.S.L.</td><td align='left'>").Append(lstDischarge[0].BSL).Append("</td>");
                        cnt++;
                    }
                    if (cnt == 2)
                    {
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        cnt = 0;
                    }
                    if (!string.IsNullOrEmpty(lstDischarge[0].BUL))
                    {
                        sb.Append("<td align='left'>B.U.L.</td><td align='left'>").Append(lstDischarge[0].BUL).Append("</td>");
                        cnt++;
                    }
                    if (cnt == 2)
                    {
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        cnt = 0;
                    }
                    if (!string.IsNullOrEmpty(lstDischarge[0].SCreat))
                    {
                        sb.Append("<td align='left'>S. Creat</td><td align='left'>").Append(lstDischarge[0].SCreat).Append("</td>");
                        cnt++;
                    }
                    if (cnt == 2)
                    {
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        cnt = 0;
                    }
                    if (!string.IsNullOrEmpty(lstDischarge[0].SElect1))
                    {
                        sb.Append("<td align='left'>S. Elect</td><td align='left'>").Append(lstDischarge[0].SElect1).Append("</td>");
                        cnt++;
                    }
                    if (cnt == 2)
                    {
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        cnt = 0;
                    }
                    if (!string.IsNullOrEmpty(lstDischarge[0].XRay))
                    {
                        sb.Append("<td align='left'>X-Ray</td><td align='left'>").Append(lstDischarge[0].XRay).Append("</td>");
                        cnt++;
                    }
                    if (cnt == 2)
                    {
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        cnt = 0;
                    }
                    if (!string.IsNullOrEmpty(lstDischarge[0].ECG))
                    {
                        sb.Append("<td align='left'>E.C.G.</td><td align='left'>").Append(lstDischarge[0].ECG).Append("</td>");
                        cnt++;
                    }
                    if (cnt == 2)
                    {
                        sb.Append("</tr>");
                        sb.Append("");
                        cnt = 0;
                    }
                    
                    sb.Append("<tr><td colspan='4' align='left'><br><strong><u>Others :</u></strong></td></tr>");
                    sb.Append("<tr><td colspan='4' align='left'>");
                    sb.Append(lstDischarge[0].Others);
                    sb.Append("</td></tr>");
                    sb.Append("</table>");
                    traet = string.Empty;
                    strReplace = lstDischarge[0].TreatmentInHospitalisation.Split('\n');
                    for (int i = 0; i < strReplace.Length; i++)
                    {
                        if (i == 0)
                        {
                            traet = traet + strReplace[i];
                        }
                        else
                        {
                            traet = traet + "<br>" + strReplace[i];
                        }
                    }
                    sb.Append("<div style='height: auto;'>");
                    sb.Append("<table style='width: 100%;'><tr><td colspan='2'><p align='left' style='font-weight:bold;'><u>Treatment Given In Hospital : </u></td></tr><tr><td colspan='2' align='left'>");
                    sb.Append(traet).Append("</p></td></tr></table></div>");
                    
                    traet = string.Empty;
                    sb.Append("<div style='height: auto;'>");
                    sb.Append("<table style='width: 100%;'><tr><td colspan='2'><p align='left' style='font-weight:bold;'><u>Advance On Discharge : </u></td></tr><tr><td colspan='2' align='left'>");
                    strReplace = lstDischarge[0].AdviceOnDischarge.Split('\n');
                    for (int i = 0; i < strReplace.Length; i++)
                    {
                        if (i == 0)
                        {
                            traet = traet + strReplace[i];
                        }
                        else
                        {
                            traet = traet + "<br>" + strReplace[i];
                        }
                    }
                    sb.Append(traet).Append("</p></td></tr></table></div>");

                    if (lstDischarge[0].TypeOfDischarge == "Routine")
                    {
                        sb.Append("<div style='height: auto;'>");
                        sb.Append("<table style='width: 100%;'><tr><td colspan='2'><p align='left' style='font-weight:bold;'><u>Next Follow Up Visit On : </u></td></tr><tr><td colspan='2' align='left'>");
                        sb.Append(lstDischarge[0].FollowUp).Append("</p></td></tr></table></div>");
                    }
                    else
                    { 
                        
                    }
                    sb.Append("<div style='height: 150px;'>");
                    sb.Append("<table style='width: 100%;'><tr><td align='left'><p align='left' style='font-weight:bold;'><u>Date : </u>&nbsp;&nbsp;&nbsp;&nbsp;");
                    sb.Append(string.Format("{0:dd-MMM-yyyy}", DateTime.Now.Date));
                    sb.Append("</td><td align='center'><p align='right' style='font-weight:bold;'><u>Signature of Medical Officer </u></p></td>");
                    sb.Append("<td></td></tr>");
                    sb.Append("<tr><td align='left'><p align='left' style='font-weight:bold;'><u>Prepared By : </u>&nbsp;");
                    sb.Append(lstDischarge[0].PreparedByName);
                    sb.Append("</td></tr></table></div>");
                    sb.Append("</div>");
                }
                Response.Write(sb.ToString());
            }
            #endregion

            #region Salary
            else if (ReportType.Equals("Salary"))
            {
                StringBuilder sb = new StringBuilder();
                decimal TotalAllow = 0;
                decimal TotalDeduc = 0;
                List<Hospital.Models.DataLayer.STP_PrintSalaryResult> lstBill = objData.STP_PrintSalary(Hospital.Models.DataLayer.QueryStringManager.Instance.LedgerId).ToList();
                string amt = Hospital.Models.DataLayer.Commons.Rupees(Convert.ToDecimal(lstBill[0].NetPayment));
                sb.Append("<table style='border:1px solid;'><tr><td style='width:100%;'><table width='100%' Height='100%'><tr><td  width='100%' align='center'> ");
                sb.Append("<img src='../images/BannerImages/HospitalNameGNH.png'  alt='#' width='100%' align='Left'/>");
                sb.Append("</td></tr><tr><td  width='100%' align='center'><strong>");
                sb.Append("</strong></td></tr></table></td></tr>");
                sb.Append("<tr style='width:100%'><td style='width:100%'><table width='100%'>");
                sb.Append("<tr style='width:100%'><td colspan='2' align='center'><strong><u>SALARY REPORT</u></strong></td></tr>");
                sb.Append("<tr style='width:100%'><hr><td style='width:50%;' align='left'><strong>Emp. Code :- ");
                sb.Append(lstBill[0].EmpCode).Append("</strong></td><td style='width:50%;' align='right'><strong>Salary Date :- ");
                sb.Append(string.Format("{0:dd/MM/yyyy}", lstBill[0].SalDate)).Append("</strong></td></tr>");
                sb.Append("<tr><td style='width:50%;colspan='2' align='left'><strong>Employee Name :- ");
                sb.Append(lstBill[0].EmployeeName).Append("</strong></td>");
                sb.Append("<td style='width:50%;' align='right'><strong>Salary Month :- "); sb.Append(lstBill[0].Sal_Month).Append("</strong></td></tr>");

                sb.Append("<tr><td style='width:50%;colspan='2' align='left'><strong>Designation :- ");
                sb.Append(lstBill[0].DesignationDesc).Append("</strong></td>");
                sb.Append("<td style='width:50%;' align='right'></td></tr>");
                
                sb.Append("<tr><td style='width:50%;colspan='2' align='left'><strong>Paid Days:-").Append(lstBill[0].Attend_Days).Append("</strong></td>");
                sb.Append("<td style='width:50%;' align='right'><strong>OT Hours :- ").Append(lstBill[0].OTHours).Append("</strong></td><tr></table></td></tr>");
                sb.Append("<tr width='100%' height='100%'><td  width='100%' height='100%'><table width='100%' height='100%' border='1'><tr><td align='center'>");
                sb.Append("<strong>Allowances</strong></td><td align='center'><strong>Amount</strong></td><td align='center'><strong>Deductions</strong>");
                sb.Append("</td><td align='center'><strong>Amount</strong></td></tr>");
                List<Hospital.Models.DataLayer.STP_PrintSalaryResult> lstDeductions = (lstBill.Where(p => p.IsDeduction == true)).ToList();
                List<Hospital.Models.DataLayer.STP_PrintSalaryResult> lstAllowances = (lstBill.Where(p => p.IsAllowance == true)).ToList();
                decimal perdaysal = lstBill[0].NetPayment / lstBill[0].No_of_Days;
                decimal hour = perdaysal / 8;
                decimal tothour = Convert.ToInt32(lstBill[0].OTHours) * decimal.Round(hour, 2);
                for (int i = 0; i < lstAllowances.Count; i++)
                {
                    sb.Append("<tr><td  align='center'>");
                    if (i < lstAllowances.Count)
                    {
                        sb.Append(lstAllowances[i].Description).Append("</td>");
                    }
                    sb.Append("<td align='center'>");
                    if (i < lstAllowances.Count)
                    {
                        sb.Append(decimal.Round(lstAllowances[i].Amount, 2)).Append("</td>");
                        TotalAllow = TotalAllow + lstAllowances[i].Amount;
                    }
                    sb.Append("<td align='center'>");
                    if (i < lstDeductions.Count)
                    {
                        sb.Append(lstDeductions[i].Description).Append("</td>");
                    }
                    sb.Append("<td align='center'>");
                    if (i < lstDeductions.Count)
                    {
                        sb.Append(decimal.Round(lstDeductions[i].Amount, 2)).Append("</td>");
                        TotalDeduc = TotalDeduc + lstDeductions[i].Amount;
                    }
                }
                sb.Append("</tr><tr height='20px'><td></td><td></td><td></td><td></td></tr><tr><td align='center'><strong>Total Allowance</strong></td><td align='center'><strong>Rs.");
                sb.Append(decimal.Round(TotalAllow, 2)).Append("/-</strong></td><td align='center'><strong>Total Deduction</strong></td><td align='center'><strong>Rs.");
                sb.Append(decimal.Round(TotalDeduc, 2)).Append("/-</strong></td></tr></table></td></tr>");
                sb.Append("<tr><td width='100%'><table width='100%' height='100%'><tr><td style='width:50%;' align='left' ><strong>");
                sb.Append("</strong></td><td align='right' style='width:80%;'><strong>OT Payment :- Rs.");
                sb.Append(decimal.Round(tothour, 2)).Append("/-</strong></td></tr></table></td></tr>");
                sb.Append("<tr><td width='100%'><table width='100%' height='100%'><tr><td style='width:55%;' align='left' ><strong>Rupees In Words:-");
                sb.Append(amt).Append("</strong></td><td align='right' style='width:45%;' ><strong>Net Amount :- Rs.");
                sb.Append(decimal.Round(lstBill[0].NetPayment, 2)).Append("/-</strong></td></tr>");
                sb.Append("<tr><td colspan='2' style='width:100%;'></td></tr>");
                sb.Append("<tr><td colspan='2' style='width:100%;'></td></tr>");
                sb.Append("<tr><td colspan='2' style='width:100%;'></td></tr>");
                sb.Append("<tr><td style='width:55%;' align='left' height='150px'><strong>Employee Signature");
                sb.Append("</strong></td><td width='45%' align='right' height='150px'><strong>Accountant Signature");
                sb.Append("</strong></td></tr></table></td></tr>");
                sb.Append("</table>");
                Response.Write(sb.ToString());
            }
            #endregion
            #region ISSUE
            else if (ReportType.Equals("ISSUE"))
            {
                StringBuilder sb = new StringBuilder();
                List<Hospital.Models.DataLayer.STP_PrintIssueResult> lstBill = objData.STP_PrintIssue(Convert.ToInt32(Session["Issue_Id"])).ToList();
                string amt = Hospital.Models.DataLayer.Commons.Rupees(Convert.ToDecimal(lstBill[0].TotalAmount));

                sb.Append("<table align='center' style='border:1px solid;' width='1000px'><tr height='100'><td height='100'><table width='100%' Height='100%'><tr><td  width='100%' align='center'> ");
                sb.Append("<img src='../images/BannerImages/HospitalNameGNH.png'  alt='#' width='100%' align='Left'/>");
                sb.Append("</td></tr><tr><td width='100%' align='center'><strong>");
                sb.Append("</strong></td></tr></table></td></tr><tr style='width:100%'><td style='width:90%'><table align='center' style='width:100%;'>");
                sb.Append("<tr><td align='center' colspan='2'><strong><u>MATERIAL ISSUE REPORT</u></strong></td></tr><tr><td style='width:50%;' align='left'><strong>Patient Name :- "); sb.Append(lstBill[0].PatientName).Append("</strong></td>");
                sb.Append("<td align='right'><strong>MRN :- "); 
                sb.Append(lstBill[0].PatientCode).Append("</strong></td></tr><tr><td align='left'><strong>Doctor Name :- "); 
                sb.Append(lstBill[0].EmpName).Append("</strong></td><td align='right'>");
                sb.Append("<strong>Issue Date :- "); sb.Append(string.Format("{0:dd/MM/yyyy}", lstBill[0].IssueDate)).Append("</strong></td></tr>");
                sb.Append("<tr><td colspan='2'></td></tr><tr><td colspan='2' align='center' style='width:100%;'>");
                sb.Append("<table border='1px' width='50%'><tr><td align='center'><strong>Product Name</strong></td><td align='center'><strong>Quantity</strong></td><td align='center'><strong>Price</strong></td></tr>");
                foreach (Hospital.Models.DataLayer.STP_PrintIssueResult item in lstBill)
                {
                    sb.Append("<tr><td align='center'style='border-bottom:hidden !important;'>");
                    sb.Append(item.ProductName).Append("</td><td align='center'style='border-bottom:hidden !important;'>");
                    sb.Append(item.Quantity).Append("</td><td align='center'style='border-bottom:hidden !important;'>");
                    sb.Append(item.Rate).Append("</td></tr>");
                }
                sb.Append("<tr><td colspan='2' align='right'><strong>Total : </strong></td><td align='center'><strong>");
                sb.Append(lstBill[0].TotalAmount).Append("</strong></td></tr></table></td></tr></table></td></tr></table>");
                Response.Write(sb.ToString());
            }
            #endregion
            #region DebitNoteReport
            else if (ReportType.Equals("DebitNoteReport"))
            {
                StringBuilder sb = new StringBuilder();
                List<Hospital.Models.DataLayer.STP_PrintDebitNoteResult> lstDebit = objData.STP_PrintDebitNote(Convert.ToInt32(Session["DNNo"])).ToList();
                string amt = Hospital.Models.DataLayer.Commons.Rupees(Convert.ToDecimal(lstDebit[0].NetAmount));
                sb.Append("<table align='center' style='border:1px solid;' width='1000px'><tr height='100'><td height='100'><table width='100%' Height='100%'><tr><td  width='100%' align='center'> ");
                sb.Append("<img src='../images/BannerImages/HospitalNameGNH.png'  alt='#' width='100%' align='Left'/>");
                sb.Append("</td></tr><tr><td width='100%' align='center'><strong>");
                sb.Append("</strong></td></tr>");
                sb.Append("<tr><td width='100%' align='center'><strong><u>");
                sb.Append("DEBIT NOTE</u></strong></td></tr>");
                sb.Append("</table></td></tr><tr style='width:100%'><td style='width:90%'>");
                sb.Append("<table width='100%'>");
                sb.Append("<tr style='width:100%'>");
                sb.Append("<td style='width:20%;' align='left'><strong>DN No </strong></td><td style='width:20%;' align='left'><strong>:- ");
                sb.Append(Convert.ToInt32(Session["DNNo"])).Append("</strong></td><td style='width:20%;'></td>");
                sb.Append("<td style='width:20%;' align='left'><strong>Date </strong></td><td style='width:20%;' align='left'><strong> :-");
                sb.Append(string.Format("{0:dd/MM/yyyy}", lstDebit[0].DNDate)).Append("</strong></td></tr>");
                sb.Append("<tr style='width:100%'>");
                sb.Append("<td style='width:20%;' align='left'><strong>Supplier Name </strong></td><td style='width:20%;' align='left'><strong>:- ");
                sb.Append(lstDebit[0].SupplierName).Append("</strong></td><td style='width:20%;'></td>");
                sb.Append("<td style='width:20%;' align='left'><strong>Supplier ID </strong></td><td style='width:20%;' align='left'><strong> :-");
                sb.Append(lstDebit[0].SupplierId).Append("</strong></td></tr>");
                sb.Append("<tr><td style='width:20%;' align='left'><strong>Address </strong></td><td style='width:20%;' align='left'><strong>:- ");
                sb.Append(lstDebit[0].Address).Append("</strong></td><td style='width:20%;'></td><td style='width:20%;'></td></tr></table></td></tr> ");
                sb.Append("<tr  width='100%' height='100%'><td  width='100%' height='100%'>");
                sb.Append("<table width='100%' height='100%' border='1'><tr>");
                sb.Append("<td align='center'><strong>ProductId</strong></td>");
                sb.Append("<td align='center'><strong>ProdutName</strong></td>");
                sb.Append("<td align='center'><strong>Quantity</strong></td>");
                sb.Append("<td align='center'><strong>Charge</strong></td>");
                sb.Append("<td align='center'><strong>BatchNo</strong></td>");
                sb.Append("<td align='center'><strong>ExpiryDate</strong></td>");
                sb.Append("<td align='center'><strong>Amount</strong></td></tr>");
                foreach (Hospital.Models.DataLayer.STP_PrintDebitNoteResult item in lstDebit)
                {
                    sb.Append("<tr>");
                    sb.Append("<td align='center'style='border-bottom:hidden !important;'>");
                    sb.Append(item.ProductCode).Append("</td>");
                    sb.Append("<td align='center'style='border-bottom:hidden !important;'>");
                    sb.Append(item.ProductName).Append("</td>");
                    sb.Append("<td align='center'style='border-bottom:hidden !important;'>");
                    sb.Append(item.Quantity).Append("</td>");
                    sb.Append("<td align='center'style='border-bottom:hidden !important;'>");
                    sb.Append(item.Price).Append("</td>");
                    sb.Append("<td align='center'style='border-bottom:hidden !important;'>");
                    sb.Append(item.BatchNo).Append("</td>");
                    sb.Append("<td align='center'style='border-bottom:hidden !important;'>");
                    sb.Append(string.Format("{0:dd/MM/yyyy}", item.ExpiryDate)).Append("</td>");
                    sb.Append("<td align='center'style='border-bottom:hidden !important;'>");
                    sb.Append(item.Quantity * item.Price).Append("</td></tr>");
                }
                sb.Append("</table></td></tr>");
                sb.Append("<tr style='width:100%'><td style='width:100%' align='Right'>");
                sb.Append("<table width='100%'><tr style='width:100%'><td style='width:'60%;' align='left'><strong>");
                sb.Append("</strong></td><td style='width:20%;' align='right'><strong>Total Amount :- Rs.</td><td style='width:20%;' align='right'><strong>");
                sb.Append(decimal.Round(Convert.ToDecimal(lstDebit[0].Amount), 2)).Append("/-</strong></td></tr>");
                sb.Append("<tr><td style='width:60%;' align='left'>");
                sb.Append("</td><td style='width:20%;' align='right'><strong>Discount(");
                sb.Append(lstDebit[0].Discount).Append("%) :- </td><td style='width:20%;' align='right'><strong>");
                sb.Append(decimal.Round(Convert.ToDecimal(lstDebit[0].Amount * lstDebit[0].Discount / 100), 2));
                sb.Append("/-</strong></td></tr>");

                sb.Append("<tr><td style='width:60%;' align='left'>");
                sb.Append("</td><td style='width:20%;' align='right'><strong>VAT(");
                sb.Append(lstDebit[0].Tax1).Append("%) :- </td><td style='width:20%;' align='right'><strong>");
                sb.Append(decimal.Round(Convert.ToDecimal(lstDebit[0].Amount * lstDebit[0].Tax1 / 100), 2));
                sb.Append("/-</strong></td></tr>");
                sb.Append("<tr><td style='width:60%;' align='left'>");
                sb.Append("</td><td style='width:20%;' align='right'><strong>Service Tax(");
                sb.Append(lstDebit[0].Tax2).Append("%) :- </td><td style='width:20%;' align='right'><strong>");
                sb.Append(decimal.Round(Convert.ToDecimal(lstDebit[0].Amount * lstDebit[0].Tax2 / 100), 2));
                sb.Append("/-</strong></td></tr>");
                sb.Append("<tr><td style='width:60%;' align='left'><strong>Rupees In Words:-");
                sb.Append(amt).Append("</strong></td><td style='width:20%;' align='right'><strong>NetAmount :- Rs.</td><td style='width:20%;' align='right'><strong>");
                sb.Append(decimal.Round(Convert.ToDecimal(lstDebit[0].NetAmount), 2)).Append("/-</strong></td></tr>");
                sb.Append("<tr height='15px'><td style='width:60%;' align='left'></td> <td style='width:20%;' align='right'></td><td style='width:20%;' align='right'><strong></td></tr>");
                sb.Append("<tr><td style='width:60%;' align='left'>");
                sb.Append("</td><td  style='height:130px;' colspan='2' align='right'><hr><strong>Accountant Signature");
                sb.Append("</strong></td></tr></table>");
                sb.Append("</td></tr></table>");
                Response.Write(sb.ToString());
            }
            #endregion
            #region SuppReceipt
            else if (ReportType.Equals("SuppReceipt"))
            {
                StringBuilder sb = new StringBuilder();

                Hospital.Models.Models.EntityCustomerTransaction entsupvoucher = (Hospital.Models.Models.EntityCustomerTransaction)Session["suppliervoucher"];
                int VoucherNo = Convert.ToInt32(Session["ReceiptNo"]);
                if (entsupvoucher != null)
                {
                    string amt = Hospital.Models.DataLayer.Commons.Rupees(Convert.ToDecimal(entsupvoucher.Amount));
                    sb.Append("<table align='center' width='1000px' style='border:1px solid;'><tr><td><table width='100%' align='center'><tr width='100%'><td style='width:100%;' align='center'> ");
                    sb.Append("<img src='../images/bannerimages/HospitalNameGNH.png'  alt='#' width='100%' align='left'/>");
                    sb.Append("<hr></td></tr>");
                    sb.Append("<tr><td colpspan='3' align='center'><strong>");
                    sb.Append("</strong></td></tr>");
                    sb.Append("</table></td></tr><tr width='100%'><td style='width:100%;'><table style='border:1px solid;border-color:black;' width='100%' align='center'>");
                    sb.Append("<tr width='100%'><td colspan='3' style='width:100%;' align='center'><strong><u>PAYMENT VOUCHER");
                    sb.Append("<hr></u></strong></td></tr><tr><td style='width:30%;' align='left'><strong>Voucher No :- ");
                    sb.Append(VoucherNo).Append("</strong></td><td></td>");
                    sb.Append("<td style='width:35%;' align='left'><strong>Voucher Date :- ");
                    sb.Append(string.Format("{0:dd/MM/yyyy}", entsupvoucher.ReceiptDate)).Append("</strong></td> </tr><tr><td colspan='3' align='left'><strong>Received From :- ");
                    sb.Append(entsupvoucher.SupplierName).Append("</strong></td></tr>");
                    sb.Append("<tr><td align='left' colspan='2'><strong>Rupees In Words :- ").Append(amt).Append("</strong></td>");
                    sb.Append("<td align='left' style='border:1px solid;background-color:lightgray;'><strong>Amount :- Rs.");
                    sb.Append(entsupvoucher.Amount).Append("/-</strong></td></tr>");
                    sb.Append("<tr height='15px'><td></td></tr><tr><td></td><td></td><td align='center'><strong><hr>Signature</strong></td></tr></table></td></tr></table>");
                    Response.Write(sb.ToString());
                }
            }
            #endregion
            #region Death
            else if (ReportType.Equals("Death"))
            {
                StringBuilder sb = new StringBuilder();
                List<Hospital.Models.DataLayer.STP_PrintDeathReportResult> lstDischarge = objData.STP_PrintDeathReport(Convert.ToInt32(Session["Death_Id"])).ToList();
                //DateTime Today = DateTime.Now.Date;
                //DateTime Birth = DateTime.Parse(Convert.ToString(lstDischarge[0].BirthDate));
                //int days = Today.Subtract(Birth).Days;
                //int Age = Convert.ToInt32(Math.Floor(days / 365.24219));
                sb.Append("<div border='1px' style='border-radius:25px; width:100%; height:100%;'><table background='../images/DeathCertificate.png' width='100%' height='100%' style='padding-bottom:20px;border:1px solid; border-radius:25px;'>");
                sb.Append("<tr style='height:15px;'><td align='center' colspan='3'><img src='../images/BannerImages/HospitalNameGNH.png' alt='#' width='100%' height='100%' align='center' style='border-top-left-radius: 15px; border-top-right-radius: 15px;' /></td></tr>");
                sb.Append("<tr style='height:15px;'><td align='center' colspan='3'><strong></strong></td></tr>");
                sb.Append("<tr><td colspan='3' align='center'><strong><u>CERTIFICATE OF DEATH</u></strong></td></tr><tr><td></td></tr><tr><td colspan='3' align='center'>");
                sb.Append("<strong>This is to confirm and certify the death of</strong></td></tr><tr><td align='left' colspan='3'><strong>Name : ");
                sb.Append(lstDischarge[0].FullName).Append("</strong></td></tr><tr><td align='left' style='width:50%;'><strong>Birth Date : ");
                sb.Append(string.Format("{0:dd/MM/yyyy}", lstDischarge[0].BirthDate)).Append("</strong></td><td align='left' style='width:25%;'><strong>Age : "); sb.Append(lstDischarge[0].Age).Append("</strong></td><td align='left' style='width:25%;'><strong>Gender : ");
                sb.Append(lstDischarge[0].GenderDesc).Append("</strong></td></tr><tr><td align='left' colspan='3'><strong>At Place : ");
                sb.Append(lstDischarge[0].Address).Append("</strong></td></tr><tr><td align='left' style='width:50%;'><strong>City : "); sb.Append(lstDischarge[0].City).Append("</strong></td>");
                sb.Append("<td align='left' style='width:25%;'><strong>State : "); sb.Append(lstDischarge[0].State).Append("</strong></td><td align='left' style='width:25%;'><strong>Country : ");
                sb.Append(lstDischarge[0].Country); sb.Append("</strong></td></tr><tr><td align='left' style='width:50%;'><strong>On Date : "); sb.Append(string.Format("{0:dd/MM/yyyy}", lstDischarge[0].Death_Date));
                sb.Append("</strong></td><td align='left' colspan='2'><strong>Time : "); sb.Append(string.Format("{0:hh:mm tt}", lstDischarge[0].Death_Time)).Append("</strong></td></tr><tr><td align='left' colspan='3'><strong>Due To : ");
                sb.Append(lstDischarge[0].Death_Reason).Append("</strong></td></tr><tr><td></td></tr><tr><td></td><td></td><td><strong>");
                sb.Append("Signature</strong></td></tr></table></div>");
                Response.Write(sb.ToString());
            }
            #endregion
            #region Birth
            else if (ReportType.Equals("Birth"))
            {
                StringBuilder sb = new StringBuilder();
                List<Hospital.Models.DataLayer.STP_PrintBirthReportResult> lstDischarge = objData.STP_PrintBirthReport(Convert.ToInt32(Session["Birth_ID"])).ToList();
                sb.Append("<div border='1px' style='border-radius:25px; width:100%; height:100%;'><table width='100%' height='100%' style='padding-bottom:20px;border:1px solid;'>");
                sb.Append("<tr style='height:15px;width:100%;'><td align='center' style='width:100%;'><img src='../images/BannerImages/HospitalNameGNH.png' alt='#' width='100%' height='100%' align='center' /></td></tr>");
                sb.Append("<tr style='height:15px;width:100%;'><td align='center' style='width:100%;'><strong></strong></td></tr><tr style='width:100%;'><td style='width:100%;'>");
                sb.Append("<table width='100%' height='100%' style='border:2px solid Black; width:100%;'>");
                sb.Append("<tr style='height:70px;'><td colspan='3' align='center'><strong></strong></td></tr><tr><td></td></tr><tr><td colspan='3' align='center'><strong>");
                sb.Append("<u>BIRTH CERTIFICATE</u></strong></td></tr><tr><td colspan='3' align='center'><strong>");
                sb.Append("It is certified that</strong></td></tr><tr style='height:30px;'><td></td></tr><tr><td align='left' style='padding-left:70px;'><strong><u>Children Discription");
                sb.Append("</u></strong></td></tr><tr><td align='left' style='padding-left:130px;' colspan='3'><strong>Child Name : ");
                sb.Append(lstDischarge[0].ChildName).Append("</strong></td><td></td></tr><tr><td align='left' style='padding-left:130px; width:50%;'><strong>Gender : ");
                sb.Append(lstDischarge[0].GenderDesc).Append("</strong></td><td align='left' style='width:25%;'><strong>Height : ");
                sb.Append(lstDischarge[0].Height).Append(" CM</strong></td><td align='left'><strong>Weight: ");
                sb.Append(lstDischarge[0].Weight).Append("</strong></td></tr><tr><td align='left' style='padding-left:130px;'><strong>Birth Date : ");
                sb.Append(string.Format("{0:dd/MM/yyyy}", lstDischarge[0].BirthDate)).Append("</strong></td><td align='left' style='width:25%;'><strong>Time : ");
                sb.Append(string.Format("{0:hh:mm tt}", lstDischarge[0].BirthTime)).Append("</strong></td></tr><tr><td colspan='3' align='left' style='padding-left:130px;'><strong>At Place : ");
                sb.Append("Ganga Clinic and Nursing Home, Pingle Vasti, Mundhwa,</strong></td></tr><tr><td align='left' style='padding-left:130px;'><strong>City : Pune</strong></td><td align='left' style='width:25%;'><strong>State : ");
                sb.Append("Maharashtra</strong></td><td align='left'><strong>Country : India</strong></td></tr><tr><td></td></tr><tr><td align='left' style='padding-left:70px;'><strong><u>");
                sb.Append("Family Discription</u></strong><td></tr><tr><td colspan='3' align='left' style='padding-left:130px;'><strong>Mother Name : ");
                sb.Append(lstDischarge[0].MotherName).Append("</strong></td></tr><tr><td colspan='3' align='left' style='padding-left:130px;'><strong>Father Name : ");
                sb.Append(lstDischarge[0].FatherName).Append("</strong></td></tr><tr><td colspan='3' align='left' style='padding-left:130px;'><strong>Family Name : ");
                sb.Append(lstDischarge[0].FamilyName).Append("</strong></td></tr><tr style='height:25px;'><td colspan='3'></td></tr><tr><td align='center' style='width:50%;'><strong>");
                sb.Append("Doc Signature</strong></td><td aign='left' style='padding-left:120px;'><strong>MS Signature</strong></td></tr></table>");
                sb.Append("</td></tr></table></div>");
                Response.Write(sb.ToString());
            }
            #endregion
            #region Medical
            else if (ReportType.Equals("MedicalCertificate"))
            {
                StringBuilder sb = new StringBuilder();
                List<Hospital.Models.DataLayer.STP_PrintMedicalReportResult> lstDischarge = objData.STP_PrintMedicalReport(Hospital.Models.DataLayer.QueryStringManager.Instance.Certi_ID).ToList();
                sb.Append("<div border='1px' style='border-radius:25px; width:100%; height:100%;'><table width='100%' height='100%' style='padding-bottom:20px;border:1px solid;'>");
                sb.Append("<tr style='height:15px;width:100%;'><td align='center' style='width:100%;'><img src='../images/BannerImages/HospitalNameGNH.png' alt='#' width='100%' center' /></td></tr>");
                sb.Append("<tr style='height:15px;width:100%;'><td align='center' style='width:100%;'><strong></strong></td></tr><tr style='width:100%;'><td style='width:100%;'>");
                sb.Append("<table width='100%' height='100%' style='border:0px solid Black; width:100%;'>");
                sb.Append("<tr style='height:10px;'><td colspan='3' align='center'><strong></strong></td></tr><tr><td></td></tr><tr><td colspan='3' align='center'><strong>");
                sb.Append("<u>MEDICAL CERTIFICATE</u></strong></td></tr><tr><td colspan='3' align='left'><strong>NO. : </strong>");
                sb.Append(lstDischarge[0].CertiID).Append(" </td><td></td></tr>");
                sb.Append("<tr><td colspan='3' align='left'><strong>Patient : </strong>");
                sb.Append(lstDischarge[0].PatientName).Append("</strong></td><td></td></tr>");
                sb.Append("<tr><td  colspan='3' align='left'><strong>Age :  </strong>");
                sb.Append(lstDischarge[0].Age).Append(" Yrs.</td><td></td></tr>");
                sb.Append("<tr><td  colspan='3' align='left'><strong>Diagnosis :  </strong>");
                sb.Append(lstDischarge[0].Daignosis).Append("</td><td></td></tr>");
                sb.Append("<tr><td  colspan='3' align='left'><strong>is under My treatment as an out - Patient and/or in - Patient, at this Hospital</strong></td><td></td></tr>");
                sb.Append("<tr><td  colspan='3' align='left'><strong> Was treated as an O.P.D. Patient from  </strong>");
                sb.Append(string.Format("{0:dd/MM/yyyy}", lstDischarge[0].OPDFrom)).Append("</strong></td></tr>");
                sb.Append("<tr><td  colspan='3' align='left'><strong> To  </strong>");
                sb.Append(string.Format("{0:dd/MM/yyyy}", lstDischarge[0].OPDTo)).Append(" . </td></tr>");
                sb.Append("<tr><td  colspan='3' align='left'><strong> Was admitted as an indoor patient on  </strong>");
                sb.Append(string.Format("{0:dd/MM/yyyy}", lstDischarge[0].IndoorOn)).Append("</strong></td></tr>");
                sb.Append("<tr><td  colspan='3' align='left'><strong>and Disharged on   </strong>");
                sb.Append(string.Format("{0:dd/MM/yyyy}", lstDischarge[0].DischargeOn)).Append("  .</td></tr>");
                sb.Append("<tr><td  colspan='3' align='left'><strong> He/She was operated for   </strong>");
                sb.Append(lstDischarge[0].OperatedFor).Append("</strong></td></tr>");
                sb.Append("<tr><td  colspan='3' align='left'><strong>  on   </strong>");
                sb.Append(string.Format("{0:dd/MM/yyyy}", lstDischarge[0].OperatedForOn)).Append(" .</td></tr>");
                sb.Append("<tr><td  colspan='3' align='left'><strong> He/She has been advised   </strong>");
                sb.Append(lstDischarge[0].AdvisedRestDays).Append("days rest from  </strong>");
                sb.Append(string.Format("{0:dd/MM/yyyy}", lstDischarge[0].AdvisedRestFrom)).Append(" . </td></tr>");
                sb.Append("<tr><td  colspan='3' align='left'><strong>However, He / She is fruther advised to continue rest from   </strong>");
                sb.Append(string.Format("{0:dd/MM/yyyy}", lstDischarge[0].ContinueRestFrom)).Append("</strong></td></tr>");
                sb.Append("<tr><td  colspan='3' align='left'><strong>for another   </strong>");
                sb.Append(string.Format("{0:dd/MM/yyyy}", lstDischarge[0].ContinuedRestDays)).Append(" days. </td></tr>");
                sb.Append("<tr><td  colspan='3' align='left'><strong>He / She is fit / Unfit to resume normal duties / Light  </strong></td></tr>");
                sb.Append("<tr><td  colspan='3' align='left'><strong>work from   </strong>");
                sb.Append(string.Format("{0:dd/MM/yyyy}", lstDischarge[0].WorkFrom)).Append(".</td></tr>");
                sb.Append("<tr><td colspan='3' height='35px'></td></tr>");
                sb.Append("<tr><td align='center' style='width:50%;'><strong>");
                sb.Append("Patient Signature/Or</strong></td><td aign='left' style='padding-left:110px;'><strong>MS Signature</strong></td></tr>");
                sb.Append("<tr><td align='center' style='width:50%;'><strong>");
                sb.Append("Thumb Impression</strong></td><td aign='left' style='padding-left:110px;'><strong></strong></td></tr>");
                sb.Append("<tr><td align='center' style='width:50%;'><strong>");
                sb.Append("Date :</strong></td><td aign='left' style='padding-left:110px;'><strong>Date :</strong></td></tr></table>");
                sb.Append("</td></tr></table></div>");
                Response.Write(sb.ToString());
            }
            #endregion

            
            #region Prescription
            else if (ReportType.Equals("Prescription"))
            {
                StringBuilder sb = new StringBuilder();
                
                List<Hospital.Models.DataLayer.STP_PrintPrescriptionResult> lstBill = objData.STP_PrintPrescription(Hospital.Models.DataLayer.QueryStringManager.Instance.Prescription_Id).ToList();
                var Dressing = string.Empty;
                var Injection = string.Empty;
                if (lstBill[0].IsDressing == true)
                {
                    Dressing = "Done";
                }
                else
                {
                    Dressing = "Not Done";
                }
                if (lstBill[0].IsInjection == true)
                {
                    Injection = lstBill[0].InjectionName;
                }
                else
                {
                    Injection = "Not Done";
                }
                sb.Append("<table align='center' style='border:1px solid; height:auto;' width='1000px'><tr height='100%'><td height='100%'><table width='100%' Height='100%'><tr><td  width='100%' align='center'> ");
                sb.Append("<img src='../images/BannerImages/HospitalNameGNH.png'  alt='#' width='100%' align='Left'/>");
                sb.Append("</td></tr><tr><td width='100%' align='center'> <hr></td></tr><tr><td width='100%' align='center'><strong>");
                sb.Append("</strong></td></tr></table></td></tr><tr style='width:100% height:auto;'><td style='width:90%; height:auto;'>");
                sb.Append("<table align='center' style='height:auto;width:100%;'><tr><td align='center' colspan='2'><strong><u>PRESCRIPTION REPORT</u></strong></td></tr>");
                sb.Append("<tr><td style='width:75%;' align='left'><strong><font size='4'>Patient Name :- ");
                sb.Append(lstBill[0].FullName).Append("</font></strong></td><td align='left'><strong><font size='4'>MRN :- ");
                sb.Append(lstBill[0].PatientCode).Append("</font></strong></td></tr><tr><td align='left'><strong><font size='4'>Prescription Date :- ");
                sb.Append(string.Format("{0:dd/MM/yyyy}", lstBill[0].Prescription_Date)).Append("</font></strong></td><td align='left'><strong><font size='4'>Patient Type :- ");
                sb.Append(lstBill[0].PatientType).Append("</font></strong></td>");
                sb.Append("<tr><td align='left'><strong><font size='4'>Department :- ");
                sb.Append(lstBill[0].EmpName).Append("</font></strong></td></tr><tr><td align='left'><strong><font size='4'>Dressing : ");
                sb.Append(Dressing).Append("</font></strong></td><td align='left'><strong><font size='4'>Injection : ");
                sb.Append(Injection).Append("</font></strong></td></tr><tr><td colspan='2'></td></tr><tr><td colspan='2' align='left' style='width:100%;'>");
                sb.Append("<table style='border:1px solid black; border-collapse:collapse;' height='auto' width='100%'><tr ><td RowSpan='2' align='center' style='border:1px solid black;'><strong><font size='4'>MedicineName</font></strong></td>");
                sb.Append("<td ColSpan='4' align='center' style='border:1px solid black;'><strong><font size='4'>Medicine Timing</font></strong></td> <td RowSpan='2' align='center' style='border:1px solid black;'><strong><font size='4'>NoOfDays</font></strong></td>");
                sb.Append("<td RowSpan='2'align='center' style='border:1px solid black;'><strong><font size='4'>Quantity</font></strong></td></tr>");
                sb.Append("<tr><td align='center' style='border:1px solid black;'><strong><font size='4'></font></strong></td><td align='center' style='border:1px solid black;'><strong><font size='4'>सकाळी</font></strong></td><td align='center' style='border:1px solid black;'><strong><font size='4'>दुपारी</font></strong></td><td align='center' style='border:1px solid black;'><strong><font size='4'>संध्याकाळी</font></strong></td></tr>");
                foreach (Hospital.Models.DataLayer.STP_PrintPrescriptionResult item in lstBill)
                {
                    sb.Append("<tr><td align='center'style='border:1px solid black;'><font size='4'>");
                    sb.Append(item.ProductName).Append("</font></td><td align='center'style='border:1px solid black;'><font size='4'>");
                    sb.Append(Convert.ToBoolean(item.IsbeforeLunch) ? "जेवणाआधी" : "जेवणानंतर").Append("</font></td><td align='center'style='border:1px solid black;'><font size='4'>");
                    sb.Append(item.Morning).Append("</font></td><td align='center'style='border:1px solid black;'><font size='4'>");
                    sb.Append(item.Afternoon).Append("</font></td><td align='center'style='border:1px solid black;'><font size='4'>");
                    sb.Append(item.Night).Append("</font></td><td align='center'style='border:1px solid black;'><font size='4'>");
                    sb.Append(item.NoOfDays).Append("</font></td><td align='center'style='border:1px solid black;'><font size='4'>");
                    sb.Append(item.Quantity).Append("</font></td></tr>");
                }
                
                sb.Append("</table></td></tr>");
                sb.Append("<tr><td align='left' colspan='2'>&nbsp;&nbsp;</td></tr>");
                sb.Append("<tr><td align='left' colspan='2'>&nbsp;&nbsp;</td></tr>");
                sb.Append("<tr><td align='left' colspan='2'><strong><font size='4'>Follow up date :- ");
                sb.Append(lstBill[0].FollowUpDate==null?DateTime.Now.Date.ToString("dd/MM/yyyy"):lstBill[0].FollowUpDate.Value.ToString("dd/MM/yyyy")).Append("</font></strong></td></tr>");
                sb.Append("<tr><td align='left' colspan='2'><strong><font size='4'>Investigation Done :- ");
                sb.Append(lstBill[0].Investigation).Append("</font></strong></td></tr><tr><td align='left' colspan='2'><strong><font size='4'>Impression :- ");
                sb.Append(lstBill[0].Impression).Append("</font></strong></td></tr>");
                sb.Append("<tr><td align='left' colspan='2'><strong><font size='4'>Advice Note :- ");
                sb.Append(lstBill[0].AdviceNote).Append("</font></strong></td><tr><td align='left' colspan='2'><strong><font size='4'>Remarks :- ");
                sb.Append(lstBill[0].Remarks).Append("</font></strong></td></tr>");
                sb.Append("</table></td></tr></table>");
                
                Response.Write(sb.ToString());
            }
            #endregion
            #region OTPatientMedicineBill
            else if (ReportType.Equals("OTPatientMedicineBill"))
            {
                StringBuilder sb = new StringBuilder();
                List<Hospital.Models.DataLayer.STP_PrintOTPatientMedicineBillResult> lstBill = objData.STP_PrintOTPatientMedicineBill(Convert.ToInt32(Session["BillNo"])).ToList();
               
                sb.Append("<table align='center' style='border:1px solid; height:auto;' width='1000px'><tr height='100%'><td height='100%'><table width='100%' Height='100%'><tr><td  width='100%' align='center'> ");
                sb.Append("<img src='../images/BannerImages/HospitalNameGNH.png'  alt='#' width='100%' align='Left'/>");
                sb.Append("</td></tr><tr><td width='100%' align='center'><strong>");
                sb.Append("</strong></td></tr></table></td></tr><tr style='width:100% height:auto;'><td style='width:90%; height:auto;'>");
                sb.Append("<table align='center' style='height:auto;width:100%;'><tr><td align='center' colspan='2'><strong><u>OT Patient Medicine Bill</u></strong></td></tr>");
                sb.Append("<tr><td style='width:40%;' align='left'><strong>Patient Name :- ");
                sb.Append(lstBill[0].FullName).Append("</strong></td><td align='center'><strong>MRN :- ");
                sb.Append(lstBill[0].PatientCode).Append("</strong></td></tr><tr><td align='left'><strong>Bill Date :- ");
                sb.Append(string.Format("{0:dd/MM/yyyy}", lstBill[0].Bill_Date)).Append("</strong></td><td align='center'><strong>Bill No :- ");
                sb.Append(lstBill[0].BillNo).Append("</strong></td></tr><tr><td align='left'><strong>Diagnosys : ");
                sb.Append(lstBill[0].Dignosys).Append("</strong></td><td align='center'><strong>IPD No : ");
                sb.Append(lstBill[0].IPDNo).Append("</strong></td></tr><tr><td align='left'><strong>Operation Name : ");
                sb.Append(lstBill[0].OperationName).Append("</strong></td></tr><tr><td align='left'><strong>Patient Type : ");
                sb.Append(lstBill[0].PatientType).Append("</strong></td></tr><tr><td colspan='2'></td></tr><tr><td colspan='2' align='left' style='width:100%;'>");
                sb.Append("<table style='border:1px solid black; border-collapse:collapse;' height='auto' width='50%'><tr ><td align='center' style='border:1px solid black;'><strong>MedicineName</strong></td>");
                sb.Append("<td align='center' style='border:1px solid black;'><strong>Quantity</strong></td> <td align='center' style='border:1px solid black;'><strong>Charges</strong></td><td align='center' style='border:1px solid black;'><strong>Amount</strong></td></tr>");
                foreach (Hospital.Models.DataLayer.STP_PrintOTPatientMedicineBillResult item in lstBill)
                {
                    sb.Append("<tr><td align='center'style='border:1px solid black;'>");
                    sb.Append(item.MedicineName).Append("</td><td align='center'style='border:1px solid black;'>");
                    sb.Append(item.Quantity).Append("</td><td align='center'style='border:1px solid black;'>");
                    sb.Append(item.Price).Append("</td><td align='center'style='border:1px solid black;'>");
                    sb.Append(item.Amount).Append("</td></tr>");
                }
                sb.Append("<tr><td colspan='3' align='right'><strong>Total : </strong></td><td align='center'><strong>");
                sb.Append(lstBill[0].TotalAmount).Append("</strong></td></tr></table></td></tr></table></td></tr></table>");
                Response.Write(sb.ToString());
            }
            #endregion    
            #region PurchaseOrder
            else if (ReportType.Equals("PurchaseOrder"))
            {
                StringBuilder sb = new StringBuilder();
                List<Hospital.Models.DataLayer.STP_PrintPurchaseOrderResult> lstBill = objData.STP_PrintPurchaseOrder(Convert.ToInt32(Session["Purchase_Id"])).ToList();
                string amt = Hospital.Models.DataLayer.Commons.Rupees(Convert.ToDecimal(lstBill[0].PO_Amount));

                sb.Append("<table align='center' style='border:1px solid;' width='1000px'><tr height='100'><td height='400'><table width='100%' Height='100%'><tr><td  width='100%' align='center'> ");
                sb.Append("<img src='../images/BannerImages/HospitalNameGNH.png'  alt='#' width='100%' align='Left'/>");
                sb.Append("</td></tr><tr><td width='100%' align='center'><strong>");
                sb.Append("</strong></td></tr></table></td></tr><tr style='width:100%'><td style='width:90%'><table align='center' style='width:100%;'>");
                sb.Append("<tr><td align='center' colspan='2'><strong>PURCHASE ORDER REPORT</strong></td></tr><tr><td style='width:50%;' align='left'><strong>Supplier Name :- "); sb.Append(lstBill[0].SupplierName).Append("</strong></td>");
                sb.Append("<td align='right'><strong>Supplier Code :- "); sb.Append(lstBill[0].SupplierCode).Append("</strong></td></tr><tr><td align='left'>");
                sb.Append("<strong>PO Date :- "); sb.Append(string.Format("{0:dd/MM/yyyy}", lstBill[0].PO_Date)).Append("</strong></td></tr>");
                sb.Append("<tr><td colspan='2'></td></tr><tr><td colspan='2' align='center' style='width:100%;'>");
                sb.Append("<table border='1px' width='50%'><tr><td align='center'><strong>Product Name</strong></td><td align='center'><strong>Quantity</strong></td><td align='center'><strong>Price</strong></td><td align='center'><strong>TotalCharges</strong></td></tr>");
                foreach (Hospital.Models.DataLayer.STP_PrintPurchaseOrderResult item in lstBill)
                {
                    sb.Append("<tr><td align='center'style='border-bottom:hidden !important;'>");
                    sb.Append(item.ProductName).Append("</td><td align='center'style='border-bottom:hidden !important;'>");
                    sb.Append(item.Quantity).Append("</td><td align='center'style='border-bottom:hidden !important;'>");
                    sb.Append(item.Rate).Append("</td><td align='center'style='border-bottom:hidden !important;'>");
                    sb.Append(item.Total).Append("</td></tr>");
                }
                sb.Append("<tr><td colspan='3' align='right'><strong>Total : </strong></td><td align='center'><strong>");
                sb.Append(lstBill[0].PO_Amount).Append("</strong></td></tr></table></td></tr></table></td></tr></table>");
                Response.Write(sb.ToString());
            }
            #endregion
            #region ICU_MainReport
            else if (ReportType.Equals("ICU_MainReport"))
            {
                StringBuilder sb = new StringBuilder();
                int ICU_Days = 1;
                Hospital.Models.BusinessLayer.ICUDischargeBilling objICUInvoice = new Hospital.Models.BusinessLayer.ICUDischargeBilling();
                string PatientId = Convert.ToString(Session["Patient__ID"]);
                List<Hospital.Models.DataLayer.STP_GetICUInvoiceBedsFinalResult> lstBill = objICUInvoice.GetFinalICUInvoice(Convert.ToInt32(Session["ICUInvoiceNo"])).ToList();
                List<Hospital.Models.Models.EntityICUInvoiceDischargeDetails> lstBedCharge = objICUInvoice.SelectFinalICUInvoiceDetails(Convert.ToInt32(Session["ICUInvoiceNo"]));
                string amount = Hospital.Models.DataLayer.Commons.Rupees(lstBill[0].NetAmount.Value);

                List<Hospital.Models.Models.EntityBedAllocToPatient> entBedAlloc = new Hospital.Models.BusinessLayer.ICUDischargeBilling().GetICUAllocatedBedDetails(Convert.ToInt32(Session["Patient__ID"]));
                foreach (var item1 in entBedAlloc)
                {
                    if (item1.CategoryDesc == "ICU")
                    {
                        if (item1.ShiftDate == null)
                        {
                            ICU_Days = ICU_Days + Convert.ToInt32(DateTime.Now.Date.Subtract(item1.AllocationDate.Value.Date).Days);
                        }
                        else
                        {
                            ICU_Days = ICU_Days + Convert.ToInt32(item1.ShiftDate.Value.Date.Subtract(item1.AllocationDate.Value.Date).Days);
                        }
                    }
                }

                sb.Append("<table align='center' style='border:1px solid;' width='1000px'><tr height='100'><td height='100'><table width='100%' Height='100%'><tr>");
                sb.Append("<td  width='100%' align='center'><img src='../images/BannerImages/HospitalNameGNH.png'  alt='#' width='100%' align='Left'/>");
                sb.Append("</td></tr><tr><td width='100%' align='center'><strong></strong></td></tr><tr><td  align='center'><strong><u>");
                sb.Append("PATIENT ICU INVOICE<hr></u></strong></td></tr></table></td></tr><tr style='width:100%'><td style='width:90%'><table width='100%'>");
                sb.Append("<tr style='width:100%'><td style='width:50%;' align='left'><strong>Patient Name :- ");
                sb.Append(lstBill[0].FullName).Append("</strong></td><td style='width:50%;' align='right'><strong>Date :- ");
                sb.Append(string.Format("{0:dd/MM/yyyy}", DateTime.Now.Date)).Append("</strong></td></tr><tr><td style='width:50%;colspan='2' align='left'>");
                sb.Append("</td><td style='width:50%;' align='right'><strong></strong></td> </tr>");
                sb.Append("<tr><td style='width:50%;' align='left'><strong>Date of Admission :- ");
                sb.Append(string.Format("{0:dd/MM/yyyy}", lstBill[0].AdmitDate)).Append("</strong></td>");
                sb.Append("<td style='width:50%;' align='right'><strong>No Of Days :- ");
                sb.Append(ICU_Days).Append("</strong></td></tr></table></td></tr>");
                sb.Append("<tr  width='100%' height='100%'><td  width='100%' height='100%'><table width='100%' height='100%' border='1'><tr>");
                sb.Append("<td style='width:50%;' align='center'><strong>Particulars</strong></td><td align='center'><strong>No. Of Days</strong></td>");
                sb.Append("<td align='center'><strong>Quantity</strong></td><td align='center'><strong>Charge</strong></td><td align='center'><strong>Amount</strong></td></tr>");
                foreach (Hospital.Models.Models.EntityICUInvoiceDischargeDetails item in lstBedCharge)
                {
                    sb.Append("<tr><td style='border-bottom:hidden !important;' align='center'>");
                    sb.Append(item.ChargesName).Append("</td><td style='border-bottom:hidden !important;' align='center'>");
                    sb.Append(item.NoofDays).Append("</td><td style='border-bottom:hidden !important;' align='center'>");
                    sb.Append(item.Quantity).Append("</td><td style='border-bottom:hidden !important;' align='center'>");
                    sb.Append(item.Charge).Append("</td><td style='border-bottom:hidden !important;' align='center'>");
                    sb.Append(decimal.Round(item.Amount.Value, 2)).Append("</td></tr>");
                }
                sb.Append("</table></td></tr>");
                sb.Append("<tr></td><td><table width='100%'><tr><td width='50%'></td><td width='35%' align='right'><strong>Total Amount :- </strong></td><td align='right'><strong>");
                sb.Append(lstBill[0].TotalAmount).Append("</strong></td></tr>");
                sb.Append("<tr><td width='50%'></td><td width='35%' align='right'><strong>Discount :-(" + lstBill[0].Discount.ToString() + "%) </strong></td><td align='right'><strong>");
                sb.Append(decimal.Round(Convert.ToDecimal(lstBill[0].TotalAmount * lstBill[0].Discount / 100), 2)).Append("</strong></td></tr>");
                sb.Append("<tr><td width='50%'></td><td width='35%' align='right'><strong>VAT :-(" + lstBill[0].Vat.ToString() + "%) </strong></td><td align='right'><strong>");
                sb.Append(decimal.Round(Convert.ToDecimal(lstBill[0].TotalAmount * lstBill[0].Vat / 100), 2)).Append("</strong></td></tr>");
                sb.Append("<tr><td width='50%'></td><td width='35%' align='right'><strong>Service Tax :-(" + lstBill[0].ServiceTax.ToString() + "%) </strong></td><td align='right'><strong>");
                sb.Append(decimal.Round(Convert.ToDecimal(lstBill[0].TotalAmount * lstBill[0].ServiceTax / 100), 2)).Append("</strong></td></tr>");
                sb.Append("<tr><td width='50%'><strong>Amount In Words:-" + amount + "</strong></td><td width='35%' align='right'><strong>NetAmount :- </strong></td><td align='right'><strong>");
                sb.Append(lstBill[0].NetAmount).Append("</strong></td></tr><tr><td colspan='2' height='70px'></td></tr><tr><td></td><td colspan='2' align='right'>");
                sb.Append("<strong><hr>Accountant Sign<strong></td></tr></table></td></tr></table>");
                Response.Write(sb.ToString());
            }
            #endregion
            
        %>
    </asp:Panel>
</asp:Content>
