using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hospital.Models.Models
{
    /// <summary>
    /// Summary description for EntityCurrency
    /// </summary>
    public class EntityCurrency
    {
        public EntityCurrency()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region "Properties"

        public string CurrencyCode { get; set; }
        public string CurrencyDesc { get; set; }
        public string EntryBy { get; set; }
        public string ChangeBy { get; set; }
        #endregion
    }
}