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

namespace DrivingAndVehicleLicenseDepartment.Applications.Manage_Applications.Local_Driving_License_Application
{
    public partial class frmListLocalDrivingApplications : Form
    {
        private DataTable allLocalDrivingLicenseApplications = new DataTable();
        public frmListLocalDrivingApplications()
        {
            InitializeComponent();
        }

        private void frmListLocalDrivingApplications_Load(object sender, EventArgs e)
        {
            allLocalDrivingLicenseApplications = clsLocalDrivingLicenseApplication.GetAllLocalDrivingLicenseAplications();
            if (allLocalDrivingLicenseApplications.Rows.Count > 0)
            {
                dgvListLocalApplications.DataSource = allLocalDrivingLicenseApplications;

                dgvListLocalApplications.Columns[0].HeaderText = "L.D.L App Id";
                dgvListLocalApplications.Columns[0].Width = 110;

                dgvListLocalApplications.Columns[1].HeaderText = "Driving Class";
                dgvListLocalApplications.Columns[1].Width = 190;

                dgvListLocalApplications.Columns[2].HeaderText = "National No";
                dgvListLocalApplications.Columns[2].Width = 125;

                dgvListLocalApplications.Columns[3].HeaderText = "Full Name";
                dgvListLocalApplications.Columns[3].Width = 140;

                dgvListLocalApplications.Columns[4].HeaderText = "Application Date";
                dgvListLocalApplications.Columns[4].Width = 140;

                dgvListLocalApplications.Columns[5].HeaderText = "Passed Tests";
                dgvListLocalApplications.Columns[5].Width = 120;

                dgvListLocalApplications.Columns[6].HeaderText = "Status";
                dgvListLocalApplications.Columns[6].Width = 120;
            }
            cbFilterBy.SelectedIndex = 0;
            lblRecords.Text = allLocalDrivingLicenseApplications.Rows.Count.ToString();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txbFilterBy.Visible = (cbFilterBy.SelectedIndex != 0);
        }
    }
}
