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

namespace Hospital
{
    public partial class frmOPDCheckedPatient : System.Web.UI.Page
    {
        OPDPatientMasterBLL mobjOPDBLL = new OPDPatientMasterBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GetCheckedPatient();
                GetCompanies();
                ddlCompany.Enabled = false;

            }
        }

        private void GetCheckedPatient()
        {
            DataTable ldt = new DataTable();
            ldt = mobjOPDBLL.SelectCheckedPatient();
            if (ldt.Rows.Count > 0 && ldt != null)
            {
                dgvChekedPatient.DataSource = ldt;
                dgvChekedPatient.DataBind();
                Session["PatientDetails"] = ldt;
                int lintRowcount = ldt.Rows.Count;
                lblRowCount.Text = "<b>Total Records:</b> " + lintRowcount.ToString();
                pnl.Style.Add(HtmlTextWriterStyle.Display, "");
                hdnPanel.Value = "";
            }
            else
            {
                pnl.Style.Add(HtmlTextWriterStyle.Display, "none");
                hdnPanel.Value = "none";
            }
        }

        protected void dgvViewIPDPatient_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                DataTable ldt = new DataTable();
                DataTable ldtPatientDetail = new DataTable();
                if (e.CommandName == "OPDPatient")
                {
                    this.programmaticModalPopup.Show();
                    GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                    LinkButton lnkPatientCode = (LinkButton)gvr.FindControl("lnkPatientId");
                    string lstrPatientCode = lnkPatientCode.Text;
                    ldt = mobjOPDBLL.GetPatientByPID(lstrPatientCode);
                    FillData(ldt);
                    ldtPatientDetail = mobjOPDBLL.GetPatientDetailById(lstrPatientCode);
                    if (ldtPatientDetail.Rows.Count > 0 && ldtPatientDetail != null)
                    {
                        dgvMedicineDetail.DataSource = ldtPatientDetail;
                        dgvMedicineDetail.DataBind();
                    }
                }

                DataTable ldtOPDBillNo = new DataTable();
                ldtOPDBillNo = mobjOPDBLL.GetNewOPDBillNo();
                if (ldtOPDBillNo.Rows.Count > 0 && ldtOPDBillNo != null)
                {
                    txtBillNo.Text = ldtOPDBillNo.Rows[0]["OPDBillNo"].ToString();
                }
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmOPDCheckedPatient -  dgvViewIPDPatient_RowCommand(object sender, GridViewCommandEventArgs e)", ex);
            }
        }

        public void FillData(DataTable ldt)
        {
            txtPatientCode.Text = ldt.Rows[0]["PatientCode"].ToString();
            txtPatientName.Text = ldt.Rows[0]["PatientName"].ToString();
            txtBloodTest.Text = ldt.Rows[0]["BloodTests"].ToString();
            txtConsultant.Text = ldt.Rows[0]["ConsultantName"].ToString();
            txtDiagnosis.Text = ldt.Rows[0]["Diagnosis"].ToString();
            txtOPDRoom.Text = ldt.Rows[0]["OPDRoomNo"].ToString();
            txtSymptoms.Text = ldt.Rows[0]["Symptoms"].ToString();
            lblConsultantCharge.Text = ldt.Rows[0]["ConsultantCharge"].ToString();
            lblDressingCharge.Text = ldt.Rows[0]["DressingCharge"].ToString();
            lblInjectionCharge.Text = ldt.Rows[0]["InjectionCharge"].ToString();
            lblRevisitCharge.Text = ldt.Rows[0]["RevisitCharge"].ToString();
            txtTotalFees.Text = ldt.Rows[0]["TotalOPDBill"].ToString();
            lblPatientType.Text = ldt.Rows[0]["PatientVisitType"].ToString();
            txtOPDNo.Text = ldt.Rows[0]["OPDDiagnosisNo"].ToString();
        }
        protected void dgvChekedPatient_PageIndexChanged(object sender, EventArgs e)
        {
            dgvChekedPatient.DataSource = (DataTable)Session["PatientDetails"];
            dgvChekedPatient.DataBind();
        }
        protected void dgvChekedPatient_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvChekedPatient.PageIndex = e.NewPageIndex;
        }
        protected void dgvChekedPatient_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int lintCurrentPage = dgvChekedPatient.PageIndex + 1;
            lblPageCount.Text = "<b>Page</b> " + lintCurrentPage.ToString() + "<b> of </b>" + dgvChekedPatient.PageCount.ToString();
        }

        public void GetCompanies()
        {
            DataTable ldt = new DataTable();
            ldt = mobjOPDBLL.GetCompanies();
            ddlCompany.DataSource = ldt;
            ddlCompany.DataValueField = "CompanyCode";
            ddlCompany.DataTextField = "CompanyName";
            ddlCompany.DataBind();

            ListItem li = new ListItem();
            li.Text = "--Select Company--";
            li.Value = "0";
            ddlCompany.Items.Insert(0, li);
        }
        protected void ChkComp_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkComp.Checked)
            {
                ddlCompany.Enabled = true;
                txtReceived.Enabled = false;
            }
            else
            {
                ddlCompany.Enabled = false;
                txtReceived.Enabled = true;
            }
            this.programmaticModalPopup.Show();
        }
        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            string lstrCompanyCode = Commons.ConvertToString(ddlCompany.SelectedValue);
            string lstrPatientCode = txtPatientCode.Text;

            if (!Commons.IsRecordExists("tblPatientMaster", new string[] { "PatientCode", "CompanyCode" }, new string[] { lstrPatientCode, lstrCompanyCode }))
            {
                lblMessage.Text = "Patient Not Registered For This Company. Please Register";
                this.programmaticModalPopup.Show();
            }
            else
            {
                lblMessage.Text = "";
                this.programmaticModalPopup.Show();
            }
            this.programmaticModalPopup.Show();
        }
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            EntityOPDBilling entOPDBilling = new EntityOPDBilling();
            int cnt = 0;
            bool lbFlag = false;
            try
            {
                entOPDBilling.OPDBillNo = txtBillNo.Text;
                entOPDBilling.OPDNo = txtOPDNo.Text;
                entOPDBilling.PatientCode = txtPatientCode.Text;
                entOPDBilling.PatientName = txtPatientName.Text;
                entOPDBilling.ReceivedFees = Commons.ConvertToDecimal(txtReceived.Text);
                entOPDBilling.InjectionCharge = Commons.ConvertToDecimal(lblInjectionCharge.Text);
                entOPDBilling.RevisitCharge = Commons.ConvertToDecimal(lblRevisitCharge.Text);
                entOPDBilling.TotalFees = Commons.ConvertToDecimal(txtTotalFees.Text);
                entOPDBilling.DressingCharge = Commons.ConvertToDecimal(lblDressingCharge.Text);
                entOPDBilling.ConsultantCharges = Commons.ConvertToDecimal(lblConsultantCharge.Text);
                entOPDBilling.BalanceAmt = Commons.ConvertToDecimal(txtBalanceFees.Text);
                entOPDBilling.CompanyCode = string.Empty;
                entOPDBilling.EntryBy = SessionManager.Instance.LoginUser.EmpCode;
                entOPDBilling.BillStatus = "P";
                if (ChkComp.Checked)
                {
                    lbFlag = true;
                    entOPDBilling.CompanyCode = ddlCompany.SelectedValue;
                    entOPDBilling.BalanceAmt = Commons.ConvertToDecimal(txtTotalFees.Text);
                }

                if (!Commons.IsRecordExists("tblOPDBilling", "OPDBillNo", entOPDBilling.OPDBillNo))
                {

                    cnt = mobjOPDBLL.InsertOPDBill(entOPDBilling, lbFlag);

                    if (cnt > 0)
                    {
                        Commons.ShowMessage("OPD Bill Cleared....", this.Page);
                        GetCheckedPatient();
                        DataTable ldtReport = new DataTable();
                        ldtReport = mobjOPDBLL.GetOPDPatientDetailForReport(entOPDBilling.PatientCode, entOPDBilling.OPDBillNo);

                        if (ldtReport.Rows.Count > 0 && ldtReport != null)
                        {
                            
                        }
                        this.programmaticModalPopup.Hide();
                    }
                    else
                    {
                        Commons.ShowMessage("Error While Inserting OPD Bill....", this.Page);
                    }
                }
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmOPDCheckedPatient - BtnSave_Click(object sender, EventArgs e)", ex);
            }

        }
        protected void txtReceived_TextChanged(object sender, EventArgs e)
        {
            txtBalanceFees.Text = Commons.ConvertToString(Commons.ConvertToDecimal(txtTotalFees.Text) - Commons.ConvertToDecimal(txtReceived.Text));
            this.programmaticModalPopup.Show();
        }

        
    }
}