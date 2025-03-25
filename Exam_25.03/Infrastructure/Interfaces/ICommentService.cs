using Domain.Entitites;
using Domain.Responses;

namespace Infrastructure.Interfaces;

public interface ICommentService
{
    public Task<Response<List<Comment>>> GetAll();
    public Task<Response<Comment>> GetComment(int id);
    public Task<Response<string>> AddComment(Comment comment);
    public Task<Response<string>> UpdateComment(Comment comment);
    public Task<Response<string>> DeleteComment(int id);
}
