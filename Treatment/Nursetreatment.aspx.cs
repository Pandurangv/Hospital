using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hospital.Models;
using Hospital.Models.DataLayer;
using System.Web.Script.Serialization;
using Hospital.Models.Models;
using Hospital.Models.BusinessLayer;

namespace Hospital.Treatment
{
    public partial class Nursetreatment : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            base.AuthenticateUser();
            if (QueryStringManager.Instance.RequestFor == "GetDetails")
            {
                GetTreatmentDetails();
            }
        }

        public void GetTreatmentDetails()
        {
            JavaScriptSerializer serialize = new JavaScriptSerializer();
            OTMedicineBillBLL mobjPatientMasterBLL = new OTMedicineBillBLL();
            List<EntityPatientAdmit> ldtRequisition = mobjPatientMasterBLL.GetPatientList();
            ldtRequisition.Insert(0, new EntityPatientAdmit() { AdmitId = 0, PatientFirstName = "----Select----" });
            PatientAllocDocBLL objdoctor = new PatientAllocDocBLL();
            DoctorTreatmentBLL objProductTypes = new DoctorTreatmentBLL();
            DoctorTreatResponse response = new DoctorTreatResponse();
            response.DoctorList = (from tbl in objdoctor.GetAllDoctor()
                                   select new EntityEmployee()
                                   {
                                       FullName = tbl.FullName,
                                       PKId = tbl.PKId
                                   }).ToList();

            response.DoctorList.Insert(0, new EntityEmployee() { FullName = "----Select----", PKId = 0 });
            serialize.MaxJsonLength = Int32.MaxValue;
            response.DoctorTreatmentList = objProductTypes.GetTreatmentDetails();
            response.PatientList = ldtRequisition;

            Response.Clear();
            Response.Output.Write(serialize.Serialize(response));
            Response.End();
        }
    }
}