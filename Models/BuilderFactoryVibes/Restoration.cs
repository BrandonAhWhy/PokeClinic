using System;

namespace PokeClinic.Models.BuilderFactoryVibes {
    public class Restoration {
        public static ITreatment grassTreatment;
        public static ITreatment fireTreatment;
        public static ITreatment waterTreatment;
        public static void main(){
            ITreatmentPlanBuilder grassTreatmentBuilder = new GrassTreatmentBuilder ();
            ITreatmentPlanBuilder fireTreatmentBuilder = new FireTreatmentBuilder ();
            ITreatmentPlanBuilder waterTreatmentBuilder = new WaterTreatmentBuilder ();

            RestorerDirector rd = new RestorerDirector (grassTreatmentBuilder);
            rd.makeRestorer ();
            grassTreatment = rd.getTreatmentPlan();

            rd.setRestorerFormat (fireTreatmentBuilder);
            rd.makeRestorer();
            fireTreatment = rd.getTreatmentPlan ();

            rd.setRestorerFormat (waterTreatmentBuilder);
            rd.makeRestorer ();
            waterTreatment = rd.getTreatmentPlan ();

            Console.WriteLine ("Grass:\n", grassTreatment);
            Console.WriteLine ("Fire:\n", fireTreatment);
            Console.WriteLine ("Water:\n", waterTreatment );
        }

        public static string[] getTreatmentItems(string type){
            switch(type){
                case "grass":
                    return grassTreatment.getNeededItems();
                case "fire":
                    return fireTreatment.getNeededItems();
                case "water":
                    return waterTreatment.getNeededItems();
                default:
                    return null;

            }
        }
    }
}