﻿using Hospital.Models.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Hospital.Models.Models;

namespace Hospital.Models.BusinessLayer
{
    public class PatientMasterBLL
    {
        clsDataAccess mobjDataAcces = new clsDataAccess();
        public PatientMasterBLL()
        {
            objData = new CriticareHospitalDataContext();
        }

        public EntityPatientAdmit GetPatientAdmitDetails(int AdmitId)
        {
            EntityPatientAdmit admit = null;
            try
            {
                admit = (from tbl in objData.tblPatientAdmitDetails
                         where tbl.IsDelete == false
                         && tbl.AdmitId == AdmitId
                         select new EntityPatientAdmit
                         {
                             AdmitDate = tbl.AdmitDate,
                             AdmitId = tbl.AdmitId,
                             PatientId = tbl.PatientId,
                             Age = tbl.Age.Value,
                             IsDischarge = tbl.IsDischarge
                         }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return admit;
        }

        public tblPatientMaster GetPatientbyId(int id)
        {
            tblPatientMaster obj = null;
            try
            {
                obj = (from tbl in objData.tblPatientMasters
                       where tbl.IsDelete == false
                       && tbl.PKId == id
                       select tbl).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }

        public tblPatientMaster GetPatientbyCode(int id)
        {
            tblPatientMaster obj = null;
            try
            {
                obj = (from tbl in objData.tblPatientMasters
                       join tblAdmit in objData.tblPatientAdmitDetails
                       on tbl.PKId equals tblAdmit.PatientId
                       where tbl.IsDelete == false
                       && tblAdmit.AdmitId == id
                       select tbl).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obj;
        }

        public CriticareHospitalDataContext objData { get; set; }
        public DataTable GetNewPatientCode()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetNewPatientCode");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ldt;
        }

        public DataTable GetNewIPDNumber()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetNewIPDNumber");
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return ldt;
        }

        public DataTable GetNewOPDNumber()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetNewOPDNumber");
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return ldt;
        }

        public DataTable GetRefDoctors()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetRefDoctors");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ldt;
        }

        public DataTable GetInitials()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetAllInitialsForPatientMaster");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ldt;
        }

