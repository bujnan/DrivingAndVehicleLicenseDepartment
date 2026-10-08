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
    public partial class ucLicenseInfoWithFilter : UserControl
    {
        private int _licenseId = -1;
        private bool _filterEnabled = true;
        public bool FilterEnabled
        {
            get { return _filterEnabled; }
            set
            {
                _filterEnabled = value;
                gbFilter.Enabled = _filterEnabled;
            }
        }

        public clsLicense SelectedLicenseInfo
        {
            get { return ucLicenseInfo1.SelectedLicenseInfo; }
        }

        public delegate void OnSelectEventHandler();
        public event OnSelectEventHandler OnSelect;

        public ucLicenseInfoWithFilter()
        {
            InitializeComponent();
        }

        public void LoadLicenseInfo(int licenseId)
        {
            clsLicense license = clsLicense.FindLicenseById(licenseId);
            if ( license == null)
            {
                MessageBox.Show($"No License Found with the Id: {licenseId}");
                return;
            }

            string nationalNo = license.DriverInfo.PersonInfo.NationalNo;
            int licenseClassId = license.LicenseClassId;
            ucLicenseInfo1.LoadInfo(nationalNo, licenseClassId);
            _licenseId = licenseId;
        }

        private void btnFindLicense_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error");
                txbLicenseId.Focus();
                return;
            }

            _licenseId = Convert.ToInt32(txbLicenseId.Text);
            LoadLicenseInfo(_licenseId);
            OnSelect?.Invoke();
        }
        public void txbLicenseIdFocus()
        {
            txbLicenseId.Focus();
        }

        private void txbLicenseId_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

            // Check if the pressed key is Enter (character code 13)
            if (e.KeyChar == (char)13)
                btnFindLicense.PerformClick();
        }

        private void txbLicenseId_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txbLicenseId.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txbLicenseId, "This field is required!");
            }
            else
            {
                //e.Cancel = false;
                errorProvider1.SetError(txbLicenseId, null);
            }
        }
    }
}
