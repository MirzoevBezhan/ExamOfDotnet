using Domain.Dtos.Car;
using Domain.Filters;
using Domain.Responses;

namespace Infrastructure.Interfaces;

public interface ICarService
{
    Task<PagedResponse<List<GetCarDto>>> GetAllAsync(CarFilter filter);
    Task<Response<GetCarDto>> GetByIdAsync(int id);
    Task<Response<GetCarDto>> CreateAsync(CreateCarDto request);
    Task<Response<GetCarDto>> UpdateAsync(int id, UpdateCarDto request);
    Task<Response<string>> DeleteAsync(int id);
}