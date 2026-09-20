using System;
using DVLD_DataAccess;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business
{
    public class clsApplication
    {
        private int _applicationId = -1;
        private int _applicantPersonId = -1;
        private DateTime _applicationDate = DateTime.Now;
        private int _applicationTypeId = -1;
        private clsApplicationsTypes _applicationTypeInfo;
        private int _applicationStatus = -1;
        private DateTime _lastStatusDate = DateTime.Now;
        private double _paidFees = 0;
        private int _createdByUserId = -1;
        private clsUser _createdByUserInfo;
        public enum enMode { AddNew = 1, Update = 2};
        public enMode mode = enMode.AddNew;

        public enum enApplicationType { NewLocalLicense = 1, RenewLicense = 2, ReplaceForLostLicense = 3, ReplaceForDamage = 4, ReleaseDetainedLicense = 5, NewInternationalLicense = 6, RetakeTest = 7 }

        // Setters and Getters
        public int ApplicationId
        {
            set { _applicationId = value; }
            get { return _applicationId; }
        }

        public int ApplicantPersonId
        {
            set { _applicantPersonId = value; }
            get { return _applicantPersonId; }
        }

        public DateTime ApplicationDate
        {
            set { _applicationDate = value; }
            get { return _applicationDate; }
        }

        public int ApplicationTypeId
        {
            set { _applicationTypeId = value; }
            get { return _applicationTypeId; }
        }

        public int ApplicationStatus
        {
            set { _applicationStatus = value; }
            get { return _applicationStatus; }
        }

        public DateTime LastStatusDate
        {
            set { _lastStatusDate = value; }
            get { return _lastStatusDate; }
        }

        public double PaidFees
        {
            set { _paidFees = value; }
            get { return _paidFees; }
        }

        public int CreatedByUserId
        {
            set { _createdByUserId = value; }
            get { return _createdByUserId; }
        }

        // Constructors
        public clsApplication()
        {
            _applicationId = -1;
            _applicantPersonId = -1;
            _applicationDate = DateTime.Now;
            _applicationTypeId = -1;
            _applicationStatus = -1;
            _lastStatusDate = DateTime.Now;
            _paidFees = 0;
            _createdByUserId = -1;
            mode = enMode.AddNew;
        }

        private clsApplication(int applicationId, int applicationPersonId, DateTime applicationDate, int applicationTypeId, int applicationStatus, DateTime lastStatusDate, double paidFees, int createdByUserId)
        {
            _applicationId = applicationId;
            _applicantPersonId = applicationPersonId;
            _applicationDate = applicationDate;
            _applicationTypeId = applicationTypeId;
            _applicationTypeInfo = clsApplicationsTypes.Find(applicationTypeId);
            _applicationStatus = applicationStatus;
            _lastStatusDate = lastStatusDate;
            _paidFees = paidFees;
            _createdByUserId = createdByUserId;
            _createdByUserInfo = clsUser.Find(createdByUserId);
            mode = enMode.Update;
        }

        // Non Static Methods
        private bool _AddNewApplication()
        {
            _applicationId = clsApplicationData.AddNew(_applicantPersonId, _applicationDate, _applicationTypeId, _applicationStatus, _lastStatusDate, _paidFees, _createdByUserId);
            return _applicationId != -1;
        }

        private bool _UpdateApplication()
        {
            return clsApplicationData.UpdateApplication(_applicationId, _lastStatusDate);
        }

        protected bool Delete()
        {
            return clsApplicationData.DeleteByApplicationId(_applicationId);
        }

        public bool Save()
        {
            switch(mode)
            {
                case enMode.AddNew:
                    if (_AddNewApplication())
                    {
                        mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return _UpdateApplication();

                default:
                    return false;
            }
        }

        // Static Methods
        public static int GetActiveApplicationIdForLicenseClass(int personId, int applicationTypeId, int licenseClassId)
        {
            return clsApplicationData.GetActiveApplicationIdForLicenseClass(personId, applicationTypeId, licenseClassId);
        }

    }
}
