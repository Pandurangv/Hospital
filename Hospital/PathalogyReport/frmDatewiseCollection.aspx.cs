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
    public partial class frmDatewiseCollection : System.Web.UI.Page
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
                    lblamount.Visible = false;
                    txtamount.Visible = false;
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
                SelectDatewiseCollection();
                lblMessage.Text = string.Empty;
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void btnInvoicePrint_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton imgEdit = (ImageButton)sender;
                GridViewRow row = (GridViewRow)imgEdit.NamingContainer;
                string transtype = row.Cells[5].Text;
                if (transtype.Equals("Invoice"))
                {
                    Session["BILLNo"] = Convert.ToInt32(row.Cells[2].Text);
                    Session["ReportType"] = "Invoice";
                    Response.Redirect("~/PathalogyReport/PathologyReport.aspx", false);
                }
                else
                {
                    Session["ReceiptNo"] = Convert.ToInt32(row.Cells[2].Text); ;
                    Session["ReportType"] = "Receipt";
                    Response.Redirect("~/PathalogyReport/PathologyReport.aspx", false);
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        private void SelectDatewiseCollection()
        {
            try
            {
                DatewiseCollectionBLL consume = new DatewiseCollectionBLL();
                List<STP_DatewiseCollectionResult> lst = consume.SearchDatewiseCollection(StringExtension.ToDateTime(txtBillDate.Text), StringExtension.ToDateTime(txtToDate.Text));
                if (lst != null)
                {
                    lbl.Text = "Datewise Collection Report";
                    lblFrom.Text = txtBillDate.Text;
                    lblTo.Text = txtToDate.Text;
                    DataTable dt = ListConverter.ToDataTable(lst);
                    dt.Columns.Add("colSrNo");
                    DataColumn dcol = new DataColumn();
                    if (dt.Rows.Count > 0)
                    {
                        int count = 1;
                        foreach (DataRow dr in dt.Rows)
                        {
                            dr["colSrNo"] = count;
                            count++;
                        }
                    }
                    dgvTestParameter.DataSource = dt;
                    dgvTestParameter.DataBind();
                    Session["BedConsump"] = dt;
                    lblamount.Visible = true;
                    txtamount.Visible = true;

                    decimal amt = 0;
                    foreach (GridViewRow item in dgvTestParameter.Rows)
                    {
                        if ((item.Cells[5].Text) != null)
                        {
                            if (item.Cells[5].Text == "&nbsp;")
                            {
                                item.Cells[5].Text = Convert.ToString(0);
                                amt = amt + Convert.ToDecimal(item.Cells[5].Text);
                            }
                            else
                            {
                                amt = amt + Convert.ToDecimal(item.Cells[5].Text);
                            }
                        }
                    }

                    txtamount.Text = Convert.ToString(amt);
                    //txtBillDate.Text = string.Empty;
                    //txtToDate.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(lblFrom.Text) && !string.IsNullOrEmpty(lblTo.Text))
                {
                    Session["Details"] = Session["BedConsump"];
                    Response.Redirect("~/ExcelReport/MonthwiseSalExcel.aspx");
                }
                else
                {
                    lblMessage.Text = "Please Enter Dates";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
    }
}