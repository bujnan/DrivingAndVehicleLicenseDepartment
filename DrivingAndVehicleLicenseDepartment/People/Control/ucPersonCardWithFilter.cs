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

namespace DrivingAndVehicleLicenseDepartment.People.Control
{
    public partial class ucPersonCardWithFilter : UserControl
    {
        private int _personId = -1;

        public int PersonId
        {
            get { return ucPersonCard1.PersonId; }
        }

        public clsPerson SelectedPersonInfo
        {
            get { return ucPersonCard1.SelectedPerson; }
        }
        public ucPersonCardWithFilter()
        {
            InitializeComponent();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txbFindBy.Text))
            {
                switch (cbFindBy.Text)
                {
                    case "National No":
                        ucPersonCard1.LoadPersonInfo(txbFindBy.Text);
                        break;

                    case "Person Id":
                        ucPersonCard1.LoadPersonInfo(Convert.ToInt32(txbFindBy.Text));
                        break;

                    default:
                        ucPersonCard1.LoadPersonInfo(txbFindBy.Text);
                        break;
                }
                errorProvider1.SetError(txbFindBy, null);
            }
            else
                errorProvider1.SetError(txbFindBy, "This Field Can NOT be Empty!");
        }

        private void ucPersonCardWithFilter_Load(object sender, EventArgs e)
        {
            cbFindBy.SelectedIndex = 0;
        }

        private void txbFindBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFindBy.Text == "Person Id")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void _RefreshPersonInfo(int personId)
        {
            ucPersonCard1.PersonId = personId;
            ucPersonCard1.LoadPersonInfo(personId);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddEditPerson addEditPerson = new frmAddEditPerson();
            addEditPerson.personSaved += _RefreshPersonInfo;
            addEditPerson.ShowDialog();
        }
    }
}
