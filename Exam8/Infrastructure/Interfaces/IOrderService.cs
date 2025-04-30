using Domain.Dtos.Customer;
using Domain.Dtos.Order;
using Domain.Filters;
using Domain.Responces;
using Domain.Responses;

namespace Infrastructure.Interfaces;

public interface IOrderService
{
    Task<PagedResponse<List<GetOrderDto>>> GetAll(OrderFilter filter);
    Task<Response<GetOrderDto>> GetById(int id);
    Task<Response<GetOrderDto>> Update(int id,UpdateOrderDto updateOrderDto);
    Task<Response<GetOrderDto>> Create(CreateOrderDto createOrderDto);
    Task<Response<string>> Delete(int id);
}