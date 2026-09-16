using System;
using System.Collections.Generic;
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
    }
}
