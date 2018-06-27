﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hospital.Models.Models
{
    /// <summary>
    /// Summary description for EntityPatientMaster
    /// </summary>
    public class EntityPatientMaster
    {
        public EntityPatientMaster()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #region "Propeties"

        public string BP { get; set; }

        public int? PatientTypeId { get; set; }

        public string PatientCode { get; set; }
        public int PatientInitial { get; set; }
        public string FullName { get; set; }
        public string PatientFirstName { get; set; }
        public string PatientMiddleName { get; set; }
        public string PatientLastName { get; set; }
        public DateTime PatientAdmitDate { get; set; }
        public string PatientAdmitTime { get; set; }
        public string PatientType { get; set; }
        public string PatientAddress { get; set; }
        public string PatientContactNo { get; set; }
        public DateTime? BirthDate { get; set; }
        public string ReferedBy { get; set; }
        public int FloorNo { get; set; }
        public int WardNo { get; set; }
        public int? BedNo { get; set; }
        public string ReasonForAdmit { get; set; }
        public int Gender { get; set; }
        public int Age { get; set; }
        public int? Occupation { get; set; }
        public string City { get; set; }
        public String State { get; set; }
        public string Country { get; set; }
        public int? ConsultingDr { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyContact { get; set; }
        public string EntryBy { get; set; }
        public string ChangeBy { get; set; }
        public bool IsDischarged { get; set; }
        public int Religion { get; set; }
        public int Caste { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public Byte FileData { get; set; }
        public Byte[] IDProof { get; set; }
        public Byte[] InsurenaceProof { get; set; }
        public Byte[] PatientPhoto { get; set; }
        public string InsuranceCompName { get; set; }
        public bool lsInsurance { get; set; }
        public string PatientHistory { get; set; }
        public string OPDRoom { get; set; }
        public string PersonalHistory { get; set; }
        public string PastMedicalHistory { get; set; }
        public string FamilyHistory { get; set; }
        public string BloodGroup { get; set; }
        public string DiscontRemark { get; set; }
        public bool Discontinued { get; set; }
        public string GenderDesc { get; set; }
        public int PatientId { get; set; }
        public int? CompanyId { get; set; }
        public int InsuranceCompID { get; set; }
        public int AdmitId { get; set; }
        //public string OPDNo { get; set; }

        #endregion

        public string Dignosys { get; set; }

        public int PKId { get; set; }

        //public string IPDNo { get; set; }
        public string AgeIn { get; set; }

        public int? DeptCategory { get; set; }

        public string EmpName { get; set; }
        public int? DeptDoctorId { get; set; }
        //public byte[] EndoscopyFile { get; set; }
        //public byte[] AudiometryFile { get; set; }

        public string Weight { get; set; }

        public string CompName { get; set; }

        public string InsuName { get; set; }

        public string AdmitTime { get; set; }

        public string Address { get; set; }

        public string ContactNo { get; set; }

        public int? InsuranceCompanyId { get; set; }

        public bool? IsInsuered { get; set; }

        public decimal? ConsultingCharges { get; set; }

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
    }
}