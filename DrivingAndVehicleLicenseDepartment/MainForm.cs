using System;
using DrivingAndVehicleLicenseDepartment.User;
using DrivingAndVehicleLicenseDepartment.Login;
using DrivingAndVehicleLicenseDepartment.Applications.Applications_Types;
using System.Windows.Forms;
using DrivingAndVehicleLicenseDepartment.Global;

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
    }
}
