using System.Net;
using AutoMapper;
using Domain.DTOs.Category;
using Domain.DTOs.Product;
using Domain.Entities;
using Domain.Filters;
using Domain.Responces;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace Infrastructure.Services;

public class CategoryService(DataContext context, IMapper mapper) : ICategoryService
{
    public async Task<Response<GetCategoryDto>> Add(CreateCategoryDto create)
    {
        var Category = mapper.Map<Category>(create);

        await context.Categories.AddAsync(Category);
        var res = await context.SaveChangesAsync();
        return res == 0
            ? new Response<GetCategoryDto>(HttpStatusCode.BadRequest, "Don't Added")
            : mapper.Map<Response<GetCategoryDto>>(Category);
    }

    public async Task<Response<string>> Delete(int id)
    {
        var Category = await context.Categories.FindAsync(id);

        if (Category == null)
        {
            return new Response<string>(HttpStatusCode.NotFound, "Not found");
        }

        context.Categories.Remove(Category);
        var res = await context.SaveChangesAsync();

        return new Response<string>("Deleted");
    }

    public async Task<Response<GetCategoryDto>> Get(int id)
    {
        var Category = await context.Categories.FindAsync(id);
        return Category == null
            ? new Response<GetCategoryDto>(HttpStatusCode.BadRequest, "Not Found")
            : mapper.Map<Response<GetCategoryDto>>(Category);
    }

    public async Task<PagedResponse<List<GetCategoryDto>>> GetAll(CategoryFilter filter)
    {
        var validFilter = new ValidFilter(filter.PagesNum, filter.PageSize);

        var Categories = context.Categories.AsQueryable();

        if (filter.Name != null)
        {
            Categories = Categories.Where(c => c.Name.Contains(filter.Name));
        }

        var mapped = mapper.Map<List<GetCategoryDto>>(Categories);

        var totalRecords = mapped.Count();

        var data = mapped
            .Skip(validFilter.PageSize * (validFilter.PageNumber - 1) * validFilter.PageSize)
            .Take(validFilter.PageSize);

        return mapper.Map<PagedResponse<List<GetCategoryDto>>>(data);
    }

    public async Task<Response<GetCategoryDto>> Update(int id, UpdateCategoryDto update)
    {
        var Category = await context.Categories.FindAsync(id);

        if (Category == null)
        {
            return new Response<GetCategoryDto>(HttpStatusCode.BadRequest, "Not Found");
        }

        Category.Name = update.Name;

        var res = await context.SaveChangesAsync();

        return res == 0
            ? new Response<GetCategoryDto>(HttpStatusCode.BadRequest, "Not Updated")
            : mapper.Map<Response<GetCategoryDto>>(Category);
    }
    //Task 1
    public async Task<Response<GetCategoryWithProductsDto>> GetCategoriesWithProducts()
    {
        var categories = await context.Categories
            .Include(c => c.Products)
            .Select(c => new GetCategoryWithProductsDto()
            {
                Id = c.Id,
                Name = c.Name,
                Products = c.Products.Select(p => new ProductsForCategoryDto()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price
                }).ToList()
            })
            .ToListAsync();

        return mapper.Map<Response<GetCategoryWithProductsDto>>(categories);
    }
}