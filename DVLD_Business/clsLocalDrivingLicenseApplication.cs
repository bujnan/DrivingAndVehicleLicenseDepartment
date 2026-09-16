using System;
using System.Data;
using DVLD_DataAccess;

namespace DVLD_Business
{
    public class clsLocalDrivingLicenseApplication : clsApplication
    {
        private enum enMode { AddNew = 1, Update = 2 };
        private enMode _mode = enMode.AddNew;
        private int _localDrivingLicenseApplicationId = -1;
        private int _licenseClassId = -1;
        private clsLicenseInfo _licenseInfo = null;

        // Setters and Getters
        public int LocalDrivingLicenseApplicationId
        {
            get { return _localDrivingLicenseApplicationId; }
        }

        public int LicenseClassId
        {
            set { _licenseClassId = value; }
            get { return _licenseClassId; }
        }

        // Constructors
        public clsLocalDrivingLicenseApplication()
        {
            _localDrivingLicenseApplicationId = -1;
            _licenseClassId = -1;
            _mode = enMode.AddNew;
        }

        private clsLocalDrivingLicenseApplication(int localDrivingLicenseApplication, int licenseClassId, int applicationId, int applicantPersonId, int applicationTypeId, byte applicationStatus, DateTime lastStatusDate, double paidFees, int createdByUserId)
        {
            _localDrivingLicenseApplicationId = localDrivingLicenseApplication;
            _licenseClassId = licenseClassId;
            _licenseInfo = clsLicenseInfo.Find(licenseClassId);
            this.ApplicationId = applicationId;
            this.ApplicantPersonId = applicantPersonId;
            this.ApplicationDate = DateTime.Now;
            this.ApplicationTypeId = applicationTypeId;
            this.ApplicationStatus = applicationStatus;
            this.LastStatusDate = lastStatusDate;
            this.PaidFees = paidFees;
            this.CreatedByUserId = createdByUserId;
            _mode = enMode.Update;
        }

        // Non Static Methods
        private bool _AddNewLocalApplication()
        {
            _localDrivingLicenseApplicationId = clsLocalDrivingLicenseApplicationData.AddNewLocalDrivingLicenseApplication(ApplicationId, _licenseClassId);
            return (_localDrivingLicenseApplicationId  != -1);
        }

        public bool Save()
        {
            base.mode = (clsApplication.enMode) _mode; // Cast the sub mode to fit the base mode
            if (!base.Save())
                return false;

            switch (_mode)
            {
                case enMode.AddNew:
                    if (_AddNewLocalApplication())
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
        public static DataTable GetAllLocalDrivingLicenseAplications()
        {
            return clsLocalDrivingLicenseApplicationData.GetAllLocalDrivingLicenseAplications();
        }

    }
}
