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

namespace Hospital.InsuranceClaim
{
    public partial class frmClaimApproval : System.Web.UI.Page
    {
        InsuranceClaimBLL MobjClaim = new InsuranceClaimBLL();
        ClaimApprovalBLL MobjApprove = new ClaimApprovalBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (SessionManager.Instance.LoginUser != null)
            {
                if (!Page.IsPostBack)
                {
                    //SessionManager.Instance.SetSession();
                    Session["Myflag"] = string.Empty;
                    BindInsuranceClaim();
                    //BindBankNames();
                    MultiView1.SetActiveView(View1);
                }
            }
            else
            {
                Response.Redirect("~/frmlogin.aspx", false);
            }
        }

        public void Clear()
        {
            txtPatient.Text = string.Empty;
            txtClaimamount.Text = string.Empty;
            txtClaimDate.Text = string.Empty;
            txtCheckNo.Text = string.Empty;
            txtBankName.Text = string.Empty;
            txtBankRefNo.Text = string.Empty;
            txtCoPayment.Text = string.Empty;
            txtBadDebts.Text = string.Empty;
            txtApprovedate.Text = string.Empty;
            txtApprovedamt.Text = string.Empty;
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                tblInsuranceClaim tblins = new tblInsuranceClaim();
                tblins.ClaimId = Convert.ToInt32(Session["ClaimId"]);
                tblins.ApprovedAmount = Convert.ToDecimal(txtApprovedamt.Text);
                tblins.ReceivedAmt = Convert.ToDecimal(txtRecAmount.Text);
                tblins.ApprovedDate = StringExtension.ToDateTime(txtApprovedate.Text);
                tblins.IsApproved = true;
                tblins.AdmitId = Convert.ToInt32(Session["AdmitId"]);
                tblins.TDS = Convert.ToInt32(txtTds.Text);
                //tblins.TDSAmt = Convert.ToDecimal(txtApprovedamt.Text) * Convert.ToDecimal(txtTds.Text) / 100;
                tblins.BadDebts = Convert.ToDecimal(txtBadDebts.Text);
                tblins.CoPayment = Convert.ToDecimal(txtCoPayment.Text);

                EntityCustomerTransaction objPatient = new EntityCustomerTransaction();
                objPatient.PatientId = Convert.ToInt32(Session["AdmitId"]);
                objPatient.PayAmount = Convert.ToDecimal(txtRecAmount.Text);
                objPatient.BillAmount = Convert.ToDecimal(txtRecAmount.Text);
                objPatient.ReceiptDate = StringExtension.ToDateTime(txtApprovedate.Text);
                objPatient.TransactionDocNo = Convert.ToInt32(Session["ClaimId"]);
                EntityCustomerTransaction objBank = null;

                if (IsNeft.Checked || IsCheque.Checked)
                {
                    objBank = new EntityCustomerTransaction();
                    objBank.TransactionDocNo = Convert.ToInt32(Session["ClaimId"]);
                    objBank.BankRefNo = Convert.ToString(txtBankRefNo.Text);
                    objBank.PatientId = 0;
                    objBank.ChequeDate = StringExtension.ToDateTime(txtApprovedate.Text);
                    objBank.BillAmount = Convert.ToDecimal(txtRecAmount.Text);
                    objBank.PayAmount = Convert.ToDecimal(txtRecAmount.Text);
                    objBank.ReceiptDate = StringExtension.ToDateTime(txtApprovedate.Text);
                    if (IsCheque.Checked)
                    {
                        objBank.BankName = txtBankName.Text;
                        objBank.ChequeNo = txtCheckNo.Text;
                    }
                }

                MobjApprove.InsertData(tblins, objPatient, objBank, IsCash.Checked, IsNeft.Checked, IsCheque.Checked);
                BindInsuranceClaim();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
            MultiView1.SetActiveView(View1);
        }
        public void BindInsuranceClaim()
        {
            try
            {
                List<EntityInsuranceClaim> lst = MobjClaim.GetInsurance();
                Session["ClaimDetails"] = lst;
                dgvClaim.DataSource = lst;
                int lintRowcount = lst.Count();
                lblRowCount.Text = "<b>Total Records:</b> " + lintRowcount.ToString();
                dgvClaim.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void BtnApprove_Click(object sender, EventArgs e)
        {
            IsCash.Checked = true;
            BtnUpdate.Visible = false;
            BtnSave.Visible = true;
            IsCash_CheckedChanged(sender, e);
            ImageButton imgEdit = (ImageButton)sender;
            GridViewRow row = (GridViewRow)imgEdit.NamingContainer;
            Session["ClaimId"] = Convert.ToInt32(dgvClaim.DataKeys[row.RowIndex].Value);
            Session["AdmitId"] = Convert.ToInt32(row.Cells[1].Text);
            txtPatient.Text = row.Cells[2].Text;
            txtClaimamount.Text = row.Cells[5].Text;
            txtRecAmount.Text = Convert.ToString(0);
            txtClaimDate.Text = row.Cells[4].Text;
            txtPatient.Enabled = false;
            txtClaimDate.Enabled = false;
            txtClaimamount.Enabled = false;
            string approve = row.Cells[6].Text;
            CalDate.StartDate = StringExtension.ToDateTime(txtClaimDate.Text);
            if (approve.Equals("Approved"))
            {
                lblMessage.Text = "You Cannot Change Claim Details. Claim Is Already Approved...";
            }
            else
            {
                MultiView1.SetActiveView(View2);
            }

        }

        protected void BtnBillSettlement_Click(object sender, EventArgs e)
        {
            BtnUpdate.Visible = true;
            BtnSave.Visible = false;
            IsCash.Checked = true;
            IsCash_CheckedChanged(sender, e);
            ImageButton imgEdit = (ImageButton)sender;
            GridViewRow row = (GridViewRow)imgEdit.NamingContainer;
            Session["ClaimId"] = Convert.ToInt32(dgvClaim.DataKeys[row.RowIndex].Value);
            Session["AdmitId"] = Convert.ToInt32(row.Cells[1].Text);
            EntityInsuranceClaim Cate = MobjClaim.GetBillSettlement(Convert.ToInt32(dgvClaim.DataKeys[row.RowIndex].Value));
            txtApprovedamt.Text = Convert.ToString(string.Format("{0:0.00}", Cate.ApprovedAmount));
            txtBadDebts.Text = Convert.ToString(string.Format("{0:0.00}", Cate.BadDebts));
            txtTds.Text = Convert.ToString(string.Format("{0:0.00}", Cate.TDS));
            txtCoPayment.Text = Convert.ToString(string.Format("{0:0.00}", Cate.CoPayment));
            txtRecAmount.Text = Convert.ToString(string.Format("{0:0.00}", Cate.ReceivedAmt));
            txtApprovedate.Text = (Cate.ApprovedDate).ToString("dd/MM/yyyy");
            txtPatient.Text = row.Cells[2].Text;
            txtClaimamount.Text = row.Cells[5].Text;
            txtClaimDate.Text = row.Cells[4].Text;
            txtPatient.Enabled = false;
            txtClaimDate.Enabled = false;
            txtClaimamount.Enabled = false;
            string approve = row.Cells[6].Text;
            CalDate.StartDate = StringExtension.ToDateTime(txtClaimDate.Text);

            MultiView1.SetActiveView(View2);

        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {

            try
            {
                tblInsuranceClaim tblins = new tblInsuranceClaim();
                tblins.ClaimId = Convert.ToInt32(Session["ClaimId"]);
                tblins.ReceivedAmt = Convert.ToDecimal(txtRecAmount.Text);
                tblins.CoPayment = Convert.ToDecimal(txtCoPayment.Text);
                tblins.BadDebts = Convert.ToDecimal(txtBadDebts.Text);
                tblins.ApprovedAmount = Convert.ToDecimal(txtApprovedamt.Text);
                tblins.ApprovedDate = StringExtension.ToDateTime(txtApprovedate.Text);
                tblins.TDS = Convert.ToInt32(txtTds.Text);
                MobjClaim.UpdateBill(tblins);
                lblMessage.Text = "Record Updated Successfully.....";
                Clear();
                //List<EntityinsuranceClaimDetails> lst = new List<EntityinsuranceClaimDetails>();
                //dgvChargeDetails.DataSource = lst;
                //dgvChargeDetails.DataBind();
                BindInsuranceClaim();
                Session["ClaimCharges"] = null;
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
            MultiView1.SetActiveView(View1);
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
                    List<EntityInsuranceClaim> lst = MobjClaim.GetInsurance(txtSearch.Text);

                    Session["ClaimDetails"] = lst;
                    dgvClaim.DataSource = lst;
                    int lintRowcount = lst.Count();
                    lblRowCount.Text = "<b>Total Records:</b> " + lintRowcount.ToString();
                    dgvClaim.DataBind();
                }
                else
                {
                    lblMessage.Text = "Please Fill Search Text...";
                    txtSearch.Focus();
                }
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
                BindInsuranceClaim();
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
                //ListConverter.ToDataTable((List<EntityInsuranceClaim>)Session["ClaimDetails"]);
                Session["Details"] = ListConverter.ToDataTable((List<EntityInsuranceClaim>)Session["ClaimDetails"]);
                Response.Redirect("~/ExcelReport/MonthwiseSalExcel.aspx");
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

        protected void IsCash_CheckedChanged(object sender, EventArgs e)
        {
            if (IsCash.Checked)
            {
                lblbankRefNo.Visible = false;
                txtBankRefNo.Visible = false;
                lblbankname.Visible = false;
                lblcheckno.Visible = false;
                txtBankName.Visible = false;
                txtCheckNo.Visible = false;
                Pnlclaim.Visible = false;
            }
            else if (IsNeft.Checked)
            {
                txtBankRefNo.Visible = true;
                txtBankName.Visible = false;
                txtCheckNo.Visible = false;
                lblbankRefNo.Visible = true;
                lblbankname.Visible = false;
                lblcheckno.Visible = false;
                Pnlclaim.Visible = true;
            }
            else if (IsCheque.Checked)
            {
                txtBankRefNo.Visible = false;
                txtBankName.Visible = true;
                txtCheckNo.Visible = true;
                lblbankRefNo.Visible = false;
                lblbankname.Visible = true;
                lblcheckno.Visible = true;
                Pnlclaim.Visible = true;
            }
        }
    }
}