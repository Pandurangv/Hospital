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
    public partial class frmSupplierMaster : BasePage
    {
        SupplierBLL mobjSupplierBLL = new SupplierBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            base.AuthenticateUser();
            if (!Page.IsPostBack)
            {
                GetSupplier();
                MultiView1.SetActiveView(View1);
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                txtSupplierCode.Text = String.Empty;
                txtSupplierName.Text = String.Empty;
                txtAddress.Text = String.Empty;
                txtPhoneNo.Text = String.Empty;
                txtMobileNo.Text = String.Empty;
                txtVATCSTNo.Text = String.Empty;
                txtExciseNo.Text = String.Empty;
                txtEmail.Text = String.Empty;
                txtServiceTaxNo.Text = String.Empty;
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

                int key =Convert.ToInt32(dgvSupplier.DataKeys[cnt.RowIndex].Value);
                var supplier=mobjSupplierBLL.GetAllSupplier().Where(p => p.PKId == key).FirstOrDefault();
                if (supplier!=null)
                {
                    txtSupplierCode.Text = string.IsNullOrEmpty(supplier.SupplierCode) ? "" : Convert.ToString(supplier.SupplierCode);
                    txtSupplierName.Text = string.IsNullOrEmpty(supplier.SupplierName) ? "" : Convert.ToString(supplier.SupplierName);
                    txtAddress.Text = string.IsNullOrEmpty(supplier.Address)?"": Convert.ToString(supplier.Address);
                    txtPhoneNo.Text = string.IsNullOrEmpty(supplier.PhoneNo) ? "" : Convert.ToString(supplier.PhoneNo);
                    txtMobileNo.Text = string.IsNullOrEmpty(supplier.MobileNo) ? "" : Convert.ToString(supplier.MobileNo);
                    txtVATCSTNo.Text = string.IsNullOrEmpty(supplier.VATCSTNo) ? "" : Convert.ToString(supplier.VATCSTNo);
                    txtExciseNo.Text = string.IsNullOrEmpty(supplier.ExciseNo) ? "" : Convert.ToString(supplier.ExciseNo);
                    txtEmail.Text = string.IsNullOrEmpty(supplier.Email) ? "" : Convert.ToString(supplier.Email);
                    txtServiceTaxNo.Text = string.IsNullOrEmpty(supplier.ServiceTaxNo) ? "" : Convert.ToString(supplier.ServiceTaxNo);
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
            lblMsg.Text = string.Empty;
            DataTable ldt = new DataTable();
            ldt = mobjSupplierBLL.GetNewSupplierCode();
            txtSupplierCode.Text = ldt.Rows[0]["SupplierCode"].ToString();
            txtSupplierName.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtPhoneNo.Text = string.Empty;
            txtMobileNo.Text = string.Empty;
            txtVATCSTNo.Text = string.Empty;
            txtExciseNo.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtServiceTaxNo.Text = string.Empty;
            BtnSave.Visible = true;
            btnUpdate.Visible = false;
            MultiView1.SetActiveView(View2);
            //this.programmaticModalPopup.Show();

        }
        public void GetSupplier()
        {
            List<EntitySupplierMaster> ldtSupplier = mobjSupplierBLL.GetAllSupplier();
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
                EntitySupplier entSupplier = new EntitySupplier();

                entSupplier.SupplierCode = txtSupplierCode.Text;
                entSupplier.SupplierName = txtSupplierName.Text;
                entSupplier.Address = txtAddress.Text;
                entSupplier.PhoneNo = txtPhoneNo.Text;
                entSupplier.MobileNo = txtMobileNo.Text;
                entSupplier.VATCSTNo = txtVATCSTNo.Text;
                entSupplier.ExciseNo = txtExciseNo.Text;
                entSupplier.Email = txtEmail.Text;
                entSupplier.ServiceTaxNo = txtServiceTaxNo.Text;
                entSupplier.ChangeBy = SessionManager.Instance.LoginUser.PKId.ToString();
                if (mobjSupplierBLL.GetSupplier(entSupplier) != null)
                {
                    lintCnt = mobjSupplierBLL.UpdateSupplier(entSupplier);
                    if (lintCnt > 0)
                    {
                        GetSupplier();
                        lblMessage.Text = "Record Updated Successfully";
                    }
                    else
                    {
                        lblMessage.Text = "Record Not Updated";
                    }
                }
                else
                {
                    lblMessage.Text = "Record Already Exist...";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
            MultiView1.SetActiveView(View1);
        }

        protected void dgvSupplier_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvSupplier.PageIndex = e.NewPageIndex;
        }

        protected void dgvSupplier_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                List<EntitySupplierMaster> ldtSupplier = mobjSupplierBLL.GetAllSupplier();

                dgvSupplier.DataSource = ldtSupplier;// (List<EntitySupplierMaster>)Session["SupplierDetail"];
                dgvSupplier.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            int lintcnt = 0;
            EntitySupplier entSupplier = new EntitySupplier();
            if (string.IsNullOrEmpty(txtSupplierCode.Text.Trim()))
            {
                lblMsg.Text = "Please Enter Discount Code";
            }
            else
            {
                if (string.IsNullOrEmpty(txtSupplierName.Text.Trim()))
                {
                    lblMsg.Text = "Please Enter Supplier Name";
                    txtSupplierName.Focus();
                    return;
                }
                else
                {
                    entSupplier.SupplierCode = txtSupplierCode.Text.Trim();
                    entSupplier.SupplierName = txtSupplierName.Text.Trim();
                    entSupplier.Address = txtAddress.Text.Trim();
                    entSupplier.PhoneNo = txtPhoneNo.Text.Trim();
                    entSupplier.MobileNo = txtMobileNo.Text.Trim();
                    entSupplier.VATCSTNo = txtVATCSTNo.Text.Trim();
                    entSupplier.ExciseNo = txtExciseNo.Text.Trim();
                    entSupplier.Email = txtEmail.Text.Trim();
                    entSupplier.ServiceTaxNo = txtServiceTaxNo.Text.Trim();
                    entSupplier.EntryBy = SessionManager.Instance.LoginUser.PKId.ToString();

                    lintcnt = mobjSupplierBLL.InsertSupplier(entSupplier);
                    if (lintcnt > 0)
                    {
                        GetSupplier();
                        lblMessage.Text = "Record Inserted Successfully";
                    }
                    else
                    {
                        lblMessage.Text = "Record Not Inserted";
                    }
                }
                MultiView1.SetActiveView(View1);
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
            List<EntitySupplierMaster> lst = mobjSupplierBLL.SelectSupplier(Prefix);
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
            GetSupplier();
        }
    }
}