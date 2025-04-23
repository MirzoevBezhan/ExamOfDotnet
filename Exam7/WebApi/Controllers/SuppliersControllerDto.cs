using Domain.DTOs.Product;
using Domain.DTOs.StockAdjustment;
using Domain.DTOs.Supplier;
using Domain.Filters;
using Domain.Responces;
using Domain.Responses;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class SuppliersControllerDto(ISupplierService service)
{
    [HttpGet("int:id")]
    public async Task<Response<GetSupplierDto>> Get(int id)
    {
        return await service.Get(id);
    }

    [HttpGet]
    public async Task<PagedResponse<List<GetSupplierDto>>> GetAll(SupplierFilter filter)
    {
        return await service.GetAll(filter);
    }

    [HttpPut("int:id")]
    public async Task<Response<GetSupplierDto>> Update(int id, UpdateSupplierDto update)
    {
        return await service.Update(id, update);
    }

    [HttpDelete("int:id")]
    public async Task<Response<string>> Delete(int id)
    {
        return await service.Delete(id);
    }

    [HttpPost]
    public async Task<Response<GetSupplierDto>> Create(CreateSupplierDto create)
    {
        return await service.Add(create);
    }

    [HttpGet]
    public async Task<Response<List<GetSupplierWithProductsDto>>> GetSuppliersWithProducts()
    {
        return await service.GetSuppliersWithProducts();
    }

}