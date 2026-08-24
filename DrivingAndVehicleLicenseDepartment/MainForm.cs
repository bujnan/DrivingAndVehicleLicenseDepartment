using System;
using DrivingAndVehicleLicenseDepartment.User;
using System.Windows.Forms;

namespace DrivingAndVehicleLicenseDepartment
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
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
    }
}
