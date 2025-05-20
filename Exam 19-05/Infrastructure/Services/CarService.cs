using System.Net;
using AutoMapper;
using Domain.Dtos.Car;
using Domain.Entities;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class CarService(IBaseRepository<Car, int> carService, DataContext context, IMapper mapper)
    : ICarService
{
    public async Task<Response<GetCarDto>> CreateAsync(CreateCarDto request)
    {
        var car = mapper.Map<Car>(request);
        if (await context.Branches.AnyAsync(c => c.Id != car.BranchId))
        {
            return new Response<GetCarDto>(HttpStatusCode.BadRequest," Branch doesn't exist");
        }

        var result = await carService.AddAsync(car);

        var mapped = mapper.Map<GetCarDto>(car);
        return result == 0
            ? new Response<GetCarDto>(HttpStatusCode.BadRequest, "Car not added!")
            : new Response<GetCarDto>(mapped);
    }

    public async Task<Response<string>> DeleteAsync(int id)
    {
        var car = await carService.GetByIdAsync(id);
        if (car == null)
        {
            return new Response<string>(HttpStatusCode.BadRequest, "Car not found");
        }

        var res = await carService.DeleteAsync(car);
        return res == 0
            ? new Response<string>(HttpStatusCode.BadRequest, "Car not deleted!")
            : new Response<string>("Car  deleted!");
    }

    public async Task<PagedResponse<List<GetCarDto>>> GetAllAsync(CarFilter filter)
    {
        try
        {
            var validFilter = new ValidFilter(filter.PagesNumber, filter.PageSize);

            var cars = await carService.GetAllAsync();

            if (filter.Manufacturer != null)
            {
              cars = cars.Where(c => c.Manufacturer.Contains(filter.Manufacturer));   
            }

            if (filter.Model != null)
            {
                cars = cars.Where(c => c.Model.Contains(filter.Model));
            }

            if (filter.From != null)
            {
                cars = cars.Where(c => c.Year >= filter.From);
            }

            if (filter.To != null)
            {
                cars = cars.Where(c => c.Year >= filter.To);
            }

            if (filter.PriceFrom != null)
            {
                cars = cars.Where(c => c.PricePerDay >= filter.PriceFrom);
            }

            if (filter.PriceTo != null)
            {
                cars = cars.Where(c => c.PricePerDay <= filter.PriceTo);
            }
            

            var maped = mapper.Map<List<GetCarDto>>(cars);

            var totalRecords = maped.Count;

            var data = maped
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToList();

            return new PagedResponse<List<GetCarDto>>(data, validFilter.PageNumber, validFilter.PageSize,
                totalRecords);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Response<GetCarDto>> GetByIdAsync(int id)
    {
        var exist = await carService.GetByIdAsync(id);

        if (exist == null)
        {
            return new Response<GetCarDto>(HttpStatusCode.NotFound, "car not found!");
        }

        var carDto = mapper.Map<GetCarDto>(exist);

        return new Response<GetCarDto>(carDto);
    }

    public async Task<Response<GetCarDto>> UpdateAsync(int id, UpdateCarDto request)
    {
        var exist = await carService.GetByIdAsync(id);
        if (exist == null)
        {
            return new Response<GetCarDto>(HttpStatusCode.BadRequest, "Car not found");
        }

        exist.BranchId = request.BranchId;
        exist.Manufacturer = request.Manufacturer;
        exist.Model = request.Model;
        exist.Year = request.Year;
        exist.PricePerDay = request.PricePerDay;

        var result = await carService.UpdateAsync(exist);

        var car = mapper.Map<GetCarDto>(exist);

        return result == 0
            ? new Response<GetCarDto>(HttpStatusCode.BadRequest, "Car not updated!")
            : new Response<GetCarDto>(car);
    }
}