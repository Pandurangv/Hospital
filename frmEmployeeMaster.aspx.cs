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

namespace Hospital
{
    public partial class frmEmployeeMaster : BasePage
    {
        EmployeeBLL mobjEmpBLL = new EmployeeBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            base.AuthenticateUser();
            if (!Page.IsPostBack)
            {
                GetEmployee();
                BindDocTypes();
                BindDesignation();
                GetDepartments();
                //GetDepartmentsForEdit();
                CalendarExtender1.EndDate = DateTime.Now;
                CalendarExtender1.StartDate = DateTime.Now.AddYears(-60);
                CalDOBDate.StartDate = DateTime.Now.Date.AddYears(-60);
                CalDOBDate.EndDate = DateTime.Now.Date;//.AddYears(-20);
                btnUpdate.Visible = false;
                BtnSave.Visible = true;
                MultiView1.SetActiveView(View1);
            }
        }

        private void BindDocTypes()
        {
            OperationCategoryBLL objdoctype = new OperationCategoryBLL();
            List<EntityOperationCategory> lst= objdoctype.GetDocCategories();
            lst.Insert(0, new EntityOperationCategory() { CategotyId=0,CategoryName="---Select Doctor Profession---" });
            ddlDocType.DataSource = lst;
            ddlDocType.DataTextField = "CategoryName";
            ddlDocType.DataValueField = "CategotyId";
            ddlDocType.DataBind();
        }

