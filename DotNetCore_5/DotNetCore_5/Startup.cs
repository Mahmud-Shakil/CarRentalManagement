using DotNetCore_5.DAL;
using DotNetCore_5.Data;
using DotNetCore_5.Models;
using FastReport.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static DotNetCore_5.DAL.Repositories;

namespace DotNetCore_5
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            FastReport.Utils.RegisteredObjects.AddConnection(typeof(MsSqlDataConnection));

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddIdentity<IdentityUser, ApplicationRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
            }).AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddRazorPages();
            services.AddMvc();
            services.AddScoped<ICustomer, CustomerRepository>();
            services.AddScoped<IVehicles, VehiclesRepository>();
            services.AddScoped<IVechileImage, VechileImageRepository>();
            services.AddScoped<IBooking, BookingRepositories>();
            services.AddScoped<IPaymentType, PaymentTypeRepository>();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }

            else
            {
                app.UseStatusCodePagesWithRedirects("/Error/{0}");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseFastReport();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute
               (
                   "Error", "error/{statusCode}",
                   defaults: new { controller = "Error", action = "HttpStatusCodeHandler" }
               );
                endpoints.MapControllerRoute(
                      name: "default",
                      pattern: "{controller=Home}/{action=Index}/{id?}");

            });
        }
    }
}
