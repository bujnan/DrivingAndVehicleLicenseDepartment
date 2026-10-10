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
    public partial class frmRenewDrivingLicense : Form
    {
        private clsLicense _license = null;
        public frmRenewDrivingLicense()
        {
            InitializeComponent();
        }

        private void _LoadNewLicenseInfo()
        {
            _license = ucLicenseInfoWithFilter1.SelectedLicenseInfo;
            if (_license.ExpirationDate > DateTime.Now)
            {
                MessageBox.Show($"You Can NOT Renew Your License, Because It Expires On: {_license.ExpirationDate.ToString("MMM dd, yyyy")}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblApplicationDate.Text = DateTime.Now.ToString("MMM dd, yyyy");
            lblIssueDate.Text = DateTime.Now.ToString("MMM dd, yyyy");
            lblExpirationDate.Text = DateTime.Now.AddYears(clsLicenseClassInfo.Find(_license.LicenseClassId).DefaultValidityLength).ToString("MMM dd, yyyy");
            lblApplicationFees.Text = clsApplicationsTypes.Find((int)clsApplicationsTypes.enApplicationType.Renew).Fees.ToString();
            lblLicenseFees.Text = clsLicenseClassInfo.Find(_license.LicenseClassId).LicenseClassFees.ToString();
            lblOldLicenseId.Text = _license.LicenseId.ToString();
            lblCreatedBy.Text = clsGlobal.CurrentUser.Username;
            lblTotalFees.Text = (Convert.ToDouble(lblApplicationFees.Text) + Convert.ToDouble(lblLicenseFees.Text)).ToString();
        }

        private void frmRenewDrivingLicense_Load(object sender, EventArgs e)
        {
            ucLicenseInfoWithFilter1.OnSelect += _LoadNewLicenseInfo;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRenew_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to Renew the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            clsLicense newLicense = ucLicenseInfoWithFilter1.SelectedLicenseInfo.RenewLicense(txbNotes.Text.Trim(), clsGlobal.CurrentUser.UserId);

            if (newLicense == null)
            {
                MessageBox.Show("Something Went Wrong! License Did NOT Renew.", "Warnning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            lblRenewedLicenseId.Text = newLicense.LicenseId.ToString();
            lblRenewLicenseApplicationId.Text = newLicense.ApplicationId.ToString();
            llShowNewLicenseInfo.Enabled = true;
            llShowLicensesHistory.Enabled = true;
            btnRenew.Enabled = false;
            ucLicenseInfoWithFilter1.FilterEnabled = false;
            _license = newLicense;
            MessageBox.Show("Driving License Renewed Successfully", "Operation Succeed");
        }

        private void llShowNewLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            llShowNewLicenseInfo.Enabled = (_license != null);
            frmShowLicenseInfo licenseInfoForm = new frmShowLicenseInfo(_license.DriverInfo.PersonInfo.NationalNo, _license.LicenseClassId);
            licenseInfoForm.ShowDialog();
        }

        private void llShowLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            llShowLicensesHistory.Enabled = (_license != null);
            frmLicensesHistory licensesHistoryForm = new frmLicensesHistory(_license.DriverId);
            licensesHistoryForm.ShowDialog();
        }
    }
}
