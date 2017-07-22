﻿using System;
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

    public class EntityOTMedicineBill
    {
        public EntityOTMedicineBill()
        {
        }
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
        public decimal TotalAmount { get; set; }
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
        public int MedicineId { get; set; }

        private int _BillDetailId;

        private System.Nullable<int> _BillNo;

        private string _MedicineName;
        public int TabletId { get; set; }

        private System.Nullable<int> _Quantity;

        private bool _IsDelete;

        public int BillDetailId
        {
            get
            {
                return this._BillDetailId;
            }
            set
            {
                if ((this._BillDetailId != value))
                {
                    this._BillDetailId = value;
                }
            }
        }

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

        public decimal Price { get; set; }
        public decimal Amount { get; set; }
    }
}