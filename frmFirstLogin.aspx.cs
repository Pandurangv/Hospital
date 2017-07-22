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
    public partial class frmFirstLogin : System.Web.UI.Page
    {
        GetLoginBLL mobjGetLoginBLL = new GetLoginBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //SessionManager.Instance.SetSession();
                //GetDepartments();
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtPassowrd.Text))
                {
                    lblMessage.Text = "Please Enter New Password";
                    txtPassowrd.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtConformPassword.Text))
                {
                    lblMessage.Text = "Please Conform Password";
                    txtConformPassword.Focus();
                    return;
                }
                if (txtPassowrd.Text != txtConformPassword.Text)
                {
                    lblMessage.Text = "Password Didn't Matched";
                    txtConformPassword.Text = string.Empty;
                    txtConformPassword.Focus();
                    return;
                }

                EntityLogin entLog = new EntityLogin()
                {
                    EmpId=SessionManager.Instance.LoginUser.PKId,
                    UserType=SessionManager.Instance.LoginUser.UserType,
                };
                //User = ParentFormMain.LoginUser;
                entLog.Password = txtConformPassword.Text;
                entLog.IsFirstLogin = false;
                GetLoginBLL LogBLL = new GetLoginBLL();
                int Result = LogBLL.Update(entLog);


                if (Result > 0)
                {
                    if (entLog.UserType.Trim() == "Doctor")
                    {
                        Response.Redirect("frmPrescription.aspx", false);
                    }
                    else
                    {
                        Response.Redirect("frmOPDPatientDetail.aspx", false);
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void BtnReset_Click(object sender, EventArgs e)
        {
            txtConformPassword.Text = string.Empty;
            txtPassowrd.Text = string.Empty;
        }
    }
}