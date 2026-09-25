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

        // Static Methods
        public static byte CountPassedTests(int localDrivingLicenseApplicationId)
        {
            return clsTestData.CountPassedTests(localDrivingLicenseApplicationId);
        }
    }
}
