using Domain.Entitites;
using Domain.Responses;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentsController
{
    private readonly CommentService commentService = new();

    [HttpGet]
    public async Task<Response<List<Comment>>> GetAll()
    {
        return await commentService.GetAll();
    }
    [HttpGet("id:int")]
    public async Task<Response<Comment>> GetComment(int id)
    {
        return await commentService.GetComment(id);
    }
    [HttpPost]
    public async Task<Response<string>> AddComment(Comment comment)
    {
        return await commentService.AddComment(comment);
    }

    [HttpDelete("id:int")]
    public async Task<Response<string>> DeleteComment(int id)
    {
        return await commentService.DeleteComment(id);
    }
    [HttpPut]
    public async Task<Response<string>> UpdateComment(Comment comment)
    {
        return await commentService.UpdateComment(comment);
    }


}
