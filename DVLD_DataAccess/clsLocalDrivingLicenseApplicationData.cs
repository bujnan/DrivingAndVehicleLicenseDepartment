using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_DataAccess
{
    public class clsLocalDrivingLicenseApplicationData
    {
        public static int AddNewLocalDrivingLicenseApplication(int applicationId, int licenseClassId)
        {
            int newLocalDrivingApplicationId = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"INSERT INTO LocalDrivingLicenseApplications
                             (
                             	ApplicationID,
                             	LicenseClassID
                             )
                             VALUES
                             (
                             	@applicationId,
                             	@licenseClassId
                             );
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@applicationId", applicationId);
            command.Parameters.AddWithValue("@licenseClassId", licenseClassId);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null)
                    newLocalDrivingApplicationId = Convert.ToInt32(result);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return newLocalDrivingApplicationId;
        }

        public static DataTable GetAllLocalDrivingLicenseAplications()
        {
            DataTable allLocalDrivingLicenseApplication = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"SELECT LDLA.LocalDrivingLicenseApplicationID, 
                             LC.ClassName,
                             P.NationalNo,
                             FullName = 
                             	P.FirstName + ' ' + P.SecondName + ' ' + IsNull(P.ThirdName, '') + ' ' + P.LastName,
                             A.ApplicationDate,
                             PassedTests =
                             (
                             	SELECT COUNT(TA.TestTypeID)
                             	FROM Tests T
                             	INNER JOIN TestAppointments TA
                             		ON T.TestAppointmentID = TA.TestAppointmentID
                             	INNER JOIN TestTypes TT
                             		ON TA.TestTypeID = TT.TestTypeID
                             	WHERE TA.LocalDrivingLicenseApplicationID = LDLA.LocalDrivingLicenseApplicationID
                             		AND T.TestResult = 1
                             ),
                             ApplicationStatus =
                             CASE
                             	WHEN A.ApplicationStatus = 1 THEN 'New'
                             	WHEN A.ApplicationStatus = 2 THEN 'Canceled'
                             	WHEN A.ApplicationStatus = 3 THEN 'Completed'
                             END
                             FROM LocalDrivingLicenseApplications LDLA
                             INNER JOIN LicenseClasses LC
                             	ON LDLA.LicenseClassID = LC.LicenseClassID
                             INNER JOIN Applications A
                             	ON LDLA.ApplicationID = A.ApplicationID
                             INNER JOIN People P
                             	ON A.ApplicantPersonID = P.PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                    allLocalDrivingLicenseApplication.Load(reader);

            }
            catch(Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return allLocalDrivingLicenseApplication;
        }

        public static bool FindLocalDrivingLicenseApplicationByApplicantPersonId(int personId, ref int localDrivingLicenseApplication, ref int licenseClassId, ref int applicationId, ref int applicationTypeId, ref byte applicationStatus, ref DateTime applicationDate, ref DateTime lastStatusDate, ref double paidFees, ref int createdByUserId)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"SELECT * FROM LocalDrivingLicenseApplications LDLA
                             INNER JOIN Applications A
                             	ON LDLA.ApplicationID = A.ApplicationID
                             WHERE A.ApplicantPersonID = @personId;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@personId", personId);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    applicationDate = Convert.ToDateTime(reader["ApplicationDate"]); 
                    localDrivingLicenseApplication = Convert.ToInt32(reader["LocalDrivingLicenseApplicationID"]);
                    licenseClassId = Convert.ToInt32(reader["LicenseClassID"]);
                    applicationId = Convert.ToInt32(reader["ApplicationID"]);
                    applicationTypeId = Convert.ToInt32(reader["ApplicationTypeID"]);
                    applicationStatus = Convert.ToByte(reader["ApplicationStatus"]);
                    lastStatusDate = Convert.ToDateTime(reader["LastStatusDate"]);
                    paidFees = Convert.ToDouble(reader["PaidFees"]);
                    createdByUserId = Convert.ToInt32(reader["CreatedByUserID"]);

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

        public static bool FindByLocalDrivinLicenseApplicationId(int localDrivingLicenseApplication, ref int personId, ref int licenseClassId, ref int applicationId, ref int applicationTypeId, ref byte applicationStatus, ref DateTime applicationDate, ref DateTime lastStatusDate, ref double paidFees, ref int createdByUserId)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"SELECT * FROM LocalDrivingLicenseApplications LDLA
                             INNER JOIN Applications A
                             	ON LDLA.ApplicationID = A.ApplicationID
                             WHERE LDLA.LocalDrivingLicenseApplicationID = @localDrivingLicenseApplication;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@localDrivingLicenseApplication", localDrivingLicenseApplication);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    applicationDate = Convert.ToDateTime(reader["ApplicationDate"]);
                    personId = Convert.ToInt32(reader["ApplicantPersonID"]);
                    licenseClassId = Convert.ToInt32(reader["LicenseClassID"]);
                    applicationId = Convert.ToInt32(reader["ApplicationID"]);
                    applicationTypeId = Convert.ToInt32(reader["ApplicationTypeID"]);
                    applicationStatus = Convert.ToByte(reader["ApplicationStatus"]);
                    lastStatusDate = Convert.ToDateTime(reader["LastStatusDate"]);
                    paidFees = Convert.ToDouble(reader["PaidFees"]);
                    createdByUserId = Convert.ToInt32(reader["CreatedByUserID"]);

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

        public static bool UpdateLocalDrivingLicenseApplication(int localDrivingLicenseApplicationId, int licenseClassId)
        {
            bool isUpdateSucceed = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"UPDATE LocalDrivingLicenseApplications 
                             SET LicenseClassID = @licenseClassId
                             WHERE LocalDrivingLicenseApplicationID = @localDrivingLicenseApplicationId;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@licenseClassId", licenseClassId);
            command.Parameters.AddWithValue("@localDrivingLicenseApplicationId", localDrivingLicenseApplicationId);

            try
            {
                connection.Open();
                isUpdateSucceed = (command.ExecuteNonQuery() > 0);
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

        public static bool DeleteByLocalDrivingLicenseApplicationId(int localDrivingLicenseApplicationId)
        {
            bool isDeletionSucceed = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"DELETE FROM LocalDrivingLicenseApplications
                             WHERE LocalDrivingLicenseApplicationID = @localDrivingLicenseApplicationId";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@localDrivingLicenseApplicationId", localDrivingLicenseApplicationId);

            try
            {
                connection.Open();
                if (command.ExecuteNonQuery() > 0)
                    isDeletionSucceed = true;
            }
            catch (Exception ex)
            {
                isDeletionSucceed = false;
            }
            finally
            {
                connection.Close();
            }

            return isDeletionSucceed;
        }

        public static bool Cancel(int applicationId)
        {
            bool isCancelationSucceed = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"UPDATE Applications
                             SET ApplicationStatus = 2
                             WHERE ApplicationId = @applicationId;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@applicationId", applicationId);

            try
            {
                connection.Open();
                if (command.ExecuteNonQuery() > 0)
                    isCancelationSucceed = true;
            }
            catch (Exception ex)
            {
                isCancelationSucceed = false;
            }
            finally
            {
                connection.Close();
            }

            return isCancelationSucceed;
        }
    }
}
