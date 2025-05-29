using System.Net;
using AutoMapper;
using Domain.DTOs.User.Product;
using Domain.Entities;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services;

public class ProductService(
    IBaseRepository<Product, int> repository,
    DataContext context,
    IMapper mapper,
    IRedisCacheService cacheService,
    ILogger<ProductService> logger)
    : IProductService
{
    public async Task<Response<GetProductDto>> Add(CreateProductDto request)
    {
        logger.LogInformation($"Add Product");
        var product = new Product()
        {
            Name = request.Name,
            Price = request.Price,
            CreatedAt = DateTime.Now
        };

        if (product == null)
        {
            return new Response<GetProductDto>(HttpStatusCode.NotFound, "Product not found");
        }

        logger.LogInformation($"Product not found");

        var result = await repository.AddAsync(product);

        var mapped = mapper.Map<GetProductDto>(product);

        logger.LogInformation($"Product added");
        await cacheService.DeleteData("products");
        return result == 0
            ? new Response<GetProductDto>(HttpStatusCode.BadRequest, "Product not added!")
            : new Response<GetProductDto>(mapped);
    }

    public async Task<Response<string>> Delete(int id)
    {
        logger.LogInformation($"Delete Product");
        var product = await repository.GetByIdAsync(id);
        if (product == null)
        {
            return new Response<string>(HttpStatusCode.BadRequest, "Product not found");
        }

        logger.LogInformation($"Product not found");

        var res = await repository.DeleteAsync(product);
        logger.LogInformation($"Product deleted");
        await cacheService.DeleteData("products");
        return res == 0
            ? new Response<string>(HttpStatusCode.BadRequest, "Product not deleted!")
            : new Response<string>("Product  deleted!");
    }

    public async Task<PagedResponse<List<GetProductDto>>> GetAll(ProductFilter filter)
    {
        try
        {
            var validFilter = new ValidFilter(filter.PagesNumber, filter.PageSize);
            const string cacheKey = "products";

            var productsInCache = await cacheService.GetData<List<GetProductDto>>(cacheKey);
            logger.LogInformation("Get products from cache");

            if (productsInCache == null)
            {
                logger.LogInformation("Get products from database");
                var products = await repository.GetAllAsync();
                productsInCache = products.Select(c => new GetProductDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Price = c.Price,
                    CreatedAt = DateTime.Now
                }).ToList();

                await cacheService.SetData(cacheKey, productsInCache, 1);
            }

            if (!string.IsNullOrEmpty(filter.Name))
            {
                productsInCache = productsInCache
                    .Where(c => c.Name.ToLower().Trim().Contains(filter.Name.ToLower().Trim())).ToList();
            }

            if (filter.From.HasValue)
            {
                var now = DateTime.UtcNow.Year;
                productsInCache = productsInCache
                    .Where(c => now - c.CreatedAt.Value.Year  >= filter.From).ToList();
            }

            if (filter.From.HasValue)
            {
                productsInCache = productsInCache
                    .Where(c => c.Price <= filter.PriceFrom).ToList();
            }

            if (filter.To.HasValue)
            {
                productsInCache = productsInCache
                    .Where(c => c.Price <= filter.PriceFrom).ToList();
            }


            var totalCount = productsInCache.Count();

            var paginatedData = productsInCache
                .Skip((filter.PagesNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToList();

            logger.LogInformation("Product successfully retrieved");
            return new PagedResponse<List<GetProductDto>>(paginatedData, filter.PagesNumber, filter.PageSize,
                totalCount);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Response<GetProductDto>> Get(int id)
    {
        logger.LogInformation($"Get Product");
        var exist = await repository.GetByIdAsync(id);

        if (exist == null)
        {
            return new Response<GetProductDto>(HttpStatusCode.NotFound, "product not found!");
        }

        logger.LogInformation($"Product not found");

        var productDto = mapper.Map<GetProductDto>(exist);

        logger.LogInformation($"Product retrieved");
        return new Response<GetProductDto>(productDto);
    }

    public async Task<Response<GetProductDto>> Update(int id, UpdateProductDto request)
    {
        logger.LogInformation($"Update Product");
        var exist = await repository.GetByIdAsync(id);
        if (exist == null)
        {
            return new Response<GetProductDto>(HttpStatusCode.BadRequest, "Product not found");
        }

        logger.LogInformation($"Product not found");

        exist.Name = request.Name;
        exist.Price = request.Price;
        exist.CreatedAt = request.CreatedAt;

        var result = await repository.UpdateAsync(exist);

        var product = mapper.Map<GetProductDto>(exist);
        await cacheService.DeleteData("products");
        logger.LogInformation($"Product updated");
        return result == 0
            ? new Response<GetProductDto>(HttpStatusCode.BadRequest, "Product not updated!")
            : new Response<GetProductDto>(product);
    }
}