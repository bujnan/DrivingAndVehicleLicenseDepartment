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
        private int _testId = -1;
        private int _testAppointmentId = -1;
        private byte _testResult = 0;
        private string _testNotes = "";
        private int _createdByUserId = -1;
        public enum enTestType { Vision = 1, Written = 2, Driven = 3};

        // Setters and Getters
        public int TestId
        {
            get { return _testId; }
        }
        
        public int TestAppointmentId
        {
            set { _testAppointmentId = value; }
            get { return _testAppointmentId; }
        }

        public byte TestResult
        {
            set { _testResult = value; }
            get { return _testResult; }
        }

        public string TestNotes
        {
            set { _testNotes = value; }
            get { return _testNotes; }
        }

        public int CreatedByUserId
        {
            set { _createdByUserId = value; }
            get { return _createdByUserId; }
        }

        // Non Static Methods
        public bool Save()
        {
            _testId = clsTestData.AddNewTest(_testAppointmentId, _testResult, _testNotes, _createdByUserId);
            return _testId != -1;
        }

        // Static Methods
        public static byte CountPassedTests(int localDrivingLicenseApplicationId)
        {
            return clsTestData.CountPassedTests(localDrivingLicenseApplicationId);
        }
    }
}
