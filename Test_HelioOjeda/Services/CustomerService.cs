using System;
using System.Collections;
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

        //READ ALL CUSTOMERS
        public ArrayList getCustomers()
        {
            ArrayList customers = new ArrayList();

            string sql = "SELECT * FROM customers";

            MySql.Data.MySqlClient.MySqlDataReader mySQLReader = null;
            MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand(sql, connection);
            mySQLReader = command.ExecuteReader();

            //While there were customers, keep reading them
            while (mySQLReader.Read())
            {
                Customer customer = new Customer();
                customer.Id = mySQLReader.GetInt16(0);
                customer.Name = mySQLReader.GetString(1);
                customer.Surname = mySQLReader.GetString(2);
                customer.PhotoURL = mySQLReader.GetString(3);
                customers.Add(customer);
            }

            return customers;
        }

        //READ CUSTOMER BY ID
        public Customer getCustomer(int id)
        {
            Customer customer = new Customer();

            string sql = "SELECT * FROM customers WHERE id = " + id;

            MySql.Data.MySqlClient.MySqlDataReader mySQLReader = null;
            MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand(sql, connection);
            mySQLReader = command.ExecuteReader();

            //If the customer exists, read him
            if (mySQLReader.Read())
            {
                customer.Id = mySQLReader.GetInt16(0);
                customer.Name = mySQLReader.GetString(1);
                customer.Surname = mySQLReader.GetString(2);
                customer.PhotoURL = mySQLReader.GetString(3);
                return customer;
            }

            else return null;
        }

        //UPDATE CUSTOMER
        public bool updateCustomer(int id, Customer updatedCustomer)
        {
            string sql = "SELECT * FROM customers WHERE id = " + id;

            MySql.Data.MySqlClient.MySqlDataReader mySQLReader = null;           
            MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand(sql, connection);
            mySQLReader = command.ExecuteReader();

            //If the customer exists, update him
            if (mySQLReader.Read())
            {
                string updateSql = "UPDATE customers SET name = '" + updatedCustomer.Name + "', surname = '" + updatedCustomer.Surname
                + "', photoUrl = '" + updatedCustomer.PhotoURL + "'  WHERE id = " + id;

                mySQLReader.Close();
                command = new MySql.Data.MySqlClient.MySqlCommand(updateSql, connection);
                command.ExecuteNonQuery();
                return true;
            }

            else return false;
        }

        //DELETE CUSTOMER BY ID
        public bool deleteCustomer(int id)
        {
            string sql = "SELECT * FROM customers WHERE id = " + id;

            MySql.Data.MySqlClient.MySqlDataReader mySQLReader = null;
            MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand(sql, connection);
            mySQLReader = command.ExecuteReader();

            //If the customer exists, delete him
            if (mySQLReader.Read())
            {
                string deleteSql = "DELETE FROM customers WHERE id = " + id;

                mySQLReader.Close();
                command = new MySql.Data.MySqlClient.MySqlCommand(deleteSql, connection);
                command.ExecuteNonQuery();
                return true;
            }

            else return false;
        }
    }
}
