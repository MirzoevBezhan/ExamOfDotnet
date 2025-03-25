using Domain.Entitites;
using Domain.Responses;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ArticlesController
{
    private readonly ArticleService articleService = new ();

    [HttpGet]
    public async Task<Response<List<Article>>> GetAll()
    {
        return await articleService.GetAll();
    }
    [HttpGet("id:int")]
    public async Task<Response<Article>> GetArticle(int id)
    {
        return await articleService.GetArticle(id);
    }
    [HttpPost]
    public async Task<Response<string>> AddArticle(Article article)
    {
        return await articleService.AddArticle(article);
    }

    [HttpDelete("id:int")]
    public async Task<Response<string>> DeleteArticle(int id)
    {
        return await articleService.DeleteArticle(id);
    }
    [HttpPut]
    public async Task<Response<string>> UpdateArticle(Article article)
    {
        return await articleService.UpdateArticle(article);
    }


}
