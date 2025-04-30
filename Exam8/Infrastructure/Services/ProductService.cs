using System.Net;
using AutoMapper;
using Domain.Dtos.Product;
using Domain.Entitites;
using Domain.Filters;
using Domain.Responces;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Interfaces;

namespace Infrastructure.Services;

public class ProductService(DataContext context, IMapper mapper) : IProductService
{
     public async Task<PagedResponse<List<GetProductDto>>> GetAll(ProductFilter filter)
    {
        var validFilter = new ValidFilter(filter.PagesNum, filter.PageSize);

        var Products = context.Products.AsQueryable();
        if (filter.Name != null)
        {
            Products = Products.Where(o => o.Name.Contains(filter.Name));
        }

        if (filter.From != null)
        {
            Products = Products.Where(o => o.Price <= filter.From);
        }

        if (filter.To != null)
        {
            Products = Products.Where(o => o.Price >= filter.To);
        }

        var mapped = mapper.Map<List<GetProductDto>>(Products);

        var totalRecords = mapped.Count();

        var data = mapped
            .Skip((filter.PagesNum - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToList();

        return new PagedResponse<List<GetProductDto>>(data, filter.PagesNum, filter.PageSize, totalRecords);
    }

    public async Task<Response<GetProductDto>> GetById(int id)
    {
        var Product = await context.Products.FindAsync(id);
        if (Product == null) return new Response<GetProductDto>(HttpStatusCode.BadRequest, "Not found");
        return mapper.Map<Response<GetProductDto>>(Product);
    }

    public async Task<Response<GetProductDto>> Update(int id, UpdateProductDto updateProductDto)
    {
        var Product = await context.Products.FindAsync(id);
        if (Product == null) return new Response<GetProductDto>(HttpStatusCode.BadRequest, "Not found");

        Product.Name = updateProductDto.Name;
        Product.Price = updateProductDto.Price;

        var res = await context.SaveChangesAsync();

        return res == 0
            ? new Response<GetProductDto>(HttpStatusCode.BadRequest, "Error")
            : mapper.Map<Response<GetProductDto>>(Product);
    }

    public async Task<Response<GetProductDto>> Create(CreateProductDto createProductDto)
    {
        var Product = mapper.Map<Product>(createProductDto);
        
        await context.Products.AddAsync(Product);
        var res = await context.SaveChangesAsync();
        
        return res == 0 
            ? new Response<GetProductDto>(HttpStatusCode.BadRequest, "Error")
            : mapper.Map<Response<GetProductDto>>(Product);
    }

    public async Task<Response<string>> Delete(int id)
    {
        var Product = await context.Products.FindAsync(id);
        
        if (Product == null) return new Response<string>(HttpStatusCode.BadRequest, "Not found");
        
        context.Products.Remove(Product);
        
        var res = await context.SaveChangesAsync();
        
        return res == 0 
            ? new Response<string>(HttpStatusCode.BadRequest,"Not found")
            : new Response<string>(HttpStatusCode.OK, "Success");
    }
}