﻿using System;
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
    public partial class frmDailyNursingChart : System.Web.UI.Page
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
            PatientMasterBLL mobjPatient = new PatientMasterBLL();
            try
            {
                DataTable ldt = new DataTable();
                ldt = mobjPatient.GetDeptCategory();
                ddlDepartment.DataSource = ldt;
                ddlDepartment.DataValueField = "CategoryId";
                ddlDepartment.DataTextField = "CategoryName";
                ddlDepartment.DataBind();

                ListItem li = new ListItem();
                li.Text = "--Select Department--";
                li.Value = "0";
                ddlDepartment.Items.Insert(0, li);
            }
            catch (Exception ex)
            {
                // lblMsg.Text = ex.Message;
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
                BedConsumtionBLL consume = new BedConsumtionBLL();
                if (ddlDepartment.SelectedIndex == 0)
                {
                    //List<STP_DailyNursingChartResult> lst = consume.SearchDailyChart(StringExtension.ToDateTime(txtBillDate.Text), StringExtension.ToDateTime(txtToDate.Text));
                    //if (lst != null)
                    //{
                    //    lbl.Text = "Daily Nursing Chart Report";
                    //    lblFrom.Text = txtBillDate.Text;
                    //    lblTo.Text = txtToDate.Text;
                    //    DataTable dt = ListConverter.ToDataTable(lst);
                    //    dt.Columns.Add("colSrNo");
                    //    DataColumn dcol = new DataColumn();
                    //    if (dt.Rows.Count > 0)
                    //    {
                    //        int count = 1;
                    //        foreach (DataRow dr in dt.Rows)
                    //        {
                    //            dr["colSrNo"] = count;
                    //            count++;
                    //        }
                    //    }
                    //    dgvTestParameter.DataSource = dt;
                    //    dgvTestParameter.DataBind();
                    //    Session["BedConsump"] = dt;
                    //    //txtBillDate.Text = string.Empty;
                    //    //txtToDate.Text = string.Empty;
                    //}
                }
                else
                {
                    //List<STP_DailyDepartmentwiseNursingChartResult> lst = consume.SearchDepartmentwiseDailyChart(StringExtension.ToDateTime(txtBillDate.Text), StringExtension.ToDateTime(txtToDate.Text), Convert.ToString(ddlDepartment.SelectedItem.Text));
                    //if (lst != null)
                    //{
                    //    lbl.Text = "Daily Nursing Chart Report";
                    //    lblFrom.Text = txtBillDate.Text;
                    //    lblTo.Text = txtToDate.Text;
                    //    DataTable dt = ListConverter.ToDataTable(lst);
                    //    dt.Columns.Add("colSrNo");
                    //    DataColumn dcol = new DataColumn();
                    //    if (dt.Rows.Count > 0)
                    //    {
                    //        int count = 1;
                    //        foreach (DataRow dr in dt.Rows)
                    //        {
                    //            dr["colSrNo"] = count;
                    //            count++;
                    //        }
                    //    }
                    //    dgvTestParameter.DataSource = dt;
                    //    dgvTestParameter.DataBind();
                    //    Session["BedConsump"] = dt;
                        //txtBillDate.Text = string.Empty;
                        //txtToDate.Text = string.Empty;
                    //}
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtBillDate.Text = string.Empty;
            txtToDate.Text = string.Empty;
            ddlDepartment.SelectedIndex = 0;
            lblTo.Text = string.Empty;
            dgvTestParameter.DataSource = null;
            dgvTestParameter.DataBind();
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