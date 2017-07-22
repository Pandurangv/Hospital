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

namespace Hospital.BankTransaction
{
    public partial class frmBankMaster : System.Web.UI.Page
    {
        BankMasterBLL mobjSupplierBLL = new BankMasterBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (SessionManager.Instance.LoginUser!=null)
            {
                if (!Page.IsPostBack)
                {
                    GetBankDetails();
                    MultiView1.SetActiveView(View1);
                }
            }
            else
            {
                Response.Redirect("~/frmLogin.aspx", false);
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                txtBankName.Text = String.Empty;
                txtAddress.Text = String.Empty;
                txtCity.Text = String.Empty;
                txtPhoneNo.Text = String.Empty;
                txtMobileNo.Text = String.Empty;
                txtIFSCCode.Text = String.Empty;
                txtMISC.Text = String.Empty;
                txtBranch.Text = String.Empty;
                txtAccNo.Text = String.Empty;
                MultiView1.SetActiveView(View1);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton imgEdit = (ImageButton)sender;
                GridViewRow cnt = (GridViewRow)imgEdit.NamingContainer;
                if (cnt != null)
                {
                    EntityBankMaster bank = mobjSupplierBLL.GetBank(Convert.ToInt32(dgvSupplier.DataKeys[cnt.RowIndex].Value));
                    if (bank != null)
                    {
                        txtPincode.Text = bank.Pin;
                        txtCity.Text = bank.City;
                        txtBranch.Text = bank.BranchName;
                        txtMobileNo.Text = bank.MobileNo;
                    }
                    txtBankName.Text = Convert.ToString(cnt.Cells[0].Text);
                    txtAddress.Text = Convert.ToString(cnt.Cells[1].Text);
                    txtPhoneNo.Text = Convert.ToString(cnt.Cells[6].Text);
                    txtIFSCCode.Text = Convert.ToString(cnt.Cells[2].Text);
                    txtMISC.Text = Convert.ToString(cnt.Cells[3].Text);
                    txtCustomerId.Text = Convert.ToString(cnt.Cells[7].Text);
                    txtAccNo.Text = Convert.ToString(cnt.Cells[4].Text);
                    bankid.Value = dgvSupplier.DataKeys[cnt.RowIndex].Value.ToString();
                }
                BtnSave.Visible = false;
                btnUpdate.Visible = true;
                MultiView1.SetActiveView(View2);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void BtnAddNewSupplier_Click(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;
            lblMsg.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtCity.Text = string.Empty;
            txtPhoneNo.Text = string.Empty;
            txtMobileNo.Text = string.Empty;
            txtIFSCCode.Text = string.Empty;
            txtMISC.Text = string.Empty;
            txtBranch.Text = string.Empty;
            txtAccNo.Text = string.Empty;
            txtCustomerId.Text = string.Empty;
            txtPincode.Text = string.Empty;
            BtnSave.Visible = true;
            btnUpdate.Visible = false;
            MultiView1.SetActiveView(View2);
        }
        public void GetBankDetails()
        {
            List<EntityBankMaster> ldtSupplier = mobjSupplierBLL.GetBankDetails();
            if (ldtSupplier.Count > 0 && ldtSupplier != null)
            {
                dgvSupplier.DataSource = ldtSupplier;
                dgvSupplier.DataBind();
                //Session["SupplierDetail"] = ldtSupplier;
                int lintRowcount = ldtSupplier.Count;
                lblRowCount.Text = "<b>Total Records:</b> " + lintRowcount.ToString();
            }
        }

        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            int lintCnt = 0;
            try
            {
                EntityBankMaster entSupplier = new EntityBankMaster();
                entSupplier.BankId = Convert.ToInt32(bankid.Value);
                entSupplier.BankName = txtBankName.Text;
                entSupplier.BankAddress = txtAddress.Text;
                entSupplier.City = txtCity.Text;
                entSupplier.PhNo = txtPhoneNo.Text;
                entSupplier.MobileNo = txtMobileNo.Text;
                entSupplier.IFSCCode = txtIFSCCode.Text;
                entSupplier.MISCCode = txtMISC.Text;
                entSupplier.BranchName = txtBranch.Text;
                entSupplier.Pin = txtPincode.Text;
                entSupplier.AccountNo = txtAccNo.Text;
                entSupplier.CustomerId = txtCustomerId.Text;
                if (mobjSupplierBLL.GetBank(entSupplier) != null)
                {
                    lintCnt = mobjSupplierBLL.Update(entSupplier);
                    if (lintCnt > 0)
                    {
                        GetBankDetails();
                        lblMessage.Text = "Record Updated Successfully";
                        MultiView1.SetActiveView(View1);
                    }
                    else
                    {
                        lblMsg.Text = "Record Not Updated";
                        MultiView1.SetActiveView(View2);
                    }
                }
                else
                {
                    EntityBankMaster bankAcc = mobjSupplierBLL.GetBankByAccNo(entSupplier);
                    if (bankAcc == null)
                    {
                        lintCnt = mobjSupplierBLL.Update(entSupplier);
                        GetBankDetails();
                        lblMessage.Text = "Record Updated Successfully";
                        MultiView1.SetActiveView(View1);
                    }
                    else
                    {
                        lblMsg.Text = "Record Already Exist...";
                        MultiView1.SetActiveView(View2);
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void dgvSupplier_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvSupplier.PageIndex = e.NewPageIndex;
        }

        protected void dgvSupplier_DataBound(object sender, EventArgs e)
        {
            int lintCurrentPage = dgvSupplier.PageIndex + 1;
            lblPageCount.Text = "<b>Page</b> " + lintCurrentPage.ToString() + "<b> of </b>" + dgvSupplier.PageCount.ToString();
        }

        protected void dgvSupplier_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                List<EntityBankMaster> ldtSupplier = mobjSupplierBLL.GetBankDetails();
                if (!string.IsNullOrEmpty(txtSearch.Text))
                {
                    ldtSupplier = ldtSupplier.Where(p => p.BankName.Contains(txtSearch.Text) || p.BankAddress.Contains(txtSearch.Text)).ToList();
                }
                dgvSupplier.DataSource = ldtSupplier;// (List<EntityBankMaster>)Session["SupplierDetail"];
                dgvSupplier.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;
            lblMsg.Text = string.Empty;
            int lintcnt = 0;
            EntityBankMaster entSupplier = new EntityBankMaster();
            if (string.IsNullOrEmpty(txtBankName.Text.Trim()))
            {
                lblMsg.Text = "Please Enter Discount Code";
            }
            else
            {
                if (string.IsNullOrEmpty(txtAddress.Text.Trim()))
                {
                    lblMsg.Text = "Please Enter Supplier Name";
                    txtAddress.Focus();
                    return;
                }
                else
                {
                    entSupplier.BankName = txtBankName.Text;
                    entSupplier.BankAddress = txtAddress.Text;
                    entSupplier.City = txtCity.Text;
                    entSupplier.PhNo = txtPhoneNo.Text;
                    entSupplier.MobileNo = txtMobileNo.Text;
                    entSupplier.IFSCCode = txtIFSCCode.Text;
                    entSupplier.MISCCode = txtMISC.Text;
                    entSupplier.BranchName = txtBranch.Text;
                    entSupplier.Pin = txtPincode.Text;
                    entSupplier.AccountNo = txtAccNo.Text;
                    entSupplier.CustomerId = txtCustomerId.Text;

                    if (!Commons.IsRecordExists("tblBankMaster", "AccountNo", entSupplier.AccountNo))
                    {
                        lintcnt = mobjSupplierBLL.InsertBankMaster(entSupplier);
                        if (lintcnt > 0)
                        {
                            GetBankDetails();
                            lblMessage.Text = "Record Inserted Successfully";
                            MultiView1.SetActiveView(View1);
                        }
                        else
                        {
                            lblMsg.Text = "Record Not Inserted";
                            MultiView1.SetActiveView(View2);
                        }
                    }
                    else
                    {
                        lblMsg.Text = "Record Already Exist...";
                        MultiView1.SetActiveView(View2);
                    }
                }
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                if (!string.IsNullOrEmpty(txtSearch.Text))
                {
                    SearchSupplierDetails(txtSearch.Text);
                }
                else
                {
                    lblMessage.Text = "Please Fill Search Text.";
                    txtSearch.Focus();
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        private void SearchSupplierDetails(string Prefix)
        {
            List<EntityBankMaster> lst = mobjSupplierBLL.SelectBanks(Prefix);
            if (lst != null)
            {
                dgvSupplier.DataSource = lst;
                dgvSupplier.DataBind();
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;
            txtSearch.Text = string.Empty;
            GetBankDetails();
        }
    }
}