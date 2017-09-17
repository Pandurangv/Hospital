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
    public partial class frmAdmitPatient : BasePage
    {
        ProductBLL mobjProductBLL = new ProductBLL();
        OPDPatientMasterBLL mobjPatientMasterBLL = new OPDPatientMasterBLL();
        PatientMasterBLL mobjPatient = new PatientMasterBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            base.AuthenticateUser();
            if (!Page.IsPostBack)
            {
                string tt=string.Format("{0:tt}",DateTime.Now);
                MKB.TimePicker.TimeSelector.AmPmSpec time=tt=="AM"?MKB.TimePicker.TimeSelector.AmPmSpec.AM:MKB.TimePicker.TimeSelector.AmPmSpec.PM;
                AdmissionTimeSelector.SetTime(DateTime.Now.Hour, DateTime.Now.Minute, time);
                GetPatientList();
                BindPatientTypes();
                GetDeptCategory();
                ddlCompName.Enabled = false;
                ddlInsurance.Enabled = false;
                GetOPDPatientList();
                GetInsuranceCompanies();
                GetCompanies();
                MultiView1.SetActiveView(View1);
            }
        }

        private void BindPatientTypes()
        {
            List<tblPatientType> lst = mobjPatient.GetPatientTypes();
            lst.Insert(0, new tblPatientType() { PatientTypeId = 0, PatientType = "--Select--" });
            ddlPatientType.DataSource = lst;
            ddlPatientType.DataValueField = "PatientTypeId";
            ddlPatientType.DataTextField = "PatientType";
            ddlPatientType.DataBind();
        }

        protected void txtNew_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtNew.Text))
                {
                    GetPatientListsearch(txtNew.Text);
                }
                else
                {
                    GetPatientList();
                }

            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void ddlPatientName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPatientName.SelectedIndex > 0)
            {
                tblPatientMaster obj = new PatientMasterBLL().GetPatientbyId(Convert.ToInt32(ddlPatientName.SelectedValue));
                if (Convert.ToBoolean(obj.IsDeath))
                {
                    lblMsg.Text = "This patient can not be re-admitted. This patient is already passed away.";
                    ddlPatientName.SelectedIndex = 0;
                }
                else
                {
                    if (obj.BirthDate != null)
                    {
                        lblDOB.Text = obj.BirthDate.Value.ToShortDateString();
                        txtAge.Text = Convert.ToString(DateTime.Now.Date.Year - obj.BirthDate.Value.Year);
                        //ddlAge.Text = obj.AgeIn;
                        txtWeight.Text = Convert.ToString(obj.Weight);
                    }
                    else
                    {
                        txtAge.Text = Convert.ToString(obj.Age);
                        //ddlAge.Text = obj.AgeIn;
                        txtWeight.Text = Convert.ToString(obj.Weight);
                        //lblDOB.Text = obj.BirthDate.Value.ToShortDateString();
                    }
                }
            }
        }

        protected void OPD_CheckedChanged(object sender, EventArgs e)
        {
            lblDiagnosis.Visible = false;
            txtDignosys.Visible = false;
            //lblIPDNo.Text = "OPD No :";
            //DataTable ldt1 = new DataTable();
            //ldt1 = mobjPatient.GetNewOPDNumber();
            //if (ldt1.Rows.Count > 0 && ldt1 != null)
            //{
            //    txtIPDNo.Text = ldt1.Rows[0]["PatientOPDNo"].ToString();
            //}
        }

        protected void IPD_CheckedChanged(object sender, EventArgs e)
        {
            lblDiagnosis.Visible = true;
            txtDignosys.Visible = true;
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

        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                txtSearch.Text = string.Empty;
                GetOPDPatientList();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void ImageShift_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton imgEdit = (ImageButton)sender;
                GridViewRow cnt = (GridViewRow)imgEdit.NamingContainer;
                string PatientType = cnt.Cells[7].Text;
                if (PatientType.Equals("OPD", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (mobjPatient.ShiftToIPD(Convert.ToInt32(dgvPatientList.DataKeys[cnt.RowIndex].Value)) > 0)
                    {
                        GetOPDPatientList();
                    }
                }
                else
                {
                    lblMessage.Text = "Patient Type Already IPD";
                }
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
                List<sp_GetAllPatientListForRegisteredResult> ldtRequisition = mobjPatientMasterBLL.GetPatientListForRegistered(txtSearch.Text);
                if (ldtRequisition.Count > 0 && ldtRequisition != null)
                {
                    if (SessionManager.Instance.LoginUser.UserType.ToLower() == "doctor")
                    {
                        ldtRequisition = ldtRequisition.Where(p => p.DeptDoctorId == SessionManager.Instance.LoginUser.PKId).ToList();
                    }
                    dgvPatientList.DataSource = ldtRequisition;
                    dgvPatientList.DataBind();
                    //Session["PatientList"] = ldtRequisition;
                    int lintRowcount = ldtRequisition.Count;
                    lblRowCount.Text = "<b>Total Records:</b> " + lintRowcount.ToString();
                }
                else
                {
                    dgvPatientList.DataSource = new List<sp_GetAllPatientListForRegisteredResult>();
                    dgvPatientList.DataBind();
                    //Session["PatientList"] = new List<sp_GetAllPatientListForRegisteredResult>();
                    lblRowCount.Text = "<b>Total Records:</b> " + 0;
                }
                //txtSearch.Text = string.Empty;
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
                List<EntityPatientMaster> ldtRequisition = mobjPatientMasterBLL.GetAllPatients();
                
                ldtRequisition.Insert(0, new EntityPatientMaster() { PatientId = 0, FullName = "----Select----" });
                ddlPatientName.DataSource = ldtRequisition;
                ddlPatientName.DataTextField = "FullName";
                ddlPatientName.DataValueField = "PatientId";
                ddlPatientName.DataBind();
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        private void GetPatientListsearch(string Prefix)
        {
            try
            {
                List<EntityPatientMaster> ldtRequisition = mobjPatientMasterBLL.GetAllPatientssearch(Prefix);
                ldtRequisition.Insert(0, new EntityPatientMaster() { PatientId = 0, FullName = "----Select----" });
                ddlPatientName.DataSource = ldtRequisition;
                ddlPatientName.DataTextField = "FullName";
                ddlPatientName.DataValueField = "PatientId";
                ddlPatientName.DataBind();
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void ddlDeptCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int linReligionId = Convert.ToInt32(ddlDeptCategory.SelectedValue);
                DataTable ldt = new DataTable();
                ldt = mobjPatient.GetDeptDoctors(linReligionId);
                if (ldt.Rows.Count > 0 && ldt != null)
                {
                    ddlDeptDoctor.DataSource = ldt;
                    ddlDeptDoctor.DataValueField = "DocAllocId";
                    ddlDeptDoctor.DataTextField = "EmpName";
                    ddlDeptDoctor.DataBind();

                    FillDeptDoctorCast();
                    ddlDeptDoctor.Enabled = true;
                }
                else
                {
                    ddlDeptDoctor.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        private void FillDeptDoctorCast()
        {
            try
            {
                ListItem li = new ListItem();
                li.Text = "--Select Dept.Doctor--";
                li.Value = "0";
                ddlDeptDoctor.Items.Insert(0, li);
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        public void GetDeptCategory()
        {
            try
            {
                DataTable ldt = new DataTable();
                ldt = mobjPatient.GetDeptCategory();
                ddlDeptCategory.DataSource = ldt;
                ddlDeptCategory.DataValueField = "CategoryId";
                ddlDeptCategory.DataTextField = "CategoryName";
                ddlDeptCategory.DataBind();

                ListItem li = new ListItem();
                li.Text = "--Select DeptCategory--";
                li.Value = "0";
                ddlDeptCategory.Items.Insert(0, li);
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        public void GetInsuranceCompanies()
        {
            try
            {
                var ldt = mobjPatient.GetInsuranceCompanies();
                ddlInsurance.DataSource = ldt;
                ddlInsurance.DataValueField = "PKId";
                ddlInsurance.DataTextField = "InsuranceDesc";
                ddlInsurance.DataBind();
                ListItem li = new ListItem();
                li.Text = "--Select Company--";
                li.Value = "0";
                ddlInsurance.Items.Insert(0, li);
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }

        }

        protected void ChkInsurance_CheckedChanged(object sender, EventArgs e)
        {
            ddlInsurance.Enabled = true;
        }

        protected void chkCom_CheckedChanged(object sender, EventArgs e)
        {
            ddlCompName.Enabled = true;
        }

        public void GetCompanies()
        {
            try
            {
                DataTable ldt = new DataTable();
                ldt = mobjPatient.GetCompanies();
                ddlCompName.DataSource = ldt;
                ddlCompName.DataValueField = "CompanyCode";
                ddlCompName.DataTextField = "CompanyName";
                ddlCompName.DataBind();
                ListItem li = new ListItem();
                li.Text = "--Select Company--";
                li.Value = "0";
                ddlCompName.Items.Insert(0, li);
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }

        }

        protected void BtnAddNewProduct_Click(object sender, EventArgs e)
        {
            Clear();
            txtAdmitDate.Enabled = false;
            txtNew.Text = string.Empty;
            txtAge.Text = string.Empty;
            txtWeight.Text = string.Empty;
            txtAdmitDate.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Now.Date);
            AdmissionTimeSelector.Enabled = true;
            BtnSave.Visible = true;
            btnUpdate.Visible = false;
            MultiView1.SetActiveView(View2);
        }

        private void Clear()
        {
            ddlPatientName.SelectedIndex = 0;
            ddlDeptCategory.SelectedIndex = 0;
            //ddlAge.SelectedIndex = 0;
            txtDignosys.Text = string.Empty;
            ddlCompName.SelectedIndex = 0;
            ddlInsurance.SelectedIndex = 0;
            txtAdmitDate.Text = string.Empty;
            //txtIPDNo.Text = string.Empty;
            txtAge.Text = string.Empty;
            txtWeight.Text = string.Empty;
            txtAdmitDate.Enabled = true;
            AdmissionTimeSelector.Enabled = true;
            lblMsg.Text = string.Empty;

            txtProvDiag.Text = "";
            txtFinalDiag.Text = "";
            
            
        }

        public void GetOPDPatientList()
        {
            try
            {
                List<sp_GetAllPatientListForRegisteredResult> ldtRequisition = mobjPatientMasterBLL.GetPatientListForRegistered();
                if (ldtRequisition.Count > 0 && ldtRequisition != null)
                {
                    if (SessionManager.Instance.LoginUser.UserType.ToLower() == "doctor")
                    {
                        ldtRequisition = ldtRequisition.Where(p => p.DeptDoctorId == SessionManager.Instance.LoginUser.PKId).ToList();
                    }
                    dgvPatientList.DataSource = ldtRequisition;
                    dgvPatientList.DataBind();
                    //Session["PatientList"] = ldtRequisition;
                    int lintRowcount = ldtRequisition.Count;
                    lblRowCount.Text = "<b>Total Records:</b> " + lintRowcount.ToString();
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }

        }


        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            int lintCnt = 0;
            try
            {
                EntityPatientAdmit entAdmit = new EntityPatientAdmit();
                entAdmit.AdmitId = Convert.ToInt32(AdmitId.Value);
                entAdmit.PatientId = Convert.ToInt32(ddlPatientName.SelectedValue);
                entAdmit.DeptCategory = Convert.ToInt32(ddlDeptCategory.SelectedValue);
                entAdmit.DeptDoctorId = Convert.ToInt32(ddlDeptDoctor.SelectedValue);
                TimeSpan objTime = new TimeSpan(AdmissionTimeSelector.Hour, AdmissionTimeSelector.Minute, 0);
                entAdmit.AdmitDate = StringExtension.ToDateTime(txtAdmitDate.Text).Add(objTime);
                entAdmit.PatientAdmitTime = Convert.ToString(objTime);
                if (rbtnIPD.Checked)
                {
                    entAdmit.IsIPD = true;
                    entAdmit.PatientType = "IPD";
                    entAdmit.IsOPD = false;
                }
                if (rbtnOPD.Checked)
                {
                    entAdmit.IsIPD = false;
                    entAdmit.IsOPD = true;
                    entAdmit.PatientType = "OPD";
                    //entAdmit.IPDNo = "";
                    //entAdmit.OPDNo = Convert.ToString(txtIPDNo.Text);
                }
                if (!string.IsNullOrEmpty(txtAge.Text))
                {
                    entAdmit.Age = Convert.ToInt32(txtAge.Text);
                }
                //entAdmit.AgeIn = ddlAge.SelectedItem.Text;
                entAdmit.Weight = Convert.ToString(txtWeight.Text);
                entAdmit.IsCompany = chkCom.Checked ? true : false;
                entAdmit.IsInsured = ChkInsurance.Checked ? true : false;
                if (chkCom.Checked)
                {
                    entAdmit.CompanyId = Convert.ToInt32(ddlCompName.SelectedValue);
                    entAdmit.CompanyName = Convert.ToString(ddlCompName.SelectedItem.Text);
                }
                else
                {
                    entAdmit.CompanyId = 0;
                    entAdmit.CompanyName = string.Empty;
                }
                if (ChkInsurance.Checked)
                {
                    entAdmit.InsuranceComId = Convert.ToInt32(ddlInsurance.SelectedValue);
                    entAdmit.InsuName = Convert.ToString(ddlCompName.SelectedItem.Text);
                }
                else
                {
                    entAdmit.InsuranceComId = 0;
                    entAdmit.InsuName = string.Empty;
                }
                entAdmit.Dignosys = txtDignosys.Text;
                entAdmit.ProvDiag = !string.IsNullOrEmpty(txtProvDiag.Text) ? txtProvDiag.Text : "";

                entAdmit.FinalDiag = !string.IsNullOrEmpty(txtFinalDiag.Text) ? txtFinalDiag.Text : "";

                entAdmit.Ailergies = !string.IsNullOrEmpty(txtAilergies.Text) ? txtAilergies.Text : "";

                entAdmit.Symptomes = !string.IsNullOrEmpty(txtSymptoms.Text) ? txtSymptoms.Text : "";

                entAdmit.PastIllness = !string.IsNullOrEmpty(txtPastIllness.Text) ? txtPastIllness.Text : "";

                entAdmit.Temperature = !string.IsNullOrEmpty(txtTemperature.Text) ? txtTemperature.Text : "";

                entAdmit.Pulse = !string.IsNullOrEmpty(txtPulse.Text) ? txtPulse.Text : "";

                entAdmit.Respiration = !string.IsNullOrEmpty(txtRespiration.Text) ? txtRespiration.Text : "";

                entAdmit.Others = !string.IsNullOrEmpty(txtOthers.Text) ? txtOthers.Text : "";

                entAdmit.RS = !string.IsNullOrEmpty(txtRS.Text) ? txtRS.Text : "";

                entAdmit.CVS = !string.IsNullOrEmpty(txtCVS.Text) ? txtCVS.Text : "";

                entAdmit.PA = !string.IsNullOrEmpty(txtPA.Text) ? txtPA.Text : "";

                entAdmit.CNS = !string.IsNullOrEmpty(txtCNS.Text) ? txtCNS.Text : "";

                entAdmit.OBGY = !string.IsNullOrEmpty(txtOBGY.Text) ? txtOBGY.Text : "";

                entAdmit.XRAY = !string.IsNullOrEmpty(txtXRay.Text) ? txtXRay.Text : "";

                entAdmit.ECG = !string.IsNullOrEmpty(txtECG.Text) ? txtECG.Text : "";

                entAdmit.USG = !string.IsNullOrEmpty(txtUSG.Text) ? txtUSG.Text : "";
                lintCnt = mobjPatient.UpdatePatient(entAdmit);

                if (lintCnt > 0)
                {
                    lblMessage.Text = "Record Updated Successfully";
                }
                else
                {
                    lblMessage.Text = "Record Not Updated";
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
            GetOPDPatientList();
            MultiView1.SetActiveView(View1);
        }



        protected void dgvPatientList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvPatientList.PageIndex = e.NewPageIndex;
        }


        protected void dgvPatientList_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                List<sp_GetAllPatientListForRegisteredResult> ldtRequisition = mobjPatientMasterBLL.GetPatientListForRegistered();
                if (!string.IsNullOrEmpty(txtSearch.Text))
                {
                    ldtRequisition = mobjPatientMasterBLL.GetPatientListForRegistered().Where(p=>p.FullName.Contains(txtSearch.Text) || p.PatientType.Contains(txtSearch.Text)).ToList();
                }
                if (SessionManager.Instance.LoginUser.UserType.ToLower() == "doctor")
                {
                    ldtRequisition = ldtRequisition.Where(p => p.DeptDoctorId == SessionManager.Instance.LoginUser.PKId).ToList();
                }
                dgvPatientList.DataSource = ldtRequisition; // (List<sp_GetAllPatientListForRegisteredResult>)Session["PatientList"];
                dgvPatientList.DataBind();
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                txtAdmitDate.Enabled = false;
                GetDeptCategory();
                AdmissionTimeSelector.Enabled = false;
                ImageButton imgEdit = (ImageButton)sender;
                GridViewRow cnt = (GridViewRow)imgEdit.NamingContainer;

                AdmitId.Value = dgvPatientList.DataKeys[cnt.RowIndex].Value.ToString();
                var patient= mobjPatientMasterBLL.GetPatientListForRegistered().Where(p => p.AdmitId == Convert.ToInt32(AdmitId.Value)).FirstOrDefault();
                ListItem item = ddlPatientName.Items.FindByValue(patient.PatientId.Value.ToString());
                ddlPatientName.SelectedValue = item.Value;
                txtAdmitDate.Text = string.Format("{0:dd/MM/yyyy}", patient.AdmitDate.Value.Date);
                txtAge.Text =Convert.ToString(patient.Age);
                if (patient.PatientType.ToUpper().Equals("IPD"))
                {
                    rbtnOPD.Checked = true;
                    rbtnIPD.Checked = false;
                    lblDiagnosis.Visible = false;
                    txtDignosys.Visible = false;
                }
                if (patient.PatientType.ToUpper().Equals("OPD"))
                {
                    rbtnOPD.Checked = false;
                    rbtnIPD.Checked = true;
                    lblDiagnosis.Visible = true;
                    txtDignosys.Visible = true;
                    //lblIPDNo.Text = "IPD No :";
                    //txtIPDNo.Text = patient.IPDNo;
                }
                txtDignosys.Text = string.Empty;
                if (!string.IsNullOrEmpty(patient.Dignosys))
                {
                    txtDignosys.Text = patient.Dignosys;
                }
                
                ListItem item1 = ddlDeptCategory.Items.FindByValue(Convert.ToString(patient.DeptCategory));
                if (item1!=null)
                {
                    ddlDeptCategory.SelectedValue = item1.Value;

                    ddlDeptCategory_SelectedIndexChanged(sender, e);
                    ListItem itemdepdoct = ddlDeptDoctor.Items.FindByValue(patient.DeptDoctorId.Value.ToString());
                    ddlDeptDoctor.SelectedValue = itemdepdoct.Value;
                }
                

                if (patient.CompanyId!=null && Convert.ToInt32(patient.CompanyId)>0)
                {
                    chkCom.Checked = true;
                    ListItem itemCompany = ddlCompName.Items.FindByValue(Convert.ToString(patient.CompanyId));
                    ddlCompName.SelectedValue = itemCompany.Value;
                }
                else
                {
                    chkCom.Checked = false;
                }

                if (patient.InsuranceComId != null && Convert.ToInt32(patient.InsuranceComId) > 0)
                {
                    ChkInsurance.Checked = true;
                    ListItem itemCompany = ddlInsurance.Items.FindByValue(Convert.ToString(patient.InsuranceComId));
                    ddlInsurance.SelectedValue = itemCompany.Value;
                }
                else
                {
                    ChkInsurance.Checked = false;
                }
                txtBP.Text = patient.BP;
                if (patient.PatientTypeId!=null && Convert.ToInt32(patient.PatientTypeId)>0)
                {
                    ListItem itemCompany = ddlPatientType.Items.FindByValue(Convert.ToString(patient.PatientTypeId));
                    ddlPatientType.SelectedValue = itemCompany.Value;
                }

                txtProvDiag.Text = patient.ProvDiag;

                txtFinalDiag.Text = patient.FinalDiag;

                txtAilergies.Text = patient.Ailergies;

                txtSymptoms.Text = patient.Symptomes;

                txtPastIllness.Text = patient.PastIllness;

                txtTemperature.Text = patient.Temperature;

                txtPulse.Text =patient.Pulse;

                txtRespiration.Text = patient.Respiration;

                txtOthers.Text = patient.Others;

                txtRS.Text = patient.RS;

                txtCVS.Text = patient.CVS;

                txtPA.Text = patient.PA;

                txtCNS.Text = patient.CNS;

                txtOBGY.Text = patient.OBGY;

                txtXRay.Text = patient.XRAY;

                txtECG.Text = patient.ECG;

                txtUSG.Text =patient.USG;
                BtnSave.Visible = false;
                btnUpdate.Visible = true;
                MultiView1.SetActiveView(View2);
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
                Clear();
                MultiView1.SetActiveView(View1);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                EntityPatientAdmit entAdmit = new EntityPatientAdmit();
                entAdmit.PatientId = Convert.ToInt32(ddlPatientName.SelectedValue);
                TimeSpan objTime = new TimeSpan(AdmissionTimeSelector.Hour, AdmissionTimeSelector.Minute, 0);
                try
                {
                    entAdmit.AdmitDate = StringExtension.ToDateTime(txtAdmitDate.Text).Add(objTime);
                }
                catch (Exception ex)
                {
                    entAdmit.AdmitDate = DateTime.Now.Date;
                }
                entAdmit.PatientAdmitTime = Convert.ToString(objTime);
                entAdmit.Dignosys = txtDignosys.Text;

                entAdmit.DeptCategory = Convert.ToInt32(ddlDeptCategory.SelectedValue);
                entAdmit.DeptDoctorId = Convert.ToInt32(ddlDeptDoctor.SelectedValue);

                if (rbtnIPD.Checked)
                {
                    //DataTable ldt1 = new DataTable();
                    //ldt1 = mobjPatient.GetNewIPDNumber();
                    //if (ldt1.Rows.Count > 0 && ldt1 != null && (ldt1.Rows[0]["PatientIPDNo"].ToString() != string.Empty))
                    //{
                    //    txtIPDNo.Text = ldt1.Rows[0]["PatientIPDNo"].ToString();
                    //}
                    //entAdmit.IsIPD = true;
                    //entAdmit.PatientType = "IPD";
                    //entAdmit.IsOPD = false;
                    //entAdmit.IPDNo = Convert.ToString(txtIPDNo.Text);
                    //entAdmit.OPDNo = "";
                }
                if (rbtnOPD.Checked)
                {
                    //DataTable ldt1 = new DataTable();
                    //ldt1 = mobjPatient.GetNewOPDNumber();
                    //if (ldt1.Rows.Count > 0 && ldt1 != null && (ldt1.Rows[0]["PatientOPDNo"].ToString() != string.Empty))
                    //{
                    //    txtIPDNo.Text = ldt1.Rows[0]["PatientOPDNo"].ToString();
                    //}
                    //entAdmit.IsIPD = false;
                    //entAdmit.IsOPD = true;
                    //entAdmit.PatientType = "OPD";
                    //entAdmit.OPDNo = Convert.ToString(txtIPDNo.Text);
                    //entAdmit.IPDNo = "";
                }
                if (!string.IsNullOrEmpty(txtAge.Text))
                {
                    entAdmit.Age = Convert.ToInt32(txtAge.Text);
                }
                //entAdmit.AgeIn = ddlAge.SelectedItem.Text;
                entAdmit.Weight = Convert.ToString(txtWeight.Text);
                entAdmit.IsCompany = chkCom.Checked ? true : false;
                entAdmit.IsInsured = ChkInsurance.Checked ? true : false;
                if (chkCom.Checked)
                {
                    entAdmit.CompanyId = Convert.ToInt32(ddlCompName.SelectedValue);
                    entAdmit.CompanyName = Convert.ToString(ddlCompName.SelectedItem.Text);
                }
                else
                {
                    entAdmit.CompanyId = 0;
                    entAdmit.CompanyName = string.Empty;
                }
                if (ChkInsurance.Checked)
                {
                    entAdmit.InsuranceComId = Convert.ToInt32(ddlInsurance.SelectedValue);
                    entAdmit.InsuName = Convert.ToString(ddlInsurance.SelectedItem.Text);
                }
                else
                {
                    entAdmit.InsuranceComId = 0;
                    entAdmit.InsuName = string.Empty;
                }
                entAdmit.BP = !string.IsNullOrEmpty(txtBP.Text) ? txtBP.Text : "";
                entAdmit.ProvDiag = !string.IsNullOrEmpty(txtProvDiag.Text) ? txtProvDiag.Text : "";

                entAdmit.FinalDiag = !string.IsNullOrEmpty(txtFinalDiag.Text) ? txtFinalDiag.Text : "";

                entAdmit.Ailergies = !string.IsNullOrEmpty(txtAilergies.Text) ? txtAilergies.Text : "";

                entAdmit.Symptomes = !string.IsNullOrEmpty(txtSymptoms.Text) ? txtSymptoms.Text : "";

                entAdmit.PastIllness = !string.IsNullOrEmpty(txtPastIllness.Text) ? txtPastIllness.Text : "";

                entAdmit.Temperature = !string.IsNullOrEmpty(txtTemperature.Text) ? txtTemperature.Text : "";

                entAdmit.Pulse = !string.IsNullOrEmpty(txtPulse.Text) ? txtPulse.Text : "";

                entAdmit.Respiration = !string.IsNullOrEmpty(txtRespiration.Text) ? txtRespiration.Text : "";

                entAdmit.Others = !string.IsNullOrEmpty(txtOthers.Text) ? txtOthers.Text : "";

                entAdmit.RS = !string.IsNullOrEmpty(txtRS.Text) ? txtRS.Text : "";

                entAdmit.CVS = !string.IsNullOrEmpty(txtCVS.Text) ? txtCVS.Text : "";

                entAdmit.PA = !string.IsNullOrEmpty(txtPA.Text) ? txtPA.Text : "";

                entAdmit.CNS = !string.IsNullOrEmpty(txtCNS.Text) ? txtCNS.Text : "";

                entAdmit.OBGY = !string.IsNullOrEmpty(txtOBGY.Text) ? txtOBGY.Text : "";

                entAdmit.XRAY = !string.IsNullOrEmpty(txtXRay.Text) ? txtXRay.Text : "";

                entAdmit.ECG = !string.IsNullOrEmpty(txtECG.Text) ? txtECG.Text : "";

                entAdmit.USG = !string.IsNullOrEmpty(txtUSG.Text) ? txtUSG.Text : "";
                entAdmit.PatientTypeId = ddlPatientType.SelectedIndex > 0 ? Convert.ToInt32(ddlPatientType.SelectedValue) : 0;
                bool Status = mobjPatient.CheckPatientExistforSameDate(entAdmit);
                if (!Status)
                {
                    int i = mobjPatient.Save(entAdmit);
                    if (i > 0)
                    {
                        Clear();
                        lblMessage.Text = "Record Save Successfully.";
                    }
                    else
                    {
                        lblMessage.Text = "Record Save Successfully.";
                    }
                }
                else
                {
                    lblMessage.Text = "This Patient Was Not Discharged.";
                }
                GetOPDPatientList();
                MultiView1.SetActiveView(View1);
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }
    }
}