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
using System.Drawing;
using Hospital.Models;

namespace Hospital.BedStatus
{
    public partial class frmBedAllocToPatient : BasePage
    {
        BedStatusBLL mobjDeptBLL = new BedStatusBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            base.AuthenticateUser();
            if (!Page.IsPostBack)
            {
                GetBedAlloc();
                MultiView1.SetActiveView(View1);
                CalDOBDate.EndDate = DateTime.Now.Date;
                CalDOBDate.StartDate = DateTime.Now.Date.AddMonths(-5);
            }
        }

        protected void btnShiftToICU_Click(object sender, EventArgs e)
        {
            IsShift_ICU.Value = true.ToString();
            IsShift_IPD.Value = false.ToString();
            ImageButton imgEdit = (ImageButton)sender;
            GridViewRow row = (GridViewRow)imgEdit.NamingContainer;
            ICUBed_Id.Value = row.Cells[0].Text;
            int BedType = mobjDeptBLL.GetBedTye(Convert.ToInt32(row.Cells[0].Text));
            int Pat_ID = Convert.ToInt32(row.Cells[5].Text);
            EntityPatientAlloc objTxt = new PatientAllocDocBLL().GetPatientType(Pat_ID);
            CalICUAlloc.StartDate = objTxt.AdmitDate;
            ShiftPatient_ID.Value = Pat_ID.ToString();
            if (BedType == 0)
            {
                List<STP_BindICUBedForShiftingResult> lst = new ICUInvoiceBLL().GetICUBedsForShifting();
                lst.Insert(0, new STP_BindICUBedForShiftingResult() { bedid = 0, bedno = "-----Select-----" });
                ddlICUBedMaster.DataSource = lst;
                ddlICUBedMaster.DataTextField = "bedno";
                ddlICUBedMaster.DataValueField = "bedid";
                ddlICUBedMaster.DataBind();
                MultiView1.SetActiveView(View3);
            }
            else
            {
                lblMessage.Text = "Patient Is Already In ICU";
                MultiView1.SetActiveView(View1);
            }
        }



        protected void BtnICUClose_Click(object sender, EventArgs e)
        {
            MultiView1.SetActiveView(View1);
        }

