using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hospital.Models.Models
{
    /// <summary>
    /// Summary description for EntityOPD
    /// </summary>
    public class EntityOPD
    {
        public EntityOPD()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #region "properties"
        public int OPDCode { get; set; }
        public string OPDDesc { get; set; }
        #endregion
    }
}