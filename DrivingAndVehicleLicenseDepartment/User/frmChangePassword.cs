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

namespace DrivingAndVehicleLicenseDepartment.User
{
    public partial class frmChangePassword : Form
    {
        private int _userId = -1;
        private clsUser _user;
        public frmChangePassword(int userId)
        {
            _userId = userId;
            InitializeComponent();
        }

        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            ucUserCard1.LoadUserInfo(_userId);
            _user = clsUser.Find(_userId);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
                return;

            _user.Password = txbNewPassword.Text;
            if (_user.UpdatePassword())
                MessageBox.Show("New Password Saved Successfully");
        }

        private void txbCurrentPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txbCurrentPassword.Text))
            {
                errorProvider1.SetError(txbCurrentPassword, "this field is required!");
                e.Cancel = true;
                return;
            }

            if (_user.Password != txbCurrentPassword.Text)
            {
                errorProvider1.SetError(txbCurrentPassword, "The Current Password Is Incorrect!");
                e.Cancel = true;
                return;
            }

            errorProvider1.SetError(txbCurrentPassword, null);
        }

        private void txbNewPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txbNewPassword.Text))
            {
                errorProvider1.SetError(txbNewPassword, "this field is required!");
                e.Cancel = true;
                return;
            }

            errorProvider1.SetError(txbNewPassword, null);
        }

        private void txbConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txbConfirmPassword.Text))
            {
                errorProvider1.SetError(txbConfirmPassword, "this field is required!");
                e.Cancel = true;
                return;
            }

            if (txbConfirmPassword.Text != txbNewPassword.Text)
            {
                errorProvider1.SetError(txbConfirmPassword, "password not match!");
                e.Cancel = true;
                return;
            }

            errorProvider1.SetError(txbConfirmPassword, null);
        }
    }
}
