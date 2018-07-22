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

namespace Hospital
{
    public partial class frmPatientAllocToDoc : BasePage
    {
        PatientAllocDocBLL mobjPatAllocBLL = new PatientAllocDocBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            base.AuthenticateUser("frmPatientAllocToDoc.aspx");
            if (!Page.IsPostBack)
            {
                BindAllocatedPatient();
                CalAppDate.StartDate = DateTime.Now.Date;
                GetPatientList();
                GetDoctortList();
                MultiView1.SetActiveView(View1);
            }
        }

        private void BindAllocatedPatient()
        {
            List<EntityPatAllocDoc> ldtDept = mobjPatAllocBLL.GetAllAllocPatient();
            if (ldtDept.Count > 0)
            {
                dgvAllocPatient.DataSource = ldtDept;
                dgvAllocPatient.DataBind();
                //Session["DepartmentDetail"] = ldtDept;
                int lintRowcount = ldtDept.Count;
                lblRowCount.Text = "<b>Total Records:</b> " + lintRowcount.ToString();
            }
        }

        private void GetDoctortList()
        {
            try
            {
                var tblCat = new PatientAllocDocBLL().GetAllDoctor();

                tblCat.Insert(0, new sp_GetAllDoctorListResult() { FullName = "----Select------", PKId = 0 });

                ddlDoctorName.DataSource = tblCat;
                ddlDoctorName.DataValueField = "PKId";
                ddlDoctorName.DataTextField = "FullName";
                ddlDoctorName.DataBind();
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
                List<sp_GetAllNonAllocPatientListResult> tblCat = new PatientAllocDocBLL().GetAllNonAllocPatient();
                if (SettingsManager.Instance.IsIPDOnly)
                {
                    tblCat = tblCat.Where(p => p.PatientType == "IPD").ToList();
                }
                tblCat.Insert(0, new sp_GetAllNonAllocPatientListResult() { AdmitId=0,FullName="---Select---"});
                ddlPatientName.DataSource = tblCat;
                ddlPatientName.DataValueField = "AdmitId";
                ddlPatientName.DataTextField = "FullName";
                ddlPatientName.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
        }

        protected void BtnAddNewRoom_Click(object sender, EventArgs e)
        {
            //txtPatientName.Visible = false;
            ddlPatientName.Visible = true;
            DataTable ldt = new DataTable();
            BtnSave.Visible = true;
            btnUpdate.Visible = false;
            Clear();
            MultiView1.SetActiveView(View2);
        }
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int lintcnt = 0;

                EntityPatientAlloc entPat = new EntityPatientAlloc();

                if (ddlPatientName.SelectedIndex == 0)
                {
                    lblMsg.Text = "Please Select Patient Name";
                }
                else
                {
                    if (ddlDoctorName.SelectedIndex == 0)
                    {
                        lblMsg.Text = "Please Select Doctor Name";
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(txtAppDate.Text))
                        {
                            lblMsg.Text = "Please Select Appointment Date";
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(txtCharge.Text))
                            {
                                lblMsg.Text = "Please Enter Charges";
                            }
                            else
                            {
                                entPat.PatientId = Convert.ToInt32(ddlPatientName.SelectedValue);
                                entPat.DocId = Convert.ToInt32(ddlDoctorName.SelectedValue);
                                entPat.AppDate = StringExtension.ToDateTime(txtAppDate.Text).Date;
                                entPat.Charges = Convert.ToDecimal(txtCharge.Text);

                                if (!mobjPatAllocBLL.IsRecordExists(Convert.ToString(entPat.PatientId), Convert.ToString(entPat.DocId), entPat.AppDate))
                                {
                                    lintcnt = mobjPatAllocBLL.InsertAllocPat(entPat);

                                    if (lintcnt > 0)
                                    {
                                        BindAllocatedPatient();
                                        lblMessage.Text = "Record Inserted Successfully....";
                                        Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                                    }
                                    else
                                    {
                                        lblMessage.Text = "Record Not Inserted...";
                                    }
                                }
                                else
                                {
                                    lblMessage.Text = "Record Already Exist....";
                                }
                                Clear();
                                MultiView1.SetActiveView(View1);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        public void Clear()
        {
            ddlPatientName.SelectedIndex = 0;
            ddlDoctorName.SelectedIndex = 0;
            txtPatientType.Text = string.Empty;
            txtAppDate.Text = string.Empty;
            //txtStartTime.Text = string.Empty;
            //txtEndTime.Text = string.Empty;
            txtCharge.Text = string.Empty;
        }

        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            int lintCnt = 0;
            try
            {
                EntityPatAllocDoc entMix = new EntityPatAllocDoc();
                entMix.PKId = Convert.ToInt32(pat_id.Value);
                entMix.Emp_Id = Convert.ToInt32(ddlDoctorName.SelectedValue);
                entMix.AppointDate = StringExtension.ToDateTime(txtAppDate.Text);
                entMix.Charges = Convert.ToDecimal(txtCharge.Text);
                PatientAllocDocBLL objOtherAcc = new PatientAllocDocBLL();
                lintCnt = objOtherAcc.Update(entMix);
                if (lintCnt > 0)
                {
                    BindAllocatedPatient();
                    lblMessage.Text = "Appointment Updated Successfully";
                }
                else
                {
                    lblMessage.Text = "Appointment Not Updated";
                }
                MultiView1.SetActiveView(View1);
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }


        protected void BtnClose_Click(object sender, EventArgs e)
        {
            try
            {
                Clear();
                BindAllocatedPatient();
                MultiView1.SetActiveView(View1);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton imgEdit = (ImageButton)sender;
                GridViewRow cnt = (GridViewRow)imgEdit.NamingContainer;
                pat_id.Value = dgvAllocPatient.DataKeys[cnt.RowIndex].Value.ToString();
                bool IsDischarge = new PatientAllocDocBLL().GetDischargeInfo(Convert.ToInt32(pat_id.Value));
                if (IsDischarge == false)
                {
                    ListItem patient = ddlPatientName.Items.FindByText(cnt.Cells[1].Text);
                    ddlPatientName.SelectedValue = patient.Value;
                    ddlPatientName_SelectedIndexChanged(sender, e);
                    ListItem Desig = ddlDoctorName.Items.FindByText(cnt.Cells[2].Text);
                    ddlDoctorName.SelectedValue = Desig.Value;
                    txtAppDate.Text = string.Format("{0:dd/MM/yyy}", StringExtension.ToDateTime(cnt.Cells[3].Text));
                    //txtStartTime.Text = cnt.Cells[4].Text;
                    //txtEndTime.Text = cnt.Cells[5].Text;
                    txtCharge.Text = cnt.Cells[6].Text;
                    btnUpdate.Visible = true;
                    BtnSave.Visible = false;
                    MultiView1.SetActiveView(View2);
                }
                else
                {
                    lblMessage.Text = "Patient Already Discharged..";
                }
                //txtPatientName.Visible = true;
                //ddlPatientName.Visible = false;

                //EntityPatAllocDoc objEdit = new PatientAllocDocBLL().SelectPatRecForEdit(Convert.ToInt32(cnt.Cells[0].Text));
                //if (objEdit != null)
                //{
                //    txtAppDate.Text = string.Format("{0:MM/dd/yyy}", objEdit.AppointDate);
                //    //txtPatientName.Text = Convert.ToString(objEdit.PatientName);
                //    txtPatientType.Text = objEdit.Pat_Type;
                //    ListItem Desig = ddlDoctorName.Items.FindByText(Convert.ToString(objEdit.EmpName));
                //    ddlDoctorName.SelectedValue = Desig.Value;
                //    txtStartTime.Text = string.Format("{0:hh:mm tt}", objEdit.StartTime);
                //    txtEndTime.Text = string.Format("{0:hh:mm tt}", objEdit.EndTime);
                //    txtCharge.Text = Convert.ToString(objEdit.Charges);

                //}

            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
        protected void dgvRoomMaster_DataBound(object sender, EventArgs e)
        {
            int lintCurrentPage = dgvAllocPatient.PageIndex + 1;
            lblPageCount.Text = "<b>Page</b> " + lintCurrentPage.ToString() + "<b> of </b>" + dgvAllocPatient.PageCount.ToString();
        }
        protected void dgvRoomMaster_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                List<EntityPatAllocDoc> ldtDept = mobjPatAllocBLL.GetAllAllocPatient();
                if (!string.IsNullOrEmpty(txtSearch.Text))
                {
                    ldtDept = ldtDept.Where(p => p.PatientName.Contains(txtSearch.Text) || p.EmpName.Contains(txtSearch.Text)).ToList();
                }
                dgvAllocPatient.DataSource = ldtDept;
                dgvAllocPatient.DataBind();
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmRoomMaster - dgvRoomMaster_PageIndexChanged(object sender, EventArgs e)", ex);
            }
        }
        protected void dgvRoomMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvAllocPatient.PageIndex = e.NewPageIndex;
        }


        protected void ddlPatientName_SelectedIndexChanged(object sender, EventArgs e)
        {
            int Val = Convert.ToInt32(ddlPatientName.SelectedValue);
            EntityPatientMaster ldtRequisition = new OPDPatientMasterBLL().GetPatientList().Where(p => p.IsDischarged == false && p.AdmitId == Convert.ToInt32(Val)).FirstOrDefault();
            CalAppDate.StartDate = ldtRequisition.PatientAdmitDate;
            txtPatientType.Text = ldtRequisition.PatientType;
            txtCharge.Text = Convert.ToString(ldtRequisition.ConsultingCharges);
            if (ldtRequisition.DeptDoctorId>0)
            {
                ListItem item = ddlDoctorName.Items.FindByValue(ldtRequisition.DeptDoctorId.ToString());
                if (item!=null)
                {
                    ddlDoctorName.SelectedValue = item.Value;
                }
            }
        }
        //protected void ddlDoctorName_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    Doc_Id.Value = ddlDoctorName.SelectedValue;
        //    EntityShift objShift = new PatientAllocDocBLL().GetStartEndTime(Convert.ToInt32(Doc_Id.Value));
        //    txtStartTime.Text = string.Format("{0:hh:mm tt}", objShift.StartTime);
        //    txtEndTime.Text = string.Format("{0:hh:mm tt}", objShift.EndTime);
        //}
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSearch.Text))
                {
                    SearchPatientAllocation(txtSearch.Text);
                }
                else
                {
                    lblMessage.Text = "Please Fill Search text.";
                    txtSearch.Focus();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void SearchPatientAllocation(string Prefix)
        {
            List<EntityPatAllocDoc> lst = mobjPatAllocBLL.SelectPatientAllocation(Prefix);
            if (lst != null)
            {
                dgvAllocPatient.DataSource = lst;
                dgvAllocPatient.DataBind();
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtSearch.Text = string.Empty;
            BindAllocatedPatient();
        }
    }
}