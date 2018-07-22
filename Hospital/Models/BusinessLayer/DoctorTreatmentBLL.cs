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
                                                   TreatmentTime=tbl.TreatmentTime,
                                                   TotalTaxAmount=tbl.TotalTaxAmount==null?0:tbl.TotalTaxAmount,
                                                   NetAmount=(tbl.TotalAmount==null?0:tbl.TotalAmount) + (tbl.TotalTaxAmount==null?0:tbl.TotalTaxAmount)
                                              }).ToList();
            return lst;
        }

        public DoctorTreatResponse Save(DoctorTreatmentModel model,bool IsValid=false)
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
                AdmitId = model.AdmitId,
                DoctorId = model.DoctorId,
                IsDelete = false,
                TreatmentPro = model.Procedures,
                Bill_Date = model.TreatmentDate,
                TreatmentDetails = model.TreatmentDetails,
                TotalAmount = model.ProductList.Sum(p => p.Amount),
                TotalTaxAmount=model.TotalTaxAmount,
                NetAmount=model.NetAmount,
                TreatmentTime=model.TreatmentTime,
            };

            objData.tblOTMedicineBills.InsertOnSubmit(tbl);
            objData.SubmitChanges();

            foreach (var item in model.ProductList)
            {
                tblOTMedicineBillDetail medicine = new tblOTMedicineBillDetail()
                {
                    Amount = item.Amount,
                    BillNo = tbl.BillNo,
                    IsDelete = false,
                    Price = item.Price,
                    Quantity = item.Quantity,
                    TabletId = item.ProductId,
                    TaxPercent=item.TaxPercent,
                    TaxAmount=item.TaxAmount,
                };
                objData.tblOTMedicineBillDetails.InsertOnSubmit(medicine);

                tblStockDetail stock = new tblStockDetail()
                {
                    ProductId = item.ProductId.Value,
                    OpeningQtyDate = model.TreatmentDate,
                    InwardQty = 0,
                    InwardPrice = 0,
                    OutwardQty = item.Quantity,
                    OutwardPrice = item.Price,
                    DocumentNo = tbl.BillNo,
                    TransactionType = "DT",
                    IsDelete = false,
                    BatchNo = item.BatchNo,
                    ExpiryDate = item.ExpiryDate,
                    InwardAmount = 0,
                    OutwardAmount = item.Amount,
                };
                objData.tblStockDetails.InsertOnSubmit(stock);

            }
            objData.SubmitChanges();

            return new DoctorTreatResponse() { Id = tbl.BillNo, status = 0 };
        }

        public DoctorTreatResponse Update(DoctorTreatmentModel model)
        {
            if (model.TreatmentDate==null)
            {
                model.TreatmentDate = DateTime.Now;
            }
            if (model.FollowUpDate==null)
            {
                model.FollowUpDate = DateTime.Now.AddDays(1);
            }
            tblOTMedicineBill tbl = objData.tblOTMedicineBills.Where(p => p.BillNo == model.TreatId).FirstOrDefault();
            if (tbl!=null)
            {
                tbl.AdmitId = model.AdmitId;
                tbl.DoctorId = model.DoctorId;
                tbl.TreatmentPro=model.Procedures;
                tbl.Bill_Date=model.TreatmentDate;
                tbl.TreatmentDetails=model.TreatmentDetails;
                tbl.TotalAmount = model.ProductList.Sum(p => p.Amount);
            }
            foreach (var item in model.ProductList)
            {
                if (item.BillDetailId==0)
                {
                    tblOTMedicineBillDetail medicine = new tblOTMedicineBillDetail()
                    {
                        Amount = item.Amount,
                        BillNo = tbl.BillNo,
                        IsDelete = false,
                        Price = item.Price,
                        Quantity = item.Quantity,
                        TabletId = item.ProductId
                    };
                    objData.tblOTMedicineBillDetails.InsertOnSubmit(medicine);

                    tblStockDetail stock = new tblStockDetail()
                    {
                        ProductId = item.ProductId.Value,
                        OpeningQtyDate = model.TreatmentDate,
                        InwardQty = 0,
                        InwardPrice = 0,
                        OutwardQty = item.Quantity,
                        OutwardPrice = item.Price,
                        DocumentNo =Convert.ToInt32(model.TreatId),
                        TransactionType = "DT",
                        IsDelete = false,
                        BatchNo = item.BatchNo,
                        ExpiryDate = item.ExpiryDate,
                        InwardAmount = 0,
                        OutwardAmount = item.Amount,
                    };
                    objData.tblStockDetails.InsertOnSubmit(stock);    
                }
                else
                {
                    tblOTMedicineBillDetail medicine = objData.tblOTMedicineBillDetails.Where(p=>p.BillDetailId==item.BillDetailId).FirstOrDefault();
                    if(medicine!=null)
                    {
                        medicine.Amount = item.Amount;
                        medicine.BillNo = tbl.BillNo;
                        medicine.Price = item.Price;
                        medicine.Quantity = item.Quantity;
                        medicine.TabletId = item.ProductId;
                    };

                    tblStockDetail stock = objData.tblStockDetails.Where(p=>p.ProductId==item.ProductId && p.DocumentNo==model.TreatId && p.TransactionType=="DT").FirstOrDefault();
                    if(stock!=null)
                    {
                        stock.ProductId = item.ProductId.Value;
                        stock.OpeningQtyDate = model.TreatmentDate;
                        stock.InwardQty = 0;
                        stock.InwardPrice = 0;
                        stock.OutwardQty = item.Quantity;
                        stock.OutwardPrice = item.Price;
                        stock.DocumentNo = Convert.ToInt32(model.TreatId);
                        stock.TransactionType = "DT";
                        stock.BatchNo = item.BatchNo;
                        stock.ExpiryDate = item.ExpiryDate;
                        stock.InwardAmount = 0;
                        stock.OutwardAmount = item.Amount;
                    };
                }
            }
            objData.SubmitChanges();

            return new DoctorTreatResponse();// { Id = tbl.TreatId, status = 0 };
        }
    }


}