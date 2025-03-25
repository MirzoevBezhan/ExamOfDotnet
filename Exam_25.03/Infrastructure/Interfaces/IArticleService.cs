using Domain.Responses;
using Domain.Entitites;
namespace Infrastructure.Interfaces;

public interface IArticleService
{
    public Task<Response<List<Article>>> GetAll();
    public Task<Response<Article>> GetArticle(int id);
    public Task<Response<string>> AddArticle(Article article);
    public Task<Response<string>> UpdateArticle(Article article);
    public Task<Response<string>> DeleteArticle(int id);
    
}
