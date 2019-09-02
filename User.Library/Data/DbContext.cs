using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace User.Library
{
    public interface IDbContext : IDisposable
    {
        IDbConnection CreateConnection();
    }

    public class DbContext : IDbContext
    {
        private readonly string _connectionString;

        // Default ctor.
        public DbContext(IOptions<ConnectionStringsOptions> connSettings)
        {
            _connectionString = connSettings.Value.MSSQL;
        }

        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);

        public void Dispose()
        {
        }
    }
}
