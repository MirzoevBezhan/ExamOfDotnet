using Domain.DTOs;
using Domain.Dtos.Customer;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController(ICustomerService service)
{
    [HttpGet("int:id")]
    public async Task<Response<GetCustomerDto>> Get(int id)
    {
        return await service.GetByIdAsync(id);
    }

    [HttpGet]
    public async Task<PagedResponse<List<GetCustomerDto>>> GetAll(CustomerFilter filter)
    {
        return await service.GetAllAsync(filter);
    }

    [HttpPut("int:id")]
    public async Task<Response<GetCustomerDto>> Update(int id, UpdateCustomerDto update)
    {
        return await service.UpdateAsync(id, update);
    }

    [HttpDelete("int:id")]
    public async Task<Response<string>> Delete(int id)
    {
        return await service.DeleteAsync(id);
    }

    [HttpPost]
    public async Task<Response<GetCustomerDto>> Create(CreateCustomerDto create)
    {
        return await service.CreateAsync(create);
    }
}