using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hospital.Models.DataLayer;
using Hospital.Models.BusinessLayer;

namespace Hospital.Models
{
    public class BasePage : System.Web.UI.Page
    {
        UserAuthenticationBLL objUserAutherization = new UserAuthenticationBLL();
        public bool AuthenticateUser(string PageName="")
        {
            bool IsLogin = true;
            if (SessionManager.Instance.LoginUser==null)
            {
                IsLogin = false;
                SessionManager.Instance.LogOut();
                HttpContext.Current.Response.Redirect("~/frmLogin.aspx", true);
            }
            else
            {
                if (SessionManager.Instance.LoginUser.UserType.ToLower()!="admin")
                {
                    var lstForms = objUserAutherization.GetAllocatedForms(SessionManager.Instance.LoginUser.PKId);
                    if (lstForms.Count == 0)
                    {
                        HttpContext.Current.Response.Redirect("~/frmAdminMain.aspx", true);
                    }
                    else
                    {
                        lstForms = lstForms.Where(p => p.FormName.ToLower() == PageName.ToLower()).ToList();
                        if (lstForms.Count == 0)
                        {
                            HttpContext.Current.Response.Redirect("~/frmAdminMain.aspx", true);
                        }
                    }
                }
            }
            return IsLogin;
        }
    }
}