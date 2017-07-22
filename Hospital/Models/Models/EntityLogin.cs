using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace Hospital.Models.Models
{
    /// <summary>
    /// Summary description for EntityLogin
    /// </summary>
    public class EntityLogin
    {
        public EntityLogin()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private int _PKId;

        private string _UserName;

        private string _Password;

        private string _UserType;

        private bool _Discontinued;

        private bool _IsFirstLogin;

        //public string UserType { get; set; }

        public int? DeisgId { get; set; }

        public int? EmpId { get; set; }

        public int PKId
        {
            get
            {
                return this._PKId;
            }
            set
            {
                if ((this._PKId != value))
                {
                    this._PKId = value;
                }
            }
        }

        public string UserName
        {
            get
            {
                return this._UserName;
            }
            set
            {
                if ((this._UserName != value))
                {
                    this._UserName = value;
                }
            }
        }

        public string Password
        {
            get
            {
                return this._Password;
            }
            set
            {
                if ((this._Password != value))
                {
                    this._Password = value;
                }
            }
        }

        public string UserType
        {
            get
            {
                return this._UserType;
            }
            set
            {
                if ((this._UserType != value))
                {
                    this._UserType = value;
                }
            }
        }

        public bool Discontinued
        {
            get
            {
                return this._Discontinued;
            }
            set
            {
                if ((this._Discontinued != value))
                {
                    this._Discontinued = value;
                }
            }
        }

        public bool IsFirstLogin
        {
            get
            {
                return this._IsFirstLogin;
            }
            set
            {
                if ((this._IsFirstLogin != value))
                {
                    this._IsFirstLogin = value;
                }
            }
        }

        public string OldPass { get; set; }

        public object ConfirmPass { get; set; }

        public string NewPass { get; set; }
    }

    public class EntityNurse
    {
        public EntityNurse()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #region "Properties"

        public string NurseCode { get; set; }
        public string NurseName { get; set; }
        public int DeptId { get; set; }
        public string DepartmentName { get; set; }
        #endregion
    }

    public class EntityNursingManagement
    {
        public EntityNursingManagement()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        private bool _IsDelete;
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

        #region "Properties"
        public int SrNo { get; set; }
        public int NurseId { get; set; }
        public string NurseName { get; set; }
        public string Department { get; set; }
        public DateTime TreatmentDate { get; set; }
        public int CategoryId { get; set; }
        public string CategoryDesc { get; set; }

        #endregion
    }
}