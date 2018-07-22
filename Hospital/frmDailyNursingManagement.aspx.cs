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
using System.Web.Script.Serialization;
using Hospital.Models;

namespace Hospital
{
    public partial class frmDailyNursingManagement : BasePage
    {
        NursingManagementBLL MobjClaim = new NursingManagementBLL();
        JavaScriptSerializer serialize = new JavaScriptSerializer();
        protected void Page_Load(object sender, EventArgs e)
        {
            base.AuthenticateUser("frmDailyNursingManagement.aspx");
            if (!Page.IsPostBack)
            {
                BindNurse();
                BindPatientList();
                BindCategory();
                Myflag.Value = string.Empty;
                BindDailyNursingManagement();
                MultiView1.SetActiveView(View1);
            }
        }

        private void BindNurse()
        {
            try
            {
                List<EntityEmployee> tblCat = new EmployeeBLL().SelectAllEmployee().Where(p => p.DesignationId == 2).ToList();
                tblCat.Insert(0, new EntityEmployee() {PKId=0,FullName="---Select---" });
                ddlNurseName.DataSource = tblCat;
                ddlNurseName.DataValueField = "PKId";
                ddlNurseName.DataTextField = "FullName";
                ddlNurseName.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        private void BindCategory()
        {
            try
            {
                DataTable tblCat = new RoomMasterBLL().GetAllCategory();
                DataRow dr = tblCat.NewRow();
                dr["PKId"] = 0;
                dr["CategoryDesc"] = "---Select---";
                tblCat.Rows.InsertAt(dr, 0);

                ddlWardCategory.DataSource = tblCat;
                ddlWardCategory.DataValueField = "PKId";
                ddlWardCategory.DataTextField = "CategoryDesc";
                ddlWardCategory.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        private void BindPatientList()
        {
            try
            {
                List<EntityPatientMaster> tblCat = new OPDPatientMasterBLL().GetPatientList().Where(p => p.IsDischarged == false && p.PatientType.ToUpper() == "IPD").ToList();
                if (tblCat != null)
                {
                    tblCat.Insert(0, new EntityPatientMaster() { AdmitId = 0, FullName = "---Select---" });
                    ddlPatient.DataSource = tblCat;
                    ddlPatient.DataValueField = "AdmitId";
                    ddlPatient.DataTextField = "FullName";
                    ddlPatient.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void ddlPatient_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPatient.SelectedIndex > 0)
            {
                EntityPatientAlloc objTxt = new PatientAllocDocBLL().GetPatientType(Convert.ToInt32(ddlPatient.SelectedValue));
                CalDate.StartDate = objTxt.AdmitDate;
            }
        }

        

        public void Clear()
        {
            // ddlPatient.SelectedIndex = 0;
            txtInjectableMedi.Text = string.Empty;
            txtInfusions.Text = string.Empty;
            txtOral.Text = string.Empty;
            txtNursingCare.Text = string.Empty;

        }

        public void BindData()
        {
            List<EntityNursingManagementDetails> lst = MobjClaim.GetDocForPatientAllocate(Convert.ToInt32(ddlPatient.SelectedValue));
            dgvChargeDetails.DataSource = lst;
            dgvChargeDetails.DataBind();
        }

        protected void BtnEditCharge_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton imgEdit = (ImageButton)sender;
                GridViewRow row = (GridViewRow)imgEdit.NamingContainer;
                if (row != null)
                {
                    btnUpdatecharge.Visible = true;
                    btnAdd.Visible = false;
                    if (Convert.ToString(Myflag.Value) == "Addnew")
                    {
                        TempId.Value = Convert.ToString(dgvChargeDetails.DataKeys[row.RowIndex].Value);
                        ListItem itemPatient = ddlPatient.Items.FindByText(row.Cells[0].Text);
                        ddlPatient.SelectedValue = itemPatient.Value;
                        txtInjectableMedi.Text = row.Cells[2].Text;
                        txtInfusions.Text = row.Cells[3].Text;
                        txtOral.Text = row.Cells[4].Text;
                        txtNursingCare.Text = row.Cells[5].Text;
                    }
                    else
                    {
                        TempId.Value = Convert.ToString(dgvChargeDetails.DataKeys[row.RowIndex].Value);
                        ListItem itemPatient = ddlPatient.Items.FindByText(row.Cells[0].Text);
                        ddlPatient.SelectedValue = itemPatient.Value;
                        txtInjectableMedi.Text = row.Cells[2].Text;
                        txtInfusions.Text = row.Cells[3].Text;
                        txtOral.Text = row.Cells[4].Text;
                        txtNursingCare.Text = row.Cells[5].Text;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ImageButton imgDelete = (ImageButton)sender;
            GridViewRow row = (GridViewRow)imgDelete.NamingContainer;
            List<EntityNursingManagementDetails> lst = serialize.Deserialize<List<EntityNursingManagementDetails>>(Prescript.Value);
            List<EntityNursingManagementDetails> lstFinal = new List<EntityNursingManagementDetails>();
            if (BtnSave.Visible)
            {
                if (lst != null)
                {
                    foreach (EntityNursingManagementDetails item in lst)
                    {
                        if (item.TempId != Convert.ToInt32(dgvChargeDetails.DataKeys[row.RowIndex].Value))
                        {
                            lstFinal.Add(item);
                        }
                    }
                    dgvChargeDetails.DataSource = lstFinal;
                    dgvChargeDetails.DataBind();
                    Prescript.Value =serialize.Serialize(lstFinal);
                }
            }
            else
            {
                foreach (EntityNursingManagementDetails item in lst)
                {
                    if (item.TempId == Convert.ToInt32(dgvChargeDetails.DataKeys[row.RowIndex].Value))
                    {
                        item.IsDelete = true;
                    }
                }
                dgvChargeDetails.DataSource = lst.Where(p => p.IsDelete == false).ToList();
                dgvChargeDetails.DataBind();
                Prescript.Value = serialize.Serialize(lstFinal);
            }
        }

        protected void btnAddCharge_Click(object sender, EventArgs e)
        {
            try
            {
                TimeSpan objTime = new TimeSpan(TreatmentTimeSelector.Hour, TreatmentTimeSelector.Minute, 0);
                List<EntityNursingManagementDetails> lst = null;
                if (Convert.ToString(Myflag.Value).Equals("Addnew", StringComparison.CurrentCultureIgnoreCase))
                {
                    lst = serialize.Deserialize<List<EntityNursingManagementDetails>>(Charges.Value);
                }
                else
                {
                    lst = serialize.Deserialize<List<EntityNursingManagementDetails>>(Prescript.Value);
                }
                lst.Add(new EntityNursingManagementDetails
                {

                    PatientName = ddlPatient.SelectedItem.Text,
                    PatientId = Convert.ToInt32(ddlPatient.SelectedValue),
                    InjectableMedications = Convert.ToString(txtInjectableMedi.Text),
                    Infusions = Convert.ToString(txtInfusions.Text),
                    Oral = Convert.ToString(txtOral.Text),
                    NursingCare = Convert.ToString(txtNursingCare.Text),
                    TreatmentTime = Convert.ToString(objTime),
                    NurseId = Convert.ToInt32(ddlNurseName.SelectedValue),
                    NurseName = Convert.ToString(ddlNurseName.SelectedItem.Text),
                    CategoryId = Convert.ToInt32(ddlWardCategory.SelectedValue),
                    CategoryDesc = Convert.ToString(ddlWardCategory.SelectedItem.Text),
                    Department = Convert.ToString(txtDepartment.Text),
                    TreatmentDate = StringExtension.ToDateTime(txtTreatmentDate.Text),
                    TempId = lst.Count + 1
                });

                dgvChargeDetails.DataSource = lst;
                dgvChargeDetails.DataBind();
                /*txtTotal.Text = Convert.ToString(lst.Sum(p => p.Amount));*/
                if (Convert.ToString(Myflag.Value).Equals("Addnew", StringComparison.CurrentCultureIgnoreCase))
                {
                    Charges.Value = serialize.Serialize(lst);
                }
                else
                {
                    Prescript.Value = serialize.Serialize(lst);
                }
                Clear();
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ddlPatient.SelectedIndex = 0;
            txtInjectableMedi.Text = string.Empty;
            txtInfusions.Text = string.Empty;
            txtOral.Text = string.Empty;
            txtNursingCare.Text = string.Empty;
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            tblNursingManagement tblins = new tblNursingManagement();
            tblins.CategoryId = Convert.ToInt32(ddlWardCategory.SelectedValue);
            tblins.NurseId = Convert.ToInt32(ddlNurseName.SelectedValue);
            tblins.Department = Convert.ToString(txtDepartment.Text);
            tblins.TreatmentDate = StringExtension.ToDateTime(txtTreatmentDate.Text);

            List<EntityNursingManagementDetails> inslst = serialize.Deserialize<List<EntityNursingManagementDetails>>(Charges.Value);
            int ClaimId = Convert.ToInt32(MobjClaim.Save(tblins, inslst));
            lblMessage.Text = "Record Saved Successfully.....";
            Charges.Value = null;
            Clear();
            inslst = new List<EntityNursingManagementDetails>();
            dgvChargeDetails.DataSource = inslst;
            dgvChargeDetails.DataBind();
            lblMsg.Text = string.Empty;

            Charges.Value =serialize.Serialize(new List<EntityNursingManagementDetails>());
            BindDailyNursingManagement();
            MultiView1.SetActiveView(View1);
        }
        public void BindDailyNursingManagement()
        {
            List<EntityNursingManagement> lst = MobjClaim.GetAllocatedPatientList();
            dgvClaim.DataSource = lst;
            int lintRowcount = lst.Count();
            lblRowCount.Text = "<b>Total Records:</b> " + lintRowcount.ToString();
            dgvClaim.DataBind();
        }

        public void BindPrescription(int Id)
        {
            try
            {
                List<EntityNursingManagementDetails> lst = MobjClaim.GetDocForPatientAllocate(Id);
                dgvChargeDetails.DataSource = lst;
                dgvChargeDetails.DataBind();
                Prescript.Value = serialize.Serialize(lst);
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }
        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                tblNursingManagement tblins = new tblNursingManagement();
                tblins.SrNo = Convert.ToInt32(PrescriptionId.Value);
                tblins.CategoryId = Convert.ToInt32(ddlWardCategory.SelectedValue);
                tblins.NurseId = Convert.ToInt32(ddlNurseName.SelectedValue);
                tblins.Department = Convert.ToString(txtDepartment.Text);
                tblins.TreatmentDate = StringExtension.ToDateTime(txtTreatmentDate.Text);
                List<EntityNursingManagementDetails> inslst = (List<EntityNursingManagementDetails>)Session["Prescript"];
                MobjClaim.Update(tblins, inslst);
                lblMessage.Text = "Record Updated Successfully.....";
                Clear();
                List<EntityNursingManagementDetails> lst = new List<EntityNursingManagementDetails>();
                dgvChargeDetails.DataSource = lst;
                dgvChargeDetails.DataBind();
                BindDailyNursingManagement();
                Charges.Value = null;
                MultiView1.SetActiveView(View1);
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void btnUpdatecharge_Click(object sender, EventArgs e)
        {
            if (Convert.ToString(Myflag.Value) == "Addnew")
            {
                List<EntityNursingManagementDetails> lst = (List<EntityNursingManagementDetails>)Session["Charges"];
                foreach (EntityNursingManagementDetails item in lst)
                {
                    if (Convert.ToInt32(TempId.Value) == item.TempId)
                    {
                        TimeSpan objTime = new TimeSpan(TreatmentTimeSelector.Hour, TreatmentTimeSelector.Minute, 0);
                        item.PatientId = Convert.ToInt32(ddlPatient.SelectedValue);
                        item.InjectableMedications = Convert.ToString(txtInjectableMedi.Text);
                        item.Infusions = Convert.ToString(txtInfusions.Text);
                        item.Oral = Convert.ToString(txtOral.Text);
                        item.NursingCare = Convert.ToString(txtNursingCare.Text);
                        item.TreatmentTime = Convert.ToString(objTime);
                        item.IsDelete = false;
                    }
                    else
                    {
                        if (item.IsDelete)
                        {
                            lst.Add(new EntityNursingManagementDetails()
                            {
                                SrDetailId = item.SrDetailId,
                                SrNo = item.SrNo,
                                IsDelete = item.IsDelete,
                                PatientId = item.PatientId,
                                InjectableMedications = item.InjectableMedications,
                                Infusions = item.Infusions,
                                Oral = item.Oral,
                                NursingCare = item.NursingCare,
                                TreatmentTime = item.TreatmentTime,
                                NurseId = item.NurseId,
                                CategoryId = item.CategoryId,
                                Department = item.Department,
                                TreatmentDate = item.TreatmentDate
                            });
                        }
                    }
                }


                dgvChargeDetails.DataSource = lst;
                dgvChargeDetails.DataBind();
                Clear();
                btnUpdatecharge.Visible = false;
                btnAdd.Visible = true;
            }
            else
            {
                List<EntityNursingManagementDetails> lst = serialize.Deserialize<List<EntityNursingManagementDetails>>(Prescript.Value);
                foreach (EntityNursingManagementDetails item in lst)
                {
                    if (Convert.ToInt32(TempId.Value) == item.TempId)
                    {
                        TimeSpan objTime = new TimeSpan(TreatmentTimeSelector.Hour, TreatmentTimeSelector.Minute, 0);
                        item.PatientId = Convert.ToInt32(ddlPatient.SelectedValue);
                        item.InjectableMedications = Convert.ToString(txtInjectableMedi.Text);
                        item.Infusions = Convert.ToString(txtInfusions.Text);
                        item.Oral = Convert.ToString(txtOral.Text);
                        item.NursingCare = Convert.ToString(txtNursingCare.Text);
                        item.TreatmentTime = Convert.ToString(objTime);
                        item.IsDelete = false;
                    }
                    else
                    {
                        if (item.IsDelete)
                        {
                            lst.Add(new EntityNursingManagementDetails()
                            {
                                SrDetailId = item.SrDetailId,
                                SrNo = item.SrNo,
                                IsDelete = item.IsDelete,
                                PatientId = item.PatientId,
                                InjectableMedications = item.InjectableMedications,
                                Infusions = item.Infusions,
                                Oral = item.Oral,
                                NursingCare = item.NursingCare,
                                TreatmentTime = item.TreatmentTime,
                                NurseId = item.NurseId,
                                CategoryId = item.CategoryId,
                                Department = item.Department,
                                TreatmentDate = item.TreatmentDate
                            });
                        }
                    }
                }


                dgvChargeDetails.DataSource = lst;
                dgvChargeDetails.DataBind();
                Clear();
                btnUpdatecharge.Visible = false;
                btnAdd.Visible = true;
            }
        }

        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            btnAdd.Visible = true;
            btnUpdatecharge.Visible = false;
            BtnSave.Visible = false;
            btnUpdate.Visible = true;
            Myflag.Value = "Edit";
            ImageButton imgEdit = (ImageButton)sender;
            GridViewRow row = (GridViewRow)imgEdit.NamingContainer;
            PrescriptionId.Value = Convert.ToString(dgvClaim.DataKeys[row.RowIndex].Value);
            ListItem item = ddlNurseName.Items.FindByText(Convert.ToString(row.Cells[2].Text));
            ListItem itemDoc = ddlWardCategory.Items.FindByText(Convert.ToString(row.Cells[4].Text));
            if (item != null && itemDoc != null)
            {
                ddlNurseName.SelectedValue = item.Value;
                ddlWardCategory.SelectedValue = itemDoc.Value;
                DateTime MDate = StringExtension.ToDateTime(row.Cells[1].Text);
                txtTreatmentDate.Text = string.Format("{0:dd/MM/yyyy}", MDate);
                txtDepartment.Text = Convert.ToString(row.Cells[3].Text);
                string dept = txtDepartment.Text;
                List<EntityPatientAdmit> lst = MobjClaim.GetPatientList(dept);
                ddlPatient.DataSource = lst;
                lst.Insert(0, new EntityPatientAdmit() { AdmitId = 0, PatientFirstName = "--Select--" });
                ddlPatient.DataValueField = "AdmitId";
                ddlPatient.DataTextField = "PatientFirstName";
                ddlPatient.DataBind();
                tblNursingManagement objPresc = MobjClaim.GetPrescriptionInfo(Convert.ToInt32(Session["PrescriptionId"]));
                BindPrescription(Convert.ToInt32(PrescriptionId.Value));
                //InjectionPara(false);
                MultiView1.SetActiveView(View2);
            }
            else
            {
                lblMessage.Text = "Category Name Not Found";
            }
        }

        protected void BtnClose_Click(object sender, EventArgs e)
        {
            Clear();
            MultiView1.SetActiveView(View1);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSearch.Text))
                {
                    List<EntityNursingManagement> lst = MobjClaim.GetInsurance(txtSearch.Text);
                    if (lst.Count > 0)
                    {
                        dgvClaim.DataSource = lst;
                        dgvClaim.DataBind();
                        int lintRowcount = lst.Count;
                        lblRowCount.Text = "<b>Total Records:</b> " + lintRowcount.ToString();
                    }
                    else
                    {
                        lblMessage.Text = "No Record Found";
                    }
                }
                else
                {
                    lblMessage.Text = "Please Enter Content To Search";
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                BindDailyNursingManagement();
                txtSearch.Text = string.Empty;
                lblMessage.Text = string.Empty;
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                string url = "~/ExcelReport/MonthwiseSalExcel.aspx?ReportType=NursingManagment";
                //Session["Details"] = ListConverter.ToDataTable((List<EntityNursingManagement>)Session["PrescriptDetails"]);
                Response.Redirect(url,false);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void BtnAddNewPrescription_Click(object sender, EventArgs e)
        {
            Myflag.Value = "Addnew";
            //ddlPatient.Enabled = true;
            //ddlPatient.SelectedIndex = 0;
            ddlNurseName.SelectedIndex = 0;
            ddlWardCategory.SelectedIndex = 0;
            txtDepartment.Text = string.Empty;
            txtTreatmentDate.Enabled = false;
            txtTreatmentDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            BtnSave.Visible = true;
            btnUpdate.Visible = false;
            btnAdd.Visible = true;
            btnUpdatecharge.Visible = false;
            List<EntityNursingManagementDetails> lst = new List<EntityNursingManagementDetails>();
            dgvChargeDetails.DataSource = lst;
            dgvChargeDetails.DataBind();
            Charges.Value =  serialize.Serialize(lst);
            MultiView1.SetActiveView(View2);
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton imgEdit = (ImageButton)sender;
                GridViewRow cnt = (GridViewRow)imgEdit.NamingContainer;
                Sr_No.Value = cnt.Cells[0].Text;
                if (Convert.ToInt32(Sr_No.Value) > 0)
                {
                    Response.Redirect("~/PathalogyReport/PathologyReport.aspx?ReportType=NursingFollowupSheet&Sr_No=" + Sr_No.Value, false);
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void dgvClaim_DataBound(object sender, EventArgs e)
        {
            int lintCurrentPage = dgvClaim.PageIndex + 1;
            lblPageCount.Text = "<b>Page</b> " + lintCurrentPage.ToString() + "<b> of </b>" + dgvClaim.PageCount.ToString();
        }
        protected void dgvClaim_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                List<EntityNursingManagement> lst = MobjClaim.GetAllocatedPatientList();
                if (!string.IsNullOrEmpty(txtSearch.Text))
                {
                    lst = lst.Where(p => p.NurseName.Contains(txtSearch.Text)).ToList();
                }
                dgvClaim.DataSource = lst;
                dgvClaim.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
        protected void dgvClaim_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvClaim.PageIndex = e.NewPageIndex;
        }
    }
}