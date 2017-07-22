using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hospital.Models.DataLayer;

namespace Hospital
{
    public partial class frmAdminMain : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (SessionManager.Instance.LoginUser==null)
            {
                Response.Redirect("~/frmLogin.aspx", false);
            }
            else
            {
                lblMessage.Text = "";
                DateTime dtExp = Commons.GetExpDate();
                DateTime alertDate = dtExp.AddDays(-5);
                if (DateTime.Now.Date.CompareTo(alertDate) >= 0)
                {
                    lblMessage.Text = "Your trial version will expired soon. Please do payment before expiry date";
                }
            }
            
        }
    }
}