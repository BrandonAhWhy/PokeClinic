namespace PokeClinic.Models.BuilderFactoryVibes {
    public interface ITreatmentPlanBuilder {
        void buildHealType ();

        void buildHealValue ();

        void buildHealPoison ();

        void buildHealParalyse ();

        void buildHealSleep ();

        TreatmentPlan getRestorer ();
    }
}