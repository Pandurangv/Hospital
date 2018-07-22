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
    public partial class frmInsuranceComMaster : BasePage
    {
        InsuranceComBLL mobjInsuranceBLL = new InsuranceComBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            base.AuthenticateUser("frmInsuranceComMaster.aspx");
            if (!Page.IsPostBack)
            {
                GetInsurance();
                ViewData.SetActiveView(View1);
            }
        }

        protected void BtnClose_Click(object sender, EventArgs e)
        {
            try
            {
                ClearAll();
                ViewData.SetActiveView(View1);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        private void ClearAll()
        {
            txtInsuranceCode.Text = string.Empty;
            txtInsuranceDesc.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtPhNo.Text = string.Empty;
            txtPostalCode.Text = string.Empty;
            txtFaxNo.Text = string.Empty;
            txtContactPerson.Text = string.Empty;
            txtContactPhNo.Text = string.Empty;
            txtContactMobNo.Text = string.Empty;
            txtContactEmail.Text = string.Empty;
            txtNotes.Text = string.Empty;
            txtCountry.Text = string.Empty;
            txtState.Text = string.Empty;
            txtCity.Text = string.Empty;
        }

        protected void BtnAddNewInsurance_Click(object sender, EventArgs e)
        {
            DataTable ldt = new DataTable();
            ldt = mobjInsuranceBLL.GetNewInsuranceCode();
            txtInsuranceCode.Text = ldt.Rows[0]["InsuranceCode"].ToString();
            txtInsuranceDesc.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtPhNo.Text = string.Empty;
            txtPostalCode.Text = string.Empty;
            txtFaxNo.Text = string.Empty;
            txtContactPerson.Text = string.Empty;
            txtCity.Text = string.Empty;
            txtEmailID.Text = string.Empty;
            txtContactPhNo.Text = string.Empty;
            txtContactMobNo.Text = string.Empty;
            txtContactEmail.Text = string.Empty;
            txtNotes.Text = string.Empty;
            BtnSave.Visible = true;
            btnUpdate.Visible = false;
            lblMessage.Text = string.Empty;
            ViewData.SetActiveView(View2);
        }

        public void GetInsurance()
        {
            List<sp_GetAllInsuranceResult> ldtInsurance = mobjInsuranceBLL.GetAllInsurance();
            if (ldtInsurance.Count > 0 && ldtInsurance != null)
            {
                dgvInsurance.DataSource = ldtInsurance;
                dgvInsurance.DataBind();
                //Session["InsuranceDetails"] = ldtInsurance;
                int lintRowcount = ldtInsurance.Count;
                lblRowCount.Text = "<b>Total Records:</b> " + lintRowcount.ToString();
            }
            else
            {
                ldtInsurance = new List<sp_GetAllInsuranceResult>();
                dgvInsurance.DataSource = ldtInsurance;
                dgvInsurance.DataBind();
            }
        }


        protected void BtnSave_Click(object sender, EventArgs e)
        {
            int lintcnt = 0;
            try
            {
                EntityInsuranceCom entInsurance = new EntityInsuranceCom();
                if (string.IsNullOrEmpty(txtInsuranceCode.Text.Trim()))
                {
                    lblMsg.Text = "Please Enter Insurance Code";
                }
                else
                {
                    if (string.IsNullOrEmpty(txtInsuranceDesc.Text.Trim()))
                    {
                        lblMsg.Text = "Please Enter Insurance Description";
                    }
                    else
                    {
                        entInsurance.InsuranceCode = txtInsuranceCode.Text.Trim();
                        entInsurance.InsuranceDesc = txtInsuranceDesc.Text.Trim();
                        entInsurance.Address = txtAddress.Text.Trim();
                        entInsurance.Country = txtCountry.Text;
                        entInsurance.State = txtState.Text;
                        entInsurance.City = txtCity.Text;
                        entInsurance.EmailID = txtEmailID.Text.Trim();
                        entInsurance.ContactNo = txtPhNo.Text.Trim();
                        entInsurance.PostalCode = txtPostalCode.Text.Trim();
                        entInsurance.FaxNumber = txtFaxNo.Text.Trim();
                        entInsurance.ContactPerson = txtContactPerson.Text.Trim();
                        entInsurance.ContactPhNo = txtContactPhNo.Text.Trim();
                        entInsurance.MobileNo = txtContactMobNo.Text.Trim();
                        entInsurance.ContactEmail = txtContactEmail.Text.Trim();
                        entInsurance.Notes = txtNotes.Text.Trim();
                        entInsurance.EntryBy = SessionManager.Instance.LoginUser.PKId.ToString();
                        lintcnt = mobjInsuranceBLL.InsertInsurance(entInsurance);

                        if (lintcnt > 0)
                        {
                            ClearAll();
                            GetInsurance();
                            ViewData.SetActiveView(View1);
                            lblMessage.Text = "Record Inserted Successfully";
                        }
                        else
                        {
                            lblMessage.Text = "Record Not Inserted";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton imgEdit = (ImageButton)sender;
                GridViewRow cnt = (GridViewRow)imgEdit.NamingContainer;
                int Id = Convert.ToInt32(dgvInsurance.DataKeys[cnt.RowIndex].Value);
                EntityInsuranceCom obj = mobjInsuranceBLL.GetInsuranceById(Id);
                if (obj != null)
                {
                    txtAddress.Text = obj.Address;
                    txtCity.Text = obj.City;
                    txtCountry.Text = obj.Country;
                    txtState.Text = obj.State;
                    txtContactEmail.Text = obj.ContactEmail;
                    txtContactMobNo.Text = obj.ContactNo;
                    txtContactPerson.Text = obj.ContactPerson;
                    txtContactPhNo.Text = obj.ContactPhNo;
                    txtEmailID.Text = obj.EmailID;
                    txtFaxNo.Text = obj.FaxNumber;
                    txtInsuranceCode.Text = obj.InsuranceCode;
                    txtInsuranceDesc.Text = obj.InsuranceDesc;
                    txtNotes.Text = obj.Notes;
                    txtPhNo.Text = obj.ContactPhNo;
                    txtPostalCode.Text = obj.PostalCode;
                }
                BtnSave.Visible = false;
                btnUpdate.Visible = true;
                ViewData.SetActiveView(View2);
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
                EntityInsuranceCom entInsurance = new EntityInsuranceCom();
                entInsurance.InsuranceCode = txtInsuranceCode.Text;
                entInsurance.InsuranceDesc = txtInsuranceDesc.Text;
                entInsurance.Address = txtAddress.Text;
                entInsurance.Country = txtCountry.Text;
                entInsurance.State = txtState.Text;
                entInsurance.City = txtCity.Text;
                entInsurance.EmailID = txtEmailID.Text;
                entInsurance.ContactNo = txtPhNo.Text;
                entInsurance.PostalCode = txtPostalCode.Text;
                entInsurance.FaxNumber = txtFaxNo.Text;
                entInsurance.ContactPerson = txtContactPerson.Text;
                entInsurance.ContactPhNo = txtContactPhNo.Text;
                entInsurance.MobileNo = txtContactMobNo.Text;
                entInsurance.ContactEmail = txtContactEmail.Text;
                entInsurance.Notes = txtNotes.Text;
                entInsurance.ChangeBy = SessionManager.Instance.LoginUser.PKId.ToString();
                lintCnt = mobjInsuranceBLL.UpdateInsurance(entInsurance);

                if (lintCnt > 0)
                {
                    GetInsurance();
                    lblMessage.Text = "Record Updated Successfully";
                    BtnClose_Click(sender, e);
                    ViewData.SetActiveView(View1);
                }
                else
                {
                    lblMessage.Text = "Record Not Updated";
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                txtSearch.Text = string.Empty;
                GetInsurance();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewData.ActiveViewIndex == 0)
                {
                    List<sp_GetAllInsuranceResult> ldtInsurance = mobjInsuranceBLL.GetAllInsurance(txtSearch.Text);
                    if (ldtInsurance.Count > 0 && ldtInsurance != null)
                    {
                        dgvInsurance.DataSource = ldtInsurance;
                        dgvInsurance.DataBind();
                        //Session["InsuranceDetails"] = ldtInsurance;
                        int lintRowcount = ldtInsurance.Count;
                        lblRowCount.Text = "<b>Total Records:</b> " + lintRowcount.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void dgvInsurance_PageIndexChanged(object sender, EventArgs e)
        {
            try
            {
                List<sp_GetAllInsuranceResult> ldtInsurance = mobjInsuranceBLL.GetAllInsurance();
                if (!string.IsNullOrEmpty(txtSearch.Text))
                {
                    ldtInsurance = ldtInsurance.Where(p => p.InsuranceDesc.Contains(txtSearch.Text) || p.Address.Contains(txtSearch.Text)).ToList();
                }
                dgvInsurance.DataSource = ldtInsurance;// (List<sp_GetAllInsuranceResult>)Session["InsuranceDetails"];
                dgvInsurance.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void dgvInsurance_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvInsurance.PageIndex = e.NewPageIndex;
        }
    }
}