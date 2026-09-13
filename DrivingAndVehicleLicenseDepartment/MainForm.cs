using DrivingAndVehicleLicenseDepartment.Applications.Applications_Types;
using DrivingAndVehicleLicenseDepartment.Applications.Local_Driving_License;
using DrivingAndVehicleLicenseDepartment.Global;
using DrivingAndVehicleLicenseDepartment.Login;
using DrivingAndVehicleLicenseDepartment.Tests;
using DrivingAndVehicleLicenseDepartment.User;
using System;
using System.Windows.Forms;

namespace DrivingAndVehicleLicenseDepartment
{
    public partial class MainForm : Form
    {
        private frmLogin _login; 
        public MainForm(frmLogin loginForm)
        {
            _login = loginForm;
            InitializeComponent();
        }

        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManagePeople managePeople = new frmManagePeople();
            managePeople.ShowDialog();
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageUsers manageUsers = new frmManageUsers();
            manageUsers.ShowDialog();
        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _login.Show();
            this.Close();
        }

        private void currentUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserInfo userInfoForm = new frmUserInfo(clsGlobal.CurrentUser.UserId);
            userInfoForm.ShowDialog();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePassword changePasswordForm = new frmChangePassword(clsGlobal.CurrentUser.UserId);
            changePasswordForm.ShowDialog();
         }

        private void manageApplicationsTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmApplicationsTypes applicationsTypeForm = new frmApplicationsTypes();
            applicationsTypeForm.ShowDialog();
        }

        private void manageTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageTestTypes manageTestTypes = new frmManageTestTypes();
            manageTestTypes.ShowDialog();
        }

        private void tToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNewLocalDrivingLicenseApplication newLocalDrivingLicenseApplication = new frmNewLocalDrivingLicenseApplication();
            newLocalDrivingLicenseApplication.ShowDialog();
        }
    }
}
