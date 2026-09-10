using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DVLD_DataAccess
{
    public class clsApplicationsTypesData
    {
        public static DataTable GetAllApplicationsTypes()
        {
            DataTable allAplicationsTypesTable = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = "SELECT * FROM ApplicationTypes;";

            SqlCommand command = new SqlCommand(query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    allAplicationsTypesTable.Load(reader);
                }
                reader.Close();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return allAplicationsTypesTable;
        }

        public static bool Find(int applicationTypeId, ref string title, ref double fees)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = "SELECT * FROM ApplicationTypes;";

            SqlCommand command = new SqlCommand(query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if(reader.Read())
                {
                    title = reader["ApplicationTypeTitle"].ToString();
                    fees = Convert.ToDouble(reader["ApplicationFees"]);
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

        public static bool Update(int id, string title, double fees)
        {
            bool isUpdatedSucceed = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"Update ApplicationTypes
                             SET ApplicationTypeTitle = @title,
                                 ApplicationFees = @fees 
                             WHERE ApplicationTypeID = @id;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@title", title);
            command.Parameters.AddWithValue("@fees", fees);
            command.Parameters.AddWithValue("@id", id);

            try
            {
                connection.Open();
                if (command.ExecuteNonQuery() > 0)
                    isUpdatedSucceed = true;
            }
            catch(Exception ex)
            {
                isUpdatedSucceed = false;
            }
            finally
            {
                connection.Close();
            }

            return isUpdatedSucceed;
        }
    }
}
