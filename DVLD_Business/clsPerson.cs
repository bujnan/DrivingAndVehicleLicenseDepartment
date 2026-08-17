using DVLD_DataAccess;
using System.Data;
using System;

namespace DVLD_Business
{
    public class clsPerson
    {
        private int _personId;
        private string _nationalNo;
        public string _firstName;
        private string _secondName;
        private string _thirdName;
        private string _lastName;
        private DateTime _dateOfBirth;
        private short _gender;
        private string _address;
        private string _phone;
        private string _email;
        private int _nationalityId;
        private string _imagePath;

        public enum enMode {AddNew = 1, Update = 2};
        private enMode _Mode = enMode.AddNew;

        // Setters and Getters
        public int PersonId
        {
            get { return _personId; }
        }

        public string NationalNo
        {
            set
            {
                _nationalNo = value;
            }

            get
            {
                return _nationalNo;
            }
        }

        public string FirstName
        {
            set
            {
                _firstName = value;
            }

            get
            {
                return _firstName;
            }
        }

        public string SecondName
        {
            set
            {
                _secondName = value;
            }

            get
            {
                return _secondName;
            }
        }

        public string ThirdName
        {
            set
            {
                _thirdName = value;
            }

            get
            {
                return _thirdName;
            }
        }

        public string LastName
        {
            set
            {
                _lastName = value;
            }

            get
            {
                return _lastName;
            }
        }

        public DateTime DateOfBirth
        {
            set { _dateOfBirth = value; }
            get { return _dateOfBirth; }
        }

        public short Gender
        {
            set { _gender = value; }
            get { return _gender; }
        }

        public string Address
        {
            set { _address = value; }
            get { return _address; }
        }

        public string Phone
        {
            set { _phone = value; }
            get { return _phone; }
        }

        public string Email
        {
            set { _email = value; }
            get { return _email; }
        }

        public int NationalityId
        {
            set { _nationalityId = value; }
            get { return _nationalityId; }
        }

        public string ImagePath
        {
            set { _imagePath = value; }
            get { return _imagePath; }
        }

        // Constructors
        public clsPerson()
        {
            _personId = -1;
            _nationalNo = "";
            _firstName = "";
            _secondName = "";
            _thirdName = "";
            _lastName = "";
            _dateOfBirth = DateTime.Now;
            _gender = 0;
            _address = "";
            _phone = "";
            _email = "";
            _nationalityId = -1;
            _imagePath = "";

            _Mode = enMode.AddNew;
        }

        public clsPerson(int personId)
        {
            _personId = personId;
            _nationalNo = "";
            _firstName = "";
            _secondName = "";
            _thirdName = "";
            _lastName = "";
            _dateOfBirth = DateTime.Now;
            _gender = 0;
            _address = "";
            _phone = "";
            _email = "";
            _nationalityId = -1;
            _imagePath = "";
        }

        // Non-Static Methods
        private bool _AddNew()
        {
            _personId = clsPersonData.AddNew(_nationalNo, _firstName, _secondName, _thirdName, _lastName, _dateOfBirth, _gender, _address, _phone, _email, _nationalityId, _imagePath);
            return (_personId != -1);
        }
        public bool Save()
        {
            if (_AddNew())
            {
                _Mode = enMode.Update;
                return true;
            }

            return false;
        }

        // Static Methods
        public static DataTable GetAllPeople()
        {
            return clsPersonData.GetAllPeople();
        }

        public static bool IsExist(string nationalNo)
        {
            return clsPersonData.IsExist(nationalNo);
        }

    }
}
