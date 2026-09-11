using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clsTestTypeData
    {
        public static DataTable GetAllTesttypes()
        {
            DataTable allTestTypes = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = "SELECT * FROM TestTypes;";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                    allTestTypes.Load(reader);
                reader.Close();
            }
            catch(Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return allTestTypes;
        }

        public static bool Find(int testTypeId, ref string title, ref string description, ref double fees)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"SELECT * FROM TestTypes
                             WHERE TestTypeID = @testTypeId;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@testTypeId", testTypeId);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    title = reader["TestTypeTitle"].ToString();
                    description = reader["TestTypeDescription"].ToString();
                    fees = Convert.ToDouble(reader["TestTypeFees"]);
                    isFound = true; 
                }
                reader.Close();
            }
            catch(Exception ex)
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static bool Update(int testTypeId, string testTitle, string testDescription, double testFees)
        {
            bool isUpdateSucceed = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"UPDATE TestTypes 
                             SET TestTypeTitle = @testTitle, 
                                 TestTypeDescription = @testDescription, 
                                 TestTypeFees = @testFees
                             WHERE TestTypeID = @testTypeId;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@testTypeId", testTypeId);
            command.Parameters.AddWithValue("@testTitle", testTitle);
            command.Parameters.AddWithValue("@testDescription", testDescription);
            command.Parameters.AddWithValue("@testFees", testFees);

            try
            {
                connection.Open();
                if (command.ExecuteNonQuery() > 0)
                    isUpdateSucceed = true;
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return isUpdateSucceed; 
        }
    }
}
