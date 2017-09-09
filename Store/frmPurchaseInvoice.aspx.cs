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

namespace Hospital.Store
{
    public partial class frmPurchaseInvoice : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (SessionManager.Instance.LoginUser != null)
            {
                if (!Page.IsPostBack)
                {
                    //SessionManager.Instance.SetSession();
                    Session["MyFlag"] = string.Empty;
                    GetPurchaseInvoice();
                    BindSupplierList();
                    BindProductList();
                    ddlSupplier.Enabled = false;
                    calExpiryDate.StartDate = DateTime.Now.AddDays(10).Date;
                    MultiView1.SetActiveView(View1);
                }
            }
            else
            {
                Response.Redirect("~/frmlogin.aspx", false);
            }
        }


        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                List<EntityPurchaseInvoiceDetails> lst = (List<EntityPurchaseInvoiceDetails>)Session["PIDetails"];
                List<EntityPurchaseInvoiceDetails> lstFinal = new List<EntityPurchaseInvoiceDetails>();
                ImageButton imgEdit = (ImageButton)sender;
                GridViewRow row = (GridViewRow)imgEdit.NamingContainer;
                if (lst != null)
                {
                    foreach (EntityPurchaseInvoiceDetails item in lst)
                    {
                        if (Convert.ToInt32(row.Cells[0].Text) != item.ProductCode)
                        {
                            lstFinal.Add(item);
                        }
                    }
                    Session["PIDetails"] = lstFinal;
                    GridView1.DataSource = lstFinal;
                    GridView1.DataBind();
                    txtTotal.Text = Convert.ToString(lstFinal.Sum(p => p.Amount));
                    Calculation();
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void chkIsPO_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkIsPO.Checked)
                {
                    if (btnUpdate.Visible == false)
                    {
                        ddlSupplier.Enabled = false;
                        ddlPONumber.Enabled = true;
                    }
                    else
                    {
                        chkIsPO.Checked = false;
                        lblMsg.Text = "You can not set invoice with Purchase Order";
                    }
                }
                else
                {
                    ddlSupplier.Enabled = true;
                    ddlPONumber.Enabled = false;
                }
                BindPODropDown();
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void ddlPONumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindProductList(Convert.ToInt32(ddlPONumber.SelectedValue));
        }

        private void BindProductList(int PONo)
        {
            try
            {
                PurchaseInvoiceBLL mobjDeptBLL = new PurchaseInvoiceBLL();
                List<EntityProduct> lst = mobjDeptBLL.GetPOProduct(PONo);
                lst.Insert(0, new EntityProduct() { ProductId = 0, ProductName = "------Select------" });
                ddlProduct.DataSource = lst;
                ddlProduct.DataValueField = "ProductId";
                ddlProduct.DataTextField = "ProductName";
                ddlProduct.DataBind();
                if (PONo > 0)
                {
                    EntitySupplierMaster supp = mobjDeptBLL.GetSupplierByPO(PONo);
                    ListItem item = ddlSupplier.Items.FindByText(supp.SupplierName);
                    ddlSupplier.SelectedValue = item.Value;
                    //ddlSupplier.Enabled = false;
                }
                else
                {
                    ddlSupplier.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        private void BindSupplierList()
        {
            try
            {
                SupplierBLL mobjSupplier = new SupplierBLL();
                List<EntitySupplierMaster> lstOther = mobjSupplier.GetAllSupplier();
                lstOther.Insert(0, new EntitySupplierMaster { PKId = 0, SupplierName = "-----Select------" });
                ddlSupplier.DataSource = lstOther;

                ddlSupplier.DataValueField = "PKId";
                ddlSupplier.DataTextField = "SupplierName";
                ddlSupplier.DataBind();
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        private void BindProductList()
        {
            try
            {
                ProductBLL mobjProduct = new ProductBLL();
                List<EntityProduct> lst = mobjProduct.GetAllProducts().Where(p=>p.Category=="Store").ToList();
                lst.Insert(0, new EntityProduct() { ProductId = 0, ProductName = "------Select------" });
                ddlProduct.DataSource = lst;
                ddlProduct.DataValueField = "ProductId";
                ddlProduct.DataTextField = "ProductName";
                ddlProduct.DataBind();
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void BtnClose_Click(object sender, EventArgs e)
        {
            try
            {
                Clear();
                GetPurchaseInvoice();
                MultiView1.SetActiveView(View1);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            PurchaseInvoiceBLL mobjDeptBLL = new PurchaseInvoiceBLL();
            try
            {
                Session["MyFlag"] = "EDIT";
                ImageButton imgEdit = (ImageButton)sender;
                GridViewRow row = (GridViewRow)imgEdit.NamingContainer;
                Session["PO_ID"] = Convert.ToInt32(dgvTestParameter.DataKeys[row.RowIndex].Value);
                List<EntityPurchaseInvoice> tblEmp = mobjDeptBLL.SelectPurchaseInvoiceForEdit(Convert.ToInt32(Session["PO_ID"]));
                if (tblEmp.Count > 0)
                {
                    if (tblEmp[0].PONo > 0)
                    {
                        Session["PONO"] = tblEmp[0].PONo;
                        chkIsPO.Checked = true;
                        // ddlSupplier.Enabled = false;
                        btnAdd.Visible = true;
                        btnUpdateItem.Visible = false;
                        if (tblEmp[0].PONo > 0)
                        {
                            BindPODropDown(tblEmp[0].PONo);
                            ListItem item = ddlPONumber.Items.FindByText(Convert.ToString(tblEmp[0].PONo));
                            if (item != null)
                            {
                                ddlPONumber.SelectedValue = item.Value;
                                BindPOProductforEdit(Convert.ToInt32(ddlPONumber.SelectedValue));
                            }
                        }
                    }
                    else
                    {
                        chkIsPO.Checked = false;
                        ddlSupplier.Enabled = true;
                        ddlPONumber.Enabled = false;
                    }
                    ListItem itemSupplier = ddlSupplier.Items.FindByText(tblEmp[0].SupplierName);
                    if (itemSupplier != null)
                    {
                        ddlSupplier.SelectedValue = itemSupplier.Value;
                    }
                    txtPurchaseDate.Text = row.Cells[2].Text;
                    txtNetAmount.Text = row.Cells[3].Text;
                    txtDiscount.Text = Convert.ToString(tblEmp[0].Discount);
                    txtVAT.Text = Convert.ToString(tblEmp[0].Tax2);
                    txtService.Text = Convert.ToString(tblEmp[0].Tax1);
                    List<EntityPurchaseInvoiceDetails> lstInvoiceDetails = mobjDeptBLL.GetPurchaseInvoiceDetails(Convert.ToInt32(Session["PO_ID"]));
                    txtTotal.Text = Convert.ToString(lstInvoiceDetails.Sum(p => p.Amount));
                    Session["PIDetails"] = lstInvoiceDetails;
                    GridView1.DataSource = lstInvoiceDetails;
                    GridView1.DataBind();
                    Calculation();

                    btnUpdate.Visible = true;
                    BtnSave.Visible = false;
                    MultiView1.SetActiveView(View2);
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        private void BindPOProductforEdit(int PONo)
        {
            try
            {
                PurchaseInvoiceBLL mobjDeptBLL = new PurchaseInvoiceBLL();
                List<EntityProduct> lst = mobjDeptBLL.GetPOProductForEdit(PONo);
                lst.Insert(0, new EntityProduct() { ProductId = 0, ProductName = "------Select------" });
                ddlProduct.DataSource = lst;
                ddlProduct.DataValueField = "ProductId";
                ddlProduct.DataTextField = "ProductName";
                ddlProduct.DataBind();
                if (PONo > 0)
                {
                    EntitySupplierMaster supp = mobjDeptBLL.GetSupplierByPO(PONo);
                    ListItem item = ddlSupplier.Items.FindByText(supp.SupplierName);
                    ddlSupplier.SelectedValue = item.Value;
                }
                else
                {
                    ddlSupplier.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        private void BindPODropDown(int? PONo)
        {
            try
            {
                PurchaseOrderBLL mobjPO = new PurchaseOrderBLL();
                List<EntityPurchaseOrder> lst = mobjPO.GetPurchaseOderNotCompleted(PONo);
                lst.Insert(0, new EntityPurchaseOrder() { PO_Id = 0, VendorName = "-----Select-----" });
                ddlPONumber.DataSource = lst;
                ddlPONumber.DataTextField = "VendorName";
                ddlPONumber.DataValueField = "PO_Id";
                ddlPONumber.DataBind();
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void BtnAddNewDept_Click(object sender, EventArgs e)
        {
            try
            {
                Session["MyFlag"] = "ADD";
                Session["PIDetails"] = new List<EntityPurchaseInvoiceDetails>();
                chkIsPO.Checked = true;
                ddlPONumber.Enabled = true;
                BindPODropDown();
                CalPurchaseDate.StartDate = DateTime.Now.Date;
                lblMsg.Text = string.Empty;
                Clear();
                BtnSave.Visible = true;
                btnUpdate.Visible = false;
                btnAdd.Visible = true;
                btnUpdateItem.Visible = false;
                MultiView1.SetActiveView(View2);
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        private void BindPODropDown()
        {
            try
            {
                PurchaseOrderBLL mobjPO = new PurchaseOrderBLL();
                List<EntityPurchaseOrder> lst = mobjPO.GetPurchaseOderNotCompleted();
                lst.Insert(0, new EntityPurchaseOrder() { PO_Id = 0, VendorName = "-----Select-----" });
                ddlPONumber.DataSource = lst;
                ddlPONumber.DataTextField = "VendorName";
                ddlPONumber.DataValueField = "PO_Id";
                ddlPONumber.DataBind();
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        private void Clear()
        {
            try
            {
                ddlSupplier.SelectedIndex = 0;
                ClearOther();
                txtPurchaseDate.Text = string.Empty;
                GridView1.DataSource = new List<EntityPurchaseOrderDetails>();
                GridView1.DataBind();
                txtTotal.Text = string.Empty;
                txtSearch.Text = string.Empty;
                txtService.Text = string.Empty;
                txtNetAmount.Text = string.Empty;
                txtVAT.Text = string.Empty;
                txtDiscount.Text = string.Empty;
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        private void GetPurchaseInvoice()
        {
            try
            {
                PurchaseInvoiceBLL mobjDeptBLL = new PurchaseInvoiceBLL();
                List<EntityPurchaseInvoice> ldtOpera = mobjDeptBLL.GetPurchaseInvoices();
                if (ldtOpera.Count > 0)
                {
                    dgvTestParameter.DataSource = ldtOpera;
                    dgvTestParameter.DataBind();
                    Session["DepartmentDetail"] = ldtOpera;
                    int lintRowcount = ldtOpera.Count;
                    lblRowCount.Text = "<b>Total Records:</b> " + lintRowcount.ToString();
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                PurchaseInvoiceBLL mobjDeptBLL = new PurchaseInvoiceBLL();
                List<EntityPurchaseInvoice> ldtOpera = mobjDeptBLL.GetPurchaseInvoices(txtSearch.Text);
                dgvTestParameter.DataSource = ldtOpera;
                dgvTestParameter.DataBind();
                Session["DepartmentDetail"] = ldtOpera;
                int lintRowcount = ldtOpera.Count;
                lblRowCount.Text = "<b>Total Records:</b> " + lintRowcount.ToString();
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
                GetPurchaseInvoice();
                txtSearch.Text = string.Empty;
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void btnEditItem_Click(object sender, EventArgs e)
        {
            try
            {
                PurchaseInvoiceBLL mobjDeptBLL = new PurchaseInvoiceBLL();
                ImageButton imgEdit = (ImageButton)sender;
                GridViewRow row = (GridViewRow)imgEdit.NamingContainer;
                btnAdd.Visible = false;
                btnUpdateItem.Visible = true;
                if (row != null)
                {
                    hdnRowIndex.Value = Convert.ToString(row.RowIndex);
                    ListItem item = ddlProduct.Items.FindByText(row.Cells[1].Text);
                    ddlProduct.SelectedValue = item.Value;
                    ddlProduct_SelectedIndexChanged1(sender, e);
                    txtBatch.Text = row.Cells[3].Text;
                    txtExpiryDt.Text = row.Cells[4].Text;
                    txtItemCharge.Text = (row.Cells[5].Text);
                    txtQuantity.Text = row.Cells[2].Text;
                    EntityPurchaseOrderDetails poQty = mobjDeptBLL.GetPOQty(Convert.ToInt32(Session["PO_ID"]), Convert.ToInt32(ddlProduct.SelectedValue));
                    if (poQty != null)
                    {
                        txtPOQty.Text = Convert.ToString(poQty.Quantity);
                    }
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void btnUpdateItem_Click(object sender, EventArgs e)
        {
            try
            {
                List<EntityPurchaseInvoiceDetails> lst = (List<EntityPurchaseInvoiceDetails>)Session["PIDetails"];
                if (lst != null)
                {
                    bool flagTest = false;
                    for (int i = 0; i < lst.Count; i++)
                    {
                        if (i != Convert.ToInt32(hdnRowIndex.Value))
                        {
                            if (lst[i].ProductCode == Convert.ToInt32(ddlProduct.SelectedValue))
                            {
                                flagTest = true;
                                break;
                            }
                        }
                    }
                    if (flagTest)
                    {
                        lblMsg.Text = "You can not change Product.";
                    }
                    else
                    {
                        for (int i = 0; i < lst.Count; i++)
                        {
                            if (lst[i].ProductCode == Convert.ToInt32(ddlProduct.SelectedValue))
                            {
                                lst[i].ProductCode = Convert.ToInt32(ddlProduct.SelectedValue);
                                lst[i].BatchNo = txtBatch.Text;
                                lst[i].ExpiryDate = StringExtension.ToDateTime(txtExpiryDt.Text);
                                lst[i].ProductName = ddlProduct.SelectedItem.Text;
                                lst[i].InvoiceQty = Convert.ToInt32(txtQuantity.Text);
                                lst[i].Amount = Convert.ToDecimal(txtItemCharge.Text) * (Convert.ToInt32(txtQuantity.Text));
                                lst[i].InvoicePrice = Convert.ToDecimal(txtItemCharge.Text);
                            }
                        }
                    }
                    Session["PIDetails"] = lst;
                    GridView1.DataSource = lst;
                    GridView1.DataBind();
                    txtTotal.Text = Convert.ToString(lst.Sum(p => p.Amount));
                    ClearOther();
                    Calculation();
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                PurchaseInvoiceBLL mobjDeptBLL = new PurchaseInvoiceBLL();
                int Invoice = 0;
                EntityPurchaseInvoice entPurchaseInvoice = new EntityPurchaseInvoice();
                if (ddlSupplier.SelectedIndex == 0)
                {
                    lblMsg.Text = "Please Select Supplier Name";
                    ddlSupplier.Focus();
                    return;
                }
                else
                {
                    if (GridView1.Rows.Count > 0)
                    {
                        if (string.IsNullOrEmpty(txtPurchaseDate.Text.Trim()))
                        {
                            lblMsg.Text = "Please Select Purchase Order Date";
                            CalPurchaseDate.Focus();
                            return;
                        }
                        else
                        {
                            entPurchaseInvoice.SupplierId = Convert.ToInt32(ddlSupplier.SelectedValue);
                            entPurchaseInvoice.Amount = Convert.ToDecimal(txtNetAmount.Text);
                            entPurchaseInvoice.PIDate = StringExtension.ToDateTime(txtPurchaseDate.Text);
                            if (!string.IsNullOrEmpty(txtDiscount.Text))
                            {
                                entPurchaseInvoice.Discount = Convert.ToInt32(txtDiscount.Text);
                            }
                            if (!string.IsNullOrEmpty(txtService.Text))
                            {
                                entPurchaseInvoice.Tax2 = Convert.ToInt32(txtService.Text);
                            }
                            entPurchaseInvoice.Tax1 = Convert.ToInt32(txtVAT.Text);
                            List<EntityPurchaseInvoiceDetails> lstInvoice = (List<EntityPurchaseInvoiceDetails>)Session["PIDetails"];
                            if (chkIsPO.Checked)
                            {
                                entPurchaseInvoice.PONo = Convert.ToInt32(ddlPONumber.SelectedValue);
                            }
                            else
                            {
                                entPurchaseInvoice.PONo = 0;
                            }
                            Invoice = mobjDeptBLL.InsertPurchaseInvoice(entPurchaseInvoice, lstInvoice);
                            if (Invoice > 0)
                            {
                                GetPurchaseInvoice();
                                lblMessage.Text = "Record Inserted Successfully....";
                                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                            }
                            else
                            {
                                lblMessage.Text = "Record Not Inserted...";
                            }
                            Session["PIDetails"] = new List<EntityPurchaseOrderDetails>();
                        }
                    }
                    else
                    {
                        lblMessage.Text = "Please Add Products in List..";
                    }
                }
                MultiView1.SetActiveView(View1);
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["PO_ID"] != null)
                {
                    int PINo = Convert.ToInt32(Session["PO_ID"]);
                    List<EntityPurchaseInvoiceDetails> lst = (List<EntityPurchaseInvoiceDetails>)Session["PIDetails"];
                    if (lst != null)
                    {
                        EntityPurchaseInvoice invoice = new EntityPurchaseInvoice()
                        {
                            PINo = PINo,
                            SupplierId = Convert.ToInt32(ddlSupplier.SelectedValue),
                            Amount = Convert.ToDecimal(txtNetAmount.Text),
                            PIDate = StringExtension.ToDateTime(txtPurchaseDate.Text),
                            Tax1 = Convert.ToInt32(txtService.Text),
                            Tax2 = Convert.ToInt32(txtVAT.Text),
                            Discount = Convert.ToInt32(txtDiscount.Text)
                        };
                        if (chkIsPO.Checked)
                        {
                            invoice.PONo = Convert.ToInt32(Session["PONO"]);
                        }
                        else
                        {
                            invoice.PONo = 0;
                        }
                        PurchaseInvoiceBLL mobjDeptBLL = new PurchaseInvoiceBLL();
                        int i = mobjDeptBLL.Update(invoice, lst);
                        if (i > 0)
                        {
                            GetPurchaseInvoice();
                            btnCancel_Click(sender, e);
                            Session["PO_ID"] = null;
                            Session["PIDetails"] = null;
                            MultiView1.SetActiveView(View1);
                        }
                        else
                        {
                            lblMsg.Text = "Record Not Updated";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }


        protected void dgvTestParameter_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dgvTestParameter.DataSource = (List<EntityPurchaseInvoice>)Session["DepartmentDetail"];
                dgvTestParameter.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void dgvTestParameter_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvTestParameter.PageIndex = e.NewPageIndex;
        }

        protected void btnAddCharge_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlProduct.SelectedIndex > 0)
                {
                    if (!string.IsNullOrEmpty(txtQuantity.Text))
                    {
                        if (DateTime.Now.Date.AddMonths(2).Date.CompareTo(StringExtension.ToDateTime(txtExpiryDt.Text)) == 1)
                        {
                            lblMsg.Text = "You can not Short expiry Products.";
                        }
                        else
                        {
                            lblMsg.Text = string.Empty;
                            List<EntityPurchaseInvoiceDetails> lst = (List<EntityPurchaseInvoiceDetails>)Session["PIDetails"];

                            int cnt = (from tbl in lst
                                       where tbl.ProductCode == Convert.ToInt32(ddlProduct.SelectedValue)
                                       && tbl.BatchNo.Equals(txtBatch.Text)
                                       select tbl).ToList().Count;
                            if (cnt > 0)
                            {
                                lblMsg.Text = "This product is already added...";
                            }
                            else
                            {
                                lst.Add(new EntityPurchaseInvoiceDetails() { ProductCode = Convert.ToInt32(ddlProduct.SelectedValue), BatchNo = txtBatch.Text, ExpiryDate = StringExtension.ToDateTime(txtExpiryDt.Text), ProductName = ddlProduct.SelectedItem.Text, InvoiceQty = Convert.ToInt32(txtQuantity.Text), Amount = Convert.ToDecimal(txtItemCharge.Text) * (Convert.ToInt32(txtQuantity.Text)), InvoicePrice = Convert.ToDecimal(txtItemCharge.Text) });
                                Session["PIDetails"] = lst;
                                GridView1.DataSource = lst;
                                GridView1.DataBind();
                                txtTotal.Text = Convert.ToString(lst.Sum(p => p.Amount));
                                ClearOther();
                                Calculation();
                            }
                        }


                    }
                }
                else
                {
                    lblMessage.Text = "Please Select Patient Name";
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        private void ClearOther()
        {
            try
            {
                txtItemCharge.Text = Convert.ToString(0);
                // txtItemCharge.Enabled = false;
                txtItemCharge.Text = string.Empty;
                ddlProduct.SelectedIndex = 0;
                txtQuantity.Text = string.Empty;
                txtExpiryDt.Text = string.Empty;
                txtPOQty.Text = string.Empty;
                txtBatch.Text = string.Empty;
                lblMsg.Text = string.Empty;
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        private void Calculation()
        {
            try
            {
                if (string.IsNullOrEmpty(txtDiscount.Text) && string.IsNullOrEmpty(txtVAT.Text) && string.IsNullOrEmpty(txtService.Text))
                {
                    txtNetAmount.Text = txtTotal.Text;
                }
                else if (!string.IsNullOrEmpty(txtDiscount.Text) && string.IsNullOrEmpty(txtVAT.Text) && string.IsNullOrEmpty(txtService.Text))
                {
                    txtNetAmount.Text = Convert.ToString(Convert.ToDecimal(txtTotal.Text) - (Convert.ToDecimal(txtTotal.Text) * Convert.ToDecimal(txtDiscount.Text) / 100));
                }
                else if (string.IsNullOrEmpty(txtDiscount.Text) && !string.IsNullOrEmpty(txtVAT.Text) && string.IsNullOrEmpty(txtService.Text))
                {
                    txtNetAmount.Text = Convert.ToString(Convert.ToDecimal(txtTotal.Text) + (Convert.ToDecimal(txtTotal.Text) * Convert.ToDecimal(txtVAT.Text) / 100));
                }
                else if (string.IsNullOrEmpty(txtDiscount.Text) && string.IsNullOrEmpty(txtVAT.Text) && !string.IsNullOrEmpty(txtService.Text))
                {
                    txtNetAmount.Text = Convert.ToString(Convert.ToDecimal(txtTotal.Text) + (Convert.ToDecimal(txtTotal.Text) * Convert.ToDecimal(txtService.Text) / 100));
                }
                else if (!string.IsNullOrEmpty(txtDiscount.Text) && !string.IsNullOrEmpty(txtVAT.Text) && string.IsNullOrEmpty(txtService.Text))
                {
                    decimal TotalDis = Convert.ToDecimal(txtTotal.Text) - (Convert.ToDecimal(txtTotal.Text) * Convert.ToDecimal(txtDiscount.Text) / 100);
                    txtNetAmount.Text = Convert.ToString(TotalDis + (TotalDis * Convert.ToDecimal(txtVAT.Text) / 100));
                }
                else if (!string.IsNullOrEmpty(txtDiscount.Text) && string.IsNullOrEmpty(txtVAT.Text) && !string.IsNullOrEmpty(txtService.Text))
                {
                    decimal TotalDis = Convert.ToDecimal(txtTotal.Text) - (Convert.ToDecimal(txtTotal.Text) * Convert.ToDecimal(txtDiscount.Text) / 100);
                    txtNetAmount.Text = Convert.ToString(TotalDis + (TotalDis * Convert.ToDecimal(txtService.Text) / 100));
                }
                else if (string.IsNullOrEmpty(txtDiscount.Text) && !string.IsNullOrEmpty(txtVAT.Text) && !string.IsNullOrEmpty(txtService.Text))
                {
                    decimal TotalDis = Convert.ToDecimal(txtTotal.Text) + (Convert.ToDecimal(txtTotal.Text) * Convert.ToDecimal(txtVAT.Text) / 100);
                    txtNetAmount.Text = Convert.ToString(TotalDis + (TotalDis * Convert.ToDecimal(txtService.Text) / 100));
                }
                else
                {
                    decimal TotalDis = Convert.ToDecimal(txtTotal.Text) - (Convert.ToDecimal(txtTotal.Text) * Convert.ToDecimal(txtDiscount.Text) / 100);
                    decimal TotalVat = TotalDis + (TotalDis * Convert.ToDecimal(txtVAT.Text) / 100);
                    txtNetAmount.Text = Convert.ToString(TotalVat + (TotalVat * Convert.ToDecimal(txtService.Text) / 100));
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearOther();
        }

        protected void ddlOther_SelectedIndexChanged(object sender, EventArgs e)
        {
            Calculation();
        }

        protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            PurchaseInvoiceBLL mobjDeptBLL = new PurchaseInvoiceBLL();
            try
            {
                if (ddlPONumber.SelectedIndex > 0)
                {
                    EntityPurchaseOrderDetails item = mobjDeptBLL.GetItemDetails(Convert.ToInt32(ddlPONumber.SelectedValue), Convert.ToInt32(ddlProduct.SelectedValue));
                    txtItemCharge.Text = Convert.ToString(decimal.Round(item.Rate.Value, 2));
                    txtPOQty.Text = Convert.ToString(item.Quantity);
                    ddlProduct.AutoPostBack = true;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
        protected void ddlProduct_SelectedIndexChanged1(object sender, EventArgs e)
        {
            try
            {
                ddlProduct_SelectedIndexChanged(sender, e);
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }
    }
}