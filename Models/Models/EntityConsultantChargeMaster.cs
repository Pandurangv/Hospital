using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hospital.Models.Models
{
    /// <summary>
    /// Summary description for EntityConsultantChargeMaster
    /// </summary>
    public class EntityConsultantChargeMaster
    {
        public EntityConsultantChargeMaster()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region Properties

        public int PKId { get; set; }
        public int ConsultantId { get; set; }
        public int WardNo { get; set; }
        public decimal Charge { get; set; }
        public string UserName { get; set; }

        #endregion
    }
}