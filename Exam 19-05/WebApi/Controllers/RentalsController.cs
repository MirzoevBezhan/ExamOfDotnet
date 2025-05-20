using Domain.DTOs;
using Domain.Dtos.Rental;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RentalsController(IRentalService service)
{
    [HttpGet("int:id")]
    public async Task<Response<GetRentalDto>> Get(int id)
    {
        return await service.GetByIdAsync(id);
    }

    [HttpGet]
    public async Task<PagedResponse<List<GetRentalDto>>> GetAll(RentalFilter filter)
    {
        return await service.GetAllAsync(filter);
    }

    [HttpPut("int:id")]
    public async Task<Response<GetRentalDto>> Update(int id, UpdateRentalDto update)
    {
        return await service.UpdateAsync(id, update);
    }

    [HttpDelete("int:id")]
    public async Task<Response<string>> Delete(int id)
    {
        return await service.DeleteAsync(id);
    }

    [HttpPost]
    public async Task<Response<GetRentalDto>> Create(CreateRentalDto create)
    {
        return await service.CreateAsync(create);
    }
}