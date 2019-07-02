using System;  
using System.Collections.Generic;  
using System.Linq;  
using System.Data;  
using System.Data.SqlClient;  
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;  
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using PokeClinic.Repository;

namespace PokeClinic
{
    public class Inventory {
        
        public Int64 Id { get; set; }
        public string Name { get; set; }
        public Int64 ItemQuantity { get; set; }
        public string RestorationAmount { get; set; }
        public string TypeLimitation {get; set; }

        public static async Task<IEnumerable<Inventory>> GetAll() {
            return await InventoryRepository.GetAll();
        }

        public static async Task<bool> AddOrUpdate(Inventory item) {
            return await InventoryRepository.AddOrUpdate(item);
        }

        public static async Task<Inventory> Find(string Name) {
            return await InventoryRepository.Find(Name);
        }
      
    }   
}