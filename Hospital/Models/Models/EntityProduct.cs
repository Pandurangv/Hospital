using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hospital.Models.Models

{
    /// <summary>
    /// Summary description for EntityCompany
    /// </summary>
    public class EntityProduct
    {
        public EntityProduct()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #region "properties"

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string UOM { get; set; }
        public string SubUOM { get; set; }
        public decimal Price { get; set; }

        public string Content { get; set; }

        public string BatchNo { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public int? ProductTypeId { get; set; }

        #endregion

        public string Category { get; set; }
    }

    public class EntityProductType
    {
        public int ProcutTypeId { get; set; }

        public string Description { get; set; }

        public string ProductType { get; set; }

        public bool? IsDelete { get; set; }
    }
}