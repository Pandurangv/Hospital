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
using System.Drawing;
using Hospital.Models;

namespace Hospital
{
    public partial class frmShiftAllocation : BasePage
    {
        ShiftBLL mobjDeptBLL = new ShiftBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            base.AuthenticateUser();
            if (!Page.IsPostBack)
            {
                GetEmployee();
                BindShiftName();
                MultiView1.SetActiveView(View1);
            }
        }

        private void BindShiftName()
        {
            try
            {
                DataTable tblCat = new ShiftBLL().GetAllShift();
                DataRow dr = tblCat.NewRow();
                dr["ShiftId"] = 0;
                dr["ShiftName"] = "---Select---";
                tblCat.Rows.InsertAt(dr, 0);

                ddlShift.DataSource = tblCat;
                ddlShift.DataValueField = "ShiftId";
                ddlShift.DataTextField = "ShiftName";
                ddlShift.DataBind();
            }
            catch (Exception ex)
            {
                //  lblMessage.Text = ex.Message;
            }
        }
        private void GetEmployee()
        {
            List<EntityEmployee> ldtShift = mobjDeptBLL.GetAllEmpDetails();
            if (ldtShift.Count > 0)
            {
                dgvAllEmp.DataSource = ldtShift;
                dgvAllEmp.DataBind();
                int lintRowcount = ldtShift.Count;
                lblRowCount.Text = "<b>Total Records:</b> " + lintRowcount.ToString();
            }
            else
            {
                dgvAllEmp.DataSource = ldtShift;
                dgvAllEmp.DataBind();
                int lintRowcount = ldtShift.Count;
                lblRowCount.Text = "<b>Total Records:</b> " + lintRowcount.ToString();
            }
        }

        

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            ViewState["update"] = update.Value;
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;
            try
            {
                if (ddlShift.SelectedIndex == 0)
                {
                    lblMessage.Text = "Please select Shift.";
                    ddlShift.Focus();
                    return;
                }
                List<tblShiftAllocEmp> lst = new List<tblShiftAllocEmp>();
                foreach (GridViewRow item in dgvAllocEmp.Rows)
                {
                    int FacAllocationId = Convert.ToInt32(item.Cells[0].Text);
                    if (FacAllocationId > 0)
                    {
                        bool lst1 = new EmployeeBLL().GetAEmpIdOnShiftId(Convert.ToInt32(ddlShift.SelectedValue), Convert.ToInt32(item.Cells[0].Text));
                        if (!lst1)
                        {
                            lst.Add(new tblShiftAllocEmp { Shift_Id = Convert.ToInt32(ddlShift.SelectedValue), Emp_Id = Convert.ToInt32(item.Cells[0].Text), IsDelete = false });
                        }
                    }
                }
                if (lst.Count > 0)
                {
                    bool b = new EmployeeBLL().Save(lst);
                    dgvAllocEmp.DataSource = new List<tblShiftAllocEmp>();
                    dgvAllocEmp.DataBind();

                    ddlShift.SelectedIndex = 0;

                    lblMessage.Text = "Record Saved SuccessFully";
                    lblRowCount1.Text = string.Empty;
                }
                else
                {
                    lblMessage.Text = "Please Firstly allocate employee for Shift.";
                }

                GetEmployee();

            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }



