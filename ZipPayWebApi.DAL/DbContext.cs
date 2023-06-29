using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace ZipPayWebApp
{
    public interface IDbContext
    {
        IDbConnection CreateConnection();
    }
    public class DbContext : IDbContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DbContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("ZipPayDB");
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }


    }
}
