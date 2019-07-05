using MySql.Data.MySqlClient;
using PokeClinic.Models.BuilderFactoryVibes;
using System.Linq;
using Dapper;
using System;
using PokeClinic.Repository;
using PokeClinic.Models.Orders;
using PokeClinic.Models.Requests;
namespace PokeClinic.Models
{
    public class TreatmentHandler
    {
        public static TreatmentReturn getAvailibleTreatment(string type) {
            Console.WriteLine("Type " + type);
            Restoration restoration = new Restoration();

            string[] treatmentItems = restoration.getTreatmentItems(type);
            TreatmentReturn returnVal = new TreatmentReturn();


            string sql = "SELECT * FROM `inventory` WHERE name IN (";
            foreach (string item in treatmentItems)
            {
                if (treatmentItems[0] != item)
                {
                    sql += ',';
                }
                sql += "'"+ item + "'";
            };
            sql += ')';
            
            Inventory[] availableItems = inventorySqlRunner(sql);
            foreach (string item in treatmentItems)
            {
                bool contained = false;
                foreach(Inventory invItem in availableItems){
                    if(invItem.Name.Equals(item) && invItem.ItemQuantity > 0)
                        contained = true;
                }
                
                if (!contained)
                {
                    returnVal.available = false;
                    // Code to copy the old array of needed items and add another item -> should become a list
                    string[] newItems = new string[returnVal.items.Length + 1];
                    Array.Copy(returnVal.items, newItems, returnVal.items.Length);
                    newItems[returnVal.items.Length] = item;
                    returnVal.items = newItems;

                    //add item to db
                    
                    //order
                    InventoryRepository invRepo = new InventoryRepository();
                    Inventory inventory = invRepo.Find(item);
                    OrderAdd orderAdd = new OrderAdd();
                    orderAdd.ItemId = inventory.Id;
                    orderAdd.Quantity = 9001;   //this is a business rule---- get off my back
                                                //also... it's over 9000 (it's a pop-culture)
                    Order order = new Order();
                    order.Add(new OrderAdd[]{orderAdd});
                }
            }

            if(returnVal.available){
                returnVal.items = treatmentItems;
            }
            return returnVal;
        }

        public static Inventory[] inventorySqlRunner(string sql){
            using (MySqlConnection conn = PokeDB.NewConnection())
            {
                try {
                    Inventory[] treatmentItems = conn.Query<Inventory>(sql).ToList().ToArray();
                    return treatmentItems;
                }
                catch{
                    return null;
                }
            }
        }

        public static Schedule[] scheduleSqlRunner(string sql){
            using (MySqlConnection conn = PokeDB.NewConnection())
            {
                try {
                    Schedule[] schedule = conn.Query<Schedule>(sql).ToList().ToArray();
                    return schedule;
                }
                catch{
                    return null;
                }
            }
        }
    }
}