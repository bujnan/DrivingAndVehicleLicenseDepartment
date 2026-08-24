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
	                            FullName = P.FirstName + ' ' + P.SecondName + ' ' + P.ThirdName + ' ' + P.LastName,
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

    }
}
