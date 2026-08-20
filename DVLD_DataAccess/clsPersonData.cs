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
                if (reader.HasRows)
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
                @"INSERT INTO People (NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gendor, Address, Phone, Email, NationalityCountryID, ImagePath)" +
                "\nVALUES (@nationalNo, @firstName, @secondName, @thirdName, @lastName, @dateOfBirth, @gender, @address, @phone, @email, @nationalityId, @imagePath); " +
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

        public static bool IsExist(string nationalNo)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"SELECT Found = 1
                             FROM People
                             WHERE NationalNo = @nationalNo";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@nationalNo", nationalNo);

            try
            {
                connection.Open();
                object returnedResult = command.ExecuteScalar();
                if (returnedResult != null)
                    isFound = true;
            }
            catch (Exception ex)
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static bool IsExist(int personId)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"SELECT Found = 1
                             FROM People
                             WHERE PersonID = @personId";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@personId", personId);

            try
            {
                connection.Open();
                object returnedResult = command.ExecuteScalar();
                if (returnedResult != null)
                    isFound = true;
            }
            catch (Exception ex)
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static bool Find(int personId, ref string firstName, ref string secondName, ref string thirdName, ref string lastName, ref DateTime dateOfBirth, ref short gender, ref string address, ref string phone, ref string email, ref int nationalityId, ref string imagePath)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"SELECT * FROM People 
                             WHERE PersonID = @personId";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@personId", personId);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    firstName = reader["FirstName"].ToString();
                    secondName = reader["SecondName"].ToString();

                    if (reader["ThirdName"] == DBNull.Value)
                        thirdName = "";
                    else
                        thirdName = reader["ThirdName"].ToString();
                    lastName = reader["LastName"].ToString();

                    dateOfBirth = (DateTime)reader["DateOfBirth"];
                    gender = Convert.ToInt16(reader["Gendor"]);
                    address = reader["Address"].ToString();
                    phone = reader["Phone"].ToString();

                    if (reader["Email"] == DBNull.Value)
                        email = "";
                    else
                        email = reader["Email"].ToString();

                    nationalityId = Convert.ToInt32(reader["NationalityCountryID"]);

                    if (reader["ImagePath"] == DBNull.Value)
                        imagePath = "";
                    else
                        imagePath = reader["ImagePath"].ToString();
                }
                else
                    return false;
                reader.Close();
            }
            catch (Exception e)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
            return true;
        }

        public static bool Delete(int personId)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"DELETE FROM People 
                             WHERE PersonID = @personId";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@personId", personId);

            try
            {
                connection.Open();
                if (command.ExecuteNonQuery() < 0)
                    return false;
            }
            catch (Exception e)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
            return true;
        }
    }
}
