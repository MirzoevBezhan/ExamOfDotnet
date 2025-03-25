using System.Data;
using Npgsql;

namespace Infrastructure.Data;

public class DataContext
{
    private readonly string connection = "Host=localhost;Password=ipo90;Database=medium_db;Username=postgres;";
    public IDbConnection GetConnection()
    {
        return new NpgsqlConnection(connection);
    }

}
