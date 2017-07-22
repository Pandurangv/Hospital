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
    public partial class frmDatewiseConsultDoctorReport : System.Web.UI.Page
    {
        PatientInvoiceBLL mobjDeptBLL = new PatientInvoiceBLL();
        EmployeeBLL mobjEmployee = new EmployeeBLL();
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
                    txtPayable.Visible = false;
                    lblPayable.Visible = false;
                    lblRecoveryAmt.Visible = false;
                    txtRecovery.Visible = false;
                    txtRecAmt.Visible = false;
                    GetDeptCategory();
                }
            }
            else
            {
                Response.Redirect("~/frmlogin.aspx", false);
            }
        }

        public void GetDeptCategory()
        {
            List<EntityEmployee> lst = mobjEmployee.SelectAllEmployee().Where(p => p.DesignationId == SettingsManager.Instance.DoctorDesigId || p.DesignationId == SettingsManager.Instance.VisitingDoctorDesigId).ToList();
            try
            {
                lst.Insert(0, new EntityEmployee() {PKId=0,FullName="---Select----" });
                ddlDeptDoctor.DataSource = lst;
                ddlDeptDoctor.DataTextField = "FullName";
                ddlDeptDoctor.DataValueField = "PKId";
                ddlDeptDoctor.DataBind();
            }
            catch (Exception ex)
            {
                //lblMsg.Text = ex.Message;
            }
        }

        //protected void ddlDeptCategory_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        PatientMasterBLL mobjPatient = new PatientMasterBLL();
        //        int linReligionId = Convert.ToInt32(ddlDeptCategory.SelectedValue);
        //        DataTable ldt = new DataTable();
        //        ldt = mobjPatient.GetDeptVisitDoctors(linReligionId);
        //        if (ldt.Rows.Count > 0 && ldt != null)
        //        {
        //            ddlDeptDoctor.DataSource = ldt;
        //            ddlDeptDoctor.DataValueField = "DocAllocId";
        //            ddlDeptDoctor.DataTextField = "EmpName";
        //            ddlDeptDoctor.DataBind();

        //            FillDeptDoctorCast();
        //            //ddlDeptDoctor.Enabled = true;
        //        }
        //        else
        //        {
        //            //ddlDeptDoctor.Enabled = false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //lblMsg.Text = ex.Message;
        //    }

        //}

        private void FillDeptDoctorCast()
        {
            try
            {
                ListItem li = new ListItem();
                li.Text = "--Select Dept.Doctor--";
                li.Value = "0";
                ddlDeptDoctor.Items.Insert(0, li);
            }
            catch (Exception ex)
            {
                //lblMsg.Text = ex.Message;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                SelectDatewiseConsultDoctorReport();
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

        protected void txtRecovery_TextChanged(object sender, EventArgs e)
        {
            txtPayable.Text = Convert.ToString(Convert.ToDecimal(txtamount.Text) - ((Convert.ToDecimal(txtRecovery.Text) * Convert.ToDecimal(txtamount.Text)) / 100));
            txtRecAmt.Text = Convert.ToString(Convert.ToDecimal(txtRecovery.Text) * Convert.ToDecimal(txtamount.Text) / 100);
        }

        protected void txtRecAmt_TextChanged(object sender, EventArgs e)
        {
            txtPayable.Text = Convert.ToString(Convert.ToDecimal(txtamount.Text) - Convert.ToDecimal(txtRecAmt.Text));
        }

        private void SelectDatewiseConsultDoctorReport()
        {
            try
            {
                DatewiseCollectionBLL consume = new DatewiseCollectionBLL();
                EntityEmployee emp = mobjEmployee.SelectAllEmployee().Where(p => p.PKId == Convert.ToInt32(ddlDeptDoctor.SelectedValue)).FirstOrDefault();// || p.DesignationId == SettingsManager.Instance.VisitingDoctorDesigId).ToList();
                if (ddlDeptDoctor.SelectedIndex != 0)
                {
                    List<STP_DatewiseConsultDoctorResult> lst = consume.SearchDatewiseConsultDoctor(StringExtension.ToDateTime(txtBillDate.Text), StringExtension.ToDateTime(txtToDate.Text),
                        emp.DesignationId.Value, Convert.ToInt32(ddlDeptDoctor.SelectedValue));
                    if (lst != null)
                    {
                        lbl.Text = "Datewise Consulting Doctor Management";
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
                        lblRecoveryAmt.Visible = true;
                        txtRecovery.Visible = true;
                        lblPayable.Visible = true;
                        txtPayable.Visible = true;
                        txtRecAmt.Visible = true;
                        decimal amt = 0;
                        foreach (GridViewRow item in dgvTestParameter.Rows)
                        {
                            if (item.Cells[4].Text == "&nbsp;")
                            {
                                item.Cells[4].Text = Convert.ToString(0);
                                amt = amt + Convert.ToDecimal(item.Cells[4].Text);
                            }
                            else
                            {
                                amt = amt + Convert.ToDecimal(item.Cells[4].Text);
                            }
                        }
                        txtamount.Text = Convert.ToString(amt);
                        txtRecovery.Text = Convert.ToString(0);
                        txtPayable.Text = Convert.ToString(amt);
                        //txtBillDate.Text = string.Empty;
                        //txtToDate.Text = string.Empty;
                    }
                }
                if (ddlDeptDoctor.SelectedIndex == 0)
                {
                    List<STP_DatewiseConsultDoctorCatResult> lst = consume.SearchDatewiseConsultDoctorCat(StringExtension.ToDateTime(txtBillDate.Text), StringExtension.ToDateTime(txtToDate.Text),emp.DeptId);
                    if (lst != null)
                    {
                        lbl.Text = "Datewise Consulting Doctor Management";
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
                        lblRecoveryAmt.Visible = true;
                        txtRecovery.Visible = true;
                        lblPayable.Visible = true;
                        txtPayable.Visible = true;
                        txtRecAmt.Visible = true;
                        decimal amt = 0;
                        foreach (GridViewRow item in dgvTestParameter.Rows)
                        {
                            if (item.Cells[4].Text == "&nbsp;")
                            {
                                item.Cells[4].Text = Convert.ToString(0);
                                amt = amt + Convert.ToDecimal(item.Cells[4].Text);
                            }
                            else
                            {
                                amt = amt + Convert.ToDecimal(item.Cells[4].Text);
                            }
                        }
                        txtamount.Text = Convert.ToString(amt);
                        txtRecovery.Text = Convert.ToString(0);
                        txtPayable.Text = Convert.ToString(amt);
                        //txtBillDate.Text = string.Empty;
                        //txtToDate.Text = string.Empty;
                    }
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