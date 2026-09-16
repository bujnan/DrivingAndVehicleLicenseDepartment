using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
