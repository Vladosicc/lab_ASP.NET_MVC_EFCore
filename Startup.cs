using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using laba5_oop.Managers.BrandOfDetails;
using laba5_oop.Managers.CarForSolds;
using laba5_oop.Managers.Categories;
using laba5_oop.Managers.Details;
using laba5_oop.Managers.ModelCars;
using laba5_oop.Managers.Orders;
using laba5_oop.Storage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace laba5_oop
{
    public class Startup
    {
        IConfigurationRoot configurationRoot;


        public Startup(Microsoft.AspNetCore.Hosting.IHostingEnvironment configuration)
        {
            configurationRoot = new ConfigurationBuilder().SetBasePath(configuration.ContentRootPath).AddJsonFile("AutoDbSetting.json").Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AutoDataContext>(options => options.UseSqlServer(configurationRoot.GetConnectionString("AutoDb")));
            services.AddControllersWithViews();
            services.AddTransient<ICategoryManager, CategoryManager>();
            services.AddTransient<IModelCarManager, ModelCarManager>();
            services.AddTransient<IDetailManager, DetailManager>();
            services.AddTransient<IOrderManager, OrderManager>();
            services.AddTransient<IBrandOfDetailManager, BrandOfDetailManager>();
            services.AddTransient<ICarForSoldManager, CarForSoldManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
