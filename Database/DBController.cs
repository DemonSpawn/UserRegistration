using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ObjectModels;

namespace Database
{
    public class DBController
    {
        static readonly string connectionString = "Data Source=(localdb)\\mssqllocaldb;AttachDbFilename=C:\\Users\\seidel\\source\\repos\\UserRegistration\\Database\\UserRegistration.mdf;Integrated Security=False;Connect Timeout=5";

        public DBController()
        {
        }

        public List<User> ReadUsers()
        {
            List<User> userList = new List<User>();

            string queryString = "SELECT username, password FROM USERS";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string username = reader.GetString(reader.GetOrdinal("username")).Trim(' ');
                    string password = reader.GetString(reader.GetOrdinal("password")).Trim(' ');

                    User user = new User(username, password);
                    userList.Add(user);
                }

                // Call Close when done reading.
                reader.Close();
            }

            return userList;
        }

        public bool WriteUser(User user)
        {
            if (user.Username == "" || user.PasswordHash == "")
                return false;

            string queryString = "INSERT USERS (username, password) VALUES ('{0}', '{1}')";

            string formatedString = string.Format(queryString, user.Username, user.PasswordHash);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(formatedString, connection);
                command.CommandType = System.Data.CommandType.Text;
                command.Connection.Open();
                var reader = command.ExecuteNonQuery();
                
                // Call Close when done reading.
                command.Connection.Close();
            }

            return false;
        }
    }
}
