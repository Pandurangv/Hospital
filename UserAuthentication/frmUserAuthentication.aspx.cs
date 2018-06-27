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
using Hospital.Models;

namespace Hospital.UserAuthentication
{
    public partial class frmUserAuthentication : BasePage
    {
        UserAuthenticationBLL mobjDeptBLL = new UserAuthenticationBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            base.AuthenticateUser();
            if (!Page.IsPostBack)
            {
                GetForms();
                BindDesignation();
                MultiView1.SetActiveView(View1);
            }
        }

        private void GetForms()
        {
            try
            {
                List<EntityFormMaster> ldtShift = mobjDeptBLL.GetAllForms();
                if (ldtShift.Count > 0)
                {
                    dgvAllForms.DataSource = ldtShift;
                    dgvAllForms.DataBind();
                    int lintRowcount = ldtShift.Count;
                    lblRowCount.Text = "<b>Total Records:</b> " + lintRowcount.ToString();
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }

        }

        private void BindDesignation()
        {
            try
            {
                List<EntityDesignation> lstPat = mobjDeptBLL.GetDesignation();
                ddlDesignation.DataSource = lstPat;
                lstPat.Insert(0, new EntityDesignation() { PKID = 0, DesignationDesc = "--Select--" });
                ddlDesignation.DataValueField = "PKID";
                ddlDesignation.DataTextField = "DesignationDesc";
                ddlDesignation.DataBind();
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
                if (ddlDesignation.SelectedIndex == 0)
                {
                    lblMessage.Text = "Please Select Designation";
                    ddlDesignation.Focus();
                    return;
                }
                else
                {
                    if (ddlEmployee.SelectedIndex == 0)
                    {
                        lblMessage.Text = "Please select Shift.";
                        ddlEmployee.Focus();
                        return;
                    }
                    else
                    {
                        List<tblUserAuthorization> lstUser = new List<tblUserAuthorization>();
                        List<tblShiftAllocEmp> lst = new List<tblShiftAllocEmp>();//
                        foreach (GridViewRow item in dgvAllocEmp.Rows)
                        {
                            int FormAllocId = Convert.ToInt32(item.Cells[0].Text);
                            if (FormAllocId > 0)
                            {
                                bool lstAlloc = new UserAuthenticationBLL().GetAllocFormOnEmp(Convert.ToInt32(ddlEmployee.SelectedValue), Convert.ToInt32(item.Cells[0].Text));
                                bool lst1 = new EmployeeBLL().GetAEmpIdOnShiftId(Convert.ToInt32(ddlEmployee.SelectedValue), Convert.ToInt32(item.Cells[0].Text));//
                                if (!lstAlloc)
                                {
                                    lstUser.Add(new tblUserAuthorization { EmpId = Convert.ToInt32(ddlEmployee.SelectedValue), FormId = Convert.ToInt32(item.Cells[0].Text), IsDelete = false });
                                }
                            }
                        }
                        if (lstUser.Count > 0)
                        {
                            bool b = new UserAuthenticationBLL().Save(lstUser);
                            dgvAllocEmp.DataSource = new List<tblUserAuthorization>();
                            dgvAllocEmp.DataBind();

                            ddlEmployee.SelectedIndex = 0;
                            ddlDesignation.SelectedIndex = 0;

                            lblMessage.Text = "Record Saved SuccessFully";
                            lblRowCount1.Text = string.Empty;
                        }
                        else
                        {
                            lblMessage.Text = "Record Not Saved";
                        }
                    }
                }
                GetForms();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        void frmDepartmentMaster_Load(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void FillControls(DataTable ldt)
        {
        }

        protected void chkDelete_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chk.NamingContainer;
            if (chk.Checked)
            {
                LinkButton lnkDeptCode = (LinkButton)row.FindControl("lnkDeptCode");
                DeptCode.Value = lnkDeptCode.Text;
            }
            else
            {
                DeptCode.Value = string.Empty;
            }
        }

        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                bool flag = false;
                bool b;
                EmployeeBLL obj1 = new EmployeeBLL();
                if (ddlDesignation.SelectedIndex == 0)
                {
                    lblMessage.Text = "Please Select Designation";
                    ddlDesignation.Focus();
                    return;
                }
                else
                {
                    if (ddlEmployee.SelectedIndex == 0)
                    {
                        lblMessage.Text = "Please Select Employee";
                        ddlEmployee.Focus();
                        return;
                    }
                    else
                    {
                        List<EntityFormMaster> lstForm = new UserAuthenticationBLL().GetAllocatedForms(Convert.ToInt32(ddlEmployee.SelectedValue));
                        int RowCount = 0;
                        int TotalRow = dgvAllForms.Rows.Count;
                        int Freq = 0;
                        foreach (GridViewRow item in dgvAllForms.Rows)
                        {
                            Freq++;
                            CheckBox CheckBox = item.FindControl("chkSelect") as CheckBox;
                            if (CheckBox.Checked)
                            {
                                RowCount++;
                                lstForm.Add(new EntityFormMaster { FormId = Convert.ToInt32(item.Cells[1].Text), FormTitle = Convert.ToString(item.Cells[2].Text) });
                                lblMessage.Text = string.Empty;
                                lblRowCount1.Text = "<b>Total Records:</b> " + RowCount.ToString();
                            }
                            if (Freq == TotalRow && RowCount == 0)
                            {
                                lblMessage.Text = "Please Select Form";
                            }
                        }
                        if (flag)
                        {
                            lblMessage.Text = "Invalid Form Allocation..";
                            foreach (GridView item in dgvAllForms.Rows)
                            {
                                item.Columns[0].Visible = true;//
                            }
                        }
                        else
                        {
                            dgvAllocEmp.AutoGenerateColumns = false;
                            dgvAllocEmp.DataSource = lstForm;
                            dgvAllocEmp.DataBind();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void ddlDesignation_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlDesignation.SelectedIndex > 0)
                {
                    List<EntityEmployee> lstEmp = mobjDeptBLL.GetEmployee(Convert.ToInt32(ddlDesignation.SelectedValue));
                    ddlEmployee.DataSource = lstEmp;
                    lstEmp.Insert(0, new EntityEmployee() { PKId = 0, EmpName = "--Select--" });
                    ddlEmployee.DataValueField = "PKId";
                    ddlEmployee.DataTextField = "EmpName";
                    ddlEmployee.DataBind();
                    dgvAllocEmp.DataSource = new List<EntityFormMaster>();
                    dgvAllocEmp.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }

        }

        protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlEmployee.SelectedIndex > 0)
                {
                    dgvAllocEmp.DataSource = null;
                    List<EntityFormMaster> lstForms = new UserAuthenticationBLL().GetAllocatedForms(Convert.ToInt32(ddlEmployee.SelectedValue));
                    if (lstForms != null)
                    {
                        dgvAllocEmp.AutoGenerateColumns = false;
                        dgvAllocEmp.DataSource = lstForms;
                        dgvAllocEmp.DataBind();
                        int lintRowcount1 = lstForms.Count;
                        lblRowCount1.Text = "<b>Total Records:</b> " + lintRowcount1.ToString();
                        pnlShow.Style.Add(HtmlTextWriterStyle.Display, "");
                        hdnPanel.Value = "";
                    }
                    else
                    {
                        dgvAllocEmp.AutoGenerateColumns = false;
                        dgvAllocEmp.DataSource = new List<EntityFormMaster>();
                        dgvAllocEmp.DataBind();
                        int lintRowcount1 = lstForms.Count;
                        lblRowCount1.Text = "<b>Total Records:</b> " + lintRowcount1.ToString();
                        pnlShow.Style.Add(HtmlTextWriterStyle.Display, "");
                        hdnPanel.Value = "";
                    }
                }
                else
                {
                    if (ddlEmployee.SelectedIndex == 0)
                    {
                        ddlEmployee.Focus();
                        dgvAllocEmp.DataSource = new List<EntityFormMaster>();
                        dgvAllocEmp.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        

        protected void dgvAllForms_DataBound(object sender, EventArgs e)
        {
            int lintCurrentPage = dgvAllForms.PageIndex + 1;
            lblPageCount.Text = "<b>Page</b> " + lintCurrentPage.ToString() + "<b> of </b>" + dgvAllForms.PageCount.ToString();
        }

        

        protected void dgvAllForms_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvAllForms.PageIndex = e.NewPageIndex;
        }

        
    }
}