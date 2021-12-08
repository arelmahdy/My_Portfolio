using Core.Interfaces;
using Infrastructure;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web
{
    public class Startup
    {
        private readonly IConfiguration configration;

        public Startup(IConfiguration configration)
        {
            // Allow To Reach To Connection String
           this.configration = configration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(configration.GetConnectionString("MyPortfolioDB"));
            });
            services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
        }
        
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseStaticFiles(); // To Useing Static Files

            app.UseEndpoints(endpoints =>
            {
                // define url Route
                endpoints.MapControllerRoute
                (
                "defaultRoute",
                "{Controller=Home}/{Action=Index}/{id?}"
                );
            });
        }
    }
}
