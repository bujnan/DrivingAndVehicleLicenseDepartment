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

namespace DrivingAndVehicleLicenseDepartment.License.Control
{
    public partial class ucLicenseInfo : UserControl
    {
        private int _licenseId = -1;
        private clsLicense _license = null;
        private string _nationalNo = "";
        private int _licenseClassId = -1;

        public int LicenseId
        {
            get { return _licenseId; }
        }

        public clsLicense SelectedLicenseInfo
        {
            get { return _license; }
        }
        public ucLicenseInfo()
        {
            InitializeComponent();
        }

        public void LoadInfo(string nationalNo, int licenseClassId)
        {
            _nationalNo = nationalNo;
            _licenseClassId = licenseClassId;

            clsLicense license = clsLicense.FindLicenseByNationalNumber(_nationalNo, _licenseClassId);
            clsPerson driver = clsPerson.Find(_nationalNo);

            if (license != null)
            {
                _licenseId = license.LicenseId;
                _license = license;

                lblClass.Text = license.LicenseClassInfo.LicenseClassName;
                lblFullName.Text = driver.FullName;
                lblLicenseId.Text = license.LicenseId.ToString();
                lblNationalNo.Text = _nationalNo;
                lblGender.Text = (driver.Gender == 0) ? "Male" : "Female";
                lblIssueDate.Text = license.IssueDate.ToString("MMM dd, yyyy");
                lblIsActive.Text = license.IsActive ? "Yes" : "No";
                lblBirthOfDate.Text = driver.DateOfBirth.ToString("MMM dd, yyyy");
                lblDriverId.Text = license.DriverId.ToString();
                lblExperationDate.Text = license.ExpirationDate.ToString("MMM dd, yyyy");

                if (license.Notes != "")
                    lblNotes.Text = license.Notes;

                if (driver.ImagePath != "")
                    pbPersonPicture.ImageLocation = driver.ImagePath;

                switch (license.IssueReason)
                {
                    case clsLicense.enIssueReason.FirstTime:
                        lblIssueReason.Text = "First Time";
                        break;

                        // other case needs here for other issueReason (Renew, Damage, etc)

                }

                //lblIsDetained.Text = ; // Need Fix Later
            }
        }
    }
}
