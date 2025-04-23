using System.Net;
using AutoMapper;
using Domain.DTOs.Product;
using Domain.Entities;
using Domain.Filters;
using Domain.Responces;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class ProductService(DataContext context, IMapper mapper) : IProductService
{
    public async Task<Response<GetProductDto>> Add(CreateProductDto create)
    {
        var Product = mapper.Map<Product>(create);

        await context.Products.AddAsync(Product);
        var res = await context.SaveChangesAsync();
        return res == 0
            ? new Response<GetProductDto>(HttpStatusCode.BadRequest, "Don't Added")
            : mapper.Map<Response<GetProductDto>>(Product);
    }

    public async Task<Response<string>> Delete(int id)
    {
        var Product = await context.Products.FindAsync(id);

        if (Product == null)
        {
            return new Response<string>(HttpStatusCode.NotFound, "Not found");
        }

        context.Products.Remove(Product);
        var res = await context.SaveChangesAsync();

        return new Response<string>("Deleted");
    }

    public async Task<Response<GetProductDto>> Get(int id)
    {
        var Product = await context.Products.FindAsync(id);
        return Product == null
            ? new Response<GetProductDto>(HttpStatusCode.BadRequest, "Not Found")
            : mapper.Map<Response<GetProductDto>>(Product);
    }

    public async Task<PagedResponse<List<GetProductDto>>> GetAll(ProductFilter filter)
    {
        var validFilter = new ValidFilter(filter.PagesNum, filter.PageSize);

        var Products = context.Products.AsQueryable();

        if (filter.Name != null)
        {
            Products = Products.Where(c => c.Name.Contains(filter.Name));
        }

        if (filter.From != null)
        {
            Products = Products.Where(p => p.Price >= filter.From);
        }

        if (filter.To != null)
        {
            Products = Products.Where(p => p.Price <= filter.To);
        }


        var mapped = mapper.Map<List<GetProductDto>>(Products);

        var totalRecords = mapped.Count();

        var data = mapped
            .Skip(validFilter.PageSize * (validFilter.PageNumber - 1) * validFilter.PageSize)
            .Take(validFilter.PageSize);

        return mapper.Map<PagedResponse<List<GetProductDto>>>(data);
    }

    public async Task<Response<GetProductDto>> Update(int id, UpdateProductDto update)
    {
        var Product = await context.Products.FindAsync(id);

        if (Product == null)
        {
            return new Response<GetProductDto>(HttpStatusCode.BadRequest, "Not Found");
        }

        Product.Name = update.Name;
        Product.Price = update.Price;
        Product.QuantityInStock = update.QuantityInStock;
        Product.CategoryId = update.CategoryId;
        Product.SupplierId = update.SupplierId;

        var res = await context.SaveChangesAsync();

        return res == 0
            ? new Response<GetProductDto>(HttpStatusCode.BadRequest, "Not Updated")
            : mapper.Map<Response<GetProductDto>>(Product);
    }

    //Tas 2
    public async Task<Response<List<GetProductsWithLowStockDto>>> GetProductsWithLowStock()
    {
        var Products = await context.Products
            .Where(p => p.QuantityInStock < 5)
            .ToListAsync();

        return mapper.Map<Response<List<GetProductsWithLowStockDto>>>(Products);
    }
    //Task 3
    public async Task<Response<List<GetProductsStatisticDto>>> GetProductsWithStatistic()
    {
        var productsStatistics = await context.Products
            .Select(p => new GetProductsStatisticDto
            {
                TotalProduct = context.Products.Count(),
                AveragePrice = context.Products.Average(p => p.Price),
                TotalSold = context.Sales.Sum(s => s.QuantitySold),
            })
            .ToListAsync();

        return new Response<List<GetProductsStatisticDto>>(productsStatistics);
    }
        
}