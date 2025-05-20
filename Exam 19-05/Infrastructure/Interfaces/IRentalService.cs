using Domain.Dtos.Rental;
using Domain.Dtos.Rental;
using Domain.Filters;
using Domain.Responses;

namespace Infrastructure.Interfaces;

public interface IRentalService
{
    Task<PagedResponse<List<GetRentalDto>>> GetAllAsync(RentalFilter filter);
    Task<Response<GetRentalDto>> GetByIdAsync(int id);
    Task<Response<GetRentalDto>> CreateAsync(CreateRentalDto request);
    Task<Response<GetRentalDto>> UpdateAsync(int id, UpdateRentalDto request);
    Task<Response<string>> DeleteAsync(int id);
}