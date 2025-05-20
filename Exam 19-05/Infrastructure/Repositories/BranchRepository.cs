using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Interfaces;

namespace Infrastructure.Repositories;

public class BranchRepository(DataContext context) : IBaseRepository<Branch , int>
{
    public Task<IQueryable<Branch>> GetAllAsync()
    {
        var students = context.Branches.AsQueryable();
        return Task.FromResult(students);
    }

    public async Task<Branch?> GetByIdAsync(int id)
    {
        var student = await context.Branches.FindAsync(id);
        return student;
    }

    public async Task<int> AddAsync(Branch entity)
    {
        await context.Branches.AddAsync(entity);
        return await context.SaveChangesAsync();
    }

    public async Task<int> UpdateAsync(Branch entity)
    { 
        context.Branches.Update(entity);
        return await context.SaveChangesAsync();
    }

    public async Task<int> DeleteAsync(Branch entity)
    {
        context.Branches.Remove(entity);
        return await context.SaveChangesAsync();
    }
}