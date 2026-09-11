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
    public partial class frmUpdateTestType : Form
    {
        private int _testTypeId = -1;
        private clsTestType _testType;
        public delegate void OnSaveEventHandler();
        public event OnSaveEventHandler OnSaveFinished;
        public frmUpdateTestType(int testTypeId)
        {
            _testTypeId = testTypeId;
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _testType.Title = txbTitle.Text;
            _testType.Description = txbDescription.Text;
            _testType.Fees = Convert.ToDouble(txbFees.Text);

            if (_testType.Save())
            {
                OnSaveFinished.Invoke();
                MessageBox.Show("Saved Successfully");
            }
            else
                MessageBox.Show("Failed to Save!");
        }

        private void frmUpdateTestType_Load(object sender, EventArgs e)
        {
            _testType = clsTestType.Find(_testTypeId);

            if (_testType != null)
            {
                lblId.Text = _testType.Id.ToString();
                txbTitle.Text = _testType.Title;
                txbDescription.Text = _testType.Description;
                txbFees.Text = _testType.Fees.ToString();
            }
        }
    }
}
