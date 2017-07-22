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
    public partial class frmChangePassword : System.Web.UI.Page
    {

        ChangePasswordBLL mobjChangePasswordBLL = new ChangePasswordBLL();

        protected void Page_Load(object sender, EventArgs e)
        {


        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtOldPass.Text.Length > 0 && txtNewPass.Text.Length > 0)
                {
                    if (string.IsNullOrEmpty(txtOldPass.Text.Trim()))
                    {
                        lblMsg.Text = "Enter Old Password..";
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(txtNewPass.Text.Trim()))
                        {
                            lblMsg.Text = "Enter New Password..";
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(txtConfirmPass.Text.Trim()))
                            {
                                lblMsg.Text = "Enter Confirm Password..";
                            }
                            else
                            {

                                DataTable dt = new DataTable();
                                //string lstrUserName = SessionManager.Instance.UserName;
                                EntityLogin entChangePass = new EntityLogin();
                                entChangePass.OldPass = txtOldPass.Text.Trim();
                                entChangePass.NewPass = txtNewPass.Text.Trim();
                                entChangePass.ConfirmPass = txtConfirmPass.Text.Trim();
                                entChangePass.UserName = SessionManager.Instance.LoginUser.EmpCode;
                                dt = mobjChangePasswordBLL.Checkpass(entChangePass);
                                string lstrOldPass = dt.Rows[0]["Password"].ToString();
                                if (lstrOldPass == txtOldPass.Text)
                                {
                                    int lintCnt = mobjChangePasswordBLL.ChangePassword(entChangePass);

                                    if (lintCnt > 0)
                                    {
                                        Commons.ShowMessage("Password Changed Sucessfully..", this.Page);
                                    }
                                }
                                else
                                {
                                    Commons.ShowMessage("Old Password Is Incorrect", this.Page);
                                }
                            }

                        }
                    }

                }

            }
            catch (System.Threading.ThreadAbortException)
            {

            }
            catch (Exception ex)
            {

                Commons.FileLog("frmChangePassword - btnsave_Click(object sender, EventArgs e)", ex);
            }

        }
    }
}