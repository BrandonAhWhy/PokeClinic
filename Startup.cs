
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PokeClinic.Models;

using Microsoft.IdentityModel.Tokens;
// using System.Text;
 using Microsoft.AspNetCore.Authentication.JwtBearer;
// using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Authentication;

namespace PokeClinic
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            PokeDB._ConnectionString = Configuration.GetConnectionString("Default");
            //use for debugging token validation
            Microsoft.IdentityModel.Logging.IdentityModelEventSource.ShowPII = true;
            PokeDB.Secret = Configuration.GetSection("AppSettings")["Secret"].ToString();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
