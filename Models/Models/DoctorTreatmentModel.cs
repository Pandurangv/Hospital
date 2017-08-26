using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hospital.Models.DataLayer;

namespace Hospital.Models.Models
{
    public class DoctorTreatmentModel
    {
        public DoctorTreatmentModel()
        {
            ProductList = new List<EntityOTMedicineBillDetails>();
        }
        public long? TreatId { get; set; }

        public DateTime? TreatmentDate { get; set; }

        public int? DoctorId { get; set; }

        public int? AdmitId { get; set; }

        public string TreatmentDetails { get; set; }

        public bool? IsDelete { get; set; }

        public DateTime? FollowUpDate { get; set; }

        public string Procedures { get; set; }

        public string PatientName { get; set; }

        public string EmployeeName { get; set; }

        public DateTime? AdmitDate { get; set; }

        public List<EntityOTMedicineBillDetails> ProductList { get; set; }

        public decimal? TotalAmount { get; set; }
    }

    public class Error
    {
        public long? Id { get; set; }

        public int status { get; set; }

        public string Message { get; set; }
    }

    public class DoctorTreatmentChartResponse
    {
        public DoctorTreatmentChartResponse()
        {
            TreatmentList = new List<EntityOTMedicineBill>();
            ProductList = new List<EntityOTMedicineBillDetails>();
        }
        public List<EntityOTMedicineBill> TreatmentList { get; set; }

        public List<EntityOTMedicineBillDetails>  ProductList { get; set; }
    }

    public class DoctorTreatResponse:Error
    {
        public List<DoctorTreatmentModel> DoctorTreatmentList { get; set; }

        public List<EntityEmployee> DoctorList { get; set; }

        public List<EntityPatientAdmit> PatientList { get; set; }

        public List<sp_GetAllProductResult> ProductList { get; set; }

        public List<EntityProduct> ProductBatchList { get; set; }

        public DoctorTreatResponse()
        {
            ProductBatchList = new List<EntityProduct>();
            DoctorTreatmentList = new List<DoctorTreatmentModel>();
            DoctorList = new List<EntityEmployee>();
            PatientList = new List<EntityPatientAdmit>();
            ProductList = new List<sp_GetAllProductResult>();
        }
        
    }

    public class NurseTreatResponse : Error
    {
        public List<EntityNursingManagementDetails> NurseTreatmentList { get; set; }

        public List<EntityEmployee> NurseList { get; set; }

        public List<EntityPatientAdmit> PatientList { get; set; }

        public NurseTreatResponse()
        {
            NurseTreatmentList = new List<EntityNursingManagementDetails>();
            NurseList = new List<EntityEmployee>();
            PatientList = new List<EntityPatientAdmit>();
        }

    }
}