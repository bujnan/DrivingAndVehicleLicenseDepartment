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

namespace DrivingAndVehicleLicenseDepartment.Tests
{
    public partial class frmScheduleTest : Form
    {
        private int _localDrivingLicenseApplicationId = -1;

        public delegate void OnSaveEventHandler();
        public event OnSaveEventHandler OnSave;
        public frmScheduleTest(int localDrivingLicenseApplicationId, int testTypeId)
        {
            InitializeComponent();
            _localDrivingLicenseApplicationId = localDrivingLicenseApplicationId;
            ucScheduleTestType1.TestType = (clsTest.enTestType) testTypeId;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ucScheduleTestType1_Load(object sender, EventArgs e)
        {
            ucScheduleTestType1.LoadInfo(_localDrivingLicenseApplicationId);
        }

        private void frmScheduleTest_FormClosed(object sender, FormClosedEventArgs e)
        {
            OnSave?.Invoke();
        }
    }
}
