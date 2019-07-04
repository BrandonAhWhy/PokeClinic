<<<<<<< HEAD
using System;  
using System.Collections.Generic;  
using System.Linq;  
using System.Data;  
using System.Data.SqlClient;  
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;  
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Dapper;

namespace PokeClinic.Repository
{
    public interface IRepository<T>
    {
        Task<bool> AddOrUpdate(T newEntity);
        Task<T> Find(string Entity);
        Task<IEnumerable<T>> GetAll();
    }
=======
using System;
using System.Collections.Generic;

namespace PokeClinic.Models
{
     public interface IRepository<T>
     {
          Int64 Id { get; set; }
          void Add(T newEntity);
          void Remove(T entity);
     }
>>>>>>> testing/Mikhail_Inventory-Read
}