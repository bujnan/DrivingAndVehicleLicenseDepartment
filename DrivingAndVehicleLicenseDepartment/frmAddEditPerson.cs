using DrivingAndVehicleLicenseDepartment.Properties;
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
    public partial class frmAddEditPerson : Form
    {
        public enum enMode { AddNew = 1, Update = 2};
        private enMode _Mode;
        clsPerson _person;
        private enum _enGender { Male = 0, Female = 1 };

        private void _FillCountriesInComboBox()
        {
            DataTable countriesTable = clsCountry.GetAllCountries();
            foreach (DataRow row in countriesTable.Rows)
            {
                cbCountry.Items.Add(row["CountryName"]);
            }

            // set default country.
            cbCountry.SelectedIndex = cbCountry.FindString("Saudi Arabia");
        }

        private void _SetMaxAndMinDateInDateTimePicker()
        {
            // set the max date to 18 years from today, and set the default value the same.
            dtpDateOfBirth.MaxDate = DateTime.Now.AddYears(-18);
            dtpDateOfBirth.Value = dtpDateOfBirth.MaxDate;

            //should not allow adding age more than 100 years
            dtpDateOfBirth.MinDate = DateTime.Now.AddYears(-100);
        }

        private void _SetDefaultImageProfile()
        {
            if (rbMale.Checked && pbPicture.ImageLocation == null)
            {
                pbPicture.Image = Resources.male_512;
                return;
            }

            if (rbFemale.Checked && pbPicture.ImageLocation == null)
                pbPicture.Image = Resources.female_512;
        }

        private void _ResetDefaultValues()
        {
            if (_Mode == enMode.AddNew)
            {
                this.Text = "Add New Person";
                lblFormTitle.Text = "Add New Person";
                _person = new clsPerson();
            }
            else
            {
                this.Text = "Update Person Info";
                lblFormTitle.Text = "Edit Person Info";
            }

            _FillCountriesInComboBox();
            _SetMaxAndMinDateInDateTimePicker();
            txbFirstName.Text = "";
            txbSecondName.Text = "";
            txbThirdName.Text = "";
            txbLastName.Text = "";
            txbNationalNo.Text = "";
            txbPhone.Text = "";
            txbEmail.Text = "";
            txbAddress.Text = "";
            rbMale.Checked = true;
            llRemove.Visible = false;
            _SetDefaultImageProfile();
        }
        public frmAddEditPerson()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }


        private void frmAddEditPerson_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _SetPersonObject()
        {
            _person.FirstName = txbFirstName.Text.Trim();
            _person.SecondName = txbSecondName.Text.Trim();
            _person.ThirdName = txbThirdName.Text.Trim();
            _person.LastName = txbLastName.Text.Trim();
            _person.DateOfBirth = dtpDateOfBirth.Value;
            _person.Address = txbAddress.Text.Trim();
            _person.Phone = txbPhone.Text.Trim();
            _person.Email = txbEmail.Text.Trim();

            _person.NationalityId = clsCountry.Find(cbCountry.Text).CountryId;

            if (pbPicture.ImageLocation != null)
                _person.ImagePath = pbPicture.ImageLocation.ToString();
            else
                _person.ImagePath = "";

            if (rbMale.Checked)
                _person.Gender = (short)_enGender.Male;
            else
                _person.Gender = (short)_enGender.Female;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _SetPersonObject();
            _HandleProfileImage();
           
            if(_person.Save())
            {
                lblPersonId.Text = _person.PersonId.ToString();
                _Mode = enMode.Update;
                this.Text = "Update Person Info";
                lblFormTitle.Text = "Edit Person Info";
                MessageBox.Show("Added Succeffully");
            }
            else
            {
                MessageBox.Show("Operation Failed!");
            }
        }

        private void txbFirstName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txbFirstName.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txbFirstName, "First Name Can NOT be Empty!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txbFirstName, "");
            }
        }

        private void txbSecondName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txbSecondName.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txbSecondName, "Second Name Can NOT be Empty!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txbSecondName ,"");
            }
        }

        private void txbLastName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txbLastName.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txbLastName, "Last Name Can NOT be Empty");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txbLastName, "");
            }
        }

        private void txbPhone_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txbPhone.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txbPhone, "Phone Can NOT be Empty");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txbPhone, "");
            }
        }

        private void txbAddress_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txbAddress.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txbAddress, "Address Can NOT be Empty");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txbAddress, "");
            }
        }

        private void txbNationalNo_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txbNationalNo.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txbNationalNo, "The National No Can NOT be Empty!");
            }
            else
            {
                if (clsPerson.IsExist(txbNationalNo.Text.Trim()))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(txbNationalNo, "This National No Already Used!");
                }
                else
                {
                    e.Cancel = false;
                    errorProvider1.SetError(txbNationalNo, "");
                }
            }
        }

        private void txbEmail_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txbEmail.Text))
            {
                if (!txbEmail.Text.Contains("@"))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(txbEmail, "Invalid Email!");
                }
                else
                {
                    e.Cancel = false;
                    errorProvider1.SetError(txbEmail, "");
                }
            }
        }

        private void llRemove_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbPicture.ImageLocation = null;
          
            _SetDefaultImageProfile();

            llRemove.Visible = false;
            llSetImage.Text = "Set Image";
        }


        private bool _HandleProfileImage()
        {
            if (_person.ImagePath != "")
            {
                string sourceFile = pbPicture.ImageLocation.ToString();
                if (clsUtil.CopyImageProfileIntoProjectImages(ref sourceFile))
                {
                    _person.ImagePath = sourceFile;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        private void llSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "Image Files | *.jpg; *.jpeg; *.png";
            openFileDialog1.FileName = "";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pbPicture.Load(openFileDialog1.FileName);
                llRemove.Visible = true;
                llSetImage.Text = "Change Image";
            }
        }

        private void rbFemale_CheckedChanged(object sender, EventArgs e)
        {
            _SetDefaultImageProfile();
        }

        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            _SetDefaultImageProfile();
        }
    }
}
