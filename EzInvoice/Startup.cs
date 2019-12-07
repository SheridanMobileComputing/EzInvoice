using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EzInvoice.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EzInvoice
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddMvc();
            //services.AddMvc(option => option.EnableEndpointRouting = false);

            services.AddSession(sessionOptions =>
            {
                sessionOptions.IdleTimeout = TimeSpan.FromSeconds(60);
            });

            services.AddControllersWithViews();
            services.AddDbContext<EZInvoiceDB>(optionsAction: options => options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=EZInvoice2.db")
            );

            //services.AddDbContext<EZInvoiceDB>(options => options.UseSqlite("Data Source=EZInvoice.db"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IWebHostEnvironment env)
        {
            //test
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSession();//this must be above app.UseMVC
            //app.UseMvcWithDefaultRoute();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "Default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                );
            });

            app.UseStatusCodePagesWithReExecute("/error/{0}");
        }
    }
}
