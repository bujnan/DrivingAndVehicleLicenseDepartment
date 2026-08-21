using DVLD_DataAccess;
using System.Data;
using System;
using System.Net.Sockets;

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

            _Mode = enMode.Update;
        }

        // Non-Static Methods
        private bool _AddNew()
        {
            _personId = clsPersonData.AddNew(_nationalNo, _firstName, _secondName, _thirdName, _lastName, _dateOfBirth, _gender, _address, _phone, _email, _nationalityId, _imagePath);
            return (_personId != -1);
        }

        private bool _Update()
        {
            return clsPersonData.Update(this.PersonId, this.FirstName, this.SecondName, this.ThirdName, this.LastName, this.NationalNo, this.DateOfBirth, this.Gender, this.Address, this.Phone, this.Email, this.NationalityId, this.ImagePath);
        }
        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    if (_AddNew())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:
                    if (_Update())
                    {
                        return true;
                    }
                    else
                        return false;
            }

            return false;
        }

        public bool Delete()
        {
            return (clsPersonData.Delete(this.PersonId));
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

        public static bool IsExist(int personId)
        {
            return clsPersonData.IsExist(personId);
        }

        public static clsPerson Find(int personId)
        {
            string firtName = "";
            string secondName = "";
            string thirdName = "";
            string lastName = "";
            string nationalNo = "";
            DateTime dateOfBirth = DateTime.Now;
            short gender = 0;
            string address = "";
            string phone = "";
            string email = "";
            int nationalityId = -1;
            string imagePath = "";


            if (clsPersonData.Find(personId, ref firtName, ref secondName, ref thirdName, ref lastName, ref nationalNo,ref dateOfBirth, ref gender, ref address, ref phone, ref email, ref nationalityId, ref imagePath))
            {
                clsPerson person = new clsPerson(personId);
                person.FirstName = firtName;
                person.SecondName = secondName;
                person.ThirdName = thirdName;
                person.LastName = lastName;
                person.NationalNo = nationalNo;
                person.DateOfBirth = dateOfBirth;
                person.Gender = gender;
                person.Address = address;
                person.Email = email;
                person.Phone = phone;
                person.NationalityId = nationalityId;
                person.ImagePath = imagePath;
                return person;
            }

            return null;
        }

    }
}
