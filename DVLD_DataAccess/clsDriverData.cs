using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clsDriverData
    {
        public static int AddNewDriver(int personId, int createdByUserId, DateTime createdDate)
        {
            int insertedId = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"INSERT INTO Drivers
                             (
                             	PersonID,
                             	CreatedByUserID,
                             	CreatedDate
                             )
                             VALUES
                             (
                             	@personId, 
                             	@createdByUserId, 
                             	@createdDate
                             );
                             
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@personId", personId);
            command.Parameters.AddWithValue("@createdByUserId", createdByUserId);
            command.Parameters.AddWithValue("@createdDate", createdDate);

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

        public static bool FindByPersonId(ref int driverId, int personId, ref int createdByUserId, ref DateTime createdDate)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"SELECT * FROM Drivers
                             WHERE PersonID = @personId;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@personId", personId);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    driverId = Convert.ToInt32(reader["DriverID"]);
                    createdByUserId = Convert.ToInt32(reader["CreatedByUserID"]);
                    createdDate = Convert.ToDateTime(reader["CreatedDate"]);
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

        public static bool FindByDriverId(int driverId, ref int personId, ref int createdByUserId, ref DateTime createdDate)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"SELECT * FROM Drivers
                             WHERE DriverID = @driverId;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@driverId", driverId);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    personId = Convert.ToInt32(reader["PersonID"]);
                    createdByUserId = Convert.ToInt32(reader["CreatedByUserID"]);
                    createdDate = Convert.ToDateTime(reader["CreatedDate"]);
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

        public static DataTable GetAllDrivers()
        {
            DataTable driversTable = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"SELECT 
                                 D.DriverID,
                                 D.PersonID,
                                 P.NationalNO,
                                 FullName = 
                                     P.FirstName + ' ' + 
                                     ISNULL(P.SecondName, '') + ' ' + 
                                     ISNULL(P.ThirdName, '') + ' ' + 
                                     P.LastName,
                                 CreatedDate = CAST(D.CreatedDate AS DATE),
                                 ActiveLicenses = 
                                     ( 
                                         SELECT COUNT(*) 
                                         FROM Licenses L
                                         WHERE L.DriverID = D.DriverID
                                     )
                             FROM Drivers D
                             INNER JOIN People P
                                 ON D.PersonID = P.PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                    driversTable.Load(reader);

                reader.Close();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return driversTable;
        }

        public static DataTable GetAllLocalDrivingLicenses(int driverId)
        {
            DataTable allLocalDrivingLicensesTable = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"SELECT
                             	L.LicenseID,
                             	L.ApplicationID,
                             	LC.ClassName,
                             	IssueDate = CAST(L.IssueDate AS DATE),
                             	ExpirationDate = CAST(L.ExpirationDate AS DATE),
                             	L.IsActive
                             FROM Licenses L
                             INNER JOIN LicenseClasses LC
                             	ON L.LicenseClass = LC.LicenseClassID
                             INNER JOIN Applications A
                             	ON L.ApplicationID = A.ApplicationID
                             INNER JOIN ApplicationTypes AT
                             	ON A.ApplicationTypeID = AT.ApplicationTypeID
                             WHERE L.DriverID = @driverId;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@driverId", driverId);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                    allLocalDrivingLicensesTable.Load(reader);

                reader.Close();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return allLocalDrivingLicensesTable;
        }

        public static DataTable GetAllInternationalDrivingLicenses(int driverId)
        {
            DataTable allLocalDrivingLicensesTable = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"SELECT
                             	L.LicenseID,
                             	L.ApplicationID,
                             	LC.ClassName,
                             	IssueDate = CAST(L.IssueDate AS DATE),
                             	ExpirationDate = CAST(L.ExpirationDate AS DATE),
                             	L.IsActive
                             FROM Licenses L
                             INNER JOIN LicenseClasses LC
                             	ON L.LicenseClass = LC.LicenseClassID
                             INNER JOIN Applications A
                             	ON L.ApplicationID = A.ApplicationID
                             INNER JOIN ApplicationTypes AT
                             	ON A.ApplicationTypeID = AT.ApplicationTypeID
                             WHERE L.DriverID = @driverId
                             AND AT.ApplicationTypeID = 6;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@driverId", driverId);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                    allLocalDrivingLicensesTable.Load(reader);

                reader.Close();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return allLocalDrivingLicensesTable;
        }
    }
}
