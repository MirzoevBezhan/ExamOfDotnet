using System.Net;
using Domain.DTOs.Booking;
using Domain.Entities;
using Domain.Responces;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.ExpressionTranslators.Internal;

namespace Infrastructure.Services;

public class BookingService(DataContext dataContext) : IBookingService
{
    public async Task<Response<GetBookingDto>> Create(CreateBookingDto createBookingDto)
    {
        var Booking = new Booking()
        {
            UserId = createBookingDto.UserId,
            CarId = createBookingDto.CarId,
            StartDate = createBookingDto.StartDate,
            EndDate = createBookingDto.EndDate,
        };

        await dataContext.Bookings.AddAsync(Booking);
        var res = await dataContext.SaveChangesAsync();

        var Result = new GetBookingDto()
        {
            id = Booking.id,
            UserId = Booking.UserId,
            CarId = Booking.CarId,
            StartDate = Booking.StartDate,
            EndDate = Booking.EndDate,
            Username = Booking.User.Username,
            CarModel = Booking.Car.Model,
        };

        return res == 0
        ? new Response<GetBookingDto>(HttpStatusCode.BadRequest, "Maybe something went wrong")
        : new Response<GetBookingDto>(Result);

    }

    public async Task<Response<string>> Delete(int id)
    {
        var booking = await dataContext.Bookings.FindAsync(id);

        dataContext.Bookings.Remove(booking);
        var res = await dataContext.SaveChangesAsync();


        return res == 0
        ? new Response<string>(HttpStatusCode.BadRequest, "Didn't deleted")
        : new Response<string>("Deleted succsesfully");

    }

    public async Task<Response<GetBookingDto>> Get(int id)
    {
        var booking = await dataContext.Bookings.FindAsync(id);

        if (booking == null)
        {
            return new Response<GetBookingDto>(HttpStatusCode.BadRequest, "Not found");
        }

        var GetBookingDto = new GetBookingDto()
        {
            id = booking.id,
            UserId = booking.UserId,
            CarId = booking.CarId,
            StartDate = booking.StartDate,
            EndDate = booking.EndDate,
            Username = booking.User.Username,
            CarModel = booking.Car.Model,
        };

        return new Response<GetBookingDto>(GetBookingDto);

    }

    public async Task<Response<List<GetBookingDto>>> GetAll()
    {
        var list = await dataContext.Bookings.ToListAsync();

        var res = list.Select(r => new GetBookingDto()
        {
            id = r.id,
            UserId = r.UserId,
            CarId = r.CarId,
            StartDate = r.StartDate,
            EndDate = r.EndDate,
            Username = r.User.Username,
            CarModel = r.Car.Model,
        }
        ).ToList();

        return new Response<List<GetBookingDto>>(res);
    }

    public async Task<Response<GetBookingDto>> Update(int id, UpdateBookingDto updateBookingDto)
    {
        var Booking = await dataContext.Bookings.FindAsync(id);

        Booking.UserId = updateBookingDto.UserId;
        Booking.CarId = updateBookingDto.CarId;
        Booking.StartDate = updateBookingDto.StartDate;
        Booking.EndDate = updateBookingDto.EndDate;

        var res = await dataContext.SaveChangesAsync();

        var Result = new GetBookingDto()
        {
            CarId = Booking.CarId,
            UserId = Booking.UserId,
            StartDate = Booking.StartDate,
            EndDate = Booking.EndDate,
            CarModel = Booking.Car.Model,
            Username  = Booking.User.Username,
        };
        return res == 0
        ? new Response<GetBookingDto>(HttpStatusCode.BadRequest, "Didn't Update")
        : new Response<GetBookingDto>(Result);
    }
}
