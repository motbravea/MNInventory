using MNInventoryApp.Data;
using MySql.Data.MySqlClient;
using System.Data;

namespace MNInventoryApp.Services
{
    public class MySqlConnectionTestService
    {
        public bool CanConnect()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(DatabaseConfig.ConnectionString))
                {
                    connection.Open();
                    return connection.State == ConnectionState.Open;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
