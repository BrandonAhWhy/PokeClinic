using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PokeClinic.Models.BuilderFactoryVibes;

namespace PokeClinic
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
            runBuilderFactoryVibes();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
        
        private static  void runBuilderFactoryVibes(){
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
<<<<<<< HEAD

}
