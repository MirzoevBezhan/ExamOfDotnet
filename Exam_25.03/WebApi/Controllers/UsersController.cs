using Domain.Entitites;
using Domain.Responses;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController
{
    private readonly UserService userService = new UserService();

    [HttpGet]
    public async Task<Response<List<User>>> GetAll()
    {
        return await userService.GetAll();
    }
    [HttpGet("id:int")]
    public async Task<Response<User>> GetUser(int id)
    {
        return await userService.GetUser(id);
    }
    [HttpPost]
    public async Task<Response<string>> AddUser(User user)
    {
        return await userService.AddUser(user);
    }

    [HttpDelete("id:int")]
    public async Task<Response<string>> DeleteUser(int id)
    {
        return await userService.DeleteUser(id);
    }
    [HttpPut]
    public async Task<Response<string>> UpdateUser(User user)
    {
        return await userService.UpdateUser(user);
    }
    [HttpGet("id:int")]
    public async Task<Response<List<Article>>> GetUserArticlesAsync(int id)
    {
        return await userService.GetUserArticlesAsync(id);
    }
    [HttpGet]
    public async Task<Response<List<Comment>>> GetArticleRecentCommentsAsync()
    {
        return await userService.GetArticleRecentCommentsAsync();
    }
    [HttpGet]
    public async Task<Response<int>> GetArticleClapsCountAsync()
    {
        return await userService.GetArticleClapsCountAsync();
    }


}
