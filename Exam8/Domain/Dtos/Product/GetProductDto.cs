using Domain.Dtos.Order;

namespace Domain.Dtos.Product;

public class GetProductDto : CreateProductDto
{
    public int Id { get; set; }
}