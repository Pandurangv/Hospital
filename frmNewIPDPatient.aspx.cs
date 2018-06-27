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
using System.IO;

namespace Hospital
{
    public partial class frmNewIPDPatient : System.Web.UI.Page
    {
        PatientMasterBLL mobjPatient = new PatientMasterBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //SessionManager.Instance.SetSession();
                txtAdmitTime.Text = DateTime.Now.ToShortTimeString();
                DataTable ldt = new DataTable();
                ldt = mobjPatient.GetNewPatientCode();
                if (ldt.Rows.Count > 0 && ldt != null)
                {
                    txtPatientCode.Text = ldt.Rows[0]["PatientCode"].ToString();
                }
                txtAdmitDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtBirthDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                GetInsuranceCompanies();
                GetCompanies();
                GetConsultants();
                GetInitials();
                GetGenders();
                GetOccupation();
                GetReligions();
                GetFloors();
                FillCast();
                FillWard();
                FillBedNo();
                ddlBedNo.Enabled = false;
                ddlWard.Enabled = false;
                ddlCasts.Enabled = false;
                ddlInsurance.Enabled = false;
                fileId.Enabled = false;
                fileInsurance.Enabled = false;
            }
        }

        protected void BtnDelete_Click(object sender, EventArgs e)
        {

        }

        #region "DropDownLists"

        public void GetInitials()
        {
            DataTable ldt = new DataTable();
            ldt = mobjPatient.GetInitials();
            ddlInitials.DataSource = ldt;
            ddlInitials.DataValueField = "PKId";
            ddlInitials.DataTextField = "InitialDesc";
            ddlInitials.DataBind();

            //ListItem li = new ListItem();
            //li.Text = "--Select Initial--";
            //li.Value = "0";
            //ddlInitials.Items.Insert(0, li);
        }

        public void GetGenders()
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



        public void GetConsultants()
        {
            DataTable ldt = new DataTable();
            ldt = mobjPatient.GetConsultants();
            ddlConsultingDoctor.DataSource = ldt;
            ddlConsultingDoctor.DataValueField = "PKId";
            ddlConsultingDoctor.DataTextField = "ConsultantName";
            ddlConsultingDoctor.DataBind();

            ListItem li = new ListItem();
            li.Text = "--Select Consultant--";
            li.Value = "0";
            ddlConsultingDoctor.Items.Insert(0, li);
        }

        public void GetCompanies()
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

        public void GetOccupation()
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

        public void GetReligions()
        {
            DataTable ldt = new DataTable();
            ldt = mobjPatient.GetReligions();
            ddlReligion.DataSource = ldt;
            ddlReligion.DataValueField = "PKId";
            ddlReligion.DataTextField = "ReligionDesc";
            ddlReligion.DataBind();

            ListItem li = new ListItem();
            li.Text = "--Select Religion--";
            li.Value = "0";
            ddlReligion.Items.Insert(0, li);
        }

        public void GetFloors()
        {
            DataTable ldt = new DataTable();
            ldt = mobjPatient.GetFloors();
            ddlFloor.DataSource = ldt;
            ddlFloor.DataValueField = "FloorId";
            ddlFloor.DataTextField = "FloorName";
            ddlFloor.DataBind();

            ListItem li = new ListItem();
            li.Text = "--Select Floors--";
            li.Value = "0";
            ddlFloor.Items.Insert(0, li);
        }

        public void GetInsuranceCompanies()
        {
            DataTable ldt = new DataTable();
            ldt = mobjPatient.GetCompanies();
            ddlInsurance.DataSource = ldt;
            ddlInsurance.DataValueField = "CompanyCode";
            ddlInsurance.DataTextField = "CompanyName";
            ddlInsurance.DataBind();

            ListItem li = new ListItem();
            li.Text = "--Select Company--";
            li.Value = "0";
            ddlInsurance.Items.Insert(0, li);
        }

        #endregion

        protected void ddlFloor_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable ldt = new DataTable();
            DataTable ldtRoomCateId = new DataTable();
            int lintPKId = Convert.ToInt32(ddlFloor.SelectedValue);
            ldtRoomCateId = mobjPatient.GetRoomCategoryByFloor(lintPKId);
            if (ldtRoomCateId.Rows.Count > 0 && ldtRoomCateId != null)
            {
                int lintRoomId = Convert.ToInt32(ldtRoomCateId.Rows[0]["CategoryId"]);
                //ldt = mobjPatient.GetWardByFloor(lintRoomId);
                //if (ldt.Rows.Count > 0 && ldt != null)
                //{
                ddlWard.DataSource = ldtRoomCateId;
                ddlWard.DataValueField = "CategoryId";
                ddlWard.DataTextField = "CategoryDesc";
                ddlWard.DataBind();
                ddlWard.Enabled = true;
                FillWard();
            }
            else
            {
                ddlWard.Items.Clear();
                ddlBedNo.Items.Clear();
                FillWard();
                FillBedNo();
                ddlBedNo.Enabled = false;
                ddlWard.Enabled = false;
            }
        }



        private void FillWard()
        {
            ListItem li = new ListItem();
            li.Text = "--Select Ward No--";
            li.Value = "0";
            ddlWard.Items.Insert(0, li);
            ddlWard.Enabled = true;
        }
        protected void ddlWard_SelectedIndexChanged(object sender, EventArgs e)
        {
            //   bool lbIsDischarged = false;
            DataTable ldt = new DataTable();
            DataTable ldtFor = new DataTable();
            ldtFor.Columns.Add("BedId", typeof(int));
            ldtFor.Columns.Add("BedNo", typeof(int));

            List<int> lstlinBed = new List<int>();
            int lintRoomId = Convert.ToInt32(ddlWard.SelectedValue);
            int lintFloorId = Convert.ToInt32(ddlFloor.SelectedValue);

            ldt = mobjPatient.GetBedByWard(lintRoomId, lintFloorId);
            if (ldt.Rows.Count > 0 && ldt != null)
            {
                int lintBedCnt = Convert.ToInt32(ldt.Rows[0]["BedId"]);

                ddlBedNo.DataSource = ldt;
                ddlBedNo.DataValueField = "BedId";
                ddlBedNo.DataTextField = "BedNo";
                ddlBedNo.DataBind();
                FillBedNo();
            }
            else
            {
                ddlBedNo.Items.Clear();
                FillBedNo();
                ddlBedNo.Enabled = false;
            }
        }

        private void FillBedNo()
        {
            ListItem li = new ListItem();
            li.Text = "--Select Bed No--";
            li.Value = "0";
            ddlBedNo.Items.Insert(0, li);
            ddlBedNo.Enabled = true;
        }
        protected void ddlReligion_SelectedIndexChanged(object sender, EventArgs e)
        {
            int linReligionId = Convert.ToInt32(ddlReligion.SelectedValue);
            DataTable ldt = new DataTable();
            ldt = mobjPatient.GetCastes(linReligionId);
            if (ldt.Rows.Count > 0 && ldt != null)
            {
                ddlCasts.DataSource = ldt;
                ddlCasts.DataValueField = "PKId";
                ddlCasts.DataTextField = "CastDesc";
                ddlCasts.DataBind();

                FillCast();
                ddlCasts.Enabled = true;
            }
            else
            {
                ddlCasts.Enabled = false;
            }
        }

        private void FillCast()
        {
            ListItem li = new ListItem();
            li.Text = "--Select Caste--";
            li.Value = "0";
            ddlCasts.Items.Insert(0, li);
        }
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            int linCnt = 0;
            Byte[] bdefault = new Byte[] { 0 };
            EntityPatientMaster entPatientMaster = new EntityPatientMaster();

            try
            {
                if (string.IsNullOrEmpty(txtFirstName.Text))
                {
                    Commons.ShowMessage("Enter First Name.....", this.Page);
                }
                else
                {
                    if (string.IsNullOrEmpty(txtMidleName.Text.Trim()))
                    {
                        Commons.ShowMessage("Enter Middle Name.....", this.Page);
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(txtLastName.Text.Trim()))
                        {
                            Commons.ShowMessage("Enter Last Name....", this.Page);
                        }
                        else
                        {
                            if (ddlGender.SelectedIndex == 0)
                            {
                                Commons.ShowMessage("Select Gender....", this.Page);
                            }
                            else
                            {
                                if (ddlConsultingDoctor.SelectedIndex == 0)
                                {
                                    Commons.ShowMessage("Select Consulting Doctor....", this.Page);
                                }
                                else
                                {
                                    if (ddlFloor.SelectedIndex == 0)
                                    {
                                        Commons.ShowMessage("Select Floor....", this.Page);
                                    }
                                    else
                                    {
                                        if (ddlWard.SelectedIndex == 0)
                                        {
                                            Commons.ShowMessage("Select Ward....", this.Page);
                                        }
                                        else
                                        {
                                            if (ddlBedNo.SelectedIndex == 0)
                                            {
                                                Commons.ShowMessage("Select Bed No....", this.Page);
                                            }
                                            else
                                            {
                                                entPatientMaster.PatientCode = txtPatientCode.Text.Trim();
                                                entPatientMaster.PatientAdmitDate = StringExtension.ToDateTime(txtAdmitDate.Text.Trim());
                                                entPatientMaster.PatientAdmitTime = txtAdmitTime.Text;
                                                entPatientMaster.PatientInitial = Convert.ToInt32(ddlInitials.SelectedValue);
                                                entPatientMaster.PatientFirstName = txtFirstName.Text.Trim();
                                                entPatientMaster.PatientMiddleName = txtMidleName.Text.Trim();
                                                entPatientMaster.PatientLastName = txtLastName.Text.Trim();
                                                entPatientMaster.Gender = Convert.ToInt32(ddlGender.SelectedValue);
                                                entPatientMaster.Age = Convert.ToInt32(txtAge.Text.Trim());
                                                entPatientMaster.Occupation = Convert.ToInt32(ddlOccupation.SelectedValue);
                                                entPatientMaster.Religion = Convert.ToInt32(ddlReligion.SelectedValue);
                                                entPatientMaster.Caste = Convert.ToInt32(ddlCasts.SelectedValue);
                                                entPatientMaster.PatientContactNo = txtContactNo.Text.Trim();
                                                entPatientMaster.PatientAddress = txtAddress.Text.Trim();
                                                entPatientMaster.City = txtCity.Text.Trim();
                                                entPatientMaster.State = txtState.Text.Trim();
                                                entPatientMaster.Country = txtCountry.Text.Trim();
                                                entPatientMaster.ReasonForAdmit = txtReasonForAdmit.Text.Trim();
                                                entPatientMaster.ReferedBy = txtReferedBy.Text.Trim();
                                                entPatientMaster.ConsultingDr = Convert.ToInt32(ddlConsultingDoctor.SelectedValue);
                                                entPatientMaster.CompanyCode = Convert.ToString(ddlCompName.SelectedValue);
                                                entPatientMaster.CompanyAddress = txtCompAddress.Text.Trim();
                                                entPatientMaster.CompanyContact = txtComContNo.Text.Trim();
                                                entPatientMaster.FloorNo = Convert.ToInt32(ddlFloor.SelectedValue);
                                                entPatientMaster.WardNo = Convert.ToInt32(ddlWard.SelectedValue);
                                                entPatientMaster.BedNo = Convert.ToInt32(ddlBedNo.SelectedValue);
                                                entPatientMaster.IsDischarged = false;
                                                entPatientMaster.PatientType = "I";
                                                entPatientMaster.BirthDate = StringExtension.ToDateTime(txtBirthDate.Text.Trim());
                                                entPatientMaster.EntryBy = SessionManager.Instance.LoginUser.EmpCode;

                                                if (ChkInsurance.Checked)
                                                {
                                                    entPatientMaster.InsuranceCompName = Convert.ToString(ddlInsurance.SelectedValue);
                                                    if (fileId.HasFile)
                                                    {
                                                        if (Path.GetExtension(fileId.PostedFile.FileName) == ".pdf")
                                                        {
                                                            Stream fs = fileId.PostedFile.InputStream;
                                                            BinaryReader br = new BinaryReader(fs);
                                                            Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                                                            entPatientMaster.IDProof = bytes;

                                                            if (fileInsurance.HasFile)
                                                            {
                                                                if (Path.GetExtension(fileInsurance.PostedFile.FileName) == ".pdf")
                                                                {
                                                                    Stream fs1 = fileInsurance.PostedFile.InputStream;
                                                                    BinaryReader br1 = new BinaryReader(fs1);
                                                                    Byte[] bytes1 = br.ReadBytes((Int32)fs1.Length);
                                                                    entPatientMaster.InsurenaceProof = bytes1;
                                                                }
                                                                else
                                                                {
                                                                    Commons.ShowMessage("Select Only pdf File...", this.Page);
                                                                    return;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                Commons.ShowMessage("Please select Insurance Proof File...", this.Page);
                                                                return;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            Commons.ShowMessage("Select Only pdf File...", this.Page);
                                                            return;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        Commons.ShowMessage("Please select Id Proof File...", this.Page);
                                                        return;
                                                    }

                                                }
                                                else
                                                {
                                                    entPatientMaster.InsuranceCompName = string.Empty;
                                                    entPatientMaster.IDProof = bdefault;
                                                    entPatientMaster.InsurenaceProof = bdefault;
                                                }

                                                if (!Commons.IsRecordExists("tblPatientMaster", "PatientCode", entPatientMaster.PatientCode))
                                                {
                                                    linCnt = mobjPatient.InsertIPDPatient(entPatientMaster);

                                                    if (linCnt > 0)
                                                    {
                                                        Commons.ShowMessage("Record Inserted Succefully...", this.Page);
                                                    }
                                                    else
                                                    {
                                                        Commons.ShowMessage("Record Not Inserted...", this.Page);
                                                    }
                                                }
                                                else
                                                {
                                                    Commons.ShowMessage("Patient Already Exists...", this.Page);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmNewIPDPatient - BtnSave_Click(object sender, EventArgs e)", ex);
            }
        }

        protected void ddlCompName_SelectedIndexChanged(object sender, EventArgs e)
        {
            string lstrCompanyCode = Convert.ToString(ddlCompName.SelectedValue);
            DataTable ldt = new DataTable();
            ldt = mobjPatient.GetCompanyDetail(lstrCompanyCode);
            if (ldt.Rows.Count > 0 && ldt != null)
            {
                txtCompAddress.Text = ldt.Rows[0]["Address"].ToString();
                txtComContNo.Text = ldt.Rows[0]["MobileNo"].ToString();
            }
            else
            {
                txtCompAddress.Text = string.Empty;
                txtComContNo.Text = string.Empty;
            }
        }
        protected void ChkInsurance_CheckedChanged(object sender, EventArgs e)
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
    }
}