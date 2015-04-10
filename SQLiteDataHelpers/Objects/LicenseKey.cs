using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteDataHelpers.Objects
{
    public class LicenseKey
    {
        #region private members
        private int _SoftwareId;
        private string _Description;
        private string _Key;
        private string _Holder;
        private string _Manager;
        private string _HolderID;
        private decimal _LicenseCost;
        private string _RequisitionNumber;
        private bool _ChargebackComplete;
        private int _Provider;
        private int _Assignment;
        private string _Speedchart;
        private DateTime _DateUpdated;
        private DateTime _DateAssigned;
        private DateTime _DateRemoved;
        private DateTime _DateExpiring;
        private int _LicenseHolderCompany;
        private string _Comments;

        #endregion

        #region public members
        public int SoftwareId
        {
            get { return _SoftwareId; }
            set
            {
                if (_SoftwareId != value)
                {
                    _SoftwareId = value;
                }
            }
        }
        public string Description
        {
            get { return _Description; }
            set
            {
                if (_Description != value)
                {
                    _Description = value;
                }
            }
        }
        public string Key
        {
            get { return _Key; }
            set
            {
                if (_Key != value)
                {
                    _Key = value;
                }
            }
        }
        public string Holder
        {
            get { return _Holder; }
            set
            {
                if (_Holder != value)
                {
                    _Holder = value;
                }
            }
        }
        public string Manager
        {
            get { return _Manager; }
            set
            {
                if (_Manager != value)
                {
                    _Manager = value;
                }
            }
        }
        public string HolderID
        {
            get { return _HolderID; }
            set
            {
                if (_HolderID != value)
                {
                    _HolderID = value;
                }
            }
        }
        public decimal LicenseCost
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
        public string RequisitionNumber
        {
            get { return _RequisitionNumber; }
            set
            {
                if (_RequisitionNumber != value)
                {
                    _RequisitionNumber = value;
                }
            }
        }
        public bool ChargebackComplete
        {
            get { return _ChargebackComplete; }
            set
            {
                if (_ChargebackComplete != value)
                {
                    _ChargebackComplete = value;
                }
            }
        }
        public int Provider
        {
            get { return _Provider; }
            set
            {
                if (_Provider != value)
                {
                    _Provider = value;
                }
            }
        }
        public int Assignment
        {
            get { return _Assignment; }
            set
            {
                if (_Assignment != value)
                {
                    _Assignment = value;
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
        public DateTime DateUpdated
        {
            get { return _DateUpdated; }
            set
            {
                if (_DateUpdated != value)
                {
                    _DateUpdated = value;
                }
            }
        }
        public DateTime DateAssigned
        {
            get { return _DateAssigned; }
            set
            {
                if (_DateAssigned != value)
                {
                    _DateAssigned = value;
                }
            }
        }
        public DateTime DateRemoved
        {
            get { return _DateRemoved; }
            set
            {
                if (_DateRemoved != value)
                {
                    _DateRemoved = value;
                }
            }
        }
        public DateTime DateExpiring
        {
            get { return _DateExpiring; }
            set
            {
                if (_DateExpiring != value)
                {
                    _DateExpiring = value;
                }
            }
        }
        public int LicenseHolderCompany
        {
            get { return _LicenseHolderCompany; }
            set
            {
                if (_LicenseHolderCompany != value)
                {
                    _LicenseHolderCompany = value;
                }
            }
        }
        public string Comments
        {
            get { return _Comments; }
            set
            {
                if (_Comments != value)
                {
                    _Comments = value;
                }
            }
        }

        #endregion

        #region constructors

        #endregion



    }
}
