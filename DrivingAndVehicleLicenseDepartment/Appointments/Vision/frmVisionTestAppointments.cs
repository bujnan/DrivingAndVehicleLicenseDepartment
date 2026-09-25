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

namespace DrivingAndVehicleLicenseDepartment.Appointments.Vision
{
    public partial class frmVisionTestAppointments : Form
    {
        private int _localDrivingLicenseApplicationId = -1;
        private DataTable _testsAppointmentsTable = new DataTable();
        public frmVisionTestAppointments(int localDrivingLicenseApplicationId)
        {
            _localDrivingLicenseApplicationId = localDrivingLicenseApplicationId;
            InitializeComponent();
        }

        private void frmVisionTestAppointments_Load(object sender, EventArgs e)
        {
            ucDrivingLicenseInfo1.LoadApplicationInfo(_localDrivingLicenseApplicationId);

            _testsAppointmentsTable = clsTestAppointment.GetAllVisionTestAppointments(_localDrivingLicenseApplicationId);

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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
