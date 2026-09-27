using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business
{
    public class clsTest
    {
        public enum enTestType { Vision = 1, Written = 2, Driven = 3};


        // Static Methods
        public static byte CountPassedTests(int localDrivingLicenseApplicationId)
        {
            return clsTestData.CountPassedTests(localDrivingLicenseApplicationId);
        }
    }
}
