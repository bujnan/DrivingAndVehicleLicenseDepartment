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

namespace DrivingAndVehicleLicenseDepartment
{
    public partial class frmPersonDetails : Form
    {

        public delegate void FormClosedEventHandler();

        public event FormClosedEventHandler formClosed;

        public frmPersonDetails(int personId)
        {
            InitializeComponent();
            ucPersonCard1.LoadPersonInfo(personId);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmPersonDetails_FormClosed(object sender, FormClosedEventArgs e)
        {
            formClosed?.Invoke();
        }
    }
}
