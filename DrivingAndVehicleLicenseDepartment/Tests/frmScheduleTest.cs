using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrivingAndVehicleLicenseDepartment.Tests
{
    public partial class frmScheduleTest : Form
    {
        private int _localDrivingLicenseApplicationId = -1;
        public frmScheduleTest(int localDrivingLicenseApplicationId)
        {
            _localDrivingLicenseApplicationId = localDrivingLicenseApplicationId;
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ucScheduleTestType1_Load(object sender, EventArgs e)
        {
            ucScheduleTestType1.LoadInfo(_localDrivingLicenseApplicationId);
        }
    }
}
