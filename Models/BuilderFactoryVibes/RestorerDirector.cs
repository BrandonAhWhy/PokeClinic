namespace PokeClinic.Models.BuilderFactoryVibes
{
    public class RestorerDirector{
            private IRestoreBuilder restorerBuilder;

            public RestorerDirector(IRestoreBuilder builderRestorerFormat){
                this.restorerBuilder = builderRestorerFormat;
            }

            public void setRestorerFormat(IRestoreBuilder builderRestorerFormat){
                this.restorerBuilder = builderRestorerFormat;
            }

            public RestoreItem getRestoreItem(){
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