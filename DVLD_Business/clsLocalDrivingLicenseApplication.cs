using DVLD_DataAccess;
using System;
using System.Data;
using static System.Net.Mime.MediaTypeNames;

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

        private clsLocalDrivingLicenseApplication(int localDrivingLicenseApplication, int licenseClassId, int applicationId, int applicantPersonId, int applicationTypeId, byte applicationStatus, DateTime applicationDate,DateTime lastStatusDate, double paidFees, int createdByUserId)
        {
            _localDrivingLicenseApplicationId = localDrivingLicenseApplication;
            _licenseClassId = licenseClassId;
            _licenseInfo = clsLicenseInfo.Find(licenseClassId);
            this.ApplicationId = applicationId;
            this.ApplicantPersonId = applicantPersonId;
            this.ApplicationDate = applicationDate;
            this.ApplicationTypeId = applicationTypeId;
            this.ApplicationStatus = applicationStatus;
            this.LastStatusDate = lastStatusDate;
            this.PaidFees = paidFees;
            this.CreatedByUserId = createdByUserId;
            _mode = enMode.Update;
        }

        // Non Static Methods
        private bool _AddNewLocalDrivingLicenseApplication()
        {
            _localDrivingLicenseApplicationId = clsLocalDrivingLicenseApplicationData.AddNewLocalDrivingLicenseApplication(ApplicationId, _licenseClassId);
            return (_localDrivingLicenseApplicationId  != -1);
        }

        private bool _UpdateLocalDarivingLicenseApplication()
        {
            return clsLocalDrivingLicenseApplicationData.UpdateLocalDrivingLicenseApplication(_localDrivingLicenseApplicationId, _licenseClassId);
        }

        public bool Delete()
        {
            if (clsLocalDrivingLicenseApplicationData.DeleteByLocalDrivingLicenseApplicationId(_localDrivingLicenseApplicationId))
            {
                return base.Delete();
            }

            return false;
        }

        public bool Save()
        {
            base.mode = (clsApplication.enMode) _mode; // Cast the sub mode to fit the base mode
            if (!base.Save())
                return false;

            switch (_mode)
            {
                case enMode.AddNew:
                    if (_AddNewLocalDrivingLicenseApplication())
                    {
                        _mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    return _UpdateLocalDarivingLicenseApplication();

                default:
                    return false;
            }
        }

        // Static Methods
        public static DataTable GetAllLocalDrivingLicenseAplications()
        {
            return clsLocalDrivingLicenseApplicationData.GetAllLocalDrivingLicenseAplications();
        }

        public static clsLocalDrivingLicenseApplication FindLocalDrivingLicenseApplicationByApplicantPersonId(int personId)
        {
            int localDrivingLicenseApplication = -1, licenseClassId = -1, applicationId = -1, applicationTypeId = -1, createdByUserId = -1;
            byte applicationStatus = 1;
            DateTime lastStatusDate = DateTime.Now, applicationDate = DateTime.Now;
            double paidFees = 0; 

            if (clsLocalDrivingLicenseApplicationData.FindLocalDrivingLicenseApplicationByApplicantPersonId(personId, ref localDrivingLicenseApplication, ref licenseClassId, ref applicationId, ref applicationTypeId, ref applicationStatus, ref applicationDate, ref lastStatusDate, ref paidFees, ref createdByUserId))
            {
                return new clsLocalDrivingLicenseApplication(localDrivingLicenseApplication, licenseClassId, applicationId, personId, applicationTypeId, applicationStatus, applicationDate, lastStatusDate, paidFees, createdByUserId);
            }
            else
                return null;
        }

        public static clsLocalDrivingLicenseApplication FindByLocalDrivinLicenseApplicationId(int localDrivingLicenseAppId)
        {
            int personId = -1, licenseClassId = -1, applicationId = -1, applicationTypeId = -1, createdByUserId = -1;
            byte applicationStatus = 1;
            DateTime lastStatusDate = DateTime.Now, applicationDate = DateTime.Now;
            double paidFees = 0;

            if (clsLocalDrivingLicenseApplicationData.FindByLocalDrivinLicenseApplicationId(localDrivingLicenseAppId, ref personId, ref licenseClassId, ref applicationId, ref applicationTypeId, ref applicationStatus, ref applicationDate, ref lastStatusDate, ref paidFees, ref createdByUserId))
            {
                return new clsLocalDrivingLicenseApplication(localDrivingLicenseAppId, licenseClassId, applicationId, personId, applicationTypeId, applicationStatus, applicationDate, lastStatusDate, paidFees, createdByUserId);
            }
            else
                return null;
        }

        public static bool Cancel(int applicationId)
        {
            return clsLocalDrivingLicenseApplicationData.Cancel(applicationId);
        }
    }
}
