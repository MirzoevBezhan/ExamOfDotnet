using System.Net;
using AutoMapper;
using Domain.Dtos.Customer;
using Domain.Dtos.Order;
using Domain.Entitites;
using Domain.Filters;
using Domain.Responces;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Interfaces;

namespace Infrastructure.Services;

public class CustomerService(DataContext context, IMapper mapper) : ICustomerService
{
    public async Task<PagedResponse<List<GetCustomerDto>>> GetAll(CustomerFilter filter)
    {
        var validFilter = new ValidFilter(filter.PagesNum, filter.PageSize);

        var Customers = context.Customers.AsQueryable();
        if (filter.FullName != null)
        {
            Customers = Customers.Where(o => o.FullName.Contains(filter.FullName));
        }

        if (filter.Email != null)
        {
            Customers = Customers.Where(o => o.Email.Contains(filter.Email));
        }

        if (filter.PhoneNumber != null)
        {
            Customers = Customers.Where(o => o.PhoneNumber.Contains(filter.PhoneNumber));
        }

        var mapped = mapper.Map<List<GetCustomerDto>>(Customers);

        var totalRecords = mapped.Count();

        var data = mapped
            .Skip((filter.PagesNum - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToList();

        return new PagedResponse<List<GetCustomerDto>>(data, filter.PagesNum, filter.PageSize, totalRecords);
    }

    public async Task<Response<GetCustomerDto>> GetById(int id)
    {
        var Customer = await context.Customers.FindAsync(id);
        if (Customer == null) return new Response<GetCustomerDto>(HttpStatusCode.BadRequest, "Not found");
        return mapper.Map<Response<GetCustomerDto>>(Customer);
    }

    public async Task<Response<GetCustomerDto>> Update(int id, UpdateCustomerDto updateCustomerDto)
    {
        var Customer = await context.Customers.FindAsync(id);
        if (Customer == null) return new Response<GetCustomerDto>(HttpStatusCode.BadRequest, "Not found");

        Customer.FullName = updateCustomerDto.FullName;
        Customer.Email = updateCustomerDto.Email;
        Customer.PhoneNumber = updateCustomerDto.PhoneNumber;

        var res = await context.SaveChangesAsync();

        return res == 0
            ? new Response<GetCustomerDto>(HttpStatusCode.BadRequest, "Error")
            : mapper.Map<Response<GetCustomerDto>>(Customer);
    }

    public async Task<Response<GetCustomerDto>> Create(CreateCustomerDto createCustomerDto)
    {   
        var Customer = mapper.Map<Customer>(createCustomerDto);

        await context.Customers.AddAsync(Customer);
        var res = await context.SaveChangesAsync();

        return res == 0
            ? new Response<GetCustomerDto>(HttpStatusCode.BadRequest, "Error")
            : mapper.Map<Response<GetCustomerDto>>(Customer);
    }

    public async Task<Response<string>> Delete(int id)
    {
        var Customer = await context.Customers.FindAsync(id);

        if (Customer == null) return new Response<string>(HttpStatusCode.BadRequest, "Not found");

        context.Customers.Remove(Customer);

        var res = await context.SaveChangesAsync();

        return res == 0
            ? new Response<string>(HttpStatusCode.BadRequest, "Not found")
            : new Response<string>(HttpStatusCode.OK, "Success");
    }
}