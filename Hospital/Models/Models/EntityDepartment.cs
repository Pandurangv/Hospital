using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hospital.Models.Models
{
    /// <summary>
    /// Summary description for EntityDepartment
    /// </summary>
    public class EntityDepartment
    {
        public EntityDepartment()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region "Properties"

        public string DeptCode { get; set; }
        public string DeptDesc { get; set; }
        public string EntryBy { get; set; }
        public string ChangeBy { get; set; }
        #endregion
    }
}