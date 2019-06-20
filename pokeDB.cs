using MySql.Data.MySqlClient;
using System;

namespace PokeClinic
{
    public class pokeDB
    {
        public static string _ConnectionString;

        public static MySqlConnection getConnection(){
            return new MySqlConnection(pokeDB._ConnectionString);
        }

    }

}
