using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business
{
    public class clsLicense
    {
        private int _licenseId = -1; 
        private int _applicationId = -1; 
        private int _driverId = -1;
        private int _licenseClassId = -1;
        private clsLicenseClassInfo _licenseClassInfo = null;
        private DateTime _issueDate = DateTime.Now;
        private DateTime _expirationDate = DateTime.Now;
        private string _notes = "";
        private double _paidFees = 0;
        private bool _isActive = false;
        private enIssueReason _issueReason = enIssueReason.FirstTime;
        private int _createdByUserId = -1;
        private enum enMode { AddNew = 1, Update = 2};
        private enMode _mode = enMode.AddNew;
        public enum enIssueReason { FirstTime = 1};


        // Setters and Getters
        public int LicenseId
        {
            get { return _licenseId; }
        }

        public int ApplicationId
        {
            set { _applicationId = value; }
            get { return _applicationId; }
        }

        public int DriverId
        {
            set { _driverId = value; }
            get { return _driverId; }
        }

        public int LicenseClassId
        {
            set { _licenseClassId = value; }
            get { return _licenseClassId; }
        }

        public DateTime IssueDate
        {
            set { _issueDate = value; }
            get { return _issueDate; }
        }

        public DateTime ExpirationDate
        {
            set { _expirationDate = value; }
            get { return _expirationDate; }
        }

        public string Notes
        {
            set { _notes = value; }
            get { return _notes; }
        }

        public enIssueReason IssueReason
        {
            set { _issueReason = value; }
            get { return _issueReason; }
        }

        public double PaidFees
        {
            set { _paidFees = value; }
            get { return _paidFees; }
        }

        public bool IsActive
        {
            set { _isActive = value; }
            get { return _isActive; }
        }

        public int CreatedByUserId
        {
            set { _createdByUserId = value; }
            get { return _createdByUserId; }
        }

        public clsLicenseClassInfo LicenseClassInfo
        {
            get { return _licenseClassInfo; }
        }

        // Constructors
        public clsLicense()
        {
             _licenseId = -1;
             _applicationId = -1;
             _driverId = -1;
             _licenseClassId = -1;
             _issueDate = DateTime.Now;
             _expirationDate = DateTime.Now;
             _notes = "";
             _paidFees = 0;
             _isActive = false;
             _issueReason = enIssueReason.FirstTime;
             _createdByUserId = -1;
            _mode = enMode.AddNew;
        }

        private clsLicense(int licenseId, int applicationId, int driverId, int licenseClassId, DateTime issueDate, DateTime experationDate, string notes, enIssueReason issueReason, double paidFees, bool isActive, int createdByUserId)
        {
            _licenseId = licenseId;
            _applicationId = applicationId;
            _driverId = driverId;
            _licenseClassId = licenseClassId;
            _licenseClassInfo = clsLicenseClassInfo.Find(licenseClassId);
            _issueDate = issueDate;
            _expirationDate = experationDate;
            _notes = notes;
            _paidFees = paidFees;
            _isActive = isActive;
            _issueReason = issueReason;
            _createdByUserId = createdByUserId;
            _mode = enMode.Update;
        }

        // Non-Static Methods

        private bool _AddNewLicense()
        {
            _licenseId = clsLicenseData.AddNewLicense(_applicationId, _driverId, _licenseClassId, _issueDate, _expirationDate, _notes, _paidFees, _isActive, (int)_issueReason, _createdByUserId);
            return (_licenseId != -1);
        }
        public bool Save()
        {
            switch (_mode)
            {
                case enMode.AddNew:
                    if (_AddNewLicense())
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
        public static clsLicense FindLicenseByNationalNumber(string nationalNo, int licenseClassId)
        {
            int licenseId = -1, applicationId = -1, driverId = -1, createdByUserId = -1;
            DateTime issueDate = DateTime.Now, experationDate = DateTime.Now;
            string notes = "";
            byte issueReason = 0;
            double paidFees = 0;
            bool isActive = false;

            if (clsLicenseData.FindByNationalNumber(nationalNo, ref licenseId, ref applicationId, ref driverId, licenseClassId, ref createdByUserId, ref issueDate, ref experationDate, ref notes, ref issueReason, ref paidFees, ref isActive))
            {
                return new clsLicense(licenseId, applicationId, driverId, licenseClassId, issueDate, experationDate, notes, (enIssueReason)issueReason, paidFees, isActive, createdByUserId);
            }
            else
                return null;
        }
    }
}
