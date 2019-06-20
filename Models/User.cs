using System;
using Dapper.Contrib.Extensions;

namespace PokeClinic.Models
{

    [Table("user")]
    public class User
    {
        [Key]
        [Computed]
        public Int64 Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        [Computed]
        public DateTime DateCreated { get; set; }
    }
}

        // public static List<User> SelectAll()
        // {
        //     string sql = "SELECT * FROM user";
        //     List<User> users = null;

        //     using (MySqlConnection conn = pokeDB.getConnection())
        //     {
        //         users = conn.Query<User>(sql).ToList();
        //     }
        //     return users;
        // }