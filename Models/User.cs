using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Dapper;

namespace PokeClinic.Models
{
    public class User
    {
        public Int64 Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime DateCreated { get; set; }

        public static List<User> SelectAll()
        {
            string sql = "SELECT * FROM user";
            List<User> users = null;

            using (MySqlConnection conn = pokeDB.getConnection())
            {
                users = conn.Query<User>(sql).ToList();
            }
            return users;
        }

    }
}