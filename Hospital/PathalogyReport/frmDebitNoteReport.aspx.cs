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

namespace Hospital.PathalogyReport
{
    public partial class frmDebitNoteReport : System.Web.UI.Page
    {
        DebitNoteBLL mobjDeptBLL = new DebitNoteBLL();
        PathologyBLL mobjPath = new PathologyBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (SessionManager.Instance.LoginUser != null)
            {
                if (!Page.IsPostBack)
                {
                    //SessionManager.Instance.SetSession();
                    Session["MyFlag"] = string.Empty;
                    Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                    SelectDebitNote();
                    MultiView1.SetActiveView(View1);
                }
            }
            else
            {
                Response.Redirect("~/frmlogin.aspx", false);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSearch.Text))
                {
                    SearchDebitNoteDetails(txtSearch.Text);
                }
                else
                {
                    lblMessage.Text = "Please Enter Supplier Name OR Supplier Address To Search";
                    txtSearch.Focus();
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtSearch.Text = string.Empty;
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            MultiView1.SetActiveView(View1);
        }

        protected void btnDebitNotePrint_Click(object sender, EventArgs e)
        {
            ImageButton imgEdit = (ImageButton)sender;
            GridViewRow row = (GridViewRow)imgEdit.NamingContainer;
            Session["DNNo"] = Convert.ToInt32(dgvTestParameter.DataKeys[row.RowIndex].Value);
            Session["ReportType"] = "DebitNoteReport";
            Response.Redirect("~/PathalogyReport/PathologyReport.aspx", false);
        }

        private void SearchDebitNoteDetails(string Prefix)
        {
            try
            {
                DebitNoteBLL objOPDPatient = new DebitNoteBLL();
                List<EntityDebitNote> lst = objOPDPatient.GetDebitNotes(Prefix);
                if (lst != null)
                {
                    dgvTestParameter.DataSource = lst;
                    dgvTestParameter.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        private void SelectDebitNote()
        {
            try
            {
                DebitNoteBLL DebitHist = new DebitNoteBLL();
                List<EntityDebitNote> lst = DebitHist.GetDebitNotes();
                if (lst != null)
                {
                    dgvTestParameter.DataSource = lst;
                    dgvTestParameter.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
    }
}