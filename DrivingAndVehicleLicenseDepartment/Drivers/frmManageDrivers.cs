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

namespace DrivingAndVehicleLicenseDepartment.Drivers
{
    public partial class frmManageDrivers : Form
    {
        private DataTable _driversTable = new DataTable();
        public frmManageDrivers()
        {
            InitializeComponent();
        }

        private void _LoadData()
        {
            _driversTable = clsDriver.GetAllDrivers();
            if (_driversTable.Rows.Count > 0)
            {
                dgvDriversList.DataSource = _driversTable;
                dgvDriversList.ClearSelection();

                dgvDriversList.Columns[0].HeaderText = "Driver Id";
                dgvDriversList.Columns[0].Width = 60;

                dgvDriversList.Columns[1].HeaderText = "Person Id";
                dgvDriversList.Columns[1].Width = 60;

                dgvDriversList.Columns[2].HeaderText = "National No";
                dgvDriversList.Columns[2].Width = 80;

                dgvDriversList.Columns[3].HeaderText = "Full Name";
                dgvDriversList.Columns[3].Width = 180;

                dgvDriversList.Columns[4].HeaderText = "Created Date";
                dgvDriversList.Columns[4].Width = 90;

                dgvDriversList.Columns[5].HeaderText = "Active Licenses";
                dgvDriversList.Columns[5].Width = 140;
            }

            lblRecords.Text = _driversTable.Rows.Count.ToString();
        }

        private void frmManageDrivers_Load(object sender, EventArgs e)
        {
            _LoadData();
            cbFilterBy.SelectedIndex = 0;
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txbFilterBy.Visible = (cbFilterBy.Text != "None");


            if (cbFilterBy.Text == "None")
            {
                txbFilterBy.Enabled = false;
            }
            else
                txbFilterBy.Enabled = true;

            txbFilterBy.Text = "";
            txbFilterBy.Focus();
        }
        

        private void txbFilterBy_TextChanged(object sender, EventArgs e)
        {
            string selectedFilter = "";
            switch (cbFilterBy.Text)
            {
                case "Driver Id":
                    selectedFilter = "DriverID";
                    break;

                case "Person Id":
                    selectedFilter = "PersonID";
                    break;

                case "National No":
                    selectedFilter = "NationalNO";
                    break;

                case "Full Name":
                    selectedFilter = "FullName";
                    break;

                case "Creation Date":
                    selectedFilter = "CreatedDate";
                    break;

                case "Active Licenses":
                    selectedFilter = "ActiveLicenses";
                    break;

                default:
                    selectedFilter = "None";
                    break;
            }

            if (txbFilterBy.Text.Trim() == "" || selectedFilter == "None")
            {
                _driversTable.DefaultView.RowFilter = "";
                lblRecords.Text = dgvDriversList.Rows.Count.ToString();
                return;
            }

            if (selectedFilter != "FullName" && selectedFilter != "NationalNO")
                _driversTable.DefaultView.RowFilter = string.Format("[{0}] = {1}", selectedFilter, txbFilterBy.Text.Trim());
            else
                _driversTable.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", selectedFilter, txbFilterBy.Text.Trim());

            lblRecords.Text = _driversTable.Rows.Count.ToString();
        }

        private void txbFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            //we allow number incase person id or user id is selected.
            if (cbFilterBy.Text == "Driver Id" || cbFilterBy.Text == "Person Id" || cbFilterBy.Text == "Active Licenses")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void showPersonInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPersonDetails personDetailsForm = new frmPersonDetails((int)dgvDriversList.CurrentRow.Cells[1].Value);
            personDetailsForm.ShowDialog();
        }
    }
}
