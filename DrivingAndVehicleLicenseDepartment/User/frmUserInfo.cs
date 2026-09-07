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
    public partial class frmUserInfo : Form
    {

        private int _userId = -1;
        public frmUserInfo(int userId)
        {
            _userId = userId;
            InitializeComponent();
        }

        private void frmUserInfo_Load(object sender, EventArgs e)
        {
            ucUserCard1.LoadUserInfo(_userId);
        }
    }
}
