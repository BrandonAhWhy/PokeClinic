using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace PokeClinic.Models
{
     public interface IRepository
     {
          Int64 Id { get; set; }
          bool Add();
          bool Update();
          bool Delete();
     }
}