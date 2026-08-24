using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccess;

namespace DVLD_Business
{
    public class clsUser
    {
        private int _userId = -1;
        private string _password = "";
        private bool _isActive = false;
        private clsPerson _person = null;

        // Setters and Getters
        public int UserId
        {
            get { return _userId; }
        }

        public string Password
        {
            set { _password = value; }
            get { return _password; }
        }

        // Static Methods
        public static DataTable GetAllUsers()
        {
            return clsUserData.GetAllUsers();
        }

    }
}
