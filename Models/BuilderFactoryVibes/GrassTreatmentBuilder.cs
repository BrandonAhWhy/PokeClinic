namespace PokeClinic.Models.BuilderFactoryVibes {
    public class GrassTreatmentBuilder : ITreatmentPlanBuilder {

        TreatmentPlan restorerItem;

        public GrassTreatmentBuilder () {
            this.restorerItem = new TreatmentPlan ();
        }

        public void buildHealType () {
            this.restorerItem.setHealType ("Grass");
        }

        public void buildHealValue () {
            this.restorerItem.setHealItem ("Potion");
        }

        public void buildHealPoison () {
            this.restorerItem.setHealPoisonItem ("Cheri");
        }

        public void buildHealParalyse () {
            this.restorerItem.setHealParalyseItem ("Pinap");
        }

        public void buildHealSleep () {
            this.restorerItem.setHealSleepItem ("Rawst");
        }

        public TreatmentPlan getRestorer () {
            return restorerItem;
        }
    }
}