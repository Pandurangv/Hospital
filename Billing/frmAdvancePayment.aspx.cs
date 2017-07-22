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

namespace Hospital.Billing
{
    public partial class frmAdvancePayment : System.Web.UI.Page
    {
        CustomerTransactionBLL mobjDeptBLL = new CustomerTransactionBLL();
        AdvancePaymentBLL mobjAdvanceBLL = new AdvancePaymentBLL();
        public bool flag = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            ////SessionManager.Instance.SetSession();
            if (SessionManager.Instance.LoginUser != null)
            {
                if (!Page.IsPostBack)
                {
                    //SessionManager.Instance.SetSession();
                    Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                    BindPatients();
                    GetCustTransactionList();
                    MultiView1.SetActiveView(View2);
                }
            }
            else
            {
                Response.Redirect("~/frmlogin.aspx", false);
            }
        }

        private void BindPatients()
        {
            try
            {
                List<EntityPatientMaster> tblpatient = new CustomerTransactionBLL().GetAllocatedPatientInfo();
                tblpatient.Insert(0, new EntityPatientMaster() { PatientId = 0, FullName = "----Select----" });
                ddlPatient.DataSource = tblpatient;
                ddlPatient.DataTextField = "FullName";
                ddlPatient.DataValueField = "PatientId";
                ddlPatient.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void dgvCustomer_PageIndexChanged(object sender, EventArgs e)
        {
            List<EntityCustomerTransaction> tblPatient = (List<EntityCustomerTransaction>)Session["CustomerTransaction"];
            //dgvTestInvoice.DataSource = tblPatient;
            //dgvTestInvoice.DataBind();
        }

        protected void dgvCustomerTansact(object sender, GridViewPageEventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

            //try
            //{
            //    List<EntityTestInvoice> tblPatient = new clsTestAllocation().GetTestInvoiceList(txtSearch.Text);
            //    //Session["Status"] = "Search";
            //    Session["TestInvoice"] = tblPatient;
            //    dgvTestInvoice.DataSource = tblPatient;
            //    dgvTestInvoice.DataBind();
            //}
            //catch (Exception ex)
            //{
            //    lblMessage.Text = ex.Message;
            //}
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    Session["Status"] = "Edit";
            //    lblMessage.Text = string.Empty;
            //    ImageButton imgEdit = (ImageButton)sender;
            //    GridViewRow cnt = (GridViewRow)imgEdit.NamingContainer;
            //    Session["ReceiptNo"] = Convert.ToInt32(cnt.Cells[0].Text);
            //    List<TestAllocation> lst = new clsTestAllocation().GetTestInvoiceDetails(Convert.ToInt32(cnt.Cells[0].Text));
            //    if (cnt != null)
            //    {
            //        ListItem item = ddlPatient.Items.FindByText(Convert.ToString(cnt.Cells[2].Text));
            //        ddlPatient.SelectedValue = item.Value;
            //        txtAllocDate.Text = string.Format("{0:dd/MM/yyyy}", StringExtension.ToDateTime(cnt.Cells[1].Text));
            //        txtNetAmount.Text = Convert.ToString(cnt.Cells[4].Text);
            //        if (lst.Count > 0)
            //        {
            //            txtDiscountAmt.Text = Convert.ToString(lst[0].DiscountAmount);
            //            txtTotal.Text = Convert.ToString(Convert.ToDecimal(txtNetAmount.Text) + Convert.ToDecimal(txtDiscountAmt.Text));
            //            txtDiscount.Text = string.Format("{0:}", Convert.ToDecimal(txtDiscountAmt.Text) / Convert.ToDecimal(txtTotal.Text) * 100);
            //        }
            //        if (lst.Rows.Count > 0)
            //        {
            //            if (Convert.ToBoolean(ldt.Rows[0]["IsICU"]))
            //            {
            //                IsCash.Checked = true;
            //            }
            //            else
            //            {
            //                rdoICU.Checked = false;
            //            }
            //            if (Convert.ToBoolean(ldt.Rows[0]["IsOT"]))
            //            {
            //                rdoOT.Checked = true;
            //            }
            //            else
            //            {
            //                rdoOT.Checked = false;
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    lblMessage.Text = ex.Message;
            //}
            //BtnUpdate.Visible = true;
            //BtnSave.Visible = false;
            //MultiView1.SetActiveView(View1);
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            ImageButton imgEdit = (ImageButton)sender;
            GridViewRow row = (GridViewRow)imgEdit.NamingContainer;
            //Session["ReceiptNo"] = Convert.ToInt32(dgvCustTransaction.DataKeys[row.RowIndex].Value);
            //Session["ReportType"] = "Refund";
            //Response.Redirect("~/PathalogyReport/PathologyReport.aspx", false);
            Response.Redirect("~/PathalogyReport/PathologyReport.aspx?ReportType=Refund&ReceiptNo=" + dgvCustTransaction.DataKeys[row.RowIndex].Value, false);
        }

        protected void BtnClose_Click(object sender, EventArgs e)
        {
            MultiView1.SetActiveView(View2);
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                ddlPatient.SelectedIndex = 0;
                txtTransactDate.Enabled = false;
                txtTransactDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
                txtTotal.Text = string.Empty;
                txtChequeNo.Text = string.Empty;
                txtChequeDate.Text = string.Empty;
                txtBankName.Text = string.Empty;
                BtnUpdate.Visible = false;
                BtnSave.Visible = true;
                lblMessage.Text = string.Empty;
                MultiView1.SetActiveView(View1);

                CheckList(false);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            GetCustTransactionList();
        }

        private void GetCustTransactionList()
        {
            try
            {
                List<EntityCustomerTransaction> tblPatient = new AdvancePaymentBLL().GetCustomerTransactionList();
                if (tblPatient.Count > 0)
                {
                    dgvCustTransaction.DataSource = tblPatient;
                    dgvCustTransaction.DataBind();
                    Session["DepartmentDetail"] = tblPatient;
                    int lintRowcount = tblPatient.Count;
                    lblRowCount1.Text = "<b>Total Records:</b> " + lintRowcount.ToString();
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            ViewState["update"] = Session["update"];
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlPatient.SelectedIndex == 0)
                {
                    lblMsg.Text = "Please Select Patient Name";
                    ddlPatient.Focus();
                    return;
                }
                else
                {
                    if ((Convert.ToDecimal(txtPayAmount.Text).CompareTo(Convert.ToDecimal(txtTotal.Text)) == 1) || (Convert.ToDecimal(txtPayAmount.Text).CompareTo(Convert.ToDecimal(txtTotal.Text)) == -1))
                    {
                        lblMsg.Text = "Please Enter Proper Full Amount";
                        txtPayAmount.Text = string.Empty;
                        txtPayAmount.Focus();
                        return;
                    }
                    else
                    {
                        if (IsCheque.Checked)
                        {
                            if (string.IsNullOrEmpty(txtChequeNo.Text))
                            {
                                lblMsg.Text = "Please Enter Cheque Number";
                                txtChequeNo.Focus();
                                return;
                            }
                            else if (string.IsNullOrEmpty(txtChequeDate.Text))
                            {
                                lblMsg.Text = "Please Enter Cheque Date";
                                txtChequeDate.Focus();
                                return;
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(txtBankName.Text))
                                {
                                    lblMsg.Text = "Please Enter Bank Name";
                                    txtBankName.Focus();
                                    return;
                                }
                            }
                        }
                        else
                        {
                            List<EntityCustomerTransaction> lst = new List<EntityCustomerTransaction>();
                            EntityCustomerTransaction entcust = new EntityCustomerTransaction();
                            entcust.PatientId = Convert.ToInt32(ddlPatient.SelectedValue);
                            entcust.EmpName = Convert.ToString(Session["AdminName"]);
                            entcust.ReceiptDate = StringExtension.ToDateTime(txtTransactDate.Text);
                            entcust.BillRefNo = Convert.ToString(txtBillRefNo.Text);
                            entcust.PayAmount = Convert.ToDecimal(0);
                            if (IsCash.Checked)
                            {
                                entcust.IsCash = true;
                                entcust.ISCheque = false;
                                entcust.IsCard = false;
                                entcust.IsRTGS = false;
                                entcust.BillAmount = Convert.ToDecimal(txtTotal.Text);
                            }
                            if (IsCheque.Checked)
                            {
                                entcust.ChequeDate = StringExtension.ToDateTime(txtChequeDate.Text);
                                entcust.ChequeNo = Convert.ToString(txtChequeNo.Text);
                                entcust.BankName = Convert.ToString(txtBankName.Text);
                                entcust.BillAmount = Convert.ToDecimal(txtTotal.Text);
                            }
                            if (IsCard.Checked)
                            {
                                entcust.IsCash = false;
                                entcust.ISCheque = false;
                                entcust.IsCard = true;
                                entcust.IsRTGS = false;
                                entcust.BankRefNo = Convert.ToString(txtBankRefNo.Text);
                                entcust.BillAmount = Convert.ToDecimal(txtTotal.Text);
                            }
                            if (IsRTGS.Checked)
                            {
                                entcust.IsCash = false;
                                entcust.ISCheque = false;
                                entcust.IsCard = false;
                                entcust.IsRTGS = true;
                                entcust.BankRefNo = Convert.ToString(txtBankRefNo.Text);
                                entcust.BillAmount = Convert.ToDecimal(txtTotal.Text);
                            }
                            int i = new AdvancePaymentBLL().Save(entcust, IsCash.Checked);

                            if (i > 0)
                            {
                                lblMessage.Text = "Record Saved Successfully";
                            }
                            else
                            {
                                lblMessage.Text = "Record Not Saved";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
            GetCustTransactionList();
            MultiView1.SetActiveView(View2);
        }

        protected void dgvAllTests_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType != DataControlRowType.Header || e.Row.RowType != DataControlRowType.Footer)
                {
                    CheckBox chk = (CheckBox)e.Row.FindControl("chkSelect");
                    chk.Attributes.Add("onclick", "CalculateSum()");
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    List<EntityCustomerTransaction> lst = new List<EntityCustomerTransaction>();
            //    tblCustomerTransaction obj = new tblCustomerTransaction();
            //    obj.ReceiptAmount = Convert.ToDecimal(txtNetAmount.Text);
            //    obj.ReceiptNo = Convert.ToInt32(Session["ReceiptNo"]);
            //    obj.Discount = Convert.ToDecimal(txtDiscountAmt.Text);
            //    obj.PatientId = Convert.ToInt32(ddlPatient.SelectedValue);
            //    string[] arr = txtTransactDate.Text.Split('/');
            //    obj.TestInvoiceDate = new DateTime(Convert.ToInt32(arr[arr.Length - 1]), Convert.ToInt32(arr[0]), Convert.ToInt32(arr[1]));
            //    foreach (GridViewRow item in dgvAllTests.Rows)
            //    {
            //        CheckBox chk = (CheckBox)item.FindControl("chkSelect");
            //        if (chk.Checked)
            //        {
            //            lst.Add(new TestAllocation() { IsTestDone = false, TestId = Convert.ToInt32(dgvAllTests.DataKeys[item.RowIndex].Value), Charges = Convert.ToDecimal(item.Cells[2].Text) });
            //        }
            //    }
            //    int i = new clsTestAllocation().Update(lst, obj);
            //    GetInvoiceList();
            //}
            //catch (Exception ex)
            //{
            //    lblMessage.Text = ex.Message;
            //}
            //MultiView1.SetActiveView(View2);
        }
        protected void IsCash_CheckedChanged(object sender, EventArgs e)
        {
            if (IsCash.Checked)
            {
                CheckList(false);
            }
            if (IsCheque.Checked)
            {
                CheckList(true);
                lblBankRefNo.Visible = false;
                txtBankRefNo.Visible = false;
            }
            if (IsCard.Checked)
            {
                CheckList(false);
                lblBankRefNo.Visible = true;
                txtBankRefNo.Visible = true;
            }
            if (IsRTGS.Checked)
            {
                CheckList(false);
                lblBankRefNo.Visible = true;
                txtBankRefNo.Visible = true;
            }
        }

        private void CheckList(bool flag)
        {
            lblCashAmount.Visible = true;
            txtPayAmount.Visible = true;
            lblChequeDate.Visible = flag;
            lblchequeNo.Visible = flag;
            lblBankName.Visible = flag;
            txtChequeDate.Visible = flag;
            txtChequeNo.Visible = flag;
            txtBankName.Visible = flag;
            lblBankRefNo.Visible = flag;
            txtBankRefNo.Visible = flag;
        }

        protected void dgvCustTransact_DataBound(object sender, EventArgs e)
        {

        }

        protected void ddlPatient_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlPatient.SelectedIndex > 0)
                {
                    //Session["pat_id"] = Convert.ToInt32(ddlPatient.SelectedValue);
                    List<tblCustomerTransaction> lst = new CustomerTransactionBLL().GetPatientTransByPatientAdmitId(Convert.ToInt32(ddlPatient.SelectedValue));
                    decimal DR = Convert.ToDecimal(lst.Sum(p => p.BillAmount));
                    decimal CR = Convert.ToDecimal(lst.Sum(p => p.PayAmount)) + Convert.ToDecimal(lst.Sum(p => p.AdvanceAmount));
                    if (CR > DR)
                    {
                        txtTotal.Text = Convert.ToString(CR - DR);
                    }
                    else
                    {
                        txtTotal.Text = Convert.ToString(DR - CR);
                    }
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void dgvCustTransaction_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}