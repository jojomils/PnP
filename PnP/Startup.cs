using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using PnP.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace PnP
{

  public class Startup
  {

    public Startup(IConfiguration configuration) =>
        Configuration = configuration;

    public IConfiguration Configuration { get; }

    //private async Task CreateUserRoles(IServiceProvider serviceProvider)
    //{
    //  var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    //}

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddDbContext<ApplicationDbContext>(options =>
          options.UseSqlServer(
              Configuration["Data:PnPProducts:ConnectionString"]));

      services.AddDbContext<AppIdentityDbContext>(options =>
          options.UseSqlServer(
            Configuration["Data:PnPIdentity:ConnectionString"]));

      services.AddIdentity<IdentityUser, IdentityRole>()
        .AddEntityFrameworkStores<AppIdentityDbContext>()
        .AddDefaultTokenProviders();

      //Identity configurations
      services.Configure<IdentityOptions>(options =>
      {
        options.Password.RequireDigit = true;
        options.Password.RequiredLength = 8;
        options.Password.RequiredUniqueChars = 6;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireNonAlphanumeric = true;

        //To uncomment after 
        //options.User.RequireUniqueEmail = true;


      });

      services.ConfigureApplicationCookie(c => {
        c.Cookie.HttpOnly = true;
        c.ExpireTimeSpan = TimeSpan.FromMinutes(1);
        //c.LoginPath = "";
        //c.LogoutPath = "";
      });

      services.AddTransient<IProductRepository, EFProductRepository>();
      services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
      services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
      services.AddTransient<IOrderRepository, EFOrderRepository>();
      services.AddMvc();
      services.AddMemoryCache();
      services.AddSession();
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler("/Error/Error404");
      }
      app.UseStatusCodePages();
      app.UseStaticFiles();
      app.UseSession();
      app.UseAuthentication();
      app.UseMvc(routes => {
        routes.MapRoute(
            name: null,
            template: "{category}/Page{productPage:int}",
            defaults: new { controller = "Product", action = "List" }
        );

        routes.MapRoute(
            name: null,
            template: "Page{productPage:int}",
            defaults: new { controller = "Product", action = "List", productPage = 1 }
        );

        routes.MapRoute(
            name: null,
            template: "{category}",
            defaults: new { controller = "Product", action = "List", productPage = 1 }
        );

        routes.MapRoute(
            name: null,
            template: "",
            defaults: new { controller = "Product", action = "List", productPage = 1 });

       

        routes.MapRoute(name: null, template: "{controller}/{action}/{id?}");
      });
      SeedData.EnsurePopulated(app);
      IdentitySeedData.EnsurePopulated(app);
    }
  }
}
