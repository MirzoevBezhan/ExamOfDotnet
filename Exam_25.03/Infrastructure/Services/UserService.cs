using System.Net;
using Dapper;
using Domain.Entitites;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Interfaces;

namespace Infrastructure.Services;

public class UserService : IUserService
{
    private readonly DataContext dataContext = new DataContext();
    public async Task<Response<string>> AddUser(User user)
    {
        using var connection = dataContext.GetConnection();
        var cmd = "insert into users (username,email,passwordHash,bio,createdAt) values (@username,@email,@passwordHash,@bio,@createdAt)";
        var res = await connection.ExecuteAsync(cmd, user);
        return res == 0 ? new Response<string>(HttpStatusCode.BadRequest, "Maybe something went wrong")
        : new Response<string>("User Added");
    }

    public async Task<Response<string>> DeleteUser(int id)
    {
        using var connection = dataContext.GetConnection();
        var cmd = "delete from users where id = @id";
        var res = await connection.ExecuteAsync(cmd, new { id = id });
        return res == 0 ? new Response<string>(HttpStatusCode.BadRequest, "Maybe something went wrong")
        : new Response<string>("User Deleted");

    }

    public async Task<Response<List<User>>> GetAll()
    {
        using var connection = dataContext.GetConnection();
        var cmd = "select * from users";
        var res = await connection.QueryAsync<User>(cmd);
        return new Response<List<User>>(res.ToList());
    }

    public async Task<Response<User>> GetUser(int id)
    {
        using var connection = dataContext.GetConnection();
        var cmd = "select * from users where id = @id";
        var res = await connection.QueryFirstOrDefaultAsync<User>(cmd);
        return res == null ? new Response<User>(HttpStatusCode.BadRequest, "Maybe something went wrong")
        : new Response<User>(res);
    }

    public async Task<Response<string>> UpdateUser(User user)
    {
        using var connection = dataContext.GetConnection();
        var cmd = "update users set username = @username , passwordHash = @passwordHash , bio = @bio , createdAt = @createdAt  where id = @id";
        var res = await connection.ExecuteAsync(cmd, user);
        return res == 0 ? new Response<string>(HttpStatusCode.BadRequest, "Maybe something went wrong")
        : new Response<string>("User Updated");
    }
    public async Task<Response<List<Article>>> GetUserArticlesAsync(int id)
    {

        using var connection = dataContext.GetConnection();
        var cmd = "select * from articles where userId = @userId";
        var res = await connection.QueryAsync<Article>(cmd, new { id = id });
        return new Response<List<Article>>(res.ToList());
    }
    public async Task<Response<List<Comment>>> GetArticleRecentCommentsAsync()
    {
        using var connection = dataContext.GetConnection();
        var cmd = "select * from comments order by createdAt desc limit 5;";
        var res = await connection.QueryAsync<Comment>(cmd);
        return new Response<List<Comment>>(res.ToList());
    }
    
    public async Task<Response<int>> GetArticleClapsCountAsync()
    {
        using var connection = dataContext.GetConnection();
        var cmd = "select count(c.id) from claps as c join articles as a on c.articleId = a.id";
        var res = await connection.ExecuteAsync(cmd);
        return new Response<int>(res);
    }
}
