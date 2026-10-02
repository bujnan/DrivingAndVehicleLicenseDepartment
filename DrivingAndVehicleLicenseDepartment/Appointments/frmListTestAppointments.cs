using System;
using DVLD_Business;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DrivingAndVehicleLicenseDepartment.Tests;
using DrivingAndVehicleLicenseDepartment.Properties;

namespace DrivingAndVehicleLicenseDepartment.Appointments.Vision
{
    public partial class frmListTestAppointments : Form
    {
        private int _localDrivingLicenseApplicationId = -1;
        private DataTable _testsAppointmentsTable = new DataTable();
        private int _testTypeId = -1;
        public int TestTypeId
        {
            get { return _testTypeId; }
            set
            {
                _testTypeId = value; 
                switch (_testTypeId)
                {
                    case (int)clsTestType.enTestType.Vision:
                        {
                            this.Text = "Vision Test Appointments";
                            lblTitle.Text = "Vision Test Appointments";
                            pbTestType.Image = Resources.vision_test_512;
                            break;
                        }

                    case (int)clsTestType.enTestType.Written:
                        {
                            this.Text = "Written Test Appointments";
                            lblTitle.Text = "Written Test Appointments";
                            pbTestType.Image = Resources.written_test_512;
                            break;
                        }

                    case (int)clsTestType.enTestType.Driving:
                        {
                            this.Text = "Driving Test Appointments";
                            lblTitle.Text = "Driving Test Appointments";
                            pbTestType.Image = Resources.driving_test_512;
                            break;
                        }
                }
            }
        }


        public frmListTestAppointments(int localDrivingLicenseApplicationId, int testTypeId)
        {
            InitializeComponent();
            _localDrivingLicenseApplicationId = localDrivingLicenseApplicationId;
            TestTypeId = testTypeId;
        }

        private void _LoadData()
        {
            _testsAppointmentsTable = clsTestAppointment.GetAllTestsAppointments(_localDrivingLicenseApplicationId, _testTypeId);

            if (_testsAppointmentsTable.Rows.Count > 0)
            {
                DataTable testsAppointmentsTableWithSpecificColumns = _testsAppointmentsTable.DefaultView.ToTable(false, "TestAppointmentID", "AppointmentDate", "PaidFees", "IsLocked");

                dgvAppointments.DataSource = testsAppointmentsTableWithSpecificColumns;
                dgvAppointments.ClearSelection();

                dgvAppointments.Columns[0].HeaderText = "Appointment Id";
                dgvAppointments.Columns[0].Width = 50;

                dgvAppointments.Columns[1].HeaderText = "Appointment Date";
                dgvAppointments.Columns[1].Width = 120;

                dgvAppointments.Columns[2].HeaderText = "Paid Fees";
                dgvAppointments.Columns[2].Width = 70;

                dgvAppointments.Columns[3].HeaderText = "Is Locked";
                dgvAppointments.Columns[3].Width = 100;
            }

            lblRecord.Text = _testsAppointmentsTable.Rows.Count.ToString();
        }

        private void frmVisionTestAppointments_Load(object sender, EventArgs e)
        {
            ucDrivingLicenseInfo1.LoadApplicationInfo(_localDrivingLicenseApplicationId);
            _LoadData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddNewAppointment_Click(object sender, EventArgs e)
        {
            if (clsTestAppointment.HasActiveTestAppointment(_localDrivingLicenseApplicationId, _testTypeId))
            {
                MessageBox.Show("Already Exist an Active Appointment for this Test", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (clsTestAppointment.DidPassActualTest(_localDrivingLicenseApplicationId, _testTypeId))
            {
                MessageBox.Show("You Can NOT Reserve an Appointment for a Succeessfylly Passed Test", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                takeTestToolStripMenuItem.Enabled = false;
                return;
            }

            frmScheduleTest scheduleTestForm = new frmScheduleTest(_localDrivingLicenseApplicationId, _testTypeId);
            scheduleTestForm.OnSave += _LoadData;
            scheduleTestForm.ShowDialog();
        }

        private void editAppointmentDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int testAppointmentId = (int)dgvAppointments.CurrentRow.Cells[0].Value;
            bool isLocked = clsTestAppointment.IsLocked(testAppointmentId, _testTypeId, _localDrivingLicenseApplicationId);

            if (isLocked)
            {
                MessageBox.Show("This Appointment is Locked! you can NOT edit it", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            frmScheduleTest scheduleTestForm = new frmScheduleTest(_localDrivingLicenseApplicationId, _testTypeId, testAppointmentId);
            scheduleTestForm.OnSave += _LoadData;
            scheduleTestForm.ShowDialog();

        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((bool)dgvAppointments.CurrentRow.Cells[3].Value)
            {
                MessageBox.Show("This Test Already Done!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            int testAppointmentId = (int)dgvAppointments.CurrentRow.Cells[0].Value;
            frmTakeTestExam takeTestForm = new frmTakeTestExam(_localDrivingLicenseApplicationId, testAppointmentId);
            takeTestForm.OnSave += _LoadData;
            takeTestForm.ShowDialog();
        }
    }
}
