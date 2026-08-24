using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_Business;

namespace DrivingAndVehicleLicenseDepartment.User
{
    public partial class frmManageUsers : Form
    {
        DataTable _AllUsersTable;
        public frmManageUsers()
        {
            InitializeComponent();
        }

        private void frmManageUsers_Load(object sender, EventArgs e)
        {
            _AllUsersTable = clsUser.GetAllUsers();
            dgvUsers.DataSource = _AllUsersTable;

            if (_AllUsersTable.Rows.Count > 0)
            {
                dgvUsers.Columns[0].HeaderText = "User Id";
                dgvUsers.Columns[0].Width = 100;

                dgvUsers.Columns[1].HeaderText = "Person Id";
                dgvUsers.Columns[1].Width = 100;

                dgvUsers.Columns[2].HeaderText = "Full Name";
                dgvUsers.Columns[2].Width = 180;

                dgvUsers.Columns[3].HeaderText = "Username";
                dgvUsers.Columns[3].Width = 140;

                dgvUsers.Columns[4].HeaderText = "Is Active";
                dgvUsers.Columns[4].Width = 100;

                lblTotalRecords.Text = _AllUsersTable.Rows.Count.ToString();
            }

        }
    }
}
