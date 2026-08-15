using DVLD_DataAccess;
using System.Data;
using System;

namespace DVLD_Business
{
    public class clsPerson
    {
        // Static Methods
        public static DataTable GetAllPeople()
        {
            return clsPersonData.GetAllPeople();
        }

        public static int AddNew(string nationalNo, string firstName, string secondName, string thirdName, string lastName, DateTime dateOfBirth, short gender, string address, string phone, string email, int nationalityId, string imagePath)
        {
            return clsPerson.AddNew(nationalNo, firstName, secondName, thirdName, lastName, dateOfBirth, gender, address, phone, email, nationalityId, imagePath);
        }
    }
}
