namespace PokeClinic.Models
{
    public class TreatmentReturn{
        public bool available{ get; set;}
        public string[] items{ get; set;}

        public TreatmentReturn(){
            this.available = true;
        }
    }
}