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
    public partial class frmStateMaster : System.Web.UI.Page
    {
        StateBLL mobjStateBLL = new StateBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //SessionManager.Instance.SetSession();
                GetState();
                GetCountryForEdit();
            }
        }
        protected void BtnAddNewState_Click(object sender, EventArgs e)
        {
            DataTable ldt = new DataTable();
            ldt = mobjStateBLL.GetNewStateCode();
            txtStateCode.Text = ldt.Rows[0]["StateCode"].ToString();
            txtStateDesc.Text = string.Empty;
            GetCountryForSave();
            this.programmaticModalPopup.Show();
        }

        public void GetCountryForSave()
        {
            DataTable ldtCountry = new DataTable();
            ldtCountry = mobjStateBLL.GetAllCountryForState();
            if (ldtCountry.Rows.Count > 0 && ldtCountry != null)
            {
                ddlCountry.DataSource = ldtCountry;
                ddlCountry.DataValueField = "PKId";
                ddlCountry.DataTextField = "CountryDesc";
                ddlCountry.DataBind();

                ListItem li = new ListItem();
                li.Text = "--Select Country--";
                li.Value = "0";
                ddlCountry.Items.Insert(0, li);
            }
        }

        public void GetCountryForEdit()
        {
            DataTable ldtCountry = new DataTable();
            ldtCountry = mobjStateBLL.GetAllCountryForState();

            if (ldtCountry.Rows.Count > 0 && ldtCountry != null)
            {
                ddlEditCountry.DataSource = ldtCountry;
                ddlEditCountry.DataValueField = "PKId";
                ddlEditCountry.DataTextField = "CountryDesc";
                ddlEditCountry.DataBind();

                ListItem li = new ListItem();
                li.Text = "--Select Country--";
                li.Value = "0";
                ddlEditCountry.Items.Insert(0, li);
            }
        }


        public void GetState()
        {
            DataTable ldtState = new DataTable();
            ldtState = mobjStateBLL.GetAllState();
            if (ldtState.Rows.Count > 0 && ldtState != null)
            {
                dgvState.DataSource = ldtState;
                dgvState.DataBind();
                Session["StateDetails"] = ldtState;
                int lintRowcount = ldtState.Rows.Count;
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
            EntityState entState = new EntityState();
            if (string.IsNullOrEmpty(txtStateCode.Text.Trim()))
            {
                Commons.ShowMessage("Enter State Code", this.Page);
            }
            else
            {
                if (ddlCountry.SelectedIndex == 0)
                {
                    Commons.ShowMessage("Select Country", this.Page);
                }
                else
                {
                    if (string.IsNullOrEmpty(txtStateDesc.Text.Trim()))
                    {
                        Commons.ShowMessage("Enter State Description", this.Page);
                    }
                    else
                    {
                        entState.StateCode = txtStateCode.Text.Trim();
                        entState.StateDesc = txtStateDesc.Text.Trim();
                        entState.Country = Convert.ToInt32(ddlCountry.SelectedValue);
                        entState.EntryBy = SessionManager.Instance.LoginUser.EmpCode;
                        lintcnt = mobjStateBLL.InsertState(entState);

                        if (lintcnt > 0)
                        {
                            GetState();
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
        protected void dgvState_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                DataTable ldt = new DataTable();
                if (e.CommandName == "EditState")
                {
                    this.programmaticModalPopupEdit.Show();
                    int linIndex = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                    LinkButton lnkStateCode = (LinkButton)gvr.FindControl("lnkStateCode");
                    string lstrStateCode = lnkStateCode.Text;
                    txtEditStateCode.Text = lstrStateCode;
                    ldt = mobjStateBLL.GetStateForEdit(lstrStateCode);
                    FillControls(ldt);
                    GetCountryForEdit();
                }
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmStateMaster -  dgvState_RowCommand(object sender, GridViewCommandEventArgs e)", ex);
            }
        }

        private void FillControls(DataTable ldt)
        {
            ddlEditCountry.SelectedValue = ldt.Rows[0]["CountryId"].ToString();
            txtEditStateDesc.Text = ldt.Rows[0]["StateDesc"].ToString();
        }

        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            int lintCnt = 0;
            try
            {
                EntityState entState = new EntityState();

                entState.StateCode = txtEditStateCode.Text;
                entState.StateDesc = txtEditStateDesc.Text;
                entState.Country = Convert.ToInt32(ddlEditCountry.SelectedValue);
                entState.ChangeBy = SessionManager.Instance.LoginUser.EmpCode;
                lintCnt = mobjStateBLL.UpdateState(entState);

                if (lintCnt > 0)
                {
                    GetState();
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
                Commons.FileLog("frmStateMaster -  BtnEdit_Click(object sender, EventArgs e)", ex);
            }
        }

        protected void chkDelete_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chk.NamingContainer;
            if (chk.Checked)
            {
                LinkButton StateCode = (LinkButton)row.FindControl("lnkStateCode");
                Session["StateCode"] = StateCode.Text;
                lblMessage.Text = string.Empty;
            }
            else
            {
                Session["StateCode"] = string.Empty;
            }
        }
        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow drv in dgvState.Rows)
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
            EntityState entState = new EntityState();
            int cnt = 0;
            try
            {
                foreach (GridViewRow drv in dgvState.Rows)
                {
                    CheckBox chkDelete = (CheckBox)drv.FindControl("chkDelete");
                    if (chkDelete.Checked)
                    {
                        LinkButton lnkStateCode = (LinkButton)drv.FindControl("lnkStateCode");
                        string lstrStateCode = lnkStateCode.Text;
                        entState.StateCode = lstrStateCode;

                        cnt = mobjStateBLL.DeleteState(entState);
                        if (cnt > 0)
                        {
                            this.modalpopupDelete.Hide();

                            Commons.ShowMessage("Record Deleted Successfully....", this.Page);

                            if (dgvState.Rows.Count <= 0)
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
                GetState();
            }
            catch (System.Threading.ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                Commons.FileLog("frmStateMaster -   BtnDeleteOk_Click(object sender, EventArgs e)", ex);
            }
        }

        protected void dgvState_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dgvState.DataSource = (DataTable)Session["StateDetail"];
                dgvState.DataBind();
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmStateMaster - dgvState_PageIndexChanged(object sender, EventArgs e)", ex);
            }
        }

        protected void dgvState_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvState.PageIndex = e.NewPageIndex;
        }

        protected void dgvState_RowDataBound(object sender, GridViewRowEventArgs e)
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
                Commons.FileLog("frmStateMaster -  dgvState_RowDataBound(object sender, GridViewRowEventArgs e)", ex);
            }
        }

        protected void dgvState_DataBound(object sender, EventArgs e)
        {
            int lintCurrentPage = dgvState.PageIndex + 1;
            lblPageCount.Text = "<b>Page</b> " + lintCurrentPage.ToString() + "<b> of </b>" + dgvState.PageCount.ToString();
        }
    }
}