        public DataTable GetGenders()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetGenders");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ldt;
        }

        public DataTable GetOccupation()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetOccupation");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ldt;
        }

        public DataTable GetDeptCategory()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetDeptCategory");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ldt;
        }

        public DataTable GetConsultants()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetConsultants");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ldt;
        }

        public DataTable GetCompanies()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetCompanies");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ldt;
        }

        public DataTable GetCompaniesId()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetCompaniesId");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ldt;
        }

        public DataTable GetReligions()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetReligions");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ldt;
        }

        public DataTable GetCastes(int pintReligionId)
        {
            DataTable ldt = new DataTable();
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@ReligionId", DbType.Int32, pintReligionId);
                ldt = mobjDataAcces.GetDataTable("sp_GetCastes", lstParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ldt;
        }

        public DataTable GetDeptDoctors(int pintReligionId)
        {
            DataTable ldt = new DataTable();
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@DeptDoctorId", DbType.Int32, pintReligionId);
                ldt = mobjDataAcces.GetDataTable("sp_GetDeptDoctors", lstParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ldt;
        }

        public DataTable GetDeptVisitDoctors(int pintReligionId)
        {
            DataTable ldt = new DataTable();
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@DeptDoctorId", DbType.Int32, pintReligionId);
                ldt = mobjDataAcces.GetDataTable("sp_GetDeptVisitingDoctors", lstParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ldt;
        }

        public EntityPatientMaster GetDeptDoctor(int pintCategoryId)
        {
            try
            {
                EntityPatientMaster objPat = (from tbl in objData.tblDocCategories
                                              join tblAdmit in objData.tblEmployees
                                              on tbl.DocId equals tblAdmit.PKId
                                              where tbl.OperaCatId.Equals(pintCategoryId)
                                              select new EntityPatientMaster { FullName = tblAdmit.EmpFirstName + ' ' + tblAdmit.EmpMiddleName + ' ' + tblAdmit.EmpLastName }).FirstOrDefault();
                return objPat;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataTable GetFloors()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetFloors");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ldt;
        }

        public List<tblInsuranceComMaster> GetInsuranceCompanies()
        {
            return (from tbl in objData.tblInsuranceComMasters
                    select tbl).ToList();
        }

        public DataTable GetInsuranceCompaniesId()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetInsuranceCompanyId");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ldt;
        }

        public DataTable GetWardByFloor(int pintPKId)
        {
            DataTable ldt = new DataTable();
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@PKId", DbType.Int32, pintPKId);
                ldt = mobjDataAcces.GetDataTable("sp_GetWardByFloor", lstParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ldt;
        }

        public DataTable GetWard()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetWard");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ldt;
        }

        public DataTable GetRoomCategoryByFloor(int pintFloorNo)
        {
            DataTable ldt = new DataTable();
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@FloorId", DbType.Int32, pintFloorNo);
                ldt = mobjDataAcces.GetDataTable("sp_GetRoomIdByFloor", lstParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ldt;
        }

        public DataTable GetBedByWard(int pintRoomId, int pintFloorNo)
        {
            DataTable ldt = new DataTable();
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@RoomId", DbType.Int32, pintRoomId);
                Commons.ADDParameter(ref lstParam, "@FloorNo", DbType.Int32, pintFloorNo);
                ldt = mobjDataAcces.GetDataTable("sp_GetBedNoByWard", lstParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ldt;
        }

        public DataTable GetState(int pintCountryId)
        {
            DataTable ldt = new DataTable();
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@CountryId", DbType.Int32, pintCountryId);
                ldt = mobjDataAcces.GetDataTable("sp_GetStateForInsurance");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ldt;
        }

        public DataTable GetCompanyDetail(string pstrCompanyCode)
        {
            DataTable ldt = new DataTable();
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@CompanyCode", DbType.String, pstrCompanyCode);
                ldt = mobjDataAcces.GetDataTable("sp_GetCompaniesByCompanyCode", lstParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ldt;
        }

        public DataTable GetIdProofFile(string pstrPatientId)
        {
            DataTable ldt = new DataTable();
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@PatientCode", DbType.String, pstrPatientId);
                ldt = mobjDataAcces.GetDataTable("sp_GetIDProofFile", lstParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ldt;
        }

        public DataTable GetInsurenceIdFile(string pstrPatientId)
        {
            DataTable ldt = new DataTable();
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@PatientCode", DbType.String, pstrPatientId);
                ldt = mobjDataAcces.GetDataTable("sp_GetInsuranceProofFile", lstParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ldt;
        }

        public DataTable GetState()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetStates");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ldt;
        }

        public DataTable GetCity()
        {
            DataTable ldt = new DataTable();
            try
            {

                ldt = mobjDataAcces.GetDataTable("sp_GetCityForInsurance");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ldt;
        }

        public DataTable GetAllCastes()
        {
            DataTable ldt = new DataTable();
            try
            {

                ldt = mobjDataAcces.GetDataTable("sp_GetAllCaste");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ldt;
        }

        public DataTable GetAllOPD()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetAllOPD");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ldt;
        }

        public DataTable GetAllPatients()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_SelectAllRegisteredPatients");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ldt;
        }

        public int InsertIPDPatient(EntityPatientMaster entPatientMaster)
        {
            int cnt = 0;

            try
            {
                tblPatientMaster obj = new tblPatientMaster()
                {
                    Address = entPatientMaster.PatientAddress,
                    AdminDate = entPatientMaster.PatientAdmitDate,
                    AdmitTime = entPatientMaster.PatientAdmitTime,
                    Age = entPatientMaster.Age,
                    AgeIn = entPatientMaster.AgeIn,
                    Weight = entPatientMaster.Weight,
                    BedNo = entPatientMaster.BedNo,
                    BirthDate = entPatientMaster.BirthDate,
                    BloodGroup = entPatientMaster.BloodGroup,
                    Caste = entPatientMaster.Caste.ToString(),
                    City = entPatientMaster.City,
                    ComContNo = entPatientMaster.CompanyContact,
                    CompanyCode = entPatientMaster.CompanyCode,
                    CompanyAddress = entPatientMaster.CompanyAddress,
                    ContactNo = entPatientMaster.PatientContactNo,
                    CompanyId = entPatientMaster.CompanyId,
                    Country = entPatientMaster.Country,
                    Dignosys = entPatientMaster.Dignosys,
                    Gender = Convert.ToChar(entPatientMaster.Gender),
                    Initial = entPatientMaster.PatientInitial,
                    InsuranceCompanyId = entPatientMaster.InsuranceCompID,
                    InsuranceCompName = entPatientMaster.InsuranceCompName,
                    Occupation = entPatientMaster.Occupation,
                    //DeptCategory=entPatientMaster.DeptCategory,
                    //DeptDoctorId=entPatientMaster.DeptDoctorId,
                    PatientCode = entPatientMaster.PatientCode,
                    PatientFirstName = entPatientMaster.PatientFirstName,
                    PatientMiddleName = entPatientMaster.PatientMiddleName,
                    PatientLastName = entPatientMaster.PatientLastName,
                    PatientType = entPatientMaster.PatientType,
                    Religion = entPatientMaster.Religion.ToString(),
                    State = entPatientMaster.State,
                    ReferedBy = entPatientMaster.ReferedBy,
                    PastMedHistory = entPatientMaster.PastMedicalHistory,
                    OPDRoomNo = "0",
                    FloorNo = 0,
                    WardNo = 0,
                    ConsultingDr = 0,
                    EntryDate = DateTime.Now.Date,
                    FamilyHistory = "",
                    EntryBy = "0",
                    ChangeBy = "0",
                    PatientCategory = "0",
                    IsDelete = false,
                    IPDNo = "0",
                    OPDNo = "0",
                    CompanyName = entPatientMaster.CompName,
                    InsuranceName = entPatientMaster.InsuName,
                    BP = entPatientMaster.BP,
                    PatientTypeId = entPatientMaster.PatientTypeId,
                    DeptCategory=entPatientMaster.DeptCategory,
                    DeptDoctorId=entPatientMaster.DeptDoctorId,
                };
                
                objData.tblPatientMasters.InsertOnSubmit(obj);

                objData.SubmitChanges();
                tblPatientAdmitDetail lastrec = (from tbl in objData.tblPatientAdmitDetails
                                                 where tbl.PatientId == obj.PKId
                                                 orderby tbl.AdmitId descending
                                                 select tbl).FirstOrDefault();

                if (lastrec!=null)
                {
                    lastrec.ProvDiag = entPatientMaster.ProvDiag;

                    lastrec.FinalDiag = entPatientMaster.FinalDiag;

                    lastrec.Ailergies = entPatientMaster.Ailergies;

                    lastrec.Symptomes = entPatientMaster.Symptomes;

                    lastrec.PastIllness = entPatientMaster.PastIllness;

                    lastrec.Temperature = entPatientMaster.Temperature;

                    lastrec.Pulse = entPatientMaster.Pulse;

                    lastrec.Respiration = entPatientMaster.Respiration;

                    lastrec.Others = entPatientMaster.Others;

                    lastrec.RS = entPatientMaster.RS;

                    lastrec.CVS = entPatientMaster.CVS;

                    lastrec.PA = entPatientMaster.PA;

                    lastrec.CNS = entPatientMaster.CNS;

                    lastrec.OBGY = entPatientMaster.OBGY;

                    lastrec.XRAY = entPatientMaster.XRAY;

                    lastrec.ECG = entPatientMaster.ECG;

                    lastrec.USG = entPatientMaster.USG;

                    objData.SubmitChanges();
                }
                cnt++;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return cnt;
        }

        public int UpdateIPDPatient(EntityPatientMaster entPatientMaster)
        {
            int cnt = 0;

            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@PatientCode", DbType.String, entPatientMaster.PatientCode);
                Commons.ADDParameter(ref lstParam, "@Initial", DbType.Int32, entPatientMaster.PatientInitial);
                Commons.ADDParameter(ref lstParam, "@PatientFirstName", DbType.String, entPatientMaster.PatientFirstName);
                Commons.ADDParameter(ref lstParam, "@PatientMiddleName", DbType.String, entPatientMaster.PatientMiddleName);
                Commons.ADDParameter(ref lstParam, "@PatientLastName", DbType.String, entPatientMaster.PatientLastName);
                Commons.ADDParameter(ref lstParam, "@AdminDate", DbType.DateTime, entPatientMaster.PatientAdmitDate);
                Commons.ADDParameter(ref lstParam, "@AdmitTime", DbType.String, entPatientMaster.PatientAdmitTime);
                Commons.ADDParameter(ref lstParam, "@Address", DbType.String, entPatientMaster.PatientAddress);
                Commons.ADDParameter(ref lstParam, "@ContactNo", DbType.String, entPatientMaster.PatientContactNo);
                Commons.ADDParameter(ref lstParam, "@BirthDate", DbType.DateTime, entPatientMaster.BirthDate);
                Commons.ADDParameter(ref lstParam, "@ReferedBy", DbType.String, entPatientMaster.ReferedBy);
                Commons.ADDParameter(ref lstParam, "@Gender", DbType.String, entPatientMaster.Gender);
                Commons.ADDParameter(ref lstParam, "@Age", DbType.Int32, entPatientMaster.Age);
                Commons.ADDParameter(ref lstParam, "@Occupation", DbType.Int32, entPatientMaster.Occupation);
                Commons.ADDParameter(ref lstParam, "@Religion", DbType.String, entPatientMaster.Religion);
                Commons.ADDParameter(ref lstParam, "@Caste", DbType.String, entPatientMaster.Caste);
                Commons.ADDParameter(ref lstParam, "@City", DbType.String, entPatientMaster.City);
                Commons.ADDParameter(ref lstParam, "@State", DbType.String, entPatientMaster.State);
                Commons.ADDParameter(ref lstParam, "@Country", DbType.String, entPatientMaster.Country);
                Commons.ADDParameter(ref lstParam, "@CompanyCode", DbType.String, entPatientMaster.CompanyCode);
                Commons.ADDParameter(ref lstParam, "@ChangeBy", DbType.String, entPatientMaster.EntryBy);
                Commons.ADDParameter(ref lstParam, "@PersonalHistory", DbType.String, entPatientMaster.PersonalHistory);
                Commons.ADDParameter(ref lstParam, "@PastMedHistory", DbType.String, entPatientMaster.PastMedicalHistory);
                Commons.ADDParameter(ref lstParam, "@FamilyHistory", DbType.String, entPatientMaster.FamilyHistory);
                Commons.ADDParameter(ref lstParam, "@BloodGroup", DbType.String, entPatientMaster.BloodGroup);
                cnt = mobjDataAcces.ExecuteQuery("sp_UpdatePatientMaster", lstParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return cnt;
        }

        public DataTable GetFilteredPatientDetail(DateTime StartDate, DateTime EndDate, string pstrPatientCode)
        {
            DataTable ldt = new DataTable();
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@StartDate", DbType.Date, StartDate);
                Commons.ADDParameter(ref lstParam, "@EndDate", DbType.Date, EndDate);
                Commons.ADDParameter(ref lstParam, "@PatientCode", DbType.String, pstrPatientCode);
                ldt = mobjDataAcces.GetDataTable("sp_GetFilteredPAtientInfo", lstParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ldt;
        }

        public DataTable GetPatientDetail(string pstrPatientCode)
        {
            DataTable ldt = new DataTable();
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@PatientCode", DbType.String, pstrPatientCode);
                ldt = mobjDataAcces.GetDataTable("sp_GetPatientInfoByPatientCode", lstParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ldt;
        }

        public DataTable GetPatientUploadedFiles(string pstrPatientCode)
        {
            DataTable ldt = new DataTable();
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@PatientCode", DbType.String, pstrPatientCode);
                ldt = mobjDataAcces.GetDataTable("sp_GetUploadedFile", lstParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ldt;
        }

        public DataTable ViewRegistredPatient(DateTime ldtStartDate, DateTime ldtEndDate, string pstrPatientCode, string pstrFirstName, string pstrMiddleName, string pstrLastName)
        {
            DataTable ldt = new DataTable();
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@StartDate", DbType.Date, ldtStartDate);
                Commons.ADDParameter(ref lstParam, "@EndDate", DbType.Date, ldtEndDate);
                Commons.ADDParameter(ref lstParam, "@PatientCode", DbType.String, pstrPatientCode);
                Commons.ADDParameter(ref lstParam, "@PatientFirstName", DbType.String, pstrFirstName);
                Commons.ADDParameter(ref lstParam, "@PatientMiddleName", DbType.String, pstrFirstName);
                Commons.ADDParameter(ref lstParam, "@PatientLastName", DbType.String, pstrFirstName);
                ldt = mobjDataAcces.GetDataTable("sp_SelectRegisteredPatientDetail", lstParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ldt;
        }

        public DataTable GetAllRegisteredPatient(string pstrPatientCode)
        {
            DataTable ldt = new DataTable();
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@PatientCode", DbType.String, pstrPatientCode);
                ldt = mobjDataAcces.GetDataTable("sp_SelectAllRegisteredPatient", lstParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ldt;
        }

        public int DeletePatient(List<EntityPatientMaster> lstentPatientMaster)
        {
            int cnt = 0;
            List<string> lstspNames = new List<string>();
            List<List<SqlParameter>> lstParamVals = new List<List<SqlParameter>>();
            try
            {
                foreach (EntityPatientMaster entPatient in lstentPatientMaster)
                {
                    lstspNames.Add("sp_DeletePatientMaster");
                    lstParamVals.Add(CreateParameterDeletePatient(entPatient));
                }
                cnt = mobjDataAcces.ExecuteTransaction(lstspNames, lstParamVals);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return cnt;
        }

        private List<SqlParameter> CreateParameterDeletePatient(EntityPatientMaster entPatientMaster)
        {
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@PatientCode", DbType.String, entPatientMaster.PatientCode);
                Commons.ADDParameter(ref lstParam, "@Discontinued", DbType.Boolean, entPatientMaster.Discontinued);
                Commons.ADDParameter(ref lstParam, "@DiscontRemark", DbType.String, entPatientMaster.DiscontRemark);
                return lstParam;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public bool CheckPatientExistforSameDate(EntityPatientAdmit entAdmit)
        {
            bool b = false;
            try
            {
                List<tblPatientAdmitDetail> admit = (from tbl in objData.tblPatientAdmitDetails
                                                     where tbl.IsDelete == false
                                                     && tbl.IsDischarge == false
                                                     && tbl.PatientId == entAdmit.PatientId
                                                     select tbl).ToList();

                if (admit.Count > 0)
                {
                    b = true;
                }
                else
                {
                    b = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return b;
        }

        public bool CheckPatientAlreadyAllocated(EntityPatientAdmit entAdmit)
        {
            bool b = false;
            try
            {
                List<tblBedAllocationToPatient> admit = (from tbl in objData.tblBedAllocationToPatients
                                                         where tbl.IsDelete == false
                                                         && tbl.DischargeDate == null
                                                         && tbl.PatientId == entAdmit.PatientId
                                                         select tbl).ToList();

                if (admit.Count > 0)
                {
                    b = true;
                }
                else
                {
                    b = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return b;
        }

        public int Save(EntityPatientAdmit admit)
        {
            int i = 0;
            try
            {
                tblPatientAdmitDetail objAdmit = new tblPatientAdmitDetail()
                {
                    AdmitDate = admit.AdmitDate,
                    AdmitTime = admit.PatientAdmitTime,
                    CompanyId = admit.CompanyId,
                    InsuranceComId = admit.InsuranceComId,
                    CompanyName = admit.CompanyName,
                    InsuranceName = admit.InsuName,
                    IsCompany = admit.IsCompany,
                    IsDelete = false,
                    IsDischarge = false,
                    IsInsured = admit.IsInsured,
                    IsIPD = admit.IsIPD,
                    IsOPD = admit.IsOPD,
                    PatientType = admit.PatientType,
                    PatientId = admit.PatientId,
                    Dignosys = admit.Dignosys,
                    Age = admit.Age,
                    Weight = admit.Weight,
                    AgeIn = admit.AgeIn,
                    //IPDNo = admit.IPDNo,
                    //OPDNo = admit.OPDNo,
                    DeptCategory = admit.DeptCategory,
                    DeptDoctorId = admit.DeptDoctorId,
                    BP=admit.BP,
                    PatientTypeId=admit.PatientTypeId,
                    ProvDiag = admit.ProvDiag,

                    FinalDiag = admit.FinalDiag,

                    Ailergies = admit.Ailergies,

                    Symptomes = admit.Symptomes,

                    PastIllness = admit.PastIllness,

                    Temperature = admit.Temperature,

                    Pulse = admit.Pulse,

                    Respiration = admit.Respiration,

                    Others = admit.Others,

                    RS = admit.RS,

                    CVS = admit.CVS,

                    PA = admit.PA,

                    CNS = admit.CNS,

                    OBGY = admit.OBGY,

                    XRAY = admit.XRAY,

                    ECG = admit.ECG,

                    USG = admit.USG,
                };
                objData.tblPatientAdmitDetails.InsertOnSubmit(objAdmit);
                objData.SubmitChanges();
                i = 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return i; ;
        }

        public int UpdatePatient(EntityPatientAdmit entAdmit)
        {
            int cnt = 0;
            try
            {
                tblPatientAdmitDetail admit = (from tbl in objData.tblPatientAdmitDetails
                                               where tbl.AdmitId == entAdmit.AdmitId && tbl.IsDischarge == false
                                               select tbl).FirstOrDefault();
                if (admit != null)
                {
                    admit.CompanyId = entAdmit.CompanyId;
                    admit.IsCompany = entAdmit.IsCompany;
                    admit.IsInsured = entAdmit.IsInsured;
                    admit.IsIPD = entAdmit.IsIPD;
                    admit.PatientType = entAdmit.PatientType;
                    admit.IsOPD = entAdmit.IsOPD;
                    admit.PatientId = entAdmit.PatientId;
                    admit.InsuranceComId = entAdmit.InsuranceComId;
                    admit.Age = entAdmit.Age;
                    admit.Weight = entAdmit.Weight;
                    admit.AgeIn = entAdmit.AgeIn;
                    //admit.IPDNo = entAdmit.IPDNo;
                    //admit.OPDNo = entAdmit.OPDNo;
                    admit.Dignosys = entAdmit.Dignosys;
                    admit.DeptCategory = entAdmit.DeptCategory;
                    admit.DeptDoctorId = entAdmit.DeptDoctorId;
                    admit.InsuranceName = entAdmit.InsuName;
                    admit.CompanyName = entAdmit.CompanyName;

                    admit.ProvDiag = entAdmit.ProvDiag;

                    admit.FinalDiag = entAdmit.FinalDiag;

                    admit.Ailergies = entAdmit.Ailergies;

                    admit.Symptomes = entAdmit.Symptomes;

                    admit.PastIllness = entAdmit.PastIllness;

                    admit.Temperature = entAdmit.Temperature;

                    admit.Pulse = entAdmit.Pulse;

                    admit.Respiration = entAdmit.Respiration;

                    admit.Others = entAdmit.Others;

                    admit.RS = entAdmit.RS;

                    admit.CVS = entAdmit.CVS;

                    admit.PA = entAdmit.PA;

                    admit.CNS = entAdmit.CNS;

                    admit.OBGY = entAdmit.OBGY;

                    admit.XRAY = entAdmit.XRAY;

                    admit.ECG = entAdmit.ECG;

                    admit.USG = entAdmit.USG;
                }
                objData.SubmitChanges();
                cnt++;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return cnt;
        }

        public List<EntityInvoiceDetails> ShowPatientHistory()
        {
            List<EntityInvoiceDetails> lst = new List<EntityInvoiceDetails>();
            try
            {
                return lst = (from tbl in objData.STP_GetPatientHistory()
                              select new EntityInvoiceDetails 
                              { 
                                  PatientID = Convert.ToInt32(tbl.AdmitId), 
                                  PatientName = tbl.FullName, 
                                  BillDate = tbl.BillDate==null?DateTime.Now.Date:tbl.BillDate.Value,
                                  AdmitDate = tbl.AdmitDate==null ? DateTime.Now.Date : tbl.AdmitDate.Value,
                                  ChargesName = tbl.PatientType 
                              }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<EntityInvoiceDetails> SearchForPatientHistory(string Prefix)
        {
            List<EntityInvoiceDetails> lst = null;
            try
            {
                lst = (from tbl in ShowPatientHistory()
                       where (tbl.PatientName.ToString().ToUpper().Trim().Contains(Prefix.ToString().ToUpper().Trim())) || (tbl.ChargesName.ToString().ToUpper().Trim().Contains(Prefix.ToString().ToUpper().Trim()))
                       select new EntityInvoiceDetails { PatientID = tbl.PatientID, PatientName = tbl.PatientName, AdmitDate = tbl.AdmitDate, BillDate = tbl.BillDate, ChargesName = tbl.ChargesName }).ToList();
                return lst;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int ShiftToIPD(int AdmitId)
        {
            int i = 0;
            try
            {
                tblPatientAdmitDetail admit = (from tbl in objData.tblPatientAdmitDetails
                                               where tbl.AdmitId == AdmitId
                                                && tbl.IsDelete == false
                                               select tbl).FirstOrDefault();
                if (admit != null)
                {
                    admit.IsIPD = true;
                    admit.IsOPD = false;
                    admit.PatientType = "IPD";
                    objData.SubmitChanges(); i++;
                }
            }
            catch (Exception ex)
            {
                i = 0;
                throw ex;
            }
            return i;
        }

        public int UpdatePatient(EntityPatientMaster entPatientMaster)
        {
            int i = 0;
            try
            {
                tblPatientMaster objPatient = (from tbl in objData.tblPatientMasters
                                               where tbl.IsDelete == false
                                               && tbl.PKId == entPatientMaster.PKId
                                               select tbl).FirstOrDefault();
                if (objPatient != null)
                {
                    objPatient.IPDNo = "0";
                    objPatient.OPDNo = "0";
                    objPatient.Address = entPatientMaster.PatientAddress;
                    objPatient.Age = entPatientMaster.Age;
                    objPatient.AgeIn = entPatientMaster.AgeIn;
                    objPatient.Weight = entPatientMaster.Weight;
                    objPatient.BirthDate = entPatientMaster.BirthDate;
                    objPatient.BloodGroup = entPatientMaster.BloodGroup;
                    objPatient.Caste = Convert.ToString(entPatientMaster.Caste);
                    objPatient.City = entPatientMaster.City;
                    objPatient.ComContNo = entPatientMaster.CompanyContact;
                    objPatient.ContactNo = entPatientMaster.PatientContactNo;
                    //objPatient.Gender =Convert.ToChar(entPatientMaster.Gender);
                    objPatient.Initial = entPatientMaster.PatientInitial;
                    objPatient.PatientFirstName = entPatientMaster.PatientFirstName;
                    objPatient.PatientMiddleName = entPatientMaster.PatientMiddleName;
                    objPatient.PatientLastName = entPatientMaster.PatientLastName;
                    objPatient.Religion = Convert.ToString(entPatientMaster.Religion);
                    objPatient.State = entPatientMaster.State;
                    objPatient.ReferedBy = entPatientMaster.ReferedBy;
                    objPatient.Occupation = entPatientMaster.Occupation;
                    objPatient.DeptCategory = entPatientMaster.DeptCategory;
                    objPatient.DeptDoctorId = entPatientMaster.DeptDoctorId;
                    objPatient.CompanyName = entPatientMaster.CompName;
                    objPatient.InsuranceName = entPatientMaster.InsuName;
                    objPatient.CompanyId = entPatientMaster.CompanyId;
                    objPatient.InsuranceCompanyId = entPatientMaster.InsuranceCompID;
                    objPatient.InsuranceCompName = entPatientMaster.InsuranceCompName;
                    objPatient.BP = entPatientMaster.BP;
                    objPatient.PatientTypeId = entPatientMaster.PatientTypeId;


                    tblPatientAdmitDetail lastrec = (from tbl in objData.tblPatientAdmitDetails
                                                     where tbl.PatientId == entPatientMaster.PKId
                                                     orderby tbl.AdmitId descending
                                                     select tbl).FirstOrDefault();

                    if (lastrec != null)
                    {
                        lastrec.ProvDiag = entPatientMaster.ProvDiag;

                        lastrec.FinalDiag = entPatientMaster.FinalDiag;

                        lastrec.Ailergies = entPatientMaster.Ailergies;

                        lastrec.Symptomes = entPatientMaster.Symptomes;

                        lastrec.PastIllness = entPatientMaster.PastIllness;

                        lastrec.Temperature = entPatientMaster.Temperature;

                        lastrec.Pulse = entPatientMaster.Pulse;

                        lastrec.Respiration = entPatientMaster.Respiration;

                        lastrec.Others = entPatientMaster.Others;

                        lastrec.RS = entPatientMaster.RS;

                        lastrec.CVS = entPatientMaster.CVS;

                        lastrec.PA = entPatientMaster.PA;

                        lastrec.CNS = entPatientMaster.CNS;

                        lastrec.OBGY = entPatientMaster.OBGY;

                        lastrec.XRAY = entPatientMaster.XRAY;

                        lastrec.ECG = entPatientMaster.ECG;

                        lastrec.USG = entPatientMaster.USG;

                        objData.SubmitChanges();
                    }
                    //List<tblPatientAdmitDetail> lst = (from tbl in objData.tblPatientAdmitDetails
                    //                                   where tbl.IsDelete == false
                    //                                   && tbl.PatientId == objPatient.PKId
                    //                                   select tbl).ToList();
                    //if (lst.Count > 0)
                    //{
                    //    foreach (tblPatientAdmitDetail item in lst)
                    //    {
                    //        //if (string.IsNullOrEmpty(item.Dignosys))
                    //        //{
                    //        item.Dignosys = entPatientMaster.Dignosys;
                    //        item.Age = entPatientMaster.Age;
                    //        item.AgeIn = entPatientMaster.AgeIn;
                    //        item.DeptCategory = entPatientMaster.DeptCategory;
                    //        item.DeptDoctorId = entPatientMaster.DeptDoctorId;
                    //        item.CompanyName = entPatientMaster.CompName;
                    //        item.InsuranceName = entPatientMaster.InsuName;
                    //        //item.BP = entPatientMaster.BP;
                    //        if (entPatientMaster.CompanyId > 0)
                    //        {
                    //            item.IsCompany = true;
                    //            item.CompanyId = entPatientMaster.CompanyId;

                    //        }
                    //        if (entPatientMaster.InsuranceCompID > 0)
                    //        {
                    //            item.IsInsured = true;
                    //            item.InsuranceComId = entPatientMaster.InsuranceCompID;
                    //        }
                    //        //}
                    //    }
                    //}
                    objData.SubmitChanges();
                    i++;
                }
            }
            catch (Exception)
            {
                i = 0;
            }
            return i;
        }

        public EntityPatientMaster GetPatientDetailsByPatientId(int PatientId=0,int AdmitId=0)
        {
            EntityPatientMaster objPatient = null;
            try
            {
                List<EntityPatientMaster> lst = (from tbl in objData.tblPatientMasters
                              join tblAdmit in objData.tblPatientAdmitDetails
                              on tbl.PKId equals tblAdmit.PatientId
                              join tblSex in objData.tblGenders
                              on tbl.Gender equals tblSex.PKId
                              where tblAdmit.IsDelete == false
                              select new EntityPatientMaster
                              {
                                  Age = Convert.ToInt32(tblAdmit.Age),
                                  AgeIn = tblAdmit.AgeIn,
                                  BloodGroup = tbl.BloodGroup,
                                  PatientCode = tbl.PatientCode,
                                  GenderDesc = tblSex.GenderDesc,
                                  PatientAdmitDate = tblAdmit.AdmitDate == null ? DateTime.Now.Date : tblAdmit.AdmitDate.Value,
                                  AdmitId=tblAdmit.AdmitId,
                                  BedNo=tbl.BedNo,
                                  BirthDate=tbl.BirthDate,
                                  BP=tblAdmit.BP,
                                  Caste=Convert.ToInt32(tbl.Caste),
                                  ChangeBy=tbl.ChangeBy,
                                  City=tbl.City,
                                  CompanyAddress=tbl.CompanyAddress,
                                  CompanyCode=tbl.CompanyCode,
                                  CompanyContact=tbl.ComContNo,
                                  CompanyId=tblAdmit.CompanyId,
                                  CompName=tbl.CompanyName,
                                  ConsultingDr=tbl.ConsultingDr,
                                  Country=tbl.Country,
                                  DeptCategory=tblAdmit.DeptCategory,
                                  DeptDoctorId=tblAdmit.DeptDoctorId,
                                  Dignosys=tblAdmit.Dignosys,
                                  //FloorNo=tbl.FloorNo,
                                  PatientFirstName = tbl.PatientFirstName,
                                  PatientMiddleName = tbl.PatientMiddleName,
                                  PatientLastName = tbl.PatientLastName,
                                  AdmitTime=tblAdmit.AdmitTime,
                                  PatientType=tblAdmit.PatientType,
                                  Address=tbl.Address,
                                  ContactNo=tbl.ContactNo,
                                  ReferedBy=tbl.ReferedBy,
                                  Weight=tbl.Weight,
                                  Occupation=tbl.Occupation,
                                  State=tbl.State,
                                  InsuranceCompanyId=tblAdmit.InsuranceComId,
                                  IsInsuered=tblAdmit.IsInsured,
                                  Religion=Convert.ToInt32(tbl.Religion),
                                  //IPDNo=tblAdmit.IPDNo,
                                  //OPDNo=tblAdmit.OPDNo,

                              }).ToList();

                if (PatientId>0)
                {
                    objPatient = lst.Where(p => p.PatientId == PatientId).FirstOrDefault();
                }
                if (AdmitId>0)
                {
                    objPatient = lst.Where(p => p.PatientId == PatientId).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objPatient;
        }

        internal tblPatientMaster GetPatientDetailsByPatientIdForUpdate(int PatientId)
        {
            tblPatientMaster objPatient = null;
            try
            {
                objPatient = (from tbl in objData.tblPatientMasters
                              where tbl.PKId == PatientId
                              && tbl.IsDelete == false
                              select tbl).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objPatient;
        }

        public string ExistIPD(string Ipdno)
        {
            try
            {
                tblPatientMaster model = (from tbl in objData.tblPatientMasters
                                          where tbl.IsDelete == false
                                          && tbl.IPDNo == Ipdno
                                          select tbl).FirstOrDefault();

                if (model != null)
                {
                    Ipdno = null;
                }
                return Ipdno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EntityPatientMaster GetDischargeOnDiadnosis(int patientId)
        {
            try
            {
                EntityPatientMaster objPat = (from tbl in objData.tblPatientMasters
                                              join tblAdmit in objData.tblPatientAdmitDetails
                                              on tbl.PKId equals tblAdmit.PatientId
                                              where tblAdmit.AdmitId.Equals(patientId)
                                              select new EntityPatientMaster { Dignosys = Convert.ToString(tblAdmit.Dignosys) }).FirstOrDefault();
                return objPat;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        internal List<tblPatientType> GetPatientTypes()
        {
            return (from tbl in objData.tblPatientTypes
                    where tbl.IsDelete == false
                    select tbl).ToList();
        }
    }
}