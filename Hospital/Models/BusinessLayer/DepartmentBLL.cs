﻿using Hospital.Models.DataLayer;
using Hospital.Models.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Hospital.Models.BusinessLayer
{
    public class DepartmentBLL
    {
        clsDataAccess mobjDataAcces = new clsDataAccess();

        public DepartmentBLL()
        {
            objData = new CriticareHospitalDataContext();
            //
            // TODO: Add constructor logic here
            //
        }
        public CriticareHospitalDataContext objData { get; set; }

        public EntityDepartment GetDepartMent(EntityDepartment ent)
        {
            EntityDepartment obj = (from tbl in objData.tblDepartments
                                    where tbl.DeptDesc.Equals(ent.DeptDesc)
                                    && tbl.DeptCode.Equals(ent.DeptCode)
                                    select new EntityDepartment { DeptCode = tbl.DeptCode, DeptDesc = tbl.DeptDesc }).FirstOrDefault();
            return obj;
        }
        public EntityDepartment GetDepartMentByName(EntityDepartment ent)
        {
            EntityDepartment obj = (from tbl in objData.tblDepartments
                                    where tbl.DeptDesc.Equals(ent.DeptDesc)

                                    select new EntityDepartment { DeptCode = tbl.DeptCode, DeptDesc = tbl.DeptDesc }).FirstOrDefault();
            return obj;
        }

        public DataTable GetNewDeptCode()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetNewDeptCode");
            }
            catch (Exception ex)
            {
                Commons.FileLog("DepartmentBLL - GetNewDeptCode()", ex);
            }
            return ldt;
        }

        public List<sp_GetAllDepartmentsResult> GetAllDepartments()
        {
            return objData.sp_GetAllDepartments().ToList();
        }

        public DataTable GetDepartmentForEdit(string pstrDeptCode)
        {
            DataTable ldt = new DataTable();
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@DeptCode", DbType.String, pstrDeptCode);
                ldt = mobjDataAcces.GetDataTable("sp_GetDepartmentForEdit", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("DepartmentBLL - GetDepartmentForEdit(string pstrDeptCode)", ex);
            }
            return ldt;
        }

        public int InsertDepartment(EntityDepartment entDept)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@DeptCode", DbType.String, entDept.DeptCode);
                Commons.ADDParameter(ref lstParam, "@DeptDesc", DbType.String, entDept.DeptDesc);
                Commons.ADDParameter(ref lstParam, "@EntryBy", DbType.String, entDept.EntryBy);
                Commons.ADDParameter(ref lstParam, "@UserType", DbType.String, entDept.DeptDesc);
                cnt = mobjDataAcces.ExecuteQuery("sp_InsertDepartment", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("DepartmentBLL -  InsertDepartment(EntityDepartment entDept)", ex);
            }

            return cnt;
        }

        public int UpdateDepartment(EntityDepartment entDept)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@DeptCode", DbType.String, entDept.DeptCode);
                Commons.ADDParameter(ref lstParam, "@DeptDesc", DbType.String, entDept.DeptDesc);
                Commons.ADDParameter(ref lstParam, "@ChangeBy", DbType.String, entDept.ChangeBy);
                cnt = mobjDataAcces.ExecuteQuery("sp_UpdateDepartment", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("DepartmentBLL -  UpdateDepartment(EntityDepartment entDept)", ex);
            }

            return cnt;
        }

        public int DeleteDepartment(EntityDepartment entDept)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@DeptCode", DbType.String, entDept.DeptCode);
                cnt = mobjDataAcces.ExecuteQuery("sp_DeleteDepartment", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("DepartmentBLL - DeleteDepartment(EntityDepartment entDept)", ex);
            }
            return cnt;
        }
    }

    public class NursingManagementBLL
    {
        clsDataAccess mobjDataAcces = new clsDataAccess();
        public NursingManagementBLL()
        {
            objData = new CriticareHospitalDataContext();
        }
        public CriticareHospitalDataContext objData { get; set; }

        public tblNursingManagement GetPrescriptionInfo(int SrNo)
        {
            tblNursingManagement obj = null;
            try
            {
                obj = (from tbl in objData.tblNursingManagements
                       where tbl.IsDelete == false
                       && tbl.SrNo == SrNo
                       select tbl).FirstOrDefault();
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
        public List<EntityPatientAdmit> GetPatientList(string dept)
        {
            List<EntityPatientAdmit> lst = null;
            try
            {
                lst = (from tbl in objData.tblBedAllocationToPatients
                       join tblad in objData.tblPatientAdmitDetails
                       on tbl.PatientId equals tblad.AdmitId
                       join tblpa in objData.tblPatientMasters
                       on tblad.PatientId equals tblpa.PKId
                       join tblOper in objData.tblOperationCategories
                       on tblpa.DeptCategory equals tblOper.CategoryId
                       where tbl.IsDelete == false
                       && tblOper.CategoryName == dept
                       && tblad.IsDischarge == false
                       select new EntityPatientAdmit
                       {
                           AdmitId = Convert.ToInt32(tbl.PatientId),
                           PatientFirstName = tblpa.PatientFirstName + ' ' + tblpa.PatientMiddleName + ' ' + tblpa.PatientLastName
                       }).ToList();
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EntityPatientAdmit> GetPatientListNursing()
        {
            List<EntityPatientAdmit> lst = null;
            try
            {
                lst = (from tbl in objData.tblBedAllocationToPatients
                       join tblad in objData.tblPatientAdmitDetails
                       on tbl.PatientId equals tblad.AdmitId
                       join tblpa in objData.tblPatientMasters
                       on tblad.PatientId equals tblpa.PKId
                       where tbl.IsDelete == false
                       select new EntityPatientAdmit
                       {
                           AdmitId = Convert.ToInt32(tblpa.PKId),
                           PatientFirstName = tblpa.PatientFirstName + ' ' + tblpa.PatientMiddleName + ' ' + tblpa.PatientLastName
                       }).ToList();
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(tblNursingManagement obj, List<EntityNursingManagementDetails> lst)
        {
            try
            {
                tblNursingManagement objcurrent = (from tbl in objData.tblNursingManagements
                                                   where tbl.SrNo == obj.SrNo
                                                   select tbl).FirstOrDefault();
                if (objcurrent != null)
                {
                    objcurrent.CategoryId = obj.CategoryId;
                    objcurrent.NurseId = obj.NurseId;
                    objcurrent.TreatmentDate = obj.TreatmentDate;
                    objcurrent.Department = obj.Department;
                }

                foreach (EntityNursingManagementDetails item in lst)
                {
                    tblNursingManagementDetail objsal = new tblNursingManagementDetail();
                    objsal = (from tbl in objData.tblNursingManagementDetails
                              where tbl.SrDetailId == item.SrDetailId
                              && tbl.SrNo == item.SrNo && tbl.PatientId == item.PatientId
                              && tbl.IsDelete == false
                              select tbl).FirstOrDefault();
                    if (objsal != null)
                    {
                        objsal.PatientId = Convert.ToInt32(item.PatientId);
                        objsal.CategoryId = obj.CategoryId;
                        objsal.NurseId = obj.NurseId;
                        objsal.TreatmentDate = obj.TreatmentDate;
                        objsal.InjectableMedications = item.InjectableMedications;
                        objsal.Infusions = item.Infusions;
                        objsal.Oral = item.Oral;
                        objsal.NursingCare = item.NursingCare;
                        objsal.TreatmentTime = item.TreatmentTime;
                        objsal.Department = obj.Department;
                        objsal.IsDelete = item.IsDelete;
                    }
                    else
                    {
                        objsal = new tblNursingManagementDetail()
                        {
                            PatientId = item.PatientId,
                            CategoryId = obj.CategoryId,
                            NurseId = obj.NurseId,
                            TreatmentDate = obj.TreatmentDate,
                            Department = obj.Department,
                            InjectableMedications = item.InjectableMedications,
                            Infusions = item.Infusions,
                            Oral = item.Oral,
                            NursingCare = item.NursingCare,
                            TreatmentTime = item.TreatmentTime,
                            SrNo = Convert.ToInt32(obj.SrNo),
                            IsDelete = false
                        };
                        objData.tblNursingManagementDetails.InsertOnSubmit(objsal);
                    }
                }
                objData.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int? Save(tblNursingManagement tblins, List<EntityNursingManagementDetails> lst)
        {
            int? SrNo = 0;
            try
            {
                objData.STP_Insert_tblNursingManagement(Convert.ToInt32(tblins.CategoryId), Convert.ToInt32(tblins.NurseId), Convert.ToString(tblins.Department), tblins.TreatmentDate, ref SrNo);
                foreach (EntityNursingManagementDetails item in lst)
                {
                    tblNursingManagementDetail tbl = new tblNursingManagementDetail()
                    {
                        PatientId = Convert.ToInt32(item.PatientId),
                        InjectableMedications = Convert.ToString(item.InjectableMedications),
                        Infusions = Convert.ToString(item.Infusions),
                        Oral = Convert.ToString(item.Oral),
                        NursingCare = Convert.ToString(item.NursingCare),
                        TreatmentTime = Convert.ToString(item.TreatmentTime),
                        CategoryId = Convert.ToInt32(tblins.CategoryId),
                        NurseId = Convert.ToInt32(tblins.NurseId),
                        TreatmentDate = tblins.TreatmentDate,
                        Department = Convert.ToString(tblins.Department),
                        SrNo = Convert.ToInt32(SrNo),
                        IsDelete = false
                    };
                    objData.tblNursingManagementDetails.InsertOnSubmit(tbl);
                }
                objData.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return SrNo;
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

        public List<EntityNursingManagement> GetAllocatedPatientList()
        {
            List<EntityNursingManagement> lst = (from tbl in objData.tblNursingManagements
                                                 join tbla in objData.tblRoomCategories
                                                 on tbl.CategoryId equals tbla.PKId
                                                 join tblp in objData.tblEmployees
                                                 on tbl.NurseId equals tblp.PKId
                                                 where tbl.IsDelete == false
                                                 select new EntityNursingManagement
                                                 {
                                                     SrNo = tbl.SrNo,
                                                     CategoryDesc = tbla.CategoryDesc,
                                                     CategoryId = Convert.ToInt32(tbl.CategoryId),
                                                     NurseName = tblp.EmpFirstName + " " + tblp.EmpLastName,
                                                     NurseId = Convert.ToInt32(tbl.NurseId),
                                                     TreatmentDate = tbl.TreatmentDate==null?DateTime.Now.Date:tbl.TreatmentDate.Value,
                                                     Department = Convert.ToString(tbl.Department)
                                                 }).ToList();
            return lst;
        }

        public List<EntityNursingManagementDetails> GetDocForPatientAllocate(int Id)
        {
            int i = 0;
            List<EntityNursingManagementDetails> lst = (from tbl in objData.tblNursingManagementDetails
                                                        join tbla in objData.tblPatientAdmitDetails
                                                        on tbl.PatientId equals tbla.AdmitId
                                                        join tblp in objData.tblPatientMasters
                                                        on tbla.PatientId equals tblp.PKId
                                                        where tbl.SrNo == Id
                                                        &&
                                                        tbl.IsDelete == false
                                                        select new EntityNursingManagementDetails
                                                        {
                                                            PatientName = tblp.PatientFirstName + " " + tblp.PatientMiddleName + " " + tblp.PatientLastName,
                                                            PatientId = Convert.ToInt32(tbl.PatientId),
                                                            SrNo = tbl.SrNo,
                                                            SrDetailId = tbl.SrDetailId,
                                                            InjectableMedications = tbl.InjectableMedications,
                                                            Infusions = tbl.Infusions,
                                                            Oral = tbl.Oral,
                                                            NursingCare = tbl.NursingCare,
                                                            TreatmentTime = tbl.TreatmentTime
                                                        }).ToList();
            foreach (EntityNursingManagementDetails item in lst)
            {
                item.TempId = i++;
            }
            return lst;
        }

        public void Update(List<EntityAllocaConDocDetails> lst)
        {
            try
            {
                foreach (EntityAllocaConDocDetails item in lst)
                {
                    tblAllocConsultDoctorDetail objsal = new tblAllocConsultDoctorDetail();
                    objsal = (from tbl in objData.tblAllocConsultDoctorDetails
                              where tbl.SrDetailId == item.SrDetailId
                              && tbl.IsDelete == false
                              select tbl).FirstOrDefault();
                    if (objsal != null)
                    {
                        objsal.AdmitId = item.AdmitId;
                        objsal.IsDelete = item.IsDelete;
                    }
                    else
                    {
                        objsal = new tblAllocConsultDoctorDetail() { AdmitId = Convert.ToInt32(item.AdmitId), SrNo = Convert.ToInt32(item.SrNo) };
                        objData.tblAllocConsultDoctorDetails.InsertOnSubmit(objsal);
                    }
                } objData.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<EntityNursingManagement> GetInsurance(string Prefix)
        {
            List<EntityNursingManagement> lst = null;
            try
            {
                lst = (from tbl in GetAllocatedPatientList()
                       where tbl.NurseName.ToString().ToUpper().Contains(Prefix.ToUpper())
                       || tbl.CategoryDesc.ToString().ToUpper().Contains(Prefix.ToUpper())
                       select new EntityNursingManagement()
                       {
                           SrNo = tbl.SrNo,
                           NurseName = tbl.NurseName,
                           CategoryDesc = tbl.CategoryDesc,
                           TreatmentDate = tbl.TreatmentDate,
                           Department = tbl.Department,
                       }).ToList();
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetConsultDoctor()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetAllAnaesthetist");
            }
            catch (Exception ex)
            {
                Commons.FileLog("AllocConsultDoctorToPatientBLL - GetConsultDoctor()", ex);
            }
            return ldt;
        }

        public string GetConsultCharge(int NurseId)
        {
            string dept = "";
            try
            {
                tblNurse lstTrans = new tblNurse();

                lstTrans = (from tbl in objData.tblNurses
                            where tbl.PKId == Convert.ToInt32(NurseId)
                            select tbl).FirstOrDefault(); ;

                if (lstTrans != null)
                {
                    dept = lstTrans.DepartmentName;
                }
                else
                {
                    dept = "";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dept;
        }
    }

    public class NurseBLL
    {
        clsDataAccess mobjDataAcces = new clsDataAccess();
        public NurseBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public DataTable GetNewReligionCode()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetNewNurseCode");

            }
            catch (Exception ex)
            {

                Commons.FileLog("NurseBLL - GetNewReligionCode()", ex);
            }
            return ldt;
        }

        public DataTable GetAllReligion()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetAllNurse");

            }
            catch (Exception ex)
            {

                Commons.FileLog("NurseBLL - GetAllNurse()", ex);
            }
            return ldt;
        }

        public DataTable GetAllIPDPatient()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetAllIPDPatientListNew");

            }
            catch (Exception ex)
            {

                Commons.FileLog("NurseBLL - GetAllNurse()", ex);
            }
            return ldt;
        }

        public int InsertReligion(EntityNurse entReligion)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@NurseCode", DbType.String, entReligion.NurseCode);
                Commons.ADDParameter(ref lstParam, "@NurseName", DbType.String, entReligion.NurseName);
                Commons.ADDParameter(ref lstParam, "@DeptId", DbType.Int32, entReligion.DeptId);
                Commons.ADDParameter(ref lstParam, "@DepartmentName", DbType.String, entReligion.DepartmentName);
                cnt = mobjDataAcces.ExecuteQuery("sp_InsertNurse ", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("NurseBLL - InsertReligion(EntityReligion entReligion)", ex);
            }
            return cnt;
        }

        public DataTable GetReligionForEdit(string pstrReligionCode)
        {
            DataTable ldt = new DataTable();
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@NurseCode", DbType.String, pstrReligionCode);
                ldt = mobjDataAcces.GetDataTable("sp_GetNurseForEdit", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("NurseBLL  - GetNurseForEdit(string pstrReligionCode)", ex);
            }
            return ldt;
        }

        public int UpdateReligion(EntityNurse entReligion)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@NurseCode", DbType.String, entReligion.NurseCode);
                Commons.ADDParameter(ref lstParam, "@NurseName", DbType.String, entReligion.NurseName);
                Commons.ADDParameter(ref lstParam, "@DeptId", DbType.Int32, entReligion.DeptId);
                Commons.ADDParameter(ref lstParam, "@DepartmentName", DbType.String, entReligion.DepartmentName);
                cnt = mobjDataAcces.ExecuteQuery("sp_UpdateNurse", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("NurseBLL -  UpdateNurse(EntityNurse entReligion)", ex);
            }

            return cnt;
        }

        public int DeleteReligion(EntityNurse entReligion)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@NurseCode", DbType.String, entReligion.NurseCode);
                cnt = mobjDataAcces.ExecuteQuery("sp_DeleteNurse", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("NurseBLL - DeleteNurse(EntityNurse entReligion)", ex);
            }
            return cnt;
        }
    }
}