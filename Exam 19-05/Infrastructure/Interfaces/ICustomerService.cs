using Domain.Dtos.Customer;
using Domain.Filters;
using Domain.Responses;

namespace Infrastructure.Interfaces;

public interface ICustomerService
{
    Task<PagedResponse<List<GetCustomerDto>>> GetAllAsync(CustomerFilter filter);
    Task<Response<GetCustomerDto>> GetByIdAsync(int id);
    Task<Response<GetCustomerDto>> CreateAsync(CreateCustomerDto request);
    Task<Response<GetCustomerDto>> UpdateAsync(int id, UpdateCustomerDto request);
    Task<Response<string>> DeleteAsync(int id);
}