using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hospital.Models.DataLayer;
using System.Text;
using System.Web.Script.Serialization;

namespace Hospital.PathalogyReport
{
    public partial class Reports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public string GetReportType()
        {
            string ReportType = QueryStringManager.Instance.ReportType;
            if (Session["ReportType"] != null)
            {
                ReportType = Convert.ToString(Session["ReportType"]);
            }
            return ReportType;
        }

        public string GetReportData()
        {
            CriticareHospitalDataContext objData = new CriticareHospitalDataContext();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength=Int32.MaxValue;
            string ReportType = QueryStringManager.Instance.ReportType;
            if (Session["ReportType"] != null)
            {
                ReportType = Convert.ToString(Session["ReportType"]);
            }
            StringBuilder sb = new StringBuilder();
            switch (ReportType)
            {
                case "Pathology":
                    sb =sb.Append(serializer.Serialize(objData.STP_PrintPathology(Hospital.Models.DataLayer.QueryStringManager.Instance.LabId).ToList()));
                    break;
                case "Prescription":
                    sb =sb.Append(serializer.Serialize(objData.STP_PrintPrescription(Hospital.Models.DataLayer.QueryStringManager.Instance.Prescription_Id).ToList()));
                    break;
                case "DoctorTreatmentChart":
                    sb = sb.Append(serializer.Serialize(objData.STP_PrintDoctorChart(Hospital.Models.DataLayer.QueryStringManager.Instance.AdmitId).ToList()));
                    break;
            }
            return sb.ToString();
        }
    }

    
}