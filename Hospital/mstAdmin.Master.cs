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
                if (SessionManager.Instance.LoginUser.UserType.ToLower()!="admin")
                {
                    HideUnAutherized();
                }
            }
        }

        
        protected void lnkLogOut_Click(object sender, EventArgs e)
        {
            SessionManager.Instance.LogOut();
            SessionManager.Instance.LoginUser = null;
            Session.Abandon();
            Response.Redirect("~/frmlogin.aspx", false);
        }

        public void HideUnAutherized()
        {
            UserAuthenticationBLL objUser = new UserAuthenticationBLL();
            var lst = objUser.GetAllocatedForms(SessionManager.Instance.LoginUser.PKId);
            
            EntityFormMaster EntForms = new EntityFormMaster();
            Dictionary<string, MenuItem> DicMenu = new Dictionary<string, MenuItem>();
            foreach (MenuItem item in Menu1.Items)
            {
                int Count = 0;
                if (item.Value == "Main")
                {
                    item.Enabled = true;

                    foreach (MenuItem childItem in item.ChildItems)
                    {
                        if (lst != null)
                        {
                            foreach (EntityFormMaster Lst in lst)
                            {
                                if (childItem.NavigateUrl.Contains(Lst.FormName))
                                {
                                    DicMenu.Add(item.Text + Count.ToString(), childItem);
                                    Count++;
                                }
                            }
                        }
                    }
                }
            }
            foreach (MenuItem item in Menu1.Items)
            {
                if (item.Value == "Main")
                {
                    for (int i = 0; i < item.ChildItems.Count; i++)
                    {
                        item.ChildItems.Clear();
                    }
                }
            }

            foreach (MenuItem item in Menu1.Items)
            {
                if (item.Value == "Main")
                {
                    foreach (KeyValuePair<string, MenuItem> item1 in DicMenu)
                    {
                        if (item1.Key.Contains(item.Text))
                        {
                            item.ChildItems.Add(item1.Value);
                        }
                    }
                }
            }
        }
        
    }
}