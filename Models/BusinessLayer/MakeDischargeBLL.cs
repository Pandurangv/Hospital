using Hospital.Models.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hospital.Models.Models;

namespace Hospital.Models.BusinessLayer
{
    public class MakeDischargeBLL
    {
        clsDataAccess mobjDataAcces = new clsDataAccess();

        public MakeDischargeBLL()
        {
            objData = new CriticareHospitalDataContext();
        }

        public CriticareHospitalDataContext objData { get; set; }

        public int Save(EntityMakeDischarge entDischarge)
        {
            tblDischarge tbl = new tblDischarge()
            {
                //DichargeId = tbl.DichargeId,
                SurgeryId = entDischarge.SurgeryId,
                PatientId = entDischarge.PatientId,
                AdviceOnDischarge = entDischarge.AdviceOnDischarge,
                //obj.BP = tbl.BP,
                BSL = entDischarge.BSL,
                BUL = entDischarge.BUL,
                Clubbing = entDischarge.Clubbing,
                CNS = entDischarge.CNS,
                Creat = entDischarge.Creat,
                CVS = entDischarge.CVS,
                Cyanosis = entDischarge.Cyanosis,
                Diagnosis = entDischarge.Diagnosis,
                DischargeReceiptDate = entDischarge.DischargeReceiptDate,
                ECG = entDischarge.ECG,
                FollowUp = entDischarge.FollowUp,
                Haemogram = entDischarge.Haemogram,
                HB = entDischarge.HB,
                HBSAG = entDischarge.HBSAG,
                HistoryClinical = entDischarge.HistoryClinical,
                HIV = entDischarge.HIV,
                Icterus = entDischarge.Icterus,
                Temp = entDischarge.Temp,
                TLC = entDischarge.TLC,
                TreatmentInHospitalisation = entDischarge.TreatmentInHospitalisation,
                TreatmentOnDischarge = entDischarge.TreatmentOnDischarge,
                TypeOfDischarge = entDischarge.TypeOfDischarge,
                Urine = entDischarge.Urine,
                UrineR = entDischarge.UrineR,
                USG = entDischarge.USG,
                XRay = entDischarge.XRay,

                Others = entDischarge.Others,
                OperativeNotes = entDischarge.OperativeNotes,
                PLC = entDischarge.PLC,
                SE = entDischarge.SE,
                PP = entDischarge.PP,
                R = entDischarge.R,
                Pulse = entDischarge.Pulse,
                OperationalProcedure = entDischarge.OperationalProcedure,
                SCreat = entDischarge.SCreat,
                SElect1 = entDischarge.SElect1,
                RespRate = entDischarge.RespRate,
                Pallor = entDischarge.Pallor,
                Oedema = entDischarge.Oedema,
                Skin = entDischarge.Skin,
                RespSystem = entDischarge.RespSystem,
                PerAbd = entDischarge.PerAbd,
                IsDelete = false
            };

            objData.tblDischarges.InsertOnSubmit(tbl);
            objData.SubmitChanges();
            return 1;
        }

        public List<EntityMakeDischarge> GetDischargeInvoiceList()
        {
            List<EntityMakeDischarge> lst = new List<EntityMakeDischarge>();

            lst = (from tbl in objData.tblDischarges
                   join tblAdmit in objData.tblPatientAdmitDetails
                   on tbl.PatientId equals tblAdmit.AdmitId
                   join tblPat in objData.tblPatientMasters
                   on tblAdmit.PatientId equals tblPat.PKId
                   where tbl.IsDelete == false
                   select new EntityMakeDischarge { MRN = tblPat.PatientCode, DichargeId = tbl.DichargeId, PatName = tblPat.PatientFirstName + ' ' + tblPat.PatientMiddleName + ' ' + tblPat.PatientLastName, DischargeReceiptDate = tbl.DischargeReceiptDate, PatientId = tbl.PatientId }).ToList();

            return lst;
        }


        public EntityMakeDischarge GetDischargeDetails(int DischargeId)
        {
            EntityMakeDischarge entDis = new EntityMakeDischarge();

            entDis = (from tbl in objData.tblDischarges
                      where tbl.IsDelete == false
                      && tbl.DichargeId == DischargeId
                      select new EntityMakeDischarge
                      {
                          DichargeId=tbl.DichargeId,
                          SurgeryId=tbl.SurgeryId,
                          PatientId=tbl.PatientId,
                          AdviceOnDischarge = tbl.AdviceOnDischarge,
            //obj.BP = tbl.BP,
            BSL = tbl.BSL,
            BUL = tbl.BUL,
            Clubbing = tbl.Clubbing,
            CNS = tbl.CNS,
            Creat = tbl.Creat,
            CVS = tbl.CVS,
            Cyanosis = tbl.Cyanosis,
            Diagnosis = tbl.Diagnosis,
            DischargeReceiptDate = tbl.DischargeReceiptDate,
            ECG = tbl.ECG,
            FollowUp = tbl.FollowUp,
            Haemogram = tbl.Haemogram,
            HB = tbl.HB,
            HBSAG = tbl.HBSAG,
            HistoryClinical = tbl.HistoryClinical,
            HIV = tbl.HIV,
            Icterus = tbl.Icterus,
            Temp = tbl.Temp,
            TLC = tbl.TLC,
            TreatmentInHospitalisation = tbl.TreatmentInHospitalisation,
            TreatmentOnDischarge = tbl.TreatmentOnDischarge,
            TypeOfDischarge = tbl.TypeOfDischarge,
            Urine = tbl.Urine,
            UrineR = tbl.UrineR,
            USG = tbl.USG,
            XRay = tbl.XRay,

            Others = tbl.Others,
            OperativeNotes = tbl.OperativeNotes,
            PLC = tbl.PLC,
            SE = tbl.SE,
            PP = tbl.PP,
            R = tbl.R,
            Pulse = tbl.Pulse,
            OperationalProcedure = tbl.OperationalProcedure,
            SCreat = tbl.SCreat,
            SElect1 = tbl.SElect1,
            RespRate = tbl.RespRate,
            Pallor = tbl.Pallor,
            Oedema = tbl.Oedema,
            Skin = tbl.Skin,
            RespSystem = tbl.RespSystem,
            PerAbd = tbl.PerAbd,
            
                          
                      }).FirstOrDefault();

            return entDis;
        }



