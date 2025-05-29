namespace Domain.DTOs.User.Product;

public class CreateProductDto
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public DateTime? CreatedAt { get; set; } = DateTime.Now;
}