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

namespace DrivingAndVehicleLicenseDepartment.License
{
    public partial class frmReplaceDrivingLicense : Form
    {
        private int _licenseId = -1;
        private clsLicense _license = null;
        public frmReplaceDrivingLicense()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private int _GetApplicationTypeId()
        {
            if (rbDamaged.Checked)
                return (int)clsApplicationsTypes.enApplicationType.ReplacementForDamage;
            else
                return (int)clsApplicationsTypes.enApplicationType.ReplacementForLost;
        }

        private void _LoadNewLicenseInfo()
        {
            _license = ucLicenseInfoWithFilter1.SelectedLicenseInfo;

            if (_license.ExpirationDate < DateTime.Now)
            {
                MessageBox.Show("This License Is Expired!", "Warning");
                return;
            }

            if (!_license.IsActive)
            {
                MessageBox.Show("This License Is NOT Active! You Can't Replace it", "Warning");
                return;
            }

            lblApplicationDate.Text = DateTime.Now.ToString("MMM dd, yyyy");
            lblApplicationFees.Text = clsApplicationsTypes.Find(_GetApplicationTypeId()).Fees.ToString();
            lblOldLicenseId.Text = _license.LicenseId.ToString();
            lblCreatedBy.Text = clsGlobal.CurrentUser.Username;
        }

        private void frmReplaceDrivingLicense_Load(object sender, EventArgs e)
        {
            ucLicenseInfoWithFilter1.OnSelect += _LoadNewLicenseInfo;
        }

        private void btnReplace_Click(object sender, EventArgs e)
        {
            clsLicense newLicense = ucLicenseInfoWithFilter1.SelectedLicenseInfo.ReplaceLicense(_GetApplicationTypeId(), _license.CreatedByUserId);
            
            if (newLicense == null)
            {
                MessageBox.Show("Something Went Wrong, the License Did NOT Replaced!", "Warning");
                return;
            }

            lblReplacedLicenseId.Text = newLicense.LicenseId.ToString();
            lblReplaceLicenseApplicationId.Text = newLicense.ApplicationId.ToString();
            btnReplace.Enabled = false;
            ucLicenseInfoWithFilter1.FilterEnabled = false;
            llShowNewLicenseInfo.Enabled = true;
            MessageBox.Show("The License Replaced Successfully", "Operation Succeed");
        }

        private void llShowLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            llShowLicensesHistory.Enabled = false; // Temporal
        }

        private void llShowNewLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo licenseInfoForm = new frmShowLicenseInfo(_license.DriverInfo.PersonInfo.NationalNo, _license.LicenseClassId);
            licenseInfoForm.ShowDialog();
        }
    }
}
