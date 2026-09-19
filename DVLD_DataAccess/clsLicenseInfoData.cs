using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clsLicenseInfoData
    {
        public static bool Find(int licenseClassId, ref string licenseClassName, ref string licenseClassDescription, ref short minimumAllowedAge, ref short defaultValidityLength, ref double licenseClassFees)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"SELECT * FROM LicenseClasses 
                         WHERE LicenseClassID = @licenseClassId";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@licenseClassId", licenseClassId);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    licenseClassName = reader["ClassName"].ToString();
                    licenseClassDescription = reader["ClassDescription"].ToString();
                    minimumAllowedAge = Convert.ToInt16(reader["MinimumAllowedAge"]);
                    defaultValidityLength = Convert.ToInt16(reader["DefaultValidityength"]);
                    licenseClassFees = Convert.ToDouble(reader["ClassFees"]);
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

        public static bool Find(ref int licenseClassId, string licenseClassName, ref string licenseClassDescription, ref short minimumAllowedAge, ref short defaultValidityLength, ref double licenseClassFees)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"SELECT * FROM LicenseClasses 
                             WHERE ClassName = @licenseClassName";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@licenseClassName", licenseClassName);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    licenseClassId = Convert.ToInt32(reader["LicenseClassID"]);
                    licenseClassDescription = reader["ClassDescription"].ToString();
                    minimumAllowedAge = Convert.ToInt16(reader["MinimumAllowedAge"]);
                    defaultValidityLength = Convert.ToInt16(reader["DefaultValidityLength"]);
                    licenseClassFees = Convert.ToDouble(reader["ClassFees"]);
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

        public static bool IsLicenseExistByPersonID(int personId, int licenseClassId)
        {
            bool isExist = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"SELECT L.LicenseID
                             FROM Licenses L
                             INNER JOIN Drivers D
                             	ON L.DriverID = D.DriverID
                             WHERE L.LicenseClass = @licenseClassId
                             	AND D.PersonID = @personId;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@personId", personId);
            command.Parameters.AddWithValue("@licenseClassId", licenseClassId);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null)
                    isExist = true;
            }
            catch (Exception ex)
            {
                isExist = false;
            }
            finally
            {
                connection.Close();
            }

            return isExist;
        }
    }
}
