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

namespace Hospital.PathalogyReport
{
    public partial class frmPatientwiseDocumentSearch : System.Web.UI.Page
    {
        PatientInvoiceBLL mobjDeptBLL = new PatientInvoiceBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (SessionManager.Instance.LoginUser != null)
            {
                if (!Page.IsPostBack)
                {
                    //SessionManager.Instance.SetSession();
                    Session["MyFlag"] = string.Empty;
                    Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                    BindPatientList();
                }
            }
            else
            {
                Response.Redirect("~/frmlogin.aspx", false);
            }
        }

        public void BindPatientList()
        {
            try
            {
                DataTable tblCat = new NurseBLL().GetAllIPDPatient();
                DataRow dr = tblCat.NewRow();
                dr["PKId"] = 0;
                dr["FullName"] = "---Select---";
                tblCat.Rows.InsertAt(dr, 0);
                ddlPatient.DataSource = tblCat;
                ddlPatient.DataValueField = "PKId";
                ddlPatient.DataTextField = "FullName";
                ddlPatient.DataBind();
            }
            catch (Exception ex)
            {
                //lblMessage.Text = ex.Message;
            }
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                SelectBedConsumtion();
                lblMessage.Text = string.Empty;
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        private void SelectBedConsumtion()
        {
            try
            {
                DocumentBLL consume = new DocumentBLL();

                if (ddlPatient.SelectedIndex > 0)
                {
                    List<STP_PatientwiseDocumentDetailResult> lst = consume.PatientwiseDocDetails(Convert.ToInt32(ddlPatient.SelectedValue));
                    if (lst != null)
                    {
                        lbl.Text = "Patientwise Document Details";

                        dgvTestParameter.DataSource = lst;
                        dgvTestParameter.DataBind();
                        Session["BedConsump"] = ListConverter.ToDataTable(lst);
                    }
                }

            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void dgvTestParameter_DataBound(object sender, EventArgs e)
        {
            int lintCurrentPage = dgvTestParameter.PageIndex + 1;
        }

        protected void dgvTestParameter_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dgvTestParameter.DataSource = (DataTable)Session["BedConsump"];
                dgvTestParameter.DataBind();
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmViewMarketingStatus - dgvCustomerDetail_PageIndexChanged(object sender, EventArgs e)", ex);
            }
        }

        protected void dgvTestParameter_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvTestParameter.PageIndex = e.NewPageIndex;
        }

        protected void dgvTestParameter_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int lintCurrentPage = dgvTestParameter.PageIndex + 1;
        }
        protected void dgvTestParameter_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                DataTable ldt = new DataTable();
                DocumentBLL mobjDocBLL = new DocumentBLL();
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
                        Response.ContentType = "application/pdf";
                        Response.AddHeader("content-disposition", "attachment;filename=DocumentFile.pdf");
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
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ddlPatient.SelectedIndex = 0;
            dgvTestParameter.DataSource = null;
            dgvTestParameter.DataBind();
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                Session["Details"] = Session["BedConsump"];
                Response.Redirect("~/ExcelReport/MonthwiseSalExcel.aspx");
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
    }
}