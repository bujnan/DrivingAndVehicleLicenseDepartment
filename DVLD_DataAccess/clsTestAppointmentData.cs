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
    }
}
