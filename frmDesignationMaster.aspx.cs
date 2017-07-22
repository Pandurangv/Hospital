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
    public partial class frmDesignationMaster : System.Web.UI.Page
    {
        DesignationBLL mobjDesignationBLL = new DesignationBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            ////SessionManager.Instance.SetSession();
            if (SessionManager.Instance.LoginUser != null)
            {
                if (!Page.IsPostBack)
                {
                    //SessionManager.Instance.SetSession();
                    GetDesignations();
                }
            }
            else
            {
                //Response.Redirect("frmlogin.aspx", false);
            }
        }

        private void GetDesignations()
        {
            DataTable ldtDesi = new DataTable();
            ldtDesi = mobjDesignationBLL.GetAllDesignations();

            if (ldtDesi.Rows.Count > 0 && ldtDesi != null)
            {
                dgvDesignation.DataSource = ldtDesi;
                dgvDesignation.DataBind();
                Session["DesignationDetail"] = ldtDesi;
                int lintRowcount = ldtDesi.Rows.Count;
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

        protected void BtnAddNewDesi_Click(object sender, EventArgs e)
        {
            DataTable ldt = new DataTable();
            txtDesiDesc.Text = string.Empty;
            ldt = mobjDesignationBLL.GetNewDesignationCode();
            txtDesiCode.Text = ldt.Rows[0]["DesnCode"].ToString();
            this.programmaticModalPopup.Show();
        }

        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow drv in dgvDesignation.Rows)
            {
                CheckBox chkDelete = (CheckBox)drv.FindControl("chkDelete");
                if (chkDelete.Checked)
                {
                    this.modalpopupDelete.Show();
                }
            }
        }

        protected void chkDelete_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chk.NamingContainer;
            if (chk.Checked)
            {
                LinkButton DeptCode = (LinkButton)row.FindControl("lnkDesiCode");
                Session["DesiCode"] = DeptCode.Text;
                lblMessage.Text = string.Empty;
            }
            else
            {
                Session["DesiCode"] = string.Empty;
            }
        }

        private void FillControls(DataTable ldt)
        {
            txtEditDesiDesc.Text = ldt.Rows[0]["DesignationDesc"].ToString();
        }

        protected void dgvDesignation_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dgvDesignation.DataSource = (DataTable)Session["DesignationDetail"];
                dgvDesignation.DataBind();
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmDesignationMaster - dgvDesignation_PageIndexChanged(object sender, EventArgs e)", ex);
            }
        }
        protected void dgvDesignation_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvDesignation.PageIndex = e.NewPageIndex;
        }
        protected void dgvDesignation_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                DataTable ldt = new DataTable();
                if (e.CommandName == "EditDesi")
                {
                    this.programmaticModalPopupEdit.Show();
                    int linIndex = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                    LinkButton lnkDesiCode = (LinkButton)gvr.FindControl("lnkDesiCode");
                    string lstrDesiCode = lnkDesiCode.Text;
                    txtEditDesiCode.Text = lstrDesiCode;
                    ldt = mobjDesignationBLL.GetDesignationForEdit(lstrDesiCode);
                    FillControls(ldt);
                }
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmDesignationMaster - dgvDesignation_RowCommand(object sender, GridViewCommandEventArgs e)", ex);
            }
        }
        protected void dgvDesignation_DataBound(object sender, EventArgs e)
        {
            int lintCurrentPage = dgvDesignation.PageIndex + 1;
            lblPageCount.Text = "<b>Page</b> " + lintCurrentPage.ToString() + "<b> of </b>" + dgvDesignation.PageCount.ToString();
        }
        protected void dgvDesignation_RowDataBound(object sender, GridViewRowEventArgs e)
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
                Commons.FileLog("frmDesignationMaster -  dgvDesignation_RowDataBound(object sender, GridViewRowEventArgs e)", ex);
            }
        }
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            int lintcnt = 0;
            EntityDesignation entDesi = new EntityDesignation();
            if (string.IsNullOrEmpty(txtDesiCode.Text.Trim()))
            {
                lblMsg.Text = "Please Enter Designation Code";
            }
            else
            {
                if (string.IsNullOrEmpty(txtDesiDesc.Text.Trim()))
                {
                    lblMsg.Text = "Please Enter Designation Description";
                }
                else
                {
                    entDesi.DesignationCode = txtDesiCode.Text.Trim();
                    entDesi.DesignationDesc = txtDesiDesc.Text.Trim();
                    entDesi.EntryBy = SessionManager.Instance.LoginUser.EmpCode;
                    lintcnt = mobjDesignationBLL.InsertDesignation(entDesi);

                    if (lintcnt > 0)
                    {
                        GetDesignations();
                        lblMessage.Text = "Record Inserted Successfully";
                        this.programmaticModalPopup.Hide();
                    }
                    else
                    {
                        lblMessage.Text = "Record Not Inserted";
                    }
                }
            }
        }
        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            int lintcnt = 0;
            EntityDesignation entDesi = new EntityDesignation();
            if (string.IsNullOrEmpty(txtEditDesiCode.Text.Trim()))
            {
                lblMsg.Text = "Please Enter Designation Code";
            }
            else
            {
                if (string.IsNullOrEmpty(txtEditDesiDesc.Text.Trim()))
                {
                    lblMsg.Text = "Please Enter Designation Description";
                }
                else
                {
                    entDesi.DesignationCode = txtEditDesiCode.Text.Trim();
                    entDesi.DesignationDesc = txtEditDesiDesc.Text.Trim();
                    entDesi.ChangeBy = SessionManager.Instance.LoginUser.EmpCode;
                    lintcnt = mobjDesignationBLL.UpdateDesignation(entDesi);

                    if (lintcnt > 0)
                    {
                        GetDesignations();
                        lblMessage.Text = "Record Updated Successfully";
                        this.programmaticModalPopup.Hide();
                    }
                    else
                    {
                        lblMessage.Text = "Record Not Updated";
                    }
                }
            }
        }
        protected void BtnDeleteOk_Click(object sender, EventArgs e)
        {
            EntityDesignation entDesi = new EntityDesignation();
            int cnt = 0;

            try
            {
                foreach (GridViewRow drv in dgvDesignation.Rows)
                {
                    CheckBox chkDelete = (CheckBox)drv.FindControl("chkDelete");
                    if (chkDelete.Checked)
                    {
                        LinkButton lnkDesiCode = (LinkButton)drv.FindControl("lnkDesiCode");
                        string lstrDesiCode = lnkDesiCode.Text;
                        entDesi.DesignationCode = lstrDesiCode;
                        cnt = mobjDesignationBLL.DeleteDesignation(entDesi);
                        if (cnt > 0)
                        {
                            this.modalpopupDelete.Hide();
                            lblMessage.Text = "Record Deleted Successfully....";
                            if (dgvDesignation.Rows.Count <= 0)
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
                GetDesignations();
            }
            catch (System.Threading.ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                Commons.FileLog("frmDesignationMaster -   BtnDeleteOk_Click(object sender, EventArgs e)", ex);
            }
        }
    }
}