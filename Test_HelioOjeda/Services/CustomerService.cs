
using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
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
        public int CreateCustomer(Customer customerToSave, string currentUser)
        {
            string sql = "INSERT INTO customers (name, surname, photoUrl, createdBy) " +
               "VALUES ('" + customerToSave.Name + "', '" + customerToSave.Surname + "', '" + customerToSave.PhotoURL + "', '" + currentUser + "')";

            MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand(sql, connection);
            command.ExecuteNonQuery();

            return (int)command.LastInsertedId;
        }

        //READ ALL CUSTOMERS
        public ArrayList GetCustomers()
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
                
                if(!mySQLReader.IsDBNull(3))
                    customer.PhotoURL = mySQLReader.GetString(3);

                if (!mySQLReader.IsDBNull(4))
                    customer.CreatedBy = mySQLReader.GetString(4);

                if (!mySQLReader.IsDBNull(5))
                    customer.ModifiedBy = mySQLReader.GetString(5);

                customers.Add(customer);
            }

            return customers;
        }

        //READ CUSTOMER BY ID
        public Customer GetCustomer(int id)
        {
            Customer customer = new Customer();
            
            string sql = "SELECT * FROM customers WHERE id = @id";

            MySql.Data.MySqlClient.MySqlDataReader mySQLReader = null;
            MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand(sql, connection);
            //Parameters to avoid SQL injection
            command.Parameters.AddWithValue("@id", id);
            mySQLReader = command.ExecuteReader();

            //If the customer exists, read him
            if (mySQLReader.Read())
            {
                customer.Id = mySQLReader.GetInt16(0);
                customer.Name = mySQLReader.GetString(1);
                customer.Surname = mySQLReader.GetString(2);

                if (!mySQLReader.IsDBNull(3))
                    customer.PhotoURL = mySQLReader.GetString(3);

                if (!mySQLReader.IsDBNull(4))
                    customer.CreatedBy = mySQLReader.GetString(4);

                if (!mySQLReader.IsDBNull(5))
                    customer.ModifiedBy = mySQLReader.GetString(5);

                return customer;
            }

            else return null;
        }

        //UPDATE CUSTOMER
        public bool UpdateCustomer(int id, Customer updatedCustomer, string currentUser)
        {
            string sql = "SELECT * FROM customers WHERE id = @id";

            MySql.Data.MySqlClient.MySqlDataReader mySQLReader = null;           
            MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand(sql, connection);
            //Parameters to avoid SQL injection
            command.Parameters.AddWithValue("@id", id);
            mySQLReader = command.ExecuteReader();

            //If the customer exists, update him
            if (mySQLReader.Read())
            {
                string updateSql = "UPDATE customers SET name = @name, surname = @surname, photoUrl = @photoUrl, modifiedBy = @modifiedBy  WHERE id = @id";

                mySQLReader.Close();
                command = new MySql.Data.MySqlClient.MySqlCommand(updateSql, connection);
                //Parameters to avoid SQL injection
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@name", updatedCustomer.Name);
                command.Parameters.AddWithValue("@surname", updatedCustomer.Surname);
                command.Parameters.AddWithValue("@photoUrl", updatedCustomer.PhotoURL);
                command.Parameters.AddWithValue("@modifiedBy", currentUser);
                command.ExecuteNonQuery();
                return true;
            }

            else return false;
        }

        //DELETE CUSTOMER BY ID
        public bool DeleteCustomer(int id)
        {
            string sql = "SELECT * FROM customers WHERE id = @id";

            MySql.Data.MySqlClient.MySqlDataReader mySQLReader = null;
            MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand(sql, connection);
            //Parameters to avoid SQL injection
            command.Parameters.AddWithValue("@id", id);
            mySQLReader = command.ExecuteReader();

            //If the customer exists, delete him
            if (mySQLReader.Read())
            {
                string deleteSql = "DELETE FROM customers WHERE id = @id";

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
