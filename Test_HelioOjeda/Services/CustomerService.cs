using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test_HelioOjeda.Models;

namespace Test_HelioOjeda.Services
{
    //This class will manage all the SQL queries and connections to the database
    public class CustomerService
    {
        private MySql.Data.MySqlClient.MySqlConnection connection;

        public CustomerService()
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

        //CREATE CUSTOMER
        public void createCustomer(Customer customerToSave)
        {
            string sql = "INSERT INTO customers (name, surname, photoUrl) " +
               "VALUES ('" + customerToSave.Name + "', '" + customerToSave.Surname + "', '" + customerToSave.PhotoURL + "')";

            MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand(sql, connection);

            command.ExecuteNonQuery();
        }
    }
}
