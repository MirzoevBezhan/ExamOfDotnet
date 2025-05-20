using System.Net;
using AutoMapper;
using Domain.Dtos.Branche;
using Domain.Entities;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class BranchService(IBaseRepository<Branch, int> branchService, DataContext context, IMapper mapper)
    : IBranchService
{
    public async Task<Response<GetBranchDto>> CreateAsync(CreateBranchDto request)
    {
        var branch = mapper.Map<Branch>(request);

        var result = await branchService.AddAsync(branch);

        var mapped = mapper.Map<GetBranchDto>(branch);
        return result == 0
            ? new Response<GetBranchDto>(HttpStatusCode.BadRequest, "Branch not added!")
            : new Response<GetBranchDto>(mapped);
    }

    public async Task<Response<string>> DeleteAsync(int id)
    {
        var branch = await branchService.GetByIdAsync(id);
        if (branch == null)
        {
            return new Response<string>(HttpStatusCode.BadRequest, "Branch not found");
        }

        var res = await branchService.DeleteAsync(branch);
        return res == 0
            ? new Response<string>(HttpStatusCode.BadRequest, "Branch not deleted!")
            : new Response<string>("Branch  deleted!");
    }

    public async Task<PagedResponse<List<GetBranchDto>>> GetAllAsync(BranchFilter filter)
    {
        try
        {
            var validFilter = new ValidFilter(filter.PagesNumber, filter.PageSize);

            var branchs = await branchService.GetAllAsync();

            if (filter.Name != null)
            {
              branchs = branchs.Where(c => c.Name.Contains(filter.Name));   
            }

            if (filter.Location != null)
            {
                branchs = branchs.Where(c => c.Location.Contains(filter.Location));
            }

            var maped = mapper.Map<List<GetBranchDto>>(branchs);

            var totalRecords = maped.Count;

            var data = maped
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToList();

            return new PagedResponse<List<GetBranchDto>>(data, validFilter.PageNumber, validFilter.PageSize,
                totalRecords);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Response<GetBranchDto>> GetByIdAsync(int id)
    {
        var exist = await branchService.GetByIdAsync(id);

        if (exist == null)
        {
            return new Response<GetBranchDto>(HttpStatusCode.NotFound, "branch not found!");
        }

        var branchDto = mapper.Map<GetBranchDto>(exist);

        return new Response<GetBranchDto>(branchDto);
    }

    public async Task<Response<GetBranchDto>> UpdateAsync(int id, UpdateBranchDto request)
    {
        var exist = await branchService.GetByIdAsync(id);
        if (exist == null)
        {
            return new Response<GetBranchDto>(HttpStatusCode.BadRequest, "Branch not found");
        }

        exist.Location = request.Location;
        exist.Name = request.Name;

        var result = await branchService.UpdateAsync(exist);

        var branch = mapper.Map<GetBranchDto>(exist);

        return result == 0
            ? new Response<GetBranchDto>(HttpStatusCode.BadRequest, "Branch not updated!")
            : new Response<GetBranchDto>(branch);
    }
}