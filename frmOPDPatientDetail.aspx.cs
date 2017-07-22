using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Hospital.Models.BusinessLayer;
using System.Data;
using Hospital.Models.Models;
using Hospital.Models.DataLayer;
using Hospital.Models;

namespace Hospital
{
    public partial class frmOPDPatientDetail : BasePage
    {

        OPDPatientMasterBLL mobjPatientMasterBLL = new OPDPatientMasterBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            base.AuthenticateUser();
            if (!Page.IsPostBack)
            {
                GetOPDPatientList();
                pnlShow.Visible = true;
                pnl.Visible = true;
                MultiView1.SetActiveView(View1);
            }
        }

        public void GetOPDPatientList()
        {
            List<EntityPatientMaster> ldtRequisition = mobjPatientMasterBLL.GetPatientList();
            if (SessionManager.Instance.LoginUser.UserType.ToLower()=="doctor")
            {
                ldtRequisition = ldtRequisition.Where(p => p.DeptDoctorId == SessionManager.Instance.LoginUser.PKId).ToList();
            }
            if (ldtRequisition.Count > 0 && ldtRequisition != null)
            {
                dgvPatientList.DataSource = ldtRequisition;
                dgvPatientList.DataBind();
                int lintRowcount = ldtRequisition.Count;
                lblRowCount.Text = "<b>Total Records:</b> " + lintRowcount.ToString();
            }
        }

        protected void dgvOPDPatientDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditPatient")
            {
                DataTable ldt = new DataTable();
                DataTable ldtConCharge = new DataTable();
                ldt.Columns.Add("SrNo");
                ldt.Columns.Add("DrugName");
                ldt.Columns.Add("Morning");
                ldt.Columns.Add("AfterNoon");
                ldt.Columns.Add("Night");
                ldt.Rows.Add("1", "", "", "", "");
                ViewState["CurrentTable"] = ldt;
                GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                LinkButton lnkDeptCode = (LinkButton)gvr.FindControl("lnkPatientCode");
                Label lblPatientName = (Label)gvr.FindControl("lbPatientName");
                Label lblAppointNo = (Label)gvr.FindControl("lblAppNo");
                AppointmentNo.Value = lblAppointNo.Text;
                Label lblConsultantId = (Label)gvr.FindControl("lblConsultantId");
                ldtConCharge = mobjPatientMasterBLL.GetOPDChargesForConsultant(Commons.ConvertToInt(lblConsultantId.Text));
                if (ldtConCharge.Rows.Count > 0 && ldtConCharge != null)
                {
                    ConsultantCharge.Value = ldtConCharge.Rows[0]["Charge"].ToString();
                }
                else
                {
                    Commons.ShowMessage("Please Set Consultant Fees In Master....", this.Page);
                    return;
                }
                ConsultantId.Value = lblConsultantId.Text;
            }

        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton imgEdit = (ImageButton)sender;
                GridViewRow cnt = (GridViewRow)imgEdit.NamingContainer;
                DataTable dt = new PatientMasterBLL().GetPatientDetail(cnt.Cells[0].Text);
                Response.Redirect("~/frmOPDPatient.aspx?PatientId=" + cnt.Cells[0].Text + "&IsEdit=" + true,false);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void dgvPatientList_DataBound(object sender, EventArgs e)
        {
            int lintCurrentPage = dgvPatientList.PageIndex + 1;
            lblPageCount.Text = "<b>Page</b> " + lintCurrentPage.ToString() + "<b> of </b>" + dgvPatientList.PageCount.ToString();
        }
        protected void dgvPatientList_PageIndexChanged(object sender, EventArgs e)
        {
            List<EntityPatientMaster> ldtRequisition = mobjPatientMasterBLL.GetPatientList();
            if (!string.IsNullOrEmpty(txtSearch.Text))
            {
                ldtRequisition = ldtRequisition.Where(p => p.FullName.Contains(txtSearch.Text) || p.Address.Contains(txtSearch.Text)).ToList();
            }
            if (SessionManager.Instance.LoginUser.UserType.ToLower() == "doctor")
            {
                ldtRequisition = ldtRequisition.Where(p => p.DeptDoctorId == SessionManager.Instance.LoginUser.PKId).ToList();
            }
            dgvPatientList.DataSource = ldtRequisition;
            dgvPatientList.DataBind();
        }
        protected void dgvPatientList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvPatientList.PageIndex = e.NewPageIndex;
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            dgvPatientList.PageIndex = 0;
            GetOPDPatientList();
            txtSearch.Text = string.Empty;
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSearch.Text))
                {
                    SelectPatient(txtSearch.Text);
                }
                else
                {
                    lblMessage.Text = "Please fill search text.";
                    txtSearch.Focus();
                }

            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        private void SelectPatient(string Prefix)
        {
            OPDPatientMasterBLL objOPDPatient = new OPDPatientMasterBLL();
            List<EntityPatientMaster> lst = objOPDPatient.SearchPatient(Prefix);
            if (SessionManager.Instance.LoginUser.UserType.ToLower() == "doctor")
            {
                lst = lst.Where(p => p.DeptDoctorId == SessionManager.Instance.LoginUser.PKId).ToList();
            }
            if (lst != null)
            {
                dgvPatientList.DataSource = lst;
                dgvPatientList.DataBind();

                lblRowCount.Text = string.Empty;
            }
        }
        protected void AddNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmOPDPatient.aspx?IsEdit=" + false, false);
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton imgEdit = (ImageButton)sender;
                GridViewRow row = (GridViewRow)imgEdit.NamingContainer;
                int AdmitId = Convert.ToInt32(dgvPatientList.DataKeys[row.RowIndex].Value);
                Response.Redirect("~/PathalogyReport/PathologyReport.aspx?AdmitId=" + AdmitId + "&ReportType=OPDPaper", false);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
    }
}