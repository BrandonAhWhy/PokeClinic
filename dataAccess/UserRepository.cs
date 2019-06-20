using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using PokeClinic;
using PokeClinic.Models;
using Dapper;



namespace PokeClinic.DataAccess
{
    public class UserRepository : IRepository<User>
    {

        public UserRepository(){
        }
        public void Add(User _user)
        {
           
        }
        public void Delete(User author)
        {

        }

        public User Get(int id)
        {
            using (MySqlConnection conn =  pokeDB.getConnection()){
                conn.Open();
                var list = (List<User>)conn.Query<User>("select * from user where id = " + id.ToString());
                Console.WriteLine("#########################################");
                Console.WriteLine(list[0].Name);
                Console.WriteLine("#########################################");

                return new User();
            }
        }
        public List<User> GetAll()
        {
            throw new Exception("REEE") ;
        }
    }
}
