using System;  
using System.Collections.Generic;  
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

<<<<<<< HEAD
        public async Task<IEnumerable<Inventory>> GetAll() {
            return await InventoryRepository.GetAll();
        }

        public async Task<bool> AddOrUpdate(Inventory item) {
            return await InventoryRepository.AddOrUpdate(item);
        }

        public async Task<Inventory> Find(string Name) {
            return await InventoryRepository.Find(Name);
=======

         public bool AddOrUpdate(Inventory _inventory)
        {
            string sqlFind = "SELECT * from inventory where name = @Name";
            string sqlUpdate = "UPDATE inventory SET name = @Name, itemQuantity = @ItemQuantity, restorationAmount = @RestorationAmount, typeLimitation = @TypeLimitation where Id = @Id";
            string sql = "INSERT INTO inventory (name, itemQuantity, restorationAmount, typeLimitation) VALUES (@Name, @ItemQuantity, @RestorationAmount, @TypeLimitation )";

            bool success = false;

            Inventory inventory = new Inventory();

            using (MySqlConnection conn = PokeDB.NewConnection())
            {
                try {
                    inventory = conn.QueryFirst<Inventory>(sqlFind, _inventory);
                    if (inventory != null) {
                        inventory.ItemQuantity += _inventory.ItemQuantity;
                        conn.Execute(sqlUpdate, inventory);
                        return success = true;
                    }
                }
                catch{
                var affectedRows = conn.Execute(sql, this);
                success = (affectedRows > 0) ? true : false;   
                }
            }
            return success;
        }

        //DELETE
        public bool Delete(Int64 id){
            string sqlFind = "SELECT * from inventory  WHERE Id = @Id";
            string sqlDelete = "DELETE FROM inventory WHERE Id = @Id";
            Inventory inventory = new Inventory();
            bool success = false;
            using (MySqlConnection conn = PokeDB.NewConnection()){
                try{
                    inventory = conn.QueryFirst<Inventory>(sqlFind,new {Id = id});
                    if(inventory != null){
                        conn.Execute(sqlDelete, new {Id = id});
                        return true;
                    }
                }
                catch{
                    var affectedRows = conn.Execute(sqlDelete, this);
                    success = (affectedRows > 0) ? true : false; 
                }
            }
            return success;
        }


        // READ
        public static Inventory Get(string name)
        {
            string sql = "SELECT * FROM inventory WHERE name = @Name";
            Inventory Inventory = null;

            using (MySqlConnection conn = PokeDB.NewConnection())
            {
                Inventory = conn.QueryFirst<Inventory>(sql, new { Name = name });
            }
            return Inventory;
        }

        public static IEnumerable<Inventory> GetAll()
        {
            string sql = "SELECT * FROM inventory";
            IEnumerable<Inventory> inventory = null;

            using (MySqlConnection conn = PokeDB.NewConnection())
            {
                inventory = conn.Query<Inventory>(sql);
            }
            return inventory;
>>>>>>> testing/Mikhail_Inventory-Read
        }
    }   
}