using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hospital.Models;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using Hospital.Models.BusinessLayer;
using Hospital.Models.Models;
using Hospital.Models.DataLayer;

namespace Hospital.Store
{
    public partial class DoctorTreatment : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            base.AuthenticateUser();
            if (QueryStringManager.Instance.RequestFor == "GetDetails")
            {
                GetTreatmentDetails();
            }
            else if ("GetProductBatch"==QueryStringManager.Instance.RequestFor)
            {
                GetProductBatchNo(QueryStringManager.Instance.ProductId);
            }
            else if ("GetBillDetails" == QueryStringManager.Instance.RequestFor)
            {
                GetBillDetails(QueryStringManager.Instance.BILLNo);
            }
        }

        private void GetBillDetails(int BillNo)
        {
            JavaScriptSerializer serialize = new JavaScriptSerializer();
            OTMedicineBillBLL mobjPatientMasterBLL = new OTMedicineBillBLL();
            List<EntityOTMedicineBillDetails> lst = mobjPatientMasterBLL.GetBillProducts(BillNo);
            Response.Clear();
            Response.Output.Write(serialize.Serialize(lst));
            Response.End();
        }

        private void GetProductBatchNo(int ProductId=0)
        {
            JavaScriptSerializer serialize = new JavaScriptSerializer();
            ProductBLL objProducts = new ProductBLL();
            DoctorTreatResponse response = new DoctorTreatResponse();
            response.ProductBatchList = objProducts.GetAllProductBatchList(ProductId);
            Response.Clear();
            Response.Output.Write(serialize.Serialize(response));
            Response.End();
        }

        public void GetTreatmentDetails()
        {
            JavaScriptSerializer serialize = new JavaScriptSerializer();
            OTMedicineBillBLL mobjPatientMasterBLL = new OTMedicineBillBLL();
            List<EntityPatientAdmit> ldtRequisition = mobjPatientMasterBLL.GetPatientList();
            ldtRequisition.Insert(0, new EntityPatientAdmit() { AdmitId = 0, PatientFirstName = "----Select Patient----" });
            PatientAllocDocBLL objdoctor = new PatientAllocDocBLL();
            DoctorTreatmentBLL objProductTypes = new DoctorTreatmentBLL();
            ProductBLL objProducts = new ProductBLL();
            DoctorTreatResponse response = new DoctorTreatResponse();
            response.DoctorList = (from tbl in objdoctor.GetAllDoctor()
                                   select new EntityEmployee()
                                   {
                                       FullName = tbl.FullName,
                                       PKId = tbl.PKId
                                   }).ToList();

            response.DoctorList.Insert(0, new EntityEmployee() { FullName = "----Select Doctor----", PKId = 0 });
            serialize.MaxJsonLength = Int32.MaxValue;
            response.DoctorTreatmentList =objProductTypes.GetTreatmentDetails();
            response.PatientList = ldtRequisition;
            List<sp_GetAllProductResult> lstp = objProducts.GetAllProduct().Where(p=>p.Category=="Store").ToList();
            lstp.Insert(0, new sp_GetAllProductResult() { ProductId=0,ProductName="----Select Product----"});
            response.ProductList = lstp;
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
            DoctorTreatResponse response= objProductTypes.Save(serialize.Deserialize<DoctorTreatmentModel>(model));
            return response.Id;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public static bool Update(string model)
        {
            DoctorTreatmentBLL objProductTypes = new DoctorTreatmentBLL();
            JavaScriptSerializer serialize = new JavaScriptSerializer();
            objProductTypes.Update(serialize.Deserialize<DoctorTreatmentModel>(model));
            return true;
        }
    }
}