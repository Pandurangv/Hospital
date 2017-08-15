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
            List<DoctorTreatmentModel> lst = (from tbl in objData.tblOTMedicineBills
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
                                                   IsDelete=tbl.IsDelete,
                                                   PatientName=tblP.PatientFirstName + " " + tblP.PatientLastName,
                                                   Procedures=tbl.TreatmentPro,
                                                   TreatId=tbl.BillNo,
                                                   TreatmentDate=tbl.Bill_Date,
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
            tblOTMedicineBill tbl = new tblOTMedicineBill()
            {
                AdmitId=model.AdmitId,
                DoctorId=model.DoctorId,
                IsDelete=false,
                TreatmentPro=model.Procedures,
                Bill_Date=model.TreatmentDate,
                TreatmentDetails=model.TreatmentDetails,
                TotalAmount=model.TotalAmount,
            };
            
            objData.tblOTMedicineBills.InsertOnSubmit(tbl);
            objData.SubmitChanges();

            foreach (var item in model.ProductList)
            {
                tblOTMedicineBillDetail medicine = new tblOTMedicineBillDetail()
                {
                    Amount=item.Amount,
                    BillNo=tbl.BillNo,
                    IsDelete=false,
                    Price=item.Price,
                    Quantity=item.Quantity,
                    TabletId=item.TabletId
                };
                objData.tblOTMedicineBillDetails.InsertOnSubmit(medicine);

                tblStockDetail stock = new tblStockDetail()
                {
                    ProductId=item.TabletId,
                    OpeningQtyDate=model.TreatmentDate,
                    InwardQty=0,
                    InwardPrice=0,
                    OutwardQty=item.Quantity,
                    OutwardPrice=item.Price,
                    DocumentNo=tbl.BillNo,
                    TransactionType="DOCTORTREATMENT",
                    IsDelete=false,
                    BatchNo=item.BatchNo,
                    ExpiryDate=item.ExpiryDate,
                    InwardAmount=0,
                    OutwardAmount=item.Amount,
                };
                objData.tblStockDetails.InsertOnSubmit(stock);

            }
            objData.SubmitChanges();
            return new DoctorTreatResponse() { Id = tbl.BillNo, status = 0 };
        }

        public DoctorTreatResponse Update(DoctorTreatmentModel model)
        {
            //tblDoctorTreatment tbl = objData.tblDoctorTreatments.Where(p => p.TreatId == model.TreatId).FirstOrDefault();
            //if (tbl!=null)
            //{
            //    tbl.AdmitId = model.AdmitId;
            //    tbl.DoctorId = model.DoctorId;
            //    tbl.FollowUpDate = model.FollowUpDate;
            //    tbl.Procedures = model.Procedures;
            //    tbl.TreatmentDate = model.TreatmentDate;
            //    tbl.TreatmentDetails = model.TreatmentDetails;

            //    objData.SubmitChanges();
            //}
            return new DoctorTreatResponse();// { Id = tbl.TreatId, status = 0 };
        }
    }


}