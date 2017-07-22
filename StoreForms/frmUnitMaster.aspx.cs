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
    public partial class frmUnitMaster : System.Web.UI.Page
    {
        UnitBLL mobjUnitBLL = new UnitBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //SessionManager.Instance.SetSession();
                BtnDelete.Enabled = false;
                GetUnit();
            }
        }
        protected void BtnAddNewUnit_Click(object sender, EventArgs e)
        {
            DataTable ldt = new DataTable();
            txtUnitCode.Text = string.Empty;
            txtUnitDesc.Text = string.Empty;
            this.programmaticModalPopup.Show();

        }
        public void GetUnit()
        {
            //DataTable ldtUnit = new DataTable();
            var ldtUnit = mobjUnitBLL.GetAllUnit();
            if (ldtUnit.Count > 0 && ldtUnit != null)
            {
                dgvUnit.DataSource = ldtUnit;
                dgvUnit.DataBind();
                //Session["UnitDetails"] = ldtUnit;
                int lintRowcount = ldtUnit.Count;
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
            EntityUnit entUnit = new EntityUnit();
            if (string.IsNullOrEmpty(txtUnitCode.Text.Trim()))
            {
                Commons.ShowMessage("Enter Unit Code", this.Page);
            }
            else
            {
                if (string.IsNullOrEmpty(txtUnitDesc.Text.Trim()))
                {
                    Commons.ShowMessage("Enter Unit Description", this.Page);
                }
                else
                {
                    entUnit.UnitCode = txtUnitCode.Text.Trim();
                    entUnit.UnitDesc = txtUnitDesc.Text.Trim();
                    //entUnit.EntryBy = SessionManager.Instance.UserName;
                    lintcnt = mobjUnitBLL.InsertUnit(entUnit);

                    if (lintcnt > 0)
                    {
                        GetUnit();
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
        protected void dgvUnit_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                DataTable ldt = new DataTable();
                if (e.CommandName == "EditUnit")
                {
                    this.programmaticModalPopupEdit.Show();
                    int linIndex = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                    LinkButton lnkUnitCode = (LinkButton)gvr.FindControl("lnkPKId");
                    string lstrUnitCode = lnkUnitCode.Text;
                    txtUnitCode.Text = lstrUnitCode;
                    ldt = mobjUnitBLL.GetUnitForEdit(lstrUnitCode);

                    int lintUnitCode = Convert.ToInt32(lnkUnitCode.Text);
                    txtEditPKId.Text = Convert.ToString(lintUnitCode);
                    //ldt = mobjUnitBLL.GetUnitForEdit(lintUnitCode);

                    FillControls(ldt);
                }
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmUnitMaster -  dgvUnit_RowCommand(object sender, GridViewCommandEventArgs e)", ex);
            }
        }

        private void FillControls(DataTable ldt)
        {
            txtEditUnitCode.Text = ldt.Rows[0]["UnitCode"].ToString();
            txtEditUnitDesc.Text = ldt.Rows[0]["UnitDesc"].ToString();
        }

        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            int lintCnt = 0;
            try
            {
                EntityUnit entUnit = new EntityUnit();

                entUnit.UnitCode = txtEditUnitCode.Text;
                entUnit.UnitDesc = txtEditUnitDesc.Text;
                //           entUnit.ChangeBy = SessionManager.Instance.UserName;
                lintCnt = mobjUnitBLL.UpdateUnit(entUnit);

                if (lintCnt > 0)
                {
                    GetUnit();
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
                Commons.FileLog("frmUnitMaster -  BtnEdit_Click(object sender, EventArgs e)", ex);
            }
        }

        protected void chkDelete_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chk.NamingContainer;
            if (chk.Checked)
            {
                LinkButton UnitCode = (LinkButton)row.FindControl("lnkPKId");
                Session["PKId"] = UnitCode.Text;
                lblMessage.Text = string.Empty;
                BtnDelete.Enabled = true;
            }
            else
            {
                Session["PKId"] = string.Empty;
                BtnDelete.Enabled = false;
            }
        }
        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow drv in dgvUnit.Rows)
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
            EntityUnit entUnit = new EntityUnit();
            int cnt = 0;

            try
            {
                foreach (GridViewRow drv in dgvUnit.Rows)
                {
                    CheckBox chkDelete = (CheckBox)drv.FindControl("chkDelete");
                    if (chkDelete.Checked)
                    {
                        LinkButton lnkPKId = (LinkButton)drv.FindControl("lnkPKId");
                        int lintPKId = Convert.ToInt32(lnkPKId.Text);
                        entUnit.PKId = lintPKId;

                        cnt = mobjUnitBLL.DeleteUnit(entUnit);
                        if (cnt > 0)
                        {
                            this.modalpopupDelete.Hide();

                            Commons.ShowMessage("Record Deleted Successfully....", this.Page);

                            if (dgvUnit.Rows.Count <= 0)
                            {
                                pnlShow.Style.Add(HtmlTextWriterStyle.Display, "none");
                                hdnPanel.Value = "none";
                            }

                        }
                        else
                        {
                            Commons.ShowMessage("Record Not Deleted....", this.Page);
                        }
                    }
                }
                GetUnit();
            }
            catch (System.Threading.ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                Commons.FileLog("frmUnitMaster -   BtnDeleteOk_Click(object sender, EventArgs e)", ex);
            }
        }

        protected void dgvUnit_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var ldtUnit = mobjUnitBLL.GetAllUnit();
                dgvUnit.DataSource = ldtUnit;// (DataTable)Session["UnitDetail"];
                dgvUnit.DataBind();
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmUnitMaster - dgvUnit_PageIndexChanged(object sender, EventArgs e)", ex);
            }
        }

        protected void dgvUnit_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvUnit.PageIndex = e.NewPageIndex;
        }

        protected void dgvUnit_RowDataBound(object sender, GridViewRowEventArgs e)
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
                Commons.FileLog("frmUnitMaster -  dgvUnit_RowDataBound(object sender, GridViewRowEventArgs e)", ex);
            }
        }

        protected void dgvUnit_DataBound(object sender, EventArgs e)
        {
            int lintCurrentPage = dgvUnit.PageIndex + 1;
            lblPageCount.Text = "<b>Page</b> " + lintCurrentPage.ToString() + "<b> of </b>" + dgvUnit.PageCount.ToString();
        }
    }
}