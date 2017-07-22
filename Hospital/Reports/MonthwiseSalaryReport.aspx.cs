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

namespace Hospital.Reports
{
    public partial class MonthwiseSalaryReport : System.Web.UI.Page
    {
        AllowanceDeductionBLL mobjAllowDed = new AllowanceDeductionBLL();
        List<EntityAllowanceDeduction> lst = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            

        }
        public List<STP_AllowanceDeductionNameResult> GetAllowance(string Sal_Month)
        {
            CriticareHospitalDataContext objData = new CriticareHospitalDataContext();
            List<STP_AllowanceDeductionNameResult> lst = objData.STP_AllowanceDeductionName(Sal_Month).ToList();
            return lst;
        }


        public List<STP_EmpNameResult> GetFacultyName()
        {
            CriticareHospitalDataContext objData = new CriticareHospitalDataContext();
            List<STP_EmpNameResult> lst = objData.STP_EmpName().ToList();
            return lst;
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                txtMonth.Text = string.Empty;
                lblMonth.Text = string.Empty;
                lblDate.Text = string.Empty;
                DataTable dt = new DataTable();
                dgvMonthSalary.DataSource = dt;
                dgvMonthSalary.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton imgEdit = (ImageButton)sender;
                GridViewRow row = (GridViewRow)imgEdit.NamingContainer;
                if (Convert.ToInt32(dgvMonthSalary.DataKeys[row.RowIndex].Value) == 0)
                {
                    lblMessage.Text = "Please Calculate Salary of this Employee For this Month.";
                }
                else
                {
                    Session["LedgerId"] = Convert.ToInt32(dgvMonthSalary.DataKeys[row.RowIndex].Value);
                    Session["ReportType"] = "Salary";
                    Response.Redirect("~/PathalogyReport/PathologyReport.aspx", false);
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtMonth.Text))
                {
                    lblMessage.Text = string.Empty;
                    lblMonth.Text = txtMonth.Text;
                    lblDate.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Now.Date);
                    DataColumn clm = null;
                    DataTable dt = new DataTable();
                    clm = new DataColumn() { Caption = "Sal Id ", ColumnName = "SalId" };
                    dt.Columns.Add(clm);
                    clm = new DataColumn() { Caption = "Emp Code", ColumnName = "EmpCode" };
                    dt.Columns.Add(clm);
                    clm = new DataColumn() { Caption = "Emp Name", ColumnName = "EmpName" };
                    dt.Columns.Add(clm);
                    clm = new DataColumn() { Caption = "Designation", ColumnName = "Designation" };
                    dt.Columns.Add(clm);
                    clm = new DataColumn() { Caption = "Attend Days", ColumnName = "AttendDays" };
                    dt.Columns.Add(clm);
                    clm = new DataColumn() { Caption = "OT Hours", ColumnName = "OTHours" };
                    dt.Columns.Add(clm);
                    clm = new DataColumn() { Caption = "Bank Ac/No", ColumnName = "BankAcNo" };
                    dt.Columns.Add(clm);
                    lst = mobjAllowDed.GetAllowDed();
                    foreach (var item in lst)
                    {
                        clm = new DataColumn() { Caption = item.AllowDedId.ToString(), ColumnName = item.Description };
                        dt.Columns.Add(clm);

                    }
                    clm = new DataColumn() { Caption = "NetPayment", ColumnName = "NetPayment" };
                    dt.Columns.Add(clm);
                    //DataTable dt = (DataTable)Session["ThisData"];
                    int rowindex = 0;
                    List<STP_AllowanceDeductionNameResult> lstAllowance = GetAllowance(string.Format("{0:MMM-yyyy}", txtMonth.Text));
                    List<STP_EmpNameResult> lstFaculty = GetFacultyName();
                    foreach (STP_EmpNameResult item in lstFaculty)
                    {

                        List<STP_AllowanceDeductionNameResult> Copy = (from tbl in lstAllowance
                                                                       where tbl.EmpId == item.PKId
                                                                       && tbl.Sal_Month == item.Sal_Month
                                                                       select tbl).ToList();
                        DataRow row = dt.NewRow();

                        if (Copy.Count > 0)
                        {
                            row[0] = Convert.ToString(Copy[0].SalId);
                        }
                        else
                        {
                            row[0] = "0";
                        }
                        row[1] = item.EmpCode;
                        row[2] = item.EmpName;
                        row[3] = item.DesignationDesc;
                        row[4] = item.Attend_Days;
                        row[5] = item.OTHours;
                        row[6] = "0";
                        decimal netpay = 0;

                        for (int i = 7; i < dt.Columns.Count; i++)
                        {
                            string clmName = Convert.ToString(dt.Columns[i].Caption);
                            bool flag = false;

                            foreach (STP_AllowanceDeductionNameResult result in Copy)
                            {
                                netpay = result.NetPayment;
                                if (clmName.Equals(result.AllowDedId.ToString()))
                                {
                                    row[i] = decimal.Round(result.Amount, 2);
                                    flag = true;
                                    break;
                                }
                            }
                            if (!flag)
                            {
                                row[i] = "";
                            }
                        }
                        row[dt.Columns.Count - 1] = decimal.Round(netpay, 2);
                        dt.Rows.InsertAt(row, rowindex);
                        //GridViewRow row = new GridViewRow(rowindex, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
                        rowindex++;

                    }
                    dgvMonthSalary.DataSource = dt;
                    dgvMonthSalary.DataBind();
                    Session["MonthSalReport"] = dt;
                    txtMonth.Text = string.Empty;
                    return;
                }
                else
                {
                    lblMessage.Text = "Please Fill Search Text...";
                    txtMonth.Focus();
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
                if (!string.IsNullOrEmpty(lblMonth.Text))
                {
                    Session["Details"] = Session["MonthSalReport"];
                    Response.Redirect("~/ExcelReport/MonthwiseSalExcel.aspx");
                }
                else
                {
                    lblMessage.Text = "Please Select Month";
                }
            }
            catch (Exception ex)
            {

                lblMessage.Text = ex.Message;
            }
        }
    }
}