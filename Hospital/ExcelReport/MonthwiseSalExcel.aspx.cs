using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using Hospital.Models.DataLayer;
using Hospital.Models.Models;
using Hospital.Models.BusinessLayer;

namespace Hospital.ExcelReport
{
    public partial class MonthwiseSalExcel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //string RptType=QueryStringManager.Instance.Rpt
            switch (QueryStringManager.Instance.Rpt)
            {
                case "Employee":
                    List<EntityEmployee> lst = (List<EntityEmployee>)Session["Details"];
                    dgvMonthSalary.DataSource = lst;
                    dgvMonthSalary.DataBind();
                    break;
                case "NursingManagment":
                    NursingManagementBLL MobjClaim = new NursingManagementBLL();
                    List<EntityNursingManagement> lstnurses = MobjClaim.GetAllocatedPatientList();
                    dgvMonthSalary.DataSource = lstnurses;
                    dgvMonthSalary.DataBind();
                    break;
                case "Prescription":
                    int DoctorId = QueryStringManager.Instance.DoctorId;
                    List<EntityPrescription> lstPrescript = null;
                    if (DoctorId>0)
                    {
                        lstPrescript = new PrescriptionBLL().GetPrescriptions(DoctorId);    
                    }
                    else
                    {
                        lstPrescript = new PrescriptionBLL().GetPrescriptions();
                    }
                    dgvMonthSalary.DataSource = lstPrescript;
                    dgvMonthSalary.DataBind();
                    break;
                case "EmpList":
                    List<EntityEmployee> ldtEmp = new EmployeeBLL().SelectAllEmployee();
                    dgvMonthSalary.DataSource = ldtEmp;
                    dgvMonthSalary.DataBind();
                    break;
                case "Death":
                    List<EntityDeathCertificate> ldtDeath = new DeathCertificateBLL().GetAllDeathDetails();
                    dgvMonthSalary.DataSource = ldtDeath;
                    dgvMonthSalary.DataBind();
                    break;
                case "ConsultDoctor":
                    List<EntityAllocaConDoc> lstConsultDoctor = new AllocConsultDoctorToPatientBLL().GetAllocatedPatientList();
                    dgvMonthSalary.DataSource = lstConsultDoctor;
                    dgvMonthSalary.DataBind();
                    break;
                default:
                    DataTable dt = (DataTable)Session["Details"];
                    dgvMonthSalary.DataSource = dt;
                    dgvMonthSalary.DataBind();
                    break;
            }
            
            string Path = Server.MapPath("ExcelReport") + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + ".xls";
            FileInfo FI = new FileInfo(Path);
            StringWriter stringWriter = new StringWriter();
            HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWriter);
            dgvMonthSalary.RenderControl(htmlWrite);
            string directory = Path.Substring(0, Path.LastIndexOf("\\"));// GetDirectory(Path);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            System.IO.StreamWriter vw = new System.IO.StreamWriter(Path, true);
            stringWriter.ToString().Normalize();
            vw.Write(stringWriter.ToString());
            vw.Flush();
            vw.Close();
            
            WriteAttachment(FI.Name, "application/vnd.ms-excel", stringWriter.ToString());
            Session["Details"] = null;
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }

        public void WriteAttachment(string FileName, string FileType, string content)
        {
            HttpResponse Response = this.Response;
            Response.ClearHeaders();
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName);
            Response.ContentType = FileType;
            Response.Write(content);
            
            Response.End();
        }
    }
}