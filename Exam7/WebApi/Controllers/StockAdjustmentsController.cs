using Domain.DTOs.Product;
using Domain.DTOs.StockAdjustment;
using Domain.Filters;
using Domain.Responces;
using Domain.Responses;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class StockAdjustmentsController(IStockAdjustmentService service)
{
    [HttpGet("int:id")]
    public async Task<Response<GetStockAdjustmentDto>> Get(int id)
    {
        return await service.Get(id);
    }

    [HttpGet]
    public async Task<PagedResponse<List<GetStockAdjustmentDto>>> GetAll(StockAdjustmentFilter filter)
    {
        return await service.GetAll(filter);
    }

    [HttpPut("int:id")]
    public async Task<Response<GetStockAdjustmentDto>> Update(int id, UpdateStockAdjustmentDto update)
    {
        return await service.Update(id, update);
    }

    [HttpDelete("int:id")]
    public async Task<Response<string>> Delete(int id)
    {
        return await service.Delete(id);
    }

    [HttpPost]
    public async Task<Response<GetStockAdjustmentDto>> Create(CreateStockAdjustmentDto create)
    {
        return await service.Add(create);
    }

}