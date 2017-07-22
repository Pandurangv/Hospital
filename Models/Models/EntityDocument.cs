using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hospital.Models.Models
{
    public class EntityDocument
    {
        public EntityDocument()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public string DocumentNAme { get; set; }
        public byte[] File { get; set; }
        public string PatientName { get; set; }
        public int PatientId { get; set; }
        public DateTime UploadDate { get; set; }
        public int PKId { get; set; }
    }
}