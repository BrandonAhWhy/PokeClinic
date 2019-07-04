namespace PokeClinic.Models.BuilderFactoryVibes {
    public class SitrusRestorerBuilder : IRestoreBuilder {

        RestoreItem restorerItem;

        public SitrusRestorerBuilder () {
            this.restorerItem = new RestoreItem ();
        }

        public void buildHealType () {
            this.restorerItem.setHealType ("Water");
        }

        public void buildHealValue () {
            this.restorerItem.setHealValue (50);
        }

        public void buildHealPoison () {
            this.restorerItem.setHealPoison (false);
        }

        public void buildHealParalyse () {
            this.restorerItem.setHealParalyse (false);
        }

        public void buildHealSleep () {
            this.restorerItem.setHealSleep (true);
        }

        public RestoreItem getRestorer () {
            return restorerItem;
        }
    }
}