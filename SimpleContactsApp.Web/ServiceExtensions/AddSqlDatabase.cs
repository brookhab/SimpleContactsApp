using Microsoft.EntityFrameworkCore;
using SimpleContactsApp.Infrastructure.Database;
using System.Diagnostics.CodeAnalysis;
using SimpleContactsApp.Infrastructure.Interfaces;
using SimpleContactsApp.Web.Exceptions;
using SimpleContactsApp.Web.Utilities.Settings;

namespace SimpleContactsApp.Web.ServiceExtensions
{
    [ExcludeFromCodeCoverage]
    public static class DatabaseServiceExtension
    {
        /*
         *This extension method adds a scoped db context to the service collection. 
         *It retrieves a Keyvault service that was previously registered and will try to get the dbconnection string from Azure Keyvault.
         * If the connection string is successfully retrieved, it will then tries to connect to db by applying different configurations.
         */
        public static void AddSqlDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextFactory<ContactDbContext>((serviceProvider, ctx) =>
            {
                var keyVaultClient = serviceProvider.GetService<IKeyVaultClient>();
                var databaseConnectionString = keyVaultClient?.GetSecretAsync("dbConnectionString", default).GetAwaiter().GetResult() ??
                    throw new KeyVaultConfigurationException("Database connection string not found in Key Vault.");

                var appConfig = configuration.GetSection(nameof(AppConfig)).Get<AppConfig>();

                ctx.UseSqlServer( databaseConnectionString, options =>
                {
                    options.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                    options.EnableRetryOnFailure(
                        maxRetryCount: appConfig!.MaxRetryCount,
                        maxRetryDelay: TimeSpan.FromSeconds(appConfig!.MaxRetryDelaySeconds),
                        errorNumbersToAdd: null).EnableRetryOnFailure();
                });
            }, ServiceLifetime.Scoped);
        }
    }
}