        public int Update(EntityMakeDischarge entDischarge, int DischargeID)
        {
            tblDischarge obj = new tblDischarge();

            obj = (from tbl in objData.tblDischarges
                   where tbl.DichargeId.Equals(DischargeID)
                   && tbl.IsDelete == false
                   select tbl).FirstOrDefault();

            obj.AdviceOnDischarge = entDischarge.AdviceOnDischarge;
            obj.BP = entDischarge.BP;
            obj.BSL = entDischarge.BSL;
            obj.BUL = entDischarge.BUL;
            obj.Clubbing = entDischarge.Clubbing;
            obj.CNS = entDischarge.CNS;
            obj.Creat = entDischarge.Creat;
            obj.CVS = entDischarge.CVS;
            obj.Cyanosis = entDischarge.Cyanosis;
            obj.Diagnosis = entDischarge.Diagnosis;
            obj.DischargeReceiptDate = entDischarge.DischargeReceiptDate;
            obj.ECG = entDischarge.ECG;
            obj.FollowUp = entDischarge.FollowUp;
            obj.Haemogram = entDischarge.Haemogram;
            obj.HB = entDischarge.HB;
            obj.HBSAG = entDischarge.HBSAG;
            obj.HistoryClinical = entDischarge.HistoryClinical;
            obj.HIV = entDischarge.HIV;
            obj.Icterus = entDischarge.Icterus;
            obj.SurgeryId = entDischarge.SurgeryId;
            obj.Temp = entDischarge.Temp;
            obj.TLC = entDischarge.TLC;
            obj.TreatmentInHospitalisation = entDischarge.TreatmentInHospitalisation;
            obj.TreatmentOnDischarge = entDischarge.TreatmentOnDischarge;
            obj.TypeOfDischarge = entDischarge.TypeOfDischarge;
            obj.Urine = entDischarge.Urine;
            obj.UrineR = entDischarge.UrineR;
            obj.USG = entDischarge.USG;
            obj.XRay = entDischarge.XRay;

            obj.Others = entDischarge.Others;
            obj.OperativeNotes = entDischarge.OperativeNotes;
            obj.PLC = entDischarge.PLC;
            obj.SE = entDischarge.SE;
            obj.PP = entDischarge.PP;
            obj.R = entDischarge.R;
            obj.Pulse = entDischarge.Pulse;
            obj.OperationalProcedure = entDischarge.OperationalProcedure;
            obj.SCreat = entDischarge.SCreat;
            obj.SElect1 = entDischarge.SElect;
            obj.RespRate = entDischarge.RespRate;
            obj.Pallor = entDischarge.Pallor;
            obj.Oedema = entDischarge.Oedema;
            obj.Skin = entDischarge.Skin;
            obj.RespSystem = entDischarge.RespSystem;
            obj.PerAbd = entDischarge.PerAbd;
            obj.Urine = entDischarge.Urine;
            obj.HIV = entDischarge.HIV;
            obj.HBSAG = entDischarge.HBSAG;
            obj.PP = entDischarge.PP;
            obj.SE = entDischarge.SE;
            obj.Creat = entDischarge.Creat;
            obj.PLC = entDischarge.PLC;
            obj.TLC = entDischarge.TLC;
            obj.HB = entDischarge.HB;
            obj.TreatmentOnDischarge = entDischarge.TreatmentOnDischarge;
            obj.OperativeNotes = entDischarge.OperativeNotes;
            obj.USG = entDischarge.USG;

            objData.SubmitChanges();
            return 1;
        }

        public DateTime? GetDischargeDetailsByPatientId(int AdmitId)
        {
            DateTime? dt = null;
            try
            {
                tblDischarge objtblDischarge = (from tbl in objData.tblDischarges
                                                where tbl.IsDelete == false
                                                && tbl.PatientId == AdmitId
                                                select tbl).FirstOrDefault();
                if (objtblDischarge != null)
                {
                    dt = objtblDischarge.DischargeReceiptDate;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
    }
}