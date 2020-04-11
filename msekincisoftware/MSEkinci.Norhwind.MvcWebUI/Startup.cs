using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using MSEkinci.Northwind.MvcWebUI.Middlewares;
using MSEkinci.Northwind.Business.Abstract;
using MSEkinci.Northwind.Business.Concrete;
using MSEkinci.Northwind.DataAccess.Abstract;
using MSEkinci.Northwind.DataAccess.Concrete.EntityFramework;
using MSEkinci.Northwind.MvcWebUI.Services;
using MSEkinci.Northwind.MvcWebUI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;

namespace MSEkinci.Northwind.MvcWebUI
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            /*              Scoped, Singleton,Transiet          */

            //Singleton: Sadece bir Interface intence'si oluşturur bütün kullanıcılar onu kullanır.
            //Scoped: Kullanıcı bir istekte bulunduğunda her istek için bir instance oluşur
            //Transiet: Singleton ile aynıdır fakat aynı anda iki aynı interface'e erişmek isterse sadece bir instance oluşturur.
            services.AddScoped<IProductService, ProductManager>();
            services.AddScoped<IProductDataAccessLayer, EfProductDataAccessLayer>();
            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<ICategoryDataAccessLayer, EfCategoryDataAccessLayer>();
            services.AddSingleton<ICartSessionService, CartSessionService>();
            services.AddSingleton<ICartService, CartService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<Services.ICipherService, Services.CipherService>();
            services.AddDbContext<CustomIdentityDbContext>(options => 
                     options.UseSqlServer(@"Server=Serkan-Ekinci;Database=Northwind;Trusted_Connection=true"));
            services.AddIdentity<CustomIdentityUser, CustomIdentityRole>()
                .AddEntityFrameworkStores<CustomIdentityDbContext>() //EntityFramework Ortamında tutulack
                .AddDefaultTokenProviders(); //kullanıcı bilgilerinin sayfalar arasında geçiş yaparken kullanılması için

            services.AddDistributedMemoryCache(); //Session Aktifleştirme
            services.AddSession();
            services.AddMvc();
    }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseNodeModules(env.ContentRootPath);

#pragma warning disable CS0618 // Type or member is obsolete
            app.UseIdentity();
#pragma warning restore CS0618 // Type or member is obsolete

            app.UseSession();
            app.UseMvc(ConfigureRoutes);
            
        }

        private void ConfigureRoutes(IRouteBuilder routeBuilder)
        {
            routeBuilder.MapRoute("Default", "{controller=Product}/{action=Index}/{id?}");
        }
    }
}
