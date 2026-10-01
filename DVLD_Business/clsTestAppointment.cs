using System;
using DVLD_DataAccess;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business
{
    public class clsTestAppointment
    {
        private int _testAppointmentId = -1;
        private int _testTypeId = -1;
        private clsTestType _testTypeInfo = null;
        private int _localDrivingLicenseApplicationId = -1;
        private clsLocalDrivingLicenseApplication _localDrivingLicenseApplicationInfo = null;
        private DateTime _appointmentDate = DateTime.Now;
        private double _paidFees = 0;
        private int _userId = -1;
        private clsUser _userInfo = null;
        private enum enMode { AddNew = 1, Update = 2 }
        private enMode _mode = enMode.AddNew;

        // Setters and Getters
        public int TestAppointmentId
        {
            get { return _testAppointmentId; }
        }

        public int TestTypeId
        {
            set { _testTypeId = value; }
            get { return _testTypeId; }
        }

        public int LocalDrivingLicenseApplicationId
        {
            set { _localDrivingLicenseApplicationId = value; }
            get { return _localDrivingLicenseApplicationId; }
        }

        public DateTime AppointmentDate
        {
            set { _appointmentDate = value; }
            get { return _appointmentDate; }
        }

        public double PaidFees
        {
            set { _paidFees = value; }
            get { return _paidFees; }
        }
        public int CreatedByUserId
        {
            set { _userId = value; }
            get { return _userId; }
        }
        public clsUser CreatedByUser
        {
            set { _userInfo = value; }
            get { return _userInfo; }
        }

        // Constructors
        public clsTestAppointment()
        {
            _testAppointmentId = -1;
            _testTypeId = -1;
            _localDrivingLicenseApplicationId = -1;
            _appointmentDate = DateTime.Now;
            _paidFees = 0;
            _userId = -1;
            _localDrivingLicenseApplicationInfo = null;
            _testTypeInfo = null;
            _userInfo = null;
            _mode = enMode.AddNew;
        }

        private clsTestAppointment(int testAppointmentId, int testTypeId, int localDrivingLicenseApplicationId, DateTime appointmentDate, double paidFees, int userId)
        {
            _testAppointmentId = testAppointmentId;
            _testTypeId = testTypeId;
            _testTypeInfo = clsTestType.Find(testTypeId);
            _localDrivingLicenseApplicationId = localDrivingLicenseApplicationId;
            _localDrivingLicenseApplicationInfo = clsLocalDrivingLicenseApplication.FindByLocalDrivinLicenseApplicationId(localDrivingLicenseApplicationId);
            _appointmentDate = appointmentDate;
            _paidFees = paidFees;
            _userId = userId;
            _userInfo = clsUser.Find(userId);
            _mode = enMode.Update;
        }

        // None-Static Methods

        private bool _AddNewTestAppointment()
        {
            _testAppointmentId = clsTestAppointmentData.AddNewTestAppointment(_testTypeId, _localDrivingLicenseApplicationId, _appointmentDate, _paidFees, _userId);
            return _testAppointmentId != -1;
        }

        private bool _UpdateTestAppointment()
        {
            return clsTestAppointmentData.UpdateTestAppointment(_testAppointmentId, _appointmentDate);
        }
        public bool Save()
        {
            switch (_mode)
            {
                case enMode.AddNew:
                {
                    if (_AddNewTestAppointment())
                    {
                        _mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;
                }

                case enMode.Update:
                {
                    return _UpdateTestAppointment();
                }
            }

            return false;
        }

        // Static Methods
        public static DataTable GetAllTestsAppointments(int localDrivingLicenseApplicationId, int testTypeId)
        {
            return clsTestAppointmentData.GetAllTestsAppointments(localDrivingLicenseApplicationId, testTypeId);
        }

        public static bool IsLocked(int testAppointmentId, int testTypeId, int localDrivingLicenseApplicationId)
        {
            return clsTestAppointmentData.IsLocked(testAppointmentId, testTypeId, localDrivingLicenseApplicationId);
        }

        public static bool HasActiveTestAppointment(int localDrivingLicenseApplicationId, int testTypeId)
        {
            return clsTestAppointmentData.HasActiveTestAppointment(localDrivingLicenseApplicationId, testTypeId);
        }

        public static clsTestAppointment Find(int testAppointmentId)
        {
            int testTypeId = -1, localDrivingLicenseApplicationId = -1, userId = -1;
            DateTime appointmentDate = DateTime.Now;
            double paidFees = 0;

            if (clsTestAppointmentData.Find(testAppointmentId, ref testTypeId, ref localDrivingLicenseApplicationId, ref userId, ref appointmentDate, ref paidFees))
            {
                return new clsTestAppointment(testAppointmentId, testTypeId, localDrivingLicenseApplicationId, appointmentDate, paidFees, userId);
            }
            else
                return null;
        }

        public static bool DidPassActualTest(int localDrivingLicenseApplicationId, int testTypeId)
        {
            return clsTestAppointmentData.DidPassTest(localDrivingLicenseApplicationId, testTypeId);
        }
    }
}
