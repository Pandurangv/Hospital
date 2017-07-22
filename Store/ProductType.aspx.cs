using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hospital.Models;
using System.Web.Script.Serialization;
using Hospital.Models.BusinessLayer;
using Hospital.Models.Models;
using System.Web.Services;
using System.Web.Script.Services;
using System.Text;
using Hospital.Models.DataLayer;

namespace Hospital.Store
{
    public partial class ProductType : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            base.AuthenticateUser();
            if (QueryStringManager.Instance.RequestFor == "GetDetails")
            {
                GetProductType();
            }
        }

        public void GetProductType()
        {
            JavaScriptSerializer serialize = new JavaScriptSerializer();
            ProductTypeBLL objProductTypes = new ProductTypeBLL();
            serialize.MaxJsonLength = Int32.MaxValue;
            Response.Clear();
            Response.Output.Write(serialize.Serialize(objProductTypes.GetProductTypes()));
            Response.End();
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public static int Save(string model)
        {
            ProductTypeBLL objProductTypes = new ProductTypeBLL();
            JavaScriptSerializer serialize = new JavaScriptSerializer();
            return objProductTypes.Save(serialize.Deserialize<EntityProductType>(model));
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public static bool Update(string model)
        {
            ProductTypeBLL objProductTypes = new ProductTypeBLL();
            JavaScriptSerializer serialize = new JavaScriptSerializer();
            objProductTypes.Update(serialize.Deserialize<EntityProductType>(model));
            return true;
        }
    }
}