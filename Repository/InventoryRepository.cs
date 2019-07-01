using System;  
using System.Collections.Generic;  
using System.Linq;  
using System.Data;  
using System.Data.SqlClient;  
using System.ComponentModel.DataAnnotations;  
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Dapper;

namespace PokeClinic.Repository {

    public class InventoryRepository
    {
         public static bool AddOrUpdate(Inventory _inventory)
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
                var affectedRows = conn.Execute(sql, _inventory);
                success = (affectedRows > 0) ? true : false;   
                }
            }
            return success;
        }
        // READ
        public static Inventory Find(string name)
        {
            string sql = "SELECT * FROM inventory WHERE name = @Name";
            Inventory Inventory = null;

            using (MySqlConnection conn = PokeDB.NewConnection())
            {
                Inventory = conn.QueryFirst<Inventory>(sql, new { name = name });
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
        }

        // public static Inventory Remove(string name) {
        // }
    }
}