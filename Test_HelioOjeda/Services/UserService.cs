using System;
using System.Collections;
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

            //If the user exists, read and return him
            if (mySQLReader.Read())
            {
                user.Id = mySQLReader.GetInt16(0);
                user.Name = mySQLReader.GetString(1);
                user.Password = mySQLReader.GetString(2);
                user.IsAdmin = mySQLReader.GetBoolean(3);

                mySQLReader.Close();
                return user;
            }

            else return null;
        }

        //USERNAME EXISTS?
        public bool UserExists(string userName)
        {
            string sql = "SELECT * FROM users WHERE name = @name";

            MySql.Data.MySqlClient.MySqlDataReader mySQLReader = null;
            MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand(sql, connection);
            //Parameters to avoid SQL injection
            command.Parameters.AddWithValue("@name", userName);
            mySQLReader = command.ExecuteReader();

            //If the user exists, return true
            if (mySQLReader.Read())
            {
                mySQLReader.Close();
                return true;
            }

            else
            {
                mySQLReader.Close();
                return false;
            }
        }

        //CREATE USER
        public int CreateUser(User userToSave)
        {
            //Manage when a username already exists
            if (UserExists(userToSave.Name))
            {
                return -1;
            }

            int isAdmin = userToSave.IsAdmin ? 1 : 0;

            string sql = "INSERT INTO users (name, password, isAdmin) " +
               "VALUES ('" + userToSave.Name + "', '" + userToSave.Password + "', '" + isAdmin + "')";

            MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand(sql, connection);
            command.ExecuteNonQuery();

            return (int)command.LastInsertedId;
        }

        //READ ALL USERS
        public ArrayList GetUsers()
        {
            ArrayList users = new ArrayList();

            string sql = "SELECT * FROM users";

            MySql.Data.MySqlClient.MySqlDataReader mySQLReader = null;
            MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand(sql, connection);
            mySQLReader = command.ExecuteReader();

            //While there were users, keep reading them
            while (mySQLReader.Read())
            {
                User user = new User();
                user.Id = mySQLReader.GetInt16(0);
                user.Name = mySQLReader.GetString(1);
                user.Password = mySQLReader.GetString(2);
                user.IsAdmin = mySQLReader.GetBoolean(3);
                users.Add(user);
            }

            return users;
        }

        //UPDATE USER
        //Here the admin can also change the user's admin status with "isAdmin"
        public bool UpdateUser(int id, User updatedUser)
        {
            //Manage when a username already exists
            if (UserExists(updatedUser.Name))
            {
                return false;
            }

            int isAdmin = updatedUser.IsAdmin ? 1 : 0;

            string sql = "SELECT * FROM users WHERE id = @id";

            MySql.Data.MySqlClient.MySqlDataReader mySQLReader = null;
            MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand(sql, connection);
            //Parameters to avoid SQL injection
            command.Parameters.AddWithValue("@id", id);
            mySQLReader = command.ExecuteReader();

            //If the customer exists, update him
            if (mySQLReader.Read())
            {
                string updateSql = "UPDATE users SET name = @name, password = @password, isAdmin = @isAdmin  WHERE id = @id";

                mySQLReader.Close();
                command = new MySql.Data.MySqlClient.MySqlCommand(updateSql, connection);
                //Parameters to avoid SQL injection
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@name", updatedUser.Name);
                command.Parameters.AddWithValue("@password", updatedUser.Password);
                command.Parameters.AddWithValue("@isAdmin", isAdmin);
                command.ExecuteNonQuery();
                return true;
            }

            else return false;
        }

        //DELETE USER BY ID
        public bool DeleteUser(int id)
        {
            string sql = "SELECT * FROM users WHERE id = @id";

            MySql.Data.MySqlClient.MySqlDataReader mySQLReader = null;
            MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand(sql, connection);
            //Parameters to avoid SQL injection
            command.Parameters.AddWithValue("@id", id);
            mySQLReader = command.ExecuteReader();

            //If the customer exists, delete him
            if (mySQLReader.Read())
            {
                string deleteSql = "DELETE FROM users WHERE id = @id";

                mySQLReader.Close();
                command = new MySql.Data.MySqlClient.MySqlCommand(deleteSql, connection);
                //Parameters to avoid SQL injection
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
                return true;
            }

            else return false;
        }
    }
}
