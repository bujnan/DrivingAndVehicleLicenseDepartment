using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrivingAndVehicleLicenseDepartment.Applications.Applications_Types
{
    public partial class frmUpdateApplicationType : Form
    {
        private int _applicationTypeId = -1;
        public frmUpdateApplicationType(int applicationTypeId)
        {
            _applicationTypeId = applicationTypeId;
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
