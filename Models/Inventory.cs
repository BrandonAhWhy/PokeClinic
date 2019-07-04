using System;  
using System.Collections.Generic;  
using System.Threading.Tasks;
using PokeClinic.Repository;
using System.Linq;  
using System.Data;  
using System.Data.SqlClient;  
using System.ComponentModel.DataAnnotations;  
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Dapper;

namespace PokeClinic
{
    public class Inventory {

        private InventoryRepository InventoryRepository;
        public Inventory() {
            InventoryRepository = new InventoryRepository();
        }
        
        public Int64 Id { get; set; }
        public string Name { get; set; }
        public Int64 ItemQuantity { get; set; }
        public string RestorationAmount { get; set; }

        public string TypeLimitation {get; set; }

        public async Task<IEnumerable<Inventory>> GetAll() {
            return await InventoryRepository.GetAll();
        }

        public async Task<bool> AddOrUpdate(Inventory item) {
            return await InventoryRepository.AddOrUpdate(item);
        }

        public async Task<Inventory> Find(string Name) {
            return await InventoryRepository.Find(Name);
        }
    }   
}