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
using Hospital.Models;

namespace Hospital.PatientStatus
{
    public partial class frmDeathCertificate : BasePage
    {
        DeathCertificateBLL mobjDeptBLL = new DeathCertificateBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            base.AuthenticateUser();
            if (!Page.IsPostBack)
            {
                update.Value = Server.UrlEncode(System.DateTime.Now.ToString());
                GetDeathDetails();
                GetPatientList();
                MultiView1.SetActiveView(View1);
            }
        }

        private void GetDeathDetails()
        {
            try
            {
                List<EntityDeathCertificate> ldtDeath = mobjDeptBLL.GetAllDeathDetails();
                if (ldtDeath.Count > 0)
                {
                    dgvShift.DataSource = ldtDeath;
                    dgvShift.DataBind();
                    //Session["DepartmentDetail"] = ldtDeath;
                    //Session["StartDeathDetails"] = ListConverter.ToDataTable(ldtDeath);
                    int lintRowcount = ldtDeath.Count;
                    lblRowCount.Text = "<b>Total Records:</b> " + lintRowcount.ToString();
                }
                else
                {
                    dgvShift.DataSource = ldtDeath;
                    dgvShift.DataBind();
                    //Session["DepartmentDetail"] = ldtDeath;
                    //Session["StartDeathDetails"] = ListConverter.ToDataTable(ldtDeath);
                    int lintRowcount = ldtDeath.Count;
                    lblRowCount.Text = "<b>Total Records:</b> " + lintRowcount.ToString();
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
                GetDeathDetails();
                MultiView1.SetActiveView(View1);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        private void GetPatientList()
        {
            try
            {
                List<EntityDeathCertificate> ldtRequisition = mobjDeptBLL.GetAllPatients();
                ldtRequisition.Insert(0, new EntityDeathCertificate() { PatientAdmitId = 0, FullName = "----Select-----" });
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
        }

        private void Clear()
        {
            StartTimeSelector.ReadOnly = false;
            StartTimeSelector.Hour = DateTime.Now.Hour;
            StartTimeSelector.Minute = DateTime.Now.Minute;
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int lintcnt = 0;
                EntityDeathCertificate entDept = new EntityDeathCertificate();

                if (ddlPatientName.SelectedIndex == 0)
                {
                    lblMsg.Text = "Please Patient Name";
                    return;
                }
                else
                {
                    if (string.IsNullOrEmpty(txtDate.Text))
                    {
                        lblMsg.Text = "Please Select Date";
                        return;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(txtReason.Text))
                        {
                            lblMsg.Text = "Please Enter The Reason Of Death";
                            return;
                        }
                        else
                        {
                            entDept.PatientAdmitId = Convert.ToInt32(ddlPatientName.SelectedValue);
                            entDept.Death_Date = StringExtension.ToDateTime(txtDate.Text);
                            TimeSpan objTime = new TimeSpan(StartTimeSelector.Hour, StartTimeSelector.Minute, 0);
                            DateTime dt = DateTime.Now.Date;
                            dt = dt.Add(objTime);
                            entDept.Death_Time = dt;

                            entDept.Death_Reason = txtReason.Text;
                            lintcnt = mobjDeptBLL.InsertDeathRecord(entDept);
                            if (lintcnt > 0)
                            {
                                GetDeathDetails();
                                lblMessage.Text = "Record Inserted Successfully....";
                                update.Value = Server.UrlEncode(System.DateTime.Now.ToString());
                            }
                            else
                            {
                                lblMessage.Text = "Record Not Inserted...";
                            }
                        }
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
                //lblMessage.Text = string.Empty;
            }
            else
            {
                DeptCode.Value = string.Empty;
            }
        }
        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow drv in dgvShift.Rows)
            {
                CheckBox chkDelete = (CheckBox)drv.FindControl("chkDelete");
                if (chkDelete.Checked)
                {
                }
            }
        }

        protected void dgvDepartment_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                List<EntityDeathCertificate> ldtDeath = mobjDeptBLL.GetAllDeathDetails();
                if (!string.IsNullOrEmpty(txtSearch.Text))
                {
                    ldtDeath = ldtDeath.Where(p => p.FullName.Contains(txtSearch.Text)).ToList();
                }
                dgvShift.DataSource = ldtDeath;// (List<sp_GetAllBedAllocResult>)Session["DepartmentDetail"];
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
                    DeathDetails(txtSearch.Text);
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

        private void DeathDetails(string Prefix)
        {
            try
            {
                List<EntityDeathCertificate> lst = mobjDeptBLL.SelectDeathDetails(Prefix);
                if (lst != null)
                {
                    dgvShift.DataSource = lst;
                    dgvShift.DataBind();
                    //Session["Death_Details"] = ListConverter.ToDataTable(lst);
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
            GetDeathDetails();
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                List<EntityDeathCertificate> ldtDeath = new DeathCertificateBLL().GetAllDeathDetails();
                if (string.IsNullOrEmpty(txtSearch.Text))
                {
                    //Session["Details"] = Session["StartDeathDetails"];
                    Response.Redirect("~/ExcelReport/MonthwiseSalExcel.aspx?Rpt=Death", false);
                }
                else
                {
                    //Session["Details"] = Session["Death_Details"];
                    Response.Redirect("~/ExcelReport/MonthwiseSalExcel.aspx?Rpt=Death", false);
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
            //Death_Id.Value = Convert.ToString(dgvShift.DataKeys[row.RowIndex].Value);
            //Session["ReportType"] = "Death";
            Response.Redirect("~/PathalogyReport/PathologyReport.aspx?ReportType=Death&Death_Id=" + Death_Id.Value, false);
        }
    }
}