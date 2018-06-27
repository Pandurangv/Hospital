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

namespace Hospital
{
    public partial class RegistrationPortal : System.Web.UI.Page
    {
        RegistrationBLL mobjRegistrationBLL = new RegistrationBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //SessionManager.Instance.SetSession();
                GetEmployees();
            }
        }

        private void GetEmployees()
        {
            DataTable ldt = new DataTable();
            ldt = mobjRegistrationBLL.GetEmployee();
            ddlEmpName.DataSource = ldt;
            ddlEmpName.DataTextField = "EmpName";
            ddlEmpName.DataValueField = "EmpCode";
            ddlEmpName.DataBind();
            ListItem li = new ListItem();
            li.Text = "--Select Employee--";
            li.Value = "0";
            ddlEmpName.Items.Insert(0, li);
        }
        protected void btnCreateUser_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlEmpName.SelectedIndex > 0)
                {
                    if (txtPassword.Text.Length > 0 && txtConfirmPassword.Text.Length > 0)
                    {
                        if (string.IsNullOrEmpty(txtPassword.Text.Trim()))
                        {
                            Commons.ShowMessage("Please Enter Password", this.Page);
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(txtConfirmPassword.Text.Trim()))
                            {
                                Commons.ShowMessage("Please Enter Confirm Password", this.Page);
                            }
                            else
                            {
                                int lintcnt = 0;
                                EntityRegistration entRegistration = new EntityRegistration();
                                entRegistration.UserName = Commons.ConvertToString(ddlEmpName.SelectedValue);
                                entRegistration.ConfirmPassword = txtConfirmPassword.Text;
                                entRegistration.UserType = Commons.ConvertToString(ddlDepartmentName.SelectedValue);
                                if (!Commons.IsRecordExists("tblLogin", "UserName", entRegistration.UserName))
                                {
                                    lintcnt = mobjRegistrationBLL.GetRegister(entRegistration);
                                    if (lintcnt > 0)
                                    {
                                        Commons.ShowMessage("User Created Successfully....", this.Page);
                                    }
                                    else
                                    {
                                        Commons.ShowMessage("User Created Not Created....", this.Page);
                                    }
                                }
                                else
                                {
                                    Commons.ShowMessage("User Already Created....", this.Page);
                                }
                            }
                        }
                    }
                }
                else
                {
                    Commons.ShowMessage("Please Select Employee", this.Page);
                }
            }
            catch (System.Threading.ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                Commons.FileLog("frmRegistrationPortal - btnCreateUser_Click(object sender, EventArgs e) ", ex);
            }
        }

        protected void ddlEmpName_SelectedIndexChanged(object sender, EventArgs e)
        {
            string lstrEmpCode = Commons.ConvertToString(ddlEmpName.SelectedValue);
            GetDepartment(lstrEmpCode);
        }

        public void GetDepartment(string pstrEmpCode)
        {

            DataTable ldt = new DataTable();
            ldt = mobjRegistrationBLL.GetDepartment(pstrEmpCode);

            ddlDepartmentName.DataSource = ldt;
            ddlDepartmentName.DataValueField = "UserType";
            ddlDepartmentName.DataTextField = "DeptDesc";
            ddlDepartmentName.DataBind();

            ddlDepartmentName.Text = ddlDepartmentName.SelectedValue;

            ListItem li = new ListItem();
            li.Text = "--Select Department--";
            li.Value = "0";
            ddlDepartmentName.Items.Insert(0, li);
        }

    }
}