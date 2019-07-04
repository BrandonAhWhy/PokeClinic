using MySql.Data.MySqlClient;

namespace PokeClinic.Models
{
    public class TreatmentHandler
    {
        public static TreatmentReturn getAvailability(string type){
            TreatmentReturn tr;
            return new TreatmentReturn();
        }

        public void sqlRunner(string SQL){
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
        }
    }
}