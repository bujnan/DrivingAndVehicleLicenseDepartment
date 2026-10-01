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

        public static int AddNewTest(int testAppointmentId, byte testResult, string testNotes, int createdByUserId)
        {
            int testId = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"INSERT INTO Tests
                             (
                             	TestAppointmentID,
                             	TestResult,
                             	Notes,
                             	CreatedByUserID
                             )
                             VALUES
                             (
                             	@testAppointmentId,
                             	@testResult,
                             	@testNotes,
                             	@createdByUserId
                             );
                             
                             UPDATE TestAppointments
                             SET IsLocked = 1 
                             WHERE TestAppointmentID = @testAppointmentId;
                             
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@testAppointmentId", testAppointmentId);
            command.Parameters.AddWithValue("@testResult", testResult);
            command.Parameters.AddWithValue("@testNotes", testNotes);
            command.Parameters.AddWithValue("@createdByUserId", createdByUserId);

            try
            {
                connection.Open();

                object insertedId = command.ExecuteScalar();
                if (insertedId != null)
                    testId = Convert.ToInt32(insertedId);
            }
            catch (Exception ex)
            {
                testId = -1;
            }
            finally
            {
                connection.Close();
            }

            return testId;
        }

    }
}
