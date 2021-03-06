using AppCore.DataAccess.Configs;
using Business.Services;
using Business.Services.Bases;
using DataAccess.EntityFramework.Contexts;
using DataAccess.EntityFramework.Repositories;
using DataAccess.EntityFramework.Repositories.Bases;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcWebUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            #region IoC Container
            // IoC Container kütüphaneleri: Autofac, Ninject
            //service.AddScpoed() // istek(request) boyunca objenin referansını (genelde interface veya abstract class) kullandığımız yerde obje(somut class'tan oluşturulacak)
            //bir kere oluşturulur ve yanıt(response) dönene kadar bu obje hayatta kalır.
            //services.AddSingleton() //web uygulaması başladığın'da objenin referanasını (genelde interface veya abstract class) kullandığımız yerde obje (somut class'tan oluşturulacak)
            //bir kere oluşturulur ve uygulama çalıştığı süre (IIS üzerinden uygulama durdurulmadığı veya yeniden başlatılmadığı) sürece bu obje hayatta kalır.
            //services.AddTransient() // istek (request) bağımsız ihtiyaç olan objenin referansı  (genelde interface veya abstract class) kullandığımız her yerde bu objeni new'ler.


            ConnectionConfig.ConnectionString = Configuration.GetConnectionString("ETradeContext");
            services.AddScoped<DbContext,ETradeContext>();
            services.AddScoped<ProductRepositoryBase, ProductRepository>();
            services.AddScoped<CategoryRepositoryBase, CategoryRepository>();
            services.AddScoped<IProductService, ProductService>();
             services.AddScoped<ICategoryService, CategoryService>();
            #endregion
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
