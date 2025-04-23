using Domain.DTOs.Product;
using Domain.DTOs.Sale;
using Domain.Filters;
using Domain.Responces;
using Domain.Responses;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SalesController(ISaleService service)
{
    [HttpGet("int:id")]
    public async Task<Response<GetSaleDto>> Get(int id)
    {
        return await service.Get(id);
    }

    [HttpGet]
    public async Task<PagedResponse<List<GetSaleDto>>> GetAll(SaleFilter filter)
    {
        return await service.GetAll(filter);
    }

    [HttpPut("int:id")]
    public async Task<Response<GetSaleDto>> Update(int id, UpdateSaleDto update)
    {
        return await service.Update(id, update);
    }

    [HttpDelete("int:id")]
    public async Task<Response<string>> Delete(int id)
    {
        return await service.Delete(id);
    }

    [HttpPost]
    public async Task<Response<GetSaleDto>> Create(CreateSaleDto create)
    {
        return await service.Add(create);
    }

    [HttpGet]
    public async Task<PagedResponse<List<GetSalesByDateDto>>> GetSalesByDate(SalesFilter filter)
    {
        return await service.GetSalesByDate(filter);
    }

    [HttpGet]
    public async Task<Response<List<GetSaleWithTopProductsDto>>> GetFiveSalesWithTopProducts()
    {
        return await service.GetFiveSalesWithTopProducts();
    }

    [HttpGet]
    public async Task<Response<List<GetSalesDailyRevenueDto>>> GetSalesDailyRevenue()
    {
        return await service.GetSalesDailyRevenue();
    }

    [HttpGet]
    public async Task<Response<List<GetStatisticForDashboardDto>>> GetStatisticsForDashboard()
    {
        return await service.GetStatisticsForDashboard();
    }
}