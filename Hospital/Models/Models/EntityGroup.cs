using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hospital.Models.Models
{

    /// <summary>
    /// Summary description for EntityGroup
    /// </summary>
    public class EntityGroup
    {
        public EntityGroup()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #region "properties"

        public int PKId { get; set; }
        public string GroupDesc { get; set; }
        public string EntryBy { get; set; }
        public string ChangeBy { get; set; }
        #endregion
    }
}