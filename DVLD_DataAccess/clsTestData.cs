using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clsTestData
    {
        public static byte CountPassedTests(int localDrivingLicenseApplicationId)
        {
            byte totalTestsPassed = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"SELECT TotalTestsPassed = COUNT(TA.TestTypeID)
                             FROM TestAppointments TA
                             INNER JOIN Tests T
                             	ON TA.TestAppointmentID = T.TestAppointmentID
                             WHERE 
                             	T.TestResult = 1
                             	AND TA.LocalDrivingLicenseApplicationID = @localDrivingLicenseApplicationId;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@localDrivingLicenseApplicationId", localDrivingLicenseApplicationId);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();
                if (result != null)
                    totalTestsPassed = Convert.ToByte(result);
            }
            catch (Exception Ex)
            {
                totalTestsPassed = 0;
            }
            finally
            {
                connection.Close();
            }

            return totalTestsPassed;
        }

    }
}
