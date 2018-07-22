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
using System.Web.Script.Serialization;

namespace Hospital
{
    public partial class frmAllocConsultDoctorToPatient : BasePage
    {
        AllocConsultDoctorToPatientBLL MobjClaim = new AllocConsultDoctorToPatientBLL();
        JavaScriptSerializer serialize = new JavaScriptSerializer();
        protected void Page_Load(object sender, EventArgs e)
        {
            base.AuthenticateUser("frmAllocConsultDoctorToPatient.aspx");
            if (!Page.IsPostBack)
            {
                BindConsultDoctor();
                BindPatientList();
                Myflag.Value = string.Empty;
                BindAllocateDocToPatient();
                MultiView1.SetActiveView(View1);
            }
        }


        public void BindConsultDoctor()
        {
            try
            {
                List<EntityEmployee> tblCat = new EmployeeBLL().SelectAllEmployee().Where(p => p.DesignationId == SettingsManager.Instance.VisitingDoctorDesigId).ToList();
                tblCat.Insert(0, new EntityEmployee() { PKId = 0, FullName = "---Select---" });
                ddlConsultDoctor.DataSource = tblCat;
                ddlConsultDoctor.DataValueField = "PKId";
                ddlConsultDoctor.DataTextField = "FullName";
                ddlConsultDoctor.DataBind();
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
                List<EntityPatientMaster> tblCat = new BedStatusBLL().GetAllPatient();
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


        protected void ddlConsutDoctor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlConsultDoctor.SelectedIndex>0)
            {
                var EntityEmp = new EmployeeBLL().SelectAllEmployee().Where(p => p.PKId == Convert.ToInt32(ddlConsultDoctor.SelectedValue)).FirstOrDefault();
                txtConsultCharge.Text = Convert.ToString(EntityEmp.ConsultingCharges);
                hdnCategoryId.Value =Convert.ToString(EntityEmp.DoctorType);
                //txtCategory.Text = EntityEmp.OperationType;
            }
        }

        public void Clear()
        {
            ddlPatient.SelectedIndex = 0;
        }

        public void BindData()
        {
            List<EntityAllocaConDocDetails> lst = MobjClaim.GetDocForPatientAllocate(Convert.ToInt32(ddlPatient.SelectedValue));
            dgvChargeDetails.DataSource = lst;
            Charges.Value = serialize.Serialize(lst);
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
                        ListItem consultDoc = ddlConsultDoctor.Items.FindByText(row.Cells[3].Text);
                        ddlConsultDoctor.SelectedValue = consultDoc.Value;

                        txtConsultCharge.Text = row.Cells[4].Text;
                        txtConsultDate.Text = row.Cells[1].Text;
                    }
                    else
                    {
                        TempId.Value = Convert.ToString(dgvChargeDetails.DataKeys[row.RowIndex].Value);
                        ListItem consultDoc = ddlConsultDoctor.Items.FindByText(row.Cells[3].Text);
                        ddlConsultDoctor.SelectedValue = consultDoc.Value;

                        txtConsultCharge.Text = row.Cells[4].Text;
                        txtConsultDate.Text = row.Cells[1].Text;
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
            List<EntityAllocaConDocDetails> lst = serialize.Deserialize<List<EntityAllocaConDocDetails>>(Prescript.Value);
            List<EntityAllocaConDocDetails> lstFinal = new List<EntityAllocaConDocDetails>();
            if (BtnSave.Visible)
            {
                if (lst != null)
                {
                    foreach (EntityAllocaConDocDetails item in lst)
                    {
                        if (item.TempId != Convert.ToInt32(dgvChargeDetails.DataKeys[row.RowIndex].Value))
                        {
                            lstFinal.Add(item);
                        }
                    }
                    dgvChargeDetails.DataSource = lstFinal;
                    dgvChargeDetails.DataBind();
                    Prescript.Value = serialize.Serialize(lstFinal);
                }
            }
            else
            {
                foreach (EntityAllocaConDocDetails item in lst)
                {
                    if (item.TempId == Convert.ToInt32(dgvChargeDetails.DataKeys[row.RowIndex].Value))
                    {
                        item.IsDelete = true;
                    }
                }
                dgvChargeDetails.DataSource = lst.Where(p => p.IsDelete == false).ToList();
                dgvChargeDetails.DataBind();
                Prescript.Value = serialize.Serialize(lst);
            }
        }

