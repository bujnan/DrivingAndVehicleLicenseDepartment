using System;
using DrivingAndVehicleLicenseDepartment.User;
using DrivingAndVehicleLicenseDepartment.Login;
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
    }
}
