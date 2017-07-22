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
    public partial class frmCountryMaster : System.Web.UI.Page
    {
        CountryBLL mobjCountryBLL = new CountryBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //SessionManager.Instance.SetSession();
                BtnDelete.Enabled = false;
                GetCountry();
            }
        }


        protected void BtnAddNewCountry_Click(object sender, EventArgs e)
        {
            DataTable ldt = new DataTable();
            ldt = mobjCountryBLL.GetNewCountryCode();
            txtCountryCode.Text = ldt.Rows[0]["CountryCode"].ToString();
            txtCountryDesc.Text = string.Empty;
            this.programmaticModalPopup.Show();
        }


        public void GetCountry()
        {
            DataTable ldtCountry = new DataTable();
            ldtCountry = mobjCountryBLL.GetAllCountry();
            if (ldtCountry.Rows.Count > 0 && ldtCountry != null)
            {
                dgvCountry.DataSource = ldtCountry;
                dgvCountry.DataBind();
                Session["CountryDetails"] = ldtCountry;
                int lintRowcount = ldtCountry.Rows.Count;
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
            EntityCountry entCountry = new EntityCountry();
            if (string.IsNullOrEmpty(txtCountryCode.Text.Trim()))
            {
                Commons.ShowMessage("Enter Country Code", this.Page);
            }
            else
            {
                if (string.IsNullOrEmpty(txtCountryDesc.Text.Trim()))
                {
                    Commons.ShowMessage("Enter Country Description", this.Page);
                }
                else
                {
                    entCountry.CountryCode = txtCountryCode.Text.Trim();
                    entCountry.CountryDesc = txtCountryDesc.Text.Trim();
                    entCountry.EntryBy = SessionManager.Instance.LoginUser.EmpCode;
                    lintcnt = mobjCountryBLL.InsertCountry(entCountry);

                    if (lintcnt > 0)
                    {
                        GetCountry();
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
        protected void dgvCountry_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                DataTable ldt = new DataTable();
                if (e.CommandName == "EditCountry")
                {
                    this.programmaticModalPopupEdit.Show();
                    int linIndex = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                    LinkButton lnkCountryCode = (LinkButton)gvr.FindControl("lnkCountryCode");
                    string lstrCountryCode = lnkCountryCode.Text;
                    txtEditCountryCode.Text = lstrCountryCode;
                    ldt = mobjCountryBLL.GetCountryForEdit(lstrCountryCode);
                    FillControls(ldt);
                }
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmCountryMaster -  dgvCountry_RowCommand(object sender, GridViewCommandEventArgs e)", ex);
            }
        }

        private void FillControls(DataTable ldt)
        {
            txtEditCountryDesc.Text = ldt.Rows[0]["CountryDesc"].ToString();
        }

        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            int lintCnt = 0;
            try
            {
                EntityCountry entCountry = new EntityCountry();

                entCountry.CountryCode = txtEditCountryCode.Text;
                entCountry.CountryDesc = txtEditCountryDesc.Text;
                entCountry.ChangeBy = SessionManager.Instance.LoginUser.EmpCode;
                lintCnt = mobjCountryBLL.UpdateCountry(entCountry);

                if (lintCnt > 0)
                {
                    GetCountry();
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
                Commons.FileLog("frmCountryMaster -  BtnEdit_Click(object sender, EventArgs e)", ex);
            }
        }

        protected void chkDelete_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chk.NamingContainer;
            if (chk.Checked)
            {
                LinkButton CountryCode = (LinkButton)row.FindControl("lnkCountryCode");
                Session["CountryCode"] = CountryCode.Text;
                lblMessage.Text = string.Empty;
                BtnDelete.Enabled = true;
            }
            else
            {
                Session["CountryCode"] = string.Empty;
                BtnDelete.Enabled = false;
            }
        }
        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow drv in dgvCountry.Rows)
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
            EntityCountry entCountry = new EntityCountry();
            int cnt = 0;

            try
            {
                foreach (GridViewRow drv in dgvCountry.Rows)
                {
                    CheckBox chkDelete = (CheckBox)drv.FindControl("chkDelete");
                    if (chkDelete.Checked)
                    {
                        LinkButton lnkCountryCode = (LinkButton)drv.FindControl("lnkCountryCode");
                        string lstrCountryCode = lnkCountryCode.Text;
                        entCountry.CountryCode = lstrCountryCode;

                        cnt = mobjCountryBLL.DeleteCountry(entCountry);
                        if (cnt > 0)
                        {
                            this.modalpopupDelete.Hide();

                            Commons.ShowMessage("Record Deleted Successfully....", this.Page);

                            if (dgvCountry.Rows.Count <= 0)
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
                GetCountry();
            }
            catch (System.Threading.ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                Commons.FileLog("frmCountryMaster -   BtnDeleteOk_Click(object sender, EventArgs e)", ex);
            }
        }

        protected void dgvCountry_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dgvCountry.DataSource = (DataTable)Session["CountryDetail"];
                dgvCountry.DataBind();
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmCountryMaster - dgvCountry_PageIndexChanged(object sender, EventArgs e)", ex);
            }
        }

        protected void dgvCountry_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvCountry.PageIndex = e.NewPageIndex;
        }

        protected void dgvCountry_RowDataBound(object sender, GridViewRowEventArgs e)
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
                Commons.FileLog("frmCountryMaster -  dgvCountry_RowDataBound(object sender, GridViewRowEventArgs e)", ex);
            }
        }

        protected void dgvCountry_DataBound(object sender, EventArgs e)
        {
            int lintCurrentPage = dgvCountry.PageIndex + 1;
            lblPageCount.Text = "<b>Page</b> " + lintCurrentPage.ToString() + "<b> of </b>" + dgvCountry.PageCount.ToString();
        }
    }
}