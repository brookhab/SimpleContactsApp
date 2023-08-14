using System.Diagnostics.CodeAnalysis;

namespace SimpleContactsApp.Web;
[ExcludeFromCodeCoverage]
public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }
    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                
                var settingFile = context.HostingEnvironment.IsDevelopment()
                    ? "appsettings.Development.json"
                    : "appsettings.json";
                
                config.SetBasePath(System.AppContext.BaseDirectory)
                    .AddJsonFile(settingFile, optional: false, reloadOnChange: true)
                    .AddEnvironmentVariables();

                config.Build();
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}

/*

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
services.AddRazorPages();

services.AddDbContext<ContactDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ContactDbContext>()
    .AddDefaultTokenProviders();

services.AddScoped<IContactRepository, ContactRepository>();
services.AddScoped<IContactService, ContactService>();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseExceptionHandler("/Error");
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
*/