        protected void BtnICUSave_Click(object sender, EventArgs e)
        {
            int lintCnt = 0;
            try
            {
                EntityBedAllocToPatient entBedAlloc = new EntityBedAllocToPatient();
                EntityBedMaster lstB = new BedStatusBLL().GetFloorRoomBed(ddlICUBedMaster.SelectedValue);

                entBedAlloc.BedId = Convert.ToInt32(ddlICUBedMaster.SelectedValue);
                entBedAlloc.FloorId = lstB.FloorNo;
                entBedAlloc.RoomId = lstB.RoomId;
                entBedAlloc.PatientId = Convert.ToInt32(ShiftPatient_ID.Value);
                entBedAlloc.ShiftDate = StringExtension.ToDateTime(txtICUAlloc.Text);
                entBedAlloc.AllocationDate = StringExtension.ToDateTime(txtICUAlloc.Text);
                if (!string.IsNullOrEmpty(IsShift_ICU.Value) && !string.IsNullOrEmpty(IsShift_IPD.Value))
                {
                    if (Convert.ToBoolean(IsShift_ICU.Value))
                    {
                        entBedAlloc.Is_ShiftTo_ICU = true;
                        entBedAlloc.ShiftBedId = Convert.ToInt32(ICUBed_Id.Value);
                    }
                    else
                    {
                        entBedAlloc.Is_ShiftTo_ICU = false;
                    }
                    if (Convert.ToBoolean(IsShift_IPD.Value))
                    {
                        entBedAlloc.Is_ShiftTo_IPD = true;
                        entBedAlloc.ShiftBedId = Convert.ToInt32(IPDBed_Id.Value);
                    }
                    else
                    {
                        entBedAlloc.Is_ShiftTo_IPD = false;
                    }

                    lintCnt = mobjDeptBLL.UpdateShiftBed(entBedAlloc);
                    if (lintCnt > 0)
                    {
                        lblMessage.Text = "Record Updated Successfully";
                    }
                    else
                    {
                        lblMessage.Text = "Record Not Updated";
                    }
                }
                else
                {
                    lblMessage.Text = "Not working";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }

            GetBedAlloc();
            MultiView1.SetActiveView(View1);
        }

        protected void BtnClose_Click(object sender, EventArgs e)
        {
            try
            {
                txtBedNo.Text = String.Empty;
                txtFloorName.Text = String.Empty;
                GetBedAlloc();
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
                lblMessage.Text = string.Empty;
                ImageButton imgEdit = (ImageButton)sender;
                GridViewRow row = (GridViewRow)imgEdit.NamingContainer;
                Bed_Id.Value = row.Cells[0].Text;
                EntityBedMaster lstB = new BedStatusBLL().GetFloorRoomBed(Convert.ToInt32(Bed_Id.Value));
                Floor_Id.Value = lstB.FloorNo.ToString();
                Room_Id.Value = lstB.RoomId.ToString();
                txtBedNo.Text = Convert.ToString(row.Cells[1].Text);
                txtFloorName.Text = Convert.ToString(row.Cells[3].Text);
                if (!Convert.ToString(row.Cells[5].Text).Equals("&nbsp;", StringComparison.CurrentCultureIgnoreCase))
                {
                    txtAllocDate.Text = DateTime.UtcNow.Date.ToString("dd/MM/yyyy");
                }
                else
                {
                    txtAllocDate.Text = DateTime.UtcNow.Date.ToString("dd/MM/yyyy");
                }
                txtRoomNo.Text = Convert.ToString(row.Cells[2].Text);
                GetBedAlloc();
                GetPatientList();
                //txtAllocDate.Enabled = false;
                btnUpdate.Visible = true;
                BtnSave.Visible = false;
                MultiView1.SetActiveView(View2);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        private void GetPatientList()
        {
            try
            {
                
                List<EntityPatientMaster> tblCat = new BedStatusBLL().GetAllPatient();
                if (tblCat!=null)
                {
                    if (SessionManager.Instance.LoginUser.UserType.ToLower() == "doctor")
                    {
                        tblCat = tblCat.Where(p => p.DeptDoctorId == SessionManager.Instance.LoginUser.PKId).ToList();
                    }
                    tblCat.Insert(0, new EntityPatientMaster() { AdmitId=0,FullName="---Select---" });
                    ddlPatientName.DataSource = tblCat;
                    ddlPatientName.DataValueField = "AdmitId";
                    ddlPatientName.DataTextField = "FullName";
                    ddlPatientName.DataBind();
                }
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
            txtFloorName.Text = string.Empty;
            txtBedNo.Text = string.Empty;
            txtRoomNo.Text = string.Empty;
            btnUpdate.Visible = false;
            BtnSave.Visible = true;
            MultiView1.SetActiveView(View2);
        }

        private void GetBedAlloc()
        {
            try
            {
                List<sp_GetAllBedAllocResult> ldtDept = new BedStatusBLL().GetAllBedAlloc();
                if (ldtDept.Count > 0)
                {
                    dgvDepartment.DataSource = ldtDept;
                    dgvDepartment.DataBind();
                    //Session["DepartmentDetail"] = ldtDept;
                    int lintRowcount = ldtDept.Count;
                    lblRowCount.Text = "<b>Total Records:</b> " + lintRowcount.ToString();
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            int lintCnt = 0;
            try
            {
                EntityBedAllocToPatient entDept = new EntityBedAllocToPatient();
                entDept.BedId = Convert.ToInt32(Bed_Id.Value);
                entDept.FloorId = Convert.ToInt32(Floor_Id.Value);
                entDept.RoomId = Convert.ToInt32(Room_Id.Value);
                entDept.PatientId = Convert.ToInt32(Pat_Id.Value);
                if (!string.IsNullOrEmpty(txtAllocDate.Text))
                {
                    entDept.AllocationDate = StringExtension.ToDateTime(txtAllocDate.Text);
                }
                else
                {
                    entDept.AllocationDate = null;
                }
                PatientMasterBLL mobjPatient = new PatientMasterBLL();
                EntityPatientAdmit entAdmit = new EntityPatientAdmit() { PatientId = Convert.ToInt32(ddlPatientName.SelectedValue) };
                bool Status = mobjPatient.CheckPatientAlreadyAllocated(entAdmit);
                if (!Status)
                {
                    lintCnt = mobjDeptBLL.InsertAllocBed(entDept);
                    if (lintCnt > 0)
                    {
                        lblMessage.Text = "Record Updated Successfully";
                    }
                    else
                    {
                        lblMessage.Text = "Record Not Updated";
                    }
                }
                else
                {
                    lblMessage.Text = "This Patient Already Allocated to Another Bed";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
            GetBedAlloc();
            MultiView1.SetActiveView(View1);
        }

        protected void dgvDepartment_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                List<sp_GetAllBedAllocResult> ldtDept = new BedStatusBLL().GetAllBedAlloc();
                if (!string.IsNullOrEmpty(txtSearch.Text))
                {
                    ldtDept = ldtDept.Where(p => p.RoomNo.Contains(txtSearch.Text) || p.BedNo.Contains(txtSearch.Text) || p.FloorName.Contains(txtSearch.Text)).ToList();
                }
                dgvDepartment.DataSource = ldtDept;// (List<sp_GetAllBedAllocResult>)Session["DepartmentDetail"];
                dgvDepartment.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
        protected void dgvDepartment_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvDepartment.PageIndex = e.NewPageIndex;
        }

        protected void dgvDepartment_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (e.Row.Cells.Count > 1)
                    {
                        if (!e.Row.Cells[1].Text.Equals("Bed Id", StringComparison.CurrentCultureIgnoreCase))
                        {
                            if (e.Row.Cells.Count > 6)
                            {
                                if (!e.Row.Cells[6].Text.Equals("&nbsp;", StringComparison.CurrentCultureIgnoreCase))
                                {
                                    e.Row.Cells[9].Enabled = false;
                                    e.Row.Cells[8].Enabled = true;
                                    e.Row.Cells[7].Enabled = true;
                                    e.Row.BackColor = Color.LightSkyBlue;
                                }
                                else
                                {
                                    e.Row.Cells[9].Enabled = true;
                                    e.Row.Cells[8].Enabled = false;
                                    e.Row.Cells[7].Enabled = false;
                                    e.Row.BackColor = Color.GhostWhite;
                                }
                            }
                        }
                    }
                }
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        try
                        {
                            if (e.Row.Cells.Count < i)
                            {
                                e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap; text-align:left;");
                            }
                        }
                        catch (Exception ex)
                        {
                            lblMessage.Text = ex.Message;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void btnShiftToIPD_Click(object sender, EventArgs e)
        {
            IsShift_IPD.Value = true.ToString();
            IsShift_ICU.Value = false.ToString();
            ImageButton imgEdit = (ImageButton)sender;
            GridViewRow row = (GridViewRow)imgEdit.NamingContainer;
            IPDBed_Id.Value = row.Cells[0].Text;
            int BedType = mobjDeptBLL.GetBedTye(Convert.ToInt32(row.Cells[0].Text));
            int Pat_ID = Convert.ToInt32(row.Cells[5].Text);
            ShiftPatient_ID.Value = row.Cells[5].Text;
            if (BedType > 0)
            {
                List<STP_BindIPDBedForShiftingResult> lst = new ICUInvoiceBLL().GetIPDBeds();
                lst.Insert(0, new STP_BindIPDBedForShiftingResult() { bedid = 0, bedno = "-----Select-----" });
                ddlICUBedMaster.DataSource = lst;
                ddlICUBedMaster.DataTextField = "bedno";
                ddlICUBedMaster.DataValueField = "bedid";
                ddlICUBedMaster.DataBind();
                MultiView1.SetActiveView(View3);
            }
            else
            {
                lblMessage.Text = "Patient Is Already In IPD";
                MultiView1.SetActiveView(View1);
            }
        }

        protected void ImageChange_Click(object sender, EventArgs e)
        {
            try
            {
                IsShift_IPD.Value = true.ToString();
                IsShift_ICU .Value= false.ToString();
                ImageButton imgEdit = (ImageButton)sender;
                GridViewRow row = (GridViewRow)imgEdit.NamingContainer;
                IPDBed_Id.Value = row.Cells[0].Text;

                if (!string.IsNullOrEmpty(row.Cells[6].Text))
                {
                    DateTime dt = StringExtension.ToDateTime(row.Cells[6].Text);
                    CalICUAlloc.StartDate = dt;
                }

                int Pat_ID = Convert.ToInt32(row.Cells[5].Text);
                ShiftPatient_ID.Value = row.Cells[5].Text;
                EntityRoomCategory objCat = mobjDeptBLL.GetRoomCategoryByBedID(Convert.ToInt32(row.Cells[0].Text));
                if (objCat != null)
                {
                    if (objCat.IsICU)
                    {
                        List<STP_BindICUBedForShiftingResult> lst = new ICUInvoiceBLL().GetICUBedsForShifting();
                        lst.Insert(0, new STP_BindICUBedForShiftingResult() { bedid = 0, bedno = "-----Select-----" });
                        ddlICUBedMaster.DataSource = lst;
                        ddlICUBedMaster.DataTextField = "bedno";
                        ddlICUBedMaster.DataValueField = "bedid";
                        ddlICUBedMaster.DataBind();
                    }
                    else
                    {
                        List<STP_BindIPDBedForShiftingResult> lst = new ICUInvoiceBLL().GetIPDBeds();
                        lst.Insert(0, new STP_BindIPDBedForShiftingResult() { bedid = 0, bedno = "-----Select-----" });
                        ddlICUBedMaster.DataSource = lst;
                        ddlICUBedMaster.DataTextField = "bedno";
                        ddlICUBedMaster.DataValueField = "bedid";
                        ddlICUBedMaster.DataBind();
                    }
                }

                MultiView1.SetActiveView(View3);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void ddlPatientName_SelectedIndexChanged1(object sender, EventArgs e)
        {
            try
            {
                if (ddlPatientName.SelectedIndex > 0)
                {
                    Pat_Id.Value = ddlPatientName.SelectedValue;
                    EntityPatientAlloc objTxt = new PatientAllocDocBLL().GetPatientType(Convert.ToInt32(ddlPatientName.SelectedValue));
                    CalDOBDate.StartDate = objTxt.AdmitDate;
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }

        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {

        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSearch.Text))
                {
                    SearchBedAllocDetails(txtSearch.Text);
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

        private void SearchBedAllocDetails(string Prefix)
        {
            try
            {
                List<sp_GetAllBedAllocResult> lst = mobjDeptBLL.SelectBedAlloc(Prefix);
                if (lst != null)
                {
                    dgvDepartment.DataSource = lst;
                    dgvDepartment.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }

        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                txtSearch.Text = string.Empty;
                GetBedAlloc();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
    }
}