using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clsLicenseData
    {
        public static int AddNewLicense(int applicationId, int driverId, int licenseClassId, DateTime issueDate, DateTime expirationDate, string notes, double paidFees, bool isActive, int issueReason, int createdByUserId)
        {
            int insertedId = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"INSERT INTO Licenses
							 (
							 	ApplicationID,
							 	DriverID,
							 	LicenseClass,
							 	IssueDate,
							 	ExpirationDate,
							 	Notes,
							 	PaidFees,
							 	IsActive,
							 	IssueReason,
							 	CreatedByUserID
							 )
							 VALUES 
							 (
							 	@applicationId, 
							 	@driverId, 
							 	@licenseClassId, 
							 	@issueDate, 
							 	@expirationDate, 
							 	@notes, 
							 	@paidFees, 
							 	@isActive, 
							 	@issueReason, 
							 	@createdByUserId
							 );
							 
							 SELECT SCOPE_IDENTITY();";

			SqlCommand command = new SqlCommand(query, connection);
			command.Parameters.AddWithValue("@applicationId", applicationId);
			command.Parameters.AddWithValue("@driverId", driverId);
			command.Parameters.AddWithValue("@licenseClassId", licenseClassId);
			command.Parameters.AddWithValue("@issueDate", issueDate);
			command.Parameters.AddWithValue("@expirationDate", expirationDate);
			command.Parameters.AddWithValue("@paidFees", paidFees);
			command.Parameters.AddWithValue("@isActive", isActive);
			command.Parameters.AddWithValue("@issueReason", issueReason);
			command.Parameters.AddWithValue("@createdByUserId", createdByUserId);

			if (notes != "")
				command.Parameters.AddWithValue("@notes", notes);
			else
				command.Parameters.AddWithValue("@notes", DBNull.Value);

			try
			{
				connection.Open();

				object result = command.ExecuteScalar();
				if (result != null)
					insertedId = Convert.ToInt32(result);
			}
			catch (Exception ex)
			{
				insertedId = -1;
			}
			finally
			{
				connection.Close();
			}

			return insertedId;
        }
    }
}
