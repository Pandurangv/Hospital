using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Hospital.Models.Models;
using System.Configuration;
using System.Web.Script.Serialization;

namespace Hospital.Models.DataLayer
{
    public class QueryStringManager
    {
        private QueryStringManager()
        { 
            
        }

        private static QueryStringManager _Instance = null;

        public static QueryStringManager Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new QueryStringManager();
                }
                return _Instance;
            }
        }


        public string Rpt
        {
            get
            {
                string _Rpt = "";
                if (HttpContext.Current.Request.QueryString.Count > 0 && HttpContext.Current.Request["Rpt"]!=null)
                {
                    _Rpt = Convert.ToString(HttpContext.Current.Request["Rpt"]);
                }
                return _Rpt;
            }
        }

        public bool IsEdit
        {
            get
            {
                bool _Rpt = false;
                if (HttpContext.Current.Request.QueryString.Count > 0 && HttpContext.Current.Request["IsEdit"] != null)
                {
                    _Rpt = Convert.ToBoolean(HttpContext.Current.Request["IsEdit"]);
                }
                return _Rpt;
            }
        }

        public string RequestFor
        {
            get
            {
                string _Rpt = "";
                if (HttpContext.Current.Request.QueryString.Count > 0 && HttpContext.Current.Request["RequestFor"] != null)
                {
                    _Rpt = Convert.ToString(HttpContext.Current.Request["RequestFor"]);
                }
                return _Rpt;
            }
        }

        public string ReportType
        {
            get
            {
                string _Rpt = "";
                if (HttpContext.Current.Request.QueryString.Count > 0 && HttpContext.Current.Request["ReportType"] != null)
                {
                    _Rpt = Convert.ToString(HttpContext.Current.Request["ReportType"]);
                }
                return _Rpt;
            }
        }

        //

        public int Sr_No
        {
            get
            {
                int _Rpt = 0;
                if (HttpContext.Current.Request.QueryString.Count > 0 && HttpContext.Current.Request["Sr_No"] != null)
                {
                    _Rpt = Convert.ToInt32(HttpContext.Current.Request["Sr_No"]);
                }
                return _Rpt;
            }
        }

        //LedgerId
        public int DischargeId
        {
            get
            {
                int _Rpt = 0;
                if (HttpContext.Current.Request.QueryString.Count > 0 && HttpContext.Current.Request["DischargeId"] != null)
                {
                    _Rpt = Convert.ToInt32(HttpContext.Current.Request["DischargeId"]);
                }
                return _Rpt;
            }
        }

        public int LedgerId
        {
            get
            {
                int _Rpt = 0;
                if (HttpContext.Current.Request.QueryString.Count > 0 && HttpContext.Current.Request["LedgerId"] != null)
                {
                    _Rpt = Convert.ToInt32(HttpContext.Current.Request["LedgerId"]);
                }
                return _Rpt;
            }
        }

        public int LabId
        {
            get
            {
                int _Rpt = 0;
                if (HttpContext.Current.Request.QueryString.Count > 0 && HttpContext.Current.Request["LabId"] != null)
                {
                    _Rpt = Convert.ToInt32(HttpContext.Current.Request["LabId"]);
                }
                return _Rpt;
            }
        }

        
        public int DoctorId
        {
            get
            {
                int _Rpt = 0;
                if (HttpContext.Current.Request.QueryString.Count > 0 && HttpContext.Current.Request["DoctorId"] != null)
                {
                    _Rpt = Convert.ToInt32(HttpContext.Current.Request["DoctorId"]);
                }
                return _Rpt;
            }
        }

        
        public int Death_Id
        {
            get
            {
                int _Rpt = 0;
                if (HttpContext.Current.Request.QueryString.Count > 0 && HttpContext.Current.Request["Death_Id"] != null)
                {
                    _Rpt = Convert.ToInt32(HttpContext.Current.Request["Death_Id"]);
                }
                return _Rpt;
            }
        }

        public int Certi_ID
        {
            get
            {
                int _Rpt = 0;
                if (HttpContext.Current.Request.QueryString.Count > 0 && HttpContext.Current.Request["Certi_ID"] != null)
                {
                    _Rpt = Convert.ToInt32(HttpContext.Current.Request["Certi_ID"]);
                }
                return _Rpt;
            }
        }

        public int AdmitId
        {
            get
            {
                int _Rpt = 0;
                if (HttpContext.Current.Request.QueryString.Count > 0 && HttpContext.Current.Request["AdmitId"] != null)
                {
                    _Rpt = Convert.ToInt32(HttpContext.Current.Request["AdmitId"]);
                }
                return _Rpt;
            }
        }

        public int Prescription_Id
        {
            get
            {
                int _Rpt = 0;
                if (HttpContext.Current.Request.QueryString.Count > 0 && HttpContext.Current.Request["Prescription_Id"] != null)
                {
                    _Rpt = Convert.ToInt32(HttpContext.Current.Request["Prescription_Id"]);
                }
                return _Rpt;
            }
        }

        public int BILLNo
        {
            get
            {
                int _Rpt = 0;
                if (HttpContext.Current.Request.QueryString.Count > 0 && HttpContext.Current.Request["BILLNo"] != null)
                {
                    _Rpt = Convert.ToInt32(HttpContext.Current.Request["BILLNo"]);
                }
                return _Rpt;
            }
        }

        public int Bed_NO
        {
            get
            {
                int _Rpt = 0;
                if (HttpContext.Current.Request.QueryString.Count > 0 && HttpContext.Current.Request["Bed_NO"] != null)
                {
                    _Rpt = Convert.ToInt32(HttpContext.Current.Request["Bed_NO"]);
                }
                return _Rpt;
            }
        }


        //PatientId

        public string PatientId
        {
            get
            {
                string _PatientId = "0";
                if (HttpContext.Current.Request.QueryString.Count > 0 && HttpContext.Current.Request["PatientId"] != null)
                {
                    _PatientId = Convert.ToString(HttpContext.Current.Request["PatientId"]);
                }
                return _PatientId;
            }
        }
        
    }

    public enum BrowserType
    {
        IE,
        Mozilla,
        Opera,
        Safari
    }

    public class SessionManager
    {
        CriticareHospitalDataContext objData = new CriticareHospitalDataContext();
        private SessionManager()
        { 
            
        }

        private static SessionManager _Instance = null;

        public static SessionManager Instance
        {
            get
            {
                if (_Instance==null)
                {
                    _Instance = new SessionManager();
                }
                return _Instance;
            }
        }

        JavaScriptSerializer serializer = new JavaScriptSerializer();
        public EntityEmployee LoginUser
        {
            get
            {
                string Id = HttpContext.Current.Session.SessionID;
                var userdata = objData.tblUserDatas.Where(p => p.SessionId == Id).FirstOrDefault();
                if (userdata==null)
                {
                    return null;
                }
                EntityEmployee emp = serializer.Deserialize<EntityEmployee>(userdata.UserData);
                return emp;
            }
            set
            {
                try
                {
                    string data = serializer.Serialize(value);
                    tblUserData user = objData.tblUserDatas.Where(p => p.UserId == value.PKId).FirstOrDefault(); 
                    if (user!=null)
                    {
                        user.SessionId = HttpContext.Current.Session.SessionID;
                    }
                    else
                    {
                        user = new tblUserData() { SessionId = HttpContext.Current.Session.SessionID, UserId = value.PKId, UserData = data };
                        objData.tblUserDatas.InsertOnSubmit(user);
                    }
                    objData.SubmitChanges();
                    HttpContext.Current.Session["user"] = data;
                }
                catch (Exception ex)
                {
                    Commons.FileLog(ex.Message, ex);
                }
            }
        }

        //public List<EntityPrescriptionDetails> Prescript
        //{
        //    get
        //    {
        //        List<EntityPrescriptionDetails> emp = serializer.Deserialize<List<EntityPrescriptionDetails>>(Convert.ToString(HttpContext.Current.Session["Prescript"]));
        //        return emp;
        //    }
        //    set
        //    {
        //        string data = serializer.Serialize(value);
        //        HttpContext.Current.Session["Prescript"] = data;
        //    }
        //}

        //public List<EntityInvoiceDetails> invDtl
        //{
        //    get {
        //        List<EntityInvoiceDetails> emp = serializer.Deserialize<List<EntityInvoiceDetails>>(Convert.ToString(HttpContext.Current.Session["invDtl"]));
        //        return emp;
        //    }
        //    set
        //    {
        //        string data = serializer.Serialize(value);
        //        HttpContext.Current.Session["invDtl"] = data;
        //    }   
        //}

        public void LogOut()
        {
            tblUserData user = objData.tblUserDatas.Where(p => p.SessionId == HttpContext.Current.Session.SessionID).FirstOrDefault();
            if (user!=null)
            {
                objData.tblUserDatas.DeleteOnSubmit(user);
                objData.SubmitChanges();
            }
        }

        //internal void SetSession()
        //{
        //    try
        //    {
        //        EntityEmployee loginuser = LoginUser;
        //        HttpContext.Current.Session.RemoveAll();
        //        HttpContext.Current.Session.Clear();
        //        HttpContext.Current.Session.Abandon();
        //        LoginUser = loginuser;
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}
    }




    public class SettingsManager
    {
        private SettingsManager()
        {

        }

        private static SettingsManager _Instance = null;

        public static SettingsManager Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new SettingsManager();
                }
                return _Instance;
            }
        }

        public string DefaultPassword
        {
            get
            {
                if (ConfigurationManager.ConnectionStrings["DefaultPassword"] == null)
                {
                    return "12345";
                }
                else
                {
                    return Convert.ToString(ConfigurationManager.ConnectionStrings["DefaultPassword"].ConnectionString);
                }
            }
        }

        public bool IsIPDOnly
        {
            get
            {
                if (ConfigurationManager.ConnectionStrings["IsIPDOnly"] == null)
                {
                    return false;
                }
                else
                {
                    string check=Convert.ToString(ConfigurationManager.ConnectionStrings["IsIPDOnly"].ConnectionString);
                    return !string.IsNullOrEmpty(check) && check =="Y"?true:false;
                }
            }
        }

        public string DateFormat
        {
            get
            {
                if (ConfigurationManager.ConnectionStrings["DateFormat"] == null)
                {
                    return "MM/dd/yyyy";
                }
                else
                {
                    return Convert.ToString(ConfigurationManager.ConnectionStrings["DateFormat"].ConnectionString);
                }
            }
        }

        public int DoctorDesigId
        {
            get
            {
                if (ConfigurationManager.ConnectionStrings["DoctorDesigId"] == null)
                {
                    return 0;
                }
                else
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(ConfigurationManager.ConnectionStrings["DoctorDesigId"] == null)))
                    {
                        return Convert.ToInt32(ConfigurationManager.ConnectionStrings["DoctorDesigId"].ConnectionString);
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
        }

        public int NurseDesigId
        {
            get
            {
                if (ConfigurationManager.ConnectionStrings["NurseDesigId"] == null)
                {
                    return 0;
                }
                else
                {
                    if (string.IsNullOrEmpty(Convert.ToString(ConfigurationManager.ConnectionStrings["NurseDesigId"] == null)))
                    {
                        return 0;
                    }
                    else
                    {
                        return Convert.ToInt32(ConfigurationManager.ConnectionStrings["NurseDesigId"].ConnectionString);
                    }
                }
            }
        }

        public int VisitingDoctorDesigId
        {
            get
            {
                if (ConfigurationManager.ConnectionStrings["VisitingDoctorDesigId"] == null)
                {
                    return 0;
                }
                else
                {
                    if (string.IsNullOrEmpty(Convert.ToString(ConfigurationManager.ConnectionStrings["VisitingDoctorDesigId"] == null)))
                    {
                        return 0;
                    }
                    else
                    {
                        return Convert.ToInt32(ConfigurationManager.ConnectionStrings["VisitingDoctorDesigId"].ConnectionString);
                    }
                }
            }
        }
    }
}