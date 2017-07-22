using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hospital.Models.Models
{
    /// <summary>
    /// Summary description for EntityUnit
    /// </summary>
    public class EntityUnit
    {
        public EntityUnit()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #region "Properties"
        public int PKId { get; set; }
        public string UnitCode { get; set; }
        public string UnitDesc { get; set; }

        #endregion
    }
}