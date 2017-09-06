using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hospital.Models.DataLayer;

namespace Hospital.Models
{
    public class BasePage : System.Web.UI.Page
    {
        public bool AuthenticateUser()
        {
            bool IsLogin = true;
            if (SessionManager.Instance.LoginUser==null)
            {
                IsLogin = false;
                SessionManager.Instance.LogOut();
                HttpContext.Current.Response.Redirect("~/frmLogin.aspx", true);
            }
            return IsLogin;
        }
    }
}