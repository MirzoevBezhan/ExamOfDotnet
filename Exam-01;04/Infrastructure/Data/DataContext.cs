using System.Data;
using Npgsql;

namespace Infrastructure.Data;

public class DataContext
{
    private readonly string connection = "Host=localhost;Password=ipo90;Database=Anatomy_db;Username=postgres;";
    public async Task<IDbConnection> GetConnection()
    {
        return new NpgsqlConnection(connection);
    }

}
