using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test_HelioOjeda.Models;

namespace Test_HelioOjeda.Services
{
    public class UserService
    {
        private MySql.Data.MySqlClient.MySqlConnection connection;

        public UserService()
        {
            string myConnectionString = "Server=localhost;Database=test_theam;Uid=root;Pwd=root;";

            //OPEN CONNECTION
            try
            {
                connection = new MySql.Data.MySqlClient.MySqlConnection();
                connection.ConnectionString = myConnectionString;
                connection.Open();
            }

            catch (MySql.Data.MySqlClient.MySqlException exception)
            {

            }
        }

        //VALIDATE USER
        public User ValidateUser(string userName, string password)
        {
            User user = new User();

            string sql = "SELECT * FROM users WHERE name = @name AND password = @password";

            MySql.Data.MySqlClient.MySqlDataReader mySQLReader = null;
            MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand(sql, connection);
            //Parameters to avoid SQL injection
            command.Parameters.AddWithValue("@name", userName);
            command.Parameters.AddWithValue("@password", password);
            mySQLReader = command.ExecuteReader();

            //If the user exists, read him
            if (mySQLReader.Read())
            {
                user.Id = mySQLReader.GetInt16(0);
                user.Name = mySQLReader.GetString(1);
                user.Password = mySQLReader.GetString(2);
                user.IsAdmin = mySQLReader.GetBoolean(3);
                return user;
            }

            else return null;
        }
    }
}
