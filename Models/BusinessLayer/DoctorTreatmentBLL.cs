using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hospital.Models.DataLayer;
using Hospital.Models.Models;

namespace Hospital.Models.BusinessLayer
{
    public class DoctorTreatmentBLL
    {
        CriticareHospitalDataContext objData = new CriticareHospitalDataContext();

        public DoctorTreatmentBLL()
        { 
            
        }

        public List<DoctorTreatmentModel> GetTreatmentDetails()
        {
            List<DoctorTreatmentModel> lst = (from tbl in objData.tblDoctorTreatments
                                              join tblA in objData.tblPatientAdmitDetails
                                              on tbl.AdmitId equals tblA.AdmitId
                                              join tblP in objData.tblPatientMasters
                                              on tblA.PatientId equals tblP.PKId
                                              join tblEmp in objData.tblEmployees
                                              on tbl.DoctorId equals tblEmp.PKId
                                              where tbl.IsDelete == false
                                              select new DoctorTreatmentModel { 
                                                   AdmitDate=tblP.AdminDate,
                                                   AdmitId=tbl.AdmitId,
                                                   DoctorId=tblEmp.PKId,
                                                   FollowUpDate=tbl.FollowUpDate,
                                                   IsDelete=tbl.IsDelete,
                                                   PatientName=tblP.PatientFirstName + " " + tblP.PatientLastName,
                                                   Procedures=tbl.Procedures,
                                                   TreatId=tbl.TreatId,
                                                   TreatmentDate=tbl.TreatmentDate,
                                                   TreatmentDetails=tbl.TreatmentDetails,
                                                   EmployeeName=tblEmp.EmpFirstName + " " + tblEmp.EmpLastName,
                                                   
                                              }).ToList();
            return lst;

        }

        public DoctorTreatResponse Save(DoctorTreatmentModel model)
        {
            if (model.TreatmentDate==null)
            {
                model.TreatmentDate = DateTime.Now;
            }
            if (model.FollowUpDate==null)
            {
                model.FollowUpDate = DateTime.Now.AddDays(1);
            }
            tblDoctorTreatment tbl = new tblDoctorTreatment() {
                AdmitId=model.AdmitId,
                DoctorId=model.DoctorId,
                FollowUpDate=model.FollowUpDate,
                IsDelete=false,
                Procedures=model.Procedures,
                TreatmentDate=model.TreatmentDate,
                TreatmentDetails=model.TreatmentDetails
            };
            
            objData.tblDoctorTreatments.InsertOnSubmit(tbl);
            objData.SubmitChanges();

            return new DoctorTreatResponse() { Id = tbl.TreatId, status = 0 };
        }

        public DoctorTreatResponse Update(DoctorTreatmentModel model)
        {
            tblDoctorTreatment tbl = objData.tblDoctorTreatments.Where(p => p.TreatId == model.TreatId).FirstOrDefault();
            if (tbl!=null)
            {
                tbl.AdmitId = model.AdmitId;
                tbl.DoctorId = model.DoctorId;
                tbl.FollowUpDate = model.FollowUpDate;
                tbl.Procedures = model.Procedures;
                tbl.TreatmentDate = model.TreatmentDate;
                tbl.TreatmentDetails = model.TreatmentDetails;

                objData.SubmitChanges();
            }
            return new DoctorTreatResponse() { Id = tbl.TreatId, status = 0 };
        }
    }


}