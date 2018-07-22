using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hospital.Models.Models
{

    /// <summary>
    /// Summary description for EntityCountry
    /// </summary>
    public class EntityCountry
    {
        public EntityCountry()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #region "Properties"

        public string CountryCode { get; set; }
        public string CountryDesc { get; set; }
        public string EntryBy { get; set; }
        public string ChangeBy { get; set; }
        #endregion
    }

    public class EntityOTMedicineBill:Error
    {
        public EntityOTMedicineBill()
        {
            ProductList = new List<EntityOTMedicineBillDetails>();
        }

        public List<EntityOTMedicineBillDetails> ProductList { get; set; }

        public string EmployeeName { get; set; }

        public string PatientCode { get; set; }

        public string TreatmentDetails { get; set; }

        public string TreatmentPro { get; set; }

        private int _BillNo;
        private bool _IsDelete;
        public string PatientName { get; set; }
        public string MedicineName { get; set; }
        public int Quantity { get; set; }
        public int BillNo
        {
            get
            {
                return this._BillNo;
            }
            set
            {
                if ((this._BillNo != value))
                {
                    this._BillNo = value;
                }
            }
        }

        public string TreatmentTime { get; set; }

        public decimal? TotalTaxAmount { get; set; }

        public decimal? NetAmount { get; set; }

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

        public DateTime? Bill_Date { get; set; }
        public int? AdmitId { get; set; }
        public decimal? TotalAmount { get; set; }
    }

    public class EntityOTMedicineBillDetails
    {
        public EntityOTMedicineBillDetails()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public int TempId { get; set; }
        public int? MedicineId { get; set; }

        public decimal? TaxPercent { get; set; }

        public decimal? TaxAmount { get; set; }

        public DateTime? PresDate { get; set; }

       

        public int? MorningQty { get; set; }

        public int? AfterNoonQty { get; set; }

        public int? EveningQty { get; set; }

        public int BillDetailId { get; set; }

        private System.Nullable<int> _BillNo;

        private string _MedicineName;
        public int? TabletId { get; set; }

        private System.Nullable<int> _Quantity;

        private bool? _IsDelete;

        

        public System.Nullable<int> BillNo
        {
            get
            {
                return this._BillNo;
            }
            set
            {
                if ((this._BillNo != value))
                {
                    this._BillNo = value;
                }
            }
        }

        public string MedicineName
        {
            get
            {
                return this._MedicineName;
            }
            set
            {
                if ((this._MedicineName != value))
                {
                    this._MedicineName = value;
                }
            }
        }


        public System.Nullable<int> Quantity
        {
            get
            {
                return this._Quantity;
            }
            set
            {
                if ((this._Quantity != value))
                {
                    this._Quantity = value;
                }
            }
        }

        public bool? IsDelete
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

        public string ProductName { get; set; }

        public decimal? Price { get; set; }
        public decimal? Amount { get; set; }

        public string BatchNo { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public int? ProductId { get; set; }

        public int? LabTestId { get; set; }

        public string TestName { get; set; }

        public int? NightQty { get; set; }
    }
}