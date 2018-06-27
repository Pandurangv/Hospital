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
    public partial class frmProductMaster : BasePage
    {

        ProductBLL mobjProductBLL = new ProductBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            base.AuthenticateUser();
            if (!Page.IsPostBack)
            {
                GetProduct();
                MultiView1.SetActiveView(View1);
            }
        }
        protected void BtnAddNewProduct_Click(object sender, EventArgs e)
        {
            EntityProduct entProduct = new EntityProduct();
            int ID = mobjProductBLL.GetNewProductId();
            txtProductId.Text = Convert.ToString(ID);
            txtProductName.Text = string.Empty;
            txtUOM.Text = string.Empty;
            txtSubUOM.Text = string.Empty;
            txtPrice.Text = string.Empty;

            BtnSave.Visible = true;
            btnUpdate.Visible = false;
            MultiView1.SetActiveView(View2);
        }
        public void GetProduct()
        {
            var ldtProduct = mobjProductBLL.GetAllProduct();
            if (ldtProduct.Count > 0 && ldtProduct != null)
            {
                dgvProduct.DataSource = ldtProduct;
                dgvProduct.DataBind();
                int lintRowcount = ldtProduct.Count;
                lblRowCount.Text = "<b>Total Records:</b> " + lintRowcount.ToString();
                pnlShow.Style.Add(HtmlTextWriterStyle.Display, "");
            }
            else
            {
                pnlShow.Style.Add(HtmlTextWriterStyle.Display, "none");
            }
        }

        protected void dgvProduct_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                DataTable ldt = new DataTable();
                if (e.CommandName == "EditProduct")
                {
                    int linIndex = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                    LinkButton lnkProductId = (LinkButton)gvr.FindControl("lnkProductId");
                    string lstrProductId = lnkProductId.Text;
                    ldt = mobjProductBLL.GetProductForEdit(lstrProductId);
                }
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmProductMaster -  dgvProduct_RowCommand(object sender, GridViewCommandEventArgs e)", ex);
            }
        }


        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            int lintCnt = 0;
            try
            {
                EntityProduct entProduct = new EntityProduct();

                entProduct.ProductId = Convert.ToInt32(txtProductId.Text);
                if (string.IsNullOrEmpty(txtProductName.Text))
                {
                    lblMsg.Text = "Please Enter Product Name";
                    txtProductName.Focus();
                    return;
                }
                else
                {
                    entProduct.ProductName = txtProductName.Text;
                }
                entProduct.UOM = txtUOM.Text;
                entProduct.SubUOM = txtSubUOM.Text;
                entProduct.Price = Convert.ToDecimal(txtPrice.Text);
                if (!Commons.IsRecordExists("tblProductMaster", "ProductName", Convert.ToString(entProduct.ProductName)))
                {
                    lintCnt = mobjProductBLL.UpdateProduct(entProduct);

                    if (lintCnt > 0)
                    {
                        GetProduct();
                        lblMessage.Text = "Record Updated Successfully";
                        //this.programmaticModalPopup.Hide();
                    }
                    else
                    {
                        lblMessage.Text = "Record Not Updated";
                    }
                }
                else
                {
                    lblMessage.Text = "Record Already Exist";
                }
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmProductMaster -  BtnEdit_Click(object sender, EventArgs e)", ex);
            }
            MultiView1.SetActiveView(View1);
        }

        protected void chkDelete_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chk.NamingContainer;
            if (chk.Checked)
            {
                LinkButton lnkProductId = (LinkButton)row.FindControl("lnkProductId");
                ProductId.Value = lnkProductId.Text;
            }
            else
            {
                ProductId.Value = string.Empty;
            }
        }
        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow drv in dgvProduct.Rows)
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
            //EntityProduct entProduct = new EntityProduct();
            //int cnt = 0;

            //try
            //{
            //    foreach (GridViewRow drv in dgvProduct.Rows)
            //    {
            //        CheckBox chkDelete = (CheckBox)drv.FindControl("chkDelete");
            //        if (chkDelete.Checked)
            //        {
            //            LinkButton lnkProductId = (LinkButton)drv.FindControl("lnkProductId");
            //            string lstrProductId = lnkProductId.Text;
            //            entProduct.ProductId = lstrProductId;

            //            cnt = mobjProductBLL.DeleteProduct(entProduct);
            //            if (cnt > 0)
            //            {
            //                //this.modalpopupDelete.Hide();

            //                Commons.ShowMessage("Record Deleted Successfully....", this.Page);

            //                if (dgvProduct.Rows.Count <= 0)
            //                {
            //                    pnlShow.Style.Add(HtmlTextWriterStyle.Display, "none");
            //                    //hdnPanel.Value = "none";
            //                }
            //            }
            //            else
            //            {
            //                Commons.ShowMessage("Record Not Deleted....", this.Page);
            //            }
            //        }
            //    }
            //    GetProduct();
            //}
            //catch (System.Threading.ThreadAbortException)
            //{

            //}
            //catch (Exception ex)
            //{
            //    Commons.FileLog("frmProductMaster -   BtnDeleteOk_Click(object sender, EventArgs e)", ex);
            //}
        }

        protected void dgvProduct_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvProduct.PageIndex = e.NewPageIndex;
        }

        protected void dgvProduct_RowDataBound(object sender, GridViewRowEventArgs e)
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
                Commons.FileLog("frmProductMaster -  dgvProduct_RowDataBound(object sender, GridViewRowEventArgs e)", ex);
            }
        }

        protected void dgvProduct_DataBound(object sender, EventArgs e)
        {
            int lintCurrentPage = dgvProduct.PageIndex + 1;
            lblPageCount.Text = "<b>Page</b> " + lintCurrentPage.ToString() + "<b> of </b>" + dgvProduct.PageCount.ToString();
        }

        protected void dgvProduct_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var ldtProduct = mobjProductBLL.GetAllProduct();
                dgvProduct.DataSource = ldtProduct;// (DataTable)Session["ProductDetails"];
                dgvProduct.DataBind();
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmProductMaster - dgvProduct_PageIndexChanged(object sender, EventArgs e)", ex);
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton imgEdit = (ImageButton)sender;
                GridViewRow cnt = (GridViewRow)imgEdit.NamingContainer;
                txtProductId.Text = Convert.ToString(cnt.Cells[0].Text);
                txtProductName.Text = Convert.ToString(cnt.Cells[1].Text);
                txtUOM.Text = Convert.ToString(cnt.Cells[2].Text);
                txtSubUOM.Text = Convert.ToString(cnt.Cells[3].Text);
                txtPrice.Text = Convert.ToString(cnt.Cells[4].Text);

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
                txtProductId.Text = String.Empty;
                txtProductName.Text = String.Empty;
                txtUOM.Text = String.Empty;
                txtSubUOM.Text = String.Empty;
                txtPrice.Text = String.Empty;

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
            EntityProduct entProduct = new EntityProduct();
            if (string.IsNullOrEmpty(txtProductName.Text.Trim()))
            {
                lblMsg.Text = "Enter Product Description";
                txtProductName.Focus();
                return;
            }
            else
            {
                entProduct.ProductId = Convert.ToInt32(txtProductId.Text);
                entProduct.ProductName = txtProductName.Text.Trim();
                entProduct.UOM = txtUOM.Text.Trim();
                entProduct.SubUOM = txtSubUOM.Text.Trim();
                entProduct.Price = Convert.ToDecimal(txtPrice.Text);
                if (!Commons.IsRecordExists("tblProductMaster", "ProductName", Convert.ToString(entProduct.ProductName)))
                {
                    lintcnt = mobjProductBLL.InsertProduct(entProduct);

                    if (lintcnt > 0)
                    {
                        GetProduct();
                        lblMessage.Text = "Record Inserted Successfully";
                        //this.programmaticModalPopup.Hide();
                    }
                    else
                    {
                        lblMessage.Text = "Record Not Inserted";
                    }
                }
                else
                {
                    lblMessage.Text = "Record Already Exist";
                }
            }
            MultiView1.SetActiveView(View1);


        }
        protected void dgvProduct_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}