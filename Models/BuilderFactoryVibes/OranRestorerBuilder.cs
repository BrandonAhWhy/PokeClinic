namespace PokeClinic.Models.BuilderFactoryVibes
{
    public class OranRestorerBuilder: IRestoreBuilder{

            RestoreItem restorerItem;

            public OranRestorerBuilder(){
                this.restorerItem = new RestoreItem();
            }

            public void buildHealType(){
                this.restorerItem.setHealType("Fire");
            }

            public void buildHealValue(){
                this.restorerItem.setHealValue(10);
            }

            public void buildHealPoison(){
                this.restorerItem.setHealPoison(false);
            }

            public void buildHealParalyse(){
                this.restorerItem.setHealParalyse(true);
            }

            public void buildHealSleep(){
                this.restorerItem.setHealSleep(false);
            }

            public RestoreItem getRestorer(){
                return restorerItem;
            }
        }
}