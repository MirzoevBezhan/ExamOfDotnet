using System.Net;
using Domain.DTOs.User;
using Domain.Entites;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Interfaces;

namespace Infrastructure.Services;

public class UserService(DataContext context) : IUserService
{
    public async Task<Response<GetUserDto>> AddUser(CreateUserDto User)
    {
        var User2 = new User()
        {
            Email = User.Email,
            ReistrationDate = User.RegistrationDate,
            Name = User.Name,
        };
        await context.Users.AddAsync(User2);
        var res = await context.SaveChangesAsync();

        var result = new GetUserDto()
        {
            id = User2.id,
            Email = User2.Email,
            Name = User2.Name,
            RegistrationDate = User2.ReistrationDate,
        };

        return res == 0
        ? new Response<GetUserDto>(HttpStatusCode.BadRequest, "Don't Created User")
        : new Response<GetUserDto>(result);

    }

    public async Task<Response<string>> DeleteUser(int id)
    {
        var User = await context.Users.FindAsync(id);

        if (User == null)
        {
            return new Response<string>(HttpStatusCode.NotFound, "Not found");
        }
        
        context.Users.Remove(User);
        var res = await context.SaveChangesAsync();

        return res == 0
        ? new Response<string>(HttpStatusCode.BadRequest, "Don't Deleted")
        : new Response<string>("Deleted Succsefuly");

    }


    public async Task<Response<GetUserDto>> GetUser(int id)
    {
        var User = await context.Users.FindAsync(id);

        if (User == null)
        {
            return new Response<GetUserDto>(HttpStatusCode.BadRequest, " Not found");
        }

        var res = new GetUserDto()
        {
            id = User.id,
            Email = User.Email,
            Name = User.Name,
            RegistrationDate = User.ReistrationDate,
        };

        return new Response<GetUserDto>(res);
    }

    public async Task<Response<GetUserWithMostTasksDto>> GetUserWithMostTasks()
    {
        var User = context.Users.OrderByDescending(t => t.Tasks.Count()).AsParallel().FirstOrDefault();

        var res = new GetUserWithMostTasksDto()
        {
            Email = User.Email,
            id = User.id,
            Name = User.Name,
            RegistrationDate = User.ReistrationDate,
            TasksCount = User.Tasks.Count(),
        };

        return new Response<GetUserWithMostTasksDto>(res);
    }

    public async Task<Response<GetUserDto>> UpdateUser(int id, UpdateUserDto User)
    {
        var User1 = await context.Users.FindAsync(id);

        if (User1==null)
        {
            return new Response<GetUserDto>(HttpStatusCode.BadRequest, "Not found");
        }
        User1.Email = User.Email;
        User1.Name = User.Name;
        User1.ReistrationDate = User1.ReistrationDate;

        var res = await context.SaveChangesAsync();

        var result = new GetUserDto()
        {
            id = User1.id,
            Email = User1.Email,
            Name = User1.Name,
            RegistrationDate = User1.ReistrationDate,
        };

        return res == 0
        ? new Response<GetUserDto>(HttpStatusCode.BadRequest, "Doesn't Updated")
        : new Response<GetUserDto>(result);
    }


}
