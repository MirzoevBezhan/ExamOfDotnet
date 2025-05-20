using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Interfaces;

namespace Infrastructure.Repositories;

public class RentalRepository(DataContext context) : IBaseRepository<Rental , int>
{
    public Task<IQueryable<Rental>> GetAllAsync()
    {
        var students = context.Rentals.AsQueryable();
        return Task.FromResult(students);
    }

    public async Task<Rental?> GetByIdAsync(int id)
    {
        var student = await context.Rentals.FindAsync(id);
        return student;
    }

    public async Task<int> AddAsync(Rental entity)
    {
        await context.Rentals.AddAsync(entity);
        return await context.SaveChangesAsync();
    }

    public async Task<int> UpdateAsync(Rental entity)
    { 
        context.Rentals.Update(entity);
        return await context.SaveChangesAsync();
    }

    public async Task<int> DeleteAsync(Rental entity)
    {
        context.Rentals.Remove(entity);
        return await context.SaveChangesAsync();
    }
}