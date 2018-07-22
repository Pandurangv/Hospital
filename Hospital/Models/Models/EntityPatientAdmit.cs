using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hospital.Models.Models
{
    public class EntityAnaesthetist
    {
        public EntityAnaesthetist()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #region "Properties"

        public string AnaesthetistCode { get; set; }
        public string AnaesthetistName { get; set; }
        public string TypeOfAnaesthetist { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public decimal ConsultCharges { get; set; }
        #endregion
    }
    public class EntityNursingManagementDetails
    {
        public EntityNursingManagementDetails()
        {
        }
        public int TempId { get; set; }

        private int _SrDetailId;

        private System.Nullable<int> _SrNo;

        private bool _IsDelete;

        public int SrDetailId
        {
            get
            {
                return this._SrDetailId;
            }
            set
            {
                if ((this._SrDetailId != value))
                {
                    this._SrDetailId = value;
                }
            }
        }

        public System.Nullable<int> SrNo
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
        public int? NurseTreatId { get; set; }

        public int NurseId { get; set; }
        public string NurseName { get; set; }
        public string Department { get; set; }
        public DateTime TreatmentDate { get; set; }
        public int CategoryId { get; set; }
        public string CategoryDesc { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public string TreatmentTime { get; set; }
        public string InjectableMedications { get; set; }
        public string Infusions { get; set; }
        public string Oral { get; set; }
        public string NursingCare { get; set; }

    }

    public class EntityAllocaConDocDetails
    {
        public EntityAllocaConDocDetails()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public int TempId { get; set; }
        public int AdmitId { get; set; }

        private int _SrDetailId;

        private System.Nullable<int> _SrNo;

        public string PatientName { get; set; }

        private bool _IsDelete;

        public int SrDetailId
        {
            get
            {
                return this._SrDetailId;
            }
            set
            {
                if ((this._SrDetailId != value))
                {
                    this._SrDetailId = value;
                }
            }
        }

        public System.Nullable<int> SrNo
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

        public int CategoryId { get; set; }
        public int ConsultDocId { get; set; }
        public string CategoryName { get; set; }
        public string ConsultName { get; set; }
        public DateTime Consult_Date { get; set; }
        public decimal ConsultCharges { get; set; }
    }
    /// <summary>
    /// Summary description for EntityPatientAdmit
    /// </summary>
    public class EntityPatientAdmit
    {
        public EntityPatientAdmit()
        {

        }

        public string BP { get; set; }

        public int? PatientTypeId { get; set; }

        //

        public int Age { get; set; }
        public string AgeIn { get; set; }

        public string CompanyName { get; set; }
        public string PatientType { get; set; }

        public string PatientFirstName { get; set; }

        public string Dignosys { get; set; }
        //public string IPDNo { get; set; }

        private int _AdmitId;

        private System.Nullable<System.DateTime> _AdmitDate;

        private System.Nullable<int> _PatientId;

        private System.Nullable<bool> _IsOPD;

        private System.Nullable<bool> _IsIPD;

        private System.Nullable<bool> _IsInsured;

        private System.Nullable<bool> _IsCompany;

        private System.Nullable<bool> _IsDelete;

        private System.Nullable<bool> _IsDischarge;

        private System.Nullable<int> _CompanyId;

        private System.Nullable<int> _InsuranceComId;

        public string ProvDiag { get; set; }

        public string FinalDiag { get; set; }

        public string Ailergies { get; set; }

        public string Symptomes { get; set; }

        public string PastIllness { get; set; }

        public string Temperature { get; set; }

        public string Pulse { get; set; }

        public string Respiration { get; set; }

        public string Others { get; set; }


        public string RS { get; set; }

        public string CVS { get; set; }

        public string PA { get; set; }

        public string CNS { get; set; }

        public string OBGY { get; set; }

        public string XRAY { get; set; }

        public string ECG { get; set; }

        public string USG { get; set; }


        public int AdmitId
        {
            get
            {
                return this._AdmitId;
            }
            set
            {
                if ((this._AdmitId != value))
                {
                    this._AdmitId = value;
                }
            }
        }

        public System.Nullable<System.DateTime> AdmitDate
        {
            get
            {
                return this._AdmitDate;
            }
            set
            {
                if ((this._AdmitDate != value))
                {
                    this._AdmitDate = value;
                }
            }
        }

        public System.Nullable<int> PatientId
        {
            get
            {
                return this._PatientId;
            }
            set
            {
                if ((this._PatientId != value))
                {
                    this._PatientId = value;
                }
            }
        }

        public System.Nullable<bool> IsOPD
        {
            get
            {
                return this._IsOPD;
            }
            set
            {
                if ((this._IsOPD != value))
                {
                    this._IsOPD = value;
                }
            }
        }

        public System.Nullable<bool> IsIPD
        {
            get
            {
                return this._IsIPD;
            }
            set
            {
                if ((this._IsIPD != value))
                {
                    this._IsIPD = value;
                }
            }
        }

        public System.Nullable<bool> IsInsured
        {
            get
            {
                return this._IsInsured;
            }
            set
            {
                if ((this._IsInsured != value))
                {
                    this._IsInsured = value;
                }
            }
        }

        public System.Nullable<bool> IsCompany
        {
            get
            {
                return this._IsCompany;
            }
            set
            {
                if ((this._IsCompany != value))
                {
                    this._IsCompany = value;
                }
            }
        }

        public System.Nullable<bool> IsDelete
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

        public System.Nullable<bool> IsDischarge
        {
            get
            {
                return this._IsDischarge;
            }
            set
            {
                if ((this._IsDischarge != value))
                {
                    this._IsDischarge = value;
                }
            }
        }

        public System.Nullable<int> CompanyId
        {
            get
            {
                return this._CompanyId;
            }
            set
            {
                if ((this._CompanyId != value))
                {
                    this._CompanyId = value;
                }
            }
        }

        public System.Nullable<int> InsuranceComId
        {
            get
            {
                return this._InsuranceComId;
            }
            set
            {
                if ((this._InsuranceComId != value))
                {
                    this._InsuranceComId = value;
                }
            }
        }

        public string PatientAdmitTime { get; set; }
        public int DeptCategory { get; set; }
        public int DeptDoctorId { get; set; }
        //public string OPDNo { get; set; }

        public string InsuName { get; set; }

        public string Weight { get; set; }
    }
}