        private void BindDesignation()
        {
            try
            {
                DataTable tblDesig = new DesignationBLL().GetAllDesignations();
                DataRow dr = tblDesig.NewRow();
                dr["PKId"] = 0;
                dr["DesignationDesc"] = "---Select---";
                tblDesig.Rows.InsertAt(dr, 0);

                ddlDesignation.DataSource = tblDesig;
                ddlDesignation.DataValueField = "PKId";
                ddlDesignation.DataTextField = "DesignationDesc";
                ddlDesignation.DataBind();
                txtConCharges.Visible = false;
                lblConCharges.Visible = false;
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
        protected void BtnAddNewEmp_Click(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;
            DataTable ldt = new DataTable();
            txtAddress.Text = string.Empty;
            txtFirstName.Text = string.Empty;
            txtMidleName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtEducation.Text = string.Empty;
            txtRegistrationNo.Text = string.Empty;
            txtJoinDate.Text = string.Empty;
            txtDOBDate.Text = string.Empty;
            ddlDepartment.SelectedIndex = 0;
            ldt = mobjEmpBLL.GetNewEmpCode();
            txtEmpCode.Text = ldt.Rows[0].ItemArray[0].ToString();
            btnUpdate.Visible = false;
            BtnSave.Visible = true;
            txtBank.Text = string.Empty;
            txtBankAc.Text = string.Empty;
            txtPF.Text = string.Empty;
            txtPan.Text = string.Empty;
            txtBaseSal.Text = string.Empty;
            txtConCharges.Text = string.Empty;
            txtCity.Text = string.Empty;
            txtState.Text = string.Empty;
            txtPinCode.Text = string.Empty;

            ddlDesignation.SelectedIndex = 0;
            MultiView1.SetActiveView(View2);
        }

        protected void dgvEmployee_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                List<EntityEmployee> ldtEmp = mobjEmpBLL.SelectAllEmployee();
                dgvEmployee.DataSource = ldtEmp;
                dgvEmployee.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
        protected void dgvEmployee_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvEmployee.PageIndex = e.NewPageIndex;
        }

        public void GetDepartments()
        {
            DataTable ldt = new DataTable();
            ldt = mobjEmpBLL.GetDepartments();

            DataRow dr = ldt.NewRow();
            dr["PKId"] = 0;
            dr["DeptDesc"] = "---Select---";
            ldt.Rows.InsertAt(dr, 0);

            ddlDepartment.DataSource = ldt;
            ddlDepartment.DataValueField = "PKId";
            ddlDepartment.DataTextField = "DeptDesc";
            ddlDepartment.DataBind();

        }


        protected void dgvEmployee_DataBound(object sender, EventArgs e)
        {
            int lintCurrentPage = dgvEmployee.PageIndex + 1;
            lblPageCount.Text = "<b>Page</b> " + lintCurrentPage.ToString() + "<b> of </b>" + dgvEmployee.PageCount.ToString();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton imgEdit = (ImageButton)sender;
                GridViewRow cnt = (GridViewRow)imgEdit.NamingContainer;
                int id = Convert.ToInt32(cnt.Cells[0].Text);
                EntityEmployee tblEmp = mobjEmpBLL.SelectAllEmployee().Where(p => p.PKId == id).FirstOrDefault();
                if (tblEmp!=null)
                {
                    hdnId.Value = id.ToString();
                    txtEmpCode.Text = "E000" + tblEmp.PKId;
                    txtFirstName.Text = tblEmp.EmpFirstName;
                    txtMidleName.Text = tblEmp.EmpMiddleName;
                    txtLastName.Text = tblEmp.EmpLastName;
                    txtEducation.Text = tblEmp.Education;
                    txtRegistrationNo.Text = tblEmp.RegistrationNo;
                    if (tblEmp.DeptId>0)
                    {
                        ListItem item = ddlDepartment.Items.FindByValue(tblEmp.DeptId.ToString());
                        ddlDepartment.SelectedValue = item.Value;
                    }
                    else
                    {
                        ddlDepartment.SelectedValue = "0";
                    }
                    if (tblEmp.DesignationId != null)
                    {
                        ListItem Desig = ddlDesignation.Items.FindByValue(tblEmp.DesignationId.ToString());
                        ddlDesignation.SelectedValue = tblEmp.DesignationId.ToString();
                    }
                    else
                    {
                        ddlDesignation.SelectedValue = "0";
                    }
                    
                    ddlDesignation_SelectedIndexChanged(sender, e);
                    if (tblEmp.DoctorType!=null)
                    {
                        ddlDocType.SelectedValue = Convert.ToString(tblEmp.DoctorType);
                    }
                    if (tblEmp.EmpDOB!=null)
                    {
                        DateTime dt = tblEmp.EmpDOB.Value.Date;
                        txtDOBDate.Text= string.Format("{0:dd/MM/yyyy}", dt);
                    }
                    if (tblEmp.EmpDOJ != null)
                    {
                        DateTime dt = tblEmp.EmpDOJ.Value.Date;
                        txtJoinDate.Text = string.Format("{0:dd/MM/yyyy}", dt);
                    }
                    txtAddress.Text = tblEmp.EmpAddress;
                    txtBank.Text = tblEmp.BankName;
                    txtBankAc.Text = tblEmp.BankACNo;
                    txtPF.Text = tblEmp.PFNo;
                    txtPan.Text = tblEmp.PanNo;
                    txtBaseSal.Text = Convert.ToString(tblEmp.BasicSal);
                    txtConCharges.Text = Convert.ToString(tblEmp.ConsultingCharges);
                    txtCity.Text = tblEmp.City;
                    txtState.Text = tblEmp.State;
                    txtPinCode.Text = tblEmp.Pin;


                    txtEmpCode.ReadOnly = true;
                    btnUpdate.Visible = true;
                    BtnSave.Visible = false;
                    MultiView1.SetActiveView(View2);
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }

        }

        private void GetEmployee()
        {
            List<EntityEmployee> ldtEmp = mobjEmpBLL.SelectAllEmployee();
            if (ldtEmp != null)
            {
                dgvEmployee.DataSource = ldtEmp;
                dgvEmployee.DataBind();
                int lintRowcount = ldtEmp.Count;
                lblRowCount.Text = "<b>Total Records:</b> " + lintRowcount.ToString();
            }
        }
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            int cnt = 0;
            try
            {
                EntityEmployee entEmployee = new EntityEmployee();
                entEmployee.EmpCode = txtEmpCode.Text;
                entEmployee.EmpFirstName = FirstCharToUpper(txtFirstName.Text);
                entEmployee.EmpMiddleName = FirstCharToUpper(txtMidleName.Text);
                entEmployee.EmpLastName = FirstCharToUpper(txtLastName.Text);
                entEmployee.Education = txtEducation.Text.Trim();
                entEmployee.RegistrationNo = txtRegistrationNo.Text.Trim();
                entEmployee.DesignationId = Convert.ToInt32(ddlDesignation.SelectedValue);
                entEmployee.DoctorType = Convert.ToInt32(ddlDocType.SelectedValue);
                entEmployee.City = !string.IsNullOrEmpty(txtCity.Text) ? txtCity.Text : "";
                entEmployee.State = !string.IsNullOrEmpty(txtState.Text) ? txtState.Text : "";
                entEmployee.Pin = !string.IsNullOrEmpty(txtPinCode.Text) ? txtPinCode.Text : "";
                if (Convert.ToInt32(ddlDesignation.SelectedValue) == 9)
                {
                    entEmployee.ConsultingCharges = Convert.ToDecimal(txtConCharges.Text);
                    entEmployee.BankName = "";
                    entEmployee.BankACNo = "";
                    entEmployee.PFNo = "";
                    entEmployee.PanNo = "";
                    entEmployee.BasicSal = 0;
                    entEmployee.EmpDOB = null;
                    entEmployee.EmpDOJ = null;
                }
                else
                {
                    if (txtDOBDate.Text == string.Empty)
                    {
                        entEmployee.EmpDOB = null;// System.DateTime.Today.Date;
                    }
                    else
                    {
                        entEmployee.EmpDOB = StringExtension.ToDateTime(txtDOBDate.Text);
                    }
                    if (txtJoinDate.Text == string.Empty)
                    {
                        entEmployee.EmpDOJ = null;// System.DateTime.Today.Date;
                    }
                    else
                    {
                        entEmployee.EmpDOJ = StringExtension.ToDateTime(txtJoinDate.Text);
                    }
                    if (!string.IsNullOrEmpty(txtDOBDate.Text) || !string.IsNullOrEmpty(txtJoinDate.Text))
                    {
                        if (entEmployee.EmpDOJ.Value.CompareTo(entEmployee.EmpDOB.Value) <= 0)
                        {
                            lblMsg.Text = "Birth Date should be older than Joining Date...";
                            return;
                        }
                        if (entEmployee.EmpDOJ.Value.Year - entEmployee.EmpDOB.Value.Year < 18)
                        {
                            lblMsg.Text = "Employee Cannot Be Less Then 18 Age";
                        }
                    }
                    entEmployee.BankName = FirstCharToUpper(txtBank.Text);
                    entEmployee.BankACNo = txtBankAc.Text.Trim();
                    entEmployee.PFNo = txtPF.Text;
                    entEmployee.PanNo = txtPan.Text;
                    entEmployee.BasicSal = 0;
                    if (!string.IsNullOrEmpty(txtBaseSal.Text))
                    {
                        entEmployee.BasicSal = Convert.ToDecimal(txtBaseSal.Text);
                    }
                }
                entEmployee.EmpAddress = FirstCharToUpper(txtAddress.Text);
                
                entEmployee.UserType = Convert.ToString(ddlDepartment.SelectedItem.Text);
                entEmployee.DeptId = Convert.ToInt32(ddlDepartment.SelectedValue);
                entEmployee.EntryBy = SessionManager.Instance.LoginUser.PKId.ToString();
                entEmployee.DoctorType = Convert.ToInt32(ddlDocType.SelectedValue);
                //entEmployee.DeptId = Convert.ToInt32(ddlDepartment.SelectedValue);
                entEmployee.DesignationId = Convert.ToInt32(ddlDesignation.SelectedValue);
                cnt = mobjEmpBLL.InsertEmployee(entEmployee);
                if (cnt > 0)
                {
                    lblMessage.Text = "Record saved successfully.";
                    GetEmployee();
                    MultiView1.SetActiveView(View1);
                }
                else
                {
                    lblMessage.Text = "Record Not Inserted";
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        ////ddlDepartment_SelectedIndexChanged
        //public void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        //{ 
            
        //}

        public void ddlDesignation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(ddlDesignation.SelectedValue)==SettingsManager.Instance.VisitingDoctorDesigId)
            {
                lblDOB.Visible = false;
                txtDOBDate.Visible = false;
                lblDOJ.Visible = false;
                txtJoinDate.Visible = false;
                lblPan.Visible = false;
                txtPan.Visible = false;
                lblPf.Visible = false;
                txtPF.Visible = false;
                lblBank.Visible = false;
                txtBank.Visible = false;
                lblBankAc.Visible = false;
                txtBankAc.Visible = false;
                lblBasicSal.Visible = false;
                txtBaseSal.Visible = false;

                rfvDOB.Enabled = false;
                //rfvDobdate.Enabled = false;
                rfvJoin.Enabled = false;
                //rfvBaseSal.Enabled = false;
                //regJoinDate.Enabled = false;
                //rfvConCharges.Enabled = false;
                
                ddlDocType.Visible = true;
                lblDocType.Visible = true;
                lblConCharges.Visible = true;
                txtConCharges.Visible = true;
                lblBasicSal.Visible=false;
                lblConCharges.Visible = true;
                txtConCharges.Visible = true;
            }
            else
            {
                if (Convert.ToInt32(ddlDesignation.SelectedValue) == SettingsManager.Instance.DoctorDesigId || Convert.ToInt32(ddlDesignation.SelectedValue) == SettingsManager.Instance.NurseDesigId)
                {
                    ddlDocType.Visible = true;
                    lblDocType.Visible = true;
                    //rfvConCharges.Enabled = true;
                }
                else
                {
                    ddlDocType.Visible = false;
                    lblDocType.Visible = false;
                    //rfvConCharges.Enabled = false;
                }
                lblBasicSal.Visible = true;
                rfvDOB.Enabled = true;
                //rfvDobdate.Enabled = true;
                rfvJoin.Enabled = true;
                //rfvBaseSal.Enabled = true;
                //regJoinDate.Enabled = true;

                lblDOB.Visible = true;
                txtDOBDate.Visible = true;
                lblDOJ.Visible = true;
                txtJoinDate.Visible = true;
                lblPan.Visible = true;
                txtPan.Visible = true;
                lblPf.Visible = true;
                txtPF.Visible = true;
                lblBank.Visible = true;
                txtBank.Visible = true;
                lblBankAc.Visible = true;
                txtBankAc.Visible = true;
                lblBasicSal.Visible = true;
                txtBaseSal.Visible = true;
                lblConCharges.Visible = false;
                txtConCharges.Visible = false;
            }
        }

        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            int cnt = 0;
            List<EntityEmployee> lstentEmployee = new List<EntityEmployee>();
            try
            {
                EntityEmployee entEmployee = new EntityEmployee();
                entEmployee.PKId = Convert.ToInt32(hdnId.Value);
                entEmployee.EmpCode = txtEmpCode.Text;
                entEmployee.EmpFirstName = FirstCharToUpper(txtFirstName.Text);
                entEmployee.EmpMiddleName = FirstCharToUpper(txtMidleName.Text);
                entEmployee.EmpLastName = FirstCharToUpper(txtLastName.Text);
                entEmployee.Education = txtEducation.Text.Trim();
                entEmployee.RegistrationNo = txtRegistrationNo.Text.Trim();
                entEmployee.DoctorType = Convert.ToInt32(ddlDocType.SelectedValue);
                entEmployee.City = !string.IsNullOrEmpty(txtCity.Text) ? txtCity.Text : "";
                entEmployee.State = !string.IsNullOrEmpty(txtState.Text) ? txtState.Text : "";
                entEmployee.Pin = !string.IsNullOrEmpty(txtPinCode.Text) ? txtPinCode.Text : "";
                entEmployee.DoctorType = Convert.ToInt32(ddlDocType.SelectedValue);
                entEmployee.DeptId = Convert.ToInt32(ddlDepartment.SelectedValue);
                entEmployee.DesignationId = Convert.ToInt32(ddlDesignation.SelectedValue);
                if (Convert.ToInt32(ddlDesignation.SelectedValue) == 9)
                {
                    entEmployee.ConsultingCharges = Convert.ToDecimal(txtConCharges.Text);
                    entEmployee.BankName = "";
                    entEmployee.BankACNo = "";
                    entEmployee.PFNo = "";
                    entEmployee.PanNo = "";
                    entEmployee.BasicSal = 0;
                    entEmployee.EmpDOB = null;
                    entEmployee.EmpDOJ = null;
                }
                else
                {
                    if (txtDOBDate.Text == string.Empty)
                    {
                        entEmployee.EmpDOB = null;// System.DateTime.Today.Date;
                    }
                    else
                    {
                        entEmployee.EmpDOB = StringExtension.ToDateTime(txtDOBDate.Text);
                    }
                    if (txtJoinDate.Text == string.Empty)
                    {
                        entEmployee.EmpDOJ = null;// System.DateTime.Today.Date;
                    }
                    else
                    {
                        entEmployee.EmpDOJ = StringExtension.ToDateTime(txtJoinDate.Text);
                    }
                    if (!string.IsNullOrEmpty(txtDOBDate.Text) || !string.IsNullOrEmpty(txtJoinDate.Text))
                    {
                        if (entEmployee.EmpDOJ.Value.CompareTo(entEmployee.EmpDOB.Value) <= 0)
                        {
                            lblMsg.Text = "Birth Date should be older than Joining Date...";
                            return;
                        }
                        if (entEmployee.EmpDOJ.Value.Year - entEmployee.EmpDOB.Value.Year < 18)
                        {
                            lblMsg.Text = "Employee Cannot Be Less Then 18 Age";
                        }
                    }
                    entEmployee.BankName = FirstCharToUpper(txtBank.Text);
                    entEmployee.BankACNo = txtBankAc.Text.Trim();
                    entEmployee.PFNo = txtPF.Text;
                    entEmployee.PanNo = txtPan.Text;
                    entEmployee.BasicSal = 0;
                    if (!string.IsNullOrEmpty(txtBaseSal.Text))
                    {
                        entEmployee.BasicSal = Convert.ToDecimal(txtBaseSal.Text);    
                    }
                    
                }
                entEmployee.EmpAddress = txtAddress.Text;
                entEmployee.ChangeBy = SessionManager.Instance.LoginUser.PKId.ToString();
                cnt = mobjEmpBLL.UpdateEmployee(entEmployee);

                if (cnt > 0)
                {
                    GetEmployee();
                    lblMessage.Text = "Record Updated Successfully.";
                }
                else
                {
                    lblMessage.Text = "Record Not Updated.";
                }
                MultiView1.SetActiveView(View1);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }


        protected void BtnClose_Click(object sender, EventArgs e)
        {
            txtAddress.Text = string.Empty;
            txtFirstName.Text = string.Empty;
            txtMidleName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtEducation.Text = string.Empty;
            txtRegistrationNo.Text = string.Empty;
            txtJoinDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtDOBDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            ddlDepartment.SelectedIndex = 0;
            txtBaseSal.Text = string.Empty;
            txtBank.Text = string.Empty;
            txtBankAc.Text = string.Empty;
            txtPan.Text = string.Empty;
            txtPF.Text = string.Empty;
            txtEmpCode.Text = string.Empty;
            txtState.Text = string.Empty;
            txtPinCode.Text = string.Empty;
            txtCity.Text = string.Empty;
            GetEmployee();
            MultiView1.SetActiveView(View1);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = string.Empty;
                if (!string.IsNullOrEmpty(txtSearch.Text))
                {
                    SearchEmployeeDetails(txtSearch.Text);
                }
                else
                {
                    lblMessage.Text = "Please Fill Search Text.";
                    txtSearch.Focus();
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }

        }

        private void SearchEmployeeDetails(string Prefix)
        {
            List<EntityEmployee> lst = mobjEmpBLL.SelectEmployee(Prefix);
            if (lst != null)
            {
                dgvEmployee.DataSource = lst;
                dgvEmployee.DataBind();
            }
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;
            txtSearch.Text = string.Empty;
            GetEmployee();
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/ExcelReport/MonthwiseSalExcel.aspx?ReportType=EmpList", false);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        public string FirstCharToUpper(string value)
        {
            char[] array = value.ToCharArray();
            // Handle the first letter in the string.
            if (array.Length >= 1)
            {
                if (char.IsLower(array[0]))
                {
                    array[0] = char.ToUpper(array[0]);
                }
            }
            // Scan through the letters, checking for spaces.
            // ... Uppercase the lowercase letters following spaces.
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i - 1] == ' ')
                {
                    if (char.IsLower(array[i]))
                    {
                        array[i] = char.ToUpper(array[i]);
                    }
                }
            }
            return new string(array);
        }
    }
}