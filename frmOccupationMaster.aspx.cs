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
    public partial class frmOccupation : BasePage
    {
        OccupationBLL mobjOccupationBLL = new OccupationBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            base.AuthenticateUser();
            if (!Page.IsPostBack)
            {
                GetOccupation();
            }
        }
        protected void BtnAddNewOccupation_Click(object sender, EventArgs e)
        {
            txtOccupationDesc.Text = string.Empty;
            this.programmaticModalPopup.Show();

        }
        public void GetOccupation()
        {
            //DataTable ldtOccupation = new DataTable();
            var ldtOccupation = mobjOccupationBLL.GetAllOccupation();
            if (ldtOccupation.Count > 0 && ldtOccupation != null)
            {
                dgvOccupation.DataSource = ldtOccupation;
                dgvOccupation.DataBind();
                //Session["OccupationDetails"] = ldtOccupation;
                int lintRowcount = ldtOccupation.Count;
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
            EntityOccupation entOccupation = new EntityOccupation();

            if (string.IsNullOrEmpty(txtOccupationDesc.Text.Trim()))
            {
                Commons.ShowMessage("Enter Occupation Description", this.Page);
            }
            else
            {
                entOccupation.OccupationDesc = txtOccupationDesc.Text.Trim();
                entOccupation.EntryBy = SessionManager.Instance.LoginUser.PKId.ToString();
                lintcnt = mobjOccupationBLL.InsertOccupation(entOccupation);

                if (lintcnt > 0)
                {
                    GetOccupation();
                    Commons.ShowMessage("Record Inserted Successfully", this.Page);
                    this.programmaticModalPopup.Hide();
                }
                else
                {
                    Commons.ShowMessage("Record Not Inserted", this.Page);
                }
            }
        }
        protected void dgvOccupation_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                DataTable ldt = new DataTable();
                if (e.CommandName == "EditOccupation")
                {
                    this.programmaticModalPopupEdit.Show();
                    int linIndex = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                    LinkButton lnkOccupationCode = (LinkButton)gvr.FindControl("lnkOccupationCode");
                    int lintOccupationCode = Convert.ToInt32(lnkOccupationCode.Text);
                    PKId.Value = lnkOccupationCode.Text;
                    ldt = mobjOccupationBLL.GetOccupationForEdit(lintOccupationCode);
                    FillControls(ldt);
                }
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmOccupationMaster -  dgvOccupation_RowCommand(object sender, GridViewCommandEventArgs e)", ex);
            }
        }

        private void FillControls(DataTable ldt)
        {
            txtEditOccupationDesc.Text = ldt.Rows[0]["OccupationDesc"].ToString();
        }

        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            int lintCnt = 0;
            try
            {
                EntityOccupation entOccupation = new EntityOccupation();

                int OccupationCode = Convert.ToInt32(PKId.Value);

                entOccupation.PKId = OccupationCode;
                entOccupation.OccupationDesc = txtEditOccupationDesc.Text;
                entOccupation.ChangeBy = SessionManager.Instance.LoginUser.PKId.ToString();
                lintCnt = mobjOccupationBLL.UpdateOccupation(entOccupation);

                if (lintCnt > 0)
                {
                    GetOccupation();
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
                Commons.FileLog("frmOccupationMaster -  BtnEdit_Click(object sender, EventArgs e)", ex);
            }
        }

        protected void chkDelete_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chk.NamingContainer;
            if (chk.Checked)
            {
                LinkButton lnkOccupationCode = (LinkButton)row.FindControl("lnkOccupationCode");
                //Session["OccupationCode"] = OccupationCode.Text;
                OccupationCode.Value = lnkOccupationCode.Text;
                lblMessage.Text = string.Empty;
            }
            else
            {
                OccupationCode.Value = string.Empty;
            }
        }
        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow drv in dgvOccupation.Rows)
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
            EntityOccupation entOccupation = new EntityOccupation();
            int cnt = 0;

            try
            {
                foreach (GridViewRow drv in dgvOccupation.Rows)
                {
                    CheckBox chkDelete = (CheckBox)drv.FindControl("chkDelete");
                    if (chkDelete.Checked)
                    {
                        LinkButton lnkOccupationCode = (LinkButton)drv.FindControl("lnkOccupationCode");
                        int lintOccupationCode = Convert.ToInt32(lnkOccupationCode.Text);
                        entOccupation.PKId = lintOccupationCode;

                        cnt = mobjOccupationBLL.DeleteOccupation(entOccupation);
                        if (cnt > 0)
                        {
                            this.modalpopupDelete.Hide();

                            Commons.ShowMessage("Record Deleted Successfully....", this.Page);

                            if (dgvOccupation.Rows.Count <= 0)
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
                GetOccupation();
            }
            catch (System.Threading.ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                Commons.FileLog("frmOccupationMaster -   BtnDeleteOk_Click(object sender, EventArgs e)", ex);
            }
        }

        protected void dgvOccupation_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var ldtOccupation = mobjOccupationBLL.GetAllOccupation();
                dgvOccupation.DataSource = ldtOccupation;// (DataTable)Session["OccupationDetail"];
                dgvOccupation.DataBind();
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmOccupationMaster - dgvOccupation_PageIndexChanged(object sender, EventArgs e)", ex);
            }
        }

        protected void dgvOccupation_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvOccupation.PageIndex = e.NewPageIndex;
        }

        protected void dgvOccupation_RowDataBound(object sender, GridViewRowEventArgs e)
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
                Commons.FileLog("frmOccupationMaster -  dgvOccupation_RowDataBound(object sender, GridViewRowEventArgs e)", ex);
            }
        }

        protected void dgvOccupation_DataBound(object sender, EventArgs e)
        {
            int lintCurrentPage = dgvOccupation.PageIndex + 1;
            lblPageCount.Text = "<b>Page</b> " + lintCurrentPage.ToString() + "<b> of </b>" + dgvOccupation.PageCount.ToString();
        }
    }
}