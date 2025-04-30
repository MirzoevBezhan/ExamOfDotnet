using Domain.Entitites;

namespace Domain.Dtos.Order;

public class CreateOrderDto
{
    public int CustomerId { get; set; }
    public DateTime OrderDate { get; set; }
}