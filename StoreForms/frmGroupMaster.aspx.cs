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
    public partial class frmGroupMaster : System.Web.UI.Page
    {
        GroupBLL mobjGroupBLL = new GroupBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //SessionManager.Instance.SetSession();
                BtnDelete.Enabled = false;
                GetGroup();
            }
        }

        protected void BtnAddNewGroup_Click(object sender, EventArgs e)
        {
            txtGroupDesc.Text = string.Empty;
            this.programmaticModalPopup.Show();
        }

        public void GetGroup()
        {
            DataTable ldtGroup = new DataTable();
            ldtGroup = mobjGroupBLL.GetAllGroup();
            if (ldtGroup.Rows.Count > 0 && ldtGroup != null)
            {
                dgvGroup.DataSource = ldtGroup;
                dgvGroup.DataBind();
                Session["GroupDetails"] = ldtGroup;
                int lintRowcount = ldtGroup.Rows.Count;
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
            EntityGroup entGroup = new EntityGroup();

            if (string.IsNullOrEmpty(txtGroupDesc.Text.Trim()))
            {
                Commons.ShowMessage("Enter Country Description", this.Page);
            }
            else
            {
                entGroup.GroupDesc = txtGroupDesc.Text.Trim();
                entGroup.EntryBy = SessionManager.Instance.LoginUser.EmpCode;
                lintcnt = mobjGroupBLL.InsertGroup(entGroup);

                if (lintcnt > 0)
                {
                    GetGroup();
                    Commons.ShowMessage("Record Inserted Successfully", this.Page);
                    this.programmaticModalPopup.Hide();
                }
                else
                {
                    Commons.ShowMessage("Record Not Inserted", this.Page);
                }
            }

        }

        protected void dgvGroup_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                DataTable ldt = new DataTable();
                if (e.CommandName == "EditGroup")
                {
                    this.programmaticModalPopupEdit.Show();
                    int linIndex = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                    LinkButton lnkGroupCode = (LinkButton)gvr.FindControl("lnkGroupCode");
                    int lintGroupCode = Convert.ToInt32(lnkGroupCode.Text);
                    Session["GroupCode"] = lintGroupCode;
                    ldt = mobjGroupBLL.GetGroupForEdit(lintGroupCode);
                    FillControls(ldt);
                }
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmGroupMaster -  dgvGroup_RowCommand(object sender, GridViewCommandEventArgs e)", ex);
            }
        }

        private void FillControls(DataTable ldt)
        {
            txtEditGroupDesc.Text = ldt.Rows[0]["GroupDesc"].ToString();
        }

        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            int lintCnt = 0;
            try
            {
                EntityGroup entGroup = new EntityGroup();
                entGroup.PKId = Convert.ToInt32(Session["GroupCode"].ToString());
                entGroup.GroupDesc = txtEditGroupDesc.Text;
                entGroup.ChangeBy = SessionManager.Instance.LoginUser.EmpCode;
                lintCnt = mobjGroupBLL.UpdateGroup(entGroup);

                if (lintCnt > 0)
                {
                    GetGroup();
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
                Commons.FileLog("frmGroupMaster -  BtnEdit_Click(object sender, EventArgs e)", ex);
            }
        }

        protected void chkDelete_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chk.NamingContainer;
            if (chk.Checked)
            {
                LinkButton GroupCode = (LinkButton)row.FindControl("lnkGroupCode");
                Session["GroupCode"] = GroupCode.Text;
                lblMessage.Text = string.Empty;
                BtnDelete.Enabled = true;
            }
            else
            {
                Session["GroupCode"] = string.Empty;
                BtnDelete.Enabled = false;
            }
        }

        protected void BtnDeleteOk_Click(object sender, EventArgs e)
        {
            EntityGroup entGroup = new EntityGroup();
            int cnt = 0;

            try
            {
                foreach (GridViewRow drv in dgvGroup.Rows)
                {
                    CheckBox chkDelete = (CheckBox)drv.FindControl("chkDelete");
                    if (chkDelete.Checked)
                    {
                        LinkButton lnkGroupCode = (LinkButton)drv.FindControl("lnkGroupCode");
                        int lintGroupCode = Convert.ToInt32(lnkGroupCode.Text);
                        entGroup.PKId = lintGroupCode;

                        cnt = mobjGroupBLL.DeleteGroup(entGroup);
                        if (cnt > 0)
                        {
                            this.modalpopupDelete.Hide();

                            Commons.ShowMessage("Record Deleted Successfully....", this.Page);

                            if (dgvGroup.Rows.Count <= 0)
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
                GetGroup();
            }
            catch (System.Threading.ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                Commons.FileLog("frmGroupMaster -   BtnDeleteOk_Click(object sender, EventArgs e)", ex);
            }

        }

        protected void BtnDelete_Click(object sender, EventArgs e)
        {

            foreach (GridViewRow drv in dgvGroup.Rows)
            {
                CheckBox chkDelete = (CheckBox)drv.FindControl("chkDelete");
                if (chkDelete.Checked)
                {
                    this.modalpopupDelete.Show();
                }
            }
        }

        protected void dgvGroup_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dgvGroup.DataSource = (DataTable)Session["GroupDetail"];
                dgvGroup.DataBind();
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmGroupMaster - dgvGroup_PageIndexChanged(object sender, EventArgs e)", ex);
            }
        }

        protected void dgvGroup_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvGroup.PageIndex = e.NewPageIndex;
        }

        protected void dgvGroup_RowDataBound(object sender, GridViewRowEventArgs e)
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
                Commons.FileLog("frmGroupMaster -  dgvGroup_RowDataBound(object sender, GridViewRowEventArgs e)", ex);
            }
        }

        protected void dgvGroup_DataBound(object sender, EventArgs e)
        {
            int lintCurrentPage = dgvGroup.PageIndex + 1;
            lblPageCount.Text = "<b>Page</b> " + lintCurrentPage.ToString() + "<b> of </b>" + dgvGroup.PageCount.ToString();
        }
    }
}