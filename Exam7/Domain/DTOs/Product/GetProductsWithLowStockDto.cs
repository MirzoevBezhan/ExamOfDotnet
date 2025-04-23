namespace Domain.DTOs.Product;

public class GetProductsWithLowStockDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int QuantityStock { get; set; }
}