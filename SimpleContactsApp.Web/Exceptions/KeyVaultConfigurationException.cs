namespace SimpleContactsApp.Web.Exceptions
{
    public class KeyVaultConfigurationException : Exception
    {
        public KeyVaultConfigurationException() { } 
        public KeyVaultConfigurationException(string message) : base(message) { }
        public KeyVaultConfigurationException(string message, Exception innerException) : base(message, innerException) { }
    }
}
