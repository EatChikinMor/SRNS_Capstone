using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteDataHelpers.Objects
{
    [Serializable]
    public class Request
    {
        #region private members
        private string _LoginID;
        private string _Name;
        private string _RequestTitle;
        private string _RequestContent;
        private DateTime _RequestDate;
        #endregion

        #region public members
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
        public string Name
        {
            get { return _Name; }
            set
            {
                if (_Name != value)
                {
                    _Name = value;
                }
            }
        }
        public string RequestTitle
        {
            get { return _RequestTitle; }
            set
            {
                if (_RequestTitle != value)
                {
                    _RequestTitle = value;
                }
            }
        }
        public string RequestContent
        {
            get { return _RequestContent; }
            set
            {
                if (_RequestContent != value)
                {
                    _RequestContent = value;
                }
            }
        }
        public DateTime RequestDate
        {
            get { return _RequestDate; }
            set
            {
                if (_RequestDate != value)
                {
                    _RequestDate = value;
                }
            }
        }
        #endregion

        #region constructors
        #endregion
    }
}
