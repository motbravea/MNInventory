namespace MNInventoryApp.Data
{
    public static class DatabaseConfig
    {
        public const string ConnectionStringEnvironmentVariable = "MNINVENTORY_CONNECTION_STRING";

        public static string ConnectionString
        {
            get
            {
                var value = Environment.GetEnvironmentVariable(ConnectionStringEnvironmentVariable);
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new InvalidOperationException(
                        "Set MNINVENTORY_CONNECTION_STRING to your own MySQL connection string before starting MNInventory. See README.md for setup instructions.");
                }

                return value;
            }
        }
    }
}
