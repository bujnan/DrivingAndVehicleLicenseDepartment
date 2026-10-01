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
        public static DataTable GetAllTestsAppointments(int localDrivingLicenseApplicationId, int testTypeId)
        {
            DataTable allVisionTestAppointmentsTable = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"SELECT * FROM TestAppointments 
                             WHERE 
                                LocalDrivingLicenseApplicationID = @localDrivingLicenseApplicationId 
                                AND TestTypeID = @testTypeId;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@localDrivingLicenseApplicationId", localDrivingLicenseApplicationId);
            command.Parameters.AddWithValue("@testTypeId", testTypeId);

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

        public static int AddNewTestAppointment(int testTypeId, int localDrivingLicenseApplicationId, DateTime appointmentDate, double paidFees, int userId, bool isLocked = false)
        {
            int testAppointmentId = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"INSERT INTO TestAppointments
                             (
                             	TestTypeID,
                             	LocalDrivingLicenseApplicationID,
                             	AppointmentDate,
                             	PaidFees,
                             	CreatedByUserID,
                             	IsLocked
                             )
                             VALUES
                             (
                             	@testTypeId, 
                             	@localDrivingLicenseApplicationId, 
                             	@appointmentDate, 
                             	@paidFees, 
                             	@userId, 
                             	@isLocked
                             );
                             SELECT SCOPE_IDENTITY();
                             ";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@testTypeId", testTypeId);
            command.Parameters.AddWithValue("@localDrivingLicenseApplicationId", localDrivingLicenseApplicationId);
            command.Parameters.AddWithValue("@appointmentDate", appointmentDate);
            command.Parameters.AddWithValue("@paidFees", paidFees);
            command.Parameters.AddWithValue("@userId", userId);
            command.Parameters.AddWithValue("@isLocked", isLocked);

            try
            {
                connection.Open();
                object insertedId = command.ExecuteScalar();
                if (insertedId != null)
                    testAppointmentId = Convert.ToInt32(insertedId);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return testAppointmentId;
        }

        public static bool IsLocked(int testAppointmentId, int testTypeId, int localDrivingLicenseApplicationId)
        {
            bool isLocked = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"SELECT IsLocked 
                             FROM TestAppointments
                             WHERE 
                             	TestAppointmentID = @testAppointmentId
                             	AND TestTypeID = @testTypeId
                             	AND LocalDrivingLicenseApplicationID = @localDrivingLicenseApplicationId;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@testAppointmentId", testAppointmentId);
            command.Parameters.AddWithValue("@testTypeId", testTypeId);
            command.Parameters.AddWithValue("@localDrivingLicenseApplicationId", localDrivingLicenseApplicationId);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null)
                    isLocked = Convert.ToBoolean(result);
            }
            catch (Exception ex)
            {
                isLocked = false;
            }
            finally
            {
                connection.Close();
            }

            return isLocked;
        }

        public static bool UpdateTestAppointment(int testAppointmentId, DateTime appointmentDate)
        {
            bool isUpdateSucceed = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"UPDATE TestAppointments
                             SET AppointmentDate = @appointmentDate
                             WHERE TestAppointmentID = @testAppointmentId;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@appointmentDate", appointmentDate);
            command.Parameters.AddWithValue("@testAppointmentId", testAppointmentId);

            try
            {
                connection.Open();
                if (command.ExecuteNonQuery() > 0)
                    isUpdateSucceed = true;
            }
            catch (Exception ex)
            {
                isUpdateSucceed = false;
            }
            finally
            {
                connection.Close();
            }

            return isUpdateSucceed;
        }

        public static bool Find(int testAppointmentId, ref int testTypeId, ref int localDrivingLicenseApplicationId, ref int userId, ref DateTime appointmentDate, ref double paidFees)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"SELECT 
                                TestTypeID,
                             	LocalDrivingLicenseApplicationID,
                             	CreatedByUserID,
                             	AppointmentDate,
                             	PaidFees
                             FROM TestAppointments
                             WHERE TestAppointmentID = @testAppointmentId;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@testAppointmentId", testAppointmentId);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    testTypeId = Convert.ToInt32(reader["TestTypeID"]);
                    localDrivingLicenseApplicationId = Convert.ToInt32(reader["LocalDrivingLicenseApplicationID"]);
                    userId = Convert.ToInt32(reader["CreatedByUserID"]);
                    appointmentDate = Convert.ToDateTime(reader["AppointmentDate"]);
                    paidFees = Convert.ToDouble(reader["PaidFees"]);

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

        public static bool HasActiveTestAppointment(int localDrivingLicenseApplicationId, int testTypeId)
        {
            bool hasActiveAppointment = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"SELECT TOP 1 IsLocked
                             FROM TestAppointments
                             WHERE 
                             	LocalDrivingLicenseApplicationID = @localDrivingLicenseApplicationId
                             	AND TestTypeID = @testTypeId
                             	AND IsLocked = 0
                             ORDER BY TestAppointmentID DESC;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@localDrivingLicenseApplicationId", localDrivingLicenseApplicationId);
            command.Parameters.AddWithValue("@testTypeId", testTypeId);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null)
                    hasActiveAppointment = true;
            }
            catch (Exception ex)
            {
                hasActiveAppointment = false;
            }
            finally
            {
                connection.Close();
            }

            return hasActiveAppointment;
        }

        public static bool DidPassTest(int localDrivingLicenseApplicationId, int testTypeId)
        {
            bool didPassTest = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"SELECT TOP 1 T.TestResult
                             FROM Tests T
                             INNER JOIN TestAppointments TA
                             	ON T.TestAppointmentID = T.TestAppointmentID
                             WHERE 
                             	TA.LocalDrivingLicenseApplicationID = @localDrivingLicenseApplicationId
                             	AND TA.TestTypeID = @testTypeId
                             	AND  T.TestResult = 1
                             ORDER BY TA.TestAppointmentID DESC;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@localDrivingLicenseApplicationId", localDrivingLicenseApplicationId);
            command.Parameters.AddWithValue("@testTypeId", testTypeId);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null)
                    didPassTest = true;
            }
            catch (Exception ex)
            {
                didPassTest = false;
            }
            finally
            {
                connection.Close();
            }

            return didPassTest;
        }
    }
}