        protected void chkDelete_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chk.NamingContainer;
            if (chk.Checked)
            {
                LinkButton lnkDeptCode = (LinkButton)row.FindControl("lnkDeptCode");
                DeptCode.Value = lnkDeptCode.Text;
                //lblMessage.Text = string.Empty;
            }
            else
            {
                DeptCode.Value = string.Empty;
            }
        }


        protected void dgvDepartment_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                List<EntityEmployee> ldtShift = mobjDeptBLL.GetAllEmpDetails();
                dgvAllEmp.DataSource = ldtShift;// (List<sp_GetAllBedAllocResult>)Session["DepartmentDetail"];
                dgvAllEmp.DataBind();
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmShiftAllocation - dgvDepartment_PageIndexChanged(object sender, EventArgs e)", ex);
            }
        }
        protected void dgvDepartment_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvAllEmp.PageIndex = e.NewPageIndex;
        }
        protected void dgvDepartment_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (!e.Row.Cells[0].Text.Equals("Bed Id", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (!e.Row.Cells[5].Text.Equals("&nbsp;", StringComparison.CurrentCultureIgnoreCase))
                    {
                        if (!e.Row.Cells[6].Text.Equals("&nbsp;", StringComparison.CurrentCultureIgnoreCase))
                        {
                            e.Row.Enabled = true;
                            e.Row.BackColor = Color.LightGreen;
                        }
                        else
                        {
                            e.Row.Enabled = false;
                            e.Row.BackColor = Color.LightSkyBlue;
                        }
                    }
                    else
                    {
                        e.Row.Enabled = true;
                        e.Row.BackColor = Color.LightGreen;
                    }
                }
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap; text-align:left;");
                }

            }
            catch (Exception ex)
            {
                Commons.FileLog("frmShiftAllocation -  dgvData_RowDataBound(object sender, GridViewRowEventArgs e)", ex);
            }
        }
        protected void dgvDepartment_DataBound(object sender, EventArgs e)
        {
            int lintCurrentPage = dgvAllEmp.PageIndex + 1;
            lblPageCount.Text = "<b>Page</b> " + lintCurrentPage.ToString() + "<b> of </b>" + dgvAllEmp.PageCount.ToString();
        }


        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                bool flag = false;
                bool b;
                EmployeeBLL obj1 = new EmployeeBLL();
                if (ddlShift.SelectedIndex == 0)
                {
                    lblMessage.Text = "Please Select Shift Name";
                    ddlShift.Focus();
                    return;
                }
                List<EntityFacAllocEmp> lst = new EmployeeBLL().GetAllocatedEmpToShift(Convert.ToInt32(ddlShift.SelectedValue));
                int RowCount = 0;
                int TotalRow = dgvAllEmp.Rows.Count;
                int Freq = 0;
                foreach (GridViewRow item in dgvAllEmp.Rows)
                {
                    Freq++;
                    CheckBox CheckBox = item.FindControl("chkSelect") as CheckBox;
                    if (CheckBox.Checked)
                    {
                        RowCount++;
                        lst.Add(new EntityFacAllocEmp { Emp_Id = Convert.ToInt32(item.Cells[1].Text), FullName = Convert.ToString(item.Cells[2].Text) });
                    }
                    if (Freq == TotalRow && RowCount == 0)
                    {
                        lblMessage.Text = "Please Select Employee";
                    }
                }
                if (flag)
                {
                    lblMessage.Text = "Invalid Employee Allocation..";
                    foreach (GridView item in dgvAllEmp.Rows)
                    {
                        item.Columns[0].Visible = true;//
                    }
                }
                else
                {
                    dgvAllocEmp.AutoGenerateColumns = false;
                    dgvAllocEmp.DataSource = lst;
                    dgvAllocEmp.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
        protected void ddlShift_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindAllocatedEmp();
        }

        private void BindAllocatedEmp()
        {
            try
            {
                if (ddlShift.SelectedIndex > 0)
                {
                    dgvAllocEmp.DataSource = null;
                    List<EntityFacAllocEmp> lst = new EmployeeBLL().GetAllocatedEmpToShift(Convert.ToInt32(ddlShift.SelectedValue));
                    if (lst != null)
                    {
                        dgvAllocEmp.AutoGenerateColumns = false;
                        dgvAllocEmp.DataSource = lst;
                        dgvAllocEmp.DataBind();
                        int lintRowcount1 = lst.Count;
                        lblRowCount1.Text = "<b>Total Records:</b> " + lintRowcount1.ToString();
                        pnlShow.Style.Add(HtmlTextWriterStyle.Display, "");
                        hdnPanel.Value = "";
                    }
                    else
                    {
                        dgvAllocEmp.AutoGenerateColumns = false;
                        dgvAllocEmp.DataSource = new List<EntityFacAllocEmp>();
                        dgvAllocEmp.DataBind();
                        int lintRowcount1 = lst.Count;
                        lblRowCount1.Text = "<b>Total Records:</b> " + lintRowcount1.ToString();
                        pnlShow.Style.Add(HtmlTextWriterStyle.Display, "");
                        hdnPanel.Value = "";
                    }
                }
                else
                {
                    if (ddlShift.SelectedIndex == 0)
                    {
                        ddlShift.Focus();
                        dgvAllocEmp.DataSource = new List<EntityFacAllocEmp>();
                        dgvAllocEmp.DataBind();
                    }

                }
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmShiftAllocation - BindAllocatedEmp", ex);
            }
        }
    }
}