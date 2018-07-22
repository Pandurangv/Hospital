using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using Hospital.Models.BusinessLayer;
using Hospital.Models.Models;
using Hospital.Models.DataLayer;
using System.Data;
using Hospital.Models;

namespace Hospital
{
    public partial class frmShiftMaster : BasePage
    {
        ShiftBLL mobjDeptBLL = new ShiftBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            base.AuthenticateUser("frmShiftMaster.aspx");
            if (!Page.IsPostBack)
            {
                GetShiftDetail();
                MultiView1.SetActiveView(View1);
            }
        }

        protected void BtnClose_Click(object sender, EventArgs e)
        {
            try
            {
                GetShiftDetail();
                MultiView1.SetActiveView(View1);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton imgEdit = (ImageButton)sender;
                GridViewRow row = (GridViewRow)imgEdit.NamingContainer;
                if (row.Cells[0].Text != null)
                {
                    Shift_Id.Value = Convert.ToString(row.Cells[0].Text);
                }
                txtShiftName.Text = row.Cells[1].Text;
                EntityShift lstB = new ShiftBLL().GetShift(Convert.ToInt32(Shift_Id.Value));
                StartTimeSelector.Hour = lstB.StartTime.Hour;
                StartTimeSelector.Minute = lstB.StartTime.Minute;
                StartTimeSelector.ReadOnly = true;

                EndTimeSelector.Hour = lstB.EndTime.Hour;
                EndTimeSelector.Minute = lstB.EndTime.Minute;
                EndTimeSelector.ReadOnly = true;

                GetShiftDetail();
                btnUpdate.Visible = true;
                BtnSave.Visible = false;
                MultiView1.SetActiveView(View2);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            ViewState["update"] = update.Value;
        }
        protected void BtnAddNewDept_Click(object sender, EventArgs e)
        {
            Clear();
            btnUpdate.Visible = false;
            BtnSave.Visible = true;
            MultiView1.SetActiveView(View2);
        }

        private void Clear()
        {
            txtShiftName.Text = string.Empty;

            StartTimeSelector.ReadOnly = false;
            StartTimeSelector.Hour = DateTime.Now.Hour;
            StartTimeSelector.Minute = DateTime.Now.Minute;

            EndTimeSelector.ReadOnly = false;
            EndTimeSelector.Hour = DateTime.Now.Hour;
            EndTimeSelector.Minute = DateTime.Now.Minute;
        }

        private void GetShiftDetail()
        {
            List<tblShiftMaster> ldtShift = mobjDeptBLL.GetAllShiftDetails();
            if (ldtShift.Count > 0)
            {
                dgvShift.DataSource = ldtShift;
                dgvShift.DataBind();
                int lintRowcount = ldtShift.Count;
                lblRowCount.Text = "<b>Total Records:</b> " + lintRowcount.ToString();
                pnlShow.Style.Add(HtmlTextWriterStyle.Display, "");
                hdnPanel.Value = "";
            }
            else
            {
                hdnPanel.Value = "none";
            }
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            int lintcnt = 0;
            EntityShift entDept = new EntityShift();

            if (string.IsNullOrEmpty(txtShiftName.Text.Trim()))
            {
                lblMsg.Text = "Please Enter Shift Name";
                return;
            }
            else
            {
                entDept.ShiftName = txtShiftName.Text.Trim();
                TimeSpan objTime = new TimeSpan(StartTimeSelector.Hour, StartTimeSelector.Minute, 0);
                DateTime dt = DateTime.Now.Date;
                dt = dt.Add(objTime);
                entDept.StartTime = dt;

                TimeSpan objTime1 = new TimeSpan(EndTimeSelector.Hour, EndTimeSelector.Minute, 0);
                DateTime dt1 = DateTime.Now.Date;
                dt1 = dt1.Add(objTime1);
                entDept.EndTime = dt1;

                int Hrs = entDept.EndTime.Subtract(entDept.StartTime).Hours;
                if (Hrs >= 8)
                {
                    if (mobjDeptBLL.GetShiftCount() < 3)
                    {
                        if (!mobjDeptBLL.IsRecordExists(entDept))
                        {
                            lintcnt = mobjDeptBLL.InsertShift(entDept);
                            if (lintcnt > 0)
                            {
                                GetShiftDetail();
                                lblMessage.Text = "Record Inserted Successfully....";
                                update.Value = Server.UrlEncode(System.DateTime.Now.ToString());
                            }
                            else
                            {
                                lblMessage.Text = "Record Not Inserted...";
                            }
                        }
                        else
                        {
                            lblMessage.Text = "Record Already Exist....";
                        }
                    }
                    else
                    {
                        lblMessage.Text = "You Can Create Only Three Shifts";
                    }
                }
                else
                {
                    lblMessage.Text = "Shift Span Should Be 8 Hours";
                }

            }
            //}
            MultiView1.SetActiveView(View1);
        }

        void frmDepartmentMaster_Load(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        

        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            int lintCnt = 0;
            try
            {
                EntityShift entDept = new EntityShift();
                entDept.ShiftId = Convert.ToInt32(Shift_Id.Value);
                entDept.ShiftName = txtShiftName.Text;

                TimeSpan objTime = new TimeSpan(StartTimeSelector.Hour, StartTimeSelector.Minute, 0);
                DateTime dt = DateTime.Now.Date;
                dt = dt.Add(objTime);
                entDept.StartTime = dt;

                TimeSpan objTime1 = new TimeSpan(EndTimeSelector.Hour, EndTimeSelector.Minute, 0);
                DateTime dt1 = DateTime.Now.Date;
                dt1 = dt1.Add(objTime1);
                entDept.EndTime = dt1;
                if (string.IsNullOrEmpty(txtShiftName.Text))
                {
                    lblMsg.Text = "Please Enter Shift Name";
                    txtShiftName.Focus();
                    return;
                }

                if (!mobjDeptBLL.IsRecordExists(entDept))
                {
                    lintCnt = mobjDeptBLL.Update(entDept);

                    if (lintCnt > 0)
                    {
                        GetShiftDetail();
                        lblMessage.Text = "Record Updated Successfully";
                        //this.programmaticModalPopup.Hide();
                    }
                    else
                    {
                        lblMessage.Text = "Record Not Updated";
                    }
                }
                else
                {
                    lblMessage.Text = "Record Already Exist....";
                }
                MultiView1.SetActiveView(View1);
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmDepartmentMaster -  BtnEdit_Click(object sender, EventArgs e)", ex);
            }

        }

        protected void chkDelete_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chk.NamingContainer;
            if (chk.Checked)
            {
                LinkButton lnkDeptCode = (LinkButton)row.FindControl("lnkDeptCode");
                DeptCode.Value = lnkDeptCode.Text;
                //lblMessage.Text = string.Empty;
            }
            else
            {
                DeptCode.Value = string.Empty;
            }
        }
        
