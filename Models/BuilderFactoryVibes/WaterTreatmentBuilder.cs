namespace PokeClinic.Models.BuilderFactoryVibes {
    public class WaterTreatmentBuilder : ITreatmentPlanBuilder {

        TreatmentPlan restorerItem;

        public WaterTreatmentBuilder () {
            this.restorerItem = new TreatmentPlan ();
        }

        public void buildHealType () {
            this.restorerItem.setHealType ("Water");
        }

        public void buildHealValue () {
            this.restorerItem.setHealItem ("Oran");
        }

        public void buildHealPoison () {
            this.restorerItem.setHealPoisonItem ("Rawst");
        }

        public void buildHealParalyse () {
            this.restorerItem.setHealParalyseItem ("Kee");
        }

        public void buildHealSleep () {
            this.restorerItem.setHealSleepItem ("Pecha");
        }

        public TreatmentPlan getRestorer () {
            return restorerItem;
        }
    }
}
