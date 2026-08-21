using DVLD_Business;
using System;
using System.Windows.Forms;
using System.Data;

namespace DrivingAndVehicleLicenseDepartment
{
    public partial class frmManagePeople : Form
    {
        private static DataTable _peopleTableAllColumns = clsPerson.GetAllPeople();

        // only select the columns that you want to show in the grid
        private DataTable _peopleTableSelectedColumns = _peopleTableAllColumns.DefaultView.ToTable(false, "PersonID", "NationalNo",
                                                       "FirstName", "SecondName", "ThirdName", "LastName",
                                                       "GendorCaption", "DateOfBirth", "CountryName",
                                                       "Phone", "Email");

        public frmManagePeople()
        {
            InitializeComponent();
        }

        private void frmManagePeople_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedItem = "None";
            dgvManagePeople.DataSource = _peopleTableSelectedColumns;
            lblTotalRecords.Text = _peopleTableSelectedColumns.DefaultView.Count.ToString();
            if (dgvManagePeople.Rows.Count > 0)
            {
                dgvManagePeople.Columns[0].HeaderText = "Person ID";
                dgvManagePeople.Columns[0].Width = 110;

                dgvManagePeople.Columns[1].HeaderText = "National No";
                dgvManagePeople.Columns[1].Width = 120;

                dgvManagePeople.Columns[2].HeaderText = "First Name";
                dgvManagePeople.Columns[2].Width = 120;

                dgvManagePeople.Columns[3].HeaderText = "Second Name";
                dgvManagePeople.Columns[3].Width = 140;

                dgvManagePeople.Columns[4].HeaderText = "Third Name";
                dgvManagePeople.Columns[4].Width = 120;

                dgvManagePeople.Columns[5].HeaderText = "Last Name";
                dgvManagePeople.Columns[5].Width = 120;

                dgvManagePeople.Columns[6].HeaderText = "Gender";
                dgvManagePeople.Columns[6].Width = 120;

                dgvManagePeople.Columns[7].HeaderText = "Date of Birth";
                dgvManagePeople.Columns[7].Width = 140;

                dgvManagePeople.Columns[8].HeaderText = "Nationality";
                dgvManagePeople.Columns[8].Width = 120;

                dgvManagePeople.Columns[9].HeaderText = "Phone";
                dgvManagePeople.Columns[9].Width = 120;

                dgvManagePeople.Columns[10].HeaderText = "Email";
                dgvManagePeople.Columns[10].Width = 170;
            }
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txbFilterBy.Visible = (cbFilterBy.SelectedItem.ToString() != "None");
            if (txbFilterBy.Visible)
            {
                txbFilterBy.Text = "";
                txbFilterBy.Focus();
            }
        }

        private void txbFilterBy_TextChanged(object sender, EventArgs e)
        {
            string selectedColumnName = "";
            switch (cbFilterBy.Text)
            {
                case "Person ID":
                    selectedColumnName = "PersonID";
                    break;

                case "National No":
                    selectedColumnName = "NationalNo";
                    break;

                case "First Name":
                    selectedColumnName = "FirstName";
                    break;

                case "Second Name":
                    selectedColumnName = "SecondName";
                    break;

                case "Third Name":
                    selectedColumnName = "ThirdName";
                    break;

                case "Last Name":
                    selectedColumnName = "LastName";
                    break;

                case "Gender":
                    selectedColumnName = "GendorCaption";
                    break;

                case "Date of Birth":
                    selectedColumnName = "DateOfBirth";
                    break;

                case "Nationality":
                    selectedColumnName = "CountryName";
                    break;

                case "Phone":
                    selectedColumnName = "Phone";
                    break;

                case "Email":
                    selectedColumnName = "Email";
                    break;

                default:
                    selectedColumnName = "None";
                    break;
            }

            //Reset the filters in case nothing selected or filter value conains nothing.
            if (txbFilterBy.Text == "" || selectedColumnName == "None")
            {
                _peopleTableSelectedColumns.DefaultView.RowFilter = "";
                lblTotalRecords.Text = _peopleTableSelectedColumns.DefaultView.Count.ToString();
                return;
            }

            if (selectedColumnName == "PersonID")
            {
                _peopleTableSelectedColumns.DefaultView.RowFilter = string.Format("{0} = {1}", selectedColumnName, txbFilterBy.Text.Trim());
            }
            else
            {
                _peopleTableSelectedColumns.DefaultView.RowFilter = $"{selectedColumnName} LIKE '{txbFilterBy.Text.Trim()}%'";
            }
            lblTotalRecords.Text = _peopleTableSelectedColumns.DefaultView.Count.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txbFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            //only allow number in case Person ID is selected.
            if (cbFilterBy.Text == "Person ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void _ShowAddEditFormAndSubscribeItsCloseEvent()
        {
            frmAddEditPerson addEditPerson = new frmAddEditPerson();
            addEditPerson.formClosed += RefrechPeopleList;
            addEditPerson.ShowDialog();
        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            _ShowAddEditFormAndSubscribeItsCloseEvent();
        }

        private void RefrechPeopleList()
        {
            _peopleTableAllColumns = clsPerson.GetAllPeople();
            _peopleTableSelectedColumns = _peopleTableAllColumns.DefaultView.ToTable(false, "PersonID", "NationalNo",
                                                       "FirstName", "SecondName", "ThirdName", "LastName",
                                                       "GendorCaption", "DateOfBirth", "CountryName",
                                                       "Phone", "Email");

            dgvManagePeople.DataSource = _peopleTableSelectedColumns;
            lblTotalRecords.Text = _peopleTableSelectedColumns.DefaultView.Count.ToString();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsPerson person = clsPerson.Find(Convert.ToInt32(dgvManagePeople.SelectedRows[0].Cells[0].Value));
            if (person.Delete())
            {
                MessageBox.Show("Deleted Successfully");
                RefrechPeopleList();
            }
            else
                MessageBox.Show("Deleted Failed");
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ShowAddEditFormAndSubscribeItsCloseEvent();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int personId = Convert.ToInt32(dgvManagePeople.SelectedRows[0].Cells[0].Value);
            frmAddEditPerson addEditPerson = new frmAddEditPerson(personId);
            addEditPerson.formClosed += RefrechPeopleList;
            addEditPerson.ShowDialog();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPersonDetails personDetail = new frmPersonDetails();
            personDetail.ShowDialog();
        }
    }
}
