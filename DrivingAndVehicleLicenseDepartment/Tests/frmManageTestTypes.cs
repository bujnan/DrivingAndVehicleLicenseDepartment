using System;
using DVLD_Business;
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
    public partial class frmManageTestTypes : Form
    {
        private DataTable allTestTypesTable = new DataTable();
        public frmManageTestTypes()
        {
            InitializeComponent();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _LoadTestTypesData()
        {
            allTestTypesTable = clsTestType.GetAllTestTypes();
            if (allTestTypesTable.Rows.Count > 0)
            {
                dgvTestTypes.DataSource = allTestTypesTable;
                dgvTestTypes.ClearSelection(); // prevent any row from being selected initially

                dgvTestTypes.Columns[0].HeaderText = "Id";
                dgvTestTypes.Columns[0].Width = 8;

                dgvTestTypes.Columns[1].HeaderText = "Title";
                dgvTestTypes.Columns[1].Width = 30;

                dgvTestTypes.Columns[2].HeaderText = "Description";
                dgvTestTypes.Columns[2].Width = 90;

                dgvTestTypes.Columns[3].HeaderText = "Fees";
                dgvTestTypes.Columns[3].Width = 70;

                lblRecords.Text = allTestTypesTable.Rows.Count.ToString();
            }
        }

        private void frmManageTestTypes_Load(object sender, EventArgs e)
        {
            _LoadTestTypesData();
        }

        private void editTestTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUpdateTestType updateTestType = new frmUpdateTestType(Convert.ToInt32(dgvTestTypes.SelectedRows[0].Cells[0].Value));
            updateTestType.OnSaveFinished += _LoadTestTypesData;
            updateTestType.ShowDialog();
        }
    }
}
