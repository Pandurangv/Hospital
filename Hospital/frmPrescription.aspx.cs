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
    public partial class frmPrescription : BasePage
    {
        PrescriptionBLL MobjClaim = new PrescriptionBLL();
        JavaScriptSerializer serialize = new JavaScriptSerializer();
        OPDPatientMasterBLL mobjPatientMasterBLL = new OPDPatientMasterBLL();
        PatientInvoiceBLL objpatientInvoice = new PatientInvoiceBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            base.AuthenticateUser();
            if (!Page.IsPostBack)
            {
                BindTablet();
                BindOtherCharge();
                GetDoctortList();
                Myflag.Value = string.Empty;
                BindPrescription();
                MultiView1.SetActiveView(View1);
            }
        }

        public void BindTablet()
        {
            try
            {
                IssueMaterialBLL mobjDeptBLL = new IssueMaterialBLL();
                List<EntityProduct> lstPat = mobjDeptBLL.GetProductList();
                ddlTablet.DataSource = lstPat;
                lstPat.Insert(0, new EntityProduct() { ProductId = 0, ProductName = "--Select--" });
                ddlTablet.DataValueField = "ProductId";
                ddlTablet.DataTextField = "ProductName";
                ddlTablet.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        private void GetDoctortList()
        {
            try
            {
                var tblCat = new PatientAllocDocBLL().GetAllDoctor();
                if (SessionManager.Instance.LoginUser!=null)
                {
                    if (SessionManager.Instance.LoginUser.UserType == "Doctor")
                    {
                        tblCat = tblCat.Where(p => p.PKId == SessionManager.Instance.LoginUser.PKId).ToList();
                    }
                }

                tblCat.Insert(0, new sp_GetAllDoctorListResult() { FullName = "----Select------", PKId = 0 });

                ddlDoctors.DataSource = tblCat;
                ddlDoctors.DataValueField = "PKId";
                ddlDoctors.DataTextField = "FullName";
                ddlDoctors.DataBind();
                if (SessionManager.Instance.LoginUser!=null)
                {
                    if (SessionManager.Instance.LoginUser.UserType == "Doctor")
                    {
                        ListItem item = ddlDoctors.Items.FindByValue(Convert.ToString(SessionManager.Instance.LoginUser.PKId));
                        ddlDoctors.SelectedValue = item.Value;
                        ddlDoctors_SelectedIndexChanged(ddlDoctors, null);
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void BtnAdd_Prescription(object sender, EventArgs e)
        {
            ImageButton imgEdit = (ImageButton)sender;
            GridViewRow row = (GridViewRow)imgEdit.NamingContainer;
            if (row != null)
            {
                int admitid= Convert.ToInt32(dgvPatientlist.DataKeys[row.RowIndex].Value);
                EntityPatientMaster patient=mobjPatientMasterBLL.GetPatientList().Where(p => p.IsDischarged == false && p.AdmitId == admitid).FirstOrDefault();
                ListItem item = ddlDoctors.Items.FindByValue(Convert.ToString(patient.DeptDoctorId));
                if (item!=null)
                {
                    BtnAddNewPrescription_Click(sender, e);
                    ddlDoctors.SelectedValue = item.Value;
                    ddlDoctors_SelectedIndexChanged(sender, e);
                    item = ddlPatient.Items.FindByValue(Convert.ToString(patient.AdmitId));
                    if (item != null)
                    {
                        ddlPatient.SelectedValue = item.Value;
                        ddlPatient_SelectedIndexChanged(ddlPatient, e);
                        txtFollowupDate.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Now.Date.AddMonths(1));
                    }
                }
            }
        }

        protected void ddlPatient_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPatient.SelectedIndex > 0)
            {
                EntityPatientAlloc objTxt = new PatientAllocDocBLL().GetPatientType(Convert.ToInt32(ddlPatient.SelectedValue));
                CalDate.StartDate = objTxt.AdmitDate;
                List<EntityInvoiceDetails> lst = new List<EntityInvoiceDetails>();
                List<EntityChargeCategory> lstCat = new ChargeCategoryBLL().GetChargeDetail().Where(p=>p.IsConsulting==true).ToList();
                foreach (var item in lstCat)
                {
                    if (item.IsConsulting)
                    {
                        EntityPatientMaster patient = mobjPatientMasterBLL.GetPatientList().Where(p => p.IsDischarged == false && p.AdmitId == Convert.ToInt32(ddlPatient.SelectedValue)).FirstOrDefault();
                        if (patient != null)
                        {

                            EntityInvoiceDetails entInv = new EntityInvoiceDetails() { DocAllocationId = Convert.ToInt32(ddlPatient.SelectedValue), Amount = patient.ConsultingCharges == null ? 0 : patient.ConsultingCharges.Value, ChargesName = item.ChargeCategoryName, OtherChargesId = item.ChargesId };
                            entInv.NoOfDays = 1;
                            entInv.Quantity = 1;
                            entInv.IsDelete = false;
                            entInv.Remarks = string.Empty;
                            entInv.PerDayCharge = Convert.ToDecimal(item.Charges);
                            entInv.Amount = Convert.ToDecimal(item.Charges);
                            entInv.TempId = lst.Count + 1;
                            lst.Add(entInv);
                        }
                    }
                }
                invDtl.Value = serialize.Serialize(lst);
                dgvChargesOPD.DataSource = lst;
                dgvChargesOPD.DataBind();
            }
        }

        protected void ddlDoctors_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDoctors.SelectedIndex > 0 || SessionManager.Instance.LoginUser != null || SessionManager.Instance.LoginUser.UserType == "Doctor")
            {
                int doctid = ddlDoctors.SelectedIndex > 0 ? Convert.ToInt32(ddlDoctors.SelectedValue) : SessionManager.Instance.LoginUser.PKId;
                List<EntityPatientMaster> ldtRequisition = mobjPatientMasterBLL.GetPatientList().Where(p => p.IsDischarged == false && p.DeptDoctorId == doctid && p.PatientType.ToUpper()=="OPD").ToList();
                List<EntityPatientMaster> lst = new List<EntityPatientMaster>();

                foreach (var item in ldtRequisition)
                {
                    var objcheck= MobjClaim.GetPrescriptions().Where(p=>p.AdmitId==item.AdmitId).FirstOrDefault();
                    if (objcheck==null)
                    {
                        lst.Add(item);
                    }
                }
                dgvClaim.Width = 820;
                dgvPatientlist.Width = 200;
                if (SessionManager.Instance.LoginUser != null)
                {
                    if (SessionManager.Instance.LoginUser.UserType != "Doctor")
                    {
                        dgvClaim.Width = 1020;
                        dgvPatientlist.Width = 0;
                    }
                    else if (SessionManager.Instance.LoginUser.UserType == "Doctor" && lst.Count==0)
                    {
                        dgvClaim.Width = 1020;
                        dgvPatientlist.Width = 0;
                    }
                }
                dgvPatientlist.DataSource = lst;
                dgvPatientlist.DataBind();
                lst.Insert(0, new EntityPatientMaster() { AdmitId = 0, FullName = "----Select----" });
                ddlPatient.DataSource = lst;
                ddlPatient.DataTextField = "FullName";
                ddlPatient.DataValueField = "AdmitId";
                ddlPatient.DataBind();
            }
            else
            {
                dgvClaim.Width = 1020;
                dgvPatientlist.Width = 0;
            }
        }

        public void Clear(bool isClose=false)
        {
            ddlTablet.SelectedIndex = 0;
            txtMorning.Text = string.Empty;
            txtafternoon.Text = string.Empty;
            txtNight.Text = string.Empty;
            txtNoOfDays.Text = string.Empty;
            txtQuantity.Text = string.Empty;
            txtNoDays.Text = string.Empty;
            txtQty.Text = string.Empty;
            txtAmount.Text = string.Empty;
            txtChargePerDay.Text = string.Empty;
            ddlOther.SelectedIndex = 0;
            if (isClose)
            {
                ddlPatient.SelectedIndex = 0;
                ddlPatient_SelectedIndexChanged(ddlPatient, null);
                //Session["PrescriptDetails"] = new List<EntityPrescription>();
                invDtl.Value = serialize.Serialize(new List<EntityInvoiceDetails>());
                txtDiscount.Text = string.Empty;
                txtTotalAmount.Text = string.Empty;
                txtRemarks.Text = string.Empty;
                txtPrescriptionDate.Text = string.Empty;
                txtFollowupDate.Text = string.Empty;
                txtAdviceNote.Text = string.Empty;
                txtAdviceNote.Text = string.Empty;
                txtInvestigation.Text = string.Empty;
                dgvChargesOPD.DataSource = new List<EntityInvoiceDetails>();
                dgvPatientlist.Width = 200;
                dgvClaim.Width = 820;
                if (SessionManager.Instance.LoginUser!=null)
                {
                    if (SessionManager.Instance.LoginUser.UserType != "Doctor")
                    {
                        dgvPatientlist.Width = 0;
                        dgvClaim.Width = 1020;
                        ddlDoctors.SelectedIndex = 0;
                    }
                    else if (SessionManager.Instance.LoginUser.UserType == "Doctor" && dgvPatientlist.Rows.Count == 0)
                    {
                        dgvClaim.Width = 1020;
                        dgvPatientlist.Width = 0;
                    }
                }
            }
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
                        ListItem itemProduct = ddlTablet.Items.FindByText(row.Cells[0].Text);
                        ddlTablet.SelectedValue = itemProduct.Value;
                        txtMorning.Text = row.Cells[1].Text;
                        txtafternoon.Text = row.Cells[2].Text;
                        txtNight.Text = row.Cells[3].Text;
                        txtNoOfDays.Text = row.Cells[4].Text;
                        txtQuantity.Text = row.Cells[5].Text;
                        if (row.Cells[6].Text == "True")
                        {
                            chkDress.Checked = true;
                        }
                        else
                        {
                            chkDress.Checked = false;
                        }
                        if (row.Cells[7].Text == "True")
                        {
                            chkInject.Checked = true;
                            txtInjection.Visible = true;
                            txtInjection.Text = row.Cells[8].Text;
                        }
                        else
                        {
                            chkInject.Checked = false;
                        }
                        //txtInjection.Text = row.Cells[11].Text;
                    }
                    else
                    {
                        TempId.Value = Convert.ToString(dgvChargeDetails.DataKeys[row.RowIndex].Value);
                        ListItem itemProduct = ddlTablet.Items.FindByText(row.Cells[0].Text);
                        ddlTablet.SelectedValue = itemProduct.Value;
                        txtMorning.Text = row.Cells[1].Text;
                        txtafternoon.Text = row.Cells[2].Text;
                        txtNight.Text = row.Cells[3].Text;
                        txtNoOfDays.Text = row.Cells[4].Text;
                        txtQuantity.Text = row.Cells[5].Text;
                        if (row.Cells[6].Text == "True")
                        {
                            chkDress.Checked = true;
                        }
                        else
                        {
                            chkDress.Checked = false;
                        }
                        if (row.Cells[7].Text == "True")
                        {
                            chkInject.Checked = true;
                            txtInjection.Visible = true;
                            txtInjection.Text = row.Cells[8].Text;
                        }
                        else
                        {
                            chkInject.Checked = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnDeleteCharge_Click(object sender, EventArgs e)
        {
            ImageButton imgDelete = (ImageButton)sender;
            GridViewRow row = (GridViewRow)imgDelete.NamingContainer;
            List<EntityInvoiceDetails> lst = serialize.Deserialize<List<EntityInvoiceDetails>>(invDtl.Value);
            List<EntityInvoiceDetails> lstFinal = new List<EntityInvoiceDetails>();
            if (BtnSave.Visible)
            {
                if (lst != null)
                {
                    foreach (EntityInvoiceDetails item in lst)
                    {
                        if (item.TempId != Convert.ToInt32(dgvChargesOPD.DataKeys[row.RowIndex].Value))
                        {
                            lstFinal.Add(item);
                        }
                    }
                    invDtl.Value = serialize.Serialize(lstFinal);
                    dgvChargesOPD.DataSource = lstFinal;
                    dgvChargesOPD.DataBind();
                }
            }
            else
            {
                foreach (EntityInvoiceDetails item in lst)
                {
                    if (item.TempId == Convert.ToInt32(dgvChargesOPD.DataKeys[row.RowIndex].Value))
                    {
                        item.IsDelete = true;
                    }
                }
                invDtl.Value = serialize.Serialize(lstFinal);
                dgvChargesOPD.DataSource = lst.Where(p => p.IsDelete == false).ToList();
                dgvChargesOPD.DataBind();
            }
            txtTotalAmount.Text = string.Format("{0:00", lstFinal.Sum(p => p.Amount));
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ImageButton imgDelete = (ImageButton)sender;
            GridViewRow row = (GridViewRow)imgDelete.NamingContainer;
            List<EntityPrescriptionDetails> lst = serialize.Deserialize<List<EntityPrescriptionDetails>>(Prescript.Value);
            List<EntityPrescriptionDetails> lstFinal = new List<EntityPrescriptionDetails>();
            if (BtnSave.Visible)
            {
                if (lst != null)
                {
                    foreach (EntityPrescriptionDetails item in lst)
                    {
                        if (item.TempId != Convert.ToInt32(dgvChargeDetails.DataKeys[row.RowIndex].Value))
                        {
                            lstFinal.Add(item);
                        }
                    }
                    Prescript.Value = serialize.Serialize(lstFinal);
                    dgvChargeDetails.DataSource = lstFinal;
                    dgvChargeDetails.DataBind();
                }
            }
            else
            {
                foreach (EntityPrescriptionDetails item in lst)
                {
                    if (item.TempId == Convert.ToInt32(dgvChargeDetails.DataKeys[row.RowIndex].Value))
                    {
                        item.IsDelete = true;
                    }
                }
                Prescript.Value = serialize.Serialize(lstFinal);
                dgvChargeDetails.DataSource = lst.Where(p => p.IsDelete == false).ToList();
                dgvChargeDetails.DataBind();
            }
        }

        protected void btnAddCharge_Click(object sender, EventArgs e)
        {
            try
            {
                List<EntityPrescriptionDetails> lst = null;
                if (Convert.ToString(Myflag.Value).Equals("Addnew", StringComparison.CurrentCultureIgnoreCase))
                {
                    lst =serialize.Deserialize<List<EntityPrescriptionDetails>>(Prescript.Value);
                }
                else
                {
                    lst = serialize.Deserialize<List<EntityPrescriptionDetails>>(Prescript.Value);
                }
                if (lst==null)
                {
                    lst = new List<EntityPrescriptionDetails>();
                }
                if (ddlTablet.SelectedIndex>0)
                {
                    lst.Add(new EntityPrescriptionDetails
                    {
                        ProductId = Convert.ToInt32(ddlTablet.SelectedValue),
                        ProductName = ddlTablet.SelectedItem.Text,
                        Morning = txtMorning.Text,
                        Afternoon = txtafternoon.Text,
                        Night = txtNight.Text,
                        NoOfDays = txtNoOfDays.Text,
                        Quantity = txtQuantity.Text,
                        TempId = lst.Count + 1,
                        IsBeforeLunch = chkbeforebf.Checked ? true : false,
                    });    
                }
                List<EntityInvoiceDetails> lstinv = null;
                try
                {
                    lstinv = serialize.Deserialize<List<EntityInvoiceDetails>>(invDtl.Value);
                }
                catch (Exception ex)
                {
                }
                if (ddlOther.SelectedIndex>0)
                {
                    decimal perdaycharge = string.IsNullOrEmpty(txtChargePerDay.Text) ? 0 : Convert.ToDecimal(txtChargePerDay.Text);
                    int qty = string.IsNullOrEmpty(txtQty.Text) == false ? Convert.ToInt32(txtQty.Text) : (string.IsNullOrEmpty(txtNoDays.Text) == false ? Convert.ToInt32(txtNoDays.Text) : 1);
                    var charge = new EntityInvoiceDetails()
                            {
                                OtherId = Convert.ToInt32(ddlOther.SelectedValue),
                                Amount =qty * perdaycharge,
                                ChargesName = ddlOther.SelectedItem.Text,
                                OtherChargesId = Convert.ToInt32(ddlOther.SelectedValue),
                                PerDayCharge = perdaycharge,
                                NoOfDays = string.IsNullOrEmpty(txtNoDays.Text) == false ? Convert.ToInt32(txtNoDays.Text) : 1,
                                Quantity = qty,
                                TempId = lstinv.Count + 1
                            };
                    lstinv.Add(charge);
                    invDtl.Value = serialize.Serialize(lstinv);
                    dgvChargesOPD.DataSource = lstinv;
                    dgvChargesOPD.DataBind();
                    txtTotalAmount.Text = string.Format("{0:0.00}", lstinv.Sum(p => p.Amount));    
                }
                if (lstinv!=null && lstinv.Count>0)
                {
                    txtTotalAmount.Text = string.Format("{0:0.00}", lstinv.Sum(p => p.Amount));
                }
                Prescript.Value = serialize.Serialize(lst);
                dgvChargeDetails.DataSource = lst;
                dgvChargeDetails.DataBind();
                Clear();
                InjectionPara(chkInject.Checked);
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        private void BindOtherCharge()
        {
            try
            {
                List<EntityChargeCategory> lstOther = objpatientInvoice.GetOtherChargeList();
                ddlOther.DataSource = lstOther;
                lstOther.Insert(0, new EntityChargeCategory() { ChargesId = 0, ChargeCategoryName = "--Select--" });
                ddlOther.DataValueField = "ChargesId";
                ddlOther.DataTextField = "ChargeCategoryName";
                ddlOther.DataBind();
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ddlTablet.SelectedIndex = 0;
            txtMorning.Text = string.Empty;
            txtafternoon.Text = string.Empty;
            txtNight.Text = string.Empty;
            txtNoOfDays.Text = string.Empty;
            txtQuantity.Text = string.Empty;
            Clear();
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                tblPrescription tblins = new tblPrescription();
                EntityPatientInvoice entInvoice = new EntityPatientInvoice();
                entInvoice.PatientId = Convert.ToInt32(ddlPatient.SelectedValue);
                entInvoice.IsCash = true;
                entInvoice.IsDelete = false;
                entInvoice.BillDate = StringExtension.ToDateTime(txtPrescriptionDate.Text);

                //entInvoice.Amount=
                tblins.DoctorId = Convert.ToInt32(ddlDoctors.SelectedValue);
                tblins.FollowUpDate = DateTime.Now.Date;
                if (!string.IsNullOrEmpty(txtFollowupDate.Text))
                    tblins.FollowUpDate = StringExtension.ToDateTime(txtFollowupDate.Text);
                //tblins.DeptDoctor = txtDeptCat.Text;
                tblins.AdmitId = Convert.ToInt32(ddlPatient.SelectedValue);
                tblins.Prescription_Date = DateTime.Now.Date;
                if (!string.IsNullOrEmpty(txtPrescriptionDate.Text))
                {
                    tblins.Prescription_Date = StringExtension.ToDateTime(txtPrescriptionDate.Text);
                }

                tblins.FollowUpDate = StringExtension.ToDateTime(txtFollowupDate.Text);
                tblins.IsDressing = chkDress.Checked;
                tblins.IsInjection = chkInject.Checked;
                if (chkInject.Checked)
                {
                    tblins.InjectionName = txtInjection.Text;
                }
                tblins.Investigation = Convert.ToString(txtInvestigation.Text);
                tblins.Impression = Convert.ToString(txtImpression.Text);
                tblins.AdviceNote = Convert.ToString(txtAdviceNote.Text);
                tblins.Remarks = Convert.ToString(txtRemarks.Text);

                tblPatientAdmitDetail objFac = MobjClaim.GetEmployee(Convert.ToInt32(ddlPatient.SelectedValue));
                if (objFac != null)
                {
                    List<EntityPrescriptionDetails> inslst = serialize.Deserialize<List<EntityPrescriptionDetails>>(Prescript.Value);
                    List<EntityInvoiceDetails> lstCat = serialize.Deserialize<List<EntityInvoiceDetails>>(invDtl.Value);
                    decimal? sum = lstCat.Sum(p => p.Amount);
                    entInvoice.BillType = "Original";
                    decimal discount;
                    entInvoice.Discount = !string.IsNullOrEmpty(txtDiscount.Text) ? (Decimal.TryParse(txtDiscount.Text, out discount) ? discount : 0) : 0;
                    entInvoice.Amount = sum;
                    entInvoice.NetAmount = sum - entInvoice.Discount;
                    entInvoice.ReceivedAmount = entInvoice.NetAmount;
                    objpatientInvoice.InsertInvoice(entInvoice, lstCat);
                    int ClaimId = Convert.ToInt32(MobjClaim.Save(tblins, inslst));
                    lblMessage.Text = "Record Saved Successfully.....";
                    Prescript.Value = null;
                    chkDress.Checked = false;
                    chkInject.Checked = false;
                    txtInjection.Text = string.Empty;
                    txtInvestigation.Text = string.Empty;
                    txtImpression.Text = string.Empty;
                    txtAdviceNote.Text = string.Empty;
                    txtRemarks.Text = string.Empty;
                    InjectionPara(chkInject.Checked);
                    Clear(true);
                    inslst = new List<EntityPrescriptionDetails>();
                    dgvChargeDetails.DataSource = inslst;
                    dgvChargeDetails.DataBind();
                    lblMsg.Text = string.Empty;
                    InjectionPara(chkInject.Checked);
                }
                else
                {
                    lblMsg.Text = "Invalid Patient";
                }
                Prescript.Value = serialize.Serialize(new List<EntityPrescriptionDetails>());
                BindPrescription();
                ddlDoctors_SelectedIndexChanged(sender, e);
                MultiView1.SetActiveView(View1);
            }
            catch (Exception ex)
            {
                Commons.FileLog(ex.Message, ex);
            }
        }
        public void BindPrescription()
        {
            try
            {
                int DoctorId = 0;
                if (SessionManager.Instance.LoginUser != null)
                {
                    if (SessionManager.Instance.LoginUser.UserType.ToLower() == "doctor")
                    {
                        DoctorId = SessionManager.Instance.LoginUser.PKId;
                    }
                }
                List<EntityPrescription> lst = null;
                if (DoctorId > 0)
                {
                    lst = MobjClaim.GetPrescriptions(DoctorId);//.Where(p => p.DoctorId == DoctorId).ToList();
                }
                else
                {
                    lst = MobjClaim.GetPrescriptions();
                }
                dgvClaim.Width = 820;
                dgvPatientlist.Width = 200;
                if (SessionManager.Instance.LoginUser != null)
                {
                    if (SessionManager.Instance.LoginUser.UserType != "Doctor")
                    {
                        dgvClaim.Width = 1020;
                        dgvPatientlist.Width = 0;
                    }
                    if (SessionManager.Instance.LoginUser.UserType == "Doctor" && dgvPatientlist.Rows.Count==0)
                    {
                        dgvClaim.Width = 1020;
                        dgvPatientlist.Width = 0;
                    }
                }
                else
                {
                    dgvClaim.Width = 1020;
                    dgvPatientlist.Width = 0;
                }
                dgvClaim.DataSource = lst;
                int lintRowcount = lst.Count();
                lblRowCount.Text = "<b>Total Records:</b> " + lintRowcount.ToString();
                dgvClaim.DataBind();
            }
            catch (Exception ex)
            {
                Commons.FileLog(ex.InnerException.Message, ex);
            }
            
        }

        public void BindPrescriptionDetails(int Id)
        {
            try
            {
                List<EntityPrescriptionDetails> lst = MobjClaim.GetPrescription(Id);
                dgvChargeDetails.DataSource = lst;
                dgvChargeDetails.DataBind();
                Prescript.Value =serialize.Serialize(lst);
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void ddlOther_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlOther.SelectedIndex>0)
            {
                EntityPatientInvoice patInv = objpatientInvoice.GetChargesForCate(Convert.ToInt32(ddlOther.SelectedValue));
                txtChargePerDay.Text = Convert.ToString(patInv.Amount);
                txtAmount.Enabled = true;
                txtAmount.Text = Convert.ToString(patInv.Amount);
                txtNoDays.Text = Convert.ToString(1);    
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                ddlPatient.Enabled = false;
                tblPrescription tblins = new tblPrescription();
                tblins.Prescription_Id = Convert.ToInt32(PrescriptionId.Value);
                tblins.AdmitId = Convert.ToInt32(ddlPatient.SelectedValue);
                tblins.DoctorId = Convert.ToInt32(ddlDoctors.SelectedValue);
                //tblins.DeptDoctor = txtDeptCat.Text;
                tblins.Prescription_Date = StringExtension.ToDateTime(txtPrescriptionDate.Text);
                tblins.InjectionName = txtInjection.Text;
                tblins.IsDressing = chkDress.Checked;
                tblins.IsInjection = chkInject.Checked;
                tblins.AdviceNote = txtAdviceNote.Text;
                tblins.Investigation = txtInvestigation.Text;
                tblins.Impression = Convert.ToString(txtImpression.Text);
                tblins.Remarks = Convert.ToString(txtRemarks.Text);
                tblins.FollowUpDate = StringExtension.ToDateTime(txtFollowupDate.Text);
                List<EntityPrescriptionDetails> inslst = serialize.Deserialize<List<EntityPrescriptionDetails>>(Prescript.Value);
                MobjClaim.Update(tblins, inslst);
                lblMessage.Text = "Record Updated Successfully.....";
                chkDress.Checked = false;
                chkInject.Checked = false;
                txtInjection.Text = string.Empty;
                txtInvestigation.Text = string.Empty;
                txtImpression.Text = string.Empty;
                txtAdviceNote.Text = string.Empty;
                txtRemarks.Text = string.Empty;
                InjectionPara(chkInject.Checked);
                Clear();
                List<EntityPrescriptionDetails> lst = new List<EntityPrescriptionDetails>();
                dgvChargeDetails.DataSource = lst;
                dgvChargeDetails.DataBind();
                
                BindPrescription();
                ddlDoctors_SelectedIndexChanged(sender, e);
                Prescript.Value = null;
                MultiView1.SetActiveView(View1);
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void btnUpdatecharge_Click(object sender, EventArgs e)
        {
            List<EntityPrescriptionDetails> lst = serialize.Deserialize<List<EntityPrescriptionDetails>>(Prescript.Value);
            foreach (EntityPrescriptionDetails item in lst)
            {
                if (Convert.ToInt32(TempId.Value) == item.TempId)
                {
                    item.ProductId = Convert.ToInt32(ddlTablet.SelectedValue);
                    item.Morning = Convert.ToString(txtMorning.Text);
                    item.Afternoon = Convert.ToString(txtafternoon.Text);
                    item.Night = Convert.ToString(txtNight.Text);
                    item.NoOfDays = Convert.ToString(txtNoOfDays.Text);
                    item.Quantity = Convert.ToString(txtQuantity.Text);
                    item.IsDelete = false;
                }
                else
                {
                    if (item.IsDelete)
                    {
                        lst.Add(new EntityPrescriptionDetails()
                        {
                            PrescriptionDetailId = item.PrescriptionDetailId,
                            Prescription_Id = item.Prescription_Id,
                            IsDelete = item.IsDelete,
                            ProductId = item.ProductId,
                            Quantity = item.Quantity,
                            Morning = item.Morning,
                            Afternoon = item.Afternoon,
                            Night = item.Night,
                            NoOfDays = item.NoOfDays
                        });
                    }
                }
            }
            //MobjClaim.Update(lst);
            Prescript.Value = serialize.Serialize(lst);
            dgvChargeDetails.DataSource = lst;
            dgvChargeDetails.DataBind();
            Clear();
            InjectionPara(false);
            btnUpdatecharge.Visible = false;
            btnAdd.Visible = true;
        }

        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            btnAdd.Visible = true;
            btnUpdatecharge.Visible = false;
            BtnSave.Visible = false;
            btnUpdate.Visible = true;
            Myflag.Value = "Edit";
            //GetDeptCategory();
            ImageButton imgEdit = (ImageButton)sender;
            GridViewRow row = (GridViewRow)imgEdit.NamingContainer;
            PrescriptionId.Value = Convert.ToString(dgvClaim.DataKeys[row.RowIndex].Value);

            tblPrescription objPresc = MobjClaim.GetPrescriptionInfo(Convert.ToInt32(PrescriptionId.Value));
            DateTime MDate = objPresc.Prescription_Date == null ? DateTime.Now.Date : objPresc.Prescription_Date.Value;
            txtPrescriptionDate.Text = string.Format("{0:dd/MM/yyyy}", MDate);
            chkDress.Checked = Convert.ToBoolean(objPresc.IsDressing);
            chkInject.Checked = Convert.ToBoolean(objPresc.IsInjection);
            txtInvestigation.Text = Convert.ToString(objPresc.Investigation);
            txtImpression.Text = Convert.ToString(objPresc.Impression);
            txtAdviceNote.Text = Convert.ToString(objPresc.AdviceNote);
            txtRemarks.Text = Convert.ToString(objPresc.Remarks);
            InjectionPara(objPresc.IsInjection.Value);
            txtInjection.Text = objPresc.InjectionName;
            ListItem item = ddlDoctors.Items.FindByValue(Convert.ToString(objPresc.DoctorId));
            ddlDoctors.SelectedValue = item.Value;
            ddlDoctors_SelectedIndexChanged(ddlDoctors, e);
            if (ddlPatient.Items.Count > 0)
            {
                item = ddlPatient.Items.FindByValue(Convert.ToString(objPresc.AdmitId));
                ddlPatient.SelectedValue = item.Value;
            }
            if (objPresc.FollowUpDate != null)
            {
                MDate = objPresc.FollowUpDate==null?DateTime.Now.Date:objPresc.FollowUpDate.Value;
                txtFollowupDate.Text = string.Format("{0:dd/MM/yyyy}", MDate);
            }
            BindPrescriptionDetails(Convert.ToInt32(PrescriptionId.Value));
            MultiView1.SetActiveView(View2);
        }

        protected void BtnClose_Click(object sender, EventArgs e)
        {
            Clear(true);
            MultiView1.SetActiveView(View1);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSearch.Text))
                {
                    List<EntityPrescription> lst = MobjClaim.GetInsurance(txtSearch.Text);
                    if (lst.Count > 0)
                    {
                        dgvClaim.DataSource = lst;
                        dgvClaim.DataBind();
                        //Session["PrescriptDetails"] = lst;
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
                BindPrescription();
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
                int DoctorId = 0;
                if (SessionManager.Instance.LoginUser.UserType.ToLower() == "doctor")
                {
                    DoctorId = SessionManager.Instance.LoginUser.PKId;
                }
                string url = "~/ExcelReport/MonthwiseSalExcel.aspx?ReportType=Prescription";
                if (DoctorId>0)
                {
                    url += "&DoctorId=" + DoctorId;
                }
                Response.Redirect(url,false);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void BtnAddNewPrescription_Click(object sender, EventArgs e)
        {
            try
            {
                Myflag.Value = "Addnew";
                ddlPatient.Enabled = true;
                txtPrescriptionDate.Text = string.Empty;
                BtnSave.Visible = true;
                btnUpdate.Visible = false;
                btnAdd.Visible = true;
                btnUpdatecharge.Visible = false;
                InjectionPara(false);
                txtInvestigation.Text = string.Empty;
                txtImpression.Text = string.Empty;
                txtAdviceNote.Text = string.Empty;
                txtRemarks.Text = string.Empty;
                List<EntityPrescriptionDetails> lst = new List<EntityPrescriptionDetails>();
                Prescript.Value = serialize.Serialize(lst);
                dgvChargeDetails.DataSource = lst;
                dgvChargeDetails.DataBind();
                MultiView1.SetActiveView(View2);
            }
            catch (Exception ex)
            {
                Commons.FileLog(ex.Message, ex);
            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton imgEdit = (ImageButton)sender;
                GridViewRow row = (GridViewRow)imgEdit.NamingContainer;
                int ID_Issue = Convert.ToInt32(dgvClaim.DataKeys[row.RowIndex].Value);
                if (ID_Issue > 0)
                {
                    Response.Redirect("~/PathalogyReport/Reports.aspx?ReportType=Prescription&Prescription_Id=" + dgvClaim.DataKeys[row.RowIndex].Value, false);
                    //Response.Redirect("~/PathalogyReport/PathologyReport.aspx?ReportType=Prescription&Prescription_Id=" + dgvClaim.DataKeys[row.RowIndex].Value, false);
                }
                else
                {

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
                int DoctorId = 0;
                if (SessionManager.Instance.LoginUser.UserType.ToLower() == "doctor")
                {
                    DoctorId = SessionManager.Instance.LoginUser.PKId;
                }
                List<EntityPrescription> lst = null;
                if (DoctorId > 0)
                {
                    lst = MobjClaim.GetPrescriptions(DoctorId);//.Where(p => p.DoctorId == DoctorId).ToList();
                }
                else
                {
                    lst = MobjClaim.GetPrescriptions();
                }
                if (!string.IsNullOrEmpty(txtSearch.Text))
                {
                    lst = lst.Where(p => p.PatientName.Contains(txtSearch.Text)).ToList();
                }
                dgvClaim.DataSource = lst;// (List<EntityPrescription>)Session["PrescriptDetails"];
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

        protected void chkInject_CheckedChanged(object sender, EventArgs e)
        {
            if (chkInject.Checked)
            {
                InjectionPara(true);
            }
            else
            {
                InjectionPara(false);
            }
        }

        private void InjectionPara(bool flag)
        {
            txtInjection.Visible = flag;
            lblInjection.Visible = flag;
        }

        //protected void ddlTablet_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        List<EntityPrescriptionDetails> lst = null;
        //        if (ddlTablet.SelectedIndex > 0)
        //        {
        //            IssueMaterialBLL mobjDeptBLL = new IssueMaterialBLL();
        //            EntityProduct entProduct = mobjDeptBLL.GetProductPrice(Convert.ToInt32(ddlTablet.SelectedValue));

        //            List<tblStockDetail> lst1 = new CustomerTransactionBLL().GetProductTransByProductId(Convert.ToInt32(ddlTablet.SelectedValue));
        //            if (lst1 != null)
        //            {
        //                lblBalQty.Text = Convert.ToString(Convert.ToInt32(lst1.Sum(p => p.InwardQty)) - Convert.ToInt32(lst1.Sum(p => p.OutwardQty)));
        //            }
        //            else
        //            {
        //                lblBalQty.Text = string.Empty;
        //            }

        //        }
        //        else
        //        {
        //            lst = new List<EntityPrescriptionDetails>();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        lblMsg.Text = ex.Message;
        //    }
        //}

       
    }
}