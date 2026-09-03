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
        private string _username = "";
        private string _password = "";
        private bool _isActive = false;

        private int _personId = -1;
        private clsPerson _person = null;

        // Setters and Getters
        public int UserId
        {
            get { return _userId; }
        }

        public string Username
        {
            set { _username = value; }
            get { return _username; }
        }

        public string Password
        {
            set { _password = value; }
            get { return _password; }
        }

        public bool IsActive
        {
            set { _isActive = value; }
            get { return _isActive; }
        }

        public int PersonId
        {
            set { _personId = value; }
            get { return _personId; }
        }

        // Non-Static Methods
        public bool Save()
        {
            _userId = clsUserData.Save(_username, _password, _isActive, _personId);
            return (_userId != -1);
        }


        // Static Methods
        public static DataTable GetAllUsers()
        {
            return clsUserData.GetAllUsers();
        }
        public static bool IsExist(string username)
        {
            return clsUserData.IsExist(username);
        }

    }
}
