using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hospital.Models.Models;

namespace Hospital
{
    public partial class Sample : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MultiView1.SetActiveView(View2);
                List<EntitySupplier> lst = new List<EntitySupplier>();
                lst.Add(new EntitySupplier() { Id = 1, SupplierName = "Mahesh", Address = "Pune" });
                lst.Add(new EntitySupplier() { Id = 2, SupplierName = "Ramesh", Address = "Sangli" });
                lst.Add(new EntitySupplier() { Id = 3, SupplierName = "Abhishek", Address = "Mumbai" });
                dgvCustTransaction.DataSource = lst;
                dgvCustTransaction.DataBind();
                Session["supplier"] = lst;
            }
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            MultiView1.SetActiveView(View2);
            List<EntitySupplier> lst = null;
            if (Session["supplier"]!=null)
            {
                lst = (List<EntitySupplier>)Session["supplier"];
            }
            else
            {
                lst = new List<EntitySupplier>();
            }
            lst.Add(new EntitySupplier() { Id = 4, SupplierName = "New Supplier", Address = "Mulund" });
            dgvCustTransaction.DataSource = lst;
            dgvCustTransaction.DataBind();
        }
    }
}