        protected void btnAddCharge_Click(object sender, EventArgs e)
        {
            try
            {
                List<EntityAllocaConDocDetails> lst = null;
                if (Convert.ToString(Myflag.Value).Equals("Addnew", StringComparison.CurrentCultureIgnoreCase))
                {
                    lst = serialize.Deserialize<List<EntityAllocaConDocDetails>>(Charges.Value);
                }
                else
                {
                    lst = serialize.Deserialize<List<EntityAllocaConDocDetails>>(Prescript.Value);
                }
                lst.Add(new EntityAllocaConDocDetails
                {
                    PatientName = ddlPatient.SelectedItem.Text,
                    AdmitId = Convert.ToInt32(ddlPatient.SelectedValue),
                    TempId = lst.Count + 1
                });

                dgvChargeDetails.DataSource = lst;
                dgvChargeDetails.DataBind();
                Unit.Value =serialize.Serialize(lst);
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
            /*txtPrice.Text = string.Empty;
            txtQuantity.Text = string.Empty;*/
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            tblAllocConsultDoctor tblins = new tblAllocConsultDoctor();
            tblins.CategoryId = Convert.ToInt32(hdnCategoryId.Value);
            tblins.ConsultDocId = Convert.ToInt32(ddlConsultDoctor.SelectedValue);
            tblins.ConsultCharges = Convert.ToDecimal(txtConsultCharge.Text);
            tblins.Consult_Date = StringExtension.ToDateTime(txtConsultDate.Text);
            List<EntityAllocaConDocDetails> inslst =serialize.Deserialize<List<EntityAllocaConDocDetails>>(Charges.Value);
            int ClaimId = Convert.ToInt32(MobjClaim.Save(tblins, inslst));
            lblMessage.Text = "Record Saved Successfully.....";
            Charges.Value = null;
            Clear();
            inslst = new List<EntityAllocaConDocDetails>();
            dgvChargeDetails.DataSource = inslst;
            dgvChargeDetails.DataBind();
            lblMsg.Text = string.Empty;
            Charges.Value =serialize.Serialize(new List<EntityAllocaConDocDetails>());
            BindAllocateDocToPatient();
            MultiView1.SetActiveView(View1);
        }
        public void BindAllocateDocToPatient()
        {
            List<EntityAllocaConDoc> lst = MobjClaim.GetAllocatedPatientList();
            dgvClaim.DataSource = lst;
            int lintRowcount = lst.Count();
            lblRowCount.Text = "<b>Total Records:</b> " + lintRowcount.ToString();
            dgvClaim.DataBind();
        }

        public void BindPrescription(int Id)
        {
            try
            {
                List<EntityAllocaConDocDetails> lst = MobjClaim.GetDocForPatientAllocate(Id);
                dgvChargeDetails.DataSource = lst;
                dgvChargeDetails.DataBind();
                Prescript.Value =serialize.Serialize(lst);
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
                tblAllocConsultDoctor tblins = new tblAllocConsultDoctor();
                tblins.SrNo = Convert.ToInt32(PrescriptionId.Value);
                tblins.CategoryId = Convert.ToInt32(hdnCategoryId.Value);
                tblins.ConsultDocId = Convert.ToInt32(ddlConsultDoctor.SelectedValue);
                tblins.ConsultCharges = Convert.ToDecimal(txtConsultCharge.Text);
                tblins.Consult_Date = StringExtension.ToDateTime(txtConsultDate.Text);
                List<EntityAllocaConDocDetails> inslst =serialize.Deserialize<List<EntityAllocaConDocDetails>>(Prescript.Value);
                MobjClaim.Update(tblins, inslst);
                lblMessage.Text = "Record Updated Successfully.....";
                Clear();
                List<EntityAllocaConDocDetails> lst = new List<EntityAllocaConDocDetails>();
                dgvChargeDetails.DataSource = lst;
                dgvChargeDetails.DataBind();
                BindAllocateDocToPatient();
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
                List<EntityAllocaConDocDetails> lst =serialize.Deserialize<List<EntityAllocaConDocDetails>>(Charges.Value);
                foreach (EntityAllocaConDocDetails item in lst)
                {
                    if (Convert.ToInt32(TempId.Value) == item.TempId)
                    {
                        item.AdmitId = Convert.ToInt32(ddlPatient.SelectedValue);
                        item.IsDelete = false;
                    }
                    else
                    {
                        if (item.IsDelete)
                        {
                            lst.Add(new EntityAllocaConDocDetails()
                            {
                                SrDetailId = item.SrDetailId,
                                SrNo = item.SrNo,
                                IsDelete = item.IsDelete,
                                AdmitId = item.AdmitId,
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
                List<EntityAllocaConDocDetails> lst = serialize.Deserialize<List<EntityAllocaConDocDetails>>(Prescript.Value);
                foreach (EntityAllocaConDocDetails item in lst)
                {
                    if (Convert.ToInt32(TempId.Value) == item.TempId)
                    {
                        item.AdmitId = Convert.ToInt32(ddlPatient.SelectedValue);
                        item.IsDelete = false;
                    }
                    else
                    {
                        if (item.IsDelete)
                        {
                            lst.Add(new EntityAllocaConDocDetails()
                            {
                                SrDetailId = item.SrDetailId,
                                SrNo = item.SrNo,
                                IsDelete = item.IsDelete,
                                AdmitId = item.AdmitId,
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
            ListItem itemDoc = ddlConsultDoctor.Items.FindByText(Convert.ToString(row.Cells[3].Text));
            if (itemDoc != null)
            {
                hdnCategoryId.Value = Convert.ToString(row.Cells[2].Text);
                ddlConsultDoctor.SelectedValue = itemDoc.Value;
                DateTime MDate = StringExtension.ToDateTime(row.Cells[1].Text);
                txtConsultDate.Text = string.Format("{0:dd/MM/yyyy}", MDate);
                txtConsultCharge.Text = Convert.ToString(row.Cells[4].Text);
                tblAllocConsultDoctor objPresc = MobjClaim.GetPrescriptionInfo(Convert.ToInt32(PrescriptionId.Value));
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
                    List<EntityAllocaConDoc> lst = MobjClaim.GetInsurance(txtSearch.Text);
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
                BindAllocateDocToPatient();
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
                List<EntityAllocaConDoc> lst = new AllocConsultDoctorToPatientBLL().GetAllocatedPatientList();
                Response.Redirect("~/ExcelReport/MonthwiseSalExcel.aspx?ReportType=ConsultDoctor");
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void BtnAddNewPrescription_Click(object sender, EventArgs e)
        {
            Myflag.Value = "Addnew";
            txtConsultCharge.Text = Convert.ToString(0);
            txtConsultDate.Enabled = false;
            txtConsultDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            BtnSave.Visible = true;
            btnUpdate.Visible = false;
            btnAdd.Visible = true;
            btnUpdatecharge.Visible = false;
            List<EntityAllocaConDocDetails> lst = new List<EntityAllocaConDocDetails>();
            dgvChargeDetails.DataSource = lst;
            dgvChargeDetails.DataBind();
            Charges.Value =serialize.Serialize(lst);
            MultiView1.SetActiveView(View2);
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
                List<EntityAllocaConDoc> lst = MobjClaim.GetAllocatedPatientList();
                if (!string.IsNullOrEmpty(txtSearch.Text))
                {
                    lst = lst.Where(p=>p.ConsultName.Contains(txtSearch.Text)).ToList();
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
        protected void dgvChargeDetails_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}