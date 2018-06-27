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
    public partial class frmBookAppointment : System.Web.UI.Page
    {
        BookAppoinmentBLL mobjAppointmnetBLL = new BookAppoinmentBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //SessionManager.Instance.SetSession();
                FillOPDCombo();
                FillConsultantCombo();
                GetBookedPatient();
                FillDepartmentCombo();
            }
        }

        private void FillOPDCombo()
        {
            DataTable ldt = new DataTable();
            ldt = mobjAppointmnetBLL.GetAllOPD();
            ddlOPD.DataSource = ldt;
            ddlOPD.DataTextField = "OPDDesc";
            ddlOPD.DataValueField = "PKId";
            ddlOPD.DataBind();
            ListItem li = new ListItem();
            li.Text = "--Select--";
            li.Value = "0";
            ddlOPD.Items.Insert(0, li);
        }

        private void FillDepartmentCombo()
        {
            DataTable ldt = new DataTable();
            ldt = mobjAppointmnetBLL.GetMedicalDepartments();
            ddlMedDepartment.DataSource = ldt;
            ddlMedDepartment.DataTextField = "MedDeptDesc";
            ddlMedDepartment.DataValueField = "PKId";
            ddlMedDepartment.DataBind();
            ListItem li = new ListItem();
            li.Text = "--Select--";
            li.Value = "0";
            ddlMedDepartment.Items.Insert(0, li);
        }

        private void FillConsultantCombo()
        {
            DataTable ldt = new DataTable();
            ldt = mobjAppointmnetBLL.GetConsultants();
            ddlConsultingDoctor.DataSource = ldt;
            ddlConsultingDoctor.DataTextField = "ConsultantName";
            ddlConsultingDoctor.DataValueField = "PKId";
            ddlConsultingDoctor.DataBind();
            ListItem li = new ListItem();
            li.Text = "--Select--";
            li.Value = "0";
            ddlConsultingDoctor.Items.Insert(0, li);
        }
        protected void btnGetInfo_Click(object sender, EventArgs e)
        {
            DataTable ldt = new DataTable();
            string lstrPatientCode = txtPatientCode.Text.Trim();
            if (!string.IsNullOrEmpty(txtPatientCode.Text.Trim()))
            {
                ldt = mobjAppointmnetBLL.GetPatientName(lstrPatientCode);
                if (ldt.Rows.Count > 0 && ldt != null)
                {
                    txtPatientName.Text = ldt.Rows[0]["PatientName"].ToString();
                }
            }
        }

        private void GetBookedPatient()
        {
            try
            {
                DataTable ldt = new DataTable();
                ldt = mobjAppointmnetBLL.GetAllAppoinment();
                if (ldt.Rows.Count > 0 && ldt != null)
                {
                    datalistPatient.DataSource = ldt;
                    datalistPatient.DataBind();
                    pnlShow.Style.Add(HtmlTextWriterStyle.Display, "");
                    hdnPanel.Value = "";
                }
                else
                {
                    pnlShow.Style.Add(HtmlTextWriterStyle.Display, "none");
                    hdnPanel.Value = "none";
                }
            }
            catch (Exception ex)
            {
                Commons.FileLog("GetBookedPatient()", ex);
            }
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtPatientCode.Text.Trim()))
                {
                    Commons.ShowMessage("Enter Patient Code...", this.Page);
                    return;
                }
                else
                {
                    if (string.IsNullOrEmpty(txtPatientName.Text.Trim()))
                    {
                        Commons.ShowMessage("Patient Name Should Not be Empty... ", this.Page);
                        return;
                    }
                    else
                    {
                        if (ddlMedDepartment.SelectedIndex == 0)
                        {
                            Commons.ShowMessage("Select Medical Department...", this.Page);
                            return;
                        }
                        else
                        {
                            if (ddlOPD.SelectedIndex == 0)
                            {
                                Commons.ShowMessage("Select OPD Room...", this.Page);
                                return;
                            }
                            else
                            {
                                if (ddlConsultingDoctor.SelectedIndex == 0)
                                {
                                    Commons.ShowMessage("Select Consulting Doctor...", this.Page);
                                    return;
                                }
                                else
                                {
                                    if (ddlVisitype.SelectedIndex == 0)
                                    {
                                        Commons.ShowMessage("Select Visit Type...", this.Page);
                                        return;
                                    }
                                    else
                                    {
                                        if (ddlShift.SelectedIndex == 0)
                                        {
                                            Commons.ShowMessage("Select Shift...", this.Page);
                                            return;
                                        }
                                        else
                                        {
                                            EntityBookAppoinment entBookAppoint = new EntityBookAppoinment();
                                            entBookAppoint.PatientCode = txtPatientCode.Text.Trim();
                                            entBookAppoint.FullName = txtPatientName.Text.Trim();
                                            entBookAppoint.DepartmentId = Commons.ConvertToInt(ddlMedDepartment.SelectedValue);
                                            entBookAppoint.ConsultingDr = Commons.ConvertToInt(ddlConsultingDoctor.SelectedValue);
                                            entBookAppoint.OPDRoom = Commons.ConvertToInt(ddlOPD.SelectedValue);
                                            entBookAppoint.Shift = Commons.ConvertToString(ddlShift.SelectedValue);
                                            entBookAppoint.VisitType = Commons.ConvertToString(ddlVisitype.SelectedValue);
                                            int cnt = mobjAppointmnetBLL.InsertAppoinment(entBookAppoint);
                                            if (cnt > 0)
                                            {
                                                Commons.ShowMessage("Appointment Booked...", this.Page);
                                                GetBookedPatient();
                                            }
                                            else
                                            {
                                                Commons.ShowMessage("Error While appointment booking...", this.Page);
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
                Commons.FileLog("BtnSave_Click(object sender, EventArgs e)", ex);
            }
        }

        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            int cnt = 0;
            foreach (DataListItem dls in datalistPatient.Items)
            {
                CheckBox chkDel = (CheckBox)dls.FindControl("chkDelete");
                if (chkDel.Checked)
                {
                    Label lblPKId = (Label)dls.FindControl("lblPKId");
                    cnt = mobjAppointmnetBLL.DeleteAppointment(Commons.ConvertToInt(lblPKId.Text));
                    if (cnt > 0)
                    {
                        Commons.ShowMessage("Appointment Deleted...", this.Page);

                    }
                    else
                    {
                        Commons.ShowMessage("Error While deleting appointment ...", this.Page);
                    }
                }
            }
            GetBookedPatient();
        }
    }
}