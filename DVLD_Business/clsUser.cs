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

        private enum enMode { AddNew = 0, Update =1 };

        private enMode _mode = enMode.AddNew;

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

        // Constructors
        public clsUser()
        {
            _userId = -1;
            _username = "";
            _password = "";
            _isActive = false;
            _personId = -1;
            _mode = enMode.AddNew;
        }

        private clsUser(int userId, string username, string password, bool isActive, int personId)
        {
            _userId = userId;
            _username = username;
            _password = password;
            _isActive = isActive;
            _personId = personId;
            _mode = enMode.Update;
        }

        private bool _AddNewUser()
        {
            _userId = clsUserData.AddNewUser(_username, _password, _isActive, _personId);
            return (_userId != -1);
        }

        private bool _UpdateUser()
        {
            return clsUserData.Update(_userId, _username, _password, _isActive, _personId);
        }

        // Non-Static Methods
        public bool Save()
        {
            switch(_mode)
            {
                case enMode.AddNew:
                {
                    if (_AddNewUser())
                    {
                        _mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;
                }

                case enMode.Update:
                    return _UpdateUser();
            }

            return false;
        }

        public  bool Delete()
        {
            return clsUserData.Delete(this._userId);
        }

        public bool UpdatePassword()
        {
            return clsUserData.UpdatePassword(this._userId, this._password);
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

        public static clsUser FindUserByPersonId(int personId)
        {
            int userId = -1;
            string username = "";
            string password = "";
            bool isActive = false;

            if (clsUserData.FindUserByPersonId(ref userId, ref username, ref password, ref isActive, personId))
            {
                return new clsUser(userId, username, password, isActive, personId);
            }
            else
                return null;
        }

        public static clsUser Find(int userId)
        {
            string username = "";
            string password = "";
            int personId = -1;
            bool isActive = false;

            if (clsUserData.Find(userId, ref username, ref password, ref isActive, ref personId))
            {
                return new clsUser(userId, username, password, isActive, personId);
            }
            else
                return null;
        }

        public static clsUser FindByUsernameAndPassword(string username, string password)
        {
            int userId = -1;
            int personId = -1;
            bool isActive = false;

            if(clsUserData.FindByUsernameAndPassword(username, password, ref userId, ref isActive, ref personId))
            {
                return new clsUser(userId, username, password, isActive, personId);
            }

            return null;
        }

    }
}
