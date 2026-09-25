using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clsTestAppointmentData
    {
        public static DataTable GetAllVisionTestAppointments(int localDrivingLicenseApplicationId)
        {
            DataTable allVisionTestAppointmentsTable = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"SELECT * FROM TestAppointments 
                             WHERE 
                                LocalDrivingLicenseApplicationID = @localDrivingLicenseApplicationId 
                                AND TestTypeID = 1;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("localDrivingLicenseApplicationId", localDrivingLicenseApplicationId);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                    allVisionTestAppointmentsTable.Load(reader);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return allVisionTestAppointmentsTable;
        }
    }
}
