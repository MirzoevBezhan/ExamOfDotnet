using System.Net;
using AutoMapper;
using Domain.Dtos.Customer;
using Domain.Entities;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services;

public class CustomerService(
    ILogger<CustomerService> logger,
    IBaseRepository<Customer, int> customerService,
    DataContext context,
    IMapper mapper) : ICustomerService
{
    public async Task<Response<GetCustomerDto>> CreateAsync(CreateCustomerDto request)
    {
        try
        {
            logger.LogInformation("Creating a new customer.");
            var customer = mapper.Map<Customer>(request);

            var result = await customerService.AddAsync(customer);

            var mapped = mapper.Map<GetCustomerDto>(customer);
            logger.LogInformation($"Successfully created a new customer.");
            return result == 0
                ? new Response<GetCustomerDto>(HttpStatusCode.BadRequest, "Customer not added!")
                : new Response<GetCustomerDto>(mapped);
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occured when creating a new customer.");
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Response<string>> DeleteAsync(int id)
    {
        try
        {
            logger.LogInformation("Deleting a new customer.");
            var customer = await customerService.GetByIdAsync(id);
            if (customer == null)
            {
                return new Response<string>(HttpStatusCode.BadRequest, "Customer not found");
            }

            var res = await customerService.DeleteAsync(customer);
            logger.LogInformation($"Successfully deleted a new customer.");
            return res == 0
                ? new Response<string>(HttpStatusCode.BadRequest, "Customer not deleted!")
                : new Response<string>("Customer  deleted!");
        }
        catch (Exception e)
        {
            logger.LogInformation(e, "An error occured when deleting a new customer.");
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<PagedResponse<List<GetCustomerDto>>> GetAllAsync(CustomerFilter filter)
    {
        try
        {
            logger.LogInformation("Getting all customers.");
            var validFilter = new ValidFilter(filter.PagesNumber, filter.PageSize);

            var customers = await customerService.GetAllAsync();

            if (filter.FullName != null)
            {
                customers = customers.Where(c => c.FullName.Contains(filter.FullName));
            }

            if (filter.Email != null)
            {
                customers = customers.Where(c => c.Email.Contains(filter.Email));
            }


            var maped = mapper.Map<List<GetCustomerDto>>(customers);

            var totalRecords = maped.Count;

            var data = maped
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToList();

            logger.LogInformation("Getting all customers.");
            return new PagedResponse<List<GetCustomerDto>>(data, validFilter.PageNumber, validFilter.PageSize,
                totalRecords);
        }
        catch (Exception e)
        {
            logger.LogError(e.Message);
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Response<GetCustomerDto>> GetByIdAsync(int id)
    {
        try
        {
            logger.LogInformation("Getting a new customer.");
            var exist = await customerService.GetByIdAsync(id);

            if (exist == null)
            {
                return new Response<GetCustomerDto>(HttpStatusCode.NotFound, "customer not found!");
            }

            var customerDto = mapper.Map<GetCustomerDto>(exist);

            logger.LogInformation($"Successfully retrieved a new customer.");
            return new Response<GetCustomerDto>(customerDto);
        }
        catch (Exception e)
        {
            logger.LogError(e.Message);
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Response<GetCustomerDto>> UpdateAsync(int id, UpdateCustomerDto request)
    {
        try
        {
            logger.LogInformation("Updating a new customer.");
            var exist = await customerService.GetByIdAsync(id);
            if (exist == null)
            {
                return new Response<GetCustomerDto>(HttpStatusCode.BadRequest, "Customer not found");
            }

            exist.FullName = request.FullName;
            exist.Email = request.Email;
            exist.Phone = request.Phone;

            var result = await customerService.UpdateAsync(exist);

            var customer = mapper.Map<GetCustomerDto>(exist);


            logger.LogInformation($"Successfully updated a new customer.");
            return result == 0
                ? new Response<GetCustomerDto>(HttpStatusCode.BadRequest, "Customer not updated!")
                : new Response<GetCustomerDto>(customer);
        }
        catch (Exception e)
        {
            logger.LogInformation(e.Message);
            Console.WriteLine(e);
            throw;
        }
    }
}