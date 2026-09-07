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

namespace DrivingAndVehicleLicenseDepartment.User.Control
{
    public partial class ucUserCard : UserControl
    {
        private clsUser _user;
        public ucUserCard()
        {
            InitializeComponent();
        }
        
        public void LoadUserInfo(int userId)
        {
            _user = clsUser.Find(userId);
            if (_user != null)
            {
                ucPersonCard1.LoadPersonInfo(_user.PersonId);
                lblUserId.Text = _user.UserId.ToString();
                lblUsername.Text = _user.Username;
                lblIsActive.Text = _user.IsActive ? "Yes" : "No";
            }
        }
    }
}
