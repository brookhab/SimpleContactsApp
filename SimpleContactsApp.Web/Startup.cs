using Microsoft.EntityFrameworkCore;
using SimpleContactsApp.Application.Interfaces;
using SimpleContactsApp.Application.Services;
using SimpleContactsApp.Infrastructure.Clients;
using SimpleContactsApp.Infrastructure.Database;
using SimpleContactsApp.Infrastructure.Interfaces;
using SimpleContactsApp.Infrastructure.Repositories;
using SimpleContactsApp.Web.ServiceExtensions;
using SimpleContactsApp.Web.Utilities.Settings;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Identity;
using SimpleContactsApp.Domain.Entities;

namespace SimpleContactsApp.Web
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        public IConfiguration Config { get; }

        public Startup(IConfiguration configuration)
        {
            Config = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add services to the container.
            
            //Get configurations from Azure App Service application settings and register Keyvault Client with connection string
            services.Configure<AppServiceConfigurations>(Config.GetSection(AppServiceConfigurations.Section));
            services.AddScoped<IKeyVaultClient>(x => new KeyVaultClient(Config["KeyvaultUrl"]));

            //Register Application insight for monitoring 
            services.AddApplicationInsightsTelemetry(options =>
            {
                options.ConnectionString = Config.GetValue<string>("APPLICATIONINSIGHTS_CONNECTION_STRING");
                options.EnableAdaptiveSampling = false;
            });


            //Registers Azure Distributed Redis Cache
            services.AddStackExchangeRedisCache(setupAction =>
            {
                setupAction.Configuration = $"{Config["RedisConnectionString"]}";
                setupAction.InstanceName = "SimpleContactsApp_Session_";
            });

            //Register service required for Session State
            services.AddSession(options =>
            {
                options.Cookie.Name = ".SimpleContactsApp.Session";
                options.IdleTimeout = TimeSpan.FromMinutes(60);
                options.Cookie.HttpOnly = true;
            });


            //Register UI, Db and Identity System for Authentication
            services.AddRazorPages();
            services.AddSqlDatabase(Config);

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ContactDbContext>()
                .AddDefaultTokenProviders();


            //Register scoped Onion architecture layers,i.e, application and infrastructure layers 
            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<IContactService, ContactService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseHsts();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            //Use Session State middleware for the app
            app.UseSession();

            //Map controllers and define routing pattern
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                  name: "default",
                  pattern: "{controller=Home}/{action=Index}/{id?}");

            });
        }
    }
}