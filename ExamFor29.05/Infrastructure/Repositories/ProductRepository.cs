using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Interfaces;

namespace Infrastructure.Repositories;

public class ProductRepository(DataContext context) : IBaseRepository<Product , int>
{
    public Task<IQueryable<Product>> GetAllAsync()
    {
        var products = context.Products.AsQueryable();
        return Task.FromResult(products);
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        var product = await context.Products.FindAsync(id);
        return product;
    }

    public async Task<int> AddAsync(Product entity)
    {
        await context.Products.AddAsync(entity);
        return await context.SaveChangesAsync();
    }

    public async Task<int> UpdateAsync(Product entity)
    { 
        context.Products.Update(entity);
        return await context.SaveChangesAsync();
    }

    public async Task<int> DeleteAsync(Product entity)
    {
        context.Products.Remove(entity);
        return await context.SaveChangesAsync();
    }
}