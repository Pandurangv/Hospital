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
    public partial class frmConsultantMaster : System.Web.UI.Page
    {
        ConsultantBLL mobjConsultantBLL = new ConsultantBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //SessionManager.Instance.SetSession();
                GetConsultant();
                FillWardCombo();
                FillEditWardCombo();
            }
        }

        public void FillWardCombo()
        {
            DataTable ldt = new DataTable();
            ldt = mobjConsultantBLL.GetAllWards();
            ddlWard.DataSource = ldt;
            ddlWard.DataTextField = "CategoryDesc";
            ddlWard.DataValueField = "PKId";
            ddlWard.DataBind();
            ListItem li = new ListItem();
            li.Text = "--Select Ward--";
            li.Value = "0";
            ddlWard.Items.Insert(0, li);
        }

        public void FillEditWardCombo()
        {
            DataTable ldt = new DataTable();
            ldt = mobjConsultantBLL.GetAllWards();
            ddlEditWardNo.DataSource = ldt;
            ddlEditWardNo.DataTextField = "CategoryDesc";
            ddlEditWardNo.DataValueField = "PKId";
            ddlEditWardNo.DataBind();
            ListItem li = new ListItem();
            li.Text = "--Select Ward--";
            li.Value = "0";
            ddlEditWardNo.Items.Insert(0, li);
        }

        protected void BtnAddNewConsultant_Click(object sender, EventArgs e)
        {
            DataTable ldt = new DataTable();
            txtAddress.Text = string.Empty;
            txtFirstName.Text = string.Empty;
            txtMiddleName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtJoiningDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtBirthDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            ldt = mobjConsultantBLL.GetNewConsultantCode();
            txtConsultantCode.Text = ldt.Rows[0]["ConsCode"].ToString();
            ddlWard.SelectedIndex = 0;
            txtFees.Text = string.Empty;
            this.programmaticModalPopup.Show();
        }
        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow drv in dgvConsultant.Rows)
            {
                CheckBox chkDelete = (CheckBox)drv.FindControl("chkDelete");
                if (chkDelete.Checked)
                {
                    txtDeleteRemark.Text = string.Empty;
                    this.modalpopupDelete.Show();
                }
            }
        }
        protected void dgvConsultant_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dgvConsultant.DataSource = (DataTable)Session["ConsultantDetail"];
                dgvConsultant.DataBind();
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmConsultantMaster - dgvConsultant_PageIndexChanged(object sender, EventArgs e)", ex);
            }
        }
        protected void dgvConsultant_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvConsultant.PageIndex = e.NewPageIndex;
        }
        protected void dgvConsultant_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                DataTable ldt = new DataTable();
                if (e.CommandName == "EditConsultant")
                {
                    this.programmaticModalPopupEdit.Show();
                    int linIndex = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                    LinkButton lnkConsultantCode = (LinkButton)gvr.FindControl("lnkConsCode");
                    string lstrConsultantCode = lnkConsultantCode.Text;
                    txtEditConsultantCode.Text = lstrConsultantCode;
                    Session["ConsCode"] = lnkConsultantCode;
                    ldt = mobjConsultantBLL.SelectConsultantForEdit(lstrConsultantCode);
                    FillControls(ldt);
                }
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmConsultantMaster -  dgvConsultant_RowCommand(object sender, GridViewCommandEventArgs e)", ex);
            }
        }

        private void FillControls(DataTable ldt)
        {
            txtEditConsultantCode.Text = ldt.Rows[0]["ConsCode"].ToString();
            txtEditFirstName.Text = ldt.Rows[0]["ConsFirstName"].ToString();
            txtEditMiddleName.Text = ldt.Rows[0]["ConsMiddleName"].ToString();
            txtEditLastName.Text = ldt.Rows[0]["ConsLastName"].ToString();
            txtEditAddress.Text = ldt.Rows[0]["ConsAddress"].ToString();
            txtEditBirthDate.Text = StringExtension.ToDateTime(ldt.Rows[0]["ConsDOB"].ToString()).ToString("dd/MM/yyyy");
            txtEditJoiningDate.Text = StringExtension.ToDateTime(ldt.Rows[0]["ConsDOJ"].ToString()).ToString("dd/MM/yyyy");
            txtEditFees.Text = ldt.Rows[0]["Fees"].ToString();
            ddlWard.SelectedValue = ldt.Rows[0]["WardNo"].ToString();
        }

        protected void dgvConsultant_DataBound(object sender, EventArgs e)
        {
            int lintCurrentPage = dgvConsultant.PageIndex + 1;
            lblPageCount.Text = "<b>Page</b> " + lintCurrentPage.ToString() + "<b> of </b>" + dgvConsultant.PageCount.ToString();
        }
        protected void dgvConsultant_RowDataBound(object sender, GridViewRowEventArgs e)
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
                Commons.FileLog("frmConsultantMaster -  dgvData_RowDataBound(object sender, GridViewRowEventArgs e)", ex);
            }
        }
        private void GetConsultant()
        {
            DataTable ldtCons = new DataTable();
            ldtCons = mobjConsultantBLL.SelectAllConsultant();

            if (ldtCons.Rows.Count > 0 && ldtCons != null)
            {
                dgvConsultant.DataSource = ldtCons;
                dgvConsultant.DataBind();
                Session["ConsultantDetail"] = ldtCons;
                int lintRowcount = ldtCons.Rows.Count;
                lblRowCount.Text = "<b>Total Records:</b> " + lintRowcount.ToString();
            }
            else
            {
                pnlShow.Style.Add(HtmlTextWriterStyle.Display, "none");
                hdnPanel.Value = "none";
            }

        }
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            int cnt = 0;
            try
            {
                EntityConsultant entConsultant = new EntityConsultant();
                entConsultant.ConsultantCode = txtConsultantCode.Text;
                entConsultant.FirstName = txtFirstName.Text.Trim();
                entConsultant.MiddleName = txtMiddleName.Text.Trim();
                entConsultant.LastName = txtLastName.Text.Trim();
                entConsultant.Address = txtAddress.Text.Trim();
                entConsultant.WardNo = Commons.ConvertToInt(ddlWard.SelectedValue);
                entConsultant.Fees = Commons.ConvertToDecimal(txtFees.Text);

                if (txtBirthDate.Text == string.Empty)
                {
                    entConsultant.DOB = System.DateTime.Today.Date;
                }
                else
                {
                    entConsultant.DOB = StringExtension.ToDateTime(txtBirthDate.Text);
                }
                if (txtJoiningDate.Text == string.Empty)
                {
                    entConsultant.DOJ = System.DateTime.Today.Date;
                }
                else
                {
                    entConsultant.DOJ = StringExtension.ToDateTime(txtJoiningDate.Text);
                }
                if (entConsultant.DOB >= entConsultant.DOJ)
                {
                    Commons.ShowMessage("Birth Date should be older than Joining Date...", this.Page);
                    return;
                }
                else
                {
                    entConsultant.EntryBy = SessionManager.Instance.LoginUser.EmpCode;
                    cnt = mobjConsultantBLL.InsertConsultant(entConsultant);
                    if (cnt > 0)
                    {
                        Commons.ShowMessage("Record Inserted Successfully", this.Page);
                        GetConsultant();
                        Response.Redirect("frmConsultantMaster.aspx");
                        this.programmaticModalPopup.Hide();
                    }
                    else
                    {
                        Commons.ShowMessage("Record Not Inserted", this.Page);
                    }
                }
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmConsultantMaster -> BtnSave_Click(object sender, EventArgs e)", ex);
            }
        }

        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            int cnt = 0;
            List<EntityConsultant> lstentConsultant = new List<EntityConsultant>();
            try
            {
                EntityConsultant entConsultant = new EntityConsultant();
                entConsultant.ConsultantCode = txtEditConsultantCode.Text;
                entConsultant.FirstName = txtEditFirstName.Text.Trim();
                entConsultant.MiddleName = txtEditMiddleName.Text.Trim();
                entConsultant.LastName = txtEditLastName.Text.Trim();
                entConsultant.Address = txtEditAddress.Text.Trim();
                entConsultant.WardNo = Commons.ConvertToInt(ddlEditWardNo.SelectedValue);
                entConsultant.Fees = Commons.ConvertToDecimal(txtEditFees.Text);

                if (txtBirthDate.Text == string.Empty)
                {
                    entConsultant.DOB = System.DateTime.Today.Date;
                }
                else
                {
                    entConsultant.DOB = StringExtension.ToDateTime(txtEditBirthDate.Text.Trim()).Date;
                }

                if (txtJoiningDate.Text == string.Empty)
                {
                    entConsultant.DOJ = System.DateTime.Today.Date;
                }
                else
                {
                    entConsultant.DOJ = StringExtension.ToDateTime(txtEditJoiningDate.Text.Trim()).Date;
                }
                entConsultant.ChaneBy = SessionManager.Instance.LoginUser.UserType;
                entConsultant.Address = txtEditAddress.Text.Trim();
                cnt = mobjConsultantBLL.UpdateConsultant(entConsultant);

                if (cnt > 0)
                {
                    GetConsultant();
                    Commons.ShowMessage("Record updated Successfully", this.Page);
                    this.programmaticModalPopupEdit.Hide();
                }
                else
                {
                    Commons.ShowMessage("Record Not Updated", this.Page);
                }
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmConsultantMaster -> BtnSave_Click(object sender, EventArgs e)", ex);
            }
        }

        protected void chkDelete_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chk.NamingContainer;
            if (chk.Checked)
            {
                LinkButton ConsCode = (LinkButton)row.FindControl("lnkConsCode");
                Session["ConsCode"] = ConsCode.Text;
                lblMessage.Text = string.Empty;
            }
            else
            {
                Session["ConsCode"] = string.Empty;
            }
        }

        protected void BtnDeleteOk_Click(object sender, EventArgs e)
        {
            EntityConsultant entConsultant = new EntityConsultant();
            List<EntityConsultant> lstentConsultant = new List<EntityConsultant>();
            DataTable ldt = new DataTable();
            int cnt = 0;
            try
            {

                foreach (GridViewRow drv in dgvConsultant.Rows)
                {
                    CheckBox chkDelete = (CheckBox)drv.FindControl("chkDelete");
                    if (chkDelete.Checked)
                    {
                        LinkButton lnkConsultantCode = (LinkButton)drv.FindControl("lnkConsCode");
                        string lstrConsultantCode = lnkConsultantCode.Text;
                        entConsultant.ConsultantCode = lstrConsultantCode;
                        entConsultant.DisContinued = true;
                        if (txtDeleteRemark.Text == string.Empty)
                        {
                            this.modalpopupDelete.Show();
                            lblMessage.Text = "Enter Delete Remarks";
                        }
                        else
                        {
                            entConsultant.DisContRemark = txtDeleteRemark.Text.Trim();
                            lstentConsultant.Add(entConsultant);
                            cnt = mobjConsultantBLL.DeleteConsultant(lstentConsultant);
                            if (cnt > 0)
                            {
                                Commons.ShowMessage("Record Deleted Successfully", this.Page);
                                GetConsultant();
                                if (dgvConsultant.Rows.Count <= 0)
                                {
                                    pnlShow.Style.Add(HtmlTextWriterStyle.Display, "none");
                                    hdnPanel.Value = "none";
                                }
                            }
                            else
                            {
                                Commons.ShowMessage("Record Not Deleted...", this.Page);
                            }
                        }
                    }
                }
                this.modalpopupDelete.Hide();

            }
            catch (System.Threading.ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                Commons.FileLog("frmConsultantMaster -   BtnDeleteOk_Click(object sender, EventArgs e)", ex);
            }
        }

    }
}