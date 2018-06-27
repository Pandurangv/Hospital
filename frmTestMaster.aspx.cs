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
    public partial class frmTestMaster : BasePage
    {
        TestBLL mobjDeptBLL = new TestBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            base.AuthenticateUser();
            if (!Page.IsPostBack)
            {
                update.Value = Server.UrlEncode(System.DateTime.Now.ToString());
                GetTests();
                BindCatagory();
                MultiView1.SetActiveView(View1);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                List<EntityTest> ldtDept = mobjDeptBLL.GetAllTests(txtSearch.Text);
                if (ldtDept.Count > 0)
                {
                    dgvDepartment.DataSource = ldtDept;
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
                dgvDepartment.PageIndex = 0;
                GetTests();
                txtSearch.Text = string.Empty;
                lblMessage.Text = string.Empty;
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        private void BindCatagory()
        {
            try
            {
                List<tblTestCatagory> ldtDept = mobjDeptBLL.GetAllTestCatagory();
                ldtDept.Insert(0, new tblTestCatagory { TestCatId = 0, TestCatDescription = "----Select----" });
                ddlTestCatagory.DataSource = ldtDept;
                ddlTestCatagory.DataTextField = "TestCatDescription";
                ddlTestCatagory.DataValueField = "TestCatId";
                ddlTestCatagory.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void BtnClose_Click(object sender, EventArgs e)
        {
            try
            {
                txtDeptCode.Text = String.Empty;
                txtDeptDesc.Text = String.Empty;
                GetTests();
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
                txtDeptCode.Text = Convert.ToString(row.Cells[3].Text);
                txtDeptDesc.Text = Convert.ToString(row.Cells[1].Text);
                testid.Value = Convert.ToString(row.Cells[0].Text);
                txtCharge.Text = Convert.ToString(row.Cells[2].Text);
                ListItem item = ddlTestCatagory.Items.FindByText(Convert.ToString(row.Cells[4].Text));
                bool b = Convert.ToBoolean(row.Cells[5].Text);
                if (!b)
                {
                    rdoPathology.Checked = true;
                    rdoRadiology.Checked = false;
                }
                else
                {
                    rdoPathology.Checked = false;
                    rdoRadiology.Checked = true;
                }
                ddlTestCatagory.SelectedValue = item.Value;
                GetTests();
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
            ddlTestCatagory.SelectedIndex = 0;
            txtDeptDesc.Text = string.Empty;
            txtDeptCode.Text = string.Empty;
            txtCharge.Text = string.Empty;
            btnUpdate.Visible = false;
            BtnSave.Visible = true;
            MultiView1.SetActiveView(View2);
        }

        private void GetTests()
        {
            try
            {
                List<EntityTest> ldtDept = mobjDeptBLL.GetAllTests();
                if (ldtDept.Count > 0)
                {
                    dgvDepartment.DataSource = ldtDept;
                    dgvDepartment.DataBind();
                    int lintRowcount = ldtDept.Count;
                    lblRowCount.Text = "<b>Total Records:</b> " + lintRowcount.ToString();
                    pnlShow.Style.Add(HtmlTextWriterStyle.Display, "");
                    hdnPanel.Value = "";
                }
                else
                {
                    hdnPanel.Value = "none";
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
                EntityTest entDept = new EntityTest();
                if (update.Value == ViewState["update"].ToString())
                {
                    entDept.TestName = txtDeptDesc.Text.Trim();
                    entDept.TestCharge = Convert.ToDecimal(txtCharge.Text);
                    entDept.Precautions = txtDeptCode.Text;
                    entDept.IsRadiology = rdoRadiology.Checked ? true : false;
                    entDept.IsPathology = rdoPathology.Checked ? true : false;
                    entDept.TestCatId = Convert.ToInt32(ddlTestCatagory.SelectedValue);
                    if (!mobjDeptBLL.IsRecordExists(entDept))
                    {
                        lintcnt = mobjDeptBLL.InsertTest(entDept);
                        if (lintcnt > 0)
                        {
                            GetTests();
                            lblMessage.Text = "Record Inserted Successfully.";
                            update.Value = Server.UrlEncode(System.DateTime.Now.ToString());
                        }
                        else
                        {
                            lblMessage.Text = "Record Not Inserted";
                        }
                    }
                    else
                    {
                        lblMessage.Text = "Record Already Exist.";
                    }
                }
                MultiView1.SetActiveView(View1);
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
                EntityTest entDept = new EntityTest();
                entDept.Precautions = txtDeptCode.Text;
                entDept.TestName = txtDeptDesc.Text;
                entDept.TestId = Convert.ToInt32(testid.Value);
                entDept.TestCharge = Convert.ToDecimal(txtCharge.Text);
                entDept.IsRadiology = rdoRadiology.Checked ? true : false;
                entDept.IsPathology = rdoPathology.Checked ? true : false;
                entDept.TestCatId = Convert.ToInt32(ddlTestCatagory.SelectedValue);
                EntityTest obj = (from tbl in mobjDeptBLL.GetAllTests()
                                  where tbl.TestId == Convert.ToInt32(testid.Value)
                                  && tbl.TestName.ToUpper().Equals(txtDeptDesc.Text.ToUpper())
                                  select tbl).FirstOrDefault();

                if (obj != null)
                {
                    lintCnt = mobjDeptBLL.Update(entDept);
                    GetTests();
                    lblMessage.Text = "Record Updated Successfully";
                }
                else
                {
                    if (!mobjDeptBLL.IsRecordExists(entDept))
                    {
                        lintCnt = mobjDeptBLL.Update(entDept);

                        if (lintCnt > 0)
                        {
                            GetTests();
                            lblMessage.Text = "Record Updated Successfully";
                        }
                        else
                        {
                            lblMessage.Text = "Record Not Updated.";
                        }
                    }
                    else
                    {
                        lblMessage.Text = "Record Already Exist.";
                    }
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
                List<EntityTest> ldtDept = mobjDeptBLL.GetAllTests();
                if (!string.IsNullOrEmpty(txtSearch.Text))
                {
                    ldtDept = ldtDept.Where(p => p.TestName.Contains(txtSearch.Text)).ToList();
                }
                dgvDepartment.DataSource = ldtDept;
                dgvDepartment.DataBind();
            }
            catch (Exception ex)
            {
                Commons.FileLog("frmDepartmentMaster - dgvDepartment_PageIndexChanged(object sender, EventArgs e)", ex);
            }
        }
        protected void dgvDepartment_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvDepartment.PageIndex = e.NewPageIndex;
        }


    }
}