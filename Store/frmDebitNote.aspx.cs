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
    public partial class frmDebitNote : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (SessionManager.Instance.LoginUser != null)
            {
                if (!Page.IsPostBack)
                {
                    //SessionManager.Instance.SetSession();
                    Session["MyFlag"] = string.Empty;
                    GetDebitNote();
                    BindSupplierList();
                    BindProductList();
                    MultiView1.SetActiveView(View1);
                }
            }
            else
            {
                Response.Redirect("~/frmlogin.aspx", false);
            }
        }

        private void BindProductList()
        {
            try
            {
                ProductBLL mobjProduct = new ProductBLL();
                List<EntityProduct> lst = mobjProduct.GetAllProducts();
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

        protected void BtnAddNewDept_Click(object sender, EventArgs e)
        {
            try
            {
                Session["BillDetails"] = new List<EntityDebitNoteDetails>();
                CalPurchaseDate.StartDate = DateTime.Now.Date;
                btnUpdate.Visible = false;
                btnUpdateItem.Visible = false;
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
            MultiView1.SetActiveView(View2);
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int Invoice = 0;
                DebitNoteBLL mobjDeptBLL = new DebitNoteBLL();
                EntityDebitNote entDebitNote = new EntityDebitNote();
                EntityDebitNoteDetails entDebitNoteDetails = new EntityDebitNoteDetails();
                if (ddlSupplier.SelectedIndex == 0)
                {
                    lblMsg.Text = "Please Select Supplier Name";
                    ddlSupplier.Focus();
                    return;
                }
                else
                {
                    if (string.IsNullOrEmpty(txtDebitDate.Text.Trim()))
                    {
                        lblMsg.Text = "Please Select Debit Note Date";
                        CalPurchaseDate.Focus();
                        return;
                    }
                    else
                    {
                        entDebitNote.SupplierId = Convert.ToInt32(ddlSupplier.SelectedValue);
                        entDebitNote.Amount = Convert.ToDecimal(txtTotal.Text);
                        entDebitNote.NetAmount = Convert.ToDecimal(txtNetAmount.Text);
                        entDebitNote.DNDate = StringExtension.ToDateTime(txtDebitDate.Text);

                        if (!string.IsNullOrEmpty(txtDiscount.Text))
                        {
                            entDebitNote.Discount = Convert.ToInt32(txtDiscount.Text);
                        }
                        if (!string.IsNullOrEmpty(txtService.Text))
                        {
                            entDebitNote.Tax2 = Convert.ToInt32(txtService.Text);
                        }
                        if (!string.IsNullOrEmpty(txtVAT.Text))
                        {
                            entDebitNote.Tax1 = Convert.ToInt32(txtVAT.Text);
                        }
                        List<EntityDebitNoteDetails> lstInvoice = (List<EntityDebitNoteDetails>)Session["BillDetails"];

                        Invoice = mobjDeptBLL.InsertPurchaseInvoice(entDebitNote, lstInvoice);
                        if (Invoice > 0)
                        {
                            GetDebitNote();
                            lblMessage.Text = "Record Inserted Successfully....";
                        }
                        else
                        {
                            lblMessage.Text = "Record Not Inserted...";
                        }
                        Session["BillDetails"] = new List<EntityDebitNoteDetails>();
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }

            MultiView1.SetActiveView(View1);
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                Session["MyFlag"] = "EDIT";
                DebitNoteBLL mobjDeptBLL = new DebitNoteBLL();
                ImageButton imgEdit = (ImageButton)sender;
                GridViewRow row = (GridViewRow)imgEdit.NamingContainer;
                Session["PO_ID"] = Convert.ToInt32(dgvTestParameter.DataKeys[row.RowIndex].Value);
                List<EntityDebitNote> tblEmp = mobjDeptBLL.SelectDebitNoteForEdit(Convert.ToInt32(Session["PO_ID"]));
                if (tblEmp.Count > 0)
                {

                    ListItem itemSupplier = ddlSupplier.Items.FindByText(tblEmp[0].SupplierName);
                    if (itemSupplier != null)
                    {
                        ddlSupplier.SelectedValue = itemSupplier.Value;
                    }
                    txtDebitDate.Text = row.Cells[2].Text;
                    txtNetAmount.Text = row.Cells[3].Text;
                    txtTotal.Text = row.Cells[4].Text;
                    txtDiscount.Text = Convert.ToString(tblEmp[0].Discount);
                    txtVAT.Text = Convert.ToString(tblEmp[0].Tax1);
                    txtService.Text = Convert.ToString(tblEmp[0].Tax2);
                    List<EntityDebitNoteDetails> lstInvoiceDetails = mobjDeptBLL.GetDebitNoteDetails(Convert.ToInt32(Session["PO_ID"]));
                    txtTotal.Text = Convert.ToString(lstInvoiceDetails.Sum(p => p.Amount));
                    Session["BillDetails"] = lstInvoiceDetails;
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

        protected void BtnEdit_Click(object sender, EventArgs e)
        {

            DebitNoteBLL mobjDeptBLL = new DebitNoteBLL();

            try
            {
                if (Session["PO_ID"] != null)
                {
                    int DNNo = Convert.ToInt32(Session["PO_ID"]);
                    List<EntityDebitNoteDetails> lst = (List<EntityDebitNoteDetails>)Session["BillDetails"];
                    if (lst != null)
                    {
                        EntityDebitNote invoice = new EntityDebitNote()
                        {
                            DNNo = DNNo,
                            SupplierId = Convert.ToInt32(ddlSupplier.SelectedValue),
                            NetAmount = Convert.ToDecimal(txtNetAmount.Text),
                            Amount = Convert.ToDecimal(txtTotal.Text),
                            DNDate = StringExtension.ToDateTime(txtDebitDate.Text),
                            Tax1 = Convert.ToInt32(txtService.Text),
                            Tax2 = Convert.ToInt32(txtVAT.Text),
                            Discount = Convert.ToInt32(txtDiscount.Text)
                        };

                        int i = mobjDeptBLL.Update(invoice, lst);
                        if (i > 0)
                        {
                            GetDebitNote();
                            btnCancel_Click(sender, e);
                            Session["PO_ID"] = null;
                            Session["BillDetails"] = null;
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

        protected void BtnClose_Click(object sender, EventArgs e)
        {
            try
            {
                Clear();
                GetDebitNote();
                MultiView1.SetActiveView(View1);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        private void GetDebitNote()
        {
            try
            {
                DebitNoteBLL mobDeptBLL = new DebitNoteBLL();
                List<EntityDebitNote> ldtOpera = mobDeptBLL.GetDebitNotes();
                if (ldtOpera.Count > 0)
                {
                    dgvTestParameter.DataSource = ldtOpera;
                    dgvTestParameter.DataBind();
                    Session["DepartmentDetail"] = ldtOpera;
                    int lintRowCount = ldtOpera.Count;
                    lblRowCount.Text = "<b>Total Records:</b>" + lintRowCount.ToString();
                }

            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void ddlOther_SelectedIndexChanged(object sender, EventArgs e)
        {
            Calculation();
        }

        protected void dgvTestParameter_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dgvTestParameter.DataSource = (List<EntityDebitNote>)Session["DepartmentDetail"];
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

        private void Clear()
        {
            try
            {
                ddlSupplier.SelectedIndex = 0;
                ddlBatchNo.SelectedIndex = 0;
                ddlExpiryDate.SelectedIndex = 0;
                ClearOther();
                txtDebitDate.Text = string.Empty;
                GridView1.DataSource = new List<EntityDebitNoteDetails>();
                GridView1.DataBind();
                txtTotal.Text = string.Empty;
                txtSearch.Text = string.Empty;
                txtService.Text = string.Empty;
                txtNetAmount.Text = string.Empty;
                txtVAT.Text = string.Empty;
                //txtDiscount.Text = string.Empty;
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }


        protected void btnAddCharge_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlProduct.SelectedIndex > 0)
                {
                    List<EntityDebitNoteDetails> lst = (List<EntityDebitNoteDetails>)Session["BillDetails"];

                    int cnt = (from tbl in lst
                               where tbl.ProductCode == Convert.ToInt32(ddlProduct.SelectedValue)
                               && tbl.BatchNo.Equals(ddlBatchNo.SelectedItem.Text)
                               select tbl).ToList().Count;
                    if (cnt > 0)
                    {
                        lblMsg.Text = "This Product Already Added";
                    }
                    else
                    {
                        lst.Add(
                            new EntityDebitNoteDetails()
                            {
                                TempId = lst.Count + 1,
                                ProductCode = Convert.ToInt32(ddlProduct.SelectedValue),
                                ProductName = ddlProduct.SelectedItem.Text,
                                Quantity = Convert.ToInt32(txtQuantity.Text),
                                Price = Convert.ToDecimal(txtItemCharge.Text),
                                Amount = Convert.ToDecimal(txtItemCharge.Text) * (Convert.ToInt32(txtQuantity.Text)),
                                BatchNo = ddlBatchNo.SelectedItem.Text,
                                ExpiryDate = StringExtension.ToDateTime(ddlExpiryDate.SelectedItem.Text),
                                DNSrNo = 0,
                                IsDelete = false,
                            });
                        Session["BillDetails"] = lst;
                        GridView1.DataSource = lst;
                        GridView1.DataBind();
                        txtTotal.Text = Convert.ToString(lst.Sum(p => p.Amount));
                        ClearOther();
                        ddlProduct_SelectedIndexChanged(sender, e);
                    }
                    Calculation();
                }
                else
                {
                    lblMessage.Text = "Please Select Supplier Name";
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

        private void ClearOther()
        {
            try
            {
                txtItemCharge.Text = Convert.ToString(0);
                // txtItemCharge.Enabled = false;
                txtItemCharge.Text = string.Empty;
                ddlProduct.SelectedIndex = 0;
                txtQuantity.Text = string.Empty;
                ddlBatchNo.SelectedIndex = 0;
                ddlExpiryDate.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }
        protected void ddlBatchNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlBatchNo.SelectedIndex > 0 && ddlProduct.SelectedIndex > 0)
                {
                    DebitNoteBLL mobjDeptBLL = new DebitNoteBLL();
                    List<EntityDebitNoteDetails> lst = mobjDeptBLL.GetBatchNoandExpiryDatesByProduct(Convert.ToInt32(ddlProduct.SelectedValue));
                    if (lst != null)
                    {
                        lst.Insert(0, new EntityDebitNoteDetails() { ProductId = 0, BatchNo = "-----Select----" });
                        ddlExpiryDate.DataSource = lst;
                        ddlExpiryDate.DataTextField = "ExpiryDate";
                        ddlExpiryDate.DataValueField = "ProductId";
                        ddlExpiryDate.DataBind();
                        if (lst.Count > 1)
                        {
                            ListItem itemDate = ddlExpiryDate.Items.FindByText(string.Format("{0:dd/MM/yyyy}", lst[1].ExpiryDate));
                            if (itemDate != null)
                            {
                                ddlExpiryDate.SelectedValue = itemDate.Value;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message; ;
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton imgDelete = (ImageButton)sender;
                GridViewRow row = (GridViewRow)imgDelete.NamingContainer;
                List<EntityDebitNoteDetails> lst = (List<EntityDebitNoteDetails>)Session["BillDetails"];
                List<EntityDebitNoteDetails> lstFinal = new List<EntityDebitNoteDetails>();
                if (BtnSave.Visible)
                {
                    if (lst != null)
                    {
                        foreach (EntityDebitNoteDetails item in lst)
                        {
                            if (item.TempId != Convert.ToInt32(GridView1.DataKeys[row.RowIndex].Value))
                            {
                                lstFinal.Add(item);
                            }
                        }
                        GridView1.DataSource = lstFinal;
                        GridView1.DataBind();
                        txtTotal.Text = Convert.ToString(lstFinal.Where(p => p.IsDelete == false).ToList().Sum(p => p.Amount));
                        Session["BillDetails"] = lst;
                    }
                }
                else
                {
                    foreach (EntityDebitNoteDetails item in lst)
                    {
                        if (item.TempId == Convert.ToInt32(GridView1.DataKeys[row.RowIndex].Value))
                        {
                            item.IsDelete = true;
                        }
                    }
                    GridView1.DataSource = lst.Where(p => p.IsDelete == false).ToList();
                    GridView1.DataBind();
                    txtTotal.Text = Convert.ToString(lst.Where(p => p.IsDelete == false).ToList().Sum(p => p.Amount));
                    Session["BillDetails"] = lst;
                }
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
                ImageButton imgEdit = (ImageButton)sender;
                GridViewRow row = (GridViewRow)imgEdit.NamingContainer;
                if (row != null)
                {
                    btnUpdateItem.Visible = true;
                    btnAdd.Visible = false;
                    Session["TempId"] = Convert.ToInt32(GridView1.DataKeys[row.RowIndex].Value);
                    ListItem itemProduct = ddlProduct.Items.FindByText(row.Cells[1].Text);
                    ddlProduct.SelectedValue = itemProduct.Value;
                    ddlProduct_SelectedIndexChanged(sender, e);
                    ListItem itemBatch = ddlBatchNo.Items.FindByText(row.Cells[3].Text);
                    ddlBatchNo.SelectedValue = itemBatch.Value;
                    ddlBatchNo_SelectedIndexChanged(sender, e);
                    txtQuantity.Text = row.Cells[2].Text;
                    txtItemCharge.Text = row.Cells[5].Text;
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
                List<EntityDebitNoteDetails> lst = (List<EntityDebitNoteDetails>)Session["BillDetails"];
                foreach (EntityDebitNoteDetails item in lst)
                {
                    if (Convert.ToInt32(Session["TempId"]) == item.TempId)
                    {
                        item.ProductCode = Convert.ToInt32(ddlProduct.SelectedValue);
                        item.BatchNo = Convert.ToString(ddlBatchNo.SelectedItem.Text);
                        item.ExpiryDate = StringExtension.ToDateTime(ddlExpiryDate.SelectedItem.Text);
                        item.Price = Convert.ToDecimal(txtItemCharge.Text);
                        item.Quantity = Convert.ToInt32(txtQuantity.Text);
                        item.Amount = Convert.ToDecimal(txtItemCharge.Text) * Convert.ToInt32(txtQuantity.Text);
                        item.IsDelete = false;
                    }
                }
                GridView1.DataSource = lst;
                GridView1.DataBind();
                txtTotal.Text = Convert.ToString(lst.Where(p => p.IsDelete == false).ToList().Sum(p => p.Amount));
                btnUpdateItem.Visible = false;
                btnAdd.Visible = true;
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                DebitNoteBLL mobjDeptBLL = new DebitNoteBLL();
                if (!string.IsNullOrEmpty(txtSearch.Text))
                {
                    List<EntityDebitNote> ldtOpera = mobjDeptBLL.GetDebitNotes(txtSearch.Text);
                    if (ldtOpera.Count > 0)
                    {
                        dgvTestParameter.DataSource = ldtOpera;
                        dgvTestParameter.DataBind();
                        Session["DepartmentDetail"] = ldtOpera;
                        int lintRowcount = ldtOpera.Count;
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
                GetDebitNote();
                txtSearch.Text = string.Empty;
                lblMessage.Text = string.Empty;
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
        protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                List<EntityDebitNoteDetails> lst = null;
                if (ddlProduct.SelectedIndex > 0)
                {
                    EntityProduct entProduct = new IssueMaterialBLL().GetProductPrice(Convert.ToInt32(ddlProduct.SelectedValue));
                    List<tblStockDetail> lst1 = new CustomerTransactionBLL().GetProductTransByProductId(Convert.ToInt32(ddlProduct.SelectedValue));
                    if (lst1 != null)
                    {
                        lblBalQty.Text = Convert.ToString(Convert.ToInt32(lst1.Sum(p => p.InwardQty)) - Convert.ToInt32(lst1.Sum(p => p.OutwardQty)));
                    }
                    else
                    {
                        lblBalQty.Text = string.Empty;
                    }
                    if (entProduct != null)
                    {
                        txtItemCharge.Text = string.Format("{0:0.00}", entProduct.Price);
                        txtQuantity.Text = Convert.ToString(1);
                    }
                    lst = new DebitNoteBLL().GetBatchNoandExpiryDatesByProduct(Convert.ToInt32(ddlProduct.SelectedValue));
                    if (lst != null)
                    {
                        lst.Insert(0, new EntityDebitNoteDetails() { ProductId = 0, BatchNo = "-----Select----" });
                        ddlBatchNo.DataSource = lst;
                        ddlBatchNo.DataTextField = "BatchNo";
                        ddlBatchNo.DataValueField = "ProductId";
                        ddlBatchNo.DataBind();
                        ddlExpiryDate.DataSource = lst;
                        ddlExpiryDate.DataTextField = "ExpiryDate";
                        ddlExpiryDate.DataValueField = "ProductId";
                        ddlExpiryDate.DataBind();
                    }
                }
                else
                {
                    lst = new List<EntityDebitNoteDetails>();
                    lst.Insert(0, new EntityDebitNoteDetails() { ProductId = 0, BatchNo = "-----Select----" });
                    ddlBatchNo.DataSource = lst;
                    ddlBatchNo.DataTextField = "BatchNo";
                    ddlBatchNo.DataValueField = "ProductId";
                    ddlBatchNo.DataBind();
                    ddlExpiryDate.DataSource = lst;
                    ddlExpiryDate.DataTextField = "ExpiryDate";
                    ddlExpiryDate.DataValueField = "ProductId";
                    ddlExpiryDate.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }
    }
}