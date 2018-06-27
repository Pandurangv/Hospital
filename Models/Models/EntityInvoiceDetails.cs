﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hospital.Models.Models
{

    /// <summary>
    /// Summary description for EntityInvoiceDetails
    /// </summary>
    /// 
    [Serializable]
    public class EntityInvoiceDetails
    {
        public EntityInvoiceDetails()
        {

        }

        public int TempId { get; set; }

        public int Quantity { get; set; }

        public int NoOfDays { get; set; }

        public decimal PerDayCharge { get; set; }

        public int BillSRNo { get; set; }

        public int BillNo { get; set; }

        public string ChargesName { get; set; }

        public int BedAllocId { get; set; }

        public int DocAllocationId { get; set; }

        public int OTBedAllocId { get; set; }

        public int OtherChargesId { get; set; }

        public decimal NetAmount { get; set; }

        public decimal Amount { get; set; }

        public decimal Total { get; set; }

        public int OtherId { get; set; }

        public string PatientName { get; set; }

        public decimal Discount { get; set; }

        public decimal Vat { get; set; }

        public decimal Service { get; set; }

        public int PatientID { get; set; }

        public DateTime BillDate { get; set; }

        public DateTime AdmitDate { get; set; }

        public bool IsCash { get; set; }


        public bool IsDelete { get; set; }

        public string BillType { get; set; }

        public string PreparedByName { get; set; }

        public decimal TotalAdvance { get; set; }

        public decimal BalanceAmount { get; set; }

        public decimal ReceivedAmount { get; set; }

        public decimal RefundAmount { get; set; }

        public string PatientType { get; set; }

        public string Remarks { get; set; }

        public string BedNo { get; set; }
    }
}