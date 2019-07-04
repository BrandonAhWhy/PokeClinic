using System;
using PokeClinic.Models.IRestoreBuilder;
using RestoreItem;

namespace PokeClinic.Models {
    public class FullHeal {

        public static void main(){
            IRestoreBuilder healAll = new PotionRestorerBuilder ();
            IRestoreBuilder healFire = new OranRestorerBuilder ();
            IRestoreBuilder healWater = new SitrusRestorerBuilder ();

            RestorerDirector rd = new RestorerDirector (healAll);
            rd.makeRestorer ();
            RestoreItem itemAll = rd.getRestoreItem ();

            rd.setRestorerFormat (healFire);
            rd.makeRestorer();
            RestoreItem itemFire = rd.getRestoreItem ();

            rd.setRestorerFormat (healWater);
            rd.makeRestorer ();
            RestoreItem itemWater = rd.getRestoreItem ();

            Console.WriteLine ("All:", itemAll);
            Console.WriteLine ("Fire:", itemFire);
            Console.WriteLine ("Water:", itemWater);
        }

    }
}