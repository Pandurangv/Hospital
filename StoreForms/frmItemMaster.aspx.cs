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

namespace Hospital.StoreForms
{
    public partial class frmItemMaster : System.Web.UI.Page
    {
        ItemBLL mobjItemBLL = new ItemBLL();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                //SessionManager.Instance.SetSession();
                BtnDelete.Enabled = false;
                GetItem();
                GetUnit();
                GetUnitForEdit();
                GetSupplier();
                GetSupplierForEdit();
                GetGroup();
                GetGroupForEdit();
            }
        }


        public void GetItem()
        {
            DataTable ldtItem = new DataTable();
            ldtItem = mobjItemBLL.GetAllItem();
            if (ldtItem.Rows.Count > 0 && ldtItem != null)
            {
                dgvItem.DataSource = ldtItem;
                dgvItem.DataBind();
                Session["ItemDetails"] = ldtItem;
                int lintRowcount = ldtItem.Rows.Count;
                lblRowCount.Text = "<b>Total Records:</b> " + lintRowcount.ToString();
                pnlShow.Style.Add(HtmlTextWriterStyle.Display, "");
                hdnPanel.Value = "";
            }
            else
            {
                pnlShow.Style.Add(HtmlTextWriterStyle.Display, "none");
                hdnPanel.Value = "none";
            }
        }

        public void GetUnit()
        {
            DataTable ldtUnit = new DataTable();
            ldtUnit = mobjItemBLL.GetUnit();

            ddlUnit.DataSource = ldtUnit;
            ddlUnit.DataValueField = "UnitCode";
            ddlUnit.DataTextField = "UnitDesc";
            ddlUnit.DataBind();

            ListItem li = new ListItem();
            li.Value = "0";
            li.Text = "--Select Unit--";
            ddlUnit.Items.Insert(0, li);

        }
        public void GetUnitForEdit()
        {
            DataTable ldtUnit = new DataTable();
            ldtUnit = mobjItemBLL.GetUnit();

            ddlEditUnit.DataSource = ldtUnit;
            ddlEditUnit.DataValueField = "UnitCode";
            ddlEditUnit.DataTextField = "UnitDesc";
            ddlEditUnit.DataBind();

            ListItem li = new ListItem();
            li.Value = "0";
            li.Text = "--Select Unit--";
            ddlEditUnit.Items.Insert(0, li);
        }


        public void GetSupplier()
        {
            DataTable ldtSupplier = new DataTable();
            ldtSupplier = mobjItemBLL.GetSupplier();

            ddlSupplier.DataSource = ldtSupplier;
            ddlSupplier.DataValueField = "SupplierCode";
            ddlSupplier.DataTextField = "SupplierName";
            ddlSupplier.DataBind();

            ListItem li = new ListItem();
            li.Value = "0";
            li.Text = "--Select Supplier--";
            ddlSupplier.Items.Insert(0, li);
        }

        public void GetSupplierForEdit()
        {
            DataTable ldtSupplier = new DataTable();
            ldtSupplier = mobjItemBLL.GetSupplier();

            ddlEditSupplier.DataSource = ldtSupplier;
            ddlEditSupplier.DataValueField = "SupplierCode";
            ddlEditSupplier.DataTextField = "SupplierName";
            ddlEditSupplier.DataBind();

            ListItem li = new ListItem();
            li.Value = "0";
            li.Text = "--Select Supplier--";
            ddlEditSupplier.Items.Insert(0, li);
        }


        public void GetGroup()
        {
            DataTable ldtGroup = new DataTable();
            ldtGroup = mobjItemBLL.GetGroup();

            ddlGroup.DataSource = ldtGroup;
            ddlGroup.DataValueField = "PKId";
            ddlGroup.DataTextField = "GroupDesc";
            ddlGroup.DataBind();

            ListItem li = new ListItem();
            li.Value = "0";
            li.Text = "--Select Group--";
            ddlGroup.Items.Insert(0, li);
        }

        public void GetGroupForEdit()
        {
            DataTable ldtGroup = new DataTable();
            ldtGroup = mobjItemBLL.GetGroup();

            ddlEditGroup.DataSource = ldtGroup;
            ddlEditGroup.DataValueField = "PKId";
            ddlEditGroup.DataTextField = "GroupDesc";
            ddlEditGroup.DataBind();

            ListItem li = new ListItem();
            li.Value = "0";
            li.Text = "--Select Group--";
            ddlEditGroup.Items.Insert(0, li);
        }

        protected void dgvItem_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                DataTable ldt = new DataTable();
                if (e.CommandName == "EditItem")
                {
                    this.programmaticModalPopupEdit.Show();
                    int linIndex = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                    LinkButton lnkItemCode = (LinkButton)gvr.FindControl("lnkItemCode");
                    string lstrItemCode = lnkItemCode.Text;
                    txtEditItemCode.Text = lstrItemCode;
                    ldt = mobjItemBLL.GetItemForEdit(lstrItemCode);
                    FillControls(ldt);
                }
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmItemMaster -  dgvItem_RowCommand(object sender, GridViewCommandEventArgs e)", ex);
            }
        }

        private void FillControls(DataTable ldt)
        {
            txtEditItemCode.Text = ldt.Rows[0]["ItemCode"].ToString();
            txtEditItemDesc.Text = ldt.Rows[0]["ItemDesc"].ToString();
            txtEditReorderLevel.Text = ldt.Rows[0]["ReorderLevel"].ToString();
            txtEditReorderMaxLevel.Text = ldt.Rows[0]["ReorderMaxLevel"].ToString();
            ddlEditUnit.Text = ldt.Rows[0]["UnitCode"].ToString();
            ddlEditSupplier.Text = ldt.Rows[0]["SupplierCode"].ToString();
            txtEditOpeningBalance.Text = ldt.Rows[0]["OpenningBalance"].ToString();
            txtEditRate.Text = ldt.Rows[0]["Rate"].ToString();
            ddlEditGroup.Text = ldt.Rows[0]["GroupId"].ToString();
            txtEditManifacturingDate.Text = StringExtension.ToDateTime(ldt.Rows[0]["ManifacturingDate"].ToString()).ToString("dd/MM/yyyy");
            txtEditExpiryDate.Text = StringExtension.ToDateTime(ldt.Rows[0]["ExpiryDate"].ToString()).ToString("dd/MM/yyyy");
        }

        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            int lintCnt = 0;
            try
            {
                EntityItem entItem = new EntityItem();

                entItem.ItemCode = txtEditItemCode.Text;
                entItem.ItemDesc = txtEditItemDesc.Text;
                entItem.ReorderLevel = Convert.ToInt32(txtEditReorderLevel.Text.Trim());
                entItem.ReorderMaxLevel = Convert.ToInt32(txtEditReorderMaxLevel.Text.Trim());
                entItem.UnitCode = ddlEditUnit.SelectedValue;
                entItem.SupplierCode = ddlEditSupplier.SelectedValue;
                entItem.OpeningBalance = Convert.ToDecimal(txtEditOpeningBalance.Text.Trim());
                entItem.Rate = Convert.ToDecimal(txtEditRate.Text.Trim());
                entItem.GroupId = Convert.ToInt32(ddlEditGroup.SelectedValue);
                entItem.ManifacturingDate = StringExtension.ToDateTime(txtEditManifacturingDate.Text);
                entItem.ExpiryDate = StringExtension.ToDateTime(txtEditExpiryDate.Text);
                entItem.ChangeBy = SessionManager.Instance.LoginUser.EmpCode;
                lintCnt = mobjItemBLL.UpdateItem(entItem);

                if (lintCnt > 0)
                {
                    GetItem();
                    Commons.ShowMessage("Record Updated Successfully", this.Page);
                    this.programmaticModalPopup.Hide();
                }
                else
                {
                    Commons.ShowMessage("Record Not Updated", this.Page);
                }
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmItemMaster -  BtnEdit_Click(object sender, EventArgs e)", ex);
            }
        }

        protected void chkDelete_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chk.NamingContainer;
            if (chk.Checked)
            {
                LinkButton ItemCode = (LinkButton)row.FindControl("lnkItemCode");
                Session["ItemCode"] = ItemCode.Text;
                lblMessage.Text = string.Empty;
                BtnDelete.Enabled = true;
            }
            else
            {
                Session["ItemCode"] = string.Empty;
                BtnDelete.Enabled = false;
            }
        }

        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow drv in dgvItem.Rows)
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
            EntityItem entItem = new EntityItem();
            List<EntityItem> lstentItem = new List<EntityItem>();
            DataTable ldt = new DataTable();
            int cnt = 0;
            try
            {

                foreach (GridViewRow drv in dgvItem.Rows)
                {
                    CheckBox chkDelete = (CheckBox)drv.FindControl("chkDelete");
                    if (chkDelete.Checked)
                    {
                        LinkButton lnkItemCode = (LinkButton)drv.FindControl("lnkItemCode");
                        string lstrItemCode = lnkItemCode.Text;
                        entItem.ItemCode = lstrItemCode;
                        entItem.DisContinued = true;
                        if (txtDeleteRemark.Text == string.Empty)
                        {
                            this.modalpopupDelete.Show();
                            lblMessage.Text = "Enter Delete Remarks";
                        }
                        else
                        {
                            entItem.DisContRemark = txtDeleteRemark.Text.Trim();
                            lstentItem.Add(entItem);
                            cnt = mobjItemBLL.DeleteItem(lstentItem);
                            if (cnt > 0)
                            {
                                Commons.ShowMessage("Record Deleted Successfully", this.Page);
                                GetItem();
                                if (dgvItem.Rows.Count <= 0)
                                {
                                    pnlShow.Style.Add(HtmlTextWriterStyle.Display, "none");
                                    hdnPanel.Value = "none";
                                }
                            }
                            else
                            {
                                Commons.ShowMessage("Record Not Deleted...", this.Page);
                            }
                        }
                    }
                }
                this.modalpopupDelete.Hide();
            }
            catch (System.Threading.ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                Commons.FileLog("frmItemMaster -   BtnDeleteOk_Click(object sender, EventArgs e)", ex);
            }
        }

        protected void dgvItem_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvItem.PageIndex = e.NewPageIndex;
        }

        protected void dgvItem_RowDataBound(object sender, GridViewRowEventArgs e)
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
                Commons.FileLog("frmItemMaster -  dgvItem_RowDataBound(object sender, GridViewRowEventArgs e)", ex);
            }
        }

        protected void dgvItem_DataBound(object sender, EventArgs e)
        {
            int lintCurrentPage = dgvItem.PageIndex + 1;
            lblPageCount.Text = "<b>Page</b> " + lintCurrentPage.ToString() + "<b> of </b>" + dgvItem.PageCount.ToString();
        }

        protected void dgvItem_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dgvItem.DataSource = (DataTable)Session["ItemDetail"];
                dgvItem.DataBind();
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmItemMaster - dgvItem_PageIndexChanged(object sender, EventArgs e)", ex);
            }
        }

        protected void BtnAddNewItem_Click(object sender, EventArgs e)
        {
            DataTable ldt = new DataTable();
            ldt = mobjItemBLL.GetNewItemCode();
            txtItemCode.Text = ldt.Rows[0]["ItemCode"].ToString();
            txtItemDesc.Text = string.Empty;
            txtReorderLevel.Text = string.Empty;
            txtReorderMaxLevel.Text = string.Empty;
            ddlUnit.SelectedIndex = 0;
            ddlSupplier.SelectedIndex = 0;
            txtOpeningBalance.Text = string.Empty;
            txtRate.Text = string.Empty;
            ddlGroup.SelectedIndex = 0;
            txtManifacturingDate.Text = string.Empty;
            txtExpiryDate.Text = string.Empty;
            //txtManifacturingDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            //txtExpiryDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            this.programmaticModalPopup.Show();
        }



        protected void BtnSave_Click(object sender, EventArgs e)
        {
            int lintcnt = 0;
            EntityItem entItem = new EntityItem();
            if (string.IsNullOrEmpty(txtItemCode.Text.Trim()))
            {
                Commons.ShowMessage("Enter Item Code", this.Page);
            }
            else
            {
                if (string.IsNullOrEmpty(txtItemDesc.Text.Trim()))
                {
                    Commons.ShowMessage("Enter Item Description", this.Page);
                }
                else
                {
                    entItem.ItemCode = txtItemCode.Text.Trim();
                    entItem.ItemDesc = txtItemDesc.Text.Trim();
                    entItem.ReorderLevel = Convert.ToInt32(txtReorderLevel.Text.Trim());
                    entItem.ReorderMaxLevel = Convert.ToInt32(txtReorderMaxLevel.Text.Trim());
                    entItem.UnitCode = ddlUnit.SelectedValue;
                    entItem.SupplierCode = ddlSupplier.SelectedValue;
                    entItem.OpeningBalance = Convert.ToDecimal(txtOpeningBalance.Text.Trim());
                    entItem.Rate = Convert.ToDecimal(txtRate.Text.Trim());
                    entItem.GroupId = Convert.ToInt32(ddlGroup.SelectedValue);
                    if (ChkIsExpire.Checked)
                    {
                        if (txtManifacturingDate.Text == string.Empty)
                        {
                            entItem.ManifacturingDate = System.DateTime.Today.Date;
                        }
                        else
                        {
                            entItem.ManifacturingDate = StringExtension.ToDateTime(txtManifacturingDate.Text);
                        }
                        if (txtEditExpiryDate.Text == string.Empty)
                        {
                            entItem.ExpiryDate = System.DateTime.Today.Date;
                        }
                        else
                        {
                            entItem.ExpiryDate = StringExtension.ToDateTime(txtExpiryDate.Text);
                        }
                        if (entItem.ManifacturingDate >= entItem.ExpiryDate)
                        {
                            Commons.ShowMessage("Manifacturing Date should be older than Expiry Date... ", this.Page);
                            return;
                        }
                        else
                        {
                            entItem.EntryBy = SessionManager.Instance.LoginUser.UserType;
                            lintcnt = mobjItemBLL.InsertItem(entItem);

                            if (lintcnt > 0)
                            {
                                GetItem();
                                Commons.ShowMessage("Record Inserted Successfully", this.Page);
                                this.programmaticModalPopup.Hide();
                            }
                            else
                            {
                                Commons.ShowMessage("Record Not Inserted", this.Page);
                            }
                        }
                    }
                    else
                    {
                        entItem.IsCheked = 0;
                        entItem.EntryBy = SessionManager.Instance.LoginUser.EmpCode;
                        lintcnt = mobjItemBLL.InsertItem(entItem);

                        if (lintcnt > 0)
                        {
                            GetItem();
                            Commons.ShowMessage("Record Inserted Successfully", this.Page);
                            this.programmaticModalPopup.Hide();
                        }
                        else
                        {
                            Commons.ShowMessage("Record Not Inserted", this.Page);
                        }
                    }
                }
            }
        }
    }
}