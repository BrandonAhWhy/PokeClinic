using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Dapper;

namespace PokeClinic.Models
{

    public class User
    {
        public Int64 Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        public DateTime DateCreated { get; set; }

        // CREATE
        public bool Add(User _user)
        {
            string sql = "INSERT INTO user (name, email, password) VALUES (@Name, @Email, @Password)";
            bool success = false;

            using (MySqlConnection conn = PokeDB.NewConnection())
            {
                var affectedRows = conn.Execute(sql, this);
                success = (affectedRows > 0) ? true : false;
            }
            return success;
        }

        // UPDATE
        public bool Update()
        {
            string sql = "UPDATE user SET name = @Name, email = @Email, password = @Password WHERE id = @Id";
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
                user = conn.QueryFirst<User>(sql, new { Id = id });
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
    }
}
