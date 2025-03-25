using System.Net;
using Dapper;
using Domain.Entitites;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Interfaces;

namespace Infrastructure.Services;

public class ArticleService : IArticleService
{
    private readonly DataContext dataContext = new DataContext();
    public async Task<Response<string>> AddArticle(Article article)
    {
        using var connection = dataContext.GetConnection();
        var cmd = "insert into articles (userId,title,content,description,createdAt,status) values (@userId,@title,@content,@description,@createdAt,@status)";
        var res = await connection.ExecuteAsync(cmd, article);
        return res == 0 ? new Response<string>(HttpStatusCode.BadRequest, "Maybe something went wrong")
        : new Response<string>("Article Added");
    }

    public async Task<Response<string>> DeleteArticle(int id)
    {
        using var connection = dataContext.GetConnection();
        var cmd = "delete from articles where id = @id";
        var res = await connection.ExecuteAsync(cmd, new { id = id });
        return res == 0 ? new Response<string>(HttpStatusCode.BadRequest, "Maybe something went wrong")
        : new Response<string>("Article Deleted");

    }

    public async Task<Response<List<Article>>> GetAll()
    {
        using var connection = dataContext.GetConnection();
        var cmd = "select * from articles";
        var res = await connection.QueryAsync<Article>(cmd);
        return new Response<List<Article>>(res.ToList());
    }

    public async Task<Response<Article>> GetArticle(int id)
    {
        using var connection = dataContext.GetConnection();
        var cmd = "select * from articles where id = @id";
        var res = await connection.QueryFirstOrDefaultAsync<Article>(cmd);
        return res == null ? new Response<Article>(HttpStatusCode.BadRequest, "Maybe something went wrong")
        : new Response<Article>(res);
    }

    public async Task<Response<string>> UpdateArticle(Article article)
    {
        using var connection = dataContext.GetConnection();
        var cmd = "update articles set userId = @userId , title = @title , content = @content , desription = @description , createdAt = @createdAt , status = @status where id = @id";
        var res = await connection.ExecuteAsync(cmd,article);
        return res == 0 ? new Response<string>(HttpStatusCode.BadRequest, "Maybe something went wrong")
        : new Response<string>("Article Updated");
    }

}
