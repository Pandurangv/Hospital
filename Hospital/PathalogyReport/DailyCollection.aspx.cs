using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hospital.Models.DataLayer;
using Hospital.Models;
using Hospital.Models.Models;
using Hospital.Models.BusinessLayer;


namespace Hospital.PathalogyReport
{
    public partial class DailyCollection : BasePage
    {
        CriticareHospitalDataContext objData = new CriticareHospitalDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            base.AuthenticateUser();
        }

        public List<EntityEmployee> GetDoctorsList()
        {
            List<EntityEmployee> tblCat = new EmployeeBLL().SelectAllEmployee().Where(p => p.DesignationId == SettingsManager.Instance.DoctorDesigId).ToList();
            if (SessionManager.Instance.LoginUser.UserType.ToLower()=="doctor")
            {
                tblCat = tblCat.Where(p => p.PKId == SessionManager.Instance.LoginUser.PKId).ToList();
            }
            return tblCat;
        }

        public List<STP_GetCollection_DetailsResult> GetDailyCollection()
        {
            var tblCat=objData.STP_GetCollection_Details().ToList();
            if (SessionManager.Instance.LoginUser.UserType.ToLower() == "doctor")
            {
                tblCat = tblCat.Where(p => p.DeptDoctorId == SessionManager.Instance.LoginUser.PKId).ToList();
            }
            return tblCat;
        }
    }
}