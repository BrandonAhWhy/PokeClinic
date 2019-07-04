namespace PokeClinic.Models.BuilderFactoryVibes
{
    public class RestorerDirector{
            private ITreatmentPlanBuilder restorerBuilder;

            public RestorerDirector(ITreatmentPlanBuilder builderRestorerFormat){
                this.restorerBuilder = builderRestorerFormat;
            }

            public void setRestorerFormat(ITreatmentPlanBuilder builderRestorerFormat){
                this.restorerBuilder = builderRestorerFormat;
            }

            public ITreatment getTreatmentPlan(){
                return this.restorerBuilder.getRestorer();
            }

            public void makeRestorer(){
                this.restorerBuilder.buildHealParalyse();
                this.restorerBuilder.buildHealPoison();
                this.restorerBuilder.buildHealSleep();
                this.restorerBuilder.buildHealType();
                this.restorerBuilder.buildHealValue();
            }
        }
}