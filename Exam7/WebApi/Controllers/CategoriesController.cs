using Domain.DTOs.Category;
using Domain.Filters;
using Domain.Responces;
using Domain.Responses;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController(ICategoryService service)
{
    [HttpGet("int:id")]
    public async Task<Response<GetCategoryDto>> Get(int id)
    {
        return await service.Get(id);
    }

    [HttpGet]
    public async Task<PagedResponse<List<GetCategoryDto>>> GetAll(CategoryFilter filter)
    {
        return await service.GetAll(filter);
    }

    [HttpPut("int:id")]
    public async Task<Response<GetCategoryDto>> Update(int id, UpdateCategoryDto update)
    {
        return await service.Update(id, update);
    }

    [HttpDelete("int:id")]
    public async Task<Response<string>> Delete(int id)
    {
        return await service.Delete(id);
    }

    [HttpPost]
    public async Task<Response<GetCategoryDto>> Create(CreateCategoryDto create)
    {
        return await service.Add(create);
    }

    [HttpGet]
    public async Task<Response<GetCategoryWithProductsDto>> GetAllWithProducts()
    {
        return await service.GetCategoriesWithProducts();
    }
}