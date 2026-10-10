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

namespace DrivingAndVehicleLicenseDepartment.License
{
    public partial class frmLicensesHistory : Form
    {
        private int _driverId = -1;
        private clsDriver _driver = null;
        private int _personId = -1;
        public frmLicensesHistory(int driverId)
        {
            InitializeComponent();
            _driverId = driverId;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmLicensesHistory_Load(object sender, EventArgs e)
        {
            _driver = clsDriver.FindByDriverId(_driverId);
            if (_driver == null)
            {
                MessageBox.Show("Something Went Wrong! Driver NOT Found", "Error");
                return;
            }


            ucPersonCardWithFilter1.FiltersEnabled = false;
            ucPersonCardWithFilter1.LoadPersonInfo(_driver.PersonId);
            ucDriverLicenses1.LoadDrivingLicensesHistory(_driverId);
        }
    }
}
