using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrivingAndVehicleLicenseDepartment.Applications.Manage_Applications.Local_Driving_License_Application
{
    public partial class frmListLocalDrivingApplications : Form
    {
        public frmListLocalDrivingApplications()
        {
            InitializeComponent();
        }

        private void frmListLocalDrivingApplications_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0;
            
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txbFilterBy.Visible = (cbFilterBy.SelectedIndex != 0);
        }
    }
}
