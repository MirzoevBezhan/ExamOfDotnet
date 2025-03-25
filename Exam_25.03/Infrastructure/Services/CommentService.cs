using System.Net;
using Dapper;
using Domain.Entitites;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Interfaces;

namespace Infrastructure.Services;

public class CommentService : ICommentService
{
        private readonly DataContext dataContext = new DataContext();
    public async Task<Response<string>> AddComment(Comment comment)
    {
        using var connection = dataContext.GetConnection();
        var cmd = "insert into comments (articleId,userId,content,createdAt) values (@articleId,@userId,@content,@createdAt)";
        var res = await connection.ExecuteAsync(cmd, comment);
        return res == 0 ? new Response<string>(HttpStatusCode.BadRequest, "Maybe something went wrong")
        : new Response<string>("Comment Added");
    }

    public async Task<Response<string>> DeleteComment(int id)
    {
        using var connection = dataContext.GetConnection();
        var cmd = "delete from comments where id = @id";
        var res = await connection.ExecuteAsync(cmd, new { id = id });
        return res == 0 ? new Response<string>(HttpStatusCode.BadRequest, "Maybe something went wrong")
        : new Response<string>("Comment Deleted");

    }

    public async Task<Response<List<Comment>>> GetAll()
    {
        using var connection = dataContext.GetConnection();
        var cmd = "select * from comments";
        var res = await connection.QueryAsync<Comment>(cmd);
        return new Response<List<Comment>>(res.ToList());
    }

    public async Task<Response<Comment>> GetComment(int id)
    {
        using var connection = dataContext.GetConnection();
        var cmd = "select * from comments where id = @id";
        var res = await connection.QueryFirstOrDefaultAsync<Comment>(cmd);
        return res == null ? new Response<Comment>(HttpStatusCode.BadRequest, "Maybe something went wrong")
        : new Response<Comment>(res);
    }

    public async Task<Response<string>> UpdateComment(Comment comment)
    {
        using var connection = dataContext.GetConnection();
        var cmd = "update comments set userId = @userId , articleId = @articleId , content = @content , createdAt = @createdAt  where id = @id";
        var res = await connection.ExecuteAsync(cmd,comment);
        return res == 0 ? new Response<string>(HttpStatusCode.BadRequest, "Maybe something went wrong")
        : new Response<string>("Comment Updated");
    }

}
