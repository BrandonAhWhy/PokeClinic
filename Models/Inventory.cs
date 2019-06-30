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
        
        public Int64 Id { get; set; }
        public string Name { get; set; }
        public Int64 ItemQuantity { get; set; }
        public string ItemPrice { get; set; }


         public bool Add(Inventory _inventory)
        {
            string sql = "INSERT INTO inventory (name, itemQuantity, itemPrice) VALUES (@Name, @ItemQuantity, @ItemPrice)";
            bool success = false;

            using (MySqlConnection conn = PokeDB.NewConnection())
            {
                var affectedRows = conn.Execute(sql, new Inventory{
                    Name = _inventory.Name,
                    ItemPrice = _inventory.ItemPrice,
                    ItemQuantity = _inventory.ItemQuantity
                });
                success = (affectedRows > 0) ? true : false;
            }
            return success;
        }
        // READ
        public static Inventory Get(int id)
        {
            string sql = "SELECT * FROM inventory WHERE id = @Id";
            Inventory Inventory = null;

            using (MySqlConnection conn = PokeDB.NewConnection())
            {
                Inventory = conn.QueryFirst<Inventory>(sql, new { Id = id });
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
    }   
}