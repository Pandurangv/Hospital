﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hospital.Models.BusinessLayer;
using Hospital.Models.Models;
using Hospital.Models.DataLayer;
using System.Data;

namespace Hospital
{
    public partial class frmAnaesthetistMaster : System.Web.UI.Page
    {
        AnaesthetistBLL mobjReligionBLL = new AnaesthetistBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GetReligion();
                BindCategory();
            }
        }

        private void BindCategory()
        {
            try
            {
                DataTable tblCat = new OpeartionMasterBLL().GetAllCategoryName();
                DataRow dr = tblCat.NewRow();
                dr["CategoryId"] = 0;
                dr["CategoryName"] = "---Select---";
                tblCat.Rows.InsertAt(dr, 0);

                ddlCategoryName.DataSource = tblCat;
                ddlCategoryName.DataValueField = "CategoryId";
                ddlCategoryName.DataTextField = "CategoryName";
                ddlCategoryName.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        private void EditBindCategory()
        {
            try
            {
                DataTable tblCat = new OpeartionMasterBLL().GetAllCategoryName();
                DataRow dr = tblCat.NewRow();
                dr["CategoryId"] = 0;
                dr["CategoryName"] = "---Select---";
                tblCat.Rows.InsertAt(dr, 0);

                ddlEditCategoryName.DataSource = tblCat;
                ddlEditCategoryName.DataValueField = "CategoryId";
                ddlEditCategoryName.DataTextField = "CategoryName";
                ddlEditCategoryName.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void BtnAddNewReligion_Click(object sender, EventArgs e)
        {
            DataTable ldt = new DataTable();
            ldt = mobjReligionBLL.GetNewReligionCode();
            txtReligionCode.Text = ldt.Rows[0][0].ToString();
            txtReligionDesc.Text = string.Empty;
            ddlCategoryName.SelectedIndex = 0;
            txtCharges.Text = Convert.ToString(0);
            txtTypeOfAnaesthetist.Text = string.Empty;
            this.programmaticModalPopup.Show();
        }

        public void GetReligion()
        {
            DataTable ldtReligion = new DataTable();
            ldtReligion = mobjReligionBLL.GetAllReligion();
            if (ldtReligion.Rows.Count > 0 && ldtReligion != null)
            {
                dgvReligion.DataSource = ldtReligion;
                dgvReligion.DataBind();
                Session["ReligionDetails"] = ldtReligion;
                int lintRowcount = ldtReligion.Rows.Count;
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

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            int lintcnt = 0;
            EntityAnaesthetist entReligion = new EntityAnaesthetist();
            if (string.IsNullOrEmpty(txtReligionCode.Text.Trim()))
            {
                lblMsg.Text = "Please Enter Sr No";
            }
            else
            {
                if (string.IsNullOrEmpty(txtReligionDesc.Text.Trim()))
                {
                    lblMsg.Text = "Please Enter Visiting/Anaesthetist Name";
                }
                else
                {
                    if (string.IsNullOrEmpty(txtTypeOfAnaesthetist.Text.Trim()))
                    {
                        lblMsg.Text = "Please Enter Type Anaesthesia";
                    }
                    else
                    {
                        entReligion.AnaesthetistCode = txtReligionCode.Text.Trim();
                        entReligion.AnaesthetistName = txtReligionDesc.Text.Trim();
                        entReligion.TypeOfAnaesthetist = txtTypeOfAnaesthetist.Text.Trim();
                        entReligion.CategoryId = Convert.ToInt32(ddlCategoryName.SelectedValue);
                        entReligion.CategoryName = Convert.ToString(ddlCategoryName.SelectedItem.Text);
                        entReligion.ConsultCharges = Convert.ToDecimal(txtCharges.Text);

                        lintcnt = mobjReligionBLL.InsertReligion(entReligion);

                        if (lintcnt > 0)
                        {
                            GetReligion();
                            lblMessage.Text = "Record Inserted Successfully";
                            this.programmaticModalPopup.Hide();
                        }
                        else
                        {
                            lblMessage.Text = "Record Not Inserted";
                        }
                    }
                }
            }

        }

        protected void dgvReligion_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                DataTable ldt = new DataTable();
                EditBindCategory();
                if (e.CommandName == "EditReligion")
                {
                    this.programmaticModalPopupEdit.Show();

                    int linIndex = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                    LinkButton lnkReligionCode = (LinkButton)gvr.FindControl("lnkReligionCode");
                    string lstrReligionCode = lnkReligionCode.Text;
                    txtEditReligionCode.Text = lstrReligionCode;
                    ldt = mobjReligionBLL.GetReligionForEdit(lstrReligionCode);
                    FillControls(ldt);
                }
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmAnaesthetistMaster -  dgvReligion_RowCommand(object sender, GridViewCommandEventArgs e)", ex);
            }
        }

        private void FillControls(DataTable ldt)
        {
            txtEditReligionDesc.Text = ldt.Rows[0]["AnaesthetistName"].ToString();
            txtEditTypeOfAnaesthetist.Text = ldt.Rows[0]["TypeOfAnaesthesia"].ToString();
            txtEditCharges.Text = ldt.Rows[0]["ConsultCharges"].ToString();
            ListItem Desig = ddlEditCategoryName.Items.FindByText(ldt.Rows[0]["CategoryName"].ToString());
            ddlEditCategoryName.SelectedValue = Desig.Value;
        }

        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            int lintCnt = 0;
            try
            {
                EntityAnaesthetist entReligion = new EntityAnaesthetist();

                entReligion.AnaesthetistCode = txtEditReligionCode.Text;
                entReligion.AnaesthetistName = txtEditReligionDesc.Text;
                entReligion.TypeOfAnaesthetist = txtEditTypeOfAnaesthetist.Text;
                entReligion.CategoryName = Convert.ToString(ddlEditCategoryName.SelectedItem.Text);
                entReligion.CategoryId = Convert.ToInt32(ddlEditCategoryName.SelectedValue);
                entReligion.ConsultCharges = Convert.ToDecimal(txtEditCharges.Text);
                lintCnt = mobjReligionBLL.UpdateReligion(entReligion);

                if (lintCnt > 0)
                {
                    GetReligion();
                    lblMessage.Text = "Record Updated Successfully";
                    this.programmaticModalPopup.Hide();
                }
                else
                {
                    lblMessage.Text = "Record Not Updated";
                }
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmAnaesthetistMaster -  BtnEdit_Click(object sender, EventArgs e)", ex);
            }
        }

        protected void chkDelete_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chk.NamingContainer;
            if (chk.Checked)
            {
                LinkButton ReligionCode = (LinkButton)row.FindControl("lnkReligionCode");
                Session["AnaesthetistCode"] = ReligionCode.Text;
                lblMessage.Text = string.Empty;
            }
            else
            {
                Session["AnaesthetistCode"] = string.Empty;
            }
        }

        protected void BtnDeleteOk_Click(object sender, EventArgs e)
        {
            EntityAnaesthetist entReligion = new EntityAnaesthetist();
            int cnt = 0;

            try
            {
                foreach (GridViewRow drv in dgvReligion.Rows)
                {
                    CheckBox chkDelete = (CheckBox)drv.FindControl("chkDelete");
                    if (chkDelete.Checked)
                    {
                        LinkButton lnkReligionCode = (LinkButton)drv.FindControl("lnkReligionCode");
                        string lstrReligionCode = lnkReligionCode.Text;
                        entReligion.AnaesthetistCode = lstrReligionCode;

                        cnt = mobjReligionBLL.DeleteReligion(entReligion);
                        if (cnt > 0)
                        {
                            this.modalpopupDelete.Hide();

                            lblMessage.Text = "Record Deleted Successfully....";

                            if (dgvReligion.Rows.Count <= 0)
                            {
                                pnlShow.Style.Add(HtmlTextWriterStyle.Display, "none");
                                hdnPanel.Value = "none";
                            }

                        }
                        else
                        {
                            lblMessage.Text = "Record Not Deleted....";
                        }
                    }
                }
                GetReligion();
            }
            catch (System.Threading.ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                Commons.FileLog("frmAnaesthetistMaster -   BtnDeleteOk_Click(object sender, EventArgs e)", ex);
            }

        }

        protected void BtnDelete_Click(object sender, EventArgs e)
        {

            foreach (GridViewRow drv in dgvReligion.Rows)
            {
                CheckBox chkDelete = (CheckBox)drv.FindControl("chkDelete");
                if (chkDelete.Checked)
                {
                    this.modalpopupDelete.Show();
                }
            }
        }

        protected void dgvReligion_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dgvReligion.DataSource = (DataTable)Session["ReligionDetail"];
                dgvReligion.DataBind();
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmAnaesthetistMaster - dgvReligion_PageIndexChanged(object sender, EventArgs e)", ex);
            }
        }

        protected void dgvReligion_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvReligion.PageIndex = e.NewPageIndex;
        }

        protected void dgvReligion_RowDataBound(object sender, GridViewRowEventArgs e)
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
                Commons.FileLog("frmAnaesthetistMaster -  dgvReligion_RowDataBound(object sender, GridViewRowEventArgs e)", ex);
            }
        }

        protected void dgvReligion_DataBound(object sender, EventArgs e)
        {
            int lintCurrentPage = dgvReligion.PageIndex + 1;
            lblPageCount.Text = "<b>Page</b> " + lintCurrentPage.ToString() + "<b> of </b>" + dgvReligion.PageCount.ToString();
        }
    }
}