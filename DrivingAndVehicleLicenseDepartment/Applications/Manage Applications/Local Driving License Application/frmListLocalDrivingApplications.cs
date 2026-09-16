using DrivingAndVehicleLicenseDepartment.Applications.Local_Driving_License;
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

        private void _LoadData()
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

        private void frmListLocalDrivingApplications_Load(object sender, EventArgs e)
        {
            _LoadData();
        }

        private void btnAddNewApplication_Click(object sender, EventArgs e)
        {
            frmAddUpdateLocalDrivingLicenseApplication addUpdateLocalDrivingLicenseApplicationForm = new frmAddUpdateLocalDrivingLicenseApplication();
            addUpdateLocalDrivingLicenseApplicationForm.OnSave += _LoadData;
            addUpdateLocalDrivingLicenseApplicationForm.ShowDialog();
        }

        private void txbFilterBy_TextChanged(object sender, EventArgs e)
        {
            string selectedColumnName = "";
            switch (cbFilterBy.Text)
            {
                case "L.D.L App Id":
                    selectedColumnName = "LocalDrivingLicenseApplicationID";
                    break;

                case "National No":
                    selectedColumnName = "NationalNo";
                    break;

                case "Full Name":
                    selectedColumnName = "FullName";
                    break;

                case "Status":
                    selectedColumnName = "ApplicationStatus";
                    break;

                case "None":
                    selectedColumnName = "None";
                    break;
                
            }

            if (txbFilterBy.Text == "" || selectedColumnName == "None")
            {
                allLocalDrivingLicenseApplications.DefaultView.RowFilter = "";
                lblRecords.Text = allLocalDrivingLicenseApplications.DefaultView.Count.ToString();
                return;
            }

            if (selectedColumnName == "LocalDrivingLicenseApplicationID")
            {
                allLocalDrivingLicenseApplications.DefaultView.RowFilter = $"{selectedColumnName} = {txbFilterBy.Text.Trim()}";
            }
            else
                allLocalDrivingLicenseApplications.DefaultView.RowFilter = $"{selectedColumnName} LIKE '{txbFilterBy.Text.Trim()}%'";
        }

        private void txbFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "L.D.L App Id")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txbFilterBy.Text = "";  
            txbFilterBy.Visible = (cbFilterBy.SelectedIndex != 0);
        }
    }
}
