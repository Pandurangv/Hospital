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
    public partial class frmConsultantChargeMaster : System.Web.UI.Page
    {
        ConsultantChargeBLL mobjConChargeBLL = new ConsultantChargeBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //SessionManager.Instance.SetSession();
                FillConsultantCombo();
                FillWardCombo();
                FillEditConsultantCombo();
                FillEditWardCombo();
                GetConsultantChrages();
            }
        }
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            int cnt = 0;
            EntityConsultantChargeMaster entConsChargeMaster = new EntityConsultantChargeMaster();
            try
            {
                entConsChargeMaster.ConsultantId = Commons.ConvertToInt(ddlConsultant.SelectedValue);
                entConsChargeMaster.WardNo = Commons.ConvertToInt(ddlWard.SelectedValue);
                entConsChargeMaster.Charge = Commons.ConvertToDecimal(txtFees.Text);
                entConsChargeMaster.UserName = SessionManager.Instance.LoginUser.EmpCode;
                if (!Commons.IsRecordExists("tblConsultantChargeMaster", "Ward", entConsChargeMaster.WardNo.ToString()))
                {
                    cnt = mobjConChargeBLL.InsertConsultantCharges(entConsChargeMaster);

                    if (cnt > 0)
                    {
                        Commons.ShowMessage("Record Inserted Successfully....", this.Page);
                        GetConsultantChrages();
                    }
                    else
                    {
                        Commons.ShowMessage("Erro While Inserting Record....", this.Page);
                    }
                }
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmConsultantChargeMaster - BtnSave_Click(object sender, EventArgs e)", ex);
            }
        }
        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            int cnt = 0;
            EntityConsultantChargeMaster entConsChargeMaster = new EntityConsultantChargeMaster();
            try
            {
                entConsChargeMaster.PKId = Commons.ConvertToInt(Session["PKId"]);
                entConsChargeMaster.ConsultantId = Commons.ConvertToInt(ddlEditConsultant.SelectedValue);
                entConsChargeMaster.WardNo = Commons.ConvertToInt(ddlEditWardNo.SelectedValue);
                entConsChargeMaster.Charge = Commons.ConvertToDecimal(txtEditFees.Text);
                entConsChargeMaster.UserName = SessionManager.Instance.LoginUser.EmpCode;
                cnt = mobjConChargeBLL.UpdateConsultantCharges(entConsChargeMaster);

                if (cnt > 0)
                {
                    Commons.ShowMessage("Record Updated Successfully....", this.Page);
                    GetConsultantChrages();
                }
                else
                {
                    Commons.ShowMessage("Error While Updating Record....", this.Page);
                }

            }
            catch (Exception ex)
            {
                Commons.FileLog("frmConsultantChargeMaster -  BtnEdit_Click(object sender, EventArgs e)", ex);
            }
        }
        protected void BtnDeleteOk_Click(object sender, EventArgs e)
        {
            int cnt = 0;
            try
            {

                foreach (GridViewRow drv in dgvConsultantCharge.Rows)
                {
                    CheckBox chkDelete = (CheckBox)drv.FindControl("chkDelete");
                    if (chkDelete.Checked)
                    {
                        LinkButton lnkPKId = (LinkButton)drv.FindControl("lnkPKId");
                        int lintPKID = Commons.ConvertToInt(lnkPKId.Text);

                        cnt = mobjConChargeBLL.DeleteConsultantCharges(lintPKID);
                        if (cnt > 0)
                        {
                            Commons.ShowMessage("Record Deleted Successfully", this.Page);
                            GetConsultantChrages();
                            if (dgvConsultantCharge.Rows.Count <= 0)
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
        protected void BtnAddNewConsultant_Click(object sender, EventArgs e)
        {
            this.programmaticModalPopup.Show();
        }
        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            this.modalpopupDelete.Show();
        }

        #region FillCombo

        public void FillConsultantCombo()
        {
            DataTable ldt = new DataTable();
            ldt = mobjConChargeBLL.GetConsultants();
            if (ldt.Rows.Count > 0 && ldt != null)
            {
                ddlConsultant.DataSource = ldt;
                ddlConsultant.DataTextField = "ConsultantName";
                ddlConsultant.DataValueField = "PKId";
                ddlConsultant.DataBind();
                ListItem li = new ListItem();
                li.Text = "--Select Consultant--";
                li.Value = "0";
                ddlConsultant.Items.Insert(0, li);
            }
        }

        public void FillWardCombo()
        {
            DataTable ldt = new DataTable();
            ldt = mobjConChargeBLL.GetWard();
            if (ldt.Rows.Count > 0 && ldt != null)
            {
                ddlWard.DataSource = ldt;
                ddlWard.DataTextField = "CategoryDesc";
                ddlWard.DataValueField = "PKId";
                ddlWard.DataBind();
                ListItem li = new ListItem();
                li.Text = "--Select Ward--";
                li.Value = "0";
                ddlWard.Items.Insert(0, li);
            }
        }

        public void FillEditConsultantCombo()
        {
            DataTable ldt = new DataTable();
            ldt = mobjConChargeBLL.GetConsultants();
            if (ldt.Rows.Count > 0 && ldt != null)
            {
                ddlEditConsultant.DataSource = ldt;
                ddlEditConsultant.DataTextField = "ConsultantName";
                ddlEditConsultant.DataValueField = "PKId";
                ddlEditConsultant.DataBind();
            }
        }

        public void FillEditWardCombo()
        {
            DataTable ldt = new DataTable();
            ldt = mobjConChargeBLL.GetWard();
            if (ldt.Rows.Count > 0 && ldt != null)
            {
                ddlEditWardNo.DataSource = ldt;
                ddlEditWardNo.DataTextField = "CategoryDesc";
                ddlEditWardNo.DataValueField = "PKId";
                ddlEditWardNo.DataBind();
            }
        }
        #endregion

        #region Methods

        private void GetConsultantChrages()
        {
            DataTable ldtCons = new DataTable();
            ldtCons = mobjConChargeBLL.GetConsultantCharges();

            if (ldtCons.Rows.Count > 0 && ldtCons != null)
            {
                dgvConsultantCharge.DataSource = ldtCons;
                dgvConsultantCharge.DataBind();
                Session["ConsultantChargeDetail"] = ldtCons;
                int lintRowcount = ldtCons.Rows.Count;
                lblRowCount.Text = "<b>Total Records:</b> " + lintRowcount.ToString();
            }
            else
            {
                pnlShow.Style.Add(HtmlTextWriterStyle.Display, "none");
                hdnPanel.Value = "none";
            }

        }

        private void FillControls(DataTable ldt)
        {
            ddlEditConsultant.SelectedValue = ldt.Rows[0]["ConsultantId"].ToString();
            ddlEditWardNo.SelectedValue = ldt.Rows[0]["Ward"].ToString();
            txtEditFees.Text = ldt.Rows[0]["Charge"].ToString();
        }
        #endregion
        protected void dgvConsultantCharge_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dgvConsultantCharge.DataSource = (DataTable)Session["ConsultantChargeDetail"];
                dgvConsultantCharge.DataBind();
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmConsultantMaster - dgvConsultantCharge_PageIndexChanged(object sender, EventArgs e)", ex);
            }
        }
        protected void dgvConsultantCharge_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvConsultantCharge.PageIndex = e.NewPageIndex;
        }
        protected void dgvConsultantCharge_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            DataTable ldt = new DataTable();

            if (e.CommandName == "EditConsultant")
            {
                this.programmaticModalPopupEdit.Show();
                GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                LinkButton lnkPKId = (LinkButton)gvr.FindControl("lnkPKId");
                int lintPKId = Commons.ConvertToInt(lnkPKId.Text);
                Session["PKId"] = lintPKId;
                ldt = mobjConChargeBLL.GetChargeDetailForEdit(lintPKId);
                if (ldt.Rows.Count > 0 && ldt != null)
                {
                    FillControls(ldt);
                }
            }
        }
        protected void dgvConsultantCharge_DataBound(object sender, EventArgs e)
        {
            int lintCurrentPage = dgvConsultantCharge.PageIndex + 1;
            lblPageCount.Text = "<b>Page</b> " + lintCurrentPage.ToString() + "<b> of </b>" + dgvConsultantCharge.PageCount.ToString();
        }
        protected void dgvConsultantCharge_RowDataBound(object sender, GridViewRowEventArgs e)
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
                Commons.FileLog("frmConsultantChargeMaster -  dgvConsultantCharge_RowDataBound(object sender, GridViewRowEventArgs e)", ex);
            }
        }


    }
}