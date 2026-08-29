using DVLD_Business;
using System;
using System.Windows.Forms;

namespace DrivingAndVehicleLicenseDepartment
{
    public partial class ucPersonCard : UserControl
    {
        private int _personId = -1;
        private clsPerson _person;

        public ucPersonCard()
        {
            InitializeComponent();
        }

        private void _ResetPersonInfo()
        {
            lblPersonId.Text = "???";
            lblFullName.Text = "???";
            lblNationalNo.Text = "???";
            lblPhone.Text = "???";
            lblEmail.Text = "???";
            lblAddress.Text = "???";
            lblDateOfBirth.Text = "???";
            lblCountry.Text = "???";
            lblGender.Text = "???";
            pbProfile.ImageLocation = "";
        }

        private void _HandleGender()
        {
            if (_person.Gender == 0)
                lblGender.Text = "Male";
            else
                lblGender.Text = "Female";
        }

        private void _HandleImage()
        {
            if (_person.ImagePath != "")
                pbProfile.ImageLocation = _person.ImagePath; 
        }

        public void LoadPersonInfo(int personId)
        {
            _person = clsPerson.Find(personId);

            if (_person == null)
            {
                _ResetPersonInfo();
                MessageBox.Show("No Person Info Found");
                return;
            }

            lblPersonId.Text = personId.ToString();
            lblFullName.Text = _person.FullName;
            lblNationalNo.Text = _person.NationalNo;
            lblPhone.Text = _person.Phone;
            lblEmail.Text = _person.Email;
            lblAddress.Text = _person.Address;
            lblDateOfBirth.Text = _person.DateOfBirth.ToString("MMM dd, yyyy");
            lblCountry.Text = clsCountry.Find(_person.NationalityId).CountryName;
            _HandleGender();
            _HandleImage();
        }

        public void LoadPersonInfo(string nationalNo)
        {
            _person = clsPerson.Find(nationalNo);

            if (_person == null)
            {
                _ResetPersonInfo();
                MessageBox.Show("No Person Info Found!");
                return;
            }

            lblPersonId.Text = _person.PersonId.ToString();
            lblFullName.Text = _person.FullName;
            lblNationalNo.Text = _person.NationalNo;
            lblPhone.Text = _person.Phone;
            lblEmail.Text = _person.Email;
            lblAddress.Text = _person.Address;
            lblDateOfBirth.Text = _person.DateOfBirth.ToString("MMM dd, yyyy");
            lblCountry.Text = clsCountry.Find(_person.NationalityId).CountryName;
            _HandleGender();
            _HandleImage();
        }

        private void llEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddEditPerson addEditPerson = new frmAddEditPerson(_person.PersonId);
            _personId = _person.PersonId;
            addEditPerson.formClosed += RefreshPersonCard_Closed;
            addEditPerson.ShowDialog();
        }

        void RefreshPersonCard_Closed()
        {
            _person = clsPerson.Find(_personId);

            if (_person == null)
            {
                _ResetPersonInfo();
                MessageBox.Show("No Person Info Found");
                return;
            }

            lblPersonId.Text = _personId.ToString();
            lblFullName.Text = _person.FullName;
            lblNationalNo.Text = _person.NationalNo;
            lblPhone.Text = _person.Phone;
            lblEmail.Text = _person.Email;
            lblAddress.Text = _person.Address;
            lblDateOfBirth.Text = _person.DateOfBirth.ToString("MMM dd, yyyy");
            lblCountry.Text = clsCountry.Find(_person.NationalityId).CountryName;
            _HandleGender();
            _HandleImage();
        }
    }
}
