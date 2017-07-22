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

namespace Hospital.StoreForms
{
    public partial class frmRequisitionDetails : System.Web.UI.Page
    {
        RequisitionDetailsBLL mobjRequisitionBLL = new RequisitionDetailsBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //SessionManager.Instance.SetSession();
                BtnSave.Enabled = false;
                GetRequisition();
            }
        }


        public void GetRequisition()
        {
            DataTable ldtRequisition = new DataTable();
            ldtRequisition = mobjRequisitionBLL.GetAllRequisitionDetails();
            if (ldtRequisition.Rows.Count > 0 && ldtRequisition != null)
            {
                dgvRequisitionDetails.DataSource = ldtRequisition;
                dgvRequisitionDetails.DataBind();
                Session["RequisitionDetails"] = ldtRequisition;
                int lintRowcount = ldtRequisition.Rows.Count;
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
            List<EntityMaterialRequisition> lstentMaterialReq = new List<EntityMaterialRequisition>();
            EntityMaterialRequisition entRequisition = new EntityMaterialRequisition();

            foreach (GridViewRow drv in dgvSaveRequisition.Rows)
            {
                CheckBox chkSelect = (CheckBox)drv.FindControl("chkSelect");
                if (chkSelect.Checked)
                {

                    if (string.IsNullOrEmpty(lblRequisitionCode.Text.Trim()))
                    {
                        Commons.ShowMessage("Enter Requisition Code", this.Page);
                    }
                    else
                    {
                        entRequisition.RequisitionCode = lblRequisitionCode.Text.Trim();
                        Label lblItemCode = (Label)drv.FindControl("lblItemCode");
                        TextBox txtQuantity = (TextBox)drv.FindControl("txtQuantity");
                        entRequisition.ItemCode = lblItemCode.Text;
                        entRequisition.Qty = Convert.ToDecimal(txtQuantity.Text);
                        entRequisition.RequisitionStatus = 'c';
                        lstentMaterialReq.Add(entRequisition);
                    }
                }
            }

            if (lstentMaterialReq.Count > 0)
            {
                lintcnt = mobjRequisitionBLL.UpdateRequisition(lstentMaterialReq, entRequisition);
                if (lintcnt > 0)
                {
                    GetRequisition();
                    Commons.ShowMessage("Record Inserted Successfully", this.Page);
                    this.programmaticModalPopup.Hide();
                }
                else
                {
                    Commons.ShowMessage("Record Not Inserted", this.Page);
                }
            }
        }


        protected void dgvRequisitionDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                DataTable ldt = new DataTable();
                if (e.CommandName == "EditRequisition")
                {
                    int linIndex = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                    LinkButton lnkRequisitionCode = (LinkButton)gvr.FindControl("lnkRequisitionCode");
                    string lstrRequisitionCode = lnkRequisitionCode.Text;
                    lblRequisitionCode.Text = lstrRequisitionCode;
                    ldt = mobjRequisitionBLL.GetRequisitionForEdit(lstrRequisitionCode);
                    FillControls(ldt);
                    if (!String.IsNullOrEmpty(lblRequisitionCode.Text) && !string.IsNullOrEmpty(lblRequisitionBy.Text))
                    {
                        DataTable ldtRequisition = new DataTable();

                        dgvSaveRequisition.DataSource = ldt;
                        dgvSaveRequisition.DataBind();
                        Session["RequisitionDetails"] = ldt;
                        int lintRowcount = ldtRequisition.Rows.Count;
                        lblRowCount.Text = "<b>Total Records:</b> " + lintRowcount.ToString();
                        pnlShow.Style.Add(HtmlTextWriterStyle.Display, "");
                        hdnPanel.Value = "";
                    }
                    programmaticModalPopup.Show();
                }
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmRequisitionDetails -  dgvRequisitionDetails_RowCommand(object sender, GridViewCommandEventArgs e)", ex);
            }
        }



        private void FillControls(DataTable ldt)
        {
            programmaticModalPopup.Show();
            lblRequisitionCode.Text = ldt.Rows[0]["RequisitionCode"].ToString();
            lblRequisitionBy.Text = ldt.Rows[0]["RequisitionBy"].ToString();


        }



        protected void chkSelect_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chk.NamingContainer;
            if (chk.Checked)
            {
                LinkButton lnkRequisitionCode = (LinkButton)row.FindControl("lnkRequisitionCode");
                Session["RequisitionCode"] = lnkRequisitionCode.Text;
                lblMessage.Text = string.Empty;
                BtnSave.Enabled = true;
                programmaticModalPopup.Show();
            }
            else
            {
                Session["RequisitionCode"] = string.Empty;
                BtnSave.Enabled = false;
            }
        }



        protected void dgvRequisitionDetails_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dgvRequisitionDetails.DataSource = (DataTable)Session["RequisitionDetail"];
                dgvRequisitionDetails.DataBind();
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmRequisitionMaster - dgvRequisition_PageIndexChanged(object sender, EventArgs e)", ex);
            }
        }


        protected void dgvRequisitionDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvRequisitionDetails.PageIndex = e.NewPageIndex;
        }


        protected void dgvRequisitionDetails_RowDataBound(object sender, GridViewRowEventArgs e)
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
                Commons.FileLog("frmRequisitionMaster -  dgvRequisition_RowDataBound(object sender, GridViewRowEventArgs e)", ex);
            }
        }


        protected void dgvRequisitionDetails_DataBound(object sender, EventArgs e)
        {
            int lintCurrentPage = dgvRequisitionDetails.PageIndex + 1;
            lblPageCount.Text = "<b>Page</b> " + lintCurrentPage.ToString() + "<b> of </b>" + dgvRequisitionDetails.PageCount.ToString();
        }
    }
}