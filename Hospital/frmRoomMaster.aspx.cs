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
using Hospital.Models;

namespace Hospital
{
    public partial class frmRoomMaster : BasePage
    {
        RoomMasterBLL mobjRoomMasterBLL = new RoomMasterBLL();


        protected void Page_Load(object sender, EventArgs e)
        {
            base.AuthenticateUser();
            if (!Page.IsPostBack)
            {
                update.Value = Server.UrlEncode(System.DateTime.Now.ToString());
                GetRooms();
                BindCategory();
                BindFloor();
                MultiView1.SetActiveView(View1);
            }
        }

        private void BindFloor()
        {
            try
            {
                DataTable tblCat = new RoomMasterBLL().GetAllFloor();
                DataRow dr = tblCat.NewRow();
                dr["FloorId"] = 0;
                dr["FloorName"] = "---Select---";
                tblCat.Rows.InsertAt(dr, 0);

                ddlFloorNo.DataSource = tblCat;
                ddlFloorNo.DataValueField = "FloorId";
                ddlFloorNo.DataTextField = "FloorName";
                ddlFloorNo.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        private void GetRooms()
        {
            var ldtRoom = mobjRoomMasterBLL.GetAllRoomDetails();

            if (ldtRoom.Count > 0 && ldtRoom != null)
            {
                dgvRoomMaster.DataSource = ldtRoom;
                dgvRoomMaster.DataBind();
                int lintRowcount = ldtRoom.Count;
                lblRowCount.Text = "<b>Total Records:</b> " + lintRowcount.ToString();
                pnlShow.Style.Add(HtmlTextWriterStyle.Display, "");
                //hdnPanel.Value = "";
            }
            else
            {
                //pnlShow.Style.Add(HtmlTextWriterStyle.Display, "none");
                //hdnPanel.Value = "none";
            }
        }

        private void BindCategory()
        {
            try
            {
                DataTable tblCat = new RoomMasterBLL().GetAllCategory();
                DataRow dr = tblCat.NewRow();
                dr["PKId"] = 0;
                dr["CategoryDesc"] = "---Select---";
                tblCat.Rows.InsertAt(dr, 0);

                ddlCategory.DataSource = tblCat;
                ddlCategory.DataValueField = "PKId";
                ddlCategory.DataTextField = "CategoryDesc";
                ddlCategory.DataBind();
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
        protected void BtnAddNewRoom_Click(object sender, EventArgs e)
        {
            DataTable ldt = new DataTable();
            BtnSave.Visible = true;
            btnUpdate.Visible = false;
            MultiView1.SetActiveView(View2);
        }
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            int lintcnt = 0;
            EnitiyRoomMaster entRoom = new EnitiyRoomMaster();
            if (update.Value.ToString() == ViewState["update"].ToString())
            {
                if (string.IsNullOrEmpty(txtRoomNo.Text.Trim()))
                {
                    lblMsg.Text = "Please Enter Room Number";
                }
                else
                {
                    if (ddlCategory.SelectedIndex == 0)
                    {
                        lblMsg.Text = "Please Select Room Category";
                    }
                    else
                    {
                        if (ddlFloorNo.SelectedIndex == 0)
                        {
                            lblMsg.Text = "Please Select Floor Number";
                        }
                        else
                        {
                            entRoom.RoomNo = txtRoomNo.Text;
                            entRoom.FloorNo = Convert.ToInt32(ddlFloorNo.SelectedValue);
                            entRoom.CategoryId = Convert.ToInt32(ddlCategory.SelectedValue);

                            if (!Commons.IsRecordExists("tblRoomMaster", "RoomNo", Convert.ToString(entRoom.RoomNo)))
                            {
                                lintcnt = mobjRoomMasterBLL.InsertRoom(entRoom);

                                if (lintcnt > 0)
                                {
                                    GetRooms();
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
                            Clear();
                            MultiView1.SetActiveView(View1);
                        }
                    }
                }
            }
            else
            {

            }
        }

        public void Clear()
        {
            txtRoomNo.Text = string.Empty;
            ddlCategory.SelectedIndex = 0;
            ddlFloorNo.SelectedIndex = 0;
        }
        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            int lintCnt = 0;
            try
            {
                EnitiyRoomMaster entRoom = new EnitiyRoomMaster();
                //entRoom.RoomId = Convert.ToInt32(txtRoomId.Text);
                entRoom.RoomNo = txtRoomNo.Text;
                entRoom.FloorNo = Convert.ToInt32(ddlFloorNo.SelectedIndex);
                entRoom.CategoryId = Convert.ToInt32(ddlCategory.SelectedIndex);
                RoomMasterBLL objOtherAcc = new RoomMasterBLL();
                tblRoomMaster objTOrg = objOtherAcc.GetRoomByNameAndId(Convert.ToString(txtRoomNo.Text.Trim()), Convert.ToInt32(id.Value));
                if (objTOrg != null)
                {
                    tblRoomMaster objT = new tblRoomMaster();
                    objT.RoomId = Convert.ToInt32(id.Value);
                    objT.RoomNo = txtRoomNo.Text;
                    objT.FloorNo = Convert.ToInt32(ddlFloorNo.SelectedValue);
                    objT.CategoryId = Convert.ToInt32(ddlCategory.SelectedValue);

                    if (objOtherAcc != null)
                    {
                        objOtherAcc.Update(objT);
                        lblMessage.Text = "Record Updated";
                        GetRooms();
                    }
                }
                else
                {
                    objTOrg = objOtherAcc.GetRoomByName(Convert.ToString(txtRoomNo.Text.Trim()));
                    if (objTOrg == null)
                    {
                        tblRoomMaster objT = new tblRoomMaster();
                        objT.RoomId = Convert.ToInt32(id.Value);
                        objT.RoomNo = txtRoomNo.Text;
                        objT.FloorNo = Convert.ToInt32(ddlFloorNo.SelectedValue);
                        objT.CategoryId = Convert.ToInt32(ddlCategory.SelectedValue);

                        if (objOtherAcc != null)
                        {
                            objOtherAcc.Update(objT);
                            lblMessage.Text = "Record Updated";
                            GetRooms();
                        }
                    }
                    else
                    {
                        lblMessage.Text = "Room Number Already Exist";
                    }
                }
                MultiView1.SetActiveView(View1);
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmDepartmentMaster -  BtnEdit_Click(object sender, EventArgs e)", ex);
            }
        }


        protected void BtnClose_Click(object sender, EventArgs e)
        {
            try
            {
                Clear();
                GetRooms();
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
                GridViewRow cnt = (GridViewRow)imgEdit.NamingContainer;
                id.Value = Convert.ToString(cnt.Cells[0].Text);
                DataTable tblEmp = new RoomMasterBLL().SelectRoomForEdit(Convert.ToInt32(cnt.Cells[0].Text));
                if (tblEmp.Rows.Count > 0)
                {
                    txtRoomNo.Text = Convert.ToString(tblEmp.Rows[0]["RoomNo"]);
                    ListItem item = ddlCategory.Items.FindByText(Convert.ToString(tblEmp.Rows[0]["CategoryDesc"]));
                    ddlCategory.SelectedValue = item.Value;
                    ListItem Desig = ddlFloorNo.Items.FindByText(Convert.ToString(tblEmp.Rows[0]["FloorName"]));
                    ddlFloorNo.SelectedValue = Desig.Value;
                    btnUpdate.Visible = true;
                    BtnSave.Visible = false;
                    MultiView1.SetActiveView(View2);
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
        protected void dgvRoomMaster_DataBound(object sender, EventArgs e)
        {
            int lintCurrentPage = dgvRoomMaster.PageIndex + 1;
            lblPageCount.Text = "<b>Page</b> " + lintCurrentPage.ToString() + "<b> of </b>" + dgvRoomMaster.PageCount.ToString();
        }
        protected void dgvRoomMaster_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var ldtRoom = mobjRoomMasterBLL.GetAllRoomDetails();
                dgvRoomMaster.DataSource = ldtRoom;
                dgvRoomMaster.DataBind();
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmRoomMaster - dgvRoomMaster_PageIndexChanged(object sender, EventArgs e)", ex);
            }
        }
        protected void dgvRoomMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvRoomMaster.PageIndex = e.NewPageIndex;
        }
        protected void dgvRoomMaster_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                DataTable ldt = new DataTable();
                if (e.CommandName == "EditDept")
                {
                    int linIndex = Convert.ToInt32(e.CommandArgument);
                    GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                    LinkButton lnkRoomId = (LinkButton)gvr.FindControl("lnkRoomId");
                    string lstrRoomId = lnkRoomId.Text;
                    //txtEditDeptCode.Text = lstrDeptCode;
                    ldt = mobjRoomMasterBLL.GetRoomForEdit(lstrRoomId);
                    FillControls(ldt);
                }
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmDepartmentMaster -  dgvDepartment_RowCommand(object sender, GridViewCommandEventArgs e)", ex);
            }
        }

        private void FillControls(DataTable ldt)
        {

        }
        protected void dgvRoomMaster_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].Attributes.Add("style", "white-space:nowrap; text-align:left;");
                }

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Attributes.Add("onmouseover", "SetMouseOver(this)");
                    e.Row.Attributes.Add("onmouseout", "SetMouseOut(this)");
                    LinkButton lnkRoomId = (LinkButton)e.Row.FindControl("lnkRoomId");
                    CheckBox chkDelete = (CheckBox)e.Row.FindControl("chkDelete");
                    if (lnkRoomId.Text == "Admin")
                    {
                        lnkRoomId.Enabled = false;
                        chkDelete.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmDepartmentMaster -  dgvData_RowDataBound(object sender, GridViewRowEventArgs e)", ex);
            }
        }
    }
}