﻿using Hospital.Models.Models;
using Hospital.Models.DataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Hospital.Models.BusinessLayer
{
    public class GetLoginBLL
    {
        clsDataAccess mobjDataAccess = new clsDataAccess();

        public GetLoginBLL()
        {
            objData = new CriticareHospitalDataContext();
        }

        public CriticareHospitalDataContext objData { get; set; }


        public EntityLogin GetLogin(EntityLogin entLogin)
        {
            EntityLogin valid = null;
            try
            {
                valid = (from tbl in objData.tblLogins
                         join tblE in objData.tblEmployees
                         on tbl.EmpId equals tblE.PKId
                         where tblE.IsDelete == false
                         && tbl.UserName.ToLower()==entLogin.UserName.ToLower()
                         && tbl.Password.ToLower() == entLogin.Password.ToLower()
                         select new EntityLogin {
                            PKId=tbl.PKId,
                            DeisgId=tblE.DesignationId,
                            EmpId=tbl.EmpId,
                            Password=tbl.Password,
                            IsFirstLogin=tbl.IsFirstLogin,
                            UserType=tbl.UserType,
                         }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Commons.FileLog("GetLoginBLL - GetLogin(EntityLogin entLogin)", ex);
            }
            return valid;
            //return dt;

        }

        public int ChangePassword(EntityLogin entLogin)
        {
            int lintCnt = 0;
            List<SqlParameter> listparam = new List<SqlParameter>();
            try
            {
                Commons.ADDParameter(ref listparam, "@UserName", DbType.String, entLogin.UserName);
                Commons.ADDParameter(ref listparam, "@ConfirmPass", DbType.String, entLogin.ConfirmPass);
                lintCnt = mobjDataAccess.ExecuteQuery("sp_ChangePassword", listparam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("GetLoginBLL - ChangePassword(EntityLogin entLogin)", ex);
            }
            return lintCnt;
        }

        public DataTable GetDepartments()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = mobjDataAccess.GetDataTable("sp_GetDepartments");
            }
            catch (Exception ex)
            {

                Commons.FileLog("RegistrationBLL - GetDepartments()", ex);
            }
            return dt;

        }


        public int Update(EntityLogin entLog)
        {
            int Result = 0;
            tblLogin objUser = (from tbl in objData.tblLogins
                                where tbl.EmpId.Equals(entLog.EmpId)
                                select tbl).FirstOrDefault();
            objUser.Password = entLog.Password;
            objUser.IsFirstLogin = false;
            objData.SubmitChanges();
            if (objUser != null)
            {
                Result = 1;
            }
            else
            {
                Result = 0;
            }
            return Result;
        }

        public bool CheckLogin(int EmpId)
        {
            bool flag1 = false;
            try
            {
                tblLogin objCharge = (from tbl in objData.tblLogins
                                      where tbl.EmpId==EmpId
                                      && tbl.IsFirstLogin.Equals(true)
                                      select tbl).FirstOrDefault();
                if (objCharge != null)
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
    }
}