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
    public partial class frmPatintICUInvoice : System.Web.UI.Page
    {
        PatientInvoiceBLL mobjDeptBLL = new PatientInvoiceBLL();

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
                    GetPatientInvoice();
                    BindOtherCharge();
                    BindPatientList(false);
                    MultiView1.SetActiveView(View1);
                }
            }
            else
            {
                Response.Redirect("~/frmlogin.aspx", false);
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
                List<EntityPatientMaster> lstPat = mobjDeptBLL.GetPatientList(IsDischarge);
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
                GetPatientInvoice();
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
                Session["Bill_Id"] = Convert.ToInt32(row.Cells[0].Text);

                List<EntityInvoiceDetails> tblEmp = mobjDeptBLL.SelectPatientInvoiceForEdit(Convert.ToInt32(Session["Bill_Id"]));
                if (tblEmp.Count > 0)
                {
                    ddlPatient.Enabled = false;
                    ListItem item = ddlPatient.Items.FindByText(Convert.ToString(tblEmp[0].PatientName));
                    ddlPatient.SelectedValue = item.Value;
                    ddlPatient_SelectedIndexChanged(sender, e);
                    GridView1.DataSource = tblEmp;
                    Session["FromEdit"] = tblEmp;
                    Session["ForUpdate"] = tblEmp;
                    GridView1.DataBind();
                    Session["BillSrNO"] = Convert.ToInt32(tblEmp[0].BillSRNo);
                    txtBillDate.Text = Convert.ToString(row.Cells[2].Text);
                    txtDiscount.Text = Convert.ToString(tblEmp[0].Discount);
                    txtTotal.Text = Convert.ToString(tblEmp[0].Total);
                    txtNetAmount.Text = Convert.ToString(tblEmp[0].NetAmount);
                    //txtVAT.Text = Convert.ToString(tblEmp[0].Vat);
                    //txtService.Text = Convert.ToString(tblEmp[0].Service);
                    if (row.Cells[4].Text == "True")
                    {
                        chkIsCash.Checked = true;
                    }
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
            lblMessage.Text = string.Empty;
            Session["MyFlag"] = "ADD";
            BindPatientList(true);
            btnUpdate.Visible = false;
            BtnSave.Visible = true;
            CalBillDate.StartDate = DateTime.Now.Date;
            Clear();
            MultiView1.SetActiveView(View2);
        }

        private void Clear()
        {
            ddlPatient.SelectedIndex = 0;
            ClearOther();
            txtAmount.Enabled = false;
            GridView1.DataSource = new List<EntityInvoiceDetails>();
            GridView1.DataBind();
            txtTotal.Text = string.Empty;
            txtNetAmount.Text = string.Empty;
            txtDiscount.Text = string.Empty;
            txtVAT.Text = string.Empty;
            txtSearch.Text = string.Empty;
            txtBillDate.Text = string.Empty;
        }

        private void GetPatientInvoice()
        {
            try
            {
                List<STP_GetPatientInvoiceResult> ldtRoom = mobjDeptBLL.GetPatientInvoice();

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
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                CriticareHospitalDataContext objData = new CriticareHospitalDataContext();
                List<STP_GetPatientInvoiceResult> lst = (from tbl in objData.STP_GetPatientInvoice()
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
            GetPatientInvoice();
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int Invoice = 0;
                EntityPatientInvoice entInvoice = new EntityPatientInvoice();
                EntityInvoiceDetails entInvoiceDetails = new EntityInvoiceDetails();
                if (Session["update"].ToString() == ViewState["update"].ToString())
                {
                    if (ddlPatient.SelectedIndex == 0)
                    {
                        lblMsg.Text = "Please Select Patient Name";
                        ddlPatient.Focus();
                        return;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(txtBillDate.Text.Trim()))
                        {
                            lblMsg.Text = "Please Selct Bill Date";
                            CalBillDate.Focus();
                            return;
                        }
                        else
                        {
                            if (ddlOther.SelectedIndex > 0 && string.IsNullOrEmpty(txtAmount.Text.Trim()))
                            {
                                lblMsg.Text = "Please Enter Amount";
                                txtAmount.Focus();
                                return;
                            }
                            else
                            {
                                entInvoice.PatientId = Convert.ToInt32(ddlPatient.SelectedValue);
                                entInvoice.Amount = Convert.ToDecimal(txtTotal.Text);
                                entInvoice.BillDate = StringExtension.ToDateTime(txtBillDate.Text);
                                entInvoice.NetAmount = Convert.ToDecimal(txtNetAmount.Text);

                                if (string.IsNullOrEmpty(txtDiscount.Text))
                                {
                                    entInvoice.Discount = 0;
                                }
                                else
                                {
                                    entInvoice.Discount = Convert.ToDecimal(txtDiscount.Text);
                                }

                                //if (string.IsNullOrEmpty(txtVAT.Text))
                                //{
                                //    entInvoice.Vat = 0;
                                //}
                                //else
                                //{
                                //    entInvoice.Vat = Convert.ToDecimal(txtVAT.Text);
                                //}

                                //if (string.IsNullOrEmpty(txtService.Text))
                                //{
                                //    entInvoice.Service = 0;
                                //}
                                //else
                                //{
                                //    entInvoice.Service = Convert.ToDecimal(txtService.Text);
                                //}

                                List<EntityInvoiceDetails> lstInvoice = (List<EntityInvoiceDetails>)Session["BillDetails"];

                                Invoice = mobjDeptBLL.InsertInvoice(entInvoice, lstInvoice, chkIsCash.Checked);
                                if (Invoice > 0)
                                {
                                    GetPatientInvoice();
                                    lblMessage.Text = "Record Inserted Successfully";
                                    Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                                }
                                else
                                {
                                    lblMessage.Text = "Record Not Inserted...";
                                }
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
                List<EntityInvoiceDetails> lstEdited = (List<EntityInvoiceDetails>)Session["FromEdit"];
                List<EntityInvoiceDetails> lstUpdate = (List<EntityInvoiceDetails>)Session["ForUpdate"];
                EntityPatientInvoice entInvoice = new EntityPatientInvoice();
                EntityInvoiceDetails entInvoiceDetails = new EntityInvoiceDetails();
                entInvoice.PatientId = Convert.ToInt32(ddlPatient.SelectedValue);
                entInvoice.Amount = Convert.ToDecimal(txtTotal.Text);
                entInvoice.BillDate = StringExtension.ToDateTime(txtBillDate.Text);
                entInvoice.NetAmount = Convert.ToDecimal(txtNetAmount.Text);
                lstEdited[0].BillDate = StringExtension.ToDateTime(txtBillDate.Text);
                lstEdited[0].Total = Convert.ToDecimal(txtTotal.Text);
                lstEdited[0].NetAmount = Convert.ToDecimal(txtNetAmount.Text);
                lstEdited[0].Discount = Convert.ToDecimal(txtDiscount.Text);
                //lstEdited[0].Vat = Convert.ToDecimal(txtVAT.Text);
                //lstEdited[0].Service = Convert.ToDecimal(txtService.Text);
                if (chkIsCash.Checked)
                {
                    lstEdited[0].IsCash = true;
                }
                else
                {
                    lstEdited[0].IsCash = false;
                }

                if (string.IsNullOrEmpty(txtDiscount.Text))
                {
                    entInvoice.Discount = 0;
                }
                else
                {
                    entInvoice.Discount = Convert.ToDecimal(txtDiscount.Text);
                }

                //if (string.IsNullOrEmpty(txtVAT.Text))
                //{
                //    entInvoice.Vat = 0;
                //}
                //else
                //{
                //    entInvoice.Vat = Convert.ToDecimal(txtVAT.Text);
                //}

                //if (string.IsNullOrEmpty(txtService.Text))
                //{
                //    entInvoice.Service = 0;
                //}
                //else
                //{
                //    entInvoice.Service = Convert.ToDecimal(txtService.Text);
                //}
                lintCnt = mobjDeptBLL.UpdateInvoice(lstEdited, lstUpdate);

                if (lintCnt > 0)
                {
                    GetPatientInvoice();
                    lblMessage.Text = "Record Updated Successfully";
                }
                else
                {
                    lblMessage.Text = "Record Not Updated";
                }
                GetPatientInvoice();
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
                List<EntityInvoiceDetails> lstFinal = new List<EntityInvoiceDetails>();
                if (Session["MyFlag"] == "ADD")
                {
                    List<EntityInvoiceDetails> lst = (List<EntityInvoiceDetails>)Session["BillDetails"];

                    foreach (EntityInvoiceDetails item in lst)
                    {
                        if (item.OtherId != Convert.ToInt32(row.Cells[0].Text))
                        {
                            lstFinal.Add(item);
                        }
                    }
                    Session["BillDetails"] = lstFinal;

                }
                if (Session["MyFlag"] == "EDIT")
                {
                    List<EntityInvoiceDetails> lst = (List<EntityInvoiceDetails>)Session["FromEdit"];

                    foreach (EntityInvoiceDetails item in lst)
                    {
                        if (item.OtherId != Convert.ToInt32(row.Cells[0].Text))
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
                dgvTestParameter.DataSource = (List<STP_GetPatientInvoiceResult>)Session["RoomDetails"];
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
            try
            {
                if (ddlPatient.SelectedIndex > 0)
                {
                    if (Session["MyFlag"] == "ADD")
                    {
                        List<EntityInvoiceDetails> lst = (List<EntityInvoiceDetails>)Session["BillDetails"];
                        int cnt = (from tbl in lst
                                   where tbl.OtherId == Convert.ToInt32(ddlOther.SelectedValue)
                                   select tbl).ToList().Count;
                        if (cnt > 0)
                        {
                            Commons.ShowMessage("This Charge Already Added", this.Page);
                        }
                        else
                        {
                            lst.Add(new EntityInvoiceDetails() { OtherId = Convert.ToInt32(ddlOther.SelectedValue), Amount = Convert.ToDecimal(txtAmount.Text), ChargesName = ddlOther.SelectedItem.Text, OtherChargesId = Convert.ToInt32(ddlOther.SelectedValue) });
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
                        List<EntityInvoiceDetails> lst = (List<EntityInvoiceDetails>)Session["FromEdit"];
                        int cnt = (from tbl in lst
                                   where tbl.OtherId == Convert.ToInt32(ddlOther.SelectedValue)
                                   select tbl).ToList().Count;
                        if (cnt > 0)
                        {
                            Commons.ShowMessage("This Charge Already Added", this.Page);
                        }
                        else
                        {
                            lst.Add(new EntityInvoiceDetails() { OtherId = Convert.ToInt32(ddlOther.SelectedValue), Amount = Convert.ToDecimal(txtAmount.Text), ChargesName = ddlOther.SelectedItem.Text, OtherChargesId = Convert.ToInt32(ddlOther.SelectedValue) });
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
            try
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
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void ddlPatient_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlPatient.SelectedIndex > 0)
                {
                    Session["Pat_Id"] = ddlPatient.SelectedValue;
                    EntityPatientMaster Cate = mobjDeptBLL.GetPatientCate(Convert.ToInt32(Session["Pat_Id"]));
                    List<EntityChargeCategory> lstCat = new ChargeCategoryBLL().GetChargeDetail();
                    List<EntityInvoiceDetails> lst = new List<EntityInvoiceDetails>();
                    EntityPatientAlloc objTxt = new PatientAllocDocBLL().GetPatientType(Convert.ToInt32(ddlPatient.SelectedValue));
                    CalBillDate.StartDate = objTxt.AdmitDate;
                    foreach (EntityChargeCategory item in lstCat)
                    {
                        int day = 0;
                        if (item.IsBed)
                        {
                            List<EntityPatientInvoice> lstBed = mobjDeptBLL.GetBedCharges(Convert.ToInt32(Session["Pat_Id"]));
                            if (lstBed.Count > 0)
                            {
                                EntityInvoiceDetails entInv = new EntityInvoiceDetails() { BedAllocId = lstBed[0].BedAllocId, ChargesName = item.ChargeCategoryName, OtherChargesId = item.ChargesId };
                                if (DateTime.Now.Date.CompareTo(lstBed[0].AllocDate.Date) == 0)
                                {
                                    day = 1;
                                }
                                else
                                {
                                    day = Convert.ToInt32(DateTime.Now.Date.Subtract(lstBed[0].AllocDate.Date).Days);
                                }
                                entInv.Amount = Convert.ToDecimal(day) * Convert.ToDecimal(lstBed[0].Amount);
                                lst.Add(entInv);
                                Session["BedAlloc_Id"] = entInv.BedAllocId;
                            }
                        }

                        if (item.IsConsulting)
                        {
                            if (Cate.PatientType == "IPD")
                            {
                                List<EntityPatientInvoice> lstConsult = mobjDeptBLL.GetConsultChargesIPD(Convert.ToInt32(Session["Pat_Id"]));
                                if (lstConsult.Count > 0)
                                {
                                    EntityInvoiceDetails entInv = new EntityInvoiceDetails() { DocAllocationId = lstConsult[0].DocAllocId, Amount = Convert.ToDecimal(lstConsult[0].NoOfDays * lstConsult[0].Amount), ChargesName = item.ChargeCategoryName, OtherChargesId = item.ChargesId };
                                    if (DateTime.Now.Date.CompareTo(lstConsult[0].AllocDate.Date) == 0)
                                    {
                                        day = 1;
                                    }
                                    else
                                    {
                                        day = Convert.ToInt32(DateTime.Now.Date.Subtract(lstConsult[0].AllocDate.Date).Days);
                                    }
                                    entInv.Amount = Convert.ToDecimal(day * lstConsult[0].Amount);
                                    lst.Add(entInv);
                                    Session["Consult_Id"] = entInv.DocAllocationId;
                                }
                            }
                            else
                            {
                                List<EntityPatientInvoice> lstConsult = mobjDeptBLL.GetConsultChargesOPD(Convert.ToInt32(Session["Pat_Id"]));
                                if (lstConsult.Count > 0)
                                {
                                    EntityInvoiceDetails entInv = new EntityInvoiceDetails() { DocAllocationId = lstConsult[0].DocAllocId, Amount = Convert.ToDecimal(lstConsult[0].NoOfDays * lstConsult[0].Amount), ChargesName = item.ChargeCategoryName, OtherChargesId = item.ChargesId };
                                    entInv.Amount = Convert.ToDecimal(lstConsult[0].Amount);
                                    lst.Add(entInv);
                                    Session["Consult_Id"] = entInv.DocAllocationId;
                                }
                            }

                        }

                        if (item.IsOperation)
                        {
                            List<EntityPatientInvoice> lstOpera = mobjDeptBLL.GetOperaCharges(Convert.ToInt32(Session["Pat_Id"]));
                            if (lstOpera.Count > 0)
                            {
                                EntityInvoiceDetails entInv = new EntityInvoiceDetails() { OTBedAllocId = lstOpera[0].OTBedAllocId, Amount = Convert.ToDecimal(lstOpera[0].Amount), ChargesName = item.ChargeCategoryName, OtherChargesId = item.ChargesId };
                                lst.Add(entInv);
                                Session["OTBedAlloc_Id"] = entInv.OTBedAllocId;
                            }
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
                lblMsg.Text = ex.Message;
            }
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
                                //if (StringExtension.ToDateTime(e.Row.Cells[8].Text).CompareTo(DateTime.Now) == -1)
                                //{
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
            Session["BILLNo"] = Convert.ToInt32(dgvTestParameter.DataKeys[row.RowIndex].Value);
            Session["ReportType"] = "Invoice";
            Response.Redirect("~/PathalogyReport/PathologyReport.aspx", false);
        }
    }
}