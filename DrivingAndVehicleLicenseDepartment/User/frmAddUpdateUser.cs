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
    public partial class frmAddUpdateUser : Form
    {
        private clsUser _user;

        private int _userId = -1;
        public enum enMode { AddNew = 0, Update = 1};
        private enMode _Mode;
        public delegate void CloseFormEventHandler();
        public event CloseFormEventHandler OnFormClosed;

        public frmAddUpdateUser()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }

        public frmAddUpdateUser(int userId)
        {
            InitializeComponent();
            _userId = userId;
            _Mode = enMode.Update;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (ucPersonCardWithFilter1.SelectedPersonInfo == null)
            {
                MessageBox.Show("You Have to Select Or Add a Person!");
                return;
            }

            if ((_user = clsUser.FindUserByPersonId(ucPersonCardWithFilter1.SelectedPersonInfo.PersonId)) != null)
            {
                if (_Mode == enMode.AddNew)
                {
                    MessageBox.Show("Selected Person Already has a User!");
                    tpLoginInfo.Enabled = false;
                }

                lblUserId.Text = _user.UserId.ToString();
                txbUsername.Text = _user.Username;
                txbPassword.Text = _user.Password;
                txbConfirmPassword.Text = _user.Password;
                chbIsActive.Checked = _user.IsActive;

                tabControl1.SelectedTab = tpLoginInfo;
            }
            else
            {
                _user = new clsUser();
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
           
            if (_user.Save())
            {
                lblUserId.Text = _user.UserId.ToString();
                this.Text = "Update User";
                lblFormTitle.Text = "Update User";
                _Mode = enMode.Update;
                MessageBox.Show("User Saved Successfully");
            }
            else
            {
                MessageBox.Show("Operation Failed! User NOT Saved");
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
                if (_user.Username == txbUsername.Text)
                {
                    errorProvider1.SetError(txbUsername, null);
                }
                else
                {
                    errorProvider1.SetError(txbUsername, "this Username already Exist!");
                    e.Cancel = true;
                }
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

        private void _ResetDefaultValues()
        {
            if(_Mode == enMode.AddNew)
            {
                _user = new clsUser();
                this.Text = "Add New User";
                lblFormTitle.Text = "Add New User";
                tpLoginInfo.Enabled = false;
            }
            else
            {
                this.Text = "Update User";
                lblFormTitle.Text = "Update User";
                tpLoginInfo.Enabled = true;
                ucPersonCardWithFilter1.FiltersEnabled = false;
            }

            txbUsername.Text = "";
            txbPassword.Text = "";
            txbConfirmPassword.Text = "";
            chbIsActive.Checked = false;
        }

        private void _LoadData()
        {
            if ((_user = clsUser.Find(_userId)) != null)
            {
                ucPersonCardWithFilter1.FiltersEnabled = false;
                ucPersonCardWithFilter1.LoadPersonInfo(_user.PersonId);

                lblUserId.Text = _user.UserId.ToString();
                txbUsername.Text = _user.Username;
                txbPassword.Text = _user.Password;
                txbConfirmPassword.Text = _user.Password;
                chbIsActive.Checked = _user.IsActive;

                tpLoginInfo.Enabled = true;
                btnSave.Enabled = true;
            }
        }
        private void frmAddUpdateUser_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();
            if (_Mode == enMode.Update)
                _LoadData();
        }
    }
}
