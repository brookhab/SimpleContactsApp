using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using SimpleContactsApp.Infrastructure.Interfaces;

namespace SimpleContactsApp.Infrastructure.Clients
{
    public class KeyVaultClient : IKeyVaultClient
    {
        private readonly SecretClient _secretClient; 

        public KeyVaultClient(string? keyVaultUrl)
        {
            if (string.IsNullOrEmpty(keyVaultUrl))
                throw new ArgumentNullException(nameof(keyVaultUrl));

            _secretClient = new SecretClient(new Uri(keyVaultUrl), new DefaultAzureCredential());
        }

        /*
         * Retrieves Secret from Azure Keyvault based on a given SecretName
         */
        public async Task<string?> GetSecretAsync(string secretName, CancellationToken cancellationToken)
        {
            try
            {
                var secret = await _secretClient.GetSecretAsync(secretName, cancellationToken: cancellationToken);
                return secret?.Value?.Value;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
