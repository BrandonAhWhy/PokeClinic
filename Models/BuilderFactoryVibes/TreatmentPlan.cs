
namespace PokeClinic.Models.BuilderFactoryVibes {
    public class TreatmentPlan : ITreatment {
        private string typeRestriction;
        private string healItem;
        private string poisonItem;
        private string paralyseItem;
        private string sleepItem;

        public void setHealType (string type) {
            this.typeRestriction = type;
        }
        public string getTypeRestriction () { return this.typeRestriction; }

        public void setHealItem (string value) {
            this.healItem = value;
        }
        public string getHealValueItem () { return this.healItem; }

        public void setHealPoisonItem (string itemNeeded) {
            this.poisonItem = itemNeeded;
        }
        public string getHealPoisonItem () { return this.poisonItem; }

        public void setHealParalyseItem (string itemNeeded) {
            this.paralyseItem = itemNeeded;
        }
        public string getHealParalyse () { return this.paralyseItem; }

        public void setHealSleepItem (string itemNeeded) {
            this.sleepItem = itemNeeded;
        }
        public string getHealSleep () { return this.sleepItem; }

        public string[] getNeededItems(){
            return( new string[] {this.healItem, this.poisonItem, this.paralyseItem, this.sleepItem} );
        }

        public string toString () {
            return "This treatment works for " + this.typeRestriction + " type(s) and needs the following items:\n" +
                this.healItem + " for health\n" +
                this.poisonItem + " for poison\n" +
                this.paralyseItem + " for paralysis\n" +
                this.sleepItem + " for sleep";
        }
    }
}