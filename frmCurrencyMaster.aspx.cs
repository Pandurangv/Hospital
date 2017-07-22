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
    public partial class frmCurrencyMaster : System.Web.UI.Page
    {
        CurrencyBLL mobjCurrencyBLL = new CurrencyBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //SessionManager.Instance.SetSession();
                GetCurrency();
            }
        }


        protected void BtnAddNewCurrency_Click(object sender, EventArgs e)
        {
            DataTable ldt = new DataTable();
            ldt = mobjCurrencyBLL.GetNewCurrencyCode();
            txtCurrencyCode.Text = ldt.Rows[0]["CurrencyCode"].ToString();
            txtCurrencyDesc.Text = string.Empty;
            this.programmaticModalPopup.Show();
        }

        public void GetCurrency()
        {
            DataTable ldtCurrency = new DataTable();
            ldtCurrency = mobjCurrencyBLL.GetAllCurrency();

            if (ldtCurrency.Rows.Count > 0 && ldtCurrency != null)
            {
                dgvCurrency.DataSource = ldtCurrency;
                dgvCurrency.DataBind();
                Session["CurrencyDetails"] = ldtCurrency;
                int lintRowcount = ldtCurrency.Rows.Count;
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
            EntityCurrency entCurrency = new EntityCurrency();
            if (string.IsNullOrEmpty(txtCurrencyCode.Text.Trim()))
            {
                Commons.ShowMessage("Enter Currency Code", this.Page);
            }
            else
            {
                if (string.IsNullOrEmpty(txtCurrencyDesc.Text.Trim()))
                {
                    Commons.ShowMessage("Enter Currency Description", this.Page);
                }
                else
                {
                    entCurrency.CurrencyCode = txtCurrencyCode.Text.Trim();
                    entCurrency.CurrencyDesc = txtCurrencyDesc.Text.Trim();
                    entCurrency.EntryBy = SessionManager.Instance.LoginUser.EmpCode;
                    lintcnt = mobjCurrencyBLL.InsertCurrency(entCurrency);

                    if (lintcnt > 0)
                    {
                        GetCurrency();
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


        protected void dgvCurrency_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                DataTable ldt = new DataTable();
                if (e.CommandName == "EditCurrency")
                {
                    this.programmaticModalPopupEdit.Show();
                    int linIndex = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                    LinkButton lnkCurrencyCode = (LinkButton)gvr.FindControl("lnkCurrencyCode");
                    string lstrCurrencyCode = lnkCurrencyCode.Text;
                    txtEditCurrencyCode.Text = lstrCurrencyCode;
                    ldt = mobjCurrencyBLL.GetCurrencyForEdit(lstrCurrencyCode);
                    FillControls(ldt);
                }
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmCurrencyMaster -  dgvCurrency_RowCommand(object sender, GridViewCommandEventArgs e)", ex);
            }
        }

        private void FillControls(DataTable ldt)
        {
            txtEditCurrencyDesc.Text = ldt.Rows[0]["CurrencyDesc"].ToString();
        }

        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            int lintCnt = 0;
            try
            {
                EntityCurrency entCurrency = new EntityCurrency();

                entCurrency.CurrencyCode = txtEditCurrencyCode.Text;
                entCurrency.CurrencyDesc = txtEditCurrencyDesc.Text;
                entCurrency.ChangeBy = SessionManager.Instance.LoginUser.UserType;
                lintCnt = mobjCurrencyBLL.UpdateCurrency(entCurrency);

                if (lintCnt > 0)
                {
                    GetCurrency();
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
                Commons.FileLog("frmCurrencyMaster -  BtnEdit_Click(object sender, EventArgs e)", ex);
            }
        }

        protected void chkDelete_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chk.NamingContainer;
            if (chk.Checked)
            {
                LinkButton CurrencyCode = (LinkButton)row.FindControl("lnkCurrencyCode");
                Session["CurrencyCode"] = CurrencyCode.Text;
                lblMessage.Text = string.Empty;
            }
            else
            {
                Session["Currencyode"] = string.Empty;
            }
        }

        protected void BtnDeleteOk_Click(object sender, EventArgs e)
        {
            EntityCurrency entCurrency = new EntityCurrency();
            int cnt = 0;

            try
            {
                foreach (GridViewRow drv in dgvCurrency.Rows)
                {
                    CheckBox chkDelete = (CheckBox)drv.FindControl("chkDelete");
                    if (chkDelete.Checked)
                    {
                        LinkButton lnkCurrencyCode = (LinkButton)drv.FindControl("lnkCurrencyCode");
                        string lstrCurrencyCode = lnkCurrencyCode.Text;
                        entCurrency.CurrencyCode = lstrCurrencyCode;

                        cnt = mobjCurrencyBLL.DeleteCurrency(entCurrency);
                        if (cnt > 0)
                        {
                            this.modalpopupDelete.Hide();

                            Commons.ShowMessage("Record Deleted Successfully....", this.Page);

                            if (dgvCurrency.Rows.Count <= 0)
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
                GetCurrency();
            }
            catch (System.Threading.ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                Commons.FileLog("frmCurrencyMaster -   BtnDeleteOk_Click(object sender, EventArgs e)", ex);
            }

        }

        protected void BtnDelete_Click(object sender, EventArgs e)
        {

            foreach (GridViewRow drv in dgvCurrency.Rows)
            {
                CheckBox chkDelete = (CheckBox)drv.FindControl("chkDelete");
                if (chkDelete.Checked)
                {
                    this.modalpopupDelete.Show();
                }
            }
        }

        protected void dgvCurrency_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dgvCurrency.DataSource = (DataTable)Session["CurrencyDetail"];
                dgvCurrency.DataBind();
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmCurrencyMaster - dgvCurrency_PageIndexChanged(object sender, EventArgs e)", ex);
            }
        }

        protected void dgvCurrency_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvCurrency.PageIndex = e.NewPageIndex;
        }

        protected void dgvCurrency_RowDataBound(object sender, GridViewRowEventArgs e)
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
                Commons.FileLog("frmCurrencyMaster -  dgvCurrency_RowDataBound(object sender, GridViewRowEventArgs e)", ex);
            }
        }

        protected void dgvCurrency_DataBound(object sender, EventArgs e)
        {
            int lintCurrentPage = dgvCurrency.PageIndex + 1;
            lblPageCount.Text = "<b>Page</b> " + lintCurrentPage.ToString() + "<b> of </b>" + dgvCurrency.PageCount.ToString();
        }

    }
}