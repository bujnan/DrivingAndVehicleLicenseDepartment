using DVLD_DataAccess;
using System.Data;

namespace DVLD_Business
{
    public class clsPerson
    {
        // Static Methods
        public static DataTable GetAllPeople()
        {
            return clsPersonData.GetAllPeople();
        }
    }
}
