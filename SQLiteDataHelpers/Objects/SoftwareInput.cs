using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteDataHelpers.Objects
{
    //[Serializable]
    public class SoftwareInput
    {
        #region private members
        private int _ID;
        private string _SoftwareName;
        private string _SoftwareDescription;
        private string _LicenseNumber;
        private string _LicenseHolder;
        private string _LicenseHoldUid;
        private string _AscReqNum;
        private string _Speedchart;
        private string _LicenseMan;
        private string _LicenseCost;
        private bool _radioAssign;
        private bool _radioRemove;
        private bool _radioAvailable;
        private bool _radioSRNS;
        private bool _radioSRR;
        private bool _radioDOE;
        private bool _radioCen;
        #endregion

        #region public members
        public int ID
        {
            get { return _ID; }
            set
            {
                if (_ID != value)
                {
                    _ID = value;
                }
            }
        }
        public string SoftwareName
        {
            get { return _SoftwareName; }
            set
            {
                if (_SoftwareName != value)
                {
                    _SoftwareName = value;
                }
            }
        }
        public string SoftwareDescription
        {
            get { return _SoftwareDescription; }
            set
            {
                if (_SoftwareDescription != value)
                {
                    _SoftwareDescription = value;
                }
            }
        }

        public string LicenseNumber
        {
            get { return _LicenseNumber; }
            set
            {
                if (_LicenseNumber != value)
                {
                    _LicenseNumber = value;
                }
            }
        }

        public string LicenseHolder
        {
            get { return _LicenseHolder; }
            set
            {
                if (_LicenseHolder != value)
                {
                    _LicenseHolder = value;
                }
            }
        }

        public string LicenseHoldUid
        {
            get { return _LicenseHoldUid; }
            set
            {
                if (_LicenseHoldUid != value)
                {
                    _LicenseHoldUid = value;
                }
            }
        }

        public string AscReqNum
        {
            get { return _AscReqNum; }
            set
            {
                if (_AscReqNum != value)
                {
                    _AscReqNum = value;
                }
            }
        }

        public string Speedchart
        {
            get { return _Speedchart; }
            set
            {
                if (_Speedchart != value)
                {
                    _Speedchart = value;
                }
            }
        }

        public string LicenseMan
        {
            get { return _LicenseMan; }
            set
            {
                if (_LicenseMan != value)
                {
                    _LicenseMan = value;
                }
            }
        }

        public bool radioAssign
        {
            get { return _radioAssign; }
            set
            {
                if (_radioAssign != value)
                {
                    _radioAssign = value;
                }
            }
        }

        public bool radioRemove
        {
            get { return _radioRemove; }
            set
            {
                if (_radioRemove != value)
                {
                    _radioRemove = value;
                }
            }
        }

        public bool radioAvailable 
        {
            get { return _radioAvailable; }
            set
            {
                if (_radioAvailable != value)
                {
                    _radioAvailable = value;
                }
            }
        }

        public bool radioSRNS
        {
            get { return _radioSRNS; }
            set
            {
                if (_radioSRNS != value)
                {
                    _radioSRNS = value;
                }
            }
        }

        public bool radioSRR
        {
            get { return _radioSRR; }
            set
            {
                if (_radioSRR != value)
                {
                    _radioSRR = value;
                }
            }
        }

        public bool radioDOE
        {
            get { return _radioDOE; }
            set
            {
                if (_radioDOE != value)
                {
                    _radioDOE = value;
                }
            }
        }

        public bool radioCen
        {
            get { return _radioCen; }
            set
            {
                if (_radioCen != value)
                {
                    _radioCen = value;
                }
            }
        }
        public string LicenseCost
        {
            get { return _LicenseCost; }
            set
            {
                if (_LicenseCost != value)
                {
                    _LicenseCost = value;
                }
            }
        }
        #endregion

        #region constructors
        #endregion
    }
}
