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

namespace Hospital.PatientStatus
{
    public partial class frmBirthCertificate : System.Web.UI.Page
    {
        BirthCertificateBLL mobjBirthBLL = new BirthCertificateBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (SessionManager.Instance.LoginUser != null)
            {
                if (!Page.IsPostBack)
                {
                    Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                    GetBirthDetails();
                    GetPatientList();
                    GetGenderDesc();
                    MultiView1.SetActiveView(View1);
                }
            }
            else
            {
                Response.Redirect("~/frmlogin.aspx", false);
            }
        }

        private void GetBirthDetails()
        {
            try
            {
                List<EntityBirthCertificate> ldtBirth = mobjBirthBLL.GetAllBirthDetails();
                if (ldtBirth.Count > 0)
                {
                    dgvShift.DataSource = ldtBirth;
                    dgvShift.DataBind();
                    Session["DepartmentDetail"] = ldtBirth;
                    Session["StartBirthDetails"] = ListConverter.ToDataTable(ldtBirth);
                    int lintRowcount = ldtBirth.Count;
                    lblRowCount.Text = "<b>Total Records:</b> " + lintRowcount.ToString();
                    pnlShow.Style.Add(HtmlTextWriterStyle.Display, "");
                    hdnPanel.Value = "";
                }
                else
                {
                    //pnlShow.Style.Add(HtmlTextWriterStyle.Display, "none");
                    hdnPanel.Value = "none";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        private void GetGenderDesc()
        {
            try
            {
                List<EntityBirthCertificate> ldtRequisition = mobjBirthBLL.GetAllGender();
                ldtRequisition.Insert(0, new EntityBirthCertificate() { GenderID = 0, GenderDesc = "----Select-----" });
                ddlGender.DataSource = ldtRequisition;
                ddlGender.DataTextField = "GenderDesc";
                ddlGender.DataValueField = "GenderID";
                ddlGender.DataBind();
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }

        }

        protected void BtnClose_Click(object sender, EventArgs e)
        {
            try
            {
                GetBirthDetails();
                MultiView1.SetActiveView(View1);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    ImageButton imgEdit = (ImageButton)sender;
            //    GridViewRow row = (GridViewRow)imgEdit.NamingContainer;
            //    if (row.Cells[0].Text != null)
            //    {
            //        Session["Shift_Id"] = Convert.ToInt32(row.Cells[0].Text);
            //    }
            //    txtShiftName.Text = row.Cells[1].Text;
            //    EntityShift lstB = new ShiftBLL().GetShift(Convert.ToInt32(Session["Shift_Id"]));
            //    StartTimeSelector.Hour = lstB.StartTime.Hour;
            //    StartTimeSelector.Minute = lstB.StartTime.Minute;
            //    StartTimeSelector.ReadOnly = true;

            //    GetShiftDetail();
            //    btnUpdate.Visible = true;
            //    BtnSave.Visible = false;
            //    MultiView1.SetActiveView(View2);
            //}
            //catch (Exception ex)
            //{
            //    lblMessage.Text = ex.Message;
            //}
        }

        private void GetPatientList()
        {
            try
            {
                List<EntityBirthCertificate> ldtRequisition = mobjBirthBLL.GetAllPatients();
                ldtRequisition.Insert(0, new EntityBirthCertificate() { PatientAdmitID = 0, FullName = "----Select-----" });
                ddlPatientName.DataSource = ldtRequisition;
                ddlPatientName.DataTextField = "FullName";
                ddlPatientName.DataValueField = "PatientAdmitId";
                ddlPatientName.DataBind();
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            ViewState["update"] = Session["update"];
        }

        protected void BtnAddNewDept_Click(object sender, EventArgs e)
        {
            Clear();
            btnUpdate.Visible = false;
            BtnSave.Visible = true;
            MultiView1.SetActiveView(View2);
            //this.programmaticModalPopup.Show();
        }

        private void Clear()
        {
            StartTimeSelector.ReadOnly = false;
            StartTimeSelector.Hour = DateTime.Now.Hour;
            StartTimeSelector.Minute = DateTime.Now.Minute;
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int lintcnt = 0;
                EntityBirthCertificate entDept = new EntityBirthCertificate();

                if (ddlPatientName.SelectedIndex == 0)
                {
                    lblMsg.Text = "Please Patient Name";
                    return;
                }
                else
                {
                    entDept.PatientAdmitID = Convert.ToInt32(ddlPatientName.SelectedValue);
                    entDept.GrandFatherName = txtGrandFather.Text;
                    entDept.ChildName = txtChildName.Text;
                    entDept.GenderID = Convert.ToInt32(ddlGender.SelectedValue);
                    entDept.BirthDate = StringExtension.ToDateTime(txtBirthDate.Text);
                    TimeSpan objTime = new TimeSpan(StartTimeSelector.Hour, StartTimeSelector.Minute, 0);
                    DateTime dt = DateTime.Now.Date;
                    dt = dt.Add(objTime);
                    entDept.BirthTime = dt;

                    entDept.Height = Convert.ToDecimal(txtHeight.Text);
                    entDept.Weight = Convert.ToDecimal(txtWeight.Text);

                    lintcnt = mobjBirthBLL.InsertBirthRecord(entDept);
                    if (lintcnt > 0)
                    {
                        GetBirthDetails();
                        lblMessage.Text = "Record Inserted Successfully....";
                        Session["update"] = Server.UrlEncode(System.DateTime.Now.ToString());
                    }
                    else
                    {
                        lblMessage.Text = "Record Not Inserted...";
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
            MultiView1.SetActiveView(View1);
        }

        void frmDepartmentMaster_Load(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected void dgvDepartment_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                //DataTable ldt = new DataTable();
                //if (e.CommandName == "EditDept")
                //{
                //    //this.programmaticModalPopupEdit.Show();
                //    int linIndex = Convert.ToInt32(e.CommandArgument);
                //    GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                //    LinkButton lnkDeptCode = (LinkButton)gvr.FindControl("lnkDeptCode");
                //    string lstrDeptCode = lnkDeptCode.Text;
                //    //txtEditDeptCode.Text = lstrDeptCode;
                //    ldt = mobjDeptBLL.Get(lstrDeptCode);
                //    FillControls(ldt);
                //}
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        private void FillControls(DataTable ldt)
        {
            //txtEditDeptDesc.Text = ldt.Rows[0]["DeptDesc"].ToString();
        }

        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            //int lintCnt = 0;
            //try
            //{
            //    EntityShift entDept = new EntityShift();
            //    //3entDept.BedAllocId = Convert.ToInt32(Session["BedAlloc_Id"]);
            //    entDept.ShiftId = Convert.ToInt32(Session["Shift_Id"]);
            //    entDept.ShiftName = txtShiftName.Text;

            //    TimeSpan objTime = new TimeSpan(StartTimeSelector.Hour, StartTimeSelector.Minute, 0);
            //    DateTime dt = DateTime.Now.Date;
            //    dt = dt.Add(objTime);
            //    entDept.StartTime = dt;

            //    TimeSpan objTime1 = new TimeSpan(EndTimeSelector.Hour, EndTimeSelector.Minute, 0);
            //    DateTime dt1 = DateTime.Now.Date;
            //    dt1 = dt1.Add(objTime1);
            //    entDept.EndTime = dt1;
            //    if (string.IsNullOrEmpty(txtShiftName.Text))
            //    {
            //        lblMsg.Text = "Please Enter Shift Name";
            //        txtShiftName.Focus();
            //        return;
            //    }

            //    if (!mobjDeptBLL.IsRecordExists(entDept))
            //    {
            //        lintCnt = mobjDeptBLL.Update(entDept);

            //        if (lintCnt > 0)
            //        {
            //            GetShiftDetail();
            //            lblMessage.Text = "Record Updated Successfully";
            //            //this.programmaticModalPopup.Hide();
            //        }
            //        else
            //        {
            //            lblMessage.Text = "Record Not Updated";
            //        }
            //    }
            //    else
            //    {
            //        lblMessage.Text = "Record Already Exist....";
            //    }
            //    MultiView1.SetActiveView(View1);
            //}
            //catch (Exception ex)
            //{
            //    Commons.FileLog("frmDepartmentMaster -  BtnEdit_Click(object sender, EventArgs e)", ex);
            //}

        }

        protected void chkDelete_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chk.NamingContainer;
            if (chk.Checked)
            {
                LinkButton DeptCode = (LinkButton)row.FindControl("lnkDeptCode");
                Session["DeptCode"] = DeptCode.Text;
                //lblMessage.Text = string.Empty;
            }
            else
            {
                Session["DeptCode"] = string.Empty;
            }
        }
        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow drv in dgvShift.Rows)
            {
                CheckBox chkDelete = (CheckBox)drv.FindControl("chkDelete");
                if (chkDelete.Checked)
                {
                    //this.modalpopupDelete.Show();
                }
            }
        }

        protected void dgvDepartment_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dgvShift.DataSource = (List<sp_GetAllBedAllocResult>)Session["DepartmentDetail"];
                dgvShift.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
        protected void dgvDepartment_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvShift.PageIndex = e.NewPageIndex;
        }
        protected void dgvDepartment_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //try
            //{
            //    if (!e.Row.Cells[0].Text.Equals("Bed Id", StringComparison.CurrentCultureIgnoreCase))
            //    {
            //        if (!e.Row.Cells[5].Text.Equals("&nbsp;", StringComparison.CurrentCultureIgnoreCase))
            //        {
            //            if (!e.Row.Cells[6].Text.Equals("&nbsp;", StringComparison.CurrentCultureIgnoreCase))
            //            {
            //                e.Row.Enabled = true;
            //                e.Row.BackColor = Color.LightGreen;
            //            }
            //            else
            //            {
            //                e.Row.Enabled = false;
            //                e.Row.BackColor = Color.LightSkyBlue;
            //            }
            //        }
            //        else
            //        {
            //            e.Row.Enabled = true;
            //            e.Row.BackColor = Color.LightGreen;
            //        }
            //    }
            //    //else
            //    //{
            //    //    if (!string.IsNullOrWhiteSpace(e.Row.Cells[5].Text) && !e.Row.Cells[5].Text.Equals("&nbsp;", StringComparison.CurrentCultureIgnoreCase) && !string.IsNullOrWhiteSpace(e.Row.Cells[6].Text) && !e.Row.Cells[6].Text.Equals("&nbsp;", StringComparison.CurrentCultureIgnoreCase))
            //    //    {
            //    //        e.Row.Enabled = true;
            //    //    }
            //    //    e.Row.Enabled = true;
            //    //}

            //    for (int i = 0; i < e.Row.Cells.Count; i++)
            //    {
            //        e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap; text-align:left;");
            //    }

            //    //if (e.Row.RowType == DataControlRowType.DataRow)
            //    //{
            //    //    e.Row.Attributes.Add("onmouseover", "SetMouseOver(this)");
            //    //    e.Row.Attributes.Add("onmouseout", "SetMouseOut(this)");
            //    //    LinkButton lnkDeptCode = (LinkButton)e.Row.FindControl("lnkDeptCode");
            //    //    CheckBox chkDelete = (CheckBox)e.Row.FindControl("chkDelete");
            //    //    if (lnkDeptCode.Text == "Admin")
            //    //    {
            //    //        lnkDeptCode.Enabled = false;
            //    //        chkDelete.Enabled = false;
            //    //    }
            //    //}
            //}
            //catch (Exception ex)
            //{
            //    Commons.FileLog("frmDepartmentMaster -  dgvData_RowDataBound(object sender, GridViewRowEventArgs e)", ex);
            //}
        }
        protected void dgvDepartment_DataBound(object sender, EventArgs e)
        {
            int lintCurrentPage = dgvShift.PageIndex + 1;
            lblPageCount.Text = "<b>Page</b> " + lintCurrentPage.ToString() + "<b> of </b>" + dgvShift.PageCount.ToString();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSearch.Text))
                {
                    BirthDetails(txtSearch.Text);
                }
                else
                {
                    lblMessage.Text = "Please Fill Search Text.";
                    txtSearch.Focus();
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        private void BirthDetails(string Prefix)
        {
            List<EntityBirthCertificate> lst = mobjBirthBLL.SelectBirthDetails(Prefix);
            try
            {
                if (lst != null)
                {
                    dgvShift.DataSource = lst;
                    dgvShift.DataBind();
                    Session["Birth_Details"] = ListConverter.ToDataTable(lst);
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtSearch.Text = string.Empty;
            GetBirthDetails();
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSearch.Text))
                {
                    Session["Details"] = Session["StartBirthDetails"];
                    Response.Redirect("~/ExcelReport/MonthwiseSalExcel.aspx", false);
                }
                else
                {
                    Session["Details"] = Session["Birth_Details"];
                    Response.Redirect("~/ExcelReport/MonthwiseSalExcel.aspx", false);
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            ImageButton imgEdit = (ImageButton)sender;
            GridViewRow row = (GridViewRow)imgEdit.NamingContainer;
            Session["Birth_ID"] = Convert.ToInt32(dgvShift.DataKeys[row.RowIndex].Value);
            Session["ReportType"] = "Birth";
            Response.Redirect("~/PathalogyReport/PathologyReport.aspx", false);
        }
    }
}