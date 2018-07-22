using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hospital.Models.DataLayer;
using System.Web.Script.Serialization;
using Hospital.Models.BusinessLayer;
using Hospital.Models.Models;
using System.Web.Script.Services;
using System.Web.Services;

namespace Hospital.PathalogyReport
{
    public partial class IPDMedicineBill : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (QueryStringManager.Instance.RequestFor == "GetMedicineBills")
            {
                GetTreatmentDetails();
            }
            else if (QueryStringManager.Instance.RequestFor == "GetBillDetails")
            {
                GetBillDetails(QueryStringManager.Instance.BILLNo);
            }
        }

        public void GetTreatmentDetails()
        {
            JavaScriptSerializer serialize = new JavaScriptSerializer();
            OTMedicineBillBLL mobjPatientMasterBLL = new OTMedicineBillBLL();
            List<EntityPatientAdmit> ldtRequisition = mobjPatientMasterBLL.GetPatientList();
            ldtRequisition.Insert(0, new EntityPatientAdmit() { AdmitId = 0, PatientFirstName = "----Select Patient----" });
            
            DoctorTreatmentBLL objProductTypes = new DoctorTreatmentBLL();
            DoctorTreatResponse response = new DoctorTreatResponse();
            response.DoctorTreatmentList = objProductTypes.GetTreatmentDetails();
            response.PatientList = ldtRequisition;
            Response.Clear();
            Response.Output.Write(serialize.Serialize(response));
            Response.End();
        }

        private void GetBillDetails(int BillNo = 0, string PatientId = "0")
        {
            JavaScriptSerializer serialize = new JavaScriptSerializer();
            OTMedicineBillBLL mobjPatientMasterBLL = new OTMedicineBillBLL();
            DoctorTreatResponse response = new DoctorTreatResponse();
            List<EntityOTMedicineBillDetails> lst = mobjPatientMasterBLL.GetBillProducts(BillNo, Convert.ToInt32(PatientId));
            response.MedicineList = lst;
            response.LabTestList = mobjPatientMasterBLL.GetLabTestDetails(BillNo);
            Response.Clear();
            Response.Output.Write(serialize.Serialize(response));
            Response.End();
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public static long? Save(string model)
        {
            DoctorTreatmentBLL objProductTypes = new DoctorTreatmentBLL();
            JavaScriptSerializer serialize = new JavaScriptSerializer();
            DoctorTreatmentModel datamodel = serialize.Deserialize<DoctorTreatmentModel>(model);
            //datamodel.TotalTaxAmount = datamodel.ProductList.Sum(p => p.TaxAmount);
            datamodel.TotalAmount = datamodel.ProductList.Sum(p => p.Quantity * p.Price);
            datamodel.NetAmount = datamodel.TotalAmount - datamodel.Discount==null?0:datamodel.Discount;
            datamodel.TreatmentDate = DateTime.Now.Date;
            DoctorTreatResponse response = objProductTypes.Save(datamodel,true);
            //if (datamodel.LabTestList.Count>0)
            //{
            //    List<TestAllocation> lst = new List<TestAllocation>();
            //    tblTestInvoice testInvoice = new tblTestInvoice();
            //    testInvoice.Amount = datamodel.LabTestList.Sum(p => p.TestCharge);
            //    testInvoice.PatientId = datamodel.AdmitId;
            //    testInvoice.TestInvoiceDate = DateTime.Now.Date;
            //    foreach (EntityTest item in datamodel.LabTestList)
            //    {
            //        lst.Add(new TestAllocation() { TestId = item.TestId, Charges = item.TestCharge == null ? 0 : item.TestCharge.Value });
            //    }
            //    int i = new clsTestAllocation().Save(lst, testInvoice, datamodel.IsCash);
            //}
            return 0;
        }

    }
}