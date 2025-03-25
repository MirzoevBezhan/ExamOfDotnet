using System.Net;
using Dapper;
using Domain.Entitites;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Interfaces;

namespace Infrastructure.Services;

public class ClapService : IClapService
{
    private readonly DataContext dataContext = new DataContext();
    public async Task<Response<string>> AddClap(Clap clap)
    {
        using var connection = dataContext.GetConnection();
        var cmd = "insert into claps (articleId,userId,count,createdAt) values (@articleId,@userId,@count,@createdAt)";
        var res = await connection.ExecuteAsync(cmd, clap);
        return res == 0 ? new Response<string>(HttpStatusCode.BadRequest, "Maybe something went wrong")
        : new Response<string>("Clap Added");
    }

    public async Task<Response<string>> DeleteClap(int id)
    {
        using var connection = dataContext.GetConnection();
        var cmd = "delete from claps where id = @id";
        var res = await connection.ExecuteAsync(cmd, new { id = id });
        return res == 0 ? new Response<string>(HttpStatusCode.BadRequest, "Maybe something went wrong")
        : new Response<string>("Clap Deleted");

    }

    public async Task<Response<List<Clap>>> GetAll()
    {
        using var connection = dataContext.GetConnection();
        var cmd = "select * from claps";
        var res = await connection.QueryAsync<Clap>(cmd);
        return new Response<List<Clap>>(res.ToList());
    }

    public async Task<Response<Clap>> GetClap(int id)
    {
        using var connection = dataContext.GetConnection();
        var cmd = "select * from claps where id = @id";
        var res = await connection.QueryFirstOrDefaultAsync<Clap>(cmd);
        return res == null ? new Response<Clap>(HttpStatusCode.BadRequest, "Maybe something went wrong")
        : new Response<Clap>(res);
    }

    public async Task<Response<string>> UpdateClap(Clap clap)
    {
        using var connection = dataContext.GetConnection();
        var cmd = "update claps set userId = @userId , articleId = @articleId , count = @count , createdAt = @createdAt  where id = @id";
        var res = await connection.ExecuteAsync(cmd, clap);
        return res == 0 ? new Response<string>(HttpStatusCode.BadRequest, "Maybe something went wrong")
        : new Response<string>("Clap Updated");
    }

}
