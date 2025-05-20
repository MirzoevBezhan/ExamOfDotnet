
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Interfaces;
namespace Infrastructure.Repositories;


public class CustomerRepository(DataContext context) : IBaseRepository<Customer , int>
{
    public Task<IQueryable<Customer>> GetAllAsync()
    {
        var students = context.Customers.AsQueryable();
        return Task.FromResult(students);
    }

    public async Task<Customer?> GetByIdAsync(int id)
    {
        var student = await context.Customers.FindAsync(id);
        return student;
    }

    public async Task<int> AddAsync(Customer entity)
    {
        await context.Customers.AddAsync(entity);
        return await context.SaveChangesAsync();
    }

    public async Task<int> UpdateAsync(Customer entity)
    { 
        context.Customers.Update(entity);
        return await context.SaveChangesAsync();
    }

    public async Task<int> DeleteAsync(Customer entity)
    {
        context.Customers.Remove(entity);
        return await context.SaveChangesAsync();
    }
}