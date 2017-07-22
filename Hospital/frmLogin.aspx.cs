using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Hospital.Models.BusinessLayer;
using Hospital.Models.Models;
using Hospital.Models.DataLayer;


namespace Hospital
{
    public partial class frmLogin1 : System.Web.UI.Page
    {
        GetLoginBLL mobjGetLoginBLL = new GetLoginBLL();
        EmployeeBLL objEmpbl = new EmployeeBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GetDepartments();
            }
        }
        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtUserName.Text) && !string.IsNullOrEmpty(txtPassword.Text))
                {
                    EntityLogin entLogin = new EntityLogin();
                    entLogin.UserName = txtUserName.Text.Trim();
                    entLogin.Password = txtPassword.Text.Trim();
                    entLogin.UserType = ddlUserType.SelectedValue;
                    EntityLogin dt = mobjGetLoginBLL.GetLogin(entLogin);

                    if (dt != null)
                    {
                        entLogin.PKId = dt.PKId;
                        var emp = objEmpbl.SelectAllEmployee().Where(p => p.PKId == dt.EmpId).FirstOrDefault();
                        emp.UserType = entLogin.UserType;
                        SessionManager.Instance.LoginUser = emp;

                        entLogin.IsFirstLogin = mobjGetLoginBLL.CheckLogin(entLogin.PKId);
                        if (entLogin.IsFirstLogin)
                        {
                            Response.Redirect("frmFirstLogin.aspx", false);
                        }
                        else
                        {
                            DateTime dtExp = Commons.GetExpDate();
                            DateTime alertDate = dtExp.AddDays(-5);
                            if (DateTime.Now.Date.CompareTo(dtExp) >= 0)
                            {
                                CriticareHospitalDataContext objData = new CriticareHospitalDataContext();
                                objData.STP_BackUpLogin();
                            }
                            else
                            {
                                if (dt.UserType.Trim() == "Doctor")
                                {
                                    Response.Redirect("frmPrescription.aspx", false);
                                }
                                else
                                {
                                    Response.Redirect("frmOPDPatientDetail.aspx", false);
                                }
                            }
                        }
                    }
                    else
                    {
                        Commons.ShowMessage("Invalid User Name or Password.", this.Page);
                        txtUserName.Focus();
                    }
                }
            }
            catch (System.Threading.ThreadAbortException)
            {
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmlogin - btnLogin_Click(object sender, EventArgs e)", ex);
            }
        }

        public void GetDepartments()
        {
            DataTable ldt = new DataTable();
            ldt = mobjGetLoginBLL.GetDepartments();
            ddlUserType.DataSource = ldt;
            ddlUserType.DataValueField = "UserType";
            ddlUserType.DataTextField = "DeptDesc";
            ddlUserType.DataBind();

            ListItem li = new ListItem();
            li.Text = "--Select Department--";
            li.Value = "0";
            ddlUserType.Items.Insert(0, li);
        }
    }
}