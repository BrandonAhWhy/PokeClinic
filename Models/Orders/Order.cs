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
        public Int64 Id { get; set; }
        public DateTime OrderDate { get; set; }


        public bool Add(IEnumerable<Models.Requests.OrderAdd> orders)
        {
            bool success = false;
            string createOrder = "INSERT INTO `order` (order_date) VALUES (@OrderDate); SELECT LAST_INSERT_ID() AS Id;";
            string insertOrder = "INSERT INTO `item_order` (order_id,item_id, quantity) VALUES (@OrderID, @ItemId, @Quantity);";

            this.OrderDate = DateTime.Now;

            var self = this;
            using (MySqlConnection conn = PokeDB.NewConnection())
            {
                conn.Open();
                using (var transactionScope = conn.BeginTransaction())
                {
                    try
                    {
                        //create order
                        self.Id = conn.ExecuteScalar<Int64>(createOrder, self);
                        //add to item_order
                        foreach(var order in orders)
                        {
                            order.OrderId = self.Id;
                            conn.Execute(insertOrder, order);
                        }
                        transactionScope.Commit();
                        success = true;
                    }
                    catch (Exception err)
                    {
                        Console.Write("SQL ADD ERR: " + err.Message);
                        success = false;
                    }
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
                try
                {
                    orders = conn.Query<Order>(sql, new { Offset = offset, Limit = limit });
                }
                catch (Exception err)
                {
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
                try
                {
                    order = conn.QueryFirst<Order>(sql, new { Id = id });
                }
                catch (Exception err)
                {
                    Console.WriteLine(err.Message);
                    return null;
                }
            }
            return order;
        }
    }
}