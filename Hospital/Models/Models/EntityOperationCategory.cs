using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hospital.Models.Models
{
    public class EntityOperationCategory
    {
        public EntityOperationCategory()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #region "Properties"

        public int CategotyId { get; set; }
        public string CategoryCode { get; set; }
        public string CategoryName { get; set; }
        #endregion
    }
}