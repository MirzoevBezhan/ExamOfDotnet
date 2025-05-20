using Domain.DTOs;
using Domain.Dtos.Branche;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BranchesController(IBranchService service)
{
    [HttpGet("int:id")]
    public async Task<Response<GetBranchDto>> Get(int id)
    {
        return await service.GetByIdAsync(id);
    }

    [HttpGet]
    public async Task<PagedResponse<List<GetBranchDto>>> GetAll(BranchFilter filter)
    {
        return await service.GetAllAsync(filter);
    }

    [HttpPut("int:id")]
    public async Task<Response<GetBranchDto>> Update(int id, UpdateBranchDto update)
    {
        return await service.UpdateAsync(id, update);
    }

    [HttpDelete("int:id")]
    public async Task<Response<string>> Delete(int id)
    {
        return await service.DeleteAsync(id);
    }

    [HttpPost]
    public async Task<Response<GetBranchDto>> Create(CreateBranchDto create)
    {
        return await service.CreateAsync(create);
    }
}