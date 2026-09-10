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
    }
}
