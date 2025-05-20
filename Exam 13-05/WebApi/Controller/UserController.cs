using Domain.Constants;
using Domain.DTOs.Auth;
using Domain.DTOs.User;
using Domain.Filter;
using Domain.Responses;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserService service) : ControllerBase
{
    [HttpGet]
    [Authorize(Roles = Roles.User)]
    public async Task<Response<List<GetUserDto>>> GetAllAsync()
    {
        return await service.GetAll();
    }
    
    [HttpGet("{id}")]
    [Authorize(Roles = Roles.User)]
    public async Task<Response<GetUserDto>> GetUserByIdAsync(string id)
    {
        return await service.Get(id);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<Response<GetUserDto>> UpdateUserAsync(string id, UpdateUserDto request)
    {
        return await service.Update(id, request);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<Response<string>> DeleteUserAsync(string id)
    {
        return await service.Delete(id);
    }

}