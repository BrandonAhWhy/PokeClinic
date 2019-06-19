using MySql.Data.MySqlClient;
using System;

namespace PokeClinic
{

    public class pokeDB
    {
        public static string _ConnectionString;

        public static MySqlConnection getConnection(){
            Console.WriteLine(pokeDB._ConnectionString + '\n');
            return new MySqlConnection(pokeDB._ConnectionString);
        }

      
        
    }
}
