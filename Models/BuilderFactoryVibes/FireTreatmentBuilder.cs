namespace PokeClinic.Models.BuilderFactoryVibes
{
    public class FireTreatmentBuilder: ITreatmentPlanBuilder{

            TreatmentPlan restorerItem;

            public FireTreatmentBuilder(){
                this.restorerItem = new TreatmentPlan();
            }

            public void buildHealType(){
                this.restorerItem.setHealType("Fire");
            }

            public void buildHealValue(){
                this.restorerItem.setHealItem("Potion");
            }

            public void buildHealPoison(){
                this.restorerItem.setHealPoisonItem("Oran");
            }

            public void buildHealParalyse(){
                this.restorerItem.setHealParalyseItem("Rawst");
            }

            public void buildHealSleep(){
                this.restorerItem.setHealSleepItem("Pinap");
            }

            public TreatmentPlan getRestorer(){
                return restorerItem;
            }
        }
}