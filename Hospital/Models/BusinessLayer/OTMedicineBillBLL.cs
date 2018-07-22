using Hospital.Models.Models;
using Hospital.Models.DataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Hospital.Models.BusinessLayer
{
    public class OTMedicineBillBLL
    {
        clsDataAccess mobjDataAcces = new clsDataAccess();
        public OTMedicineBillBLL()
        {
            objData = new CriticareHospitalDataContext();
        }
        public CriticareHospitalDataContext objData { get; set; }

        public tblOTMedicineBill GetPrescriptionInfo(int BillNo=0,int PatientId=0,bool IsDischarge=false)
        {
            tblOTMedicineBill obj = null;
            try
            {
                if (BillNo==0)
                {
                    obj = (from tbl in objData.tblOTMedicineBills
                           join tblAdmit in objData.tblPatientAdmitDetails
                           on tbl.AdmitId equals tblAdmit.AdmitId
                           where tbl.IsDelete == false
                           && tbl.AdmitId == PatientId
                           && tblAdmit.IsDischarge == IsDischarge
                           select tbl).FirstOrDefault();
                }
                else
                {
                    obj = (from tbl in objData.tblOTMedicineBills
                           where tbl.IsDelete == false
                           && tbl.BillNo == BillNo
                           select tbl).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }

        public EntityPatientAdmit GetAdmitDate(int AdmitId)
        {
            EntityPatientAdmit lst = (from tbl in objData.tblPatientAdmitDetails
                                      where tbl.IsDelete == false
                                      &&
                                      tbl.AdmitId == AdmitId
                                      select new EntityPatientAdmit
                                      {
                                          AdmitDate = tbl.AdmitDate
                                      }).FirstOrDefault();
            return lst;
        }
        public List<EntityPatientAdmit> GetPatientList()
        {
            List<EntityPatientAdmit> lst = null;
            try
            {
                lst = (from tbl in objData.tblPatientAdmitDetails
                       join tblpa in objData.tblPatientMasters
                       on tbl.PatientId equals tblpa.PKId
                       where tbl.IsDischarge == false
                       &&
                       tbl.IsDelete == false
                       select new EntityPatientAdmit
                       {
                           AdmitId = tbl.AdmitId,
                           PatientFirstName = tblpa.PatientFirstName + ' ' + tblpa.PatientMiddleName + ' ' + tblpa.PatientLastName
                       }).ToList();
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(tblOTMedicineBill obj, List<EntityOTMedicineBillDetails> lst)
        {
            try
            {
                tblOTMedicineBill objcurrent = (from tbl in objData.tblOTMedicineBills
                                                where tbl.BillNo == obj.BillNo
                                                select tbl).FirstOrDefault();
                if (objcurrent != null)
                {
                    objcurrent.AdmitId = obj.AdmitId;
                    objcurrent.Bill_Date = obj.Bill_Date;
                    objcurrent.TotalAmount = obj.TotalAmount;
                }

                foreach (EntityOTMedicineBillDetails item in lst)
                {
                    tblOTMedicineBillDetail objsal = new tblOTMedicineBillDetail();
                    objsal = (from tbl in objData.tblOTMedicineBillDetails
                              where tbl.BillDetailId == item.BillDetailId
                              && tbl.BillNo == item.BillNo && tbl.TabletId == item.TabletId
                              && tbl.IsDelete == false
                              select tbl).FirstOrDefault();
                    if (objsal != null)
                    {
                        objsal.TabletId = Convert.ToInt32(item.TabletId);
                        objsal.Quantity = item.Quantity;
                        objsal.Price = item.Price;
                        objsal.Amount = item.Quantity * item.Price;
                        objsal.IsDelete = item.IsDelete;
                        objsal.MorningQty = item.MorningQty;
                        objsal.EveningQty = item.EveningQty;
                        objsal.AfterNoonQty = item.AfterNoonQty;
                        objsal.NightQty = item.NightQty;
                    }
                    else
                    {
                        objsal = new tblOTMedicineBillDetail()
                        {
                            TabletId = item.TabletId,
                            Quantity = item.Quantity,
                            Price = item.Price,
                            Amount = item.Quantity * item.Price,
                            BillNo = Convert.ToInt32(obj.BillNo),
                            IsDelete = false,
                            MorningQty = item.MorningQty,
                            EveningQty = item.EveningQty,
                            AfterNoonQty = item.AfterNoonQty,
                            NightQty = item.NightQty,
                        };
                        objData.tblOTMedicineBillDetails.InsertOnSubmit(objsal);
                    }
                }
                objData.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int? Save(tblOTMedicineBill tblins, List<EntityOTMedicineBillDetails> lst)
        {
            int? BillNo = 0;
            try
            {
                objData.STP_Insert_tblOTMedicineBill(Convert.ToInt32(tblins.AdmitId), tblins.Bill_Date, tblins.TotalAmount, ref BillNo);
                foreach (EntityOTMedicineBillDetails item in lst)
                {
                    tblOTMedicineBillDetail tbl = new tblOTMedicineBillDetail()
                    {
                        TabletId = Convert.ToInt32(item.TabletId),
                        Quantity = item.Quantity,
                        Price = item.Price,
                        Amount = item.Quantity * item.Price,
                        BillNo = Convert.ToInt32(BillNo),
                        IsDelete = false,
                        MorningQty = item.MorningQty,
                        EveningQty = item.EveningQty,
                        AfterNoonQty = item.AfterNoonQty,
                        NightQty=item.NightQty,
                    };
                    objData.tblOTMedicineBillDetails.InsertOnSubmit(tbl);
                }
                objData.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return BillNo;
        }

        public bool ValidateAllocation(tblOTMedicineBill sal)
        {
            try
            {
                var res = (from tbl in objData.tblOTMedicineBills
                           where tbl.AdmitId == sal.AdmitId
                           select tbl).FirstOrDefault();
                if (res != null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public tblPatientAdmitDetail GetEmployee(int AdmitId)
        {
            return (from tbl in objData.tblPatientAdmitDetails
                    where tbl.AdmitId == AdmitId
                    select tbl).FirstOrDefault();
        }

        public List<EntityOTMedicineBill> GetInsurance()
        {
            List<EntityOTMedicineBill> lst = (from tbl in objData.tblOTMedicineBills
                                              join tbla in objData.tblPatientAdmitDetails
                                              on tbl.AdmitId equals tbla.AdmitId
                                              join tblp in objData.tblPatientMasters
                                              on tbla.PatientId equals tblp.PKId
                                              where tbl.IsDelete == false
                                              select new EntityOTMedicineBill
                                              {
                                                  BillNo = tbl.BillNo,
                                                  PatientName = tblp.PatientFirstName + " " + tblp.PatientMiddleName + " " + tblp.PatientLastName,
                                                  Bill_Date = tbl.Bill_Date,
                                                  AdmitId = tbl.AdmitId,
                                                  TotalAmount = Convert.ToDecimal(tbl.TotalAmount)
                                              }).ToList();
            return lst;
        }

        public List<EntityOTMedicineBillDetails> GetPrescription(int Id)
        {
            int i = 0;
            List<EntityOTMedicineBillDetails> lst = (from tbl in objData.tblOTMedicineBillDetails
                                                     join tblTab in objData.tblTabletMasters
                                                     on tbl.TabletId equals tblTab.PKId
                                                     where tbl.BillNo == Id
                                                     &&
                                                     tbl.IsDelete == false
                                                     select new EntityOTMedicineBillDetails
                                                     {
                                                         MedicineName = tblTab.MedicineName,
                                                         TabletId = Convert.ToInt32(tbl.TabletId),
                                                         Quantity = tbl.Quantity,
                                                         Price = Convert.ToDecimal(tbl.Price),
                                                         Amount = Convert.ToDecimal(tbl.Amount),
                                                         BillNo = tbl.BillNo,
                                                         BillDetailId = tbl.BillDetailId,
                                                         MorningQty = tbl.MorningQty!=null?tbl.MorningQty.Value:0,
                                                         EveningQty = tbl.EveningQty != null ? tbl.EveningQty.Value : 0,
                                                         AfterNoonQty = tbl.AfterNoonQty != null ? tbl.AfterNoonQty.Value : 0,
                                                         NightQty = tbl.NightQty != null ? tbl.NightQty.Value : 0,
                                                     }).ToList();
            foreach (EntityOTMedicineBillDetails item in lst)
            {
                item.TempId = i++;
            }
            return lst;
        }

        public void Update(List<EntityOTMedicineBillDetails> lst)
        {
            try
            {
                foreach (EntityOTMedicineBillDetails item in lst)
                {
                    tblOTMedicineBillDetail objsal = new tblOTMedicineBillDetail();
                    objsal = (from tbl in objData.tblOTMedicineBillDetails
                              where tbl.BillDetailId == item.BillDetailId
                              && tbl.IsDelete == false
                              select tbl).FirstOrDefault();
                    if (objsal != null)
                    {
                        objsal.TabletId = item.TabletId;
                        objsal.Quantity = item.Quantity;
                        objsal.Price = item.Price;
                        objsal.Amount = item.Quantity * item.Price;
                        objsal.IsDelete = item.IsDelete;
                        objsal.MorningQty = item.MorningQty;
                        objsal.EveningQty = item.EveningQty;
                        objsal.AfterNoonQty = item.AfterNoonQty;
                        objsal.NightQty = item.NightQty;
                    }
                    else
                    {
                        objsal = new tblOTMedicineBillDetail() { 
                            TabletId = Convert.ToInt32(item.TabletId), 
                            Quantity = item.Quantity, 
                            Price = item.Price, 
                            Amount = item.Price * item.Quantity, 
                            BillNo = Convert.ToInt32(item.BillNo),
                            MorningQty = item.MorningQty,
                            EveningQty = item.EveningQty,
                            AfterNoonQty = item.AfterNoonQty,
                            NightQty=item.NightQty,
                        };
                        objData.tblOTMedicineBillDetails.InsertOnSubmit(objsal);
                    }
                }
                objData.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<EntityOTMedicineBill> GetInsurance(string Prefix)
        {
            List<EntityOTMedicineBill> lst = null;
            try
            {
                lst = (from tbl in GetInsurance()
                       where tbl.BillNo.ToString().ToUpper().Contains(Prefix.ToUpper())
                       || tbl.PatientName.ToString().ToUpper().Contains(Prefix.ToUpper())
                       select new EntityOTMedicineBill()
                       {
                           BillNo = tbl.BillNo,
                           PatientName = tbl.PatientName,
                           Bill_Date = tbl.Bill_Date,
                           TotalAmount = tbl.TotalAmount
                       }).ToList();
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetTablet()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetTabletForPresc");
            }
            catch (Exception ex)
            {
                Commons.FileLog("PrescriptionBLL - GetTablet()", ex);
            }
            return ldt;
        }

        internal List<EntityTest> GetLabTestDetails(int BillNo)
        {
            var lst = (from tbl in objData.tblOTMedicineBillDetails
                   join tblproduct in objData.tblTestMasters
                   on (tbl.LabTestId==null?0:tbl.LabTestId) equals tblproduct.TestId
                   where tbl.BillNo == BillNo
                   && tbl.IsDelete == false
                       select new EntityTest
                   {
                       BillDetailId = tbl.BillDetailId,
                       TestCharge = tblproduct.TestCharge,
                       IsDelete = tbl.IsDelete==null?false:tbl.IsDelete.Value,
                       TestId = tbl.LabTestId==null?0:tbl.LabTestId.Value,
                       TestName = tblproduct.TestName,
                   }).ToList();
            return lst;
        }

        internal List<EntityOTMedicineBillDetails> GetBillProducts(int BillNo,int PatientId=0)
        {
            List<EntityOTMedicineBillDetails> lst = null;
            if (BillNo!=0)
            {
                lst = (from tbl in objData.tblOTMedicineBillDetails
                       join tblproduct in objData.tblProductMasters
                       on tbl.TabletId equals tblproduct.ProductId
                       where tbl.IsDelete == false
                       && tbl.BillNo == BillNo
                       && tbl.IsDelete == false
                       select new EntityOTMedicineBillDetails
                       {
                           BillDetailId = tbl.BillDetailId,
                           Amount = tbl.Amount,
                           BatchNo = tbl.BatchNo,
                           ExpiryDate = tbl.ExpiryDate,
                           IsDelete = tbl.IsDelete,
                           ProductId = tbl.TabletId,
                           ProductName = tblproduct.ProductName,
                           Quantity = (tbl.MorningQty != null ? tbl.MorningQty.Value : 0) + (tbl.AfterNoonQty != null ? tbl.AfterNoonQty.Value : 0) + (tbl.EveningQty != null ? tbl.EveningQty.Value : 0) + (tbl.NightQty != null ? tbl.NightQty.Value : 0),
                           Price = tbl.Price,
                           BillNo = BillNo,
                           TaxPercent = tbl.TaxPercent != null ? tbl.TaxPercent : 0,
                           TaxAmount = tbl.TaxAmount != null ? tbl.TaxAmount : 0,
                           MorningQty = tbl.MorningQty != null ? tbl.MorningQty.Value : 0,
                           AfterNoonQty = tbl.AfterNoonQty != null ? tbl.AfterNoonQty.Value : 0,
                           EveningQty = tbl.EveningQty != null ? tbl.EveningQty.Value : 0,
                           NightQty = tbl.NightQty != null ? tbl.NightQty.Value : 0,
                       }).ToList();    
            }
            else
            {
                lst = (from tbl in objData.tblOTMedicineBills
                       join tblbills in objData.tblOTMedicineBillDetails
                       on tbl.BillNo equals tblbills.BillNo
                       join tblp in objData.tblProductMasters
                       on tblbills.TabletId equals tblp.ProductId
                       where tblbills.IsDelete == false
                       && tbl.AdmitId == PatientId
                       select new EntityOTMedicineBillDetails
                       {
                           AfterNoonQty = tblbills.AfterNoonQty,
                           BatchNo = tblbills.BatchNo,
                           BillDetailId = tblbills.BillDetailId,
                           BillNo = tbl.BillNo,
                           EveningQty = tblbills.EveningQty,
                           ExpiryDate = tblbills.ExpiryDate,
                           IsDelete = tblbills.IsDelete,
                           TabletId = tblbills.TabletId,
                           ProductName = tblp.ProductName,
                           MorningQty = tblbills.MorningQty,
                           NightQty=tblbills.NightQty,
                           PresDate = tblbills.PresDate,
                           LabTestId=tblbills.LabTestId,
                           TestName = tblbills.LabTestId==0?"":(objData.tblTestMasters.Where(p=>p.TestId==tblbills.LabTestId).FirstOrDefault().TestName),
                       }).ToList();
            }
            return lst;
        }
    }
}