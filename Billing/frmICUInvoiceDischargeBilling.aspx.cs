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
    public partial class frmICUInvoiceDischargeBilling : System.Web.UI.Page
    {
        ICUDischargeBilling mobjDeptBLL = new ICUDischargeBilling();

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
                    BindIPDBeds();
                    BindPatientList();
                    //BindPatientList(false);
                    MultiView1.SetActiveView(View1);
                }
            }
            else
            {
                Response.Redirect("~/frmlogin.aspx", false);
            }
        }

        protected void ddlPatient_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlPatient.SelectedIndex > 0)
                {
                    EntityPatientAlloc objTxt = new PatientAllocDocBLL().GetPatientType(Convert.ToInt32(ddlPatient.SelectedValue));

                    EntityBedAllocToPatient alloc = mobjDeptBLL.GetICUAllocatedBed(Convert.ToInt32(ddlPatient.SelectedValue));
                    if (alloc == null)
                    {
                        lblMsg.Text = "Please allocate ICU bed to Selected Patient First";
                    }
                    else
                    {
                        int ICU_Days = 0;
                        lblMsg.Text = string.Empty;
                        List<EntityChargeCategory> lstCat = new ChargeCategoryBLL().GetChargeDetail();
                        List<EntityICUInvoiceDetail> lstInvoiceDetails = new List<EntityICUInvoiceDetail>();
                        foreach (EntityChargeCategory item in lstCat)
                        {
                            if (item.IsICU)
                            {
                                List<EntityBedAllocToPatient> lstBeds = mobjDeptBLL.GetICUAllocatedBedDetails(Convert.ToInt32(ddlPatient.SelectedValue)).Where(p => p.CategoryDesc.Equals("ICU")).ToList();
                                decimal charge = 0;
                                if (DateTime.Now.Date.CompareTo(lstBeds[0].AllocationDate.Value.Date) == 0)
                                {
                                    ICU_Days = 1;
                                    charge = lstBeds[0].Charges;
                                }
                                else
                                {
                                    foreach (EntityBedAllocToPatient bedinfo in lstBeds)
                                    {
                                        charge = bedinfo.Charges;
                                        if (bedinfo.ShiftDate == null)
                                        {
                                            ICU_Days = ICU_Days + Convert.ToInt32(StringExtension.ToDateTime(txtBillDate.Text).Subtract(bedinfo.AllocationDate.Value.Date).Days);
                                            ICU_Days++;
                                        }
                                        else
                                        {
                                            ICU_Days = ICU_Days + Convert.ToInt32(bedinfo.ShiftDate.Value.Date.Subtract(bedinfo.AllocationDate.Value.Date).Days);
                                        }
                                    }
                                }

                                Session["CurrentBedID"] = lstBeds[lstBeds.Count - 1].BedId;
                                CalBillDate.StartDate = lstBeds[lstBeds.Count - 1].AllocationDate;
                                Session["AllocDate"] = lstBeds[lstBeds.Count - 1].AllocationDate;
                                //ICU_Days++;

                                EntityICUInvoiceDetail obj = new EntityICUInvoiceDetail();
                                obj.ChargesId = item.ChargesId;
                                obj.TempId = lstInvoiceDetails.Count + 1;
                                obj.ChargesName = item.ChargeCategoryName;
                                obj.Charges = charge;
                                obj.NoofDays = ICU_Days;
                                obj.Quantity = 0;
                                obj.Amount = charge * ICU_Days;
                                obj.IsDelete = false;
                                Session["Days"] = ICU_Days;
                                lstInvoiceDetails.Add(obj);

                                GridView1.DataSource = lstInvoiceDetails;
                                GridView1.DataBind();
                                Session["BillDetails"] = lstInvoiceDetails;
                            }
                        }
                    }
                }
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
                List<EntityPatientMaster> lst = new PatientInvoiceBLL().GetPatientList(true);
                ddlPatient.DataSource = lst;
                lst.Insert(0, new EntityPatientMaster() { PatientId = 0, PatientFirstName = "--Select--" });
                ddlPatient.DataValueField = "PatientId";
                ddlPatient.DataTextField = "PatientFirstName";
                ddlPatient.DataBind();
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }

        }

        private void BindIPDBeds()
        {
            try
            {
                List<EntityBedMaster> lst = mobjDeptBLL.FreeBedsofIPD();
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
                List<EntityPatientMaster> lstPat = new PatientInvoiceBLL().GetPatientList(IsDischarge);
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
                ImageButton imgEdit = (ImageButton)sender;
                GridViewRow row = (GridViewRow)imgEdit.NamingContainer;
                Session["Bill_Id"] = Convert.ToInt32(dgvTestParameter.DataKeys[row.RowIndex].Value);

                List<EntityICUInvoiceDetail> tblEmp = mobjDeptBLL.SelectICUDischargeInvoiceDetails(Convert.ToInt32(Session["Bill_Id"]));

                if (tblEmp.Count > 0)
                {
                    int AdmitId = Convert.ToInt32(row.Cells[0].Text);
                    EntityPatientAdmit admit = new PatientMasterBLL().GetPatientAdmitDetails(AdmitId);
                    if (admit.IsDischarge.Value)
                    {
                        BindPatientList();
                    }
                    else
                    {
                        BindPatientList(false);
                    }
                    ddlPatient.Enabled = false;
                    ListItem item = ddlPatient.Items.FindByText(Convert.ToString(row.Cells[1].Text));
                    ddlPatient.SelectedValue = item.Value;
                    GridView1.DataSource = tblEmp;
                    GridView1.DataBind();
                    Session["BillDetails"] = tblEmp;
                    Session["Days"] = tblEmp[0].NoofDays;
                    txtBillDate.Text = Convert.ToString(row.Cells[2].Text);
                    txtDiscount.Text = Convert.ToString(decimal.Round(Convert.ToDecimal(row.Cells[4].Text) * 100 / Convert.ToDecimal(row.Cells[7].Text), 0));
                    txtTotal.Text = row.Cells[7].Text;
                    txtNetAmount.Text = row.Cells[3].Text;
                    txtVAT.Text = Convert.ToString(decimal.Round(Convert.ToDecimal(row.Cells[5].Text) * 100 / Convert.ToDecimal(row.Cells[7].Text), 0));
                    txtService.Text = Convert.ToString(decimal.Round(Convert.ToDecimal(row.Cells[6].Text) * 100 / Convert.ToDecimal(row.Cells[7].Text), 0));
                    txtBillDate.Enabled = false;
                    ddlBedMaster.Enabled = false;
                    chkDischarge.Enabled = false;
                    btnUpdate.Visible = true;
                    BtnSave.Visible = false;
                    btnUpdateCharge.Visible = false;
                    MultiView1.SetActiveView(View2);
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void btnUpdateCharge_Click(object sender, EventArgs e)
        {
            try
            {
                btnUpdateCharge.Visible = false;
                btnAdd.Visible = true;
                if (Session["tempid"] != null)
                {
                    int id = Convert.ToInt32(Session["tempid"]);
                    if (id > 0)
                    {
                        List<EntityICUInvoiceDetail> lst = (List<EntityICUInvoiceDetail>)Session["BillDetails"];
                        if (lst != null)
                        {
                            foreach (EntityICUInvoiceDetail item in lst)
                            {
                                if (id == item.TempId)
                                {
                                    item.Charges = Convert.ToDecimal(txtCharges.Text);
                                    item.NoofDays = string.IsNullOrEmpty(txtNoofDays.Text) == false ? Convert.ToInt32(txtNoofDays.Text) : 0;
                                    item.Quantity = string.IsNullOrEmpty(txtQuantity.Text) == false ? Convert.ToInt32(txtQuantity.Text) : 0;
                                    item.Amount = string.IsNullOrEmpty(txtAmount.Text) == false ? Convert.ToDecimal(txtAmount.Text) : 0;
                                    break;
                                }
                            }
                            ClearOther();
                            GridView1.DataSource = lst.Where(p => p.IsDelete == false).ToList();
                            GridView1.DataBind();
                            txtTotal.Text = Convert.ToString(lst.Where(p => p.IsDelete == false).Sum(p => p.Amount));
                            Calculation();
                            Session["BillDetails"] = lst;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
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
                lblMsg.Text = string.Empty;
                //Session["MyFlag"] = "ADD";
                BindPatientList(false);
                btnUpdate.Visible = false;
                BtnSave.Visible = true;
                //CalBillDate.StartDate = DateTime.Now.Date;
                txtBillDate.Enabled = true;
                btnAdd.Visible = true;
                btnUpdateCharge.Visible = false;
                lblMsg.Text = string.Empty;
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
            //ClearOther();
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
            List<EntitySelectICUInvoiceDetail> ldtRoom = mobjDeptBLL.GetPatientInvoice();

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
                        lblMsg.Text = "Please Select Bill Date.";
                        CalBillDate.Focus();
                        return;
                    }
                    else
                    {
                        tblPatientMaster obj = new PatientMasterBLL().GetPatientbyId(Convert.ToInt32(ddlPatient.SelectedValue));
                        if (Convert.ToBoolean(obj.IsDeath))
                        {
                            if (ddlBedMaster.SelectedIndex > 0)
                            {
                                lblMsg.Text = "This patient can not be re-admitted. This patient is already passed away.";
                                ddlBedMaster.SelectedIndex = 0;
                            }
                            else
                            {
                                SaveICUInvoiceDetails(entInvoice);
                            }
                        }
                        else
                        {
                            SaveICUInvoiceDetails(entInvoice);
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

        private void SaveICUInvoiceDetails(EntityICUInvoice entInvoice)
        {
            try
            {
                int Invoice = 0;
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
                ///Shift to IPD
                EntityBedAllocToPatient entBedAlloc = new EntityBedAllocToPatient();
                entBedAlloc.BedId = Convert.ToInt32(ddlBedMaster.SelectedValue);
                EntityBedMaster lstB = new BedStatusBLL().GetFloorRoomBed(ddlBedMaster.SelectedValue);
                entBedAlloc.FloorId = lstB.FloorNo;
                entBedAlloc.RoomId = lstB.RoomId;
                entBedAlloc.PatientId = Convert.ToInt32(ddlPatient.SelectedValue);
                entBedAlloc.ShiftDate = StringExtension.ToDateTime(txtBillDate.Text);
                entBedAlloc.AllocationDate = StringExtension.ToDateTime(txtBillDate.Text);
                entBedAlloc.Is_ShiftTo_IPD = true;
                entBedAlloc.Is_ShiftTo_ICU = false;
                entBedAlloc.ShiftBedId = Convert.ToInt32(Session["CurrentBedID"]);
                int lintCnt = new BedStatusBLL().UpdateShiftBed(entBedAlloc);

                entInvoice.BedId = Convert.ToInt32(Session["CurrentBedID"]);
                entInvoice.ShiftDate = DateTime.Now.Date;
                List<EntityICUInvoiceDetail> lstInvoice = (List<EntityICUInvoiceDetail>)Session["BillDetails"];
                Invoice = mobjDeptBLL.InsertInvoice(entInvoice, lstInvoice, chkIsCash.Checked);
                if (Invoice > 0)
                {
                    GetICUInvoice();
                    lblMessage.Text = "Record Inserted Successfully";
                }
                else
                {
                    lblMessage.Text = "Record Not Inserted...";
                }
                Session["BillDetails"] = new List<EntityInvoiceDetails>();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            int lintCnt = 0;
            try
            {
                List<EntityICUInvoiceDetail> lstUpdate = (List<EntityICUInvoiceDetail>)Session["BillDetails"];
                EntityICUInvoice entInvoice = new EntityICUInvoice();
                entInvoice.PatientId = Convert.ToInt32(ddlPatient.SelectedValue);
                entInvoice.TotalAmount = Convert.ToDecimal(txtTotal.Text);
                entInvoice.AllocationDate = StringExtension.ToDateTime(txtBillDate.Text);
                entInvoice.NetAmount = Convert.ToDecimal(txtNetAmount.Text);
                entInvoice.InvoiceNo = Convert.ToInt32(Session["Bill_Id"]);
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
                lintCnt = mobjDeptBLL.UpdateInvoice(lstUpdate, entInvoice);
                GetICUInvoice();
                txtBillDate.Enabled = true;
                ddlBedMaster.Enabled = true;
                chkDischarge.Enabled = true;
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
            MultiView1.SetActiveView(View1);
        }

        protected void btnEditCharges_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton imgEdit = (ImageButton)sender;
                GridViewRow row = (GridViewRow)imgEdit.NamingContainer;
                Session["tempid"] = Convert.ToInt32(GridView1.DataKeys[row.RowIndex].Value);
                if (row != null)
                {
                    List<EntityICUInvoiceDetail> lst = (List<EntityICUInvoiceDetail>)Session["BillDetails"];
                    if (lst != null)
                    {
                        EntityICUInvoiceDetail objEdited = (from tmp in lst
                                                            where tmp.TempId == Convert.ToInt32(Session["tempid"])
                                                            select tmp).FirstOrDefault();
                        if (objEdited != null)
                        {
                            txtNoofDays.Text = Convert.ToString(objEdited.NoofDays);
                            txtQuantity.Text = Convert.ToString(objEdited.Quantity);
                            txtCharges.Text = Convert.ToString(objEdited.Charges);
                            txtAmount.Text = Convert.ToString(objEdited.Amount);
                            ListItem objCharge = ddlOther.Items.FindByText(objEdited.ChargesName);
                            if (objCharge != null)
                            {
                                ddlOther.SelectedValue = objCharge.Value;
                            }
                            btnUpdateCharge.Visible = true;
                            btnAdd.Visible = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton imgDelete = (ImageButton)sender;
                GridViewRow row = (GridViewRow)imgDelete.NamingContainer;
                if (row.RowIndex == 0)
                {
                    lblMsg.Text = "You can not remove bed charges.";
                }
                else
                {
                    Session["Delete_Charge"] = Convert.ToInt32(row.Cells[0].Text);
                    List<EntityICUInvoiceDetail> lstFinal = new List<EntityICUInvoiceDetail>();
                    if (BtnSave.Visible)
                    {
                        List<EntityICUInvoiceDetail> lst = (List<EntityICUInvoiceDetail>)Session["BillDetails"];

                        foreach (EntityICUInvoiceDetail item in lst)
                        {
                            if (item.ChargesId != Convert.ToInt32(row.Cells[0].Text))
                            {
                                lstFinal.Add(item);
                            }
                        }
                        GridView1.DataSource = lstFinal;
                        GridView1.DataBind();
                        Session["BillDetails"] = lstFinal;
                        txtTotal.Text = Convert.ToString(lstFinal.Sum(p => p.Amount));
                    }
                    if (btnUpdate.Visible)
                    {
                        List<EntityICUInvoiceDetail> lst = (List<EntityICUInvoiceDetail>)Session["BillDetails"];

                        foreach (EntityICUInvoiceDetail item in lst)
                        {
                            if (item.ChargesId == Convert.ToInt32(row.Cells[0].Text))
                            {
                                item.IsDelete = true;
                            }
                        }
                        Session["BillDetails"] = lst;
                        GridView1.DataSource = lst.Where(p => p.IsDelete == false).ToList();
                        GridView1.DataBind();
                        txtTotal.Text = Convert.ToString(lst.Where(p => p.IsDelete == false).ToList().Sum(p => p.Amount));
                    }
                    Calculation();
                }
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
                dgvTestParameter.DataSource = (List<EntitySelectICUInvoiceDetail>)Session["RoomDetails"];
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

        

        protected void txtBillDate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlPatient.SelectedIndex > 0)
                {
                    List<EntityBedAllocToPatient> lstBeds = mobjDeptBLL.GetICUAllocatedBedDetails(Convert.ToInt32(ddlPatient.SelectedValue)).Where(p => p.CategoryDesc.Equals("ICU")).ToList();
                    bool Status = false;
                    foreach (EntityBedAllocToPatient item in lstBeds)
                    {
                        if (item.ShiftDate == null)
                        {
                            Status = false;
                            break;
                        }
                        else
                        {
                            Status = true;
                        }
                    }
                    if (Status)
                    {
                        lblMsg.Text = "Patient Already Shifted to IPD. Please Shift to ICU again.";
                        ddlPatient.SelectedIndex = 0;
                        txtBillDate.Text = string.Empty;
                    }
                    else
                    {
                        ddlPatient_OnSelectedIndexChanged(sender, e);
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnAddCharge_Click(object sender, EventArgs e)
        {
            try
            {
                lblMsg.Text = string.Empty;
                if (ddlPatient.SelectedIndex > 0)
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
                        lblMsg.Text = "This Charge already added.";
                        GridView1.DataSource = lst;
                        GridView1.DataBind();
                    }
                    else
                    {
                        EntityICUInvoiceDetail obj = new EntityICUInvoiceDetail()
                        {
                            ChargesId = Convert.ToInt32(ddlOther.SelectedValue),
                            ChargesName = ddlOther.SelectedItem.Text,
                            Charges = Convert.ToDecimal(txtCharges.Text),
                        };
                        if (!string.IsNullOrEmpty(txtNoofDays.Text))
                        {
                            obj.NoofDays = Convert.ToInt32(txtNoofDays.Text);
                        }
                        else
                        {
                            obj.NoofDays = 0;
                        }
                        if (!string.IsNullOrEmpty(txtQuantity.Text))
                        {
                            obj.Quantity = Convert.ToInt32(txtQuantity.Text);
                        }
                        else
                        {
                            obj.Quantity = 0;
                        }
                        if (obj.NoofDays > 0)
                        {
                            obj.Amount = obj.Charges * obj.NoofDays;
                        }
                        if (obj.Quantity > 0)
                        {
                            obj.Amount = obj.Charges * obj.Quantity;
                        }
                        obj.TempId = lst.Count + 1;
                        obj.IsDelete = false;
                        lst.Add(obj);
                        Session["BillDetails"] = lst;
                        GridView1.DataSource = lst.Where(p => p.IsDelete == false).ToList();
                        GridView1.DataBind();
                        txtTotal.Text = Convert.ToString(lst.Where(p => p.IsDelete == false).ToList().Sum(p => p.Amount));
                        Calculation();
                        ClearOther();
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
            txtNoofDays.Text = string.Empty;
            txtQuantity.Text = string.Empty;
            txtCharges.Text = string.Empty;
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
                txtNoofDays.Text = Convert.ToString(Session["Days"]);
            }
            else
            {
                //ClearOther();
            }
            Calculation();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            ImageButton imgEdit = (ImageButton)sender;
            GridViewRow row = (GridViewRow)imgEdit.NamingContainer;
            //Session["ICUInvoiceNo"] = Convert.ToInt32(dgvTestParameter.DataKeys[row.RowIndex].Value);
            //Session["Patient__ID"] = Convert.ToString(row.Cells[0].Text);
            //Session["ReportType"] = "ICU_MainReport";
            //Response.Redirect("~/PathalogyReport/PathologyReport.aspx", false);
            Response.Redirect("~/PathalogyReport/PathologyReport.aspx?ReportType=ICU_MainReport&ICUInvoiceNo=" + dgvTestParameter.DataKeys[row.RowIndex].Value + "&Patient__ID=" + row.Cells[0].Text, false);
        }

    }
}