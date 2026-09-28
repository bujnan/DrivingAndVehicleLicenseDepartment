using DrivingAndVehicleLicenseDepartment.Global;
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

namespace DrivingAndVehicleLicenseDepartment.Tests
{
    public partial class ucScheduleTestType : UserControl
    {
        private enum enMode {AddNew = 1, Update = 2};
        private enMode _mode = enMode.AddNew;
        private enum enCreationMode { FirstTimeSchedule = 1, RetakeTestSchedule = 2};
        private enCreationMode _creationMode = enCreationMode.FirstTimeSchedule;
        private clsTest.enTestType _testTypeId = clsTest.enTestType.Vision;
        public clsTest.enTestType TestType
        {
            set 
            { 
                _testTypeId = value; 
                switch (_testTypeId)
                {
                    case clsTest.enTestType.Vision:
                        {
                            gbTestType.Text = "Vision Test";
                            pbTestType.Image = Resources.vision_test_512;
                            break;
                        }

                    case clsTest.enTestType.Written:
                        {
                            gbTestType.Text = "Written Test";
                            pbTestType.Image = Resources.written_test_512;
                            break;
                        }

                    case clsTest.enTestType.Driven:
                        {
                            gbTestType.Text = "Driving Test";
                            pbTestType.Image = Resources.driving_test_512;
                            break;
                        }
                }
            }

            get { return _testTypeId; }
        }

        private int _localDrivingLicenseApplicationId = -1;
        private clsLocalDrivingLicenseApplication _localDrivingLicenseApplication = null;
        private int _testAppointmentId = -1;
        private clsTestAppointment _testAppointment = null;

        public ucScheduleTestType()
        {
            InitializeComponent();
        }

        private bool _LoadTestAppointmentData()
        {
            _testAppointment = clsTestAppointment.Find(_testAppointmentId);

            if (_testAppointment == null)
                return false;

            // we compare the current date with the appointment date to set the min date.
            if (DateTime.Compare(DateTime.Now, _testAppointment.AppointmentDate) < 0)
                dtpDate.MinDate = DateTime.Now;
            else
                dtpDate.MinDate = _testAppointment.AppointmentDate;

            dtpDate.Value = _testAppointment.AppointmentDate;

            return true;
        }

        public void LoadInfo(int localDrivingLicenseApplicationId, int testAppointmentId = -1)
        {
            _localDrivingLicenseApplicationId = localDrivingLicenseApplicationId;
            _testAppointmentId = testAppointmentId;

            _localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivinLicenseApplicationId(localDrivingLicenseApplicationId);

            if (testAppointmentId == -1)
                _mode = enMode.AddNew;
            else
                _mode = enMode.Update;

            if (_mode == enMode.AddNew)
            {
                dtpDate.MinDate = DateTime.Now;
                lblFees.Text = clsTestType.Find((int)_testTypeId).Fees.ToString();
                lblRetakeTestApplicationId.Text = "N/A"; 

                _testAppointment = new clsTestAppointment();
            }
            else
            {
                if (!_LoadTestAppointmentData())
                    return;
            }

            lblDrivingLicenseApplicationId.Text = _localDrivingLicenseApplication.LocalDrivingLicenseApplicationId.ToString();
            lblDrivingClass.Text = _localDrivingLicenseApplication.LicenseInfo.LicenseClassName;
            lblFullName.Text = clsPerson.Find(_localDrivingLicenseApplication.ApplicantPersonId).FullName;
            lblTrial.Text = _localDrivingLicenseApplication.TotalTrialsPerTest((int)_testTypeId).ToString();
            lblFees.Text = _localDrivingLicenseApplication.PaidFees.ToString();

            if (_localDrivingLicenseApplication.DidAttendTestType((int)_testTypeId))
                _creationMode = enCreationMode.RetakeTestSchedule;
            else
                _creationMode = enCreationMode.FirstTimeSchedule;

            if (_creationMode == enCreationMode.RetakeTestSchedule)
            {
                gbRetakeTestInfo.Enabled = true;
                lblRetakeApplicationFees.Text = clsApplicationsTypes.Find((int)clsApplication.enApplicationType.RetakeTest).Fees.ToString();
                lblRetakeTestApplicationId.Text = "0";
            }
            else
            {
                gbRetakeTestInfo.Enabled = false;
                lblRetakeApplicationFees.Text = "0";
                lblRetakeTestApplicationId.Text = "N/A"; 
            }

            lblTotalFees.Text = (Convert.ToSingle(lblFees.Text) + Convert.ToSingle(lblRetakeApplicationFees.Text)).ToString();
        }

        private bool _HasPassedPreviousTest()
        {
            switch (_testTypeId)
            {
                case clsTest.enTestType.Vision:
                {
                    lblWarningMessage.Visible = false;
                    return true;
                }

                case clsTest.enTestType.Written:
                {
                    if (!_localDrivingLicenseApplication.DidPassTestType((int)clsTest.enTestType.Vision))
                    {
                        lblWarningMessage.Text = "Can NOT Schedule! Vision Test should be passed first";
                        lblWarningMessage.Visible = true;
                        dtpDate.Enabled = false;
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }

                case clsTest.enTestType.Driven:
                {
                    if (!_localDrivingLicenseApplication.DidPassTestType((int)clsTest.enTestType.Written))
                    {
                        lblWarningMessage.Text = "Can NOT Schedule! Writing Test should be passed first";
                        lblWarningMessage.Visible = true;
                        dtpDate.Enabled = false;
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!_HasPassedPreviousTest())
                return;

            //if (!_HandleRetakeApplication())
            //    return;

            _testAppointment.TestTypeId = (int)_testTypeId;
            _testAppointment.LocalDrivingLicenseApplicationId = _localDrivingLicenseApplication.LocalDrivingLicenseApplicationId;
            _testAppointment.AppointmentDate = dtpDate.Value;
            _testAppointment.PaidFees = Convert.ToSingle(lblFees.Text);
            _testAppointment.CreatedByUserId = clsGlobal.CurrentUser.UserId;

            if (_testAppointment.Save())
            {
                _mode = enMode.Update;
                MessageBox.Show("Saved Successfully", "Operation Succeed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Error: NOT Saved!", "Operation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
