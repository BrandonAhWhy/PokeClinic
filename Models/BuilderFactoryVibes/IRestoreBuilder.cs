namespace PokeClinic.Models.BuilderFactoryVibes
{
    public interface IRestoreBuilder{
            void buildHealType();

            void buildHealValue();

            void buildHealPoison();

            void buildHealParalyse();

            void buildHealSleep();

            RestoreItem getRestorer();
    }
}