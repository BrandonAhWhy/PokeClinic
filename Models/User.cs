using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Dapper;
using Dapper.Contrib.Extensions;




using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;


namespace PokeClinic.Models
{

    public enum USER_ROLE: int{
        USER = 0,
        ADMIN = 1
    }

    public class User 
    {
        public Int64 Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }   
        [JsonIgnore]
        public string Password { get; set; }
        public DateTime DateCreated { get; set; }
        [WriteAttribute(false)]
        public string Token { get; set;}
        public int Role {get; set;} = 0;


        // CREATE
        public bool Add()
        {
            string sql = "INSERT INTO user (name, email, password, date_created, role) VALUES (@Name, @Email, @Password, @DateCreated, @Role)";
            bool success = false;

            this.DateCreated = DateTime.Now;
            using (MySqlConnection conn = PokeDB.NewConnection())
            {
                try {
                    var affectedRows = conn.Execute(sql, this);
                    success = (affectedRows > 0) ? true : false;
                }catch(Exception err){
                    Console.Write("SQL ADD ERR: " + err.Message);
                    return false;
                }
            }
            return success;
        }

        // UPDATE
        public bool Update()
        {
            string sql = "UPDATE user SET name = @Name, email = @Email WHERE id = @Id";
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
        public static User Get(int id)
        {
            string sql = "SELECT * FROM user WHERE id = @Id";
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

        public static User GetByName(string name)
        {
            string sql = "SELECT * FROM user WHERE name = @Name";
            User user = null;

            using (MySqlConnection conn = PokeDB.NewConnection())
            {
                try{
                    user = conn.QueryFirst<User>(sql, new { Name = name });
                }catch(Exception err){
                    Console.WriteLine(err.Message);
                    return null;
                }
                
            }
            return user;
        }

        public static IEnumerable<User> GetAll(Int64 limit, Int64 offset)
        {
            string sql = "SELECT * FROM user LIMIT @Limit OFFSET @Offset ";
            IEnumerable<User> users = null;

            using (MySqlConnection conn = PokeDB.NewConnection())
            {
                users = conn.Query<User>(sql, new { Offset = offset, Limit = limit });
            }
            return users;
        }

        public void hashPassword()
        {
            this.Password = PasswordHash.HashPassword(this.Password);
        }

        public bool validatePassword(string password)
        {
            return PasswordHash.ValidatePassword(password, this.Password);
        }
    }
}
