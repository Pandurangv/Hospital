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
using Hospital.Models;

namespace Hospital
{
    public partial class frmOperationCatMaster : BasePage
    {
        OperationCategoryBLL mobjReligionBLL = new OperationCategoryBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GetReligion();
            }
        }

        protected void BtnAddNewReligion_Click(object sender, EventArgs e)
        {
            DataTable ldt = new DataTable();
            ldt = mobjReligionBLL.GetNewReligionCode();
            txtReligionCode.Text = ldt.Rows[0][0].ToString();
            txtReligionDesc.Text = string.Empty;
            this.programmaticModalPopup.Show();
        }

        public void GetReligion()
        {
            var ldtReligion = mobjReligionBLL.GetAllReligion();
            if (ldtReligion.Count > 0 && ldtReligion != null)
            {
                dgvReligion.DataSource = ldtReligion;
                dgvReligion.DataBind();
                int lintRowcount = ldtReligion.Count;
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
            EntityOperationCategory entReligion = new EntityOperationCategory();
            if (string.IsNullOrEmpty(txtReligionCode.Text.Trim()))
            {
                lblMsg.Text = "Please Enter Category Code";
            }
            else
            {
                if (string.IsNullOrEmpty(txtReligionDesc.Text.Trim()))
                {
                    lblMsg.Text = "Please Enter Category Name";
                }
                else
                {
                    entReligion.CategoryCode = txtReligionCode.Text.Trim();
                    entReligion.CategoryName = txtReligionDesc.Text.Trim();
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

        protected void dgvReligion_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                DataTable ldt = new DataTable();
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
                Commons.FileLog("frmOperatioCatMaster -  dgvReligion_RowCommand(object sender, GridViewCommandEventArgs e)", ex);
            }
        }

        private void FillControls(DataTable ldt)
        {
            txtEditReligionDesc.Text = ldt.Rows[0]["CategoryName"].ToString();
        }

        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            int lintCnt = 0;
            try
            {
                EntityOperationCategory entReligion = new EntityOperationCategory();

                entReligion.CategoryCode = txtEditReligionCode.Text;
                entReligion.CategoryName = txtEditReligionDesc.Text;
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
                Commons.FileLog("frmOperationCatMaster -  BtnEdit_Click(object sender, EventArgs e)", ex);
            }
        }

        protected void chkDelete_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chk.NamingContainer;
            if (chk.Checked)
            {
                LinkButton ReligionCode = (LinkButton)row.FindControl("lnkReligionCode");
                CategoryCode.Value = ReligionCode.Text;
                lblMessage.Text = string.Empty;
            }
            else
            {
                CategoryCode.Value = string.Empty;
            }
        }

        protected void BtnDeleteOk_Click(object sender, EventArgs e)
        {
            EntityOperationCategory entReligion = new EntityOperationCategory();
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
                        entReligion.CategoryCode = lstrReligionCode;

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
                Commons.FileLog("frmOperationCatMaster -   BtnDeleteOk_Click(object sender, EventArgs e)", ex);
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
                var ldtReligion = mobjReligionBLL.GetAllReligion();
                dgvReligion.DataSource = ldtReligion;// (DataTable)Session["ReligionDetail"];
                dgvReligion.DataBind();
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmOperationCatMaster - dgvReligion_PageIndexChanged(object sender, EventArgs e)", ex);
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
                Commons.FileLog("frmOperationCatMaster -  dgvReligion_RowDataBound(object sender, GridViewRowEventArgs e)", ex);
            }
        }

        protected void dgvReligion_DataBound(object sender, EventArgs e)
        {
            int lintCurrentPage = dgvReligion.PageIndex + 1;
            lblPageCount.Text = "<b>Page</b> " + lintCurrentPage.ToString() + "<b> of </b>" + dgvReligion.PageCount.ToString();
        }
    }
}