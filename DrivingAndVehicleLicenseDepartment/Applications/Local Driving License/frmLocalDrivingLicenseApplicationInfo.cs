using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrivingAndVehicleLicenseDepartment.Applications.Local_Driving_License
{
    public partial class frmLocalDrivingLicenseApplicationInfo : Form
    {
        private int _localDrivingLicenseApplicationId = -1;
        public frmLocalDrivingLicenseApplicationInfo(int localDrivingLicenseApplicationId)
        {
            _localDrivingLicenseApplicationId = localDrivingLicenseApplicationId;
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LocalDrivingLicenseApplicationInfo_Load(object sender, EventArgs e)
        {
            ucDrivingLicenseInfo1.LoadApplicationInfo(_localDrivingLicenseApplicationId);
        }
    }
}
