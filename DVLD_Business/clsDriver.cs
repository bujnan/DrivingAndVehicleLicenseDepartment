using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business
{
    public class clsDriver
    {
        private int _driverId = -1;
        private int _personId = -1;
        private int _createdByUserId = -1;
        private DateTime _createdDate = DateTime.Now;
        private enum enMode { AddNew = 1, Update = 2};
        private enMode _mode = enMode.AddNew;

        // Setter and Getters
        public int DriverId
        {
            get { return _driverId; }
        }

        public int PersonId
        {
            set { _personId = value; }
            get { return _personId; }
        }

        public int CreatedByUserId
        {
            set { _createdByUserId = value; }
            get { return _createdByUserId; }
        }

        public DateTime CreatedDate
        {
            set { _createdDate = value; }
            get { return _createdDate; }
        }

        // Constructors
        public clsDriver()
        {
            _driverId = -1;
            _personId = -1;
            _createdByUserId = -1;
            _createdDate = DateTime.Now;
            _mode = enMode.AddNew;
        }

        private clsDriver(int driverId, int personId, int createdByUserId, DateTime createdDate)
        {
            _driverId = driverId;
            _personId = personId;
            _createdByUserId = createdByUserId;
            _createdDate = createdDate;
            _mode = enMode.Update;
        }

        // Non-Static Methods

        private bool _AddNewDriver()
        {
            _driverId = clsDriverData.AddNewDriver(_personId, _createdByUserId, _createdDate);
            return _driverId != -1;
        }
        public bool Save()
        {
            switch (_mode)
            {
                case enMode.AddNew:
                    if (_AddNewDriver())
                    {
                        _mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                default: 
                    return false;
            }
        }

        // Static Methods
        public static clsDriver FindByPersonId(int personId)
        {
            int driverId = -1;
            int createdByUserId = -1;
            DateTime createdDate = DateTime.Now;

            if (clsDriverData.FindByPersonId(ref driverId, personId, ref createdByUserId, ref createdDate))
                return new clsDriver(driverId, personId, createdByUserId, createdDate);
            else
                return null;
        }

    }
}
