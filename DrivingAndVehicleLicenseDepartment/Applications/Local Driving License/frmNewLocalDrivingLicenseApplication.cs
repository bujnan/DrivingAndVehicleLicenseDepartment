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
using static System.Net.Mime.MediaTypeNames;

namespace DrivingAndVehicleLicenseDepartment.Applications.Local_Driving_License
{
    public partial class frmNewLocalDrivingLicenseApplication : Form
    {
        private clsPerson _selectedPerson = null;
        private clsLocalDrivingLicenseApplication _localDrivingLicenseApplication = null;
        private clsApplicationsTypes _applicationType = clsApplicationsTypes.Find((int)clsApplication.enApplicationType.NewLocalLicense);

        public frmNewLocalDrivingLicenseApplication()
        {
            InitializeComponent();
            tpApplicationInfo.Enabled = false;
            lblApplicationDate.Text = DateTime.Now.ToString("MMM dd, yyyy");
            cbLicenseClasses.SelectedIndex = 2;

            lblApplicationFees.Text = _applicationType.Fees.ToString();
            lblCreatedBy.Text = clsGlobal.CurrentUser.Username;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            _selectedPerson = ucPersonCardWithFilter1.SelectedPersonInfo;
            if (_selectedPerson == null)
            {
                MessageBox.Show("You Must Select a Person to Be Able to Apply for an Application!");
                return;
            }

            tabControl1.SelectedTab = tpApplicationInfo;
            tpApplicationInfo.Enabled = true;


        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Check if there is already active application for this license class by this personId
            int licenseClassId = clsLicenseInfo.Find(cbLicenseClasses.Text).LicenseClassId;
            int activeApplicationId = clsApplication.GetActiveApplicationIdForLicenseClass(_selectedPerson.PersonId, (int)clsApplication.enApplicationType.NewLocalLicense, licenseClassId);

            if (activeApplicationId != -1)
            {
                MessageBox.Show("You already have an Active Application for this License Class!", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Check if this personId obtained this license class before
            if (clsLicenseInfo.IsLicenseExistByPersonID(_selectedPerson.PersonId, licenseClassId))
            {
                MessageBox.Show("Person Already Has a License with the Same Applied Driving Class, Choose Diffrent Driving Class", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _localDrivingLicenseApplication = new clsLocalDrivingLicenseApplication();

            _localDrivingLicenseApplication.ApplicantPersonId = _selectedPerson.PersonId;
            _localDrivingLicenseApplication.ApplicationDate = DateTime.Now;
            _localDrivingLicenseApplication.ApplicationTypeId = _applicationType.Id;
            _localDrivingLicenseApplication.ApplicationStatus = 1; 
            _localDrivingLicenseApplication.LastStatusDate = DateTime.Now;
            _localDrivingLicenseApplication.PaidFees = _applicationType.Fees;
            _localDrivingLicenseApplication.LicenseClassId = licenseClassId;
            _localDrivingLicenseApplication.CreatedByUserId = clsGlobal.CurrentUser.UserId;

            if (_localDrivingLicenseApplication.Save())
            {
                lblApplicationId.Text = _localDrivingLicenseApplication.ApplicationId.ToString();
                MessageBox.Show("Application Added Successfully");
            }
            else
            {
                MessageBox.Show("Saving Failed!");
            }
        }
    }
}
