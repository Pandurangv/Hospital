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
using System.IO;

namespace Hospital
{
    public partial class frmViewDocument : System.Web.UI.Page
    {
        DocumentBLL mobjDocBLL = new DocumentBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (SessionManager.Instance.LoginUser != null)
            {
                if (!Page.IsPostBack)
                {
                    //SessionManager.Instance.SetSession();
                    GetDocuments();
                }
            }
            else
            {
                Response.Redirect("frmHomePage.aspx", false);
            }
        }

        protected override void OnPreInit(EventArgs e)
        {
            try
            {
                if (SessionManager.Instance.LoginUser.UserType == "M")
                    this.MasterPageFile = "~/mstMarketing.master";
                else if (SessionManager.Instance.LoginUser.UserType == "A")
                    this.MasterPageFile = "~/mstAdmin.master";
                base.OnPreInit(e);
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmViewDocument - OnPreInit(EventArgs e)", ex);
            }
        }

        protected void dgvCustomerDetail_DataBound(object sender, EventArgs e)
        {
            int lintCurrentPage = dgvCustomerDetail.PageIndex + 1;
            lblPageCount.Text = "<b>Page</b> " + lintCurrentPage.ToString() + "<b> of </b>" + dgvCustomerDetail.PageCount.ToString();
        }

        protected void dgvCustomerDetail_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dgvCustomerDetail.DataSource = (DataTable)Session["DocumentDetail"];
                dgvCustomerDetail.DataBind();
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmViewMarketingStatus - dgvCustomerDetail_PageIndexChanged(object sender, EventArgs e)", ex);
            }
        }

        protected void dgvCustomerDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvCustomerDetail.PageIndex = e.NewPageIndex;
        }

        protected void dgvCustomerDetail_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                DataTable ldt = new DataTable();
                GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;

                if (e.CommandName == "DownloadFile")
                {
                    Label lblPKId = (Label)gvr.FindControl("lblPKId");
                    Label lblDocument = (Label)gvr.FindControl("lblDocumentName");

                    int lintPKId = Commons.ConvertToInt(lblPKId.Text);
                    string lstrFullName = lblDocument.Text;
                    ldt = mobjDocBLL.GetDocumentByName(lintPKId, lstrFullName);

                    if (ldt.Rows.Count > 0 && ldt != null)
                    {
                        Response.Clear();
                        Byte[] sBytes = (Byte[])ldt.Rows[0]["FileContent"];
                        MemoryStream ms = new MemoryStream(sBytes);
                        Response.Charset = "";
                        Response.ContentType = "application";
                        Response.AddHeader("content-disposition", "attachment;filename=Document");
                        Response.Buffer = true;
                        ms.WriteTo(Response.OutputStream);
                        Response.BinaryWrite(sBytes);
                        Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        Response.End();
                    }
                }
            }
            catch (System.Threading.ThreadAbortException)
            {

            }
            //catch (Exception ex)
            //{
            //    Commons.FileLog("frmViewManualQuotation -  dgvCustomerDetail_RowCommand(object sender, GridViewCommandEventArgs e)", ex);
            //}
        }
        protected void dgvCustomerDetail_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int lintCurrentPage = dgvCustomerDetail.PageIndex + 1;
            lblPageCount.Text = "<b>Page</b> " + lintCurrentPage.ToString() + "<b> of </b>" + dgvCustomerDetail.PageCount.ToString();
        }

        private void GetDocuments()
        {
            DataTable dt = new DataTable();
            dt = mobjDocBLL.GetDocuments();

            Session["DocumentDetail"] = dt;
            if (dt != null && dt.Rows.Count > 0)
            {
                dgvCustomerDetail.PageIndex = 0;
                dgvCustomerDetail.DataSource = dt;
                dgvCustomerDetail.DataBind();
                pnlShow.Style.Add(HtmlTextWriterStyle.Display, "");
                hdnPanel.Value = "";
                int lintRowcount = dt.Rows.Count;
                lblRowCount.Text = "<b>Total Records:</b> " + lintRowcount.ToString();
            }
            else
            {
                pnlShow.Style.Add(HtmlTextWriterStyle.Display, "none");
                hdnPanel.Value = "none";
                Commons.ShowMessage("No Data to Display.", this.Page);
            }

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSearch.Text))
                {
                    SelectPatient(txtSearch.Text);
                }
                else
                {
                    //lblMessage.Text = "Please fill search text.";
                    txtSearch.Focus();
                }

            }
            catch (Exception ex)
            {
                //lblMessage.Text = ex.Message;
            }
        }

        private void SelectPatient(string Prefix)
        {
            DocumentBLL objPatient = new DocumentBLL();
            List<EntityDocument> lst = objPatient.SearchPatient(Prefix);
            if (lst != null)
            {
                dgvCustomerDetail.DataSource = lst;
                dgvCustomerDetail.DataBind();

                lblRowCount.Text = string.Empty;
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            dgvCustomerDetail.PageIndex = 0;
            GetDocuments();
            txtSearch.Text = string.Empty;
        }
    }
}