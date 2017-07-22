using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Hospital.Models.DataLayer;
using System.Data;
using Hospital.Models.Models;

namespace Hospital.Models.BusinessLayer
{
    public class OperationCategoryBLL
    {
        clsDataAccess mobjDataAcces = new clsDataAccess();
        public OperationCategoryBLL()
        {
            objData = new CriticareHospitalDataContext();
        }

        public CriticareHospitalDataContext objData { get; set; }

        public DataTable GetNewReligionCode()
        {
            DataTable ldt = new DataTable();
            try
            {
                ldt = mobjDataAcces.GetDataTable("sp_GetNewOperationCatCode");

            }
            catch (Exception ex)
            {

                Commons.FileLog("OperationCategoryBLL - GetNewOperationCatCode()", ex);
            }
            return ldt;
        }

        public List<EntityOperationCategory> GetDocCategories()
        {
            List<EntityOperationCategory> lst = (from tbl in objData.tblOperationCategories
                                                 select new EntityOperationCategory { 
                                                    CategoryName=tbl.CategoryName,
                                                    CategotyId=tbl.CategoryId
                                                 }).ToList();
            return lst;
        }

        public List<sp_GetAllOperationCatResult> GetAllReligion()
        {
            return objData.sp_GetAllOperationCat().ToList();
        }

        public int InsertReligion(EntityOperationCategory entReligion)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@CategoryCode", DbType.String, entReligion.CategoryCode);
                Commons.ADDParameter(ref lstParam, "@CategoryName", DbType.String, entReligion.CategoryName);
                cnt = mobjDataAcces.ExecuteQuery("sp_InsertOperationCat", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("OperationCategoryBLL - InsertOperationCat(EntityOperationCategory entReligion)", ex);
            }
            return cnt;
        }

        public DataTable GetReligionForEdit(string pstrReligionCode)
        {
            DataTable ldt = new DataTable();
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@CategoryCode", DbType.String, pstrReligionCode);
                ldt = mobjDataAcces.GetDataTable("sp_GetOperationCatForEdit", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("OperationCategoryBLL  - GetOperationCatForEdit(string pstrReligionCode)", ex);
            }
            return ldt;
        }

        public int UpdateReligion(EntityOperationCategory entReligion)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@CategoryCode", DbType.String, entReligion.CategoryCode);
                Commons.ADDParameter(ref lstParam, "@CategoryName", DbType.String, entReligion.CategoryName);
                cnt = mobjDataAcces.ExecuteQuery("sp_UpdateOperationCat", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("OperationCategoryBLL -  UpdateOperationCat(EntityOperationCategory entReligion)", ex);
            }

            return cnt;
        }

        public int DeleteReligion(EntityOperationCategory entReligion)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@CategoryCode", DbType.String, entReligion.CategoryCode);
                cnt = mobjDataAcces.ExecuteQuery("sp_DeleteOperationCat", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("OperationCategoryBLL - DeleteOperationCat(EntityOperationCategory entReligion)", ex);
            }
            return cnt;
        }

        public EntityOperationCategory SelectOperation(int OperaId)
        {
            try
            {
                return (from tbl in objData.tblOperationCategories
                        where tbl.CategoryId.Equals(OperaId)
                        select new EntityOperationCategory
                        {
                            CategoryName = tbl.CategoryName,
                        }).FirstOrDefault();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}