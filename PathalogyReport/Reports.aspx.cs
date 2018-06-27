using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hospital.Models.DataLayer;
using System.Text;
using System.Web.Script.Serialization;
using Hospital.Models.Models;
using Hospital.Models.BusinessLayer;

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
            OTMedicineBillBLL mobjPatientMasterBLL = new OTMedicineBillBLL();
            List<EntityOTMedicineBillDetails> lst = new List<EntityOTMedicineBillDetails>();
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
                    var response = new DoctorTreatmentChartResponse()
                    {
                        TreatmentList =
                                (from tbl in objData.STP_PrintDoctorChart(Hospital.Models.DataLayer.QueryStringManager.Instance.AdmitId)
                                 select new EntityOTMedicineBill { 
                                    BillNo=tbl.BillNo,
                                    Bill_Date=tbl.Bill_Date,
                                    EmployeeName=tbl.EmployeeName,
                                    PatientCode=tbl.PatientCode,
                                    PatientName=tbl.PatientName,
                                    TreatmentDetails=tbl.TreatmentDetails,
                                    TreatmentPro=tbl.TreatmentPro,
                                    TreatmentTime = tbl.TreatmentTime,
                                    NetAmount = tbl.NetAmount,
                                    TotalTaxAmount = tbl.TotalTaxAmount,
                                 }).ToList()
                    };
                    foreach (var item in response.TreatmentList)
                    {
                        item.ProductList.AddRange(mobjPatientMasterBLL.GetBillProducts(item.BillNo));
                    }
                    sb = sb.Append(serializer.Serialize(response));
                    break;
                case "OTMedicinBill":
                    var responsebill = new DoctorTreatmentChartResponse()
                    {
                        TreatmentList =
                                (from tbl in objData.STP_PrintDoctorChart(Hospital.Models.DataLayer.QueryStringManager.Instance.AdmitId)
                                 where tbl.BillNo==Hospital.Models.DataLayer.QueryStringManager.Instance.BILLNo
                                 select new EntityOTMedicineBill
                                 {
                                     BillNo = tbl.BillNo,
                                     Bill_Date = tbl.Bill_Date,
                                     EmployeeName = tbl.EmployeeName,
                                     PatientCode = tbl.PatientCode,
                                     PatientName = tbl.PatientName,
                                     TreatmentDetails = tbl.TreatmentDetails,
                                     TreatmentPro = tbl.TreatmentPro,
                                     TreatmentTime=tbl.TreatmentTime,
                                     NetAmount=tbl.NetAmount,
                                     TotalTaxAmount=tbl.TotalTaxAmount,
                                     TotalAmount = mobjPatientMasterBLL.GetBillProducts(Hospital.Models.DataLayer.QueryStringManager.Instance.BILLNo).Sum(p=>p.Price * p.Quantity),
                                 }).ToList()
                    };


                    var patientbill=  responsebill.TreatmentList.FirstOrDefault();
                    var ProductList = new List<EntityOTMedicineBillDetails>();
                    if (patientbill!=null)
                    {
                        var lstbills = objData.tblOTMedicineBills.Where(p => patientbill.Bill_Date >= p.Bill_Date && patientbill.Bill_Date <= p.Bill_Date && p.BillNo!=patientbill.BillNo).ToList();
                        foreach (var item in lstbills)
                        {
                            ProductList.AddRange(mobjPatientMasterBLL.GetBillProducts(item.BillNo));
                        }
                    }

                    foreach (var item in responsebill.TreatmentList)
                    {
                        item.ProductList.AddRange(mobjPatientMasterBLL.GetBillProducts(item.BillNo));
                    }
                    if (responsebill.TreatmentList.Count>0)
                    {
                        responsebill.TreatmentList[0].ProductList.AddRange(ProductList);
                    }
                    sb = sb.Append(serializer.Serialize(responsebill));
                    break;

            }
            return sb.ToString();
        }
    }

    
}