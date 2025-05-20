using System.Net;
using Domain.DTOs.Analytics;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Service;

public class AnalyticeService(DataContext context) : IAnalyticsService
{
    public async Task<Response<List<CarOccupancyDto>>> GetCarOccupancy(AnalyticsDto request)
    {
        var totalDays = (request.To - request.From).TotalDays;

        var rentals = await context.Rentals
            .Where(r => r.StartDate < request.To && r.EndDate > request.From)
            .ToListAsync();

        var cars = await context.Cars.ToListAsync();

        var occupancies = cars.Select(car =>
        {

            var carRentals = rentals.Where(r => r.CarId == car.Id);


            double rentedDays = carRentals.Sum(r =>
            {
                var start = r.StartDate < request.From ? request.From : r.StartDate;
                var end = r.EndDate > request.To ? request.To : r.EndDate;
                return (end - start).TotalDays;
            });


            decimal prosent = totalDays > 0 ? (decimal)(rentedDays / totalDays * 100) : 0;

            return new CarOccupancyDto
            {
                CarId = car.Id,
                Model = car.Model,
                CarProsent = Math.Round(prosent, 2)
            };
        }).ToList();

        return new Response<List<CarOccupancyDto>>(occupancies);
    }

    public async Task<Response<List<ActiveCustomersDto>>> GetCustomerActivity(AnalyticsDto request)
    {

        var activity = await context.Rentals
            .Where(r => r.StartDate >= request.From && r.EndDate <= request.To)
            .GroupBy(r => r.Customer.FullName)
            .Select(g => new ActiveCustomersDto()
            {
                CustomerName = g.Key,
                RentalsCount = g.Count(),
                TotalSpent = g.Sum(r => r.TotalCost)
            })
            .OrderByDescending(x => x.RentalsCount)
            .ToListAsync();
        return new Response<List<ActiveCustomersDto>>(activity);
    }


    public async Task<Response<List<PopularCarsDto>>> GetTopModels(int month, int year)
    {
        var models = await context.Rentals
        .Where(r => r.StartDate.Month == month && r.StartDate.Year == year)
        .GroupBy(r => r.Car.Model)
        .Select(g => new PopularCarsDto()
        {
            Model = g.Key,
            Count = g.Count()
        })
        .OrderByDescending(m => m.Count)
        .Take(5)
        .ToListAsync();

        return new Response<List<PopularCarsDto>>(models);
    }

    public async Task<Response<decimal>> GetTotalRevenue(AnalyticsDto request)
    {
        var total = await context.Rentals
       .Where(r => r.StartDate >= request.From && r.EndDate <= request.To)
       .SumAsync(r => r.TotalCost);
        return new Response<decimal>(total);
    }

}
