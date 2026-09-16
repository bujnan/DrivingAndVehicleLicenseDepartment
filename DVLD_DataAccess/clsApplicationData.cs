using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clsApplicationData
    {
        public static int AddNew(int applicantPersonId, DateTime applicationDate, int applicationTypeId, int applicationStatus, DateTime lastStatusDate, double paidFees, int createdByUserId)
        {
            int applicationId = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"INSERT INTO Applications 
                             (
                             	ApplicantPersonID, 
                             	ApplicationDate, 
                             	ApplicationTypeID, 
                             	ApplicationStatus,
	                            LastStatusDate,
                             	PaidFees, 
                             	CreatedByUserID
                             )
                             VALUES
                             (
                             	@applicantPersonId,
                                @applicationDate,
                                @applicationTypeId,
                                @applicationStatus,
                                @lastStatusDate,
                                @paidFees,
                                @createdByUserId
                             );
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@applicantPersonId", applicantPersonId);
            command.Parameters.AddWithValue("@applicationDate", applicationDate);
            command.Parameters.AddWithValue("@applicationTypeId", applicationTypeId);
            command.Parameters.AddWithValue("@applicationStatus", applicationStatus);
            command.Parameters.AddWithValue("@lastStatusDate", lastStatusDate);
            command.Parameters.AddWithValue("@paidFees", paidFees);
            command.Parameters.AddWithValue("@createdByUserId", createdByUserId);

            try
            {
                connection.Open();
                object generatedId = command.ExecuteScalar();
                if (generatedId != null)
                {
                    applicationId = Convert.ToInt32(generatedId);
                }
            }
            catch(Exception)
            {

            }
            finally
            {
                connection.Close();
            }
            return applicationId;
        }

        public static int GetActiveApplicationIdForLicenseClass(int personId, int applicationTypeId, int licenseClassId)
        {
            int activeApplicationId = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"SELECT A.ApplicationId AS ActiveApplicationId
                             FROM Applications A
                             INNER JOIN LocalDrivingLicenseApplications LA
                             	ON A.ApplicationID = LA.ApplicationID
                             WHERE A.ApplicantPersonID = @personId
                             	  AND A.ApplicationTypeID = @applicationTypeId
                             	  AND A.ApplicationStatus = 1
                             	  AND LA.LicenseClassID = @licenseClassId;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@personId", personId);
            command.Parameters.AddWithValue("@applicationTypeId", applicationTypeId);
            command.Parameters.AddWithValue("@licenseClassId", licenseClassId);

            try
            {
                connection.Open();
                object activeAppId = command.ExecuteScalar();
                if (activeAppId != null)
                    activeApplicationId = Convert.ToInt32(activeAppId);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return activeApplicationId;
        }
    }
}
