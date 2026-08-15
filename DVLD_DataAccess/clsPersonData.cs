using System;
using System.Data;
using System.Data.SqlClient;
using System.Net;

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

        public static int AddNew(string nationalNo, string firstName, string secondName, string thirdName, string lastName, DateTime dateOfBirth, short gender, string address, string phone, string email, int nationalityId, string imagePath)
        {
            int personId = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = 
                @"INSERT INTO People (NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gendor, Address, Phone, Email, NationalityCountryID, ImagePath)\n" +
                "VALUES ('@nationalId', '@fistName', '@secondName', '@thirdName', '@lastName', '@dateOfBirth', '@gender', '@address', '@phone', '@email', '@nationalityId', '@imagePath');" +
                "\nSELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
            // handle the allowed NULL fields 
            if (thirdName != "" && thirdName != null)
                command.Parameters.AddWithValue("@thirdName", thirdName);
            else
                command.Parameters.AddWithValue("@thirdName", DBNull.Value);

            if (email != "" && email != null)
                command.Parameters.AddWithValue("@email", email);
            else
                command.Parameters.AddWithValue("@email", DBNull.Value);

            if (imagePath != "" && imagePath != null)
                command.Parameters.AddWithValue("@imagePath", imagePath);
            else
                command.Parameters.AddWithValue("@imagePath", DBNull.Value);

            command.Parameters.AddWithValue("@nationalNo", nationalNo);
            command.Parameters.AddWithValue("@firstName", firstName);
            command.Parameters.AddWithValue("@secondName", secondName);
            command.Parameters.AddWithValue("@lastName", lastName);
            command.Parameters.AddWithValue("@dateOfBirth", dateOfBirth);
            command.Parameters.AddWithValue("@gender", gender);
            command.Parameters.AddWithValue("@address", address);
            command.Parameters.AddWithValue("@phone", phone);
            command.Parameters.AddWithValue("@nationalityId", nationalityId);

            try
            {
                connection.Open();
                personId = Convert.ToInt32(command.ExecuteScalar());
            }
            catch (Exception ex) 
            {
            
            }
            finally
            {
                connection.Close();
            }

            return personId;
        }

    }
}
