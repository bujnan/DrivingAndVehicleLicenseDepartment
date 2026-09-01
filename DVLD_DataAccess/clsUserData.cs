using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clsUserData
    {
        public static DataTable GetAllUsers()
        { 
            DataTable _allUsersTable = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"SELECT 
	                            U.UserID,
	                            U.PersonID,
	                            FullName = P.FirstName + ' ' + P.SecondName + ' ' + IsNULL(P.ThirdName, '') + ' ' + P.LastName,
	                            U.UserName,
	                            U.IsActive
                             FROM Users U
                             INNER JOIN People P
	                            ON U.PersonID = P.PersonID;";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                    _allUsersTable.Load(reader);
                reader.Close();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return _allUsersTable;
        }

        public static int Save(string username, string password, bool isActive, int personId)
        {
            int userId = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"INSERT INTO Users (UserName, Password, IsActive, PersonID)
                             VALUES (@username, @password, @isActive, @personId);
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", password);
            command.Parameters.AddWithValue("@isActive", isActive);
            command.Parameters.AddWithValue("@personId", personId);

            try
            {
                connection.Open();
                object newUserId = command.ExecuteScalar();
                if (newUserId != null)
                    userId = Convert.ToInt32(newUserId);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return userId;
        }

    }
}
