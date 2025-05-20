using System.Net;
using AutoMapper;
using Domain.Dtos.Rental;
using Domain.Entities;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Interfaces;

namespace Infrastructure.Services;

public class RentalService(IBaseRepository<Rental, int> rentalService, DataContext context, IMapper mapper) : IRentalService
{
        public async Task<Response<GetRentalDto>> CreateAsync(CreateRentalDto request)
        {
            var rental = mapper.Map<Rental>(request);
            
            if (context.Customers.Any(c => rental.CustomerId != c.Id))
            {
                return new Response<GetRentalDto>(HttpStatusCode.BadRequest, "Customer not found");
            }

            if (context.Cars.Any(c => rental.CarId != c.Id))
            {
                return new Response<GetRentalDto>(HttpStatusCode.BadRequest,"Car not found");
            }
            var car = context.Cars.FirstOrDefault(c => c.Id == rental.CarId);

            if (car == null)
            {
                return new Response<GetRentalDto>(HttpStatusCode.NotFound, "Car not found");
            }
            var rentalDays = (request.EndDate - request.StartDate).Days;
            var totalPrice = car.PricePerDay * rentalDays;
            
            rental.TotalCost = totalPrice;

            var result = await rentalService.AddAsync(rental);
            var mapped = mapper.Map<GetRentalDto>(rental);
            return result == 0
                ? new Response<GetRentalDto>(HttpStatusCode.BadRequest, "Rental not added!")
                : new Response<GetRentalDto>(mapped);
        }

    public async Task<Response<string>> DeleteAsync(int id)
    {
        var rental = await rentalService.GetByIdAsync(id);
        if (rental == null)
        {
            return new Response<string>(HttpStatusCode.BadRequest, "Rental not found");
        }

        var res = await rentalService.DeleteAsync(rental);
        return res == 0
            ? new Response<string>(HttpStatusCode.BadRequest, "Rental not deleted!")
            : new Response<string>("Rental  deleted!");
    }

    public async Task<PagedResponse<List<GetRentalDto>>> GetAllAsync(RentalFilter filter)
    {

        try
        {
            var validFilter = new ValidFilter(filter.PagesNumber, filter.PageSize);

            var rentals = await rentalService.GetAllAsync();

            if (filter.From != null)
            {
                rentals = rentals.Where(s => s.TotalCost<=filter.From);
            }

            if (filter.To != null)
            {
                rentals = rentals.Where(s => s.TotalCost<=filter.To);
            }

            if (filter.CustomerId.HasValue)
            {
                rentals = rentals.Where(s => s.Customer.Id == filter.CustomerId);
            }

            if (filter.CarId.HasValue)
            {
                rentals = rentals.Where(s => s.Car.Id == filter.CarId);
            }
            
            var maped = mapper.Map<List<GetRentalDto>>(rentals);

            var totalRecords = maped.Count;

            var data = maped
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToList();

            return new PagedResponse<List<GetRentalDto>>(data, validFilter.PageNumber, validFilter.PageSize,
                totalRecords);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    public async Task<Response<GetRentalDto>> GetByIdAsync(int id)
    {

        var exist = await rentalService.GetByIdAsync(id);
        
        if (exist == null)
        {
            return new Response<GetRentalDto>(HttpStatusCode.NotFound, "rental not found!");
        }

        var rentalDto = mapper.Map<GetRentalDto>(exist);
        
        return new Response<GetRentalDto>(rentalDto);
    }

    public async Task<Response<GetRentalDto>> UpdateAsync(int id, UpdateRentalDto request)
    {
        
        var exist = await rentalService.GetByIdAsync(id);
        if (exist == null)
        {
            return new Response<GetRentalDto>(HttpStatusCode.BadRequest, "Rental not found");
        }

        exist.CustomerId = request.CustomerId;
        exist.CarId = request.CarId;
        exist.StartDate = request.StartDate;
        exist.EndDate = request.EndDate;
        exist.TotalCost = request.TotalCost;
        
        var result = await rentalService.UpdateAsync(exist);
        
        var rental = mapper.Map<GetRentalDto>(exist);
        
        return result == 0
            ? new Response<GetRentalDto>(HttpStatusCode.BadRequest, "Rental not updated!")
            : new Response<GetRentalDto>(rental);

    }
}
