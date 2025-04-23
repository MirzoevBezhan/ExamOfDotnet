using Domain.DTOs.Category;
using Domain.DTOs.Product;
using Domain.Filters;
using Domain.Responces;
using Domain.Responses;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductService service)
{
    [HttpGet("int:id")]
    public async Task<Response<GetProductDto>> Get(int id)
    {
        return await service.Get(id);
    }

    [HttpGet]
    public async Task<PagedResponse<List<GetProductDto>>> GetAll(ProductFilter filter)
    {
        return await service.GetAll(filter);
    }

    [HttpPut("int:id")]
    public async Task<Response<GetProductDto>> Update(int id, UpdateProductDto update)
    {
        return await service.Update(id, update);
    }

    [HttpDelete("int:id")]
    public async Task<Response<string>> Delete(int id)
    {
        return await service.Delete(id);
    }

    [HttpPost]
    public async Task<Response<GetProductDto>> Create(CreateProductDto create)
    {
        return await service.Add(create);
    }

    [HttpGet]
    public async Task<Response<List<GetProductsStatisticDto>>> GetProductsWithStatistic()
    {
        return await service.GetProductsWithStatistic();
    }
    [HttpGet]
    public async Task<Response<List<GetProductsWithLowStockDto>>> GetProductsWithLowStock()
    {
        return await service.GetProductsWithLowStock();
    }
}