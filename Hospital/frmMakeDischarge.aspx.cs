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
    public partial class frmMakeDischarge : BasePage
    {
        ShiftBLL mobjDeptBLL = new ShiftBLL();

        MakeDischargeBLL mobjDischarge = new MakeDischargeBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            base.AuthenticateUser("frmMakeDischarge.aspx");
            if (!Page.IsPostBack)
            {
                update.Value = Server.UrlEncode(System.DateTime.Now.ToString());
                BindPatients();
                BindSurgery();
                GetDischargeList();
                MultiView1.SetActiveView(View2);
            }
        }

        private void BindSurgery()
        {
            try
            {
                List<EntitySurgeryMaster> tblSurgery = new SurgeryMasterBLL().GetAllSurgeryName();
                tblSurgery.Insert(0, new EntitySurgeryMaster() { PKId = 0, NameOfSurgery = "----Select----" });
                ddlNameOfSurgery.DataSource = tblSurgery;
                ddlNameOfSurgery.DataTextField = "NameOFSurgery";
                ddlNameOfSurgery.DataValueField = "PKId";
                ddlNameOfSurgery.DataBind();
            }
            catch (Exception ex)
            {
                //lblMessage.Text = ex.Message;
            }
        }

        protected void ddlNameOfSurgery_IndexChanged(object sender, EventArgs e)
        {
            //int Val = Convert.ToInt32(ddlNameOfSurgery.SelectedValue);
            //EntitySurgeryMaster objTxt = new SurgeryMasterBLL().GetOperationalProcedure(Val);
            //txtOperatonalProce.Text = objTxt.OperationalProcedure;
        }

        protected void ddlDischargeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDischargeType.SelectedItem.Text == "Routine")
            {
                lblFollowUp.Visible = true;
                txtFollowUp.Visible = true;
            }
            else
            {
                lblFollowUp.Visible = false;
                txtFollowUp.Visible = false;
            }
        }

        private void BindPatients()
        {
            try
            {
                List<EntityPatientMaster> tblPatient = new clsTestAllocation().GetPatientListForDischarge();
                if (tblPatient==null)
                {
                    tblPatient = new List<EntityPatientMaster>();
                }
                tblPatient = tblPatient.Where(p => p.PatientType.ToLower() == "ipd").ToList();
                tblPatient.Insert(0, new EntityPatientMaster() { PatientId = 0, FullName = "----Select----" });
                ddlPatient.DataSource = tblPatient;
                ddlPatient.DataTextField = "FullName";
                ddlPatient.DataValueField = "PatientId";
                ddlPatient.DataBind();
            }
            catch (Exception ex)
            {
                //lblMessage.Text = ex.Message;
            }
        }

        protected void ddlPatient_SelectedIndexChanged(object sender, EventArgs e)
        {
            int Val = Convert.ToInt32(ddlPatient.SelectedValue);
            EntityPatientMaster objTxt = new PatientMasterBLL().GetDischargeOnDiadnosis(Val);
            txtDiagnosis.Text = objTxt.Dignosys;
            txtBP.Text = objTxt.BP;
            txtDiagnosis.Text = objTxt.Dignosys;
            txtHistory.Text = objTxt.PatientHistory;
            txtTemp.Text = objTxt.Temperature;
            txtPulse.Text = objTxt.Pulse;
            txtRespRate.Text = objTxt.Respiration;
            txtOthers.Text = objTxt.Others;
            txtCVS.Text = objTxt.CVS;
            txtCNS.Text = objTxt.CNS;
            txtXRay.Text = objTxt.XRAY;
            txtECG.Text = objTxt.ECG;
        }

        protected void dgvTestInvoice_PageIndexChanged(object sender, EventArgs e)
        {
            List<EntityMakeDischarge> tblPatient = mobjDischarge.GetDischargeInvoiceList();
            if (!string.IsNullOrEmpty(txtSearch.Text))
            {
                tblPatient = tblPatient.Where(p => p.PatName.Contains(txtSearch.Text)).ToList();
            }
            dgvTestInvoice.DataSource = tblPatient;
            dgvTestInvoice.DataBind();
        }

        protected void dgvTestInvoice_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvTestInvoice.PageIndex = e.NewPageIndex;
        }

        protected void dgvTestInvoice_DataBound(object sender, EventArgs e)
        {
            int lintCurrentPage = dgvTestInvoice.PageIndex + 1;
            lblPageCount.Text = "<b>Page</b> " + lintCurrentPage.ToString() + "<b> of </b>" + dgvTestInvoice.PageCount.ToString();
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                List<EntityTestInvoice> tblPatient = new clsTestAllocation().GetTestInvoiceList(txtSearch.Text);
                dgvTestInvoice.DataSource = tblPatient;
                dgvTestInvoice.DataBind();
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
                Status.Value = "Edit";
                lblMessage.Text = string.Empty;
                ImageButton imgEdit = (ImageButton)sender;
                GridViewRow cnt = (GridViewRow)imgEdit.NamingContainer;
                DischargeId.Value = dgvTestInvoice.DataKeys[cnt.RowIndex].Value.ToString();
                EntityMakeDischarge lst = mobjDischarge.GetDischargeDetails(Convert.ToInt32(DischargeId.Value));
                if (lst != null)
                {
                    ListItem item = ddlPatient.Items.FindByValue(lst.PatientId.ToString());
                    ddlPatient.SelectedValue = item.Value;
                    if (lst.SurgeryId!=null)
                    {
                        ListItem itemSur = ddlNameOfSurgery.Items.FindByValue(Convert.ToString(lst.SurgeryId));
                        ddlNameOfSurgery.SelectedValue = itemSur.Value;
                    }
                    
                    patientID.Value = ddlPatient.SelectedValue;
                    SurID.Value = ddlNameOfSurgery.SelectedValue;
                    ddlPatient.Enabled = false;
                    ddlDischargeType.Text = lst.TypeOfDischarge;
                    if (lst.TypeOfDischarge == "Routine")
                    {
                        lblFollowUp.Visible = true;
                        txtFollowUp.Visible = true;
                    }
                    else
                    {
                        lblFollowUp.Visible = false;
                        txtFollowUp.Visible = false;
                    }
                    txtDiagnosis.Text = lst.Diagnosis;
                    txtDischargeDate.Text = lst.DischargeReceiptDate==null?DateTime.Now.Date.ToString("dd/MM/yyyy"): lst.DischargeReceiptDate.Value.ToString("dd/MM/yyyy");
                    txtOperatonalProce.Text = lst.OperationalProcedure;
                    txtHistory.Text = lst.HistoryClinical;
                    txtXRay.Text = lst.XRay;
                    txtHaemogram.Text = lst.Haemogram;
                    txtOthers.Text = lst.Others;
                    txtBP.Text = lst.BP;
                    txtTreatmentHospital.Text = lst.TreatmentInHospitalisation;
                    txtAdviceDischarge.Text = lst.AdviceOnDischarge;
                    txtFollowUp.Text = lst.FollowUp;
                    txtECG.Text = lst.ECG;
                    txtBUL.Text = lst.BUL;
                    txtSCreat.Text = lst.SCreat;
                    txtSElect.Text = lst.SElect;
                    txtBSL.Text = lst.BSL;
                    txtUrineR.Text = lst.UrineR;
                    txtTemp.Text = lst.Temp;
                    txtPulse.Text = lst.Pulse;
                    txtBP.Text = lst.BP;
                    txtRespRate.Text = lst.RespRate;
                    txtPallor.Text = lst.Pallor;
                    txtOedema.Text = lst.Oedema;
                    txtCyanosis.Text = lst.Cyanosis;
                    txtClubbing.Text = lst.Clubbing;
                    txtIcterus.Text = lst.Icterus;
                    txtSkin.Text = lst.Skin;
                    txtRespSystem.Text = lst.RespSystem;
                    txtCNS.Text = lst.CNS;
                    txtPerAbd.Text = lst.PerAbd;
                    txtCVS.Text = lst.CVS;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
            BtnUpdate.Visible = true;
            BtnSave.Visible = false;
            MultiView1.SetActiveView(View1);
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton imgEdit = (ImageButton)sender;
                GridViewRow cnt = (GridViewRow)imgEdit.NamingContainer;
                Response.Redirect("~/PathalogyReport/PathologyReport.aspx?ReportType=Discharge&AdmitId=" + Convert.ToInt32(cnt.Cells[1].Text) + "&DischargeId=" + dgvTestInvoice.DataKeys[cnt.RowIndex].Value, false);
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
                lblMessage.Text = string.Empty;
                ddlPatient.SelectedIndex = 0;
                ddlNameOfSurgery.SelectedIndex = 0;
                txtOperatonalProce.Text = string.Empty;
                txtDiagnosis.Text = string.Empty;
                txtAdviceDischarge.Text = string.Empty;
                txtFollowUp.Text = string.Empty;
                txtHistory.Text = string.Empty;
                txtTreatmentHospital.Text = string.Empty;
                txtOthers.Text = string.Empty;
                ddlDischargeType.SelectedIndex = 0;
                txtXRay.Text = string.Empty;
                txtHaemogram.Text = string.Empty;
                txtBUL.Text = string.Empty;
                txtSCreat.Text = string.Empty;
                txtSElect.Text = string.Empty;
                txtBSL.Text = string.Empty;
                txtECG.Text = string.Empty;
                txtUrineR.Text = string.Empty;
                txtTemp.Text = string.Empty;
                txtPulse.Text = string.Empty;
                txtBP.Text = string.Empty;
                txtRespRate.Text = string.Empty;
                txtPallor.Text = string.Empty;
                txtOedema.Text = string.Empty;
                txtCyanosis.Text = string.Empty;
                txtClubbing.Text = string.Empty;
                txtIcterus.Text = string.Empty;
                txtSkin.Text = string.Empty;
                txtRespSystem.Text = string.Empty;
                txtCNS.Text = string.Empty;
                txtPerAbd.Text = string.Empty;
                txtCVS.Text = string.Empty;
                MultiView1.SetActiveView(View2);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                ddlPatient.SelectedIndex = 0;
                ddlPatient.Enabled = true;
                ddlNameOfSurgery.SelectedIndex = 0;
                txtOperatonalProce.Text = string.Empty;
                txtDischargeDate.Enabled = false;
                txtDischargeDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
                txtDiagnosis.Text = string.Empty;
                txtAdviceDischarge.Text = string.Empty;
                txtFollowUp.Text = string.Empty;
                txtHistory.Text = string.Empty;
                txtTreatmentHospital.Text = string.Empty;
                txtOthers.Text = string.Empty;
                ddlDischargeType.SelectedIndex = 0;
                txtXRay.Text = string.Empty;
                txtHaemogram.Text = string.Empty;
                txtBUL.Text = string.Empty;
                txtSCreat.Text = string.Empty;
                txtSElect.Text = string.Empty;
                txtBSL.Text = string.Empty;
                txtECG.Text = string.Empty;
                txtUrineR.Text = string.Empty;
                txtTemp.Text = string.Empty;
                txtPulse.Text = string.Empty;
                txtBP.Text = string.Empty;
                txtRespRate.Text = string.Empty;
                txtPallor.Text = string.Empty;
                txtOedema.Text = string.Empty;
                txtCyanosis.Text = string.Empty;
                txtClubbing.Text = string.Empty;
                txtIcterus.Text = string.Empty;
                txtSkin.Text = string.Empty;
                txtRespSystem.Text = string.Empty;
                txtCNS.Text = string.Empty;
                txtPerAbd.Text = string.Empty;
                txtCVS.Text = string.Empty;
                BtnUpdate.Visible = false;
                MultiView1.SetActiveView(View1);
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtSearch.Text = string.Empty;
            dgvTestInvoice.PageIndex = 0;
            GetDischargeList();
        }

        private void GetDischargeList()
        {
            try
            {
                List<EntityMakeDischarge> tblPatient = mobjDischarge.GetDischargeInvoiceList();
                dgvTestInvoice.DataSource = tblPatient;
                dgvTestInvoice.DataBind();
                int lintRowcount = tblPatient.Count;
                lblRowCount1.Text = "<b>Total Records:</b> " + lintRowcount.ToString();
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
                TimeSpan span = new TimeSpan(AdmissionTimeSelector.Hour, AdmissionTimeSelector.Minute, 0);
                EntityMakeDischarge entDischarge = new EntityMakeDischarge()
                {
                    AdviceOnDischarge = FirstCharToUpper(txtAdviceDischarge.Text),
                    BP = txtBP.Text,
                    BSL = txtBSL.Text,
                    BUL = txtBUL.Text,
                    Clubbing = txtClubbing.Text,
                    CNS = txtCNS.Text,
                    CVS = txtCVS.Text,
                    Cyanosis = txtCyanosis.Text,
                    DischargeReceiptDate = StringExtension.ToDateTime(txtDischargeDate.Text).Add(span),
                    Diagnosis = FirstCharToUpper(txtDiagnosis.Text),
                    ECG = txtECG.Text,
                    FollowUp = txtFollowUp.Text,
                    HistoryClinical = FirstCharToUpper(txtHistory.Text),
                    Icterus = txtIcterus.Text,

                    NameOfSurgery = Convert.ToString(ddlNameOfSurgery.SelectedItem.Text),

                    OperationalProcedure = FirstCharToUpper(txtOperatonalProce.Text),
                    Oedema = txtOedema.Text,
                    Others = txtOthers.Text,

                    PatientId = Convert.ToInt32(ddlPatient.SelectedValue),
                    Pulse = txtPulse.Text,
                    Pallor = txtPallor.Text,
                    PerAbd = txtPerAbd.Text,
                    PreparedByName = SessionManager.Instance.LoginUser.EmpName,

                    RespRate = txtRespRate.Text,
                    RespSystem = txtRespSystem.Text,

                    SCreat = txtSCreat.Text,
                    SElect = txtSElect.Text,
                    Skin = txtSkin.Text,

                    SurgeryId = Convert.ToInt32(ddlNameOfSurgery.SelectedValue),
                    
                    TypeOfDischarge = ddlDischargeType.SelectedItem.Text,
                    Temp = txtTemp.Text,
                    TreatmentInHospitalisation = FirstCharToUpper(txtTreatmentHospital.Text),

                    XRay = txtXRay.Text,
                    Haemogram = txtHaemogram.Text,

                    UrineR = txtUrineR.Text,
                };

                int i = mobjDischarge.Save(entDischarge);
                if (i > 0)
                {
                    lblMsg.Text = "Record Saved Successfully";
                }
                else
                {
                    lblMsg.Text = "Record Not Saved";
                }
                GetDischargeList();
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
            MultiView1.SetActiveView(View2);
        }

        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                TimeSpan span = new TimeSpan(AdmissionTimeSelector.Hour, AdmissionTimeSelector.Minute, 0);
                EntityMakeDischarge entDischarge = new EntityMakeDischarge()
                {
                    DichargeId=Convert.ToInt32(DischargeId.Value),
                    AdviceOnDischarge = FirstCharToUpper(txtAdviceDischarge.Text),
                    BP = txtBP.Text,
                    BSL = txtBSL.Text,
                    BUL = txtBUL.Text,
                    Clubbing = txtClubbing.Text,
                    CNS = txtCNS.Text,
                    CVS = txtCVS.Text,
                    Cyanosis = txtCyanosis.Text,
                    DischargeReceiptDate = StringExtension.ToDateTime(txtDischargeDate.Text).Add(span),
                    Diagnosis = FirstCharToUpper(txtDiagnosis.Text),
                    ECG = txtECG.Text,
                    FollowUp = txtFollowUp.Text,
                    HistoryClinical = FirstCharToUpper(txtHistory.Text),
                    Icterus = txtIcterus.Text,

                    NameOfSurgery = Convert.ToString(ddlNameOfSurgery.SelectedItem.Text),

                    OperationalProcedure = FirstCharToUpper(txtOperatonalProce.Text),
                    Oedema = txtOedema.Text,
                    Others = txtOthers.Text,

                    PatientId = Convert.ToInt32(ddlPatient.SelectedValue),
                    Pulse = txtPulse.Text,
                    Pallor = txtPallor.Text,
                    PerAbd = txtPerAbd.Text,
                    PreparedByName = SessionManager.Instance.LoginUser.EmpName,

                    RespRate = txtRespRate.Text,
                    RespSystem = txtRespSystem.Text,

                    SCreat = txtSCreat.Text,
                    SElect = txtSElect.Text,
                    Skin = txtSkin.Text,

                    SurgeryId = Convert.ToInt32(ddlNameOfSurgery.SelectedValue),

                    TypeOfDischarge = ddlDischargeType.SelectedItem.Text,
                    Temp = txtTemp.Text,
                    TreatmentInHospitalisation = FirstCharToUpper(txtTreatmentHospital.Text),

                    XRay = txtXRay.Text,
                    Haemogram = txtHaemogram.Text,
                    
                    UrineR = txtUrineR.Text,
                };

                int i = mobjDischarge.Update(entDischarge, Convert.ToInt32(DischargeId.Value));
                if (i > 0)
                {
                    lblMsg.Text = "Record Updated Successfully";
                }
                else
                {
                    lblMsg.Text = "Record Not Updated";
                }
                GetDischargeList();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
            MultiView1.SetActiveView(View2);
        }

        public string FirstCharToUpper(string value)
        {
            char[] array = value.ToCharArray();
            // Handle the first letter in the string.
            if (array.Length >= 1)
            {
                if (char.IsLower(array[0]))
                {
                    array[0] = char.ToUpper(array[0]);
                }
            }
            // Scan through the letters, checking for spaces.
            // ... Uppercase the lowercase letters following spaces.
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i - 1] == ' ')
                {
                    if (char.IsLower(array[i]))
                    {
                        array[i] = char.ToUpper(array[i]);
                    }
                }
            }
            return new string(array);
        }

    }
}