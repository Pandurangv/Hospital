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
using System.Globalization;

namespace Hospital.StoreForms
{
    public partial class frmMaterialRequisiton : System.Web.UI.Page
    {
        MaterialRequisitionBLL mobjMaterialRequisitionBLL = new MaterialRequisitionBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //SessionManager.Instance.SetSession();
                txtStartDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtEndDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                btnSave.Enabled = false;
                BtnDelete.Enabled = false;
            }
        }


        protected void BtnAddNew_Click(object sender, EventArgs e)
        {
            DataTable ldt = new DataTable();
            ldt = mobjMaterialRequisitionBLL.GetNewRequisitionNo();
            txtRequisitionCode.Text = ldt.Rows[0]["RequisitionNo"].ToString();
            GetGroup();
            pnlGridForItem.Style.Add(HtmlTextWriterStyle.Display, "none");
            hdnPanel.Value = "none";
            this.programmaticModalPopup.Show();
        }


        public void GetAllRequisition()
        {
            DataTable ldtRequisition = new DataTable();
            ldtRequisition = mobjMaterialRequisitionBLL.GetAllRequisition();
            if (ldtRequisition.Rows.Count > 0 && ldtRequisition != null)
            {
                dgvRequisition.DataSource = ldtRequisition;
                dgvRequisition.DataBind();
                Session["requisitionDetails"] = ldtRequisition;
                PanelView.Style.Add(HtmlTextWriterStyle.Display, "");
                hdnPanel.Value = "";
            }
            else
            {
                PanelView.Style.Add(HtmlTextWriterStyle.Display, "none");
                hdnPanel.Value = "none";
            }
        }


        public void GetGroup()
        {
            DataTable ldtGroup = new DataTable();
            ldtGroup = mobjMaterialRequisitionBLL.GetGroup();

            ddlGroup.DataSource = ldtGroup;
            ddlGroup.DataValueField = "PKId";
            ddlGroup.DataTextField = "GroupDesc";
            ddlGroup.DataBind();

            ListItem li = new ListItem();
            li.Value = "0";
            li.Text = "--Select Group--";
            ddlGroup.Items.Insert(0, li);
        }


        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow drv in dgvRequisition.Rows)
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
                LinkButton RequisitionCode = (LinkButton)row.FindControl("lnkRequisitionCode");
                Session["RequisitionCode"] = RequisitionCode.Text;
                lblMessage.Text = string.Empty;
                BtnDelete.Enabled = true;
            }
            else
            {
                Session["RequisitionCode"] = string.Empty;
                BtnDelete.Enabled = false;
            }
        }


        protected void chkSelect_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chk.NamingContainer;
            programmaticModalPopup.Show();
            if (chk.Checked)
            {
                lblMessage.Text = string.Empty;
                btnSave.Enabled = true;
            }
            else
            {
                btnSave.Enabled = false;
            }
        }


        protected void BtnDeleteOk_Click(object sender, EventArgs e)
        {
            EntityMaterialRequisition entRequisition = new EntityMaterialRequisition();
            int cnt = 0;

            try
            {
                foreach (GridViewRow drv in dgvRequisition.Rows)
                {
                    CheckBox chkDelete = (CheckBox)drv.FindControl("chkDelete");
                    if (chkDelete.Checked)
                    {
                        LinkButton lnkRequisitionCode = (LinkButton)drv.FindControl("lnkRequisitionCode");
                        string lstrRequisitionCode = lnkRequisitionCode.Text;
                        entRequisition.RequisitionCode = lstrRequisitionCode;

                        cnt = mobjMaterialRequisitionBLL.DeleteRequisition(entRequisition);
                        if (cnt > 0)
                        {
                            this.modalpopupDelete.Hide();

                            Commons.ShowMessage("Record Deleted Successfully....", this.Page);

                            if (dgvRequisition.Rows.Count <= 0)
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
                GetAllRequisition();
            }
            catch (System.Threading.ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                Commons.FileLog("frmRequisitionMaster -   BtnDeleteOk_Click(object sender, EventArgs e)", ex);
            }
        }


        protected void dgvRequisition_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dgvRequisition.DataSource = (DataTable)Session["RequisitionDetail"];
                dgvRequisition.DataBind();
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmRequisitionMaster - dgvRequisition_PageIndexChanged(object sender, EventArgs e)", ex);
            }
        }


        protected void dgvRequisition_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvRequisition.PageIndex = e.NewPageIndex;
        }


        protected void dgvRequisition_RowDataBound(object sender, GridViewRowEventArgs e)
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


        protected void dgvRequisition_DataBound(object sender, EventArgs e)
        {
            int lintCurrentPage = dgvRequisition.PageIndex + 1;
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtStartDate.Text.Trim().Length <= 0 || txtEndDate.Text.Trim().Length <= 0)
                {
                    Commons.ShowMessage("Start Date and End Date Should Not be Blank.", this.Page);
                    return;
                }

                DateTime ldtStart = DateTime.ParseExact(txtStartDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime ldtEnd = DateTime.ParseExact(txtEndDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                if (ldtStart > ldtEnd)
                {
                    Commons.ShowMessage("Start Date Should Not be Greater Than End Date.", this.Page);
                    return;
                }
                string lstrRequisitionNo = txtRequisitionNo.Text.Trim();

                if (lstrRequisitionNo != string.Empty)
                {
                    ldtStart = StringExtension.ToDateTime("01/01/1753");
                    ldtEnd = StringExtension.ToDateTime("01/01/1753");
                }

                DataTable ldt = new DataTable();
                ldt = mobjMaterialRequisitionBLL.GetFilteredRequisition(ldtStart, ldtEnd, lstrRequisitionNo);

                if (ldt.Rows.Count > 0)
                {
                    dgvRequisition.DataSource = ldt;
                    dgvRequisition.DataBind();
                    PanelView.Style.Add(HtmlTextWriterStyle.Display, "");
                    hdnPanel.Value = "";
                }
                else
                {
                    PanelView.Style.Add(HtmlTextWriterStyle.Display, "none");
                    hdnPanel.Value = "none";
                    Commons.ShowMessage("No Data To Display..", this.Page);
                }
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmRequisitionMaster -  btnView_Click(object sender, EventArgs e)", ex);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            List<EntityMaterialRequisition> lstentMaterialReq = new List<EntityMaterialRequisition>();
            EntityMaterialRequisition entRequisition = new EntityMaterialRequisition();

            foreach (GridViewRow drv in dgvSaveRequisition.Rows)
            {
                CheckBox chkSelect = (CheckBox)drv.FindControl("chkSelect");
                if (chkSelect.Checked)
                {

                    if (string.IsNullOrEmpty(txtRequisitionCode.Text.Trim()))
                    {
                        Commons.ShowMessage("Enter Requisition Code", this.Page);
                    }
                    else
                    {
                        entRequisition.RequisitionCode = txtRequisitionCode.Text;
                        Label lblItemCode = (Label)drv.FindControl("lblItemCode");
                        Label lblItemDesc = (Label)drv.FindControl("lblItemDesc");
                        TextBox txtQuantity = (TextBox)drv.FindControl("txtQty");
                        Label lblUnit = (Label)drv.FindControl("lblUnit");
                        entRequisition.ItemCode = lblItemCode.Text;
                        entRequisition.Item = lblItemDesc.Text;
                        entRequisition.Qty = Convert.ToDecimal(txtQuantity.Text);
                        entRequisition.Unit = lblUnit.Text;
                        entRequisition.Group = Convert.ToInt32(ddlGroup.SelectedValue);
                        entRequisition.RequisitionStatus = 'P';
                        entRequisition.EntryBy = SessionManager.Instance.LoginUser.UserType;
                        string lstrusername = entRequisition.EntryBy.ToString();

                        if (!string.IsNullOrEmpty(lstrusername))
                        {
                            DataTable ldtEmpName = new DataTable();
                            ldtEmpName = mobjMaterialRequisitionBLL.GetEmpName(entRequisition);
                            entRequisition.RequisitionBy = ldtEmpName.Rows[0]["EmpName"].ToString();
                            lstentMaterialReq.Add(entRequisition);
                        }
                        else
                        {
                            Commons.ShowMessage("Session Expired Or Employee Not Valid..", this.Page);
                        }
                    }
                }
            }

            if (lstentMaterialReq.Count > 0)
            {
                int lintcnt = 0;
                lintcnt = mobjMaterialRequisitionBLL.InsertRequisition(lstentMaterialReq, entRequisition);
                if (lintcnt > 0)
                {
                    GetAllRequisition();
                    Commons.ShowMessage("Record Inserted Successfully..", this.Page);
                    this.programmaticModalPopup.Hide();
                }
                else
                {
                    Commons.ShowMessage("Record Not Inserted..", this.Page);
                }
            }
        }

        protected void btnShowAll_Click(object sender, EventArgs e)
        {
            GetAllRequisition();
        }


        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            EntityMaterialRequisition entRequisition = new EntityMaterialRequisition();
            DataTable ldt = new DataTable();
            if (ddlGroup.SelectedIndex > 0)
            {
                entRequisition.Group = Convert.ToInt32(ddlGroup.SelectedValue);
                ldt = mobjMaterialRequisitionBLL.GetItem(entRequisition);
                if (ldt.Rows.Count > 0)
                {
                    dgvSaveRequisition.DataSource = ldt;
                    dgvSaveRequisition.DataBind();
                    pnlGridForItem.Style.Add(HtmlTextWriterStyle.Display, "");
                    hdnPanel.Value = "";
                }
                else
                {
                    pnlGridForItem.Style.Add(HtmlTextWriterStyle.Display, "none");
                    hdnPanel.Value = "none";
                }
            }
            else
            {
                pnlGridForItem.Style.Add(HtmlTextWriterStyle.Display, "none");
                hdnPanel.Value = "none";
            }
            this.programmaticModalPopup.Show();
        }


        protected void dgvSaveRequisition_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    TextBox txtQty = (TextBox)e.Row.FindControl("txtQty");
                    CheckBox chkSelect = (CheckBox)e.Row.FindControl("chkSelect");
                    if (txtQty.Text == "0.00")
                    {
                        txtQty.Enabled = false;
                        chkSelect.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmRequisitionMaster -  dgvSaveRequisition_RowDataBound(object sender, GridViewRowEventArgs e)", ex);
            }
        }
    }
}