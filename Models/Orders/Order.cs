using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Dapper;
using Dapper.Contrib.Extensions;


namespace PokeClinic.Models.Orders
{
    public class Order
    {
        public Int64 id { get; set; }
        public DateTime OrderDate { get; set; }


        public bool Add()
        {
            bool success = false;
            string sql = "INSERT INTO `order` (order_date) VALUES (@OrderDate);";
            this.OrderDate = DateTime.Now;
            using (MySqlConnection conn = PokeDB.NewConnection())
            {
                try {
                    int affectedRows = conn.Execute(sql, this);
                    success = (affectedRows > 0) ? true : false;
                }catch(Exception err){
                    Console.Write("SQL ADD ERR: " + err.Message);
                    return false;
                }
            }
            return success;
        }

        public static IEnumerable<Order> GetAll(Int64 limit, Int64 offset)
        {
            string sql = "SELECT id, order_date AS orderDate FROM `order` LIMIT @Limit OFFSET @Offset ";
            IEnumerable<Order> orders = null;

            using (MySqlConnection conn = PokeDB.NewConnection())
            {
                try{
                    orders = conn.Query<Order>(sql, new { Offset = offset, Limit = limit });
                }catch(Exception err){
                    Console.WriteLine(err.Message);
                    return null;
                }
            }
            return orders;
        }

        //UPDATE
        public bool Update()
        {
            string sql = "UPDATE `order` SET order_date = @OrderDate";
            bool success = false;
            using (MySqlConnection conn = PokeDB.NewConnection())
            {
                var affectedRows = conn.Execute(sql, this);
                success = (affectedRows > 0) ? true : false;
            }
            return success;
        }


        // DELETE
        public bool Delete()
        {
            string sql = "DELETE FROM `order` WHERE id = @Id";
            bool success = false;

            using (MySqlConnection conn = PokeDB.NewConnection())
            {
                var affectedRows = conn.Execute(sql, this);
                success = (affectedRows > 0) ? true : false;
            }
            return success;
        }
        
        public static Order Get(Int64 id)
        {
            string sql = "SELECT id, order_date AS orderDate FROM `order` WHERE id = @Id";
            Order order = null;

            using (MySqlConnection conn = PokeDB.NewConnection())
            {
                try{
                    order = conn.QueryFirst<Order>(sql, new { Id = id });
                }catch(Exception err){
                    Console.WriteLine(err.Message);
                    return null;
                }
            }
            return order;
        }
    }
}