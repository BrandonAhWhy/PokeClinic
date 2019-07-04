using MySql.Data.MySqlClient;
using PokeClinic.Models.BuilderFactoryVibes;
using System.Linq;
using Dapper;
using System;

namespace PokeClinic.Models
{
    public class TreatmentHandler
    {
        public static TreatmentReturn getAvailibleTreatment(string type) {
            ITreatment treatment = Restoration.getTreatment(type);
            string[] treatmentItems = treatment.getNeededItems();
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
            foreach (Inventory item in availableItems)
            {
                Console.WriteLine(item.Name + " "+ item.ItemQuantity);
                if (item.ItemQuantity <= 0)
                {
                    returnVal.available = false;
                    returnVal.items.Append(item.Name);
                }
            }
            Console.WriteLine(returnVal.available);
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