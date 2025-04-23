namespace Domain.DTOs.Product;

public class GetProductsStatisticDto
{
    public int TotalProduct { get; set; }
    public decimal AveragePrice { get; set; }
    public int TotalSold { get; set; }
}