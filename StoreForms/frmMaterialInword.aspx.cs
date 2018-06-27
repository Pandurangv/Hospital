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
using System.Globalization;

namespace Hospital.StoreForms
{
    public partial class frmMaterialInword : System.Web.UI.Page
    {
        MterialInwordBLL mobjMaterialInwardBLL = new MterialInwordBLL();
        decimal mdcTotalAmount = 0.0M;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //SessionManager.Instance.SetSession();
                txtStartDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtEndDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                GetSupplierForView();
                GetGroupForSave();
                GetSupplierForSave();
                BtnDelete.Enabled = false;
            }
        }

        protected void BtnAddNew_Click(object sender, EventArgs e)
        {
            DataTable ldt = new DataTable();
            txtInwardNo.Text = string.Empty;
            txtInwordDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            ddlSupplierName.SelectedIndex = 0;
            ddlGroup.SelectedIndex = 0;
            ldt = mobjMaterialInwardBLL.GetNewInwardNo();
            txtInwardNo.Text = ldt.Rows[0]["InwordNo"].ToString();
            pnlGridItem.Style.Add(HtmlTextWriterStyle.Display, "none");
            hdnPanel.Value = "none";
            Session["SumAmount"] = 0.0M;
            mdcTotalAmount = 0.0M;
            lblTotalInwardAmount.Text = "0.00";
            this.programmaticModalPopup.Show();
        }


        protected void GetGroupForSave()
        {
            DataTable ldtGroup = new DataTable();
            ldtGroup = mobjMaterialInwardBLL.GetGroup();
            if (ldtGroup.Rows.Count > 0 && ldtGroup != null)
            {
                ddlGroup.DataSource = ldtGroup;
                ddlGroup.DataValueField = "PKId";
                ddlGroup.DataTextField = "GroupDesc";
                ddlGroup.DataBind();

                ListItem li = new ListItem();
                li.Text = "--Select Group--";
                li.Value = "0";
                ddlGroup.Items.Insert(0, li);
            }
        }


        protected void GetSupplierForSave()
        {
            DataTable ldtSupplier = new DataTable();
            ldtSupplier = mobjMaterialInwardBLL.GetSupplier();
            if (ldtSupplier.Rows.Count > 0 && ldtSupplier != null)
            {
                ddlSupplierName.DataSource = ldtSupplier;
                ddlSupplierName.DataValueField = "SupplierCode";
                ddlSupplierName.DataTextField = "SupplierName";
                ddlSupplierName.DataBind();

                ListItem li = new ListItem();
                li.Value = "0";
                li.Text = "--Select Supplier--";
                ddlSupplierName.Items.Insert(0, li);
            }
        }


        protected void GetSupplierForView()
        {
            DataTable ldtSupplier = new DataTable();
            ldtSupplier = mobjMaterialInwardBLL.GetSupplier();
            if (ldtSupplier.Rows.Count > 0 && ldtSupplier != null)
            {
                ddlSupplier.DataSource = ldtSupplier;
                ddlSupplier.DataValueField = "SupplierCode";
                ddlSupplier.DataTextField = "SupplierName";
                ddlSupplier.DataBind();

                ListItem li = new ListItem();
                li.Value = "0";
                li.Text = "--Select Supplier--";
                ddlSupplier.Items.Insert(0, li);
            }
        }


        protected void GetAllMaterialInward()
        {
            DataTable ldtMaterialInword = new DataTable();
            ldtMaterialInword = mobjMaterialInwardBLL.GetMaterialInward();
            if (ldtMaterialInword.Rows.Count > 0 && ldtMaterialInword != null)
            {
                dgvMaterialInwardShow.DataSource = ldtMaterialInword;
                dgvMaterialInwardShow.DataBind();
                Session["MaterialInward"] = ldtMaterialInword;
                int lintRowcount = ldtMaterialInword.Rows.Count;
                lblRowCount.Text = "<b>Total Records:</b> " + lintRowcount.ToString();
                PanelView.Style.Add(HtmlTextWriterStyle.Display, "");
                hdnPanel.Value = "";
            }
            else
            {
                PanelView.Style.Add(HtmlTextWriterStyle.Display, "none");
                hdnPanel.Value = "none";
                Commons.ShowMessage("No Data To Display....", this.Page);
            }
        }




        protected void BtnSave_Click(object sender, EventArgs e)
        {
            List<EntityMatrialInword> lstentMaterialInward = new List<EntityMatrialInword>();

            int lintcnt = 0;
            foreach (GridViewRow drv in dgvMaterialInword.Rows)
            {
                CheckBox chkSelect = (CheckBox)drv.FindControl("chkSelect");
                if (chkSelect.Checked)
                {

                    EntityMatrialInword entMaterialInward = new EntityMatrialInword();
                    entMaterialInward.Group = Convert.ToInt32(ddlGroup.SelectedValue);
                    if (ddlSupplierName.SelectedIndex > 0)
                    {
                        entMaterialInward.SupplierCode = ddlSupplierName.SelectedItem.Value;
                        entMaterialInward.SupplierName = ddlSupplierName.SelectedItem.Text;
                        entMaterialInward.InwardNo = txtInwardNo.Text;
                        entMaterialInward.InwardDate = StringExtension.ToDateTime(txtInwordDate.Text);
                        Label lblItemCode = (Label)drv.FindControl("lblItemCode");
                        Label lblItemDesc = (Label)drv.FindControl("lblItemDesc");
                        TextBox txtQty = (TextBox)drv.FindControl("txtQty");
                        Label lblUnitCode = (Label)drv.FindControl("lblUnitCode");
                        Label lblRate = (Label)drv.FindControl("lblRate");
                        Label lblTotalAmount = (Label)drv.FindControl("lblTotalAmount");
                        TextBox txtOtherCharges = (TextBox)drv.FindControl("txtOtherCharges");
                        Label lblTotalProductAmount = (Label)drv.FindControl("lblTotalProductAmount");
                        entMaterialInward.ItemCode = lblItemCode.Text;
                        entMaterialInward.ItemDesc = lblItemDesc.Text;
                        entMaterialInward.Quantity = Convert.ToDecimal(txtQty.Text);
                        entMaterialInward.unit = lblUnitCode.Text;
                        entMaterialInward.Rate = Convert.ToDecimal(lblRate.Text);
                        entMaterialInward.TotalAmount = Convert.ToDecimal(lblTotalAmount.Text);
                        entMaterialInward.OtherCharges = Convert.ToDecimal(txtOtherCharges.Text);
                        entMaterialInward.TotalProductAmount = Convert.ToDecimal(lblTotalProductAmount.Text);
                        entMaterialInward.TotalInwardAmount = Convert.ToDecimal(lblTotalInwardAmount.Text);
                        entMaterialInward.EntryBy = SessionManager.Instance.LoginUser.EmpCode;
                        lstentMaterialInward.Add(entMaterialInward);
                    }
                }
            }
            if (lstentMaterialInward.Count > 0)
            {
                EntityMatrialInword entMaterialInward = new EntityMatrialInword();
                entMaterialInward.SupplierCode = ddlSupplierName.SelectedItem.Value;
                entMaterialInward.SupplierName = ddlSupplierName.SelectedItem.Text;
                entMaterialInward.InwardNo = txtInwardNo.Text;
                entMaterialInward.InwardDate = StringExtension.ToDateTime(txtInwordDate.Text);
                entMaterialInward.TotalInwardAmount = Convert.ToDecimal(lblTotalInwardAmount.Text);
                entMaterialInward.EntryBy = SessionManager.Instance.LoginUser.EmpCode;
                lintcnt = mobjMaterialInwardBLL.InsertMaterialInward(lstentMaterialInward, entMaterialInward);
                if (lintcnt > 0)
                {
                    GetAllMaterialInward();
                    Commons.ShowMessage("Record Inserted Successfully", this.Page);
                    this.programmaticModalPopup.Hide();
                }
                else
                {
                    Commons.ShowMessage("Record Not Inserted", this.Page);
                }
            }
            //else
            //{
            //    this.programmaticModalPopup.Show();
            //}
        }


        protected void btnShowAll_Click(object sender, EventArgs e)
        {
            GetAllMaterialInward();
        }


        protected void chkSelect_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chk.NamingContainer;
            programmaticModalPopup.Show();
            TextBox txtQuantity = (TextBox)row.FindControl("txtQty");
            Label lblRate = (Label)row.FindControl("lblRate");
            TextBox txtOtherCharges = (TextBox)row.FindControl("txtOtherCharges");
            Label lblTotalProductAmount = (Label)row.FindControl("lblTotalProductAmount");
            Label lblTotalAmount = (Label)row.FindControl("lblTotalAmount");

            decimal qty = Commons.ConvertToDecimal(txtQuantity.Text);
            decimal rate = Commons.ConvertToDecimal(lblRate.Text);
            decimal totalAmount = qty * rate;
            lblTotalAmount.Text = totalAmount.ToString();
            decimal OtherCharges = Commons.ConvertToDecimal(txtOtherCharges.Text);
            decimal TotalProductAmount = totalAmount + OtherCharges;
            lblTotalProductAmount.Text = TotalProductAmount.ToString();
            lblTotalInwardAmount.Text = TotalProductAmount.ToString();
            mdcTotalAmount = Commons.ConvertToDecimal(lblTotalInwardAmount.Text);

            if (chk.Checked)
            {
                if (!string.IsNullOrEmpty(txtQuantity.Text))
                {
                    if (!string.IsNullOrEmpty(lblRate.Text))
                    {
                        if (!string.IsNullOrEmpty(txtOtherCharges.Text))
                        {
                            BtnSave.Enabled = true;
                            txtQuantity.Enabled = true;

                            if (Session["SumAmount"] == null)
                            {
                                Session["SumAmount"] = mdcTotalAmount;
                            }
                            else
                            {
                                mdcTotalAmount = (decimal)Session["SumAmount"] + mdcTotalAmount;
                                Session["SumAmount"] = mdcTotalAmount;
                            }
                            lblTotalInwardAmount.Text = mdcTotalAmount.ToString();
                            txtOtherCharges.ReadOnly = true;
                            txtQuantity.ReadOnly = true;
                        }
                    }
                }
            }
            else
            {
                if (Session["SumAmount"] == null)
                {
                    Session["SumAmount"] = mdcTotalAmount;
                }
                else
                {
                    mdcTotalAmount = (decimal)Session["SumAmount"] - mdcTotalAmount;
                    Session["SumAmount"] = mdcTotalAmount;
                }

                lblTotalInwardAmount.Text = mdcTotalAmount.ToString();
                txtQuantity.Text = string.Empty;
                txtOtherCharges.Text = string.Empty;
                lblTotalAmount.Text = string.Empty;
                lblTotalProductAmount.Text = string.Empty;
                txtOtherCharges.ReadOnly = false;
                txtQuantity.ReadOnly = false;
            }

            this.programmaticModalPopup.Show();

        }


        protected void dgvMaterialInwardShow_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                DataTable ldt = new DataTable();
                DataTable ldtInward = new DataTable();
                if (e.CommandName == "ShowInward")
                {
                    this.programmaticModalPopupEdit.Show();
                    int linIndex = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                    LinkButton lnkInwardno = (LinkButton)gvr.FindControl("lnkInwardNo");
                    string lstrInwardNo = lnkInwardno.Text;
                    lblInwardNumber.Text = lstrInwardNo;
                    ldt = mobjMaterialInwardBLL.GetInwardDTStatus(lstrInwardNo);
                    FillControls(ldt);
                    dgvInwardStatus.DataSource = ldt;
                    dgvInwardStatus.DataBind();
                    this.programmaticModalPopupEdit.Show();
                }
            }
            catch (Exception ex)
            {
                Commons.FileLog("StoreForms_frmMaterialInword -  dgvMaterialInwardShow_RowCommand(object sender, GridViewCommandEventArgs e)", ex);
            }
        }

        private void FillControls(DataTable ldt)
        {
            lblGroupType.Text = ldt.Rows[0]["GroupDesc"].ToString();
            lblInwardDate.Text = ldt.Rows[0]["InwardDate"].ToString();
            lblInwardNum.Text = ldt.Rows[0]["InwardNo"].ToString();
            lblSupplierCode.Text = ldt.Rows[0]["SupplierCode"].ToString();
            lblSupplierNM.Text = ldt.Rows[0]["SupplierName"].ToString();
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtStartDate.Text.Trim().Length <= 0 || txtEndDate.Text.Trim().Length <= 0)
                {
                    Commons.ShowMessage("Start Date and End Date Should Not be Blank.", this.Page);
                    return;
                }
                DateTime ldtStart = DateTime.ParseExact(txtStartDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime ldtEnd = DateTime.ParseExact(txtEndDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                if (ldtStart > ldtEnd)
                {
                    Commons.ShowMessage("Start Date Should Not be Greater Than End Date.", this.Page);
                    return;
                }
                string lstrInwardNo = txtInwardNumber.Text.Trim();

                if (lstrInwardNo != string.Empty)
                {
                    ldtStart = StringExtension.ToDateTime("01/01/1753");
                    ldtEnd = StringExtension.ToDateTime("01/01/1753");

                    DataTable ldt = new DataTable();
                    ldt = mobjMaterialInwardBLL.GetFilteredInward(ldtStart, ldtEnd, lstrInwardNo);

                    if (ldt.Rows.Count > 0)
                    {
                        dgvMaterialInwardShow.DataSource = ldt;
                        dgvMaterialInwardShow.DataBind();
                        int lintRowcount = ldt.Rows.Count;
                        lblRowCount.Text = "<b>Total Records:</b> " + lintRowcount.ToString();
                        PanelView.Style.Add(HtmlTextWriterStyle.Display, "");
                        hdnPanel.Value = "";
                    }
                }
                else
                {
                    PanelView.Style.Add(HtmlTextWriterStyle.Display, "none");
                    hdnPanel.Value = "none";
                    Commons.ShowMessage("No Data To Display.. ", this.Page);
                }
            }
            catch (Exception ex)
            {
                Commons.FileLog("StoreForms_frmMaterialInword -  btnView_Click(object sender, EventArgs e)", ex);
            }
        }


        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            EntityMatrialInword entIward = new EntityMatrialInword();
            DataTable ldt = new DataTable();
            if (ddlGroup.SelectedIndex > 0)
            {
                entIward.Group = Convert.ToInt32(ddlGroup.SelectedValue);
                ldt = mobjMaterialInwardBLL.GetItemByGroup(entIward);
                if (ldt.Rows.Count > 0 && ldt != null)
                {
                    dgvMaterialInword.DataSource = ldt;
                    dgvMaterialInword.DataBind();
                    pnlGridItem.Style.Add(HtmlTextWriterStyle.Display, "");
                    hdnPanel.Value = "";
                }
                else
                {
                    pnlGridItem.Style.Add(HtmlTextWriterStyle.Display, "none");
                    hdnPanel.Value = "none";
                }
            }
            else
            {
                pnlGridItem.Style.Add(HtmlTextWriterStyle.Display, "none");
                hdnPanel.Value = "none";
            }
            this.programmaticModalPopup.Show();
        }


        protected void chkDelete_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chk.NamingContainer;
            if (chk.Checked)
            {
                LinkButton lnkInwardNo = (LinkButton)row.FindControl("lnkInwardNo");
                Session["InwardNo"] = lnkInwardNo.Text;
                lblMessage.Text = string.Empty;
                BtnDelete.Enabled = true;
            }
            else
            {
                Session["Inwardno"] = string.Empty;
                BtnDelete.Enabled = false;
            }
        }

        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow drv in dgvMaterialInwardShow.Rows)
            {
                CheckBox chkDelete = (CheckBox)drv.FindControl("chkDelete");
                if (chkDelete.Checked)
                {
                    this.modalpopupDelete.Show();
                }
            }
        }

        protected void BtnDeleteOk_Click(object sender, EventArgs e)
        {
            List<EntityItem> lstentItemMaster = new List<EntityItem>();

            List<EntityMatrialInword> lstentInward = new List<EntityMatrialInword>();

            //List<EntityMatrialInword> lstentInward = new List<EntityMatrialInword>();

            int cnt = 0;

            try
            {
                foreach (GridViewRow drv in dgvMaterialInwardShow.Rows)
                {
                    CheckBox chkDelete = (CheckBox)drv.FindControl("chkDelete");
                    if (chkDelete.Checked)
                    {
                        EntityMatrialInword entInward = new EntityMatrialInword();
                        string lstrInwardNo = string.Empty;
                        string lstrSupplierCode = string.Empty;
                        decimal ldcTotInwardAmnt = 0.0M;

                        LinkButton lnkInwardNo = (LinkButton)drv.FindControl("lnkInwardNo");
                        Label lbldgvSupplier = (Label)drv.FindControl("lbldgvSupplierCode");
                        Label lbldgvTotalinwardAmt = (Label)drv.FindControl("lbldgvTotalInwardAmt");
                        lstrSupplierCode = lbldgvSupplier.Text;
                        lstrInwardNo = lnkInwardNo.Text;
                        ldcTotInwardAmnt = Convert.ToDecimal(lbldgvTotalinwardAmt.Text);

                        entInward.InwardNo = lstrInwardNo;
                        entInward.SupplierCode = lstrSupplierCode;
                        entInward.TotalInwardAmount = ldcTotInwardAmnt;
                        DataTable ldtItemDetail = new DataTable();
                        ldtItemDetail = mobjMaterialInwardBLL.GetItemCode(lstrInwardNo);
                        if (ldtItemDetail.Rows.Count > 0 && ldtItemDetail != null)
                        {
                            foreach (DataRow dr in ldtItemDetail.Rows)
                            {
                                EntityItem entItemMaster = new EntityItem();
                                entItemMaster.ItemCode = dr["ItemCode"].ToString();
                                entItemMaster.OpeningBalance = Convert.ToDecimal(dr["Quantity"]);
                                lstentItemMaster.Add(entItemMaster);
                            }
                        }
                        lstentInward.Add(entInward);
                    }
                }

                if (lstentInward.Count > 0)
                {
                    foreach (EntityMatrialInword entInward in lstentInward)
                    {
                        cnt = mobjMaterialInwardBLL.DeleteMaterialInward(lstentItemMaster, entInward);
                    }

                    if (cnt > 0)
                    {
                        this.modalpopupDelete.Hide();
                        Commons.ShowMessage("Record Deleted Successfully", this.Page);
                        GetAllMaterialInward();
                        if (dgvMaterialInwardShow.Rows.Count <= 0)
                        {
                            pnlShow.Style.Add(HtmlTextWriterStyle.Display, "none");
                            hdnPanel.Value = "none";
                        }
                    }
                    else
                    {
                        this.modalpopupDelete.Hide();
                        Commons.ShowMessage("Record Not Deleted...", this.Page);
                    }

                }

            }
            catch (System.Threading.ThreadAbortException)
            {
            }
            catch (Exception ex)
            {
                Commons.FileLog("StoreForms_frmMaterialInword -   BtnDeleteOk_Click(object sender, EventArgs e)", ex);
            }
        }

        protected void dgvMaterialInwardShow_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvMaterialInwardShow.PageIndex = e.NewPageIndex;
        }

        protected void dgvMaterialInwardShow_RowDataBound(object sender, GridViewRowEventArgs e)
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
                Commons.FileLog("StoreForms_frmMaterialInword -  dgvMaterialInwardShow_RowDataBound(object sender, GridViewRowEventArgs e)", ex);
            }
        }

        protected void dgvMaterialInwardShow_DataBound(object sender, EventArgs e)
        {
            int lintCurrentPage = dgvMaterialInwardShow.PageIndex + 1;
            lblPageCount.Text = "<b>Page</b> " + lintCurrentPage.ToString() + "<b> of </b>" + dgvMaterialInwardShow.PageCount.ToString();
        }

        protected void dgvMaterialInwardShow_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dgvMaterialInwardShow.DataSource = (DataTable)Session["ItemDetail"];
                dgvMaterialInwardShow.DataBind();
            }
            catch (Exception ex)
            {
                Commons.FileLog("StoreForms_frmMaterialInword - dgvMaterialInwardShow_PageIndexChanged(object sender, EventArgs e)", ex);
            }
        }


        protected void ddlSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            EntityMatrialInword entInward = new EntityMatrialInword();
            DataTable ldt = new DataTable();
            if (ddlSupplier.SelectedIndex > 0)
            {
                entInward.Group = Convert.ToInt32(ddlSupplier.SelectedValue);
                ldt = mobjMaterialInwardBLL.GetItemByGroup(entInward);
                if (ldt.Rows.Count > 0 && ldt != null)
                {
                    dgvMaterialInword.DataSource = ldt;
                    dgvMaterialInword.DataBind();
                    pnlGridItem.Style.Add(HtmlTextWriterStyle.Display, "");
                    hdnPanel.Value = "";
                }
                else
                {
                    pnlGridItem.Style.Add(HtmlTextWriterStyle.Display, "none");
                    hdnPanel.Value = "none";
                }
            }
            else
            {
                pnlGridItem.Style.Add(HtmlTextWriterStyle.Display, "none");
                hdnPanel.Value = "none";
            }
            this.programmaticModalPopup.Show();
        }

    }
}