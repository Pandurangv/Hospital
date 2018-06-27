using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hospital.Models.BusinessLayer;
using Hospital.Models.Models;
using Hospital.Models.DataLayer;
using System.Data;

namespace Hospital.StoreForms
{
    public partial class frmStoreMain : System.Web.UI.Page
    {
        StoreMainBLL mobjStoreMainBLL = new StoreMainBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //SessionManager.Instance.SetSession();
                GetData();
            }
        }

        public void GetData()
        {
            EntityStoreMain mobjEntityStoreMain = new EntityStoreMain();
            DataTable ldtStoreMain = new DataTable();
            ldtStoreMain = mobjStoreMainBLL.GetData();

            if (ldtStoreMain.Rows.Count > 0 && ldtStoreMain != null)
            {
                string lstrItemCode = string.Empty;
                string lstrSupplierName = string.Empty;
                string lstritemDesc = string.Empty;
                string lstrQty = string.Empty;


                for (int i = 0; i < ldtStoreMain.Rows.Count; i++)
                {

                    lstrItemCode += ldtStoreMain.Rows[i]["ItemCode"].ToString();
                    lstrItemCode += "</br>";

                    lstritemDesc += ldtStoreMain.Rows[i]["ItemDesc"].ToString();
                    lstritemDesc += "</br>";
                    lstrQty += ldtStoreMain.Rows[i]["Quantity"].ToString();
                    lstrQty += "</br>";
                    lstrSupplierName += ldtStoreMain.Rows[i]["SupplierName"].ToString();
                    lstrSupplierName += "</br>";
                    //lblQuantity.Text= ldtStoreMain.Rows[i]["Quantity"].ToString();                
                    //lblSupplierName.Text = ldtStoreMain.Rows[i]["SupplierName"].ToString();
                }


                lblItemCode.Text = lstrItemCode;
                lblitemDesc.Text = lstritemDesc;
                lblQuantity.Text = lstrQty;
                lblSupplierName.Text = lstrSupplierName;
            }
        }
    }
}