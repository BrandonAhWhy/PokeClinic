using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Dapper;
using Dapper.Contrib.Extensions;
using System.Data;

namespace PokeClinic.Models
{

    public enum USER_ROLE: int{
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
        public string Token { get; set;}
        public int CustomerRole {get; set;} = 0;


        // CREATE
        public bool Add()
        {
            // string sql = "INSERT INTO customer (customerName, customerEmail, customerPassword, dateCreated, customerRole) VALUES (@CustomerName, @CustomerEmail, @CustomerPassword, @DateCreated, @CustomerRole)";
            bool success = false;

            this.CustomerRole = 1;
            this.DateCreated = DateTime.Now;
            using (MySqlConnection conn = PokeDB.NewConnection())
            {
                                     System.Console.WriteLine("test1");
                        MySqlParameter[] pms = new MySqlParameter[5];
                        pms[0] = new MySqlParameter("p_CustomerName", MySqlDbType.VarChar,50);
                        pms[0].Value = "HEllo";

                        pms[1] = new MySqlParameter("p_Email", MySqlDbType.VarChar, 50);
                        pms[1].Value = "Help";

                        pms[2] = new MySqlParameter("p_Password", MySqlDbType.VarChar, 50);
                        pms[2].Value = "NO";
                        pms[3] = new MySqlParameter("p_DateCreated", MySqlDbType.DateTime, 50);
                        pms[3].Value = DateTime.Now;
                        pms[4] = new MySqlParameter("p_Role", MySqlDbType.Int64);
                        pms[4].Value = 1;

                        MySqlCommand command = new MySqlCommand();

                        command.Connection = conn;
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "EXEC_ADD_USER";

                        command.Parameters.AddRange(pms);

                        conn.Open();
                        if(command.ExecuteNonQuery() == 1)
                        {
                            System.Console.WriteLine("Yes");
                        }
                        else
                        {
                            System.Console.WriteLine("No");
                        }
                        conn.Close();

                // using (MySqlCommand cmd = new MySqlCommand("EXEC_ADD_USER", conn)) {
                //      cmd.CommandType =  CommandType.StoredProcedure;
                //     //  cmd.Parameters.Add("p_CustomerName", MySqlDbType.VarChar).Value = "tessdgsdgdst";
                //     //  cmd.Parameters.Add("p_Email", MySqlDbType.VarChar).Value = "tessdgsdgt";
                //     //  cmd.Parameters.Add("p_Password", MySqlDbType.VarChar).Value = "tedsgdsgst";
                //     //  cmd.Parameters.Add("p_DateCreated", MySqlDbType.DateTime).Value = DateTime.Now;
                //     //  cmd.Parameters.Add("p_Role", MySqlDbType.Int64).Value = 1;
                //      System.Console.WriteLine("test");
                //     //  System.Console.WriteLine(cmd.Parameters.Clear());
                //     // cmd.Parameters.Clear();
                //     // cmd
                //      conn.Open();
                //      cmd.ExecuteNonQuery();
                //      }

                // try {
                //     var affectedRows = conn.Execute(sql, this);
                //     success = (affectedRows > 0) ? true : false;
                // }catch(Exception err){
                //     Console.Write("SQL ADD ERR: " + err.Message);
                //     return false;
                // }
            }
            return success;
        }

        // UPDATE
        public bool Update()
        {
            string sql = "UPDATE customer SET customerName = @CustomerName, customerEmail = @CustomerEmail WHERE customerID = @CustomerID";
            bool success = false;

            using (MySqlConnection conn = PokeDB.NewConnection())
            {
                var affectedRows = conn.Execute(sql, this);
                success = (affectedRows > 0) ? true : false;
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
            string sql = "DELETE FROM user WHERE id = @Id";
            bool success = false;

            using (MySqlConnection conn = PokeDB.NewConnection())
            {
                var affectedRows = conn.Execute(sql, this);
                success = (affectedRows > 0) ? true : false;
            }
            return success;
        }

        // READ
        public static User Get(Int64 id)
        {
            string sql = "SELECT * FROM customer WHERE id = @Id";
            User user = null;

            using (MySqlConnection conn = PokeDB.NewConnection())
            {
                try{
                    user = conn.QueryFirst<User>(sql, new { Id = id });
                }catch(Exception err){
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
                try{
                    user = conn.QueryFirst<User>(sql, new { CustomerName = customerName });
                }catch(Exception err){
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
