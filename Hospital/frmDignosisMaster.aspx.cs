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
    public partial class frmDignosisMaster : System.Web.UI.Page
    {
        DignosisBLL mobjDignosisBLL = new DignosisBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //SessionManager.Instance.SetSession();
                GetDignosis();
            }

        }
        protected void BtnAddNewDignosis_Click(object sender, EventArgs e)
        {
            DataTable ldt = new DataTable();
            ldt = mobjDignosisBLL.GetNewDignosisCode();
            txtDignosisCode.Text = ldt.Rows[0]["DignosisCode"].ToString();
            txtDignosisDesc.Text = string.Empty;
            this.programmaticModalPopup.Show();

        }
        public void GetDignosis()
        {
            DataTable ldtDignosis = new DataTable();
            ldtDignosis = mobjDignosisBLL.GetAllDignosis();
            if (ldtDignosis.Rows.Count > 0 && ldtDignosis != null)
            {
                dgvDignosis.DataSource = ldtDignosis;
                dgvDignosis.DataBind();
                Session["DignosisDetails"] = ldtDignosis;
                int lintRowcount = ldtDignosis.Rows.Count;
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
            EntityDignosis entDignosis = new EntityDignosis();
            if (string.IsNullOrEmpty(txtDignosisCode.Text.Trim()))
            {
                Commons.ShowMessage("Enter Dignosis Code", this.Page);
            }
            else
            {
                if (string.IsNullOrEmpty(txtDignosisDesc.Text.Trim()))
                {
                    Commons.ShowMessage("Enter Dignosis Description", this.Page);
                }
                else
                {
                    entDignosis.DignosisCode = txtDignosisCode.Text.Trim();
                    entDignosis.DignosisDesc = txtDignosisDesc.Text.Trim();
                    entDignosis.EntryBy = SessionManager.Instance.LoginUser.EmpCode;
                    lintcnt = mobjDignosisBLL.InsertDignosis(entDignosis);

                    if (lintcnt > 0)
                    {
                        GetDignosis();
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
        protected void dgvDignosis_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                DataTable ldt = new DataTable();
                if (e.CommandName == "EditDignosis")
                {
                    this.programmaticModalPopupEdit.Show();
                    int linIndex = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                    LinkButton lnkDignosisCode = (LinkButton)gvr.FindControl("lnkDignosisCode");
                    string lstrDignosisCode = lnkDignosisCode.Text;
                    txtEditDignosisCode.Text = lstrDignosisCode;
                    ldt = mobjDignosisBLL.GetDignosisForEdit(lstrDignosisCode);
                    FillControls(ldt);
                }
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmDignosisMaster -  dgvDignosis_RowCommand(object sender, GridViewCommandEventArgs e)", ex);
            }
        }

        private void FillControls(DataTable ldt)
        {
            txtEditDignosisDesc.Text = ldt.Rows[0]["DignosisDesc"].ToString();
        }

        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            int lintCnt = 0;
            try
            {
                EntityDignosis entDignosis = new EntityDignosis();

                entDignosis.DignosisCode = txtEditDignosisCode.Text;
                entDignosis.DignosisDesc = txtEditDignosisDesc.Text;
                entDignosis.ChangeBy = SessionManager.Instance.LoginUser.EmpCode;
                lintCnt = mobjDignosisBLL.UpdateDignosis(entDignosis);

                if (lintCnt > 0)
                {
                    GetDignosis();
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
                Commons.FileLog("frmDignosisMaster -  BtnEdit_Click(object sender, EventArgs e)", ex);
            }
        }

        protected void chkDelete_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chk.NamingContainer;
            if (chk.Checked)
            {
                LinkButton DignosisCode = (LinkButton)row.FindControl("lnkDignosisCode");
                Session["DignosisCode"] = DignosisCode.Text;
                lblMessage.Text = string.Empty;
            }
            else
            {
                Session["DignosisCode"] = string.Empty;
            }
        }
        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow drv in dgvDignosis.Rows)
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
            EntityDignosis entDignosis = new EntityDignosis();
            int cnt = 0;
            try
            {
                foreach (GridViewRow drv in dgvDignosis.Rows)
                {
                    CheckBox chkDelete = (CheckBox)drv.FindControl("chkDelete");
                    if (chkDelete.Checked)
                    {
                        LinkButton lnkDignosisCode = (LinkButton)drv.FindControl("lnkDignosisCode");
                        string lstrDignosisCode = lnkDignosisCode.Text;
                        entDignosis.DignosisCode = lstrDignosisCode;

                        cnt = mobjDignosisBLL.DeleteDignosis(entDignosis);
                        if (cnt > 0)
                        {
                            this.modalpopupDelete.Hide();
                            Commons.ShowMessage("Record Deleted Successfully....", this.Page);
                            if (dgvDignosis.Rows.Count <= 0)
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
                GetDignosis();
            }
            catch (System.Threading.ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                Commons.FileLog("frmDignosisMaster -   BtnDeleteOk_Click(object sender, EventArgs e)", ex);
            }
        }

        protected void dgvDignosis_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dgvDignosis.DataSource = (DataTable)Session["DignosisDetail"];
                dgvDignosis.DataBind();
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmDignosisMaster - dgvDignosis_PageIndexChanged(object sender, EventArgs e)", ex);
            }
        }

        protected void dgvDignosis_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvDignosis.PageIndex = e.NewPageIndex;
        }

        protected void dgvDignosis_RowDataBound(object sender, GridViewRowEventArgs e)
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
                Commons.FileLog("frmDignosisMaster -  dgvDignosis_RowDataBound(object sender, GridViewRowEventArgs e)", ex);
            }
        }

        protected void dgvDignosis_DataBound(object sender, EventArgs e)
        {
            int lintCurrentPage = dgvDignosis.PageIndex + 1;
            lblPageCount.Text = "<b>Page</b> " + lintCurrentPage.ToString() + "<b> of </b>" + dgvDignosis.PageCount.ToString();
        }
    }
}