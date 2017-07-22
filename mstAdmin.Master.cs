using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hospital.Models.BusinessLayer;
using Hospital.Models.Models;
using Hospital.Models.DataLayer;
using System.Web.UI.HtmlControls;

namespace Hospital
{
    public partial class mstAdmin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (SessionManager.Instance.LoginUser!=null)
            {
                lblUserName.Text = SessionManager.Instance.LoginUser.EmpFirstName + " " + SessionManager.Instance.LoginUser.EmpLastName;
            }
        }

        
        protected void lnkLogOut_Click(object sender, EventArgs e)
        {
            SessionManager.Instance.LogOut();
            SessionManager.Instance.LoginUser = null;
            Session.Abandon();
            Response.Redirect("~/frmlogin.aspx", false);
        }

        
    }
}