using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hospital.Models.Models
{
    public class EntityAllocaConDoc
    {
        public EntityAllocaConDoc()
        {
        }
        private int _SrNo;
        private bool _IsDelete;
        public int SrNo
        {
            get
            {
                return this._SrNo;
            }
            set
            {
                if ((this._SrNo != value))
                {
                    this._SrNo = value;
                }
            }
        }

        public bool IsDelete
        {
            get
            {
                return this._IsDelete;
            }
            set
            {
                if ((this._IsDelete != value))
                {
                    this._IsDelete = value;
                }
            }
        }

        public DateTime Consult_Date { get; set; }
        public int CategoryId { get; set; }
        public int ConsultDocId { get; set; }
        public decimal ConsultCharges { get; set; }
        public string CategoryName { get; set; }
        public string ConsultName { get; set; }

    }
}