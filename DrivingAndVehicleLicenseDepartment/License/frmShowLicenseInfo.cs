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

namespace DrivingAndVehicleLicenseDepartment.License
{
    public partial class frmShowLicenseInfo : Form
    {
        private string _nationalNo = "";
        private int _licenseClassId = -1;
        public frmShowLicenseInfo(string nationalNo, int licenseClassId)
        {
            InitializeComponent();
            _nationalNo = nationalNo;
            _licenseClassId = licenseClassId;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmShowLicenseInfo_Load(object sender, EventArgs e)
        {
            clsLicense license = clsLicense.FindLicenseByNationalNumber(_nationalNo, _licenseClassId);
            clsPerson driver = clsPerson.Find(_nationalNo);

            if (license != null)
            {
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
