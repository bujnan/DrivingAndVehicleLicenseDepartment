using DrivingAndVehicleLicenseDepartment.Global;
using DrivingAndVehicleLicenseDepartment.License;
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

namespace DrivingAndVehicleLicenseDepartment.Applications.Controls
{
    public partial class ucDrivingLicenseInfo : UserControl
    {
        private int _localDrivingLicenseApplicationId = -1;
        private clsLocalDrivingLicenseApplication _localDrivingLicenseApplication = null;
        private clsPerson _person = null;
        public int LocalDrivingLicenseApplicationId
        {
            // set { _localDrivingLicenseApplicationId = value; }
            get { return _localDrivingLicenseApplicationId; }
        }
        public ucDrivingLicenseInfo()
        {
            InitializeComponent();
        }

        private void _ResetDefaultValue()
        {
            lblDrivingLocalApplicationId.Text = "???";
            lblAppliedForLicense.Text = "???";
            lblPassedTests.Text = "???";
            lblApplicationId.Text = "???";
            lblApplicationFees.Text = "???";
            lblApplicationType.Text = "???";
            lblApplicantFullName.Text = "???";
            lblApplicationDate.Text = "???";
            lblApplicationStatusDate.Text = "???";
            lblCreatedByUser.Text = "???";
            lblApplicationStatus.Text = "???";
        }
        public void LoadApplicationInfo(int localDrivingLicenseApplicationId)
        {
            _localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivinLicenseApplicationId(localDrivingLicenseApplicationId);

            if (_localDrivingLicenseApplication != null)
            {
                _person = clsPerson.Find(_localDrivingLicenseApplication.ApplicantPersonId);

                lblDrivingLocalApplicationId.Text = _localDrivingLicenseApplication.LocalDrivingLicenseApplicationId.ToString();
                lblAppliedForLicense.Text = clsLicenseClassInfo.Find(_localDrivingLicenseApplication.LicenseClassId).LicenseClassName;
                lblPassedTests.Text = _localDrivingLicenseApplication.GeTotalPassedTests().ToString() + "/3";
                lblApplicationId.Text = _localDrivingLicenseApplication.ApplicationId.ToString();
                lblApplicationFees.Text = _localDrivingLicenseApplication.PaidFees.ToString();
                lblApplicationType.Text = clsApplicationsTypes.Find(_localDrivingLicenseApplication.ApplicationTypeId).Title;
                lblApplicantFullName.Text = _person.FullName;
                lblApplicationDate.Text = _localDrivingLicenseApplication.ApplicationDate.ToString("MMM dd, yyyy");
                lblApplicationStatusDate.Text = _localDrivingLicenseApplication.LastStatusDate.ToString("MMM dd, yyyy");
                lblCreatedByUser.Text = clsGlobal.CurrentUser.Username;

                switch (_localDrivingLicenseApplication.ApplicationStatus)
                {
                    case (int)clsApplication.enApplicationStatus.New:
                        lblApplicationStatus.Text = "New";
                        break;

                    case (int)clsApplication.enApplicationStatus.Canceled:
                        lblApplicationStatus.Text = "Canceled";
                        break;

                    case (int)clsApplication.enApplicationStatus.Completed:
                        lblApplicationStatus.Text = "Completed";
                        break;
                }

                llShowLicenseInfo.Enabled = _localDrivingLicenseApplication.HasObtainedLicenseClass();
            }
            else
                _ResetDefaultValue();
        }

        private void llViewPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_person != null)
            {
                frmPersonDetails personDetailsForm = new frmPersonDetails(_person.PersonId);
                personDetailsForm.ShowDialog();
            }
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo showLicenseInfoForm = new frmShowLicenseInfo(_person.NationalNo, _localDrivingLicenseApplication.LicenseClassId);

            showLicenseInfoForm.ShowDialog();
        }
    }
}
