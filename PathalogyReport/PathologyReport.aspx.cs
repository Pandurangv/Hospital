using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hospital.Models.BusinessLayer;
using Hospital.Models.Models;
using Hospital.Models.DataLayer;
using System.Data;

namespace Hospital.PathalogyReport
{
    public partial class PathalogyReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (SessionManager.Instance.LoginUser == null)
            {
                Response.Redirect("~/frmlogin.aspx", false);
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            string ReportType = string.Empty;
            if (Session["ReportType"]!=null)
            {
                ReportType = Convert.ToString(Session["ReportType"]);
            }
            else
            {
                ReportType = QueryStringManager.Instance.ReportType;
            }
            
            if (ReportType.Equals("Pathology"))
            {
                Response.Redirect("~/frmPathology.aspx", false);
            }
            else if (ReportType.Equals("Invoice"))
            {
                Response.Redirect("~/Billing/frmPatientInvoice.aspx", false);
            }
            else if (ReportType.Equals("Receipt"))
            {
                Response.Redirect("~/Billing/frmCustomerTransaction.aspx", false);
            }
            else if (ReportType.Equals("Refund"))
            {
                Response.Redirect("~/Billing/frmAdvancePayment.aspx", false);
            }
            else if (ReportType.Equals("ICUInvoice"))
            {
                Response.Redirect("~/Billing/frmICUInvoice.aspx", false);
            }
            else if (ReportType.Equals("TestInvoice"))
            {
                Response.Redirect("~/frmTestAllocation.aspx", false);
            }
            else if (ReportType.Equals("AdmitCard"))
            {
                Response.Redirect("~/frmAdmitPatient.aspx", false);
            }
            else if (ReportType.Equals("OPDPaper"))
            {
                Response.Redirect("~/frmOPDPatientDetail.aspx", false);
            }
            else if (ReportType.Equals("AdmitCardNew"))
            {
                Response.Redirect("~/frmOPDPatientDetail.aspx", false);
            }
            else if (ReportType.Equals("Discharge"))
            {
                Response.Redirect("~/frmMakeDischarge.aspx", false);
            }
            else if (ReportType.Equals("Salary"))
            {
                Response.Redirect("~/Payroll/frmSalaryCalculation.aspx", false);
            }
            else if (ReportType.Equals("ISSUE"))
            {
                Response.Redirect("~/Store/frmIssueMaterial.aspx", false);
            }
            else if (ReportType.Equals("DebitNoteReport"))
            {
                Response.Redirect("~/PathalogyReport/frmDebitNoteReport.aspx", false);
            }
            else if (ReportType.Equals("SuppReceipt"))
            {
                Response.Redirect("~/Store/frmSupplierPayment.aspx", false);
            }
            else if (ReportType.Equals("Death"))
            {
                Response.Redirect("~/PatientStatus/frmDeathCertificate.aspx", false);
            }
            else if (ReportType.Equals("Birth"))
            {
                Response.Redirect("~/PatientStatus/frmBirthCertificate.aspx", false);
            }
            else if (ReportType.Equals("Prescription"))
            {
                Response.Redirect("~/frmPrescription.aspx", false);
            }
            else if (ReportType.Equals("PurchaseOrder"))
            {
                Response.Redirect("~/Store/frmPurchaseOrder.aspx", false);
            }
            else if (ReportType.Equals("NursingFollowupSheet"))
            {
                Response.Redirect("~/frmDailyNursingManagement.aspx", false);
            }
        }
    }
}