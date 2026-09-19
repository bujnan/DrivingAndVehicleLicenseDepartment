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
    public partial class frmAddUpdateLocalDrivingLicenseApplication : Form
    {
        private int _personId = -1;
        private clsPerson _selectedPerson = null;
        private clsLocalDrivingLicenseApplication _localDrivingLicenseApplication = null;
        private clsApplicationsTypes _applicationType = clsApplicationsTypes.Find((int)clsApplication.enApplicationType.NewLocalLicense);
        public delegate void OnSaveEventHandler();
        public event OnSaveEventHandler OnSave;
        private enum enMode { AddNew = 0, Update = 2};
        private enMode _mode = enMode.AddNew;

        public frmAddUpdateLocalDrivingLicenseApplication()
        {
            _mode = enMode.AddNew;
            InitializeComponent();
            //tpApplicationInfo.Enabled = false;
            //lblApplicationDate.Text = DateTime.Now.ToString("MMM dd, yyyy");
            //cbLicenseClasses.SelectedIndex = 2;

            //lblApplicationFees.Text = _applicationType.Fees.ToString();
            //lblCreatedBy.Text = clsGlobal.CurrentUser.Username;
        }

        public frmAddUpdateLocalDrivingLicenseApplication(int localDrivingLicenseId)
        {
            _localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivinLicenseApplicationId(localDrivingLicenseId);
            _personId = _localDrivingLicenseApplication.ApplicantPersonId;
            _mode = enMode.Update;
            InitializeComponent();
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
            if (_selectedPerson == null)
                return;

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

            if (_mode == enMode.AddNew)
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
                lblApplicationId.Text = _localDrivingLicenseApplication.LocalDrivingLicenseApplicationId.ToString();
                OnSave?.Invoke();
                MessageBox.Show("Application Added Successfully");
            }
            else
            {
                MessageBox.Show("Saving Failed!");
            }
        }

        private  void _ResetDefaultValues()
        {
            this.Text = "New Local Driving License Application";
            lblFormTitle.Text = "New Local Driving License Application";
            lblApplicationId.Text = "???";
            lblApplicationDate.Text = DateTime.Now.ToString("MMM dd, yyyy");
            cbLicenseClasses.SelectedIndex = 2;
            lblApplicationFees.Text = _applicationType.Fees.ToString(); ;
            lblCreatedBy.Text = clsGlobal.CurrentUser.Username;

            tpApplicationInfo.Enabled = false;
        }

        private void _LoadData()
        {
            this.Text = "Update Local Driving License Application";
            lblFormTitle.Text = "Update Local Driving License Application";
            _selectedPerson = clsPerson.Find(_personId);
            ucPersonCardWithFilter1.LoadPersonInfo(_personId);
            ucPersonCardWithFilter1.FiltersEnabled = false;

            //_localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindLocalDrivingLicenseApplicationByApplicantPersonId(_personId);

            lblApplicationId.Text = _localDrivingLicenseApplication.LocalDrivingLicenseApplicationId.ToString();
            lblApplicationDate.Text = _localDrivingLicenseApplication.ApplicationDate.ToString("MMM dd, yyyy");
            cbLicenseClasses.SelectedIndex = _localDrivingLicenseApplication.LicenseClassId - 1; // need fix: find the name
            lblApplicationFees.Text = _localDrivingLicenseApplication.PaidFees.ToString();
            lblCreatedBy.Text = clsGlobal.CurrentUser.Username;
            
            tpApplicationInfo.Enabled = true; ;
        }

        private void frmAddUpdateLocalDrivingLicenseApplication_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();
            if (_mode == enMode.Update)
                _LoadData();
        }
    }
}
