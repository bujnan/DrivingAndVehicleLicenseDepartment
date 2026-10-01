using DrivingAndVehicleLicenseDepartment.Properties;
using DVLD_Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrivingAndVehicleLicenseDepartment.Tests.Controls
{
    public partial class ucActiveTestType : UserControl
    {
        private int _localDrivingLicenseApplicationId = -1;
        private clsLocalDrivingLicenseApplication _localDrivingLicenseApplication = null;
        private int _testAppointmentId = -1;
        private clsTestAppointment _testAppointment = null;
        private clsTestType.enTestType _testType = clsTestType.enTestType.Vision;
        public clsTestType.enTestType TestType
        {
            set
            {
                _testType = value;
                switch (_testType)
                {
                    case clsTestType.enTestType.Vision:
                        {
                            gbTestType.Text = "Vision Test";
                            pbTestType.Image = Resources.vision_test_512;
                            break;
                        }

                    case clsTestType.enTestType.Written:
                        {
                            gbTestType.Text = "Writing Test";
                            pbTestType.Image = Resources.written_test_512;
                            break;
                        }

                    case clsTestType.enTestType.Driving:
                        {
                            gbTestType.Text = "Driving Test";
                            pbTestType.Image = Resources.driving_test_512;
                            break;
                        }
                }
            }

            get { return _testType; }
        }

        private int _testId = -1;
        public int TestId
        {
            set { _testId = value; }
            get { return _testId; }
        }

        public ucActiveTestType()
        {
            InitializeComponent();
        }

        public void LoadTestInfo(int localDrivingLicenseApplicationId, int testAppointmentId)
        {
            _localDrivingLicenseApplicationId = localDrivingLicenseApplicationId;
            _testAppointmentId = testAppointmentId;
            _testAppointment = clsTestAppointment.Find(testAppointmentId);
            TestType = (clsTestType.enTestType)_testAppointment.TestTypeId;

            _localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivinLicenseApplicationId(localDrivingLicenseApplicationId);

            if (_localDrivingLicenseApplication == null)
            {
                MessageBox.Show("Seems That You Haven't Any Local Driving License Application Yet!", "Error");
                return;
            }

            lblDrivingLicenseApplicationId.Text = _localDrivingLicenseApplicationId.ToString();
            lblDrivingClass.Text = clsLicenseInfo.Find(_localDrivingLicenseApplication.LicenseClassId).LicenseClassName;
            lblFullName.Text = clsPerson.Find(_localDrivingLicenseApplication.ApplicantPersonId).FullName;
            lblTrial.Text = _localDrivingLicenseApplication.TotalTrialsPerTest(_testAppointment.TestTypeId).ToString();
            lblTestDate.Text = _testAppointment.AppointmentDate.ToString("MMM dd, yyyy");
            lblFees.Text = _testAppointment.PaidFees.ToString();
            lblTestId.Text = (_testId == -1) ? "N/A" : _testId.ToString();
        }

        public void RefreshTestId()
        {
            lblTestId.Text = (_testId == -1) ? "N/A" : _testId.ToString();
        }
    }
}
