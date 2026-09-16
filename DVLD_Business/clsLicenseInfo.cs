using System;
using DVLD_DataAccess;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business
{
    public class clsLicenseInfo
    {
        private int _licenseClassId = -1;
        private string _licenseClassName = "";
        private string _licenseClassDescription = "";
        private short _minimumAllowedAge = -1;
        private short _defaultValidityLength = -1;
        private double _LicenseClassFees = 0;

        // Setters and Getters
        public int LicenseClassId
        {
            get { return _licenseClassId; }
        }
        
        public string LicenseClassName
        {
            get { return _licenseClassName; }
        }

        public string LicenseDescription
        {
            get { return _licenseClassDescription; }
        }

        public short MinimumAllowedAge
        {
            get { return _minimumAllowedAge; }
        }

        public short DefaultValidityLength
        {
            get { return _defaultValidityLength; }
        }

        public double LicenseClassFees
        {
            get { return _LicenseClassFees; }
        }

        // Constructors
        private clsLicenseInfo(int licenseClassId, string licenseClassName, string licenseClassDescription, short minimumAllowedAge, short defaultValidityLength, double licenseClassFees)
        {
            _licenseClassId = licenseClassId;
            _licenseClassName = licenseClassName;
            _licenseClassDescription = licenseClassDescription;
            _minimumAllowedAge = minimumAllowedAge;
            _defaultValidityLength = defaultValidityLength;
            _LicenseClassFees = licenseClassFees;
        }

        // Static Methods
        public static clsLicenseInfo Find(int licenseClassId)
        {
            string licenseClassName = "", licenseClassDescription = "";
            short minimumAllowedAge = -1, defaultValidityLength = -1;
            double licenseClassFees = 0;

            if (clsLicenseInfoData.Find(licenseClassId, ref licenseClassName, ref licenseClassDescription, ref minimumAllowedAge, ref defaultValidityLength, ref licenseClassFees))
            {
                return new clsLicenseInfo(licenseClassId, licenseClassName, licenseClassDescription, minimumAllowedAge, defaultValidityLength, licenseClassFees);
            }
            else
                return null;
        }

        public static clsLicenseInfo Find(string licenseClassName)
        {
            int licenseClassId = -1;
            string licenseClassDescription = "";
            short minimumAllowedAge = -1, defaultValidityLength = -1;
            double licenseClassFees = 0;

            if (clsLicenseInfoData.Find(ref licenseClassId, licenseClassName, ref licenseClassDescription, ref minimumAllowedAge, ref defaultValidityLength, ref licenseClassFees))
            {
                return new clsLicenseInfo(licenseClassId, licenseClassName, licenseClassDescription, minimumAllowedAge, defaultValidityLength, licenseClassFees);
            }
            else
                return null;
        }

        public static bool IsLicenseExistByPersonID(int personId, int licenseClassId)
        {
            return clsLicenseInfoData.IsLicenseExistByPersonID(personId, licenseClassId);
        }
    }
}
