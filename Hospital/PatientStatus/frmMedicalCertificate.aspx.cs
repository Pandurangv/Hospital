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

namespace Hospital.PatientStatus
{
    public partial class frmMedicalCertificate : System.Web.UI.Page
    {
        MedicalCertificateBLL mobjBirthBLL = new MedicalCertificateBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                update.Value = Server.UrlEncode(System.DateTime.Now.ToString());
                GetBirthDetails();
                GetPatientList();
                MultiView1.SetActiveView(View1);
            }
        }

        private void GetBirthDetails()
        {
            try
            {
                List<EntityMedicalCertificate> ldtBirth = mobjBirthBLL.GetAllBirthDetails();
                if (ldtBirth.Count > 0)
                {
                    dgvShift.DataSource = ldtBirth;
                    dgvShift.DataBind();
                    //Session["DepartmentDetail"] = ldtBirth;
                    //Session["StartBirthDetails"] = ListConverter.ToDataTable(ldtBirth);
                    int lintRowcount = ldtBirth.Count;
                    lblRowCount.Text = "<b>Total Records:</b> " + lintRowcount.ToString();
                    pnlShow.Style.Add(HtmlTextWriterStyle.Display, "");
                    hdnPanel.Value = "";
                }
                else
                {
                    hdnPanel.Value = "none";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void BtnClose_Click(object sender, EventArgs e)
        {
            try
            {
                GetBirthDetails();
                MultiView1.SetActiveView(View1);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {

        }

        private void GetPatientList()
        {
            try
            {
                List<EntityMedicalCertificate> ldtRequisition = mobjBirthBLL.GetAllPatients();
                ldtRequisition.Insert(0, new EntityMedicalCertificate() { PatientAdmitID = 0, FullName = "----Select-----" });
                ddlPatientName.DataSource = ldtRequisition;
                ddlPatientName.DataTextField = "FullName";
                ddlPatientName.DataValueField = "PatientAdmitId";
                ddlPatientName.DataBind();
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            ViewState["update"] = update.Value;
        }

        protected void BtnAddNewDept_Click(object sender, EventArgs e)
        {
            Clear();
            btnUpdate.Visible = false;
            BtnSave.Visible = true;
            MultiView1.SetActiveView(View2);
            //this.programmaticModalPopup.Show();
        }

        private void Clear()
        {
            ddlPatientName.SelectedIndex = 0;
            txtAge.Text = Convert.ToString(0);
            txtDaignosis.Text = string.Empty;
            txtOPDFrom.Text = Convert.ToString(DateTime.Now);
            txtOPDTo.Text = Convert.ToString(DateTime.Now);
            txtIndoorOn.Text = Convert.ToString(DateTime.Now);
            txtDischargeOn.Text = Convert.ToString(DateTime.Now);
            txtOperatedFor.Text = string.Empty;
            txtOperatedForOn.Text = Convert.ToString(DateTime.Now);
            txtAdvisedRestDays.Text = Convert.ToString(0);
            txtAdvisedRestFrom.Text = Convert.ToString(DateTime.Now);
            txtContinueRestFrom.Text = Convert.ToString(DateTime.Now);
            txtContinuedRestDays.Text = Convert.ToString(0);
            txtWorkFrom.Text = Convert.ToString(DateTime.Now);
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int lintcnt = 0;
                EntityMedicalCertificate entDept = new EntityMedicalCertificate();

                if (ddlPatientName.SelectedIndex == 0)
                {
                    lblMsg.Text = "Please Patient Name";
                    return;
                }
                else
                {
                    entDept.PatientAdmitID = Convert.ToInt32(ddlPatientName.SelectedValue);
                    entDept.Age = Convert.ToInt32(txtAge.Text);
                    entDept.Daignosis = txtDaignosis.Text;
                    entDept.OPDFrom = StringExtension.ToDateTime(txtOPDFrom.Text);
                    entDept.OPDTo = StringExtension.ToDateTime(txtOPDTo.Text);
                    entDept.IndoorOn = StringExtension.ToDateTime(txtIndoorOn.Text);
                    entDept.DischargeOn = StringExtension.ToDateTime(txtDischargeOn.Text);
                    entDept.OperatedFor = txtOperatedFor.Text;
                    entDept.OperatedForOn = StringExtension.ToDateTime(txtOperatedForOn.Text);
                    entDept.AdvisedRestDays = txtAdvisedRestDays.Text;
                    entDept.AdvisedRestFrom = StringExtension.ToDateTime(txtAdvisedRestFrom.Text);
                    entDept.ContinueRestFrom = StringExtension.ToDateTime(txtContinueRestFrom.Text);
                    entDept.ContinuedRestDays = txtContinuedRestDays.Text;
                    entDept.WorkFrom = StringExtension.ToDateTime(txtWorkFrom.Text);
                    lintcnt = mobjBirthBLL.InsertBirthRecord(entDept);
                    if (lintcnt > 0)
                    {
                        GetBirthDetails();
                        lblMessage.Text = "Record Inserted Successfully....";
                        Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                    }
                    else
                    {
                        lblMessage.Text = "Record Not Inserted...";
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
            MultiView1.SetActiveView(View1);
        }

        void frmDepartmentMaster_Load(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        

        protected void chkDelete_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chk.NamingContainer;
            if (chk.Checked)
            {
                LinkButton lnkDeptCode = (LinkButton)row.FindControl("lnkDeptCode");
                DeptCode.Value = lnkDeptCode.Text;
            }
            else
            {
                DeptCode.Value = string.Empty;
            }
        }
        

        protected void dgvDepartment_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                List<EntityMedicalCertificate> ldtBirth = mobjBirthBLL.GetAllBirthDetails();
                if (!string.IsNullOrEmpty(txtSearch.Text))
                {
                    ldtBirth = ldtBirth.Where(p => p.FullName.Contains(txtSearch.Text)).ToList();
                }
                dgvShift.DataSource = ldtBirth;// Session["StartBirthDetails"];
                dgvShift.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
        
        protected void dgvDepartment_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvShift.PageIndex = e.NewPageIndex;
        }

        protected void dgvDepartment_DataBound(object sender, EventArgs e)
        {
            int lintCurrentPage = dgvShift.PageIndex + 1;
            lblPageCount.Text = "<b>Page</b> " + lintCurrentPage.ToString() + "<b> of </b>" + dgvShift.PageCount.ToString();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSearch.Text))
                {
                    BirthDetails(txtSearch.Text);
                }
                else
                {
                    lblMessage.Text = "Please Fill Search Text.";
                    txtSearch.Focus();
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        private void BirthDetails(string Prefix)
        {
            List<EntityMedicalCertificate> lst = mobjBirthBLL.SelectBirthDetails(Prefix);
            try
            {
                if (lst != null)
                {
                    dgvShift.DataSource = lst;
                    dgvShift.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtSearch.Text = string.Empty;
            GetBirthDetails();
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSearch.Text))
                {
                    Session["Details"] = Session["StartBirthDetails"];
                    Response.Redirect("~/ExcelReport/MonthwiseSalExcel.aspx", false);
                }
                else
                {
                    Session["Details"] = Session["Birth_Details"];
                    Response.Redirect("~/ExcelReport/MonthwiseSalExcel.aspx", false);
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            ImageButton imgEdit = (ImageButton)sender;
            GridViewRow row = (GridViewRow)imgEdit.NamingContainer;
            //Session["Certi_ID"] = Convert.ToInt32(dgvShift.DataKeys[row.RowIndex].Value);
            //Session["ReportType"] = "MedicalCertificate";
            Response.Redirect("~/PathalogyReport/PathologyReport.aspx?ReportType=MedicalCertificate&Certi_ID=" + dgvShift.DataKeys[row.RowIndex].Value, false);
        }
    }
}