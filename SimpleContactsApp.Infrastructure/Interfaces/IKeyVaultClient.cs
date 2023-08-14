namespace SimpleContactsApp.Infrastructure.Interfaces
{
    public interface IKeyVaultClient
    {
        Task<string?> GetSecretAsync(string secretName, CancellationToken cancellationToken);
    }
}
