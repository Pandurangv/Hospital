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
using System.Globalization;
using System.IO;

namespace Hospital
{
    public partial class frmViewIPDPatient : System.Web.UI.Page
    {

        PatientMasterBLL mobjPatientMaster = new PatientMasterBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            ////SessionManager.Instance.SetSession();
            if (SessionManager.Instance.LoginUser != null)
            {
                if (!Page.IsPostBack)
                {
                    //SessionManager.Instance.SetSession();
                    txtStartDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtEndDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    GetCompanies();
                    GetInitials();
                    GetGenders();
                    GetOccupation();
                    GetReligions();
                    FillCast();
                    GetReferenceDoctor();
                }
            }
            else
            {
                //Response.Redirect("frmlogin.aspx", false);
            }
        }
        protected void BtnView_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtStartDate.Text.Trim().Length <= 0 || txtEndDate.Text.Trim().Length <= 0)
                {
                    Commons.ShowMessage("Start Date and End Date Should Not be Blank.", this.Page);
                    return;
                }

                DateTime ldtStart = DateTime.ParseExact(txtStartDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime ldtEnd = DateTime.ParseExact(txtEndDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                if (ldtStart > ldtEnd)
                {
                    Commons.ShowMessage("Start Date Should Not be Greater Than End Date.", this.Page);
                    return;
                }

                string lstrPatientCode = txtPatientId.Text;
                string lstrPatientFName = txtPatientFirstName.Text;
                string lstrPatinetMName = txtPatientMiddleName.Text;
                string lstrPatientLName = txtPatientLastName.Text;


                if (lstrPatientCode != string.Empty || lstrPatientFName != string.Empty || lstrPatinetMName != string.Empty || lstrPatientLName != string.Empty)
                {
                    ldtStart = StringExtension.ToDateTime("01/01/1753");
                    ldtEnd = StringExtension.ToDateTime("01/01/1753");
                }

                DataTable dt = new DataTable();
                dt = mobjPatientMaster.ViewRegistredPatient(ldtStart, ldtEnd, lstrPatientCode, lstrPatientFName, lstrPatinetMName, lstrPatientLName);
                Session["PatientDetail"] = dt;

                if (dt != null && dt.Rows.Count > 0)
                {
                    dgvViewIPDPatient.PageIndex = 0;
                    dgvViewIPDPatient.DataSource = dt;
                    dgvViewIPDPatient.DataBind();
                    pnlShow.Style.Add(HtmlTextWriterStyle.Display, "");
                    hdnPanel.Value = "";
                    int lintRowcount = dt.Rows.Count;
                    lblRowCount.Text = "<b>Total Records:</b> " + lintRowcount.ToString();
                    BtnDelete.Enabled = false;
                }
                else
                {
                    pnlShow.Style.Add(HtmlTextWriterStyle.Display, "none");
                    hdnPanel.Value = "none";
                    Commons.ShowMessage("No Data to Display.", this.Page);
                }
            }
            catch (System.Threading.ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                Commons.FileLog("frmViewIPDPatient - BtnView_Click(object sender, EventArgs e)", ex);
            }
        }
        protected void dgvViewIPDPatient_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                DataTable ldt = new DataTable();
                GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;

                if (e.CommandName == "IdProofDownload")
                {
                    LinkButton lnkPatientId = (LinkButton)gvr.FindControl("lnkPatientId");
                    Label lblFirstName = (Label)gvr.FindControl("lblPFirstName");
                    Label lblMiddleName = (Label)gvr.FindControl("lblPMiddleName");
                    Label lblLastName = (Label)gvr.FindControl("lblPLastName");
                    string lstrPatientId = lnkPatientId.Text;
                    ldt = mobjPatientMaster.GetIdProofFile(lstrPatientId);

                    if (ldt.Rows.Count > 0 && ldt != null)
                    {
                        Response.Clear();
                        Byte[] sBytes = (Byte[])ldt.Rows[0]["IdProofFile"];
                        MemoryStream ms = new MemoryStream(sBytes);
                        Response.Charset = "";
                        Response.ContentType = "application/pdf";
                        Response.AddHeader("content-disposition", "attachment;filename=" + lblFirstName.Text + lblMiddleName.Text + lblLastName.Text + ".pdf");
                        Response.Buffer = true;
                        ms.WriteTo(Response.OutputStream);
                        Response.BinaryWrite(sBytes);
                        Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        Response.End();
                    }
                }

                if (e.CommandName == "InsureanceProofDownload")
                {
                    LinkButton lnkPatientId = (LinkButton)gvr.FindControl("lnkPatientId");
                    Label lblFirstName = (Label)gvr.FindControl("lblPFirstName");
                    Label lblMiddleName = (Label)gvr.FindControl("lblPMiddleName");
                    Label lblLastName = (Label)gvr.FindControl("lblPLastName");
                    string lstrPatientId = lnkPatientId.Text;
                    ldt = mobjPatientMaster.GetInsurenceIdFile(lstrPatientId);

                    if (ldt.Rows.Count > 0 && ldt != null)
                    {
                        Response.Clear();
                        Byte[] sBytes = (Byte[])ldt.Rows[0]["InsureanceProof"];
                        MemoryStream ms = new MemoryStream(sBytes);
                        Response.Charset = "";
                        Response.ContentType = "application/pdf";
                        Response.AddHeader("content-disposition", "attachment;filename=" + lblFirstName.Text + lblMiddleName.Text + lblLastName.Text + ".pdf");
                        Response.Buffer = true;
                        ms.WriteTo(Response.OutputStream);
                        Response.BinaryWrite(sBytes);
                        Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        Response.End();
                    }
                }

                if (e.CommandName == "HMSDownload")
                {
                    LinkButton lnkPatientId = (LinkButton)gvr.FindControl("lnkPatientId");
                    Label lblFirstName = (Label)gvr.FindControl("lblPFirstName");
                    Label lblMiddleName = (Label)gvr.FindControl("lblPMiddleName");
                    Label lblLastName = (Label)gvr.FindControl("lblPLastName");

                    DataTable ldtCardInfo = new DataTable();
                    ldtCardInfo = mobjPatientMaster.GetAllRegisteredPatient(lnkPatientId.Text);

                    if (ldtCardInfo.Rows.Count > 0 && ldtCardInfo != null)
                    {
                        //ConvertToPDF(ldtCardInfo, "rptPatientRegistration.rpt", lblFirstName.Text + lblMiddleName.Text + lblLastName.Text);
                    }
                }

                if (e.CommandName == "EditIPDPatient")
                {
                    LinkButton lblPatientCode = (LinkButton)gvr.FindControl("lnkPatientId");
                    string lstrPatientCode = lblPatientCode.Text;
                    DataTable ldtPatientInfo = new DataTable();
                    ldtPatientInfo = mobjPatientMaster.GetPatientDetail(lstrPatientCode);
                    FillControls(ldtPatientInfo);
                    this.programmaticModalPopup.Show();
                }


            }
            catch (System.Threading.ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                Commons.FileLog("frmViewIPDPatient -  dgvDepartment_RowCommand(object sender, GridViewCommandEventArgs e)", ex);
            }
        }

        private void FillControls(DataTable ldt)
        {
            bool IsInsuered = false;
            txtPatientCode.Text = ldt.Rows[0]["PatientCode"].ToString();
            txtAdmitDate.Text = ldt.Rows[0]["AdminDate"].ToString();
            txtAdmitTime.Text = ldt.Rows[0]["AdmitTime"].ToString();
            ddlInitials.SelectedValue = ldt.Rows[0]["Initial"].ToString();
            txtFirstName.Text = ldt.Rows[0]["PatientFirstName"].ToString();
            txtMidleName.Text = ldt.Rows[0]["PatientMiddleName"].ToString();
            txtLastName.Text = ldt.Rows[0]["PatientLastName"].ToString();
            ddlGender.SelectedValue = ldt.Rows[0]["Gender"].ToString();
            txtAge.Text = ldt.Rows[0]["Age"].ToString();
            ddlOccupation.SelectedValue = ldt.Rows[0]["Occupation"].ToString();
            txtBirthDate.Text = ldt.Rows[0]["BirthDate"].ToString();
            ddlRefDoctor.SelectedValue = ldt.Rows[0]["ReferedBy"].ToString();
            txtAddress.Text = ldt.Rows[0]["Address"].ToString();
            txtContactNo.Text = ldt.Rows[0]["ContactNo"].ToString();
            txtCity.Text = ldt.Rows[0]["City"].ToString();
            txtState.Text = ldt.Rows[0]["State"].ToString();
            txtCountry.Text = ldt.Rows[0]["Country"].ToString();
            ddlCompName.SelectedValue = ldt.Rows[0]["CompanyCode"].ToString();
            ddlReligion.SelectedValue = ldt.Rows[0]["Religion"].ToString();
            ddlCasts.SelectedValue = ldt.Rows[0]["Caste"].ToString();
            IsInsuered = Convert.ToBoolean(ldt.Rows[0]["IsInsuered"]);
            txtPatientHistory.Text = ldt.Rows[0]["PatientHistory"].ToString();
            txtFamilyHistory.Text = ldt.Rows[0]["FamilyHistory"].ToString();
            txtPastMedHistory.Text = ldt.Rows[0]["PastMedHistory"].ToString();
            ddlBloodGroup.SelectedValue = ldt.Rows[0]["BloodGroup"].ToString();
            if (ldt.Rows[0]["PatientPhoto"] != DBNull.Value)
            {
                Byte[] bytes1 = (Byte[])ldt.Rows[0]["PatientPhoto"];
                string base64String = Convert.ToBase64String(bytes1, 0, bytes1.Length);
                imgPhoto.ImageUrl = "data:image/png;base64," + base64String;

            }
            else
            {
                imgPhoto.ImageUrl = "images/User_256x256.png";
            }
        }

        protected void dgvViewIPDPatient_DataBound(object sender, EventArgs e)
        {
            int lintCurrentPage = dgvViewIPDPatient.PageIndex + 1;
            lblPageCount.Text = "<b>Page</b> " + lintCurrentPage.ToString() + "<b> of </b>" + dgvViewIPDPatient.PageCount.ToString();
        }
        protected void dgvViewIPDPatient_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap; text-align:left;");
                }

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Attributes.Add("onmouseover", "SetMouseOver(this)");
                    e.Row.Attributes.Add("onmouseout", "SetMouseOut(this)");
                    Label lblInsuranceCompName = (Label)e.Row.FindControl("lblInsurCompName");
                    LinkButton lnkPatientCode = (LinkButton)e.Row.FindControl("lnkPatientId");
                    ImageButton lnkId = (ImageButton)e.Row.FindControl("IDFile");
                    ImageButton lnkInsurance = (ImageButton)e.Row.FindControl("InsuranceFile");
                    DataTable ldt = new DataTable();

                    ldt = mobjPatientMaster.GetPatientUploadedFiles(lnkPatientCode.Text);
                    if (ldt.Rows.Count > 0 && ldt != null)
                    {
                        Byte[] byteIdProof = (Byte[])ldt.Rows[0]["IdProofFile"];
                        Byte[] byteInsurance = (Byte[])ldt.Rows[0]["InsureanceProof"];
                        if (byteIdProof.Length > 1)
                        {
                            lnkId.Enabled = true;
                            lnkId.ImageUrl = "~/images/pdfIcon.jpg";
                        }
                        else
                        {
                            lnkId.Enabled = false;
                            lnkId.ImageUrl = "~/images/pdficonblack.jpg";
                        }

                        if (byteInsurance.Length > 1)
                        {
                            lnkInsurance.Enabled = true;
                            lnkInsurance.ImageUrl = "~/images/pdfIcon.jpg";
                        }
                        else
                        {
                            lnkInsurance.Enabled = false;
                            lnkInsurance.ImageUrl = "~/images/pdficonblack.jpg";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmViewIPDPatient -  dgvData_RowDataBound(object sender, GridViewRowEventArgs e)", ex);
            }
        }

        #region "DropDownLists"

        public void GetInitials()
        {
            DataTable ldt = new DataTable();
            ldt = mobjPatientMaster.GetInitials();
            ddlInitials.DataSource = ldt;
            ddlInitials.DataValueField = "PKId";
            ddlInitials.DataTextField = "InitialDesc";
            ddlInitials.DataBind();
        }

        public void GetReferenceDoctor()
        {
            DataTable ldt = new DataTable();
            ldt = mobjPatientMaster.GetRefDoctors();
            ddlRefDoctor.DataSource = ldt;
            ddlRefDoctor.DataValueField = "RefCode";
            ddlRefDoctor.DataTextField = "ReferDoctor";
            ddlRefDoctor.DataBind();

            ListItem li = new ListItem();
            li.Text = "--Select Reference Doctor--";
            li.Value = "0";
            ddlRefDoctor.Items.Insert(0, li);
        }

        public void GetGenders()
        {
            DataTable ldt = new DataTable();
            ldt = mobjPatientMaster.GetGenders();
            ddlGender.DataSource = ldt;
            ddlGender.DataValueField = "PKId";
            ddlGender.DataTextField = "GenderDesc";
            ddlGender.DataBind();

            ListItem li = new ListItem();
            li.Text = "--Select Gender--";
            li.Value = "0";
            ddlGender.Items.Insert(0, li);
        }


        public void GetCompanies()
        {
            DataTable ldt = new DataTable();
            ldt = mobjPatientMaster.GetCompanies();
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
            ldt = mobjPatientMaster.GetOccupation();
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
            ldt = mobjPatientMaster.GetReligions();
            ddlReligion.DataSource = ldt;
            ddlReligion.DataValueField = "PKId";
            ddlReligion.DataTextField = "ReligionDesc";
            ddlReligion.DataBind();

            ListItem li = new ListItem();
            li.Text = "--Select Religion--";
            li.Value = "0";
            ddlReligion.Items.Insert(0, li);
        }

        private void FillCast()
        {
            DataTable ldt = new DataTable();
            ldt = mobjPatientMaster.GetAllCastes();
            ddlCasts.DataSource = ldt;
            ddlCasts.DataValueField = "PKId";
            ddlCasts.DataTextField = "CastDesc";
            ddlCasts.DataBind();

            ListItem li = new ListItem();
            li.Text = "--Select Caste--";
            li.Value = "0";
            ddlCasts.Items.Insert(0, li);
        }

        #endregion



        protected void ddlReligion_SelectedIndexChanged(object sender, EventArgs e)
        {
            int linReligionId = Convert.ToInt32(ddlReligion.SelectedValue);
            DataTable ldt = new DataTable();
            ldt = mobjPatientMaster.GetCastes(linReligionId);
            if (ldt.Rows.Count > 0 && ldt != null)
            {
                ddlCasts.DataSource = ldt;
                ddlCasts.DataValueField = "PKId";
                ddlCasts.DataTextField = "CastDesc";
                ddlCasts.DataBind();
                //FillCast();
                ddlCasts.Enabled = true;
            }
            else
            {
                ddlCasts.Enabled = false;
            }
            this.programmaticModalPopup.Show();
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
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
                                entPatientMaster.ReferedBy = ddlRefDoctor.SelectedValue;
                                entPatientMaster.CompanyCode = Convert.ToString(ddlCompName.SelectedValue);
                                entPatientMaster.BirthDate = StringExtension.ToDateTime(txtBirthDate.Text.Trim());
                                entPatientMaster.EntryBy = SessionManager.Instance.LoginUser.EmpCode;
                                entPatientMaster.PersonalHistory = txtPatientHistory.Text;
                                entPatientMaster.PastMedicalHistory = txtPastMedHistory.Text;
                                entPatientMaster.FamilyHistory = txtFamilyHistory.Text;
                                entPatientMaster.BloodGroup = ddlBloodGroup.SelectedValue;

                                linCnt = mobjPatientMaster.UpdateIPDPatient(entPatientMaster);

                                if (linCnt > 0)
                                {
                                    Commons.ShowMessage("Record Updated Succefully...", this.Page);
                                }
                                else
                                {
                                    Commons.ShowMessage("Record Not Updated...", this.Page);
                                }

                                this.programmaticModalPopup.Hide();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmViewIPDPatient - BtnSave_Click(object sender, EventArgs e)", ex);
            }
        }
        protected void dgvViewIPDPatient_PageIndexChanged(object sender, EventArgs e)
        {
            dgvViewIPDPatient.DataSource = (DataTable)Session["PatientDetail"];
            dgvViewIPDPatient.DataBind();
        }
        protected void dgvViewIPDPatient_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvViewIPDPatient.PageIndex = e.NewPageIndex;
        }
        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow drv in dgvViewIPDPatient.Rows)
            {
                CheckBox chkDelete = (CheckBox)drv.FindControl("chkDelete");
                if (chkDelete.Checked)
                {
                    this.modalpopupDelete.Show();
                }
            }
        }
        protected void BtnDownload_Click(object sender, EventArgs e)
        {
            DataTable ldt = new DataTable();
            if (Session["PatientDetail"] != null)
            {
                ldt = (DataTable)Session["PatientDetail"];
                Session["PatientDetail"] = null;
            }

            if (Session["AllPatientDetail"] != null)
            {
                ldt = (DataTable)Session["AllPatientDetail"];
                Session["AllPatientDetail"] = null;
            }

            if (ldt.Rows.Count > 0 && ldt != null)
            {
                //ConvertToPDF(ldt, "rptRegisteredPatientListReport.rpt", "RegisteredPatientList");
            }
        }

        //private void ConvertToPDF(DataTable ldtReport, string pstrReportName, string ReportName)
        //{
        //    ReportDocument Rel = new ReportDocument();
        //    Rel.Load(Server.MapPath("~/Reports/" + pstrReportName + ""));
        //    Rel.SetDataSource(ldtReport);
        //    BinaryReader stream = new BinaryReader(Rel.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat));
        //    Response.ClearContent();
        //    Response.ClearHeaders();
        //    Response.ContentType = "application/pdf";
        //    Response.AddHeader("content-disposition", "attachment; filename= " + ReportName + ".pdf");
        //    // Response.AddHeader("content-disposition", "inline; filename= CustomerPayemntDetail");
        //    Response.AddHeader("content-length", stream.BaseStream.Length.ToString());
        //    Response.BinaryWrite(stream.ReadBytes(Convert.ToInt32(stream.BaseStream.Length)));
        //    Response.Flush();
        //    Response.Close();
        //}
        protected void BtnShowAll_Click(object sender, EventArgs e)
        {
            DataTable ldt = new DataTable();
            ldt = mobjPatientMaster.GetAllPatients();

            Session["AllPatientDetail"] = ldt;

            if (ldt != null && ldt.Rows.Count > 0)
            {
                dgvViewIPDPatient.PageIndex = 0;
                dgvViewIPDPatient.DataSource = ldt;
                dgvViewIPDPatient.DataBind();
                pnlShow.Style.Add(HtmlTextWriterStyle.Display, "");
                hdnPanel.Value = "";
                int lintRowcount = ldt.Rows.Count;
                lblRowCount.Text = "<b>Total Records:</b> " + lintRowcount.ToString();
                BtnDelete.Enabled = false;
            }
            else
            {
                pnlShow.Style.Add(HtmlTextWriterStyle.Display, "none");
                hdnPanel.Value = "none";
                Commons.ShowMessage("No Data to Display.", this.Page);
            }
        }

        protected void BtnDeleteOk_Click(object sender, EventArgs e)
        {
            List<EntityPatientMaster> lstentPatientMaster = new List<EntityPatientMaster>();
            EntityPatientMaster entPatientMaster = new EntityPatientMaster();
            int cnt = 0;

            try
            {
                foreach (GridViewRow drv in dgvViewIPDPatient.Rows)
                {
                    CheckBox chkDelete = (CheckBox)drv.FindControl("chkDelete");
                    if (chkDelete.Checked)
                    {
                        LinkButton lnkPatientCode = (LinkButton)drv.FindControl("lnkPatientId");
                        string lstrPatientCode = lnkPatientCode.Text;
                        entPatientMaster.PatientCode = lstrPatientCode;

                        entPatientMaster.Discontinued = true;
                        if (txtDeleteRemark.Text == string.Empty)
                        {
                            this.modalpopupDelete.Show();
                            lblMessage.Text = "Enter Delete Remarks";
                        }
                        else
                        {
                            entPatientMaster.DiscontRemark = txtDeleteRemark.Text.Trim();
                            lstentPatientMaster.Add(entPatientMaster);
                            cnt = mobjPatientMaster.DeletePatient(lstentPatientMaster);
                            if (cnt > 0)
                            {
                                Commons.ShowMessage("Record Deleted Successfully", this.Page);

                                if (dgvViewIPDPatient.Rows.Count <= 0)
                                {
                                    pnlShow.Style.Add(HtmlTextWriterStyle.Display, "none");
                                    hdnPanel.Value = "none";
                                }
                            }
                            else
                            {
                                Commons.ShowMessage("Record Not Deleted...", this.Page);
                            }
                        }
                    }
                }
            }
            catch (System.Threading.ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                Commons.FileLog("frmViewIPDPatient -   BtnDeleteOk_Click(object sender, EventArgs e)", ex);
            }
        }
        protected void chkDelete_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chk.NamingContainer;
            if (chk.Checked)
            {
                LinkButton PatientCode = (LinkButton)row.FindControl("lnkPatientId");
                Session["PatientCode"] = PatientCode.Text;
                lblMessage.Text = string.Empty;
                BtnDelete.Enabled = true;
            }
            else
            {
                Session["PatientCode"] = string.Empty;
                BtnDelete.Enabled = false;
            }
        }

    }
}