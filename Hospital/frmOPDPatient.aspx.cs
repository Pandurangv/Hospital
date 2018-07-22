using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Hospital.Models.DataLayer;
using System.Data;
using Hospital.Models.Models;
using Hospital.Models.BusinessLayer;
using Hospital.Models;

namespace Hospital
{
    public partial class frmOPDPatient : BasePage
    {
        PatientMasterBLL mobjPatient = new PatientMasterBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                base.AuthenticateUser("frmOPDPatientDetail.aspx");
                if (!Page.IsPostBack)
                {
                    DateTimeOffset localtime=Commons.ConvertUTCBasedOnCuture();
                    string tt = string.Format("{0:tt}", localtime);
                    MKB.TimePicker.TimeSelector.AmPmSpec time = tt == "AM" ? MKB.TimePicker.TimeSelector.AmPmSpec.AM : MKB.TimePicker.TimeSelector.AmPmSpec.PM;
                    AdmissionTimeSelector.SetTime(localtime.Hour, localtime.Minute, time);
                    if (QueryStringManager.Instance.IsEdit)
                    {
                        BtnSave.Visible = false;
                        btnUpdate.Visible = true;
                        txtAdmitDate.Enabled = false;
                        AdmissionTimeSelector.Enabled = false;
                        rbtnIPD.Enabled = false;
                        rbtnOPD.Enabled = false;
                        GetCompanies();
                        GetDeptCategory();
                        GetInitials();
                        GetGenders();
                        GetOccupation();
                        GetInsuranceCompanies();
                        BindPatientTypes();
                        tblPatientMaster ldtPat = new PatientMasterBLL().GetPatientbyId(Convert.ToInt32(QueryStringManager.Instance.PatientId));
                        PatientId.Value = Convert.ToString(ldtPat.PKId.ToString());
                        txtPatientCode.Text = ldtPat.PatientCode;
                        ddlOccupation.SelectedIndex = Convert.ToInt32(ldtPat.Occupation);
                        ddlBloodGroup.Text = Convert.ToString(ldtPat.BloodGroup);
                        txtAdmitDate.Text = string.Format("{0:dd/MM/yyyy}", ldtPat.AdminDate);
                        if (Convert.ToInt32(ldtPat.InsuranceCompanyId) > 0)
                        {
                            ChkInsurance.Checked = true;
                            ddlInsurance.SelectedIndex = Convert.ToInt32(ldtPat.InsuranceCompanyId);
                        }
                        ddlInitials.SelectedIndex = Convert.ToInt32(ldtPat.Initial);
                        txtFirstName.Text = ldtPat.PatientFirstName;
                        txtMidleName.Text = ldtPat.PatientMiddleName;
                        txtLastName.Text = ldtPat.PatientLastName;
                        ddlGender.SelectedIndex = Convert.ToInt32(ldtPat.Gender);
                        if (ldtPat.BirthDate!=null)
                        {
                            txtBirthDate.Text = string.Format("{0:dd/MM/yyyy}", ldtPat.BirthDate);
                        }
                        ddlOccupation.SelectedIndex = Convert.ToInt32(ldtPat.Occupation);
                        if (ldtPat.DeptCategory!=null)
                        {
                            ddlDeptCategory.SelectedIndex = Convert.ToInt32(ldtPat.DeptCategory);
                            DataTable ldt = new DataTable();
                            ldt = mobjPatient.GetDeptDoctors(Convert.ToInt32(ldtPat.DeptCategory));
                            if (ldt.Rows.Count > 0 && ldt != null)
                            {
                                ddlDeptDoctor.DataSource = ldt;
                                ddlDeptDoctor.DataValueField = "DocAllocId";
                                ddlDeptDoctor.DataTextField = "EmpName";
                                ddlDeptDoctor.DataBind();
                                ddlDeptDoctor.SelectedIndex = Convert.ToInt32(ldtPat.DeptDoctorId);
                            }
                        }
                        txtAge.Text = ldtPat.Age.ToString();
                        txtWeight.Text = ldtPat.Weight.ToString();
                        txtAddress.Text = string.IsNullOrEmpty(ldtPat.Address) ? string.Empty : ldtPat.Address;
                        if (ldtPat.PatientType == "IPD")
                        {
                            rbtnIPD.Checked = true;
                            lblDiagnosys.Visible = true;
                            txtDignosys.Visible = true;
                            IPD_CheckedChanged(sender, e);
                        }
                        else
                        {
                            rbtnOPD.Checked = true;
                            lblDiagnosys.Visible = false;
                            txtDignosys.Visible = false;
                            OPD_CheckedChanged(sender, e);
                        }
                        txtContactNo.Text = ldtPat.ContactNo;
                        txtCity.Text = ldtPat.City;
                        txtState.Text = ldtPat.State;
                        txtCountry.Text = ldtPat.Country;
                        txtBP.Text = ldtPat.BP;
                        txtRefDoctor.Text = ldtPat.ReferedBy;
                        txtDignosys.Text = ldtPat.Dignosys;

                        tblPatientAdmitDetail admit = mobjPatient.AdmitPatientList().Where(p => p.PatientId == Convert.ToInt32(QueryStringManager.Instance.PatientId)).OrderByDescending(p=>p.AdmitId).FirstOrDefault();
                        if (admit!=null)
                        {
                            txtProvDiag.Text = admit.ProvDiag;//"].ToString();

                            txtFinalDiag.Text = admit.FinalDiag;//"].ToString();

                            txtAilergies.Text = admit.Ailergies;//"].ToString();

                            txtSymptoms.Text = admit.Symptomes;//"].ToString();

                            txtPastIllness.Text = admit.PastIllness;//"].ToString();

                            txtTemperature.Text = admit.Temperature;//"].ToString();

                            txtPulse.Text = admit.Pulse;//"].ToString();

                            txtRespiration.Text = admit.Respiration;//"].ToString();

                            txtOthers.Text = admit.Others;//"].ToString();

                            txtRS.Text = admit.RS;//"].ToString();

                            txtCVS.Text = admit.CVS;//"].ToString();

                            txtPA.Text = admit.PA;//"].ToString();

                            txtCNS.Text = admit.CNS;//"].ToString();

                            txtOBGY.Text = admit.OBGY;//"].ToString();

                            txtXRay.Text = admit.XRAY;//"].ToString();

                            txtECG.Text = admit.ECG;//"].ToString();

                            txtUSG.Text = admit.USG;//"].ToString();    
                        }

                        if (!string.IsNullOrEmpty(ldtPat.CompanyCode) && Convert.ToInt32(ldtPat.CompanyCode) > 0)
                        {
                            chkCom.Checked = true;
                            ListItem item = ddlCompName.Items.FindByValue(ldtPat.CompanyCode);
                            if (item != null)
                            {
                                ddlCompName.SelectedValue = item.Value;
                            }
                        }
                        if (ldtPat.InsuranceCompanyId != null && ldtPat.InsuranceCompanyId > 0)
                        {
                            ChkInsurance.Checked = true;
                            ListItem item = ddlInsurance.Items.FindByValue(ldtPat.InsuranceCompanyId.ToString());
                            if (item != null)
                            {
                                ddlInsurance.SelectedValue = item.Value;
                            }
                        }
                        if (ldtPat.PatientTypeId != null && ldtPat.PatientTypeId>0)
                        {
                            ListItem item = ddlPatientType.Items.FindByValue(ldtPat.PatientTypeId.ToString());
                            if (item != null)
                            {
                                ddlPatientType.SelectedValue = item.Value;
                            }
                        }
                        chkCom_CheckedChanged(sender, e);
                        ChkInsurance_CheckedChanged(sender, e);
                    }
                    else
                    {
                        AddNewDetails();
                        OPD_CheckedChanged(sender, e);
                    }
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        private void AddNewDetails()
        {
            rbtnOPD.Checked = true;
            txtAdmitDate.Enabled = false;
            AdmissionTimeSelector.Enabled = true;
            rbtnIPD.Enabled = true;
            rbtnOPD.Enabled = true;
            BtnSave.Visible = true;
            btnUpdate.Visible = false;
            
            BindPatientTypes();
            DataTable ldt = new DataTable();
            ldt = mobjPatient.GetNewPatientCode();
            if (ldt.Rows.Count > 0 && ldt != null)
            {
                txtPatientCode.Text = ldt.Rows[0]["PatientCode"].ToString();
            }

            GetCompanies();
            GetInitials();
            GetGenders();
            GetDeptCategory();
            GetOccupation();
            //GetReligions();
            //FillCast();
            GetInsuranceCompanies();
            ddlInsurance.SelectedIndex = 0;
            txtState.Text = "Maharashtra";
            txtCountry.Text = "India";
            txtWeight.Text = string.Empty;
            txtAdmitDate.Text = DateTime.UtcNow.Date.ToString("dd/MM/yyyy");
            ddlDeptCategory.SelectedIndex = 0;
            //ddlAge.SelectedIndex = 0;
            ddlInsurance.Enabled = false;
            fileId.Enabled = false;
            fileInsurance.Enabled = false;
            ddlCompName.Enabled = false;
        }

        private void BindPatientTypes()
        {
            List<tblPatientType> lst= mobjPatient.GetPatientTypes();
            lst.Insert(0,new tblPatientType() { PatientTypeId=0,PatientType="--Select--"});
            ddlPatientType.DataSource = lst;
            ddlPatientType.DataValueField = "PatientTypeId";
            ddlPatientType.DataTextField = "PatientType";
            ddlPatientType.DataBind();
        }

        protected void OPD_CheckedChanged(object sender, EventArgs e)
        {
            lblDiagnosys.Visible = false;
            txtDignosys.Visible = false;
            lblref.Visible = false;
            txtRefDoctor.Visible = false;
            lblblood.Visible = false;
            ddlBloodGroup.Visible = false;
            
            lbloccupation.Visible = false;
            ddlOccupation.Visible = false;
            lblcity.Visible = false;
            txtCity.Visible = false;
            lblstate.Visible = false;
            txtState.Visible = false;
            lblcountry.Visible = false;
            txtCountry.Visible = false;
        }

        protected void IPD_CheckedChanged(object sender, EventArgs e)
        {
            lblDiagnosys.Visible = true;
            txtDignosys.Visible = true;
            lblref.Visible = true;
            txtRefDoctor.Visible = true;
            lblblood.Visible = true;
            ddlBloodGroup.Visible = true;
            lbloccupation.Visible = true;
            ddlOccupation.Visible = true;
            lblcity.Visible = true;
            txtCity.Visible = true;
            lblstate.Visible = true;
            txtState.Visible = true;
            lblcountry.Visible = true;
            txtCountry.Visible = true;
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

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int linCnt = 0;
            Byte[] bdefault = new Byte[] { 0 };
            EntityPatientMaster entPatientMaster = new EntityPatientMaster();
            try
            {
                //if (ddlDeptDoctor.Items.Count == 0)
                //{
                //    lblMsg.Text = "Please Select dept doctor.";
                //    return;
                //}
                //if (ddlDeptCategory.SelectedIndex == 0)
                //{
                //    lblMsg.Text = "Please Select Department Category";
                //}
                //else
                {
                    if (ddlInitials.SelectedIndex == 0)
                    {
                        lblMsg.Text = "Please Select Initial For Patient Name";
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(txtFirstName.Text))
                        {
                            lblMsg.Text = "Please Enter First Name.....";
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(txtLastName.Text.Trim()))
                            {
                                lblMsg.Text = "Please Enter Last Name....";
                            }
                            else
                            {
                                if (ddlGender.SelectedIndex == 0)
                                {
                                    lblMsg.Text = "Please Select Gender....";
                                }
                                else
                                {
                                    if (rbtnOPD.Checked == false && rbtnIPD.Checked == false)
                                    {
                                        lblMsg.Text = "Please Select Type Of Patient";
                                        rbtnOPD.Focus();
                                        return;
                                    }
                                    else
                                    {
                                        
                                        entPatientMaster.PatientCode = txtPatientCode.Text.Trim();
                                        entPatientMaster.PKId = Convert.ToInt32(PatientId.Value);

                                        TimeSpan objTime = new TimeSpan(AdmissionTimeSelector.Hour, AdmissionTimeSelector.Minute, 0);
                                        entPatientMaster.PatientAdmitDate = StringExtension.ToDateTime(txtAdmitDate.Text).Add(objTime);
                                        entPatientMaster.PatientAdmitTime = Convert.ToString(objTime);
                                        entPatientMaster.PatientInitial = Convert.ToInt32(ddlInitials.SelectedValue);
                                        entPatientMaster.PatientFirstName = FirstCharToUpper(txtFirstName.Text);
                                        entPatientMaster.PatientMiddleName = FirstCharToUpper(txtMidleName.Text);
                                        entPatientMaster.PatientLastName = FirstCharToUpper(txtLastName.Text);
                                        entPatientMaster.Gender = Convert.ToInt32(ddlGender.SelectedValue);
                                        
                                        if (!string.IsNullOrEmpty(txtBirthDate.Text))
                                        {
                                            entPatientMaster.Age = StringExtension.ToDateTime(txtAdmitDate.Text).Year - StringExtension.ToDateTime(txtBirthDate.Text.Trim()).Year;
                                        }
                                        if (!string.IsNullOrEmpty(txtAge.Text))
                                        {
                                            entPatientMaster.Age = Convert.ToInt32(txtAge.Text);
                                        }

                                        entPatientMaster.Weight = Convert.ToString(txtWeight.Text);
                                        entPatientMaster.DeptCategory = ddlDeptCategory.SelectedIndex>0? Convert.ToInt32(ddlDeptCategory.SelectedValue):SettingsManager.Instance.DeptId;
                                        entPatientMaster.DeptDoctorId = Convert.ToInt32(ddlDeptDoctor.SelectedValue);
                                        entPatientMaster.Occupation = ddlDeptDoctor.SelectedIndex > 0 ? Convert.ToInt32(ddlOccupation.SelectedValue) : SettingsManager.Instance.DoctorId;
                                        entPatientMaster.PatientContactNo = txtContactNo.Text.Trim();
                                        entPatientMaster.PatientAddress = FirstCharToUpper(txtAddress.Text);
                                        entPatientMaster.City = FirstCharToUpper(txtCity.Text);
                                        entPatientMaster.State = FirstCharToUpper(txtState.Text);
                                        entPatientMaster.Country = FirstCharToUpper(txtCountry.Text);
                                        entPatientMaster.ReferedBy = FirstCharToUpper(txtRefDoctor.Text);
                                        entPatientMaster.Dignosys = FirstCharToUpper(txtDignosys.Text);

                                        entPatientMaster.ProvDiag = !string.IsNullOrEmpty(txtProvDiag.Text) ? txtProvDiag.Text : "";

                                        entPatientMaster.FinalDiag = !string.IsNullOrEmpty(txtFinalDiag.Text) ? txtFinalDiag.Text : "";

                                        entPatientMaster.Ailergies = !string.IsNullOrEmpty(txtAilergies.Text) ? txtAilergies.Text : "";

                                        entPatientMaster.Symptomes = !string.IsNullOrEmpty(txtSymptoms.Text) ? txtSymptoms.Text : "";

                                        entPatientMaster.PastIllness = !string.IsNullOrEmpty(txtPastIllness.Text) ? txtPastIllness.Text : "";

                                        entPatientMaster.Temperature = !string.IsNullOrEmpty(txtTemperature.Text) ? txtTemperature.Text : "";

                                        entPatientMaster.Pulse = !string.IsNullOrEmpty(txtPulse.Text) ? txtPulse.Text : "";

                                        entPatientMaster.Respiration = !string.IsNullOrEmpty(txtRespiration.Text) ? txtRespiration.Text : "";

                                        entPatientMaster.Others = !string.IsNullOrEmpty(txtOthers.Text) ? txtOthers.Text : "";

                                        entPatientMaster.RS = !string.IsNullOrEmpty(txtRS.Text) ? txtRS.Text : "";

                                        entPatientMaster.CVS = !string.IsNullOrEmpty(txtCVS.Text) ? txtCVS.Text : "";

                                        entPatientMaster.PA = !string.IsNullOrEmpty(txtPA.Text) ? txtPA.Text : "";

                                        entPatientMaster.CNS = !string.IsNullOrEmpty(txtCNS.Text) ? txtCNS.Text : "";

                                        entPatientMaster.OBGY = !string.IsNullOrEmpty(txtOBGY.Text) ? txtOBGY.Text : "";

                                        entPatientMaster.XRAY = !string.IsNullOrEmpty(txtXRay.Text) ? txtXRay.Text : "";

                                        entPatientMaster.ECG = !string.IsNullOrEmpty(txtECG.Text) ? txtECG.Text : "";

                                        entPatientMaster.USG = !string.IsNullOrEmpty(txtUSG.Text) ? txtUSG.Text : "";

                                        if (chkCom.Checked)
                                        {
                                            if (ddlCompName.SelectedIndex == 0)
                                            {
                                                lblMsg.Text = "Please Select Company Name";
                                                ddlCompName.Focus();
                                                entPatientMaster.CompanyCode = null;
                                                return;
                                            }
                                            else
                                            {
                                                entPatientMaster.CompanyCode = Convert.ToString(ddlCompName.SelectedValue);
                                                entPatientMaster.CompanyId = Convert.ToInt32(ddlCompName.SelectedValue);
                                                entPatientMaster.CompName = Convert.ToString(ddlCompName.SelectedItem.Text);
                                            }
                                        }
                                        else
                                        {
                                            entPatientMaster.CompanyCode = string.Empty;
                                            entPatientMaster.CompName = string.Empty;
                                        }
                                        entPatientMaster.IsDischarged = false;
                                        if (rbtnOPD.Checked == true)
                                        {
                                            entPatientMaster.PatientType = "OPD";
                                        }
                                        else
                                        {
                                            entPatientMaster.PatientType = "IPD";
                                        }

                                        entPatientMaster.EntryBy = SessionManager.Instance.LoginUser.PKId.ToString();
                                        entPatientMaster.PatientPhoto = (Byte[])Session["Photo"];
                                        entPatientMaster.BloodGroup = ddlBloodGroup.SelectedValue;
                                        if (ChkInsurance.Checked)
                                        {
                                            entPatientMaster.InsuranceCompName = Convert.ToString(ddlInsurance.SelectedValue);
                                            entPatientMaster.InsuranceCompID = Convert.ToInt32(ddlInsurance.SelectedValue);
                                            entPatientMaster.InsuName = Convert.ToString(ddlInsurance.SelectedItem.Text);
                                            entPatientMaster.IDProof = bdefault;
                                            entPatientMaster.InsurenaceProof = bdefault;
                                        }
                                        else
                                        {
                                            entPatientMaster.InsuranceCompName = string.Empty;
                                            entPatientMaster.InsuName = string.Empty;
                                            entPatientMaster.InsuranceCompID = 0;
                                            entPatientMaster.IDProof = bdefault;
                                            entPatientMaster.InsurenaceProof = bdefault;
                                        }
                                        entPatientMaster.PatientHistory = "";
                                        entPatientMaster.PastMedicalHistory = "";
                                        entPatientMaster.FamilyHistory = "";
                                        entPatientMaster.BP = "";
                                        if (!string.IsNullOrEmpty(txtBP.Text))
                                        {
                                            entPatientMaster.BP = txtBP.Text;
                                        }
                                        entPatientMaster.PatientTypeId = 0;
                                        if (ddlPatientType.SelectedIndex>0)
                                        {
                                            entPatientMaster.PatientTypeId = Convert.ToInt32(ddlPatientType.SelectedValue);
                                        }
                                        linCnt = mobjPatient.UpdatePatient(entPatientMaster);

                                        if (linCnt > 0)
                                        {
                                            Response.Redirect("frmOPDPatientDetail.aspx", false);
                                            lblMsg.Text = "Record Updated Succefully...";
                                            DataTable ldt = new DataTable();
                                        }
                                        else
                                        {
                                            lblMsg.Text = "Record Not Inserted...";
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                Clear();
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void ChkInsurance_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (ChkInsurance.Checked)
                {
                    ddlInsurance.Enabled = true;
                    fileId.Enabled = true;
                    fileInsurance.Enabled = true;
                }
                else
                {
                    ddlInsurance.SelectedIndex = 0;
                    ddlInsurance.Enabled = false;
                    fileId.Enabled = false;
                    fileInsurance.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        #region "DropDownLists"

        public void GetInsuranceCompanies()
        {
            try
            {
                var ldt = mobjPatient.GetInsuranceCompanies();
                ddlInsurance.DataSource = ldt;
                ddlInsurance.DataValueField = "PKID";
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

        //private void FillCast()
        //{
        //    try
        //    {
        //        ListItem li = new ListItem();
        //        li.Text = "--Select Caste--";
        //        li.Value = "0";
        //        ddlCasts.Items.Insert(0, li);
        //    }
        //    catch (Exception ex)
        //    {
        //        lblMsg.Text = ex.Message;
        //    }
        //}

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

        public void GetInitials()
        {
            try
            {
                DataTable ldt = new DataTable();
                ldt = mobjPatient.GetInitials();
                ddlInitials.DataSource = ldt;
                ddlInitials.DataValueField = "PKId";
                ddlInitials.DataTextField = "InitialDesc";
                ddlInitials.DataBind();

                ListItem li = new ListItem();
                li.Text = "--Select--";
                li.Value = "0";
                ddlInitials.Items.Insert(0, li);
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        public void GetGenders()
        {
            try
            {
                DataTable ldt = new DataTable();
                ldt = mobjPatient.GetGenders();
                ddlGender.DataSource = ldt;
                ddlGender.DataValueField = "PKId";
                ddlGender.DataTextField = "GenderDesc";
                ddlGender.DataBind();

                ListItem li = new ListItem();
                li.Text = "--Select Gender--";
                li.Value = "0";
                ddlGender.Items.Insert(0, li);
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
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

        public void GetOccupation()
        {
            try
            {
                DataTable ldt = new DataTable();
                ldt = mobjPatient.GetOccupation();
                ddlOccupation.DataSource = ldt;
                ddlOccupation.DataValueField = "PKId";
                ddlOccupation.DataTextField = "OccupationDesc";
                ddlOccupation.DataBind();

                ListItem li = new ListItem();
                li.Text = "--Select Occupation--";
                li.Value = "0";
                ddlOccupation.Items.Insert(0, li);
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

        //public void GetReligions()
        //{
        //    try
        //    {
        //        DataTable ldt = new DataTable();
        //        ldt = mobjPatient.GetReligions();
        //        ddlReligion.DataSource = ldt;
        //        ddlReligion.DataValueField = "PKId";
        //        ddlReligion.DataTextField = "ReligionDesc";
        //        ddlReligion.DataBind();

        //        ListItem li = new ListItem();
        //        li.Text = "--Select Religion--";
        //        li.Value = "0";
        //        ddlReligion.Items.Insert(0, li);
        //    }
        //    catch (Exception ex)
        //    {
        //        lblMsg.Text = ex.Message;
        //    }
        //}
        #endregion


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

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            int linCnt = 0;
            Byte[] bdefault = new Byte[] { 0 };
            EntityPatientMaster entPatientMaster = new EntityPatientMaster();
            try
            {
                //if (ddlDeptDoctor.Items.Count==0)
                //{
                //    lblMsg.Text = "Please Select dept doctor.";
                //    return;
                //}
                if (ddlInitials.SelectedIndex == 0)
                {
                    lblMsg.Text = "Please Select Initial For Patient Name....";
                }
                else
                {
                    if (string.IsNullOrEmpty(txtFirstName.Text))
                    {
                        lblMsg.Text = "Please Enter First Name.....";
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(txtLastName.Text.Trim()))
                        {
                            lblMsg.Text = "Please Enter Last Name....";
                        }
                        else
                        {
                            if (ddlGender.SelectedIndex == 0)
                            {
                                lblMsg.Text = "Please Select Gender....";
                            }
                            else
                            {
                                //if (rbtnOPD.Checked == true && rbtnIPD.Checked == false)
                                //{
                                //    //DataTable ldt1 = new DataTable();
                                //    //ldt1 = mobjPatient.GetNewOPDNumber();
                                //    //if (ldt1.Rows.Count > 0 && ldt1 != null && (ldt1.Rows[0]["PatientOPDNo"].ToString() != string.Empty))
                                //    //{
                                //    //    txtIPDNo.Text = ldt1.Rows[0]["PatientOPDNo"].ToString();
                                //    //}
                                //    //entPatientMaster.OPDNo = txtIPDNo.Text;
                                //    entPatientMaster.IPDNo = "";
                                //}
                                //if (rbtnIPD.Checked == true && rbtnOPD.Checked == false)
                                //{
                                //    DataTable ldt1 = new DataTable();
                                //    ldt1 = mobjPatient.GetNewIPDNumber();
                                //    if (ldt1.Rows.Count > 0 && ldt1 != null && (ldt1.Rows[0]["PatientIPDNo"].ToString() != string.Empty))
                                //    {
                                //        txtIPDNo.Text = ldt1.Rows[0]["PatientIPDNo"].ToString();
                                //    }
                                //    entPatientMaster.IPDNo = txtIPDNo.Text;
                                //    entPatientMaster.OPDNo = "";
                                //}
                                if (rbtnOPD.Checked == false && rbtnIPD.Checked == false)
                                {
                                    lblMsg.Text = "Please Select Type Of Patient";
                                    rbtnOPD.Focus();
                                    return;
                                }
                                else
                                {
                                    DataTable ldt = new DataTable();
                                    ldt = mobjPatient.GetNewPatientCode();
                                    if (ldt.Rows.Count > 0 && ldt != null)
                                    {
                                        txtPatientCode.Text = ldt.Rows[0]["PatientCode"].ToString();
                                    }
                                    entPatientMaster.PatientCode = txtPatientCode.Text.Trim();
                                    TimeSpan objTime = new TimeSpan(AdmissionTimeSelector.Hour, AdmissionTimeSelector.Minute, 0);
                                    try
                                    {
                                        entPatientMaster.PatientAdmitDate = StringExtension.ToDateTime(txtAdmitDate.Text).Add(objTime);
                                        entPatientMaster.PatientAdmitTime = Convert.ToString(objTime);
                                    }
                                    catch (Exception ex)
                                    {
                                        entPatientMaster.PatientAdmitDate = DateTime.Now.Date;
                                    }
                                    entPatientMaster.PatientInitial = Convert.ToInt32(ddlInitials.SelectedValue);
                                    entPatientMaster.BP = !string.IsNullOrEmpty(txtBP.Text) ? txtBP.Text : "";
                                    entPatientMaster.PatientTypeId = ddlPatientType.SelectedIndex > 0 ? Convert.ToInt32(ddlPatientType.SelectedValue) : 0;

                                    entPatientMaster.PatientFirstName = FirstCharToUpper(txtFirstName.Text);
                                    entPatientMaster.PatientMiddleName = FirstCharToUpper(txtMidleName.Text);
                                    entPatientMaster.PatientLastName = FirstCharToUpper(txtLastName.Text);
                                    entPatientMaster.Gender = Convert.ToInt32(ddlGender.SelectedIndex);
                                    if (!string.IsNullOrEmpty(txtBirthDate.Text))
                                    {
                                        entPatientMaster.Age = StringExtension.ToDateTime(txtAdmitDate.Text).Year - StringExtension.ToDateTime(txtBirthDate.Text.Trim()).Year;
                                    }
                                    entPatientMaster.Age = string.IsNullOrEmpty(txtAge.Text) == false ? Convert.ToInt32(txtAge.Text) : 0;
                                    if (!string.IsNullOrEmpty(txtBirthDate.Text))
                                    {
                                        entPatientMaster.BirthDate = StringExtension.ToDateTime(txtBirthDate.Text);
                                    }
                                    entPatientMaster.Weight = Convert.ToString(txtWeight.Text);
                                    entPatientMaster.Occupation = Convert.ToInt32(ddlOccupation.SelectedValue);
                                    entPatientMaster.DeptCategory =ddlDeptCategory.SelectedIndex>0? Convert.ToInt32(ddlDeptCategory.SelectedValue):SettingsManager.Instance.DeptId;
                                    entPatientMaster.DeptDoctorId =ddlDeptDoctor.SelectedIndex>0? Convert.ToInt32(ddlDeptDoctor.SelectedValue):SettingsManager.Instance.DoctorId;
                                    entPatientMaster.PatientContactNo = txtContactNo.Text.Trim();
                                    entPatientMaster.PatientAddress = FirstCharToUpper(txtAddress.Text);
                                    entPatientMaster.City = FirstCharToUpper(txtCity.Text);
                                    entPatientMaster.State = FirstCharToUpper(txtState.Text);
                                    entPatientMaster.Country = FirstCharToUpper(txtCountry.Text);
                                    entPatientMaster.ReferedBy = FirstCharToUpper(txtRefDoctor.Text);
                                    entPatientMaster.Dignosys = FirstCharToUpper(txtDignosys.Text);
                                    if (chkCom.Checked)
                                    {
                                        if (ddlCompName.SelectedIndex == 0)
                                        {
                                            lblMsg.Text = "Please Select Company Name";
                                            ddlCompName.Focus();
                                            entPatientMaster.CompanyCode = null;
                                            return;
                                        }
                                        else
                                        {
                                            entPatientMaster.CompanyCode = Convert.ToString(ddlCompName.SelectedValue);
                                            entPatientMaster.CompanyId = Convert.ToInt32(ddlCompName.SelectedValue);
                                            entPatientMaster.CompName = Convert.ToString(ddlCompName.SelectedItem.Text);
                                        }
                                    }
                                    else
                                    {
                                        entPatientMaster.CompanyCode = string.Empty;
                                        entPatientMaster.CompName = string.Empty;
                                    }
                                    entPatientMaster.IsDischarged = false;
                                    if (rbtnOPD.Checked == true)
                                    {
                                        entPatientMaster.PatientType = "OPD";
                                    }
                                    else
                                    {
                                        entPatientMaster.PatientType = "IPD";
                                    }

                                    entPatientMaster.EntryBy = SessionManager.Instance.LoginUser.PKId.ToString();
                                    entPatientMaster.PatientPhoto = (Byte[])Session["Photo"];
                                    entPatientMaster.BloodGroup = ddlBloodGroup.SelectedValue;
                                    if (ChkInsurance.Checked)
                                    {
                                        entPatientMaster.InsuranceCompName = Convert.ToString(ddlInsurance.SelectedValue);
                                        entPatientMaster.InsuranceCompID = Convert.ToInt32(ddlInsurance.SelectedValue);
                                        entPatientMaster.InsuName = Convert.ToString(ddlInsurance.SelectedItem.Text);
                                        entPatientMaster.IDProof = bdefault;
                                        entPatientMaster.InsurenaceProof = bdefault;
                                    }
                                    else
                                    {
                                        entPatientMaster.InsuranceCompName = string.Empty;
                                        entPatientMaster.InsuName = string.Empty;
                                        entPatientMaster.InsuranceCompID = 0;
                                        entPatientMaster.IDProof = bdefault;
                                        entPatientMaster.InsurenaceProof = bdefault;
                                    }
                                    entPatientMaster.PatientHistory = "";
                                    entPatientMaster.PastMedicalHistory = "";
                                    entPatientMaster.FamilyHistory = "";
                                    entPatientMaster.ProvDiag = !string.IsNullOrEmpty(txtProvDiag.Text) ? txtProvDiag.Text : "";

                                    entPatientMaster.FinalDiag = !string.IsNullOrEmpty(txtFinalDiag.Text) ? txtFinalDiag.Text : "";

                                    entPatientMaster.Ailergies = !string.IsNullOrEmpty(txtAilergies.Text) ? txtAilergies.Text : "";

                                    entPatientMaster.Symptomes = !string.IsNullOrEmpty(txtSymptoms.Text) ? txtSymptoms.Text : "";

                                    entPatientMaster.PastIllness = !string.IsNullOrEmpty(txtPastIllness.Text) ? txtPastIllness.Text : "";

                                    entPatientMaster.Temperature = !string.IsNullOrEmpty(txtTemperature.Text) ? txtTemperature.Text : "";

                                    entPatientMaster.Pulse = !string.IsNullOrEmpty(txtPulse.Text) ? txtPulse.Text : "";

                                    entPatientMaster.Respiration = !string.IsNullOrEmpty(txtRespiration.Text) ? txtRespiration.Text : "";

                                    entPatientMaster.Others = !string.IsNullOrEmpty(txtOthers.Text) ? txtOthers.Text : "";

                                    entPatientMaster.RS = !string.IsNullOrEmpty(txtRS.Text) ? txtRS.Text : "";

                                    entPatientMaster.CVS = !string.IsNullOrEmpty(txtCVS.Text) ? txtCVS.Text : "";

                                    entPatientMaster.PA = !string.IsNullOrEmpty(txtPA.Text) ? txtPA.Text : "";

                                    entPatientMaster.CNS = !string.IsNullOrEmpty(txtCNS.Text) ? txtCNS.Text : "";

                                    entPatientMaster.OBGY = !string.IsNullOrEmpty(txtOBGY.Text) ? txtOBGY.Text : "";

                                    entPatientMaster.XRAY = !string.IsNullOrEmpty(txtXRay.Text) ? txtXRay.Text : "";

                                    entPatientMaster.ECG = !string.IsNullOrEmpty(txtECG.Text) ? txtECG.Text : "";

                                    entPatientMaster.USG = !string.IsNullOrEmpty(txtUSG.Text) ? txtUSG.Text : "";

                                    if (!Commons.IsRecordExists("tblPatientMaster", "PatientFirstName + ' ' + PatientMiddleName + ' ' + PatientLastName", entPatientMaster.PatientFirstName + ' ' + entPatientMaster.PatientMiddleName + ' ' + entPatientMaster.PatientLastName))
                                    {
                                        if (!Commons.IsRecordExists("tblPatientMaster", "PatientCode", entPatientMaster.PatientCode))
                                        {
                                            linCnt = mobjPatient.InsertIPDPatient(entPatientMaster);

                                            if (linCnt > 0)
                                            {
                                                Response.Redirect("frmOPDPatientDetail.aspx", false);
                                                lblMsg.Text = "Record Inserted Succefully...";
                                            }
                                            else
                                            {
                                                lblMsg.Text = "Record Not Inserted...";
                                            }
                                        }
                                        else
                                        {
                                            lblMsg.Text = "Patient Already Exists...";
                                        }
                                    }
                                    else
                                    {
                                        lblMsg.Text = "Patient Name Already Exists...";
                                    }
                                }
                            }
                        }
                        //    }
                        //}
                    }
                }
                //Clear();
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }

        }
        protected void BtnimgUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (imgUpload.HasFile)
                {
                    if (Path.GetExtension(imgUpload.PostedFile.FileName) == ".jpg" || Path.GetExtension(imgUpload.PostedFile.FileName) == ".bmp" || Path.GetExtension(imgUpload.PostedFile.FileName) == ".png")
                    {
                        Stream fs1 = imgUpload.PostedFile.InputStream;
                        BinaryReader br1 = new BinaryReader(fs1);
                        Byte[] bytes1 = br1.ReadBytes((Int32)fs1.Length);
                        Session["Photo"] = bytes1;
                        string base64String = Convert.ToBase64String(bytes1, 0, bytes1.Length);
                        imgPhoto.ImageUrl = "data:image/png;base64," + base64String;
                    }
                    else
                    {
                        lblMsg.Text = "Please Select Only Image File With Extension .JPG, .BMP, .PNG ...";
                        return;
                    }
                }
                else
                {
                    lblMsg.Text = "Please select Patient Photo File...";
                    return;
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        //private void ConvertToPDF(DataTable ldtReport, string pstrReportName, string ReportName)
        //{
        //    try
        //    {
        //        ReportDocument Rel = new ReportDocument();
        //        Rel.Load(Server.MapPath("~/Reports/" + pstrReportName + ""));
        //        Rel.SetDataSource(ldtReport);
        //        BinaryReader stream = new BinaryReader(Rel.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat));
        //        Response.ClearContent();
        //        Response.ClearHeaders();
        //        Response.ContentType = "application/pdf";
        //        Response.AddHeader("content-disposition", "attachment; filename= " + ReportName + ".pdf");
        //        // Response.AddHeader("content-disposition", "inline; filename= CustomerPayemntDetail");
        //        Response.AddHeader("content-length", stream.BaseStream.Length.ToString());
        //        Response.BinaryWrite(stream.ReadBytes(Convert.ToInt32(stream.BaseStream.Length)));
        //        Response.Flush();
        //        Response.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        lblMsg.Text = ex.Message;
        //    }
        //}

        protected void chkCom_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkCom.Checked)
                {
                    ddlCompName.Enabled = true;
                }
                else
                {
                    ddlCompName.Enabled = false;
                }
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
                Response.Redirect("frmOPDPatientDetail.aspx", false);
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }



        private void Clear()
        {
            try
            {
                imgPhoto.ImageUrl = "";
                txtAdmitDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                ddlInitials.SelectedIndex = 0;
                txtFirstName.Text = string.Empty;
                txtMidleName.Text = string.Empty;
                //txtIPDNo.Text = string.Empty;
                txtLastName.Text = string.Empty;
                ddlGender.SelectedIndex = 0;
                txtAge.Text = string.Empty;
                txtWeight.Text = string.Empty;
                ddlOccupation.SelectedIndex = 0;
                //ddlReligion.SelectedIndex = 0;
                ddlDeptCategory.SelectedIndex = 0;
                //ddlAge.SelectedIndex = 0;
                //ddlCasts.SelectedIndex = 0;
                txtBirthDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtAddress.Text = string.Empty;
                txtContactNo.Text = string.Empty;
                txtCity.Text = string.Empty;
                txtState.Text = string.Empty;
                txtCountry.Text = string.Empty;
                txtRefDoctor.Text = string.Empty;
                ddlBloodGroup.SelectedIndex = 0;
                chkCom.Checked = false;
                ddlCompName.SelectedIndex = 0;
                ddlCompName.Enabled = false;
                ChkInsurance.Checked = false;
                ddlInsurance.SelectedIndex = 0;
                lblMsg.Text = string.Empty;
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }
    }
}