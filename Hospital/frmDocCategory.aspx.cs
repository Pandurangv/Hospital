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
    public partial class frmDocCategory : BasePage
    {
        DocCategoryBLL mobjDocCategoryBLL = new DocCategoryBLL();


        protected void Page_Load(object sender, EventArgs e)
        {
            base.AuthenticateUser("frmDocCategory.aspx");
            if (!Page.IsPostBack)
            {
                update.Value = Server.UrlEncode(System.DateTime.Now.ToString());
                GetDoctorDetails();
                BindCategory();
                BindDocName();
                MultiView1.SetActiveView(View1);
            }
        }

        private void BindDocName()
        {
            try
            {
                List<EntityEmployee> tblCat = new EmployeeBLL().SelectAllEmployee().Where(p => p.DesignationId == SettingsManager.Instance.DoctorDesigId).ToList();
                tblCat.Insert(0, new EntityEmployee() { PKId = 0, FullName = "---Select---" });
                

                ddlDocName.DataSource = tblCat;
                ddlDocName.DataValueField = "PKId";
                ddlDocName.DataTextField = "FullName";
                ddlDocName.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        private void GetDoctorDetails()
        {
            List<EntityDocCategory> ldtOpera = mobjDocCategoryBLL.GetAlDoctorDetails();

            if (ldtOpera.Count > 0)
            {
                dgvRoomMaster.DataSource = ldtOpera;
                dgvRoomMaster.DataBind();
                //Session["DepartmentDetail"] = ldtOpera;
                int lintRowcount = ldtOpera.Count;
                lblRowCount.Text = "<b>Total Records:</b> " + lintRowcount.ToString();
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

                ddlCategoryName.DataSource = tblCat;
                ddlCategoryName.DataValueField = "CategoryId";
                ddlCategoryName.DataTextField = "CategoryName";
                ddlCategoryName.DataBind();
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
            Clear();
            MultiView1.SetActiveView(View2);
        }
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            int lintcnt = 0;
            EntityDocCategory entOpera = new EntityDocCategory();
            if (update.Value.ToString() == ViewState["update"].ToString())
            {
                if (ddlCategoryName.SelectedIndex == 0)
                {
                    lblMsg.Text = "Please Select Operation Category Name";
                    ddlCategoryName.Focus();
                    return;
                }
                else
                {
                    if (ddlDocName.SelectedIndex == 0)
                    {
                        lblMsg.Text = "Please Select Operation Doctor Name";
                        ddlDocName.Focus();
                        return;
                    }
                    else
                    {
                        entOpera.OperaCatId = Convert.ToInt32(Cat_Id.Value);
                        entOpera.DocId = Convert.ToInt32(Doc_Id.Value);
                        entOpera.Charges = 0;// Convert.ToDecimal(txtCharges.Text);

                        if (!mobjDocCategoryBLL.IsRecordExists(entOpera.DocId, entOpera.OperaCatId))
                        {
                            lintcnt = mobjDocCategoryBLL.InsertDoctorDetails(entOpera);
                            if (lintcnt > 0)
                            {
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
                    }
                }
            }
            GetDoctorDetails();
            MultiView1.SetActiveView(View1);
        }

        public void Clear()
        {
            ddlCategoryName.SelectedIndex = 0;
            ddlDocName.SelectedIndex = 0;
            //txtCharges.Text = "0";//Convert.ToString(0);
        }

        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            int lintCnt = 0;
            try
            {
                EntityDocCategory entOpera = new EntityDocCategory();
                entOpera.DocAllocId = Convert.ToInt32(Opera_Id.Value);
                entOpera.OperaCatId = Convert.ToInt32(Cat_Id.Value);
                entOpera.DocId = Convert.ToInt32(Doc_Id.Value);
                entOpera.Charges = 0;
                DocCategoryBLL mobjDocCategoryBLL = new DocCategoryBLL();
                lintCnt = mobjDocCategoryBLL.Update(entOpera);
                if (lintCnt > 0)
                {
                    GetDoctorDetails();
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
                Commons.FileLog("frmDepartmentMaster -  BtnEdit_Click(object sender, EventArgs e)", ex);
            }
        }


        protected void BtnClose_Click(object sender, EventArgs e)
        {
            try
            {
                Clear();
                //GetDoctorDetails();
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
                Opera_Id.Value = Convert.ToString(cnt.Cells[0].Text);

                //Session["Doc_Id"] = Convert.ToInt32(cnt.Cells[2].Text);
                EntityDocCategory tblOpera = new DocCategoryBLL().SelectDoc(Convert.ToInt32(cnt.Cells[0].Text));
                if (tblOpera != null)
                {
                    ListItem Desig = ddlCategoryName.Items.FindByText(Convert.ToString(tblOpera.Opera_Name));
                    ddlCategoryName.SelectedValue = Desig.Value;
                    Cat_Id.Value = ddlCategoryName.SelectedValue;
                    BindAllEmpDoc();
                    ListItem Desig1 = ddlDocName.Items.FindByText(Convert.ToString(tblOpera.Doc_Name));
                    ddlDocName.SelectedValue = Desig1.Value;
                    Doc_Id.Value = ddlDocName.SelectedValue;
                    Charges.Value = Convert.ToString(tblOpera.Charges);
                    //txtCharges.Text = Convert.ToString(tblOpera.Charges);
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

        private void BindAllEmpDoc()
        {
            List<EntityDocCategory> lst = mobjDocCategoryBLL.GetAllDoctors();
            if (lst.Count > 0)
            {
                ddlDocName.DataSource = lst;
                ddlDocName.DataValueField = "DocId";
                ddlDocName.DataTextField = "Doc_Name";
                ddlDocName.DataBind();
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
                List<EntityDocCategory> ldtOpera = mobjDocCategoryBLL.GetAlDoctorDetails();
                if (!string.IsNullOrEmpty(txtSearch.Text))
                {
                    ldtOpera = ldtOpera.Where(p => p.Doc_Name.Contains(txtSearch.Text)).ToList();
                }
                dgvRoomMaster.DataSource = ldtOpera;// (DataTable)Session["RoomDetails"];
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
                    //  ldt = mobjRoomMasterBLL.GetRoomForEdit(lstrRoomId);
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
        protected void ddlCategoryName_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cat_Id.Value = Convert.ToString(ddlCategoryName.SelectedValue);
        }
        protected void ddlDocName_SelectedIndexChanged(object sender, EventArgs e)
        {
            Doc_Id.Value = Convert.ToString(ddlDocName.SelectedValue);
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSearch.Text))
                {
                    SearchDoctorAlloc(txtSearch.Text);
                }
                else
                {
                    lblMessage.Text = "Please Fill Search Text.";
                    txtSearch.Focus();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void SearchDoctorAlloc(string Prefix)
        {
            List<EntityDocCategory> lst = mobjDocCategoryBLL.SelectDoctCategory(Prefix);
            if (lst != null)
            {
                dgvRoomMaster.DataSource = lst;
                dgvRoomMaster.DataBind();
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtSearch.Text = string.Empty;
            GetDoctorDetails();
        }
    }
}