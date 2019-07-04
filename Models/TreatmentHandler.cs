using MySql.Data.MySqlClient;

namespace PokeClinic.Models
{
    public class TreatmentHandler
    {
        public static TreatmentReturn getAvailability(string type){
            return new TreatmentReturn();
        }

        public void sqlRunner(string SQL){
            using (MySqlConnection conn = PokeDB.NewConnection())
            {
                try {
                    
                }
                catch{
                
                }
            }

        }
    }
}