using Hospital.Models.DataLayer;
using Hospital.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hospital.Models.BusinessLayer
{
    public class BankMasterBLL
    {
        public BankMasterBLL()
        {
            objData = new CriticareHospitalDataContext();
        }

        public CriticareHospitalDataContext objData { get; set; }

        public int InsertBankMaster(EntityBankMaster bank)
        {
            int i = 0;
            try
            {
                tblBankMaster objbank = new tblBankMaster()
                {
                    AccountNo = bank.AccountNo,
                    BankAddress = bank.BankAddress,
                    BankName = bank.BankName,
                    IFSCCode = bank.IFSCCode,
                    MISCCode = bank.MISCCode,
                    BranchName = bank.BranchName,
                    City = bank.City,
                    CustomerId = bank.CustomerId,
                    MobileNo = bank.MobileNo,
                    PhNo = bank.PhNo,
                    Pin = bank.Pin,
                    IsDelete = false
                };
                objData.tblBankMasters.InsertOnSubmit(objbank);
                objData.SubmitChanges();
                i++;
            }
            catch (Exception ex)
            {
                i = 0;
                throw ex;
            }
            return i;
        }

        public int Update(EntityBankMaster bank)
        {
            int i = 0;
            try
            {
                tblBankMaster objBank = (from tbl in objData.tblBankMasters
                                         where tbl.BankId == bank.BankId
                                         && tbl.IsDelete == false
                                         select tbl).FirstOrDefault();
                if (objBank != null)
                {
                    objBank.BankAddress = bank.BankAddress;
                    objBank.BankName = bank.BankName;
                    objBank.IFSCCode = bank.IFSCCode;
                    objBank.MISCCode = bank.MISCCode;
                    objBank.BranchName = bank.BranchName;
                    objBank.City = bank.City;
                    objBank.CustomerId = bank.CustomerId;
                    objBank.MobileNo = bank.MobileNo;
                    objBank.PhNo = bank.PhNo;
                    objBank.Pin = bank.Pin;
                }
                objData.SubmitChanges();
                i++;
            }
            catch (Exception ex)
            {
                i = 0;
                throw ex;
            }
            return i;
        }

        public List<EntityBankMaster> GetBankDetails()
        {
            List<EntityBankMaster> lst = null;
            try
            {
                lst = (from bank in objData.tblBankMasters
                       where bank.IsDelete == false
                       select new EntityBankMaster
                       {
                           AccountNo = bank.AccountNo,
                           BankAddress = bank.BankAddress,
                           BankName = bank.BankName,
                           IFSCCode = bank.IFSCCode,
                           MISCCode = bank.MISCCode,
                           BranchName = bank.BranchName,
                           City = bank.City,
                           CustomerId = bank.CustomerId,
                           MobileNo = bank.MobileNo,
                           PhNo = bank.PhNo,
                           Pin = bank.Pin,
                           BankId = bank.BankId
                       }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lst;
        }

        public EntityBankMaster GetBank(EntityBankMaster entSupplier)
        {
            EntityBankMaster bank = null;
            try
            {
                bank = (from tbl in objData.tblBankMasters
                        where tbl.BankId == entSupplier.BankId
                        && tbl.IsDelete == false
                        && tbl.AccountNo == entSupplier.AccountNo
                        select new EntityBankMaster { }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bank;
        }

        public List<EntityBankMaster> SelectBanks(string Prefix)
        {
            try
            {
                return (from tbl in GetBankDetails()
                        where tbl.BankName.ToUpper().Contains(Prefix.ToUpper())
                        || tbl.BankAddress.ToUpper().Contains(Prefix.ToUpper())
                        || tbl.IFSCCode.ToUpper().Contains(Prefix.ToUpper())
                        || tbl.AccountNo.ToUpper().Contains(Prefix.ToUpper())
                        select tbl).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EntityBankMaster GetBank(int BankId)
        {
            EntityBankMaster bank = null;
            try
            {
                bank = (from tbl in objData.tblBankMasters
                        where tbl.BankId == BankId
                        && tbl.IsDelete == false
                        select new EntityBankMaster
                        {
                            AccountNo = tbl.AccountNo,
                            BankAddress = tbl.BankAddress,
                            BankName = tbl.BankName,
                            IFSCCode = tbl.IFSCCode,
                            MISCCode = tbl.MISCCode,
                            BranchName = tbl.BranchName,
                            City = tbl.City,
                            CustomerId = tbl.CustomerId,
                            MobileNo = tbl.MobileNo,
                            PhNo = tbl.PhNo,
                            Pin = tbl.Pin,
                            BankId = tbl.BankId
                        }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bank;
        }

        public EntityBankMaster GetBankByAccNo(EntityBankMaster entSupplier)
        {
            EntityBankMaster bank = null;
            try
            {
                bank = (from tbl in objData.tblBankMasters
                        where tbl.IsDelete == false
                        && tbl.AccountNo == entSupplier.AccountNo
                        select new EntityBankMaster { }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bank;
        }
    }

    public class MedicalCertificateBLL
    {
        clsDataAccess mobjDataAcces = new clsDataAccess();

        public MedicalCertificateBLL()
        {
            objData = new CriticareHospitalDataContext();
        }
        public CriticareHospitalDataContext objData { get; set; }


        public List<EntityMedicalCertificate> GetAllBirthDetails()
        {
            List<EntityMedicalCertificate> lst = null;
            try
            {
                lst = (from tbl in objData.tblMedicalCertificates
                       join tbla in objData.tblPatientAdmitDetails
                       on tbl.PatientAdmitID equals tbla.AdmitId
                       join tblPat in objData.tblPatientMasters
                       on tbla.PatientId equals tblPat.PKId
                       where tbl.IsDelete == false
                       select new EntityMedicalCertificate
                       {
                           CertiId = tbl.CertiID,
                           Age = Convert.ToInt32(tbl.Age),
                           DischargeOn = tbl.DischargeOn==null?DateTime.Now.Date:tbl.DischargeOn.Value,
                           AdvisedRestDays = tbl.AdvisedRestDays,
                           ContinuedRestDays = tbl.ContinuedRestDays,
                           ContinueRestFrom = tbl.ContinueRestFrom == null ? DateTime.Now.Date : tbl.ContinueRestFrom.Value,
                           AdvisedRestFrom = tbl.AdvisedRestFrom == null ? DateTime.Now.Date : tbl.AdvisedRestFrom.Value,
                           IndoorOn = tbl.IndoorOn == null ? DateTime.Now.Date : tbl.IndoorOn.Value,
                           Daignosis = tbl.Daignosis,
                           OPDFrom = tbl.OPDFrom == null ? DateTime.Now.Date : tbl.OPDFrom.Value,
                           OPDTo = tbl.OPDTo == null ? DateTime.Now.Date : tbl.OPDTo.Value,
                           OperatedFor = tbl.OperatedFor,
                           OperatedForOn = tbl.OperatedForOn == null ? DateTime.Now.Date : tbl.OperatedForOn.Value,
                           WorkFrom = tbl.WorkFrom == null ? DateTime.Now.Date : tbl.WorkFrom.Value,
                           FullName = tblPat.PatientFirstName + ' ' + tblPat.PatientMiddleName + ' ' + tblPat.PatientLastName
                       }).ToList();
                return lst;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<EntityMedicalCertificate> GetAllPatients()
        {
            List<EntityMedicalCertificate> lst = null;
            try
            {
                lst = (from tbla in objData.tblPatientAdmitDetails
                       join tblPat in objData.tblPatientMasters
                       on tbla.PatientId equals tblPat.PKId
                       where tbla.IsDelete == false
                       && tbla.IsDischarge == false
                       select new EntityMedicalCertificate
                       {
                           PatientAdmitID = tbla.AdmitId,
                           FullName = tblPat.PatientFirstName + ' ' + tblPat.PatientMiddleName + ' ' + tblPat.PatientLastName
                       }).ToList();
                return lst;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int InsertBirthRecord(EntityMedicalCertificate entDept)
        {
            try
            {
                tblMedicalCertificate obj = new tblMedicalCertificate()
                {
                    PatientAdmitID = entDept.PatientAdmitID,
                    Age = entDept.Age,
                    Daignosis = entDept.Daignosis,
                    OPDFrom = entDept.OPDFrom,
                    OPDTo = entDept.OPDTo,
                    IndoorOn = entDept.IndoorOn,
                    DischargeOn = entDept.DischargeOn,
                    OperatedFor = entDept.OperatedFor,
                    OperatedForOn = entDept.OperatedForOn,
                    AdvisedRestDays = entDept.AdvisedRestDays,
                    AdvisedRestFrom = entDept.AdvisedRestFrom,
                    ContinuedRestDays = entDept.ContinuedRestDays,
                    ContinueRestFrom = entDept.ContinueRestFrom,
                    WorkFrom = entDept.WorkFrom
                };
                objData.tblMedicalCertificates.InsertOnSubmit(obj);
                objData.SubmitChanges();
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EntityMedicalCertificate> SelectBirthDetails(string Prefix)
        {
            List<EntityMedicalCertificate> lst = null;
            try
            {
                lst = (from tbl in objData.tblMedicalCertificates
                       join tbla in objData.tblPatientAdmitDetails
                       on tbl.PatientAdmitID equals tbla.AdmitId
                       join tblPat in objData.tblPatientMasters
                       on tbla.PatientId equals tblPat.PKId
                       where tbl.IsDelete == false
                       && (tblPat.PatientFirstName + ' ' + tblPat.PatientMiddleName + ' ' + tblPat.PatientLastName).ToString().ToUpper().Trim().Contains(Prefix.ToUpper().ToString().Trim())
                       select new EntityMedicalCertificate
                       {
                           CertiId = tbl.CertiID,
                           Age = Convert.ToInt32(tbl.Age),
                           DischargeOn = tbl.DischargeOn==null?DateTime.Now.Date:tbl.DischargeOn.Value,
                           AdvisedRestDays = tbl.AdvisedRestDays,
                           ContinuedRestDays = tbl.ContinuedRestDays,
                           ContinueRestFrom = tbl.ContinueRestFrom == null ? DateTime.Now.Date : tbl.ContinueRestFrom.Value,
                           AdvisedRestFrom = tbl.AdvisedRestFrom == null ? DateTime.Now.Date : tbl.AdvisedRestFrom.Value,
                           IndoorOn = tbl.IndoorOn == null ? DateTime.Now.Date : tbl.IndoorOn.Value,
                           Daignosis = tbl.Daignosis,
                           OPDFrom = tbl.OPDFrom == null ? DateTime.Now.Date : tbl.OPDFrom.Value,
                           OPDTo = tbl.OPDTo == null ? DateTime.Now.Date : tbl.OPDTo.Value,
                           OperatedFor = tbl.OperatedFor,
                           OperatedForOn = tbl.OperatedForOn == null ? DateTime.Now.Date : tbl.OperatedForOn.Value,
                           WorkFrom = tbl.WorkFrom == null ? DateTime.Now.Date : tbl.WorkFrom.Value,
                           FullName = tblPat.PatientFirstName + ' ' + tblPat.PatientMiddleName + ' ' + tblPat.PatientLastName
                       }).ToList();
                return lst;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}