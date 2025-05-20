using Domain.Dtos.Customer;
using Domain.Filters;
using Domain.Responces;
using Domain.Responses;

namespace Infrastructure.Interfases;

public interface ICustomerService
{
    Task<PagedResponse<List<GetCustomerDto>>> GetAll(CustomerFilter filter);
    Task<Response<GetCustomerDto>> Create(CreateCustomerDto customerDto);
    Task<Response<GetCustomerDto>> Update(int id,UpdateCustomerDto customerDto);
    Task<Response<GetCustomerDto>> Get(int id);
    Task<Response<string>> Delete(int id);
}