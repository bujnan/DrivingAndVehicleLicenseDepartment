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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;


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

                if (chbRememberMe.Checked)
                {
                    clsGlobal.RememberUsernameAndPassword(txbUsername.Text.Trim(), txbPassword.Text.Trim());
                }
                else
                {
                    clsGlobal.RememberUsernameAndPassword("", "");
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

        private void frmLogin_Load(object sender, EventArgs e)
        {
            string username = "", password = "";
            if (clsGlobal.GetStoredCredential(ref username, ref password))
            {
                txbUsername.Text = username;
                txbPassword.Text = password;
                chbRememberMe.Checked = true;
            }
            else
                chbRememberMe.Checked = false;
        }
    }
}
