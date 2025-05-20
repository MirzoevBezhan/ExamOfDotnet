using System.Net;
using AutoMapper;
using Domain.Dtos.Customer;
using Domain.Entities;
using Domain.Filters;
using Domain.Responces;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Interfases;

namespace Infrastructure.Services;

public class CustomerService(DataContext context, IMapper mapper) : ICustomerService
{
    public async Task<PagedResponse<List<GetCustomerDto>>> GetAll(CustomerFilter filter)
    {
        var validFilter = new ValidFilter(filter.PagesNumber, filter.PageSize);

        var Customers = context.Customers.AsQueryable();

        if (filter.Name != null)
        {
            Customers = Customers.Where(c => c.FullName.Contains(filter.Name));
        }

        var mapped = mapper.Map<List<GetCustomerDto>>(Customers);

        var totalRecords = mapped.Count();

        var data = mapped
            .Skip(validFilter.PageNumber * validFilter.PageSize)
            .Take(validFilter.PageSize)
            .ToList();


        return new PagedResponse<List<GetCustomerDto>>(data, validFilter.PageNumber, validFilter.PageSize,
            totalRecords);
    }

    public async Task<Response<GetCustomerDto>> Create(CreateCustomerDto customerDto)
    {
        var Customer = mapper.Map<Customer>(customerDto);

        await context.Customers.AddAsync(Customer);

        var res = await context.SaveChangesAsync();

        if (res == 0)
        {
            return new Response<GetCustomerDto>(HttpStatusCode.BadRequest, "Not Added");
        }

        return mapper.Map<Response<GetCustomerDto>>(Customer);
    }

    public async Task<Response<GetCustomerDto>> Update(int id, UpdateCustomerDto customerDto)
    {
        var Customer = await context.Customers.FindAsync(id);
        if (Customer == null)
        {
            return new Response<GetCustomerDto>(HttpStatusCode.NotFound, "Not Found");
        }

        Customer.FullName = customerDto.FullName;
        Customer.Phone = customerDto.Phone;
        var res = await context.SaveChangesAsync();

        return res == 0
            ? new Response<GetCustomerDto>(HttpStatusCode.BadRequest, "Not Updated")
            : mapper.Map<Response<GetCustomerDto>>(Customer);
    }

    public async Task<Response<GetCustomerDto>> Get(int id)
    {
        var Customer = await context.Customers.FindAsync(id);
        if (Customer == null)
        {
            return new Response<GetCustomerDto>(HttpStatusCode.NotFound, "Not Found");
        }

        return mapper.Map<Response<GetCustomerDto>>(Customer);
    }

    public async Task<Response<string>> Delete(int id)
    {
        var Customer = await context.Customers.FindAsync(id);

        if (Customer == null)
        {
            return new Response<string>(HttpStatusCode.NotFound, "Not Found");
        }
        context.Customers.Remove(Customer);
        var res = await context.SaveChangesAsync();
        
        return res == 0
            ? new Response<string>(HttpStatusCode.BadRequest, "Not Deleted")
            : new Response<string>(HttpStatusCode.OK, "Deleted");
    }
}