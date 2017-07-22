﻿using System;
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
    public partial class StoreProducts : BasePage
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
            ProductBLL objProductTypes = new ProductBLL();
            serialize.MaxJsonLength = Int32.MaxValue;
            Response.Clear();
            Response.Output.Write(serialize.Serialize(objProductTypes.GetAllProduct()));
            Response.End();
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public static int Save(string model)
        {
            ProductBLL objProductTypes = new ProductBLL();
            JavaScriptSerializer serialize = new JavaScriptSerializer();
            return objProductTypes.InsertProduct(serialize.Deserialize<EntityProduct>(model));
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public static bool Update(string model)
        {
            ProductBLL objProductTypes = new ProductBLL();
            JavaScriptSerializer serialize = new JavaScriptSerializer();
            objProductTypes.UpdateProduct(serialize.Deserialize<EntityProduct>(model));
            return true;
        }
    }
}