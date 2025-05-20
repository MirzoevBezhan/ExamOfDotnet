using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Interfaces;

namespace Infrastructure.Repositories;

public class CarRepository(DataContext context) : IBaseRepository<Car , int>
{
    public Task<IQueryable<Car>> GetAllAsync()
    {
        var cars = context.Cars.AsQueryable();
        return Task.FromResult(cars);
    }

    public async Task<Car?> GetByIdAsync(int id)
    {
        var student = await context.Cars.FindAsync(id);
        return student;
    }

    public async Task<int> AddAsync(Car entity)
    {
        await context.Cars.AddAsync(entity);
        return await context.SaveChangesAsync();
    }

    public async Task<int> UpdateAsync(Car entity)
    { 
        context.Cars.Update(entity);
        return await context.SaveChangesAsync();
    }

    public async Task<int> DeleteAsync(Car entity)
    {
        context.Cars.Remove(entity);
        return await context.SaveChangesAsync();
    }
}