using Domain.Dtos.Branche;
using Domain.Filters;
using Domain.Responses;

namespace Infrastructure.Interfaces;

public interface IBranchService
{
    Task<PagedResponse<List<GetBranchDto>>> GetAllAsync(BranchFilter filter);
    Task<Response<GetBranchDto>> GetByIdAsync(int id);
    Task<Response<GetBranchDto>> CreateAsync(CreateBranchDto request);
    Task<Response<GetBranchDto>> UpdateAsync(int id, UpdateBranchDto request);
    Task<Response<string>> DeleteAsync(int id);
}