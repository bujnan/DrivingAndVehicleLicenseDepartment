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

namespace DrivingAndVehicleLicenseDepartment.License.Control
{
    public partial class ucDriverLicenses : UserControl
    {
        private int _driverId = -1;
        private clsDriver _driver = null;
        private DataTable _localDrivingLicensesTable = new DataTable();
        private DataTable _internationalDrivingLicensesTable = new DataTable();

        public ucDriverLicenses()
        {
            InitializeComponent();
        }

        private void _LoadLocalDrivingLicenses()
        {
            _localDrivingLicensesTable = clsDriver.GetAllLocalDrivingLicenses(_driverId);
            if (_localDrivingLicensesTable.Rows.Count > 0)
            {
                dgvLocalLicenses.DataSource = _localDrivingLicensesTable;
                dgvLocalLicenses.ClearSelection();

                dgvLocalLicenses.Columns[0].HeaderText = "Lic. Id";
                dgvLocalLicenses.Columns[0].Width = 60;

                dgvLocalLicenses.Columns[1].HeaderText = "App. Id";
                dgvLocalLicenses.Columns[1].Width = 60;

                dgvLocalLicenses.Columns[2].HeaderText = "Class Name";
                dgvLocalLicenses.Columns[2].Width = 140;

                dgvLocalLicenses.Columns[3].HeaderText = "Issue Date";
                dgvLocalLicenses.Columns[3].Width = 70;

                dgvLocalLicenses.Columns[4].HeaderText = "Expiration Date";
                dgvLocalLicenses.Columns[4].Width = 90;

                dgvLocalLicenses.Columns[5].HeaderText = "Is Active";
                dgvLocalLicenses.Columns[5].Width = 80;
            }

            lblTotalRecords.Text = dgvLocalLicenses.Rows.Count.ToString();
        }

        private void _LoadInternationalDrivingLicenses()
        {
            _internationalDrivingLicensesTable = clsDriver.GetAllInternationalDrivingLicenses(_driverId);
            if (_localDrivingLicensesTable.Rows.Count > 0)
            {
                dgvInterNationalLicenses.DataSource = _localDrivingLicensesTable;
                dgvInterNationalLicenses.ClearSelection();

                dgvInterNationalLicenses.Columns[0].HeaderText = "Int. Lic. Id";
                dgvInterNationalLicenses.Columns[0].Width = 100;

                dgvInterNationalLicenses.Columns[1].HeaderText = "App. Id";
                dgvInterNationalLicenses.Columns[1].Width = 60;

                dgvInterNationalLicenses.Columns[2].HeaderText = "Class Name";
                dgvInterNationalLicenses.Columns[2].Width = 140;
  
                dgvInterNationalLicenses.Columns[3].HeaderText = "Issue Date";
                dgvInterNationalLicenses.Columns[3].Width = 70;

                dgvInterNationalLicenses.Columns[4].HeaderText = "Expiration Date";
                dgvInterNationalLicenses.Columns[4].Width = 90;

                dgvInterNationalLicenses.Columns[5].HeaderText = "Is Active";
                dgvInterNationalLicenses.Columns[5].Width = 80;
            }

            lblTotalRecords.Text = dgvInterNationalLicenses.Rows.Count.ToString();
        }

        public void LoadDrivingLicensesHistory(int driverId)
        {
            _driverId = driverId;
            _driver = clsDriver.FindByDriverId(driverId);

            if (_driver == null)
            {
                MessageBox.Show($"Driver Id: {driverId} Does NOT Exist!", "Warning");
                return;
            }

            _LoadLocalDrivingLicenses();
            // _LoadInternationalDrivingLicenses();
        }
    }
}
