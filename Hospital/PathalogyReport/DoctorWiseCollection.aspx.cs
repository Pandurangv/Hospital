using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hospital.Models.BusinessLayer;
using System.Web.Script.Serialization;
using System.Web.Services;
using Hospital.Models.DataLayer;

namespace Hospital.PathalogyReport
{
    public partial class DoctorWiseCollection : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (QueryStringManager.Instance.RequestFor == "GetDoctorlist")
            {
                GetDoctorlist();
            }
            if (QueryStringManager.Instance.RequestFor == "GetDoctorCollection")
            {
                GetDoctorCollection();
            }
        }

        public void GetDoctorlist()
        {
            JavaScriptSerializer serialize = new JavaScriptSerializer();
            PatientAllocDocBLL objdoctor = new PatientAllocDocBLL();
            serialize.MaxJsonLength = Int32.MaxValue;
            Response.Clear();
            Response.Output.Write(serialize.Serialize(objdoctor.GetAllDoctor()));
            Response.End();
        }

        public void GetDoctorCollection()
        {
            JavaScriptSerializer serialize = new JavaScriptSerializer();
            CriticareHospitalDataContext objData = new CriticareHospitalDataContext();
            serialize.MaxJsonLength = Int32.MaxValue;
            Response.Clear();
            Response.Output.Write(serialize.Serialize(objData.STP_GetDoctowiseCollection(QueryStringManager.Instance.DoctorId)));
            Response.End();
        }
    }
}