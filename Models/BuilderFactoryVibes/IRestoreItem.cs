namespace PokeClinic.Models.BuilderFactoryVibes
{
    interface IRestoreItem{
        void setHealType(string type);

            void setHealValue(int value);

            void setHealPoison(bool effect);

            void setHealParalyse(bool effect);

            void setHealSleep(bool effect);

            string toString();
    }
}