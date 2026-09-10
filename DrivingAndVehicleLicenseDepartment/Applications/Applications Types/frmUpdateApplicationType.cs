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

namespace DrivingAndVehicleLicenseDepartment.Applications.Applications_Types
{
    public partial class frmUpdateApplicationType : Form
    {
        private int _applicationTypeId = -1;
        private clsApplicationsTypes _applicationType;
        public delegate void OnSaveFinishedEventHandler();
        public event OnSaveFinishedEventHandler OnSaveFinished;

        public frmUpdateApplicationType(int applicationTypeId)
        {
            _applicationTypeId = applicationTypeId;
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmUpdateApplicationType_Load(object sender, EventArgs e)
        {
            _applicationType = clsApplicationsTypes.Find(_applicationTypeId);
            if (_applicationType != null)
            {
                lblId.Text = _applicationType.Id.ToString();
                txbTitle.Text = _applicationType.Title;
                txbFees.Text = _applicationType.Fees.ToString();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _applicationType.Title = txbTitle.Text;
            _applicationType.Fees = Convert.ToDouble(txbFees.Text);

            if (_applicationType.Save())
            {
                MessageBox.Show("Updated Successfully");
                OnSaveFinished.Invoke();
            }    
            else
                MessageBox.Show("Updated Successfully");
        }
    }
}
