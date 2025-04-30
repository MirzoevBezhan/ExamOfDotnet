using Domain.Dtos.OrderItem;
using Domain.Filters;
using Domain.Responces;
using Domain.Responses;

namespace Infrastructure.Interfaces;

public interface IOrderItemService
{
    Task<PagedResponse<List<GetOrderItemDto>>> GetAll(OrderItemFilter filter);
    Task<Response<GetOrderItemDto>> GetById(int id);
    Task<Response<GetOrderItemDto>> Update(int id,UpdateOrderItemDto GetOrderItemDto);
    Task<Response<GetOrderItemDto>> Create(CreateOrderItemDto GetOrderItemDto);
    Task<Response<string>> Delete(int id);
}