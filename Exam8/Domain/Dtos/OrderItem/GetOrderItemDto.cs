using Domain.Dtos.Order;

namespace Domain.Dtos.OrderItem;

public class GetOrderItemDto : CreateOrderDto
{
    public int Id { get; set; }
}