        protected void dgvDepartment_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                List<tblShiftMaster> ldtShift = mobjDeptBLL.GetAllShiftDetails();
                dgvShift.DataSource = ldtShift;// (List<sp_GetAllBedAllocResult>)Session["DepartmentDetail"];
                dgvShift.DataBind();
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmDepartmentMaster - dgvDepartment_PageIndexChanged(object sender, EventArgs e)", ex);
            }
        }
        protected void dgvDepartment_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvShift.PageIndex = e.NewPageIndex;
        }
        protected void dgvDepartment_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (!e.Row.Cells[0].Text.Equals("Bed Id", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (!e.Row.Cells[5].Text.Equals("&nbsp;", StringComparison.CurrentCultureIgnoreCase))
                    {
                        if (!e.Row.Cells[6].Text.Equals("&nbsp;", StringComparison.CurrentCultureIgnoreCase))
                        {
                            e.Row.Enabled = true;
                            e.Row.BackColor = Color.LightGreen;
                        }
                        else
                        {
                            e.Row.Enabled = false;
                            e.Row.BackColor = Color.LightSkyBlue;
                        }
                    }
                    else
                    {
                        e.Row.Enabled = true;
                        e.Row.BackColor = Color.LightGreen;
                    }
                }
                

                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap; text-align:left;");
                }
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmDepartmentMaster -  dgvData_RowDataBound(object sender, GridViewRowEventArgs e)", ex);
            }
        }
        protected void dgvDepartment_DataBound(object sender, EventArgs e)
        {
            int lintCurrentPage = dgvShift.PageIndex + 1;
            lblPageCount.Text = "<b>Page</b> " + lintCurrentPage.ToString() + "<b> of </b>" + dgvShift.PageCount.ToString();
        }

        
        protected void ddlPatientName_SelectedIndexChanged1(object sender, EventArgs e)
        {
            
        }
    }
}