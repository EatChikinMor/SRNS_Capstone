using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteDataHelpers.Objects
{
    //[Serializable]
    public class User
    {
        #region private members
        private int _ID;
        private string _FirstName;
        private string _LastName;
        private bool _IsAdmin;
        private string _LoginID;
        private string _PassHash;
        private int _ManagerID;
        private string _Salt;
        private bool _IsManager;
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
        public string FirstName
        {
            get { return _FirstName; }
            set
            {
                if (_FirstName != value)
                {
                    _FirstName = value;
                }
            }
        }
        public string LastName
        {
            get { return _LastName; }
            set
            {
                if (_LastName != value)
                {
                    _LastName = value;
                }
            }
        }
        public bool IsAdmin
        {
            get { return _IsAdmin; }
            set
            {
                if (_IsAdmin != value)
                {
                    _IsAdmin = value;
                }
            }
        }
        public string LoginID
        {
            get { return _LoginID; }
            set
            {
                if (_LoginID != value)
                {
                    _LoginID = value;
                }
            }
        }
        public string PassHash
        {
            get { return _PassHash; }
            set
            {
                if (_PassHash != value)
                {
                    _PassHash = value;
                }
            }
        }
        public int ManagerID
        {
            get { return _ManagerID; }
            set
            {
                if (_ManagerID != value)
                {
                    _ManagerID = value;
                }
            }
        }
        public string Salt
        {
            get { return _Salt; }
            set
            {
                if (_Salt != value)
                {
                    _Salt = value;
                }
            }
        }
        public bool IsManager
        {
            get { return _IsManager; }
            set
            {
                if (_IsManager != value)
                {
                    _IsManager = value;
                }
            }
        }
        #endregion

        #region constructors
        #endregion
    }
}
