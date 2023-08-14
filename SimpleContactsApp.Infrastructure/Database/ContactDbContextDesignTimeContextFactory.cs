using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using SimpleContactsApp.Infrastructure.Clients;
using Microsoft.Extensions.Options;

namespace SimpleContactsApp.Infrastructure.Database
{
    public class SimpleContactsAppDesignTimeDbContextFactory : IDesignTimeDbContextFactory<ContactDbContext>
    {

        public SimpleContactsAppDesignTimeDbContextFactory()
        {
        }

        public ContactDbContext CreateDbContext(string[] args)
        {
            var config = GetConfiguration();
            var keyVaultClient = new KeyVaultClient(config["KeyvaultUrl"]);

            var databaseConnectionString = keyVaultClient?.GetSecretAsync("dbConnectionString", default).GetAwaiter().GetResult();

            var builder = new DbContextOptionsBuilder<ContactDbContext>();
            builder.UseSqlServer(databaseConnectionString, options =>
            {
                options.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                options.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: System.TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null).EnableRetryOnFailure();
            });

            return new ContactDbContext(builder.Options);
        }

        private IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
                .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true);

            var config = builder.Build();
            return config;
        }
    }
}