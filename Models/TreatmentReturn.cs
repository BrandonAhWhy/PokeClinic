namespace PokeClinic.Models
{
    public class TreatmentReturn{
        public bool available{ get; set;}
        public string healthItem{ get; set;}
        public string poisonItem{ get; set;}
        public string paralyseItem{ get; set;}
        public string sleepItem{ get; set;}

        public TreatmentReturn(){
            available = true;
        }
    }
}