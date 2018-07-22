﻿using System;
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
    public partial class frmBedMaster : BasePage
    {
        BedMasterBLL mobjBedMaster = new BedMasterBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            base.AuthenticateUser("frmBedMaster.aspx");
            if (!Page.IsPostBack)
            {
                BindRoomNo();
                GetBedMasters();
                MultiView1.SetActiveView(View1);
            }
        }

        private void BindRoomNo()
        {
            try
            {
                DataTable tblCat = new BedMasterBLL().GetAllRooms();
                DataRow dr = tblCat.NewRow();
                dr["RoomId"] = 0;
                dr["RoomNo"] = "---Select---";
                tblCat.Rows.InsertAt(dr, 0);

                ddlRoomNo.DataSource = tblCat;
                ddlRoomNo.DataValueField = "RoomId";
                ddlRoomNo.DataTextField = "RoomNo";
                ddlRoomNo.DataBind();
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        private void GetBedMasters()
        {
            try
            {
                var ldtDept = mobjBedMaster.GetAllBedMasters();
                if (ldtDept.Count>0 && ldtDept != null)
                {
                    dgvBedMaster.DataSource = ldtDept;
                    dgvBedMaster.DataBind();
                    //Session["bedmasterdetail"] = ldtDept;
                    int lintrowcount = ldtDept.Count;
                    lblRowCount.Text = "<b>Total Records:</b> " + lintrowcount.ToString();
                    pnlShow.Style.Add(HtmlTextWriterStyle.Display, "");
                    hdnPanel.Value = "";
                }
                else
                {
                    pnlShow.Style.Add(HtmlTextWriterStyle.Display, "none");
                    hdnPanel.Value = "none";
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
                EntityBedMaster entBedMaster = new EntityBedMaster();
                int i = Convert.ToInt32(Room_Id.Value);
                entBedMaster.BedId = Convert.ToInt32(Session["id"]);
                entBedMaster.BedNo = txtBedNo.Text;
                if (ddlRoomNo.SelectedIndex == i)
                {
                    entBedMaster.FloorNo = Convert.ToInt32(Floor_No.Value);
                    entBedMaster.CategoryId = Convert.ToInt32(Cat_id.Value);
                }
                else
                {
                    entBedMaster.FloorNo = Convert.ToInt32(Floor_No.Value);
                    entBedMaster.CategoryId = Convert.ToInt32(CategoryId.Value);
                }

                entBedMaster.RoomId = Convert.ToInt32(ddlRoomNo.SelectedValue);
                if (!Commons.IsRecordExists("tblBedMaster", "BedNo", Convert.ToString(entBedMaster.BedNo)))
                {
                    lintCnt = mobjBedMaster.UpdateBedMaster(entBedMaster);
                    if (lintCnt > 0)
                    {
                        GetBedMasters();
                        lblMessage.Text = "Record Updated Successfully";
                    }
                    else
                    {
                        lblMessage.Text = "Record Not Updated";
                    }
                }
                else
                {
                    lblMessage.Text = "Record Already Exist";
                }

                Clear();

            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
            MultiView1.SetActiveView(View1);
        }




        protected void dgvBedMaster_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var ldtDept = mobjBedMaster.GetAllBedMasters();
                dgvBedMaster.DataSource = ldtDept;// (DataTable)Session["BedMasterDetail"];
                dgvBedMaster.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void dgvBedMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvBedMaster.PageIndex = e.NewPageIndex;
        }

        protected void dgvBedMaster_DataBound(object sender, EventArgs e)
        {
            int lintCurrentPage = dgvBedMaster.PageIndex + 1;
            lblPageCount.Text = "<b>Page</b> " + lintCurrentPage.ToString() + "<b> of </b>" + dgvBedMaster.PageCount.ToString();
        }

        protected void dgvBedMaster_RowDataBound(object sender, GridViewRowEventArgs e)
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
                }

            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int lintcnt = 0;
                EntityBedMaster entBedMaster = new EntityBedMaster();
                if (ddlRoomNo.SelectedIndex == 0)
                {
                    lblMsg.Text = "Please Select Room Number";
                }
                else
                {
                    if (string.IsNullOrEmpty(txtBedNo.Text))
                    {
                        lblMsg.Text = "Please Enter Bed Number";
                    }
                    else
                    {
                        entBedMaster.CategoryId = Convert.ToInt32(CategoryId.Value);
                        entBedMaster.FloorNo = Convert.ToInt32(FloorNo.Value);
                        entBedMaster.RoomId = Convert.ToInt32(ddlRoomNo.SelectedValue);
                        entBedMaster.BedNo = txtBedNo.Text;

                        if (!Commons.IsRecordExists("tblBedMaster", "BedNo", Convert.ToString(entBedMaster.BedNo)))//
                        {

                            lintcnt = mobjBedMaster.InsertBedMaster(entBedMaster);

                            if (lintcnt > 0)
                            {
                                GetBedMasters();
                                lblMessage.Text = "Record Inserted Successfully....";
                                //this.programmaticModalPopup.Hide();
                            }
                            else
                            {
                                lblMessage.Text = "Record Not Inserted...";
                            }
                        }
                        else
                        {
                            lblMessage.Text = "Record Of This Bed Already Exist";
                        }
                        Clear();

                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
            MultiView1.SetActiveView(View1);
        }
        protected void BtnAddNewBed_Click(object sender, EventArgs e)
        {
            DataTable ldt = new DataTable();
            Clear();
            BindRoomNo();
            btnUpdate.Visible = false;
            BtnSave.Visible = true;
            btnUpdate.Visible = false;
            MultiView1.SetActiveView(View2);
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {

                ImageButton imgEdit = (ImageButton)sender;
                GridViewRow cnt = (GridViewRow)imgEdit.NamingContainer;
                Session["id"] = Convert.ToInt32(cnt.Cells[0].Text);

                DataTable tblEmp = new BedMasterBLL().SelectBedForEdit(Convert.ToInt32(cnt.Cells[0].Text));
                if (tblEmp.Rows.Count > 0)
                {
                    // txtRoomNo.Text = Convert.ToString(tblEmp.Rows[0]["RoomNo"]);
                    ListItem item = ddlRoomNo.Items.FindByText(Convert.ToString(tblEmp.Rows[0]["RoomNo"]));
                    ddlRoomNo.SelectedValue = item.Value;
                    // ListItem Desig = ddlFloorNo.Items.FindByText(Convert.ToString(tblEmp.Rows[0]["FloorName"]));
                    //ddlFloorNo.SelectedValue = Desig.Value;
                    Room_Id.Value = tblEmp.Rows[0]["RoomId"].ToString();
                    Floor_No.Value = tblEmp.Rows[0]["FloorNo"].ToString();
                    Cat_id.Value = tblEmp.Rows[0]["CategoryId"].ToString();
                    txtFloorNo.Text = cnt.Cells[3].Text;
                    txtCategory.Text = cnt.Cells[4].Text;
                    txtBedNo.Text = cnt.Cells[1].Text;

                    btnUpdate.Visible = true;
                    BtnSave.Visible = false;
                    MultiView1.SetActiveView(View2);
                }

                //Session["Floor_No"]=Convert.ToInt32(cnt.Cells[3].Text);
                //Session["Cat_Id"] = Convert.ToInt32(cnt.Cells[4].Text);
                //txtFloorNo.Text = cnt.Cells[3].Text;
                //txtCategory.Text = cnt.Cells[4].Text;
                //txtBedNo.Text = cnt.Cells[1].Text;
                // ListItem item = ddlRoomNo.Items.FindByText(Convert.ToString(cnt.Cells[2].Text));
                // ddlRoomNo.SelectedValue = item.Value;

                //btnUpdate.Visible = true;
                //BtnSave.Visible = false;
                //MultiView1.SetActiveView(View2);
                //}
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void ddlRoomNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlRoomNo.SelectedIndex > 0)
                {
                    EnitiyRoomMaster objLect = new BedMasterBLL().GetFloorAndCategory(Convert.ToInt32(ddlRoomNo.SelectedValue));

                    txtCategory.Text = objLect.CategoryName;
                    txtFloorNo.Text = objLect.FloorName;
                    CategoryId.Value = objLect.CategoryId.ToString();
                    FloorNo.Value = objLect.FloorNo.ToString();
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
                GetBedMasters();
                MultiView1.SetActiveView(View1);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        public void Clear()
        {
            ddlRoomNo.SelectedIndex = 0;
            txtCategory.Text = string.Empty;
            txtFloorNo.Text = string.Empty;
            txtBedNo.Text = string.Empty;
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSearch.Text))
                {
                    SearchBedMasters(txtSearch.Text);
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

        private void SearchBedMasters(string Prefix)
        {
            try
            {
                List<EntityBedMaster> lst = mobjBedMaster.SelectBedMaster(Prefix);
                if (lst != null)
                {
                    dgvBedMaster.DataSource = lst;
                    dgvBedMaster.DataBind();
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
                GetBedMasters();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }

        }
    }
}