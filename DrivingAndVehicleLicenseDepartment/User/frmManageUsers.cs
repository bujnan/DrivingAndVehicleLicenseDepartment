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

        private clsUser _user;
        public frmManageUsers()
        {
            InitializeComponent();
        }

        private void _LoadUsersList()
        {
            _AllUsersTable = clsUser.GetAllUsers();
            dgvUsers.DataSource = _AllUsersTable;
            cbFilterBy.SelectedIndex = 0;
            cbIsActive.SelectedIndex = 0;

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

        private void frmManageUsers_Load(object sender, EventArgs e)
        {
            _LoadUsersList();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txbFilterBy.Visible = (cbFilterBy.SelectedItem != "None" && cbFilterBy.SelectedItem != "Is Active");
            cbIsActive.Visible = (cbFilterBy.SelectedItem == "Is Active");

            if (txbFilterBy.Visible)
            {
                txbFilterBy.Text = "";
                txbFilterBy.Focus();
            }

            if (!cbIsActive.Visible)
            {
                cbIsActive.SelectedIndex = 0;
            }
        }

        private void txbFilterBy_TextChanged(object sender, EventArgs e)
        {
            string selectedFilter = "";          
            switch (cbFilterBy.Text)
            {
                case "User Id":
                    selectedFilter = "UserID";
                    break;

                case "Person Id":
                    selectedFilter = "PersonID";
                    break;

                case "Username":
                    selectedFilter = "UserName";
                    break;

                case "Full Name":
                    selectedFilter = "FullName";
                    break;

                default:
                    selectedFilter = "None";
                    break;
            }

            if (selectedFilter == "None" || txbFilterBy.Text == "")
            {
                _AllUsersTable.DefaultView.RowFilter = "";
                lblTotalRecords.Text = _AllUsersTable.DefaultView.Count.ToString();
                return;
            }
            if (selectedFilter == "UserID" || selectedFilter == "PersonID")
                _AllUsersTable.DefaultView.RowFilter = string.Format("{0} = {1}", selectedFilter, txbFilterBy.Text.Trim());
            else
                _AllUsersTable.DefaultView.RowFilter = $"{selectedFilter} LIKE '{txbFilterBy.Text.Trim()}%'";

            lblTotalRecords.Text = _AllUsersTable.DefaultView.Count.ToString();
            
        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbIsActive.Text == "Yes")
                _AllUsersTable.DefaultView.RowFilter = $"IsActive = 1";
            else if (cbIsActive.Text == "No")
                _AllUsersTable.DefaultView.RowFilter = $"IsActive = 0";
            else
            {
                _AllUsersTable.DefaultView.RowFilter = "";
                lblTotalRecords.Text = _AllUsersTable.DefaultView.Count.ToString();
                return;
            }

            lblTotalRecords.Text = _AllUsersTable.DefaultView.Count.ToString();
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            frmAddUpdateUser addNewUser = new frmAddUpdateUser();
            addNewUser.OnFormClosed += _LoadUsersList;
            addNewUser.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _user = clsUser.Find(Convert.ToInt32(dgvUsers.SelectedRows[0].Cells[0].Value));
            if (_user.Delete())
            {
                _user = null;
                MessageBox.Show("User Deleted Successfully");
                _LoadUsersList();
            }
            else
            {
                MessageBox.Show("Deletion Failed!");
            }
        }

        private void addNewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateUser addNewUser = new frmAddUpdateUser();
            addNewUser.OnFormClosed += _LoadUsersList;
            addNewUser.ShowDialog();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateUser addNewUser = new frmAddUpdateUser(Convert.ToInt32(dgvUsers.SelectedRows[0].Cells[0].Value));
            addNewUser.OnFormClosed += _LoadUsersList;
            addNewUser.ShowDialog();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserInfo userInfo = new frmUserInfo(Convert.ToInt32(dgvUsers.SelectedRows[0].Cells[0].Value));
            userInfo.ShowDialog();
        }
    }
}
