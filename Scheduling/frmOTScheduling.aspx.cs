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

namespace Hospital.Scheduling
{
    public partial class frmOTScheduling : System.Web.UI.Page
    {
        OTScheduleBLL mobjDeptBLL = new OTScheduleBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (SessionManager.Instance.LoginUser != null)
            {
                if (!Page.IsPostBack)
                {
                    //SessionManager.Instance.SetSession();
                    GetBedAllocOT();
                    // BindDoctors();
                    BindAnaesthetist();
                    BindNurse();
                    MultiView1.SetActiveView(View1);
                }
            }
            else
            {
                Response.Redirect("~/frmlogin.aspx", false);
            }
        }

        private void BindNurse()
        {
            try
            {
                DataTable tblCat = new NurseBLL().GetAllReligion();
                DataRow dr = tblCat.NewRow();
                dr["PKId"] = 0;
                dr["NurseName"] = "---Select---";
                tblCat.Rows.InsertAt(dr, 0);
                ddlAssistant.DataSource = tblCat;
                ddlAssistant.DataValueField = "PKId";
                ddlAssistant.DataTextField = "NurseName";
                ddlAssistant.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        private void BindAnaesthetist()
        {
            try
            {
                DataTable tblCat = new AnaesthetistBLL().GetAllReligion();
                DataRow dr = tblCat.NewRow();
                dr["PKId"] = 0;
                dr["AnaesthetistName"] = "---Select---";
                tblCat.Rows.InsertAt(dr, 0);
                ddlAnesthatic.DataSource = tblCat;
                ddlAnesthatic.DataValueField = "PKId";
                ddlAnesthatic.DataTextField = "AnaesthetistName";
                ddlAnesthatic.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        private void BindOperations()
        {
            try
            {
                OpeartionMasterBLL objOper = new OpeartionMasterBLL();
                List<EntityOperationMaster> lst = (from tbl in objOper.GetAllOperationDetails()
                                                   where tbl.OperationCategoryId.Equals(Convert.ToInt32(ddlOperationName.SelectedValue))
                                                   select tbl).ToList();
                lst.Insert(0, new EntityOperationMaster() { OperationId = 0, OperationName = "------Select-----" });
                ddlOper.DataSource = lst;
                ddlOper.DataTextField = "OperationName";
                ddlOper.DataValueField = "OperationId";
                ddlOper.DataBind();
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void ddlOper_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                OpeartionMasterBLL objOper = new OpeartionMasterBLL();
                EntityOperationMaster objOpercharges = objOper.SelectOperation(Convert.ToInt32(ddlOper.SelectedValue));
                if (objOpercharges != null)
                {
                    txtOperationCharges.Text = Convert.ToString(objOpercharges.Price);
                }
                else
                {
                    txtOperationCharges.Text = string.Empty;
                }
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
                Clear();
                GetBedAllocOT();
                MultiView1.SetActiveView(View1);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        private void Clear()
        {
            txtFloorName.Text = string.Empty;
            txtRoomNo.Text = string.Empty;
            txtBedNo.Text = string.Empty;
            txtAllocTime.Text = string.Empty;
            txtDischargeTime.Text = string.Empty;
            ddlPatientName.SelectedIndex = 0;
            txtTypeOfAnaesthetist.Text = string.Empty;
            txtSurgeon.Text = string.Empty;
            txtAnaestheticNote.Text = string.Empty;
            txtSurgeryNote.Text = string.Empty;
            ddlAnesthatic.SelectedIndex = 0;
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton imgEdit = (ImageButton)sender;
                GridViewRow row = (GridViewRow)imgEdit.NamingContainer;
                ddlOperationName_SelectedIndexChanged(sender, e);
                Session["Bed_Id"] = Convert.ToInt32(row.Cells[0].Text);
                EntityBedMaster lstB = new BedStatusBLL().GetFloorRoomBed(Convert.ToInt32(Session["Bed_Id"]));
                Session["Floor_Id"] = lstB.FloorNo;
                Session["Room_Id"] = lstB.RoomId;
                txtBedNo.Text = Convert.ToString(row.Cells[1].Text);
                txtFloorName.Text = Convert.ToString(row.Cells[3].Text);
                //if (!Convert.ToString(row.Cells[7].Text).Equals("&nbsp;", StringComparison.CurrentCultureIgnoreCase))
                //{
                //    txtAllocTime.Text = string.Empty;
                //    StartTimeSelector.Hour = DateTime.Now.Hour;
                //    StartTimeSelector.Minute = DateTime.Now.Minute;
                //}
                //else
                //{
                //    txtAllocTime.Text = string.Empty;
                //    StartTimeSelector.Hour = DateTime.Now.Hour;
                //    StartTimeSelector.Minute = DateTime.Now.Minute;
                //}
                //if (!Convert.ToString(row.Cells[8].Text).Equals("&nbsp;", StringComparison.CurrentCultureIgnoreCase))
                //{
                //    txtDischargeTime.Text = string.Empty;
                //    EndTimeSelector.Hour = DateTime.Now.Hour;
                //    EndTimeSelector.Minute = DateTime.Now.Minute;
                //}
                //else
                //{
                //    EndTimeSelector.Hour = DateTime.Now.Hour;
                //    EndTimeSelector.Minute = DateTime.Now.Minute;
                //    txtDischargeTime.Text = string.Empty;
                //}
                txtRoomNo.Text = Convert.ToString(row.Cells[2].Text);
                GetBedAllocOT();
                BindCategory();
                GetPatientList();
                btnUpdate.Visible = true;
                BtnSave.Visible = false;
                MultiView1.SetActiveView(View2);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        private void BindCategory()
        {
            try
            {
                DataTable tblCat = new OpeartionMasterBLL().GetAllCategoryName();
                DataRow dr = tblCat.NewRow();
                dr["CategoryId"] = 0;
                dr["CategoryName"] = "---Select---";
                tblCat.Rows.InsertAt(dr, 0);
                ddlOperationName.DataSource = tblCat;
                ddlOperationName.DataValueField = "CategoryId";
                ddlOperationName.DataTextField = "CategoryName";
                ddlOperationName.DataBind();
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
                    tblCat.Insert(0, new EntityPatientMaster() { AdmitId=0,FullName="---Select---"});
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

        private void GetBedAllocOT()
        {
            try
            {
                List<sp_GetAllBedAllocOTResult> ldtDept = new OTScheduleBLL().GetAllBedAllocOT();
                if (ldtDept.Count > 0)
                {
                    dgvDepartment.DataSource = ldtDept;
                    dgvDepartment.DataBind();
                    Session["DepartmentDetail"] = ldtDept;
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
                EntityOTBedAlloc entDept = new EntityOTBedAlloc();
                entDept.BedId = Convert.ToInt32(Session["Bed_Id"]);
                entDept.FloorId = Convert.ToInt32(Session["Floor_Id"]);
                entDept.RoomId = Convert.ToInt32(Session["Room_Id"]);
                entDept.PatientId = Convert.ToInt32(Session["Pat_Id"]);
                entDept.DocId = Convert.ToInt32(Session["Doc_Id"]);
                entDept.OperCatId = Convert.ToInt32(Session["Opera_Id"]);
                entDept.OperationId = Convert.ToInt32(ddlOper.SelectedValue);
                entDept.TypeOfAnaesthetist = txtTypeOfAnaesthetist.Text;
                entDept.Surgeon = txtSurgeon.Text;
                entDept.Implant = txtImplant.Text;
                entDept.MaterialHPE = txtMaterialHPE.Text;
                entDept.SurgeryNote = txtSurgeryNote.Text;
                entDept.AnaestheticNote = txtAnaestheticNote.Text;
                if (!string.IsNullOrEmpty(txtAllocTime.Text))
                {
                    TimeSpan objTime = new TimeSpan(StartTimeSelector.Hour, StartTimeSelector.Minute, 0);
                    DateTime dt = StringExtension.ToDateTime(txtAllocTime.Text);
                    dt = dt.Add(objTime);
                    entDept.AllocationDate = dt;
                }
                else
                {
                    entDept.AllocationDate = null;
                }
                if (!string.IsNullOrEmpty(txtDischargeTime.Text))
                {
                    TimeSpan objTime = new TimeSpan(EndTimeSelector.Hour, EndTimeSelector.Minute, 0);
                    DateTime dt = StringExtension.ToDateTime(txtDischargeTime.Text);
                    dt = dt.Add(objTime);
                    entDept.DischargeDate = dt;
                }
                else
                {
                    entDept.DischargeDate = null;
                }
                entDept.AnestheticId = Convert.ToInt32(ddlAnesthatic.SelectedValue);
                entDept.AssistantId = Convert.ToInt32(ddlAssistant.SelectedValue);
                lintCnt = mobjDeptBLL.InsertOTAllocBed(entDept);

                if (lintCnt > 0)
                {
                    GetBedAllocOT();
                    lblMessage.Text = "Record Updated Successfully";
                }
                else
                {
                    lblMessage.Text = "Record Not Updated";
                }
                MultiView1.SetActiveView(View1);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void dgvDepartment_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dgvDepartment.DataSource = (List<sp_GetAllBedAllocResult>)Session["DepartmentDetail"];
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
                if (!e.Row.Cells[0].Text.Equals("Bed Id", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (e.Row.Cells.Count > 7)
                    {
                        if (!e.Row.Cells[7].Text.Equals("&nbsp;", StringComparison.CurrentCultureIgnoreCase))
                        {
                            if (StringExtension.ToDateTime(e.Row.Cells[8].Text).CompareTo(DateTime.Now) >= -1)
                            {
                                e.Row.Cells[4].Text = string.Empty;
                                e.Row.Cells[5].Text = string.Empty;
                                e.Row.Cells[6].Text = string.Empty;
                                e.Row.Cells[7].Text = string.Empty;
                                e.Row.Cells[8].Text = string.Empty;
                                e.Row.Enabled = true;
                                e.Row.BackColor = Color.FloralWhite;
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
                            e.Row.BackColor = Color.FloralWhite;
                        }
                    }
                    else
                    {
                        e.Row.Enabled = true;
                        e.Row.BackColor = Color.FloralWhite;
                    }
                }

                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap; text-align:left;");
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
        protected void dgvDepartment_DataBound(object sender, EventArgs e)
        {
            int lintCurrentPage = dgvDepartment.PageIndex + 1;
            lblPageCount.Text = "<b>Page</b> " + lintCurrentPage.ToString() + "<b> of </b>" + dgvDepartment.PageCount.ToString();
        }


        protected void ddlPatientName_SelectedIndexChanged1(object sender, EventArgs e)
        {
            try
            {
                if (ddlPatientName.SelectedIndex > 0)
                {
                    Session["Pat_Id"] = Convert.ToInt32(ddlPatientName.SelectedValue);
                    EntityPatientAlloc objTxt = new PatientAllocDocBLL().GetPatientType(Convert.ToInt32(ddlPatientName.SelectedValue));
                    CalAllocTime.StartDate = objTxt.AdmitDate;
                    CalDischargeTime.StartDate = objTxt.AdmitDate;
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }
        protected void ddlOperationName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlOperationName.SelectedIndex > 0)
                {
                    BindOperations();
                    Session["Opera_Id"] = Convert.ToInt32(ddlOperationName.SelectedValue);
                    GetDocName(Convert.ToInt32(Session["Opera_Id"]));
                }
                else
                {
                    ddlOper.DataSource = new List<EntityOperationMaster>();
                    ddlOper.DataBind();
                    ddlDocName.DataSource = new List<EntityDocCategory>();
                    ddlDocName.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        //public void BindDoctors()
        //{
        //    try
        //    {
        //        List<EntityDocCategory> lst = mobjDeptBLL.GetAllDoctors();
        //        if (lst.Count > 0)
        //        {

        //            lst.Insert(0, new EntityDocCategory() { DocId = 0, Doc_Name = "--Select--" });
        //            ddlAssistant.DataSource = lst;
        //            ddlAssistant.DataValueField = "DocId";
        //            ddlAssistant.DataTextField = "Doc_Name";
        //            ddlAssistant.DataBind();

        //            ddlAnesthatic.DataSource = lst;
        //            //lst.Insert(0, new EntityDocCategory() { DocId = 0, Doc_Name = "--Select--" });
        //            ddlAnesthatic.DataValueField = "DocId";
        //            ddlAnesthatic.DataTextField = "Doc_Name";
        //            ddlAnesthatic.DataBind();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        lblMessage.Text=ex.Message;
        //    }
        //}

        protected void ddlDocName_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["Doc_Id"] = Convert.ToInt32(ddlDocName.SelectedValue);
        }

        private void GetDocName(int OperaId)
        {
            try
            {
                List<EntityDocCategory> lst = mobjDeptBLL.GetAllDoctors(OperaId);
                if (lst.Count > 0)
                {
                    ddlDocName.DataSource = lst;
                    lst.Insert(0, new EntityDocCategory() { DocId = 0, Doc_Name = "--Select--" });
                    ddlDocName.DataValueField = "DocId";
                    ddlDocName.DataTextField = "Doc_Name";
                    ddlDocName.DataBind();
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


        protected void ddlAnesthatic_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                AnaesthetistBLL objOper = new AnaesthetistBLL();
                EntityAnaesthetist objOpercharges = objOper.SelectOperation(Convert.ToInt32(ddlAnesthatic.SelectedValue));
                if (objOpercharges != null)
                {
                    txtTypeOfAnaesthetist.Text = Convert.ToString(objOpercharges.TypeOfAnaesthetist);
                }
                else
                {
                    txtTypeOfAnaesthetist.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }

        }
    }
}