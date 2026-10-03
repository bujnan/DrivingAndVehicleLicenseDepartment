using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

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

		public static bool FindByNationalNumber(string nationalNo, ref int licenseId, ref int applicationId, ref int driverId, int licenseClassId, ref int createdByUserId, ref DateTime issueDate, ref DateTime experationDate, ref string notes, ref byte issueReason, ref double paidFees, ref bool isActive)
		{
			bool isFound = false;

			SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
			string query = @"SELECT 
							 	L.LicenseID, 
							 	L.ApplicationID,
							 	L.DriverID,
							 	L.LicenseClass,
							 	L.IssueDate,
							 	L.ExpirationDate,
							 	L.Notes,
							 	L.PaidFees,
							 	L.IsActive,
							 	L.IssueReason,
							 	L.CreatedByUserID
							 FROM Licenses L
							 INNER JOIN Drivers D
							 	ON L.DriverID = D.DriverID
							 INNER JOIN People P
							 	ON D.PersonID = P.PersonID
							 WHERE 
							 	P.NationalNo = @nationalNo
							 	AND L.LicenseClass = @licenseClassId;";

			SqlCommand command = new SqlCommand(query, connection);
			command.Parameters.AddWithValue("@nationalNo", nationalNo);
			command.Parameters.AddWithValue("@licenseClassId", licenseClassId);

			try
			{
				connection.Open();

				SqlDataReader reader = command.ExecuteReader();
				if (reader.Read())
				{
					licenseId = Convert.ToInt32(reader["LicenseID"]);
					applicationId = Convert.ToInt32(reader["ApplicationID"]);
					driverId = Convert.ToInt32(reader["DriverID"]);
					licenseClassId = Convert.ToInt32(reader["LicenseClass"]);
					issueDate = Convert.ToDateTime(reader["IssueDate"]);
                    experationDate = Convert.ToDateTime(reader["ExpirationDate"]);
					issueReason = Convert.ToByte(reader["IssueReason"]);
                    paidFees = Convert.ToDouble(reader["PaidFees"]);
					isActive = Convert.ToBoolean(reader["IsActive"]);
					createdByUserId = Convert.ToInt32(reader["CreatedByUserID"]);

					if (reader["Notes"] != DBNull.Value)
						notes = Convert.ToString(reader["Notes"]);

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
    }
}
