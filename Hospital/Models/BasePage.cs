using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hospital.Models.DataLayer;

namespace Hospital.Models
{
    public class BasePage : System.Web.UI.Page
    {
        public void AuthenticateUser()
        {
            if (SessionManager.Instance.LoginUser==null)
            {
                SessionManager.Instance.LogOut();
                HttpContext.Current.Response.Redirect("~/frmLogin.aspx", false);
            }
        }
    }
}