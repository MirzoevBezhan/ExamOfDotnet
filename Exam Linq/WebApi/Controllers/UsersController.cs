using Domain.DTOs.Project;
using Domain.DTOs.User;
using Domain.Responses;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class UsersController(IUserService service)
{
    [HttpGet("int:id")]
    public async Task<Response<GetUserDto>> GetUser(int id)
    {
     return  await service.GetUser(id);
    }

    [HttpPost]
    public async Task<Response<GetUserDto>> AddUser(CreateUserDto user)
    {
        return await service.AddUser(user);
    }

    [HttpPut("int:id")]
    public async Task<Response<GetUserDto>> UpdateUser(int id, UpdateUserDto user)
    {
        return await service.UpdateUser(id, user);
    }

    [HttpDelete("int:id")]
    public async Task<Response<string>> DeleteUser(int id)
    {
        return await service.DeleteUser(id);
    }

    [HttpGet]
    public async Task<Response<GetUserWithMostTasksDto>> GetUserWithMostTasks()
    {
        return await service.GetUserWithMostTasks();
    }
    
}