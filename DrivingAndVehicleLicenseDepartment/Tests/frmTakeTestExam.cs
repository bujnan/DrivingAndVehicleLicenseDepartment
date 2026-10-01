using DrivingAndVehicleLicenseDepartment.Global;
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
    public partial class frmTakeTestExam : Form
    {
        private int _localDrivingLicenseApplicationId = -1;
        private int _testAppointmentId = -1;
        private clsTest _test = null;

        public delegate void OnSaveEventHandler();
        public event OnSaveEventHandler OnSave;
        public frmTakeTestExam(int localDrivingLicenseApplicationId, int testAppointmentId)
        {
            InitializeComponent();
            _localDrivingLicenseApplicationId = localDrivingLicenseApplicationId;
            _testAppointmentId = testAppointmentId;
        }

        private void _SetDataIntoObject()
        {
            _test = new clsTest();
            _test.TestAppointmentId = _testAppointmentId;
            _test.TestResult = rbPass.Checked ? (byte)1 : (byte)0;
            _test.TestNotes = txbNotes.Text.Trim();
            _test.CreatedByUserId = clsGlobal.CurrentUser.UserId;
        }

        private void frmTakeTestExam_Load(object sender, EventArgs e)
        {
            ucActiveTestType1.LoadTestInfo(_localDrivingLicenseApplicationId, _testAppointmentId);
            _SetDataIntoObject();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_test.Save())
            {
                ucActiveTestType1.TestId = _test.TestId;
                ucActiveTestType1.RefreshTestId();
                rbPass.Enabled = false;
                rbFail.Enabled = false;
                txbNotes.Enabled = false;
                btnSave.Enabled = false;
                OnSave?.Invoke();
                MessageBox.Show("Saved Succefully", "Operation Succeed");
            }
            else
                MessageBox.Show("Failed to Save!", "Operation Failed");

        }
    }
}
