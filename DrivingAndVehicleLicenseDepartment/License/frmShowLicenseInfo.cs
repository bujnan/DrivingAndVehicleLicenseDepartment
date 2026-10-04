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
    public partial class frmShowLicenseInfo : Form
    {
        private string _nationalNo = "";
        private int _licenseClassId = -1;
        public frmShowLicenseInfo(string nationalNo, int licenseClassId)
        {
            InitializeComponent();
            _nationalNo = nationalNo;
            _licenseClassId = licenseClassId;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmShowLicenseInfo_Load(object sender, EventArgs e)
        {
            ucLicenseInfo1.LoadInfo(_nationalNo, _licenseClassId); 
        }
    }
}
