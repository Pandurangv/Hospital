﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hospital.Models.BusinessLayer;
using Hospital.Models.Models;
using Hospital.Models.DataLayer;
using System.Data;
using Hospital.Models;

namespace Hospital.PathalogyReport
{
    public partial class frmPatientLedger : BasePage
    {
        PatientInvoiceBLL mobjDeptBLL = new PatientInvoiceBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ClearLabels();
                BindPatients();
            }
        }

        private void ClearLabels()
        {
            lbl.Text = string.Empty;
            lblFrom.Text = string.Empty;
            lblTo.Text = string.Empty;
            lblAge.Text = string.Empty;
            lblBloodGroup.Text = string.Empty;
            lblSex.Text = string.Empty;
        }
        private void BindPatients()
        {
            try
            {
                List<EntityPatientMaster> tblpatient = new CustomerTransactionBLL().GetAllocatedPatient();
                tblpatient.Insert(0, new EntityPatientMaster() { PatientId = 0, FullName = "----Select----" });
                ddlPatient.DataSource = tblpatient;
                ddlPatient.DataTextField = "FullName";
                ddlPatient.DataValueField = "PatientId";
                ddlPatient.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                lbl.Text = "Patient Ledger";
                lblFrom.Text = ddlPatient.SelectedItem.Text;
                lblTo.Text = string.Format("{0:dd-MMM-yyyy}", DateTime.Now);
                lblMessage.Text = string.Empty;

                List<tblCustomerTransaction> lst = new CustomerTransactionBLL().GetPatientTransByPatientId(Convert.ToInt32(ddlPatient.SelectedValue));
                EntityPatientMaster objPatient = new PatientMasterBLL().GetPatientDetailsByPatientId(Convert.ToInt32(ddlPatient.SelectedValue));
                if (objPatient != null)
                {
                    lblAge.Text = Convert.ToString(objPatient.Age);
                    MRN.Text = Convert.ToString(objPatient.PatientCode);
                    lblSex.Text = Convert.ToString(objPatient.GenderDesc);
                    lblBloodGroup.Text = Convert.ToString(objPatient.BloodGroup);
                }
                tblCustomerTransaction obj = new tblCustomerTransaction() { };
                lst.Add(obj);
                tblCustomerTransaction objBills = new tblCustomerTransaction()
                {
                    TransactionType = "Total Invoices Amt",
                    BillAmount = Convert.ToDecimal(lst.Sum(p => p.BillAmount))
                };
                lst.Add(objBills);
                tblCustomerTransaction objReceived = new tblCustomerTransaction()
                {
                    TransactionType = "Total Received",
                    PayAmount = Convert.ToDecimal(lst.Sum(p => p.PayAmount)) + Convert.ToDecimal(lst.Sum(p => p.AdvanceAmount))
                };
                lst.Add(objReceived);

                tblCustomerTransaction objFinal = new tblCustomerTransaction()
                {
                    TransactionType = "Balance(+)/Refund(-) Amt",

                    BillAmount = objBills.BillAmount - objReceived.PayAmount
                };
                lst.Add(objFinal);

                dgvTestParameter.DataSource = lst;
                dgvTestParameter.DataBind();
                Session["PatientLedg"] = ListConverter.ToDataTable(lst);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        private void SelectBedConsumtion()
        {

        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(lblFrom.Text) && !string.IsNullOrEmpty(lblTo.Text))
                {
                    Session["Details"] = Session["PatientLedg"];
                    Response.Redirect("~/ExcelReport/MonthwiseSalExcel.aspx");
                }
                else
                {
                    lblMessage.Text = "Please Select Patient Name";
                }

            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void ddlPatient_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                EntityPatientMaster ldtRequisition = new OPDPatientMasterBLL().GetPatientList().Where(p => p.AdmitId == Convert.ToUInt32(ddlPatient.SelectedValue)).FirstOrDefault();// && p.DeptDoctorId == Convert.ToInt32(ddlDoctors.SelectedValue) && p.PatientType.ToUpper() == "OPD").ToList();
                MRN.Text = ldtRequisition.PatientCode;
                lblSex.Text = ldtRequisition.Gender == 1 ? "Male" : "Female";
                lblBloodGroup.Text = ldtRequisition.BloodGroup;
                lblAge.Text = ldtRequisition.Age.ToString();
            }
            catch (Exception ex)
            {
            }
        }
    }
}