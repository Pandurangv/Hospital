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
    public partial class frmDiscountMaster : System.Web.UI.Page
    {
        DiscountBLL mobjDiscountBLL = new DiscountBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //SessionManager.Instance.SetSession();
                GetDiscount();
            }
        }

        protected void BtnAddNewDiscount_Click(object sender, EventArgs e)
        {
            DataTable ldt = new DataTable();
            ldt = mobjDiscountBLL.GetNewDiscountCode();
            txtDiscountCode.Text = ldt.Rows[0]["DisCode"].ToString();
            txtDiscountDesc.Text = string.Empty;
            txtDiscount.Text = "";
            this.programmaticModalPopup.Show();
        }

        public void GetDiscount()
        {
            DataTable ldtDiscount = new DataTable();
            ldtDiscount = mobjDiscountBLL.GetAllDiscount();
            if (ldtDiscount.Rows.Count > 0 && ldtDiscount != null)
            {
                dgvDiscount.DataSource = ldtDiscount;
                dgvDiscount.DataBind();
                Session["DiscountDetails"] = ldtDiscount;
                int lintRowcount = ldtDiscount.Rows.Count;
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
            EntityDiscount entDiscount = new EntityDiscount();
            if (string.IsNullOrEmpty(txtDiscountCode.Text.Trim()))
            {
                Commons.ShowMessage("Enter Discount Code", this.Page);
            }
            else
            {
                if (string.IsNullOrEmpty(txtDiscountDesc.Text.Trim()))
                {
                    Commons.ShowMessage("Enter Discount Description", this.Page);
                }
                else
                {
                    if (string.IsNullOrEmpty(txtDiscount.Text.Trim()))
                    {
                        Commons.ShowMessage("Enter Discount ", this.Page);
                    }
                    else
                    {
                        entDiscount.DiscountCode = txtDiscountCode.Text.Trim();
                        entDiscount.DiscountDesc = txtDiscountDesc.Text.Trim();
                        entDiscount.Discount = Convert.ToDecimal(txtDiscount.Text.Trim());
                        entDiscount.EntryBy = SessionManager.Instance.LoginUser.EmpCode;
                        lintcnt = mobjDiscountBLL.InsertDiscount(entDiscount);

                        if (lintcnt > 0)
                        {
                            GetDiscount();
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


        protected void dgvDiscount_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                DataTable ldt = new DataTable();
                if (e.CommandName == "EditDiscount")
                {
                    this.programmaticModalPopupEdit.Show();
                    int linIndex = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                    LinkButton lnkDiscountCode = (LinkButton)gvr.FindControl("lnkDiscountCode");
                    string lstrDiscountCode = lnkDiscountCode.Text;
                    txtEditDiscountCode.Text = lstrDiscountCode;
                    ldt = mobjDiscountBLL.GetDiscountForEdit(lstrDiscountCode);
                    FillControls(ldt);
                }
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmDiscountMaster -  dgvDiscount_RowCommand(object sender, GridViewCommandEventArgs e)", ex);
            }
        }

        private void FillControls(DataTable ldt)
        {
            txtEditDiscountDesc.Text = ldt.Rows[0]["DiscountDesc"].ToString();
            txtEditDiscount.Text = ldt.Rows[0]["Discount"].ToString();
        }

        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            int lintCnt = 0;
            try
            {
                EntityDiscount entDiscount = new EntityDiscount();

                entDiscount.DiscountCode = txtEditDiscountCode.Text;
                entDiscount.DiscountDesc = txtEditDiscountDesc.Text;
                entDiscount.Discount = Convert.ToDecimal(txtEditDiscount.Text.Trim());
                entDiscount.ChangeBy = SessionManager.Instance.LoginUser.EmpCode;
                lintCnt = mobjDiscountBLL.UpdateDiscount(entDiscount);

                if (lintCnt > 0)
                {
                    GetDiscount();
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
                Commons.FileLog("frmDiscountMaster -  BtnEdit_Click(object sender, EventArgs e)", ex);
            }
        }

        protected void chkDelete_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chk.NamingContainer;
            if (chk.Checked)
            {
                LinkButton DiscountCode = (LinkButton)row.FindControl("lnkDiscountCode");
                Session["DiscountCode"] = DiscountCode.Text;
                lblMessage.Text = string.Empty;
            }
            else
            {
                Session["Discountode"] = string.Empty;
            }
        }

        protected void BtnDeleteOk_Click(object sender, EventArgs e)
        {
            EntityDiscount entDiscount = new EntityDiscount();
            int cnt = 0;

            try
            {
                foreach (GridViewRow drv in dgvDiscount.Rows)
                {
                    CheckBox chkDelete = (CheckBox)drv.FindControl("chkDelete");
                    if (chkDelete.Checked)
                    {
                        LinkButton lnkDiscountCode = (LinkButton)drv.FindControl("lnkDiscountCode");
                        string lstrDiscountCode = lnkDiscountCode.Text;
                        entDiscount.DiscountCode = lstrDiscountCode;

                        cnt = mobjDiscountBLL.DeleteDiscount(entDiscount);
                        if (cnt > 0)
                        {
                            this.modalpopupDelete.Hide();

                            Commons.ShowMessage("Record Deleted Successfully....", this.Page);

                            if (dgvDiscount.Rows.Count <= 0)
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
                GetDiscount();
            }
            catch (System.Threading.ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                Commons.FileLog("frmDiscountMaster -   BtnDeleteOk_Click(object sender, EventArgs e)", ex);
            }

        }

        protected void BtnDelete_Click(object sender, EventArgs e)
        {

            foreach (GridViewRow drv in dgvDiscount.Rows)
            {
                CheckBox chkDelete = (CheckBox)drv.FindControl("chkDelete");
                if (chkDelete.Checked)
                {
                    this.modalpopupDelete.Show();
                }
            }
        }

        protected void dgvDiscount_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dgvDiscount.DataSource = (DataTable)Session["DiscountDetail"];
                dgvDiscount.DataBind();
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmDiscountMaster - dgvDiscount_PageIndexChanged(object sender, EventArgs e)", ex);
            }
        }

        protected void dgvDiscount_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvDiscount.PageIndex = e.NewPageIndex;
        }

        protected void dgvDiscount_RowDataBound(object sender, GridViewRowEventArgs e)
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
                Commons.FileLog("frmDiscountMaster -  dgvDiscount_RowDataBound(object sender, GridViewRowEventArgs e)", ex);
            }
        }

        protected void dgvDiscount_DataBound(object sender, EventArgs e)
        {
            int lintCurrentPage = dgvDiscount.PageIndex + 1;
            lblPageCount.Text = "<b>Page</b> " + lintCurrentPage.ToString() + "<b> of </b>" + dgvDiscount.PageCount.ToString();
        }

    }
}