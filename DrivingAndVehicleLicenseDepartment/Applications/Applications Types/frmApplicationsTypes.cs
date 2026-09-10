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

namespace DrivingAndVehicleLicenseDepartment.Applications.Applications_Types
{
    public partial class frmApplicationsTypes : Form
    {
        DataTable applicationsTypesTable;
        public frmApplicationsTypes()
        {
            InitializeComponent();
        }

        private void _LoadData()
        {
            applicationsTypesTable = clsApplicationsTypes.GetAllApplicationsTypes();

            if (applicationsTypesTable.Rows.Count > 0)
            {
                dgvApplicationTypes.DataSource = applicationsTypesTable;
                dgvApplicationTypes.ClearSelection(); // prevent any row from being selected initially

                dgvApplicationTypes.Columns[0].HeaderText = "Id";
                dgvApplicationTypes.Columns[0].Width = 10;

                dgvApplicationTypes.Columns[1].HeaderText = "Title";
                dgvApplicationTypes.Columns[1].Width = 110;

                dgvApplicationTypes.Columns[2].HeaderText = "Fees";
                dgvApplicationTypes.Columns[2].Width = 90;
            }

            lblRecords.Text = applicationsTypesTable.Rows.Count.ToString();
        }


        private void frmApplicationsTypes_Load(object sender, EventArgs e)
        {
            _LoadData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _RefreshApplicationTypesList()
        {
            _LoadData();
        }
        private void editAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUpdateApplicationType updateApplicationType = new frmUpdateApplicationType(Convert.ToInt32(dgvApplicationTypes.SelectedRows[0].Cells[0].Value));

            updateApplicationType.OnSaveFinished += _RefreshApplicationTypesList;
            updateApplicationType.ShowDialog();
        }
    }
}
