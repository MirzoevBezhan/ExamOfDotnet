using Domain.Dtos.Car;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarsController(ICarService service)
{
    [HttpGet("int:id")]
    public async Task<Response<GetCarDto>> Get(int id)
    {
        return await service.GetByIdAsync(id);
    }

    [HttpGet]
    public async Task<PagedResponse<List<GetCarDto>>> GetAll(CarFilter filter)
    {
        return await service.GetAllAsync(filter);
    }

    [HttpPut("int:id")]
    public async Task<Response<GetCarDto>> Update(int id, UpdateCarDto update)
    {
        return await service.UpdateAsync(id, update);
    }

    [HttpDelete("int:id")]
    public async Task<Response<string>> Delete(int id)
    {
        return await service.DeleteAsync(id);
    }

    [HttpPost]
    public async Task<Response<GetCarDto>> Create(CreateCarDto create)
    {
        return await service.CreateAsync(create);
    }
}