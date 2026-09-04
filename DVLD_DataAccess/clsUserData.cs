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

        public static bool IsExist(string username)
        {
            bool found = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"SELECT Found = 1
                             FROM Users
                             WHERE UserName = @username;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@username", username);

            try
            {
                connection.Open();
                if (command.ExecuteScalar() != null)
                    found = true;
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }
            return found;
        }

        public static bool Find(ref int userId, ref string username, ref string password, ref bool isActive, int personId)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
            string query = @"SELECT * FROM Users 
                             WHERE PersonID = @personId;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("personId", personId);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    userId = Convert.ToInt32(reader["UserID"]);
                    username = reader["UserName"].ToString();
                    password = reader["Password"].ToString();
                    isActive = Convert.ToBoolean(reader["IsActive"]);
                    personId = Convert.ToInt32(reader["PersonID"]);
                    reader.Close();
                }
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
