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
    public partial class BookAppoinment : System.Web.UI.Page
    {
        BookAppoinmentBLL mobjBookAppoinmentBLL = new BookAppoinmentBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //SessionManager.Instance.SetSession();
                txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                GetAppoinmentNo();
            }
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            int lintcnt = 0;
            EntityBookAppoinment entAppoinment = new EntityBookAppoinment();
            if (string.IsNullOrEmpty(txtAppoinmentNo.Text.Trim()))
            {
                Commons.ShowMessage("Enter Item Code", this.Page);
            }
            else
            {
                DateTime DOA = StringExtension.ToDateTime(txtDate.Text.Trim());
                entAppoinment.Shift = ddlTime.SelectedItem.Text;
                entAppoinment.VisitType = ddlVisitype.SelectedItem.Text;
                DateTime now = DateTime.Now;
                if (DOA > now)
                {
                    lintcnt = mobjBookAppoinmentBLL.InsertAppoinment(entAppoinment);

                    if (lintcnt > 0)
                    {
                        Commons.ShowMessage("Record Inserted Successfully", this.Page);
                    }
                    else
                    {
                        Commons.ShowMessage("Record Not Inserted", this.Page);
                    }
                }
                else
                {
                    Commons.ShowMessage("Date Should Be Less or Equal to Todays Date ", this.Page);
                }
            }
        }


        protected void GetAppoinmentNo()
        {
            DataTable ldt = new DataTable();
            ldt = mobjBookAppoinmentBLL.GetNewAppoinmentCode();
            txtAppoinmentNo.Text = ldt.Rows[0]["AppoinmentNo"].ToString();
        }

        protected void BtnReset_Click(object sender, EventArgs e)
        {
            GetAppoinmentNo();
            txtFirstName.Text = string.Empty;
            txtMiddleName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtPhoneNo.Text = string.Empty;
            txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            ddlTime.SelectedIndex = 0;
            ddlVisitype.SelectedIndex = 0;
        }
    }
}