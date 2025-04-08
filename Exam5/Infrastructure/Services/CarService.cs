using System.Net;
using Domain.DTOs;
using Domain.DTOs.Booking;
using Domain.DTOs.Car;
using Domain.Entities;
using Domain.Responces;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class CarService(DataContext datacontext) : ICarService
{
    public async Task<Response<GetCarDto>> Create(CreateCarDto CreateCarDto)
    {
        var Car = new Car()
        {
            Model = CreateCarDto.Model,
            PricePerDay = CreateCarDto.PricePerDay,
            IsAvailable = CreateCarDto.IsAvailable,
        };

        await datacontext.Cars.AddAsync(Car);
        var res = await datacontext.SaveChangesAsync();

        var Result = new GetCarDto()
        {
            id = Car.id,
            Model = Car.Model,
            PricePerDay = Car.PricePerDay,
            IsAvailable = Car.IsAvailable,
            BookingCount = Car.Bookings.Count,
        };

        return res == 0
        ? new Response<GetCarDto>(HttpStatusCode.BadRequest, "Maybe something went wrong")
        : new Response<GetCarDto>(Result);

    }

    public async Task<Response<string>> Delete(int id)
    {
        var car = await datacontext.Cars.FindAsync(id);

        datacontext.Cars.Remove(car);
        var res = await datacontext.SaveChangesAsync();


        return res == 0
        ? new Response<string>(HttpStatusCode.BadRequest, "Didn't deleted")
        : new Response<string>("Deleted succsesfully");

    }

    public async Task<Response<GetCarDto>> Get(int id)
    {
        var car = await datacontext.Cars.FindAsync(id);

        if (car == null)
        {
            return new Response<GetCarDto>(HttpStatusCode.BadRequest, "Not found");
        }

        var GetCarDto = new GetCarDto()
        {
            id = car.id,
            Model = car.Model,
            PricePerDay = car.PricePerDay,
            IsAvailable = car.IsAvailable,
            BookingCount = car.Bookings.Count,
        };

        return new Response<GetCarDto>(GetCarDto);

    }

    public async Task<Response<List<GetCarDto>>> GetAll()
    {
        var list = await datacontext.Cars.ToListAsync();

        var res = list.Select(r => new GetCarDto()
        {
            id = r.id,
            Model = r.Model,
            PricePerDay = r.PricePerDay,
            IsAvailable = r.IsAvailable,
            BookingCount = r.Bookings.Count,
        }
        ).ToList();

        return new Response<List<GetCarDto>>(res);
    }

    public async Task<Response<GetCarDto>> Update(int id, UpdateCarDto updateCarDto)
    {
        var Car = await datacontext.Cars.FindAsync(id);

        Car.Model = updateCarDto.Model;
        Car.PricePerDay = updateCarDto.PricePerDay;
        Car.IsAvailable = updateCarDto.IsAvailable;


        var res = await datacontext.SaveChangesAsync();

        var Result = new GetCarDto()
        {
            id = Car.id,
            Model = Car.Model,
            PricePerDay = Car.PricePerDay,
            IsAvailable = Car.IsAvailable,
            BookingCount = Car.Bookings.Count,
        };
        return res == 0
        ? new Response<GetCarDto>(HttpStatusCode.BadRequest, "Didn't Update")
        : new Response<GetCarDto>(Result);
    }

    public async Task<Response<List<AvailableCarDto>>> GetAvailableCarDtos()
    {

        var list = await datacontext.Cars
        .Where(c => c.IsAvailable == true)
        .ToListAsync();

        var res = list.Select(r => new AvailableCarDto()
        {
            Model = r.Model,
            PricePerDay = r.PricePerDay,
        }).ToList();

        return new Response<List<AvailableCarDto>>(res);
    }


    public async Task<Response<List<PopularCarDto>>> GetPopularCars()
    {

        var list = await datacontext.Cars
        .OrderByDescending(l => l.Bookings.Count)
        .Take(3)
        .ToListAsync();

        var res = list.Select(r => new PopularCarDto()
        {
            Model = r.Model,
            BookingCount = r.Bookings.Count,

        }).ToList();

        return new Response<List<PopularCarDto>>(res);
    }

}

