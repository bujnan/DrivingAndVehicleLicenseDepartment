using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clsDriverData
    {
        public static int AddNewDriver(int personId, int createdByUserId, DateTime createdDate)
        {
            int insertedId = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"INSERT INTO Drivers
                             (
                             	PersonID,
                             	CreatedByUserID,
                             	CreatedDate
                             )
                             VALUES
                             (
                             	@personId, 
                             	@createdByUserId, 
                             	@createdDate
                             );
                             
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@personId", personId);
            command.Parameters.AddWithValue("@createdByUserId", createdByUserId);
            command.Parameters.AddWithValue("@createdDate", createdDate);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();
                if (result != null)
                    insertedId = Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                insertedId = -1;
            }
            finally
            {
                connection.Close();
            }

            return insertedId;
        }

        public static bool FindByPersonId(ref int driverId, int personId, ref int createdByUserId, ref DateTime createdDate)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"SELECT * FROM Drivers
                             WHERE PersonID = @personId;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@personId", personId);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    driverId = Convert.ToInt32(reader["DriverID"]);
                    createdByUserId = Convert.ToInt32(reader["CreatedByUserID"]);
                    createdDate = Convert.ToDateTime(reader["CreatedDate"]);
                    isFound = true;
                }

                reader.Close();
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
    }
}
