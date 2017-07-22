using Hospital.Models.DataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Hospital.Models.Models;

namespace Hospital.Models.BusinessLayer
{
    public class EmployeeBLL
    {
        clsDataAccess mobjDataAcces = new clsDataAccess();

        public EmployeeBLL()
        {
            objData = new CriticareHospitalDataContext();
        }

        public CriticareHospitalDataContext objData { get; set; }

        public DataTable GetNewEmpCode()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetNewEmpCode");
            }
            catch (Exception ex)
            {
                Commons.FileLog("EmployeeBLL - GetNewEmpCode()", ex);
            }
            return ldt;
        }

        public DataTable GetDepartments()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetDepartmentsForEmp");
            }
            catch (Exception ex)
            {
                Commons.FileLog("DepartmentBLL - GetDepartments()", ex);
            }
            return ldt;
        }

        public List<EntityEmployee> SelectAllEmployee()
        {
            List<EntityEmployee> ldt = null;
            try
            {
                ldt = (from tbl in objData.tblEmployees
                       join tbldesig in objData.tblDesignationMasters
                       on tbl.DesignationId equals tbldesig.PKId
                       where tbl.IsDelete==false
                       select new EntityEmployee
                       {
                           //OperationType=tbldoctype.CategoryName,
                           Designation=tbldesig.DesignationDesc,
                           Education=tbl.Education,
                           DoctorType=tbl.DoctorType,
                           DesignationId=tbl.DesignationId,
                           DeptId=tbl.DeptId,
                           PKId=tbl.PKId,
                           EmpCode = "E000" + tbl.PKId,
                           EmpFirstName = tbl.EmpFirstName,
                           EmpMiddleName = tbl.EmpMiddleName,
                           EmpLastName = tbl.EmpLastName,
                           FullName=tbl.EmpFirstName + " " + tbl.EmpMiddleName + " " + tbl.EmpLastName,
                           EmpAddress = tbl.EmpAddress,
                           EmpDOB = tbl.EmpDOB,
                           EmpDOJ = tbl.EmpDOJ,
                           BankName = tbl.BankName,
                           BankACNo = tbl.BankACNo,
                           PFNo = tbl.PFNo,
                           PanNo = tbl.PanNo,
                           BasicSal = tbl.BasicSalary,
                           ConsultingCharges=tbl.ConsultingCharges,
                           UserType=tbl.UserType,
                           City=tbl.City,
                           State=tbl.State,
                           Pin=tbl.Pin,
                           RegistrationNo=tbl.RegNo,
                       }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ldt;
        }

        public EntityEmployee SelectEmployeeForEdit(int PKId)
        {
            EntityEmployee entity = SelectAllEmployee().Where(p => p.PKId == PKId).FirstOrDefault();
            return entity;
        }

        public DataTable GetEmployeeName(string pstrEmpCode)
        {
            DataTable ldt = new DataTable();
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@EmpCode", DbType.String, pstrEmpCode);
                ldt = mobjDataAcces.GetDataTable("GetEmpName", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("EmployeeBLL - GetEmployeeName(string pstrEmpCode)", ex);
            }
            return ldt;
        }

        private List<SqlParameter> CreateParameterInsertEmp(EntityEmployee entEmployee)
        {
            List<SqlParameter> lstParam = new List<SqlParameter>();
            Commons.ADDParameter(ref lstParam, "@EmpCode", DbType.String, entEmployee.EmpCode);
            Commons.ADDParameter(ref lstParam, "@EmpFirstName", DbType.String, entEmployee.EmpFirstName);
            Commons.ADDParameter(ref lstParam, "@EmpMiddleName", DbType.String, entEmployee.EmpMiddleName);
            Commons.ADDParameter(ref lstParam, "@EmpLastName", DbType.String, entEmployee.EmpLastName);
            Commons.ADDParameter(ref lstParam, "@EmpAddress", DbType.String, entEmployee.EmpAddress);
            Commons.ADDParameter(ref lstParam, "@EmpDOB", DbType.DateTime, entEmployee.EmpDOB);
            Commons.ADDParameter(ref lstParam, "@EmpDOJ", DbType.DateTime, entEmployee.EmpDOJ);
            Commons.ADDParameter(ref lstParam, "@DeptId", DbType.String, entEmployee.DeptId);
            return lstParam;
        }

        private List<SqlParameter> CreateParameterDeleteEmp(EntityEmployee entEmployee)
        {
            List<SqlParameter> lstParam = new List<SqlParameter>();
            Commons.ADDParameter(ref lstParam, "@EmpCode", DbType.String, entEmployee.EmpCode);
            Commons.ADDParameter(ref lstParam, "@Discontinued", DbType.Boolean, entEmployee.DisContinued);
            Commons.ADDParameter(ref lstParam, "@DiscontRemark", DbType.String, entEmployee.DisContRemark);
            return lstParam;
        }

        public int UpdateEmployee(EntityEmployee entEmployee)
        {
            int cnt = 0;
            try
            {
                tblEmployee emp = objData.tblEmployees.Where(p => p.PKId==entEmployee.PKId).FirstOrDefault();
                if (emp != null)
                {
                    emp.BankACNo = entEmployee.BankACNo; 
                    emp.BankName = entEmployee.BankName; 
                    emp.BasicSalary = entEmployee.BasicSal; 
                    emp.ConsultingCharges = entEmployee.ConsultingCharges; 
                    emp.DeptId = entEmployee.DeptId; 
                    emp.DesignationId = entEmployee.DesignationId; 
                    emp.Education = entEmployee.Education; 
                    emp.EmpAddress = !string.IsNullOrEmpty(entEmployee.EmpAddress)?entEmployee.EmpAddress:""; 
                    emp.EmpDOB = entEmployee.EmpDOB; 
                    emp.EmpDOJ = entEmployee.EmpDOJ; 
                    emp.EmpFirstName = entEmployee.EmpFirstName; 
                    emp.EmpLastName = entEmployee.EmpLastName; 
                    emp.EmpMiddleName = entEmployee.EmpMiddleName; 
                    emp.EntryBy = entEmployee.EntryBy; 
                    emp.EntryDate = DateTime.Now.Date; 
                    emp.IsDelete = false; 
                    emp.PanNo = entEmployee.PanNo; 
                    //emp.Password = SettingsManager.Instance.DefaultPassword; 
                    emp.PFNo = entEmployee.PFNo; 
                    emp.RegNo = entEmployee.RegistrationNo; 
                    emp.UserType = !string.IsNullOrEmpty(entEmployee.UserType)?entEmployee.UserType:""; 
                    emp.DoctorType = entEmployee.DoctorType; 
                    emp.DisContFrom = null; 
                    emp. DiscontRemark = ""; 
                    emp.Discontinued = false; 
                    emp.ChangeBy = "";
                    emp.EntryBy = "";
                    emp.ChangeDate = DateTime.Now.Date;
                    emp.City = entEmployee.City;
                    emp.State = entEmployee.State;
                    emp.Pin = entEmployee.Pin;
                    objData.SubmitChanges();
                    cnt++;
                }
                //emp.EmpCode = EmpCode,
                        
                //List<SqlParameter> lstParam = new List<SqlParameter>();
                //Commons.ADDParameter(ref lstParam, "@EmpCode", DbType.String, entEmployee.EmpCode);
                //Commons.ADDParameter(ref lstParam, "@EmpFirstName", DbType.String, entEmployee.EmpFirstName);
                //Commons.ADDParameter(ref lstParam, "@EmpMiddleName", DbType.String, entEmployee.EmpMiddleName);
                //Commons.ADDParameter(ref lstParam, "@EmpLastName", DbType.String, entEmployee.EmpLastName);
                //Commons.ADDParameter(ref lstParam, "@EmpAddress", DbType.String, entEmployee.EmpAddress);
                //Commons.ADDParameter(ref lstParam, "@EmpDOB", DbType.DateTime, entEmployee.EmpDOB);
                //Commons.ADDParameter(ref lstParam, "@EmpDOJ", DbType.DateTime, entEmployee.EmpDOJ);
                //Commons.ADDParameter(ref lstParam, "@DeptId", DbType.String, entEmployee.DeptId);
                //Commons.ADDParameter(ref lstParam, "@ChangeBy", DbType.String, entEmployee.ChangeBy);
                //Commons.ADDParameter(ref lstParam, "@DesigId", DbType.Int32, entEmployee.DesignationId);
                //Commons.ADDParameter(ref lstParam, "@BankName", DbType.String, entEmployee.BankName);
                //Commons.ADDParameter(ref lstParam, "@BankACNo", DbType.String, entEmployee.BankACNo);
                //Commons.ADDParameter(ref lstParam, "@PFNo", DbType.String, entEmployee.PFNo);
                //Commons.ADDParameter(ref lstParam, "@PanNo", DbType.String, entEmployee.PanNo);
                //Commons.ADDParameter(ref lstParam, "@BasicSalary", DbType.Decimal, entEmployee.BasicSal);
                //cnt = mobjDataAcces.ExecuteQuery("sp_UpdateEmployee", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("EmployeeBLL - CreateParameterUpdateEmp(EntityEmployee entEmployee)", ex);
            }
            return cnt;
        }

        private Object thisLock = new Object();  

        public int InsertEmployee(EntityEmployee entEmployee)
        {
            int cnt = 0;

            try
            {
                lock (thisLock)
                {
                    int EmpId = objData.tblEmployees.Max(p => p.PKId);
                    string EmpCode = "E000" + EmpId;
                    tblEmployee emp = new tblEmployee()
                    {
                        EmpCode = EmpCode,
                        BankACNo = entEmployee.BankACNo,
                        BankName = entEmployee.BankName,
                        BasicSalary = entEmployee.BasicSal,
                        ConsultingCharges = entEmployee.ConsultingCharges,
                        DeptId = entEmployee.DeptId,
                        DesignationId = entEmployee.DesignationId,
                        Education = entEmployee.Education,
                        EmpAddress = entEmployee.EmpAddress,
                        EmpDOB = entEmployee.EmpDOB,
                        EmpDOJ = entEmployee.EmpDOJ,
                        EmpFirstName = entEmployee.EmpFirstName,
                        EmpLastName = entEmployee.EmpLastName,
                        EmpMiddleName = entEmployee.EmpMiddleName,
                        EntryBy = entEmployee.EntryBy,
                        EntryDate = DateTime.Now.Date,
                        IsDelete = false,
                        PanNo = entEmployee.PanNo,
                        Password = SettingsManager.Instance.DefaultPassword,
                        PFNo=entEmployee.PFNo,
                        RegNo=entEmployee.RegistrationNo,
                        UserType=entEmployee.UserType,
                        DoctorType=entEmployee.DoctorType,
                        DisContFrom=null,
                        DiscontRemark="",
                        Discontinued=false,
                        ChangeBy="",
                        ChangeDate=DateTime.Now.Date,
                        City=entEmployee.City,
                        State=entEmployee.State,
                        Pin=entEmployee.Pin,
                    };
                    objData.tblEmployees.InsertOnSubmit(emp);
                    objData.SubmitChanges();
                    cnt = 1;
                }
            }
            catch (Exception ex)
            {
                Commons.FileLog("EmployeeBLL - InsertEmployee(EntityEmployee entEmployee) ", ex);
            }
            return cnt;
        }

        public int DeleteEmployee(List<EntityEmployee> lstEntEmployee)
        {
            int cnt = 0;
            List<string> lstspNames = new List<string>();
            List<List<SqlParameter>> lstParamVals = new List<List<SqlParameter>>();
            try
            {
                foreach (EntityEmployee entEmployee in lstEntEmployee)
                {
                    lstspNames.Add("sp_DeleteEmployee");
                    lstParamVals.Add(CreateParameterDeleteEmp(entEmployee));

                    lstspNames.Add("sp_DeleteEmployeeLogin");
                    List<SqlParameter> lstParam = new List<SqlParameter>();
                    Commons.ADDParameter(ref lstParam, "@EmpCode", DbType.String, entEmployee.EmpCode);
                    Commons.ADDParameter(ref lstParam, "@Discontinued", DbType.Boolean, entEmployee.DisContinued);
                    lstParamVals.Add(lstParam);
                }
                cnt = mobjDataAcces.ExecuteTransaction(lstspNames, lstParamVals);
            }
            catch (Exception ex)
            {
                Commons.FileLog("EmployeeBLL - DeleteEmployee(List<EntityEmployee> lstEntEmployee) ", ex);
            }
            return cnt;
        }

        //public List<EntityEmployee> GetAllocatedEmployee(int p)
        //{
        //try
        //{
        //    return (from tbl in objData.STP_AllocatedStudents(ClassId, DivId, YearId)
        //            select new ClassAllocationInfo
        //            {
        //                FirstName = tbl.FirstName + " " + tbl.MiddleName + " " + tbl.LastName,
        //                RegNo = tbl.RegNo,
        //                StudentId = tbl.StudentId,
        //                AllocationId = tbl.ClassAlloc_Id
        //            }).ToList();
        //}
        //catch (Exception ex)
        //{
        //    throw ex;
        //}
        //}

        public List<EntityFacAllocEmp> GetAllocatedEmpToShift(int ShiftId)
        {
            try
            {
                List<STP_AllocatedEmployeeResult> lst = objData.STP_AllocatedEmployee(ShiftId).ToList();
                return (from tbl in lst
                        select new EntityFacAllocEmp
                        {
                            Emp_Id = tbl.PKId,
                            FullName = tbl.EmpFirstName + " " + tbl.EmpMiddleName + " " + tbl.EmpLastName
                        }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public EntityFacAllocEmp GetAllocatedEmp(int ShiftId, int EmpId)
        //{

        //}

        public bool Save(List<tblShiftAllocEmp> lst)
        {
            try
            {
                foreach (tblShiftAllocEmp item in lst)
                {
                    objData.tblShiftAllocEmps.InsertOnSubmit(item);
                }
                objData.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool GetAEmpIdOnShiftId(int ShiftId, int EmpId)
        {
            bool flag1 = false;
            tblShiftAllocEmp obj = new tblShiftAllocEmp();
            try
            {
                obj = (from tbl in objData.tblShiftAllocEmps
                       where tbl.IsDelete == false
                       && tbl.Shift_Id.Equals(ShiftId)
                       && tbl.Emp_Id.Equals(EmpId)
                       select tbl).FirstOrDefault();
                if (obj != null)
                {
                    flag1 = true;
                }
                else
                {
                    flag1 = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return flag1;
        }

        public List<EntityEmployee> SelectEmployee(string Prefix)
        {
            List<EntityEmployee> lst = null;
            try
            {
                lst = (from tbl in objData.sp_SelectAllEmployee()
                       where
                       tbl.EmpFirstName.ToString().ToUpper().Contains(Prefix.ToUpper()) ||
                       tbl.EmpMiddleName.ToString().ToUpper().Contains(Prefix.ToUpper()) || tbl.EmpLastName.ToString().ToUpper().Contains(Prefix.ToUpper()) ||
                       tbl.EmpAddress.ToString().ToUpper().Contains(Prefix.ToUpper())
                       select new EntityEmployee
                       {
                           PKId=tbl.PKId,
                           EmpCode = "E000" + tbl.PKId,
                           EmpFirstName = tbl.EmpFirstName,
                           EmpMiddleName = tbl.EmpMiddleName,
                           EmpLastName = tbl.EmpLastName,
                           EmpAddress = tbl.EmpAddress,
                           EmpDOB = tbl.EmpDOB,
                           EmpDOJ = tbl.EmpDOJ,
                           BankName = tbl.BankName,
                           BankACNo = tbl.BankACNo,
                           PFNo = tbl.PFNo,
                           PanNo = tbl.PanNo,
                           BasicSal = Convert.ToDecimal(tbl.BasicSalary),
                           
                       }).ToList();
                return lst;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string GetEmpName(string UserCode)
        {
            EntityEmployee entEmpp = new EntityEmployee();

            entEmpp = (from tbl in objData.tblEmployees
                       where tbl.EmpCode.Equals(UserCode)
                       && tbl.IsDelete == false
                       select new EntityEmployee { EmpFirstName = tbl.EmpFirstName + ' ' + tbl.EmpMiddleName + ' ' + tbl.EmpLastName }).FirstOrDefault();

            if (entEmpp!=null)
            {
                return "";
            }
            else
            {
                return entEmpp.EmpFirstName;
            }
        }
    }
}