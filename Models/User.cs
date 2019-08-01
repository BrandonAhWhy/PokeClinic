using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Dapper;
using Dapper.Contrib.Extensions;
using System.Data;

namespace PokeClinic.Models
{

    public enum USER_ROLE : int
    {
        USER = 0,
        ADMIN = 1
    }

    public class User
    {
        public Int64 CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        [JsonIgnore]
        public string CustomerPassword { get; set; }
        public DateTime DateCreated { get; set; }
        [WriteAttribute(false)]
        public string Token { get; set; }
        public int CustomerRole { get; set; } = 0;


        // CREATE
        public bool Add()
        {

            bool success = false;
            this.CustomerRole = 1;
            this.DateCreated = DateTime.Now;

            MySqlConnection conn = PokeDB.NewConnection();
            MySqlCommand cmd = new MySqlCommand();

            // This is once off
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = "CREATE PROCEDURE EXEC_ADD_USER(" +
                                  "IN CustomerName VARCHAR(250), IN CustomerEmail VARCHAR(250), IN CustomerPassword VARCHAR(250), IN DateCreated DATETIME, IN CustomerRole INTEGER)" +
                                  "BEGIN INSERT INTO customer(customerName, customerEmail, customerPassword, dateCreated, customerRole) " +
                                  "VALUES(CustomerName, CustomerEmail, CustomerPassword, DateCreated, CustomerRole); END";

                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error " + ex.Number + " has occurred: " + ex.Message);
            }
            conn.Close();
            Console.WriteLine("Connection closed.");

            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                cmd.Connection = conn;

                cmd.CommandText = "EXEC_ADD_USER";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CustomerName", this.CustomerName);
                cmd.Parameters["@CustomerName"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue("@CustomerEmail", this.CustomerEmail);
                cmd.Parameters["@CustomerEmail"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue("@CustomerPassword", this.CustomerPassword);
                cmd.Parameters["@CustomerPassword"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue("@DateCreated", this.DateCreated);
                cmd.Parameters["@DateCreated"].Direction = ParameterDirection.Input;

                cmd.Parameters.AddWithValue("@CustomerRole", this.CustomerRole);
                cmd.Parameters["@CustomerRole"].Direction = ParameterDirection.Input;


                if (cmd.ExecuteNonQuery() == 1)
                {
                    success = true;
                }
                else
                {
                    success = false;
                };
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.WriteLine("Error " + ex.Number + " has occurred: " + ex.Message);
                success = false;
            }
            conn.Close();
            Console.WriteLine("Done.");
            return success;
        }

        // UPDATE
        public bool Update()
        {

            string sql = "UPDATE customer SET customerName = @CustomerName, customerEmail = @CustomerEmail WHERE customerID = @CustomerID";
            bool success = false;

            using (MySqlConnection conn = PokeDB.NewConnection())
            {
                MySqlCommand cmd = new MySqlCommand();

                try
                {
                    Console.WriteLine("Connecting to MySQL...");
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.CommandText = "CREATE PROCEDURE PROC_UPDATE_USER ( CustomerName VARCHAR(255), CustomerEmail VARCHAR(255), CustomerID INTEGER) BEGIN UPDATE customer SET customerName = CustomerName, customerEmail = CustomerEmail WHERE customerID = CustomerID; END";

                    cmd.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Error " + ex.Number + " has occurred: " + ex.Message);
                }
                conn.Close();
                Console.WriteLine("Connection closed.");

                try
                {
                    Console.WriteLine("Connecting to MySQL...");

                    conn.Open();
                    cmd.Connection = conn;

                    cmd.CommandText = "PROC_UPDATE_USER";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@CustomerName", this.CustomerName);

                    cmd.Parameters.AddWithValue("@CustomerEmail", this.CustomerEmail);


                    cmd.Parameters.AddWithValue("@CustomerID", this.CustomerID);


                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        success = true;
                    }
                    else
                    {
                        success = false;
                    };
                }
                catch (MySql.Data.MySqlClient.MySqlException ex)
                {
                    Console.WriteLine("Error " + ex.Number + " has occurred: " + ex.Message);
                    success = false;
                }
                // var affectedRows = conn.Execute(sql, this);
                // success = (affectedRows > 0) ? true : false;
            }
            return success;
        }

        public bool UpdatePassword()
        {
            string sql = "UPDATE user SET password = @Password WHERE id = @Id";
            bool success = false;

            using (MySqlConnection conn = PokeDB.NewConnection())
            {
                var affectedRows = conn.Execute(sql, this);
                success = (affectedRows > 0) ? true : false;
            }
            return success;
        }

        // DELETE
        public bool Delete()
        {
            string sql = "DELETE FROM customer WHERE customerID = @CustomerID";
            bool success = false;
            using (MySqlConnection conn = PokeDB.NewConnection())
            {
                MySqlCommand cmd = new MySqlCommand();
                try
                {
                    Console.WriteLine("Connecting to MySQL...");
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.CommandText = "CREATE PROCEDURE PROC_DELETE_USER (CustomerID INTEGER) BEGIN DELETE FROM customer WHERE customerID = CustomerID; END";

                    cmd.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Error " + ex.Number + " has occurred: " + ex.Message);
                }
                conn.Close();
                Console.WriteLine("Connection closed.");
                try
                {
                    Console.WriteLine("Connecting to MySQL...");

                    conn.Open();
                    cmd.Connection = conn;

                    cmd.CommandText = "PROC_DELETE_USER";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@CustomerID", this.CustomerID);

                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        success = true;
                    }
                    else
                    {
                        success = false;
                    };
                }
                catch (MySql.Data.MySqlClient.MySqlException ex)
                {
                    Console.WriteLine("Error " + ex.Number + " has occurred: " + ex.Message);
                    success = false;
                }
            }

            // using (MySqlConnection conn = PokeDB.NewConnection())
            // {
            //     var affectedRows = conn.Execute(sql, this);
            //     success = (affectedRows > 0) ? true : false;
            // }
            return success;
        }

        // READ
        public static User Get(Int64 customerID)
        {
            System.Console.WriteLine("Running");
            string sql = "SELECT * FROM customer WHERE customerID = @CustomerID";
            User user = null;

            using (MySqlConnection conn = PokeDB.NewConnection())
            {
                try
                {
                    user = conn.QueryFirst<User>(sql, new { CustomerID = customerID });
                }
                catch (Exception err)
                {
                    Console.WriteLine(err.Message);
                    return null;
                }

            }
            return user;
        }

        public static User GetByName(string customerName)
        {
            string sql = "SELECT * FROM customer WHERE customerName = @CustomerName";
            User user = null;

            using (MySqlConnection conn = PokeDB.NewConnection())
            {
                try
                {
                    user = conn.QueryFirst<User>(sql, new { CustomerName = customerName });
                }
                catch (Exception err)
                {
                    Console.WriteLine(err.Message);
                    return null;
                }

            }
            return user;
        }

        public static IEnumerable<User> GetAll(Int64 limit, Int64 offset)
        {
            string sql = "SELECT * FROM customer LIMIT @Limit OFFSET @Offset ";
            IEnumerable<User> users = null;

            using (MySqlConnection conn = PokeDB.NewConnection())
            {
                users = conn.Query<User>(sql, new { Offset = offset, Limit = limit });
            }
            return users;
        }

        public void hashPassword()
        {
            this.CustomerPassword = PasswordHash.HashPassword(this.CustomerPassword);
        }

        public bool validatePassword(string password)
        {
            return PasswordHash.ValidatePassword(password, this.CustomerPassword);
        }
    }
}
