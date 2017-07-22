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
    public partial class frmPatientMainType : System.Web.UI.Page
    {
        PatientTypeBLL mobjPatientTypeBLL = new PatientTypeBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //SessionManager.Instance.SetSession();
                GetPatient();
            }
        }
        protected void BtnAddNewPatient_Click(object sender, EventArgs e)
        {
            DataTable ldt = new DataTable();
            ldt = mobjPatientTypeBLL.GetNewPatientCode();
            txtPatientCode.Text = ldt.Rows[0]["PatientCode"].ToString();
            txtPatientDesc.Text = string.Empty;
            this.programmaticModalPopup.Show();

        }

        public void GetPatient()
        {
            DataTable ldtPatient = new DataTable();
            ldtPatient = mobjPatientTypeBLL.GetAllPatient();
            if (ldtPatient.Rows.Count > 0 && ldtPatient != null)
            {
                dgvPatient.DataSource = ldtPatient;
                dgvPatient.DataBind();
                Session["PatientDetails"] = ldtPatient;
                int lintRowcount = ldtPatient.Rows.Count;
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
            EntityPatientType entPatient = new EntityPatientType();
            if (string.IsNullOrEmpty(txtPatientCode.Text.Trim()))
            {
                Commons.ShowMessage("Enter Country Code", this.Page);
            }
            else
            {
                if (string.IsNullOrEmpty(txtPatientDesc.Text.Trim()))
                {
                    Commons.ShowMessage("Enter Country Description", this.Page);
                }
                else
                {
                    entPatient.PatientCode = txtPatientCode.Text.Trim();
                    entPatient.PatientDesc = txtPatientDesc.Text.Trim();
                    entPatient.EntryBy = SessionManager.Instance.LoginUser.EmpCode;
                    lintcnt = mobjPatientTypeBLL.InsertPatient(entPatient);

                    if (lintcnt > 0)
                    {
                        GetPatient();
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


        protected void dgvPatient_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                DataTable ldt = new DataTable();
                if (e.CommandName == "EditPatient")
                {
                    this.programmaticModalPopupEdit.Show();
                    int linIndex = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                    LinkButton lnkPatientCode = (LinkButton)gvr.FindControl("lnkPatientCode");
                    string lstrPatientCode = lnkPatientCode.Text;
                    txtEditPatientCode.Text = lstrPatientCode;
                    ldt = mobjPatientTypeBLL.GetPatientForEdit(lstrPatientCode);
                    FillControls(ldt);
                }
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmPatientMainTypeMaster -  dgvPatient_RowCommand(object sender, GridViewCommandEventArgs e)", ex);
            }
        }

        private void FillControls(DataTable ldt)
        {
            txtEditPatientDesc.Text = ldt.Rows[0]["PatientDesc"].ToString();
        }

        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            int lintCnt = 0;
            try
            {
                EntityPatientType entPatient = new EntityPatientType();

                entPatient.PatientCode = txtEditPatientCode.Text;
                entPatient.PatientDesc = txtEditPatientDesc.Text;
                entPatient.ChangeBy = SessionManager.Instance.LoginUser.EmpCode;
                lintCnt = mobjPatientTypeBLL.UpdatePatient(entPatient);

                if (lintCnt > 0)
                {
                    GetPatient();
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
                Commons.FileLog("frmPatientMainTypeMaster -  BtnEdit_Click(object sender, EventArgs e)", ex);
            }
        }
        protected void chkDelete_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chk.NamingContainer;
            if (chk.Checked)
            {
                LinkButton PatientCode = (LinkButton)row.FindControl("lnkPatientCode");
                Session["PatientCode"] = PatientCode.Text;
                lblMessage.Text = string.Empty;
            }
            else
            {
                Session["PatientCode"] = string.Empty;
            }
        }


        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow drv in dgvPatient.Rows)
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
            EntityPatientType entPatient = new EntityPatientType();
            int cnt = 0;

            try
            {
                foreach (GridViewRow drv in dgvPatient.Rows)
                {
                    CheckBox chkDelete = (CheckBox)drv.FindControl("chkDelete");
                    if (chkDelete.Checked)
                    {
                        LinkButton lnkPatientCode = (LinkButton)drv.FindControl("lnkPatientCode");
                        string lstrPatientCode = lnkPatientCode.Text;
                        entPatient.PatientCode = lstrPatientCode;

                        cnt = mobjPatientTypeBLL.DeletePatient(entPatient);
                        if (cnt > 0)
                        {
                            this.modalpopupDelete.Hide();

                            Commons.ShowMessage("Record Deleted Successfully....", this.Page);

                            if (dgvPatient.Rows.Count <= 0)
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
                GetPatient();
            }
            catch (System.Threading.ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                Commons.FileLog("frmPatientMainTypeMaster -   BtnDeleteOk_Click(object sender, EventArgs e)", ex);
            }
        }


        protected void dgvPatient_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dgvPatient.DataSource = (DataTable)Session["PatientDetail"];
                dgvPatient.DataBind();
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmPatientMainTypeMaster - dgvPatient_PageIndexChanged(object sender, EventArgs e)", ex);
            }
        }

        protected void dgvPatient_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvPatient.PageIndex = e.NewPageIndex;
        }


        protected void dgvPatient_RowDataBound(object sender, GridViewRowEventArgs e)
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
                Commons.FileLog("frmPatientMainTypeMaster -  dgvPatient_RowDataBound(object sender, GridViewRowEventArgs e)", ex);
            }
        }


        protected void dgvPatient_DataBound(object sender, EventArgs e)
        {
            int lintCurrentPage = dgvPatient.PageIndex + 1;
            lblPageCount.Text = "<b>Page</b> " + lintCurrentPage.ToString() + "<b> of </b>" + dgvPatient.PageCount.ToString();
        }

    }
}