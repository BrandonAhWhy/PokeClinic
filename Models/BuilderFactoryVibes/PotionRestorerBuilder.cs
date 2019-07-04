namespace PokeClinic.Models.BuilderFactoryVibes
{
    public class PotionRestorerBuilder: IRestoreBuilder{

            RestoreItem restorerItem;

            public PotionRestorerBuilder(){
                this.restorerItem = new RestoreItem();
            }

            public void buildHealType(){
                this.restorerItem.setHealType("All");
            }

            public void buildHealValue(){
                this.restorerItem.setHealValue(20);
            }

            public void buildHealPoison(){
                this.restorerItem.setHealPoison(false);
            }

            public void buildHealParalyse(){
                this.restorerItem.setHealParalyse(false);
            }

            public void buildHealSleep(){
                this.restorerItem.setHealSleep(false);
            }

            public RestoreItem getRestorer(){
                return restorerItem;
            }
        }
}