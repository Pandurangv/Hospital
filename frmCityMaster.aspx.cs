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
    public partial class frmCityMaster : System.Web.UI.Page
    {
        CityBLL mobjCityBLL = new CityBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            ////SessionManager.Instance.SetSession();
            if (SessionManager.Instance.LoginUser != null)
            {
                if (!Page.IsPostBack)
                {
                    //SessionManager.Instance.SetSession();
                    BtnDelete.Enabled = false;
                    GetCity();
                    GetStateForSave();
                }
            }
            else
            {
                //Response.Redirect("frmlogin.aspx", false);
            }
        }
        protected void BtnAddNewCity_Click(object sender, EventArgs e)
        {
            txtCityDesc.Text = string.Empty;
            GetStateForSave();
            this.programmaticModalPopup.Show();
        }

        public void GetCity()
        {
            DataTable ldtCity = new DataTable();
            ldtCity = mobjCityBLL.GetAllCity();
            if (ldtCity.Rows.Count > 0 && ldtCity != null)
            {
                dgvCity.DataSource = ldtCity;
                dgvCity.DataBind();
                Session["CityDetails"] = ldtCity;
                int lintRowcount = ldtCity.Rows.Count;
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

        public void GetStateForSave()
        {
            DataTable ldtState = new DataTable();
            ldtState = mobjCityBLL.GetAllState();
            if (ldtState.Rows.Count > 0 && ldtState != null)
            {
                ddlState.DataSource = ldtState;
                ddlState.DataValueField = "PKId";
                ddlState.DataTextField = "StateDesc";
                ddlState.DataBind();
                ListItem li = new ListItem();
                li.Text = "--Select State--";
                li.Value = "0";
                ddlState.Items.Insert(0, li);
            }
        }

        public void GetStateForEdit()
        {
            DataTable ldtState = new DataTable();
            ldtState = mobjCityBLL.GetAllState();
            if (ldtState.Rows.Count > 0 && ldtState != null)
            {
                ddlEditState.DataSource = ldtState;
                ddlEditState.DataValueField = "PKId";
                ddlEditState.DataTextField = "StateDesc";
                ddlEditState.DataBind();
                ListItem li = new ListItem();
                li.Text = "--Select State--";
                li.Value = "0";
                ddlEditState.Items.Insert(0, li);
            }

        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            int lintcnt = 0;
            EntityCity entCity = new EntityCity();

            if (ddlState.SelectedIndex == 0)
            {
                lblMsg.Text = "Please Select State";
                ddlState.Focus();
                return;
            }
            else if (string.IsNullOrEmpty(txtCityDesc.Text.Trim()))
            {
                lblMsg.Text = "Enter City Description";
                txtCityDesc.Focus();
                return;
            }
            else
            {
                entCity.CityDesc = txtCityDesc.Text.Trim();
                entCity.State = Convert.ToInt32(ddlState.SelectedValue);
                entCity.EntryBy = SessionManager.Instance.LoginUser.UserType;
                lintcnt = mobjCityBLL.InsertCity(entCity);

                if (lintcnt > 0)
                {
                    GetCity();
                    lblMessage.Text = "Record Inserted Successfully";
                    this.programmaticModalPopup.Hide();
                }
                else
                {
                    lblMessage.Text = "Record Not Inserted";
                }
            }
        }

        protected void dgvCity_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                DataTable ldt = new DataTable();
                if (e.CommandName == "EditCity")
                {
                    this.programmaticModalPopupEdit.Show();
                    int linIndex = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                    LinkButton lnkCityCode = (LinkButton)gvr.FindControl("lnkCityCode");
                    string lstrCityCode = lnkCityCode.Text;
                    Session["CityId"] = lnkCityCode.Text;
                    ldt = mobjCityBLL.GetCityForEdit(lstrCityCode);
                    FillControls(ldt);
                    GetStateForEdit();
                }
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmCityMaster -  dgvCity_RowCommand(object sender, GridViewCommandEventArgs e)", ex);
            }
        }

        private void FillControls(DataTable ldt)
        {
            ddlEditState.SelectedValue = ldt.Rows[0]["StateId"].ToString();
            txtEditCityDesc.Text = ldt.Rows[0]["CityDesc"].ToString();
        }

        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            int lintCnt = 0;
            try
            {
                EntityCity entCity = new EntityCity();

                int lintCityCode = Convert.ToInt32(Session["CityId"].ToString());

                entCity.CityCode = lintCityCode;
                entCity.State = Convert.ToInt32(ddlEditState.SelectedValue);
                entCity.CityDesc = txtEditCityDesc.Text;
                entCity.ChangeBy = SessionManager.Instance.LoginUser.UserType;
                lintCnt = mobjCityBLL.UpdateCity(entCity);

                if (lintCnt > 0)
                {
                    GetCity();
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
                Commons.FileLog("frmCityMaster -  BtnEdit_Click(object sender, EventArgs e)", ex);
            }
        }

        protected void chkDelete_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chk.NamingContainer;
            if (chk.Checked)
            {
                LinkButton CityCode = (LinkButton)row.FindControl("lnkCityCode");
                Session["CityCode"] = CityCode.Text;
                lblMessage.Text = string.Empty;
                BtnDelete.Enabled = true;
            }
            else
            {
                Session["CityCode"] = string.Empty;
                BtnDelete.Enabled = false;
            }
        }
        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow drv in dgvCity.Rows)
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
            EntityCity entCity = new EntityCity();
            int cnt = 0;

            try
            {
                foreach (GridViewRow drv in dgvCity.Rows)
                {
                    CheckBox chkDelete = (CheckBox)drv.FindControl("chkDelete");
                    if (chkDelete.Checked)
                    {
                        LinkButton lnkCityCode = (LinkButton)drv.FindControl("lnkCityCode");
                        int lstrCityCode = Convert.ToInt32(lnkCityCode.Text);
                        entCity.CityCode = lstrCityCode;

                        cnt = mobjCityBLL.DeleteCity(entCity);
                        if (cnt > 0)
                        {
                            this.modalpopupDelete.Hide();
                            lblMessage.Text = "Record Deleted Successfully....";

                            if (dgvCity.Rows.Count <= 0)
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
                GetCity();
            }
            catch (System.Threading.ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                Commons.FileLog("frmCityMaster -   BtnDeleteOk_Click(object sender, EventArgs e)", ex);
            }
        }

        protected void dgvCity_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dgvCity.DataSource = (DataTable)Session["CityDetail"];
                dgvCity.DataBind();
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmCityMaster - dgvCity_PageIndexChanged(object sender, EventArgs e)", ex);
            }
        }

        protected void dgvCity_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvCity.PageIndex = e.NewPageIndex;
        }

        protected void dgvCity_RowDataBound(object sender, GridViewRowEventArgs e)
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
                Commons.FileLog("frmCityMaster -  dgvCity_RowDataBound(object sender, GridViewRowEventArgs e)", ex);
            }
        }

        protected void dgvCity_DataBound(object sender, EventArgs e)
        {
            int lintCurrentPage = dgvCity.PageIndex + 1;
            lblPageCount.Text = "<b>Page</b> " + lintCurrentPage.ToString() + "<b> of </b>" + dgvCity.PageCount.ToString();
        }
    }
}