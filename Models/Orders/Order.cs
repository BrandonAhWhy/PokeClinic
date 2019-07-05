using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Dapper;
using Dapper.Contrib.Extensions;
using PokeClinic.Models.Requests;


namespace PokeClinic.Models.Orders
{
    public class Order
    {
        public Int64 Id { get; set; }
        public DateTime OrderDate { get; set; }

        public IEnumerable<Models.Orders.ItemOrder> ItemOrders { get; set; }

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
                        foreach (var order in orders)
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
        public bool Update(IEnumerable<Models.Orders.ItemOrder> items)
        {
            string sql = "REPLACE INTO `item_order` (order_id,item_id, quantity) VALUES (@OrderID, @ItemId, @Quantity)";
            // string sql = "INSERT INTO `item_order` (order_id,item_id, quantity) VALUES (@OrderID, @ItemId, @Quantity) ON DUPLICATE KEY UPDATE item_order SET quantity = @Quantity WHERE item_id = @ItemId AND order_id = @OrderId";
            string sql2 = "UPDATE `order` SET order_date = @OrderDate WHERE id = @Id";
     
            bool success = false;
            this.OrderDate =  DateTime.Now;
            this.ItemOrders = items;

            var self = this;

            using (MySqlConnection conn = PokeDB.NewConnection())
            {
                conn.Open();
                using (var transactionScope = conn.BeginTransaction())
                {
                    foreach(var item in ItemOrders){
                        conn.Execute(sql, new {Quantity = item.Quantity, OrderId = this.Id, ItemId = item.ItemId});
                    }
                    conn.Execute(sql2, self);
                    transactionScope.Commit();
                    success = true;
                }
            }
            return success;
        }


        // DELETE
        public bool Delete()
        {
            string sql = "DELETE FROM `item_order` WHERE order_id = @Id";
            string sql2 = "DELETE FROM `order` WHERE id = @Id";
            bool success = false;

            var self = this;
            using (MySqlConnection conn = PokeDB.NewConnection())
            {
                conn.Open();
                using (var transactionScope = conn.BeginTransaction())
                {
                    var affectedRows = conn.Execute(sql, self);
                    var affectedRows2 = conn.Execute(sql2, self);
                    transactionScope.Commit();
                    success = true;
                }
            }
            return success;
        }

        public bool Receive()
        {
            string deleteOrderItems = "DELETE FROM `item_order` WHERE order_id = @Id";
            string deleteOrder = "DELETE FROM `order` WHERE id = @Id";
            string updateItems = "UPDATE inventory SET itemQuantity = itemQuantity + @Quantity WHERE id = @ItemId";


            bool success = false;

            var self = this;
            using (MySqlConnection conn = PokeDB.NewConnection())
            {
                conn.Open();
                using (var transactionScope = conn.BeginTransaction())
                {
                    conn.Execute(deleteOrderItems, self);
                    conn.Execute(deleteOrder, self);

                    foreach(var item in self.ItemOrders){
                        conn.Execute(updateItems, item);
                    }
                    transactionScope.Commit();
                    success = true;
                }
            }
            return success;
        }

        //GET ID
        public static Order Get(Int64 id)
        {
            string sql = "SELECT id, order_date AS orderDate FROM `order` WHERE id = @Id";
            string sql2 = "SELECT order_id AS OrderID, item_id AS ItemID, quantity AS Quantity FROM item_order WHERE order_id = @Id";
            Order order = null;
            
            using (MySqlConnection conn = PokeDB.NewConnection())
            {
                try
                {
                    order = conn.QueryFirst<Order>(sql, new { Id = id });
                    var itemList = conn.Query<ItemOrder>(sql2, new { Id = id });
                    order.ItemOrders = itemList;
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