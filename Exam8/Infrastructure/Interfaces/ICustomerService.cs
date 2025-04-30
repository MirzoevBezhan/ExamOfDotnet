using Domain.Dtos.Customer;
using Domain.Entitites;
using Domain.Filters;
using Domain.Responces;
using Domain.Responses;

namespace Infrastructure.Interfaces;

public interface ICustomerService
{
    Task<PagedResponse<List<GetCustomerDto>>> GetAll(CustomerFilter filter);
    Task<Response<GetCustomerDto>> GetById(int id);
    Task<Response<GetCustomerDto>> Update(int id,UpdateCustomerDto updateCustomerDto);
    Task<Response<GetCustomerDto>> Create(CreateCustomerDto createCustomerDto);
    Task<Response<string>> Delete(int id);
}