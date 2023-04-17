namespace TiendaAPI.Connection
{
    public class ConnectionDB
    {
        public readonly string _connectionString;

        public ConnectionDB(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConnectionStoreAPI")!;
        }
    }
}
