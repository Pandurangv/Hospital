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

namespace Hospital.Billing
{
    public partial class frmICUInvoice : System.Web.UI.Page
    {
        ICUInvoiceBLL mobjDeptBLL = new ICUInvoiceBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            ////SessionManager.Instance.SetSession();
            if (SessionManager.Instance.LoginUser != null)
            {
                if (!Page.IsPostBack)
                {
                    //SessionManager.Instance.SetSession();
                    Session["MyFlag"] = string.Empty;
                    Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                    GetICUInvoice();
                    BindOtherCharge();
                    BindICUBeds();
                    BindPatientList(false);
                    MultiView1.SetActiveView(View1);
                }
            }
            else
            {
                Response.Redirect("~/frmlogin.aspx", false);
            }
        }

        private void BindICUBeds()
        {
            try
            {
                List<EntityBedMaster> lst = mobjDeptBLL.GetICUBeds();
                lst.Insert(0, new EntityBedMaster() { BedId = 0, BedNo = "-----Select-----" });
                ddlBedMaster.DataSource = lst;
                ddlBedMaster.DataTextField = "BedNo";
                ddlBedMaster.DataValueField = "BedId";
                ddlBedMaster.DataBind();
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void ddlBedMaster_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlBedMaster.SelectedIndex > 0)
                {
                    List<EntityChargeCategory> lstCat = new ChargeCategoryBLL().GetICUChargeDetail();
                    List<EntityICUInvoiceDetail> lst = new List<EntityICUInvoiceDetail>();
                    decimal amount = mobjDeptBLL.GetICUCharges(Convert.ToDecimal(ddlBedMaster.SelectedValue));
                    foreach (EntityChargeCategory item in lstCat)
                    {
                        if (item.IsICU)
                        {
                            lst.Add(new EntityICUInvoiceDetail { ChargesId = item.ChargesId, ChargesName = item.ChargeCategoryName, Amount = amount });
                        }
                    }
                    Session["BillDetails"] = lst;
                    GridView1.DataSource = lst;
                    GridView1.DataBind();
                    txtTotal.Text = Convert.ToString(lst.Sum(p => p.Amount));
                    Calculation();
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        private void BindOtherCharge()
        {
            try
            {
                List<EntityChargeCategory> lstOther = mobjDeptBLL.GetOtherChargeList();
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

        private void BindPatientList(bool IsDischarge)
        {
            try
            {
                List<EntityPatientMaster> lstPat = mobjDeptBLL.GetICUPatientList(IsDischarge);
                ddlPatient.DataSource = lstPat;
                lstPat.Insert(0, new EntityPatientMaster() { PatientId = 0, PatientFirstName = "--Select--" });
                ddlPatient.DataValueField = "PatientId";
                ddlPatient.DataTextField = "PatientFirstName";
                ddlPatient.DataBind();
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
                GetICUInvoice();
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
                lblMessage.Text = string.Empty;
                Session["MyFlag"] = "EDIT";
                BindPatientList(false);
                ImageButton imgEdit = (ImageButton)sender;
                GridViewRow row = (GridViewRow)imgEdit.NamingContainer;
                Session["Bill_Id"] = Convert.ToInt32(dgvTestParameter.DataKeys[row.RowIndex].Value);

                List<EntityICUInvoiceDetail> tblEmp = mobjDeptBLL.SelectICUInvoiceDetails(Convert.ToInt32(Session["Bill_Id"]));
                if (tblEmp.Count > 0)
                {
                    ddlPatient.Enabled = false;
                    ListItem item = ddlPatient.Items.FindByText(Convert.ToString(row.Cells[2].Text));
                    ddlPatient.SelectedValue = item.Value;
                    ListItem itemBed = ddlBedMaster.Items.FindByText(Convert.ToString(row.Cells[0].Text));
                    ddlBedMaster.SelectedValue = itemBed.Value;
                    GridView1.DataSource = tblEmp;
                    Session["FromEdit"] = tblEmp;
                    Session["ForUpdate"] = tblEmp;
                    GridView1.DataBind();
                    txtBillDate.Text = Convert.ToString(row.Cells[3].Text);
                    txtDiscount.Text = Convert.ToString(decimal.Round(tblEmp[0].Discount.Value, 0));
                    txtTotal.Text = Convert.ToString(tblEmp.Sum(p => p.Amount));
                    txtNetAmount.Text = Convert.ToString(tblEmp[0].NetAmount);
                    txtVAT.Text = Convert.ToString(tblEmp[0].Vat);
                    txtService.Text = Convert.ToString(tblEmp[0].Service);

                    //if (row.Cells[5].Text == "True")
                    //{
                    //    chkIsCash.Checked = true;
                    //}
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
            try
            {
                lblMessage.Text = string.Empty;
                Session["MyFlag"] = "ADD";
                BindPatientList(true);
                btnUpdate.Visible = false;
                BtnSave.Visible = true;
                CalBillDate.StartDate = DateTime.Now.Date;
                Clear();
                MultiView1.SetActiveView(View2);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        private void Clear()
        {
            ddlPatient.SelectedIndex = 0;
            ddlPatient.Enabled = true;
            ClearOther();
            txtAmount.Enabled = false;
            GridView1.DataSource = new List<sp_GetAllBedAllocICUResult>();
            GridView1.DataBind();
            txtTotal.Text = string.Empty;
            txtNetAmount.Text = string.Empty;
            txtDiscount.Text = string.Empty;
            txtVAT.Text = string.Empty;
            txtSearch.Text = string.Empty;
            txtBillDate.Text = string.Empty;
        }

        private void GetICUInvoice()
        {
            List<sp_GetAllBedAllocICUResult> ldtRoom = mobjDeptBLL.GetPatientInvoice();

            if (ldtRoom.Count > 0 && ldtRoom != null)
            {
                dgvTestParameter.DataSource = ldtRoom;
                dgvTestParameter.DataBind();
                Session["RoomDetails"] = ldtRoom;
                int lintRowcount = ldtRoom.Count;
                lblRowCount.Text = "<b>Total Records:</b> " + lintRowcount.ToString();
                pnlShow.Style.Add(HtmlTextWriterStyle.Display, "");
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                CriticareHospitalDataContext objData = new CriticareHospitalDataContext();
                List<sp_GetAllBedAllocICUResult> lst = (from tbl in objData.sp_GetAllBedAllocICU()
                                                        where tbl.FullName.Contains(txtSearch.Text)
                                                        select tbl).ToList();
                dgvTestParameter.DataSource = lst;
                dgvTestParameter.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            GetICUInvoice();
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int Invoice = 0;
                EntityICUInvoice entInvoice = new EntityICUInvoice();
                EntityInvoiceDetails entInvoiceDetails = new EntityInvoiceDetails();
                if (Session["update"].ToString() == ViewState["update"].ToString())
                {
                    if (ddlPatient.SelectedIndex == 0)
                    {
                        Commons.ShowMessage("Please Select Patient Name", this.Page);
                        ddlPatient.Focus();
                        return;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(txtBillDate.Text.Trim()))
                        {
                            Commons.ShowMessage("Please Selct Bill Date", this.Page);
                            CalBillDate.Focus();
                            return;
                        }
                        else
                        {
                            if (ddlOther.SelectedIndex > 0 && string.IsNullOrEmpty(txtAmount.Text.Trim()))
                            {
                                Commons.ShowMessage("Please Enter Amount", this.Page);
                                txtAmount.Focus();
                                return;
                            }
                            else
                            {
                                entInvoice.PatientId = Convert.ToInt32(ddlPatient.SelectedValue);
                                entInvoice.TotalAmount = Convert.ToDecimal(txtTotal.Text);
                                entInvoice.AllocationDate = StringExtension.ToDateTime(txtBillDate.Text);
                                entInvoice.NetAmount = Convert.ToDecimal(txtNetAmount.Text);

                                if (string.IsNullOrEmpty(txtDiscount.Text))
                                {
                                    entInvoice.Discount = 0;
                                }
                                else
                                {
                                    entInvoice.Discount = Convert.ToInt32(txtDiscount.Text);
                                }

                                if (string.IsNullOrEmpty(txtVAT.Text))
                                {
                                    entInvoice.Tax1 = 0;
                                }
                                else
                                {
                                    entInvoice.Tax1 = Convert.ToDecimal(txtVAT.Text);
                                }

                                if (chkDischarge.Checked)
                                {
                                    entInvoice.IsDischarge = true;
                                    entInvoice.DischargeDate = DateTime.Now.Date;
                                }
                                else
                                {
                                    entInvoice.IsDischarge = false;
                                    entInvoice.DischargeDate = null;
                                }

                                if (string.IsNullOrEmpty(txtService.Text))
                                {
                                    entInvoice.Tax2 = 0;
                                }
                                else
                                {
                                    entInvoice.Tax2 = Convert.ToDecimal(txtService.Text);
                                }
                                entInvoice.BedId = Convert.ToInt32(ddlBedMaster.SelectedValue);
                                List<EntityICUInvoiceDetail> lstInvoice = (List<EntityICUInvoiceDetail>)Session["BillDetails"];
                                Invoice = mobjDeptBLL.InsertInvoice(entInvoice, lstInvoice, chkIsCash.Checked);
                                if (Invoice > 0)
                                {
                                    GetICUInvoice();
                                    lblMessage.Text = "Record Inserted Successfully";
                                    Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                                }
                                else
                                {
                                    lblMessage.Text = "Record Not Inserted...";
                                }
                                //int Check = mobjDeptBLL.IsBedAlloc(entInvoice.BedId);
                                //if (Check>0)
                                //{

                                //}
                                //else
                                //{
                                //    lblMessage.Text = "This Bed Is Already Occupied";
                                //}
                                Session["BillDetails"] = new List<EntityInvoiceDetails>();
                            }
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
                List<EntityICUInvoiceDetail> lstEdited = (List<EntityICUInvoiceDetail>)Session["FromEdit"];
                List<EntityICUInvoiceDetail> lstUpdate = (List<EntityICUInvoiceDetail>)Session["ForUpdate"];
                EntityICUInvoice entInvoice = new EntityICUInvoice();
                //EntityInvoiceDetails entInvoiceDetails = new EntityInvoiceDetails();
                entInvoice.PatientId = Convert.ToInt32(ddlPatient.SelectedValue);
                entInvoice.TotalAmount = Convert.ToDecimal(txtTotal.Text);
                entInvoice.AllocationDate = StringExtension.ToDateTime(txtBillDate.Text);
                entInvoice.NetAmount = Convert.ToDecimal(txtNetAmount.Text);
                entInvoice.ICUBedAllocId = Convert.ToInt32(Session["Bill_Id"]);
                //lstEdited[0].A = StringExtension.ToDateTime(txtBillDate.Text);
                lstEdited[0].Total = Convert.ToDecimal(txtTotal.Text);
                lstEdited[0].NetAmount = Convert.ToDecimal(txtNetAmount.Text);
                lstEdited[0].Discount = Convert.ToDecimal(txtDiscount.Text);
                lstEdited[0].Vat = Convert.ToDecimal(txtVAT.Text);
                lstEdited[0].Service = Convert.ToDecimal(txtService.Text);
                if (chkIsCash.Checked)
                {
                    entInvoice.IsCash = true;
                }
                else
                {
                    entInvoice.IsCash = false;
                }

                if (string.IsNullOrEmpty(txtDiscount.Text))
                {
                    entInvoice.Discount = 0;
                }
                else
                {
                    entInvoice.Discount = Convert.ToInt32(txtDiscount.Text);
                }

                if (string.IsNullOrEmpty(txtVAT.Text))
                {
                    entInvoice.Tax1 = 0;
                }
                else
                {
                    entInvoice.Tax1 = Convert.ToDecimal(txtVAT.Text);
                }

                if (string.IsNullOrEmpty(txtService.Text))
                {
                    entInvoice.Tax2 = 0;
                }
                else
                {
                    entInvoice.Tax2 = Convert.ToDecimal(txtService.Text);
                }
                lintCnt = mobjDeptBLL.UpdateInvoice(lstEdited, lstUpdate, entInvoice);

                if (lintCnt > 0)
                {
                    GetICUInvoice();
                    Commons.ShowMessage("Record Updated Successfully", this.Page);
                }
                else
                {
                    Commons.ShowMessage("Record Not Updated", this.Page);
                }
                GetICUInvoice();

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
                List<EntityICUInvoiceDetail> lstFinal = new List<EntityICUInvoiceDetail>();
                if (Session["MyFlag"] == "ADD")
                {
                    List<EntityICUInvoiceDetail> lst = (List<EntityICUInvoiceDetail>)Session["BillDetails"];

                    foreach (EntityICUInvoiceDetail item in lst)
                    {
                        if (item.ChargesId != Convert.ToInt32(row.Cells[0].Text))
                        {
                            lstFinal.Add(item);
                        }
                    }
                    Session["BillDetails"] = lstFinal;

                }
                if (Session["MyFlag"] == "EDIT")
                {
                    List<EntityICUInvoiceDetail> lst = (List<EntityICUInvoiceDetail>)Session["FromEdit"];

                    foreach (EntityICUInvoiceDetail item in lst)
                    {
                        if (item.ChargesId != Convert.ToInt32(row.Cells[0].Text))
                        {
                            lstFinal.Add(item);
                        }
                    }
                    Session["FromEdit"] = lstFinal;
                }
                GridView1.DataSource = lstFinal;
                GridView1.DataBind();
                txtTotal.Text = Convert.ToString(lstFinal.Sum(p => p.Amount));
                Calculation();
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }


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
                lblMessage.Text = ex.Message;
            }
        }

        protected void dgvTestParameter_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvTestParameter.PageIndex = e.NewPageIndex;
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
            try
            {
                if (ddlPatient.SelectedIndex > 0)
                {
                    if (Session["MyFlag"] == "ADD")
                    {
                        List<EntityICUInvoiceDetail> lst = (List<EntityICUInvoiceDetail>)Session["BillDetails"];
                        if (lst == null)
                        {
                            lst = new List<EntityICUInvoiceDetail>();
                        }
                        int cnt = (from tbl in lst
                                   where tbl.ChargesId == Convert.ToInt32(ddlOther.SelectedValue)
                                   select tbl).ToList().Count;
                        if (cnt > 0)
                        {
                            Commons.ShowMessage("This Charge Already Added", this.Page);
                            GridView1.DataSource = lst;
                            GridView1.DataBind();
                        }
                        else
                        {
                            lst.Add(new EntityICUInvoiceDetail() { ChargesId = Convert.ToInt32(ddlOther.SelectedValue), Amount = Convert.ToDecimal(txtAmount.Text), ChargesName = ddlOther.SelectedItem.Text });
                            Session["BillDetails"] = lst;
                            GridView1.DataSource = lst;
                            GridView1.DataBind();
                            txtTotal.Text = Convert.ToString(lst.Sum(p => p.Amount));
                            Calculation();
                            ClearOther();
                        }
                    }
                    if (Session["MyFlag"] == "EDIT")
                    {
                        List<EntityICUInvoiceDetail> lst = (List<EntityICUInvoiceDetail>)Session["FromEdit"];
                        int cnt = (from tbl in lst
                                   where tbl.ChargesId == Convert.ToInt32(ddlOther.SelectedValue)
                                   select tbl).ToList().Count;
                        if (cnt > 0)
                        {
                            Commons.ShowMessage("This Charge Already Added", this.Page);
                        }
                        else
                        {
                            lst.Add(new EntityICUInvoiceDetail() { ChargesId = Convert.ToInt32(ddlOther.SelectedValue), Amount = Convert.ToDecimal(txtAmount.Text), ChargesName = ddlOther.SelectedItem.Text });
                            Session["FromEdit"] = lst;
                            GridView1.DataSource = lst;
                            GridView1.DataBind();
                            txtTotal.Text = Convert.ToString(lst.Sum(p => p.Amount));
                            Calculation();
                            ClearOther();
                        }
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
            txtAmount.Text = Convert.ToString(0);
            txtAmount.Enabled = false;
            ddlOther.SelectedIndex = 0;
        }

        private void Calculation()
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


        protected void btnCancel_Click(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;
            ClearOther();
        }

        protected void ddlOther_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlOther.SelectedIndex > 0)
            {
                Session["OtherCharge_ID"] = ddlOther.SelectedValue;
                txtAmount.Enabled = true;
                txtAmount.Text = string.Empty;
            }
            else
            {
                ClearOther();
            }
            Calculation();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                e.Row.Enabled = false;
                if (!e.Row.Cells[0].Text.Equals("Charge Id", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (!e.Row.Cells[0].Text.Equals("1", StringComparison.CurrentCultureIgnoreCase))
                    {
                        if (!e.Row.Cells[0].Text.Equals("2", StringComparison.CurrentCultureIgnoreCase))
                        {
                            if (!e.Row.Cells[0].Text.Equals("3", StringComparison.CurrentCultureIgnoreCase))
                            {
                                e.Row.Enabled = true;
                            }
                            else
                            {
                                e.Row.Enabled = false;
                            }
                        }
                        else
                        {
                            e.Row.Enabled = false;
                        }
                    }
                }
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap; text-align:left;");
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            ImageButton imgEdit = (ImageButton)sender;
            GridViewRow row = (GridViewRow)imgEdit.NamingContainer;
            //Session["BILLNo"] = Convert.ToInt32(dgvTestParameter.DataKeys[row.RowIndex].Value);
            //Session["Bed_NO"] = Convert.ToString(row.Cells[0].Text);
            //Session["ReportType"] = "ICUInvoice";
            //Response.Redirect("~/PathalogyReport/PathologyReport.aspx", false);
            Response.Redirect("~/PathalogyReport/PathologyReport.aspx?ReportType=ICUInvoice&BILLNo=" + dgvTestParameter.DataKeys[row.RowIndex].Value + "&Bed_NO=" + row.Cells[0].Text, false);
        }
    }
}