using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Domain.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Domain.Constants;
using Domain.DTOs.Auth;
using Domain.DTOs.User;
using Domain.Filter;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services;

public class UserService(
    DataContext context,
    UserManager<IdentityUser> userManager,
    RoleManager<IdentityRole> roleManager,
    IMapper mapper,
    IConfiguration configuration) : IUserService
{
    public async Task<Response<GetUserDto>> Update(string id, UpdateUserDto request)
    {
        var Users = await userManager.FindByIdAsync(id);
        if (Users == null)
        {
            return new Response<GetUserDto>(HttpStatusCode.NotFound, "Id in not found");
        }

        Users.Email = request.Email;
        Users.UserName = request.Email;
        Users.PhoneNumber = request.Phone;

        var res = await userManager.UpdateAsync(Users);

        var data = mapper.Map<GetUserDto>(Users);

        return res == null
            ? new Response<GetUserDto>(HttpStatusCode.BadRequest, "User not Updated!")
            : new Response<GetUserDto>(data);
    }

    [Authorize]
    public async Task<Response<string>> Delete(string id)
    {
        var User = await userManager.FindByIdAsync(id);
        if (User == null)
        {
            return new Response<string>(HttpStatusCode.NotFound, "Not found");
        }

        var res = await userManager.DeleteAsync(User);

        return res == null
            ? new Response<string>(HttpStatusCode.BadRequest, "Not found ")
            : new Response<string>("User deleted !");
    }

    [Authorize]
    public async Task<Response<List<GetUserDto>>> GetAll()
    {
        var Users = await context.Users.ToListAsync();
        var data = mapper.Map<List<GetUserDto>>(Users);
        return new Response<List<GetUserDto>>(data);
    }

    public async Task<Response<GetUserDto>> Get(string id)
    {
        var Users = await userManager.FindByIdAsync(id);
        if (Users == null)
        {
            return new Response<GetUserDto>(HttpStatusCode.NotFound, "Id in not found");
        }

        var data = mapper.Map<GetUserDto>(Users);
        return new Response<GetUserDto>(data);
    }
}