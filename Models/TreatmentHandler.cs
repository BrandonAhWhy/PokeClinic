using MySql.Data.MySqlClient;
using PokeClinic.Models.BuilderFactoryVibes;
using System.Linq;
using Dapper;

namespace PokeClinic.Models
{
    public class TreatmentHandler
    {
        public static ITreatment getAvailibleTreatment(string type) {
            ITreatment treatment = Restoration.getTreatment(type);
            string[] treatmentItems = treatment.getNeededItems();
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

            Inventory[] availableItems = sqlRunner(sql);
            foreach (Inventory item in availableItems)
            {
                if (item.ItemQuantity <= 0)
                {
                    return null;
                }
            }
            return treatment;
        }

        public static Inventory[] sqlRunner(string sql){
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
    }
}