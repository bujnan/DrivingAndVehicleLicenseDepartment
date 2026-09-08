using DVLD_Business;
using DrivingAndVehicleLicenseDepartment.Global;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace DrivingAndVehicleLicenseDepartment.Login
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            clsUser user = clsUser.FindByUsernameAndPassword(txbUsername.Text.Trim(), txbPassword.Text.Trim());
            if (user != null)
            {
                if (!user.IsActive)
                {
                    MessageBox.Show("User Is NOT Active! Please Contact Admin");
                    return;
                }

                clsGlobal.CurrentUser = user;
                this.Hide();
                MainForm mainForm = new MainForm(this);
                mainForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Invalid Username Or Password!");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
