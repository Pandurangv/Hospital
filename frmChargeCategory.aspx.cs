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
    public partial class frmChargeCategory : BasePage
    {
        ChargeCategoryBLL mobjDeptBLL = new ChargeCategoryBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            base.AuthenticateUser();
            if (!Page.IsPostBack)
            {
                GetChargeCategoryDetail();
                MultiView1.SetActiveView(View1);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                GetChargeCategoryDetail(txtSearch.Text);
                txtSearch.Text = string.Empty;
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        private void GetChargeCategoryDetail(string Prefix)
        {
            List<EntityChargeCategory> ldtShift = (from tbl in mobjDeptBLL.GetChargeDetail()
                                                   where tbl.ChargeCategoryName.ToUpper().Contains(Prefix.ToUpper())
                                                   select tbl).ToList();
            if (ldtShift.Count > 0)
            {
                dgvShift.DataSource = ldtShift;
                dgvShift.DataBind();
                int lintRowcount = ldtShift.Count;
                lblRowCount.Text = "<b>Total Records:</b> " + lintRowcount.ToString();
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtSearch.Text = string.Empty;
            GetChargeCategoryDetail();
        }

        protected void BtnClose_Click(object sender, EventArgs e)
        {
            try
            {
                Clear();
                GetChargeCategoryDetail();
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
                    Charges_Id.Value = Convert.ToString(row.Cells[0].Text);
                }
                txtChargeCategory.Text = row.Cells[1].Text;
                txtCharges.Text = row.Cells[2].Text;
                EntityChargeCategory lstB = new ChargeCategoryBLL().GetChargeCategory(Convert.ToInt32(Charges_Id.Value));
                txtChargeCategory.Text = lstB.ChargeCategoryName;
                lblMsg.Text = string.Empty;
                if (lstB.IsOperation)
                {
                    rdOpera.Checked = true;
                }
                else
                {
                    if (lstB.IsBed)
                    {
                        rdBed.Checked = true;
                    }
                    else
                    {
                        if (lstB.IsConsulting)
                        {
                            rdConsult.Checked = true;
                        }
                        else
                        {
                            if (lstB.IsICU)
                            {
                                rdoICU.Checked = true;
                            }
                            else
                            {
                                if (lstB.IsRMO)
                                {
                                    rdoRMO.Checked = true;
                                }
                                else
                                {
                                    if (lstB.IsNursing)
                                    {
                                        rdoNursing.Checked = true;
                                    }
                                    else
                                    {
                                        if (lstB.IsOther)
                                        {
                                            rdOther.Checked = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                GetChargeCategoryDetail();
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
                if (tblCat != null)
                {
                    tblCat.Insert(0, new EntityPatientMaster() { AdmitId = 0, FullName = "---Select---" });
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
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
            txtChargeCategory.Text = string.Empty;
            txtCharges.Text = Convert.ToString(0);
            rdOpera.Checked = false;
            rdBed.Checked = false;
            rdConsult.Checked = false;
            rdOther.Checked = false;
            lblMsg.Text = string.Empty;
        }

        private void GetChargeCategoryDetail()
        {
            List<EntityChargeCategory> ldtShift = mobjDeptBLL.GetChargeDetail();
            if (ldtShift.Count > 0)
            {
                dgvShift.DataSource = ldtShift;
                dgvShift.DataBind();
                //Session["DepartmentDetail"] = ldtShift;
                int lintRowcount = ldtShift.Count;
                lblRowCount.Text = "<b>Total Records:</b> " + lintRowcount.ToString();
            }
        }



        protected void BtnSave_Click(object sender, EventArgs e)
        {
            int lintcnt = 0;
            EntityChargeCategory entDept = new EntityChargeCategory();
            int cnt = 0;
            if (string.IsNullOrEmpty(txtChargeCategory.Text.Trim()))
            {
                lblMsg.Text = "Please Enter Charge Category Name";
                return;
            }
            else if (!rdOther.Checked && !rdConsult.Checked && !rdOpera.Checked && !rdBed.Checked && !rdoICU.Checked && !rdoRMO.Checked && !rdoNursing.Checked)
            {
                lblMsg.Text = "Please select Charges Category.";
                return;
            }
            else
            {
                if (rdOpera.Checked)
                {
                    cnt = new ChargeCategoryBLL().GetCount("O");
                    if (cnt > 0)
                    {
                        lblMsg.Text = "Record Already Exist for Operation Charges.";
                        return;
                    }
                    else
                    {
                        lintcnt = SaveDetails(lintcnt, entDept);
                    }
                }
                if (rdBed.Checked)
                {
                    cnt = new ChargeCategoryBLL().GetCount("B");
                    if (cnt > 0)
                    {
                        lblMsg.Text = "Record Already Exist for Bed Charges.";
                        return;
                    }
                    else
                    {
                        lintcnt = SaveDetails(lintcnt, entDept);
                    }
                }
                if (rdConsult.Checked)
                {
                    cnt = new ChargeCategoryBLL().GetCount("C");
                    if (cnt > 0)
                    {
                        lblMsg.Text = "Record Already Exist for Consulting Charges.";
                        return;
                    }
                    else
                    {
                        lintcnt = SaveDetails(lintcnt, entDept);
                    }
                }
                if (rdoICU.Checked)
                {
                    cnt = new ChargeCategoryBLL().GetCount("I");
                    if (cnt > 0)
                    {

                        lblMessage.Text = "Record Already Exist for Consulting Charges.";

                        return;
                    }
                    else
                    {
                        lintcnt = SaveDetails(lintcnt, entDept);
                    }
                }
                if (rdoICU.Checked)
                {
                    cnt = new ChargeCategoryBLL().GetCount("I");
                    if (cnt > 0)
                    {
                        lblMsg.Text = "Record Already Exist for ICU Charges.";
                        return;
                    }
                    else
                    {
                        lintcnt = SaveDetails(lintcnt, entDept);
                    }
                }
                if (rdoRMO.Checked)
                {
                    cnt = new ChargeCategoryBLL().GetCount("R");
                    if (cnt > 0)
                    {
                        lblMsg.Text = "Record Already Exist for RMO Charges.";
                        return;
                    }
                    else
                    {
                        lintcnt = SaveDetails(lintcnt, entDept);
                    }
                }
                if (rdoNursing.Checked)
                {
                    cnt = new ChargeCategoryBLL().GetCount("N");
                    if (cnt > 0)
                    {
                        lblMsg.Text = "Record Already Exist For Nursing Charges";
                        return;
                    }
                    else
                    {
                        lintcnt = SaveDetails(lintcnt, entDept);
                    }
                }

                if (rdOther.Checked)
                {
                    lintcnt = SaveDetails(lintcnt, entDept);
                }
            }

            MultiView1.SetActiveView(View1);
        }

        private int SaveDetails(int lintcnt, EntityChargeCategory entDept)
        {
            entDept.ChargeCategoryName = txtChargeCategory.Text.Trim();
            entDept.Charges = Convert.ToDecimal(txtCharges.Text);
            entDept.IsOperation = rdOpera.Checked;
            entDept.IsBed = rdBed.Checked;
            entDept.IsConsulting = rdConsult.Checked;
            entDept.IsOther = rdOther.Checked;
            entDept.IsICU = rdoICU.Checked;
            entDept.IsRMO = rdoRMO.Checked;
            entDept.IsNursing = rdoRMO.Checked;
            entDept.Charges = Convert.ToDecimal(txtCharges.Text);
            if (!mobjDeptBLL.IsRecordExists(entDept))
            {
                lintcnt = mobjDeptBLL.InsertChargeCategory(entDept);

                if (lintcnt > 0)
                {
                    GetChargeCategoryDetail();
                    lblMessage.Text = "Record Inserted Successfully....";
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
            return lintcnt;
        }

        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            int lintCnt = 0;
            int cnt = 0;
            EntityChargeCategory entDept = new EntityChargeCategory();
            try
            {
                if (string.IsNullOrEmpty(txtChargeCategory.Text.Trim()))
                {
                    lblMsg.Text = "Please Enter Charge Category Name";
                    return;
                }
                else
                {
                    entDept.ChargesId = Convert.ToInt32(Session["Charges_Id"]);
                    if (rdOpera.Checked)
                    {
                        cnt = new ChargeCategoryBLL().GetCount("O", entDept.ChargesId);
                        if (cnt > 0)
                        {
                            lintCnt = EditDetails(lintCnt, entDept);
                        }
                        else
                        {
                            lblMsg.Text = "Record Already Exist for Operation Charges.";
                            return;
                        }
                    }
                    if (rdBed.Checked)
                    {
                        cnt = new ChargeCategoryBLL().GetCount("B", entDept.ChargesId);
                        if (cnt > 0)
                        {
                            lintCnt = EditDetails(lintCnt, entDept);
                        }
                        else
                        {
                            lblMsg.Text = "Record Already Exist for Bed Charges.";
                            return;
                        }
                    }
                    if (rdConsult.Checked)
                    {
                        cnt = new ChargeCategoryBLL().GetCount("C", entDept.ChargesId);
                        if (cnt > 0)
                        {
                            lintCnt = EditDetails(lintCnt, entDept);
                        }
                        else
                        {
                            lblMsg.Text = "Record Already Exist for Consulting Charges.";
                            return;
                        }
                    }
                    if (rdOther.Checked)
                    {
                        lintCnt = EditDetails(lintCnt, entDept);
                    }
                    if (rdoICU.Checked)
                    {
                        cnt = new ChargeCategoryBLL().GetCount("I", entDept.ChargesId);
                        if (cnt > 0)
                        {
                            lintCnt = EditDetails(lintCnt, entDept);
                        }
                        else
                        {
                            lblMsg.Text = "Record Already Exist for ICU Charges.";
                            return;
                        }
                    }
                    if (rdoRMO.Checked)
                    {
                        cnt = new ChargeCategoryBLL().GetCount("R", entDept.ChargesId);
                        if (cnt > 0)
                        {
                            lintCnt = EditDetails(lintCnt, entDept);
                        }
                        else
                        {
                            lblMsg.Text = "Record Already Exist for RMO Charges.";
                            return;
                        }
                    }
                    if (rdoNursing.Checked)
                    {
                        cnt = new ChargeCategoryBLL().GetCount("N", entDept.ChargesId);
                        if (cnt > 0)
                        {
                            lintCnt = EditDetails(lintCnt, entDept);
                        }
                        else
                        {
                            lblMsg.Text = "Record Aleready Exist for Nursing Charges";
                            return;
                        }
                    }
                }
                MultiView1.SetActiveView(View1);
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        private int EditDetails(int lintCnt, EntityChargeCategory entDept)
        {
            entDept.ChargesId = Convert.ToInt32(Charges_Id.Value);
            entDept.ChargeCategoryName = txtChargeCategory.Text;
            entDept.Charges = Convert.ToDecimal(txtCharges.Text);
            entDept.IsOperation = rdOpera.Checked;
            entDept.IsBed = rdBed.Checked;
            entDept.IsConsulting = rdConsult.Checked;
            entDept.IsOther = rdOther.Checked;
            entDept.IsICU = rdoICU.Checked;
            entDept.IsRMO = rdoRMO.Checked;
            entDept.IsNursing = rdoNursing.Checked;

            if (entDept.IsOther == true)
            {
                lintCnt = ForUpdation(lintCnt, entDept);
            }
            else
            {
                if (!mobjDeptBLL.CheckRecordExists(entDept))
                {
                    lintCnt = ForUpdation(lintCnt, entDept);
                    lblMessage.Text = "Record Updated Successfully";
                }
                else
                {
                    lintCnt = ForUpdation(lintCnt, entDept,true);
                    lblMessage.Text = "Record Updated Successfully";
                }
            }
            return lintCnt;
        }

        private int ForUpdation(int lintCnt, EntityChargeCategory entDept,bool updateonlycharges=false)
        {
            lintCnt = mobjDeptBLL.Update(entDept,updateonlycharges);

            if (lintCnt > 0)
            {
                GetChargeCategoryDetail();
                lblMessage.Text = "Record Updated Successfully";
            }
            else
            {
                lblMessage.Text = "Record Not Updated";
            }
            return lintCnt;
        }

        protected void dgvDepartment_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                List<EntityChargeCategory> ldtShift = mobjDeptBLL.GetChargeDetail();
                if (!string.IsNullOrEmpty(txtSearch.Text))
                {
                    ldtShift = (from tbl in ldtShift
                                where tbl.ChargeCategoryName.ToUpper().Contains(txtSearch.Text.ToUpper())
                     select tbl).ToList();
                }
                dgvShift.DataSource = ldtShift;// (List<EntityChargeCategory>)Session["DepartmentDetail"];
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

        protected void dgvDepartment_DataBound(object sender, EventArgs e)
        {
            int lintCurrentPage = dgvShift.PageIndex + 1;
            lblPageCount.Text = "<b>Page</b> " + lintCurrentPage.ToString() + "<b> of </b>" + dgvShift.PageCount.ToString();
        }
    }
}