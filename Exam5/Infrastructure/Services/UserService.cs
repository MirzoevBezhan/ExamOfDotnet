using System.Net;
using Domain.DTOs.User;
using Domain.DTOs.User;
using Domain.Entities;
using Domain.Responces;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class UserService(DataContext dataContext) : IUserService
{
    public async Task<Response<GetUserDto>> Create(CreateUserDto CreateUserDto)
    {
        var User = new User()
        {
            Username = CreateUserDto.Username,
            Email = CreateUserDto.Email,
            Phone = CreateUserDto.Phone,
        };

        await dataContext.Users.AddAsync(User);
        var res = await dataContext.SaveChangesAsync();

        var Result = new GetUserDto()
        {
            id = User.id,
            Username = User.Username,
            Email = User.Email,
            Phone = User.Phone,
            BookingCount = User.Bookings.Count,
        };

        return res == 0
        ? new Response<GetUserDto>(HttpStatusCode.BadRequest, "Maybe something went wrong")
        : new Response<GetUserDto>(Result);

    }

    public async Task<Response<string>> Delete(int id)
    {
        var User = await dataContext.Users.FindAsync(id);

        dataContext.Users.Remove(User);
        var res = await dataContext.SaveChangesAsync();


        return res == 0
        ? new Response<string>(HttpStatusCode.BadRequest, "Didn't deleted")
        : new Response<string>("Deleted succsesfully");

    }

    public async Task<Response<GetUserDto>> Get(int id)
    {
        var User = await dataContext.Users.FindAsync(id);

        if (User == null)
        {
            return new Response<GetUserDto>(HttpStatusCode.BadRequest, "Not found");
        }

        var GetUserDto = new GetUserDto()
        {
            id = User.id,
            Username = User.Username,
            Email = User.Email,
            Phone = User.Phone,
            BookingCount = User.Bookings.Count,
        };

        return new Response<GetUserDto>(GetUserDto);

    }

    public async Task<Response<List<GetUserDto>>> GetAll()
    {
        var list = await dataContext.Users.ToListAsync();

        var res = list.Select(r => new GetUserDto()
        {
            id = r.id,
            Username = r.Username,
            Email = r.Email,
            Phone = r.Phone,
            BookingCount = r.Bookings.Count,
        }
        ).ToList();

        return new Response<List<GetUserDto>>(res);
    }

    public async Task<Response<GetUserDto>> Update(int id, UpdateUserDto updateUserDto)
    {
        var User = await dataContext.Users.FindAsync(id);

        User.Username = updateUserDto.Username;
        User.Email = updateUserDto.Email;
        User.Phone = updateUserDto.Phone;


        var res = await dataContext.SaveChangesAsync();

        var Result = new GetUserDto()
        {
            id = User.id,
            Email = User.Email,
            Phone = User.Phone,
            Username = User.Username,
            BookingCount = User.Bookings.Count,
        };
        return res == 0
        ? new Response<GetUserDto>(HttpStatusCode.BadRequest, "Didn't Update")
        : new Response<GetUserDto>(Result);
    }
    public async Task<Response<List<FrequentRenterDto>>> GetPopularCars()
    {

        var list = await dataContext.Users
        .Where(u => u.Bookings.Count > 3)
        .Take(3)
        .ToListAsync();

        var res = list.Select(r => new FrequentRenterDto()
        {
            Username = r.Username,
            BookingCount = r.Bookings.Count,

        }).ToList();

        return new Response<List<FrequentRenterDto>>(res);
    }

}
