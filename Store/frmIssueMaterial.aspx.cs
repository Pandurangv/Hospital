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
    public partial class frmIssueMaterial : System.Web.UI.Page
    {
        IssueMaterialBLL mobjDeptBLL = new IssueMaterialBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (SessionManager.Instance.LoginUser != null)
            {
                if (!Page.IsPostBack)
                {
                    //SessionManager.Instance.SetSession();
                    GetMaterialIssue();
                    BindPatientList();
                    BindEmployeeList();
                    BindProductList();
                    MultiView1.SetActiveView(View1);
                }
            }
            else
            {
                Response.Redirect("~/frmlogin.aspx", false);
            }
        }

        private void BindEmployeeList()
        {
            try
            {
                List<EntityEmployee> lstOther = mobjDeptBLL.GetEmployeeList();
                ddlEmployee.DataSource = lstOther;
                lstOther.Insert(0, new EntityEmployee() { PKId = 0, EmpName = "--Select--" });
                ddlEmployee.DataValueField = "PKId";
                ddlEmployee.DataTextField = "EmpName";
                ddlEmployee.DataBind();

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
                List<EntityPatientMaster> lstOther = new PatientInvoiceBLL().GetPatientList(true);
                lstOther.Insert(0, new EntityPatientMaster() { PatientId = 0, PatientFirstName = "--Select--" });
                ddlPatient.DataSource = lstOther;
                ddlPatient.DataValueField = "PatientId";
                ddlPatient.DataTextField = "PatientFirstName";
                ddlPatient.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        private void BindProductList()
        {
            try
            {
                ProductBLL objIssue = new ProductBLL();
                List<EntityProduct> lstPat = objIssue.GetAllProducts().Where(p => p.Category == "Store").ToList();
                ddlProduct.DataSource = lstPat;
                lstPat.Insert(0, new EntityProduct() { ProductId = 0, ProductName = "--Select--" });
                ddlProduct.DataValueField = "ProductId";
                ddlProduct.DataTextField = "ProductName";
                ddlProduct.DataBind();
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
                Clear();
                GetMaterialIssue();
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
                GridViewRow row = (GridViewRow)imgEdit.NamingContainer;
                Session["PO_ID"] = Convert.ToInt32(row.Cells[0].Text);
                List<EntityIssueMaterialDetails> tblEmp = mobjDeptBLL.SelectPurchaseOrderForEdit(Convert.ToInt32(Session["PO_ID"]));
                if (tblEmp.Count > 0)
                {
                    ListItem item = ddlEmployee.Items.FindByText(row.Cells[1].Text);
                    ddlEmployee.SelectedValue = item.Value;
                    ListItem item1 = ddlPatient.Items.FindByText(row.Cells[2].Text);
                    ddlPatient.SelectedValue = item1.Value;
                    GridView1.DataSource = tblEmp;
                    GridView1.DataBind();
                    Session["BillDetails"] = tblEmp;
                    //Session["BillSrNO"] = Convert.ToInt32(tblEmp[0].SR_No);
                    txtPurchaseDate.Text = row.Cells[3].Text;
                    txtTotal.Text = Convert.ToString(row.Cells[4].Text);
                    BtnSave.Visible = false;
                    btnUpdate.Visible = true;
                    btnAdd.Visible = true;
                    btnUpdateCharge.Visible = false;
                    MultiView1.SetActiveView(View2);
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void ImageEditItem_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton imgEdit = (ImageButton)sender;
                GridViewRow row = (GridViewRow)imgEdit.NamingContainer;
                if (row != null)
                {
                    btnUpdateCharge.Visible = true;
                    btnAdd.Visible = false;
                    Session["TempId"] = Convert.ToInt32(GridView1.DataKeys[row.RowIndex].Value);
                    ListItem itemProduct = ddlProduct.Items.FindByText(row.Cells[1].Text);
                    ddlProduct.SelectedValue = itemProduct.Value;
                    ddlProduct_SelectedIndexChanged(sender, e);
                    //ListItem itemBatch = ddlBatchNo.Items.FindByText(row.Cells[2].Text);
                    //ddlBatchNo.SelectedValue = itemBatch.Value;
                    //ddlBatchNo_SelectedIndexChanged(sender, e);
                    txtQuantity.Text = row.Cells[2].Text;
                    txtItemCharge.Text = row.Cells[3].Text;
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void btnUpdateCharge_Click(object sender, EventArgs e)
        {
            try
            {
                List<EntityIssueMaterialDetails> lst = (List<EntityIssueMaterialDetails>)Session["BillDetails"];
                foreach (EntityIssueMaterialDetails item in lst)
                {
                    if (Convert.ToInt32(Session["TempId"]) == item.TempId)
                    {
                        item.ProductId = Convert.ToInt32(ddlProduct.SelectedValue);
                        //item.BatchNo = ddlBatchNo.SelectedItem.Text;
                        //item.ExpiryDate = StringExtension.ToDateTime(ddlExpiryDate.SelectedItem.Text);
                        item.Rate = Convert.ToDecimal(txtItemCharge.Text);
                        item.Quantity = Convert.ToInt32(txtQuantity.Text);
                        item.Total = Convert.ToDecimal(txtItemCharge.Text) * Convert.ToInt32(txtQuantity.Text);
                        item.IsDelete = false;
                    }
                }
                GridView1.DataSource = lst;
                GridView1.DataBind();
                txtTotal.Text = Convert.ToString(lst.Where(p => p.IsDelete == false).ToList().Sum(p => p.Total));
                btnUpdateCharge.Visible = false;
                btnAdd.Visible = true;
                ClearOther();
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
                Session["BillDetails"] = new List<EntityIssueMaterialDetails>();
                CalPurchaseDate.StartDate = DateTime.Now.Date;
                BtnSave.Visible = true;
                btnUpdate.Visible = false;
                btnAdd.Visible = true;
                btnUpdateCharge.Visible = false;
                Clear();
                MultiView1.SetActiveView(View2);
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
                ddlEmployee.SelectedIndex = 0;
                ddlPatient.SelectedIndex = 0;
                ClearOther();
                txtPurchaseDate.Text = string.Empty;
                GridView1.DataSource = new List<EntityIssueMaterialDetails>();
                GridView1.DataBind();
                txtTotal.Text = string.Empty;
                txtSearch.Text = string.Empty;
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        private void GetMaterialIssue()
        {
            try
            {
                List<EntityIssueMaterial> ldtOpera = mobjDeptBLL.GetMaterialIssue();
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
                List<EntityIssueMaterial> ldtOpera = mobjDeptBLL.GetMaterialIssue(txtSearch.Text);
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

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton imgEdit = (ImageButton)sender;
                GridViewRow cnt = (GridViewRow)imgEdit.NamingContainer;
                Session["Issue_Id"] = cnt.Cells[0].Text;
                int ID_Issue = Convert.ToInt32(Session["Issue_Id"]);
                if (ID_Issue > 0)
                {
                    Session["ReportType"] = "ISSUE";
                    Response.Redirect("~/PathalogyReport/PathologyReport.aspx", false);
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
                GetMaterialIssue();
                txtSearch.Text = string.Empty;
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        //protected void ddlBatchNo_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (ddlBatchNo.SelectedIndex > 0 && ddlProduct.SelectedIndex > 0)
        //        {
        //            List<EntityIssueMaterialDetails> lst = mobjDeptBLL.GetBatchNoandExpiryDatesByProduct(Convert.ToInt32(ddlProduct.SelectedValue));
        //            if (lst != null)
        //            {
        //                lst.Insert(0, new EntityIssueMaterialDetails() { ProductId = 0, BatchNo = "-----Select----" });
        //                ddlExpiryDate.DataSource = lst;
        //                ddlExpiryDate.DataTextField = "ExpiryDate";
        //                ddlExpiryDate.DataValueField = "ProductId";
        //                ddlExpiryDate.DataBind();
        //                if (lst.Count > 1)
        //                {
        //                    ListItem itemDate = ddlExpiryDate.Items.FindByText(string.Format("{0:dd/MM/yyyy}", lst[1].ExpiryDate));
        //                    if (itemDate != null)
        //                    {
        //                        ddlExpiryDate.SelectedValue = itemDate.Value;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        lblMessage.Text = ex.Message; ;
        //    }
        //}

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int Invoice = 0;
                EntityIssueMaterial entPurchaseOrder = new EntityIssueMaterial();
                EntityIssueMaterialDetails entPurchaseOrderDetails = new EntityIssueMaterialDetails();
                if (ddlEmployee.SelectedIndex == 0)
                {
                    lblMsg.Text = "Please Select Employee Name";
                    ddlEmployee.Focus();
                    return;
                }
                else
                {
                    if (ddlPatient.SelectedIndex == 0)
                    {
                        lblMsg.Text = "Please Select Patient Name";
                        ddlPatient.Focus();
                        return;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(txtPurchaseDate.Text.Trim()))
                        {
                            lblMsg.Text = "Please Selct Issue Material Date";
                            CalPurchaseDate.Focus();
                            return;
                        }
                        else
                        {
                            entPurchaseOrder.EmpId = Convert.ToInt32(ddlEmployee.SelectedValue);
                            entPurchaseOrder.PatientId = Convert.ToInt32(ddlPatient.SelectedValue);
                            entPurchaseOrder.TotalAmount = Convert.ToDecimal(txtTotal.Text);
                            entPurchaseOrder.IssueDate = StringExtension.ToDateTime(txtPurchaseDate.Text);

                            List<EntityIssueMaterialDetails> lstInvoice = (List<EntityIssueMaterialDetails>)Session["BillDetails"];

                            Invoice = mobjDeptBLL.InsertPurchaseOrder(entPurchaseOrder, lstInvoice);
                            if (Invoice > 0)
                            {
                                GetMaterialIssue();
                                lblMessage.Text = "Record Inserted Successfully....";
                            }
                            else
                            {
                                lblMessage.Text = "Record Not Inserted...";
                            }
                            Session["BillDetails"] = new List<EntityIssueMaterialDetails>();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                lblMessage.Text = ex.Message;
            }

            MultiView1.SetActiveView(View1);
        }

        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            int lintCnt = 0;
            try
            {
                EntityIssueMaterial entPurchaseOrder = new EntityIssueMaterial();
                entPurchaseOrder.EmpId = Convert.ToInt32(ddlEmployee.SelectedValue);
                entPurchaseOrder.PatientId = Convert.ToInt32(ddlPatient.SelectedValue);
                entPurchaseOrder.TotalAmount = Convert.ToDecimal(txtTotal.Text);
                entPurchaseOrder.IssueDate = StringExtension.ToDateTime(txtPurchaseDate.Text);
                entPurchaseOrder.IssueId = Convert.ToInt32(Session["PO_ID"]);
                List<EntityIssueMaterialDetails> lst = (List<EntityIssueMaterialDetails>)Session["BillDetails"];
                lintCnt = mobjDeptBLL.UpdateMaterialIssueDetails(entPurchaseOrder, lst);

                if (lintCnt > 0)
                {
                    GetMaterialIssue();
                    lblMessage.Text = "Record Updated Successfully";
                }
                else
                {
                    lblMessage.Text = "Record Not Updated";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
            MultiView1.SetActiveView(View1);
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton imgDelete = (ImageButton)sender;
                GridViewRow row = (GridViewRow)imgDelete.NamingContainer;
                List<EntityIssueMaterialDetails> lst = (List<EntityIssueMaterialDetails>)Session["BillDetails"];
                List<EntityIssueMaterialDetails> lstFinal = new List<EntityIssueMaterialDetails>();
                if (BtnSave.Visible)
                {
                    if (lst != null)
                    {
                        foreach (EntityIssueMaterialDetails item in lst)
                        {
                            if (item.TempId != Convert.ToInt32(GridView1.DataKeys[row.RowIndex].Value))
                            {
                                lstFinal.Add(item);
                            }
                        }
                        GridView1.DataSource = lstFinal;
                        GridView1.DataBind();
                        txtTotal.Text = Convert.ToString(lstFinal.Where(p => p.IsDelete == false).ToList().Sum(p => p.Total));
                        Session["BillDetails"] = lst;
                    }
                }
                else
                {
                    foreach (EntityIssueMaterialDetails item in lst)
                    {
                        if (item.TempId == Convert.ToInt32(GridView1.DataKeys[row.RowIndex].Value))
                        {
                            item.IsDelete = true;
                        }
                    }
                    GridView1.DataSource = lst.Where(p => p.IsDelete == false).ToList();
                    GridView1.DataBind();
                    txtTotal.Text = Convert.ToString(lst.Where(p => p.IsDelete == false).ToList().Sum(p => p.Total));
                    Session["BillDetails"] = lst;
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
                dgvTestParameter.DataSource = (List<EntityIssueMaterial>)Session["DepartmentDetail"];
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
                    List<EntityIssueMaterialDetails> lst = (List<EntityIssueMaterialDetails>)Session["BillDetails"];

                    int cnt = (from tbl in lst
                               where tbl.ProductId == Convert.ToInt32(ddlProduct.SelectedValue)
                               select tbl).ToList().Count;
                    if (cnt > 0)
                    {
                        lblMsg.Text = "This Product Already Added";
                    }
                    else
                    {
                        lst.Add(
                            new EntityIssueMaterialDetails()
                            {
                                TempId = lst.Count + 1,
                                ProductId = Convert.ToInt32(ddlProduct.SelectedValue),
                                ProductName = ddlProduct.SelectedItem.Text,
                                Quantity = Convert.ToInt32(txtQuantity.Text),
                                Rate = Convert.ToDecimal(txtItemCharge.Text),
                                Total = Convert.ToDecimal(txtItemCharge.Text) * (Convert.ToInt32(txtQuantity.Text)),
                                //BatchNo = ddlBatchNo.SelectedItem.Text,
                                //ExpiryDate = StringExtension.ToDateTime(ddlExpiryDate.SelectedItem.Text),
                                IsDelete = false,
                                SR_No = 0,
                            });
                        Session["BillDetails"] = lst;
                        GridView1.DataSource = lst;
                        GridView1.DataBind();
                        txtTotal.Text = Convert.ToString(lst.Sum(p => p.Total));
                        ClearOther();
                        ddlProduct_SelectedIndexChanged(sender, e);
                    }
                }
                else
                {
                    lblMsg.Text = "Please Select Patient Name";
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        private void ClearOther()
        {
            ddlProduct.SelectedIndex = 0;
            txtItemCharge.Text = string.Empty;
            txtQuantity.Text = string.Empty;
            //if (ddlBatchNo.Items.Count > 0)
            //{
            //    ddlBatchNo.SelectedIndex = 0;
            //}
            //if (ddlExpiryDate.Items.Count > 0)
            //{
            //    ddlExpiryDate.SelectedIndex = 0;
            //}
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearOther();
        }

        protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                List<EntityIssueMaterialDetails> lst = null;
                if (ddlProduct.SelectedIndex > 0)
                {
                    EntityProduct entProduct = mobjDeptBLL.GetProductPrice(Convert.ToInt32(ddlProduct.SelectedValue));
                    if (entProduct != null)
                    {
                        txtItemCharge.Text = Convert.ToString(entProduct.Price);
                        txtQuantity.Text = Convert.ToString(1);
                    }
                    List<tblStockDetail> lst1 = new CustomerTransactionBLL().GetProductTransByProductId(Convert.ToInt32(ddlProduct.SelectedValue));
                    if (lst1 != null)
                    {
                        lblBalQty.Text = Convert.ToString(Convert.ToInt32(lst1.Sum(p => p.InwardQty)) - Convert.ToInt32(lst1.Sum(p => p.OutwardQty)));
                    }
                    else
                    {
                        lblBalQty.Text = string.Empty;
                    }
                }
                else
                {
                    lst = new List<EntityIssueMaterialDetails>();
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }
    }
}