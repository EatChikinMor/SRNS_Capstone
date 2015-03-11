using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteDataHelpers.Objects
{
    public class UserAccess
    {
        #region private members
        private int _UserID;
        private bool _Requests;
        private bool _AddLicense;
        private bool _LicenseCountReport;
        private bool _AvailLicenseReport;
        private bool _ManagLicenseReport;
        private bool _LicenseExpReport;
        private bool _PendChargeReport;
        #endregion

        #region public members
        public int UserID
        {
            get { return _UserID; }
            set
            {
                if (_UserID != value)
                {
                    _UserID = value;
                }
            }
        }
        public bool Requests
        {
            get { return _Requests; }
            set
            {
                if (_Requests != value)
                {
                    _Requests = value;
                }
            }
        }
        public bool AddLicense
        {
            get { return _AddLicense; }
            set
            {
                if (_AddLicense != value)
                {
                    _AddLicense = value;
                }
            }
        }
        public bool LicenseCountReport
        {
            get { return _LicenseCountReport; }
            set
            {
                if (_LicenseCountReport != value)
                {
                    _LicenseCountReport = value;
                }
            }
        }
        public bool AvailLicenseReport
        {
            get { return _AvailLicenseReport; }
            set
            {
                if (_AvailLicenseReport != value)
                {
                    _AvailLicenseReport = value;
                }
            }
        }
        public bool ManagLicenseReport
        {
            get { return _ManagLicenseReport; }
            set
            {
                if (_ManagLicenseReport != value)
                {
                    _ManagLicenseReport = value;
                }
            }
        }
        public bool LicenseExpReport
        {
            get { return _LicenseExpReport; }
            set
            {
                if (_LicenseExpReport != value)
                {
                    _LicenseExpReport = value;
                }
            }
        }
        public bool PendChargeReport
        {
            get { return _PendChargeReport; }
            set
            {
                if (_PendChargeReport != value)
                {
                    _PendChargeReport = value;
                }
            }
        }
        #endregion

        #region constructors
        #endregion
        
    }
}
