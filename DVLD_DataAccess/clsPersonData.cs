using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccess
{
    public class clsPersonData
    {
       public static DataTable GetAllPeople()
        {
            DataTable peopleTable = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = "" +
                "SELECT PersonID," +
                "       NationalNo," +
                "       FirstName," +
                "       SecondName," +
                "       ThirdName," +
                "       LastName," +
                "       DateOfBirth," +
                "       Gendor," +
                "       GendorCaption =" +
                "           CASE" +
                "               WHEN Gendor = 0 THEN 'Male'"  +
                "               WHEN Gendor = 1 THEN 'Female'" +
                "           END," +
                "       Address," +
                "       Phone," +
                "       Email," +
                "       CountryName," +
                "       ImagePath " +
                "FROM People P " +
                "INNER JOIN Countries C" +
                "   ON P.NationalityCountryID = C.CountryID";

            SqlCommand command = new SqlCommand(query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while(reader.Read())
                {
                    peopleTable.Load(reader);
                }
                reader.Close();
            } 
            catch(Exception ex)
            {
               
            }
            finally
            {
                connection.Close();
            }
            return peopleTable;
        }

    }
}
