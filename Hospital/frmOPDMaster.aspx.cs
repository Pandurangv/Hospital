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

namespace Hospital
{
    public partial class frmOPDMaster : System.Web.UI.Page
    {
        OPDBLL mobjOPDBLL = new OPDBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //SessionManager.Instance.SetSession();
                GetOPD();
            }
        }
        protected void BtnAddNewOPD_Click(object sender, EventArgs e)
        {
            txtOPDDesc.Text = string.Empty;
            this.programmaticModalPopup.Show();
        }

        public void GetOPD()
        {
            DataTable ldtOPD = new DataTable();
            ldtOPD = mobjOPDBLL.GetAllOPD();
            if (ldtOPD.Rows.Count > 0 && ldtOPD != null)
            {
                dgvOPD.DataSource = ldtOPD;
                dgvOPD.DataBind();
                Session["OPDDetails"] = ldtOPD;
                int lintRowcount = ldtOPD.Rows.Count;
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
            EntityOPD entOPD = new EntityOPD();


            if (string.IsNullOrEmpty(txtOPDDesc.Text.Trim()))
            {
                Commons.ShowMessage("Enter OPD Description", this.Page);
            }
            else
            {
                entOPD.OPDDesc = txtOPDDesc.Text.Trim();

                if (!Commons.IsRecordExists("tblOPDMaster", "OPDDesc", entOPD.OPDDesc))
                {
                    lintcnt = mobjOPDBLL.InsertOPD(entOPD);
                    if (lintcnt > 0)
                    {
                        GetOPD();
                        Commons.ShowMessage("Record Inserted Successfully", this.Page);
                        this.programmaticModalPopup.Hide();
                    }
                    else
                    {
                        Commons.ShowMessage("Record Not Inserted", this.Page);
                    }
                }
                else
                {
                    Commons.ShowMessage("Record Already Exist....", this.Page);
                }
            }
        }

        protected void dgvOPD_DataBound(object sender, EventArgs e)
        {
            int lintCurrentPage = dgvOPD.PageIndex + 1;
            lblPageCount.Text = "<b>Page</b> " + lintCurrentPage.ToString() + "<b> of </b>" + dgvOPD.PageCount.ToString();
        }
        protected void dgvOPD_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dgvOPD.DataSource = (DataTable)Session["OPDDetail"];
                dgvOPD.DataBind();
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmOPDMaster - dgvOPD_PageIndexChanged(object sender, EventArgs e)", ex);
            }
        }
        protected void dgvOPD_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvOPD.PageIndex = e.NewPageIndex;
        }
        protected void dgvOPD_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                DataTable ldt = new DataTable();
                if (e.CommandName == "EditOPD")
                {
                    this.programmaticModalPopupEdit.Show();
                    int linIndex = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                    LinkButton lnkOPDCode = (LinkButton)gvr.FindControl("lnkOPDCode");
                    string lstrOPDCode = lnkOPDCode.Text;
                    Session["OPDId"] = lnkOPDCode.Text;
                    ldt = mobjOPDBLL.GetOPDForEdit(lstrOPDCode);
                    FillControls(ldt);
                }
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmOPDMaster -  dgvOPD_RowCommand(object sender, GridViewCommandEventArgs e)", ex);
            }
        }


        protected void dgvOPD_RowDataBound(object sender, GridViewRowEventArgs e)
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
                Commons.FileLog("frmOPDMaster -  dgvOPD_RowDataBound(object sender, GridViewRowEventArgs e)", ex);
            }
        }

        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            int lintCnt = 0;
            try
            {
                EntityOPD entOPD = new EntityOPD();

                int lintOPDCode = Convert.ToInt32(Session["OPDId"].ToString());
                entOPD.OPDCode = lintOPDCode;
                entOPD.OPDDesc = txtEditOPDDesc.Text;

                lintCnt = mobjOPDBLL.UpdateOPD(entOPD);

                if (lintCnt > 0)
                {
                    GetOPD();
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
                Commons.FileLog("frmOPDMaster -  BtnEdit_Click(object sender, EventArgs e)", ex);
            }
        }

        private void FillControls(DataTable ldt)
        {
            txtEditOPDDesc.Text = ldt.Rows[0]["OPDDesc"].ToString();
        }


        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow drv in dgvOPD.Rows)
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
            EntityOPD entOPD = new EntityOPD();
            int cnt = 0;

            try
            {
                foreach (GridViewRow drv in dgvOPD.Rows)
                {
                    CheckBox chkDelete = (CheckBox)drv.FindControl("chkDelete");
                    if (chkDelete.Checked)
                    {
                        LinkButton lnkOPDCode = (LinkButton)drv.FindControl("lnkOPDCode");
                        int lstrOPDCode = Convert.ToInt32(lnkOPDCode.Text);
                        entOPD.OPDCode = lstrOPDCode;

                        cnt = mobjOPDBLL.DeleteOPD(entOPD);
                        if (cnt > 0)
                        {
                            this.modalpopupDelete.Hide();
                            Commons.ShowMessage("Record Deleted Successfully....", this.Page);

                            if (dgvOPD.Rows.Count <= 0)
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
                GetOPD();
            }
            catch (System.Threading.ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                Commons.FileLog("frmOPDMaster -   BtnDeleteOk_Click(object sender, EventArgs e)", ex);
            }
        }

        protected void chkDelete_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chk.NamingContainer;
            if (chk.Checked)
            {
                LinkButton OPDCode = (LinkButton)row.FindControl("lnkOPDCode");
                Session["OPDCode"] = OPDCode.Text;
                lblMessage.Text = string.Empty;
            }
            else
            {
                Session["OPDCode"] = string.Empty;
            }
        }
    }
}