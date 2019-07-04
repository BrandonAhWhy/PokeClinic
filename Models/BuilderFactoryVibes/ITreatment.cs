namespace PokeClinic.Models.BuilderFactoryVibes {
    public interface ITreatment {
        void setHealType (string type);

        void setHealItem (string value);

        void setHealPoisonItem (string effect);

        void setHealParalyseItem (string effect);

        void setHealSleepItem (string effect);

        string[] getNeededItems();

        string toString ();
    }
}