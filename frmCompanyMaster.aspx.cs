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
    public partial class frmCompanyMaster : BasePage
    {

        CompanyBLL mobjCompanyBLL = new CompanyBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            base.AuthenticateUser();
            if (!Page.IsPostBack)
            {
                GetCompany();
                MultiView1.SetActiveView(View1);
            }
        }
        protected void BtnAddNewCompany_Click(object sender, EventArgs e)
        {
            DataTable ldt = new DataTable();
            ldt = mobjCompanyBLL.GetNewCompanyCode();
            txtCompanyCode.Text = ldt.Rows[0]["CompanyCode"].ToString();
            txtCompanyName.Text = string.Empty;
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
        }
        public void GetCompany()
        {
            var ldtCompany = mobjCompanyBLL.GetAllCompany();
            if (ldtCompany.Count > 0 && ldtCompany != null)
            {
                dgvCompany.DataSource = ldtCompany;
                dgvCompany.DataBind();
                int lintRowcount = ldtCompany.Count;
                lblRowCount.Text = "<b>Total Records:</b> " + lintRowcount.ToString();
                pnlShow.Style.Add(HtmlTextWriterStyle.Display, "");
            }
            else
            {
                pnlShow.Style.Add(HtmlTextWriterStyle.Display, "none");
                //hdnPanel.Value = "none";
            }
        }

        protected void dgvCompany_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                DataTable ldt = new DataTable();
                if (e.CommandName == "EditCompany")
                {
                    //this.programmaticModalPopupEdit.Show();
                    int linIndex = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                    LinkButton lnkCompanyCode = (LinkButton)gvr.FindControl("lnkCompanyCode");
                    string lstrCompanyCode = lnkCompanyCode.Text;
                    //txtEditCompanyCode.Text = lstrCompanyCode;
                    ldt = mobjCompanyBLL.GetCompanyForEdit(lstrCompanyCode);
                    FillControls(ldt);
                }
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmCompanyMaster -  dgvCompany_RowCommand(object sender, GridViewCommandEventArgs e)", ex);
            }
        }

        private void FillControls(DataTable ldt)
        {
            //txtEditCompanyCode.Text=ldt.Rows[0]["CompanyCode"].ToString();
            //txtEditCompanyName.Text = ldt.Rows[0]["CompanyName"].ToString();
            //txtEditAddress.Text=ldt.Rows[0]["Address"].ToString();
            //txtEditPhoneNo.Text = ldt.Rows[0]["PhoneNo"].ToString();
            //txtEditMobileNo.Text = ldt.Rows[0]["MobileNo"].ToString();
            //txtEditVATCSTNo.Text = ldt.Rows[0]["VATCSTNo"].ToString();
            //txtEditExciseNo.Text = ldt.Rows[0]["ExciseNo"].ToString();
            //txtEditEmail.Text = ldt.Rows[0]["Email"].ToString();
            //txtEditServiceTaxNo.Text = ldt.Rows[0]["ServiceTaxNo"].ToString();

        }

        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            int lintCnt = 0;
            try
            {
                EntityCompany entCompany = new EntityCompany();

                entCompany.CompanyCode = txtCompanyCode.Text;
                entCompany.CompanyName = txtCompanyName.Text;
                entCompany.Address = txtAddress.Text;
                entCompany.PhoneNo = txtPhoneNo.Text;
                entCompany.MobileNo = txtMobileNo.Text;
                entCompany.VATCSTNo = txtVATCSTNo.Text;
                entCompany.ExciseNo = txtExciseNo.Text;
                entCompany.Email = txtEmail.Text;
                entCompany.ServiceTaxNo = txtServiceTaxNo.Text;
                entCompany.ChangeBy = SessionManager.Instance.LoginUser.EmpCode;
                //if (mobjCompanyBLL.GetCompany(entCompany) != null)
                //{
                lintCnt = mobjCompanyBLL.UpdateCompany(entCompany);
                if (lintCnt > 0)
                {
                    GetCompany();
                    lblMessage.Text = "Record Updated Successfully";
                    //this.programmaticModalPopup.Hide();
                }
                else
                {
                    lblMessage.Text = "Record Not Updated";
                }
                //}
                //else
                //{
                //    lblMessage.Text = "Record Already Exist...";
                //}

            }
            catch (Exception ex)
            {
                Commons.FileLog("frmCompanyMaster -  BtnEdit_Click(object sender, EventArgs e)", ex);
            }
            MultiView1.SetActiveView(View1);
        }

        protected void chkDelete_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chk.NamingContainer;
            if (chk.Checked)
            {
                LinkButton CompanyCode = (LinkButton)row.FindControl("lnkCompanyCode");
                Session["CompanyCode"] = CompanyCode.Text;
            }
            else
            {
                Session["CompanyCode"] = string.Empty;
            }
        }
        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow drv in dgvCompany.Rows)
            {
                CheckBox chkDelete = (CheckBox)drv.FindControl("chkDelete");
                if (chkDelete.Checked)
                {
                    //this.modalpopupDelete.Show();
                }
            }
        }

        protected void BtnDeleteOk_Click(object sender, EventArgs e)
        {
            EntityCompany entCompany = new EntityCompany();
            int cnt = 0;

            try
            {
                foreach (GridViewRow drv in dgvCompany.Rows)
                {
                    CheckBox chkDelete = (CheckBox)drv.FindControl("chkDelete");
                    if (chkDelete.Checked)
                    {
                        LinkButton lnkCompanyCode = (LinkButton)drv.FindControl("lnkCompanyCode");
                        string lstrCompanyCode = lnkCompanyCode.Text;
                        entCompany.CompanyCode = lstrCompanyCode;

                        cnt = mobjCompanyBLL.DeleteCompany(entCompany);
                        if (cnt > 0)
                        {
                            //this.modalpopupDelete.Hide();

                            lblMessage.Text = "Record Deleted Successfully....";

                            if (dgvCompany.Rows.Count <= 0)
                            {
                                pnlShow.Style.Add(HtmlTextWriterStyle.Display, "none");
                                //hdnPanel.Value = "none";
                            }
                        }
                        else
                        {
                            lblMessage.Text = "Record Not Deleted....";
                        }
                    }
                }
                GetCompany();
            }
            catch (System.Threading.ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                Commons.FileLog("frmCompanyMaster -   BtnDeleteOk_Click(object sender, EventArgs e)", ex);
            }
        }

        protected void dgvCompany_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvCompany.PageIndex = e.NewPageIndex;
        }

        protected void dgvCompany_RowDataBound(object sender, GridViewRowEventArgs e)
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

                }

            }
            catch (Exception ex)
            {
                Commons.FileLog("frmCompanyMaster -  dgvCompany_RowDataBound(object sender, GridViewRowEventArgs e)", ex);
            }
        }

        protected void dgvCompany_DataBound(object sender, EventArgs e)
        {
            int lintCurrentPage = dgvCompany.PageIndex + 1;
            lblPageCount.Text = "<b>Page</b> " + lintCurrentPage.ToString() + "<b> of </b>" + dgvCompany.PageCount.ToString();
        }

        protected void dgvCompany_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var ldtCompany = mobjCompanyBLL.GetAllCompany();
                if (!string.IsNullOrEmpty(txtSearch.Text))
                {
                    ldtCompany = ldtCompany.Where(p => p.CompanyName.Contains(txtSearch.Text) || p.Address.Contains(txtSearch.Text)).ToList();
                }
                dgvCompany.DataSource = ldtCompany;// (DataTable)Session["CompanyDetails"];
                dgvCompany.DataBind();
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmCompanyMaster - dgvCompany_PageIndexChanged(object sender, EventArgs e)", ex);
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton imgEdit = (ImageButton)sender;
                GridViewRow cnt = (GridViewRow)imgEdit.NamingContainer;
                txtCompanyCode.Text = Convert.ToString(cnt.Cells[0].Text);
                txtCompanyName.Text = Convert.ToString(cnt.Cells[1].Text);
                txtAddress.Text = Convert.ToString(cnt.Cells[2].Text);
                txtPhoneNo.Text = Convert.ToString(cnt.Cells[3].Text);
                txtMobileNo.Text = Convert.ToString(cnt.Cells[4].Text);
                txtVATCSTNo.Text = Convert.ToString(cnt.Cells[5].Text);
                txtExciseNo.Text = Convert.ToString(cnt.Cells[6].Text);
                txtEmail.Text = Convert.ToString(cnt.Cells[7].Text);
                txtServiceTaxNo.Text = Convert.ToString(cnt.Cells[8].Text);
                BtnSave.Visible = false;
                btnUpdate.Visible = true;
                MultiView1.SetActiveView(View2);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void BtnClose_Click(object sender, EventArgs e)
        {
            try
            {
                txtCompanyCode.Text = String.Empty;
                txtCompanyName.Text = String.Empty;
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
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            int lintcnt = 0;
            EntityCompany entCompany = new EntityCompany();
            if (string.IsNullOrEmpty(txtCompanyCode.Text.Trim()))
            {
                lblMsg.Text = "Please Enter Discount Code";
            }
            else
            {
                if (string.IsNullOrEmpty(txtCompanyName.Text.Trim()))
                {
                    lblMsg.Text = "Please Enter Discount Description";
                }
                else
                {
                    entCompany.CompanyCode = txtCompanyCode.Text.Trim();
                    entCompany.CompanyName = txtCompanyName.Text.Trim();
                    entCompany.Address = txtAddress.Text.Trim();
                    entCompany.PhoneNo = txtPhoneNo.Text.Trim();
                    entCompany.MobileNo = txtMobileNo.Text.Trim();
                    entCompany.VATCSTNo = txtVATCSTNo.Text.Trim();
                    entCompany.ExciseNo = txtExciseNo.Text.Trim();
                    entCompany.Email = txtEmail.Text.Trim();
                    entCompany.ServiceTaxNo = txtServiceTaxNo.Text.Trim();
                    entCompany.EntryBy = SessionManager.Instance.LoginUser.EmpCode;

                    //if (!Commons.IsRecordExists("tblCompanyMaster", "VATCSTNo", entCompany.VATCSTNo))
                    //{
                    lintcnt = mobjCompanyBLL.InsertCompany(entCompany);
                    if (lintcnt > 0)
                    {
                        GetCompany();
                        lblMessage.Text = "Record Inserted Successfully";
                        //this.programmaticModalPopup.Hide();
                    }
                    else
                    {
                        lblMessage.Text = "Record Not Inserted";
                    }
                    //}
                    //else
                    //{
                    //    lblMessage.Text = "Record Already Exist...";
                    //}

                }
                MultiView1.SetActiveView(View1);
            }

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSearch.Text))
                {
                    SearchCompanyDetails(txtSearch.Text);
                }
                else
                {
                    lblMessage.Text = "Please Fill search Text.";
                    txtSearch.Focus();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void SearchCompanyDetails(string Prefix)
        {
            List<EntityCompany> lst = mobjCompanyBLL.SelectCompanyDetails(Prefix);
            if (lst != null)
            {
                dgvCompany.DataSource = lst;
                dgvCompany.DataBind();
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtSearch.Text = string.Empty;
            GetCompany();
        }
    }
}