namespace PokeClinic.Models.BuilderFactoryVibes
{
    public class RestoreItem: IRestoreItem{
        private string typeRestriction;
            private int healValue;
            private bool healPoison;
            private bool healParalyse;
            private bool healSleep;

            public void setHealType(string type){
                this.typeRestriction = type;
            }
            public string getTypeRestriction(){ return this.typeRestriction; }

            public void setHealValue(int value){
                this.healValue = value;
            }
            public int getHealValue(){ return this.healValue; }

            public void setHealPoison(bool effect){
                this.healPoison = effect;
            }
            public bool getHealPoison(){ return this.healPoison; }

            public void setHealParalyse(bool effect){
                this.healParalyse = effect;
            }
            public bool getHealParalyse(){ return this.healParalyse; }

            public void setHealSleep(bool effect){
                this.healSleep = effect;
            }
            public bool getHealSleep(){ return this.healSleep; }

            public string toString(){
                return "This item has the following effects:\nHeals " + this.typeRestriction + " type(s)\n" + 
                        (this.healPoison ? "Heals poison\n" : "") + 
                        (this.healParalyse ? "Heals paralysis\n" : "") + 
                        (this.healSleep ? "Heals sleep\n" : "") +
                        "and heals " + this.healValue + " health points.";
            }
    }
}