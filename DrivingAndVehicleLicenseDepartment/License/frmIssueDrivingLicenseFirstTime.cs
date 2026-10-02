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
    public partial class frmIssueDrivingLicenseFirstTime : Form
    {
        private int _localDrivingLicenseApplicationId = -1;
        private clsLocalDrivingLicenseApplication _localDrivingLicenseApplication = null;
        public frmIssueDrivingLicenseFirstTime(int localDrivingLicenseApplication)
        {
            InitializeComponent();
            _localDrivingLicenseApplicationId = localDrivingLicenseApplication;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmIssueDrivingLicenseFirstTime_Load(object sender, EventArgs e)
        {
            _localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivinLicenseApplicationId(_localDrivingLicenseApplicationId);

            if (_localDrivingLicenseApplication != null)
            {
                ucDrivingLicenseInfo1.LoadApplicationInfo(_localDrivingLicenseApplicationId);
            }
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            if (_localDrivingLicenseApplication.GeTotalPassedTests() != 3)
            {
                MessageBox.Show("You Must Pass All Test!", "Error");
                return;
            }

            int licenseId = _localDrivingLicenseApplication.IssueDrivingLicenseForFirstTime(txbNotes.Text.Trim(), clsGlobal.CurrentUser.UserId);
            if (licenseId != -1)
                MessageBox.Show("License Issued Successfully!", "Operation Succeed");
            else
                MessageBox.Show("License NOT Issued!", "Operation Faild");
        }
    }
}
