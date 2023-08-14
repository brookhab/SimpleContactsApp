using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Configuration;
using SimpleContactsApp.Infrastructure.Clients;
using SimpleContactsApp.Infrastructure.Interfaces;
using Xunit;

namespace SimpleContactsApp.Test.Integration
{
    public class KeyVaultClientIntegrationTests : IDisposable
    {
        private readonly IKeyVaultClient _keyVaultClient;

        public KeyVaultClientIntegrationTests()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json") 
                .Build();

            var keyVaultUrl = configuration["AppServiceConfigurations:KeyvaultUrl"];

            if (string.IsNullOrEmpty(keyVaultUrl))
            {
                throw new InvalidOperationException("KeyVaultUrl is not configured.");
            }


            _keyVaultClient = new KeyVaultClient(keyVaultUrl);
        }

        [Fact(Skip = "Integration setup in progress")]
        public async Task GetSecretAsync_ValidSecret_ReturnsSecretValue()
        {
            // Arrange
            var secretName = "dbConnectionString";

            // Act
            var result = await _keyVaultClient.GetSecretAsync(secretName, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
        }

        [Fact(Skip = "Integration setup in progress")]
        public async Task GetSecretAsync_NonExistentSecret_ReturnsNull()
        {
            // Arrange
            var secretName = "NonExistentSecret";

            // Act
            var result = await _keyVaultClient.GetSecretAsync(secretName, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }

        [Fact(Skip = "Integration setup in progress")]
        public async Task GetSecretAsync_InvalidSecretName_ThrowsException()
        {
            // Arrange
            var invalidSecretName = "Invalid$Secret";

            // Act & Assert
            await Assert.ThrowsAsync<UriFormatException>(() =>
                _keyVaultClient.GetSecretAsync(invalidSecretName, CancellationToken.None));
        }


        public void Dispose()
        {
            // Clean up or dispose any resources used for integration tests
        }
    }
}
