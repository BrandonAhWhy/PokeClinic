using System;  
using System.Collections.Generic;  
using System.Linq;  
using System.Data;  
using System.Data.SqlClient;  
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;  
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using PokeClinic.Models;
using Dapper;

namespace PokeClinic.Repository {

    public class InventoryRepository :IRepository<Inventory> 
    {
         public bool AddOrUpdate(Inventory _inventory)
        {
            string sqlUpdate = "UPDATE inventory SET name = @Name, itemQuantity = @ItemQuantity, restorationAmount = @RestorationAmount, typeLimitation = @TypeLimitation where Id = @Id";
            string sql = "INSERT INTO inventory (name, itemQuantity, restorationAmount, typeLimitation) VALUES (@Name, @ItemQuantity, @RestorationAmount, @TypeLimitation )";

            bool success = false;

            Inventory inventory = new Inventory();

            using (MySqlConnection conn = PokeDB.NewConnection())
            {
                try {
                    inventory =  Find(_inventory.Name);
                    if (inventory != null) {
                        inventory.ItemQuantity += _inventory.ItemQuantity;
                        conn.Execute(sqlUpdate, inventory);
                        return success = true;
                    }
                }
                catch
                {                                 
                    var affectedRows =  conn.Execute(sql, _inventory);
                    success = (affectedRows > 0) ? true : false;   
                }
            }
            return success;
        }
        // READ
        public Inventory Find(string name)
        {
            string sql = "SELECT * FROM inventory WHERE name = @Name";
            Inventory Inventory = null;

            using (MySqlConnection conn = PokeDB.NewConnection())
            {
                Inventory =  conn.QueryFirst<Inventory>(sql, new { name = name });
            }
            return Inventory;
        }

        public IEnumerable<Inventory> GetAll()
        {
            string sql = "SELECT * FROM inventory";

            using (MySqlConnection conn = PokeDB.NewConnection())
            {
                var InventoryCollection =   conn.Query<Inventory>(sql);
                return InventoryCollection;
            }
        }

         public bool Delete(string name) {
            string sqlDelete = "DELETE FROM inventory WHERE Id = @Id";
            bool success = false;
            Inventory inventory = new Inventory();
            using (MySqlConnection conn = PokeDB.NewConnection()){
                try
                {
                    inventory =  Find(name);
                    var affectedRows =  conn.Execute(sqlDelete, inventory);
                    success = (affectedRows > 0) ? true : false;
                }
                catch
                {
                    return success;
                }
            }
            return success;
        }
    }
}