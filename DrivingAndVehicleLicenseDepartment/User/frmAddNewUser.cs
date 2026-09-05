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
    public partial class frmAddNewUser : Form
    {

        public delegate void CloseFormEventHandler();
        public event CloseFormEventHandler OnFormClosed;

        private clsUser _user;
        public frmAddNewUser()
        {
            InitializeComponent();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (ucPersonCardWithFilter1.SelectedPersonInfo == null)
            {
                MessageBox.Show("You Have to Select Or Add a Person!");
                return;
            }

            if ((_user = clsUser.Find(ucPersonCardWithFilter1.SelectedPersonInfo.PersonId)) != null)
            {
                MessageBox.Show("Selected Person Already has a User!");

                lblUserId.Text = _user.UserId.ToString();
                txbUsername.Text = _user.Username;
                txbPassword.Text = _user.Password;
                txbConfirmPassword.Text = _user.Password;
                chbIsActive.Checked = _user.IsActive;

                tpLoginInfo.Enabled = false;
                tabControl1.SelectedTab = tpLoginInfo;
                btnSave.Enabled = false;
            }
            else
            {
                tabControl1.SelectedTab = tpLoginInfo;

                lblUserId.Text = "???";
                txbUsername.Text = "";
                txbPassword.Text = "";
                txbConfirmPassword.Text = "";
                chbIsActive.Checked = false;

                tpLoginInfo.Enabled = true;
                btnSave.Enabled = true;
            }  
        }

        private void _SetUserInfo()
        {
            if (ucPersonCardWithFilter1.SelectedPersonInfo != null)
            {
                _user = new clsUser();
                _user.PersonId = ucPersonCardWithFilter1.PersonId;
                _user.Username = txbUsername.Text.Trim();
                _user.Password = txbPassword.Text.Trim();

                _user.IsActive = chbIsActive.Checked;
            }
            else
            {
                MessageBox.Show("No Person Is Selected!");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
           
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some Fields are NOT valid!");
                return;
            }

            _SetUserInfo();
            if (_user != null)
            {
                if (_user.Save())
                {
                    lblUserId.Text = _user.UserId.ToString();
                    MessageBox.Show("User Saved Successfully");
                }
                else
                {
                    MessageBox.Show("Operation Failed! User NOT Saved");
                }
            }
        }

        private void txbUsername_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txbUsername.Text))
            {
                errorProvider1.SetError(txbUsername, "this field can't be empty");
                e.Cancel = true;
                return;
            }

            if (clsUser.IsExist(txbUsername.Text))
            {
                errorProvider1.SetError(txbUsername, "this Username already Exist!");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txbUsername, null);
            }
        }

        private void txbPassword_Validating(object sender, CancelEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txbPassword.Text))
            {
                errorProvider1.SetError(txbPassword, "this field can't be empty");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txbPassword, null);
            }
        }

        private void txbConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txbConfirmPassword.Text))
            {
                errorProvider1.SetError(txbConfirmPassword, "this field can't be empty");
                e.Cancel = true;
                return;
            }
            
            if (txbPassword.Text != txbConfirmPassword.Text && !string.IsNullOrWhiteSpace(txbPassword.Text))
            {
                errorProvider1.SetError(txbConfirmPassword, "password Does NOT Match!");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txbConfirmPassword, null);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAddNewUser_FormClosed(object sender, FormClosedEventArgs e)
        {
            OnFormClosed.Invoke();
        }
    }
}
