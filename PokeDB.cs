using MySql.Data.MySqlClient;
using System;

namespace PokeClinic
{
    public class PokeDB
    {
        public static string _ConnectionString;
        public static string Secret{get ; set ;}


        public static MySqlConnection NewConnection()
        {
            return new MySqlConnection(PokeDB._ConnectionString);
        }

    }

}
