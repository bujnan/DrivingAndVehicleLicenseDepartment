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

        private clsUser _user;
        public frmAddNewUser()
        {
            InitializeComponent();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tpLoginInfo;
        }

        private void _SetUserInfo()
        {
            _user = new clsUser();
            _user.PersonId = ucPersonCardWithFilter1.PersonId;
            _user.Username = txbUsername.Text.Trim();

            // this logic should be inside error provider 
            if (txbPassword.Text == txbConfirmPassword.Text)
                _user.Password = txbPassword.Text.Trim();

            _user.IsActive = chbIsActive.Checked;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _SetUserInfo();
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
}
