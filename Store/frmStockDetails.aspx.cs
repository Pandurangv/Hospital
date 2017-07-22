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
    public partial class frmStockDetails : System.Web.UI.Page
    {
        PurchaseOrderBLL mobjDeptBLL = new PurchaseOrderBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (SessionManager.Instance.LoginUser != null)
            {
                if (!Page.IsPostBack)
                {
                    //SessionManager.Instance.SetSession();
                    Session["MyFlag"] = string.Empty;
                    Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                    GetPurchaseOrder();
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

        private void BindSupplierList()
        {
            try
            {
                List<EntitySupplierMaster> lstOther = mobjDeptBLL.GetSupplierList();
                ddlProductName.DataSource = lstOther;
                lstOther.Insert(0, new EntitySupplierMaster() { PKId = 0, SupplierName = "--Select--" });
                ddlProductName.DataValueField = "PKId";
                ddlProductName.DataTextField = "SupplierName";
                ddlProductName.DataBind();

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
                List<EntityProduct> lstPat = mobjDeptBLL.GetProductList();
                ddlProductName.DataSource = lstPat;
                lstPat.Insert(0, new EntityProduct() { ProductId = 0, ProductName = "--Select--" });
                ddlProductName.DataValueField = "ProductId";
                ddlProductName.DataTextField = "ProductName";
                ddlProductName.DataBind();
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
                GetPurchaseOrder();
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
                Session["MyFlag"] = "EDIT";
                ImageButton imgEdit = (ImageButton)sender;
                GridViewRow row = (GridViewRow)imgEdit.NamingContainer;
                Session["PO_ID"] = Convert.ToInt32(row.Cells[0].Text);
                List<EntityPurchaseOrderDetails> tblEmp = mobjDeptBLL.SelectPurchaseOrderForEdit(Convert.ToInt32(Session["PO_ID"]));
                if (tblEmp.Count > 0)
                {
                    ListItem item = ddlProductName.Items.FindByText(Convert.ToString(tblEmp[0].VendorName));
                    ddlProductName.SelectedValue = item.Value;
                    //ddlPatient_SelectedIndexChanged(sender, e);
                    GridView1.DataSource = tblEmp;
                    Session["FromEdit"] = tblEmp;
                    Session["ForUpdate"] = tblEmp;
                    GridView1.DataBind();
                    Session["BillSrNO"] = Convert.ToInt32(tblEmp[0].SR_No);
                    txtTotal.Text = Convert.ToString(tblEmp[0].Total);
                    txtInwardPrice.Text = Convert.ToString(0);
                    txtInwardPrice.Enabled = false;
                    txtQuantity.Text = string.Empty;
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

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            ViewState["update"] = Session["update"];
        }
        protected void BtnAddNewDept_Click(object sender, EventArgs e)
        {
            Session["MyFlag"] = "ADD";
            Session["BillDetails"] = new List<EntityPurchaseOrderDetails>();
            //btnUpdate.Visible = false;
            //BtnSave.Visible = true;
            CalPurchaseDate.StartDate = DateTime.Now.Date;
            Clear();
            MultiView1.SetActiveView(View2);
        }

        private void Clear()
        {
            ddlProductName.SelectedIndex = 0;
            ClearOther();
            txtOpeningDate.Text = string.Empty;
            GridView1.DataSource = new List<EntityPurchaseOrderDetails>();
            GridView1.DataBind();
            txtTotal.Text = string.Empty;
            txtSearch.Text = string.Empty;
        }

        private void GetPurchaseOrder()
        {
            try
            {
                List<EntityPurchaseOrder> ldtOpera = mobjDeptBLL.GetPurchaseOder();

                if (ldtOpera.Count > 0)
                {
                    dgvTestParameter.DataSource = ldtOpera;
                    dgvTestParameter.DataBind();
                    Session["DepartmentDetail"] = ldtOpera;
                    int lintRowcount = ldtOpera.Count;
                    lblRowCount.Text = "<b>Total Records:</b> " + lintRowcount.ToString();
                    pnlShow.Style.Add(HtmlTextWriterStyle.Display, "");
                    hdnPanel.Value = "";
                }
                else
                {
                    //pnlShow.Style.Add(HtmlTextWriterStyle.Display, "none");
                    hdnPanel.Value = "none";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

        }

        protected void btnReset_Click(object sender, EventArgs e)
        {

        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int Invoice = 0;
                EntityPurchaseOrder entPurchaseOrder = new EntityPurchaseOrder();
                EntityPurchaseOrderDetails entPurchaseOrderDetails = new EntityPurchaseOrderDetails();
                if (Session["update"].ToString() == ViewState["update"].ToString())
                {
                    if (ddlProductName.SelectedIndex == 0)
                    {
                        lblMsg.Text = "Please Select Supplier Name";
                        ddlProductName.Focus();
                        return;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(txtOpeningDate.Text.Trim()))
                        {
                            lblMsg.Text = "Please Selct Purchase Order Date";
                            CalPurchaseDate.Focus();
                            return;
                        }
                        else
                        {
                            entPurchaseOrder.VendorId = Convert.ToInt32(ddlProductName.SelectedValue);
                            entPurchaseOrder.PO_Amount = Convert.ToDecimal(txtTotal.Text);
                            entPurchaseOrder.PO_Date = StringExtension.ToDateTime(txtOpeningDate.Text);

                            List<EntityPurchaseOrderDetails> lstInvoice = (List<EntityPurchaseOrderDetails>)Session["BillDetails"];

                            Invoice = mobjDeptBLL.InsertPurchaseOrder(entPurchaseOrder, lstInvoice);
                            if (Invoice > 0)
                            {
                                GetPurchaseOrder();
                                lblMessage.Text = "Record Inserted Successfully....";
                                Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                            }
                            else
                            {
                                lblMessage.Text = "Record Not Inserted...";
                            }
                            Session["BillDetails"] = new List<EntityPurchaseOrderDetails>();
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

        void frmDepartmentMaster_Load(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void FillControls(DataTable ldt)
        {
            //txtEditDeptDesc.Text = ldt.Rows[0]["DeptDesc"].ToString();
        }

        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            int lintCnt = 0;
            try
            {
                List<EntityPurchaseOrderDetails> lstEdited = (List<EntityPurchaseOrderDetails>)Session["FromEdit"];
                List<EntityPurchaseOrderDetails> lstUpdate = (List<EntityPurchaseOrderDetails>)Session["ForUpdate"];
                EntityPurchaseOrder entPurchaseOrder = new EntityPurchaseOrder();
                EntityPurchaseOrderDetails entPurchaseOrderDetails = new EntityPurchaseOrderDetails();
                entPurchaseOrder.VendorId = Convert.ToInt32(ddlProductName.SelectedValue);
                entPurchaseOrder.PO_Amount = Convert.ToDecimal(txtTotal.Text);
                entPurchaseOrder.PO_Date = StringExtension.ToDateTime(txtOpeningDate.Text);
                lstEdited[0].Total = Convert.ToDecimal(txtTotal.Text);

                lintCnt = mobjDeptBLL.UpdatePurchaseOrderDetails(lstEdited, lstUpdate);

                if (lintCnt > 0)
                {
                    GetPurchaseOrder();
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

        protected void chkDelete_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chk.NamingContainer;
            if (chk.Checked)
            {
                LinkButton DeptCode = (LinkButton)row.FindControl("lnkDeptCode");
                Session["DeptCode"] = DeptCode.Text;
                //lblMessage.Text = string.Empty;
            }
            else
            {
                Session["DeptCode"] = string.Empty;
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton imgDelete = (ImageButton)sender;
                GridViewRow row = (GridViewRow)imgDelete.NamingContainer;
                Session["Delete_Charge"] = Convert.ToInt32(row.Cells[0].Text);
                List<EntityPurchaseOrderDetails> lstFinal = new List<EntityPurchaseOrderDetails>();
                if (Session["MyFlag"] == "ADD")
                {
                    List<EntityPurchaseOrderDetails> lst = (List<EntityPurchaseOrderDetails>)Session["BillDetails"];
                    if (lst.Count > 0)
                    {
                        foreach (EntityPurchaseOrderDetails item in lst)
                        {
                            if (item.Product_Id != Convert.ToInt32(row.Cells[0].Text))
                            {
                                lstFinal.Add(item);
                            }
                        }
                        Session["BillDetails"] = lstFinal;
                    }

                }
                if (Session["MyFlag"] == "EDIT")
                {
                    List<EntityPurchaseOrderDetails> lst = (List<EntityPurchaseOrderDetails>)Session["FromEdit"];

                    foreach (EntityPurchaseOrderDetails item in lst)
                    {
                        if (item.Product_Id != Convert.ToInt32(row.Cells[0].Text))
                        {
                            lstFinal.Add(item);
                        }
                    }
                    Session["FromEdit"] = lstFinal;
                }
                GridView1.DataSource = lstFinal;
                GridView1.DataBind();
                txtTotal.Text = Convert.ToString(lstFinal.Sum(p => p.Rate));
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }

        }

        //protected void BtnDeleteOk_Click(object sender, EventArgs e)
        //{
        //    EntityDepartment entDept = new EntityDepartment();
        //    int cnt = 0;

        //    try
        //    {
        //        foreach (GridViewRow drv in dgvDepartment.Rows)
        //        {
        //            CheckBox chkDelete = (CheckBox)drv.FindControl("chkDelete");
        //            if (chkDelete.Checked)
        //            {
        //                LinkButton lnkDeptCode = (LinkButton)drv.FindControl("lnkDeptCode");
        //                string lstrDeptCode = lnkDeptCode.Text;
        //                entDept.DeptCode = lstrDeptCode;

        //                cnt = mobjDeptBLL.DeleteDepartment(entDept);
        //                if (cnt > 0)
        //                {
        //                    //this.modalpopupDelete.Hide();

        //                    Commons.ShowMessage("Record Deleted Successfully....", this.Page);

        //                    if (dgvDepartment.Rows.Count <= 0)
        //                    {
        //                        //pnlShow.Style.Add(HtmlTextWriterStyle.Display, "none");
        //                        hdnPanel.Value = "none";
        //                    }

        //                }
        //                else
        //                {
        //                    Commons.ShowMessage("Record Not Deleted....", this.Page);
        //                }
        //            }
        //        }
        //        GetDepartments();

        //    }
        //    catch (System.Threading.ThreadAbortException)
        //    {

        //    }
        //    catch (Exception ex)
        //    {
        //        Commons.FileLog("frmDepartmentMaster -   BtnDeleteOk_Click(object sender, EventArgs e)", ex);
        //    }
        //}

        protected void dgvTestParameter_DataBound(object sender, EventArgs e)
        {
            int lintCurrentPage = dgvTestParameter.PageIndex + 1;
            lblPageCount.Text = "<b>Page</b> " + lintCurrentPage.ToString() + "<b> of </b>" + dgvTestParameter.PageCount.ToString();
        }

        protected void dgvTestParameter_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dgvTestParameter.DataSource = (DataTable)Session["DepartmentDetail"];
                dgvTestParameter.DataBind();
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmDepartmentMaster - dgvDepartment_PageIndexChanged(object sender, EventArgs e)", ex);
            }
        }

        protected void dgvTestParameter_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvTestParameter.PageIndex = e.NewPageIndex;
        }

        protected void dgvTestParameter_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void dgvTestParameter_RowDataBound(object sender, GridViewRowEventArgs e)
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
                    LinkButton lnkDeptCode = (LinkButton)e.Row.FindControl("lnkDeptCode");
                    CheckBox chkDelete = (CheckBox)e.Row.FindControl("chkDelete");
                    if (lnkDeptCode.Text == "Admin")
                    {
                        lnkDeptCode.Enabled = false;
                        chkDelete.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void btnAddCharge_Click(object sender, EventArgs e)
        {
            //if (ddlProductName.SelectedIndex > 0)
            //{
            //    if (!string.IsNullOrEmpty(txtQuantity.Text))
            //    {
            //        if (Session["MyFlag"] == "ADD")
            //        {
            //            List<EntityStockDetails> lst = (List<EntityStockDetails>)Session["BillDetails"];

            //            int cnt = (from tbl in lst
            //                       where tbl.Product_Id == Convert.ToInt32(ddlProductName.SelectedValue)
            //                       select tbl).ToList().Count;
            //            if (cnt > 0)
            //            {
            //                Commons.ShowMessage("This Product Already Added", this.Page);
            //            }
            //            else
            //            {
            //                lst.Add(new EntityStockDetails() { Product_Id = Convert.ToInt32(ddlProductName.SelectedValue), ProductName = ddlProductName.SelectedItem.Text, Quantity = Convert.ToInt32(txtQuantity.Text), Rate = Convert.ToDecimal(txtInwardPrice.Text) * (Convert.ToInt32(txtQuantity.Text)) });
            //                Session["BillDetails"] = lst;
            //                GridView1.DataSource = lst;
            //                GridView1.DataBind();
            //                txtTotal.Text = Convert.ToString(lst.Sum(p => p.Rate));
            //                ClearOther();
            //            }

            //        }
            //        if (Session["MyFlag"] == "EDIT")
            //        {
            //            List<EntityStockDetails> lst = (List<EntityStockDetails>)Session["FromEdit"];

            //            int cnt = (from tbl in lst
            //                       where tbl.Product_Id == Convert.ToInt32(ddlProductName.SelectedValue)
            //                       select tbl).ToList().Count;
            //            if (cnt > 0)
            //            {
            //                Commons.ShowMessage("This Product Already Added", this.Page);
            //            }
            //            else
            //            {
            //                lst.Add(new EntityStockDetails() { Product_Id = Convert.ToInt32(ddlProductName.SelectedValue), ProductName = ddlProductName.SelectedItem.Text, Quantity = Convert.ToInt32(txtQuantity.Text), Rate = Convert.ToDecimal(txtInwardPrice.Text) * (Convert.ToInt32(txtQuantity.Text)) });
            //                Session["FromEdit"] = lst;
            //                GridView1.DataSource = lst;
            //                GridView1.DataBind();
            //                txtTotal.Text = Convert.ToString(lst.Sum(p => p.Rate));
            //                Calculation();
            //                ClearOther();
            //            }
            //        }
            //    }
            //}
            //else
            //{
            //    Commons.ShowMessage("Please Select Patient Name", this.Page);
            //}
        }

        private void ClearOther()
        {
            //txtInwardPrice.Text = Convert.ToString(0);
            //txtInwardPrice.Enabled = false;
            ddlProductName.SelectedIndex = 0;
            txtQuantity.Text = Convert.ToString(1);
        }

        private void Calculation()
        {
            //if (string.IsNullOrEmpty(txtDiscount.Text) && string.IsNullOrEmpty(txtVAT.Text) && string.IsNullOrEmpty(txtService.Text))
            //{
            //    txtNetAmount.Text = txtTotal.Text;
            //}
            //else if (!string.IsNullOrEmpty(txtDiscount.Text) && string.IsNullOrEmpty(txtVAT.Text) && string.IsNullOrEmpty(txtService.Text))
            //{
            //    txtNetAmount.Text = Convert.ToString(Convert.ToDecimal(txtTotal.Text) - (Convert.ToDecimal(txtTotal.Text) * Convert.ToDecimal(txtDiscount.Text) / 100));
            //}
            //else if (string.IsNullOrEmpty(txtDiscount.Text) && !string.IsNullOrEmpty(txtVAT.Text) && string.IsNullOrEmpty(txtService.Text))
            //{
            //    txtNetAmount.Text = Convert.ToString(Convert.ToDecimal(txtTotal.Text) + (Convert.ToDecimal(txtTotal.Text) * Convert.ToDecimal(txtVAT.Text) / 100));
            //}
            //else if (string.IsNullOrEmpty(txtDiscount.Text) && string.IsNullOrEmpty(txtVAT.Text) && !string.IsNullOrEmpty(txtService.Text))
            //{
            //    txtNetAmount.Text = Convert.ToString(Convert.ToDecimal(txtTotal.Text) + (Convert.ToDecimal(txtTotal.Text) * Convert.ToDecimal(txtService.Text) / 100));
            //}
            //else if (!string.IsNullOrEmpty(txtDiscount.Text) && !string.IsNullOrEmpty(txtVAT.Text) && string.IsNullOrEmpty(txtService.Text))
            //{
            //    decimal TotalDis = Convert.ToDecimal(txtTotal.Text) - (Convert.ToDecimal(txtTotal.Text) * Convert.ToDecimal(txtDiscount.Text) / 100);
            //    txtNetAmount.Text = Convert.ToString(TotalDis + (TotalDis * Convert.ToDecimal(txtVAT.Text) / 100));
            //}
            //else if (!string.IsNullOrEmpty(txtDiscount.Text) && string.IsNullOrEmpty(txtVAT.Text) && !string.IsNullOrEmpty(txtService.Text))
            //{
            //    decimal TotalDis = Convert.ToDecimal(txtTotal.Text) - (Convert.ToDecimal(txtTotal.Text) * Convert.ToDecimal(txtDiscount.Text) / 100);
            //    txtNetAmount.Text = Convert.ToString(TotalDis + (TotalDis * Convert.ToDecimal(txtService.Text) / 100));
            //}
            //else if (string.IsNullOrEmpty(txtDiscount.Text) && !string.IsNullOrEmpty(txtVAT.Text) && !string.IsNullOrEmpty(txtService.Text))
            //{
            //    decimal TotalDis = Convert.ToDecimal(txtTotal.Text) + (Convert.ToDecimal(txtTotal.Text) * Convert.ToDecimal(txtVAT.Text) / 100);
            //    txtNetAmount.Text = Convert.ToString(TotalDis + (TotalDis * Convert.ToDecimal(txtService.Text) / 100));
            //}
            //else
            //{
            //    decimal TotalDis = Convert.ToDecimal(txtTotal.Text) - (Convert.ToDecimal(txtTotal.Text) * Convert.ToDecimal(txtDiscount.Text) / 100);
            //    decimal TotalVat = TotalDis + (TotalDis * Convert.ToDecimal(txtVAT.Text) / 100);
            //    txtNetAmount.Text = Convert.ToString(TotalVat + (TotalVat * Convert.ToDecimal(txtService.Text) / 100));
            //}
        }

        protected void btnUp_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearOther();
        }

        protected void ddlOther_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ddlOther.SelectedIndex > 0)
            //{
            //    Session["OtherCharge_ID"] = ddlOther.SelectedValue;
            //    txtAmount.Enabled = true;
            //    txtAmount.Text = string.Empty;
            //}
            //else
            //{
            //    ClearOther();
            //}
            Calculation();
        }
        protected void ddlPatient_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ddlPatient.SelectedIndex > 0)
            //{
            //    Session["Pat_Id"] = ddlPatient.SelectedIndex;
            //    EntityPatientMaster Cate = mobjDeptBLL.GetPatientCate(Convert.ToInt32(Session["Pat_Id"]));
            //    List<EntityChargeCategory> lstCat = new ChargeCategoryBLL().GetChargeDetail();
            //    List<EntityInvoiceDetails> lst = new List<EntityInvoiceDetails>();
            //    foreach (EntityChargeCategory item in lstCat)
            //    {
            //        if (item.IsBed)
            //        {
            //            List<EntityPatientInvoice> lstBed = mobjDeptBLL.GetBedCharges(Convert.ToInt32(Session["Pat_Id"]));
            //            if (lstBed.Count > 0)
            //            {
            //                EntityInvoiceDetails entInv = new EntityInvoiceDetails() { BedAllocId = lstBed[0].BedAllocId, ChargesName = item.ChargeCategoryName, OtherChargesId = item.ChargesId };
            //                entInv.Amount = Convert.ToDecimal(DateTime.Now.Date.Subtract(lstBed[0].AllocDate.Date).Days * lstBed[0].Amount);
            //                lst.Add(entInv);
            //                Session["BedAlloc_Id"] = entInv.BedAllocId;
            //            }
            //        }

            //        if (item.IsConsulting)
            //        {
            //            if (Cate.PatientType == "IPD")
            //            {
            //                List<EntityPatientInvoice> lstConsult = mobjDeptBLL.GetConsultChargesIPD(Convert.ToInt32(Session["Pat_Id"]));
            //                if (lstConsult.Count > 0)
            //                {
            //                    EntityInvoiceDetails entInv = new EntityInvoiceDetails() { DocAllocationId = lstConsult[0].DocAllocId, Amount = Convert.ToDecimal(lstConsult[0].NoOfDays * lstConsult[0].Amount), ChargesName = item.ChargeCategoryName, OtherChargesId = item.ChargesId };
            //                    entInv.Amount = Convert.ToDecimal(DateTime.Now.Date.Subtract(lstConsult[0].AllocDate.Date).Days * lstConsult[0].Amount);
            //                    lst.Add(entInv);
            //                    Session["Consult_Id"] = entInv.DocAllocationId;
            //                }
            //            }
            //            else
            //            {
            //                List<EntityPatientInvoice> lstConsult = mobjDeptBLL.GetConsultChargesOPD(Convert.ToInt32(Session["Pat_Id"]));
            //                if (lstConsult.Count > 0)
            //                {
            //                    EntityInvoiceDetails entInv = new EntityInvoiceDetails() { DocAllocationId = lstConsult[0].DocAllocId, Amount = Convert.ToDecimal(lstConsult[0].NoOfDays * lstConsult[0].Amount), ChargesName = item.ChargeCategoryName, OtherChargesId = item.ChargesId };
            //                    entInv.Amount = Convert.ToDecimal(lstConsult[0].Amount);
            //                    lst.Add(entInv);
            //                    Session["Consult_Id"] = entInv.DocAllocationId;
            //                }
            //            }

            //        }

            //        if (item.IsOperation)
            //        {
            //            List<EntityPatientInvoice> lstOpera = mobjDeptBLL.GetOperaCharges(Convert.ToInt32(Session["Pat_Id"]));
            //            if (lstOpera.Count > 0)
            //            {
            //                EntityInvoiceDetails entInv = new EntityInvoiceDetails() { OTBedAllocId = lstOpera[0].OTBedAllocId, Amount = Convert.ToDecimal(lstOpera[0].Amount), ChargesName = item.ChargeCategoryName, OtherChargesId = item.ChargesId };
            //                lst.Add(entInv);
            //                Session["OTBedAlloc_Id"] = entInv.OTBedAllocId;
            //            }
            //        }
            //    }
            //    Session["BillDetails"] = lst;
            //    GridView1.DataSource = lst;
            //    GridView1.DataBind();
            //    txtTotal.Text = Convert.ToString(lst.Sum(p => p.Amount));
            //    Calculation();
            //}
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                //e.Row.Enabled = false;
                //if (!e.Row.Cells[0].Text.Equals("Charge Id", StringComparison.CurrentCultureIgnoreCase))
                //{
                //    if (!e.Row.Cells[0].Text.Equals("1", StringComparison.CurrentCultureIgnoreCase))
                //    {
                //        if (!e.Row.Cells[0].Text.Equals("2", StringComparison.CurrentCultureIgnoreCase))
                //        {
                //            if (!e.Row.Cells[0].Text.Equals("3", StringComparison.CurrentCultureIgnoreCase))
                //            {
                //                //if (StringExtension.ToDateTime(e.Row.Cells[8].Text).CompareTo(DateTime.Now) == -1)
                //                //{
                //                e.Row.Enabled = true;
                //            }
                //            else
                //            {
                //                e.Row.Enabled = false;
                //            }
                //        }
                //        else
                //        {
                //            e.Row.Enabled = false;
                //        }
                //    }
                //}
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap; text-align:left;");
                    if (!e.Row.Cells[0].Text.Equals("Product Id", StringComparison.CurrentCultureIgnoreCase))
                    {
                        e.Row.Enabled = false;
                    }
                    e.Row.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }
        //protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlProduct.SelectedIndex > 0)
        //    {
        //        Session["Prod_ID"] = ddlProduct.SelectedIndex;
        //        EntityProduct entProduct = mobjDeptBLL.GetProductPrice(Convert.ToInt32(Session["Prod_ID"]));
        //        txtItemCharge.Text = Convert.ToString(entProduct.Price);
        //        txtQuantity.Text = Convert.ToString(1);
        //    }
        //}
        protected void ddlProductName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void rdnInward_CheckedChanged(object sender, EventArgs e)
        {
            if (rdnInward.Checked)
            {
                CheckList(true);
                txtOutwardPrice.Text = string.Empty;
                txtOutwardQty.Text = string.Empty;
            }
            else
            {
                txtInwardQty.Text = string.Empty;
                txtInwardPrice.Text = string.Empty;
                CheckList(false);
            }
        }

        public void CheckList(bool Flag)
        {
            txtInwardQty.Enabled = Flag;
            txtInwardPrice.Enabled = Flag;
            txtOutwardQty.Enabled = !Flag;
            txtOutwardPrice.Enabled = !Flag;
        }
        protected void rdnOutward_CheckedChanged(object sender, EventArgs e)
        {
            if (rdnOutward.Checked)
            {
                CheckList(false);
                txtInwardQty.Text = string.Empty;
                txtInwardPrice.Text = string.Empty;
            }
            else
            {
                txtOutwardPrice.Text = string.Empty;
                txtOutwardQty.Text = string.Empty;
                CheckList(true);
            }
        }
    }
}